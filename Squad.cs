using System;
using System.Collections.Generic;
using System.Linq;
using Lua;
using Pathfinding;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class Squad
{
	// Token: 0x06000539 RID: 1337 RVA: 0x000053A6 File Offset: 0x000035A6
	public Squad(IEnumerable<Actor> actors, SpawnPoint lastSpawnPoint, Order order, Vehicle squadVehicle, float timeUntilReady) : this((from a in actors
	select a.controller).ToList<ActorController>(), lastSpawnPoint, order, squadVehicle, timeUntilReady)
	{
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00059E9C File Offset: 0x0005809C
	public Squad(List<ActorController> members, SpawnPoint lastSpawnPoint, Order order, Vehicle squadVehicle, float timeUntilReady)
	{
		this.number = Squad.nextNumber++;
		this.members = new List<ActorController>();
		this.aiMembers = new List<AiActorController>();
		this.leader = members[0];
		this.team = this.leader.actor.team;
		this.lastReachedSpawnPoint = lastSpawnPoint;
		this.disbanded = false;
		if (this.HasPlayerLeader())
		{
			this.formationWidth = 3f;
			this.formationDepth = 3f;
			this.acceptedFollowerLagDistance = 15f;
		}
		else
		{
			this.formationWidth = UnityEngine.Random.Range(3f, 5f);
			this.formationDepth = UnityEngine.Random.Range(2f, 4f);
			this.acceptedFollowerLagDistance = 80f;
		}
		foreach (ActorController a in members)
		{
			this.AddMember(a);
		}
		this.draggedPosition = this.leader.actor.Position() - this.leader.actor.transform.forward * 6f;
		this.readyTime = Time.time + timeUntilReady;
		this.AssignOrder(order);
		if (squadVehicle != null)
		{
			this.ClaimVehicle(squadVehicle);
			this.EnterVehicle(squadVehicle);
		}
		this.leaderStandStillTimeout.Start();
		this.temporaryCoverCooldownAction.Start();
		ActorManager.RegisterSquad(this);
		RavenscriptManager.events.onSquadCreated.Invoke(this);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x000053DE File Offset: 0x000035DE
	public void SetFormation(Squad.FormationType formation)
	{
		this.formation = formation;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0005A1A0 File Offset: 0x000583A0
	public void SetRandomFormation()
	{
		Squad.FormationType formationType = (Squad.FormationType)UnityEngine.Random.Range(0, 7);
		this.SetFormation(formationType);
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x000053E7 File Offset: 0x000035E7
	public void SetCustomFormation(Vector2[] formation)
	{
		this.customFormation = formation;
		this.formation = Squad.FormationType.Custom;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x000053F7 File Offset: 0x000035F7
	public void SetFormationSize(float width, float depth)
	{
		this.formationWidth = width;
		this.formationDepth = depth;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0005A1BC File Offset: 0x000583BC
	public bool MayEngageTarget(Actor actor)
	{
		if (actor == this.attackTarget)
		{
			return true;
		}
		if (this.engagement == Squad.EngagementType.None)
		{
			return false;
		}
		if (this.engagement == Squad.EngagementType.OnlyAlerted)
		{
			AiActorController aiActorController = actor.controller as AiActorController;
			return aiActorController == null || (aiActorController.IsAlert() && !aiActorController.slowTargetDetection);
		}
		return true;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0005A21C File Offset: 0x0005841C
	public void AssignOrder(Order order)
	{
		if (this.order != null)
		{
			this.DropOrder();
		}
		this.order = order;
		this.isCarryingOutOrder = (order == null);
		this.orderPriorityModifier = 0;
		if (this.order != null)
		{
			RavenscriptManager.events.onSquadAssignedNewOrder.Invoke(this, order);
		}
		else
		{
			RavenscriptManager.events.onSquadFailedToAssignNewOrder.Invoke(this);
		}
		if (this.order != null)
		{
			if (this.order.source != null)
			{
				this.lastReachedSpawnPoint = this.order.source;
			}
			this.remainingMovementReissues = 2;
			this.squadHasbeenDroppedFromTransportVehicle = false;
			if (this.order.type == Order.OrderType.Defend)
			{
				this.defenseTimeoutAction.StartLifetime(UnityEngine.Random.Range(10f, 25f));
			}
			if (this.order.type == Order.OrderType.Roam && !this.HasSquadVehicle())
			{
				Vehicle availableRoamingVehicle = this.order.source.GetAvailableRoamingVehicle();
				if (availableRoamingVehicle != null)
				{
					this.ClaimVehicle(availableRoamingVehicle);
					this.EnterVehicle(availableRoamingVehicle);
				}
			}
		}
		this.ModifyOrderPriority(-this.members.Count);
		this.leaderStandStillTimeout.Start();
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0005A33C File Offset: 0x0005853C
	private void DropOrder()
	{
		if (this.order != null)
		{
			foreach (AiActorController aiActorController in this.aiMembers)
			{
				aiActorController.CancelOrder(this.order);
			}
			this.order.ResetPriorityModifier(this);
		}
		this.order = null;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00005407 File Offset: 0x00003607
	private void ModifyOrderPriority(int modifier)
	{
		if (this.order != null)
		{
			this.order.ModifyPriority(this, modifier);
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0000541E File Offset: 0x0000361E
	public void PlayerSquadTakeOverSquadVehicle(Vehicle vehicle)
	{
		if (!vehicle.canBeTakenOverByPlayerSquad)
		{
			return;
		}
		vehicle.KickOutSquad();
		vehicle.SquadClaim(this);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00005436 File Offset: 0x00003636
	private bool IsMember(Actor actor)
	{
		return this.members.Contains(actor.controller);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00005449 File Offset: 0x00003649
	public void OnResupplyThrown()
	{
		this.resupplyThrownAction.Start();
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00005456 File Offset: 0x00003656
	public void OnMedipackThrown()
	{
		this.medipackThrownAction.Start();
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00005463 File Offset: 0x00003663
	public bool ResupplyCoolingDown()
	{
		return !this.resupplyThrownAction.TrueDone();
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00005473 File Offset: 0x00003673
	public bool MedipackCoolingDown()
	{
		return !this.medipackThrownAction.TrueDone();
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00005483 File Offset: 0x00003683
	public Vector3 GetCurrentWaypoint()
	{
		return this.order.waypoints[this.currentWaypoint];
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0005A3B0 File Offset: 0x000585B0
	public void SetCurrentWaypointToClosest()
	{
		int num = 0;
		float num2 = float.MaxValue;
		Vector3 a = this.Leader().actor.Position();
		for (int i = 0; i < this.order.waypoints.Length; i++)
		{
			float magnitude = (a - this.order.waypoints[i]).ToGround().magnitude;
			if (magnitude < num2)
			{
				num = i;
				num2 = magnitude;
			}
		}
		this.currentWaypoint = num;
		if (num2 < 10f)
		{
			this.NextWaypoint();
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0000549B File Offset: 0x0000369B
	public void NextWaypoint()
	{
		this.currentWaypoint = (this.currentWaypoint + 1) % this.order.waypoints.Length;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0005A434 File Offset: 0x00058634
	public bool PlayerOrderEnterVehicle(Vehicle vehicle)
	{
		int num = 0;
		for (int i = 0; i < vehicle.seats.Count; i++)
		{
			if (!vehicle.seats[i].IsOccupied() || !this.IsMember(vehicle.seats[i].occupant))
			{
				num++;
			}
		}
		bool result = false;
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if ((!aiActorController.HasTargetVehicle() || aiActorController.targetVehicle.claimingSquad != this) && num > 0)
			{
				aiActorController.GotoAndEnterVehicle(vehicle, false);
				num--;
				result = true;
			}
		}
		this.leader.LookAt(vehicle.transform.position);
		this.leader.actor.EmoteMove();
		return result;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000054B9 File Offset: 0x000036B9
	public void ClaimVehicle(Vehicle vehicle)
	{
		this.squadVehicle = vehicle;
		this.leavingBurningVehicle = false;
		this.outsideTransportVehicle = false;
		vehicle.SquadClaim(this);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000054D7 File Offset: 0x000036D7
	public bool Ready()
	{
		return Time.time > this.readyTime;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0005A520 File Offset: 0x00058720
	public void AddMember(ActorController a)
	{
		AiActorController aiActorController = a as AiActorController;
		if (aiActorController != null)
		{
			aiActorController.squadMemberIndex = this.members.Count;
			aiActorController.isSquadLeader = false;
			this.aiMembers.Add(aiActorController);
		}
		this.members.Add(a);
		a.OnAssignedToSquad(this);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0005A574 File Offset: 0x00058774
	public void DropMember(ActorController a)
	{
		this.members.Remove(a);
		AiActorController aiActorController = a as AiActorController;
		if (aiActorController != null)
		{
			if (this.order != null)
			{
				aiActorController.CancelOrder(this.order);
			}
			this.aiMembers.Remove(aiActorController);
		}
		if (this.disbanded)
		{
			return;
		}
		if (this.members.Count == 0)
		{
			this.Disband();
			return;
		}
		for (int i = 0; i < this.members.Count; i++)
		{
			aiActorController = (this.members[i] as AiActorController);
			if (aiActorController != null)
			{
				aiActorController.squadMemberIndex = i;
				if (i == 0)
				{
					aiActorController.isSquadLeader = true;
				}
			}
		}
		this.ModifyOrderPriority(1);
		a.OnDroppedFromSquad();
		if (this.leader == a)
		{
			this.leader = this.members[0];
			this.NewLeader();
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x000054E6 File Offset: 0x000036E6
	public ActorController Leader()
	{
		return this.leader;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0005A654 File Offset: 0x00058854
	private void NewLeader()
	{
		if (this.order != null)
		{
			this.ReissueLastMoveSegment();
		}
		if (this.HasSquadVehicle() && !this.squadVehicle.dead && !this.leader.actor.IsDriver())
		{
			this.leader.FillDriverSeat();
		}
		this.leaderStandStillTimeout.Start();
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0005A6AC File Offset: 0x000588AC
	public Actor GetTarget()
	{
		foreach (ActorController actorController in this.members)
		{
			if (actorController.HasSpottedTarget() && actorController.GetTarget().team != this.Leader().actor.team)
			{
				return actorController.GetTarget();
			}
		}
		return null;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000054EE File Offset: 0x000036EE
	public void OnLeaderPathDone()
	{
		if (this.activePathSegment != null && this.activePathSegment.exitVehicleOnComplete)
		{
			this.ExitVehicle();
		}
		this.activePathSegment = null;
		if (this.HasQueuedMovePathSegments())
		{
			this.DequeueMovePathSegment();
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0005A72C File Offset: 0x0005892C
	private void MoveToSpawnPoint(SpawnPoint spawnPoint)
	{
		if (this.HasSquadVehicle() && this.squadVehicle.IsAmphibious())
		{
			try
			{
				SpawnPoint spawnPoint2 = ActorManager.ClosestSpawnPoint(this.leader.actor.Position());
				bool flag = false;
				bool flag2 = spawnPoint2.landingZones.Count > 0;
				bool flag3 = spawnPoint.landingZones.Count > 0;
				SpawnPointNeighborManager.SpawnPointNeighbor neighborInfo = SpawnPointNeighborManager.GetNeighborInfo(spawnPoint2, spawnPoint);
				bool flag4 = this.squadVehicle.IsInWater() || (neighborInfo != null && neighborInfo.waterConnection);
				if (spawnPoint2 == spawnPoint)
				{
					if (!this.squadVehicle.IsInWater())
					{
						this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
						return;
					}
				}
				else if (!this.squadVehicle.IsInWater())
				{
					if ((!flag4 || (!flag2 && !flag3) || (spawnPoint.closestCarNavmeshArea == spawnPoint2.closestCarNavmeshArea && UnityEngine.Random.Range(0f, 1f) < 0.7f)) && SpawnPointNeighborManager.HasLandConnection(spawnPoint2, spawnPoint))
					{
						this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
						return;
					}
					flag = true;
				}
				List<Squad.MovePathSegment> list = new List<Squad.MovePathSegment>();
				if (flag)
				{
					LandingZone landingZone;
					if (flag2)
					{
						landingZone = spawnPoint2.landingZones[UnityEngine.Random.Range(0, spawnPoint2.landingZones.Count)];
					}
					else
					{
						landingZone = ActorManager.ClosestLandingZone(this.Leader().actor.Position());
						if (landingZone == null)
						{
							this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
							return;
						}
					}
					Squad.MovePathSegment item = new Squad.MovePathSegment(landingZone.transform.position);
					list.Add(item);
				}
				LandingZone landingZone2;
				if (flag3)
				{
					landingZone2 = spawnPoint.landingZones[UnityEngine.Random.Range(0, spawnPoint.landingZones.Count)];
				}
				else
				{
					landingZone2 = ActorManager.ClosestLandingZone(spawnPoint.transform.position);
					if (landingZone2 == null)
					{
						this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
						return;
					}
				}
				Squad.MovePathSegment movePathSegment = new Squad.MovePathSegment(landingZone2.transform.position);
				movePathSegment.ForceGraphType(PathfindingBox.Type.Boat);
				list.Add(movePathSegment);
				list.Add(new Squad.MovePathSegment(spawnPoint.RandomPositionInCaptureZone()));
				this.IssueMovePathSegmentQueue(list.ToArray());
				return;
			}
			catch (Exception)
			{
				this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
				return;
			}
		}
		if (this.HasSquadVehicle() && this.squadVehicle.IsWatercraft())
		{
			int count = spawnPoint.landingZones.Count;
			if (count > 0)
			{
				LandingZone landingZone3 = spawnPoint.landingZones[UnityEngine.Random.Range(0, count)];
				Vector3 landingPoint = landingZone3.GetLandingPoint();
				Squad.MovePathSegment movePathSegment2 = new Squad.MovePathSegment(landingPoint);
				movePathSegment2.AppendNode(landingPoint + landingZone3.transform.forward * 6f);
				movePathSegment2.exitVehicleOnComplete = true;
				Squad.MovePathSegment movePathSegment3 = new Squad.MovePathSegment(spawnPoint.RandomPositionInCaptureZone());
				this.IssueMovePathSegmentQueue(new Squad.MovePathSegment[]
				{
					movePathSegment2,
					movePathSegment3
				});
				return;
			}
			this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
			return;
		}
		else
		{
			this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0005AA30 File Offset: 0x00058C30
	public void MoveTo(Vector3 position)
	{
		Squad.MovePathSegment segment = new Squad.MovePathSegment(position);
		this.ClearMovePathQueue();
		this.IssueMovePathSegment(segment);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00005520 File Offset: 0x00003720
	public void ReissueLastMoveSegment()
	{
		if (this.activePathSegment != null)
		{
			this.IssueMovePathSegment(this.activePathSegment);
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0005AA54 File Offset: 0x00058C54
	private void IssueMovePathSegment(Squad.MovePathSegment segment)
	{
		this.activePathSegment = segment;
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			aiActorController.LeaveCover();
			if (aiActorController.actor.IsSeated() && aiActorController.actor.seat.vehicle.claimingSquad != this)
			{
				aiActorController.LeaveVehicle(false);
			}
		}
		this.leader.LookAt(segment.destination);
		this.leader.actor.EmoteMove();
		AiActorController aiActorController2 = this.leader as AiActorController;
		if (aiActorController2 != null)
		{
			aiActorController2.Goto(segment, false);
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0005AB18 File Offset: 0x00058D18
	public void IssueMovePathSegmentQueue(params Squad.MovePathSegment[] segments)
	{
		this.ClearMovePathQueue();
		this.IssueMovePathSegment(segments[0]);
		for (int i = 1; i < segments.Length; i++)
		{
			this.pathSegmentQueue.Enqueue(segments[i]);
		}
		Vector3 start = this.leader.transform.position;
		foreach (Squad.MovePathSegment movePathSegment in segments)
		{
			Debug.DrawLine(start, movePathSegment.destination, Color.blue, 100f);
			start = movePathSegment.destination;
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00005536 File Offset: 0x00003736
	public bool HasQueuedMovePathSegments()
	{
		return this.pathSegmentQueue.Count > 0;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00005546 File Offset: 0x00003746
	public void DequeueMovePathSegment()
	{
		this.IssueMovePathSegment(this.pathSegmentQueue.Dequeue());
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00005559 File Offset: 0x00003759
	private void ClearMovePathQueue()
	{
		this.pathSegmentQueue.Clear();
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0005AB98 File Offset: 0x00058D98
	public void DefendSpawn(SpawnPoint spawn)
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (!this.HasSquadVehicle())
			{
				aiActorController.LeaveCover();
				if (!aiActorController.HasTargetVehicle() || !aiActorController.actor.IsSeated() || !spawn.IsValidDefenseTurret(aiActorController.targetVehicle))
				{
					Vehicle availableTurret = spawn.GetAvailableTurret();
					if (aiActorController.HasTargetVehicle())
					{
						aiActorController.LeaveVehicle(false);
					}
					if (availableTurret != null)
					{
						aiActorController.GotoAndEnterVehicle(availableTurret, false);
					}
					else
					{
						CoverPoint availableCoverPoint = spawn.GetAvailableCoverPoint();
						if (availableCoverPoint != null)
						{
							aiActorController.EnterCover(availableCoverPoint);
						}
						else
						{
							aiActorController.FindCoverAwayFrom(spawn.transform.position);
						}
					}
				}
			}
			else
			{
				this.MoveToSpawnPoint(spawn);
			}
		}
		this.blockingDefenderPointCapture = false;
		this.trackingDownPointAttacker = false;
		this.leader.actor.EmoteHalt();
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0005ACA0 File Offset: 0x00058EA0
	public void BlockSpawnCapture(SpawnPoint spawn)
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (aiActorController.actor.IsSeated() && aiActorController.actor.seat.vehicle.isTurret)
			{
				aiActorController.LeaveVehicle(false);
			}
			aiActorController.Goto(this.order.target.RandomPositionInCaptureZone(), false);
		}
		this.blockingDefenderPointCapture = true;
		this.trackingDownPointAttacker = false;
		this.blockSpawnCaptureAction.Start();
		this.leader.actor.EmoteRegroup();
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00005566 File Offset: 0x00003766
	public void Regroup()
	{
		this.shouldFollow = true;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0000556F File Offset: 0x0000376F
	public void Ungroup()
	{
		this.shouldFollow = false;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0005AD58 File Offset: 0x00058F58
	public void PlayerOrderMoveTo(Vector3 point)
	{
		List<AiActorController> list = new List<AiActorController>();
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (!aiActorController.HasTargetVehicle() || !aiActorController.IsEnteringVehicle())
			{
				list.Add(aiActorController);
			}
		}
		foreach (AiActorController aiActorController2 in list)
		{
			aiActorController2.LeaveCover();
			aiActorController2.actor.EmoteHail();
			float num = 3f;
			if (aiActorController2.actor.IsSeated())
			{
				num += aiActorController2.actor.seat.vehicle.pathingRadius * 2f;
			}
			aiActorController2.Goto(point + UnityEngine.Random.insideUnitSphere.ToGround().normalized * num, false);
			aiActorController2.MarkCompletingPlayerMoveOrder();
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0005AE6C File Offset: 0x0005906C
	public void MoveToAndDigInToProtect(Vector3 point)
	{
		foreach (ActorController actorController in this.members)
		{
			actorController.LeaveCover();
		}
		foreach (ActorController actorController2 in this.members)
		{
			actorController2.FindCoverAtPoint(point);
			if (actorController2 == this.leader)
			{
				actorController2.actor.EmoteHalt();
			}
			else
			{
				actorController2.actor.EmoteHail();
			}
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0005AF28 File Offset: 0x00059128
	public void DigIn()
	{
		foreach (ActorController actorController in this.members)
		{
			actorController.LeaveCover();
		}
		foreach (ActorController actorController2 in this.members)
		{
			actorController2.FindCover();
			if (actorController2 == this.leader)
			{
				actorController2.actor.EmoteHalt();
			}
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0005AFD4 File Offset: 0x000591D4
	public void DigInTowards(Vector3 direction)
	{
		foreach (ActorController actorController in this.members)
		{
			actorController.LeaveCover();
		}
		foreach (ActorController actorController2 in this.members)
		{
			actorController2.FindCoverTowards(direction);
			if (actorController2 == this.leader)
			{
				actorController2.actor.EmoteHalt();
			}
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0005B080 File Offset: 0x00059280
	public void EnterVehicle(Vehicle vehicle)
	{
		if (vehicle.seats.Count < this.aiMembers.Count)
		{
			int n = this.aiMembers.Count - vehicle.seats.Count;
			this.KickMembersFromSquad(n);
		}
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			aiActorController.GotoAndEnterVehicle(vehicle, false);
		}
		this.leader.LookAt(vehicle.transform.position);
		this.leader.actor.EmoteMove();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0005B130 File Offset: 0x00059330
	public bool IsEnteringSquadVehicle()
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (!aiActorController.HasTargetVehicle() || aiActorController.targetVehicle != this.squadVehicle)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0005B1A0 File Offset: 0x000593A0
	public void ExitVehicle()
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			aiActorController.LeaveVehicle(false);
		}
		this.DropSquadVehicleClaim();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00005578 File Offset: 0x00003778
	public void DropSquadVehicleClaim()
	{
		if (this.HasSquadVehicle())
		{
			this.squadVehicle.DropSquadClaim();
		}
		this.squadVehicle = null;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0005B1F8 File Offset: 0x000593F8
	public void PlayerOrderExitVehicle(Vehicle vehicle)
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (aiActorController.targetVehicle == vehicle)
			{
				aiActorController.LeaveVehicle(false);
			}
		}
		if (vehicle.claimingSquad != null && vehicle.claimingSquad == FpsActorController.instance.playerSquad)
		{
			vehicle.DropSquadClaim();
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00005594 File Offset: 0x00003794
	public void ContinueOnFoot()
	{
		this.ExitVehicle();
		this.ReissueLastMoveSegment();
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000055A2 File Offset: 0x000037A2
	public Squad KickMembersFromSquad(int n)
	{
		if (n > 0)
		{
			return this.SplitSquad(this.members.GetRange(this.members.Count - n, n));
		}
		return null;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0005B27C File Offset: 0x0005947C
	public bool IsTakingFire()
	{
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsTakingFire())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0005B2D8 File Offset: 0x000594D8
	public bool AllSeated()
	{
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.actor.IsSeated())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0005B338 File Offset: 0x00059538
	private void Disband()
	{
		this.disbanded = true;
		if (this.order != null)
		{
			this.DropOrder();
		}
		if (this.HasSquadVehicle())
		{
			this.squadVehicle.DropSquadClaim();
		}
		if (this.hostCluster != null)
		{
			this.hostCluster.Destroy();
		}
		if (this.parentCluster != null)
		{
			this.LeaveParentCluster();
		}
		ActorManager.RemoveSquad(this);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0005B394 File Offset: 0x00059594
	public bool CanLeadCluster()
	{
		return this.parentCluster == null && this.squadVehicle != null && this.squadVehicle.GetType() == typeof(ArcadeCar) && !this.squadVehicle.IsInWater() && this.EveryoneSeatedInSquadVehicle();
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x000055C9 File Offset: 0x000037C9
	public void JoinParentCluster(SquadCluster cluster)
	{
		this.parentCluster = cluster;
		this.parentCluster.AddChild(this);
		if (this.hostCluster != null)
		{
			this.hostCluster.Destroy();
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000055F1 File Offset: 0x000037F1
	public void LeaveParentCluster()
	{
		this.parentCluster.RemoveChild(this);
		this.parentCluster = null;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00005606 File Offset: 0x00003806
	public void CreateHostCluster()
	{
		this.hostCluster = new SquadCluster(this);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00005614 File Offset: 0x00003814
	public bool IsGroupedUp()
	{
		return this.groupedUp;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0005B3E8 File Offset: 0x000595E8
	public bool NoAiMemberHasPath()
	{
		using (List<AiActorController>.Enumerator enumerator = this.aiMembers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasPath())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0000561C File Offset: 0x0000381C
	public bool IsInTransportVehicle()
	{
		return this.HasSquadVehicle() && this.squadVehicle.aiType == Vehicle.AiType.Transport;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00005636 File Offset: 0x00003836
	private bool OrderNeedsContinousMovementUpdate()
	{
		return this.order != null && this.order.targetSquad != null;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0005B444 File Offset: 0x00059644
	public void Update()
	{
		if (this.attackTarget != null && this.attackTarget.dead)
		{
			this.attackTarget = null;
		}
		if (this.OrderNeedsContinousMovementUpdate() && this.justIssuedMovementAction.TrueDone())
		{
			this.IssueMovement();
		}
		bool flag = false;
		this.anyMemberIsInTemporaryCover = false;
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			flag |= (aiActorController.IsTakingFire() || aiActorController.JustFired());
			if (aiActorController.IsInTemporaryCover())
			{
				this.anyMemberIsInTemporaryCover = true;
				break;
			}
		}
		if (flag)
		{
			this.isInCombatAction.Start();
		}
		if (!this.anyMemberIsInTemporaryCover)
		{
			float lifetime = 16f;
			if (this.order != null && this.order.type == Order.OrderType.Attack && this.order.target != null && Vector3.Distance(this.Leader().actor.Position(), this.order.target.transform.position) < this.order.target.GetCaptureRange() + 40f)
			{
				lifetime = 6f;
			}
			this.takingTemporaryCoverAction.StartLifetime(lifetime);
		}
		if (this.takingTemporaryCoverAction.TrueDone() && this.temporaryCoverCooldownAction.TrueDone())
		{
			this.temporaryCoverCooldownAction.Start();
		}
		if (!this.HasPlayerLeader())
		{
			if (this.scriptedLanding)
			{
				this.UpdateScriptedLanding();
			}
			if (this.order == null && this.allowRequestNewOrders)
			{
				this.RequestNewOrder();
				return;
			}
			if (this.Ready())
			{
				this.UpdateOrders();
			}
			if (this.HasSquadVehicle())
			{
				this.UpdateVehicleStatus();
			}
		}
		this.closestEnemySpawnPointDistance = 9999f;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.owner == 1 - this.team)
			{
				float magnitude = (spawnPoint.transform.position - this.Leader().actor.Position()).ToGround().magnitude;
				if (magnitude < this.closestEnemySpawnPointDistance)
				{
					this.closestEnemySpawnPointDistance = magnitude;
				}
			}
		}
		this.UpdateGroupedUpFlag();
		Vector3 b = Vector3.ClampMagnitude((this.draggedPosition - this.leader.transform.position).ToGround(), 6f);
		this.draggedPosition = this.leader.transform.position + b;
		if (this.hostCluster != null)
		{
			for (int j = 0; j < 32; j++)
			{
				float f = (float)j / 16f * 3.1415927f;
				float f2 = ((float)j + 1f) / 16f * 3.1415927f;
				float radius = this.hostCluster.GetRadius();
				Vector3 start = this.Leader().actor.Position() + new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f)) * radius;
				Vector3 end = this.Leader().actor.Position() + new Vector3(Mathf.Sin(f2), 0f, Mathf.Cos(f2)) * radius;
				Debug.DrawLine(start, end, Color.magenta, 0.5f);
			}
			return;
		}
		if (this.parentCluster != null)
		{
			Debug.DrawLine(this.Leader().actor.Position(), this.parentCluster.host.Leader().actor.Position(), Color.magenta, 0.5f);
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00005650 File Offset: 0x00003850
	public bool AllowTakeTemporaryCover()
	{
		return !this.leader.actor.parachuteDeployed && this.temporaryCoverCooldownAction.TrueDone();
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0005B7F0 File Offset: 0x000599F0
	private void UpdateOrders()
	{
		if (this.HasSquadVehicle() && !this.EveryoneSeatedInSquadVehicle() && !this.outsideTransportVehicle && !this.IsEnteringSquadVehicle())
		{
			this.EnterVehicle(this.squadVehicle);
		}
		if (!this.isCarryingOutOrder && this.ReadyToCarryOutOrder())
		{
			this.CarryOutOrder();
		}
		if (this.allowRequestNewOrders && this.OrderIsDone())
		{
			this.RequestNewOrder();
			if (this.order == null)
			{
				return;
			}
		}
		if (!this.isCarryingOutOrder || !this.ReadyToCarryOutOrder() || this.OrderIsDone() || this.leader.HasPath())
		{
			this.leaderStandStillTimeout.Start();
		}
		if (this.leaderStandStillTimeout.TrueDone())
		{
			if (this.order.type == Order.OrderType.Defend && this.trackingDownPointAttacker)
			{
				this.BlockSpawnCapture(this.order.target);
				this.trackingDownPointAttacker = false;
			}
			if (this.CompletingOrderStandingStill())
			{
				this.leaderStandStillTimeout.Start();
			}
			else if (this.remainingMovementReissues > 0)
			{
				this.remainingMovementReissues -= 1;
				this.IssueMovement();
			}
			else if (!this.HasPlayerLeader() && this.aiMembers[0].modifier.dieOnMovementFail)
			{
				ActorController[] array = this.members.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].actor.Kill(DamageInfo.Default);
				}
			}
			else if (this.allowRequestNewOrders)
			{
				this.RequestNewOrder();
			}
			else
			{
				this.remainingMovementReissues = 1;
			}
		}
		if (this.order != null)
		{
			if (this.order.type == Order.OrderType.Defend)
			{
				this.UpdateDefense();
			}
			else if (this.order.type == Order.OrderType.PatrolWaypoints)
			{
				this.UpdatePatrolWaypoints();
			}
			if (this.isCarryingOutOrder && this.order.type == Order.OrderType.Roam)
			{
				if (!this.HasSquadVehicle())
				{
					this.RequestNewOrder();
					return;
				}
				bool flag = this.Leader().HasPath() && Vector3.Distance(this.Leader().actor.Position(), this.Leader().PathEndPoint()) < this.squadVehicle.roamCompleteDistance;
				if (this.roamKeepTargetAction.TrueDone() && (!this.Leader().HasPath() || this.roamChangeTargetAction.TrueDone() || flag))
				{
					this.RoamToNewTarget();
				}
			}
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0005BA3C File Offset: 0x00059C3C
	private bool CompletingOrderStandingStill()
	{
		if (this.order.type == Order.OrderType.Repair)
		{
			return this.aiMembers[0].IsRepairing();
		}
		if (this.order.type == Order.OrderType.Defend)
		{
			return true;
		}
		if (this.order.type == Order.OrderType.Attack)
		{
			if (this.aiMembers[0].IsInTemporaryCover())
			{
				return true;
			}
			if (this.order.hasOverrideTargetPosition)
			{
				return this.FlatDistanceToOverrideTargetPosition() < 10f;
			}
			return this.IsInsideTargetOrderCaptureRadius();
		}
		else
		{
			if (this.order.type != Order.OrderType.Move)
			{
				return false;
			}
			if (this.aiMembers[0].IsInTemporaryCover())
			{
				return true;
			}
			if (this.order.hasOverrideTargetPosition)
			{
				return this.FlatDistanceToOverrideTargetPosition() < 10f;
			}
			return this.IsInsideTargetOrderCaptureRadius();
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0005BB04 File Offset: 0x00059D04
	private float FlatDistanceToOverrideTargetPosition()
	{
		return (this.leader.actor.Position() - this.order.overrideTargetPosition).ToGround().magnitude;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0005BB40 File Offset: 0x00059D40
	private void UpdatePatrolWaypoints()
	{
		Debug.DrawLine(this.leader.actor.Position(), this.GetCurrentWaypoint(), Color.green, 1f);
		if (!this.leader.HasPath() && Vector3.Distance(this.leader.actor.Position(), this.GetCurrentWaypoint()) < 10f)
		{
			this.NextWaypoint();
			this.MoveTo(this.GetCurrentWaypoint());
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0005BBB4 File Offset: 0x00059DB4
	private void UpdateDefense()
	{
		bool flag = !this.order.target.IsSafe();
		if (!this.defenseTimeoutAction.TrueDone() && this.defenseTimeoutAction.Remaining() < 10f && (flag || this.AnyMemberHasSpottedTarget()))
		{
			this.defenseTimeoutAction.StartLifetime(10f);
		}
		if (!flag && this.trackingDownPointAttacker)
		{
			this.trackingDownPointAttacker = false;
			this.BlockSpawnCapture(this.order.target);
		}
		if (!this.trackingDownPointAttacker)
		{
			if ((!this.blockingDefenderPointCapture || this.blockSpawnCaptureAction.TrueDone()) && flag)
			{
				this.TrackDownAttackerOfSpawn(this.order.target);
				if (!this.trackingDownPointAttacker)
				{
					this.BlockSpawnCapture(this.order.target);
					return;
				}
			}
			else if (this.blockingDefenderPointCapture && !flag)
			{
				this.DefendSpawn(this.order.target);
			}
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0005BC9C File Offset: 0x00059E9C
	private void TrackDownAttackerOfSpawn(SpawnPoint spawnPoint)
	{
		CapturePoint capturePoint = spawnPoint as CapturePoint;
		this.trackingDownPointAttacker = false;
		if (capturePoint != null)
		{
			foreach (Actor actor in capturePoint.highPriorityTargets)
			{
				if (actor.team != this.team && !actor.dead && actor.IsHighlighted() && actor.IsHighPriorityTarget())
				{
					this.trackingDownPointAttacker = true;
					this.MoveTo(actor.Position());
					break;
				}
			}
		}
		if (this.trackingDownPointAttacker)
		{
			this.blockingDefenderPointCapture = true;
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0005BD4C File Offset: 0x00059F4C
	public bool HasRepairTool()
	{
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.actor.hasRepairTool)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00005671 File Offset: 0x00003871
	public bool HasSquadVehicle()
	{
		return this.squadVehicle != null;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0005BDAC File Offset: 0x00059FAC
	private bool EveryoneSeatedInSquadVehicle()
	{
		foreach (ActorController actorController in this.members)
		{
			if (!actorController.actor.IsSeated() || !(actorController.actor.seat.vehicle == this.squadVehicle))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0000567F File Offset: 0x0000387F
	private bool ReadyToCarryOutOrder()
	{
		return !this.HasSquadVehicle() || this.EveryoneSeatedInSquadVehicle();
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0005BE2C File Offset: 0x0005A02C
	public bool OrderIsDone()
	{
		if (this.order == null)
		{
			return true;
		}
		if (this.HasSquadVehicle() && this.squadVehicle.aiType == Vehicle.AiType.Transport && (this.outsideTransportVehicle || this.pickingUpPassengers))
		{
			return false;
		}
		if (this.order.type == Order.OrderType.Attack)
		{
			if (this.order.targetSquad != null)
			{
				return this.order.targetSquad.disbanded;
			}
			return this.order.target.owner == this.team && this.order.target.IsSafe();
		}
		else
		{
			if (this.order.type == Order.OrderType.Defend)
			{
				return this.defenseTimeoutAction.TrueDone();
			}
			if (this.order.type == Order.OrderType.Repair)
			{
				return this.aiMembers[0].targetRepairVehicle == null || this.aiMembers[0].targetRepairVehicle.GetHealthRatio() >= 0.95f;
			}
			if (this.order.type == Order.OrderType.Roam)
			{
				return this.squadVehicle == null || this.squadVehicle.dead;
			}
			if (this.order.type == Order.OrderType.PatrolBase || this.order.type == Order.OrderType.PatrolWaypoints)
			{
				return this.AnyMemberHasSpottedTarget();
			}
			if (this.order.type != Order.OrderType.Move)
			{
				return false;
			}
			if (this.leader.HasPath())
			{
				return false;
			}
			if (this.order.hasOverrideTargetPosition)
			{
				return this.FlatDistanceToOverrideTargetPosition() < 10f;
			}
			return this.order.target.IsInsideCaptureRange(this.leader.actor.Position());
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0005BFCC File Offset: 0x0005A1CC
	public void CarryOutOrder()
	{
		this.isCarryingOutOrder = true;
		if (this.order.type == Order.OrderType.Attack)
		{
			if (this.HasSquadVehicle() && this.squadVehicle.aiType == Vehicle.AiType.Roam)
			{
				this.ChangeToRoamOrder();
				return;
			}
			if (this.order.target != null)
			{
				if (!this.CanReachSpawnPoint(this.order.target))
				{
					if (this.order.source == this.lastReachedSpawnPoint && this.HasSquadVehicle())
					{
						this.ExitVehicle();
						return;
					}
					this.AssignOrder(new Order(Order.OrderType.Defend, this.order.source, this.order.source, true));
					return;
				}
				else
				{
					this.lastReachedSpawnPoint = this.order.target;
				}
			}
		}
		else if (this.order.type == Order.OrderType.Roam)
		{
			this.PopulateRoamingList();
		}
		else if (this.order.type == Order.OrderType.PatrolBase)
		{
			foreach (AiActorController aiActorController in this.aiMembers)
			{
				aiActorController.SetNotAlert(true);
			}
		}
		this.IssueMovement();
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0005C104 File Offset: 0x0005A304
	public void SetNotAlert(bool applyToMembers, bool limitSpeed)
	{
		this.isAlert = false;
		if (applyToMembers)
		{
			foreach (AiActorController aiActorController in this.aiMembers)
			{
				aiActorController.SetNotAlert(limitSpeed);
			}
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0005C160 File Offset: 0x0005A360
	public void SetAlert(bool applyToMembers)
	{
		this.isAlert = true;
		if (applyToMembers)
		{
			foreach (AiActorController aiActorController in this.aiMembers)
			{
				aiActorController.SetAlert();
			}
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00005691 File Offset: 0x00003891
	public bool IsInCombat()
	{
		return !this.isInCombatAction.TrueDone();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0005C1BC File Offset: 0x0005A3BC
	private void IssueMovement()
	{
		this.leaderStandStillTimeout.Start();
		if (this.order.type == Order.OrderType.Attack)
		{
			if (this.order.source != null && !this.HasPlayerLeader())
			{
				((AiActorController)this.leader).EnableAlternativePathPenalty();
			}
			if (this.order.hasOverrideTargetPosition)
			{
				this.MoveTo(this.order.overrideTargetPosition);
			}
			else if (this.order.targetSquad != null)
			{
				this.MoveTo(this.order.targetSquad.Leader().actor.Position());
			}
			else
			{
				this.MoveToSpawnPoint(this.order.target);
			}
		}
		else if (this.order.type == Order.OrderType.Roam)
		{
			if (!this.HasSquadVehicle())
			{
				this.RequestNewOrder();
				return;
			}
			this.RoamToNewTarget();
		}
		else if (this.order.type == Order.OrderType.Defend)
		{
			if (this.order.hasOverrideTargetPosition)
			{
				this.MoveToAndDigInToProtect(this.order.overrideTargetPosition);
			}
			else if (this.order.targetSquad != null)
			{
				this.MoveTo(this.order.targetSquad.draggedPosition);
			}
			else if (this.order.target.IsSafe())
			{
				this.DefendSpawn(this.order.target);
			}
			else
			{
				this.TrackDownAttackerOfSpawn(this.order.target);
				if (!this.trackingDownPointAttacker)
				{
					this.BlockSpawnCapture(this.order.target);
				}
			}
		}
		else if (this.order.type == Order.OrderType.Repair)
		{
			this.DoRepairs();
		}
		else if (this.order.type == Order.OrderType.Move)
		{
			if (this.order.hasOverrideTargetPosition)
			{
				this.MoveTo(this.order.overrideTargetPosition);
			}
			else if (this.order.targetSquad != null)
			{
				this.MoveTo(this.order.targetSquad.draggedPosition);
			}
			else
			{
				this.MoveToSpawnPoint(this.order.target);
			}
		}
		else if (this.order.type != Order.OrderType.PatrolBase && this.order.type == Order.OrderType.PatrolWaypoints)
		{
			this.SetCurrentWaypointToClosest();
			this.MoveTo(this.GetCurrentWaypoint());
		}
		this.justIssuedMovementAction.Start();
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0005C410 File Offset: 0x0005A610
	public bool CanReachSpawnPoint(SpawnPoint target)
	{
		AiActorController aiActorController = (AiActorController)this.leader;
		if (this.HasSquadVehicle())
		{
			if (!target.vehicleFilter.VehiclePassesFilter(this.squadVehicle))
			{
				return false;
			}
			if (this.squadVehicle.IsAircraft())
			{
				return true;
			}
			if (this.squadVehicle.HasDriver() && !this.squadVehicle.Driver().aiControlled)
			{
				return true;
			}
		}
		PathfindingBox.Type pathfindingGraphType = aiActorController.GetPathfindingGraphType();
		GraphNode graphNode = PathfindingManager.instance.FindClosestNode(aiActorController.actor.Position(), pathfindingGraphType, -1);
		return graphNode != null && graphNode.Area == target.GetClosestNavmeshArea(pathfindingGraphType);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0005C4AC File Offset: 0x0005A6AC
	private void PopulateRoamingList()
	{
		this.roamingList = new List<SpawnPoint>();
		if (!this.HasSquadVehicle())
		{
			return;
		}
		bool flag = this.squadVehicle.IsWatercraft();
		bool flag2 = !this.squadVehicle.IsAircraft();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			bool flag3 = true;
			if (flag)
			{
				flag3 = SpawnPointNeighborManager.HasWaterConnection(this.order.source, spawnPoint);
			}
			else if (flag2)
			{
				flag3 = SpawnPointNeighborManager.HasLandConnection(this.order.source, spawnPoint);
			}
			if (flag3 && spawnPoint.vehicleFilter.VehiclePassesFilter(this.squadVehicle))
			{
				this.roamingList.Add(spawnPoint);
			}
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0005C560 File Offset: 0x0005A760
	private void DoRepairs()
	{
		foreach (TurretSpawner turretSpawner in this.order.source.turretSpawners)
		{
			Vehicle activeTurret = turretSpawner.GetActiveTurret();
			if (activeTurret != null && activeTurret.GetHealthRatio() < 0.9f)
			{
				using (List<AiActorController>.Enumerator enumerator2 = this.aiMembers.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						AiActorController aiActorController = enumerator2.Current;
						aiActorController.Repair(activeTurret);
					}
					break;
				}
			}
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0005C614 File Offset: 0x0005A814
	private void RoamToNewTarget()
	{
		if (this.squadVehicle.IsAircraft())
		{
			this.roamChangeTargetAction.StartLifetime(UnityEngine.Random.Range(10f, 20f));
		}
		else
		{
			this.roamChangeTargetAction.StartLifetime(UnityEngine.Random.Range(30f, 60f));
		}
		this.roamKeepTargetAction.Start();
		if (this.order.hasOverrideTargetPosition)
		{
			this.MoveTo(this.order.overrideTargetPosition);
			return;
		}
		if (this.roamingList.Count == 0)
		{
			this.MoveTo(this.Leader().actor.Position() + UnityEngine.Random.insideUnitSphere.ToGround().normalized * 100f);
			return;
		}
		int index = UnityEngine.Random.Range(0, this.roamingList.Count);
		SpawnPoint spawnPoint = this.roamingList[index];
		if (spawnPoint.owner == this.team)
		{
			index = UnityEngine.Random.Range(0, this.roamingList.Count);
			spawnPoint = this.roamingList[index];
		}
		if (spawnPoint != null)
		{
			this.MoveTo(spawnPoint.RandomPositionInCaptureZone());
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0005C734 File Offset: 0x0005A934
	private void ChangeToRoamOrder()
	{
		Order order = new Order(Order.OrderType.Roam, this.order.source, this.order.target, false);
		this.DropOrder();
		this.AssignOrder(order);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0005C76C File Offset: 0x0005A96C
	public void RequestNewOrder()
	{
		RavenscriptManager.events.onSquadRequestNewOrder.Invoke(this);
		if (RavenscriptManager.events.onSquadRequestNewOrder.isConsumed)
		{
			return;
		}
		if (this.order != null)
		{
			this.DropOrder();
		}
		if (this.autoAssignNewOrders)
		{
			this.AssignOrder(OrderManager.GetHighestPriorityOrderForExistingSquad(this));
			if (!this.HasSquadVehicle())
			{
				this.FindSquadVehicle();
			}
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0005C7CC File Offset: 0x0005A9CC
	private void FindSquadVehicle()
	{
		if (this.order != null && this.order.type == Order.OrderType.Attack && this.lastReachedSpawnPoint.owner == this.team)
		{
			Vehicle availableVehicle = this.order.source.GetAvailableVehicle(this.order.requiredVehicleFilter, this.members.Count);
			if (availableVehicle != null)
			{
				this.ClaimVehicle(availableVehicle);
				this.EnterVehicle(availableVehicle);
			}
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0005C840 File Offset: 0x0005AA40
	private void UpdateGroupedUpFlag()
	{
		if (this.members.Count < 2)
		{
			this.groupedUp = false;
			return;
		}
		Vector3 vector = Vector3.zero;
		this.meanFollowerDistance = 0f;
		foreach (ActorController actorController in this.members)
		{
			vector += actorController.transform.position;
			if (actorController != this.leader)
			{
				this.meanFollowerDistance += Vector3.Distance(actorController.actor.Position(), this.leader.actor.Position());
			}
		}
		vector /= (float)this.members.Count;
		this.meanFollowerDistance /= (float)(this.members.Count - 1);
		int num = 0;
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Vector3.Distance(enumerator.Current.transform.position, vector) < 7f)
				{
					num++;
				}
			}
		}
		this.groupedUp = (num >= 2);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0005C994 File Offset: 0x0005AB94
	private bool IsInsideTargetOrderCaptureRadius()
	{
		return this.order != null && this.order.target != null && this.isCarryingOutOrder && this.order.target.IsInsideTransportDropRange(this.Leader().actor.Position());
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0005C9E8 File Offset: 0x0005ABE8
	private void UpdateScriptedLanding()
	{
		if (!this.reachedScriptedLandingPosition)
		{
			if (!this.aiMembers[0].hasLandedUNSAFE)
			{
				this.isCompletingScriptedLandingAction.Start();
				return;
			}
			if (this.isCompletingScriptedLandingAction.TrueDone())
			{
				this.reachedScriptedLandingPosition = true;
				this.OnCompletedScriptedLanding();
			}
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x000056A1 File Offset: 0x000038A1
	public void TakeOff()
	{
		this.scriptedLanding = false;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000056AA File Offset: 0x000038AA
	public void LandAtPosition(Vector3 position)
	{
		this.scriptedLanding = true;
		this.reachedScriptedLandingPosition = false;
		this.scriptedLandingPosition = position;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x000056A1 File Offset: 0x000038A1
	public void CancelLanding()
	{
		this.scriptedLanding = false;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000056C1 File Offset: 0x000038C1
	public void OnCompletedScriptedLanding()
	{
		Squad.OnLandingCompletedDelegate onLandingCompletedDelegate = this.onLandingCompleted;
		if (onLandingCompletedDelegate == null)
		{
			return;
		}
		onLandingCompletedDelegate();
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0005CA38 File Offset: 0x0005AC38
	private void UpdateVehicleStatus()
	{
		if (this.squadVehicle.IsFull() && !this.AllSeated())
		{
			List<ActorController> list = new List<ActorController>();
			foreach (ActorController actorController in this.members)
			{
				if (!actorController.actor.IsSeated())
				{
					list.Add(actorController);
				}
			}
			Debug.Log("Splitting squad because some members are stranded.");
			this.SplitSquad(list);
		}
		if (!this.pickingUpPassengers && this.outsideTransportVehicle && this.order.target.owner == this.team)
		{
			if (this.squadVehicle.IsAircraft())
			{
				this.PickUpPassengersAt(this.order.target.GetHelicopterLandingZone());
			}
			else
			{
				this.PickUpPassengersAt(this.Leader().actor.Position());
			}
		}
		if (this.pickingUpPassengers)
		{
			if (this.AllSeated())
			{
				this.pickingUpPassengers = false;
				this.outsideTransportVehicle = false;
			}
			else if (!this.Leader().actor.IsSeated())
			{
				this.pickingUpPassengers = false;
				this.outsideTransportVehicle = false;
			}
			else if (this.outsideTransportVehicle && this.Leader().IsReadyToPickUpPassengers())
			{
				this.ReenterTransportVehicle();
			}
		}
		if (this.order != null && this.order.type == Order.OrderType.Attack && this.squadVehicle.aiType == Vehicle.AiType.Transport && !this.squadHasbeenDroppedFromTransportVehicle && this.IsInsideTargetOrderCaptureRadius() && !this.outsideTransportVehicle)
		{
			this.TempExitTransportVehicle();
		}
		if (this.squadVehicle.burning)
		{
			if (!this.leavingBurningVehicle)
			{
				this.leavingBurningVehicle = true;
				this.leavingBurningVehicleAction.StartLifetime(UnityEngine.Random.Range(0f, AiActorController.PARAMETERS.LEAVE_BURNING_VEHICLE_TIME_MULTIPLIER * this.squadVehicle.burnTime));
			}
			if (this.leavingBurningVehicleAction.TrueDone())
			{
				if (this.squadVehicle.IsAircraft())
				{
					if (PathfindingManager.ShouldEjectFromAircraftAtPosition(this.squadVehicle.transform.position))
					{
						this.ExitVehicle();
					}
				}
				else
				{
					this.ExitVehicle();
				}
			}
		}
		if (this.squadVehicle != null && this.squadVehicle.dead)
		{
			this.squadVehicle = null;
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0005CC70 File Offset: 0x0005AE70
	private void TempExitTransportVehicle()
	{
		this.outsideTransportVehicle = true;
		this.squadHasbeenDroppedFromTransportVehicle = true;
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (aiActorController.actor.IsSeated() && !aiActorController.actor.seat.IsDriverSeat() && !aiActorController.actor.seat.HasAnyMountedWeapons())
			{
				aiActorController.LeaveVehicle(false);
			}
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0005CD04 File Offset: 0x0005AF04
	private void ReenterTransportVehicle()
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (!aiActorController.actor.IsSeated() && (aiActorController.targetVehicle != this.squadVehicle || !aiActorController.HasPath()))
			{
				aiActorController.GotoAndEnterVehicle(this.squadVehicle, false);
			}
		}
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0005CD88 File Offset: 0x0005AF88
	private void PickUpPassengersAt(Vector3 position)
	{
		this.pickingUpPassengers = true;
		this.pickUpPosition = position;
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (!aiActorController.actor.IsSeated())
			{
				aiActorController.Goto(position, false);
			}
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0005CDF8 File Offset: 0x0005AFF8
	public Squad SplitSquad(List<ActorController> leavingMembers)
	{
		foreach (ActorController a in leavingMembers)
		{
			this.DropMember(a);
		}
		Squad squad = new Squad(leavingMembers, this.lastReachedSpawnPoint, null, null, 0.5f);
		foreach (ActorController actorController in leavingMembers)
		{
			actorController.OnAssignedToSquad(squad);
		}
		return squad;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0005CE98 File Offset: 0x0005B098
	public bool MemberNeedsResupply()
	{
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.actor.needsResupply)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
	public bool MemberNeedsHealth()
	{
		using (List<ActorController>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.actor.health < 80f)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0005CF5C File Offset: 0x0005B15C
	private Vector3 GetVehicleConvoyFollowTarget(Vehicle vehicle)
	{
		if (vehicle.IsAircraft())
		{
			return this.leader.actor.Position();
		}
		List<Vehicle> list = new List<Vehicle>();
		foreach (ActorController actorController in this.members)
		{
			if (actorController.actor.IsSeated() && !vehicle.IsAircraft() && !list.Contains(actorController.actor.seat.vehicle))
			{
				list.Add(actorController.actor.seat.vehicle);
			}
		}
		int num = list.IndexOf(vehicle);
		if (num <= 0)
		{
			return this.leader.actor.Position();
		}
		return list[num - 1].transform.position;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0005D03C File Offset: 0x0005B23C
	public bool IsInVehicleFollowerRange(Vehicle vehicle)
	{
		float num = 20f + vehicle.pathingRadius * 3f;
		return Vector3.Distance(vehicle.transform.position, this.GetVehicleConvoyFollowTarget(vehicle)) < num;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000056D3 File Offset: 0x000038D3
	public Vector3 GetVehicleFollowerPosition(Vehicle vehicle)
	{
		return this.GetVehicleConvoyFollowTarget(vehicle);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0005D078 File Offset: 0x0005B278
	public Vector3 GetFollowerPosition(ActorController member)
	{
		AiActorController aiActorController = (AiActorController)member;
		Vector3 leaderPosition = this.leader.actor.Position();
		if (this.leader.actor.aiControlled && this.leader.actor.parachuteDeployed)
		{
			AiActorController aiActorController2 = (AiActorController)this.leader;
			if (aiActorController2.hasParachuteLandPosition)
			{
				leaderPosition = aiActorController2.parachuteLandPosition;
			}
		}
		Vector3 normalized = (this.leader.actor.Position() - this.draggedPosition).ToGround().normalized;
		Vector3 vector = Vector3.Cross(Vector3.up, normalized);
		int squadMemberIndex = aiActorController.squadMemberIndex;
		switch (this.formation)
		{
		case Squad.FormationType.Wedge:
			return this.GetFollowerPositionWedge(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.Vee:
			return this.GetFollowerPositionVee(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.Line:
			return this.GetFollowerPositionLine(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.File:
			return this.GetFollowerPositionFile(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.EchelonLeft:
			return this.GetFollowerPositionEchelon(leaderPosition, normalized, -vector, squadMemberIndex);
		case Squad.FormationType.EchelonRight:
			return this.GetFollowerPositionEchelon(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.Diamond:
			return this.GetFollowerPositionDiamond(leaderPosition, normalized, vector, squadMemberIndex);
		case Squad.FormationType.Custom:
			return this.GetFollowerPositionCustom(leaderPosition, normalized, vector, squadMemberIndex);
		default:
			return this.GetFollowerPositionWedge(leaderPosition, normalized, vector, squadMemberIndex);
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0005D1B4 File Offset: 0x0005B3B4
	private Vector3 GetFollowerPositionWedge(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		int num = (memberIndex + 1) / 2;
		int num2 = (memberIndex % 2 == 0) ? -1 : 1;
		return leaderPosition - forward * (float)num * this.formationDepth - right * (float)num2 * (float)num * this.formationWidth;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0005D20C File Offset: 0x0005B40C
	private Vector3 GetFollowerPositionVee(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		int num = (memberIndex + 1) / 2;
		int num2 = (memberIndex % 2 == 0) ? -1 : 1;
		return leaderPosition + forward * (float)num * this.formationDepth - right * (float)num2 * (float)num * this.formationWidth;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0005D264 File Offset: 0x0005B464
	private Vector3 GetFollowerPositionLine(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		int num = (memberIndex - 1) / 2 + 1;
		int num2 = (memberIndex % 2 == 0) ? -1 : 1;
		return leaderPosition + right * (float)num2 * (float)num * this.formationWidth;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x000056DC File Offset: 0x000038DC
	private Vector3 GetFollowerPositionFile(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		return leaderPosition - forward * (float)memberIndex * this.formationWidth;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
	private Vector3 GetFollowerPositionEchelon(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		int num = (memberIndex - 1) / 2 + 1;
		int num2 = (memberIndex % 2 == 0) ? -1 : 1;
		int num3 = num * num2;
		return leaderPosition + (forward * -this.formationDepth + right * this.formationWidth / 2f) * (float)num3;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0005D300 File Offset: 0x0005B500
	private Vector3 GetFollowerPositionDiamond(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		int num = Squad.DIAMOND_FORMATION_LEVEL_START_INDEX.Length - 1;
		for (int i = 1; i < Squad.DIAMOND_FORMATION_LEVEL_START_INDEX.Length; i++)
		{
			if (memberIndex < Squad.DIAMOND_FORMATION_LEVEL_START_INDEX[i])
			{
				num = i - 1;
				break;
			}
		}
		int num2 = memberIndex - Squad.DIAMOND_FORMATION_LEVEL_START_INDEX[num];
		int num3 = (num2 % 2 == 0) ? -1 : 1;
		int num4 = num + num2 / 2;
		int num5 = num - num2 / 2;
		return leaderPosition - forward * (float)num4 * this.formationDepth + right * (float)num5 * (float)num3 * this.formationWidth;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0005D39C File Offset: 0x0005B59C
	private Vector3 GetFollowerPositionCustom(Vector3 leaderPosition, Vector3 forward, Vector3 right, int memberIndex)
	{
		Vector2 vector = this.customFormation[(memberIndex - 1) % this.customFormation.Length];
		Vector3 b = forward * vector.y * this.formationDepth + right * vector.x * this.formationWidth;
		return leaderPosition + b;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0005D400 File Offset: 0x0005B600
	public bool AnyMemberHasSpottedTarget()
	{
		using (List<AiActorController>.Enumerator enumerator = this.aiMembers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasSpottedTarget())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0005D45C File Offset: 0x0005B65C
	public float GetMaxTopSpeed()
	{
		if (this.HasSquadVehicle() && this.squadVehicle.GetType() == typeof(ArcadeCar))
		{
			if (!this.EveryoneSeatedInSquadVehicle())
			{
				return 3.2f;
			}
			return ((ArcadeCar)this.squadVehicle).topSpeed * 0.8f;
		}
		else
		{
			if (this.anyMemberIsInTemporaryCover)
			{
				return 2f;
			}
			if (this.IsTakingFire())
			{
				return 3.2f;
			}
			if (!this.IsTakingFire())
			{
				return 5.5f;
			}
			return 3.2f;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000056F8 File Offset: 0x000038F8
	public float GetSpeedRestriction()
	{
		if (this.hostCluster != null)
		{
			return this.hostCluster.GetMaxAllowedSpeed();
		}
		if (this.parentCluster != null)
		{
			return this.parentCluster.GetMaxAllowedSpeed() + this.bonusSpeedInParentCluster;
		}
		return float.MaxValue;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0005D4E4 File Offset: 0x0005B6E4
	public bool AreFollowersLaggingBehind()
	{
		return this.order != null && this.order.type == Order.OrderType.Attack && (!this.HasSquadVehicle() || this.EveryoneSeatedInSquadVehicle()) && this.members.Count >= 2 && this.meanFollowerDistance > 15f;
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0000572E File Offset: 0x0000392E
	public string CurrentOrderString()
	{
		if (this.order != null)
		{
			return this.order.ToString() + (this.OrderIsDone() ? " DONE" : "");
		}
		return "No order";
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00005762 File Offset: 0x00003962
	public bool HasPlayerLeader()
	{
		return !this.Leader().actor.aiControlled;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00005777 File Offset: 0x00003977
	public void MakeLeader(ActorController member)
	{
		this.leader = member;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0005D534 File Offset: 0x0005B734
	public bool ShouldWaitForPassengers(Vehicle vehicle)
	{
		foreach (AiActorController aiActorController in this.aiMembers)
		{
			if (aiActorController.HasTargetVehicle() && aiActorController.targetVehicle == vehicle && (!aiActorController.actor.IsSeated() || aiActorController.actor.seat.vehicle != vehicle))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
	public void JoinSquad(Squad squad)
	{
		AiActorController[] array = this.aiMembers.ToArray();
		this.Disband();
		AiActorController[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].ChangeToSquad(squad);
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00005780 File Offset: 0x00003980
	public override int GetHashCode()
	{
		return this.number;
	}

	// Token: 0x0400053B RID: 1339
	private const int FORMATION_MAX_RANDOM_VALUE = 6;

	// Token: 0x0400053C RID: 1340
	private const float FORMATION_WIDTH_DISTANCE_MIN = 3f;

	// Token: 0x0400053D RID: 1341
	private const float FORMATION_WIDTH_DISTANCE_MAX = 5f;

	// Token: 0x0400053E RID: 1342
	private const float FORMATION_DEPTH_DISTANCE_MIN = 2f;

	// Token: 0x0400053F RID: 1343
	private const float FORMATION_DEPTH_DISTANCE_MAX = 4f;

	// Token: 0x04000540 RID: 1344
	private const float FOLLOWERS_LAGGING_BEHIND_DISTANCE = 15f;

	// Token: 0x04000541 RID: 1345
	private const float DEFAULT_PLAYER_FORMATION_WIDTH = 3f;

	// Token: 0x04000542 RID: 1346
	private const float DEFAULT_PLAYER_FORMATION_DEPTH = 3f;

	// Token: 0x04000543 RID: 1347
	private const float INFANTRY_MAX_SPEED_OUTSIDE_COMBAT = 5.5f;

	// Token: 0x04000544 RID: 1348
	private const float INFANTRY_MAX_SPEED_IN_COMBAT = 3.2f;

	// Token: 0x04000545 RID: 1349
	private const float INFANTRY_COVER_CLUSTER_SPEED = 2f;

	// Token: 0x04000546 RID: 1350
	private const float DEFENSE_MIN_TIME = 10f;

	// Token: 0x04000547 RID: 1351
	private const float DEFENSE_MAX_TIME = 25f;

	// Token: 0x04000548 RID: 1352
	private const float ROAM_KEEP_TARGET_MIN_TIME = 30f;

	// Token: 0x04000549 RID: 1353
	private const float ROAM_KEEP_TARGET_MAX_TIME = 60f;

	// Token: 0x0400054A RID: 1354
	private const float ROAM_KEEP_TARGET_MIN_TIME_AIRCRAFT = 10f;

	// Token: 0x0400054B RID: 1355
	private const float ROAM_KEEP_TARGET_MAX_TIME_AIRCRAFT = 20f;

	// Token: 0x0400054C RID: 1356
	private const float WAYPOINT_COMPLETE_RANGE = 10f;

	// Token: 0x0400054D RID: 1357
	private const byte ALLOWED_REISSUE_MOVEMENT_TRIES = 2;

	// Token: 0x0400054E RID: 1358
	private const float GROUPED_UP_DISTANCE = 7f;

	// Token: 0x0400054F RID: 1359
	private const float DRAG_DISTANCE = 6f;

	// Token: 0x04000550 RID: 1360
	private const float ATTACK_CLOSE_TO_FLAG_DISTANCE = 40f;

	// Token: 0x04000551 RID: 1361
	private const float TEMPORARY_COVER_ALLOWED_TIME = 16f;

	// Token: 0x04000552 RID: 1362
	private const float TEMPORARY_COVER_ALLOWED_TIME_ATTACK_CLOSE_TO_FLAG = 6f;

	// Token: 0x04000553 RID: 1363
	private const float ACCEPTED_FOLLOWER_LAG_DISTANCE_PLAYER = 15f;

	// Token: 0x04000554 RID: 1364
	private const float ACCEPTED_FOLLOWER_LAG_DISTANCE_BOT = 80f;

	// Token: 0x04000555 RID: 1365
	private static int nextNumber = 1;

	// Token: 0x04000556 RID: 1366
	private ActorController leader;

	// Token: 0x04000557 RID: 1367
	public List<ActorController> members;

	// Token: 0x04000558 RID: 1368
	public List<AiActorController> aiMembers;

	// Token: 0x04000559 RID: 1369
	public Vehicle squadVehicle;

	// Token: 0x0400055A RID: 1370
	public int number;

	// Token: 0x0400055B RID: 1371
	public float meanFollowerDistance;

	// Token: 0x0400055C RID: 1372
	public int team;

	// Token: 0x0400055D RID: 1373
	private float readyTime;

	// Token: 0x0400055E RID: 1374
	private bool groupedUp;

	// Token: 0x0400055F RID: 1375
	private bool blockingDefenderPointCapture;

	// Token: 0x04000560 RID: 1376
	private bool trackingDownPointAttacker;

	// Token: 0x04000561 RID: 1377
	private int recentTakingFireEvents;

	// Token: 0x04000562 RID: 1378
	[NonSerialized]
	public Vector3 draggedPosition;

	// Token: 0x04000563 RID: 1379
	[NonSerialized]
	public Order order;

	// Token: 0x04000564 RID: 1380
	private bool isCarryingOutOrder;

	// Token: 0x04000565 RID: 1381
	[NonSerialized]
	public bool autoAssignNewOrders = true;

	// Token: 0x04000566 RID: 1382
	[NonSerialized]
	public bool allowRequestNewOrders = true;

	// Token: 0x04000567 RID: 1383
	[NonSerialized]
	public bool allowAutoLeaveVehicle = true;

	// Token: 0x04000568 RID: 1384
	private int orderPriorityModifier;

	// Token: 0x04000569 RID: 1385
	private TimedAction defenseTimeoutAction = new TimedAction(5f, false);

	// Token: 0x0400056A RID: 1386
	private TimedAction blockSpawnCaptureAction = new TimedAction(6f, false);

	// Token: 0x0400056B RID: 1387
	private TimedAction roamKeepTargetAction = new TimedAction(0.5f, false);

	// Token: 0x0400056C RID: 1388
	private TimedAction roamChangeTargetAction = new TimedAction(10f, false);

	// Token: 0x0400056D RID: 1389
	private TimedAction leaderStandStillTimeout = new TimedAction(3f, false);

	// Token: 0x0400056E RID: 1390
	private TimedAction isInCombatAction = new TimedAction(10f, false);

	// Token: 0x0400056F RID: 1391
	private TimedAction isCompletingScriptedLandingAction = new TimedAction(2f, false);

	// Token: 0x04000570 RID: 1392
	[NonSerialized]
	public SpawnPoint lastReachedSpawnPoint;

	// Token: 0x04000571 RID: 1393
	[NonSerialized]
	public List<SpawnPoint> roamingList;

	// Token: 0x04000572 RID: 1394
	private byte remainingMovementReissues = 2;

	// Token: 0x04000573 RID: 1395
	public bool shouldFollow = true;

	// Token: 0x04000574 RID: 1396
	private TimedAction resupplyThrownAction = new TimedAction(30f, false);

	// Token: 0x04000575 RID: 1397
	private TimedAction medipackThrownAction = new TimedAction(30f, false);

	// Token: 0x04000576 RID: 1398
	private TimedAction takingTemporaryCoverAction = new TimedAction(16f, false);

	// Token: 0x04000577 RID: 1399
	private TimedAction temporaryCoverCooldownAction = new TimedAction(12f, false);

	// Token: 0x04000578 RID: 1400
	private TimedAction justIssuedMovementAction = new TimedAction(10f, false);

	// Token: 0x04000579 RID: 1401
	private bool disbanded;

	// Token: 0x0400057A RID: 1402
	private bool leavingBurningVehicle;

	// Token: 0x0400057B RID: 1403
	private TimedAction leavingBurningVehicleAction = new TimedAction(1f, false);

	// Token: 0x0400057C RID: 1404
	public bool outsideTransportVehicle;

	// Token: 0x0400057D RID: 1405
	private bool squadHasbeenDroppedFromTransportVehicle;

	// Token: 0x0400057E RID: 1406
	public bool pickingUpPassengers;

	// Token: 0x0400057F RID: 1407
	public Vector3 pickUpPosition = Vector3.zero;

	// Token: 0x04000580 RID: 1408
	public bool reachedScriptedLandingPosition;

	// Token: 0x04000581 RID: 1409
	public bool scriptedLanding;

	// Token: 0x04000582 RID: 1410
	public Vector3 scriptedLandingPosition = Vector3.zero;

	// Token: 0x04000583 RID: 1411
	public bool hasSetSmokeTarget;

	// Token: 0x04000584 RID: 1412
	public float closestEnemySpawnPointDistance = 9999f;

	// Token: 0x04000585 RID: 1413
	private Squad.FormationType formation;

	// Token: 0x04000586 RID: 1414
	private float formationWidth;

	// Token: 0x04000587 RID: 1415
	private float formationDepth;

	// Token: 0x04000588 RID: 1416
	private int currentWaypoint;

	// Token: 0x04000589 RID: 1417
	private bool anyMemberIsInTemporaryCover;

	// Token: 0x0400058A RID: 1418
	public bool isAlert = true;

	// Token: 0x0400058B RID: 1419
	private Queue<Squad.MovePathSegment> pathSegmentQueue = new Queue<Squad.MovePathSegment>(3);

	// Token: 0x0400058C RID: 1420
	private Squad.MovePathSegment activePathSegment;

	// Token: 0x0400058D RID: 1421
	public Vector2[] customFormation = new Vector2[]
	{
		new Vector2(0f, -1f)
	};

	// Token: 0x0400058E RID: 1422
	public SquadCluster hostCluster;

	// Token: 0x0400058F RID: 1423
	public SquadCluster parentCluster;

	// Token: 0x04000590 RID: 1424
	public float bonusSpeedInParentCluster;

	// Token: 0x04000591 RID: 1425
	public Squad.EngagementType engagement;

	// Token: 0x04000592 RID: 1426
	public Actor attackTarget;

	// Token: 0x04000593 RID: 1427
	public float acceptedFollowerLagDistance = 15f;

	// Token: 0x04000594 RID: 1428
	private bool updateMovement;

	// Token: 0x04000595 RID: 1429
	public Squad.OnLandingCompletedDelegate onLandingCompleted;

	// Token: 0x04000596 RID: 1430
	private static readonly int[] DIAMOND_FORMATION_LEVEL_START_INDEX = new int[]
	{
		0,
		1,
		4,
		9,
		16,
		25,
		36,
		49
	};

	// Token: 0x020000AA RID: 170
	public enum FormationType
	{
		// Token: 0x04000598 RID: 1432
		Wedge,
		// Token: 0x04000599 RID: 1433
		Vee,
		// Token: 0x0400059A RID: 1434
		Line,
		// Token: 0x0400059B RID: 1435
		File,
		// Token: 0x0400059C RID: 1436
		EchelonLeft,
		// Token: 0x0400059D RID: 1437
		EchelonRight,
		// Token: 0x0400059E RID: 1438
		Diamond,
		// Token: 0x0400059F RID: 1439
		Custom
	}

	// Token: 0x020000AB RID: 171
	public enum EngagementType
	{
		// Token: 0x040005A1 RID: 1441
		FireAtWill,
		// Token: 0x040005A2 RID: 1442
		OnlyAlerted,
		// Token: 0x040005A3 RID: 1443
		None
	}

	// Token: 0x020000AC RID: 172
	// (Invoke) Token: 0x060005B5 RID: 1461
	public delegate void OnLandingCompletedDelegate();

	// Token: 0x020000AD RID: 173
	public class MovePathSegment
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x000057A6 File Offset: 0x000039A6
		public MovePathSegment(Vector3 destination)
		{
			this.destination = destination;
			this.appendedNodes = new List<Vector3>();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000057C0 File Offset: 0x000039C0
		public void AppendNode(Vector3 position)
		{
			this.appendedNodes.Add(position);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000057CE File Offset: 0x000039CE
		public void ForceGraphType(PathfindingBox.Type type)
		{
			this.forceGraphType = true;
			this.graphType = type;
		}

		// Token: 0x040005A4 RID: 1444
		public Vector3 destination;

		// Token: 0x040005A5 RID: 1445
		public bool forceGraphType;

		// Token: 0x040005A6 RID: 1446
		public PathfindingBox.Type graphType;

		// Token: 0x040005A7 RID: 1447
		public List<Vector3> appendedNodes;

		// Token: 0x040005A8 RID: 1448
		public bool exitVehicleOnComplete;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003C7 RID: 967
	public class SpecOpsPatrol : SpecOpsObjective
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x000A3BAC File Offset: 0x000A1DAC
		public SpecOpsPatrol(PathfindingBox.Type type, List<Vector3> waypoints, SpecOpsMode specOps)
		{
			this.specOps = specOps;
			this.type = type;
			List<Vector3> list = new List<Vector3>(waypoints);
			if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
			{
				list.Reverse();
			}
			list = this.ShuffleWaypointStart(list);
			this.CreateOrder(list);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000A3C24 File Offset: 0x000A1E24
		public SpecOpsPatrol(Squad squad, SpecOpsMode specOps)
		{
			this.squad = squad;
			this.specOps = specOps;
			this.patrolOrder = this.squad.order;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000129E0 File Offset: 0x00010BE0
		public void InvestigateFlare(SpawnPoint spawn)
		{
			if (this.searchAndDestroy)
			{
				return;
			}
			this.investigateOrder = new Order(Order.OrderType.Move, null, spawn, true);
			this.squad.AssignOrder(this.investigateOrder);
			this.squad.SetAlert(true);
			this.isCompletingInvestigation = false;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000A3C78 File Offset: 0x000A1E78
		public void InitializeObjective()
		{
			string text = "Neutralize Patrol";
			switch (this.type)
			{
			case PathfindingBox.Type.Infantry:
				text = "Neutralize Infantry Patrol";
				break;
			case PathfindingBox.Type.Car:
				text = "Neutralize Vehicle Patrol";
				break;
			case PathfindingBox.Type.Boat:
				text = "Neutralize Boat Patrol";
				break;
			}
			this.objective = ObjectiveUi.CreateObjective(text, this.squadSpawnPoint);
			ObjectiveUi.SortEntries();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00012A1E File Offset: 0x00010C1E
		public override bool IsCompletedTest()
		{
			return this.squad.members.Count == 0;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00012A33 File Offset: 0x00010C33
		private void UpdateInvestigation()
		{
			if (this.squad.OrderIsDone())
			{
				if (!this.isCompletingInvestigation)
				{
					this.isCompletingInvestigation = true;
					this.completeInvestigationAction.Start();
					return;
				}
				if (this.completeInvestigationAction.TrueDone())
				{
					this.ReturnToPatrol();
				}
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00012A70 File Offset: 0x00010C70
		private void ReturnToPatrol()
		{
			this.squad.SetNotAlert(true, true);
			this.squad.AssignOrder(this.patrolOrder);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000A3CD4 File Offset: 0x000A1ED4
		public override void Update()
		{
			base.Update();
			if (this.squad.order == this.investigateOrder)
			{
				this.UpdateInvestigation();
			}
			if (this.objective != null && !this.objective.isCompleted)
			{
				this.objective.SetWorldTarget(this.squad.Leader().actor.CenterPosition());
			}
			if (this.squad.AnyMemberHasSpottedTarget() && !this.searchAndDestroy)
			{
				this.SearchAndDestroy();
			}
			if (this.specOps.IntroIsDone() && this.squad.members.Count > 0 && this.eyesReportTimeout.TrueDone())
			{
				bool flag = ActorManager.ActorDistanceToPlayer(this.squad.Leader().actor) < 140f;
				if (flag && !this.isInReportRange)
				{
					this.specOps.OnIncomingPatrol(this);
					this.eyesReportTimeout.Start();
				}
				this.isInReportRange = flag;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000A3DCC File Offset: 0x000A1FCC
		private List<Vector3> ShuffleWaypointStart(List<Vector3> waypoints)
		{
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < waypoints.Count; i++)
			{
				float num3 = Vector3.Distance(waypoints[i], this.specOps.attackerSpawnPosition);
				if (num3 > num)
				{
					num2 = i;
					num = num3;
				}
			}
			int num4 = num2;
			List<Vector3> list = new List<Vector3>(waypoints.Count);
			list.AddRange(waypoints.GetRange(num4, waypoints.Count - num4));
			list.AddRange(waypoints.GetRange(0, num4));
			return list;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00012A90 File Offset: 0x00010C90
		public void Spawn()
		{
			this.SpawnSquad();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000A3E48 File Offset: 0x000A2048
		private void SearchAndDestroy()
		{
			this.searchAndDestroy = true;
			Order order = new Order(Order.OrderType.Attack, null, null, true);
			order.targetSquad = this.specOps.attackerSquad;
			foreach (AiActorController aiActorController in this.squad.aiMembers)
			{
				aiActorController.modifier.canSprint = true;
			}
			this.squad.AssignOrder(order);
			this.squad.SetAlert(true);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000A3EE0 File Offset: 0x000A20E0
		private void CreateOrder(List<Vector3> waypoints)
		{
			this.patrolOrder = new Order(Order.OrderType.PatrolWaypoints, null, null, true);
			this.patrolOrder.SetWaypoints(waypoints.ToArray());
			this.squadSpawnPoint = waypoints[0];
			Vector3 forward = waypoints[1] - waypoints[0];
			this.squadSpawnRotation = SMath.LookRotationRespectUp(forward, Vector3.up);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000A3F40 File Offset: 0x000A2140
		private void SpawnSquad()
		{
			int num = 4;
			Vehicle vehicle = null;
			if (this.type == PathfindingBox.Type.Car)
			{
				VehicleSpawner.VehicleSpawnType vehicleSpawnType = this.specOps.patrolUseApc ? VehicleSpawner.VehicleSpawnType.Apc : VehicleSpawner.VehicleSpawnType.JeepMachineGun;
				vehicle = VehicleSpawner.SpawnVehicleAt(this.squadSpawnPoint, this.squadSpawnRotation, this.specOps.defendingTeam, vehicleSpawnType);
			}
			if (this.type == PathfindingBox.Type.Boat)
			{
				VehicleSpawner.VehicleSpawnType vehicleSpawnType2 = this.specOps.patrolUseAttackBoat ? VehicleSpawner.VehicleSpawnType.AttackBoat : VehicleSpawner.VehicleSpawnType.Rhib;
				vehicle = VehicleSpawner.SpawnVehicleAt(this.squadSpawnPoint, this.squadSpawnRotation, this.specOps.defendingTeam, vehicleSpawnType2);
			}
			if (vehicle != null)
			{
				num = Mathf.Min(vehicle.seats.Count, 4);
			}
			List<Actor> list = new List<Actor>();
			float num2 = GameManager.GameParameters().nightMode ? 60f : 100f;
			float num3 = GameManager.GameParameters().nightMode ? 0.33f : 0.4f;
			bool ignoreFovCheck = vehicle != null && vehicle.seats[0].enclosed;
			num2 *= 1.2f;
			num3 *= 0.7f;
			for (int i = 0; i < num; i++)
			{
				Actor actor = ActorManager.instance.CreateAIActor(this.specOps.defendingTeam);
				actor.SpawnAt(this.squadSpawnPoint, this.squadSpawnRotation, null);
				AiActorController aiActorController = actor.controller as AiActorController;
				aiActorController.ActivateSlowTargetDetection(num3);
				aiActorController.modifier.maxDetectionDistance = num2;
				aiActorController.modifier.ignoreFovCheck = ignoreFovCheck;
				aiActorController.modifier.canSprint = false;
				list.Add(actor);
			}
			this.squad = new Squad(list, null, this.patrolOrder, vehicle, 0f);
			this.squad.SetRandomFormation();
			this.squad.allowRequestNewOrders = false;
			this.squad.autoAssignNewOrders = false;
			this.squad.SetNotAlert(true, true);
			if (vehicle != null)
			{
				foreach (Actor actor2 in list)
				{
					actor2.EnterVehicle(vehicle);
				}
				this.squad.allowAutoLeaveVehicle = false;
			}
		}

		// Token: 0x040019E9 RID: 6633
		private const int MAX_BOTS_IN_PATROL_VEHICLE = 4;

		// Token: 0x040019EA RID: 6634
		private const float SPEED_MULTIPLIER_INFANTRY = 0.4f;

		// Token: 0x040019EB RID: 6635
		private const float SPEED_MULTIPLIER_VEHICLE = 0.4f;

		// Token: 0x040019EC RID: 6636
		public PathfindingBox.Type type;

		// Token: 0x040019ED RID: 6637
		public Squad squad;

		// Token: 0x040019EE RID: 6638
		public Order patrolOrder;

		// Token: 0x040019EF RID: 6639
		public Order investigateOrder;

		// Token: 0x040019F0 RID: 6640
		private Vector3 squadSpawnPoint;

		// Token: 0x040019F1 RID: 6641
		private Quaternion squadSpawnRotation;

		// Token: 0x040019F2 RID: 6642
		private TimedAction eyesReportTimeout = new TimedAction(120f, false);

		// Token: 0x040019F3 RID: 6643
		private TimedAction completeInvestigationAction = new TimedAction(10f, false);

		// Token: 0x040019F4 RID: 6644
		private bool isCompletingInvestigation;

		// Token: 0x040019F5 RID: 6645
		private bool isInReportRange;

		// Token: 0x040019F6 RID: 6646
		private bool searchAndDestroy;
	}
}

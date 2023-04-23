using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class AiActorController : ActorController
{
	// Token: 0x06000350 RID: 848 RVA: 0x00004276 File Offset: 0x00002476
	public static void LoadParameters(int index)
	{
		if (index == 0)
		{
			AiActorController.PARAMETERS = AiActorController.PARAMETERS_EASY;
			return;
		}
		if (index == 1)
		{
			AiActorController.PARAMETERS = AiActorController.PARAMETERS_NORMAL;
			return;
		}
		AiActorController.PARAMETERS = AiActorController.PARAMETERS_HARD;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0004D184 File Offset: 0x0004B384
	public static void SetupParameters()
	{
		AiActorController.PARAMETERS_EASY = default(AiActorController.AiParameters);
		AiActorController.PARAMETERS_EASY.REACTION_TIME = 0.6f;
		AiActorController.PARAMETERS_EASY.LEAD_SWAY_MAGNITUDE = 0.3f;
		AiActorController.PARAMETERS_EASY.LEAD_NOISE_MAGNITUDE = 0.1f;
		AiActorController.PARAMETERS_EASY.ACQUIRE_TARGET_OFFSET_PER_METER = 0.12f;
		AiActorController.PARAMETERS_EASY.ACQUIRE_TARGET_DEPTH_EXTRA_OFFSET_PER_METER = 0.6f;
		AiActorController.PARAMETERS_EASY.ACQUIRE_TARGET_DURATION_BASE = 2f;
		AiActorController.PARAMETERS_EASY.ACQUIRE_TARGET_DURATION_PER_METER = 0.015f;
		AiActorController.PARAMETERS_EASY.AIM_BASE_SWAY = 0.01f;
		AiActorController.PARAMETERS_EASY.HALT_LONG_RANGE_ENGAGEMENT_RANGE = 50f;
		AiActorController.PARAMETERS_EASY.AIM_SWING_PARACHUTING = 10f;
		AiActorController.PARAMETERS_EASY.AI_FIRE_RECTANGLE_BOUND = 2.5f;
		AiActorController.PARAMETERS_EASY.VISIBILITY_MULTIPLIER = 1f;
		AiActorController.PARAMETERS_EASY.TAKING_FIRE_REACTION_TIME = 0.5f;
		AiActorController.PARAMETERS_EASY.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MIN = 1.5f;
		AiActorController.PARAMETERS_EASY.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MAX = 8f;
		AiActorController.PARAMETERS_EASY.RECOIL_MULTIPLIER = 1.2f;
		AiActorController.PARAMETERS_EASY.LEAVE_BURNING_VEHICLE_TIME_MULTIPLIER = 2f;
		AiActorController.PARAMETERS_EASY.FOCUS_DISTANCE_GAIN_INFANTRY = 40f;
		AiActorController.PARAMETERS_EASY.FOCUS_DISTANCE_GAIN_VEHICLES = 150f;
		AiActorController.PARAMETERS_EASY.FOCUS_DISTANCE_GAIN_HUGE_VEHICLES = 1000f;
		AiActorController.PARAMETERS_EASY.ALERT_SQUAD_TIME = 3f;
		AiActorController.PARAMETERS_EASY.SKILL_NORMAL_MOD = 4;
		AiActorController.PARAMETERS_EASY.SKILL_VETERAN_MOD = 11;
		AiActorController.PARAMETERS_NORMAL = default(AiActorController.AiParameters);
		AiActorController.PARAMETERS_NORMAL.REACTION_TIME = 0.4f;
		AiActorController.PARAMETERS_NORMAL.LEAD_SWAY_MAGNITUDE = 0.15f;
		AiActorController.PARAMETERS_NORMAL.LEAD_NOISE_MAGNITUDE = 0.08f;
		AiActorController.PARAMETERS_NORMAL.ACQUIRE_TARGET_OFFSET_PER_METER = 0.06f;
		AiActorController.PARAMETERS_NORMAL.ACQUIRE_TARGET_DEPTH_EXTRA_OFFSET_PER_METER = 0.5f;
		AiActorController.PARAMETERS_NORMAL.ACQUIRE_TARGET_DURATION_BASE = 1.5f;
		AiActorController.PARAMETERS_NORMAL.ACQUIRE_TARGET_DURATION_PER_METER = 0.01f;
		AiActorController.PARAMETERS_NORMAL.AIM_BASE_SWAY = 0.004f;
		AiActorController.PARAMETERS_NORMAL.HALT_LONG_RANGE_ENGAGEMENT_RANGE = 80f;
		AiActorController.PARAMETERS_NORMAL.AIM_SWING_PARACHUTING = 8f;
		AiActorController.PARAMETERS_NORMAL.AI_FIRE_RECTANGLE_BOUND = 1.5f;
		AiActorController.PARAMETERS_NORMAL.VISIBILITY_MULTIPLIER = 2f;
		AiActorController.PARAMETERS_NORMAL.TAKING_FIRE_REACTION_TIME = 0.25f;
		AiActorController.PARAMETERS_NORMAL.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MIN = 1f;
		AiActorController.PARAMETERS_NORMAL.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MAX = 3f;
		AiActorController.PARAMETERS_NORMAL.RECOIL_MULTIPLIER = 0.8f;
		AiActorController.PARAMETERS_NORMAL.LEAVE_BURNING_VEHICLE_TIME_MULTIPLIER = 1.1f;
		AiActorController.PARAMETERS_NORMAL.FOCUS_DISTANCE_GAIN_INFANTRY = 80f;
		AiActorController.PARAMETERS_NORMAL.FOCUS_DISTANCE_GAIN_VEHICLES = 300f;
		AiActorController.PARAMETERS_NORMAL.FOCUS_DISTANCE_GAIN_HUGE_VEHICLES = 2000f;
		AiActorController.PARAMETERS_NORMAL.ALERT_SQUAD_TIME = 2f;
		AiActorController.PARAMETERS_NORMAL.SKILL_NORMAL_MOD = 2;
		AiActorController.PARAMETERS_NORMAL.SKILL_VETERAN_MOD = 7;
		AiActorController.PARAMETERS_HARD = default(AiActorController.AiParameters);
		AiActorController.PARAMETERS_HARD.REACTION_TIME = 0.2f;
		AiActorController.PARAMETERS_HARD.LEAD_SWAY_MAGNITUDE = 0.07f;
		AiActorController.PARAMETERS_HARD.LEAD_NOISE_MAGNITUDE = 0.03f;
		AiActorController.PARAMETERS_HARD.ACQUIRE_TARGET_OFFSET_PER_METER = 0.03f;
		AiActorController.PARAMETERS_HARD.ACQUIRE_TARGET_DEPTH_EXTRA_OFFSET_PER_METER = 0.3f;
		AiActorController.PARAMETERS_HARD.ACQUIRE_TARGET_DURATION_BASE = 1f;
		AiActorController.PARAMETERS_HARD.ACQUIRE_TARGET_DURATION_PER_METER = 0.01f;
		AiActorController.PARAMETERS_HARD.AIM_BASE_SWAY = 0.001f;
		AiActorController.PARAMETERS_HARD.HALT_LONG_RANGE_ENGAGEMENT_RANGE = 120f;
		AiActorController.PARAMETERS_HARD.AIM_SWING_PARACHUTING = 5f;
		AiActorController.PARAMETERS_HARD.AI_FIRE_RECTANGLE_BOUND = 1f;
		AiActorController.PARAMETERS_HARD.VISIBILITY_MULTIPLIER = 2.5f;
		AiActorController.PARAMETERS_HARD.TAKING_FIRE_REACTION_TIME = 0.15f;
		AiActorController.PARAMETERS_HARD.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MIN = 0.7f;
		AiActorController.PARAMETERS_HARD.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MAX = 1.5f;
		AiActorController.PARAMETERS_HARD.RECOIL_MULTIPLIER = 0.4f;
		AiActorController.PARAMETERS_HARD.LEAVE_BURNING_VEHICLE_TIME_MULTIPLIER = 0.7f;
		AiActorController.PARAMETERS_HARD.FOCUS_DISTANCE_GAIN_INFANTRY = 130f;
		AiActorController.PARAMETERS_HARD.FOCUS_DISTANCE_GAIN_VEHICLES = 600f;
		AiActorController.PARAMETERS_HARD.FOCUS_DISTANCE_GAIN_HUGE_VEHICLES = 4000f;
		AiActorController.PARAMETERS_HARD.ALERT_SQUAD_TIME = 2f;
		AiActorController.PARAMETERS_HARD.SKILL_NORMAL_MOD = 1;
		AiActorController.PARAMETERS_HARD.SKILL_VETERAN_MOD = 2;
		AiActorController.LoadParameters(1);
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000352 RID: 850 RVA: 0x0000429F File Offset: 0x0000249F
	public Vehicle targetVehicle
	{
		get
		{
			return this._targetVehicle;
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0004D5AC File Offset: 0x0004B7AC
	private void Awake()
	{
		this.seeker = base.GetComponent<Seeker>();
		Seeker seeker = this.seeker;
		seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		this.randomTimeOffset = UnityEngine.Random.Range(0f, 10f);
		this.radiusModifier = base.GetComponent<RadiusModifier>();
		this.modifier = new AiActorController.Modifier();
		this.infantryFocus = new AiActorController.FocusDistance(1000f, 50f, AiActorController.PARAMETERS.FOCUS_DISTANCE_GAIN_INFANTRY);
		this.vehicleFocus = new AiActorController.FocusDistance(3000f, 300f, AiActorController.PARAMETERS.FOCUS_DISTANCE_GAIN_INFANTRY);
		this.hugeVehicleFocus = new AiActorController.FocusDistance(10000f, 2000f, AiActorController.PARAMETERS.FOCUS_DISTANCE_GAIN_INFANTRY);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x000042A7 File Offset: 0x000024A7
	public override bool IsMoving()
	{
		return this.hasPath && !this.IsHalted();
	}

	// Token: 0x06000355 RID: 853 RVA: 0x000042BC File Offset: 0x000024BC
	public override Squad GetSquad()
	{
		return this.squad;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0004D678 File Offset: 0x0004B878
	private void StartAiCoroutines()
	{
		this.aiRoutines = new IEnumerator[]
		{
			this.AiBlocked(),
			this.AiVehicle(),
			this.AiOrders(),
			this.AiTarget(),
			this.AiWeapon(),
			this.AiTrack(),
			this.AiScan()
		};
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
	public void TickAiCoroutines()
	{
		if (this.aiRoutines == null)
		{
			return;
		}
		for (int i = 0; i < this.aiRoutines.Length; i++)
		{
			try
			{
				this.aiRoutines[i].MoveNext();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x000042C4 File Offset: 0x000024C4
	private IEnumerator AiBlocked()
	{
		yield return 0;
		Collider[] colliders = new Collider[128];
		for (;;)
		{
			if (this.hasPath)
			{
				Vector3 direction = this.vectorPath[this.waypoint] - this.actor.Position();
				new Ray(this.actor.CenterPosition(), direction);
				this.blockerAhead = false;
				if (this.isSeated)
				{
					Vehicle vehicle = this.actor.seat.vehicle;
					if (vehicle.HasBlockSensor())
					{
						int num = vehicle.BlockTest(colliders, 1f, 256);
						int i = 0;
						while (i < num)
						{
							Hurtable parent = colliders[i].GetComponent<Hitbox>().parent;
							this.blockerAhead = (parent.team == this.actor.team);
							if (this.blockerAhead)
							{
								Actor actor = parent as Actor;
								if (actor != null)
								{
									this.blockerPosition = actor.Position();
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
			}
			yield return 0;
		}
		yield break;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0004D724 File Offset: 0x0004B924
	private bool PlayerIsApproaching()
	{
		Actor actor = FpsActorController.instance.actor;
		if (!actor.fallenOver && !actor.IsSeated())
		{
			Vector3 vector = actor.Position();
			Vector3 normalized = (this.actor.Position() - vector).normalized;
			return Vector3.Dot(normalized, actor.controller.FacingDirection()) > 0.9f && Vector3.Dot(normalized, actor.Velocity().normalized) > 0.7f && Vector3.Distance(vector, this.actor.Position()) < 30f;
		}
		return false;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000042D3 File Offset: 0x000024D3
	public override void StartClimbingSlope()
	{
		this.movementSpeed = 0.5f;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000042E0 File Offset: 0x000024E0
	private IEnumerator AiVehicle()
	{
		yield return 0;
		Vector3 lastSampledVehiclePosition = Vector3.zero;
		float lastSampleTime = 0f;
		for (;;)
		{
			if (!this.isDriver && this.targetVehicle != null && !this.targetVehicle.gameObject.activeInHierarchy)
			{
				this.DropTargetVehicle();
			}
			if (!this.isDriver || !this.actor.seat.vehicle.IsBeingTrackedByMissile())
			{
				this.countermeasureReactAction.StartLifetime(UnityEngine.Random.Range(AiActorController.PARAMETERS.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MIN, AiActorController.PARAMETERS.MISSILE_LAUNCH_COUNTERMEASURE_TIME_MAX));
			}
			if (this.isDriver)
			{
				this.vehicleSpeedLimitMultiplier = 1f;
				foreach (SpeedLimitZone speedLimitZone in ActorManager.instance.speedLimitZones)
				{
					if (speedLimitZone.IsInside(this.actor.Position()))
					{
						this.vehicleSpeedLimitMultiplier = Mathf.Min(this.vehicleSpeedLimitMultiplier, speedLimitZone.speedMultiplier);
					}
				}
			}
			if (this.isSeated && this.actor.seat.vehicle != null && this.isDriver)
			{
				Type vehicleType = this.actor.seat.vehicle.GetType();
				if (this.isSquadLeader && this.actor.seat.vehicle.IsWatercraft() && this.actor.seat.vehicle.transform.up.y < 0f)
				{
					this.actor.seat.vehicle.stuck = true;
					this.squad.ContinueOnFoot();
				}
				else if (this.hasPath)
				{
					this.forceAntiStuckReverse = false;
					bool flag = vehicleType == typeof(Car) || vehicleType == typeof(Boat) || vehicleType == typeof(Tank);
					this.waitForPlayer = (flag && !this.actor.seat.vehicle.IsFull() && this.actor.team == GameManager.PlayerTeam() && !this.squad.HasPlayerLeader() && this.PlayerIsApproaching());
					if (vehicleType == typeof(ArcadeCar))
					{
						Vector3 waypointDelta = this.GetWaypointDelta();
						ArcadeCar arcadeCar = (ArcadeCar)this.actor.seat.vehicle;
						this.canTurnCarTowardsWaypoint = arcadeCar.CanTurnTowards(waypointDelta);
					}
					if (!this.IsLastWaypoint() && flag)
					{
						Vector3 lhs = this.GetUpcomingBetweenWaypointsDelta().ToGround();
						this.GetWaypointDelta().ToGround();
						if (lhs == Vector3.zero)
						{
							this.waypoint++;
						}
					}
					if (this.squad == null || !this.squad.pickingUpPassengers)
					{
						Vector3 position = this.actor.seat.vehicle.transform.position;
						if (this.IsHalted() || Vector3.Distance(position, lastSampledVehiclePosition) > 0.4f)
						{
							lastSampledVehiclePosition = position;
							lastSampleTime = Time.time;
						}
						else if (Time.time > lastSampleTime + 1.5f)
						{
							if (this.actor.seat.vehicle.IsWatercraft() && this.actor.seat.vehicle.aiType != Vehicle.AiType.Roam && !this.actor.seat.vehicle.IsInWater())
							{
								this.actor.seat.vehicle.stuck = true;
								this.squad.ContinueOnFoot();
							}
							else
							{
								this.PushAntiStuckEvent();
								this.forceAntiStuckReverse = true;
								this.forceAntiStuckReverseAction.Start();
								while (!this.forceAntiStuckReverseAction.TrueDone())
								{
									yield return 0;
								}
								this.forceAntiStuckReverse = false;
								this.forceAntiStuckReverseAction.Start();
								while (!this.forceAntiStuckReverseAction.TrueDone())
								{
									yield return 0;
								}
								if (!this.isSeated)
								{
									continue;
								}
								this.RecalculatePath();
								lastSampleTime = Time.time;
							}
						}
					}
				}
				if (vehicleType == typeof(Helicopter))
				{
					if (this.squad.ShouldWaitForPassengers(this.actor.seat.vehicle))
					{
						this.helicopterTakeoffAction.Start();
						this.signalPassengersEnterAircraft = false;
					}
					if (this.actor.seat.HasAnyMountedWeapons() && this.HasSpottedTarget() && Vector3.Dot(this.actor.seat.vehicle.transform.forward, this.target.CenterPosition() - this.actor.seat.vehicle.transform.position) > 0f && this.attackRunAction.TrueDone() && this.attackRunCooldownAction.TrueDone() && Vector3.Distance(base.transform.position, this.target.transform.position) < 300f)
					{
						this.attackRunAction.StartLifetime(4f);
						this.attackRunCooldownAction.StartLifetime(8f);
					}
				}
				if (vehicleType == typeof(Airplane) && this.actor.seat.HasAnyMountedWeapons() && this.HasSpottedTarget() && this.attackRunAction.TrueDone())
				{
					Vector3 vector = this.target.CenterPosition() - this.actor.seat.vehicle.transform.position;
					Vector3 vector2 = vector.ToGround();
					float num = -Mathf.Atan2(vector.y, vector2.magnitude) * 57.29578f;
					if (this.attackRunAction.TrueDone() && this.attackRunCooldownAction.TrueDone() && Vector3.Dot(this.actor.seat.vehicle.transform.forward.ToGround().normalized, vector2.normalized) > 0.85f && vector2.magnitude > 80f && num < 30f)
					{
						this.attackRunAction.StartLifetime(8f);
						this.attackRunCooldownAction.StartLifetime(8f);
					}
				}
				vehicleType = null;
			}
			if (this.actor.CanEnterSeat() && this.HasTargetVehicle() && Vector3.Distance(this.actor.CenterPosition(), this.targetVehicle.transform.position) < 4f + this.targetVehicle.pathingRadius)
			{
				this.CancelPath(false);
				if (!this.targetVehicle.IsFull())
				{
					bool allowDriverSeat = this.squad == null || this.squad.squadVehicle != this.targetVehicle || this.isSquadLeader || this.squad.HasPlayerLeader();
					this.EnterSeat(this.targetVehicle.GetEmptySeat(allowDriverSeat));
				}
				else
				{
					this.CreateRougeSquad();
				}
			}
			yield return 0;
		}
		yield break;
	}

	// Token: 0x0600035C RID: 860 RVA: 0x000042EF File Offset: 0x000024EF
	private bool IsHalted()
	{
		return !this.haltAction.TrueDone();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x000042FF File Offset: 0x000024FF
	private void Halt(float duration)
	{
		this.haltAction.StartLifetime(duration);
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0004D7BC File Offset: 0x0004B9BC
	private void PushAntiStuckEvent()
	{
		if ((float)this.recentAntiStuckEvents > 2f)
		{
			this.actor.seat.vehicle.stuck = true;
			if (this.isSquadLeader && this.squad.allowAutoLeaveVehicle)
			{
				this.squad.ContinueOnFoot();
			}
			this.recentAntiStuckEvents = 0;
			base.CancelInvoke("PopAntiStuckEvent");
		}
		this.recentAntiStuckEvents++;
		base.Invoke("PopAntiStuckEvent", 30f);
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0000430D File Offset: 0x0000250D
	private void PopAntiStuckEvent()
	{
		this.recentAntiStuckEvents--;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0004D840 File Offset: 0x0004BA40
	public void CreateRougeSquad()
	{
		List<ActorController> list = new List<ActorController>(1);
		list.Add(this);
		this.squad.SplitSquad(list);
		this.moveTimeoutAction.Start();
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0004D874 File Offset: 0x0004BA74
	private bool UpdateSprintFlag()
	{
		if (!this.hasPath || !this.modifier.canSprint || this.isSeated || this.isTraversingCorner || this.IsDeployingSmoke() || this.IsHalted() || this.IsStrafingTarget())
		{
			return false;
		}
		bool flag = this.HasSpottedTarget();
		if (this.isFleeing)
		{
			return true;
		}
		if (this.IsMeleeCharging())
		{
			return Vector3.Distance(this.actor.Position(), this.target.Position()) > this.actor.activeWeapon.configuration.effectiveRange;
		}
		if (!this.isAlert && this.forceUnalertMovementSpeed)
		{
			return false;
		}
		if (this.cachedIsFollowing)
		{
			return this.cachedFollowTargetDistance > 7f;
		}
		if (this.InSquad() && this.isSquadLeader)
		{
			if (this.squad.parentCluster != null && !this.IsTakingFire())
			{
				return true;
			}
			if (this.squad.AreFollowersLaggingBehind())
			{
				return false;
			}
		}
		return (!flag || this.targetDistance >= 50f) && (!this.sprintAction.TrueDone() || this.IsEnteringVehicle());
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0000431D File Offset: 0x0000251D
	private IEnumerator AiOrders()
	{
		yield return 0;
		for (;;)
		{
			this.isSprinting = this.UpdateSprintFlag();
			this.aboutToEnterCaptureZoneAttack = false;
			if (this.IsEnteringVehicle() && this.hasPath && !this.IsInTemporaryCover() && (this.targetVehicle.transform.position - this.vectorPath[this.vectorPath.Count - 1]).ToGround().magnitude > 4f)
			{
				this.GotoTargetVehicle(false);
			}
			if (this.squad == null)
			{
				yield return 0;
			}
			else
			{
				if (this.isSquadLeader)
				{
					if (this.hasPath && this.sprintCooldownAction.TrueDone() && !this.HasTarget())
					{
						this.StartSprint();
					}
					if (!this.hasPath && this.combatMovementHasOverridenLeaderPath)
					{
						this.squad.ReissueLastMoveSegment();
					}
					this.squad.Update();
					if (this.squad.order != null && this.squad.order.type == Order.OrderType.Attack && this.squad.order.target != null)
					{
						float num = Vector3.Distance(base.transform.position, this.squad.order.target.transform.position);
						float captureRange = this.squad.order.target.GetCaptureRange();
						this.aboutToEnterCaptureZoneAttack = (num < 2f * captureRange && num > captureRange);
					}
				}
				if (!this.isSeated && this.AllowCombatOverrideMovement())
				{
					if (this.isInCqcZone && (this.squad == null || this.squad.AllowTakeTemporaryCover()))
					{
						if (this.HasSpottedTarget())
						{
							this.FindTemporaryCoverTowardsPosition(this.target.CenterPosition());
						}
						else if (this.IsTakingFire())
						{
							this.FindTemporaryCoverTowardsDirection(this.takingFireDirection);
						}
					}
					else if (this.HasSpottedTarget() && !this.IsStrafingTarget() && this.targetDistance < 30f)
					{
						this.StrafeTarget();
					}
				}
				if (this.IsPatrolingBase())
				{
					if (!this.hasPath)
					{
						Vector3 vector = this.squad.order.source.RandomPatrolPosition();
						Debug.DrawRay(vector, Vector3.up * 10f, Color.blue, 100f);
						this.Goto(vector, false);
					}
				}
				else if (!this.isSquadLeader)
				{
					if (this.cachedIsFollowing)
					{
						if (!this.actor.parachuteDeployed && !this.GotoFollowPointIsCoolingDown())
						{
							this.GotoFollowPoint();
						}
					}
					else if (!this.HasPath() && this.squad.outsideTransportVehicle && !this.squad.pickingUpPassengers && this.squad.order != null)
					{
						this.Goto(this.squad.order.target.RandomPositionInCaptureZone(), false);
					}
					else if (this.squad.HasPlayerLeader() && this.isSeated && this.actor.seat.vehicle.claimingSquad != this.squad && !this.actor.seat.vehicle.playerIsInside)
					{
						this.LeaveVehicle(false);
					}
				}
				if (this.IsRepairing() && !this.actor.activeWeapon.CanRepair())
				{
					this.SwitchToRepairWeapon();
				}
				if (this.IsEnteringVehicle() && !this.hasPath)
				{
					List<Vector3> list = new List<Vector3>(1);
					list.Add(this.targetVehicle.transform.position);
					this.GotoAppendNodes(this.targetVehicle.transform.position, list, false);
				}
				yield return 0;
			}
		}
		yield break;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0000432C File Offset: 0x0000252C
	public bool IsInTemporaryCover()
	{
		return !this.getInTemporaryCoverAction.TrueDone();
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0000433C File Offset: 0x0000253C
	public override bool UseEyeMuzzle()
	{
		return (this.inCover || this.IsInTemporaryCover()) && !this.hasPath;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00004359 File Offset: 0x00002559
	private bool IsStrafingTarget()
	{
		return !this.strafeAction.TrueDone();
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0004D994 File Offset: 0x0004BB94
	private void StrafeTarget()
	{
		Vector3 vector = this.target.Position() - this.actor.Position();
		vector.y = 0f;
		Vector3 normalized = vector.normalized;
		Vector3 vector2 = new Vector3(-normalized.z, 0f, normalized.x);
		if (this.hasPath && !this.IsLastWaypoint() && UnityEngine.Random.Range(0f, 1f) < 0.5f)
		{
			if (Vector3.Dot(vector2, this.GetNextWaypointDelta()) < 0f)
			{
				vector2 = -vector2;
			}
		}
		else if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
		{
			vector2 = -vector2;
		}
		Vector3 b = (this.targetDistance < 10f) ? (-normalized) : normalized;
		this.strafeAction.StartLifetime(UnityEngine.Random.Range(1.4f, 3f));
		this.Goto(this.actor.Position() + vector2 * 2f + b, false);
		if (this.isSquadLeader)
		{
			this.combatMovementHasOverridenLeaderPath = true;
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0004DAB4 File Offset: 0x0004BCB4
	private void FindTemporaryCoverTowardsPosition(Vector3 target)
	{
		if (this.squad != null && !this.isSquadLeader && this.squad.shouldFollow && Vector3.Distance(this.actor.Position(), this.squad.Leader().actor.Position()) > 30f)
		{
			return;
		}
		if (this.HasCover() && this.cover.CoversPoint(target))
		{
			this.getInTemporaryCoverAction.Start();
			return;
		}
		CoverPoint coverPositionAgainstTarget = CoverManager.instance.GetCoverPositionAgainstTarget(this.actor.Position(), target);
		if (coverPositionAgainstTarget != null)
		{
			this.EnterCover(coverPositionAgainstTarget);
			this.getInTemporaryCoverAction.Start();
		}
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0004DB60 File Offset: 0x0004BD60
	private void FindTemporaryCoverTowardsDirection(Vector3 direction)
	{
		if (this.HasCover() && this.cover.CoversDirection(direction))
		{
			this.getInTemporaryCoverAction.Start();
			return;
		}
		CoverPoint coverPositionAgainstDirection = CoverManager.instance.GetCoverPositionAgainstDirection(this.actor.Position(), direction);
		if (coverPositionAgainstDirection != null)
		{
			this.EnterCover(coverPositionAgainstDirection);
			this.getInTemporaryCoverAction.Start();
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
	private bool GotoFollowPointIsCoolingDown()
	{
		int num = this.gotoFollowPointIterationCooldown;
		this.gotoFollowPointIterationCooldown = num + 1;
		return num <= 2 || !this.gotoFollowPointCooldown.TrueDone();
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0004DBF8 File Offset: 0x0004BDF8
	private void GotoFollowPoint()
	{
		this.isCompletingPlayerMoveOrder = false;
		this.gotoFollowPointIterationCooldown = 0;
		this.gotoFollowPointCooldown.Start();
		Vector3 vector;
		if (this.IsFollowingInVehicle())
		{
			if (this.squad.IsInVehicleFollowerRange(this.actor.seat.vehicle))
			{
				this.CancelPath(false);
				return;
			}
			vector = this.squad.GetVehicleFollowerPosition(this.actor.seat.vehicle);
		}
		else
		{
			vector = this.squad.GetFollowerPosition(this);
		}
		this.cachedFollowTargetDistance = Vector3.Distance(this.actor.Position(), vector);
		if (!this.actor.IsOnLadder() && this.cachedFollowTargetDistance > 3f)
		{
			if (!this.squad.Leader().actor.IsSwimming())
			{
				this.SetGotoVerifyPathDelegate(new AiActorController.DelVerifyPath(this.VerifyPathDestinationIsNotUnderwater));
			}
			this.GotoCheckCachedTargetNode(vector, false);
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0004DCD8 File Offset: 0x0004BED8
	private bool VerifyPathDestinationIsNotUnderwater(Path path)
	{
		bool result;
		try
		{
			if (!this.actor.IsSeated() && path.path[path.path.Count - 1].Tag == 3U)
			{
				result = false;
			}
			else
			{
				result = true;
			}
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00004369 File Offset: 0x00002569
	private IEnumerator AiTarget()
	{
		yield return 0;
		new TimedAction(3f, false);
		for (;;)
		{
			if (this.IsMeleeCharging())
			{
				this.Goto(this.target.Position() - this.actor.transform.forward, false);
			}
			if (this.target != null && this.KeepTarget() && !this.keepTargetAction.TrueDone())
			{
				if (ActorManager.ActorsCanSeeEachOther(this.actor, this.target) && this.actor.activeWeapon != null && this.GetWeaponEffectivenessAgainstTarget(this.actor.activeWeapon, this.target) > AiActorController.EvaluatedWeaponEffectiveness.No)
				{
					this.keepTargetAction.Start();
				}
			}
			else
			{
				Actor nextTargetOfActor = ActorManager.GetNextTargetOfActor(this.actor);
				if (nextTargetOfActor != null)
				{
					this.SetTarget(nextTargetOfActor);
				}
				else if (this.target != null)
				{
					this.DropTarget();
				}
			}
			if (this.HasSpottedTarget())
			{
				this.infantryFocus.UpdateTargetDistance(this.targetDistance);
				this.vehicleFocus.UpdateTargetDistance(this.targetDistance);
				this.hugeVehicleFocus.UpdateTargetDistance(this.targetDistance);
				if (this.AllowCombatOverrideMovement())
				{
					if (!this.engageTargetWhileProne)
					{
						this.CheckEngageTargetWhileProne();
					}
					if (!this.aboutToEnterCaptureZoneAttack && (this.isDriver || !this.isSquadLeader) && this.actor.activeWeapon != null && this.actor.activeWeapon.configuration.haltStrategy > Weapon.HaltStrategy.Never && !this.IsTakingFire())
					{
						Weapon.HaltStrategy haltStrategy = this.actor.activeWeapon.configuration.haltStrategy;
						bool flag = haltStrategy == Weapon.HaltStrategy.PreferredAnyRange || haltStrategy == Weapon.HaltStrategy.PreferredLongRange;
						bool flag2 = haltStrategy == Weapon.HaltStrategy.AlwaysLongRange || haltStrategy == Weapon.HaltStrategy.PreferredLongRange;
						if ((!flag || this.targetEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.Preferred || this.targetEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.PreferredRangePenalty) && (!flag2 || this.targetDistance > AiActorController.PARAMETERS.HALT_LONG_RANGE_ENGAGEMENT_RANGE))
						{
							this.Halt(1f);
						}
					}
				}
				yield return 0;
			}
			else
			{
				yield return 0;
			}
		}
		yield break;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00004378 File Offset: 0x00002578
	private void StartSprint()
	{
		this.sprintAction.StartLifetime(UnityEngine.Random.Range(5f, 12f));
		this.sprintCooldownAction.StartLifetime(UnityEngine.Random.Range(10f, 16f));
	}

	// Token: 0x0600036E RID: 878 RVA: 0x000043AE File Offset: 0x000025AE
	private void StopSprint()
	{
		this.sprintAction.Stop();
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0004DD30 File Offset: 0x0004BF30
	private bool ShouldSwitchWeaponForTarget()
	{
		return this.actor.activeWeapon == null || this.GetWeaponEffectivenessAgainstTarget(this.actor.activeWeapon, this.target) == AiActorController.EvaluatedWeaponEffectiveness.No || this.actor.activeWeapon.EffectivenessAgainst(this.target.GetTargetType()) != Weapon.Effectiveness.Preferred || (this.actor.activeWeapon.reloading && this.targetDistance < 40f);
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0004DDAC File Offset: 0x0004BFAC
	private void SetTarget(Actor target)
	{
		if (target != this.target && target.team != this.actor.team)
		{
			bool flag = target == this.lastTarget;
			this.target = target;
			this.lastTarget = target;
			this.targetEffectiveness = this.GetEffectivenessAgainst(this.target);
			this.forceAimFireAction.StartLifetime(UnityEngine.Random.Range(6f, 10f));
			this.engageTargetWhileProne = false;
			this.CheckEngageTargetWhileProne();
			if (this.ShouldSwitchWeaponForTarget())
			{
				this.SwitchToEffectiveWeapon(target);
			}
			this.targetDistance = ActorManager.ActorsDistance(this.actor, target);
			bool flag2 = Vector3.Distance(this.target.Position(), this.lastSeenTargetPosition) < 20f;
			float offsetMultiplier = 1f;
			if (flag2 && flag)
			{
				offsetMultiplier = 0.1f;
			}
			else if (flag)
			{
				offsetMultiplier = 0.8f;
			}
			else if (flag2)
			{
				offsetMultiplier = 0.3f;
			}
			this.StartAcquireTargetAimPenaltyAction(offsetMultiplier);
			if (this.KeepTarget())
			{
				this.keepTargetAction.Start();
			}
			this.attackRunAction.Stop();
			this.StopSprint();
		}
		this.planeForceManeuverAction.Start();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0004DED0 File Offset: 0x0004C0D0
	public void OnSeesEnemy(bool isPlayer, float detectionSpeedMultiplier)
	{
		if (isPlayer && this.slowTargetDetection)
		{
			DetectionUi.StartDetection(this);
		}
		this.isDetectingEnemyAction.Start();
		if (detectionSpeedMultiplier != 1f && (this.modifyDetectionSpeedAction.TrueDone() || detectionSpeedMultiplier > this.modifiedDetectionSpeed))
		{
			this.modifiedDetectionSpeed = detectionSpeedMultiplier;
			this.modifyDetectionSpeedAction.Start();
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0004DF2C File Offset: 0x0004C12C
	private void StartAcquireTargetAimPenaltyAction(float offsetMultiplier = 1f)
	{
		if (!this.HasTarget())
		{
			return;
		}
		Vector3 a = this.target.Position() - this.actor.Position();
		this.acquireTargetOffset = UnityEngine.Random.insideUnitSphere.normalized * AiActorController.PARAMETERS.ACQUIRE_TARGET_OFFSET_PER_METER * this.targetDistance + UnityEngine.Random.Range(-1f, 1f) * a * AiActorController.PARAMETERS.ACQUIRE_TARGET_DEPTH_EXTRA_OFFSET_PER_METER * offsetMultiplier * this.modifier.aquireTargetAimOffsetMultiplier;
		this.acquireTargetAction.StartLifetime(UnityEngine.Random.Range(1f, 1.2f) * AiActorController.PARAMETERS.ACQUIRE_TARGET_DURATION_BASE + AiActorController.PARAMETERS.ACQUIRE_TARGET_DURATION_PER_METER * this.targetDistance);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000043BB File Offset: 0x000025BB
	private void CheckEngageTargetWhileProne()
	{
		if (!this.isSeated && !this.hasPath && this.ShouldProneAtTargetRange(ActorManager.ActorsDistance(this.actor, this.target)))
		{
			this.engageTargetWhileProne = this.CanSeeTargetWhileProneRay();
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000043F2 File Offset: 0x000025F2
	private bool ShouldProneAtTargetRange(float distance)
	{
		return distance > 200f;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0004E004 File Offset: 0x0004C204
	private bool CanSeeTargetWhileProneRay()
	{
		if (!this.HasSpottedTarget())
		{
			return false;
		}
		Vector3 aOrigin = this.actor.Position();
		aOrigin.y += 0.2f;
		return ActorManager.CanSeeRayTest(this.actor, this.target, aOrigin, this.target.CenterPosition());
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0004E054 File Offset: 0x0004C254
	private bool KeepTarget()
	{
		if (this.target.IsHighPriorityTarget())
		{
			return true;
		}
		if (!this.isSeated)
		{
			return false;
		}
		Vehicle vehicle = this.actor.seat.vehicle;
		if (vehicle.isTurret)
		{
			return this.targetEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.Preferred;
		}
		return vehicle.IsAircraft() && (this.targetEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.Preferred || this.targetEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.PreferredRangePenalty);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000043FC File Offset: 0x000025FC
	private void DropTarget()
	{
		this.target = null;
		this.attackRunAction.Stop();
		if (!this.actor.fallenOver)
		{
			this.SwitchToPrimaryWeapon();
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00004423 File Offset: 0x00002623
	private bool SpamFire()
	{
		return this.actor.activeWeapon.configuration.auto || this.spamFireInput;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00004444 File Offset: 0x00002644
	private IEnumerator AiWeapon()
	{
		yield return 0;
		for (;;)
		{
			this.fire = !this.holdDownFireAction.TrueDone();
			this.spamFireInput = !this.spamFireInput;
			bool flag = false;
			int index = 0;
			if (!this.actor.IsSeated() && !this.actor.fallenOver && this.actor.hasSmokeScreen && this.deploySmokeAction.TrueDone() && this.actor.weapons[this.actor.smokeScreenSlot].HasAnyAmmo())
			{
				Vector3 zero = Vector3.zero;
				flag = ActorManager.GetSmokeTargetInRange(this.actor.team, this.actor.Position(), this.actor.weapons[this.actor.smokeScreenSlot].configuration.effectiveRange, out index, out zero);
				if (flag)
				{
					flag = !Physics.Linecast(this.actor.CenterPosition(), zero + new Vector3(0f, 3f, 0f), 8388609);
				}
			}
			if (this.HasSpottedTarget())
			{
				this.allowInstantReloadAction.Start();
			}
			if (this.actor.HasUnholsteredWeapon() && !this.actor.activeWeapon.HasAnyAmmo() && this.justFiredAction.TrueDone())
			{
				if (this.target != null)
				{
					if (this.HasEffectiveWeaponAgainst(this.target))
					{
						this.SwitchToEffectiveWeapon(this.target);
					}
					else
					{
						this.DropTarget();
						this.SwitchToPrimaryWeapon();
					}
				}
				else
				{
					this.SwitchToPrimaryWeapon();
				}
			}
			if (this.HasSpottedTarget() && this.squad.MayEngageTarget(this.target) && this.actor.activeWeapon != null && this.actor.activeWeapon.IsMeleeWeapon() && this.targetDistance < this.modifier.meleeChargeRange)
			{
				this.meleeChargeAction.Start();
			}
			if (this.actor.dead)
			{
				this.fire = false;
				yield return 0;
			}
			else if (this.IsDeployingSmoke())
			{
				this.fire = (this.deploySmokeAction.Ratio() > 0.7f);
				Debug.DrawLine(this.actor.CenterPosition(), this.smokeTarget, Color.green, 0.2f);
				yield return 0;
			}
			else if (this.IsRepairing())
			{
				if (this.actor.activeWeapon.CanRepair())
				{
					this.fire = this.SpamFire();
				}
				yield return 0;
			}
			else if (this.isSeated && this.actor.seat.vehicle.GetType() == typeof(Car) && this.isDriver && this.actor.seat.activeWeapon.GetType() == typeof(CarHorn))
			{
				this.fire = this.blockerAhead;
				yield return 0;
			}
			else if (this.isSeated && this.actor.seat.HasActiveWeapon() && this.actor.seat.activeWeapon.GetType() == typeof(Mortar))
			{
				this.fire = (this.HasSpottedTarget() && this.SpamFire());
				yield return 0;
			}
			else if (flag)
			{
				this.smokeTarget = ActorManager.CompleteSmokeTarget(this.actor.team, index).position;
				this.deploySmokeAction.Start();
				this.fire = false;
				this.actor.SwitchWeapon(this.actor.smokeScreenSlot);
			}
			else if (!this.HasSpottedTarget() && this.actor.hasAmmoBox && this.actor.weapons[this.actor.ammoBoxSlot].AmmoFull() && this.squad != null && !this.squad.ResupplyCoolingDown() && (this.squad.MemberNeedsResupply() || (!ActorManager.instance.player.dead && this.actor.team == ActorManager.instance.player.team && ActorManager.instance.player.needsResupply && Vector3.Distance(ActorManager.instance.player.transform.position, base.transform.position) < 10f)))
			{
				if (this.actor.activeWeapon == this.actor.weapons[this.actor.ammoBoxSlot])
				{
					if (!ActorManager.instance.player.dead && this.actor.team == ActorManager.instance.player.team && ActorManager.instance.player.needsResupply && Vector3.Distance(ActorManager.instance.player.transform.position, base.transform.position) < 15f)
					{
						this.LookAt(ActorManager.instance.player.transform.position);
					}
					bool flag2 = this.fire;
					this.fire = (!this.IsMovingToCover() && this.SpamFire());
					if (!flag2 && this.fire)
					{
						this.squad.OnResupplyThrown();
					}
				}
				else
				{
					this.actor.SwitchWeapon(this.actor.ammoBoxSlot);
				}
				yield return 0;
			}
			else if (!this.HasSpottedTarget() && this.actor.hasMedipack && this.actor.weapons[this.actor.medipackSlot].AmmoFull() && this.squad != null && !this.squad.MedipackCoolingDown() && (this.squad.MemberNeedsHealth() || (!ActorManager.instance.player.dead && this.actor.team == ActorManager.instance.player.team && ActorManager.instance.player.health < 80f && Vector3.Distance(ActorManager.instance.player.transform.position, base.transform.position) < 10f)))
			{
				if (this.actor.activeWeapon == this.actor.weapons[this.actor.medipackSlot])
				{
					if (!ActorManager.instance.player.dead && this.actor.team == ActorManager.instance.player.team && ActorManager.instance.player.health < 80f && Vector3.Distance(ActorManager.instance.player.transform.position, base.transform.position) < 10f)
					{
						this.LookAt(ActorManager.instance.player.transform.position);
					}
					bool flag3 = this.fire;
					this.fire = (!this.IsMovingToCover() && this.SpamFire());
					if (!flag3 && this.fire)
					{
						this.squad.OnMedipackThrown();
					}
				}
				else
				{
					this.actor.SwitchWeapon(this.actor.medipackSlot);
				}
				yield return 0;
			}
			else if (!this.HasSpottedTarget() && this.actor.activeWeapon != null)
			{
				if (this.allowInstantReloadAction.TrueDone() && !this.actor.activeWeapon.reloading)
				{
					for (int i = 0; i < this.actor.weapons.Length; i++)
					{
						if (this.actor.weapons[i] != null && !this.actor.weapons[i].AmmoFull() && this.actor.weapons[i].spareAmmo > 0)
						{
							this.actor.weapons[i].InstantlyReload();
						}
					}
					this.SwitchToPrimaryWeapon();
				}
				yield return 0;
			}
			else if (!this.actor.HasUnholsteredWeapon())
			{
				this.fire = false;
				yield return 0;
			}
			else if (this.HasSpottedTarget() && this.actor.activeWeapon.IsEmpty())
			{
				if (this.ShouldSwitchWeaponForTarget())
				{
					this.SwitchToEffectiveWeapon(this.target);
				}
				yield return 0;
			}
			else if (!this.actor.activeWeapon.configuration.auto && this.fire)
			{
				this.fire = false;
				yield return 0;
			}
			else
			{
				TargetTracker targetTracker = this.actor.activeWeapon.targetTracker;
				bool flag4 = targetTracker != null && targetTracker.useVehicleLock;
				bool flag5 = targetTracker != null && targetTracker.usePointTarget;
				if (this.actor.activeWeapon.isBomb)
				{
					this.fire = this.actor.activeWeapon.ShouldDropBombs(this.GetTargetAcquiredPosition(), this.target.Velocity());
					if (this.fire)
					{
						this.dropBombsAction.Start();
						while (!this.dropBombsAction.TrueDone())
						{
							yield return 0;
						}
					}
					else
					{
						yield return 0;
					}
				}
				else if (flag4)
				{
					if ((this.squad == null || this.squad.MayEngageTarget(this.target)) && targetTracker.TargetIsLocked())
					{
						Vehicle vehicleTarget = targetTracker.vehicleTarget;
						bool flag6 = vehicleTarget.maxHealth > 2000f;
						bool flag7;
						if (!vehicleTarget.AnySeatsTaken())
						{
							flag7 = false;
						}
						else if (vehicleTarget.hasCountermeasures)
						{
							flag7 = (!vehicleTarget.CountermeasuresAreActive() && !vehicleTarget.IsBeingTrackedByMissile());
						}
						else
						{
							flag7 = (flag6 || !vehicleTarget.IsBeingTrackedByMissile());
						}
						if (flag7)
						{
							this.fire = this.spamFireInput;
						}
						else
						{
							this.fire = false;
						}
					}
					else
					{
						this.fire = false;
					}
					yield return 0;
				}
				else if (flag5)
				{
					if ((this.squad == null || this.squad.MayEngageTarget(this.target)) && targetTracker.PointTargetIsAvailable())
					{
						this.fire = this.spamFireInput;
					}
					else
					{
						this.fire = false;
					}
					yield return 0;
				}
				else
				{
					Vector3 vector = this.actor.WeaponMuzzlePosition();
					Vector3 lhs = this.GetTargetAcquiredPosition() - vector + this.WeaponLead();
					float magnitude = lhs.magnitude;
					Vector3 vector2;
					if (this.isSeated && this.actor.seat.HasActiveWeapon())
					{
						vector2 = this.actor.activeWeapon.CurrentMuzzle().forward;
					}
					else
					{
						vector2 = this.FacingDirection();
					}
					Vector3 normalized = Vector3.Cross(vector2, Vector3.up).normalized;
					Vector3 rhs = Vector3.Cross(vector2, normalized);
					float f = Vector3.Dot(lhs, normalized);
					float f2 = Vector3.Dot(lhs, rhs);
					float num = this.actor.activeWeapon.configuration.aiAllowedAimSpread + this.target.GetTargetSize() + this.GetSwingMagnitude();
					if (this.forceAimFireAction.TrueDone() && (!this.actor.IsDriver() || !this.actor.seat.vehicle.IsAircraft()))
					{
						num += 20f;
					}
					bool flag8 = Vector3.Dot(lhs, vector2) > 0f && Mathf.Abs(f) < AiActorController.PARAMETERS.AI_FIRE_RECTANGLE_BOUND * num && Mathf.Abs(f2) < AiActorController.PARAMETERS.AI_FIRE_RECTANGLE_BOUND * num;
					bool flag9 = false;
					if (!this.actor.activeWeapon.IsMountedWeapon() && this.actor.activeWeapon.teammateDangerRange > 0f && magnitude > 30f)
					{
						flag9 = !this.acquireTargetAction.TrueDone();
					}
					bool flag10 = this.squad == null || this.squad.MayEngageTarget(this.target);
					if (!flag9 && flag10 && flag8 && this.CanSeeActor(this.target))
					{
						Ray ray = new Ray(vector + 0.3f * vector2, vector2);
						RaycastHit raycastHit;
						if (magnitude > 5f && Physics.Raycast(ray, out raycastHit, magnitude - 5f, 5376))
						{
							if (raycastHit.collider.gameObject.layer == 8 || raycastHit.collider.gameObject.layer == 10)
							{
								Hitbox component = raycastHit.collider.GetComponent<Hitbox>();
								this.fire = (component == null || component.parent == null || component.parent.team != this.actor.team);
							}
							else
							{
								this.fire = true;
							}
						}
						else
						{
							this.fire = true;
						}
						if (this.fire)
						{
							this.holdDownFireAction.StartLifetime(UnityEngine.Random.Range(0.3f, 1.2f) + (this.actor.activeWeapon.configuration.useChargeTime ? this.actor.activeWeapon.configuration.chargeTime : 0f));
							this.forceAimFireAction.StartLifetime(UnityEngine.Random.Range(6f, 10f));
						}
					}
					if (this.isSeated || this.target.IsSeated())
					{
						yield return 0;
					}
					else
					{
						float.IsNaN(magnitude / 100f);
						yield return 0;
					}
				}
			}
			if (this.fire)
			{
				this.justFiredAction.Start();
				this.allowInstantReloadAction.Start();
				this.calmDownStartTimestamp = Time.time;
			}
		}
		yield break;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00004453 File Offset: 0x00002653
	public bool JustFired()
	{
		return !this.justFiredAction.TrueDone();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00004463 File Offset: 0x00002663
	private IEnumerator AiTrack()
	{
		yield return 0;
		for (;;)
		{
			if (this.HasSpottedTarget())
			{
				AiActorController.EvaluatedWeaponEffectiveness effectivenessAgainst = this.GetEffectivenessAgainst(this.target);
				if (this.target.dead || this.target.IsInBurningVehicle() || effectivenessAgainst == AiActorController.EvaluatedWeaponEffectiveness.No)
				{
					this.DropTarget();
				}
				else if (this.CanSeeActor(this.target))
				{
					this.lastSeenTargetPosition = this.target.Position();
					this.lastSeenTargetVelocity = this.target.Velocity();
					this.targetDistance = Vector3.Distance(this.actor.Position(), this.lastSeenTargetPosition);
				}
				else if (!this.isSeated && !this.HasCover() && !this.target.IsSeated() && !this.cachedIsFollowing && !this.IsOnPlayerSquad() && this.targetDistance < 30f)
				{
					this.Goto(this.lastSeenTargetPosition + this.lastSeenTargetVelocity * 2f, false);
				}
				this.targetEffectiveness = effectivenessAgainst;
			}
			yield return 0;
		}
		yield break;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00004472 File Offset: 0x00002672
	private IEnumerator AiScan()
	{
		yield return 0;
		for (;;)
		{
			yield return 0;
			if (this.lookAroundDisabledAction.TrueDone() && !this.freezeFacingDirection && !this.HasSpottedTarget())
			{
				Vector3 vector;
				if (this.IsTakingFire())
				{
					vector = this.takingFireDirection;
				}
				if (this.InCover())
				{
					vector = this.cover.transform.forward + UnityEngine.Random.insideUnitSphere * 0.2f;
				}
				else
				{
					MountedWeapon mountedWeapon = null;
					if (this.isSeated)
					{
						mountedWeapon = this.actor.seat.activeWeapon;
					}
					bool flag = mountedWeapon != null && mountedWeapon.IsClampedTurret();
					bool flag2 = !this.isSquadLeader && UnityEngine.Random.Range(0f, 1f) < 0.5f;
					if (flag)
					{
						vector = mountedWeapon.GetClampedTurretRandomLookDirection();
					}
					else if (flag2)
					{
						Component component = ActorManager.RandomSpawnPoint();
						SpawnPoint spawnPoint = ActorManager.RandomSpawnPoint();
						float t = Mathf.Pow(UnityEngine.Random.Range(0f, 1.2f), 2f);
						vector = (Vector3.Lerp(component.transform.position, spawnPoint.transform.position, t) - this.actor.CenterPosition()).normalized;
						vector.y += UnityEngine.Random.Range(0f, 0.2f);
					}
					else
					{
						bool flag3 = this.isSquadLeader || this.IsSprinting() || UnityEngine.Random.Range(0f, 1f) < 0.7f;
						vector = this.FacingDirection() * 0.5f + UnityEngine.Random.insideUnitSphere;
						if (this.IsSprinting())
						{
							vector = Vector3.zero;
						}
						if (this.hasPath && flag3)
						{
							vector += this.actor.Velocity().normalized * 1.5f;
						}
						if (this.squad != null)
						{
							if (!flag3)
							{
								vector += 0.4f * this.SquadFacingBias();
							}
							else
							{
								vector += 0.1f * this.SquadFacingBias();
							}
						}
						vector.Normalize();
						if (this.IsSprinting())
						{
							vector.y = 0f;
						}
						else
						{
							vector.y *= UnityEngine.Random.Range(0.1f, 1f);
							if (vector.y < 0f)
							{
								vector.y *= 0.2f;
							}
						}
					}
				}
				if (vector != Vector3.zero)
				{
					this.targetLookDirection = vector;
				}
				this.lookAroundDisabledAction.StartLifetime(UnityEngine.Random.Range(0.8f, 3f));
			}
		}
		yield break;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0004E0BC File Offset: 0x0004C2BC
	private Vector3 SquadFacingBias()
	{
		Vector3 a = Vector3.zero;
		int num = 0;
		foreach (ActorController actorController in this.squad.members)
		{
			if (actorController != this)
			{
				a -= actorController.transform.position - base.transform.position;
				num++;
			}
		}
		if (num == 0)
		{
			return Vector3.zero;
		}
		return a / (float)num;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0004E158 File Offset: 0x0004C358
	public AiActorController.EvaluatedWeaponEffectiveness GetEffectivenessAgainst(Actor target)
	{
		if (this.isSeated && !this.actor.seat.CanUsePersonalWeapons())
		{
			return this.GetMaxEffectiveness(this.actor.seat.weapons, target);
		}
		return this.GetMaxEffectiveness(this.actor.weapons, target);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0004E1AC File Offset: 0x0004C3AC
	public AiActorController.EvaluatedWeaponEffectiveness GetMaxEffectiveness(IEnumerable<Weapon> weapons, Actor target)
	{
		AiActorController.EvaluatedWeaponEffectiveness evaluatedWeaponEffectiveness = AiActorController.EvaluatedWeaponEffectiveness.No;
		foreach (Weapon weapon in weapons)
		{
			if (!(weapon == null))
			{
				AiActorController.EvaluatedWeaponEffectiveness weaponEffectivenessAgainstTarget = this.GetWeaponEffectivenessAgainstTarget(weapon, target);
				if (weaponEffectivenessAgainstTarget > evaluatedWeaponEffectiveness)
				{
					evaluatedWeaponEffectiveness = weaponEffectivenessAgainstTarget;
				}
			}
		}
		return evaluatedWeaponEffectiveness;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00004481 File Offset: 0x00002681
	public bool OnlyAllowPreferredOrHighPriorityTargets()
	{
		return !this.onlyPreferredOrHighPriorityTargetAction.TrueDone();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0004E208 File Offset: 0x0004C408
	public bool HasEffectiveWeaponAgainst(Actor targetActor)
	{
		if (this.isSeated && this.actor.seat.HasAnyMountedWeapons())
		{
			foreach (MountedWeapon weapon in this.actor.seat.weapons)
			{
				if (this.GetWeaponEffectivenessAgainstTarget(weapon, targetActor) > AiActorController.EvaluatedWeaponEffectiveness.No)
				{
					return true;
				}
			}
		}
		else
		{
			foreach (Weapon weapon2 in this.actor.weapons)
			{
				if (weapon2 != null && this.GetWeaponEffectivenessAgainstTarget(weapon2, targetActor) > AiActorController.EvaluatedWeaponEffectiveness.No)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0004E29C File Offset: 0x0004C49C
	private AiActorController.EvaluatedWeaponEffectiveness GetWeaponEffectivenessAgainstTarget(Weapon weapon, Actor targetActor)
	{
		if (!weapon.HasAnyAmmo() || !targetActor.CanBeDamagedBy(weapon) || !weapon.CanAimAt(targetActor.Position()))
		{
			return AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		if (!targetActor.IsHighPriorityTarget() && targetActor.closestEnemyDistance < weapon.teammateDangerRange)
		{
			return AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		float num = ActorManager.ActorsDistance(this.actor, targetActor);
		Actor.TargetType targetType = targetActor.GetTargetType();
		int num2 = (int)weapon.EvaluateDifficulty(num, targetType);
		int num3 = (int)this.skill;
		if (targetActor == this.squad.attackTarget)
		{
			num3 = 5;
		}
		else if (this.skill == AiActorController.SkillLevel.Beginner)
		{
			num3 = 1;
		}
		else if (Time.time - this.calmDownStartTimestamp > 20f)
		{
			num3++;
		}
		if (num3 < num2)
		{
			return AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		bool flag = weapon.IsMeleeWeapon();
		bool flag2 = flag && num < this.modifier.meleeChargeRange;
		if (flag && !flag2)
		{
			return AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		float effectiveRangeRatio = flag2 ? 0f : (num / (1f + weapon.configuration.effectiveRange));
		return AiActorController.BaseToEvaluatedEffectiveness(weapon.EffectivenessAgainst(targetType), effectiveRangeRatio);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0004E398 File Offset: 0x0004C598
	public static AiActorController.EvaluatedWeaponEffectiveness BaseToEvaluatedEffectiveness(Weapon.Effectiveness e, float effectiveRangeRatio)
	{
		if (e == Weapon.Effectiveness.No)
		{
			return AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		AiActorController.EvaluatedWeaponEffectiveness evaluatedWeaponEffectiveness = (e == Weapon.Effectiveness.Preferred) ? AiActorController.EvaluatedWeaponEffectiveness.Preferred : AiActorController.EvaluatedWeaponEffectiveness.Standard;
		if (effectiveRangeRatio > 2f)
		{
			evaluatedWeaponEffectiveness = AiActorController.EvaluatedWeaponEffectiveness.No;
		}
		else if (effectiveRangeRatio > 1f)
		{
			if (evaluatedWeaponEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.Preferred)
			{
				evaluatedWeaponEffectiveness = AiActorController.EvaluatedWeaponEffectiveness.PreferredRangePenalty;
			}
			else if (evaluatedWeaponEffectiveness == AiActorController.EvaluatedWeaponEffectiveness.Standard)
			{
				evaluatedWeaponEffectiveness = AiActorController.EvaluatedWeaponEffectiveness.StandardRangePenalty;
			}
		}
		return evaluatedWeaponEffectiveness;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00004491 File Offset: 0x00002691
	public override void LookAt(Vector3 position)
	{
		this.LookDirection(position - this.actor.Position());
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000044AA File Offset: 0x000026AA
	private void LookDirection(Vector3 direction)
	{
		if (direction.sqrMagnitude > 0.0001f)
		{
			this.targetLookDirection = direction;
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000044C1 File Offset: 0x000026C1
	public void DisableAutoLookAround(float duration)
	{
		this.lookAroundDisabledAction.StartLifetime(duration);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0004E3D8 File Offset: 0x0004C5D8
	private void OnPathComplete(Path p)
	{
		this.calculatingPath = false;
		if (this.verifyPathCallback != null)
		{
			if (!this.verifyPathCallback(p))
			{
				this.verifyPathCallback = null;
				return;
			}
			this.verifyPathCallback = null;
		}
		if (!p.error)
		{
			this.hasPath = true;
			this.originalPathLength = p.vectorPath.Count;
			this.vectorPath = p.vectorPath;
			this.laddersInPath = new List<AiActorController.LadderPathSection>();
			uint num = (this.actor.team == 0) ? 4U : 5U;
			bool flag = this.ShouldUseAlternativePath();
			bool flag2 = flag && !this.actor.IsSeated() && !this.squad.hasSetSmokeTarget;
			int num2 = 0;
			if (flag2)
			{
				num2 = p.path.Count / 3;
			}
			for (int i = 0; i < p.path.Count; i++)
			{
				GraphNode graphNode = p.path[i];
				if (graphNode.Tag == 31U)
				{
					Ladder ladderWithNode = ActorManager.GetLadderWithNode(graphNode);
					if (!(ladderWithNode == null))
					{
						float num3 = 100000f;
						int enterWaypointIndex = 0;
						for (int j = 0; j < this.vectorPath.Count; j++)
						{
							float num4 = Vector3.Distance(this.vectorPath[j], (Vector3)graphNode.position);
							if (num4 < num3)
							{
								num3 = num4;
								enterWaypointIndex = j;
							}
						}
						this.laddersInPath.Add(new AiActorController.LadderPathSection(ladderWithNode, ladderWithNode.bottomNode == graphNode, enterWaypointIndex));
						i++;
					}
				}
				else if (flag2 && i > num2 && i < p.path.Count - 5 && (graphNode.Tag == num || graphNode.Tag == 6U))
				{
					float num5 = Vector3.Distance(p.vectorPath[0], p.vectorPath[p.vectorPath.Count - 1]) * 1.4f / 3.2f;
					if (ActorManager.RegisterSmokeTarget((Vector3)p.path[Mathf.Min(i + 2, p.path.Count - 1)].position, this.actor.team, num5 * 1.2f))
					{
						flag2 = false;
						this.squad.hasSetSmokeTarget = true;
					}
				}
			}
			if (flag)
			{
				int num6 = p.path.Count / 5;
				PathfindingBox.Type pathfindingGraphType = this.GetPathfindingGraphType();
				for (int k = 0; k < num6; k++)
				{
					PathfindingManager.PushAlternatePathNode(p.path[UnityEngine.Random.Range(0, p.path.Count)], pathfindingGraphType, this.actor.team);
				}
			}
			if (this.pathAppendedNodes != null)
			{
				this.vectorPath.AddRange(this.pathAppendedNodes);
			}
			this.waypoint = 0;
			this.nextWaypointIsCorner = false;
			this.avoidedVehicles.Clear();
			if (!this.inCover && this.HasCover())
			{
				this.vectorPath.Add(this.cover.transform.position);
			}
			if (this.vectorPath.Count > 1)
			{
				Vector3 vector = this.vectorPath[0];
				Vector3 v = this.vectorPath[1] - vector;
				Vector3 v2 = this.actor.Position() - vector;
				float magnitude = v2.ToGround().magnitude;
				Vector3 a = v.ToGround();
				float magnitude2 = v.magnitude;
				Vector3 vector2 = a / magnitude2;
				float num7 = Vector3.Dot(v2.ToGround(), vector2);
				if (num7 > -0.2f)
				{
					float num8 = num7 + 2f;
					if (num8 > magnitude2)
					{
						this.waypoint++;
					}
					else
					{
						this.vectorPath[0] = vector + vector2 * num8;
					}
				}
			}
			TriangleMeshNode triangleMeshNode = p.path[p.path.Count - 1] as TriangleMeshNode;
			if (triangleMeshNode != null)
			{
				this.cachedTargetNode = triangleMeshNode;
			}
			if (flag)
			{
				this.DisableAlternativePathPenalty();
				return;
			}
		}
		else
		{
			this.hasPath = false;
			if (PathfindingManager.HasAnyBoatGraphs() && !this.isSeated && this.actor.IsSwimming() && !this.swimmingToShore)
			{
				this.SwimToShore();
			}
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x000044CF File Offset: 0x000026CF
	private bool ShouldUseAlternativePath()
	{
		return this.isSquadLeader && !this.IsInTemporaryCover() && this.squad != null && this.squad.order != null && this.squad.order.type == Order.OrderType.Attack;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0000450B File Offset: 0x0000270B
	private void SwimToShore()
	{
		this.swimmingToShore = true;
		this.seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.Original;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00004525 File Offset: 0x00002725
	private void ReachedShore()
	{
		this.swimmingToShore = false;
		if (!this.isSeated)
		{
			this.seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;
		}
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0004E80C File Offset: 0x0004CA0C
	private void RecalculatePath()
	{
		if (!this.hasPath)
		{
			return;
		}
		Vector3 destination = this.lastGotoPoint;
		this.CancelPath(false);
		if (this.IsEnteringVehicle())
		{
			this.GotoTargetVehicle(false);
			return;
		}
		this.GotoAppendNodes(destination, this.pathAppendedNodes, false);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00004547 File Offset: 0x00002747
	public void EnableAlternativePathPenalty()
	{
		this.seeker.tagPenalties[(this.actor.team == 0) ? 4 : 5] = 30000;
		this.seeker.tagPenalties[6] = 30000;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0000457D File Offset: 0x0000277D
	private void DisableAlternativePathPenalty()
	{
		this.seeker.tagPenalties[(this.actor.team == 0) ? 4 : 5] = 0;
		this.seeker.tagPenalties[6] = 0;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0004E850 File Offset: 0x0004CA50
	public PathfindingBox.Type GetPathfindingGraphType()
	{
		if (!this.isDriver)
		{
			if (PathfindingManager.HasAnyBoatGraphs())
			{
				if (this.swimmingToShore)
				{
					return PathfindingBox.Type.Boat;
				}
				if (this.actor.IsSwimming() && this.IsEnteringVehicle() && (this.targetVehicle.IsWatercraft() || this.targetVehicle.IsInWater()))
				{
					return PathfindingBox.Type.Boat;
				}
			}
			return PathfindingBox.Type.Infantry;
		}
		if (this.aquatic)
		{
			return PathfindingBox.Type.Boat;
		}
		return PathfindingBox.Type.Car;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0004E8B4 File Offset: 0x0004CAB4
	public void FleeFrom(Vector3 fleePoint)
	{
		if (this.isSeated || this.isSeated)
		{
			return;
		}
		FleePath fleePath = FleePath.Construct(this.actor.Position(), fleePoint, 10000, null);
		fleePath.aimStrength = 1f;
		fleePath.calculatePartial = true;
		fleePath.spread = 4000;
		this.seeker.StartPath(fleePath, null, PathfindingManager.infantryGraphMask);
		this.lastWaypoint = this.actor.Position();
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000045AB File Offset: 0x000027AB
	public void SetGotoVerifyPathDelegate(AiActorController.DelVerifyPath verifyPathCallback)
	{
		this.verifyPathCallback = verifyPathCallback;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000045B4 File Offset: 0x000027B4
	public void OverrideDefaultMovement()
	{
		this.isDefaultMovementOverridden = true;
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000045BD File Offset: 0x000027BD
	public void ReleaseDefaultMovementOverride()
	{
		this.isDefaultMovementOverridden = false;
	}

	// Token: 0x06000393 RID: 915 RVA: 0x000045C6 File Offset: 0x000027C6
	public void MarkCompletingPlayerMoveOrder()
	{
		this.isCompletingPlayerMoveOrder = true;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x000045CF File Offset: 0x000027CF
	public void Goto(Vector3 destination, bool isMovementOverride = false)
	{
		this.Goto(destination, null, this.GetPathfindingGraphType(), isMovementOverride);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x000045E0 File Offset: 0x000027E0
	public void GotoAppendNodes(Vector3 destination, List<Vector3> appendedNodes, bool isMovementOverride = false)
	{
		this.Goto(destination, appendedNodes, this.GetPathfindingGraphType(), isMovementOverride);
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0004E930 File Offset: 0x0004CB30
	public void Goto(Squad.MovePathSegment pathSegment, bool isMovementOverride = false)
	{
		PathfindingBox.Type graphType = pathSegment.forceGraphType ? pathSegment.graphType : this.GetPathfindingGraphType();
		this.Goto(pathSegment.destination, pathSegment.appendedNodes, graphType, isMovementOverride);
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0004E968 File Offset: 0x0004CB68
	private void Goto(Vector3 destination, List<Vector3> appendedNodes, PathfindingBox.Type graphType, bool isMovementOverride = false)
	{
		this.combatMovementHasOverridenLeaderPath = false;
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		this.reachedWaypoint = false;
		if (this.flying && this.isDriver)
		{
			this.flightTargetPosition = destination;
			this.hasFlightTargetPosition = true;
			return;
		}
		if (this.calculatingPath || (this.isSeated && !this.isDriver))
		{
			return;
		}
		if (!this.hasPath || Vector3.Distance(this.vectorPath[this.vectorPath.Count - 1], destination) > 2f)
		{
			this.calculatingPath = true;
			int mask = PathfindingManager.infantryGraphMask;
			if (graphType == PathfindingBox.Type.Car)
			{
				mask = PathfindingManager.carGraphMask;
			}
			else if (graphType == PathfindingBox.Type.Boat)
			{
				mask = PathfindingManager.boatGraphMask;
			}
			this.lastGotoPoint = destination;
			this.pathAppendedNodes = appendedNodes;
			Vector3 vector = this.actor.Position();
			RaycastHit raycastHit;
			if (this.actor.parachuteDeployed && Physics.SphereCast(new Ray(vector, Vector3.down), 0.3f, out raycastHit, 9999f, 2232321))
			{
				vector = raycastHit.point;
			}
			if (this.ShouldUseAlternativePath())
			{
				this.EnableAlternativePathPenalty();
			}
			this.seeker.StartPath(vector, destination, null, mask);
			this.lastWaypoint = base.transform.position;
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0004EAA4 File Offset: 0x0004CCA4
	public void GotoCheckCachedTargetNode(Vector3 targetPoint, bool isMovementOverride = false)
	{
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		this.Goto(targetPoint, false);
		if (this.cachedTargetNode == null)
		{
			this.Goto(targetPoint, false);
			return;
		}
		if (!this.cachedTargetNode.ContainsPoint((Int3)targetPoint))
		{
			this.Goto(targetPoint, false);
			return;
		}
		if (this.hasPath)
		{
			this.vectorPath[this.vectorPath.Count - 1] = targetPoint;
			return;
		}
		this.GotoDirect(targetPoint, false);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0004EB2C File Offset: 0x0004CD2C
	public void GotoDirect(Vector3 targetPoint, bool isMovementOverride = false)
	{
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		this.calculatingPath = false;
		this.hasPath = true;
		this.vectorPath = new List<Vector3>(1);
		this.waypoint = 0;
		this.vectorPath.Add(targetPoint);
		this.avoidedVehicles.Clear();
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000045F1 File Offset: 0x000027F1
	public void CancelPath(bool isMovementOverride = false)
	{
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		this.calculatingPath = false;
		this.verifyPathCallback = null;
		this.hasPath = false;
		this.moveTimeoutAction.Start();
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0004EB80 File Offset: 0x0004CD80
	private Vector3 GetTargetAcquireOffset()
	{
		float f = this.acquireTargetAction.Ratio();
		return this.acquireTargetOffset * (1f - Mathf.Pow(f, 2f));
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0000461F File Offset: 0x0000281F
	private AiActorController.LadderPathSection NextLadderInPath()
	{
		if (this.laddersInPath == null || this.laddersInPath.Count == 0)
		{
			return null;
		}
		return this.laddersInPath[0];
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00004644 File Offset: 0x00002844
	private void PopLadderInPath()
	{
		this.waypoint = this.laddersInPath[0].enterWaypoint;
		this.laddersInPath.RemoveAt(0);
		this.NextWaypoint();
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0004EBB8 File Offset: 0x0004CDB8
	private void UpdateLadder()
	{
		if (this.isSeated)
		{
			return;
		}
		if (this.actor.IsOnLadder())
		{
			AiActorController.LadderPathSection ladderPathSection = this.NextLadderInPath();
			if (ladderPathSection != null)
			{
				if (ladderPathSection.goUp && this.actor.CanExitLadderAtTop())
				{
					this.actor.ExitLadder();
					this.PopLadderInPath();
					return;
				}
				if (!ladderPathSection.goUp && this.actor.CanExitLadderAtBottom())
				{
					this.actor.ExitLadder();
					this.PopLadderInPath();
					return;
				}
			}
			else
			{
				this.actor.ExitLadder();
			}
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0004EC40 File Offset: 0x0004CE40
	private void UpdateInfantryMovement()
	{
		this.isTraversingCorner = false;
		if (this.hasPath)
		{
			Vector3 b = this.currentWaypoint - this.lastWaypoint;
			if (this.hasFutureWaypoint)
			{
				float num = Mathf.Max(1f, 1f * Mathf.Max(this.movementSpeed, 3.2f)) - this.waypointDistance;
				bool flag = num > 0f;
				if (flag)
				{
					Vector3 normalized = (this.futureWaypoint - this.currentWaypoint).normalized;
					this.infantryPathLookAheadPoint = this.currentWaypoint + normalized * num;
					this.infantryPathLookAheadPoint.y = this.infantryPathLookAheadPoint.y + 1f;
				}
				else
				{
					this.infantryPathLookAheadPoint = this.currentWaypoint;
					this.infantryPathLookAheadPoint.y = this.infantryPathLookAheadPoint.y + 1f;
				}
				this.isTraversingCorner = (flag && this.nextWaypointIsCorner);
				return;
			}
			this.infantryPathLookAheadPoint = this.currentWaypoint + b;
			this.infantryPathLookAheadPoint.y = this.infantryPathLookAheadPoint.y + 1f;
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0004ED54 File Offset: 0x0004CF54
	private void UpdateMovementSpeed()
	{
		float num = 0.5f;
		if (this.hasPath)
		{
			num = 3.2f;
			if (this.actor.immersedInWater)
			{
				num = 4.5f;
			}
			else if (!this.isAlert && this.forceUnalertMovementSpeed)
			{
				num = 1f;
			}
			else if (this.IsSprinting())
			{
				num = Mathf.Lerp(5.5f, 7f, this.actor.GetBonusSprintAmount());
				if (this.cachedIsFollowing)
				{
					num += 1f;
				}
			}
			else if (this.IsStrafingTarget())
			{
				num = 1.5f;
			}
			else if (this.isTraversingCorner && this.isInCqcZone)
			{
				num = 1.5f;
			}
			else if (this.HasSpottedTarget())
			{
				if (!this.IsMeleeCharging() && Vector3.Distance(this.target.Position(), this.actor.Position()) < 50f)
				{
					num = 2f;
				}
				else
				{
					num = 3.2f;
				}
			}
			else if (this.cachedIsFollowing)
			{
				num = Mathf.Clamp(2f * this.cachedFollowTargetDistance, 1f, 3.2f);
			}
			if (this.Crouch())
			{
				num = Mathf.Min(num, 2f);
			}
			else if (this.Prone())
			{
				num = Mathf.Min(num, 1f);
			}
			num *= this.actor.speedMultiplier;
		}
		if (this.isSquadLeader)
		{
			num = Mathf.Min(num, this.squad.GetSpeedRestriction());
		}
		if (this.movementSpeed > num || (this.cachedIsFollowing && this.squad.HasPlayerLeader()) || this.IsMeleeCharging())
		{
			this.movementSpeed = num;
			return;
		}
		this.movementSpeed = Mathf.MoveTowards(this.movementSpeed, num, 10f * Time.deltaTime);
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0004EF14 File Offset: 0x0004D114
	private void Update()
	{
		if (this.actor.dead)
		{
			return;
		}
		this.cachedIsFollowing = this.IsFollowing();
		this.isInCqcZone = CoverManager.instance.IsInCqcCell(this.actor.Position());
		if (!this.isAlert && (this.HasSpottedTarget() || this.IsTakingFire() || ActorManager.ActorCanHearEnemy(this.actor)))
		{
			this.SetAlert();
		}
		if (this.isAlert && !this.isSeated)
		{
			this.UpdateInfantryMovement();
		}
		this.UpdateMovementSpeed();
		this.UpdateLadder();
		if (this.isFleeing && this.fleeAndDiveAction.TrueDone())
		{
			Vector3 a = this.actor.Position() - this.fleeFromPosition;
			float magnitude = a.magnitude;
			if (!this.actor.fallenOver && magnitude < this.diveRange)
			{
				Vector3 force = a / magnitude * 200f;
				this.actor.KnockOver(force);
			}
			this.isFleeing = false;
		}
		if (this.isDriver && !this.actor.seat.vehicle.isTurret)
		{
			Type type = this.actor.seat.vehicle.GetType();
			if (type == typeof(Helicopter))
			{
				this.aircraftInput = this.GenerateHelicopterInput();
			}
			else if (type == typeof(ArcadeCar) || type == typeof(AnimationDrivenVehicle))
			{
				this.carInput = this.GenerateCarInput();
			}
			else if (type == typeof(Boat))
			{
				this.carInput = this.GenerateBoatInput();
			}
			else
			{
				this.aircraftInput = this.GenerateAirplaneInput();
			}
		}
		if (this.slowTargetDetection)
		{
			if (!this.isDetectingEnemyAction.TrueDone())
			{
				float num = this.slowTargetDetectionRate;
				if (!this.modifyDetectionSpeedAction.TrueDone())
				{
					num *= this.modifiedDetectionSpeed;
				}
				this.targetDetectionProgress = Mathf.MoveTowards(this.targetDetectionProgress, 1f, num * Time.deltaTime);
				if (this.targetDetectionProgress >= 1f)
				{
					this.OnDetectedEnemy();
				}
			}
			else
			{
				this.targetDetectionProgress = Mathf.MoveTowards(this.targetDetectionProgress, 0f, 0.3f * Time.deltaTime);
			}
		}
		if (!this.HasSpottedTarget())
		{
			this.infantryFocus.AutoGain();
			this.vehicleFocus.AutoGain();
			this.hugeVehicleFocus.AutoGain();
		}
		if (this.actor.parachuteDeployed && this.hasPath && this.waypoint < this.vectorPath.Count - 2)
		{
			Vector3 nextWaypointDelta = this.GetNextWaypointDelta();
			if (nextWaypointDelta.ToGround().magnitude / -nextWaypointDelta.y < 0.53846157f)
			{
				this.NextWaypoint();
			}
		}
		float num2 = Time.time + this.randomTimeOffset;
		Vector3 b = new Vector3(Mathf.Sin(num2 * 3.1f), Mathf.Cos(num2 * 5.3f), Mathf.Cos(num2 * 3.7f)) * AiActorController.PARAMETERS.AIM_BASE_SWAY;
		this.facingDirectionVector = this.lookRotation * Vector3.forward + b;
		if (!this.InCover() || this.IsReloading() || this.CoolingDown())
		{
			this.lean = Mathf.MoveTowards(this.lean, 0f, 2f * Time.deltaTime);
		}
		else if (this.InCover() && this.cover.type == CoverPoint.Type.LeanLeft)
		{
			this.lean = Mathf.MoveTowards(this.lean, -1f, 2f * Time.deltaTime);
		}
		else if (this.InCover() && this.cover.type == CoverPoint.Type.LeanRight)
		{
			this.lean = Mathf.MoveTowards(this.lean, 1f, 2f * Time.deltaTime);
		}
		else if (this.isInCqcZone && this.isTraversingCorner)
		{
			this.lean = Mathf.MoveTowards(this.lean, this.nextWaypointCornerLean, 2f * Time.deltaTime);
		}
		else
		{
			this.lean = Mathf.MoveTowards(this.lean, 0f, 2f * Time.deltaTime);
		}
		if (this.IsDeployingSmoke())
		{
			if (this.actor.HasUnholsteredWeapon())
			{
				Vector3 vector = this.actor.WeaponMuzzlePosition();
				Vector3 vector2 = this.smokeTarget - vector + this.actor.activeWeapon.GetLead(vector, this.smokeTarget, Vector3.zero);
				this.targetLookDirection = vector2;
			}
			else
			{
				this.targetLookDirection = this.smokeTarget - this.actor.Position();
			}
		}
		else if (!this.IsAlert())
		{
			if (this.actor.IsSeated())
			{
				this.targetLookDirection = this.actor.seat.transform.forward;
			}
			else if (this.hasPath)
			{
				this.targetLookDirection = this.currentWaypoint - this.lastWaypoint;
			}
		}
		else if (this.IsRepairing())
		{
			this.targetLookDirection = this.targetRepairVehicle.transform.position - (this.actor.Position() + new Vector3(0f, 0.4f, 0f));
		}
		else if (this.HasSpottedTarget())
		{
			if (this.actor.HasUnholsteredWeapon())
			{
				Vector3 b2 = new Vector3(Mathf.Sin(num2 * 4.1f), Mathf.Cos(num2 * 7.3f), Mathf.Cos(num2 * 5.7f)) * this.GetSwingMagnitude();
				Vector3 vector3 = this.GetTargetAcquiredPosition() - this.actor.WeaponMuzzlePosition() + this.WeaponLead() + b2;
				if (vector3.sqrMagnitude > 0.0001f)
				{
					this.targetLookDirection = vector3;
				}
			}
			else
			{
				this.targetLookDirection = this.target.CenterPosition() - this.actor.CenterPosition();
			}
		}
		else if (this.hasPath && !this.isSeated)
		{
			this.targetLookDirection = this.infantryPathLookAheadPoint - this.actor.CenterPosition();
		}
		Quaternion to = (this.targetLookDirection == Vector3.zero) ? Quaternion.identity : Quaternion.LookRotation(this.targetLookDirection);
		this.lookRotation = SMath.DampSlerp(this.lookRotation, to, 0.005f, Time.deltaTime);
		this.lookRotation = Quaternion.RotateTowards(this.lookRotation, to, 5f * Time.deltaTime);
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0000466F File Offset: 0x0000286F
	private void OnDetectedEnemy()
	{
		AiActorController.DelOnDetectedEnemy delOnDetectedEnemy = this.onDetectedEnemy;
		if (delOnDetectedEnemy != null)
		{
			delOnDetectedEnemy();
		}
		this.slowTargetDetection = false;
		this.StartAcquireTargetAimPenaltyAction(1f);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0004F5B4 File Offset: 0x0004D7B4
	private float GetSwingMagnitude()
	{
		float num = this.actor.parachuteDeployed ? AiActorController.PARAMETERS.AIM_SWING_PARACHUTING : 0f;
		if (this.actor.activeWeapon != null)
		{
			num += this.actor.activeWeapon.configuration.aiAimSwing;
		}
		return num;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0004F60C File Offset: 0x0004D80C
	private Vector3 GetTargetAcquiredPosition()
	{
		Vector3 targetAcquireOffset = this.GetTargetAcquireOffset();
		return this.target.CenterPosition() + targetAcquireOffset;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00004694 File Offset: 0x00002894
	private bool IsReloading()
	{
		return this.actor.HasUnholsteredWeapon() && this.actor.activeWeapon.reloading;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x000046B5 File Offset: 0x000028B5
	private bool CoolingDown()
	{
		return this.actor.HasUnholsteredWeapon() && this.actor.activeWeapon.configuration.cooldown > 0.3f && this.actor.activeWeapon.CoolingDown();
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000046F2 File Offset: 0x000028F2
	private Vector3 WeaponLead()
	{
		return this.actor.activeWeapon.GetLead(this.actor.Position(), this.target.Position(), this.target.Velocity());
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00004725 File Offset: 0x00002925
	public override float Lean()
	{
		return this.lean;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0000472D File Offset: 0x0000292D
	public override bool Fire()
	{
		return this.fire && !this.isSprinting;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool Aiming()
	{
		return false;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00004742 File Offset: 0x00002942
	public override bool Reload()
	{
		return this.actor.HasUnholsteredWeapon() && this.actor.activeWeapon.IsEmpty() && UnityEngine.Random.Range(0, 2) == 0;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool OnGround()
	{
		return true;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool ProjectToGround()
	{
		return true;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00004772 File Offset: 0x00002972
	private Vector3 GetWaypointDeltaBlockable()
	{
		if (this.blockerAhead)
		{
			return Vector3.zero;
		}
		return this.GetWaypointDelta();
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0004F634 File Offset: 0x0004D834
	private Vector3 GetWaypointDelta()
	{
		if (this.IsHalted())
		{
			return Vector3.zero;
		}
		Vector3 position;
		if (this.isDriver)
		{
			position = this.actor.seat.vehicle.transform.position;
		}
		else
		{
			position = this.actor.transform.position;
		}
		Vector3 vector;
		Vector3 result = vector = this.vectorPath[this.waypoint] - position;
		vector.y = 0f;
		float num = SMath.LineSegmentVsPointClosestT(this.lastWaypoint, this.vectorPath[this.waypoint], position);
		float num2;
		if (this.isSeated)
		{
			num2 = (this.aquatic ? 5f : 2.5f);
			num2 += this.actor.seat.vehicle.pathingRadius;
		}
		else
		{
			num2 = 0.2f;
		}
		this.waypointDistance = vector.magnitude;
		if (this.reachedWaypoint || num >= 1f - Mathf.Epsilon || this.waypointDistance < num2)
		{
			this.NextWaypoint();
		}
		return result;
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00004788 File Offset: 0x00002988
	public override void MarkReachedWaypoint()
	{
		this.reachedWaypoint = true;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0004F738 File Offset: 0x0004D938
	private void NextWaypoint()
	{
		this.reachedWaypoint = false;
		this.lastWaypoint = this.vectorPath[this.waypoint];
		if (!this.IsLastWaypoint())
		{
			this.waypoint++;
			this.NewWaypoint(this.lastWaypoint, this.vectorPath[this.waypoint]);
			return;
		}
		this.PathDone();
		this.hasPath = false;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0004F7A4 File Offset: 0x0004D9A4
	private void NewWaypoint(Vector3 origin, Vector3 target)
	{
		this.currentWaypoint = target;
		this.hasFutureWaypoint = (this.vectorPath.Count > this.waypoint + 1);
		AiActorController.LadderPathSection ladderPathSection = this.NextLadderInPath();
		if (ladderPathSection != null && ladderPathSection.enterWaypoint == this.waypoint - 1)
		{
			this.actor.GetOnLadder(ladderPathSection.ladder);
			return;
		}
		if (this.actor.parachuteDeployed)
		{
			this.hasParachuteLandPosition = true;
			this.parachuteLandPosition = target;
		}
		this.nextWaypointIsCorner = false;
		if (this.hasFutureWaypoint)
		{
			this.futureWaypoint = this.vectorPath[this.waypoint + 1];
			Vector3 vector = target - origin;
			Vector3 vector2 = this.futureWaypoint - target;
			Vector3 normalized = vector.normalized;
			Vector3 normalized2 = vector2.normalized;
			if (Vector3.Dot(normalized, normalized2) < 0.95f)
			{
				Ray ray = new Ray(target - normalized * 1.5f + Vector3.up, normalized2);
				this.nextWaypointIsCorner = Physics.Raycast(ray, 3f, 8388609);
				Vector3 vector3 = Vector3.Cross(normalized2, normalized);
				this.nextWaypointCornerLean = Mathf.Sign(vector3.y);
			}
		}
		if (this.lookAroundDisabledAction.TrueDone() && !this.HasSpottedTarget() && (this.isSquadLeader || UnityEngine.Random.Range(0f, 1f) < 0.5f))
		{
			this.LookAt(target);
		}
		if (this.targetVehicle != null)
		{
			float pathingRadius = this.targetVehicle.pathingRadius;
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0004F928 File Offset: 0x0004DB28
	private void AvoidVehicle(Vehicle vehicle, Vector3 origin, Vector3 target, float pathingRadius)
	{
		int num = this.waypoint;
		bool flag = false;
		for (int i = this.waypoint; i < this.vectorPath.Count; i++)
		{
			if (!vehicle.IsCoarseOverlapping(this.vectorPath[i], pathingRadius))
			{
				num = i;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		Vector3 a = vehicle.transform.forward * (vehicle.avoidanceSize.y + pathingRadius);
		Vector3 b = vehicle.transform.right * (vehicle.avoidanceSize.x + pathingRadius);
		float origin2 = (origin - vehicle.transform.position).ToVector2XZ().AtanAngle();
		float num2 = (this.vectorPath[num] - vehicle.transform.position).ToVector2XZ().AtanAngle();
		float num3 = SMath.V2D.RadiansFromTo(origin2, num2);
		bool shortLtNext = num3 < 3.1415927f;
		Vector3[] array = new Vector3[4];
		float[] cornerFromOriginAngles = new float[4];
		array[0] = a + b;
		array[1] = a - b;
		array[2] = -a - b;
		array[3] = -a + b;
		List<int> list = new List<int>(4);
		List<int> list2 = new List<int>(4);
		for (int j = 0; j < 4; j++)
		{
			Vector2 v = array[j].ToVector2XZ();
			float num4 = SMath.V2D.RadiansFromTo(origin2, v.AtanAngle());
			cornerFromOriginAngles[j] = num4;
			if (shortLtNext ^ num4 < num3)
			{
				list2.Add(j);
				Debug.DrawRay(vehicle.transform.position, array[j], Color.red, 5f);
			}
			else
			{
				list.Add(j);
				Debug.DrawRay(vehicle.transform.position, array[j], Color.blue, 5f);
			}
		}
		list.Sort(delegate(int x, int y)
		{
			int num7 = cornerFromOriginAngles[x].CompareTo(cornerFromOriginAngles[y]);
			if (shortLtNext)
			{
				return num7;
			}
			return -num7;
		});
		List<Vector3> list3 = new List<Vector3>(5);
		foreach (int num5 in list)
		{
			list3.Add(vehicle.transform.position + array[num5]);
		}
		List<Vector3> list4 = new List<Vector3>(list3);
		list4.Insert(0, origin);
		Vector3 vector = this.vectorPath[num] - list4[list4.Count - 1];
		list4.Add(list4[list4.Count - 1] + Vector3.ClampMagnitude(vector, 5f));
		if (!this.RayPathClear(list4, 1))
		{
			list2.Sort(delegate(int x, int y)
			{
				int num7 = cornerFromOriginAngles[x].CompareTo(cornerFromOriginAngles[y]);
				if (shortLtNext)
				{
					return -num7;
				}
				return num7;
			});
			Vector3 item = vehicle.transform.position + array[list2[list2.Count - 1]];
			list3.Add(item);
			list4.Insert(list4.Count - 1, item);
			if (!this.RayPathClear(list4, 1))
			{
				list3 = new List<Vector3>(5);
				foreach (int num6 in list2)
				{
					list3.Add(vehicle.transform.position + array[num6]);
				}
				list4 = new List<Vector3>(list3);
				list4.Insert(0, origin);
				vector = this.vectorPath[num] - list4[list4.Count - 1];
				list4.Add(list4[list4.Count - 1] + Vector3.ClampMagnitude(vector, 5f));
				if (!this.RayPathClear(list4, 1))
				{
					return;
				}
			}
		}
		this.vectorPath.RemoveRange(this.waypoint, num - this.waypoint);
		this.vectorPath.InsertRange(this.waypoint, list3);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0004FD7C File Offset: 0x0004DF7C
	private bool RayPathClear(List<Vector3> points, int mask)
	{
		for (int i = 0; i < points.Count - 1; i++)
		{
			if (Physics.Linecast(points[i], points[i + 1], mask) || !Physics.Raycast(points[i], Vector3.down, 3f, mask))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00004791 File Offset: 0x00002991
	private Vector3 GetUpcomingBetweenWaypointsDelta()
	{
		return this.vectorPath[this.waypoint + 1] - this.vectorPath[this.waypoint];
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
	private Vector3 GetNextWaypointDelta()
	{
		Vector3 b;
		if (this.isDriver)
		{
			b = this.actor.seat.vehicle.transform.position;
		}
		else
		{
			b = this.actor.Position();
		}
		return this.vectorPath[this.waypoint + 1] - b;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000047BC File Offset: 0x000029BC
	private bool IsLastWaypoint()
	{
		return this.vectorPath.Count <= this.waypoint + 1;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0004FE2C File Offset: 0x0004E02C
	private void PathDone()
	{
		this.isCompletingPlayerMoveOrder = false;
		if (this.HasCover())
		{
			this.LookDirection(this.cover.transform.forward);
			this.inCover = true;
			this.stayInCoverAction.Start();
			this.StopSprint();
		}
		if (this.isSquadLeader && !this.IsInTemporaryCover())
		{
			this.squad.OnLeaderPathDone();
		}
		this.moveTimeoutAction.Start();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000047D6 File Offset: 0x000029D6
	public bool IsEnteringVehicle()
	{
		return this.HasTargetVehicle() && !this.targetVehicle.dead && !this.isSeated;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000047F8 File Offset: 0x000029F8
	private bool IsFollowingInVehicle()
	{
		return this.squad != null && this.squad.HasPlayerLeader() && this.isSeated && this.isDriver && this.squad.shouldFollow;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0004FE9C File Offset: 0x0004E09C
	private bool IsFollowing()
	{
		return this.squad != null && (this.IsFollowingInVehicle() || ((!this.HasCover() || this.squad.order == null || this.squad.order.type != Order.OrderType.Defend) && (!this.IsStrafingTarget() && !this.IsMeleeCharging() && !this.isFleeing && this.squad.shouldFollow && !this.squad.outsideTransportVehicle && !this.isSquadLeader && !this.IsInTemporaryCover() && !this.IsEnteringVehicle()) && !this.isSeated));
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0004FF3C File Offset: 0x0004E13C
	public override Vector3 Velocity()
	{
		Vector3 result = Vector3.zero;
		if (this.hasPath && !this.actor.JustChangedProneStance())
		{
			result = this.GetWaypointDeltaBlockable().ToGround().normalized * this.movementSpeed;
		}
		return result;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0004FF84 File Offset: 0x0004E184
	public override Vector3 SwimInput()
	{
		if (this.hasPath)
		{
			return this.GetWaypointDeltaBlockable().ToGround().normalized;
		}
		return Vector3.zero;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0000482C File Offset: 0x00002A2C
	public override bool ForceStopVehicle()
	{
		return this.IsHalted();
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00004834 File Offset: 0x00002A34
	public override Vector2 BoatInput()
	{
		return this.carInput;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0004FFB4 File Offset: 0x0004E1B4
	public Vector2 GenerateBoatInput()
	{
		Vehicle vehicle = this.actor.seat.vehicle;
		if (!this.hasPath || this.IsHalted())
		{
			return new Vector2(0f, -Mathf.Clamp(vehicle.LocalVelocity().z, -1f, 1f));
		}
		vehicle.LocalVelocity();
		Vector3 vector = this.GetProjectedDrivingTarget(2f * vehicle.avoidanceSize.y + 5f, 1f, 1f, vehicle) - vehicle.transform.position;
		vector.y = 0f;
		Vector3 normalized = vector.normalized;
		Vector2 vector2 = new Vector2(Vector3.Dot(normalized, this.actor.transform.right), Vector3.Dot(normalized, this.actor.transform.forward));
		if (this.forceAntiStuckReverse)
		{
			return new Vector2(0f, -1f);
		}
		vector2.y = Mathf.Sign(vector2.y) * Mathf.Min(Mathf.Abs(vector2.y), this.vehicleSpeedLimitMultiplier) * this.modifier.vehicleTopSpeedMultiplier;
		return Vector2.ClampMagnitude(vector2, 1f);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00004834 File Offset: 0x00002A34
	public override Vector2 CarInput()
	{
		return this.carInput;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000500E8 File Offset: 0x0004E2E8
	public Vector2 GenerateCarInput()
	{
		Vehicle vehicle = this.actor.seat.vehicle;
		if (!this.hasPath || this.IsHalted())
		{
			return new Vector2(0f, -Mathf.Clamp(vehicle.LocalVelocity().z, -1f, 1f));
		}
		if (this.waitForPlayer)
		{
			return new Vector2(0f, -vehicle.LocalVelocity().z);
		}
		if (vehicle.GetType() == typeof(ArcadeCar))
		{
			if (((ArcadeCar)vehicle).tankTurning)
			{
				return this.GetTankInput();
			}
			return this.GetCarInput();
		}
		else
		{
			if (vehicle.GetType() == typeof(AnimationDrivenVehicle))
			{
				return this.GetTankInput();
			}
			if (vehicle.GetType() == typeof(Tank))
			{
				return this.GetTankInput();
			}
			return this.GetCarInput();
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000501D0 File Offset: 0x0004E3D0
	private Vector2 GetTankInput()
	{
		Vehicle vehicle = this.actor.seat.vehicle;
		ArcadeCar arcadeCar = vehicle as ArcadeCar;
		float z = vehicle.LocalVelocity().z;
		if (this.blockerAhead)
		{
			float num = Mathf.Sign(vehicle.transform.worldToLocalMatrix.MultiplyPoint(this.blockerPosition).x) * 0.3f;
			if (z > 0.1f)
			{
				return new Vector2(-num, -1f);
			}
			return new Vector2(num, 1f);
		}
		else
		{
			Vector3 vector = this.GetWaypointDeltaBlockable();
			if (vector == Vector3.zero)
			{
				return Vector2.zero;
			}
			float magnitude = vector.magnitude;
			vector = (this.GetProjectedDrivingTarget(3f, 0.6f, 0.2f, vehicle) - vehicle.transform.position).ToGround();
			Vector3 vector2 = base.transform.worldToLocalMatrix.MultiplyVector(vector);
			vector2.y = 0f;
			bool flag = Mathf.Abs(vector2.z) > Mathf.Abs(vector2.x);
			if (this.forceAntiStuckReverse && flag && magnitude > 2.5f)
			{
				return new Vector2(0f, Mathf.Sign(-vector2.z) * 0.5f);
			}
			float num2 = 15f;
			if (arcadeCar != null)
			{
				num2 = arcadeCar.topSpeed * Mathf.Min(this.modifier.vehicleTopSpeedMultiplier, this.vehicleSpeedLimitMultiplier);
			}
			if (!this.isAlert && this.forceUnalertMovementSpeed)
			{
				num2 = Mathf.Min(num2, 4f);
			}
			if (this.squad != null)
			{
				num2 = Mathf.Min(num2, this.squad.GetSpeedRestriction());
			}
			float num3 = flag ? Mathf.Sign(vector2.z) : 0f;
			num2 *= num3;
			float y = Mathf.Clamp01(num2 - z);
			return new Vector2(Mathf.Clamp(vector2.x, -1f, 1f), y);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x000503CC File Offset: 0x0004E5CC
	private Vector2 GetCarInput()
	{
		Vehicle vehicle = this.actor.seat.vehicle;
		ArcadeCar arcadeCar = vehicle as ArcadeCar;
		float z = vehicle.LocalVelocity().z;
		if (!this.blockerAhead)
		{
			Vector3 vector = this.GetWaypointDeltaBlockable();
			vector.y = 0f;
			float magnitude = vector.magnitude;
			vector = (this.GetProjectedDrivingTarget(5f, 0.8f, 0.35f, vehicle) - vehicle.transform.position).ToGround();
			float value = Vector3.Dot((this.GetFutureProjectedDrivingTarget(4f, 3f, vehicle) - vehicle.transform.position).ToGround().normalized, vector.normalized);
			float magnitude2 = (vehicle.Velocity() * 1f).magnitude;
			float num;
			if (this.forceAntiStuckReverse)
			{
				num = 7f;
			}
			else
			{
				if (arcadeCar != null)
				{
					num = arcadeCar.topSpeed * (0.4f + 0.7f * Mathf.Pow(Mathf.Clamp01(value), 3f)) * Mathf.Min(this.modifier.vehicleTopSpeedMultiplier, this.vehicleSpeedLimitMultiplier);
				}
				else
				{
					num = 15f;
				}
				if (!this.isAlert && this.forceUnalertMovementSpeed)
				{
					num = Mathf.Min(num, 4f);
				}
			}
			if (this.squad != null)
			{
				num = Mathf.Min(num, this.squad.GetSpeedRestriction());
			}
			float num2 = Mathf.Clamp01(num - z);
			if (z > 1.1f * num)
			{
				num2 = -1f;
			}
			Debug.DrawRay(vehicle.transform.position + Vector3.up, Vector3.up, Color.black);
			Debug.DrawRay(vehicle.transform.position + Vector3.up, Vector3.up * num2, Color.red);
			Vector2 vector2 = new Vector2(Vector3.Dot(vector * 5f, this.actor.transform.right), Vector3.Dot(vector, this.actor.transform.forward));
			bool flag = !this.canTurnCarTowardsWaypoint ^ this.forceAntiStuckReverse;
			Color color = Color.blue;
			vector2.y = Mathf.Clamp(Mathf.Sign(vector2.y) * num2, -1f, 0.8f);
			vector2.x = Mathf.Clamp(vector2.x / (1f + Mathf.Abs(z)), -1f, 1f);
			if (flag)
			{
				vector2.y = Mathf.Abs(vector2.y);
				color = Color.red;
			}
			if (this.forceAntiStuckReverse)
			{
				vector2.y = -0.7f;
			}
			if (z < 0f)
			{
				vector2.x *= -1f;
			}
			Vector3 rhs = vehicle.transform.forward.ToGround();
			rhs.Normalize();
			float t = Mathf.Abs(Vector3.Dot(vector.normalized, rhs));
			float num3 = Mathf.Lerp(1f, 0.5f, t);
			float num4 = Mathf.Lerp(0.5f, 1f, t);
			vector2.x *= num3;
			vector2.y *= num4;
			Debug.DrawRay(this.actor.seat.vehicle.transform.position, this.GetWaypointDelta(), color);
			return vector2;
		}
		float num5 = Mathf.Sign(vehicle.transform.worldToLocalMatrix.MultiplyPoint(this.blockerPosition).x) * 0.3f;
		if (z > 0.1f)
		{
			return new Vector2(-num5, -1f);
		}
		return new Vector2(num5, 1f);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00050780 File Offset: 0x0004E980
	private Vector3 GetProjectedDrivingTarget(float minDistance, float speedGain, float antiSwingGain, Vehicle vehicle)
	{
		if (this.vectorPath[this.waypoint] == this.lastWaypoint)
		{
			this.NextWaypoint();
			return this.lastWaypoint;
		}
		Vector3 lhs = vehicle.Velocity().ToGround();
		Vector3 vector = this.vectorPath[this.waypoint] - this.lastWaypoint;
		float d = Mathf.Max(minDistance, speedGain * lhs.magnitude);
		Vector3 normalized = vector.ToGround().normalized;
		Vector3 vector2 = new Vector3(normalized.z, 0f, -normalized.x);
		float d2 = Vector3.Dot(lhs, vector2);
		float num = SMath.LineVsPointClosestT(this.lastWaypoint, vector, vehicle.transform.position);
		Vector3 vector3 = this.lastWaypoint + num * vector + vector.normalized * d;
		Vector3 vector4 = vector3 + vector2 * d2 * -antiSwingGain;
		Debug.DrawLine(vehicle.transform.position, vector3, Color.red);
		Debug.DrawRay(vector4, Vector3.up * 5f, new Color(255f, 0f, 255f));
		if (this.IsLastWaypoint())
		{
			if (num > 1f || Vector3.Distance(vehicle.transform.position, this.vectorPath[this.waypoint]) < 0.2f)
			{
				this.NextWaypoint();
				return vector4;
			}
		}
		else if (num > 1f || Vector3.Distance(vector3, this.vectorPath[this.waypoint]) < 0.2f)
		{
			this.NextWaypoint();
		}
		return vector4;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00050930 File Offset: 0x0004EB30
	private Vector3 GetFutureProjectedDrivingTarget(float minDistance, float speedGain, Vehicle vehicle)
	{
		Vector3 vector = this.vectorPath[this.waypoint];
		if (this.waypoint + 1 < this.vectorPath.Count)
		{
			float magnitude = (vector - vehicle.transform.position).magnitude;
			float num = Mathf.Max(minDistance, speedGain * vehicle.Velocity().magnitude);
			Vector3 vector2 = this.vectorPath[this.waypoint + 1] - this.vectorPath[this.waypoint];
			Vector3 vector3 = vector + Mathf.Max(0f, num - magnitude) * vector2.normalized;
			Debug.DrawLine(vehicle.transform.position, vector3, Color.red);
			return vector3;
		}
		return vector;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0000483C File Offset: 0x00002A3C
	public override Vector4 HelicopterInput()
	{
		return this.aircraftInput;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00050A00 File Offset: 0x0004EC00
	public Vector4 GenerateHelicopterInput()
	{
		if (!this.helicopterTakeoffAction.TrueDone())
		{
			return new Vector4(0f, -1f + this.helicopterTakeoffAction.Ratio() * 1.5f, 0f, 0f);
		}
		Vehicle vehicle = this.actor.seat.vehicle;
		Transform transform = vehicle.transform;
		Vector3 position = transform.position;
		float y = transform.eulerAngles.y;
		Vector3 vector = vehicle.Velocity();
		float num = position.y - WaterLevel.GetHeight();
		Vector3 vector2 = position + vector * 3f;
		float num2 = vector2.y - WaterLevel.GetHeight();
		bool flag = false;
		RaycastHit raycastHit;
		if (Physics.SphereCast(new Ray(vector2 + Vector3.up * 10f, Vector3.down), 1f, out raycastHit, 999f, 4194305))
		{
			num = position.y - raycastHit.point.y;
			num2 = vector2.y - raycastHit.point.y;
			flag = true;
		}
		if (Physics.SphereCast(new Ray(position + Vector3.up * 10f, Vector3.down), 1f, out raycastHit, 999f, 4194305))
		{
			if (flag)
			{
				num = Mathf.Min(position.y - raycastHit.point.y, num);
			}
			else
			{
				num = position.y - raycastHit.point.y;
			}
			flag = true;
		}
		Vector3 vector3 = Vector3.zero;
		Vector3 forward = transform.forward;
		forward.y = 0f;
		forward.Normalize();
		Vector3 rhs = new Vector3(forward.z, 0f, -forward.x);
		if (num2 < 15f)
		{
			this.attackRunAction.Stop();
		}
		bool flag2 = this.actor.seat.HasAnyMountedWeapons() && this.HasSpottedTarget() && !this.attackRunAction.TrueDone();
		bool flag3 = false;
		Vector3 vector4 = position + forward;
		Vector3 vector5;
		if (this.ShouldLandHelicopter(out vector5))
		{
			flag2 = false;
			vector4 = vector5;
			flag3 = ((vector5 - position).ToGround().magnitude < 250f);
		}
		else if (flag2)
		{
			Vector3 a = Vector3.ProjectOnPlane(this.actor.activeWeapon.CurrentMuzzle().forward, transform.forward);
			float d = Vector3.Distance(this.actor.activeWeapon.CurrentMuzzle().position, this.target.CenterPosition());
			vector4 = this.target.CenterPosition() + this.WeaponLead() - a * d;
		}
		else if (this.hasFlightTargetPosition)
		{
			vector4 = this.flightTargetPosition;
		}
		if (!flag && Physics.Linecast(position, vector4, out raycastHit, 4194305))
		{
			Debug.DrawLine(position, raycastHit.point, Color.magenta);
			num = position.y - raycastHit.point.y;
		}
		float num3 = this.Heading(position, vector4);
		vector3 = vector4 - position;
		float y2 = vector3.y;
		vector3.y = 0f;
		float magnitude = vector3.magnitude;
		float deltaHeading = Mathf.DeltaAngle(y, num3);
		float num4 = this.targetFlightHeight;
		float targetPitch;
		float targetRoll;
		float w;
		float x;
		float z;
		float helicopterThrustInput;
		if (!flag3)
		{
			float num5 = num4 - num;
			float vehicleTopSpeedMultiplier = this.modifier.vehicleTopSpeedMultiplier;
			float num6 = flag2 ? 35f : (vehicleTopSpeedMultiplier * 20f);
			float num7 = flag2 ? 35f : (vehicleTopSpeedMultiplier * 25f);
			targetPitch = num6 * Mathf.Clamp(Vector3.Dot(vector3 * 0.02f, forward), -1f, 1f);
			targetRoll = -num7 * Mathf.Clamp(Vector3.Dot(vector3 * 0.02f, rhs), -1f, 1f);
			if (num5 > 5f)
			{
				targetPitch = 0f;
				targetRoll = 0f;
			}
			float inputMultiplier = 1f;
			if (flag2)
			{
				targetPitch = -Mathf.Atan2(y2, magnitude) * 57.29578f;
				inputMultiplier = 2.5f;
			}
			this.GetHelicopterInput(vehicle, inputMultiplier, targetPitch, deltaHeading, targetRoll, out w, out x, out z);
			helicopterThrustInput = this.GetHelicopterThrustInput(num5, magnitude);
			return new Vector4(x, helicopterThrustInput, z, w);
		}
		Vector3 vector6 = vector4 - position - vector * 2f;
		float magnitude2 = vector6.ToGround().magnitude;
		if (magnitude2 > 15f)
		{
			this.landingPlannedHeading = num3;
		}
		Debug.DrawLine(position, vector5, Color.red);
		float num8 = position.y - vector5.y;
		num = Mathf.Max(num, num8);
		if (num < 4f)
		{
			this.signalPassengersEnterAircraft = true;
		}
		float num9 = Vector3.Dot(vector6, rhs);
		float num10 = Vector3.Dot(vector6, forward);
		float num11 = Vector3.Dot(vector, rhs);
		float num12 = Vector3.Dot(vector, forward);
		targetPitch = 20f * Mathf.Clamp(num10 * 0.1f - num12 * 0.05f, -1f, 1f);
		targetRoll = -15f * Mathf.Clamp(num9 * 0.1f - num11 * 0.05f, -1f, 1f);
		float deltaHeading2 = Mathf.DeltaAngle(y, this.landingPlannedHeading);
		this.GetHelicopterInput(vehicle, 2f, targetPitch, deltaHeading2, targetRoll, out w, out x, out z);
		float num13 = Mathf.InverseLerp(15f, 5f, magnitude2);
		float y3 = vector.y;
		bool flag4 = num13 > 0.99f && num8 + y3 < 15f;
		this.hasLandedUNSAFE = (flag4 && Mathf.Abs(vector.y) < 0.2f);
		if (flag4)
		{
			return new Vector4(x, Mathf.Clamp(-1.5f - y3 * 0.5f, -1f, 1f), z, w);
		}
		num4 = Mathf.Lerp(40f, 0f, num13);
		helicopterThrustInput = this.GetHelicopterThrustInput(num4 - num, magnitude2);
		return new Vector4(x, helicopterThrustInput, z, w);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00051010 File Offset: 0x0004F210
	private bool ShouldLandHelicopter(out Vector3 landingPosition)
	{
		landingPosition = Vector3.zero;
		if (this.squad == null)
		{
			return false;
		}
		if (this.squad.pickingUpPassengers)
		{
			landingPosition = this.squad.pickUpPosition;
			return true;
		}
		if (this.squad.scriptedLanding)
		{
			landingPosition = this.squad.scriptedLandingPosition;
			return true;
		}
		return false;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00051074 File Offset: 0x0004F274
	private void GetHelicopterInput(Vehicle vehicle, float inputMultiplier, float targetPitch, float deltaHeading, float targetRoll, out float pitch, out float yaw, out float roll)
	{
		Vector3 localEulerAngles = vehicle.transform.localEulerAngles;
		Vector3 vector = vehicle.LocalAngularVelocity();
		yaw = 0.01f * inputMultiplier * deltaHeading - vector.y;
		pitch = 0.1f * inputMultiplier * Mathf.DeltaAngle(localEulerAngles.x, targetPitch) - 2f * vector.x;
		roll = 0.1f * inputMultiplier * Mathf.DeltaAngle(targetRoll, localEulerAngles.z) + 4f * vector.z;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000510F4 File Offset: 0x0004F2F4
	private float GetHelicopterThrustInput(float deltaTargetFlightHeight, float deltaTargetDistance)
	{
		deltaTargetFlightHeight -= this.actor.seat.vehicle.Velocity().y;
		if (deltaTargetDistance > 50f)
		{
			return Mathf.Clamp(10f + deltaTargetFlightHeight * 0.5f, 0f, 1f);
		}
		return Mathf.Clamp(deltaTargetFlightHeight * 0.5f, -0.2f, 1f);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0000483C File Offset: 0x00002A3C
	public override Vector4 AirplaneInput()
	{
		return this.aircraftInput;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0005115C File Offset: 0x0004F35C
	public Vector4 GenerateAirplaneInput()
	{
		if (this.squad.ShouldWaitForPassengers(this.actor.seat.vehicle))
		{
			this.planeTakeoffAction.Start();
			return new Vector4(0f, -1f, 0f, 0f);
		}
		Vehicle vehicle = this.actor.seat.vehicle;
		Transform transform = this.actor.seat.vehicle.transform;
		Vector3 position = transform.position;
		Vector3 localEulerAngles = transform.localEulerAngles;
		float num = position.y - WaterLevel.GetHeight();
		Vector3 b = vehicle.Velocity() * 3f;
		Vector3 a = position + b;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(position, transform.forward), out raycastHit, b.magnitude, 4194305))
		{
			num = position.y - raycastHit.point.y;
		}
		else if (Physics.Raycast(new Ray(a + Vector3.up * 10f, Vector3.down), out raycastHit, 999f, 4194305))
		{
			num = position.y - raycastHit.point.y;
		}
		float y = vehicle.Velocity().y;
		bool flag = num + y * 2f < 15f;
		if (!this.attackRunAction.TrueDone() && flag)
		{
			this.attackRunAction.Stop();
		}
		bool flag2 = this.HasSpottedTarget() && this.target.IsSeated() && this.target.GetTargetType() == Actor.TargetType.AirFastMover;
		Vector3 vector = position + base.transform.forward;
		if (this.HasSpottedTarget())
		{
			if (this.actor.activeWeapon != null)
			{
				if (this.actor.activeWeapon.isBomb)
				{
					vector = this.target.CenterPosition() + this.target.Velocity() * this.actor.activeWeapon.DropBombTravelTime(this.target.CenterPosition());
				}
				else
				{
					Vector3 a2 = Vector3.ProjectOnPlane(this.actor.activeWeapon.CurrentMuzzle().forward, vehicle.transform.forward);
					float d = Vector3.Distance(this.actor.activeWeapon.CurrentMuzzle().position, this.target.CenterPosition());
					vector = this.target.CenterPosition() + this.WeaponLead() - a2 * d;
				}
			}
			else
			{
				vector = this.target.CenterPosition();
			}
		}
		else if (this.hasFlightTargetPosition)
		{
			vector = this.flightTargetPosition;
			Debug.DrawLine(position, vector, Color.magenta);
		}
		if (float.IsNaN(vector.x))
		{
			vector = position + base.transform.forward;
		}
		Vector3 vector2 = vector - position;
		vector2.ToGround();
		Vector3 vector3 = transform.worldToLocalMatrix.MultiplyVector(vector2);
		if (this.planeCollisionCourseAction.TrueDone() && this.HasSpottedTarget())
		{
			Vector3 position2 = transform.position;
			Vector3 b2 = transform.position + vehicle.Velocity() * this.planeCollisionCourseExtrapolationTime;
			Vector3 vector4 = this.target.Position() + this.target.Velocity() * this.planeCollisionCourseExtrapolationTime * 0.8f;
			if (Vector3.Distance(SMath.LineSegmentVsPointClosest(position2, b2, vector4), vector4) < 30f)
			{
				this.planeCollisionCourseAction.Start();
			}
		}
		if (!this.planeCollisionCourseAction.TrueDone())
		{
			float num2 = 1f - Mathf.Abs(Mathf.Cos(localEulerAngles.z * 0.017453292f));
			float y2 = 0f;
			float value = 0f;
			if (num2 < 0.1f)
			{
				value = Mathf.Sign(-vector3.x);
			}
			float num3 = Mathf.Sign(vector3.x) * 40f;
			float value2 = Mathf.Clamp(Mathf.Sign(vector3.y), -1f, -0.5f);
			float value3 = -20f * Mathf.DeltaAngle(localEulerAngles.z, num3);
			return new Vector4(Mathf.Clamp(value, -1f, 1f), y2, Mathf.Clamp(value3, -1f, 1f), Mathf.Clamp(value2, -1f, 1f));
		}
		if ((flag2 && Vector3.Dot(this.target.Velocity().normalized, vehicle.Velocity().normalized) > 0.2f && Vector3.Dot(vector2.normalized, transform.forward) < -0.3f && this.planeDefensiveManeuverAction.TrueDone()) || (this.HasSpottedTarget() && this.attackRunAction.TrueDone() && this.planeForceManeuverAction.TrueDone()))
		{
			this.RandomizePlaneDefensiveManeuver();
		}
		float value4 = 0f;
		float value5 = 0f;
		float value6 = 0f;
		float y3 = 0f;
		if (!this.planeTakeoffAction.TrueDone())
		{
			value4 = 0f;
			value5 = 0f;
			value6 = 0f;
			y3 = 1f;
		}
		else if (flag2 && !this.planeDefensiveManeuverAction.TrueDone())
		{
			this.GetPlaneInputManeuver(vehicle.rigidbody, this.target.seat.vehicle.transform, num, out value4, out value5, out value6, out y3);
		}
		else
		{
			this.GetPlaneInputOffensive(transform, vector, num, out value4, out value5, out value6, out y3);
		}
		return new Vector4(Mathf.Clamp(value5, -1f, 1f), y3, Mathf.Clamp(value6, -1f, 1f), Mathf.Clamp(value4, -1f, 1f));
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0005171C File Offset: 0x0004F91C
	private void GetPlaneInputOffensive(Transform vehicleTransform, Vector3 targetPosition, float currentHeight, out float pitch, out float yaw, out float roll, out float throttle)
	{
		pitch = 0f;
		yaw = 0f;
		roll = 0f;
		throttle = 0f;
		bool flag = this.actor.seat.HasAnyMountedWeapons() && this.HasSpottedTarget() && !this.attackRunAction.TrueDone();
		Vector3 vector = targetPosition - vehicleTransform.position;
		Vector3 vector2 = vector.ToGround();
		float num = this.Heading(vehicleTransform.position, targetPosition);
		bool flag2 = Vector3.Dot(vector2.normalized, vehicleTransform.forward.ToGround().normalized) < 0.5f && vector2.magnitude < this.planeUTurnDistance;
		if (flag2)
		{
			num += 180f;
			if (this.airplaneUTurnAutoReloadAction.TrueDone() && this.actor.seat.activeWeapon != null && !this.actor.seat.activeWeapon.AmmoFull())
			{
				this.actor.seat.activeWeapon.InstantlyReload();
			}
		}
		else
		{
			this.airplaneUTurnAutoReloadAction.Start();
		}
		float num2 = this.targetFlightHeight;
		if (this.HasSpottedTarget())
		{
			if (flag2)
			{
				num2 = 0.8f * this.targetFlightHeight;
			}
			else
			{
				num2 = 0.6f * this.targetFlightHeight;
			}
		}
		float inputMultiplier = (flag && !this.actor.activeWeapon.isBomb) ? 4f : this.planeSteeringMultiplier;
		float num3 = num2 - currentHeight;
		float targetPitch;
		if (flag && !this.actor.activeWeapon.isBomb)
		{
			targetPitch = -Mathf.Atan2(vector.y, vector2.magnitude) * 57.29578f;
		}
		else
		{
			float min = -30f;
			if (!this.planeTakeoffAction.TrueDone())
			{
				min = Mathf.Lerp(0f, -30f, Mathf.Pow(this.planeTakeoffAction.Ratio(), 2f));
			}
			if (num3 > 0f)
			{
				targetPitch = Mathf.Clamp(-num3, min, 15f);
			}
			else
			{
				targetPitch = Mathf.Clamp(-0.4f * num3, min, 15f);
			}
		}
		this.GetPlaneInputFromTarget(vehicleTransform, targetPitch, num, inputMultiplier, out pitch, out yaw, out roll);
		throttle = (flag ? 0f : 1f);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00051964 File Offset: 0x0004FB64
	private void GetPlaneInputFromTarget(Transform vehicleTransform, float targetPitch, float targetHeading, float inputMultiplier, out float pitch, out float yaw, out float roll)
	{
		pitch = 0f;
		yaw = 0f;
		roll = 0f;
		Vector3 localEulerAngles = vehicleTransform.localEulerAngles;
		float num = Mathf.DeltaAngle(vehicleTransform.eulerAngles.y, targetHeading);
		float num2 = Mathf.Clamp(-num, -40f, 40f);
		pitch = -6f * inputMultiplier * Mathf.DeltaAngle(targetPitch, localEulerAngles.x);
		yaw = 3f * inputMultiplier * num;
		roll = -3f * inputMultiplier * Mathf.DeltaAngle(localEulerAngles.z, num2);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x000519F4 File Offset: 0x0004FBF4
	private void GetPlaneInputManeuver(Rigidbody vehicleRigidbody, Transform chaserTransform, float currentHeight, out float pitch, out float yaw, out float roll, out float throttle)
	{
		Transform transform = vehicleRigidbody.transform;
		pitch = 0f;
		yaw = 0f;
		roll = 0f;
		throttle = 1f;
		if (currentHeight + vehicleRigidbody.velocity.y * 3f < 15f)
		{
			this.planeDefensiveManeuver = AiActorController.FastMoverDefensiveManeuver.SpiralUp;
		}
		switch (this.planeDefensiveManeuver)
		{
		case AiActorController.FastMoverDefensiveManeuver.Break:
		{
			float num = this.maneuverSeed % 1f;
			this.GetPlaneInputFromManeuver(transform, 0f, Mathf.Sign(this.maneuverSeed % 1f - 0.5f) * 90f, 30f, out pitch, out yaw, out roll);
			Vector3 eulerAngles = transform.eulerAngles;
			if (Mathf.Abs(Mathf.DeltaAngle(eulerAngles.x, 0f)) < 10f && Mathf.Cos(eulerAngles.z * 0.017453292f) > 0.5f)
			{
				pitch = 1f;
				return;
			}
			break;
		}
		case AiActorController.FastMoverDefensiveManeuver.GunsDefense:
			pitch = Mathf.Sign(Mathf.PerlinNoise(this.maneuverSeed, this.maneuverSeed + Time.time) - 0.5f);
			yaw = Mathf.Sign(Mathf.PerlinNoise(this.maneuverSeed + Time.time, this.maneuverSeed + Time.time) - 0.5f);
			roll = Mathf.Sign(Mathf.PerlinNoise(this.maneuverSeed + Time.time, this.maneuverSeed) - 0.5f);
			return;
		case AiActorController.FastMoverDefensiveManeuver.SpiralUp:
			this.GetPlaneInputFromManeuver(transform, -40f, Mathf.Sign(this.maneuverSeed % 1f - 0.5f) * 60f, 30f, out pitch, out yaw, out roll);
			break;
		case AiActorController.FastMoverDefensiveManeuver.SpiralDown:
			this.GetPlaneInputFromManeuver(transform, 30f, Mathf.Sign(this.maneuverSeed % 1f - 0.5f) * 90f, 30f, out pitch, out yaw, out roll);
			return;
		case AiActorController.FastMoverDefensiveManeuver.Immelmann:
			if (this.planeDefensiveManeuverAction.Ratio() < 0.6f)
			{
				this.GetPlaneInputFromManeuver(transform, -90f, this.maneuverSeed, 30f, out pitch, out yaw, out roll);
				return;
			}
			pitch = Mathf.Sign(this.maneuverSeed % 1f - 0.5f);
			return;
		default:
			return;
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00051C20 File Offset: 0x0004FE20
	private void GetPlaneInputFromManeuver(Transform vehicleTransform, float targetPitch, float targetRoll, float inputMultiplier, out float pitch, out float yaw, out float roll)
	{
		pitch = 0f;
		yaw = 0f;
		roll = 0f;
		Vector3 localEulerAngles = vehicleTransform.localEulerAngles;
		pitch = -10f * inputMultiplier * Mathf.DeltaAngle(targetPitch, localEulerAngles.x);
		yaw = 0f;
		roll = -3f * inputMultiplier * Mathf.DeltaAngle(localEulerAngles.z, targetRoll);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00051C84 File Offset: 0x0004FE84
	private void RandomizePlaneDefensiveManeuver()
	{
		AiActorController.FastMoverDefensiveManeuver[] array = (AiActorController.FastMoverDefensiveManeuver[])Enum.GetValues(typeof(AiActorController.FastMoverDefensiveManeuver));
		this.planeDefensiveManeuver = array[UnityEngine.Random.Range(0, array.Length)];
		this.planeDefensiveManeuverAction.Start();
		this.maneuverSeed = (float)(base.GetInstanceID() % 10000) + Time.time * 3333f;
		this.planeForceManeuverAction.Start();
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00051CEC File Offset: 0x0004FEEC
	private float Heading(Vector3 root, Vector3 target)
	{
		Vector3 vector = target - root;
		return -Mathf.Atan2(vector.z, vector.x) * 57.29578f + 90f;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00004844 File Offset: 0x00002A44
	public override Vector3 FacingDirection()
	{
		return this.facingDirectionVector;
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool UseMuzzleDirection()
	{
		return false;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00051D20 File Offset: 0x0004FF20
	public override void OnVehicleWasDamaged(Actor source, float damage)
	{
		if (source != null && source.team != this.actor.team && damage > 400f && (!(this.target != null) || (this.targetEffectiveness != AiActorController.EvaluatedWeaponEffectiveness.Preferred && !this.target.IsHighPriorityTarget())) && this.HasEffectiveWeaponAgainst(source))
		{
			base.StartCoroutine(this.ReactToVehicleDamageCoroutine(source));
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0000484C File Offset: 0x00002A4C
	private IEnumerator ReactToVehicleDamageCoroutine(Actor source)
	{
		yield return new WaitForSeconds(AiActorController.PARAMETERS.REACTION_TIME);
		if (this.target != null && !this.keepTargetAction.TrueDone())
		{
			yield break;
		}
		this.DropTarget();
		this.LookAt(source.CenterPosition() + UnityEngine.Random.insideUnitSphere * 5f);
		this.DisableAutoLookAround(2f);
		this.onlyPreferredOrHighPriorityTargetAction.Start();
		yield break;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00051D94 File Offset: 0x0004FF94
	public override void ReceivedDamage(bool friendlyFire, float damage, float balanceDamage, Vector3 point, Vector3 direction, Vector3 force)
	{
		if (!this.HasSpottedTarget())
		{
			this.LookAt(point - direction * 10f);
			this.DisableAutoLookAround(1f);
		}
		else if (damage > 20f || balanceDamage > 20f)
		{
			this.StartAcquireTargetAimPenaltyAction(0.4f);
		}
		if (!friendlyFire)
		{
			this.SetAlert();
		}
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void DisableInput()
	{
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void EnableInput()
	{
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00004862 File Offset: 0x00002A62
	public override bool IsReadyToPickUpPassengers()
	{
		return this.signalPassengersEnterAircraft || (this.isSeated && !this.actor.seat.vehicle.IsAircraft());
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00051DF4 File Offset: 0x0004FFF4
	public override void StartSeated(Seat seat)
	{
		this.aircraftInput = Vector4.zero;
		this.carInput = Vector2.zero;
		this.StartAcquireTargetAimPenaltyAction(1f);
		this.isSeated = true;
		this.isDriver = seat.IsDriverSeat();
		this.signalPassengersEnterAircraft = false;
		if (this.isDriver && this.squad != null && !seat.vehicle.isTurret && !this.squad.HasPlayerLeader())
		{
			this.squad.MakeLeader(this);
		}
		this.flying = (seat.vehicle.GetType() == typeof(Helicopter) || seat.vehicle.GetType() == typeof(Airplane));
		if (seat.vehicle.pathingRadius > 0f)
		{
			this.radiusModifier.radius = seat.vehicle.pathingRadius;
			this.radiusModifier.enabled = true;
		}
		if (seat.vehicle.GetType() == typeof(Helicopter))
		{
			Helicopter helicopter = seat.vehicle as Helicopter;
			this.targetFlightHeight = helicopter.flightAltitudeMultiplier * UnityEngine.Random.Range(40f, 80f);
		}
		else if (seat.vehicle.GetType() == typeof(Airplane))
		{
			Airplane airplane = seat.vehicle as Airplane;
			this.planeTakeoffAction.Start();
			this.targetFlightHeight = airplane.flightAltitudeMultiplier * UnityEngine.Random.Range(130f, 300f);
			this.planeSteeringMultiplier = UnityEngine.Random.Range(1f, 2f);
			this.planeUTurnDistance = UnityEngine.Random.Range(350f, 250f);
			this.planeCollisionCourseExtrapolationTime = UnityEngine.Random.Range(2f, 3f);
			this.planeCollisionCourseAction = new TimedAction(this.planeCollisionCourseExtrapolationTime, false);
		}
		if (seat.vehicle.GetType() == typeof(ArcadeCar))
		{
			if (seat.vehicle.IsAmphibious())
			{
				this.seeker.traversableTags = -1;
			}
			else
			{
				this.seeker.traversableTags = -9;
			}
		}
		else if (seat.vehicle.GetType() == typeof(Boat))
		{
			Boat boat = (Boat)seat.vehicle;
			this.aquatic = true;
			if (seat.vehicle.aiType != Vehicle.AiType.Roam)
			{
				this.seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.Original;
			}
			if (boat.requireDeepWater)
			{
				this.seeker.traversableTags = -5;
			}
		}
		if (seat.HasAnyMountedWeapons())
		{
			Vector3 forward = seat.weapons[0].CurrentMuzzle().forward;
			this.facingDirectionVector = forward;
			this.lookRotation = Quaternion.LookRotation(this.facingDirectionVector);
			this.targetLookDirection = this.facingDirectionVector;
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x000520B8 File Offset: 0x000502B8
	private void BaseEndSeated()
	{
		this.StartAcquireTargetAimPenaltyAction(1f);
		this.isSeated = false;
		this.isDriver = false;
		this.flying = false;
		this.aquatic = false;
		this.radiusModifier.enabled = false;
		this.haltAction.Stop();
		this.seeker.tagPenalties[0] = 0;
		this.seeker.tagPenalties[1] = 0;
		this.seeker.tagPenalties[2] = 0;
		this.seeker.tagPenalties[3] = 0;
		this.seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;
		this.seeker.traversableTags = -1;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00004890 File Offset: 0x00002A90
	public override void EndSeatedSwap(Seat leftSeat)
	{
		this.BaseEndSeated();
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00004898 File Offset: 0x00002A98
	public override void EndSeated(Seat leftSeat, Vector3 exitPosition, Quaternion flatFacing, bool forcedByFallingOver)
	{
		if (!forcedByFallingOver || leftSeat.enclosed)
		{
			base.transform.position = exitPosition;
		}
		this.BaseEndSeated();
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void StartRagdoll()
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void GettingUp()
	{
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00052158 File Offset: 0x00050358
	public override void EndRagdoll()
	{
		if (this.HasSpottedTarget())
		{
			this.StartAcquireTargetAimPenaltyAction(1f);
		}
		if (this.swimmingToShore)
		{
			this.ReachedShore();
		}
		if (this.inCover)
		{
			this.Goto(this.cover.transform.position, false);
			return;
		}
		if (this.hasPath)
		{
			this.RecalculatePath();
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000521B4 File Offset: 0x000503B4
	public override void Die(Actor killer)
	{
		this.LeaveCover();
		this.LeaveVehicle(false);
		this.CancelAlertSquad();
		this.CancelPath(false);
		if (this.squad != null)
		{
			this.squad.DropMember(this);
			this.squad = null;
		}
		base.StopAllCoroutines();
		this.aiRoutines = null;
		base.CancelInvoke();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000048B8 File Offset: 0x00002AB8
	public bool HasTarget()
	{
		return this.target != null && !this.target.dead;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000048D8 File Offset: 0x00002AD8
	public override bool HasSpottedTarget()
	{
		return !this.slowTargetDetection && this.HasTarget();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000048EA File Offset: 0x00002AEA
	public override Actor GetTarget()
	{
		return this.target;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0005220C File Offset: 0x0005040C
	public bool CanSeePointFOV(Vector3 position)
	{
		return Vector3.Dot((position - this.actor.CenterPosition()).normalized, this.FacingDirection()) > 0.65f;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00052244 File Offset: 0x00050444
	public bool CanSeeActorFOV(Actor target)
	{
		return this.modifier.ignoreFovCheck || target.IsHighlighted() || Vector3.Dot((target.Position() - this.actor.Position()).normalized, this.FacingDirection()) > 0.65f;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00052298 File Offset: 0x00050498
	public bool CanSeeActorFog(Actor target, float modifiedDistance)
	{
		if (this.HasSpottedTarget() && target == this.target)
		{
			return true;
		}
		if (!target.IsHighlighted() && modifiedDistance > this.modifier.maxDetectionDistance)
		{
			return false;
		}
		float num = 1f;
		if (RenderSettings.fog)
		{
			float f;
			if (target.GetTargetType() == Actor.TargetType.Air || target.GetTargetType() == Actor.TargetType.AirFastMover)
			{
				f = Mathf.Exp(-Mathf.Pow(modifiedDistance * RenderSettings.fogDensity / 2f, 2f));
			}
			else
			{
				f = Mathf.Exp(-Mathf.Pow(modifiedDistance * RenderSettings.fogDensity, 2f));
			}
			num = Mathf.Pow(f, 2f);
			if (target.IsHighlighted())
			{
				num *= 2f;
			}
			num *= AiActorController.PARAMETERS.VISIBILITY_MULTIPLIER;
		}
		if (target.IsSeated())
		{
			num *= target.seat.vehicle.spotChanceMultiplier;
		}
		if (this.slowTargetDetection)
		{
			return num > Mathf.Min(0.95f, modifiedDistance * 0.005f);
		}
		return UnityEngine.Random.Range(0f, 1f) < num;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000048F2 File Offset: 0x00002AF2
	private bool CanSeeActor(Actor target)
	{
		return ActorManager.ActorsCanSeeEachOther(this.actor, target);
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000523A4 File Offset: 0x000505A4
	public override void SpawnAt(Vector3 position, Quaternion rotation)
	{
		this.isCompletingPlayerMoveOrder = false;
		this.freezeFacingDirection = false;
		this.target = null;
		this.DropTargetVehicle();
		this.hasFlightTargetPosition = false;
		this.takingFireAction.Stop();
		this.radiusModifier.enabled = false;
		this.recentAntiStuckEvents = 0;
		this.moveTimeoutAction.Start();
		this.swimmingToShore = false;
		this.cachedTargetNode = null;
		this.getInTemporaryCoverAction.Stop();
		this.vehicleSpeedLimitMultiplier = 1f;
		this.signalPassengersEnterAircraft = false;
		this.isSeated = false;
		this.isDriver = false;
		this.isSquadLeader = false;
		this.isInCqcZone = false;
		this.movementSpeed = 0f;
		this.isFleeing = false;
		this.hasParachuteLandPosition = false;
		this.deploySmokeAction.Stop();
		this.sprintCooldownAction.Stop();
		this.sprintAction.Stop();
		this.infantryFocus.range = 20f;
		this.vehicleFocus.range = 20f;
		this.hugeVehicleFocus.range = 20f;
		this.lookRotation = rotation;
		this.calmDownStartTimestamp = Time.time;
		this.StartAiCoroutines();
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000524C8 File Offset: 0x000506C8
	public override void ApplyRecoil(Vector3 impulse)
	{
		this.lookRotation = Quaternion.LookRotation(this.FacingDirection() * 20f + AiActorController.PARAMETERS.RECOIL_MULTIPLIER * (impulse.z * Vector3.down + Vector3.right * impulse.x), Vector3.up);
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00004900 File Offset: 0x00002B00
	public override bool FindCover()
	{
		return this.FindCoverAtPoint(this.actor.Position());
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00052530 File Offset: 0x00050730
	public override bool FindCoverAtPoint(Vector3 point)
	{
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		this.inCover = false;
		this.cover = CoverManager.instance.ClosestVacant(point);
		if (this.HasCover())
		{
			return this.EnterCover(this.cover);
		}
		this.CancelPath(false);
		this.Goto(point, false);
		return false;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00052588 File Offset: 0x00050788
	public override bool FindCoverAwayFrom(Vector3 point)
	{
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		this.inCover = false;
		this.cover = CoverManager.instance.ClosestVacantCoveringAwayFrom(base.transform.position);
		return this.HasCover() && this.EnterCover(this.cover);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x000525DC File Offset: 0x000507DC
	public override bool FindCoverTowards(Vector3 direction)
	{
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		this.inCover = false;
		this.cover = CoverManager.instance.GetCoverPositionAgainstDirection(base.transform.position, direction);
		return this.HasCover() && this.EnterCover(this.cover);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00052630 File Offset: 0x00050830
	public override bool EnterCover(CoverPoint coverPoint)
	{
		if (coverPoint.taken)
		{
			return false;
		}
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		this.cover = coverPoint;
		this.cover.taken = true;
		this.Goto(this.cover.transform.position, false);
		if (!this.HasSpottedTarget())
		{
			this.StartSprint();
		}
		return true;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00004913 File Offset: 0x00002B13
	public override void LeaveCover()
	{
		this.inCover = false;
		if (this.HasCover())
		{
			this.cover.taken = false;
			this.cover = null;
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00004937 File Offset: 0x00002B37
	public bool HasCover()
	{
		return this.cover != null;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00004945 File Offset: 0x00002B45
	public bool IsMovingToCover()
	{
		return this.HasCover() && !this.InCover();
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0000495A File Offset: 0x00002B5A
	public bool InSquad()
	{
		return this.squad != null;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00052690 File Offset: 0x00050890
	public override void OnAssignedToSquad(Squad squad)
	{
		this.squad = squad;
		this.isSquadLeader = (this.squad.members[0] == this);
		if (this.isSquadLeader)
		{
			this.actor.EmoteRegroup();
		}
		else
		{
			if (this.lookAroundDisabledAction.TrueDone())
			{
				this.LookAt(this.squad.Leader().actor.CenterPosition());
			}
			this.actor.EmoteHail();
		}
		if (this.squad.HasPlayerLeader())
		{
			this.actor.OnJoinPlayerSquad();
		}
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00004965 File Offset: 0x00002B65
	public override void OnDroppedFromSquad()
	{
		this.isSquadLeader = false;
		this.actor.OnDroppedFromSquad();
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00052724 File Offset: 0x00050924
	public override void ChangeToSquad(Squad squad)
	{
		if (this.InSquad())
		{
			this.squad.DropMember(this);
		}
		if (this.HasCover())
		{
			this.LeaveCover();
		}
		if (this.IsEnteringVehicle() && this.targetVehicle.claimingSquad != squad)
		{
			this.LeaveVehicle(false);
		}
		if (this.HasPath())
		{
			this.CancelPath(false);
		}
		squad.AddMember(this);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00004979 File Offset: 0x00002B79
	public bool InCover()
	{
		return this.inCover;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00004981 File Offset: 0x00002B81
	public void MarkTakingFireFrom(Vector3 direction)
	{
		this.takingFireDirection = direction;
		this.takingFireAction.Start();
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00004995 File Offset: 0x00002B95
	public override bool IsTakingFire()
	{
		return !this.takingFireAction.TrueDone() && !this.IsMeleeCharging();
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000049AF File Offset: 0x00002BAF
	public override SpawnPoint SelectedSpawnPoint()
	{
		if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
		{
			return ActorManager.RandomSpawnPointForTeam(this.actor.team);
		}
		return ActorManager.RandomFrontlineSpawnPointForTeam(this.actor.team);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000049E8 File Offset: 0x00002BE8
	public override Transform WeaponParent()
	{
		return this.weaponParent;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000049E8 File Offset: 0x00002BE8
	public override Transform TpWeaponParent()
	{
		return this.weaponParent;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000049F0 File Offset: 0x00002BF0
	public bool HasTargetVehicle()
	{
		return this.targetVehicle != null;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00052788 File Offset: 0x00050988
	public void GotoAndEnterVehicle(Vehicle vehicle, bool isMovementOverride = false)
	{
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		if (!vehicle.gameObject.activeInHierarchy)
		{
			this.DropTargetVehicle();
			return;
		}
		if (this.isSeated && this.actor.seat.vehicle == vehicle)
		{
			return;
		}
		if (this.targetVehicle != null)
		{
			this.LeaveVehicle(false);
		}
		vehicle.ReserveSeat();
		this.SetTargetVehicle(vehicle);
		this.GotoTargetVehicle(true);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00052800 File Offset: 0x00050A00
	private void GotoTargetVehicle(bool isMovementOverride = false)
	{
		List<Vector3> list = new List<Vector3>(1);
		list.Add(this.targetVehicle.transform.position);
		this.GotoAppendNodes(this.targetVehicle.transform.position, list, isMovementOverride);
		this.StartSprint();
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00052848 File Offset: 0x00050A48
	public void Repair(Vehicle vehicle)
	{
		this.targetRepairVehicle = vehicle;
		this.Goto(vehicle.transform.position + UnityEngine.Random.insideUnitSphere.ToGround().normalized * vehicle.GetAvoidanceCoarseRadius(), false);
		this.StartSprint();
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000049FE File Offset: 0x00002BFE
	public override void FillDriverSeat()
	{
		if (this.isSeated)
		{
			this.actor.SwitchSeat(0, false);
		}
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00004A16 File Offset: 0x00002C16
	private void DropTargetVehicle()
	{
		this._targetVehicle = null;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00004A1F File Offset: 0x00002C1F
	private void SetTargetVehicle(Vehicle vehicle)
	{
		this._targetVehicle = vehicle;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00004A28 File Offset: 0x00002C28
	public void EnterSeat(Seat seat)
	{
		this.actor.EnterSeat(seat, false);
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00052898 File Offset: 0x00050A98
	public void LeaveVehicle(bool isMovementOverride = false)
	{
		if (this.isDefaultMovementOverridden && !isMovementOverride)
		{
			return;
		}
		if (this.targetVehicle != null)
		{
			this.targetVehicle.DropSeatReservation();
		}
		if (this.isSeated)
		{
			this.actor.LeaveSeat(false);
		}
		else if (this.targetVehicle != null)
		{
			this.CancelPath(false);
		}
		this.DropTargetVehicle();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void SwitchedToWeapon(Weapon weapon)
	{
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void HolsteredActiveWeapon()
	{
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool Jump()
	{
		return false;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x000528FC File Offset: 0x00050AFC
	public override bool Crouch()
	{
		if (this.InCover())
		{
			return this.cover.type == CoverPoint.Type.Crouch && (this.IsReloading() || this.CoolingDown());
		}
		return !this.isFleeing && !this.IsSprinting() && this.isAlert && (!this.IsMoving() || this.IsTakingFire() || (this.InSquad() && this.squad.HasPlayerLeader() && FpsActorController.instance.Crouch()));
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00052980 File Offset: 0x00050B80
	public override bool Prone()
	{
		return (this.actor.activeWeapon == null || this.actor.activeWeapon.allowProne) && ((!this.hasPath && this.HasSpottedTarget() && this.engageTargetWhileProne) || (this.cachedIsFollowing && this.squad.Leader().actor.stance == Actor.Stance.Prone));
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool ChangeStance(Actor.Stance stance)
	{
		return true;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void ForceChangeStance(Actor.Stance stance)
	{
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x000529F0 File Offset: 0x00050BF0
	public override WeaponManager.LoadoutSet GetLoadout()
	{
		WeaponManager.LoadoutSet loadoutSet = new WeaponManager.LoadoutSet();
		loadoutSet.primary = WeaponManager.GetAiWeaponPrimary(this.loadoutStrategy, this.actor.team);
		loadoutSet.secondary = WeaponManager.GetAiWeaponSecondary(this.loadoutStrategy, this.actor.team);
		loadoutSet.gear1 = WeaponManager.GetAiWeaponSmallGear(this.loadoutStrategy, this.actor.team);
		AiActorController.LoadoutPickStrategy loadoutPickStrategy = new AiActorController.LoadoutPickStrategy();
		loadoutPickStrategy.type = this.loadoutStrategy.type;
		loadoutPickStrategy.distance = this.loadoutStrategy.distance;
		WeaponManager.WeaponEntry.LoadoutType loadoutType = WeaponManager.WeaponEntry.LoadoutType.Normal;
		if (loadoutSet.gear1 != null)
		{
			loadoutType = loadoutSet.gear1.type;
		}
		if (this.loadoutStrategy.type == WeaponManager.WeaponEntry.LoadoutType.Normal)
		{
			if (UnityEngine.Random.Range(0f, 1f) < 0.2f)
			{
				loadoutPickStrategy.type = WeaponManager.WeaponEntry.LoadoutType.AntiArmor;
				if (loadoutPickStrategy.distance == WeaponManager.WeaponEntry.Distance.Short)
				{
					loadoutPickStrategy.distance = WeaponManager.WeaponEntry.Distance.Mid;
				}
			}
		}
		else if (loadoutType == this.loadoutStrategy.type)
		{
			loadoutPickStrategy.type = WeaponManager.WeaponEntry.LoadoutType.Normal;
		}
		loadoutPickStrategy.distance = WeaponManager.WeaponEntry.Distance.Any;
		loadoutSet.gear2 = WeaponManager.GetAiWeaponAllGear(loadoutPickStrategy, this.actor.team);
		return loadoutSet;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00052B04 File Offset: 0x00050D04
	private void SwitchToPrimaryWeapon()
	{
		for (int i = 0; i < this.actor.weapons.Length; i++)
		{
			if (this.actor.weapons[i] != null && this.actor.weapons[i].HasAnyAmmo())
			{
				if (this.actor.weapons[i] != this.actor.activeWeapon)
				{
					this.actor.SwitchWeapon(i);
				}
				return;
			}
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00052B80 File Offset: 0x00050D80
	private void SwitchToRepairWeapon()
	{
		for (int i = 0; i < this.actor.weapons.Length; i++)
		{
			if (this.actor.weapons[i] != null && this.actor.weapons[i].CanRepair())
			{
				this.actor.SwitchWeapon(i);
				return;
			}
		}
		Debug.Log("Failed to switch to repair weapon!");
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00004A38 File Offset: 0x00002C38
	private bool HasPrimaryWeaponEquipped()
	{
		return this.actor.weapons[0] != null && this.actor.activeWeapon == this.actor.weapons[0];
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00052BE8 File Offset: 0x00050DE8
	private void SwitchToEffectiveWeapon(Actor target)
	{
		target.GetTargetType();
		Vector3.Distance(base.transform.position, target.transform.position);
		Weapon[] array = this.actor.weapons;
		if (this.isSeated && this.actor.seat.HasActiveWeapon())
		{
			Weapon[] weapons = this.actor.seat.weapons;
			array = weapons;
		}
		int num = -1;
		AiActorController.EvaluatedWeaponEffectiveness evaluatedWeaponEffectiveness = AiActorController.EvaluatedWeaponEffectiveness.No;
		for (int i = array.Length - 1; i >= 0; i--)
		{
			Weapon weapon = array[i];
			if (weapon != null)
			{
				AiActorController.EvaluatedWeaponEffectiveness weaponEffectivenessAgainstTarget = this.GetWeaponEffectivenessAgainstTarget(weapon, target);
				if (weaponEffectivenessAgainstTarget != AiActorController.EvaluatedWeaponEffectiveness.No && weaponEffectivenessAgainstTarget >= evaluatedWeaponEffectiveness)
				{
					evaluatedWeaponEffectiveness = weaponEffectivenessAgainstTarget;
					num = i;
				}
			}
		}
		if (num != -1)
		{
			this.actor.SwitchWeapon(num);
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00052CA4 File Offset: 0x00050EA4
	private void OnGUI()
	{
		if (ActorManager.instance.debug && !this.actor.dead)
		{
			Camera mainCamera = GameManager.GetMainCamera();
			if (mainCamera == null)
			{
				return;
			}
			float num = Vector3.Dot(this.actor.CenterPosition() - mainCamera.transform.position, mainCamera.transform.forward);
			if (num > 1f && num < 100f)
			{
				Vector3 vector = mainCamera.WorldToScreenPoint(this.actor.CenterPosition() + Vector3.up);
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				if (this.squad == null)
				{
					this.Label(new Rect(vector.x - 100f, (float)Screen.height - vector.y, 200f, 50f), "No Squad :(");
					return;
				}
				this.Label(new Rect(vector.x - 100f, (float)Screen.height - vector.y, 200f, 50f), string.Concat(new string[]
				{
					"Squad #",
					this.squad.number.ToString(),
					": ",
					this.squad.CurrentOrderString(),
					" ",
					this.squad.members.Count.ToString(),
					" members"
				}));
				if (!this.stayInCoverAction.TrueDone())
				{
					this.Label(new Rect(vector.x - 100f, (float)Screen.height - vector.y + 20f, 200f, 50f), "Staying in cover");
				}
				if (this.blockerAhead)
				{
					this.Label(new Rect(vector.x - 100f, (float)Screen.height - vector.y + 40f, 200f, 50f), "Blocker ahead!");
				}
				if (!this.squad.AllowTakeTemporaryCover())
				{
					this.Label(new Rect(vector.x - 100f, (float)Screen.height - vector.y + 60f, 200f, 50f), "Temp Cover Not Allowed!");
				}
			}
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00004A6E File Offset: 0x00002C6E
	private void Label(Rect rect, string s)
	{
		GUI.color = Color.black;
		GUI.Label(rect, s);
		rect.position -= Vector2.one;
		GUI.color = Color.white;
		GUI.Label(rect, s);
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00004AA9 File Offset: 0x00002CA9
	public override bool IsGroupedUp()
	{
		return this.squad != null && this.squad.IsGroupedUp();
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00004AC0 File Offset: 0x00002CC0
	public override bool IdlePose()
	{
		return !this.IsAlert();
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00052EEC File Offset: 0x000510EC
	private bool IsMeleeCharging()
	{
		return this.HasSpottedTarget() && this.actor.activeWeapon != null && ((this.actor.activeWeapon.IsMeleeWeapon() && !this.meleeChargeAction.TrueDone()) || this.modifier.alwaysChargeTarget);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool HoldingSprint()
	{
		return false;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00004ACB File Offset: 0x00002CCB
	public override bool IsSprinting()
	{
		return this.isSprinting;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool IsAirborne()
	{
		return false;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00004AD3 File Offset: 0x00002CD3
	public override bool UseSprintingAnimation()
	{
		return this.IsSprinting() && this.movementSpeed > 5f;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00004AEC File Offset: 0x00002CEC
	public override bool HasPath()
	{
		return this.hasPath || (this.flying && this.hasFlightTargetPosition);
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00004B08 File Offset: 0x00002D08
	public bool IsDeployingSmoke()
	{
		return !this.deploySmokeAction.TrueDone();
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00052F44 File Offset: 0x00051144
	public bool IsRepairing()
	{
		return this.squad != null && this.squad.order != null && this.squad.order.type == Order.OrderType.Repair && !this.IsInTemporaryCover() && this.targetRepairVehicle != null && Vector3.Distance(this.actor.Position(), this.targetRepairVehicle.transform.position) - this.targetRepairVehicle.GetAvoidanceCoarseRadius() < 2f;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00004B18 File Offset: 0x00002D18
	public bool IsPatrolingBase()
	{
		return this.squad != null && this.squad.order != null && this.squad.order.type == Order.OrderType.PatrolBase;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00004B44 File Offset: 0x00002D44
	public override Vector2 AimInput()
	{
		return Vector2.zero;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00004B4B File Offset: 0x00002D4B
	public override float RangeInput()
	{
		return 0f;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00004B52 File Offset: 0x00002D52
	public override Vector3 PathEndPoint()
	{
		return this.lastGotoPoint;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00004B5A File Offset: 0x00002D5A
	public override bool CurrentWaypoint(out Vector3 waypoint)
	{
		waypoint = this.currentWaypoint;
		return this.hasPath;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x00004B6E File Offset: 0x00002D6E
	public override bool Countermeasures()
	{
		return this.isDriver && this.actor.seat.vehicle.IsBeingTrackedByMissile() && this.countermeasureReactAction.TrueDone();
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void StartLadder(Ladder ladder)
	{
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00004B9C File Offset: 0x00002D9C
	public override void EndLadder(Vector3 exitPosition, Quaternion flatFacing)
	{
		base.transform.position = exitPosition;
		base.transform.rotation = flatFacing;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00052FC4 File Offset: 0x000511C4
	public override float LadderInput()
	{
		AiActorController.LadderPathSection ladderPathSection = this.NextLadderInPath();
		if (ladderPathSection == null)
		{
			return -1f;
		}
		if (!ladderPathSection.goUp)
		{
			return -1f;
		}
		return 1f;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool DeployParachute()
	{
		return true;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00004BB6 File Offset: 0x00002DB6
	public override void OnCancelParachute()
	{
		this.hasParachuteLandPosition = false;
		if (this.hasPath)
		{
			this.RecalculatePath();
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void DisableMovement()
	{
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void EnableMovement()
	{
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool IsMovementEnabled()
	{
		return false;
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00004BCD File Offset: 0x00002DCD
	public override void Move(Vector3 movement)
	{
		base.transform.position += movement;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00052FF4 File Offset: 0x000511F4
	public override Vector2 ParachuteInput()
	{
		if (this.cachedIsFollowing)
		{
			Vector3 vector = this.squad.Leader().actor.Position() - this.actor.Position();
			return new Vector2(vector.x, vector.z);
		}
		if (this.hasPath)
		{
			Vector3 waypointDelta = this.GetWaypointDelta();
			return new Vector2(waypointDelta.x, waypointDelta.z);
		}
		return Vector3.zero;
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00004BE6 File Offset: 0x00002DE6
	public void CancelOrder(Order order)
	{
		if (order.type == Order.OrderType.Defend && this.HasTargetVehicle() && this.targetVehicle.isTurret)
		{
			this.LeaveVehicle(false);
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00004C0D File Offset: 0x00002E0D
	public override bool IsOnPlayerSquad()
	{
		return this.squad != null && this.squad.HasPlayerLeader();
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0005306C File Offset: 0x0005126C
	public void FleeAndDiveFrom(Vector3 position, float diveTime, float diveRange)
	{
		if (this.isSeated)
		{
			return;
		}
		this.isFleeing = true;
		this.LeaveCover();
		this.LookDirection(this.actor.Position() - position);
		this.FleeFrom(position);
		this.fleeAndDiveAction.StartLifetime(diveTime + UnityEngine.Random.Range(-0.3f, 0.3f));
		this.diveRange = diveRange;
		this.fleeFromPosition = position;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool SwitchFireMode()
	{
		return false;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool NextSightMode()
	{
		return false;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0000257D File Offset: 0x0000077D
	public override bool PreviousSightMode()
	{
		return false;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void ChangeAimFieldOfView(float fov)
	{
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00004C24 File Offset: 0x00002E24
	public void FreezeFacingDirection()
	{
		this.freezeFacingDirection = true;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00004C2D File Offset: 0x00002E2D
	public float GetFocusRange(AiActorController.FocusType type)
	{
		if (type == AiActorController.FocusType.Vehicle)
		{
			return this.vehicleFocus.range;
		}
		if (type == AiActorController.FocusType.HugeVehicle)
		{
			return this.hugeVehicleFocus.range;
		}
		return this.infantryFocus.range;
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00004C5A File Offset: 0x00002E5A
	public void ActivateSlowTargetDetection(float detectionRate = 0.3f)
	{
		this.slowTargetDetection = true;
		this.slowTargetDetectionRate = detectionRate;
		this.targetDetectionProgress = 0f;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00004C75 File Offset: 0x00002E75
	public void DeactivateSlowTargetDetection()
	{
		this.slowTargetDetection = false;
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x000530D8 File Offset: 0x000512D8
	public bool AllowCombatOverrideMovement()
	{
		return (this.squad == null || (!this.isCompletingPlayerMoveOrder && (!this.cachedIsFollowing || this.cachedFollowTargetDistance <= this.squad.acceptedFollowerLagDistance) && (!this.HasTarget() || this.squad.MayEngageTarget(this.target)))) && (!this.actor.IsSwimming() && this.actor.activeWeapon != null && !this.actor.activeWeapon.IsMeleeWeapon() && !this.IsEnteringVehicle()) && this.HasTarget();
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00004C7E File Offset: 0x00002E7E
	public override bool IsAlert()
	{
		return this.isAlert;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00004C86 File Offset: 0x00002E86
	public void SetNotAlert(bool limitSpeed)
	{
		this.isAlert = false;
		this.forceUnalertMovementSpeed = limitSpeed;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00053178 File Offset: 0x00051378
	public void SetAlert()
	{
		this.forceUnalertMovementSpeed = false;
		this.isAlert = true;
		this.DeactivateSlowTargetDetection();
		if (this.squad != null && !this.squad.isAlert && this.alertSquadCoroutine == null)
		{
			this.alertSquadCoroutine = base.StartCoroutine(this.AlertSquad());
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00004C96 File Offset: 0x00002E96
	public void CancelAlertSquad()
	{
		if (this.alertSquadCoroutine != null)
		{
			base.StopCoroutine(this.alertSquadCoroutine);
			this.alertSquadCoroutine = null;
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00004CB3 File Offset: 0x00002EB3
	private IEnumerator AlertSquad()
	{
		yield return new WaitForSeconds(AiActorController.PARAMETERS.ALERT_SQUAD_TIME);
		this.squad.SetAlert(true);
		this.alertSquadCoroutine = null;
		yield break;
	}

	// Token: 0x040002E1 RID: 737
	private const float AI_TICK_PERIOD = 0.2f;

	// Token: 0x040002E2 RID: 738
	private const float SPRINT_DURATION_MIN = 5f;

	// Token: 0x040002E3 RID: 739
	private const float SPRINT_DURATION_MAX = 12f;

	// Token: 0x040002E4 RID: 740
	private const float SPRINT_COOLDOWN_MIN = 10f;

	// Token: 0x040002E5 RID: 741
	private const float SPRINT_COOLDOWN_MAX = 16f;

	// Token: 0x040002E6 RID: 742
	private const float AI_MOVEMENT_SPEED_PENALTY = 0.5f;

	// Token: 0x040002E7 RID: 743
	private const float AI_SPRINT_SPEED_PENALTY = 1f;

	// Token: 0x040002E8 RID: 744
	private const float CROUCH_SPEED_CAP = 2f;

	// Token: 0x040002E9 RID: 745
	private const float PRONE_SPEED_CAP = 1f;

	// Token: 0x040002EA RID: 746
	public const float UNALERT_WALK_SPEED = 1f;

	// Token: 0x040002EB RID: 747
	public const float UNALERT_DRIVE_SPEED = 4f;

	// Token: 0x040002EC RID: 748
	public const float NORMAL_WALK_SPEED = 3.2f;

	// Token: 0x040002ED RID: 749
	private const float STRAFE_TARGET_SPEED = 1.5f;

	// Token: 0x040002EE RID: 750
	private const float SWIM_SPEED = 4.5f;

	// Token: 0x040002EF RID: 751
	public const float SPRINT_SPEED = 5.5f;

	// Token: 0x040002F0 RID: 752
	public const float BONUS_SPRINT_SPEED = 7f;

	// Token: 0x040002F1 RID: 753
	private const float FOLLOW_SPRINT_SPEED_BONUS = 1f;

	// Token: 0x040002F2 RID: 754
	private const float MIN_MOVEMENT_SPEED = 0.5f;

	// Token: 0x040002F3 RID: 755
	private const float CLIMB_SLOPE_MOVEMENT_SPEED = 0.5f;

	// Token: 0x040002F4 RID: 756
	private const float HAS_TARGET_WALK_SPEED = 3.2f;

	// Token: 0x040002F5 RID: 757
	private const float HAS_TARGET_CLOSE_WALK_SPEED = 2f;

	// Token: 0x040002F6 RID: 758
	private const float WAIT_FOR_FOLLOWERS_SPEED = 2f;

	// Token: 0x040002F7 RID: 759
	private const float ACCELERATION = 10f;

	// Token: 0x040002F8 RID: 760
	private const float MAX_DETECTION_DISTANCE = 1000f;

	// Token: 0x040002F9 RID: 761
	private const float MAX_FOCUS_DISTANCE_INFANTRY = 1000f;

	// Token: 0x040002FA RID: 762
	private const float FOCUS_DISTANCE_OFFSET_INFANTRY = 50f;

	// Token: 0x040002FB RID: 763
	private const float MAX_FOCUS_DISTANCE_VEHICLES = 3000f;

	// Token: 0x040002FC RID: 764
	private const float FOCUS_DISTANCE_OFFSET_VEHICLES = 300f;

	// Token: 0x040002FD RID: 765
	private const float MAX_FOCUS_DISTANCE_HUGE_VEHICLES = 10000f;

	// Token: 0x040002FE RID: 766
	private const float FOCUS_DISTANCE_OFFSET_HUGE_VEHICLES = 2000f;

	// Token: 0x040002FF RID: 767
	private const float SPAWN_FOCUS_DISTANCE = 20f;

	// Token: 0x04000300 RID: 768
	private const float PRONE_TARGET_RANGE = 200f;

	// Token: 0x04000301 RID: 769
	private const float PATROL_BASE_HEAR_DISTANCE = 90f;

	// Token: 0x04000302 RID: 770
	private const float HAS_TARGET_CLOSE_DISTANCE = 50f;

	// Token: 0x04000303 RID: 771
	private const float MELEE_CHARGE_RANGE = 30f;

	// Token: 0x04000304 RID: 772
	private const float SWITCH_TO_BACKUP_WEAPON_TARGET_DISTANCE = 40f;

	// Token: 0x04000305 RID: 773
	private const float FOLLOW_MIN_SPEED = 1f;

	// Token: 0x04000306 RID: 774
	private const float FOLLOW_MAX_SPEED = 3.2f;

	// Token: 0x04000307 RID: 775
	private const float FOLLOW_SPEED_GAIN_PER_METER = 2f;

	// Token: 0x04000308 RID: 776
	private const float FOLLOW_START_SPRINT_DISTANCE = 7f;

	// Token: 0x04000309 RID: 777
	private const float FOLLOWING_FIND_TEMPORARY_COVER_MAX_DISTANCE_TO_LEADER = 30f;

	// Token: 0x0400030A RID: 778
	private const float REPAIR_MAX_DISTANCE = 2f;

	// Token: 0x0400030B RID: 779
	private const float FLEE_DISTANCE = 10f;

	// Token: 0x0400030C RID: 780
	private const float FLEE_SPREAD_DISTANCE = 4f;

	// Token: 0x0400030D RID: 781
	private const float FLEE_DIVE_TIME_VARIATION = 0.3f;

	// Token: 0x0400030E RID: 782
	private const float DIVE_FORCE = 200f;

	// Token: 0x0400030F RID: 783
	private const float LEAD_CORNER_AIM_LOOKAHEAD_EXTRAPOLATION_TIME = 1f;

	// Token: 0x04000310 RID: 784
	public const float VEHICLE_DAMAGE_FACE_SOURCE_THRESHOLD = 400f;

	// Token: 0x04000311 RID: 785
	private const float CLOSE_QUARTERS_COMBAT_MOVEMENT_TARGET_THRESHOLD = 30f;

	// Token: 0x04000312 RID: 786
	private const float STRAFE_TARGET_BACK_OFF_RANGE_TRESHOLD = 10f;

	// Token: 0x04000313 RID: 787
	private const float CLOSE_TO_LAST_TARGET_THRESHOLD = 20f;

	// Token: 0x04000314 RID: 788
	private const float SKIP_AIMING_AOE_WEAPON_TARGET_DISTANCE = 30f;

	// Token: 0x04000315 RID: 789
	private const float WAYPOINT_CORNER_DOT_THRESHOLD = 0.95f;

	// Token: 0x04000316 RID: 790
	private const float CALM_DOWN_TIME = 20f;

	// Token: 0x04000317 RID: 791
	public static AiActorController.AiParameters PARAMETERS;

	// Token: 0x04000318 RID: 792
	private static AiActorController.AiParameters PARAMETERS_EASY;

	// Token: 0x04000319 RID: 793
	private static AiActorController.AiParameters PARAMETERS_NORMAL;

	// Token: 0x0400031A RID: 794
	private static AiActorController.AiParameters PARAMETERS_HARD;

	// Token: 0x0400031B RID: 795
	private const int ALTERNATIVE_PATH_TAG_PENALTY = 30000;

	// Token: 0x0400031C RID: 796
	private const float VEHICLE_STUCK_DISTANCE = 0.4f;

	// Token: 0x0400031D RID: 797
	private const float VEHICLE_STUCK_TIME = 1.5f;

	// Token: 0x0400031E RID: 798
	private const float VEHICLE_STUCK_RECOVER_TIME = 1.8f;

	// Token: 0x0400031F RID: 799
	private const float MAX_RECENT_ANTI_STUCK_EVENTS = 2f;

	// Token: 0x04000320 RID: 800
	private const float ANTI_STUCK_EVENT_LIFETIME = 30f;

	// Token: 0x04000321 RID: 801
	private const int CAR_UNEVEN_SURFACE_PENALTY = 100000;

	// Token: 0x04000322 RID: 802
	private const float CAR_REVERSE_SPEED = 7f;

	// Token: 0x04000323 RID: 803
	private const float CAR_DEFAULT_SPEED = 15f;

	// Token: 0x04000324 RID: 804
	public const float HELICOPTER_TARGET_FLIGHT_HEIGHT_MIN = 40f;

	// Token: 0x04000325 RID: 805
	public const float HELICOPTER_TARGET_FLIGHT_HEIGHT_MAX = 80f;

	// Token: 0x04000326 RID: 806
	private const float HELICOPTER_HEIGHT_EXTRAPOLATION_TIME = 3f;

	// Token: 0x04000327 RID: 807
	private const float HELICOPTER_CANCEL_ATTACK_EXTRAPOLATED_HEIGHT = 15f;

	// Token: 0x04000328 RID: 808
	private const float HELICOPTER_MAX_PITCH = 20f;

	// Token: 0x04000329 RID: 809
	private const float HELICOPTER_MAX_PITCH_ATTACK = 35f;

	// Token: 0x0400032A RID: 810
	private const float HELICOPTER_MAX_ROLL = 25f;

	// Token: 0x0400032B RID: 811
	private const float HELICOPTER_MAX_ROLL_ATTACK = 35f;

	// Token: 0x0400032C RID: 812
	private const float HELICOPTER_MAX_ROLL_LANDING = 15f;

	// Token: 0x0400032D RID: 813
	private const float HELICOPTER_ATTACK_RANGE = 300f;

	// Token: 0x0400032E RID: 814
	private const float PLANE_TARGET_FLIGHT_HEIGHT_MIN = 130f;

	// Token: 0x0400032F RID: 815
	private const float PLANE_TARGET_FLIGHT_HEIGHT_MAX = 300f;

	// Token: 0x04000330 RID: 816
	private const float PLANE_HEIGHT_EXTRAPOLATION_TIME = 3f;

	// Token: 0x04000331 RID: 817
	private const float PLANE_MIN_PITCH = -30f;

	// Token: 0x04000332 RID: 818
	private const float PLANE_MAX_PITCH = 15f;

	// Token: 0x04000333 RID: 819
	private const float PLANE_MAX_ROLL = 40f;

	// Token: 0x04000334 RID: 820
	private const float PLANE_START_ATTACK_MIN_DISTANCE = 80f;

	// Token: 0x04000335 RID: 821
	private const float PLANE_START_ATTACK_DOT = 0.85f;

	// Token: 0x04000336 RID: 822
	private const float PLANE_STEERING_MULTIPLIER_NOOB = 1f;

	// Token: 0x04000337 RID: 823
	private const float PLANE_STEERING_MULTIPLIER_PRO = 2f;

	// Token: 0x04000338 RID: 824
	private const float PLANE_MIN_U_TURN_DISTANCE_PRO = 250f;

	// Token: 0x04000339 RID: 825
	private const float PLANE_MIN_U_TURN_DISTANCE_NOOB = 350f;

	// Token: 0x0400033A RID: 826
	private const float PLANE_U_TURN_DOT = 0.5f;

	// Token: 0x0400033B RID: 827
	private const float PLANE_MAX_ATTACK_ANGLE = 30f;

	// Token: 0x0400033C RID: 828
	private const float PLANE_MIN_ATTACK_CANCEL_EXTRAPOLATION_TIME = 2f;

	// Token: 0x0400033D RID: 829
	private const float PLANE_MIN_ATTACK_CANCEL_HEIGHT = 15f;

	// Token: 0x0400033E RID: 830
	private const float PLANE_BEING_CHASED_DOT = 0.2f;

	// Token: 0x0400033F RID: 831
	private const float PLANE_COLLISION_COURSE_EXTRAPOLATION_TIME_MIN = 2f;

	// Token: 0x04000340 RID: 832
	private const float PLANE_COLLISION_COURSE_EXTRAPOLATION_TIME_MAX = 3f;

	// Token: 0x04000341 RID: 833
	private const float PLANE_COLLISION_COURSE_PASS_DISTANCE = 30f;

	// Token: 0x04000342 RID: 834
	private const float TANK_PROJECTED_DRIVING_MIN_DISTANCE = 3f;

	// Token: 0x04000343 RID: 835
	private const float TANK_PROJECTED_DRIVING_SPEED_GAIN = 0.6f;

	// Token: 0x04000344 RID: 836
	private const float TANK_ANTI_SWING_GAIN = 0.2f;

	// Token: 0x04000345 RID: 837
	private const float CAR_PROJECTED_DRIVING_MIN_DISTANCE = 5f;

	// Token: 0x04000346 RID: 838
	private const float CAR_PROJECTED_DRIVING_SPEED_GAIN = 0.8f;

	// Token: 0x04000347 RID: 839
	private const float CAR_ANTI_SWING_GAIN = 0.35f;

	// Token: 0x04000348 RID: 840
	private const float CAR_PREDICTION_PROJECTED_DRIVING_MIN_DISTANCE = 4f;

	// Token: 0x04000349 RID: 841
	private const float CAR_PREDICTION_PROJECTED_DRIVING_SPEED_GAIN = 3f;

	// Token: 0x0400034A RID: 842
	private const float BOAT_PROJECTED_DRIVING_MIN_DISTANCE = 5f;

	// Token: 0x0400034B RID: 843
	private const float BOAT_PROJECTED_DRIVING_SPEED_GAIN = 1f;

	// Token: 0x0400034C RID: 844
	private const float BOAT_ANTI_SWING_GAIN = 1f;

	// Token: 0x0400034D RID: 845
	private const float CAR_TURN_MULTIPLIER = 5f;

	// Token: 0x0400034E RID: 846
	private const float TRANSPORT_EXIT_AND_WALK_MAX_DISTANCE = 40f;

	// Token: 0x0400034F RID: 847
	private const int TAG_MASK_ANY = -1;

	// Token: 0x04000350 RID: 848
	private const int TAG_MASK_NO_SHALLOW = -5;

	// Token: 0x04000351 RID: 849
	private const int TAG_MASK_NO_UNDERWATER = -9;

	// Token: 0x04000352 RID: 850
	private const float AI_WEAPON_FAST_TICK_PERIOD = 0.05f;

	// Token: 0x04000353 RID: 851
	private const float AI_WEAPON_SLOW_TICK_PERIOD = 0.3f;

	// Token: 0x04000354 RID: 852
	private const float AI_WEAPON_DROP_BOMBS_TICK_PERIOD = 2f;

	// Token: 0x04000355 RID: 853
	private const float AI_WEAPON_SLOW_TICK_DISTANCE = 100f;

	// Token: 0x04000356 RID: 854
	private const float AI_FORCE_FIRE_MIN_TIME = 6f;

	// Token: 0x04000357 RID: 855
	private const float AI_FORCE_FIRE_MAX_TIME = 10f;

	// Token: 0x04000358 RID: 856
	public const float TAKING_FIRE_MAX_DISTANCE = 5f;

	// Token: 0x04000359 RID: 857
	private const float GUNNER_STAY_IN_SEAT_MAX_LEADER_DISTANCE = 30f;

	// Token: 0x0400035A RID: 858
	private const float FOOT_BLOCK_SPHERECAST_RADIUS = 0.5f;

	// Token: 0x0400035B RID: 859
	private const float FOOT_CHECK_BLOCKER_AHEAD_RANGE = 2f;

	// Token: 0x0400035C RID: 860
	private const int FOOT_BLOCK_MASK = 4096;

	// Token: 0x0400035D RID: 861
	private const float VEHICLE_BLOCK_AHEAD_TIME = 1f;

	// Token: 0x0400035E RID: 862
	private const float VEHICLE_BLOCK_AVOID_MULTIPLIER = 0.3f;

	// Token: 0x0400035F RID: 863
	private const int VEHICLE_BLOCK_MASK = 256;

	// Token: 0x04000360 RID: 864
	private const float CAR_TURNING_SPEED_MULTIPLIER = 0.5f;

	// Token: 0x04000361 RID: 865
	private const float CAR_DRIVING_FORWARD_TURN_MULTIPLIER = 0.5f;

	// Token: 0x04000362 RID: 866
	private const float AI_MIN_SCAN_TIME = 0.8f;

	// Token: 0x04000363 RID: 867
	private const float AI_MAX_SCAN_TIME = 3f;

	// Token: 0x04000364 RID: 868
	private const float LOOK_FORWARD_CHANCE = 0.7f;

	// Token: 0x04000365 RID: 869
	private const float SCAN_LEVEL_CHANCE = 0.5f;

	// Token: 0x04000366 RID: 870
	private const float SCAN_MAX_EXTRA_NORMALIZED_HEIGHT = 0.2f;

	// Token: 0x04000367 RID: 871
	private const float AI_FACE_HIGHLIGHTED_DISTANCE = 30f;

	// Token: 0x04000368 RID: 872
	private const float AI_FACE_HIGHLIGHTED_CHANCE = 0.2f;

	// Token: 0x04000369 RID: 873
	private const float AI_CHASE_EXTRAPOLATION_TIME = 2f;

	// Token: 0x0400036A RID: 874
	private const float AI_INVESTIGATE_MIN_TIME = 3f;

	// Token: 0x0400036B RID: 875
	private const float AI_UPDATE_CLOSE_ACTORS_TIME = 1f;

	// Token: 0x0400036C RID: 876
	private const float CLOSE_ACTORS_RANGE = 10f;

	// Token: 0x0400036D RID: 877
	private const float LOCAL_AVOIDANCE_MIN_DISTANCE = 1.5f;

	// Token: 0x0400036E RID: 878
	private const float LOCAL_AVOIDANCE_SPEED = 2f;

	// Token: 0x0400036F RID: 879
	private const int FRIENDLY_LAYER_MASK = 5376;

	// Token: 0x04000370 RID: 880
	public const int GROUND_LAYER_MASK = 1;

	// Token: 0x04000371 RID: 881
	public const int AIRCRAFT_HEIGHT_CHECK_RAY_LAYER_MASK = 4194305;

	// Token: 0x04000372 RID: 882
	private const float FATIGUE_GAIN = 0.04f;

	// Token: 0x04000373 RID: 883
	private const float FATIGUE_DRAIN = 0.4f;

	// Token: 0x04000374 RID: 884
	public const float LOOK_TOWARDS_TARGET_DAMP = 0.005f;

	// Token: 0x04000375 RID: 885
	private const float AIM_CONSTANT_SPEED = 5f;

	// Token: 0x04000376 RID: 886
	private const float MIN_GOTO_DELTA = 2f;

	// Token: 0x04000377 RID: 887
	public const float SIGHT_FOV_DOT = 0.65f;

	// Token: 0x04000378 RID: 888
	private const float FOLLOW_MAX_ALLOWED_TARGET_DISTANCE = 3f;

	// Token: 0x04000379 RID: 889
	public const float WAYPOINT_COMPLETE_DISTANCE = 0.2f;

	// Token: 0x0400037A RID: 890
	public const float WAYPOINT_COMPLETE_DISTANCE_VEHICLE = 2.5f;

	// Token: 0x0400037B RID: 891
	public const float WAYPOINT_COMPLETE_DISTANCE_VEHICLE_AQUATIC = 5f;

	// Token: 0x0400037C RID: 892
	private const float LEAN_SPEED = 2f;

	// Token: 0x0400037D RID: 893
	private const float EYE_HEIGHT = 0.2f;

	// Token: 0x0400037E RID: 894
	private const float MAX_ENTER_SEAT_DISTANCE = 4f;

	// Token: 0x0400037F RID: 895
	private const float SELECT_NON_FRONTLINE_SPAWN_CHANCE = 0.3f;

	// Token: 0x04000380 RID: 896
	private const float PLAYER_APPROACHING_DOT = 0.7f;

	// Token: 0x04000381 RID: 897
	private const float PLAYER_APPROACHING_LOOK_DOT = 0.9f;

	// Token: 0x04000382 RID: 898
	private const float PLAYER_APPROACHING_MAX_RANGE = 30f;

	// Token: 0x04000383 RID: 899
	private const float GET_IN_PLAYER_VEHICLE_RANGE = 8f;

	// Token: 0x04000384 RID: 900
	private const float HOLD_FIRE_MIN_TIME = 0.3f;

	// Token: 0x04000385 RID: 901
	private const float HOLD_FIRE_MAX_TIME = 1.2f;

	// Token: 0x04000386 RID: 902
	private const float SLOW_TARGET_DETECTION_DEFAULT_RATE = 0.3f;

	// Token: 0x04000387 RID: 903
	public const float SLOW_DETECTION_CLOSE_DISTANCE_RANGE = 25f;

	// Token: 0x04000388 RID: 904
	public const float DETECTION_RATE_CLOSE_MULTIPLIER = 3f;

	// Token: 0x04000389 RID: 905
	public const float DETECTION_RATE_PRONE_MULTIPLIER = 0.5f;

	// Token: 0x0400038A RID: 906
	private const float DETECTION_DRAIN_RATE = 0.3f;

	// Token: 0x0400038B RID: 907
	private const float STRAFE_MIN_TIME = 1.4f;

	// Token: 0x0400038C RID: 908
	private const float STRAFE_MAX_TIME = 3f;

	// Token: 0x0400038D RID: 909
	public const float PARACHUTE_TARGET_EXTRA_PRIORITY_DISTANCE = 150f;

	// Token: 0x0400038E RID: 910
	public Transform eyeTransform;

	// Token: 0x0400038F RID: 911
	public Transform weaponParent;

	// Token: 0x04000390 RID: 912
	[NonSerialized]
	public AiActorController.SkillLevel skill;

	// Token: 0x04000391 RID: 913
	private Quaternion lookRotation = Quaternion.identity;

	// Token: 0x04000392 RID: 914
	private Vector3 targetLookDirection = Vector3.forward;

	// Token: 0x04000393 RID: 915
	[NonSerialized]
	public Actor target;

	// Token: 0x04000394 RID: 916
	private Actor lastTarget;

	// Token: 0x04000395 RID: 917
	[NonSerialized]
	public AiActorController.Modifier modifier;

	// Token: 0x04000396 RID: 918
	private AiActorController.DelVerifyPath verifyPathCallback;

	// Token: 0x04000397 RID: 919
	private bool hasPath;

	// Token: 0x04000398 RID: 920
	private bool calculatingPath;

	// Token: 0x04000399 RID: 921
	private bool swimmingToShore;

	// Token: 0x0400039A RID: 922
	private bool isInCqcZone;

	// Token: 0x0400039B RID: 923
	private Seeker seeker;

	// Token: 0x0400039C RID: 924
	[NonSerialized]
	public List<Vector3> vectorPath;

	// Token: 0x0400039D RID: 925
	private List<AiActorController.LadderPathSection> laddersInPath;

	// Token: 0x0400039E RID: 926
	private int waypoint;

	// Token: 0x0400039F RID: 927
	private float waypointDistance;

	// Token: 0x040003A0 RID: 928
	private bool nextWaypointIsCorner;

	// Token: 0x040003A1 RID: 929
	private bool hasFutureWaypoint;

	// Token: 0x040003A2 RID: 930
	private float nextWaypointCornerLean;

	// Token: 0x040003A3 RID: 931
	private bool isTraversingCorner;

	// Token: 0x040003A4 RID: 932
	private Vector3 infantryPathLookAheadPoint;

	// Token: 0x040003A5 RID: 933
	private float targetDistance;

	// Token: 0x040003A6 RID: 934
	private AiActorController.EvaluatedWeaponEffectiveness targetEffectiveness;

	// Token: 0x040003A7 RID: 935
	private Vector3 lastSeenTargetPosition = Vector3.zero;

	// Token: 0x040003A8 RID: 936
	private Vector3 lastSeenTargetVelocity = Vector3.zero;

	// Token: 0x040003A9 RID: 937
	private RadiusModifier radiusModifier;

	// Token: 0x040003AA RID: 938
	private bool fire;

	// Token: 0x040003AB RID: 939
	private bool spamFireInput;

	// Token: 0x040003AC RID: 940
	private float randomTimeOffset;

	// Token: 0x040003AD RID: 941
	private bool aboutToEnterCaptureZoneAttack;

	// Token: 0x040003AE RID: 942
	private Vector3 acquireTargetOffset;

	// Token: 0x040003AF RID: 943
	private TimedAction acquireTargetAction = new TimedAction(1f, false);

	// Token: 0x040003B0 RID: 944
	private TimedAction getInTemporaryCoverAction = new TimedAction(2.5f, false);

	// Token: 0x040003B1 RID: 945
	private TimedAction haltAction = new TimedAction(1f, false);

	// Token: 0x040003B2 RID: 946
	private TimedAction strafeAction = new TimedAction(1f, false);

	// Token: 0x040003B3 RID: 947
	private CoverPoint cover;

	// Token: 0x040003B4 RID: 948
	private bool inCover;

	// Token: 0x040003B5 RID: 949
	private TimedAction stayInCoverAction = new TimedAction(3f, false);

	// Token: 0x040003B6 RID: 950
	private float lean;

	// Token: 0x040003B7 RID: 951
	private Vector3 facingDirectionVector = Vector3.forward;

	// Token: 0x040003B8 RID: 952
	private TimedAction takingFireAction = new TimedAction(3f, false);

	// Token: 0x040003B9 RID: 953
	[NonSerialized]
	public Vector3 takingFireDirection;

	// Token: 0x040003BA RID: 954
	private float calmDownStartTimestamp;

	// Token: 0x040003BB RID: 955
	private bool combatMovementHasOverridenLeaderPath;

	// Token: 0x040003BC RID: 956
	[NonSerialized]
	public Vehicle targetRepairVehicle;

	// Token: 0x040003BD RID: 957
	private Vehicle _targetVehicle;

	// Token: 0x040003BE RID: 958
	private bool forceAntiStuckReverse;

	// Token: 0x040003BF RID: 959
	private bool waitForPlayer;

	// Token: 0x040003C0 RID: 960
	private int recentAntiStuckEvents;

	// Token: 0x040003C1 RID: 961
	private bool canTurnCarTowardsWaypoint = true;

	// Token: 0x040003C2 RID: 962
	private float planeSteeringMultiplier;

	// Token: 0x040003C3 RID: 963
	private float planeUTurnDistance;

	// Token: 0x040003C4 RID: 964
	private float planeCollisionCourseExtrapolationTime;

	// Token: 0x040003C5 RID: 965
	private List<Vehicle> avoidedVehicles = new List<Vehicle>();

	// Token: 0x040003C6 RID: 966
	private bool isSeated;

	// Token: 0x040003C7 RID: 967
	private bool isDriver;

	// Token: 0x040003C8 RID: 968
	private bool aquatic;

	// Token: 0x040003C9 RID: 969
	private bool flying;

	// Token: 0x040003CA RID: 970
	private bool hasFlightTargetPosition;

	// Token: 0x040003CB RID: 971
	[NonSerialized]
	public float targetFlightHeight;

	// Token: 0x040003CC RID: 972
	private Vector3 flightTargetPosition;

	// Token: 0x040003CD RID: 973
	private TimedAction attackRunAction = new TimedAction(4f, false);

	// Token: 0x040003CE RID: 974
	private TimedAction airplaneUTurnAutoReloadAction = new TimedAction(3f, false);

	// Token: 0x040003CF RID: 975
	private TimedAction attackRunCooldownAction = new TimedAction(8f, false);

	// Token: 0x040003D0 RID: 976
	private TimedAction helicopterTakeoffAction = new TimedAction(2f, false);

	// Token: 0x040003D1 RID: 977
	private TimedAction helicopterNewOrderAction = new TimedAction(10f, false);

	// Token: 0x040003D2 RID: 978
	private TimedAction planeTakeoffAction = new TimedAction(9f, false);

	// Token: 0x040003D3 RID: 979
	private TimedAction planeCollisionCourseAction = new TimedAction(3f, false);

	// Token: 0x040003D4 RID: 980
	private float vehicleSpeedLimitMultiplier = 1f;

	// Token: 0x040003D5 RID: 981
	private TimedAction planeDefensiveManeuverAction = new TimedAction(8f, false);

	// Token: 0x040003D6 RID: 982
	private AiActorController.FastMoverDefensiveManeuver planeDefensiveManeuver;

	// Token: 0x040003D7 RID: 983
	private float maneuverSeed;

	// Token: 0x040003D8 RID: 984
	private TimedAction planeForceManeuverAction = new TimedAction(30f, false);

	// Token: 0x040003D9 RID: 985
	private bool signalPassengersEnterAircraft;

	// Token: 0x040003DA RID: 986
	private TimedAction sprintAction = new TimedAction(1f, false);

	// Token: 0x040003DB RID: 987
	private TimedAction sprintCooldownAction = new TimedAction(4f, false);

	// Token: 0x040003DC RID: 988
	private TimedAction countermeasureReactAction = new TimedAction(1f, false);

	// Token: 0x040003DD RID: 989
	private TimedAction forceAimFireAction = new TimedAction(4f, false);

	// Token: 0x040003DE RID: 990
	private TimedAction moveTimeoutAction = new TimedAction(3f, false);

	// Token: 0x040003DF RID: 991
	private TimedAction holdDownFireAction = new TimedAction(1f, false);

	// Token: 0x040003E0 RID: 992
	private TimedAction lookAroundDisabledAction = new TimedAction(1f, false);

	// Token: 0x040003E1 RID: 993
	private Vector3 smokeTarget;

	// Token: 0x040003E2 RID: 994
	private TimedAction deploySmokeAction = new TimedAction(2f, false);

	// Token: 0x040003E3 RID: 995
	[NonSerialized]
	public Squad squad;

	// Token: 0x040003E4 RID: 996
	[NonSerialized]
	public bool isSquadLeader;

	// Token: 0x040003E5 RID: 997
	[NonSerialized]
	public Vector3 lastWaypoint;

	// Token: 0x040003E6 RID: 998
	[NonSerialized]
	public Vector3 currentWaypoint;

	// Token: 0x040003E7 RID: 999
	[NonSerialized]
	public Vector3 futureWaypoint;

	// Token: 0x040003E8 RID: 1000
	[NonSerialized]
	public Vector3 lastGotoPoint;

	// Token: 0x040003E9 RID: 1001
	private bool blockerAhead;

	// Token: 0x040003EA RID: 1002
	private Vector3 blockerPosition;

	// Token: 0x040003EB RID: 1003
	private TimedAction onlyPreferredOrHighPriorityTargetAction = new TimedAction(2f, false);

	// Token: 0x040003EC RID: 1004
	private TimedAction keepTargetAction = new TimedAction(8f, false);

	// Token: 0x040003ED RID: 1005
	private TimedAction meleeChargeAction = new TimedAction(10f, false);

	// Token: 0x040003EE RID: 1006
	private TimedAction allowInstantReloadAction = new TimedAction(4f, false);

	// Token: 0x040003EF RID: 1007
	private TimedAction justFiredAction = new TimedAction(1.5f, false);

	// Token: 0x040003F0 RID: 1008
	private Vector4 aircraftInput = Vector4.zero;

	// Token: 0x040003F1 RID: 1009
	private Vector2 carInput = Vector2.zero;

	// Token: 0x040003F2 RID: 1010
	private TriangleMeshNode cachedTargetNode;

	// Token: 0x040003F3 RID: 1011
	private List<Vector3> pathAppendedNodes;

	// Token: 0x040003F4 RID: 1012
	private float movementSpeed;

	// Token: 0x040003F5 RID: 1013
	private bool isSprinting;

	// Token: 0x040003F6 RID: 1014
	private bool cachedIsFollowing;

	// Token: 0x040003F7 RID: 1015
	private float cachedFollowTargetDistance;

	// Token: 0x040003F8 RID: 1016
	private bool isFleeing;

	// Token: 0x040003F9 RID: 1017
	private TimedAction fleeAndDiveAction = new TimedAction(1f, false);

	// Token: 0x040003FA RID: 1018
	private float diveRange;

	// Token: 0x040003FB RID: 1019
	private Vector3 fleeFromPosition = Vector3.zero;

	// Token: 0x040003FC RID: 1020
	private bool engageTargetWhileProne;

	// Token: 0x040003FD RID: 1021
	[NonSerialized]
	public AiActorController.LoadoutPickStrategy loadoutStrategy = new AiActorController.LoadoutPickStrategy();

	// Token: 0x040003FE RID: 1022
	[NonSerialized]
	public int squadMemberIndex;

	// Token: 0x040003FF RID: 1023
	[NonSerialized]
	public Vector3 parachuteLandPosition;

	// Token: 0x04000400 RID: 1024
	[NonSerialized]
	public bool hasParachuteLandPosition;

	// Token: 0x04000401 RID: 1025
	[NonSerialized]
	public bool isCompletingPlayerMoveOrder;

	// Token: 0x04000402 RID: 1026
	private AiActorController.FocusDistance infantryFocus;

	// Token: 0x04000403 RID: 1027
	private AiActorController.FocusDistance vehicleFocus;

	// Token: 0x04000404 RID: 1028
	private AiActorController.FocusDistance hugeVehicleFocus;

	// Token: 0x04000405 RID: 1029
	private bool freezeFacingDirection;

	// Token: 0x04000406 RID: 1030
	private Coroutine alertSquadCoroutine;

	// Token: 0x04000407 RID: 1031
	private bool isAlert = true;

	// Token: 0x04000408 RID: 1032
	private bool forceUnalertMovementSpeed;

	// Token: 0x04000409 RID: 1033
	private TimedAction isDetectingEnemyAction = new TimedAction(0.25f, false);

	// Token: 0x0400040A RID: 1034
	private TimedAction modifyDetectionSpeedAction = new TimedAction(0.25f, false);

	// Token: 0x0400040B RID: 1035
	[NonSerialized]
	public bool slowTargetDetection;

	// Token: 0x0400040C RID: 1036
	private float slowTargetDetectionRate = 1f;

	// Token: 0x0400040D RID: 1037
	private float modifiedDetectionSpeed = 1f;

	// Token: 0x0400040E RID: 1038
	[NonSerialized]
	public float targetDetectionProgress;

	// Token: 0x0400040F RID: 1039
	[NonSerialized]
	public bool isDefaultMovementOverridden;

	// Token: 0x04000410 RID: 1040
	private IEnumerator[] aiRoutines;

	// Token: 0x04000411 RID: 1041
	private TimedAction forceAntiStuckReverseAction = new TimedAction(1.8f, false);

	// Token: 0x04000412 RID: 1042
	private TimedAction gotoFollowPointCooldown = new TimedAction(0.4f, false);

	// Token: 0x04000413 RID: 1043
	private int gotoFollowPointIterationCooldown;

	// Token: 0x04000414 RID: 1044
	private TimedAction dropBombsAction = new TimedAction(2f, false);

	// Token: 0x04000415 RID: 1045
	private int originalPathLength;

	// Token: 0x04000416 RID: 1046
	public AiActorController.DelOnDetectedEnemy onDetectedEnemy;

	// Token: 0x04000417 RID: 1047
	private bool reachedWaypoint;

	// Token: 0x04000418 RID: 1048
	private float landingPlannedHeading;

	// Token: 0x04000419 RID: 1049
	private TimedAction landingInProgressAction = new TimedAction(1f, false);

	// Token: 0x0400041A RID: 1050
	[NonSerialized]
	public bool hasLandedUNSAFE;

	// Token: 0x0200007C RID: 124
	public enum FocusType
	{
		// Token: 0x0400041C RID: 1052
		Infantry,
		// Token: 0x0400041D RID: 1053
		Vehicle,
		// Token: 0x0400041E RID: 1054
		HugeVehicle
	}

	// Token: 0x0200007D RID: 125
	public enum EvaluatedWeaponEffectiveness
	{
		// Token: 0x04000420 RID: 1056
		No,
		// Token: 0x04000421 RID: 1057
		StandardRangePenalty,
		// Token: 0x04000422 RID: 1058
		PreferredRangePenalty,
		// Token: 0x04000423 RID: 1059
		Standard,
		// Token: 0x04000424 RID: 1060
		Preferred
	}

	// Token: 0x0200007E RID: 126
	public enum SkillLevel
	{
		// Token: 0x04000426 RID: 1062
		Beginner,
		// Token: 0x04000427 RID: 1063
		Normal,
		// Token: 0x04000428 RID: 1064
		Veteran,
		// Token: 0x04000429 RID: 1065
		Elite
	}

	// Token: 0x0200007F RID: 127
	public struct AiParameters
	{
		// Token: 0x0400042A RID: 1066
		public float REACTION_TIME;

		// Token: 0x0400042B RID: 1067
		public float LEAD_SWAY_MAGNITUDE;

		// Token: 0x0400042C RID: 1068
		public float LEAD_NOISE_MAGNITUDE;

		// Token: 0x0400042D RID: 1069
		public float ACQUIRE_TARGET_OFFSET_PER_METER;

		// Token: 0x0400042E RID: 1070
		public float ACQUIRE_TARGET_DEPTH_EXTRA_OFFSET_PER_METER;

		// Token: 0x0400042F RID: 1071
		public float ACQUIRE_TARGET_DURATION_BASE;

		// Token: 0x04000430 RID: 1072
		public float ACQUIRE_TARGET_DURATION_PER_METER;

		// Token: 0x04000431 RID: 1073
		public float AIM_BASE_SWAY;

		// Token: 0x04000432 RID: 1074
		public float HALT_LONG_RANGE_ENGAGEMENT_RANGE;

		// Token: 0x04000433 RID: 1075
		public float AIM_SWING_PARACHUTING;

		// Token: 0x04000434 RID: 1076
		public float VISIBILITY_MULTIPLIER;

		// Token: 0x04000435 RID: 1077
		public float FOCUS_DISTANCE_GAIN_INFANTRY;

		// Token: 0x04000436 RID: 1078
		public float FOCUS_DISTANCE_GAIN_VEHICLES;

		// Token: 0x04000437 RID: 1079
		public float FOCUS_DISTANCE_GAIN_HUGE_VEHICLES;

		// Token: 0x04000438 RID: 1080
		public float AI_FIRE_RECTANGLE_BOUND;

		// Token: 0x04000439 RID: 1081
		public float TAKING_FIRE_REACTION_TIME;

		// Token: 0x0400043A RID: 1082
		public float MISSILE_LAUNCH_COUNTERMEASURE_TIME_MIN;

		// Token: 0x0400043B RID: 1083
		public float MISSILE_LAUNCH_COUNTERMEASURE_TIME_MAX;

		// Token: 0x0400043C RID: 1084
		public float RECOIL_MULTIPLIER;

		// Token: 0x0400043D RID: 1085
		public float LEAVE_BURNING_VEHICLE_TIME_MULTIPLIER;

		// Token: 0x0400043E RID: 1086
		public float ALERT_SQUAD_TIME;

		// Token: 0x0400043F RID: 1087
		public int SKILL_NORMAL_MOD;

		// Token: 0x04000440 RID: 1088
		public int SKILL_VETERAN_MOD;
	}

	// Token: 0x02000080 RID: 128
	private enum FastMoverDefensiveManeuver
	{
		// Token: 0x04000442 RID: 1090
		Break,
		// Token: 0x04000443 RID: 1091
		GunsDefense,
		// Token: 0x04000444 RID: 1092
		SpiralUp,
		// Token: 0x04000445 RID: 1093
		SpiralDown,
		// Token: 0x04000446 RID: 1094
		Immelmann
	}

	// Token: 0x02000081 RID: 129
	// (Invoke) Token: 0x06000443 RID: 1091
	public delegate bool DelVerifyPath(Path path);

	// Token: 0x02000082 RID: 130
	// (Invoke) Token: 0x06000447 RID: 1095
	public delegate void DelOnDetectedEnemy();

	// Token: 0x02000083 RID: 131
	[Serializable]
	public class Modifier
	{
		// Token: 0x04000447 RID: 1095
		public float meleeChargeRange = 30f;

		// Token: 0x04000448 RID: 1096
		public bool canSprint = true;

		// Token: 0x04000449 RID: 1097
		public bool ignoreFovCheck;

		// Token: 0x0400044A RID: 1098
		public bool dieOnMovementFail;

		// Token: 0x0400044B RID: 1099
		public bool alwaysChargeTarget;

		// Token: 0x0400044C RID: 1100
		public bool showKillMessage = true;

		// Token: 0x0400044D RID: 1101
		public bool canJoinPlayerSquad = true;

		// Token: 0x0400044E RID: 1102
		public float maxDetectionDistance = 1000f;

		// Token: 0x0400044F RID: 1103
		public float vehicleTopSpeedMultiplier = 1f;

		// Token: 0x04000450 RID: 1104
		public float aquireTargetAimOffsetMultiplier = 1f;
	}

	// Token: 0x02000084 RID: 132
	private class LadderPathSection
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00004CC2 File Offset: 0x00002EC2
		public LadderPathSection(Ladder ladder, bool goUp, int enterWaypointIndex)
		{
			this.ladder = ladder;
			this.goUp = goUp;
			this.enterWaypoint = enterWaypointIndex;
		}

		// Token: 0x04000451 RID: 1105
		public Ladder ladder;

		// Token: 0x04000452 RID: 1106
		public bool goUp;

		// Token: 0x04000453 RID: 1107
		public int enterWaypoint;
	}

	// Token: 0x02000085 RID: 133
	public struct FocusDistance
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00004CDF File Offset: 0x00002EDF
		public FocusDistance(float max, float offset, float gainSpeed)
		{
			this.max = max;
			this.offset = offset;
			this.gainSpeed = gainSpeed;
			this.range = 0f;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00004D01 File Offset: 0x00002F01
		public void AutoGain()
		{
			this.range = Mathf.Min(this.range + this.gainSpeed * Time.deltaTime, this.max);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00004D27 File Offset: 0x00002F27
		public void UpdateTargetDistance(float distance)
		{
			this.range = distance + this.offset;
		}

		// Token: 0x04000454 RID: 1108
		private float max;

		// Token: 0x04000455 RID: 1109
		private float offset;

		// Token: 0x04000456 RID: 1110
		private float gainSpeed;

		// Token: 0x04000457 RID: 1111
		public float range;
	}

	// Token: 0x02000086 RID: 134
	public class LoadoutPickStrategy
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00004D37 File Offset: 0x00002F37
		public LoadoutPickStrategy()
		{
			this.type = WeaponManager.WeaponEntry.LoadoutType.Normal;
			this.distance = WeaponManager.WeaponEntry.Distance.Any;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00004D4D File Offset: 0x00002F4D
		public LoadoutPickStrategy(WeaponManager.WeaponEntry.LoadoutType type, WeaponManager.WeaponEntry.Distance distance)
		{
			this.type = type;
			this.distance = distance;
		}

		// Token: 0x04000458 RID: 1112
		public WeaponManager.WeaponEntry.LoadoutType type;

		// Token: 0x04000459 RID: 1113
		public WeaponManager.WeaponEntry.Distance distance;
	}
}

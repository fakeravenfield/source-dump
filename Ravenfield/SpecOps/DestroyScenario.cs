using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A4 RID: 932
	public class DestroyScenario : SpecOpsScenario
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x000A1340 File Offset: 0x0009F540
		public override void Initialize(SpecOpsMode specOps, SpawnPoint spawn)
		{
			base.Initialize(specOps, spawn);
			if (this.spawn.vehicleSpawners.Count == 0)
			{
				this.objective = ObjectiveUi.CreateObjective("Oops this objective failed", base.SpawnPointObjectivePosition());
				this.objective.SetCompleted();
				return;
			}
			VehicleSpawner targetSpawner = this.GetTargetSpawner(spawn);
			this.targetVehicle = targetSpawner.SpawnVehicle();
			if (this.targetVehicle == null)
			{
				throw new Exception("Could not spawn vehicle for DestroyScenario");
			}
			this.targetVehicle.isLocked = true;
			this.targetVehicle.squadOrderPoint.targetText = string.Format("PLANT EXPLOSIVE ({0}s)", 30f);
			this.targetVehicle.squadOrderPoint.type = SquadOrderPoint.ObjectiveType.Custom;
			this.targetVehicle.squadOrderPoint.OnIssue = new SquadOrderPoint.OnIssueDel(this.OnIssuePlantExplosiveOrder);
			this.objective = ObjectiveUi.CreateObjective("Destroy " + this.targetVehicle.name, this.targetVehicle.targetLockPoint);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000A1440 File Offset: 0x0009F640
		private bool CanIssuePlantExplosiveOrder()
		{
			if (this.targetVehicle.dead || !this.specOps.C4IsReady())
			{
				this.specOps.dialog.OnC4OrderCannotComply();
				return false;
			}
			return (!(this.destroyOrderAi != null) || this.destroyOrderAi.actor.dead) && FpsActorController.instance.playerSquad.aiMembers.Count > 0;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000A14B4 File Offset: 0x0009F6B4
		private void OnIssuePlantExplosiveOrder()
		{
			if (!this.CanIssuePlantExplosiveOrder())
			{
				return;
			}
			Vector3 vehiclePosition = this.targetVehicle.transform.position;
			if (this.targetVehicle.rigidbody != null)
			{
				vehiclePosition = this.targetVehicle.GetWorldCenterOfMass();
			}
			List<AiActorController> list = new List<AiActorController>(FpsActorController.instance.playerSquad.aiMembers);
			list.Sort((AiActorController a, AiActorController b) => Vector3.Distance(a.actor.Position(), vehiclePosition).CompareTo(Vector3.Distance(b.actor.Position(), vehiclePosition)));
			this.destroyOrderAi = list[0];
			Vector3 direction = (vehiclePosition - this.destroyOrderAi.actor.Position()).ToGround();
			this.destroyOrderGotoPosition = vehiclePosition - direction.normalized * this.targetVehicle.GetAvoidanceCoarseRadius();
			this.destroyOrderPlantPoint = vehiclePosition;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(this.destroyOrderGotoPosition, direction), out raycastHit, 10000f, 4096))
			{
				this.destroyOrderPlantPoint = raycastHit.point;
				this.destroyOrderGotoPosition = raycastHit.point - direction.normalized * 2f;
			}
			this.destroyOrderAi.OverrideDefaultMovement();
			this.destroyOrderAi.Goto(this.destroyOrderGotoPosition, true);
			this.getObjectivePathAction.Start();
			this.destroyOrderHasPlanted = false;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000A1614 File Offset: 0x0009F814
		public override void Update()
		{
			base.Update();
			if (this.destroyOrderAi != null)
			{
				if (this.destroyOrderAi.actor.dead)
				{
					this.destroyOrderAi = null;
					return;
				}
				if (!this.destroyOrderAi.HasPath() && this.getObjectivePathAction.TrueDone())
				{
					if (!this.destroyOrderHasPlanted)
					{
						this.SpawnC4();
						this.destroyOrderHasPlanted = true;
						this.waitAfterPlantingAction.Start();
					}
					if (this.waitAfterPlantingAction.TrueDone())
					{
						this.destroyOrderAi.ReleaseDefaultMovementOverride();
						this.destroyOrderAi = null;
					}
				}
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000A16A8 File Offset: 0x0009F8A8
		private void SpawnC4()
		{
			Vector3 vector = this.destroyOrderAi.actor.CenterPosition();
			Vector3 forward = this.destroyOrderPlantPoint - vector;
			RemoteDetonatedProjectile remoteDetonatedProjectile = this.specOps.SpawnC4(vector, Quaternion.LookRotation(forward), this.targetVehicle, this.destroyOrderAi.actor);
			float trajectoryHeight = Mathf.Max(vector.y, this.destroyOrderPlantPoint.y) + 1f - vector.y;
			Vector3 velocity = Mortar.CalculateProjectileVelocity(vector, this.destroyOrderPlantPoint, trajectoryHeight, 1f, 20f, remoteDetonatedProjectile.configuration.gravityMultiplier * Physics.gravity.y);
			remoteDetonatedProjectile.velocity = velocity;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000A1754 File Offset: 0x0009F954
		public VehicleSpawner GetTargetSpawner(SpawnPoint spawn)
		{
			foreach (VehicleSpawner vehicleSpawner in spawn.vehicleSpawners)
			{
				if (DestroyScenario.IsHighValueType(vehicleSpawner.typeToSpawn))
				{
					return vehicleSpawner;
				}
			}
			return spawn.vehicleSpawners[0];
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000122BB File Offset: 0x000104BB
		public override bool IsCompletedTest()
		{
			return this.targetVehicle.dead;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000122C8 File Offset: 0x000104C8
		public override bool DetectionTest()
		{
			return this.targetVehicle.health < this.targetVehicle.maxHealth * 0.9f;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000122E8 File Offset: 0x000104E8
		public static bool IsHighValueType(VehicleSpawner.VehicleSpawnType type)
		{
			return type == VehicleSpawner.VehicleSpawnType.Apc || type == VehicleSpawner.VehicleSpawnType.AttackBoat || type == VehicleSpawner.VehicleSpawnType.AttackHelicopter || type == VehicleSpawner.VehicleSpawnType.BomberPlane || type == VehicleSpawner.VehicleSpawnType.Tank || type == VehicleSpawner.VehicleSpawnType.TransportHelicopter;
		}

		// Token: 0x04001939 RID: 6457
		private const int ORDER_RAY_MASK = 4096;

		// Token: 0x0400193A RID: 6458
		private Vehicle targetVehicle;

		// Token: 0x0400193B RID: 6459
		private AiActorController destroyOrderAi;

		// Token: 0x0400193C RID: 6460
		private Vector3 destroyOrderPlantPoint;

		// Token: 0x0400193D RID: 6461
		private Vector3 destroyOrderGotoPosition;

		// Token: 0x0400193E RID: 6462
		private bool destroyOrderHasPlanted;

		// Token: 0x0400193F RID: 6463
		private TimedAction getObjectivePathAction = new TimedAction(1f, false);

		// Token: 0x04001940 RID: 6464
		private TimedAction waitAfterPlantingAction = new TimedAction(0.5f, false);
	}
}

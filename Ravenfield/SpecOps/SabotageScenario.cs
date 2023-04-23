using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A7 RID: 935
	public class SabotageScenario : SpecOpsScenario
	{
		// Token: 0x06001727 RID: 5927 RVA: 0x000A21A8 File Offset: 0x000A03A8
		public override void Initialize(SpecOpsMode specOps, SpawnPoint spawn)
		{
			base.Initialize(specOps, spawn);
			this.targets = new List<Destructible>();
			this.destroyedTargets = 0;
			this.RemoveExistingResupplyCrate();
			this.PlaceTargets(specOps.sabotagePrefab);
			this.objective = ObjectiveUi.CreateObjective(this.GetObjectiveText(), base.SpawnPointObjectivePosition());
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000A21F8 File Offset: 0x000A03F8
		private void RemoveExistingResupplyCrate()
		{
			foreach (ResupplyCrate resupplyCrate in UnityEngine.Object.FindObjectsOfType<ResupplyCrate>())
			{
				if (Vector3.Distance(resupplyCrate.transform.position, this.spawn.transform.position) < 200f)
				{
					UnityEngine.Object.Destroy(resupplyCrate.gameObject);
				}
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000A2250 File Offset: 0x000A0450
		private void PlaceTargets(GameObject prefab)
		{
			int num = 0;
			int num2 = 0;
			while (num < 5 && num2 < 100)
			{
				num2++;
				Vector3 b = UnityEngine.Random.insideUnitSphere * 20f;
				b.y *= 0.1f;
				GraphNode graphNode = PathfindingManager.instance.FindClosestNode(this.spawn.transform.position + b, PathfindingBox.Type.Infantry, -1);
				RaycastHit raycastHit;
				if (graphNode.Tag != 3U && Physics.Raycast(new Ray(graphNode.RandomPointOnSurface() + Vector3.up * 10f, Vector3.down), out raycastHit, 20f, 2232321))
				{
					num++;
					Vector3 point = raycastHit.point;
					Quaternion rotation = SMath.LookRotationRespectUp(UnityEngine.Random.insideUnitSphere, raycastHit.normal);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, point, rotation);
					this.targets.Add(gameObject.GetComponentInChildren<Destructible>());
				}
			}
			this.targetsToDestroy = this.targets.Count - 2;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000A234C File Offset: 0x000A054C
		public override void Update()
		{
			base.Update();
			int num = this.targets.RemoveAll((Destructible t) => t.isDead);
			if (num > 0)
			{
				this.destroyedTargets += num;
				this.objective.SetText(this.GetObjectiveText());
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0001233F File Offset: 0x0001053F
		private string GetObjectiveText()
		{
			return string.Format("Destroy Ammo Crates {0}/{1}", this.destroyedTargets, this.targetsToDestroy);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000A23B0 File Offset: 0x000A05B0
		public VehicleSpawner GetTargetSpawner(SpawnPoint spawn)
		{
			foreach (VehicleSpawner vehicleSpawner in spawn.vehicleSpawners)
			{
				if (this.IsHighValueType(vehicleSpawner.typeToSpawn))
				{
					return vehicleSpawner;
				}
			}
			return spawn.vehicleSpawners[0];
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00012361 File Offset: 0x00010561
		public override bool IsCompletedTest()
		{
			return this.destroyedTargets >= this.targetsToDestroy;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00012374 File Offset: 0x00010574
		public override bool DetectionTest()
		{
			return this.destroyedTargets > 0;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0001237F File Offset: 0x0001057F
		public bool IsHighValueType(VehicleSpawner.VehicleSpawnType type)
		{
			return type == VehicleSpawner.VehicleSpawnType.Apc || type == VehicleSpawner.VehicleSpawnType.AttackBoat || type == VehicleSpawner.VehicleSpawnType.AttackHelicopter || type == VehicleSpawner.VehicleSpawnType.BomberPlane || type == VehicleSpawner.VehicleSpawnType.Tank || type == VehicleSpawner.VehicleSpawnType.TransportHelicopter;
		}

		// Token: 0x04001956 RID: 6486
		private const int TARGET_COUNT = 5;

		// Token: 0x04001957 RID: 6487
		private const int TARGETS_TO_CLEAR_REMAINING = 2;

		// Token: 0x04001958 RID: 6488
		private const float REMOVE_EXISTING_CRATES_DISTANCE = 200f;

		// Token: 0x04001959 RID: 6489
		private List<Destructible> targets;

		// Token: 0x0400195A RID: 6490
		private int destroyedTargets;

		// Token: 0x0400195B RID: 6491
		private int targetsToDestroy;
	}
}

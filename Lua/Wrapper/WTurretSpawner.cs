using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200099B RID: 2459
	[Wrapper(typeof(TurretSpawner))]
	public static class WTurretSpawner
	{
		// Token: 0x06003E77 RID: 15991 RVA: 0x0002A37E File Offset: 0x0002857E
		[Getter]
		public static TurretSpawner.TurretSpawnType GetSpawnType(TurretSpawner self)
		{
			return self.typeToSpawn;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0002A386 File Offset: 0x00028586
		[Getter]
		public static Vehicle GetActiveTurret(TurretSpawner self)
		{
			return self.GetActiveTurret();
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0002A38E File Offset: 0x0002858E
		[Doc("Get the turret prefab for the specified team and type.")]
		public static GameObject GetPrefab(WTeam team, TurretSpawner.TurretSpawnType type)
		{
			return TurretSpawner.GetPrefab((int)team, type);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x0012F3A4 File Offset: 0x0012D5A4
		public static Vehicle GetPrefabVehicle(WTeam team, TurretSpawner.TurretSpawnType type)
		{
			try
			{
				return TurretSpawner.GetPrefab((int)team, type).GetComponent<Vehicle>();
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x0002A397 File Offset: 0x00028597
		[Getter]
		public static SpawnPoint GetParentSpawnPoint(TurretSpawner self)
		{
			return self.spawnPoint;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0002A39F File Offset: 0x0002859F
		[Setter]
		public static void SetParentSpawnPoint(TurretSpawner self, SpawnPoint spawn)
		{
			if (self.HasAssignedSpawn())
			{
				self.spawnPoint.turretSpawners.Remove(self);
			}
			self.spawnPoint = spawn;
			spawn.turretSpawners.Add(self);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0002A3CE File Offset: 0x000285CE
		[Doc("Spawns a turret of the specified type")]
		public static Vehicle SpawnTurret(WTeam team, TurretSpawner.TurretSpawnType type, Vector3 position, Quaternion rotation)
		{
			return TurretSpawner.SpawnTurretAt(position, rotation, (int)team, type);
		}
	}
}

using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200099E RID: 2462
	[Wrapper(typeof(VehicleSpawner))]
	public static class WVehicleSpawner
	{
		// Token: 0x06003EA2 RID: 16034 RVA: 0x0002A4F7 File Offset: 0x000286F7
		[Getter]
		public static VehicleSpawner.VehicleSpawnType GetSpawnType(VehicleSpawner self)
		{
			return self.typeToSpawn;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0002A4FF File Offset: 0x000286FF
		[Getter]
		public static SpawnPoint GetParentSpawnPoint(VehicleSpawner self)
		{
			return self.spawnPoint;
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0002A507 File Offset: 0x00028707
		[Setter]
		public static void SetParentSpawnPoint(VehicleSpawner self, SpawnPoint spawn)
		{
			if (self.spawnPoint != null)
			{
				self.spawnPoint.vehicleSpawners.Remove(self);
			}
			self.spawnPoint = spawn;
			spawn.vehicleSpawners.Add(self);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0002A53C File Offset: 0x0002873C
		[Doc("Get the active vehicle prefab of this spawner.[..] Please note that the current value depends on which team owns the spawner's parent spawn point.")]
		public static GameObject GetPrefab(VehicleSpawner self)
		{
			return self.GetPrefab();
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0012F444 File Offset: 0x0012D644
		public static Vehicle GetPrefabVehicle(VehicleSpawner self)
		{
			try
			{
				return self.GetPrefab().GetComponent<Vehicle>();
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0002A544 File Offset: 0x00028744
		[Doc("Get the vehicle prefab for the specified team and type.")]
		public static GameObject GetPrefab(WTeam team, VehicleSpawner.VehicleSpawnType type)
		{
			return VehicleSpawner.GetPrefab((int)team, type);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x0002A54D File Offset: 0x0002874D
		[Getter]
		[Doc("The vehicle that was last spawned by the spawner")]
		public static Vehicle GetLastSpawnedVehicle(VehicleSpawner self)
		{
			return self.lastSpawnedVehicle;
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0002A555 File Offset: 0x00028755
		[Getter]
		[Doc("Has the last spawned vehicle been used?")]
		public static bool GetLastSpawnedVehicleHasBeenUsed(VehicleSpawner self)
		{
			return self.lastSpawnedVehicleHasBeenUsed;
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x0012F478 File Offset: 0x0012D678
		[Doc("Returns true if the spawn is currently blocked.")]
		public static bool SpawnIsBlocked(VehicleSpawner self)
		{
			GameObject prefab = self.GetPrefab();
			return !(prefab == null) && self.SpawnIsBlocked(prefab.GetComponent<Vehicle>());
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x0002A55D File Offset: 0x0002875D
		[Doc("Force spawns the active vehicle type.[..] Does not check if the spawn area is clear or if the previous vehicle is still alive.")]
		public static Vehicle SpawnNow(VehicleSpawner self)
		{
			return self.SpawnVehicle();
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0002A565 File Offset: 0x00028765
		[Doc("Spawns a vehicle of the specified type")]
		public static Vehicle SpawnVehicle(WTeam team, VehicleSpawner.VehicleSpawnType type, Vector3 position, Quaternion rotation)
		{
			return VehicleSpawner.SpawnVehicleAt(position, rotation, (int)team, type);
		}
	}
}

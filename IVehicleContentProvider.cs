using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public interface IVehicleContentProvider
{
	// Token: 0x06000EE3 RID: 3811
	GameObject GetVehiclePrefab(VehicleSpawner.VehicleSpawnType spawnType);

	// Token: 0x06000EE4 RID: 3812
	GameObject GetVehiclePrefab(TurretSpawner.TurretSpawnType spawnType);

	// Token: 0x06000EE5 RID: 3813
	string GetName();

	// Token: 0x06000EE6 RID: 3814
	VehicleContentMod GetSourceContentMod();

	// Token: 0x06000EE7 RID: 3815
	ModInformation GetSourceMod();
}

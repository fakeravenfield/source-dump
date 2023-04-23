using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class TeamInfo
{
	// Token: 0x06000600 RID: 1536 RVA: 0x0005E564 File Offset: 0x0005C764
	public static TeamInfo Default()
	{
		TeamInfo teamInfo = new TeamInfo();
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (weaponEntry.isAvailableByDefault)
			{
				teamInfo.AddWeaponEntry(weaponEntry);
			}
		}
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		TurretSpawner.TurretSpawnType[] array2 = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in array)
		{
			teamInfo.SetVehicle(vehicleSpawnType, ActorManager.instance.defaultVehiclePrefabs[(int)vehicleSpawnType]);
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in array2)
		{
			teamInfo.SetTurret(turretSpawnType, ActorManager.instance.defaultTurretPrefabs[(int)turretSpawnType]);
		}
		return teamInfo;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0005E658 File Offset: 0x0005C858
	public TeamInfo()
	{
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		TurretSpawner.TurretSpawnType[] array2 = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (VehicleSpawner.VehicleSpawnType key in array)
		{
			this.vehiclePrefab.Add(key, null);
		}
		foreach (TurretSpawner.TurretSpawnType key2 in array2)
		{
			this.turretPrefab.Add(key2, null);
		}
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00005C5E File Offset: 0x00003E5E
	public IEnumerable<GameObject> AvailableVehiclePrefabs()
	{
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		foreach (VehicleSpawner.VehicleSpawnType key in array)
		{
			GameObject gameObject = this.vehiclePrefab[key];
			if (gameObject != null)
			{
				yield return gameObject;
			}
		}
		VehicleSpawner.VehicleSpawnType[] array2 = null;
		yield break;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00005C6E File Offset: 0x00003E6E
	public IEnumerable<GameObject> AvailableTurretPrefabs()
	{
		TurretSpawner.TurretSpawnType[] array = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (TurretSpawner.TurretSpawnType key in array)
		{
			GameObject gameObject = this.turretPrefab[key];
			if (gameObject != null)
			{
				yield return gameObject;
			}
		}
		TurretSpawner.TurretSpawnType[] array2 = null;
		yield break;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00005C7E File Offset: 0x00003E7E
	public void AddWeaponEntry(WeaponManager.WeaponEntry entry)
	{
		this.availableWeapons.Add(entry);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00005C8D File Offset: 0x00003E8D
	public void RemoveWeaponEntry(WeaponManager.WeaponEntry entry)
	{
		this.availableWeapons.Remove(entry);
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x00005C9C File Offset: 0x00003E9C
	public bool IsWeaponEntryAvailable(WeaponManager.WeaponEntry entry)
	{
		return this.availableWeapons.Contains(entry);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00005CAA File Offset: 0x00003EAA
	public void SetVehicle(VehicleSpawner.VehicleSpawnType type, GameObject prefab)
	{
		this.vehiclePrefab[type] = prefab;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00005CB9 File Offset: 0x00003EB9
	public void SetTurret(TurretSpawner.TurretSpawnType type, GameObject prefab)
	{
		this.turretPrefab[type] = prefab;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0005E6FC File Offset: 0x0005C8FC
	public void LoadOfficial()
	{
		this.AddDefaultWeaponsOfMod(ModInformation.OfficialContent);
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		TurretSpawner.TurretSpawnType[] array2 = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in array)
		{
			this.vehiclePrefab[vehicleSpawnType] = ActorManager.instance.defaultVehiclePrefabs[(int)vehicleSpawnType];
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in array2)
		{
			this.turretPrefab[turretSpawnType] = ActorManager.instance.defaultTurretPrefabs[(int)turretSpawnType];
		}
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0005E79C File Offset: 0x0005C99C
	private void AddDefaultWeaponsOfMod(ModInformation sourceMod)
	{
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (weaponEntry.sourceMod == sourceMod && weaponEntry.isAvailableByDefault)
			{
				this.AddWeaponEntry(weaponEntry);
			}
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0005E804 File Offset: 0x0005CA04
	private void ApplyVehiclesOfMod(ModInformation sourceMod)
	{
		if (!sourceMod.HasVehicleContentMods())
		{
			return;
		}
		VehicleContentMod vehicleContentMod = sourceMod.vehicleContentMods[0];
		if (vehicleContentMod.IsLegacyVersion())
		{
			this.UpdateVehiclePrefabs(vehicleContentMod);
			return;
		}
		this.UpdateVehiclePrefabs(vehicleContentMod.variants[0]);
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0005E848 File Offset: 0x0005CA48
	private void UpdateVehiclePrefabs(IVehicleContentProvider provider)
	{
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		TurretSpawner.TurretSpawnType[] array2 = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in array)
		{
			GameObject gameObject = provider.GetVehiclePrefab(vehicleSpawnType);
			if (gameObject != null)
			{
				this.vehiclePrefab[vehicleSpawnType] = gameObject;
			}
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in array2)
		{
			GameObject gameObject2 = provider.GetVehiclePrefab(turretSpawnType);
			if (gameObject2 != null)
			{
				this.turretPrefab[turretSpawnType] = gameObject2;
			}
			else
			{
				this.turretPrefab[turretSpawnType] = ActorManager.instance.defaultTurretPrefabs[(int)turretSpawnType];
			}
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0005E90C File Offset: 0x0005CB0C
	public void AdditiveLoadSingleMod(ModInformation mod, int team)
	{
		this.AddDefaultWeaponsOfMod(mod);
		this.ApplyVehiclesOfMod(mod);
		if (mod.HasSkinContentMods())
		{
			ActorSkinContentMod actorSkinContentMod = mod.skinContentMods[0];
			int index = Mathf.Max(team, actorSkinContentMod.skins.Count);
			this.skin = actorSkinContentMod.skins[index];
		}
	}

	// Token: 0x040005F2 RID: 1522
	public HashSet<WeaponManager.WeaponEntry> availableWeapons = new HashSet<WeaponManager.WeaponEntry>();

	// Token: 0x040005F3 RID: 1523
	public Dictionary<VehicleSpawner.VehicleSpawnType, GameObject> vehiclePrefab = new Dictionary<VehicleSpawner.VehicleSpawnType, GameObject>();

	// Token: 0x040005F4 RID: 1524
	public Dictionary<TurretSpawner.TurretSpawnType, GameObject> turretPrefab = new Dictionary<TurretSpawner.TurretSpawnType, GameObject>();

	// Token: 0x040005F5 RID: 1525
	public ActorSkin skin;
}

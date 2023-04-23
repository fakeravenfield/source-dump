using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BC RID: 188
[Serializable]
public class SerializedTeamInfo
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x0005DEF0 File Offset: 0x0005C0F0
	public SerializedTeamInfo(TeamInfo teamInfo)
	{
		List<SerializedTeamInfo.SerializedWeaponEntry> list = new List<SerializedTeamInfo.SerializedWeaponEntry>();
		List<SerializedTeamInfo.SerializedVehicleSwitch> list2 = new List<SerializedTeamInfo.SerializedVehicleSwitch>();
		List<SerializedTeamInfo.SerializedTurretSwitch> list3 = new List<SerializedTeamInfo.SerializedTurretSwitch>();
		List<WeaponManager.WeaponEntry> list4 = new List<WeaponManager.WeaponEntry>();
		foreach (WeaponManager.WeaponEntry item in teamInfo.availableWeapons)
		{
			list4.Add(item);
		}
		foreach (WeaponManager.WeaponEntry entry in list4)
		{
			SerializedTeamInfo.SerializedWeaponEntry item2 = new SerializedTeamInfo.SerializedWeaponEntry(entry);
			list.Add(item2);
		}
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType)))
		{
			GameObject gameObject = teamInfo.vehiclePrefab[vehicleSpawnType];
			if (gameObject != null)
			{
				SerializedTeamInfo.SerializedVehicleSwitch item3 = new SerializedTeamInfo.SerializedVehicleSwitch(gameObject, vehicleSpawnType);
				list2.Add(item3);
			}
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType)))
		{
			GameObject gameObject2 = teamInfo.turretPrefab[turretSpawnType];
			if (gameObject2 != null)
			{
				SerializedTeamInfo.SerializedTurretSwitch item4 = new SerializedTeamInfo.SerializedTurretSwitch(gameObject2, turretSpawnType);
				list3.Add(item4);
			}
		}
		this.skin = new SerializedTeamInfo.SerializedSkin(teamInfo.skin);
		this.availableWeapons = list.ToArray();
		this.vehicleConfiguration = list2.ToArray();
		this.turretConfiguration = list3.ToArray();
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0005E098 File Offset: 0x0005C298
	public TeamInfo Deserialize()
	{
		TeamInfo teamInfo = new TeamInfo();
		List<WeaponManager.WeaponEntry> allWeapons = WeaponManager.instance.allWeapons;
		VehicleSpawner.VehicleSpawnType[] array = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		TurretSpawner.TurretSpawnType[] array2 = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (SerializedTeamInfo.SerializedWeaponEntry serializedWeaponEntry in this.availableWeapons)
		{
			int hashCode = serializedWeaponEntry.name.GetHashCode();
			WeaponManager.WeaponEntry weaponEntry = null;
			WeaponManager.WeaponEntry weaponEntry2 = null;
			foreach (WeaponManager.WeaponEntry weaponEntry3 in allWeapons)
			{
				if (weaponEntry3.nameHash == hashCode)
				{
					weaponEntry2 = weaponEntry3;
					if (weaponEntry3.sourceMod.isOfficialContent || (weaponEntry3.sourceMod.IsWorkshopItem() && serializedWeaponEntry.sourceWorkshopId == weaponEntry3.sourceMod.workshopItemId.m_PublishedFileId) || weaponEntry3.sourceMod.directoryName == serializedWeaponEntry.sourceDirectory)
					{
						weaponEntry = weaponEntry3;
						break;
					}
				}
			}
			if (weaponEntry != null)
			{
				teamInfo.AddWeaponEntry(weaponEntry);
			}
			else if (weaponEntry2 != null)
			{
				teamInfo.AddWeaponEntry(weaponEntry2);
			}
		}
		foreach (SerializedTeamInfo.SerializedVehicleSwitch serializedVehicleSwitch in this.vehicleConfiguration)
		{
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			if (serializedVehicleSwitch.sourceDirectory == "")
			{
				gameObject = ActorManager.instance.defaultVehiclePrefabs[(int)serializedVehicleSwitch.type];
			}
			else
			{
				foreach (GameObject gameObject3 in ModManager.AllVehiclePrefabs())
				{
					ModInformation vehiclePrefabSourceMod = ModManager.GetVehiclePrefabSourceMod(gameObject3);
					if (gameObject3.name == serializedVehicleSwitch.name)
					{
						gameObject2 = gameObject3;
						if (vehiclePrefabSourceMod != null && (vehiclePrefabSourceMod.workshopItemId.m_PublishedFileId == serializedVehicleSwitch.sourceWorkshopId || vehiclePrefabSourceMod.directoryName == serializedVehicleSwitch.sourceDirectory))
						{
							gameObject = gameObject3;
							break;
						}
					}
				}
				if (gameObject == null)
				{
					gameObject = gameObject2;
				}
			}
			teamInfo.vehiclePrefab[serializedVehicleSwitch.type] = gameObject;
		}
		foreach (SerializedTeamInfo.SerializedTurretSwitch serializedTurretSwitch in this.turretConfiguration)
		{
			GameObject gameObject4 = null;
			GameObject gameObject5 = null;
			if (serializedTurretSwitch.sourceDirectory == "")
			{
				gameObject4 = ActorManager.instance.defaultTurretPrefabs[(int)serializedTurretSwitch.type];
			}
			else
			{
				foreach (GameObject gameObject6 in ModManager.instance.turretPrefabs[serializedTurretSwitch.type])
				{
					ModInformation vehiclePrefabSourceMod2 = ModManager.GetVehiclePrefabSourceMod(gameObject6);
					if (gameObject6.name == serializedTurretSwitch.name)
					{
						gameObject5 = gameObject6;
						if (vehiclePrefabSourceMod2 != null && (vehiclePrefabSourceMod2.workshopItemId.m_PublishedFileId == serializedTurretSwitch.sourceWorkshopId || vehiclePrefabSourceMod2.directoryName == serializedTurretSwitch.sourceDirectory))
						{
							gameObject4 = gameObject6;
							break;
						}
					}
				}
				if (gameObject4 == null)
				{
					gameObject4 = gameObject5;
				}
			}
			teamInfo.turretPrefab[serializedTurretSwitch.type] = gameObject4;
		}
		teamInfo.skin = null;
		foreach (ActorSkin actorSkin in ModManager.instance.actorSkins)
		{
			if (actorSkin != null && actorSkin.name == this.skin.name)
			{
				if (actorSkin.sourceMod.isOfficialContent)
				{
					teamInfo.skin = actorSkin;
					break;
				}
				if (this.skin.sourceWorkshopId == actorSkin.sourceMod.workshopItemId.m_PublishedFileId)
				{
					teamInfo.skin = actorSkin;
					break;
				}
				if (this.skin.sourceDirectory == actorSkin.sourceMod.directoryName)
				{
					teamInfo.skin = actorSkin;
				}
			}
		}
		return teamInfo;
	}

	// Token: 0x040005E0 RID: 1504
	public SerializedTeamInfo.SerializedWeaponEntry[] availableWeapons;

	// Token: 0x040005E1 RID: 1505
	public SerializedTeamInfo.SerializedVehicleSwitch[] vehicleConfiguration;

	// Token: 0x040005E2 RID: 1506
	public SerializedTeamInfo.SerializedTurretSwitch[] turretConfiguration;

	// Token: 0x040005E3 RID: 1507
	public SerializedTeamInfo.SerializedSkin skin;

	// Token: 0x020000BD RID: 189
	[Serializable]
	public class SerializedWeaponEntry
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00005B3E File Offset: 0x00003D3E
		public SerializedWeaponEntry()
		{
			this.sourceDirectory = "";
			this.sourceWorkshopId = 0UL;
			this.name = "";
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00005B64 File Offset: 0x00003D64
		public SerializedWeaponEntry(WeaponManager.WeaponEntry entry)
		{
			this.name = entry.name;
			this.sourceDirectory = entry.sourceMod.directoryName;
			this.sourceWorkshopId = entry.sourceMod.workshopItemId.m_PublishedFileId;
		}

		// Token: 0x040005E4 RID: 1508
		public string sourceDirectory;

		// Token: 0x040005E5 RID: 1509
		public ulong sourceWorkshopId;

		// Token: 0x040005E6 RID: 1510
		public string name;
	}

	// Token: 0x020000BE RID: 190
	[Serializable]
	public class SerializedVehicleSwitch
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00005B9F File Offset: 0x00003D9F
		public SerializedVehicleSwitch()
		{
			this.type = VehicleSpawner.VehicleSpawnType.Jeep;
			this.sourceDirectory = "";
			this.sourceWorkshopId = 0UL;
			this.name = "";
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0005E4C4 File Offset: 0x0005C6C4
		public SerializedVehicleSwitch(GameObject prefab, VehicleSpawner.VehicleSpawnType type)
		{
			ModInformation vehiclePrefabSourceMod = ModManager.GetVehiclePrefabSourceMod(prefab);
			this.name = prefab.name;
			this.type = type;
			if (vehiclePrefabSourceMod != null)
			{
				this.sourceDirectory = vehiclePrefabSourceMod.directoryName;
				this.sourceWorkshopId = vehiclePrefabSourceMod.workshopItemId.m_PublishedFileId;
			}
		}

		// Token: 0x040005E7 RID: 1511
		public VehicleSpawner.VehicleSpawnType type;

		// Token: 0x040005E8 RID: 1512
		public string sourceDirectory;

		// Token: 0x040005E9 RID: 1513
		public ulong sourceWorkshopId;

		// Token: 0x040005EA RID: 1514
		public string name;
	}

	// Token: 0x020000BF RID: 191
	[Serializable]
	public class SerializedTurretSwitch
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00005BCC File Offset: 0x00003DCC
		public SerializedTurretSwitch()
		{
			this.type = TurretSpawner.TurretSpawnType.MachineGun;
			this.sourceDirectory = "";
			this.sourceWorkshopId = 0UL;
			this.name = "";
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0005E514 File Offset: 0x0005C714
		public SerializedTurretSwitch(GameObject prefab, TurretSpawner.TurretSpawnType type)
		{
			ModInformation vehiclePrefabSourceMod = ModManager.GetVehiclePrefabSourceMod(prefab);
			this.name = prefab.name;
			this.type = type;
			if (vehiclePrefabSourceMod != null)
			{
				this.sourceDirectory = vehiclePrefabSourceMod.directoryName;
				this.sourceWorkshopId = vehiclePrefabSourceMod.workshopItemId.m_PublishedFileId;
			}
		}

		// Token: 0x040005EB RID: 1515
		public TurretSpawner.TurretSpawnType type;

		// Token: 0x040005EC RID: 1516
		public string sourceDirectory;

		// Token: 0x040005ED RID: 1517
		public ulong sourceWorkshopId;

		// Token: 0x040005EE RID: 1518
		public string name;
	}

	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class SerializedSkin
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00005BF9 File Offset: 0x00003DF9
		public SerializedSkin()
		{
			this.sourceDirectory = "";
			this.sourceWorkshopId = 0UL;
			this.name = "";
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00005C1F File Offset: 0x00003E1F
		public SerializedSkin(ActorSkin skin)
		{
			if (skin == null)
			{
				return;
			}
			this.name = skin.name;
			this.sourceDirectory = skin.sourceMod.directoryName;
			this.sourceWorkshopId = skin.sourceMod.workshopItemId.m_PublishedFileId;
		}

		// Token: 0x040005EF RID: 1519
		public string sourceDirectory;

		// Token: 0x040005F0 RID: 1520
		public ulong sourceWorkshopId;

		// Token: 0x040005F1 RID: 1521
		public string name;
	}
}

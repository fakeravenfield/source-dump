using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.Tech
{
	// Token: 0x02000416 RID: 1046
	public static class TechManager
	{
		// Token: 0x06001A1C RID: 6684 RVA: 0x00014184 File Offset: 0x00012384
		public static void UnlockEntry(int team, TechTreeEntry entry)
		{
			TechManager.UnlockTechId(team, entry.techId);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00014192 File Offset: 0x00012392
		public static void UnlockTechId(int team, string techId)
		{
			CampaignBase.cachedState.teamTechStatus[team].AddTechId(techId);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000141A6 File Offset: 0x000123A6
		public static bool IsTechIdUnlocked(int team, string techId)
		{
			return CampaignBase.cachedState.teamTechStatus[team].HasTechId(techId);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000141BA File Offset: 0x000123BA
		public static bool IsEntryUnlocked(int team, TechTreeEntry entry)
		{
			return TechManager.IsTechIdUnlocked(team, entry.techId);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000AC1B0 File Offset: 0x000AA3B0
		public static bool IsTechTreeCompleted(int team, TechTree tree)
		{
			foreach (TechTreeEntry entry in tree.entries)
			{
				if (!TechManager.IsEntryUnlocked(team, entry))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000AC20C File Offset: 0x000AA40C
		public static TeamInfo GenerateTeamInfo(int team, CampaignTeamPrefabs teamPrefabs)
		{
			TeamInfo teamInfo = new TeamInfo();
			Dictionary<VehicleSpawner.VehicleSpawnType, int> dictionary = new Dictionary<VehicleSpawner.VehicleSpawnType, int>();
			Dictionary<TurretSpawner.TurretSpawnType, int> dictionary2 = new Dictionary<TurretSpawner.TurretSpawnType, int>();
			foreach (CampaignTeamPrefabs.WeaponGroup weaponGroup in teamPrefabs.weaponGroups)
			{
				if (TechManager.IsTechIdUnlocked(team, weaponGroup.techId))
				{
					string[] weaponEntryNames = weaponGroup.weaponEntryNames;
					for (int j = 0; j < weaponEntryNames.Length; j++)
					{
						WeaponManager.WeaponEntry weaponEntryByName = WeaponManager.GetWeaponEntryByName(weaponEntryNames[j], null);
						if (weaponEntryByName != null)
						{
							teamInfo.AddWeaponEntry(weaponEntryByName);
						}
					}
				}
			}
			foreach (CampaignTeamPrefabs.VehicleGroup vehicleGroup in teamPrefabs.vehicleGroups)
			{
				if (TechManager.IsTechIdUnlocked(team, vehicleGroup.techId))
				{
					foreach (CampaignTeamPrefabs.VehicleGroup.VehicleEntry vehicleEntry in vehicleGroup.entries)
					{
						bool flag = true;
						if (!dictionary.ContainsKey(vehicleEntry.slot))
						{
							dictionary.Add(vehicleEntry.slot, vehicleEntry.priority);
						}
						else
						{
							flag = (vehicleEntry.priority > dictionary[vehicleEntry.slot]);
						}
						if (flag)
						{
							GameObject prefab = ActorManager.instance.defaultVehiclePrefabs[(int)vehicleEntry.slot];
							if (!string.IsNullOrEmpty(vehicleEntry.vehicleName))
							{
								prefab = ModManager.GetVehiclePrefabByName(vehicleEntry.vehicleName, null);
							}
							teamInfo.SetVehicle(vehicleEntry.slot, prefab);
							dictionary[vehicleEntry.slot] = vehicleEntry.priority;
						}
					}
				}
			}
			foreach (CampaignTeamPrefabs.TurretGroup turretGroup in teamPrefabs.turretGroups)
			{
				if (TechManager.IsTechIdUnlocked(team, turretGroup.techId))
				{
					foreach (CampaignTeamPrefabs.TurretGroup.TurretEntry turretEntry in turretGroup.entries)
					{
						bool flag2 = true;
						if (!dictionary2.ContainsKey(turretEntry.slot))
						{
							dictionary2.Add(turretEntry.slot, turretEntry.priority);
						}
						else
						{
							flag2 = (turretEntry.priority > dictionary2[turretEntry.slot]);
						}
						if (flag2)
						{
							GameObject prefab2 = ActorManager.instance.defaultTurretPrefabs[(int)turretEntry.slot];
							if (!string.IsNullOrEmpty(turretEntry.turretName))
							{
								prefab2 = ModManager.GetVehiclePrefabByName(turretEntry.turretName, null);
							}
							teamInfo.SetTurret(turretEntry.slot, prefab2);
							dictionary2[turretEntry.slot] = turretEntry.priority;
						}
					}
				}
			}
			return teamInfo;
		}
	}
}

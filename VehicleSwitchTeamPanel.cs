using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200020E RID: 526
public class VehicleSwitchTeamPanel : MonoBehaviour
{
	// Token: 0x06000DEF RID: 3567 RVA: 0x0007DAF0 File Offset: 0x0007BCF0
	public void Initialize(int team)
	{
		this.team = team;
		this.entries = base.GetComponentsInChildren<VehicleSwitchEntry>(true);
		this.presetDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnPresetValueChanged));
		VehicleSwitchEntry[] array = this.entries;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].team = team;
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0007DB4C File Offset: 0x0007BD4C
	public void OnPresetValueChanged(int index)
	{
		if (this.ignorePresetValueChange)
		{
			return;
		}
		if (index > 1)
		{
			int index2 = index - 2;
			IVehicleContentProvider provider = ModManager.instance.vehicleContentProviders[index2];
			this.ApplyContentProvider(provider);
			return;
		}
		if (index == 1)
		{
			this.ApplyNoneVehicles();
			return;
		}
		this.ApplyDefaultVehicles();
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0000B3A8 File Offset: 0x000095A8
	public void SetPresets(List<string> presetOptions)
	{
		this.presetDropdown.ClearOptions();
		this.presetDropdown.AddOptions(presetOptions);
		this.ignorePresetValueChange = true;
		this.presetDropdown.value = 0;
		this.ignorePresetValueChange = false;
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0007DB94 File Offset: 0x0007BD94
	public void UpdateEntries(TeamInfo teamInfo)
	{
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			this.GetEntry(new VehicleCompoundType(vehicleSpawnType)).SetPrefab(teamInfo.vehiclePrefab[vehicleSpawnType]);
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
		{
			this.GetEntry(new VehicleCompoundType(turretSpawnType)).SetPrefab(teamInfo.turretPrefab[turretSpawnType]);
		}
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0000B3DB File Offset: 0x000095DB
	public void UpdateEntry(VehicleCompoundType type, GameObject prefab)
	{
		this.GetEntry(type).SetPrefab(prefab);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0007DC10 File Offset: 0x0007BE10
	public VehicleSwitchEntry GetEntry(VehicleCompoundType type)
	{
		return this.entries.First((VehicleSwitchEntry e) => e.type == type);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0007DC44 File Offset: 0x0007BE44
	private void ApplyContentProvider(IVehicleContentProvider provider)
	{
		TeamInfo teamInfo = GameManager.instance.gameInfo.team[this.team];
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			teamInfo.SetVehicle(vehicleSpawnType, provider.GetVehiclePrefab(vehicleSpawnType));
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
		{
			teamInfo.SetTurret(turretSpawnType, provider.GetVehiclePrefab(turretSpawnType));
		}
		this.UpdateEntries(teamInfo);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0007DCC4 File Offset: 0x0007BEC4
	private void ApplyDefaultVehicles()
	{
		TeamInfo teamInfo = GameManager.instance.gameInfo.team[this.team];
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			teamInfo.SetVehicle(vehicleSpawnType, ActorManager.instance.defaultVehiclePrefabs[(int)vehicleSpawnType]);
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
		{
			teamInfo.SetTurret(turretSpawnType, ActorManager.instance.defaultTurretPrefabs[(int)turretSpawnType]);
		}
		this.UpdateEntries(teamInfo);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0007DD4C File Offset: 0x0007BF4C
	private void ApplyNoneVehicles()
	{
		TeamInfo teamInfo = GameManager.instance.gameInfo.team[this.team];
		foreach (VehicleSpawner.VehicleSpawnType type in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			teamInfo.SetVehicle(type, null);
		}
		foreach (TurretSpawner.TurretSpawnType type2 in TurretSpawner.ALL_TURRET_TYPES)
		{
			teamInfo.SetTurret(type2, null);
		}
		this.UpdateEntries(teamInfo);
	}

	// Token: 0x04000EEF RID: 3823
	[NonSerialized]
	public int team;

	// Token: 0x04000EF0 RID: 3824
	public Dropdown presetDropdown;

	// Token: 0x04000EF1 RID: 3825
	private VehicleSwitchEntry[] entries;

	// Token: 0x04000EF2 RID: 3826
	private bool ignorePresetValueChange = true;
}

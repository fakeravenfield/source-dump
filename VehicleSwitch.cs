using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class VehicleSwitch : MonoBehaviour
{
	// Token: 0x06000DDE RID: 3550 RVA: 0x0007D6C0 File Offset: 0x0007B8C0
	private void Awake()
	{
		VehicleSwitch.instance = this;
		for (int i = 0; i < this.panels.Length; i++)
		{
			this.panels[i].Initialize(i);
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0007D6F4 File Offset: 0x0007B8F4
	private void OnEnable()
	{
		if (VehicleSwitch.requireReload)
		{
			this.ReloadVehiclePrefabData();
		}
		for (int i = 0; i < this.panels.Length; i++)
		{
			this.panels[i].UpdateEntries(GameManager.instance.gameInfo.team[i]);
		}
		VehicleSwitch.requireReload = false;
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0007D748 File Offset: 0x0007B948
	private void ReloadVehiclePrefabData()
	{
		List<string> presets = this.GenerateVehiclePresets();
		for (int i = 0; i < this.panels.Length; i++)
		{
			this.panels[i].SetPresets(presets);
		}
		this.PopulatePickerUI();
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0007D784 File Offset: 0x0007B984
	private void PopulatePickerUI()
	{
		this.picker.Clear();
		List<VehiclePicker.VehicleEntryElement> list = new List<VehiclePicker.VehicleEntryElement>();
		foreach (VehicleSpawner.VehicleSpawnType vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			VehicleCompoundType type = new VehicleCompoundType(vehicleSpawnType);
			list.Add(new VehiclePicker.VehicleEntryElement
			{
				prefab = null,
				type = type
			});
			list.Add(new VehiclePicker.VehicleEntryElement
			{
				prefab = ActorManager.instance.defaultVehiclePrefabs[(int)vehicleSpawnType],
				type = type
			});
			foreach (GameObject prefab in ModManager.instance.vehiclePrefabs[vehicleSpawnType])
			{
				list.Add(new VehiclePicker.VehicleEntryElement
				{
					prefab = prefab,
					type = type
				});
			}
		}
		foreach (TurretSpawner.TurretSpawnType turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
		{
			VehicleCompoundType type2 = new VehicleCompoundType(turretSpawnType);
			list.Add(new VehiclePicker.VehicleEntryElement
			{
				prefab = null,
				type = type2
			});
			list.Add(new VehiclePicker.VehicleEntryElement
			{
				prefab = ActorManager.instance.defaultTurretPrefabs[(int)turretSpawnType],
				type = type2
			});
			foreach (GameObject prefab2 in ModManager.instance.turretPrefabs[turretSpawnType])
			{
				list.Add(new VehiclePicker.VehicleEntryElement
				{
					prefab = prefab2,
					type = type2
				});
			}
		}
		this.picker.Populate(list);
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0007D974 File Offset: 0x0007BB74
	private List<string> GenerateVehiclePresets()
	{
		List<string> list = (from p in ModManager.instance.vehicleContentProviders
		select p.GetName()).ToList<string>();
		list.Insert(0, "Official Vehicles");
		list.Insert(1, "None");
		return list;
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0000B318 File Offset: 0x00009518
	public static void OpenPicker(VehicleCompoundType type, int team)
	{
		VehicleSwitch.instance.pickingType = type;
		VehicleSwitch.instance.pickingTeam = team;
		VehicleSwitch.instance.picker.Open(type);
		MainMenu.instance.OpenPageIndex(15);
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0007D9CC File Offset: 0x0007BBCC
	public static void OnPick(GameObject prefab)
	{
		TeamInfo teamInfo = GameManager.instance.gameInfo.team[VehicleSwitch.instance.pickingTeam];
		if (VehicleSwitch.instance.pickingType.isTurret)
		{
			teamInfo.SetTurret(VehicleSwitch.instance.pickingType.turretType, prefab);
		}
		else
		{
			teamInfo.SetVehicle(VehicleSwitch.instance.pickingType.vehicleType, prefab);
		}
		VehicleSwitch.instance.panels[VehicleSwitch.instance.pickingTeam].UpdateEntry(VehicleSwitch.instance.pickingType, prefab);
		MainMenu.instance.OpenPageIndex(9);
	}

	// Token: 0x04000EE3 RID: 3811
	public static bool requireReload = true;

	// Token: 0x04000EE4 RID: 3812
	public static VehicleSwitch instance;

	// Token: 0x04000EE5 RID: 3813
	public VehiclePicker picker;

	// Token: 0x04000EE6 RID: 3814
	public VehicleSwitchTeamPanel[] panels;

	// Token: 0x04000EE7 RID: 3815
	private int pickingTeam;

	// Token: 0x04000EE8 RID: 3816
	private VehicleCompoundType pickingType;
}

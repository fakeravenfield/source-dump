using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class VehicleContentMod : MonoBehaviour, IVehicleContentProvider
{
	// Token: 0x06000EE8 RID: 3816 RVA: 0x0000BE0F File Offset: 0x0000A00F
	public IEnumerable<IVehicleContentProvider> AllContentProviders()
	{
		if (this.IsLegacyVersion())
		{
			yield return this;
		}
		else
		{
			int num;
			for (int i = 0; i < this.variants.Length; i = num + 1)
			{
				yield return this.variants[i];
				num = i;
			}
		}
		yield break;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0000BE1F File Offset: 0x0000A01F
	public bool IsLegacyVersion()
	{
		return this.variants == null || this.variants.Length == 0;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00081E10 File Offset: 0x00080010
	public GameObject GetVehiclePrefab(VehicleSpawner.VehicleSpawnType spawnType)
	{
		switch (spawnType)
		{
		case VehicleSpawner.VehicleSpawnType.Jeep:
			return this.jeep;
		case VehicleSpawner.VehicleSpawnType.JeepMachineGun:
			return this.jeepMachineGun;
		case VehicleSpawner.VehicleSpawnType.Quad:
			return this.quad;
		case VehicleSpawner.VehicleSpawnType.Tank:
			return this.tank;
		case VehicleSpawner.VehicleSpawnType.AttackHelicopter:
			return this.attackHelicopter;
		case VehicleSpawner.VehicleSpawnType.AttackPlane:
			return this.attackPlane;
		case VehicleSpawner.VehicleSpawnType.Rhib:
			return this.rhib;
		case VehicleSpawner.VehicleSpawnType.AttackBoat:
			return this.attackBoat;
		case VehicleSpawner.VehicleSpawnType.BomberPlane:
			return this.bombPlane;
		case VehicleSpawner.VehicleSpawnType.TransportHelicopter:
			return this.transportHelicopter;
		case VehicleSpawner.VehicleSpawnType.Apc:
			return this.apc;
		default:
			return null;
		}
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0000BE35 File Offset: 0x0000A035
	public GameObject GetVehiclePrefab(TurretSpawner.TurretSpawnType spawnType)
	{
		switch (spawnType)
		{
		case TurretSpawner.TurretSpawnType.MachineGun:
			return this.turretMachineGun;
		case TurretSpawner.TurretSpawnType.AntiTank:
			return this.turretAntiTank;
		case TurretSpawner.TurretSpawnType.AntiAir:
			return this.turretAntiAir;
		default:
			return null;
		}
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0000BE61 File Offset: 0x0000A061
	public string GetName()
	{
		if (string.IsNullOrEmpty(this.name))
		{
			return this.sourceMod.title;
		}
		return this.name;
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x0000BE82 File Offset: 0x0000A082
	public ModInformation GetSourceMod()
	{
		return this.sourceMod;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00008D0C File Offset: 0x00006F0C
	public VehicleContentMod GetSourceContentMod()
	{
		return this;
	}

	// Token: 0x04000FAC RID: 4012
	[NonSerialized]
	public ModInformation sourceMod;

	// Token: 0x04000FAD RID: 4013
	public new string name = "";

	// Token: 0x04000FAE RID: 4014
	public VehicleContentMod.Variant[] variants = new VehicleContentMod.Variant[0];

	// Token: 0x04000FAF RID: 4015
	[Header("Legacy")]
	public GameObject jeep;

	// Token: 0x04000FB0 RID: 4016
	public GameObject jeepMachineGun;

	// Token: 0x04000FB1 RID: 4017
	public GameObject quad;

	// Token: 0x04000FB2 RID: 4018
	public GameObject tank;

	// Token: 0x04000FB3 RID: 4019
	public GameObject apc;

	// Token: 0x04000FB4 RID: 4020
	public GameObject attackBoat;

	// Token: 0x04000FB5 RID: 4021
	public GameObject rhib;

	// Token: 0x04000FB6 RID: 4022
	public GameObject attackHelicopter;

	// Token: 0x04000FB7 RID: 4023
	public GameObject transportHelicopter;

	// Token: 0x04000FB8 RID: 4024
	public GameObject attackPlane;

	// Token: 0x04000FB9 RID: 4025
	public GameObject bombPlane;

	// Token: 0x04000FBA RID: 4026
	public GameObject turretMachineGun;

	// Token: 0x04000FBB RID: 4027
	public GameObject turretAntiTank;

	// Token: 0x04000FBC RID: 4028
	public GameObject turretAntiAir;

	// Token: 0x0200022F RID: 559
	[Serializable]
	public class Variant : IVehicleContentProvider
	{
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00081EA0 File Offset: 0x000800A0
		public GameObject GetVehiclePrefab(VehicleSpawner.VehicleSpawnType spawnType)
		{
			switch (spawnType)
			{
			case VehicleSpawner.VehicleSpawnType.Jeep:
				return this.jeep;
			case VehicleSpawner.VehicleSpawnType.JeepMachineGun:
				return this.jeepMachineGun;
			case VehicleSpawner.VehicleSpawnType.Quad:
				return this.quad;
			case VehicleSpawner.VehicleSpawnType.Tank:
				return this.tank;
			case VehicleSpawner.VehicleSpawnType.AttackHelicopter:
				return this.attackHelicopter;
			case VehicleSpawner.VehicleSpawnType.AttackPlane:
				return this.attackPlane;
			case VehicleSpawner.VehicleSpawnType.Rhib:
				return this.rhib;
			case VehicleSpawner.VehicleSpawnType.AttackBoat:
				return this.attackBoat;
			case VehicleSpawner.VehicleSpawnType.BomberPlane:
				return this.bombPlane;
			case VehicleSpawner.VehicleSpawnType.TransportHelicopter:
				return this.transportHelicopter;
			case VehicleSpawner.VehicleSpawnType.Apc:
				return this.apc;
			default:
				return null;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0000BEA9 File Offset: 0x0000A0A9
		public GameObject GetVehiclePrefab(TurretSpawner.TurretSpawnType spawnType)
		{
			switch (spawnType)
			{
			case TurretSpawner.TurretSpawnType.MachineGun:
				return this.turretMachineGun;
			case TurretSpawner.TurretSpawnType.AntiTank:
				return this.turretAntiTank;
			case TurretSpawner.TurretSpawnType.AntiAir:
				return this.turretAntiAir;
			default:
				return null;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0000BED5 File Offset: 0x0000A0D5
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		public ModInformation GetSourceMod()
		{
			return this.sourceContentMod.sourceMod;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0000BEEA File Offset: 0x0000A0EA
		public VehicleContentMod GetSourceContentMod()
		{
			return this.sourceContentMod;
		}

		// Token: 0x04000FBD RID: 4029
		[NonSerialized]
		public VehicleContentMod sourceContentMod;

		// Token: 0x04000FBE RID: 4030
		public string name;

		// Token: 0x04000FBF RID: 4031
		public GameObject jeep;

		// Token: 0x04000FC0 RID: 4032
		public GameObject jeepMachineGun;

		// Token: 0x04000FC1 RID: 4033
		public GameObject quad;

		// Token: 0x04000FC2 RID: 4034
		public GameObject tank;

		// Token: 0x04000FC3 RID: 4035
		public GameObject apc;

		// Token: 0x04000FC4 RID: 4036
		public GameObject attackBoat;

		// Token: 0x04000FC5 RID: 4037
		public GameObject rhib;

		// Token: 0x04000FC6 RID: 4038
		public GameObject attackHelicopter;

		// Token: 0x04000FC7 RID: 4039
		public GameObject transportHelicopter;

		// Token: 0x04000FC8 RID: 4040
		public GameObject attackPlane;

		// Token: 0x04000FC9 RID: 4041
		public GameObject bombPlane;

		// Token: 0x04000FCA RID: 4042
		public GameObject turretMachineGun;

		// Token: 0x04000FCB RID: 4043
		public GameObject turretAntiTank;

		// Token: 0x04000FCC RID: 4044
		public GameObject turretAntiAir;
	}
}

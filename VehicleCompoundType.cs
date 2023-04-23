using System;

// Token: 0x02000203 RID: 515
[Serializable]
public struct VehicleCompoundType : IEquatable<VehicleCompoundType>
{
	// Token: 0x06000DB7 RID: 3511 RVA: 0x0000B14A File Offset: 0x0000934A
	public VehicleCompoundType(VehicleSpawner.VehicleSpawnType type)
	{
		this.vehicleType = type;
		this.turretType = TurretSpawner.TurretSpawnType.MachineGun;
		this.isTurret = false;
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0000B161 File Offset: 0x00009361
	public VehicleCompoundType(TurretSpawner.TurretSpawnType type)
	{
		this.vehicleType = VehicleSpawner.VehicleSpawnType.Jeep;
		this.turretType = type;
		this.isTurret = true;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0000B178 File Offset: 0x00009378
	public bool Equals(VehicleCompoundType other)
	{
		if (this.isTurret != other.isTurret)
		{
			return false;
		}
		if (!this.isTurret)
		{
			return this.vehicleType == other.vehicleType;
		}
		return this.turretType == other.turretType;
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0000B1AF File Offset: 0x000093AF
	public override bool Equals(object obj)
	{
		return obj is VehicleCompoundType && this.Equals((VehicleCompoundType)obj);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0000B1C7 File Offset: 0x000093C7
	public static bool operator ==(VehicleCompoundType a, VehicleCompoundType b)
	{
		return a.Equals(b);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0000B1D1 File Offset: 0x000093D1
	public static bool operator !=(VehicleCompoundType a, VehicleCompoundType b)
	{
		return !a.Equals(b);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0000B1DE File Offset: 0x000093DE
	public override int GetHashCode()
	{
		if (!this.isTurret)
		{
			return this.vehicleType.GetHashCode() << 2;
		}
		return this.turretType.GetHashCode();
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0000B20D File Offset: 0x0000940D
	public override string ToString()
	{
		if (!this.isTurret)
		{
			return this.vehicleType.ToString();
		}
		return this.turretType.ToString();
	}

	// Token: 0x04000ECE RID: 3790
	public VehicleSpawner.VehicleSpawnType vehicleType;

	// Token: 0x04000ECF RID: 3791
	public TurretSpawner.TurretSpawnType turretType;

	// Token: 0x04000ED0 RID: 3792
	public bool isTurret;
}

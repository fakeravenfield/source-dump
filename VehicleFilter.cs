using System;

// Token: 0x02000350 RID: 848
[Serializable]
public class VehicleFilter
{
	// Token: 0x06001596 RID: 5526 RVA: 0x0009C20C File Offset: 0x0009A40C
	public VehicleFilter()
	{
		this.landcraft = true;
		this.amphibious = true;
		this.watercraft = true;
		this.air = true;
		this.airFastmover = true;
		this.allowOnlyFromFrontlineSpawnUsage = true;
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x0009C274 File Offset: 0x0009A474
	public override string ToString()
	{
		return string.Format("landcraft={0}, ambphibious={1}, watercraft={2}, air={3}, airFastmover={4}, allowOnlyFrontline={5}", new object[]
		{
			this.landcraft,
			this.amphibious,
			this.watercraft,
			this.air,
			this.airFastmover,
			this.allowOnlyFromFrontlineSpawnUsage
		});
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x0009C2E8 File Offset: 0x0009A4E8
	public bool VehiclePassesFilter(Vehicle vehicle)
	{
		if (vehicle == null)
		{
			return true;
		}
		if (vehicle.aiUseStrategy == Vehicle.AiUseStrategy.OnlyFromFrontlineSpawn && !this.allowOnlyFromFrontlineSpawnUsage)
		{
			return false;
		}
		if (vehicle.IsAmphibious())
		{
			return this.amphibious;
		}
		if (vehicle.IsWatercraft())
		{
			return this.watercraft;
		}
		Actor.TargetType targetType = vehicle.targetType;
		if (targetType == Actor.TargetType.Air)
		{
			return this.air;
		}
		if (targetType != Actor.TargetType.AirFastMover)
		{
			return this.landcraft;
		}
		return this.airFastmover;
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0009C358 File Offset: 0x0009A558
	public VehicleFilter Clone()
	{
		return new VehicleFilter
		{
			landcraft = this.landcraft,
			watercraft = this.watercraft,
			air = this.air,
			airFastmover = this.airFastmover,
			allowOnlyFromFrontlineSpawnUsage = this.allowOnlyFromFrontlineSpawnUsage
		};
	}

	// Token: 0x040017D8 RID: 6104
	public static VehicleFilter any = new VehicleFilter();

	// Token: 0x040017D9 RID: 6105
	public bool landcraft = true;

	// Token: 0x040017DA RID: 6106
	public bool amphibious = true;

	// Token: 0x040017DB RID: 6107
	public bool watercraft = true;

	// Token: 0x040017DC RID: 6108
	public bool air = true;

	// Token: 0x040017DD RID: 6109
	public bool airFastmover = true;

	// Token: 0x040017DE RID: 6110
	public bool allowOnlyFromFrontlineSpawnUsage = true;
}

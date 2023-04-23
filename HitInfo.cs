using System;

// Token: 0x020000EC RID: 236
public struct HitInfo
{
	// Token: 0x06000707 RID: 1799 RVA: 0x00006858 File Offset: 0x00004A58
	public HitInfo(Actor actor)
	{
		this.actor = actor;
		this.vehicle = null;
		this.destructible = null;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0000686F File Offset: 0x00004A6F
	public HitInfo(Vehicle vehicle)
	{
		this.vehicle = vehicle;
		this.actor = null;
		this.destructible = null;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00006886 File Offset: 0x00004A86
	public HitInfo(Destructible destructible)
	{
		this.destructible = destructible;
		this.actor = null;
		this.vehicle = null;
	}

	// Token: 0x04000718 RID: 1816
	public Actor actor;

	// Token: 0x04000719 RID: 1817
	public Vehicle vehicle;

	// Token: 0x0400071A RID: 1818
	public Destructible destructible;
}

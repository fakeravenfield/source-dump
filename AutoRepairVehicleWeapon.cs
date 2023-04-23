using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class AutoRepairVehicleWeapon : MountedWeapon
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x00064388 File Offset: 0x00062588
	protected override bool Shoot(Vector3 direction, bool useMuzzleDirection)
	{
		if (base.user.IsSeated())
		{
			base.user.seat.vehicle.Repair(this.repairAmount);
		}
		this.audio.Play();
		this.lastFiredTimestamp = Time.time;
		return true;
	}

	// Token: 0x040007C9 RID: 1993
	public float repairAmount = 100f;
}

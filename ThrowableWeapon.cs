using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class ThrowableWeapon : Weapon
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x00006EF8 File Offset: 0x000050F8
	public override void Unholster()
	{
		base.Unholster();
		this.isPreparingToThrowProjectile = false;
		if (this.ammo == 0)
		{
			base.InstantlyReload();
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00006F15 File Offset: 0x00005115
	public override bool CanFire()
	{
		return !this.isPreparingToThrowProjectile && base.CanFire();
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000642A0 File Offset: 0x000624A0
	public override void Fire(Vector3 direction, bool useMuzzleDirection)
	{
		if (this.CanFire())
		{
			this.lastFiredTimestamp = Time.time;
			if (this.animator != null)
			{
				this.animator.SetTrigger(ThrowableWeapon.THROW_PARAMETER_HASH);
				this.hasFiredSingleRoundThisTrigger = true;
				this.isPreparingToThrowProjectile = true;
			}
			else
			{
				this.Shoot(direction, useMuzzleDirection);
			}
		}
		this.holdingFire = true;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00064300 File Offset: 0x00062500
	public override bool Resupply()
	{
		bool flag = base.HasAnyAmmo();
		bool flag2 = base.Resupply();
		if (flag2)
		{
			base.InstantlyReload();
			if (!flag && this.unholstered)
			{
				this.Unholster();
			}
		}
		return flag2;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00006F27 File Offset: 0x00005127
	public void SpawnThrowable()
	{
		this.Shoot(Vector3.zero, true);
		this.isPreparingToThrowProjectile = false;
		if (!this.holdingFire)
		{
			this.hasFiredSingleRoundThisTrigger = false;
		}
		base.InstantlyReload();
	}

	// Token: 0x040007C3 RID: 1987
	public static readonly int THROW_PARAMETER_HASH = Animator.StringToHash("throw");

	// Token: 0x040007C4 RID: 1988
	private bool isPreparingToThrowProjectile;
}

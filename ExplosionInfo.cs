using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public struct ExplosionInfo
{
	// Token: 0x060006F7 RID: 1783 RVA: 0x00060F48 File Offset: 0x0005F148
	public static ExplosionInfo Create(Vector3 point, Actor sourceActor, Weapon sourceWeapon, Vehicle.ArmorRating damageRating, ExplodingProjectile.ExplosionConfiguration configuration)
	{
		return new ExplosionInfo
		{
			point = point,
			sourceActor = sourceActor,
			sourceWeapon = sourceWeapon,
			configuration = configuration,
			damageRating = damageRating
		};
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000067C7 File Offset: 0x000049C7
	// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000067CF File Offset: 0x000049CF
	public Weapon sourceWeapon
	{
		get
		{
			return this._sourceWeapon;
		}
		set
		{
			this._sourceWeapon = value;
			Weapon sourceWeapon = this._sourceWeapon;
			this.sourceWeaponEntry = ((sourceWeapon != null) ? sourceWeapon.weaponEntry : null);
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060006FA RID: 1786 RVA: 0x000067F0 File Offset: 0x000049F0
	// (set) Token: 0x060006FB RID: 1787 RVA: 0x000067F8 File Offset: 0x000049F8
	public WeaponManager.WeaponEntry sourceWeaponEntry { readonly get; private set; }

	// Token: 0x040006FF RID: 1791
	public Actor sourceActor;

	// Token: 0x04000700 RID: 1792
	public Vector3 point;

	// Token: 0x04000701 RID: 1793
	public ExplodingProjectile.ExplosionConfiguration configuration;

	// Token: 0x04000702 RID: 1794
	public Vehicle.ArmorRating damageRating;

	// Token: 0x04000703 RID: 1795
	private Weapon _sourceWeapon;
}

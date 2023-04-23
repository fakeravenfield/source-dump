using System;
using Lua;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public struct DamageInfo
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060006DA RID: 1754 RVA: 0x00006689 File Offset: 0x00004889
	// (set) Token: 0x060006DB RID: 1755 RVA: 0x00006691 File Offset: 0x00004891
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

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060006DC RID: 1756 RVA: 0x000066B2 File Offset: 0x000048B2
	// (set) Token: 0x060006DD RID: 1757 RVA: 0x000066BA File Offset: 0x000048BA
	public WeaponManager.WeaponEntry sourceWeaponEntry { readonly get; private set; }

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060006DE RID: 1758 RVA: 0x000066C3 File Offset: 0x000048C3
	public bool isScripted
	{
		get
		{
			return this.type == DamageInfo.DamageSourceType.Scripted;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060006DF RID: 1759 RVA: 0x000066CF File Offset: 0x000048CF
	public bool isPlayerSource
	{
		get
		{
			return this.sourceActor != null && !this.sourceActor.aiControlled;
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0006095C File Offset: 0x0005EB5C
	[Ignore]
	public static DamageInfo DefaultDamage(float amount)
	{
		return new DamageInfo(DamageInfo.DamageSourceType.Unknown, null, null)
		{
			healthDamage = amount
		};
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0006097C File Offset: 0x0005EB7C
	public DamageInfo(DamageInfo.DamageSourceType type, Actor sourceActor, Weapon sourceWeapon)
	{
		this.type = type;
		this.healthDamage = 0f;
		this.balanceDamage = 0f;
		this.isSplashDamage = false;
		this.isPiercing = false;
		this.isCriticalHit = false;
		this.point = default(Vector3);
		this.direction = Vector3.up;
		this.impactForce = default(Vector3);
		this.sourceActor = sourceActor;
		this._sourceWeapon = null;
		this.sourceWeaponEntry = null;
		this.sourceWeapon = sourceWeapon;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x000609FC File Offset: 0x0005EBFC
	public DamageInfo(DamageInfo source)
	{
		this.type = source.type;
		this.healthDamage = source.healthDamage;
		this.balanceDamage = source.balanceDamage;
		this.isSplashDamage = source.isSplashDamage;
		this.isPiercing = source.isPiercing;
		this.isCriticalHit = source.isCriticalHit;
		this.point = source.point;
		this.direction = source.direction;
		this.impactForce = source.impactForce;
		this.sourceActor = source.sourceActor;
		this._sourceWeapon = null;
		this.sourceWeaponEntry = null;
		this.sourceWeapon = this.sourceWeapon;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x000066EF File Offset: 0x000048EF
	[Doc("Evaluates the explosion damage at the specified point.[..] Can optionally ignore level geometry which would otherwise block damage through walls, etc.")]
	public static DamageInfo EvaluateLastExplosionDamage(Vector3 point, bool ignoreLevelGeometry)
	{
		return ActorManager.EvaluateLastExplosionDamage(point, ignoreLevelGeometry);
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00060A9C File Offset: 0x0005EC9C
	public override string ToString()
	{
		return string.Format("(DamageInfo({0}): health={1}, balance={2}, isSplash={3}, isCrit={4})", new object[]
		{
			this.type,
			this.healthDamage,
			this.balanceDamage,
			this.isSplashDamage,
			this.isCriticalHit
		});
	}

	// Token: 0x040006D1 RID: 1745
	public DamageInfo.DamageSourceType type;

	// Token: 0x040006D2 RID: 1746
	public float healthDamage;

	// Token: 0x040006D3 RID: 1747
	public float balanceDamage;

	// Token: 0x040006D4 RID: 1748
	public bool isSplashDamage;

	// Token: 0x040006D5 RID: 1749
	public bool isPiercing;

	// Token: 0x040006D6 RID: 1750
	public bool isCriticalHit;

	// Token: 0x040006D7 RID: 1751
	public Vector3 point;

	// Token: 0x040006D8 RID: 1752
	public Vector3 direction;

	// Token: 0x040006D9 RID: 1753
	public Vector3 impactForce;

	// Token: 0x040006DA RID: 1754
	public Actor sourceActor;

	// Token: 0x040006DB RID: 1755
	private Weapon _sourceWeapon;

	// Token: 0x040006DD RID: 1757
	[Ignore]
	public static readonly DamageInfo Default = new DamageInfo(DamageInfo.DamageSourceType.Unknown, null, null);

	// Token: 0x040006DE RID: 1758
	[Ignore]
	public static readonly DamageInfo Explosion = new DamageInfo(DamageInfo.DamageSourceType.Explosion, null, null)
	{
		isSplashDamage = true
	};

	// Token: 0x020000E6 RID: 230
	public enum DamageSourceType
	{
		// Token: 0x040006E0 RID: 1760
		Unknown,
		// Token: 0x040006E1 RID: 1761
		Projectile,
		// Token: 0x040006E2 RID: 1762
		Melee,
		// Token: 0x040006E3 RID: 1763
		Explosion,
		// Token: 0x040006E4 RID: 1764
		StickyExplosive,
		// Token: 0x040006E5 RID: 1765
		VehicleRam,
		// Token: 0x040006E6 RID: 1766
		FallDamage,
		// Token: 0x040006E7 RID: 1767
		DamageZone,
		// Token: 0x040006E8 RID: 1768
		Exception,
		// Token: 0x040006E9 RID: 1769
		Scripted
	}
}

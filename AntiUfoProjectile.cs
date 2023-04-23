using System;
using Lua;
using UnityEngine;

// Token: 0x020000DE RID: 222
[ResolveDynValueAs(typeof(ExplodingProjectile))]
public class AntiUfoProjectile : ExplodingProjectile
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x0005FE9C File Offset: 0x0005E09C
	protected override Projectile.HitType Hit(Ray ray, RaycastHit hitInfo, out bool showHitIndicator)
	{
		Projectile.HitType result = base.Hit(ray, hitInfo, out showHitIndicator);
		if (hitInfo.collider.attachedRigidbody == null)
		{
			return result;
		}
		Ufo component = hitInfo.collider.attachedRigidbody.GetComponent<Ufo>();
		if (component != null)
		{
			IngameUi.OnDamageDealt(new DamageInfo(DamageInfo.DamageSourceType.Explosion, this.killCredit, this.sourceWeapon), default(HitInfo));
			component.Damage();
			return Projectile.HitType.Stop;
		}
		return result;
	}
}

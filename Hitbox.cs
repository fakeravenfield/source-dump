using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class Hitbox : MonoBehaviour
{
	// Token: 0x0600034C RID: 844 RVA: 0x00004246 File Offset: 0x00002446
	public static bool IsHitboxLayer(int layer)
	{
		return layer == 8 || layer == 10 || layer == 16;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0004CF84 File Offset: 0x0004B184
	public bool ProjectileHit(Projectile p, Vector3 position)
	{
		float num = this.multiplier;
		if (this.heightBasedMultiplier)
		{
			float x = base.transform.worldToLocalMatrix.MultiplyPoint(position).x;
			float t = Mathf.Clamp01(0.95f - x);
			num = Mathf.Lerp(this.minMultiplier, this.multiplier, t);
		}
		bool result;
		try
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Projectile, p.killCredit, p.sourceWeapon)
			{
				healthDamage = p.Damage() * num,
				balanceDamage = p.BalanceDamage(),
				isPiercing = p.configuration.piercing,
				isCriticalHit = (num > 1.01f),
				point = position,
				direction = p.transform.forward,
				impactForce = p.configuration.impactForce * p.transform.forward
			};
			result = this.parent.Damage(info);
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0004D098 File Offset: 0x0004B298
	public bool VehicleHit(Vehicle vehicle, Vector3 position)
	{
		Actor actor = this.parent as Actor;
		Rigidbody rigidbody = vehicle.rigidbody;
		float healthDamage = Mathf.Clamp(rigidbody.velocity.magnitude * 5f, 0f, 200f);
		float d = Mathf.Min(10f, rigidbody.mass * 0.01f);
		DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.VehicleRam, vehicle.Driver(), null)
		{
			healthDamage = healthDamage,
			balanceDamage = 200f,
			isSplashDamage = true,
			point = position,
			direction = rigidbody.velocity.normalized,
			impactForce = rigidbody.velocity * d
		};
		if (!(actor == null) && !actor.aiControlled)
		{
			info.healthDamage *= 0.5f;
		}
		return this.parent.Damage(info);
	}

	// Token: 0x040002D8 RID: 728
	public const int LAYER = 8;

	// Token: 0x040002D9 RID: 729
	public const int RAGDOLL_LAYER = 10;

	// Token: 0x040002DA RID: 730
	public const int SEATED_LAYER = 16;

	// Token: 0x040002DB RID: 731
	private const float RIGIDBODY_HIT_FORCE = 0.01f;

	// Token: 0x040002DC RID: 732
	private const float MAX_HIT_FORCE_GAIN = 10f;

	// Token: 0x040002DD RID: 733
	public Hurtable parent;

	// Token: 0x040002DE RID: 734
	public float multiplier = 1f;

	// Token: 0x040002DF RID: 735
	public bool heightBasedMultiplier;

	// Token: 0x040002E0 RID: 736
	public float minMultiplier = 0.5f;
}

using System;
using Lua;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class ExplodingProjectile : Projectile
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x000066F8 File Offset: 0x000048F8
	protected override void AssignArmorDamage()
	{
		this.armorDamage = Vehicle.ArmorRating.HeavyArms;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00006701 File Offset: 0x00004901
	protected override void Awake()
	{
		base.Awake();
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00060B34 File Offset: 0x0005ED34
	public override void StartTravelling()
	{
		base.StartTravelling();
		base.enabled = true;
		if (this.impactParticles != null)
		{
			this.impactParticles.Stop();
		}
		if (this.trailParticles != null)
		{
			this.trailParticles.Play();
		}
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00060BA0 File Offset: 0x0005EDA0
	protected override Projectile.HitType Hit(Ray ray, RaycastHit hitInfo, out bool showHitIndicator)
	{
		bool flag;
		Projectile.HitType hitType = base.Hit(ray, hitInfo, out flag);
		bool flag2 = false;
		if (hitType == Projectile.HitType.Stop || hitType == Projectile.HitType.StopNoDecal)
		{
			Vector3 position = hitInfo.point + hitInfo.normal * 0.3f;
			base.transform.position = position;
			flag2 = this.Explode(position, hitInfo.normal);
		}
		showHitIndicator = (flag || flag2);
		return hitType;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00060C04 File Offset: 0x0005EE04
	protected override bool Travel(Vector3 delta)
	{
		Ray ray = new Ray(base.transform.position + new Vector3(0f, 1.5f, 0f), delta.normalized);
		RaycastHit raycastHit;
		if (this.explodeAtWaterSurface && WaterLevel.Raycast(ray, out raycastHit, delta.magnitude))
		{
			this.OnWaterSurfaceDetonation(raycastHit.point);
			return false;
		}
		return base.Travel(delta);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00006715 File Offset: 0x00004915
	protected virtual bool OnWaterSurfaceDetonation(Vector3 enterPosition)
	{
		base.transform.position = enterPosition;
		return this.Explode(enterPosition, Vector3.up);
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00060C74 File Offset: 0x0005EE74
	protected virtual bool Explode(Vector3 position, Vector3 up)
	{
		bool reduceFriendlyDamage = this.firedByAI && this.travelDistance < 5f;
		bool result = ActorManager.Explode(this.killCredit, this.sourceWeapon, position, this.explosionConfiguration, this.armorDamage, reduceFriendlyDamage);
		base.transform.rotation = Quaternion.LookRotation(up);
		base.enabled = false;
		try
		{
			Renderer[] array = this.renderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
		if (this.impactParticles != null)
		{
			this.impactParticles.Play();
		}
		if (this.trailParticles != null)
		{
			this.trailParticles.Stop();
		}
		if (this.audioSource != null)
		{
			Vector3 vector = position + up * 0.5f;
			GameManager.UpdateSoundOutputGroupCombat(this.audioSource, Vector3.Distance(vector, GameManager.GetPlayerCameraPosition()), !Physics.Linecast(vector, GameManager.GetPlayerCameraPosition(), 8392705));
			this.audioSource.pitch *= UnityEngine.Random.Range(0.9f, 1.1f);
			this.audioSource.Play();
		}
		if (this.activateOnExplosion != null)
		{
			this.activateOnExplosion.SetActive(true);
			if (this.deactivateAgainTime > 0f)
			{
				base.Invoke("Deactivate", this.deactivateAgainTime);
			}
		}
		base.Invoke("StopSmoke", this.smokeTime);
		return result;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0000672F File Offset: 0x0000492F
	public override void StopScripted(bool silent)
	{
		if (silent)
		{
			this.Cleanup(false);
			return;
		}
		this.Explode(base.transform.position, Vector3.up);
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00006753 File Offset: 0x00004953
	private void Deactivate()
	{
		this.activateOnExplosion.SetActive(false);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00006761 File Offset: 0x00004961
	private void StopSmoke()
	{
		if (this.impactParticles != null)
		{
			this.impactParticles.Stop(true);
		}
		base.Invoke("CleanupDelayed", 5f);
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0000678D File Offset: 0x0000498D
	protected override void Cleanup(bool hitSomething)
	{
		if (!hitSomething)
		{
			base.Cleanup(hitSomething);
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00006799 File Offset: 0x00004999
	private void CleanupDelayed()
	{
		base.Cleanup(false);
	}

	// Token: 0x040006EA RID: 1770
	public const float EXPLOSION_SOURCE_NORMAL_OFFSET = 0.3f;

	// Token: 0x040006EB RID: 1771
	private const float EXPLOSION_WATER_DEPTH = 1.5f;

	// Token: 0x040006EC RID: 1772
	private const float AI_MISFIRE_TRAVEL_DISTANCE_THRESHOLD = 5f;

	// Token: 0x040006ED RID: 1773
	private const float CLEANUP_TIME = 5f;

	// Token: 0x040006EE RID: 1774
	public ExplodingProjectile.ExplosionConfiguration explosionConfiguration;

	// Token: 0x040006EF RID: 1775
	public float smokeTime = 8f;

	// Token: 0x040006F0 RID: 1776
	public Renderer[] renderers;

	// Token: 0x040006F1 RID: 1777
	public ParticleSystem trailParticles;

	// Token: 0x040006F2 RID: 1778
	public ParticleSystem impactParticles;

	// Token: 0x040006F3 RID: 1779
	public GameObject activateOnExplosion;

	// Token: 0x040006F4 RID: 1780
	public float deactivateAgainTime = -1f;

	// Token: 0x040006F5 RID: 1781
	public bool explodeAtWaterSurface = true;

	// Token: 0x040006F6 RID: 1782
	protected AudioSource audioSource;

	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class ExplosionConfiguration
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00060E00 File Offset: 0x0005F000
		[Doc("Creates an explosion configuration with a linear falloff.[..] Typically useful for small size explosions.")]
		public ExplodingProjectile.ExplosionConfiguration CreateLinearFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			AnimationCurve linearFalloffCurve = ActorManager.instance.linearFalloffCurve;
			return new ExplodingProjectile.ExplosionConfiguration
			{
				damage = damage,
				damageRange = damageRange,
				balanceDamage = balanceDamage,
				balanceRange = balanceDamageRange,
				force = force,
				damageFalloff = linearFalloffCurve,
				balanceFalloff = linearFalloffCurve
			};
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00060E50 File Offset: 0x0005F050
		[Doc("Creates an explosion configuration with sharp falloff.[..] Typically useful for large explosions, where the explosion should deal damage to but not neccessarily kill everyone in its range.")]
		public ExplodingProjectile.ExplosionConfiguration CreateSharpFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			AnimationCurve sharpFalloffCurve = ActorManager.instance.sharpFalloffCurve;
			return new ExplodingProjectile.ExplosionConfiguration
			{
				damage = damage,
				damageRange = damageRange,
				balanceDamage = balanceDamage,
				balanceRange = balanceDamageRange,
				force = force,
				damageFalloff = sharpFalloffCurve,
				balanceFalloff = sharpFalloffCurve
			};
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		[Doc("Creates an explosion configuration with smooth step falloff.[..] Typically useful for small size explosions with a sharp change from high to low damage at half range.")]
		public ExplodingProjectile.ExplosionConfiguration CreateSmoothStepFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			AnimationCurve smoothStepFalloffCurve = ActorManager.instance.smoothStepFalloffCurve;
			return new ExplodingProjectile.ExplosionConfiguration
			{
				damage = damage,
				damageRange = damageRange,
				balanceDamage = balanceDamage,
				balanceRange = balanceDamageRange,
				force = force,
				damageFalloff = smoothStepFalloffCurve,
				balanceFalloff = smoothStepFalloffCurve
			};
		}

		// Token: 0x040006F7 RID: 1783
		public float damage = 300f;

		// Token: 0x040006F8 RID: 1784
		public float balanceDamage = 300f;

		// Token: 0x040006F9 RID: 1785
		public float infantryDamageMultiplier = 1f;

		// Token: 0x040006FA RID: 1786
		public float force = 500f;

		// Token: 0x040006FB RID: 1787
		public float damageRange = 6f;

		// Token: 0x040006FC RID: 1788
		public AnimationCurve damageFalloff;

		// Token: 0x040006FD RID: 1789
		public float balanceRange = 9f;

		// Token: 0x040006FE RID: 1790
		public AnimationCurve balanceFalloff;
	}
}

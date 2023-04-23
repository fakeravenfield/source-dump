using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200098B RID: 2443
	[Wrapper(typeof(Projectile))]
	public static class WProjectile
	{
		// Token: 0x06003E13 RID: 15891 RVA: 0x00029F8F File Offset: 0x0002818F
		[Getter]
		public static Vector3 GetVelocity(Projectile self)
		{
			return self.velocity;
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00029F97 File Offset: 0x00028197
		[Setter]
		[Doc("The current movement velocity of the projectile[..]")]
		public static void SetVelocity(Projectile self, Vector3 velocity)
		{
			self.velocity = velocity;
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x00029FA0 File Offset: 0x000281A0
		[Getter]
		public static Actor GetSource(Projectile self)
		{
			return self.killCredit;
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00029FA8 File Offset: 0x000281A8
		[Setter]
		[Deprecated("Use ``Projectile.killCredit`` instead")]
		public static void SetSource(Projectile self, Actor source)
		{
			self.killCredit = source;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00029FA0 File Offset: 0x000281A0
		[Getter]
		public static Actor GetKillCredit(Projectile self)
		{
			return self.killCredit;
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00029FA8 File Offset: 0x000281A8
		[Setter]
		[Doc("The actor who fired the projectile[..] If the projectile kills a target, this is the actor that is awarded the kill")]
		public static void SetKillCredit(Projectile self, Actor source)
		{
			self.killCredit = source;
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00029FB1 File Offset: 0x000281B1
		[Getter]
		public static Vehicle.ArmorRating GetArmorDamage(Projectile self)
		{
			return self.armorDamage;
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00029FB9 File Offset: 0x000281B9
		[Setter]
		[Doc("The minimum armor rating that this projectile can damage[..]")]
		public static void SetArmorDamage(Projectile self, Vehicle.ArmorRating armorDamage)
		{
			self.armorDamage = armorDamage;
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x00029FC2 File Offset: 0x000281C2
		[Getter]
		[Doc("Gets the weapon that fired this projectile[..]")]
		public static Weapon GetSourceWeapon(Projectile self)
		{
			return self.sourceWeapon;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00029FCA File Offset: 0x000281CA
		[Getter]
		[Doc("Returns true if the projectile is heading towards the player actor, otherwise false[..]")]
		public static bool IsTravellingTowardsPlayer(Projectile self)
		{
			return self.travellingTowardsPlayer;
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x00029FD2 File Offset: 0x000281D2
		[Getter]
		[Doc("The total distance the projectile has travelled[..]")]
		public static float GetDistanceTravelled(Projectile self)
		{
			return self.travelDistance;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00029FDA File Offset: 0x000281DA
		[Getter]
		public static float GetImpactForce(Projectile self)
		{
			return self.configuration.impactForce;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00029FE7 File Offset: 0x000281E7
		[Setter]
		[Doc("The force applied to any hit rigidbody[..]")]
		public static void SetImpactForce(Projectile self, float force)
		{
			self.configuration.impactForce = force;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00006CA5 File Offset: 0x00004EA5
		[Getter]
		public static float GetDamage(Projectile self)
		{
			return self.configuration.damage;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00029FF5 File Offset: 0x000281F5
		[Setter]
		[Doc("The health damage value dealt when hitting a hitbox[..]")]
		public static void SetDamage(Projectile self, float damage)
		{
			self.configuration.damage = damage;
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x00006CB2 File Offset: 0x00004EB2
		[Getter]
		public static float GetBalanceDamage(Projectile self)
		{
			return self.configuration.balanceDamage;
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x0002A003 File Offset: 0x00028203
		[Setter]
		[Doc("The balance damage value dealt when hitting a hitbox[..]")]
		public static void SetBalanceDamage(Projectile self, float balanceDamage)
		{
			self.configuration.balanceDamage = balanceDamage;
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x0002A011 File Offset: 0x00028211
		[Getter]
		public static float GetGravityMultiplier(Projectile self)
		{
			return self.configuration.gravityMultiplier;
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x0002A01E File Offset: 0x0002821E
		[Setter]
		[Doc("The amount of gravity affecting the path of this projectile[..]")]
		public static void SetGravityMultiplier(Projectile self, float gravityMultiplier)
		{
			self.configuration.gravityMultiplier = gravityMultiplier;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x0002A02C File Offset: 0x0002822C
		[Doc("Stops and destroys the projectile.[..] Exploding projectiles trigger an explosion if silent is false, otherwise they are immediately destroyed.")]
		public static void Stop(Projectile self, bool silent)
		{
			self.StopScripted(silent);
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x0002A035 File Offset: 0x00028235
		[Getter]
		[Doc("True if the projectile is an ExplodingProjectile.[..] If true, you can safely access fields via the ExplodingProjectile class API.")]
		public static bool GetIsExplodingProjectile(Projectile self)
		{
			return self is ExplodingProjectile;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x0002A040 File Offset: 0x00028240
		[Getter]
		[Doc("True if the projectile is a RocketProjectile.[..] If true, you can safely access fields via the RocketProjectile class API.")]
		public static bool GetIsRocketProjectile(Projectile self)
		{
			return self is Rocket;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x0002A04B File Offset: 0x0002824B
		[Getter]
		[Doc("True if the projectile is a TargetSeekingMissileProjectile.[..] If true, you can safely access fields via the TargetSeekingMissileProjectile class API.")]
		public static bool GetIsTargetSeekingMissileProjectile(Projectile self)
		{
			return self is TargetSeekingMissile;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x0002A056 File Offset: 0x00028256
		[Getter]
		[Doc("True if the projectile is a GrenadeProjectile.[..] If true, you can safely access fields via the GrenadeProjectile class API.")]
		public static bool GetIsGrenadeProjectile(Projectile self)
		{
			return self is GrenadeProjectile;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x0002A061 File Offset: 0x00028261
		[Getter]
		[Doc("True if the projectile is a RigidbodyProjectile.[..] If true, you can safely access fields via the RigidbodyProjectile class API.")]
		public static bool GetIsRigidbodyProjectile(Projectile self)
		{
			return self is RigidbodyProjectile;
		}
	}
}

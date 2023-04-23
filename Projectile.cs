using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class Projectile : MonoBehaviour
{
	// Token: 0x0600074B RID: 1867 RVA: 0x00006AA3 File Offset: 0x00004CA3
	protected virtual void AssignArmorDamage()
	{
		this.armorDamage = Vehicle.ArmorRating.SmallArms;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void OnReturnedToPool()
	{
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00006AAC File Offset: 0x00004CAC
	protected virtual void Awake()
	{
		this.trailRenderer = base.GetComponentInChildren<TrailRenderer>();
		if (this.trailRenderer != null)
		{
			this.trailRenderer.autodestruct = false;
		}
		if (this.autoAssignArmorDamage)
		{
			this.AssignArmorDamage();
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000623C0 File Offset: 0x000605C0
	private void Start()
	{
		try
		{
			if (this.sourceWeapon == null)
			{
				this.StartTravelling();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00006AE2 File Offset: 0x00004CE2
	public virtual void ResetFields()
	{
		this.performInfantryInitialMuzzleTravel = false;
		this.initialMuzzleTravelDistance = 0f;
		this.travelDistance = 0f;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x000623F8 File Offset: 0x000605F8
	public virtual void StartTravelling()
	{
		this.firedByAI = (this.killCredit != null && this.killCredit.aiControlled);
		this.firedByPlayer = (this.killCredit != null && !this.killCredit.aiControlled);
		if (this.trailRenderer != null)
		{
			this.trailRenderer.Clear();
		}
		this.velocity = base.transform.forward * this.configuration.speed;
		if (this.configuration.inheritVelocity && this.killCredit != null)
		{
			this.velocity += this.killCredit.Velocity();
		}
		this.expireTime = Time.time + this.configuration.lifetime;
		if (this.killCredit != null && this.killCredit.aiControlled)
		{
			this.hitMask = (this.configuration.passThroughPenetrateLayer ? -14715397 : -12618245);
		}
		else
		{
			this.hitMask = (this.configuration.passThroughPenetrateLayer ? -14977541 : -12880389);
		}
		ActorManager.RegisterProjectile(this);
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00062538 File Offset: 0x00060738
	protected virtual void Update()
	{
		try
		{
			if (Time.time > this.expireTime)
			{
				this.Cleanup(false);
			}
			else
			{
				Vector3 position = base.transform.position;
				this.UpdatePosition();
				if (!this.firedByPlayer && this.configuration.makesFlybySound)
				{
					Vector3 vector = ActorManager.instance.player.Position();
					Vector3 lhs = base.transform.position - vector;
					bool flag = this.travellingTowardsPlayer;
					this.travellingTowardsPlayer = (Vector3.Dot(lhs, this.velocity) < 0f);
					if (!this.travellingTowardsPlayer && flag && (!ActorManager.instance.player.IsSeated() || this.armorDamage >= ActorManager.instance.player.seat.vehicle.armorDamagedBy))
					{
						Vector3 vector2 = SMath.LineVsPointClosest(position, base.transform.position, vector);
						if (Vector3.Distance(vector2, vector) < 15f)
						{
							FpsActorController.instance.BulletFlyby(vector2, UnityEngine.Random.Range(this.configuration.flybyPitch, 0.9f * this.configuration.flybyPitch));
						}
					}
				}
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00062684 File Offset: 0x00060884
	protected virtual void UpdatePosition()
	{
		if (this.performInfantryInitialMuzzleTravel)
		{
			this.performInfantryInitialMuzzleTravel = false;
			Vector3 vector = this.initialMuzzleTravelDistance * base.transform.forward;
			base.transform.position -= vector;
			if (!this.Travel(vector))
			{
				return;
			}
		}
		this.velocity += Physics.gravity * this.configuration.gravityMultiplier * Time.deltaTime;
		Vector3 delta = this.velocity * Time.deltaTime;
		this.Travel(delta);
		this.travelDistance += delta.magnitude;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00062738 File Offset: 0x00060938
	protected virtual bool Travel(Vector3 delta)
	{
		Ray ray = new Ray(base.transform.position, delta.normalized);
		RaycastHit hitInfo;
		if (WaterLevel.Raycast(ray, out hitInfo, delta.magnitude))
		{
			GameManager.CreateSplashEffect(false, hitInfo.point);
		}
		if (this.ProjectileRaycast(ray, out hitInfo, delta.magnitude * 2f, this.hitMask))
		{
			bool flag;
			Projectile.HitType hitType = this.Hit(ray, hitInfo, out flag);
			if (hitType == Projectile.HitType.Stop)
			{
				if (hitInfo.collider.gameObject.layer == 0 && hitInfo.rigidbody == null)
				{
					this.SpawnDecal(hitInfo);
				}
				return false;
			}
			if (hitType == Projectile.HitType.StopNoDecal)
			{
				return false;
			}
			if (hitType == Projectile.HitType.Stutter)
			{
				base.transform.position = hitInfo.point + this.velocity.normalized * 0.01f;
				return true;
			}
		}
		base.transform.position += delta;
		if (delta != Vector3.zero)
		{
			base.transform.rotation = Quaternion.LookRotation(delta);
		}
		return true;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00062844 File Offset: 0x00060A44
	public bool ProjectileRaycast(Ray ray, out RaycastHit hitInfo, float distance, int mask)
	{
		hitInfo = default(RaycastHit);
		int num = Physics.RaycastNonAlloc(ray, Projectile.RAYCAST_RESULTS, distance, mask);
		bool result = false;
		float num2 = distance;
		for (int i = 0; i < num; i++)
		{
			if (Projectile.RAYCAST_RESULTS[i].collider != null && Projectile.RAYCAST_RESULTS[i].distance < num2)
			{
				if (this.killCredit != null)
				{
					if (Hitbox.IsHitboxLayer(Projectile.RAYCAST_RESULTS[i].collider.gameObject.layer))
					{
						if (Projectile.RAYCAST_RESULTS[i].collider.GetComponent<Hitbox>().parent == this.killCredit)
						{
							goto IL_138;
						}
					}
					else if (Projectile.RAYCAST_RESULTS[i].collider.gameObject.layer == 12)
					{
						Vehicle componentInParent = Projectile.RAYCAST_RESULTS[i].collider.GetComponentInParent<Vehicle>();
						if (componentInParent != null && !componentInParent.canFireAtOwnVehicle && this.killCredit.IsSeated() && this.killCredit.seat.vehicle == componentInParent)
						{
							goto IL_138;
						}
					}
				}
				hitInfo = Projectile.RAYCAST_RESULTS[i];
				num2 = hitInfo.distance;
				result = true;
			}
			IL_138:;
		}
		return result;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00062998 File Offset: 0x00060B98
	protected virtual Projectile.HitType Hit(Ray ray, RaycastHit hitInfo, out bool showHitIndicator)
	{
		showHitIndicator = false;
		Projectile.HitType result;
		try
		{
			if (this.configuration.piercing && hitInfo.collider.CompareTag("Piercable"))
			{
				Collider collider = hitInfo.collider;
				collider.enabled = false;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(hitInfo.point, ray.direction), out raycastHit, 5f, this.hitMask))
				{
					hitInfo = raycastHit;
				}
				collider.enabled = true;
			}
			Destructible component = hitInfo.collider.GetComponent<Destructible>();
			bool flag = component is VehicleDestructibleHitbox;
			if (component != null && this.DealDamageOnHit())
			{
				Projectile.HitDestructibleResult hitDestructibleResult = this.OnHitDestructible(component);
				if (hitDestructibleResult == Projectile.HitDestructibleResult.HitIndicator)
				{
					showHitIndicator = true;
				}
				else if (hitDestructibleResult == Projectile.HitDestructibleResult.Stutter)
				{
					return Projectile.HitType.Stutter;
				}
			}
			bool flag2 = component == null && hitInfo.collider.attachedRigidbody == null;
			if (Hitbox.IsHitboxLayer(hitInfo.collider.gameObject.layer))
			{
				Hitbox component2 = hitInfo.collider.GetComponent<Hitbox>();
				if (this.killCredit != null && this.killCredit.aiControlled && component2.parent.team == this.killCredit.team && this.travelDistance < 100f)
				{
					return Projectile.HitType.None;
				}
				Actor actor = component2.parent as Actor;
				if ((!(this.killCredit != null) || this.killCredit.aiControlled) && actor != null && actor.HeroArmorIgnoresDamage())
				{
					return Projectile.HitType.None;
				}
				if (actor != null && this.killCredit != null && this.killCredit.IsSeated() && this.killCredit.seat.vehicle.SeatsActor(actor))
				{
					return Projectile.HitType.None;
				}
				if (this.DealDamageOnHit())
				{
					showHitIndicator = component2.ProjectileHit(this, hitInfo.point);
				}
			}
			bool flag3 = false;
			bool flag4 = false;
			if (!flag && 12 == hitInfo.collider.gameObject.layer)
			{
				Vehicle componentInParent = hitInfo.collider.GetComponentInParent<Vehicle>();
				if (componentInParent == null)
				{
					this.Cleanup(true);
					return Projectile.HitType.StopNoDecal;
				}
				flag4 = true;
				flag3 = componentInParent.AnySeatsTaken();
				if (componentInParent.IsDamagedByRating(this.armorDamage))
				{
					showHitIndicator = !componentInParent.dead;
					if (this.DealDamageOnHit())
					{
						DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Projectile, this.killCredit, this.sourceWeapon)
						{
							healthDamage = this.Damage() * componentInParent.GetDamageMultiplier(this.armorDamage)
						};
						componentInParent.Damage(info);
					}
				}
			}
			Rigidbody attachedRigidbody = hitInfo.collider.attachedRigidbody;
			if (attachedRigidbody != null)
			{
				if (!flag4)
				{
					Vector3 force = this.velocity.normalized * this.configuration.impactForce;
					attachedRigidbody.AddForceAtPosition(force, hitInfo.point, ForceMode.Impulse);
				}
				else if (this.configuration.impactForce > 300f)
				{
					float num = this.configuration.impactForce - 300f;
					if (flag3)
					{
						num *= 0.2f;
					}
					Vector3 force2 = this.velocity.normalized * num;
					attachedRigidbody.AddForceAtPosition(force2, hitInfo.point, ForceMode.Impulse);
				}
			}
			if (this.firedByAI && this.travelDistance < 1.5f)
			{
				result = Projectile.HitType.Stutter;
			}
			else
			{
				if (!this.firedByPlayer && this.configuration.makesFlybySound && this.travellingTowardsPlayer && Vector3.Distance(hitInfo.point, ActorManager.instance.player.Position()) < 15f)
				{
					FpsActorController.instance.BulletFlyby(hitInfo.point, this.configuration.flybyPitch);
				}
				this.Cleanup(true);
				result = (flag2 ? Projectile.HitType.Stop : Projectile.HitType.StopNoDecal);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			this.Cleanup(true);
			result = Projectile.HitType.StopNoDecal;
		}
		return result;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0000476F File Offset: 0x0000296F
	protected virtual bool DealDamageOnHit()
	{
		return true;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00062D98 File Offset: 0x00060F98
	protected virtual Projectile.HitDestructibleResult OnHitDestructible(Destructible destructible)
	{
		VehicleDestructibleHitbox vehicleDestructibleHitbox = destructible as VehicleDestructibleHitbox;
		if (destructible.IsShatteredByRating(this.armorDamage))
		{
			destructible.Shatter();
			return Projectile.HitDestructibleResult.Stutter;
		}
		if (destructible.IsDamagedByRating(this.armorDamage))
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Projectile, this.killCredit, this.sourceWeapon)
			{
				healthDamage = this.Damage() * destructible.GetDamageMultiplier(this.armorDamage)
			};
			destructible.Damage(info);
		}
		bool flag;
		if (vehicleDestructibleHitbox != null)
		{
			flag = (destructible.showHitIndicator && !vehicleDestructibleHitbox.vehicle.dead);
		}
		else
		{
			flag = destructible.showHitIndicator;
		}
		if (!flag)
		{
			return Projectile.HitDestructibleResult.Default;
		}
		return Projectile.HitDestructibleResult.HitIndicator;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00006B01 File Offset: 0x00004D01
	protected virtual void Cleanup(bool hitSomething)
	{
		ProjectilePoolManager.CleanupProjectile(this);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00006B09 File Offset: 0x00004D09
	protected virtual void SpawnDecal(RaycastHit hitInfo)
	{
		DecalManager.AddDecal(hitInfo.point, hitInfo.normal, this.configuration.impactDecalSize, DecalManager.DecalType.Impact);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00006B2A File Offset: 0x00004D2A
	public virtual float Damage()
	{
		return this.DamageDropOff() * this.configuration.damage;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00006B3E File Offset: 0x00004D3E
	public virtual float BalanceDamage()
	{
		return this.DamageDropOff() * this.configuration.balanceDamage;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00006B52 File Offset: 0x00004D52
	private float DamageDropOff()
	{
		return this.configuration.damageDropOff.Evaluate(this.travelDistance / this.configuration.dropoffEnd);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00006B76 File Offset: 0x00004D76
	public virtual void StopScripted(bool silent)
	{
		this.Cleanup(false);
	}

	// Token: 0x04000748 RID: 1864
	public const int PROJECTILE_PENETRATE_LAYER = 21;

	// Token: 0x04000749 RID: 1865
	private const float AI_IGNORE_GEOMETRY_DISTANCE = 1.5f;

	// Token: 0x0400074A RID: 1866
	private static RaycastHit[] RAYCAST_RESULTS = new RaycastHit[1024];

	// Token: 0x0400074B RID: 1867
	private const float PASS_PLAYER_MAX_SOUND_DISTANCE = 15f;

	// Token: 0x0400074C RID: 1868
	private const float MIN_AI_FRIENDLY_FIRE_DISTANCE = 100f;

	// Token: 0x0400074D RID: 1869
	private const int LEVEL_LAYER = 0;

	// Token: 0x0400074E RID: 1870
	private const int RAGDOLL_LAYER = 10;

	// Token: 0x0400074F RID: 1871
	private const int IGNORE_RAYCAST_LAYER = 2;

	// Token: 0x04000750 RID: 1872
	private const int NO_ACTOR_COLLISION_LAYER = 15;

	// Token: 0x04000751 RID: 1873
	public const int HIT_MASK = -12618245;

	// Token: 0x04000752 RID: 1874
	public const int HIT_MASK_PENETRATING = -14715397;

	// Token: 0x04000753 RID: 1875
	public const int HIT_MASK_NO_FIRST_PERSON_MODEL_COLLISION = -12880389;

	// Token: 0x04000754 RID: 1876
	public const int HIT_MASK_PENETRATING_NO_FIRST_PERSON_MODEL_COLLISION = -14977541;

	// Token: 0x04000755 RID: 1877
	private const float PIERCING_RANGE = 5f;

	// Token: 0x04000756 RID: 1878
	private const float VEHICLE_IMPACT_FORCE_THRESHOLD = 300f;

	// Token: 0x04000757 RID: 1879
	private const float MANNED_VEHICLE_IMPACT_FORCE_MULTIPLIER = 0.2f;

	// Token: 0x04000758 RID: 1880
	public Projectile.Configuration configuration;

	// Token: 0x04000759 RID: 1881
	public bool autoAssignArmorDamage = true;

	// Token: 0x0400075A RID: 1882
	public Vehicle.ArmorRating armorDamage;

	// Token: 0x0400075B RID: 1883
	[NonSerialized]
	public Vector3 velocity = Vector3.zero;

	// Token: 0x0400075C RID: 1884
	protected float expireTime;

	// Token: 0x0400075D RID: 1885
	[NonSerialized]
	public Actor killCredit;

	// Token: 0x0400075E RID: 1886
	[NonSerialized]
	public Weapon sourceWeapon;

	// Token: 0x0400075F RID: 1887
	[NonSerialized]
	public bool travellingTowardsPlayer;

	// Token: 0x04000760 RID: 1888
	[NonSerialized]
	public bool performInfantryInitialMuzzleTravel;

	// Token: 0x04000761 RID: 1889
	[NonSerialized]
	public float initialMuzzleTravelDistance;

	// Token: 0x04000762 RID: 1890
	[NonSerialized]
	public float travelDistance;

	// Token: 0x04000763 RID: 1891
	[NonSerialized]
	public int prefabInstanceId;

	// Token: 0x04000764 RID: 1892
	private TrailRenderer trailRenderer;

	// Token: 0x04000765 RID: 1893
	protected int hitMask;

	// Token: 0x04000766 RID: 1894
	[NonSerialized]
	public bool firedByAI;

	// Token: 0x04000767 RID: 1895
	[NonSerialized]
	public bool firedByPlayer;

	// Token: 0x020000F7 RID: 247
	protected enum HitType
	{
		// Token: 0x04000769 RID: 1897
		None,
		// Token: 0x0400076A RID: 1898
		Stop,
		// Token: 0x0400076B RID: 1899
		StopNoDecal,
		// Token: 0x0400076C RID: 1900
		Stutter
	}

	// Token: 0x020000F8 RID: 248
	protected enum HitDestructibleResult
	{
		// Token: 0x0400076E RID: 1902
		Stutter,
		// Token: 0x0400076F RID: 1903
		HitIndicator,
		// Token: 0x04000770 RID: 1904
		Default
	}

	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class Configuration
	{
		// Token: 0x04000771 RID: 1905
		public float speed = 300f;

		// Token: 0x04000772 RID: 1906
		public float impactForce = 200f;

		// Token: 0x04000773 RID: 1907
		public float lifetime = 2f;

		// Token: 0x04000774 RID: 1908
		public float damage = 70f;

		// Token: 0x04000775 RID: 1909
		public float balanceDamage = 60f;

		// Token: 0x04000776 RID: 1910
		public float impactDecalSize = 0.2f;

		// Token: 0x04000777 RID: 1911
		public bool passThroughPenetrateLayer = true;

		// Token: 0x04000778 RID: 1912
		public bool piercing;

		// Token: 0x04000779 RID: 1913
		public bool makesFlybySound;

		// Token: 0x0400077A RID: 1914
		public float flybyPitch = 1f;

		// Token: 0x0400077B RID: 1915
		public float dropoffEnd = 300f;

		// Token: 0x0400077C RID: 1916
		public AnimationCurve damageDropOff;

		// Token: 0x0400077D RID: 1917
		public float gravityMultiplier = 1f;

		// Token: 0x0400077E RID: 1918
		public bool inheritVelocity;
	}
}

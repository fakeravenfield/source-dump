using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class GrenadeProjectile : Projectile
{
	// Token: 0x060006FD RID: 1789 RVA: 0x00006809 File Offset: 0x00004A09
	protected override void AssignArmorDamage()
	{
		this.armorDamage = Vehicle.ArmorRating.AntiTank;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00006812 File Offset: 0x00004A12
	protected override void Awake()
	{
		base.Awake();
		if (this.explosionParticles == null)
		{
			this.explosionParticles = base.GetComponent<ParticleSystem>();
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00060F88 File Offset: 0x0005F188
	public override void StartTravelling()
	{
		this.velocity = base.transform.forward * this.configuration.speed;
		this.rotationAxis = UnityEngine.Random.insideUnitSphere.normalized;
		this.angularSpeed = 400f;
		float num = this.configuration.lifetime - 2f;
		if (num > 0.5f)
		{
			base.Invoke("Flee", num);
		}
		this.detonationTime = Time.time + this.configuration.lifetime;
		if (this.onlyDetonateOnGround)
		{
			base.Invoke("ArmGround", this.configuration.lifetime);
			return;
		}
		base.Invoke("Explode", this.configuration.lifetime);
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00061048 File Offset: 0x0005F248
	protected override void Update()
	{
		this.velocity += Physics.gravity * Time.deltaTime;
		Vector3 vector = this.velocity * Time.deltaTime;
		Ray ray = new Ray(base.transform.position, vector);
		RaycastHit raycastHit;
		if (Physics.SphereCast(ray, this.radius, out raycastHit, vector.magnitude * 2f, 2101249))
		{
			base.transform.position = raycastHit.point + raycastHit.normal * (this.radius + 0.01f);
			Vector3 a = Vector3.Project(this.velocity, raycastHit.normal);
			this.velocity -= a * (this.bounciness + 1f);
			Vector3 vector2 = this.velocity * this.bounceDrag;
			this.velocity -= vector2;
			this.rotationAxis = base.transform.worldToLocalMatrix.MultiplyVector((Vector3.Cross(vector2, Vector3.up) + this.rotationAxis).normalized);
			this.angularSpeed = -vector2.magnitude * 400f;
			if (this.groundDetonationArmed && this.velocity.magnitude < this.groundDetonationSpeedThreshold)
			{
				this.Explode();
			}
		}
		else
		{
			base.transform.position += vector;
		}
		if (WaterLevel.Raycast(ray, out raycastHit, vector.magnitude))
		{
			GameManager.CreateSplashEffect(false, raycastHit.point);
		}
		base.transform.Rotate(this.rotationAxis, this.angularSpeed * Time.deltaTime);
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00061208 File Offset: 0x0005F408
	protected void Flee()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.velocity.magnitude < 2f)
		{
			float num = Mathf.Max(this.explosionConfiguration.balanceRange, this.explosionConfiguration.damageRange) * 0.8f;
			num = Mathf.Max(num, 10f);
			float num2 = this.detonationTime - Time.time;
			ActorManager.MakeActorsFleeFrom(base.transform.position, num, num2 - 0.5f, num * 0.8f, true);
			return;
		}
		base.Invoke("Flee", 0.2f);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00006834 File Offset: 0x00004A34
	protected virtual void ArmGround()
	{
		this.groundDetonationArmed = true;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0006129C File Offset: 0x0005F49C
	protected virtual void Explode()
	{
		ActorManager.Explode(this.killCredit, this.sourceWeapon, base.transform.position, this.explosionConfiguration, this.armorDamage, false);
		base.transform.rotation = Quaternion.LookRotation(Vector3.up);
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position, Vector3.down), out raycastHit, 1f, 1))
		{
			DecalManager.AddDecal(raycastHit.point, raycastHit.normal, UnityEngine.Random.Range(1f, 2f), DecalManager.DecalType.Impact);
		}
		base.enabled = false;
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		this.explosionParticles.Play(true);
		AudioSource component = base.GetComponent<AudioSource>();
		if (component != null)
		{
			Vector3 position = base.transform.position;
			position.y += 0.5f;
			GameManager.UpdateSoundOutputGroupCombat(component, Vector3.Distance(position, GameManager.GetPlayerCameraPosition()), !Physics.Linecast(position, GameManager.GetPlayerCameraPosition(), 8392705));
			component.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			component.Play();
		}
		if (this.activateOnExplosion != null)
		{
			this.activateOnExplosion.SetActive(true);
			if (this.deactivateAgainTime > 0f)
			{
				base.Invoke("Deactivate", this.deactivateAgainTime);
			}
		}
		base.Invoke("Cleanup", this.cleanupTime);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0000683D File Offset: 0x00004A3D
	private void Deactivate()
	{
		this.activateOnExplosion.SetActive(false);
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0000684B File Offset: 0x00004A4B
	private void Cleanup()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000705 RID: 1797
	private const int LAYER_MASK = 2101249;

	// Token: 0x04000706 RID: 1798
	private const float ROTATION_SPEED_MAGNITUDE = 400f;

	// Token: 0x04000707 RID: 1799
	private const float MAX_FLEE_RANGE = 10f;

	// Token: 0x04000708 RID: 1800
	private const float FLEE_BEFORE_FUSE_TIME = 2f;

	// Token: 0x04000709 RID: 1801
	public ExplodingProjectile.ExplosionConfiguration explosionConfiguration;

	// Token: 0x0400070A RID: 1802
	public Renderer[] renderers;

	// Token: 0x0400070B RID: 1803
	public ParticleSystem explosionParticles;

	// Token: 0x0400070C RID: 1804
	public GameObject activateOnExplosion;

	// Token: 0x0400070D RID: 1805
	public float deactivateAgainTime = -1f;

	// Token: 0x0400070E RID: 1806
	public float radius = 0.1f;

	// Token: 0x0400070F RID: 1807
	public float bounciness = 0.5f;

	// Token: 0x04000710 RID: 1808
	public float bounceDrag = 0.2f;

	// Token: 0x04000711 RID: 1809
	public float cleanupTime = 10f;

	// Token: 0x04000712 RID: 1810
	public bool onlyDetonateOnGround;

	// Token: 0x04000713 RID: 1811
	public float groundDetonationSpeedThreshold = 0.1f;

	// Token: 0x04000714 RID: 1812
	private Vector3 rotationAxis;

	// Token: 0x04000715 RID: 1813
	private float angularSpeed;

	// Token: 0x04000716 RID: 1814
	private float detonationTime;

	// Token: 0x04000717 RID: 1815
	private bool groundDetonationArmed;
}

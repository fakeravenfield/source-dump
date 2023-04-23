using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class Ufo : MonoBehaviour
{
	// Token: 0x06001083 RID: 4227 RVA: 0x00089864 File Offset: 0x00087A64
	private void Start()
	{
		this.bounds = new Bounds(Vector3.zero, Vector3.zero);
		foreach (PathfindingBox pathfindingBox in UnityEngine.Object.FindObjectsOfType<PathfindingBox>())
		{
			Bounds bounds = new Bounds(pathfindingBox.transform.position, pathfindingBox.transform.localScale);
			if (this.bounds.size == Vector3.zero)
			{
				this.bounds = bounds;
			}
			else
			{
				this.bounds.Encapsulate(bounds);
			}
		}
		this.UpdateTargetPosition();
		this.explosionMaterial = this.explosionRenderer.material;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00089900 File Offset: 0x00087B00
	private void Update()
	{
		if (this.newTargetAction.TrueDone())
		{
			this.UpdateTargetPosition();
		}
		if (!this.dead)
		{
			float t = this.tractorBeamAction.TrueDone() ? (Time.deltaTime * 2f) : (Time.deltaTime * 0.2f);
			Vector3 position = Vector3.Lerp(base.transform.position, this.targetPosition + this.driftVector * this.newTargetAction.Ratio(), t);
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(base.transform.position, Vector3.down), out raycastHit, 50f, 4194305))
			{
				position.y = Mathf.Lerp(base.transform.position.y, Mathf.Max(raycastHit.point.y + 50f, WaterLevel.GetHeight()), Time.deltaTime * 10f);
			}
			base.transform.position = position;
			if (!this.tractorBeamAction.TrueDone())
			{
				goto IL_18A;
			}
			try
			{
				this.UpdateTurret();
				goto IL_18A;
			}
			catch (Exception)
			{
				goto IL_18A;
			}
		}
		this.explosionMaterial.SetColor("_TintColor", new Color(0.6f, 0.6f, 1f, Mathf.Sin(this.deathExplosionAction.Ratio() * 3.1415927f)));
		if (this.triggerShakeAction.Done() && !this.triggeredShake)
		{
			this.triggeredShake = true;
			PlayerFpParent.instance.ApplyScreenshake(10f, 8);
		}
		if (this.deathExplosionAction.TrueDone())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		IL_18A:
		this.tractorBeamEffect.SetActive(!this.dead && !this.tractorBeamAction.TrueDone());
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00089ACC File Offset: 0x00087CCC
	private void FixedUpdate()
	{
		if (!this.tractorBeamAction.TrueDone())
		{
			try
			{
				this.UpdateTractorBeam();
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x00089B04 File Offset: 0x00087D04
	private void UpdateTractorBeam()
	{
		foreach (Actor actor in ActorManager.instance.actors)
		{
			bool flag = (base.transform.position - actor.Position()).ToGround().magnitude < 35f;
			if (flag && !actor.dead && !actor.fallenOver && (!actor.IsSeated() || !actor.seat.enclosed))
			{
				actor.FallOver();
			}
			if (flag && (actor.dead || actor.fallenOver))
			{
				this.BeamRigidbody(actor.ragdoll.MainRigidbody(), 90f);
			}
		}
		foreach (Vehicle vehicle in ActorManager.instance.vehicles)
		{
			if (vehicle.rigidbody == null)
			{
				break;
			}
			if ((base.transform.position - vehicle.transform.position).ToGround().magnitude < 35f)
			{
				this.BeamRigidbody(vehicle.rigidbody, 50f);
			}
		}
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00089C70 File Offset: 0x00087E70
	private void BeamRigidbody(Rigidbody rigidbody, float force)
	{
		Vector3 force2 = (base.transform.position - rigidbody.transform.position).normalized * force - rigidbody.velocity * 4f;
		rigidbody.AddForce(force2, ForceMode.Acceleration);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00089CC4 File Offset: 0x00087EC4
	private void UpdateTurret()
	{
		if (this.burstCooldownAction.TrueDone() && this.burstNumber == 0)
		{
			this.burstNumber = UnityEngine.Random.Range(2, 6);
		}
		Actor actor = null;
		float num = 1000f;
		foreach (Actor actor2 in ActorManager.instance.actors)
		{
			if (!actor2.dead)
			{
				float num2 = Vector3.Distance(base.transform.position, actor2.Position());
				if (num2 < num)
				{
					num = num2;
					actor = actor2;
				}
			}
		}
		if (actor != null)
		{
			Vector3 vector = actor.CenterPosition() - this.turret.position;
			if (this.tractorBeamCooldown.TrueDone() && vector.ToGround().magnitude < 20f)
			{
				this.tractorBeamAction.Start();
				this.tractorBeamCooldown.Start();
				return;
			}
			this.turret.transform.rotation = Quaternion.RotateTowards(this.turret.transform.rotation, Quaternion.LookRotation(vector), Time.deltaTime * 1000f);
			if (Vector3.ProjectOnPlane(vector, this.turret.transform.forward).magnitude < 3f && this.cooldownAction.TrueDone() && this.burstNumber > 0)
			{
				this.Fire();
			}
		}
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00089E44 File Offset: 0x00088044
	private void Fire()
	{
		this.cooldownAction.Start();
		UnityEngine.Object.Instantiate<GameObject>(this.projectilePrefab, this.muzzle.position, this.muzzle.rotation);
		this.muzzleFlash.Play();
		this.fireSound.Play();
		this.burstNumber--;
		this.burstCooldownAction.StartLifetime(UnityEngine.Random.Range(1f, 6f));
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0000D313 File Offset: 0x0000B513
	public void Damage()
	{
		if (this.dead)
		{
			return;
		}
		this.health--;
		if (this.health <= 0)
		{
			this.Die();
			return;
		}
		this.damageParticles.Play();
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00089EBC File Offset: 0x000880BC
	private void Die()
	{
		this.explosionParticles.Play();
		this.triggerShakeAction.Start();
		base.GetComponent<Animation>().Play("Ufo Explosion", PlayMode.StopAll);
		GameObject[] array = this.deactivateOnDeath;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.dead = true;
		this.deathExplosionAction.Start();
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = false;
		component.AddTorque(UnityEngine.Random.insideUnitSphere, ForceMode.VelocityChange);
		AudioSource component2 = base.GetComponent<AudioSource>();
		component2.Stop();
		component2.clip = this.explosionClip;
		component2.PlayDelayed(1.5f);
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00089F5C File Offset: 0x0008815C
	private void UpdateTargetPosition()
	{
		this.driftVector = UnityEngine.Random.insideUnitSphere.ToGround() * 100f;
		this.newTargetAction.StartLifetime(UnityEngine.Random.Range(1f, 2f));
		this.targetPosition = new Vector3(UnityEngine.Random.Range(this.bounds.min.x, this.bounds.max.x), UnityEngine.Random.Range(this.bounds.min.y, this.bounds.max.y), UnityEngine.Random.Range(this.bounds.min.z, this.bounds.max.z));
		float maxLength = 20f;
		if (this.tractorBeamAction.TrueDone() && UnityEngine.Random.Range(0f, 1f) < 0.1f)
		{
			maxLength = 200f;
		}
		Vector3 b = Vector3.ClampMagnitude(this.targetPosition - base.transform.position, maxLength);
		this.targetPosition = base.transform.position + b;
		this.targetPosition.y = this.bounds.max.y;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(this.targetPosition + new Vector3(0f, 10f, 0f), Vector3.down), out raycastHit, 99999f, 4194305))
		{
			this.targetPosition.y = Mathf.Max(raycastHit.point.y + 80f, WaterLevel.GetHeight());
		}
	}

	// Token: 0x040011A1 RID: 4513
	private const float CRUISE_HEIGHT = 80f;

	// Token: 0x040011A2 RID: 4514
	private const float MIN_HEIGHT = 50f;

	// Token: 0x040011A3 RID: 4515
	private const float TRACTOR_ACTIVE_LERP_SPEED = 0.2f;

	// Token: 0x040011A4 RID: 4516
	private const float LERP_SPEED = 2f;

	// Token: 0x040011A5 RID: 4517
	private const float HEIGHT_LERP_SPEED = 10f;

	// Token: 0x040011A6 RID: 4518
	private const float AIM_ROTATION_SPEED = 1000f;

	// Token: 0x040011A7 RID: 4519
	private const float TRACTOR_RANGE = 35f;

	// Token: 0x040011A8 RID: 4520
	private const float TRACTOR_START_RANGE = 20f;

	// Token: 0x040011A9 RID: 4521
	private const float TRACTOR_FORCE_ACTOR = 90f;

	// Token: 0x040011AA RID: 4522
	private const float TRACTOR_FORCE_VEHICLE = 50f;

	// Token: 0x040011AB RID: 4523
	private const float TRACTOR_DRAG_FORCE = 4f;

	// Token: 0x040011AC RID: 4524
	private const float JUMP_DISTANCE = 20f;

	// Token: 0x040011AD RID: 4525
	private const float LONG_JUMP_DISTANCE = 200f;

	// Token: 0x040011AE RID: 4526
	private const float LONG_JUMP_CHANCE = 0.1f;

	// Token: 0x040011AF RID: 4527
	public Transform turret;

	// Token: 0x040011B0 RID: 4528
	public Transform muzzle;

	// Token: 0x040011B1 RID: 4529
	public GameObject projectilePrefab;

	// Token: 0x040011B2 RID: 4530
	public GameObject tractorBeamEffect;

	// Token: 0x040011B3 RID: 4531
	public GameObject[] deactivateOnDeath;

	// Token: 0x040011B4 RID: 4532
	public ParticleSystem muzzleFlash;

	// Token: 0x040011B5 RID: 4533
	public AudioSource fireSound;

	// Token: 0x040011B6 RID: 4534
	public ParticleSystem damageParticles;

	// Token: 0x040011B7 RID: 4535
	public ParticleSystem explosionParticles;

	// Token: 0x040011B8 RID: 4536
	private Bounds bounds;

	// Token: 0x040011B9 RID: 4537
	private Vector3 targetPosition;

	// Token: 0x040011BA RID: 4538
	private TimedAction newTargetAction = new TimedAction(2f, false);

	// Token: 0x040011BB RID: 4539
	private TimedAction cooldownAction = new TimedAction(0.3f, false);

	// Token: 0x040011BC RID: 4540
	private TimedAction burstCooldownAction = new TimedAction(1.4f, false);

	// Token: 0x040011BD RID: 4541
	private TimedAction tractorBeamAction = new TimedAction(13f, false);

	// Token: 0x040011BE RID: 4542
	private TimedAction tractorBeamCooldown = new TimedAction(25f, false);

	// Token: 0x040011BF RID: 4543
	private int burstNumber;

	// Token: 0x040011C0 RID: 4544
	private Vector3 driftVector;

	// Token: 0x040011C1 RID: 4545
	public int health = 1;

	// Token: 0x040011C2 RID: 4546
	private bool dead;

	// Token: 0x040011C3 RID: 4547
	private bool triggeredShake;

	// Token: 0x040011C4 RID: 4548
	private TimedAction deathExplosionAction = new TimedAction(6f, false);

	// Token: 0x040011C5 RID: 4549
	private TimedAction triggerShakeAction = new TimedAction(3f, false);

	// Token: 0x040011C6 RID: 4550
	public AudioClip explosionClip;

	// Token: 0x040011C7 RID: 4551
	public Renderer explosionRenderer;

	// Token: 0x040011C8 RID: 4552
	private Material explosionMaterial;
}

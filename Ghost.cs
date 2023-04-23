using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class Ghost : Hurtable
{
	// Token: 0x0600102C RID: 4140 RVA: 0x00087DD8 File Offset: 0x00085FD8
	private void Start()
	{
		this.spawnPoint = base.transform.position;
		this.team = -1;
		this.explodeAudio = this.explodeParticles.GetComponent<AudioSource>();
		this.coreParticles.Stop();
		this.chargeParticles.Stop();
		this.burstParticles.Stop();
		this.seeker = base.GetComponent<Seeker>();
		this.stunAction.StartLifetime(5f);
		base.StartCoroutine(this.Roam());
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x00087E58 File Offset: 0x00086058
	private void Update()
	{
		this.animator.transform.localPosition = new Vector3(0f, Mathf.Sin(0.7f * Time.time) * 0.1f + 0.1f, 0f);
		bool flag = this.pathNodes.Count > 0;
		if (!this.isRoamer && this.stunAction.TrueDone())
		{
			this.UpdateCombat();
		}
		if (flag)
		{
			Vector3 forward = (this.pathNodes[0] - base.transform.position).ToGround();
			float magnitude = forward.magnitude;
			Vector3 vector = base.transform.position + forward.normalized * 1.5f * Time.deltaTime;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(vector + new Vector3(0f, 2f, 0f), Vector3.down), out raycastHit, 6f, 2228225))
			{
				vector = raycastHit.point;
			}
			base.transform.position = vector;
			if (!this.isInCombat)
			{
				Quaternion to = Quaternion.LookRotation(forward);
				base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, 80f * Time.deltaTime);
			}
			if (magnitude >= this.lastNodeDistance || magnitude < 0.1f)
			{
				this.pathNodes.RemoveAt(0);
				this.lastNodeDistance = float.MaxValue;
			}
			else
			{
				this.lastNodeDistance = magnitude;
			}
		}
		this.animator.SetBool("moving", flag && !this.isInCombat);
		this.animator.SetBool("combat", this.isInCombat);
		if (this.justTookDamageAction.TrueDone())
		{
			this.push = Mathf.MoveTowards(this.push, 0f, 0.5f * Time.deltaTime);
		}
		this.animator.SetFloat("push", this.push);
		if (this.isCharging && this.push >= 1f)
		{
			this.TakeDamage();
		}
		this.hitbox.SetActive(this.isCharging);
		if (this.isRoamer && (Vector3.Distance(FpsActorController.instance.actor.Position(), base.transform.position) < 60f || FpsActorController.instance.inPhotoMode))
		{
			this.Explode(true);
		}
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000880C0 File Offset: 0x000862C0
	private void UpdateCombat()
	{
		Vector3 v = FpsActorController.instance.actor.Position() - base.transform.position;
		float magnitude = v.magnitude;
		if (this.isInCombat && ((!this.isCharging && magnitude > 60f) || FpsActorController.instance.actor.dead))
		{
			this.EndCombat();
		}
		else if (!this.isInCombat && magnitude < 30f && !FpsActorController.instance.actor.dead)
		{
			this.StartCombat();
		}
		if (this.isInCombat)
		{
			Quaternion to = Quaternion.LookRotation(v.ToGround());
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, 120f * Time.deltaTime);
		}
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x0000CF87 File Offset: 0x0000B187
	private void StartCombat()
	{
		this.isInCombat = true;
		this.combatCoroutine = base.StartCoroutine(this.Combat());
		this.coreParticles.Play();
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x00088188 File Offset: 0x00086388
	private void TakeDamage()
	{
		this.health--;
		if (this.health <= 0)
		{
			this.Explode(false);
			if (SpiritTrack.instance != null)
			{
				SpiritTrack.instance.NextTrack();
				return;
			}
		}
		else
		{
			this.stunAction.StartLifetime(3f);
			PlayerFpParent.instance.ApplyScreenshake(15f, 10);
			this.audio.PlayOneShot(this.takeDamageSound);
			this.explodeParticles.Play();
			this.EndCombat();
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00088210 File Offset: 0x00086410
	public void Explode(bool silent)
	{
		this.explodeParticles.transform.parent = null;
		this.explodeParticles.gameObject.AddComponent<Lifetime>().lifetime = 10f;
		this.explodeParticles.Play();
		if (!silent)
		{
			this.explodeAudio.Play();
			PlayerFpParent.instance.ApplyScreenshake(50f, 20);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x00088280 File Offset: 0x00086480
	private void EndCombat()
	{
		this.isInCombat = false;
		this.isCharging = false;
		this.animator.SetBool("charge", false);
		if (this.combatCoroutine != null)
		{
			base.StopCoroutine(this.combatCoroutine);
			this.combatCoroutine = null;
		}
		this.coreParticles.Stop();
		this.chargeParticles.Stop();
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0000CFAD File Offset: 0x0000B1AD
	public override bool Damage(DamageInfo damageInfo)
	{
		this.push += 0.07f;
		this.justTookDamageAction.Start();
		this.takeDamageParticles.Emit(10);
		return true;
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0000CFDA File Offset: 0x0000B1DA
	private IEnumerator Combat()
	{
		for (;;)
		{
			yield return new WaitForSeconds(4f);
			int nSmallAttacks = UnityEngine.Random.Range(2, 5);
			int num;
			for (int i = 0; i < nSmallAttacks; i = num + 1)
			{
				this.animator.SetTrigger("attack");
				this.coreParticles.Stop();
				this.burstParticles.Play();
				yield return new WaitForSeconds(0.1f);
				this.audio.PlayOneShot(this.burstFire);
				this.coreParticles.Play();
				base.StartCoroutine(this.FireLightProjectileBurst());
				yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f, 3f));
				num = i;
			}
			this.animator.SetBool("charge", true);
			this.isCharging = true;
			this.chargeParticles.Play();
			yield return new WaitForSeconds(5f);
			this.chargeParticles.Stop();
			this.coreParticles.Stop();
			this.isCharging = false;
			this.animator.SetTrigger("attack");
			this.burstParticles.Play();
			this.audio.PlayOneShot(this.chargeFire);
			yield return new WaitForSeconds(0.1f);
			this.FireChargedProjectile();
			this.animator.SetBool("charge", false);
			this.coreParticles.Play();
		}
		yield break;
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0000CFE9 File Offset: 0x0000B1E9
	private IEnumerator FireLightProjectileBurst()
	{
		Vector3 direction = (FpsActorController.instance.actor.Position() - this.core.position).normalized;
		int num;
		for (int i = 0; i < 12; i = num + 1)
		{
			yield return new WaitForSeconds(0.03f);
			UnityEngine.Object.Instantiate<GameObject>(this.lightProjectile, this.core.transform.position, Quaternion.LookRotation(direction + UnityEngine.Random.insideUnitSphere * 0.3f));
			num = i;
		}
		yield break;
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x000882E0 File Offset: 0x000864E0
	private void FireChargedProjectile()
	{
		Vector3 normalized = (FpsActorController.instance.actor.Position() - this.core.position).normalized;
		UnityEngine.Object.Instantiate<GameObject>(this.heavyProjectile, this.core.transform.position, Quaternion.LookRotation(normalized + UnityEngine.Random.insideUnitSphere * 0.005f));
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
	private IEnumerator Roam()
	{
		yield return new WaitForSeconds(2f);
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 7f));
			if (this.pathNodes.Count == 0)
			{
				this.StartPath();
			}
		}
		yield break;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0008834C File Offset: 0x0008654C
	private void StartPath()
	{
		this.pathNodes.Clear();
		Vector3 end = (this.isRoamer ? base.transform.position : this.spawnPoint) + UnityEngine.Random.insideUnitSphere.ToGround() * 30f;
		this.seeker.StartPath(base.transform.position, end, new OnPathDelegate(this.OnPathCompleted), PathfindingManager.infantryGraphMask);
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0000D007 File Offset: 0x0000B207
	private void OnPathCompleted(Path path)
	{
		if (path.error)
		{
			return;
		}
		this.lastNodeDistance = float.MaxValue;
		this.pathNodes = new List<Vector3>(path.vectorPath);
	}

	// Token: 0x0400111A RID: 4378
	private const float ROAM_MAX_DISTANCE = 30f;

	// Token: 0x0400111B RID: 4379
	private const float SIN_BOB_AMPLITUDE = 0.1f;

	// Token: 0x0400111C RID: 4380
	private const float SIN_BOB_FREQUENCY = 0.7f;

	// Token: 0x0400111D RID: 4381
	private const float MIN_ROAM_TIME = 3f;

	// Token: 0x0400111E RID: 4382
	private const float MAX_ROAM_TIME = 7f;

	// Token: 0x0400111F RID: 4383
	private const float TURN_SPEED = 80f;

	// Token: 0x04001120 RID: 4384
	private const float TURN_SPEED_COMBAT = 120f;

	// Token: 0x04001121 RID: 4385
	private const float MOVEMENT_SPEED = 1.5f;

	// Token: 0x04001122 RID: 4386
	private const float ENTER_COMBAT_RANGE = 30f;

	// Token: 0x04001123 RID: 4387
	private const float EXIT_COMBAT_RANGE = 60f;

	// Token: 0x04001124 RID: 4388
	private const float PUSH_PER_HIT = 0.07f;

	// Token: 0x04001125 RID: 4389
	private const float PUSH_RETURN_SPEED = 0.5f;

	// Token: 0x04001126 RID: 4390
	private const float COMBAT_COOLDOWN_AFTER_CHARGE_ATTACK = 4f;

	// Token: 0x04001127 RID: 4391
	private const float COMBAT_COOLDOWN_AFTER_SMALL_ATTACK_MIN = 1.5f;

	// Token: 0x04001128 RID: 4392
	private const float COMBAT_COOLDOWN_AFTER_SMALL_ATTACK_MAX = 3f;

	// Token: 0x04001129 RID: 4393
	private const float COMBAT_CHARGE_TIME = 5f;

	// Token: 0x0400112A RID: 4394
	private const int COMBAT_SMALL_ATTACKS_MIN = 2;

	// Token: 0x0400112B RID: 4395
	private const int COMBAT_SMALL_ATTACKS_MAX = 5;

	// Token: 0x0400112C RID: 4396
	private const int PROJECTILE_BURST_NUMBER = 12;

	// Token: 0x0400112D RID: 4397
	private const int MOVEMENT_HIT_MASK = 2228225;

	// Token: 0x0400112E RID: 4398
	private List<Vector3> pathNodes = new List<Vector3>();

	// Token: 0x0400112F RID: 4399
	private Vector3 spawnPoint;

	// Token: 0x04001130 RID: 4400
	private Coroutine combatCoroutine;

	// Token: 0x04001131 RID: 4401
	public Animator animator;

	// Token: 0x04001132 RID: 4402
	private Seeker seeker;

	// Token: 0x04001133 RID: 4403
	private float lastNodeDistance = float.MaxValue;

	// Token: 0x04001134 RID: 4404
	private float push;

	// Token: 0x04001135 RID: 4405
	private TimedAction justTookDamageAction = new TimedAction(0.3f, false);

	// Token: 0x04001136 RID: 4406
	private TimedAction stunAction = new TimedAction(5f, false);

	// Token: 0x04001137 RID: 4407
	private bool isInCombat;

	// Token: 0x04001138 RID: 4408
	private bool isCharging;

	// Token: 0x04001139 RID: 4409
	public AudioSource audio;

	// Token: 0x0400113A RID: 4410
	public AudioClip burstFire;

	// Token: 0x0400113B RID: 4411
	public AudioClip chargeFire;

	// Token: 0x0400113C RID: 4412
	public AudioClip takeDamageSound;

	// Token: 0x0400113D RID: 4413
	private AudioSource explodeAudio;

	// Token: 0x0400113E RID: 4414
	public Transform core;

	// Token: 0x0400113F RID: 4415
	public ParticleSystem coreParticles;

	// Token: 0x04001140 RID: 4416
	public ParticleSystem chargeParticles;

	// Token: 0x04001141 RID: 4417
	public ParticleSystem burstParticles;

	// Token: 0x04001142 RID: 4418
	public ParticleSystem takeDamageParticles;

	// Token: 0x04001143 RID: 4419
	public ParticleSystem explodeParticles;

	// Token: 0x04001144 RID: 4420
	public GameObject hitbox;

	// Token: 0x04001145 RID: 4421
	public GameObject lightProjectile;

	// Token: 0x04001146 RID: 4422
	public GameObject heavyProjectile;

	// Token: 0x04001147 RID: 4423
	public bool isRoamer;

	// Token: 0x04001148 RID: 4424
	private int health = 3;
}

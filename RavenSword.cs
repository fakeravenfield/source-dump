using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class RavenSword : MeleeWeapon
{
	// Token: 0x06000765 RID: 1893 RVA: 0x00063144 File Offset: 0x00061344
	protected override void Update()
	{
		base.Update();
		if (base.user == null || !base.UserIsPlayer())
		{
			return;
		}
		this.light.enabled = !this.unholsterAction.TrueDone();
		this.light.transform.localPosition = new Vector3(0.1f, 0.2f, this.unholsterAction.Ratio() * 4f - 0.7f);
		bool flag = !base.CoolingDown() && this.unholstered && base.user.controller.IsSprinting();
		bool flag2 = Vector3.Dot(base.user.Velocity(), base.transform.forward) > 2f;
		if (!flag)
		{
			if (this.HasSkeweredActor())
			{
				this.Unskewer();
			}
			this.prepareSkewerAction.Start();
		}
		if (flag2 && this.prepareSkewerAction.TrueDone())
		{
			this.UpdateSkewer();
		}
		if (this.HasSkeweredActor() && this.shakeAction.TrueDone() && flag2)
		{
			this.shakeAction.Start();
			PlayerFpParent.instance.ApplyScreenshake(1f, 2);
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0006326C File Offset: 0x0006146C
	private void UpdateSkewer()
	{
		Matrix4x4 inverse = Matrix4x4.TRS(this.thirdPersonTransform.position, Quaternion.LookRotation(base.transform.forward), Vector3.one).inverse;
		foreach (Actor actor in ActorManager.AliveActorsOnTeam(1 - base.user.team))
		{
			Vector3 vector = inverse.MultiplyPoint(actor.CenterPosition());
			if (!actor.fallenOver && !actor.IsSeated() && vector.z > 0.3f && vector.z < 2.4f && Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.7f)
			{
				if (this.HasSkeweredActor())
				{
					this.Ram(actor);
					return;
				}
				this.Skewer(actor);
				return;
			}
		}
		foreach (Actor actor2 in ActorManager.AliveActorsOnTeam(base.user.team))
		{
			if (actor2.aiControlled)
			{
				Vector3 vector2 = inverse.MultiplyPoint(actor2.CenterPosition());
				if (!actor2.fallenOver && !actor2.IsSeated() && vector2.z > 0.3f && vector2.z < 2.4f && Mathf.Abs(vector2.x) < 0.5f && Mathf.Abs(vector2.y) < 0.7f)
				{
					if (this.HasSkeweredActor())
					{
						this.Ram(actor2);
						break;
					}
					this.Skewer(actor2);
					break;
				}
			}
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00006BF8 File Offset: 0x00004DF8
	private bool HasSkeweredActor()
	{
		return this.skeweredActor != null;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00063454 File Offset: 0x00061654
	private void Ram(Actor actor)
	{
		Vector3 vector = actor.Position() - base.user.Position();
		actor.KnockOver(vector.normalized * 400f);
		PlayerFpParent.instance.ApplyScreenshake(13f, 3);
		PlayerFpParent.instance.ApplyRecoil(base.transform.worldToLocalMatrix.MultiplyVector(vector.normalized * -3f), true);
		this.audio.PlayOneShot(this.ramSound);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000634E0 File Offset: 0x000616E0
	private void Skewer(Actor actor)
	{
		this.shakeAction.Start();
		PlayerFpParent.instance.ApplyScreenshake(13f, 3);
		this.audio.PlayOneShot(this.skewerSound);
		this.skeweredActor = actor;
		this.sprintBobMultiplier = 0.15f;
		this.skeweredSoldier.SetActive(true);
		this.skeweredSoldierMeshRenderer.sharedMesh = actor.skinnedRenderer.sharedMesh;
		this.skeweredSoldierMeshRenderer.sharedMaterials = actor.skinnedRenderer.sharedMaterials;
		this.skeweredSoldierMeshRenderer.localBounds = new Bounds(Vector3.zero, new Vector3(100f, 100f, 100f));
		this.skeweredActor.ForceStance(Actor.Stance.Stand);
		this.skeweredActor.Deactivate();
		this.animator.SetBool("skewer", true);
		this.animator.SetTrigger("stab");
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000635C4 File Offset: 0x000617C4
	private void Unskewer()
	{
		PlayerFpParent.instance.ApplyScreenshake(7f, 3);
		this.audio.PlayOneShot(this.tossSound);
		this.skeweredActor.Activate();
		this.skeweredActor.SetPositionAndRotation(this.thirdPersonTransform.position + base.transform.forward, SMath.LookRotationRespectUp(base.transform.forward, Vector3.up));
		DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Melee, base.user, this)
		{
			healthDamage = 200f,
			point = this.thirdPersonTransform.position,
			direction = base.transform.forward,
			impactForce = base.transform.forward * 600f,
			isCriticalHit = true
		};
		this.skeweredActor.Damage(info);
		this.skeweredSoldier.SetActive(false);
		this.skeweredActor = null;
		this.sprintBobMultiplier = 1f;
		base.StartCoroutine(this.UnskewerRoutine());
		this.animator.ResetTrigger("stab");
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00006C06 File Offset: 0x00004E06
	private IEnumerator UnskewerRoutine()
	{
		yield return new WaitForSeconds(0.1f);
		this.animator.SetBool("skewer", false);
		yield break;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00006C15 File Offset: 0x00004E15
	public override void Holster()
	{
		if (this.HasSkeweredActor())
		{
			this.Unskewer();
		}
		base.Holster();
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00006C2B File Offset: 0x00004E2B
	public override void Unholster()
	{
		base.Unholster();
		this.unholsterAction.Start();
	}

	// Token: 0x04000786 RID: 1926
	private const float SKEWER_RANGE = 2.4f;

	// Token: 0x04000787 RID: 1927
	private const float SKEWER_RELEASE_FORCE = 600f;

	// Token: 0x04000788 RID: 1928
	private const float RAM_FORCE = 400f;

	// Token: 0x04000789 RID: 1929
	public GameObject skeweredSoldier;

	// Token: 0x0400078A RID: 1930
	public SkinnedMeshRenderer skeweredSoldierMeshRenderer;

	// Token: 0x0400078B RID: 1931
	public AudioClip skewerSound;

	// Token: 0x0400078C RID: 1932
	public AudioClip tossSound;

	// Token: 0x0400078D RID: 1933
	public AudioClip ramSound;

	// Token: 0x0400078E RID: 1934
	public Light light;

	// Token: 0x0400078F RID: 1935
	private TimedAction unholsterAction = new TimedAction(1.5f, false);

	// Token: 0x04000790 RID: 1936
	private TimedAction prepareSkewerAction = new TimedAction(0.65f, false);

	// Token: 0x04000791 RID: 1937
	private TimedAction shakeAction = new TimedAction(0.5f, false);

	// Token: 0x04000792 RID: 1938
	private Actor skeweredActor;
}

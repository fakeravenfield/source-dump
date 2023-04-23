using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class MeleeWeapon : Weapon
{
	// Token: 0x0600070F RID: 1807 RVA: 0x000068B0 File Offset: 0x00004AB0
	protected override void SetupTeammateDangerRange(Projectile projectile)
	{
		this.teammateDangerRange = -1f;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000068BD File Offset: 0x00004ABD
	protected override bool Shoot(Vector3 direction, bool useMuzzleDirection)
	{
		if (!base.Shoot(direction, useMuzzleDirection))
		{
			return false;
		}
		base.StartCoroutine(this.Swing());
		return true;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00002FD8 File Offset: 0x000011D8
	protected override Projectile SpawnProjectile(Vector3 direction, Vector3 muzzlePosition, bool hasUser)
	{
		return null;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x000068D9 File Offset: 0x00004AD9
	private IEnumerator Swing()
	{
		if (base.user != null)
		{
			base.user.EmoteSwing();
		}
		yield return new WaitForSeconds(this.swingTime);
		if (base.user != null)
		{
			this.ShootMeleeRay();
		}
		yield break;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x000614E4 File Offset: 0x0005F6E4
	public void SphereCast(Ray ray, float distance, int mask, out RaycastHit hitWall, out MeleeWeapon.RaycastHitHitbox hitEnemy, out MeleeWeapon.RaycastHitHitbox hitAlly, out RaycastHit hitVehicle)
	{
		hitWall = new RaycastHit
		{
			distance = float.MaxValue
		};
		hitEnemy = new MeleeWeapon.RaycastHitHitbox
		{
			hitInfo = new RaycastHit
			{
				distance = float.MaxValue
			}
		};
		hitAlly = new MeleeWeapon.RaycastHitHitbox
		{
			hitInfo = new RaycastHit
			{
				distance = float.MaxValue
			}
		};
		hitVehicle = new RaycastHit
		{
			distance = float.MaxValue
		};
		int num = Physics.SphereCastNonAlloc(ray, this.radius, MeleeWeapon.hitResults, distance, mask);
		int i = 0;
		while (i < num)
		{
			RaycastHit raycastHit = MeleeWeapon.hitResults[i];
			int layer = raycastHit.collider.gameObject.layer;
			if (Hitbox.IsHitboxLayer(layer))
			{
				try
				{
					Hitbox component = raycastHit.collider.GetComponent<Hitbox>();
					if (component.parent == base.user)
					{
						goto IL_1A5;
					}
					MeleeWeapon.RaycastHitHitbox b = new MeleeWeapon.RaycastHitHitbox
					{
						hitInfo = raycastHit,
						hitbox = component
					};
					if (component.parent.team == base.user.team)
					{
						hitAlly = this.Closest(hitAlly, b);
					}
					else
					{
						hitEnemy = this.Closest(hitEnemy, b);
					}
					goto IL_1A5;
				}
				catch (Exception e)
				{
					ModManager.HandleModException(e);
					goto IL_1A5;
				}
				goto IL_16B;
			}
			goto IL_16B;
			IL_1A5:
			i++;
			continue;
			IL_16B:
			if (layer == 12)
			{
				hitVehicle = this.Closest(hitVehicle, raycastHit);
				goto IL_1A5;
			}
			if (layer != 18)
			{
				hitWall = this.Closest(hitWall, raycastHit);
				goto IL_1A5;
			}
			goto IL_1A5;
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x000068E8 File Offset: 0x00004AE8
	public RaycastHit Closest(RaycastHit a, RaycastHit b)
	{
		if (a.distance < b.distance)
		{
			return a;
		}
		return b;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000068FD File Offset: 0x00004AFD
	public MeleeWeapon.RaycastHitHitbox Closest(MeleeWeapon.RaycastHitHitbox a, MeleeWeapon.RaycastHitHitbox b)
	{
		if (a.hitInfo.distance < b.hitInfo.distance)
		{
			return a;
		}
		return b;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x000616B4 File Offset: 0x0005F8B4
	protected virtual void ShootMeleeRay()
	{
		float num = this.radius * 2f;
		Vector3 vector = base.user.controller.FacingDirection();
		Ray ray = new Ray(base.transform.position - vector * num, base.user.controller.FacingDirection());
		bool flag = base.user.DisableHitboxColliders();
		RaycastHit raycastHit;
		MeleeWeapon.RaycastHitHitbox hit;
		MeleeWeapon.RaycastHitHitbox hit2;
		RaycastHit raycastHit2;
		this.SphereCast(ray, this.range + num, 70912, out raycastHit, out hit, out hit2, out raycastHit2);
		float num2 = Mathf.Min(raycastHit.distance, raycastHit2.distance);
		if (this.HitIsValid(hit) && hit.hitInfo.distance < num2)
		{
			this.DamageHitbox(vector, hit);
		}
		else if (this.HitIsValid(hit2) && hit2.hitInfo.distance < num2)
		{
			if (base.UserIsPlayer())
			{
				this.DamageHitbox(vector, hit2);
			}
		}
		else if (this.HitIsValid(raycastHit2))
		{
			this.HitVehicle(vector, raycastHit2);
		}
		else if (this.HitIsValid(raycastHit))
		{
			Rigidbody attachedRigidbody = raycastHit.collider.attachedRigidbody;
			if (attachedRigidbody != null)
			{
				this.HitRigidbody(vector, attachedRigidbody, raycastHit);
			}
		}
		if (flag)
		{
			base.user.EnableHitboxColliders();
		}
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x000617E8 File Offset: 0x0005F9E8
	protected virtual void HitVehicle(Vector3 direction, RaycastHit hitInfo)
	{
		Rigidbody attachedRigidbody = hitInfo.collider.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			this.HitRigidbody(direction, attachedRigidbody, hitInfo);
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0000691C File Offset: 0x00004B1C
	protected void HitRigidbody(Vector3 direction, Rigidbody rigidbody, RaycastHit hitInfo)
	{
		rigidbody.AddForceAtPosition(direction * this.force, hitInfo.point, ForceMode.Impulse);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00061814 File Offset: 0x0005FA14
	private void DamageHitbox(Vector3 direction, MeleeWeapon.RaycastHitHitbox hit)
	{
		this.audio.PlayOneShot(this.hitSound);
		Vector3 impactForce = direction * this.force;
		DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Melee, base.user, this)
		{
			healthDamage = this.damage,
			balanceDamage = this.balanceDamage,
			point = hit.hitInfo.point,
			direction = direction,
			impactForce = impactForce
		};
		hit.hitbox.parent.Damage(info);
		if (hit.hitInfo.collider.attachedRigidbody != null)
		{
			this.HitRigidbody(direction, hit.hitInfo.collider.attachedRigidbody, hit.hitInfo);
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00006938 File Offset: 0x00004B38
	private bool HitIsValid(RaycastHit hit)
	{
		return hit.collider != null;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00006947 File Offset: 0x00004B47
	private bool HitIsValid(MeleeWeapon.RaycastHitHitbox hit)
	{
		return this.HitIsValid(hit.hitInfo);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00006955 File Offset: 0x00004B55
	public override Transform CurrentMuzzle()
	{
		return base.transform;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsMeleeWeapon()
	{
		return true;
	}

	// Token: 0x0400071F RID: 1823
	private const int HIT_MASK = 70912;

	// Token: 0x04000720 RID: 1824
	public float radius = 0.4f;

	// Token: 0x04000721 RID: 1825
	public float range = 2f;

	// Token: 0x04000722 RID: 1826
	public float swingTime = 0.15f;

	// Token: 0x04000723 RID: 1827
	public float damage = 55f;

	// Token: 0x04000724 RID: 1828
	public float balanceDamage = 150f;

	// Token: 0x04000725 RID: 1829
	public float force = 100f;

	// Token: 0x04000726 RID: 1830
	public AudioClip hitSound;

	// Token: 0x04000727 RID: 1831
	private static RaycastHit[] hitResults = new RaycastHit[64];

	// Token: 0x020000F0 RID: 240
	public struct RaycastHitHitbox
	{
		// Token: 0x04000728 RID: 1832
		public RaycastHit hitInfo;

		// Token: 0x04000729 RID: 1833
		public Hitbox hitbox;
	}
}

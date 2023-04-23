using System;
using Lua;
using UnityEngine;

// Token: 0x020000FD RID: 253
[ResolveDynValueAs(typeof(ExplodingProjectile))]
public class RemoteDetonatedProjectile : ExplodingProjectile
{
	// Token: 0x06000775 RID: 1909 RVA: 0x00063748 File Offset: 0x00061948
	public override void StartTravelling()
	{
		base.StartTravelling();
		this.isDetonated = false;
		this.attachedVehicle = null;
		this.hitMask = -12947205;
		if (this.sourceWeapon != null)
		{
			RemoteDetonatorWeapon remoteDetonatorWeapon = this.sourceWeapon as RemoteDetonatorWeapon;
			if (remoteDetonatorWeapon != null)
			{
				remoteDetonatorWeapon.RegisterProjectile(this);
			}
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x000637A0 File Offset: 0x000619A0
	protected override Projectile.HitType Hit(Ray ray, RaycastHit hitInfo, out bool showHitIndicator)
	{
		Projectile.HitType hitType = base.Hit(ray, hitInfo, out showHitIndicator);
		if (hitType == Projectile.HitType.Stop || hitType == Projectile.HitType.StopNoDecal)
		{
			this.Stick(hitInfo);
			showHitIndicator = false;
			return Projectile.HitType.StopNoDecal;
		}
		return hitType;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x000637CC File Offset: 0x000619CC
	private void Stick(RaycastHit hitInfo)
	{
		base.enabled = false;
		base.transform.position = hitInfo.point;
		base.transform.rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up) * Quaternion.Euler(this.stickRotationOffset);
		this.attachedDestructible = hitInfo.collider.GetComponent<Destructible>();
		if (hitInfo.rigidbody != null)
		{
			this.attachedVehicle = hitInfo.rigidbody.GetComponent<Vehicle>();
		}
		base.transform.parent = hitInfo.transform;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00006C90 File Offset: 0x00004E90
	protected override bool OnWaterSurfaceDetonation(Vector3 enterPosition)
	{
		base.transform.position = enterPosition;
		this.Detonate();
		return false;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0000257D File Offset: 0x0000077D
	protected override bool Explode(Vector3 position, Vector3 up)
	{
		return false;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00006CA5 File Offset: 0x00004EA5
	public override float Damage()
	{
		return this.configuration.damage;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00006CB2 File Offset: 0x00004EB2
	public override float BalanceDamage()
	{
		return this.configuration.balanceDamage;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0000257D File Offset: 0x0000077D
	protected override bool DealDamageOnHit()
	{
		return false;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00063864 File Offset: 0x00061A64
	public void Detonate()
	{
		if (this.isDetonated)
		{
			return;
		}
		this.isDetonated = true;
		if (this.sourceWeapon != null)
		{
			RemoteDetonatorWeapon remoteDetonatorWeapon = this.sourceWeapon as RemoteDetonatorWeapon;
			if (remoteDetonatorWeapon != null)
			{
				remoteDetonatorWeapon.DeregisterProjectile(this);
			}
		}
		if (this.attachedDestructible != null)
		{
			base.OnHitDestructible(this.attachedDestructible);
		}
		base.transform.rotation = base.transform.rotation * Quaternion.Euler(-this.stickRotationOffset);
		Vector3 forward = base.transform.forward;
		base.Explode(base.transform.position + forward * 0.3f, forward);
		if (this.attachedVehicle != null)
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.StickyExplosive, this.killCredit, this.sourceWeapon)
			{
				healthDamage = this.attachedVehicle.GetDamageMultiplier(this.armorDamage) * this.configuration.damage
			};
			this.attachedVehicle.Damage(info);
		}
		base.transform.parent = null;
	}

	// Token: 0x04000796 RID: 1942
	public const int STICK_HIT_MASK = -12947205;

	// Token: 0x04000797 RID: 1943
	public Vector3 stickRotationOffset;

	// Token: 0x04000798 RID: 1944
	private bool isDetonated;

	// Token: 0x04000799 RID: 1945
	private Vehicle attachedVehicle;

	// Token: 0x0400079A RID: 1946
	private Destructible attachedDestructible;

	// Token: 0x0400079B RID: 1947
	private Vector3 explosionPoint;
}

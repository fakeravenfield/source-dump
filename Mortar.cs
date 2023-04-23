using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class Mortar : MountedWeapon
{
	// Token: 0x06000726 RID: 1830 RVA: 0x000619AC File Offset: 0x0005FBAC
	protected override void Awake()
	{
		base.Awake();
		this.aimIndicator.SetActive(false);
		this.configuration.effectiveRange = this.maxRange;
		this.topArchTime = Mathf.Sqrt(-(2f * this.trajectoryHeight) / this.projectileGravity.y);
		this.speedVertical = -this.topArchTime * this.projectileGravity.y;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00006982 File Offset: 0x00004B82
	public override void Unholster()
	{
		base.Unholster();
		this.range = this.defaultRange;
		this.aimIndicator.SetActive(base.UserIsPlayer());
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x000069A7 File Offset: 0x00004BA7
	public override void Holster()
	{
		base.Holster();
		this.aimIndicator.SetActive(false);
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00061A1C File Offset: 0x0005FC1C
	protected override void Update()
	{
		base.Update();
		if (this.unholstered && base.user != null)
		{
			this.range = Mathf.Clamp(this.range + base.user.controller.RangeInput() * this.rangeChangeSpeed, this.minRange, this.maxRange);
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00061A7C File Offset: 0x0005FC7C
	private void LateUpdate()
	{
		if (this.aimIndicator.activeInHierarchy && base.user != null)
		{
			this.aimIndicator.transform.position = this.GetTargetPosition();
			this.aimIndicator.transform.rotation = Quaternion.identity;
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00061AD0 File Offset: 0x0005FCD0
	protected override Projectile SpawnProjectile(Vector3 direction, Vector3 muzzlePosition, bool hasUser)
	{
		Vector3 targetPosition = this.GetTargetPosition() + UnityEngine.Random.insideUnitSphere * this.targetSpreadRange;
		Vector3 velocity = Mortar.CalculateProjectileVelocity(muzzlePosition, targetPosition, this.trajectoryHeight, this.topArchTime, this.speedVertical, this.projectileGravity.y);
		Projectile projectile = base.SpawnProjectile(velocity.normalized, muzzlePosition, hasUser);
		projectile.velocity = velocity;
		return projectile;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00061B34 File Offset: 0x0005FD34
	public static Vector3 CalculateProjectileVelocity(Vector3 muzzlePosition, Vector3 targetPosition, float trajectoryHeight, float topArchTime, float speedVertical, float projectileGravity)
	{
		Vector3 vector = targetPosition - muzzlePosition;
		Vector3 vector2 = vector.ToGround();
		float magnitude = vector2.magnitude;
		float num = Mathf.Sqrt(-2f * (trajectoryHeight - vector.y) / projectileGravity);
		float d = magnitude / (topArchTime + num);
		Vector3 result = vector2.normalized * d;
		result.y = speedVertical;
		return result;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00061B90 File Offset: 0x0005FD90
	private Vector3 GetFlatAimDirection()
	{
		return base.user.controller.FacingDirection().ToGround().normalized;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00061BBC File Offset: 0x0005FDBC
	private Vector3 GetTargetPosition()
	{
		Vector3 vector;
		if (!base.UserIsPlayer() && base.user.controller.HasSpottedTarget())
		{
			Actor target = base.user.controller.GetTarget();
			vector = target.Position() + target.Velocity() * this.topArchTime * UnityEngine.Random.Range(1f, 2.05f);
		}
		else
		{
			vector = base.transform.position + this.GetFlatAimDirection() * this.range;
		}
		vector.y += this.CurrentMuzzle().position.y + this.trajectoryHeight;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(vector, Vector3.down), out raycastHit, 1000f, 1))
		{
			return raycastHit.point;
		}
		vector.y = WaterLevel.GetHeight();
		return vector;
	}

	// Token: 0x0400072D RID: 1837
	private const int INDICATOR_HIT_MASK = 1;

	// Token: 0x0400072E RID: 1838
	public float maxRange = 300f;

	// Token: 0x0400072F RID: 1839
	public float minRange = 10f;

	// Token: 0x04000730 RID: 1840
	public float defaultRange = 100f;

	// Token: 0x04000731 RID: 1841
	public float rangeChangeSpeed = 1f;

	// Token: 0x04000732 RID: 1842
	public float trajectoryHeight = 200f;

	// Token: 0x04000733 RID: 1843
	public float targetSpreadRange;

	// Token: 0x04000734 RID: 1844
	public GameObject aimIndicator;

	// Token: 0x04000735 RID: 1845
	private float topArchTime;

	// Token: 0x04000736 RID: 1846
	private float speedVertical;

	// Token: 0x04000737 RID: 1847
	private float range = 10f;
}

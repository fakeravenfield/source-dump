using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class TankTurret : MountedWeapon
{
	// Token: 0x060007E3 RID: 2019 RVA: 0x0000723F File Offset: 0x0000543F
	protected override void Awake()
	{
		base.Awake();
		this.rigidbody = base.GetComponentInParent<Rigidbody>();
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00065080 File Offset: 0x00063280
	protected override void Update()
	{
		base.Update();
		if (this.towerJoint == null)
		{
			return;
		}
		JointSpring spring = this.cannonJoint.spring;
		Vector3 eulerAngles = this.towerJoint.targetRotation.eulerAngles;
		Vector2 input = this.GetInput();
		eulerAngles.z = Mathf.Clamp(eulerAngles.z - input.x, eulerAngles.z - 5f, eulerAngles.z + 5f);
		spring.targetPosition = Mathf.Clamp(Mathf.Clamp(spring.targetPosition - input.y, spring.targetPosition - 5f, spring.targetPosition + 5f), this.cannonJoint.limits.min, this.cannonJoint.limits.max);
		this.towerJoint.targetRotation = Quaternion.Euler(eulerAngles);
		this.cannonJoint.spring = spring;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00065178 File Offset: 0x00063378
	private Vector2 GetInput()
	{
		if (base.user == null || IngameMenuUi.IsOpen())
		{
			return Vector2.zero;
		}
		if (!base.user.aiControlled)
		{
			return new Vector2(-SteelInput.GetAxis(SteelInput.KeyBinds.AimX) * this.sensitivityX, SteelInput.GetAxis(SteelInput.KeyBinds.AimY) * this.sensitivityY) * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity) * 4f;
		}
		Vector3 vector = this.CurrentMuzzle().worldToLocalMatrix.MultiplyVector(base.user.controller.FacingDirection());
		return new Vector2(vector.x * 3f, vector.y * 3f);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00065228 File Offset: 0x00063428
	protected override Projectile SpawnProjectile(Vector3 direction, Vector3 muzzlePosition, bool hasUser)
	{
		this.rigidbody.AddForceAtPosition(-this.CurrentMuzzle().forward * this.configuration.kickback + UnityEngine.Random.insideUnitSphere * this.configuration.randomKick, muzzlePosition, ForceMode.Impulse);
		return base.SpawnProjectile(direction, muzzlePosition, hasUser);
	}

	// Token: 0x040007FF RID: 2047
	private const float MAX_TURN_DELTA = 5f;

	// Token: 0x04000800 RID: 2048
	public float sensitivityX = 1f;

	// Token: 0x04000801 RID: 2049
	public float sensitivityY = 1f;

	// Token: 0x04000802 RID: 2050
	public ConfigurableJoint towerJoint;

	// Token: 0x04000803 RID: 2051
	public HingeJoint cannonJoint;

	// Token: 0x04000804 RID: 2052
	public Renderer cannonRenderer;

	// Token: 0x04000805 RID: 2053
	private Rigidbody rigidbody;
}

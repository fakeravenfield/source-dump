using System;
using Lua;
using UnityEngine;

// Token: 0x0200010C RID: 268
[ResolveDynValueAs(typeof(MountedWeapon))]
public class MountedTurret : MountedWeapon
{
	// Token: 0x060007C9 RID: 1993 RVA: 0x00064A78 File Offset: 0x00062C78
	protected override void Awake()
	{
		base.Awake();
		Rigidbody componentInParent = base.GetComponentInParent<Rigidbody>();
		if (componentInParent != null)
		{
			this.parentTransform = componentInParent.transform;
		}
		else
		{
			this.parentTransform = base.transform.root;
		}
		Vector3 localEulerAngles = this.bearingTransform.localEulerAngles;
		Vector3 localEulerAngles2 = this.pitchTransform.localEulerAngles;
		Vector3 localEulerAngles3 = localEulerAngles;
		Vector3 localEulerAngles4 = localEulerAngles2;
		if (this.clampX.enabled)
		{
			localEulerAngles3.y = (this.clampX.min + this.clampX.max) / 2f;
		}
		if (this.clampY.enabled)
		{
			localEulerAngles4.x = (this.clampY.min + this.clampY.max) / 2f;
		}
		this.bearingTransform.localEulerAngles = localEulerAngles3;
		this.pitchTransform.localEulerAngles = localEulerAngles4;
		this.defaultLocalCenterAim = this.parentTransform.worldToLocalMatrix.MultiplyVector(this.configuration.muzzles[0].forward);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00064B7C File Offset: 0x00062D7C
	public override bool CanAimAt(Vector3 position)
	{
		if (!this.clampX.enabled)
		{
			return true;
		}
		Vector3 vector = this.parentTransform.localToWorldMatrix.MultiplyVector(this.defaultLocalCenterAim);
		Vector3 vector2 = position - this.bearingTransform.position;
		position - this.pitchTransform.position;
		float num = Vector3.Angle(Vector3.ProjectOnPlane(vector2, this.bearingTransform.up), Vector3.ProjectOnPlane(vector, this.bearingTransform.up));
		float num2 = Vector3.Angle(Vector3.ProjectOnPlane(vector2, this.pitchTransform.right), Vector3.ProjectOnPlane(vector, this.pitchTransform.right));
		return num > this.clampX.min && num < this.clampX.max && (!this.clampY.enabled || (num2 > this.clampY.min && num2 < this.clampY.max));
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00064C70 File Offset: 0x00062E70
	protected override void Update()
	{
		base.Update();
		if (base.user != null)
		{
			Vector2 vector = Vector2.ClampMagnitude(this.GetInput(), 10f);
			Vector3 localEulerAngles = this.bearingTransform.localEulerAngles;
			localEulerAngles.y += vector.x;
			if (this.clampX.enabled)
			{
				localEulerAngles.y = this.clampX.ClampAngle(localEulerAngles.y);
			}
			this.bearingTransform.localEulerAngles = localEulerAngles;
			Vector3 localEulerAngles2 = this.pitchTransform.localEulerAngles;
			localEulerAngles2.x -= vector.y;
			if (this.clampY.enabled)
			{
				localEulerAngles2.x = this.clampY.ClampAngle(localEulerAngles2.x);
			}
			this.pitchTransform.localEulerAngles = localEulerAngles2;
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00064D44 File Offset: 0x00062F44
	private Vector2 GetInput()
	{
		if (base.user == null || IngameMenuUi.IsOpen())
		{
			return Vector2.zero;
		}
		if (!base.user.aiControlled)
		{
			float d = Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity) * 4f;
			base.ShouldUseFineAim();
			return new Vector2(-SteelInput.GetAxis(SteelInput.KeyBinds.AimX) * this.sensitivityX, SteelInput.GetAxis(SteelInput.KeyBinds.AimY) * this.sensitivityY) * d;
		}
		Vector3 vector = this.CurrentMuzzle().worldToLocalMatrix.MultiplyVector(base.user.controller.FacingDirection());
		return new Vector2(vector.x * 5f, vector.y * 5f);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x000070D1 File Offset: 0x000052D1
	public override bool IsClampedTurret()
	{
		return this.clampX.enabled;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000070DE File Offset: 0x000052DE
	public override Vector3 GetClampedTurretRandomLookDirection()
	{
		return this.bearingTransform.parent.forward + UnityEngine.Random.insideUnitSphere * 0.7f;
	}

	// Token: 0x040007EB RID: 2027
	private const float MAX_TURN_DELTA = 10f;

	// Token: 0x040007EC RID: 2028
	public Transform bearingTransform;

	// Token: 0x040007ED RID: 2029
	public Transform pitchTransform;

	// Token: 0x040007EE RID: 2030
	private Transform parentTransform;

	// Token: 0x040007EF RID: 2031
	private Vector3 defaultLocalCenterAim;

	// Token: 0x040007F0 RID: 2032
	public float sensitivityX = 1f;

	// Token: 0x040007F1 RID: 2033
	public float sensitivityY = 1f;

	// Token: 0x040007F2 RID: 2034
	public MountedTurret.Clamp clampX;

	// Token: 0x040007F3 RID: 2035
	public MountedTurret.Clamp clampY;

	// Token: 0x0200010D RID: 269
	[Serializable]
	public class Clamp
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00007122 File Offset: 0x00005322
		public float ClampAngle(float a)
		{
			return Mathf.Clamp(Mathf.DeltaAngle(0f, a), this.min, this.max);
		}

		// Token: 0x040007F4 RID: 2036
		public bool enabled;

		// Token: 0x040007F5 RID: 2037
		public float min;

		// Token: 0x040007F6 RID: 2038
		public float max;
	}
}

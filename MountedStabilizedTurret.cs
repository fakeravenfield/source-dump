using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class MountedStabilizedTurret : MountedWeapon
{
	// Token: 0x060007BB RID: 1979 RVA: 0x000643D8 File Offset: 0x000625D8
	protected override void Awake()
	{
		base.Awake();
		if (this.useSpring)
		{
			this.spring = new Spring(this.springForce, this.springDrag, -this.springMaxOffset, this.springMaxOffset, 8);
		}
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
		this.ResetLookDirection();
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00007018 File Offset: 0x00005218
	private void ResetLookDirection()
	{
		this.lookDirection = Quaternion.LookRotation(this.configuration.muzzles[0].forward);
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0006451C File Offset: 0x0006271C
	public override bool CanAimAt(Vector3 position)
	{
		if (!this.clampX.enabled && !this.clampY.enabled)
		{
			return true;
		}
		Vector3 eulerAngles = Quaternion.LookRotation(this.bearingTransform.parent.worldToLocalMatrix.MultiplyPoint(position)).eulerAngles;
		if (this.clampX.enabled && !this.clampX.IsInRange(eulerAngles.y))
		{
			return false;
		}
		if (this.clampY.enabled)
		{
			float num;
			float num2;
			this.GetNotchedClampYValues(eulerAngles.y, out num, out num2);
			float num3 = Mathf.DeltaAngle(0f, eulerAngles.x);
			return num3 >= num && num3 <= num2;
		}
		return true;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000645D0 File Offset: 0x000627D0
	public void GetNotchedClampYValues(float bearing, out float min, out float max)
	{
		min = this.clampY.min;
		max = this.clampY.max;
		for (int i = 0; i < this.clampNotch.Length; i++)
		{
			float num = Mathf.Abs(Mathf.DeltaAngle(bearing, this.clampNotch[i].bearingAngle));
			float num2 = this.clampNotch[i].notchWidth + this.clampNotch[i].notchSlopeWidth;
			if (num < num2)
			{
				float num3 = Mathf.InverseLerp(num2, this.clampNotch[i].notchWidth, num);
				float num4 = this.clampNotch[i].notchHeight * num3;
				if (this.clampNotch[i].type == MountedStabilizedTurret.ClampYNotch.Type.AffectMax)
				{
					max -= num4;
				}
				else
				{
					min += num4;
				}
			}
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00007037 File Offset: 0x00005237
	private void FixedUpdate()
	{
		if (this.useSpring)
		{
			this.spring.Update();
		}
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x000646A8 File Offset: 0x000628A8
	protected override void Update()
	{
		base.Update();
		if (base.user != null && !GameManager.IsPaused())
		{
			Vector2 vector = this.GetInput();
			Transform transform;
			if (base.UserIsPlayer())
			{
				transform = FpsActorController.instance.GetActiveCamera().transform;
			}
			else
			{
				transform = this.pitchTransform;
			}
			if (this.useSpring)
			{
				this.spring.position += vector * this.springAmount;
				vector += this.spring.position;
			}
			if (this.useMaxTurnSpeed)
			{
				vector = Vector2.ClampMagnitude(vector, this.maxTurnSpeed * Time.deltaTime);
			}
			Vector3 euler = transform.up * vector.x - transform.right * vector.y;
			this.lookDirection = Quaternion.Euler(euler) * this.lookDirection;
			this.Stabilize(vector);
		}
		this.lookDirection = this.pitchTransform.rotation;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000647B8 File Offset: 0x000629B8
	private void Stabilize(Vector2 rawInput)
	{
		Vector3 v = this.lookDirection * Vector3.forward;
		Vector3 localEulerAngles = this.bearingTransform.localEulerAngles;
		float num;
		if (this.stabilizeX)
		{
			num = SMath.SignedAngleBetween(this.bearingTransform.parent.forward, v, this.bearingTransform.up);
		}
		else
		{
			num = localEulerAngles.y + rawInput.x;
		}
		if (this.clampX.enabled)
		{
			localEulerAngles.y = this.clampX.ClampAngle(num);
		}
		else
		{
			localEulerAngles.y = num;
		}
		this.bearingTransform.localEulerAngles = localEulerAngles;
		Vector3 localEulerAngles2 = this.pitchTransform.localEulerAngles;
		float num2;
		if (this.stabilizeY)
		{
			num2 = SMath.SignedAngleBetween(this.bearingTransform.forward, v, this.pitchTransform.right);
		}
		else
		{
			num2 = localEulerAngles2.x - rawInput.y;
		}
		if (this.clampY.enabled)
		{
			float min;
			float max;
			this.GetNotchedClampYValues(num, out min, out max);
			float value = Mathf.DeltaAngle(0f, num2);
			localEulerAngles2.x = Mathf.Clamp(value, min, max);
		}
		else
		{
			localEulerAngles2.x = num2;
		}
		this.pitchTransform.localEulerAngles = localEulerAngles2;
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0000704C File Offset: 0x0000524C
	public override void Unholster()
	{
		base.Unholster();
		this.ResetLookDirection();
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x000648F4 File Offset: 0x00062AF4
	private Vector2 GetInput()
	{
		if (base.user == null || IngameMenuUi.IsOpen())
		{
			return Vector2.zero;
		}
		if (!base.user.aiControlled && !GameManager.gameOver)
		{
			float num = 4f * Options.GetScaledMouseSensitivity(OptionSlider.Id.MouseSensitivity);
			num *= FpsActorController.GetSensitivityMultiplierFromFov(FpsActorController.instance.GetActiveCamera().fieldOfView);
			if (base.ShouldUseFineAim())
			{
				num *= this.configuration.aimSensitivityMultiplier;
			}
			return new Vector2(-SteelInput.GetAxis(SteelInput.KeyBinds.AimX) * this.sensitivityX, SteelInput.GetAxis(SteelInput.KeyBinds.AimY) * this.sensitivityY) * num;
		}
		Vector3 vector = this.pitchTransform.worldToLocalMatrix.MultiplyVector(base.user.controller.FacingDirection());
		return new Vector2(Mathf.Clamp(vector.x * 5f, -3f, 3f), Mathf.Clamp(vector.y * 5f, -3f, 3f));
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0000705A File Offset: 0x0000525A
	public override bool IsClampedTurret()
	{
		return this.clampX.enabled;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00007067 File Offset: 0x00005267
	public override Vector3 GetClampedTurretRandomLookDirection()
	{
		return this.bearingTransform.parent.forward + UnityEngine.Random.insideUnitSphere * 0.5f;
	}

	// Token: 0x040007CA RID: 1994
	private const float AI_MAX_TURN_INPUT = 3f;

	// Token: 0x040007CB RID: 1995
	private const float AI_MAX_TURN_INPUT_SMOOTH_DRAG = 0.05f;

	// Token: 0x040007CC RID: 1996
	public Transform bearingTransform;

	// Token: 0x040007CD RID: 1997
	public Transform pitchTransform;

	// Token: 0x040007CE RID: 1998
	private Transform parentTransform;

	// Token: 0x040007CF RID: 1999
	private Vector3 defaultLocalCenterAim;

	// Token: 0x040007D0 RID: 2000
	private Quaternion lookDirection;

	// Token: 0x040007D1 RID: 2001
	public bool stabilizeX = true;

	// Token: 0x040007D2 RID: 2002
	public bool stabilizeY = true;

	// Token: 0x040007D3 RID: 2003
	public float sensitivityX = 1f;

	// Token: 0x040007D4 RID: 2004
	public float sensitivityY = 1f;

	// Token: 0x040007D5 RID: 2005
	public MountedStabilizedTurret.Clamp clampX;

	// Token: 0x040007D6 RID: 2006
	public MountedStabilizedTurret.Clamp clampY;

	// Token: 0x040007D7 RID: 2007
	public MountedStabilizedTurret.ClampYNotch[] clampNotch = new MountedStabilizedTurret.ClampYNotch[0];

	// Token: 0x040007D8 RID: 2008
	public bool useMaxTurnSpeed;

	// Token: 0x040007D9 RID: 2009
	public float maxTurnSpeed = 100f;

	// Token: 0x040007DA RID: 2010
	public bool useSpring;

	// Token: 0x040007DB RID: 2011
	public float springAmount = 0.002f;

	// Token: 0x040007DC RID: 2012
	public float springForce = 30f;

	// Token: 0x040007DD RID: 2013
	public float springDrag = 5f;

	// Token: 0x040007DE RID: 2014
	public Vector2 springMaxOffset = new Vector2(0.1f, 0.1f);

	// Token: 0x040007DF RID: 2015
	private Spring spring;

	// Token: 0x02000109 RID: 265
	[Serializable]
	public struct Clamp
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x0000708D File Offset: 0x0000528D
		public float ClampAngle(float a)
		{
			return Mathf.Clamp(Mathf.DeltaAngle(0f, a), this.min, this.max);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000070AB File Offset: 0x000052AB
		public bool IsInRange(float a)
		{
			a = Mathf.DeltaAngle(0f, a);
			return a >= this.min && a <= this.max;
		}

		// Token: 0x040007E0 RID: 2016
		public bool enabled;

		// Token: 0x040007E1 RID: 2017
		public float min;

		// Token: 0x040007E2 RID: 2018
		public float max;
	}

	// Token: 0x0200010A RID: 266
	[Serializable]
	public struct ClampYNotch
	{
		// Token: 0x040007E3 RID: 2019
		public MountedStabilizedTurret.ClampYNotch.Type type;

		// Token: 0x040007E4 RID: 2020
		public float bearingAngle;

		// Token: 0x040007E5 RID: 2021
		public float notchWidth;

		// Token: 0x040007E6 RID: 2022
		public float notchSlopeWidth;

		// Token: 0x040007E7 RID: 2023
		public float notchHeight;

		// Token: 0x0200010B RID: 267
		public enum Type
		{
			// Token: 0x040007E9 RID: 2025
			AffectMin,
			// Token: 0x040007EA RID: 2026
			AffectMax
		}
	}
}

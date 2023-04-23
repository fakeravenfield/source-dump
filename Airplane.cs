using System;
using Lua;
using UnityEngine;

// Token: 0x02000311 RID: 785
[ProxyName("AirplaneProxy")]
public class Airplane : Vehicle
{
	// Token: 0x06001455 RID: 5205 RVA: 0x000103CE File Offset: 0x0000E5CE
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
		this.engineOnAction.Start();
		this.liftGainAction.StartLifetime(this.liftGainTime);
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x00095D70 File Offset: 0x00093F70
	private void RetractLandingGear()
	{
		this.gearsRetracted = true;
		if (this.animator != null)
		{
			this.animator.SetBool("landing gears", false);
		}
		if (this.landingGearActivationObjects != null)
		{
			GameObject[] array = this.landingGearActivationObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x00095DCC File Offset: 0x00093FCC
	private void ExtendLandingGear()
	{
		this.gearsRetracted = false;
		if (this.animator != null)
		{
			this.animator.SetBool("landing gears", true);
		}
		if (this.landingGearActivationObjects != null)
		{
			GameObject[] array = this.landingGearActivationObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(true);
			}
		}
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000103F2 File Offset: 0x0000E5F2
	protected override void Update()
	{
		base.Update();
		bool dead = this.dead;
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x00095E28 File Offset: 0x00094028
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.dead)
		{
			return;
		}
		Ray ray = new Ray(this.targetLockPoint.position, Vector3.down);
		ray.origin += Vector3.up;
		this.altitude = Mathf.Max(0f, ray.origin.y - WaterLevel.instance.height);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 1000f, 1))
		{
			this.altitude = raycastHit.distance;
		}
		this.isAirborne = (this.altitude > 3f);
		Vector4 vector = Vector4.zero;
		if (base.HasDriver())
		{
			vector = base.Driver().controller.AirplaneInput();
		}
		vector = Vehicle.Clamp4(vector);
		this.driverInput = vector;
		this.airbrake = (vector.y < -0.3f);
		if (!this.engineOnAction.TrueDone())
		{
			if (base.HasDriver() && this.airbrake)
			{
				this.engineOnAction.Start();
			}
			return;
		}
		Vector3 vector2 = base.LocalVelocity();
		float z = vector2.z;
		bool flag = (!base.HasDriver() || this.airbrake) && this.altitude < 50f;
		bool flag2 = flag;
		if (flag2 || this.altitude <= 20f || z <= 30f)
		{
			this.retractLandingGearAction.Start();
		}
		if (!flag2)
		{
			this.extendLandingGearAction.Start();
		}
		if (this.extendLandingGearAction.TrueDone() && this.gearsRetracted)
		{
			this.ExtendLandingGear();
		}
		else if (this.retractLandingGearAction.TrueDone() && !this.gearsRetracted)
		{
			this.RetractLandingGear();
		}
		float num = Mathf.Pow(this.engine.power, 2f);
		float time = Mathf.Atan2(-vector2.y, vector2.z) * 57.29578f;
		float num2 = this.liftVsAngleOfAttack.Evaluate(time);
		float num3 = this.controlVsAngleOfAttack.Evaluate(time) * this.engine.power;
		float num4 = Mathf.Clamp01(Mathf.InverseLerp(10f, 30f, z));
		float num5 = this.autoPitchTorqueGain * num2 * vector2.z;
		float num6 = this.baseLift * num4 * num2 * vector2.z;
		float num7 = 1f;
		bool flag3 = flag && this.altitude < 10f;
		if (!base.HasDriver())
		{
			num *= 0.2f;
			num6 *= 0.1f;
			if (flag)
			{
				num7 *= 0.1f;
			}
		}
		else if (flag3)
		{
			num7 *= 0.7f;
		}
		if (!this.liftGainAction.TrueDone())
		{
			float num8 = Mathf.Pow(this.liftGainAction.Ratio(), 2f);
			num6 *= num8;
			num5 *= num8;
		}
		if (this.burning)
		{
			num6 *= 0.6f;
			num *= 0.7f;
			num3 *= this.controlWhenBurning;
		}
		Vector3 vector3 = new Vector3(Vector3.Dot(base.transform.right, this.Velocity()) * -this.perpendicularDrag, num4 * num7 * Vector3.Dot(base.transform.up, this.Velocity()) * -this.perpendicularDrag, 0f);
		Vector3 torque = new Vector3(this.pitchSensitivity * vector.w, this.yawSensitivity * vector.x, this.rollSensitivity * -vector.z) * num3;
		torque.x += num5;
		if (this.airbrake && (flag || z > 60f))
		{
			float num9 = flag ? 0.1f : 0.05f;
			vector3.z = vector2.z * -num9;
			if (flag3 && z > 0.1f && z < 30f)
			{
				num = 0f;
				vector3.z -= 0.5f;
				this.liftGainAction.StartLifetime(this.liftGainTime);
			}
		}
		Vector3 a = base.transform.localToWorldMatrix.MultiplyVector(vector3);
		float d = this.acceleration;
		if (base.HasDriver())
		{
			if (vector.y >= 0f)
			{
				d = Mathf.Lerp(this.acceleration, this.accelerationThrottleUp, vector.y);
			}
			else
			{
				d = Mathf.Lerp(this.acceleration, this.accelerationThrottleDown, -vector.y);
			}
			this.engine.targetPitch = 1f + vector.y * this.throttleEngineAudioPitchControl;
		}
		Vector3 b = this.thruster.forward * num * d;
		Vector3 b2 = base.transform.up * num6;
		Vector3 force = a + b + b2;
		this.rigidbody.AddForce(force, ForceMode.Acceleration);
		this.rigidbody.AddRelativeTorque(torque, ForceMode.Acceleration);
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x0009630C File Offset: 0x0009450C
	protected override void StartBurning(DamageInfo info)
	{
		base.StartBurning(info);
		this.rigidbody.drag = 0f;
		this.rigidbody.angularDrag = 0f;
		this.acceleration = 4f;
		this.perpendicularDrag *= 0.1f;
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x00010401 File Offset: 0x0000E601
	[Ignore]
	public override bool ShouldBeAvoided()
	{
		return !this.isAirborne && base.ShouldBeAvoided();
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x0000476F File Offset: 0x0000296F
	[Ignore]
	public override bool IsAircraft()
	{
		return true;
	}

	// Token: 0x040015AA RID: 5546
	private const float ROTOR_SPEED = 1000f;

	// Token: 0x040015AB RID: 5547
	private const float RETRACT_LANDING_GEAR_SPEED_THRESHOLD = 30f;

	// Token: 0x040015AC RID: 5548
	private const float RETRACT_LANDING_GEAR_ALTITUDE_THRESHOLD = 20f;

	// Token: 0x040015AD RID: 5549
	private const float EXTEND_LANDING_GEAR_ALTITUDE_THRESHOLD = 50f;

	// Token: 0x040015AE RID: 5550
	private const float LANDING_PHYSICS_ALTITUDE = 10f;

	// Token: 0x040015AF RID: 5551
	private const float LIFT_SPEED_THRESHOLD_MIN = 10f;

	// Token: 0x040015B0 RID: 5552
	private const float LIFT_SPEED_THRESHOLD_MAX = 30f;

	// Token: 0x040015B1 RID: 5553
	private const float AIRBRAKE_DRAG_FLIGHT = 0.05f;

	// Token: 0x040015B2 RID: 5554
	private const float AIRBRAKE_DRAG_LANDING = 0.1f;

	// Token: 0x040015B3 RID: 5555
	public AnimationCurve liftVsAngleOfAttack;

	// Token: 0x040015B4 RID: 5556
	public AnimationCurve controlVsAngleOfAttack;

	// Token: 0x040015B5 RID: 5557
	public Transform thruster;

	// Token: 0x040015B6 RID: 5558
	public float baseLift = 0.5f;

	// Token: 0x040015B7 RID: 5559
	public new float acceleration = 10f;

	// Token: 0x040015B8 RID: 5560
	public float accelerationThrottleUp = 12f;

	// Token: 0x040015B9 RID: 5561
	public float accelerationThrottleDown = 8f;

	// Token: 0x040015BA RID: 5562
	public float throttleEngineAudioPitchControl = 0.1f;

	// Token: 0x040015BB RID: 5563
	public float autoPitchTorqueGain = -0.001f;

	// Token: 0x040015BC RID: 5564
	public float perpendicularDrag = 1f;

	// Token: 0x040015BD RID: 5565
	public float pitchSensitivity = 1f;

	// Token: 0x040015BE RID: 5566
	public float yawSensitivity = 0.5f;

	// Token: 0x040015BF RID: 5567
	public float rollSensitivity = 1f;

	// Token: 0x040015C0 RID: 5568
	public float liftGainTime = 7f;

	// Token: 0x040015C1 RID: 5569
	public float controlWhenBurning = 0.1f;

	// Token: 0x040015C2 RID: 5570
	public float flightAltitudeMultiplier = 1f;

	// Token: 0x040015C3 RID: 5571
	public GameObject[] landingGearActivationObjects;

	// Token: 0x040015C4 RID: 5572
	[NonSerialized]
	public bool airbrake;

	// Token: 0x040015C5 RID: 5573
	[NonSerialized]
	public bool gearsRetracted;

	// Token: 0x040015C6 RID: 5574
	[NonSerialized]
	public float altitude;

	// Token: 0x040015C7 RID: 5575
	[NonSerialized]
	public bool isAirborne;

	// Token: 0x040015C8 RID: 5576
	private TimedAction engineOnAction = new TimedAction(1f, false);

	// Token: 0x040015C9 RID: 5577
	private TimedAction liftGainAction = new TimedAction(1f, false);

	// Token: 0x040015CA RID: 5578
	private TimedAction retractLandingGearAction = new TimedAction(2f, false);

	// Token: 0x040015CB RID: 5579
	private TimedAction extendLandingGearAction = new TimedAction(2f, false);
}

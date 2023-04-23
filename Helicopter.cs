using System;
using Lua;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class Helicopter : Vehicle
{
	// Token: 0x06001497 RID: 5271 RVA: 0x000106B6 File Offset: 0x0000E8B6
	protected override void Awake()
	{
		base.Awake();
		this.rigidbody.maxAngularVelocity = 1.5f;
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000106CE File Offset: 0x0000E8CE
	protected override void Update()
	{
		base.Update();
		if (this.dead)
		{
			return;
		}
		float power = this.engine.power;
		this.isAirborne = !Physics.Raycast(base.transform.position, Vector3.down, 3f);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0001064E File Offset: 0x0000E84E
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x00010656 File Offset: 0x0000E856
	protected override void DriverExited()
	{
		base.DriverExited();
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x00097E00 File Offset: 0x00096000
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
		Vector3 normalized = (base.transform.forward + 0.15f * base.transform.up).normalized;
		float d = Vector3.Dot(normalized, this.rigidbody.velocity);
		float magnitude = Vector3.Cross(normalized, this.rigidbody.velocity).magnitude;
		this.rigidbody.AddForce(base.transform.up * d * this.aerodynamicLift, ForceMode.Acceleration);
		if (!base.HasDriver())
		{
			this.driverInput = Vector4.zero;
			return;
		}
		Vector4 vector = Vehicle.Clamp4(base.Driver().controller.HelicopterInput()) * this.engine.power;
		this.driverInput = vector;
		float d2 = this.burning ? this.controlWhenBurning : 1f;
		float y = vector.y;
		Vector3 torque = new Vector3(vector.w, vector.x, -vector.z) * this.manouverability * 0.35f * d2;
		Vector3 normalized2 = (base.transform.up + base.transform.forward * 0.05f).normalized;
		Vector3 a = Vector3.Project(normalized2, Vector3.up);
		normalized2 = (normalized2 - 0.05f * a).normalized;
		float t = Mathf.Clamp01(-Vector3.Dot(normalized2, this.rigidbody.velocity.normalized));
		float num = 1f + Mathf.Lerp(0f, this.extraForceWhenStopping, t);
		float num2 = Mathf.Clamp01(Mathf.Max(y, 0f) * Vector3.Dot(normalized2, Vector3.up) * (20f - this.altitude) * 0.1f) * this.groundEffectAcceleration + y * this.rotorForce * num;
		Vector3 vector2 = normalized2 * (num2 - Physics.gravity.y - 0.5f);
		if (this.burning)
		{
			this.rigidbody.AddForce(0.3f * vector2, ForceMode.Acceleration);
			return;
		}
		this.rigidbody.AddForce(vector2, ForceMode.Acceleration);
		this.rigidbody.AddRelativeTorque(torque, ForceMode.Acceleration);
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x0001070E File Offset: 0x0000E90E
	protected override void StartBurning(DamageInfo info)
	{
		base.StartBurning(info);
		this.randomBurningTorque = UnityEngine.Random.insideUnitSphere * 0.005f + Vector3.up * 0.025f;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x00010740 File Offset: 0x0000E940
	[Ignore]
	public override bool ShouldBeAvoided()
	{
		return !this.isAirborne && base.ShouldBeAvoided();
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x0000476F File Offset: 0x0000296F
	[Ignore]
	public override bool IsAircraft()
	{
		return true;
	}

	// Token: 0x0400163D RID: 5693
	private const float MANOUVERABILITY_SCALE = 0.35f;

	// Token: 0x0400163E RID: 5694
	private const float GROUND_EFFECT_START_ALTITUDE = 20f;

	// Token: 0x0400163F RID: 5695
	private const float GROUND_EFFECT_MAX_ALTITUDE = 10f;

	// Token: 0x04001640 RID: 5696
	private const float GROUND_EFFECT_ALTITUDE_GAIN = 0.1f;

	// Token: 0x04001641 RID: 5697
	public float rotorForce = 5f;

	// Token: 0x04001642 RID: 5698
	public float manouverability = 1f;

	// Token: 0x04001643 RID: 5699
	public float extraForceWhenStopping = 0.3f;

	// Token: 0x04001644 RID: 5700
	public float controlWhenBurning = 0.1f;

	// Token: 0x04001645 RID: 5701
	public float groundEffectAcceleration = 2f;

	// Token: 0x04001646 RID: 5702
	public float aerodynamicLift = 0.03f;

	// Token: 0x04001647 RID: 5703
	public float flightAltitudeMultiplier = 1f;

	// Token: 0x04001648 RID: 5704
	[NonSerialized]
	public float altitude;

	// Token: 0x04001649 RID: 5705
	private bool isAirborne;

	// Token: 0x0400164A RID: 5706
	private Vector3 randomBurningTorque = Vector3.zero;
}

using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class Car : Vehicle
{
	// Token: 0x06001488 RID: 5256 RVA: 0x00097654 File Offset: 0x00095854
	protected override void Awake()
	{
		base.Awake();
		this.rigidbody.centerOfMass += Vector3.down * this.extraStability;
		foreach (Car.WheelConfiguration wheelConfiguration in this.wheels)
		{
			wheelConfiguration.collider.motorTorque = 0f;
			wheelConfiguration.collider.brakeTorque = 120f;
		}
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000976C4 File Offset: 0x000958C4
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
		Car.WheelConfiguration[] array = this.wheels;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].collider.brakeTorque = 0f;
		}
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x00097700 File Offset: 0x00095900
	protected override void DriverExited()
	{
		foreach (Car.WheelConfiguration wheelConfiguration in this.wheels)
		{
			wheelConfiguration.collider.motorTorque = 0f;
			wheelConfiguration.collider.brakeTorque = 120f;
		}
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x00097744 File Offset: 0x00095944
	private void UpdateVisuals(Car.WheelConfiguration wheel)
	{
		WheelCollider collider = wheel.collider;
		Transform child = collider.transform.GetChild(0);
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);
		child.transform.position = position;
		child.transform.rotation = rotation;
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x00097788 File Offset: 0x00095988
	protected override void Update()
	{
		base.Update();
		if (this.dead)
		{
			return;
		}
		float targetPitch = base.HasDriver() ? 0.5f : 0f;
		if (base.HasDriver() && !this.burning)
		{
			Vector2 vector = Vehicle.Clamp2(base.Driver().controller.CarInput());
			float num = 0f;
			foreach (Car.WheelConfiguration wheelConfiguration in this.wheels)
			{
				num += wheelConfiguration.collider.rpm;
			}
			num /= (float)this.wheels.Length;
			float target = vector.x * this.maxSteer;
			this.steerAngle = Mathf.MoveTowards(this.steerAngle, target, 5f * this.maxSteer * Time.deltaTime);
			foreach (Car.WheelConfiguration wheelConfiguration2 in this.wheels)
			{
				if (wheelConfiguration2.motor)
				{
					wheelConfiguration2.collider.motorTorque = vector.y * this.maxTorque;
					if ((vector.y < 0f && wheelConfiguration2.collider.rpm > 10f) || (vector.y > 0f && wheelConfiguration2.collider.rpm < -10f))
					{
						wheelConfiguration2.collider.brakeTorque = 300f;
						targetPitch = 0.5f;
					}
					else
					{
						wheelConfiguration2.collider.brakeTorque = 0f;
						if (vector.y > 0f)
						{
							targetPitch = 0.5f + Mathf.Abs(vector.y) * 0.6f;
						}
						else
						{
							targetPitch = 0.5f + Mathf.Abs(vector.y) * 0.2f;
						}
					}
				}
				if (wheelConfiguration2.steer)
				{
					wheelConfiguration2.collider.steerAngle = this.steerAngle;
				}
			}
		}
		this.engine.targetPitch = targetPitch;
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x00097978 File Offset: 0x00095B78
	public bool CanTurnTowards(Vector3 deltaPosition)
	{
		if (deltaPosition.magnitude < 4f)
		{
			return true;
		}
		float num = 2f * this.turningRadius * Vector3.Cross(deltaPosition.normalized, base.transform.forward).magnitude;
		return deltaPosition.magnitude > num + 1f;
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000979D4 File Offset: 0x00095BD4
	private void LateUpdate()
	{
		foreach (Car.WheelConfiguration wheel in this.wheels)
		{
			this.UpdateVisuals(wheel);
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x00097A04 File Offset: 0x00095C04
	public override void Die(DamageInfo info)
	{
		base.Die(info);
		Car.WheelConfiguration[] array = this.wheels;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].collider.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400161B RID: 5659
	private const float STEER_RATE = 5f;

	// Token: 0x0400161C RID: 5660
	private const float MIN_BRAKE_RPM = 10f;

	// Token: 0x0400161D RID: 5661
	private const float BRAKE_TORQUE = 300f;

	// Token: 0x0400161E RID: 5662
	private const float CAN_TURN_TOWARDS_DISTANCE_BIAS = 1f;

	// Token: 0x0400161F RID: 5663
	public float extraStability = 0.5f;

	// Token: 0x04001620 RID: 5664
	public float maxTorque = 300f;

	// Token: 0x04001621 RID: 5665
	public float maxSteer = 40f;

	// Token: 0x04001622 RID: 5666
	public float turningRadius = 5f;

	// Token: 0x04001623 RID: 5667
	private float steerAngle;

	// Token: 0x04001624 RID: 5668
	public new Car.WheelConfiguration[] wheels;

	// Token: 0x04001625 RID: 5669
	private float enginePitch;

	// Token: 0x02000318 RID: 792
	[Serializable]
	public class WheelConfiguration
	{
		// Token: 0x04001626 RID: 5670
		public WheelCollider collider;

		// Token: 0x04001627 RID: 5671
		public bool motor;

		// Token: 0x04001628 RID: 5672
		public bool steer;
	}
}

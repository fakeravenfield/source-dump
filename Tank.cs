using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class Tank : Vehicle
{
	// Token: 0x060014C7 RID: 5319 RVA: 0x0001090D File Offset: 0x0000EB0D
	protected override void Awake()
	{
		base.Awake();
		this.rigidbody.centerOfMass += Vector3.down * this.extraStability;
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000988F4 File Offset: 0x00096AF4
	protected override void Update()
	{
		base.Update();
		if (this.dead)
		{
			return;
		}
		this.UpdateTrack(this.tracksLeft, this.wheelCollidersLeft, this.trackOffsetLeft);
		this.UpdateTrack(this.tracksRight, this.wheelCollidersRight, this.trackOffsetRight);
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x0001093B File Offset: 0x0000EB3B
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.dead)
		{
			return;
		}
		this.UpdateMovement();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x00010952 File Offset: 0x0000EB52
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
		this.ownerIndicator.SetOwner(base.Driver().team);
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x00010970 File Offset: 0x0000EB70
	protected override void DriverExited()
	{
		base.DriverExited();
		this.ownerIndicator.SetOwner(-1);
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x00098940 File Offset: 0x00096B40
	private void UpdateMovement()
	{
		float motorTorque = 0f;
		float motorTorque2 = 0f;
		float brakeTorque = 3f;
		WheelFrictionCurve forwardFriction = this.wheelCollidersLeft[0].forwardFriction;
		forwardFriction.stiffness = 1f;
		float targetPitch = 0f;
		if (base.HasDriver() && !this.burning)
		{
			Vector2 vector = base.Driver().controller.CarInput();
			if (vector != Vector2.zero)
			{
				motorTorque = Mathf.Clamp(vector.y * this.maxTorque + vector.x * this.maxTorque, -this.maxTorque, this.maxTorque);
				motorTorque2 = Mathf.Clamp(vector.y * this.maxTorque - vector.x * this.maxTorque, -this.maxTorque, this.maxTorque);
				forwardFriction.stiffness = this.driveStiffness;
				brakeTorque = 0f;
			}
			targetPitch = Mathf.Clamp01(0.7f + Mathf.Abs(vector.x) + Mathf.Abs(vector.y));
		}
		this.engine.targetPitch = targetPitch;
		foreach (WheelCollider wheelCollider in this.wheelCollidersLeft)
		{
			wheelCollider.motorTorque = motorTorque;
			wheelCollider.brakeTorque = brakeTorque;
			wheelCollider.forwardFriction = forwardFriction;
		}
		foreach (WheelCollider wheelCollider2 in this.wheelCollidersRight)
		{
			wheelCollider2.motorTorque = motorTorque2;
			wheelCollider2.brakeTorque = brakeTorque;
			wheelCollider2.forwardFriction = forwardFriction;
		}
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x00098AC4 File Offset: 0x00096CC4
	private void UpdateTrack(Transform track, WheelCollider[] wheels, UvOffset offset)
	{
		Vector3 point;
		Quaternion quaternion;
		wheels[0].GetWorldPose(out point, out quaternion);
		Vector3 point2;
		wheels[1].GetWorldPose(out point2, out quaternion);
		float d = (wheels[0].rpm * wheels[0].radius + wheels[1].rpm * wheels[1].radius) / 2f;
		float num = base.transform.worldToLocalMatrix.MultiplyPoint(point).y - 0.743f;
		float num2 = base.transform.worldToLocalMatrix.MultiplyPoint(point2).y - 0.743f;
		Vector3 localPosition = track.transform.localPosition;
		localPosition.y = (num + num2) / 2f + 0.7f;
		track.transform.localPosition = localPosition;
		track.transform.localEulerAngles = new Vector3(-90f - Mathf.Atan2(num - num2, 4.13f) * 57.29578f, 0f, 0f);
		offset.IncrementOffset(Vector2.right * d * this.trackUvSpeedMultiplier * Time.deltaTime);
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x00098BE4 File Offset: 0x00096DE4
	public override void Die(DamageInfo info)
	{
		base.Die(info);
		Transform[] array = this.autoDestroyOnKill;
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i].gameObject);
		}
		foreach (WheelCollider wheelCollider in this.wheelCollidersRight)
		{
			wheelCollider.motorTorque = 0f;
			wheelCollider.brakeTorque = 100f;
		}
		foreach (WheelCollider wheelCollider2 in this.wheelCollidersLeft)
		{
			wheelCollider2.motorTorque = 0f;
			wheelCollider2.brakeTorque = 100f;
		}
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x00010984 File Offset: 0x0000EB84
	protected override void Explode()
	{
		base.Explode();
		UnityEngine.Object.Destroy(this.towerJoint);
		base.StartCoroutine(this.ApplyExplosionForce());
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000109A4 File Offset: 0x0000EBA4
	private IEnumerator ApplyExplosionForce()
	{
		yield return new WaitForFixedUpdate();
		this.rigidbody.AddForce(Vector3.up * 3f + UnityEngine.Random.insideUnitSphere * 2f, ForceMode.VelocityChange);
		this.towerRigidbody.AddForce(Vector3.up * 25f + UnityEngine.Random.insideUnitSphere * 6f, ForceMode.VelocityChange);
		this.towerRigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 10f, ForceMode.VelocityChange);
		yield break;
	}

	// Token: 0x04001688 RID: 5768
	private const float DEFAULT_WHEEL_HEIGHT = 0.743f;

	// Token: 0x04001689 RID: 5769
	private const float DEFAULT_TRACK_HEIGHT = 0.7f;

	// Token: 0x0400168A RID: 5770
	private const float WHEEL_DISTANCE = 4.13f;

	// Token: 0x0400168B RID: 5771
	public WheelCollider[] wheelCollidersLeft;

	// Token: 0x0400168C RID: 5772
	public WheelCollider[] wheelCollidersRight;

	// Token: 0x0400168D RID: 5773
	public Transform[] autoDestroyOnKill;

	// Token: 0x0400168E RID: 5774
	public Transform tracksLeft;

	// Token: 0x0400168F RID: 5775
	public Transform tracksRight;

	// Token: 0x04001690 RID: 5776
	public Joint towerJoint;

	// Token: 0x04001691 RID: 5777
	public float maxTorque = 7000f;

	// Token: 0x04001692 RID: 5778
	public float driveStiffness = 3f;

	// Token: 0x04001693 RID: 5779
	public float extraStability = 1f;

	// Token: 0x04001694 RID: 5780
	public UvOffset trackOffsetLeft;

	// Token: 0x04001695 RID: 5781
	public UvOffset trackOffsetRight;

	// Token: 0x04001696 RID: 5782
	public float trackUvSpeedMultiplier = 1f;

	// Token: 0x04001697 RID: 5783
	public Rigidbody towerRigidbody;

	// Token: 0x04001698 RID: 5784
	public OwnerIndicator ownerIndicator;

	// Token: 0x04001699 RID: 5785
	private float enginePitch;
}

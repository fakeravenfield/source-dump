using System;
using System.Collections.Generic;
using Lua;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class ArcadeCar : Vehicle
{
	// Token: 0x06001465 RID: 5221 RVA: 0x000966E8 File Offset: 0x000948E8
	protected override void Awake()
	{
		base.Awake();
		this.StartSettleAction();
		this.collider = base.GetComponent<Collider>();
		this.floatPhysics = base.GetComponent<FloatingRigidbody>();
		this.centerOfMassAir = this.rigidbody.centerOfMass;
		this.centerOfMassGrounded = this.centerOfMassAir + new Vector3(0f, -this.extraStability, 0f);
		this.driftAction = new TimedAction(this.driftDuration, false);
		this.maxSpeedTorque = this.topSpeed * this.speedTurnTorque;
		this.rigidbody.sleepThreshold = 0.05f;
		this.wheelUpdateBaseIndex = UnityEngine.Random.Range(0, 2);
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x00010471 File Offset: 0x0000E671
	private void StartSettleAction()
	{
		this.justSpawnedSettleAction.Start();
		this.justSpawnedSettling = true;
		this.rigidbody.constraints = (RigidbodyConstraints)10;
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x00010492 File Offset: 0x0000E692
	private void EndSettleAction()
	{
		this.justSpawnedSettling = false;
		this.rigidbody.constraints = RigidbodyConstraints.None;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000104A7 File Offset: 0x0000E6A7
	[Ignore]
	public override bool IsAmphibious()
	{
		return this.isAmphibious;
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000104AF File Offset: 0x0000E6AF
	[Ignore]
	public override bool IsInWater()
	{
		if (this.floatPhysics != null)
		{
			return this.floatPhysics.waterDragApplied;
		}
		return base.IsInWater();
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x00096794 File Offset: 0x00094994
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.dead)
		{
			return;
		}
		this.UpdateWheels();
		int groundedWheelsCount = this.GetGroundedWheelsCount();
		if (groundedWheelsCount >= 3)
		{
			this.isStableGroundedAction.Start();
		}
		bool flag = !this.isStableGroundedAction.TrueDone();
		bool flag2 = groundedWheelsCount > 0;
		bool flag3 = this.floatPhysics != null && this.floatPhysics.waterDragApplied;
		bool flag4 = flag || (this.isAmphibious && flag3);
		if (base.HasDriver())
		{
			if (flag4)
			{
				this.UpdateDriving();
			}
			this.UpdateEngine();
		}
		else
		{
			this.applyBrakes = this.ramExitAction.TrueDone();
		}
		if (flag3)
		{
			this.ApplyWaterPhysics();
		}
		else if (flag)
		{
			this.ApplyStableGroundPhysics();
		}
		else if (flag2)
		{
			this.ApplyUnstableGroundPhysics();
		}
		else
		{
			this.ApplyAirPhysics();
		}
		if (!flag3)
		{
			this.ApplyDownForce();
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x00096868 File Offset: 0x00094A68
	[Ignore]
	public bool CanTurnTowards(Vector3 deltaPosition)
	{
		if (this.tankTurning || deltaPosition.magnitude < 4f)
		{
			return true;
		}
		float num = 3f * Vector3.Cross(deltaPosition.normalized, base.transform.forward).magnitude + 1f;
		return deltaPosition.magnitude > num;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000968C4 File Offset: 0x00094AC4
	private void UpdateWheels()
	{
		int num = this.playerIsInside ? 1 : 2;
		this.wheelUpdateBaseIndex = (this.wheelUpdateBaseIndex + 1) % num;
		for (int i = this.wheelUpdateBaseIndex; i < this.wheels.Count; i += num)
		{
			this.wheels[i].UpdateWheel();
		}
		float num2 = 0f;
		foreach (CarWheel carWheel in this.wheels)
		{
			if (carWheel.isTouchingGround)
			{
				this.rigidbody.AddForceAtPosition(carWheel.acceleration, carWheel.touchGroundWorldPoint, ForceMode.Acceleration);
			}
			num2 = Mathf.Max(num2, carWheel.suspensionLoad);
		}
		if (this.rigidbody.velocity.magnitude > 0.5f && num2 > 0.3f)
		{
			this.PlayHighSuspensionLoadSound(0.5f + (num2 - 0.3f) * 1.5f);
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000104D1 File Offset: 0x0000E6D1
	protected override void OnPlayerEntered()
	{
		base.OnPlayerEntered();
		if (this.brakeSounds != null)
		{
			this.brakeSounds.SetPriority(2);
		}
		if (this.suspensionShiftSounds != null)
		{
			this.suspensionShiftSounds.SetPriority(3);
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000969D0 File Offset: 0x00094BD0
	protected override void OnPlayerExited()
	{
		if (Mathf.Abs(base.LocalVelocity().z) > 8f)
		{
			this.ramExitAction.Start();
		}
		base.OnPlayerExited();
		if (this.brakeSounds != null)
		{
			this.brakeSounds.ResetPriority();
		}
		if (this.suspensionShiftSounds != null)
		{
			this.suspensionShiftSounds.ResetPriority();
		}
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x00096A38 File Offset: 0x00094C38
	private void PlayHighSuspensionLoadSound(float volume)
	{
		if (this.highSuspensionLoadCooldown.TrueDone())
		{
			this.highSuspensionLoadCooldown.Start();
			if (this.suspensionShiftSounds != null)
			{
				this.suspensionShiftSounds.SetVolume(volume);
				this.suspensionShiftSounds.PlayRandom();
				return;
			}
		}
		else if (this.suspensionShiftSounds != null && this.suspensionShiftSounds.audioSource.volume < volume)
		{
			this.suspensionShiftSounds.SetVolume(volume);
		}
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0001050D File Offset: 0x0000E70D
	private void PlayBrakeSound(float volume)
	{
		if (this.brakeSounds != null && !this.brakeSounds.IsPlaying())
		{
			this.brakeSounds.SetVolume(volume);
			this.brakeSounds.PlayRandom();
		}
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00096AB0 File Offset: 0x00094CB0
	private int GetGroundedWheelsCount()
	{
		int num = 0;
		using (List<CarWheel>.Enumerator enumerator = this.wheels.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isTouchingGround)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x00010541 File Offset: 0x0000E741
	public bool IsChangingGears()
	{
		return !this.changeGearAction.TrueDone();
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x00010551 File Offset: 0x0000E751
	private void ChangeGear(bool forwardGear)
	{
		this.inReverseGear = !forwardGear;
		this.changeGearAction.Start();
		if (forwardGear)
		{
			this.engine.PlayShiftForwardSound();
			return;
		}
		this.engine.PlayShiftReverseSound();
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x00096B0C File Offset: 0x00094D0C
	private void UpdateEngine()
	{
		float num = 0.5f;
		float targetThrottle = 0f;
		Vector2 vector = base.Driver().controller.CarInput();
		if (this.IsChangingGears())
		{
			num = 0.5f;
		}
		else if (vector.y > 0.1f && !this.inReverseGear)
		{
			num = 0.5f + Mathf.Abs(vector.y) * 0.6f;
			targetThrottle = vector.y;
		}
		else if (vector.y < 0.1f && this.inReverseGear)
		{
			num = 0.5f + Mathf.Abs(vector.y) * 0.2f;
			targetThrottle = vector.y * 0.8f;
		}
		if (!this.highSuspensionLoadCooldown.TrueDone())
		{
			num = Mathf.Lerp(Mathf.Max(0.5f, num - 0.3f), num, this.highSuspensionLoadCooldown.Ratio());
		}
		this.engine.targetPitch = num;
		this.engine.targetThrottle = targetThrottle;
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x00096C00 File Offset: 0x00094E00
	private void ApplyDownForce()
	{
		Vector3 vector = base.LocalVelocity();
		float num = Mathf.Abs(vector.z);
		float num2 = Vector3.Dot(vector.normalized, Vector3.forward);
		float num3 = num * num2 * this.downforcePerSpeed;
		this.rigidbody.AddRelativeForce(new Vector3(0f, -num3, 0f), ForceMode.Acceleration);
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x00010582 File Offset: 0x0000E782
	private void ApplyAirPhysics()
	{
		this.rigidbody.centerOfMass = this.centerOfMassAir;
		this.rigidbody.drag = this.airDrag;
		this.rigidbody.angularDrag = this.airAngularDrag;
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000105B7 File Offset: 0x0000E7B7
	private void ApplyUnstableGroundPhysics()
	{
		this.rigidbody.centerOfMass = this.centerOfMassGrounded;
		this.rigidbody.drag = this.airDrag;
		this.rigidbody.angularDrag = this.airAngularDrag;
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x00096C58 File Offset: 0x00094E58
	private void ApplyStableGroundPhysics()
	{
		this.rigidbody.centerOfMass = this.centerOfMassGrounded;
		this.rigidbody.drag = this.groundDrag;
		this.rigidbody.angularDrag = this.groundAngularDrag;
		if (!this.driftAction.TrueDone())
		{
			this.rigidbody.angularDrag *= 1f - this.driftingSlip;
		}
		this.UpdateFriction();
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x000105EC File Offset: 0x0000E7EC
	private void ApplyWaterPhysics()
	{
		this.rigidbody.centerOfMass = this.centerOfMassGrounded;
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000105FF File Offset: 0x0000E7FF
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
		if (this.justSpawnedSettling)
		{
			this.EndSettleAction();
		}
		this.ChangeGear(true);
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x0001061C File Offset: 0x0000E81C
	[Ignore]
	public override void Die(DamageInfo info)
	{
		base.Die(info);
		this.ApplyAirPhysics();
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x00096CCC File Offset: 0x00094ECC
	private void UpdateDriving()
	{
		Vector2 vector = Vehicle.Clamp2(base.Driver().controller.CarInput());
		this.driverInput = vector;
		Vector3 vector2 = base.LocalVelocity();
		bool flag = false;
		if (base.Driver().controller.ForceStopVehicle())
		{
			this.applyBrakes = true;
		}
		else if (Mathf.Abs(vector2.z) > 0.5f)
		{
			this.applyBrakes = (Mathf.Sign(vector.y) != Mathf.Sign(vector2.z));
		}
		else if (this.tankTurning)
		{
			this.applyBrakes = (Mathf.Abs(vector.y) < 0.05f && Mathf.Abs(vector.x) < 0.05f);
		}
		else
		{
			this.applyBrakes = (Mathf.Abs(vector.y) < 0.05f);
		}
		if (this.applyBrakes && !flag && Mathf.Abs(vector2.z) > this.topSpeed / 2f)
		{
			this.PlayBrakeSound(Mathf.Clamp01(Mathf.Abs(vector2.z) / this.topSpeed));
		}
		if (this.brakeSounds != null && this.brakeSounds.IsPlaying())
		{
			float targetVolume = this.applyBrakes ? Mathf.Clamp01(0.3f + Mathf.Abs(vector2.z) / this.topSpeed) : 0f;
			this.brakeSounds.MoveVolume(targetVolume, 2f);
		}
		if (this.driftByBrake && this.applyBrakes && Mathf.Abs(vector2.z) > this.brakeDriftMinSpeed)
		{
			this.driftAction.Start();
		}
		float d = 0f;
		if (!this.IsChangingGears())
		{
			d = ((vector.y > 0f) ? (vector.y * this.acceleration) : (vector.y * this.reverseAcceleration));
			if (this.inReverseGear && vector.y > 0f && vector2.z > -0.1f)
			{
				this.ChangeGear(true);
			}
			if (!this.inReverseGear && vector.y < 0f && vector2.z < 0.1f)
			{
				if (vector2.z < 0f)
				{
					d = 0f;
				}
				if (this.changeToReverseTimeoutAction.TrueDone())
				{
					this.ChangeGear(false);
				}
			}
			else
			{
				this.changeToReverseTimeoutAction.Start();
			}
		}
		Vector3 force = this.rigidbody.transform.forward * d;
		Vector3 position = this.rigidbody.worldCenterOfMass - this.rigidbody.transform.up * this.accelerationTipAmount;
		this.rigidbody.AddForceAtPosition(force, position, ForceMode.Acceleration);
		float num = Mathf.Min(Mathf.Abs(this.speedTurnTorque * vector2.z), this.maxSpeedTorque);
		if (this.tankTurning)
		{
			if (!this.IsChangingGears())
			{
				num += this.baseTurnTorque;
			}
		}
		else
		{
			num += Mathf.Lerp(0f, this.baseTurnTorque, Mathf.Abs(vector2.z * 2f));
		}
		if (this.reverseTurnType == ArcadeCar.ReverseTurnType.ReverseThrottle)
		{
			num *= Mathf.Sign(vector.y);
		}
		else if (this.reverseTurnType == ArcadeCar.ReverseTurnType.ReverseSpeed && this.inReverseGear)
		{
			num = -num;
		}
		this.rigidbody.AddRelativeTorque(new Vector3(0f, vector.x * num, 0f), ForceMode.Acceleration);
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x00097038 File Offset: 0x00095238
	private void UpdateFriction()
	{
		Vector3 vector = base.LocalVelocity();
		float num = this.slideDrag;
		Vector3 vector2 = new Vector3(-vector.x * num, 0f, 0f);
		bool flag = this.applyBrakes && Mathf.Abs(vector.z) < 0.5f && Mathf.Abs(vector.x) < 1f && Mathf.Abs(vector.y) < 0.5f;
		if (this.justSpawnedSettling)
		{
			this.applyBrakes = true;
			if (this.justSpawnedSettleAction.TrueDone())
			{
				this.EndSettleAction();
			}
		}
		if (this.applyBrakes)
		{
			if (!base.HasDriver())
			{
				vector2.z = -vector.z * Mathf.Max(0.5f, this.brakeDrag);
			}
			else
			{
				vector2.z = -vector.z * this.brakeDrag;
			}
		}
		Vector3 force = base.transform.localToWorldMatrix.MultiplyVector(vector2);
		if (flag)
		{
			this.rigidbody.drag = 50f;
			this.rigidbody.AddForce(-this.rigidbody.velocity * 0.5f, ForceMode.VelocityChange);
		}
		Vector3 position = this.rigidbody.worldCenterOfMass - this.rigidbody.transform.up * this.frictionTipAmount;
		this.rigidbody.AddForceAtPosition(force, position, ForceMode.Acceleration);
		Vector3 torque = new Vector3(0f, this.rigidbody.angularVelocity.y * -this.groundSteeringDrag, 0f);
		this.rigidbody.AddRelativeTorque(torque, ForceMode.Acceleration);
	}

	// Token: 0x040015D3 RID: 5587
	private const float FORCE_STOP_FORWARD_SPEED = 0.5f;

	// Token: 0x040015D4 RID: 5588
	private const float FORCE_STOP_SIDEWAYS_SPEED = 1f;

	// Token: 0x040015D5 RID: 5589
	private const float SLEEP_THRESHOLD = 0.05f;

	// Token: 0x040015D6 RID: 5590
	private const int NUMBER_WHEEL_UPDATE_BUCKETS = 2;

	// Token: 0x040015D7 RID: 5591
	private const float HIGH_SUSPENSION_LOAD = 0.3f;

	// Token: 0x040015D8 RID: 5592
	private const float EMPTY_BRAKE_DRAG = 0.5f;

	// Token: 0x040015D9 RID: 5593
	private const float DONT_APPLY_BRAKES_WHEN_EXITING_SPEED = 8f;

	// Token: 0x040015DA RID: 5594
	public new float acceleration = 8f;

	// Token: 0x040015DB RID: 5595
	public float reverseAcceleration = 4f;

	// Token: 0x040015DC RID: 5596
	public float accelerationTipAmount = 0.3f;

	// Token: 0x040015DD RID: 5597
	public float frictionTipAmount = 0.1f;

	// Token: 0x040015DE RID: 5598
	public float topSpeed = 10f;

	// Token: 0x040015DF RID: 5599
	public float speedTurnTorque = 0.3f;

	// Token: 0x040015E0 RID: 5600
	public float baseTurnTorque = 3f;

	// Token: 0x040015E1 RID: 5601
	public bool tankTurning;

	// Token: 0x040015E2 RID: 5602
	public ArcadeCar.ReverseTurnType reverseTurnType;

	// Token: 0x040015E3 RID: 5603
	public float slideDrag = 5f;

	// Token: 0x040015E4 RID: 5604
	public float brakeDrag = 0.5f;

	// Token: 0x040015E5 RID: 5605
	public bool driftByAcceleration;

	// Token: 0x040015E6 RID: 5606
	public float brakeAccelerationTriggerSpeed = 0.5f;

	// Token: 0x040015E7 RID: 5607
	public bool driftByBrake = true;

	// Token: 0x040015E8 RID: 5608
	public float brakeDriftMinSpeed = 5f;

	// Token: 0x040015E9 RID: 5609
	public float driftingSlip = 0.5f;

	// Token: 0x040015EA RID: 5610
	public float driftDuration = 1.5f;

	// Token: 0x040015EB RID: 5611
	public bool isAmphibious;

	// Token: 0x040015EC RID: 5612
	public float extraStability;

	// Token: 0x040015ED RID: 5613
	public float groundDrag = 0.1f;

	// Token: 0x040015EE RID: 5614
	public float groundAngularDrag = 3f;

	// Token: 0x040015EF RID: 5615
	public float groundSteeringDrag;

	// Token: 0x040015F0 RID: 5616
	public float airDrag;

	// Token: 0x040015F1 RID: 5617
	public float airAngularDrag = 0.1f;

	// Token: 0x040015F2 RID: 5618
	public float downforcePerSpeed = 0.15f;

	// Token: 0x040015F3 RID: 5619
	public SoundBank suspensionShiftSounds;

	// Token: 0x040015F4 RID: 5620
	public SoundBank brakeSounds;

	// Token: 0x040015F5 RID: 5621
	private FloatingRigidbody floatPhysics;

	// Token: 0x040015F6 RID: 5622
	private float maxSpeedTorque;

	// Token: 0x040015F7 RID: 5623
	private TimedAction isStableGroundedAction = new TimedAction(0.1f, false);

	// Token: 0x040015F8 RID: 5624
	private bool applyBrakes = true;

	// Token: 0x040015F9 RID: 5625
	private TimedAction driftAction;

	// Token: 0x040015FA RID: 5626
	private TimedAction justSpawnedSettleAction = new TimedAction(2f, false);

	// Token: 0x040015FB RID: 5627
	private bool justSpawnedSettling;

	// Token: 0x040015FC RID: 5628
	[NonSerialized]
	public bool inReverseGear;

	// Token: 0x040015FD RID: 5629
	private TimedAction changeGearAction = new TimedAction(0.5f, false);

	// Token: 0x040015FE RID: 5630
	private TimedAction changeToReverseTimeoutAction = new TimedAction(0.2f, false);

	// Token: 0x040015FF RID: 5631
	private TimedAction highSuspensionLoadCooldown = new TimedAction(0.6f, false);

	// Token: 0x04001600 RID: 5632
	private TimedAction ramExitAction = new TimedAction(1f, false);

	// Token: 0x04001601 RID: 5633
	private Vector3 centerOfMassGrounded;

	// Token: 0x04001602 RID: 5634
	private Vector3 centerOfMassAir;

	// Token: 0x04001603 RID: 5635
	private Collider collider;

	// Token: 0x04001604 RID: 5636
	private int wheelUpdateBaseIndex;

	// Token: 0x02000315 RID: 789
	public enum ReverseTurnType
	{
		// Token: 0x04001606 RID: 5638
		ReverseSpeed,
		// Token: 0x04001607 RID: 5639
		ReverseThrottle,
		// Token: 0x04001608 RID: 5640
		Never
	}
}

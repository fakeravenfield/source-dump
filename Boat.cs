using System;
using Lua;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class Boat : Vehicle
{
	// Token: 0x0600147F RID: 5247 RVA: 0x0001062B File Offset: 0x0000E82B
	protected override void Awake()
	{
		base.Awake();
		this.rigidbody.centerOfMass = Vector3.down * this.stability;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x0001064E File Offset: 0x0000E84E
	protected override void OnDriverEntered()
	{
		base.OnDriverEntered();
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x00010656 File Offset: 0x0000E856
	protected override void DriverExited()
	{
		base.DriverExited();
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x0009731C File Offset: 0x0009551C
	[Ignore]
	public override void Die(DamageInfo info)
	{
		base.Die(info);
		this.sinkAction = new TimedAction(UnityEngine.Random.Range(5f, 10f), false);
		this.sinkAction.Start();
		this.sinkTorque = UnityEngine.Random.insideUnitSphere.normalized * this.sinkingMaxTorque;
		this.sinkTorque.y = this.sinkTorque.y * 0.1f;
		this.isSinking = true;
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x0001065E File Offset: 0x0000E85E
	protected override void PlayImpactSound(Vector3 position)
	{
		if (!this.dead)
		{
			base.PlayImpactSound(position);
		}
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x00097390 File Offset: 0x00095590
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		float num = this.floatAcceleration;
		if (this.isSinking)
		{
			float d = Mathf.Clamp01(base.transform.up.y);
			if (this.sinkAction.TrueDone())
			{
				num = this.sinkingFloatAcceleration;
				this.rigidbody.AddRelativeTorque(this.sinkTorque * d, ForceMode.Acceleration);
			}
			else
			{
				num = Mathf.Lerp(this.floatAcceleration, this.sinkingFloatAcceleration, this.sinkAction.Ratio());
				this.rigidbody.AddRelativeTorque(this.sinkTorque * this.sinkAction.Ratio() * d, ForceMode.Acceleration);
			}
		}
		int num2 = FloatingRigidbody.ApplyFloatingPhysics(this.floatingSamplers, this.rigidbody, this.floatDepth, num);
		this.inWater = (num2 >= 2);
		float targetPitch = base.HasDriver() ? 0.7f : 0f;
		if (this.inWater)
		{
			this.inWaterAction.Start();
		}
		if (this.dead)
		{
			return;
		}
		if (this.inWater && base.HasDriver())
		{
			Vector2 vector = base.Driver().controller.BoatInput() * this.engine.power;
			if (vector.y < 0f)
			{
				vector.y *= this.reverseMultiplier;
			}
			this.rigidbody.AddForce(base.transform.forward.ToGround().normalized * this.speed * vector.y, ForceMode.Acceleration);
			this.rigidbody.AddRelativeTorque(base.transform.up * this.turnSpeed * vector.x, ForceMode.Acceleration);
			targetPitch = 1f + 0.3f * Mathf.Clamp01(Mathf.Abs(vector.y) + Mathf.Abs(vector.x) * 0.5f);
		}
		this.engine.targetPitch = targetPitch;
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x0000476F File Offset: 0x0000296F
	[Ignore]
	public override bool IsWatercraft()
	{
		return true;
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x00097594 File Offset: 0x00095794
	[Ignore]
	public override bool IsInWater()
	{
		return !this.inWaterAction.TrueDone() && this.rigidbody.velocity.magnitude < 0.1f;
	}

	// Token: 0x04001609 RID: 5641
	public float floatAcceleration = 15f;

	// Token: 0x0400160A RID: 5642
	public float sinkingFloatAcceleration = 9f;

	// Token: 0x0400160B RID: 5643
	public float sinkingMaxTorque = 5f;

	// Token: 0x0400160C RID: 5644
	public float floatDepth = 0.5f;

	// Token: 0x0400160D RID: 5645
	public float speed = 5f;

	// Token: 0x0400160E RID: 5646
	public float reverseMultiplier = 0.5f;

	// Token: 0x0400160F RID: 5647
	public float turnSpeed = 5f;

	// Token: 0x04001610 RID: 5648
	public float stability = 1f;

	// Token: 0x04001611 RID: 5649
	public Transform[] floatingSamplers;

	// Token: 0x04001612 RID: 5650
	public bool requireDeepWater;

	// Token: 0x04001613 RID: 5651
	private const float SINK_TIME_MIN = 5f;

	// Token: 0x04001614 RID: 5652
	private const float SINK_TIME_MAX = 10f;

	// Token: 0x04001615 RID: 5653
	private TimedAction sinkAction;

	// Token: 0x04001616 RID: 5654
	private Vector3 sinkTorque;

	// Token: 0x04001617 RID: 5655
	private bool isSinking;

	// Token: 0x04001618 RID: 5656
	[NonSerialized]
	public bool inWater;

	// Token: 0x04001619 RID: 5657
	private float audioPitch = 1f;

	// Token: 0x0400161A RID: 5658
	private TimedAction inWaterAction = new TimedAction(2f, false);
}

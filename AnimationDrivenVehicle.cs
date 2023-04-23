using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class AnimationDrivenVehicle : Vehicle
{
	// Token: 0x0600145E RID: 5214 RVA: 0x00010413 File Offset: 0x0000E613
	protected override void Awake()
	{
		base.Awake();
		if (this.inputSmoothness > 0)
		{
			this.inputFilter = new MeanFilterVector3(this.inputSmoothness);
		}
		this.controller = base.GetComponent<CharacterController>();
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x00096448 File Offset: 0x00094648
	protected override void Update()
	{
		base.Update();
		if (this.dead)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		bool value = false;
		bool value2 = false;
		bool value3 = false;
		float value4 = 0f;
		if (base.HasDriver())
		{
			vector = Vehicle.Clamp2(base.Driver().controller.CarInput());
			value = base.Driver().controller.HoldingSprint();
			value3 = base.Driver().controller.Crouch();
			value2 = base.Driver().controller.Jump();
			value4 = base.Driver().controller.Lean();
			if (this.planeInput)
			{
				this.driverInput = Vehicle.Clamp4(base.Driver().controller.AirplaneInput());
			}
			else
			{
				this.driverInput = Vehicle.Clamp4(base.Driver().controller.HelicopterInput());
			}
		}
		if (this.inputSmoothness > 0)
		{
			vector = this.inputFilter.Tick(vector);
		}
		this.animator.SetFloat("input forward", vector.y);
		this.animator.SetFloat("input right", vector.x);
		this.animator.SetBool("moving", vector != Vector2.zero);
		this.animator.SetBool("sprint", value);
		this.animator.SetBool("jump", value2);
		this.animator.SetBool("crouch", value3);
		this.animator.SetFloat("lean", value4);
		this.animator.SetBool("touching ground", this.controller.isGrounded);
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x000965E4 File Offset: 0x000947E4
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.dead)
		{
			return;
		}
		if (this.controller.isGrounded)
		{
			this.velocity = Vector3.zero;
		}
		else
		{
			this.velocity += Physics.gravity * Time.deltaTime;
		}
		Vector3 motion = this.Velocity() * Time.deltaTime;
		this.controller.Move(motion);
		Vector3 vector = base.transform.eulerAngles;
		vector += this.animator.angularVelocity * Time.deltaTime * 57.29578f;
		vector.x = 0f;
		vector.z = 0f;
		base.transform.eulerAngles = vector;
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x000966B0 File Offset: 0x000948B0
	public override Vector3 Velocity()
	{
		Vector3 result = this.velocity + this.animator.velocity;
		result.y -= 0.1f;
		return result;
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x00010441 File Offset: 0x0000E641
	public override Vector3 AngularVelocity()
	{
		return this.animator.angularVelocity;
	}

	// Token: 0x040015CC RID: 5580
	public int inputSmoothness = 16;

	// Token: 0x040015CD RID: 5581
	public bool planeInput;

	// Token: 0x040015CE RID: 5582
	private CharacterController controller;

	// Token: 0x040015CF RID: 5583
	private MeanFilterVector3 inputFilter;

	// Token: 0x040015D0 RID: 5584
	private Vector3 velocity;

	// Token: 0x02000313 RID: 787
	[Serializable]
	public class GroundChecker
	{
		// Token: 0x040015D1 RID: 5585
		public Transform checker;

		// Token: 0x040015D2 RID: 5586
		public float rayLength = 1f;
	}
}

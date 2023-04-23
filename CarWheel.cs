using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class CarWheel : MonoBehaviour
{
	// Token: 0x06001492 RID: 5266 RVA: 0x00097A40 File Offset: 0x00095C40
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
		Matrix4x4 worldToLocalMatrix = this.vehicle.transform.worldToLocalMatrix;
		this.localRayOrigin = worldToLocalMatrix.MultiplyPoint(base.transform.position);
		this.localDownVector = worldToLocalMatrix.MultiplyVector(-base.transform.up);
		this.localOriginPosition = base.transform.localPosition;
		this.localOriginPosition.y = this.localOriginPosition.y + this.suspensionHeight;
		this.localRayOrigin.y = this.localRayOrigin.y + this.suspensionHeight;
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000106A3 File Offset: 0x0000E8A3
	private void Start()
	{
		this.vehicle.wheels.Add(this);
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00097ADC File Offset: 0x00095CDC
	private void Update()
	{
		if (this.wheelModel == null)
		{
			return;
		}
		Vector3 localEulerAngles = this.wheelModel.localEulerAngles;
		if (this.rotate)
		{
			float num;
			if (this.speedSampleTransform != null)
			{
				num = Vector3.Dot(this.speedSampleTransform.forward, this.vehicle.rigidbody.GetPointVelocity(this.speedSampleTransform.position));
			}
			else
			{
				num = this.vehicle.LocalVelocity().z;
			}
			float num2 = num / this.wheelRadius * 57.29578f;
			if (this.invertRotation)
			{
				num2 = -num2;
			}
			localEulerAngles.z += num2 * Time.deltaTime;
		}
		float target = 0f;
		if (this.vehicle.HasDriver())
		{
			target = this.vehicle.Driver().controller.CarInput().x;
		}
		this.turn = Mathf.MoveTowards(this.turn, target, Time.deltaTime * 5f);
		localEulerAngles.y = this.turn * this.turnAngle;
		this.wheelModel.localEulerAngles = localEulerAngles;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x00097BF8 File Offset: 0x00095DF8
	public void UpdateWheel()
	{
		if (this.vehicle.rigidbody.IsSleeping())
		{
			return;
		}
		Matrix4x4 cachedLocalToWorldMatrix = this.vehicle.cachedLocalToWorldMatrix;
		Vector3 vector = cachedLocalToWorldMatrix.MultiplyVector(this.localDownVector);
		Ray ray = new Ray(cachedLocalToWorldMatrix.MultiplyPoint(this.localRayOrigin), vector);
		Debug.DrawRay(ray.origin, ray.direction, Color.red);
		float num = this.suspensionHeight + this.wheelRadius;
		RaycastHit raycastHit;
		this.isTouchingGround = Physics.Raycast(ray, out raycastHit, num, 1);
		if (this.isTouchingGround)
		{
			base.transform.localPosition = this.localOriginPosition + new Vector3(0f, -raycastHit.distance + this.wheelRadius, 0f);
			this.suspensionLoad = 1f - raycastHit.distance / num;
			Vector3 b = Vector3.Cross(this.vehicle.rigidbody.angularVelocity, ray.origin - this.vehicle.rigidbody.worldCenterOfMass);
			Vector3 vector2 = this.vehicle.cachedWorldToLocalMatrix.MultiplyVector(this.vehicle.rigidbody.velocity + b);
			Vector3 b2 = ray.direction * this.suspensionDrag * vector2.y * Time.deltaTime;
			this.acceleration = (vector * -this.suspensionAcceleration + b2) * this.suspensionLoad;
			this.touchGroundWorldPoint = raycastHit.point;
			return;
		}
		this.suspensionLoad = 0f;
	}

	// Token: 0x04001629 RID: 5673
	private const int LAYER_MASK = 1;

	// Token: 0x0400162A RID: 5674
	private const float WHEEL_TURN_SPEED = 5f;

	// Token: 0x0400162B RID: 5675
	public float suspensionHeight = 0.5f;

	// Token: 0x0400162C RID: 5676
	public float wheelRadius = 0.3f;

	// Token: 0x0400162D RID: 5677
	public float suspensionAcceleration = 10f;

	// Token: 0x0400162E RID: 5678
	public float suspensionDrag = 100f;

	// Token: 0x0400162F RID: 5679
	public Transform wheelModel;

	// Token: 0x04001630 RID: 5680
	public bool rotate = true;

	// Token: 0x04001631 RID: 5681
	public bool invertRotation;

	// Token: 0x04001632 RID: 5682
	public float turnAngle = 20f;

	// Token: 0x04001633 RID: 5683
	[NonSerialized]
	public float suspensionLoad;

	// Token: 0x04001634 RID: 5684
	[NonSerialized]
	public bool isTouchingGround;

	// Token: 0x04001635 RID: 5685
	private float turn;

	// Token: 0x04001636 RID: 5686
	private Vector3 localRayOrigin;

	// Token: 0x04001637 RID: 5687
	private Vector3 localDownVector;

	// Token: 0x04001638 RID: 5688
	private Vector3 localOriginPosition;

	// Token: 0x04001639 RID: 5689
	private Vehicle vehicle;

	// Token: 0x0400163A RID: 5690
	[NonSerialized]
	public Vector3 acceleration = Vector3.zero;

	// Token: 0x0400163B RID: 5691
	[NonSerialized]
	public Vector3 touchGroundWorldPoint = Vector3.zero;

	// Token: 0x0400163C RID: 5692
	public Transform speedSampleTransform;
}

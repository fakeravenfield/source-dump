using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class WaterPlaning : MonoBehaviour
{
	// Token: 0x06001533 RID: 5427 RVA: 0x00010E5A File Offset: 0x0000F05A
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
		this.highImpactCooldown = new TimedAction(this.minTimeBetweenHighImpactSounds, false);
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0009A13C File Offset: 0x0009833C
	public Vector3 GetLowestPoint()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		float f = eulerAngles.z * 0.017453292f;
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		float num3 = Mathf.Pow(num, 2f);
		float num4 = Mathf.Pow(num2, 2f);
		float num5 = num3 / Mathf.Pow(this.width, 2f) + num4 / Mathf.Pow(this.height, 2f);
		float num6 = num4 / Mathf.Pow(this.width, 2f) + num3 / Mathf.Pow(this.height, 2f);
		float num7 = 2f * num * num2 * (1f / Mathf.Pow(this.width, 2f) - 1f / Mathf.Pow(this.height, 2f));
		float num8 = -1f / Mathf.Sqrt(num6 - Mathf.Pow(num7, 2f) / (4f * num5));
		float x = -num7 / (2f * num5) * num8;
		eulerAngles.z = 0f;
		Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(eulerAngles), Vector3.one);
		return base.transform.position + matrix4x.MultiplyVector(new Vector3(x, num8, 0f));
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x0009A290 File Offset: 0x00098490
	private void FixedUpdate()
	{
		Vector3 lowestPoint = this.GetLowestPoint();
		float num;
		if (WaterLevel.IsInWater(lowestPoint, out num))
		{
			Vector3 vector = base.transform.worldToLocalMatrix.MultiplyVector(this.vehicle.Velocity());
			float num2 = Mathf.Clamp01(num / this.floatDepth);
			float num3 = Mathf.Max(0f, vector.z - this.speedThreshold);
			float num4 = num2 * this.accelerationPerSpeed * num3;
			this.vehicle.rigidbody.AddForceAtPosition(new Vector3(0f, num4, 0f), lowestPoint, ForceMode.Acceleration);
			if (num4 > this.highImpactAccelerationThreshold)
			{
				if (this.highImpactParticles != null)
				{
					this.highImpactParticles.Play();
				}
				if (this.highImpactSoundBank != null && this.highImpactCooldown.TrueDone())
				{
					this.highImpactSoundBank.PlayRandom();
					this.highImpactCooldown.Start();
				}
			}
		}
	}

	// Token: 0x0400172F RID: 5935
	public float width = 3f;

	// Token: 0x04001730 RID: 5936
	public float height = 2f;

	// Token: 0x04001731 RID: 5937
	public float maxAngle = 120f;

	// Token: 0x04001732 RID: 5938
	public float floatDepth = 0.5f;

	// Token: 0x04001733 RID: 5939
	public float speedThreshold = 7f;

	// Token: 0x04001734 RID: 5940
	public float accelerationPerSpeed = 1f;

	// Token: 0x04001735 RID: 5941
	public ParticleSystem highImpactParticles;

	// Token: 0x04001736 RID: 5942
	public SoundBank highImpactSoundBank;

	// Token: 0x04001737 RID: 5943
	public float highImpactAccelerationThreshold = 5f;

	// Token: 0x04001738 RID: 5944
	public float minTimeBetweenHighImpactSounds = 0.3f;

	// Token: 0x04001739 RID: 5945
	private TimedAction highImpactCooldown;

	// Token: 0x0400173A RID: 5946
	private Vehicle vehicle;
}

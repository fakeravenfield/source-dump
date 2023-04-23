using System;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class SpinWhenBurning : MonoBehaviour
{
	// Token: 0x060014F8 RID: 5368 RVA: 0x00099738 File Offset: 0x00097938
	private void Awake()
	{
		this.vehicle = base.GetComponent<Vehicle>();
		this.torque = new Vector3(UnityEngine.Random.Range(this.minTorque.x, this.maxTorque.x), UnityEngine.Random.Range(this.minTorque.y, this.maxTorque.y), UnityEngine.Random.Range(this.minTorque.z, this.maxTorque.z));
		if (this.allowFlipX && UnityEngine.Random.Range(0, 1) == 1)
		{
			this.torque.x = -this.torque.x;
		}
		if (this.allowFlipY && UnityEngine.Random.Range(0, 1) == 1)
		{
			this.torque.y = -this.torque.y;
		}
		if (this.allowFlipZ && UnityEngine.Random.Range(0, 1) == 1)
		{
			this.torque.z = -this.torque.z;
		}
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x00099828 File Offset: 0x00097A28
	private void FixedUpdate()
	{
		if (this.vehicle.burning && !this.vehicle.dead)
		{
			this.vehicle.rigidbody.AddRelativeTorque(this.torque * this.vehicle.engine.power, ForceMode.Acceleration);
		}
	}

	// Token: 0x040016DA RID: 5850
	public Vector3 minTorque;

	// Token: 0x040016DB RID: 5851
	public Vector3 maxTorque;

	// Token: 0x040016DC RID: 5852
	public bool allowFlipX;

	// Token: 0x040016DD RID: 5853
	public bool allowFlipY;

	// Token: 0x040016DE RID: 5854
	public bool allowFlipZ;

	// Token: 0x040016DF RID: 5855
	private Vector3 torque;

	// Token: 0x040016E0 RID: 5856
	private Vehicle vehicle;
}

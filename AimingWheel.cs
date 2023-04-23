using System;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class AimingWheel : MonoBehaviour
{
	// Token: 0x06001509 RID: 5385 RVA: 0x000999AC File Offset: 0x00097BAC
	private void Update()
	{
		float num;
		if (this.axis == AimingWheel.Axis.X)
		{
			num = this.target.localEulerAngles.x;
		}
		else if (this.axis == AimingWheel.Axis.Y)
		{
			num = this.target.localEulerAngles.y;
		}
		else
		{
			num = this.target.localEulerAngles.z;
		}
		float z = Mathf.DeltaAngle(0f, num) * this.rotationMultiplier;
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		localEulerAngles.z = z;
		base.transform.localEulerAngles = localEulerAngles;
	}

	// Token: 0x040016EC RID: 5868
	public Transform target;

	// Token: 0x040016ED RID: 5869
	public float rotationMultiplier = 5f;

	// Token: 0x040016EE RID: 5870
	public AimingWheel.Axis axis;

	// Token: 0x040016EF RID: 5871
	private bool hasOldFacing;

	// Token: 0x040016F0 RID: 5872
	private Vector3 oldFacing = Vector3.zero;

	// Token: 0x02000339 RID: 825
	public enum Axis
	{
		// Token: 0x040016F2 RID: 5874
		X,
		// Token: 0x040016F3 RID: 5875
		Y,
		// Token: 0x040016F4 RID: 5876
		Z
	}
}

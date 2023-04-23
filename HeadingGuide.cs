using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class HeadingGuide : MonoBehaviour
{
	// Token: 0x060011D2 RID: 4562 RVA: 0x0000E02D File Offset: 0x0000C22D
	private void Awake()
	{
		this.rigidbody = base.GetComponentInParent<Rigidbody>();
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x0008D3C4 File Offset: 0x0008B5C4
	private void LateUpdate()
	{
		Quaternion to = base.transform.parent.rotation;
		if (this.rigidbody.velocity.magnitude > 0.2f)
		{
			to = Quaternion.LookRotation(this.rigidbody.velocity, this.rigidbody.transform.up);
		}
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, 200f * Time.deltaTime);
	}

	// Token: 0x040012F0 RID: 4848
	private const float ROTATION_SPEED = 200f;

	// Token: 0x040012F1 RID: 4849
	private const float VELOCITY_THRESHOLD = 0.2f;

	// Token: 0x040012F2 RID: 4850
	private Rigidbody rigidbody;
}

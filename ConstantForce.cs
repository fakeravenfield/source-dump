using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class ConstantForce : MonoBehaviour
{
	// Token: 0x06001127 RID: 4391 RVA: 0x0000D91A File Offset: 0x0000BB1A
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0000D928 File Offset: 0x0000BB28
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.force, ForceMode.Acceleration);
	}

	// Token: 0x0400126E RID: 4718
	private Rigidbody rigidbody;

	// Token: 0x0400126F RID: 4719
	public Vector3 force;
}

using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class SplashMove : MonoBehaviour
{
	// Token: 0x060010A5 RID: 4261 RVA: 0x0000D484 File Offset: 0x0000B684
	private void Update()
	{
		base.transform.localPosition += this.localVelocity * Time.deltaTime;
	}

	// Token: 0x040011DE RID: 4574
	public Vector3 localVelocity = Vector3.forward;
}

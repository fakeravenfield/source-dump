using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
public class CopyRotation : MonoBehaviour
{
	// Token: 0x06000960 RID: 2400 RVA: 0x0000846F File Offset: 0x0000666F
	private void LateUpdate()
	{
		base.transform.rotation = this.target.rotation;
	}

	// Token: 0x04000A4A RID: 2634
	public Transform target;
}

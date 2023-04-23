using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class ForceHeight : MonoBehaviour
{
	// Token: 0x06000964 RID: 2404 RVA: 0x0006AF74 File Offset: 0x00069174
	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		position.y = this.height;
		base.transform.position = position;
	}

	// Token: 0x04000A4C RID: 2636
	public float height;
}

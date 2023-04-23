using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class MB_ExampleMover : MonoBehaviour
{
	// Token: 0x060000E0 RID: 224 RVA: 0x000404CC File Offset: 0x0003E6CC
	private void Update()
	{
		Vector3 position = new Vector3(5f, 5f, 5f);
		ref Vector3 ptr = ref position;
		int index = this.axis;
		ptr[index] *= Mathf.Sin(Time.time);
		base.transform.position = position;
	}

	// Token: 0x0400008C RID: 140
	public int axis;
}

using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class KeepScale : MonoBehaviour
{
	// Token: 0x06000913 RID: 2323 RVA: 0x00069FCC File Offset: 0x000681CC
	private void LateUpdate()
	{
		Vector3 localScale = base.transform.parent.localScale;
		base.transform.localScale = new Vector3(this.scaleMultiplier.x / localScale.x, this.scaleMultiplier.y / localScale.y, this.scaleMultiplier.z / localScale.z);
	}

	// Token: 0x040009E2 RID: 2530
	public Vector3 scaleMultiplier = Vector3.one;
}

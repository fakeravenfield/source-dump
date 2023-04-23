using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class ForceRenderQueue : MonoBehaviour
{
	// Token: 0x06000901 RID: 2305 RVA: 0x00007E99 File Offset: 0x00006099
	private void OnEnable()
	{
		base.GetComponent<Renderer>().material.renderQueue = this.queue;
	}

	// Token: 0x040009BF RID: 2495
	public int queue;
}

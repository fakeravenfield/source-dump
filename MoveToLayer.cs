using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class MoveToLayer : MonoBehaviour
{
	// Token: 0x06001415 RID: 5141 RVA: 0x0001000C File Offset: 0x0000E20C
	private void Awake()
	{
		base.gameObject.layer = this.layer;
	}

	// Token: 0x0400158C RID: 5516
	public int layer;
}

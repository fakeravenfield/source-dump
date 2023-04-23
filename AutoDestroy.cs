using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class AutoDestroy : MonoBehaviour
{
	// Token: 0x060013C3 RID: 5059 RVA: 0x0000FC79 File Offset: 0x0000DE79
	private void Start()
	{
		base.Invoke("Cleanup", this.duration);
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x0000684B File Offset: 0x00004A4B
	private void Cleanup()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400153F RID: 5439
	public float duration = 10f;
}

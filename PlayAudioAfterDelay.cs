using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class PlayAudioAfterDelay : MonoBehaviour
{
	// Token: 0x060005CE RID: 1486 RVA: 0x000058CC File Offset: 0x00003ACC
	private void OnEnable()
	{
		base.Invoke("Play", this.delay);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x000058DF File Offset: 0x00003ADF
	private void OnDisable()
	{
		base.CancelInvoke();
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x000058E7 File Offset: 0x00003AE7
	private void Play()
	{
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x040005C2 RID: 1474
	public float delay;
}

using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class MenuMusic : MonoBehaviour
{
	// Token: 0x06000D63 RID: 3427 RVA: 0x0007C954 File Offset: 0x0007AB54
	private void Update()
	{
		if (!this.audio.isPlaying)
		{
			this.audio.PlayOneShot(this.clips[this.index]);
			this.index++;
			if (this.index >= this.clips.Length)
			{
				this.index = 0;
			}
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0000AD02 File Offset: 0x00008F02
	public void Play()
	{
		this.audio.PlayOneShot(this.clips[0]);
		this.index = 0;
	}

	// Token: 0x04000E84 RID: 3716
	public AudioSource audio;

	// Token: 0x04000E85 RID: 3717
	public AudioClip[] clips;

	// Token: 0x04000E86 RID: 3718
	private int index;
}

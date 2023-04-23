using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class RepeatAudio : MonoBehaviour
{
	// Token: 0x060005D2 RID: 1490 RVA: 0x000058F4 File Offset: 0x00003AF4
	private void Start()
	{
		this.audio = base.GetComponent<AudioSource>();
		base.InvokeRepeating("Play", this.startTime, this.repeatTime);
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00005919 File Offset: 0x00003B19
	private void Play()
	{
		this.audio.Play();
	}

	// Token: 0x040005C3 RID: 1475
	public float startTime = 1f;

	// Token: 0x040005C4 RID: 1476
	public float repeatTime = 30f;

	// Token: 0x040005C5 RID: 1477
	private AudioSource audio;
}

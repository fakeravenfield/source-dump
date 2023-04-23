using System;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class SpeedSound : MonoBehaviour
{
	// Token: 0x06000957 RID: 2391 RVA: 0x0006ACB4 File Offset: 0x00068EB4
	private void Awake()
	{
		this.rigidbody = base.GetComponentInParent<Rigidbody>();
		this.audio = base.GetComponent<AudioSource>();
		this.maxVolume = this.audio.volume;
		this.audio.volume = 0f;
		this.audio.Stop();
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0006AD08 File Offset: 0x00068F08
	private void Update()
	{
		float target = Mathf.Clamp01((this.rigidbody.velocity.magnitude - this.minSpeed) / (this.maxSpeed - this.minSpeed)) * this.maxVolume;
		this.audio.volume = Mathf.MoveTowards(this.audio.volume, target, Time.deltaTime);
		if (!this.audio.isPlaying && this.audio.volume > 0f)
		{
			this.audio.Play();
			return;
		}
		if (this.audio.isPlaying && this.audio.volume == 0f)
		{
			this.audio.Stop();
		}
	}

	// Token: 0x04000A3B RID: 2619
	public float minSpeed = 5f;

	// Token: 0x04000A3C RID: 2620
	public float maxSpeed = 10f;

	// Token: 0x04000A3D RID: 2621
	private float maxVolume = 1f;

	// Token: 0x04000A3E RID: 2622
	private Rigidbody rigidbody;

	// Token: 0x04000A3F RID: 2623
	private AudioSource audio;
}

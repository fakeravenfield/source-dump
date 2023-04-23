using System;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x0200012E RID: 302
public class AudioVolume : MonoBehaviour
{
	// Token: 0x060008AF RID: 2223 RVA: 0x00068B7C File Offset: 0x00066D7C
	private void Start()
	{
		base.GetComponent<AudioSource>().volume *= Mathf.Clamp01(PlayerPrefs.GetFloat("s_" + this.optionId.ToString(), 1f));
		if (this.autoPlay)
		{
			base.Invoke("Play", this.autoPlayDelay);
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00068BE0 File Offset: 0x00066DE0
	private void Play()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		VideoPlayer component2 = base.GetComponent<VideoPlayer>();
		if (component2 != null)
		{
			component2.Play();
			return;
		}
		component.Play();
	}

	// Token: 0x0400094C RID: 2380
	public OptionSlider.Id optionId;

	// Token: 0x0400094D RID: 2381
	public bool autoPlay = true;

	// Token: 0x0400094E RID: 2382
	public float autoPlayDelay;
}

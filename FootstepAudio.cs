using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020000B0 RID: 176
public class FootstepAudio : MonoBehaviour
{
	// Token: 0x060005C6 RID: 1478 RVA: 0x0005D8D4 File Offset: 0x0005BAD4
	public void Awake()
	{
		FootstepAudio.instance = this;
		this.audioSources = base.GetComponentsInChildren<AudioSource>();
		AudioSource[] array = this.audioSources;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].volume = 1f;
		}
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0005D918 File Offset: 0x0005BB18
	public static void Play(Vector3 position, FootstepAudio.FootstepType type, bool isProne, bool hasSight)
	{
		if (type == FootstepAudio.FootstepType.Indoor && isProne)
		{
			type = FootstepAudio.FootstepType.Terrain;
		}
		AudioClip clip;
		switch (type)
		{
		case FootstepAudio.FootstepType.Terrain:
			clip = FootstepAudio.GetOutdoorClip();
			break;
		case FootstepAudio.FootstepType.Indoor:
			clip = FootstepAudio.GetIndoorClip();
			break;
		case FootstepAudio.FootstepType.Water:
			clip = FootstepAudio.GetWaterClip();
			break;
		default:
			clip = FootstepAudio.GetOutdoorClip();
			break;
		}
		AudioSource audioSource = FootstepAudio.GetAudioSource();
		audioSource.transform.position = position;
		audioSource.outputAudioMixerGroup = (hasSight ? FootstepAudio.instance.sightOutputGroup : FootstepAudio.instance.noSightOutputGroup);
		if (isProne)
		{
			audioSource.pitch = 0.5f;
		}
		else
		{
			audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.05f);
		}
		audioSource.PlayOneShot(clip);
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00005861 File Offset: 0x00003A61
	public static AudioClip GetRandomClip(AudioClip[] clips, ref int clipIndex)
	{
		clipIndex = (clipIndex + UnityEngine.Random.Range(0, clips.Length - 1)) % clips.Length;
		return clips[clipIndex];
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0000587B File Offset: 0x00003A7B
	public static AudioClip GetIndoorClip()
	{
		return FootstepAudio.GetRandomClip(FootstepAudio.instance.indoorClips, ref FootstepAudio.instance.indoorIndex);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00005896 File Offset: 0x00003A96
	public static AudioClip GetOutdoorClip()
	{
		return FootstepAudio.GetRandomClip(FootstepAudio.instance.outdoorClips, ref FootstepAudio.instance.outdoorIndex);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x000058B1 File Offset: 0x00003AB1
	public static AudioClip GetWaterClip()
	{
		return FootstepAudio.GetRandomClip(FootstepAudio.instance.waterClips, ref FootstepAudio.instance.waterIndex);
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0005D9C0 File Offset: 0x0005BBC0
	private static AudioSource GetAudioSource()
	{
		FootstepAudio.instance.audioSourceIndex = (FootstepAudio.instance.audioSourceIndex + UnityEngine.Random.Range(0, FootstepAudio.instance.audioSources.Length - 1)) % FootstepAudio.instance.audioSources.Length;
		return FootstepAudio.instance.audioSources[FootstepAudio.instance.audioSourceIndex];
	}

	// Token: 0x040005B3 RID: 1459
	public static FootstepAudio instance;

	// Token: 0x040005B4 RID: 1460
	public AudioClip[] indoorClips;

	// Token: 0x040005B5 RID: 1461
	public AudioClip[] outdoorClips;

	// Token: 0x040005B6 RID: 1462
	public AudioClip[] waterClips;

	// Token: 0x040005B7 RID: 1463
	public AudioMixerGroup sightOutputGroup;

	// Token: 0x040005B8 RID: 1464
	public AudioMixerGroup noSightOutputGroup;

	// Token: 0x040005B9 RID: 1465
	private AudioSource[] audioSources;

	// Token: 0x040005BA RID: 1466
	private int audioSourceIndex;

	// Token: 0x040005BB RID: 1467
	private int indoorIndex;

	// Token: 0x040005BC RID: 1468
	private int outdoorIndex;

	// Token: 0x040005BD RID: 1469
	private int waterIndex;

	// Token: 0x020000B1 RID: 177
	public enum FootstepType
	{
		// Token: 0x040005BF RID: 1471
		Terrain,
		// Token: 0x040005C0 RID: 1472
		Indoor,
		// Token: 0x040005C1 RID: 1473
		Water
	}
}

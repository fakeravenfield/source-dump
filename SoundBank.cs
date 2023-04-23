using System;
using Lua;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class SoundBank : MonoBehaviour
{
	// Token: 0x060005D5 RID: 1493 RVA: 0x0005DA18 File Offset: 0x0005BC18
	public virtual void Start()
	{
		try
		{
			if (this.audioSource == null)
			{
				this.audioSource = base.GetComponent<AudioSource>();
			}
			if (this.audioSource != null && this.audioSource.outputAudioMixerGroup == null)
			{
				this.audioSource.outputAudioMixerGroup = GameManager.instance.worldMixerGroup;
			}
			this.lastIndex = UnityEngine.Random.Range(0, this.clips.Length);
			this.originalPriority = this.audioSource.priority;
			this.originalVolume = this.audioSource.volume;
			if (this.audioSource.playOnAwake)
			{
				this.audioSource.Stop();
				this.PlayRandom();
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00005944 File Offset: 0x00003B44
	public void PlayRandom()
	{
		this.lastIndex = (this.lastIndex + UnityEngine.Random.Range(1, this.clips.Length)) % this.clips.Length;
		this.PlaySoundBank(this.lastIndex);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00005976 File Offset: 0x00003B76
	public void PlaySoundBank(int index)
	{
		this.audioSource.PlayOneShot(this.clips[index]);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0000598B File Offset: 0x00003B8B
	[Ignore]
	public void SetPriority(int priority)
	{
		this.audioSource.priority = priority;
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00005999 File Offset: 0x00003B99
	[Ignore]
	public void ResetPriority()
	{
		this.audioSource.priority = this.originalPriority;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x000059AC File Offset: 0x00003BAC
	public void SetVolume(float volume)
	{
		this.audioSource.volume = this.originalVolume * volume;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0005DAE4 File Offset: 0x0005BCE4
	[Ignore]
	public void MoveVolume(float targetVolume, float speed)
	{
		float target = this.originalVolume * targetVolume;
		this.audioSource.volume = Mathf.MoveTowards(this.audioSource.volume, target, speed * Time.deltaTime);
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x000059C1 File Offset: 0x00003BC1
	public bool IsPlaying()
	{
		return this.audioSource.isPlaying;
	}

	// Token: 0x040005C6 RID: 1478
	public AudioClip[] clips;

	// Token: 0x040005C7 RID: 1479
	public AudioSource audioSource;

	// Token: 0x040005C8 RID: 1480
	private int lastIndex;

	// Token: 0x040005C9 RID: 1481
	private int originalPriority;

	// Token: 0x040005CA RID: 1482
	private float originalVolume = 1f;
}

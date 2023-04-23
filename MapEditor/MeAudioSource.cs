using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000627 RID: 1575
	public class MeAudioSource : MonoBehaviour, IPropertyChangeNotify
	{
		// Token: 0x06002870 RID: 10352 RVA: 0x0001BE29 File Offset: 0x0001A029
		private void Awake()
		{
			this.OnPropertyChanged();
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000FA764 File Offset: 0x000F8964
		public void OnPropertyChanged()
		{
			if (this.audioSource == null)
			{
				this.audioSource = base.gameObject.AddComponent<AudioSource>();
			}
			this.audioSource.clip = this.audioClip.audioClip;
			this.audioSource.volume = this.volume;
			this.audioSource.minDistance = this.minDistance;
			this.audioSource.maxDistance = this.maxDistance;
			this.audioSource.loop = true;
			this.audioSource.playOnAwake = true;
			bool flag = this.maxDistance > 1000f;
			this.audioSource.spatialBlend = (flag ? 0f : 1f);
			if (this.audioSource.clip && !this.audioSource.isPlaying)
			{
				this.audioSource.Play();
			}
		}

		// Token: 0x0400266D RID: 9837
		public AudioAsset audioClip;

		// Token: 0x0400266E RID: 9838
		[Range(0f, 1f)]
		public float volume = 1f;

		// Token: 0x0400266F RID: 9839
		public float minDistance = 1f;

		// Token: 0x04002670 RID: 9840
		public float maxDistance = 500f;

		// Token: 0x04002671 RID: 9841
		private AudioSource audioSource;
	}
}

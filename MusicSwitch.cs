using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class MusicSwitch : MonoBehaviour
{
	// Token: 0x06000D83 RID: 3459 RVA: 0x0000AF0A File Offset: 0x0000910A
	public void Awake()
	{
		if (PlayerPrefs.HasKey("unlockedmusic"))
		{
			this.backgroundSound.SetActive(false);
			this.backgroundMusic.SetActive(true);
		}
	}

	// Token: 0x04000E9C RID: 3740
	public const string KEY = "unlockedmusic";

	// Token: 0x04000E9D RID: 3741
	public GameObject backgroundSound;

	// Token: 0x04000E9E RID: 3742
	public GameObject backgroundMusic;
}

using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class Mouth : MonoBehaviour
{
	// Token: 0x0600069D RID: 1693 RVA: 0x00006338 File Offset: 0x00004538
	private void Awake()
	{
		this.material = base.GetComponent<Renderer>().materials[this.mouthMaterialIndex];
		this.material.mainTextureScale = new Vector2(1f, 0.125f);
		this.Idle();
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0005FADC File Offset: 0x0005DCDC
	public void RandomPhoneme()
	{
		int phonemeIndex = (this.lastPhoneme + UnityEngine.Random.Range(3, 8)) % 8;
		this.lastPhoneme = phonemeIndex;
		this.ShowPhoneme(phonemeIndex);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00006372 File Offset: 0x00004572
	public void Idle()
	{
		this.ShowPhoneme(7);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0000637B File Offset: 0x0000457B
	private void ShowPhoneme(int phonemeIndex)
	{
		this.material.mainTextureOffset = new Vector2(0f, 0.125f * (float)phonemeIndex) + this.uvOffset;
	}

	// Token: 0x0400067C RID: 1660
	private const int PHONEMES_PER_SHEET = 8;

	// Token: 0x0400067D RID: 1661
	private const int IDLE_PHONEME = 7;

	// Token: 0x0400067E RID: 1662
	private const float PHONEME_SHEET_OFFSET = 0.125f;

	// Token: 0x0400067F RID: 1663
	private const float PHONEME_TIME = 0.1f;

	// Token: 0x04000680 RID: 1664
	private const float WORD_PAUSE_TIME = 0.3f;

	// Token: 0x04000681 RID: 1665
	private const int MIN_PHONEMES_PER_WORD = 4;

	// Token: 0x04000682 RID: 1666
	private const int MAX_PHONEMES_PER_WORD = 8;

	// Token: 0x04000683 RID: 1667
	private int phonemesPerWord = 4;

	// Token: 0x04000684 RID: 1668
	private int lastPhoneme;

	// Token: 0x04000685 RID: 1669
	private bool idle = true;

	// Token: 0x04000686 RID: 1670
	public int mouthMaterialIndex;

	// Token: 0x04000687 RID: 1671
	public Vector2 uvOffset = Vector2.zero;

	// Token: 0x04000688 RID: 1672
	private Material material;
}

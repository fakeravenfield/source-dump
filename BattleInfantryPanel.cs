using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017A RID: 378
public class BattleInfantryPanel : MonoBehaviour
{
	// Token: 0x060009CD RID: 2509 RVA: 0x00008920 File Offset: 0x00006B20
	public void SetTicketCount(int count)
	{
		this.label.text = count.ToString();
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00008934 File Offset: 0x00006B34
	public void SetActive(bool active)
	{
		if (!active)
		{
			this.CrossfadeAlpha(0.25f, 0f);
			return;
		}
		this.CrossfadeAlpha(1f, 0.5f);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0000895A File Offset: 0x00006B5A
	public void Die()
	{
		this.icon.enabled = false;
		this.label.enabled = false;
		this.deadObject.SetActive(true);
		this.StopFlashing();
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00008986 File Offset: 0x00006B86
	public void StartFlashing()
	{
		base.StartCoroutine(this.Flash());
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00008995 File Offset: 0x00006B95
	public void StopFlashing()
	{
		base.StopAllCoroutines();
		this.CrossfadeAlpha(1f, 0.3f);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000089AD File Offset: 0x00006BAD
	private IEnumerator Flash()
	{
		for (;;)
		{
			this.CrossfadeAlpha(0.5f, 0.3f);
			yield return new WaitForSecondsRealtime(0.3f);
			this.CrossfadeAlpha(1f, 0.3f);
			yield return new WaitForSecondsRealtime(1.3f);
		}
		yield break;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x000089BC File Offset: 0x00006BBC
	private void CrossfadeAlpha(float targetAlpha, float duration)
	{
		this.icon.CrossFadeAlpha(targetAlpha, duration, true);
		this.label.CrossFadeAlpha(targetAlpha, duration, true);
	}

	// Token: 0x04000ABA RID: 2746
	private const float INACTIVE_ALPHA = 0.25f;

	// Token: 0x04000ABB RID: 2747
	private const float FLASHING_ALPHA = 0.5f;

	// Token: 0x04000ABC RID: 2748
	private const float FLASHING_CROSSFADE_DURATION = 0.3f;

	// Token: 0x04000ABD RID: 2749
	private const float FLASHING_CROSSFADE_PAUSE = 1f;

	// Token: 0x04000ABE RID: 2750
	public RawImage background;

	// Token: 0x04000ABF RID: 2751
	public RawImage icon;

	// Token: 0x04000AC0 RID: 2752
	public Text label;

	// Token: 0x04000AC1 RID: 2753
	public GameObject deadObject;
}

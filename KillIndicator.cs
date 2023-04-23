using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B3 RID: 691
public class KillIndicator : MonoBehaviour
{
	// Token: 0x06001253 RID: 4691 RVA: 0x0008F134 File Offset: 0x0008D334
	private void Awake()
	{
		this.background.canvasRenderer.SetColor(new Color(1f, 1f, 1f, 0f));
		this.text.text = "";
		this.mask.anchorMin = new Vector2(0f, 0f);
		this.mask.anchorMax = new Vector2(0f, 1f);
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x0000E74B File Offset: 0x0000C94B
	public void ShowMessage(string message)
	{
		if (GameManager.gameOver)
		{
			return;
		}
		base.StopAllCoroutines();
		this.text.text = message;
		base.StartCoroutine(this.ShowMessageRoutine());
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0000E774 File Offset: 0x0000C974
	private IEnumerator ShowMessageRoutine()
	{
		this.background.canvasRenderer.SetColor(new Color(1f, 1f, 1f, 0f));
		this.background.CrossFadeAlpha(1f, 0.05f, false);
		this.maskAction.Start();
		this.maskFadeIn = true;
		yield return new WaitForSeconds(2f);
		this.maskAction.Start();
		this.maskFadeIn = false;
		yield return new WaitForSeconds(0.2f);
		this.background.CrossFadeAlpha(0f, 0.1f, false);
		yield break;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0000E783 File Offset: 0x0000C983
	public void Hide()
	{
		base.StopAllCoroutines();
		this.background.gameObject.SetActive(false);
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x0008F1B0 File Offset: 0x0008D3B0
	private void Update()
	{
		float x = 1f;
		float x2 = 0f;
		if (this.maskFadeIn)
		{
			x = (this.maskAction.TrueDone() ? 1f : this.maskAction.Ratio());
		}
		else
		{
			x2 = (this.maskAction.TrueDone() ? 1f : this.maskAction.Ratio());
		}
		this.text.rectTransform.pivot = new Vector2(this.maskFadeIn ? 0f : 1f, 0.5f);
		this.text.rectTransform.anchorMin = new Vector2(this.maskFadeIn ? 0f : 1f, 0f);
		this.text.rectTransform.anchorMax = new Vector2(this.maskFadeIn ? 0f : 1f, 1f);
		this.mask.anchorMin = new Vector2(x2, 0f);
		this.mask.anchorMax = new Vector2(x, 1f);
	}

	// Token: 0x0400138B RID: 5003
	private const float BACKGROUND_FADE_IN_TIME = 0.05f;

	// Token: 0x0400138C RID: 5004
	private const float BACKGROUND_FADE_OUT_TIME = 0.1f;

	// Token: 0x0400138D RID: 5005
	private const float MASK_TIME = 0.2f;

	// Token: 0x0400138E RID: 5006
	private const float PAUSE_TIME = 2f;

	// Token: 0x0400138F RID: 5007
	public Image background;

	// Token: 0x04001390 RID: 5008
	public RectTransform mask;

	// Token: 0x04001391 RID: 5009
	public Text text;

	// Token: 0x04001392 RID: 5010
	private TimedAction maskAction = new TimedAction(0.2f, false);

	// Token: 0x04001393 RID: 5011
	private bool maskFadeIn = true;
}

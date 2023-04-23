using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CE RID: 718
public class OverlayUi : MonoBehaviour
{
	// Token: 0x0600131E RID: 4894 RVA: 0x0000F296 File Offset: 0x0000D496
	public void Awake()
	{
		OverlayUi.instance = this;
		this.canvas = base.GetComponent<Canvas>();
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x0000F2AA File Offset: 0x0000D4AA
	private void Update()
	{
		this.canvas.enabled = IngameUi.instance.canvas.enabled;
		this.overlayText.enabled = !this.showOverlayAction.TrueDone();
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x0000F2DF File Offset: 0x0000D4DF
	public static void ShowOverlayText(string text, float duration = 3.5f)
	{
		OverlayUi.instance.overlayText.text = text;
		OverlayUi.instance.showOverlayAction.StartLifetime(duration);
	}

	// Token: 0x04001494 RID: 5268
	public static OverlayUi instance;

	// Token: 0x04001495 RID: 5269
	public Text overlayText;

	// Token: 0x04001496 RID: 5270
	private TimedAction showOverlayAction = new TimedAction(4f, false);

	// Token: 0x04001497 RID: 5271
	private Canvas canvas;
}

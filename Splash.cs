using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025F RID: 607
public class Splash : MonoBehaviour
{
	// Token: 0x060010A1 RID: 4257 RVA: 0x0008A4CC File Offset: 0x000886CC
	private void Start()
	{
		this.fadeInAction.Start();
		this.presentsTitleAction.Start();
		float duration = UnityEngine.Object.FindObjectOfType<GotoMenu>().duration;
		this.endAction.StartLifetime(duration);
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0000D40A File Offset: 0x0000B60A
	private void Update()
	{
		new Color(1f, 1f, 1f, 1f - Mathf.Clamp01(3f - 3f * this.presentsTitleAction.Ratio()));
		this.UpdateOverlay();
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0008A508 File Offset: 0x00088708
	private void UpdateOverlay()
	{
		Color black = Color.black;
		black.a = Mathf.Clamp01(2f - 2f * this.fadeInAction.Ratio()) + (1f - Mathf.Clamp01(this.endAction.Remaining() - 1f));
		this.overlay.color = black;
	}

	// Token: 0x040011DA RID: 4570
	public Image overlay;

	// Token: 0x040011DB RID: 4571
	private TimedAction fadeInAction = new TimedAction(2f, false);

	// Token: 0x040011DC RID: 4572
	private TimedAction presentsTitleAction = new TimedAction(4f, false);

	// Token: 0x040011DD RID: 4573
	private TimedAction endAction = new TimedAction(20f, false);
}

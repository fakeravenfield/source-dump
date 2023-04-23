using System;
using System.Collections;
using Ravenfield.Dialog;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D0 RID: 208
public class IngameDialog : DialogPlayerBase
{
	// Token: 0x06000651 RID: 1617 RVA: 0x00005F49 File Offset: 0x00004149
	public override void Awake()
	{
		base.Awake();
		IngameDialog.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.dialogContainerRect = (RectTransform)this.dialogContainer.transform;
		IngameDialog.HideInstant();
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00005F7E File Offset: 0x0000417E
	private void Start()
	{
		base.StartCoroutine(this.SetupFontSize());
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00005F8D File Offset: 0x0000418D
	private IEnumerator SetupFontSize()
	{
		int sortingOrder = this.canvas.sortingOrder;
		this.canvas.sortingOrder = -10000;
		IngameDialog.ShowInstant();
		yield return new WaitForEndOfFrame();
		DialogPlayerBase.SetFontSize(this.text, 3);
		IngameDialog.HideInstant();
		this.canvas.sortingOrder = sortingOrder;
		yield break;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0005F154 File Offset: 0x0005D354
	private void Update()
	{
		this.actor.forceMouthIdle = !this.isPrintingNormalCharacter;
		if (!this.appearAction.TrueDone())
		{
			float y = this.isVisible ? this.appearAction.Ratio() : (1f - this.appearAction.Ratio());
			this.dialogContainerRect.localScale = new Vector3(1f, y, 1f);
		}
		else if (this.isVisible)
		{
			this.dialogContainerRect.localScale = Vector3.one;
		}
		else
		{
			this.dialogContainer.SetActive(false);
		}
		this.grain.uvRect = new Rect(new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)), Vector2.one);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00005F9C File Offset: 0x0000419C
	public static void SetName(string name)
	{
		IngameDialog.instance.title.text = name;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00005FAE File Offset: 0x000041AE
	public static void SetText(string text)
	{
		IngameDialog.instance.text.text = text;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00005FC0 File Offset: 0x000041C0
	public static void PrintText(string text)
	{
		IngameDialog.instance.StartPrinting(IngameDialog.instance.text, text, 0, false);
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00005FD9 File Offset: 0x000041D9
	public override void OnPrintDone()
	{
		base.OnPrintDone();
		this.actor.StopTalking();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0005F224 File Offset: 0x0005D424
	public static void PrintActorText(string actorPose, string text, string overrideName = "")
	{
		if (IngameDialog.instance.actorPrintCoroutine != null)
		{
			IngameDialog.instance.StopCoroutine(IngameDialog.instance.actorPrintCoroutine);
		}
		IngameDialog.CancelAutoHide();
		IngameDialog.instance.actorPrintCoroutine = IngameDialog.instance.StartCoroutine(IngameDialog.instance.PrintActorTextCoroutine(actorPose, text, overrideName));
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00005FEC File Offset: 0x000041EC
	public void CrossFadeGrainAlpha(float alpha, float time)
	{
		this.grain.CrossFadeAlpha(alpha, time, false);
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00005FFC File Offset: 0x000041FC
	private IEnumerator PrintActorTextCoroutine(string actorPose, string text, string overrideName)
	{
		IngameDialog.SetName("");
		IngameDialog.SetText("");
		this.CrossFadeGrainAlpha(1f, 0f);
		if (!this.isVisible)
		{
			this.actor.Hide();
			IngameDialog.Show();
			yield return new WaitForSeconds(0.2f);
		}
		this.actor.TriggerPose(actorPose);
		yield return new WaitForSeconds(0.2f);
		this.CrossFadeGrainAlpha(0f, 0.2f);
		yield return new WaitForSeconds(0.3f);
		this.blipSound = this.actor.blipSound;
		IngameDialog.SetName(string.IsNullOrEmpty(overrideName) ? this.actor.pose.defaultDisplayName : overrideName);
		IngameDialog.PrintText(text);
		this.actor.StartTalking();
		yield break;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00006020 File Offset: 0x00004220
	private static void CancelAutoHide()
	{
		if (IngameDialog.instance.autoHideCoroutine != null)
		{
			IngameDialog.instance.StopCoroutine(IngameDialog.instance.autoHideCoroutine);
		}
		IngameDialog.instance.autoHideCoroutine = null;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0000604D File Offset: 0x0000424D
	public static void HideAfter(float delay)
	{
		IngameDialog.CancelAutoHide();
		IngameDialog.instance.autoHideCoroutine = IngameDialog.instance.StartCoroutine(IngameDialog.HideAfterCoroutine(delay));
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0000606E File Offset: 0x0000426E
	private static IEnumerator HideAfterCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		IngameDialog.Hide();
		IngameDialog.instance.autoHideCoroutine = null;
		yield break;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0000607D File Offset: 0x0000427D
	public static void Show()
	{
		IngameDialog.instance.isVisible = true;
		IngameDialog.instance.appearAction.Start();
		IngameDialog.instance.dialogContainer.SetActive(true);
		IngameDialog.CancelAutoHide();
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0005F278 File Offset: 0x0005D478
	public static void ShowInstant()
	{
		IngameDialog.instance.isVisible = true;
		IngameDialog.instance.dialogContainer.SetActive(true);
		IngameDialog.instance.dialogContainerRect.localScale = new Vector3(1f, 1f, 1f);
		IngameDialog.instance.appearAction.Stop();
		IngameDialog.CancelAutoHide();
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0005F2D8 File Offset: 0x0005D4D8
	public static void Hide()
	{
		IngameDialog.instance.isVisible = false;
		IngameDialog.instance.appearAction.Start();
		IngameDialog.instance.actor.Hide();
		IngameDialog.instance.CrossFadeGrainAlpha(1f, 0f);
		IngameDialog.CancelAutoHide();
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x000060AE File Offset: 0x000042AE
	public static void HideInstant()
	{
		IngameDialog.instance.isVisible = false;
		IngameDialog.instance.dialogContainer.SetActive(false);
		IngameDialog.instance.appearAction.Stop();
		IngameDialog.CancelAutoHide();
	}

	// Token: 0x0400063B RID: 1595
	private const int TEXT_LINES = 3;

	// Token: 0x0400063C RID: 1596
	public static IngameDialog instance;

	// Token: 0x0400063D RID: 1597
	public GameObject dialogContainer;

	// Token: 0x0400063E RID: 1598
	public Text title;

	// Token: 0x0400063F RID: 1599
	public Text text;

	// Token: 0x04000640 RID: 1600
	public SpriteDialogActor actor;

	// Token: 0x04000641 RID: 1601
	public RawImage grain;

	// Token: 0x04000642 RID: 1602
	private RectTransform dialogContainerRect;

	// Token: 0x04000643 RID: 1603
	private bool isVisible;

	// Token: 0x04000644 RID: 1604
	private TimedAction appearAction = new TimedAction(0.1f, false);

	// Token: 0x04000645 RID: 1605
	private Coroutine actorPrintCoroutine;

	// Token: 0x04000646 RID: 1606
	private Coroutine autoHideCoroutine;

	// Token: 0x04000647 RID: 1607
	private Canvas canvas;
}

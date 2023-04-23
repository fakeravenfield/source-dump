using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000295 RID: 661
public class DetectionBlip : MonoBehaviour
{
	// Token: 0x0600119F RID: 4511 RVA: 0x0000DE03 File Offset: 0x0000C003
	private void Awake()
	{
		base.gameObject.SetActive(false);
		this.rectTransform = (base.transform as RectTransform);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0008C928 File Offset: 0x0008AB28
	public void Activate(Actor actor)
	{
		if (FpsActorController.instance.GetActiveCamera().WorldToScreenPoint(actor.CenterPosition()).z <= 0f)
		{
			return;
		}
		this.actor = actor;
		this.blipAction.Start();
		base.gameObject.SetActive(true);
		base.StartCoroutine(this.BlipCoroutine());
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0000DE22 File Offset: 0x0000C022
	private IEnumerator BlipCoroutine()
	{
		this.blob.CrossFadeAlpha(0f, 0f, true);
		this.ring.CrossFadeAlpha(0f, 0f, true);
		yield return -1;
		this.blob.CrossFadeAlpha(1f, 0.15f, false);
		this.ring.CrossFadeAlpha(1f, 0.15f, false);
		yield return new WaitForSeconds(0.15f);
		this.blob.CrossFadeAlpha(0f, 0.15f, false);
		yield return new WaitForSeconds(0.15f);
		this.ring.CrossFadeAlpha(0f, 0.15f, false);
		yield return new WaitForSeconds(0.15f);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x0008C984 File Offset: 0x0008AB84
	private void LateUpdate()
	{
		float num = 0.2f + 0.8f * (this.blipAction.Ratio() * 2f);
		this.blob.rectTransform.localScale = new Vector3(num, num, num);
		float num2 = this.blipAction.Ratio();
		this.ring.rectTransform.localScale = new Vector3(num2, num2, num2);
		Vector3 v = FpsActorController.instance.GetActiveCamera().WorldToScreenPoint(this.actor.CenterPosition());
		this.rectTransform.anchoredPosition = v;
	}

	// Token: 0x040012B7 RID: 4791
	private const float BLIP_DURATION = 0.3f;

	// Token: 0x040012B8 RID: 4792
	private const float BLIP_DURATION_HALF = 0.15f;

	// Token: 0x040012B9 RID: 4793
	private const float BLIP_DURATION_QUARTER = 0.15f;

	// Token: 0x040012BA RID: 4794
	public Graphic blob;

	// Token: 0x040012BB RID: 4795
	public Graphic ring;

	// Token: 0x040012BC RID: 4796
	private RectTransform rectTransform;

	// Token: 0x040012BD RID: 4797
	private Actor actor;

	// Token: 0x040012BE RID: 4798
	private TimedAction blipAction = new TimedAction(0.6f, false);
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000290 RID: 656
public class ButtonHoverTransition : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600118C RID: 4492 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
	private void OnEnable()
	{
		this.overlayGraphic.CrossFadeColor(this.normalColor, 0.01f, false, true);
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x0000DCFA File Offset: 0x0000BEFA
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.hovering = true;
		if (!this.overrideColor)
		{
			this.overlayGraphic.CrossFadeColor(this.hoverColor, this.transitionTime, false, true);
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0000DD24 File Offset: 0x0000BF24
	public void OnPointerExit(PointerEventData eventData)
	{
		this.hovering = false;
		if (!this.overrideColor)
		{
			this.overlayGraphic.CrossFadeColor(this.normalColor, this.transitionTime, false, true);
		}
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0000DD4E File Offset: 0x0000BF4E
	public void OverrideColor(Color color)
	{
		this.overrideColor = true;
		this.overlayGraphic.CrossFadeColor(color, this.transitionTime, false, true);
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0000DD6B File Offset: 0x0000BF6B
	public void ReleaseOverride()
	{
		this.overrideColor = false;
		this.overlayGraphic.CrossFadeColor(this.hovering ? this.hoverColor : this.normalColor, this.transitionTime, false, true);
	}

	// Token: 0x0400129F RID: 4767
	public Graphic overlayGraphic;

	// Token: 0x040012A0 RID: 4768
	public Color normalColor;

	// Token: 0x040012A1 RID: 4769
	public Color hoverColor;

	// Token: 0x040012A2 RID: 4770
	private bool overrideColor;

	// Token: 0x040012A3 RID: 4771
	private bool hovering;

	// Token: 0x040012A4 RID: 4772
	public float transitionTime = 0.1f;
}

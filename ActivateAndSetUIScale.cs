using System;
using UnityEngine.UI;

// Token: 0x0200028A RID: 650
public class ActivateAndSetUIScale : ActivateChildren
{
	// Token: 0x06001164 RID: 4452 RVA: 0x0008BCC8 File Offset: 0x00089EC8
	public override void Awake()
	{
		base.Awake();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			CanvasScaler component = base.transform.GetChild(i).GetComponent<CanvasScaler>();
			if (component != null && component.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize && GameManager.UI_SCALE <= 1.01f)
			{
				component.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
				component.scaleFactor = 1f;
			}
		}
	}
}

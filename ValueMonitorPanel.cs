using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000128 RID: 296
public class ValueMonitorPanel : MonoBehaviour
{
	// Token: 0x0600089A RID: 2202 RVA: 0x00007A5C File Offset: 0x00005C5C
	public void Initialize(string titleText, string valueText)
	{
		this.title.text = titleText;
		this.value.text = valueText;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00007A76 File Offset: 0x00005C76
	public void UpdateValueText(string text)
	{
		this.value.text = text;
		this.changeBlink.CrossFadeColor(this.blinkColor, 0f, true, true);
		this.changeBlink.CrossFadeColor(this.idleColor, 0.4f, true, true);
	}

	// Token: 0x04000932 RID: 2354
	public Color blinkColor;

	// Token: 0x04000933 RID: 2355
	public Color idleColor;

	// Token: 0x04000934 RID: 2356
	public Image changeBlink;

	// Token: 0x04000935 RID: 2357
	public Text title;

	// Token: 0x04000936 RID: 2358
	public Text value;
}

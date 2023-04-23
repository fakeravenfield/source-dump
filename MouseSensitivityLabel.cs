using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BE RID: 702
public class MouseSensitivityLabel : MonoBehaviour
{
	// Token: 0x060012BE RID: 4798 RVA: 0x0000ED10 File Offset: 0x0000CF10
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.id = base.transform.parent.GetComponent<OptionSlider>().id;
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x00090FBC File Offset: 0x0008F1BC
	private void Update()
	{
		this.text.text = (Mathf.Round(Options.GetScaledMouseSensitivity(this.id) * 100f) / 100f).ToString();
	}

	// Token: 0x040013FD RID: 5117
	private Text text;

	// Token: 0x040013FE RID: 5118
	private OptionSlider.Id id;
}

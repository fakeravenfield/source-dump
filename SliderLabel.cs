using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D5 RID: 725
public class SliderLabel : MonoBehaviour
{
	// Token: 0x0600133B RID: 4923 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00092434 File Offset: 0x00090634
	private void Update()
	{
		this.text.text = this.prefix + (Mathf.Round(this.slider.value * (float)this.decimalMultiplier) / (float)this.decimalMultiplier).ToString();
	}

	// Token: 0x040014B1 RID: 5297
	private Text text;

	// Token: 0x040014B2 RID: 5298
	public Slider slider;

	// Token: 0x040014B3 RID: 5299
	public int decimalMultiplier = 1;

	// Token: 0x040014B4 RID: 5300
	public string prefix = "";
}

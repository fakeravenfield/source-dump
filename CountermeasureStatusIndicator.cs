using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A2 RID: 674
public class CountermeasureStatusIndicator : MonoBehaviour
{
	// Token: 0x060011DE RID: 4574 RVA: 0x0008D634 File Offset: 0x0008B834
	public void Update()
	{
		if (this.vehicle != null)
		{
			bool flag = this.vehicle.CountermeasuresAreReady();
			if (this.textIndicator != null)
			{
				this.textIndicator.text = (flag ? this.readyText : this.notReadyText);
			}
			Color color = flag ? this.readyColor : this.notReadyColor;
			Graphic[] array = this.tintTargets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = color;
			}
			if (this.readyObject != null)
			{
				this.readyObject.SetActive(flag);
			}
			if (this.notReadyObject != null)
			{
				this.notReadyObject.SetActive(!flag);
			}
		}
	}

	// Token: 0x040012FB RID: 4859
	public Vehicle vehicle;

	// Token: 0x040012FC RID: 4860
	public Text textIndicator;

	// Token: 0x040012FD RID: 4861
	public string readyText = "";

	// Token: 0x040012FE RID: 4862
	public string notReadyText = "";

	// Token: 0x040012FF RID: 4863
	public Graphic[] tintTargets;

	// Token: 0x04001300 RID: 4864
	public Color readyColor;

	// Token: 0x04001301 RID: 4865
	public Color notReadyColor;

	// Token: 0x04001302 RID: 4866
	public GameObject readyObject;

	// Token: 0x04001303 RID: 4867
	public GameObject notReadyObject;
}

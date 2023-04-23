using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A4 RID: 676
public class RefreshRateText : MonoBehaviour
{
	// Token: 0x060011E3 RID: 4579 RVA: 0x0008D74C File Offset: 0x0008B94C
	private void Awake()
	{
		base.GetComponent<Text>().text = Screen.currentResolution.refreshRate.ToString() + " Hz";
	}
}

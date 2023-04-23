using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class ValueMonitorCanvas : MonoBehaviour
{
	// Token: 0x06000896 RID: 2198 RVA: 0x00007A27 File Offset: 0x00005C27
	private void Awake()
	{
		ValueMonitorCanvas.instance = this;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00007A2F File Offset: 0x00005C2F
	public static bool IsAvailable()
	{
		return ValueMonitorCanvas.instance != null;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00007A3C File Offset: 0x00005C3C
	public static ValueMonitorPanel CreateValueMonitor()
	{
		return UnityEngine.Object.Instantiate<GameObject>(ValueMonitorCanvas.instance.valuePanelPrefab, ValueMonitorCanvas.instance.container).GetComponent<ValueMonitorPanel>();
	}

	// Token: 0x0400092F RID: 2351
	private static ValueMonitorCanvas instance;

	// Token: 0x04000930 RID: 2352
	public GameObject valuePanelPrefab;

	// Token: 0x04000931 RID: 2353
	public RectTransform container;
}

using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class FramerateLimiter : MonoBehaviour
{
	// Token: 0x06001408 RID: 5128 RVA: 0x000953A4 File Offset: 0x000935A4
	private void Start()
	{
		Application.targetFrameRate = Mathf.Clamp(Screen.currentResolution.refreshRate, 30, 200);
	}
}

using System;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class TakeScreenshot : MonoBehaviour
{
	// Token: 0x0600112F RID: 4399 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Start()
	{
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x0000D94E File Offset: 0x0000BB4E
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			ScreenCapture.CaptureScreenshot("screenshot.png");
		}
	}
}

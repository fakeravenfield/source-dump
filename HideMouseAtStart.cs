using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class HideMouseAtStart : MonoBehaviour
{
	// Token: 0x06000BDC RID: 3036 RVA: 0x00009C55 File Offset: 0x00007E55
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

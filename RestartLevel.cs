using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class RestartLevel : MonoBehaviour
{
	// Token: 0x06001423 RID: 5155 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Start()
	{
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000100A4 File Offset: 0x0000E2A4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

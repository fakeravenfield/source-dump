using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class LevelTester : MonoBehaviour
{
	// Token: 0x06000BFB RID: 3067 RVA: 0x00009E35 File Offset: 0x00008035
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			this.InstantiateManagers();
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00009E4A File Offset: 0x0000804A
	private void InstantiateManagers()
	{
		UnityEngine.Object.Instantiate<GameObject>(Resources.Load("_Managers") as GameObject);
	}
}

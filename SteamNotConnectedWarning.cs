using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class SteamNotConnectedWarning : MonoBehaviour
{
	// Token: 0x060010C6 RID: 4294 RVA: 0x0000D5F5 File Offset: 0x0000B7F5
	private void Awake()
	{
		this.target.SetActive(false);
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0000D603 File Offset: 0x0000B803
	private void Update()
	{
		this.target.SetActive(!GameManager.IsConnectedToSteam());
	}

	// Token: 0x0400123D RID: 4669
	public GameObject target;
}

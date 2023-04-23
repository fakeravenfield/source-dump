using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class SplashPanel : MonoBehaviour
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x0000B133 File Offset: 0x00009333
	public void Update()
	{
		this.textObject.SetActive(ModManager.instance.contentHasFinishedLoading);
	}

	// Token: 0x04000ECD RID: 3789
	public GameObject textObject;
}

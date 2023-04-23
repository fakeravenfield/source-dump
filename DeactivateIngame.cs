using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class DeactivateIngame : MonoBehaviour
{
	// Token: 0x06000B5D RID: 2909 RVA: 0x0000969C File Offset: 0x0000789C
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class TestOverrideSpawnUI : MonoBehaviour
{
	// Token: 0x0600114F RID: 4431 RVA: 0x0000DA07 File Offset: 0x0000BC07
	private void Start()
	{
		base.StartCoroutine(this.WaitABitLol());
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0000DA16 File Offset: 0x0000BC16
	private IEnumerator WaitABitLol()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("Override");
		LoadoutUi.instance.SetLoadoutOverride(base.gameObject);
		yield break;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}
}

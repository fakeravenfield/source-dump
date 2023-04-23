using System;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class TestCapturePointIsFrontline : MonoBehaviour
{
	// Token: 0x06001134 RID: 4404 RVA: 0x0000D963 File Offset: 0x0000BB63
	private void Start()
	{
		base.Invoke("Test", 1f);
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0008BA78 File Offset: 0x00089C78
	private void Test()
	{
		CapturePoint component = base.GetComponent<CapturePoint>();
		Debug.Log("Is frontline? " + component.IsFrontLine().ToString());
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0000296E File Offset: 0x00000B6E
	private void Update()
	{
	}
}

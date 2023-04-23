using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class SplashRotor : MonoBehaviour
{
	// Token: 0x060010A7 RID: 4263 RVA: 0x0000D4BF File Offset: 0x0000B6BF
	private void LateUpdate()
	{
		base.transform.localEulerAngles = new Vector3(0f, 0f, 1000f * Time.time);
	}

	// Token: 0x040011DF RID: 4575
	private const float ROTOR_SPEED = 1000f;
}

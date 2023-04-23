using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class PitchRollGuide : MonoBehaviour
{
	// Token: 0x06001322 RID: 4898 RVA: 0x0000F31A File Offset: 0x0000D51A
	private void LateUpdate()
	{
		base.transform.eulerAngles = new Vector3(0f, base.transform.parent.eulerAngles.y, 0f);
	}
}

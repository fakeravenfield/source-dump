using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class RollGuide : MonoBehaviour
{
	// Token: 0x06001324 RID: 4900 RVA: 0x00092274 File Offset: 0x00090474
	private void LateUpdate()
	{
		base.transform.eulerAngles = new Vector3(base.transform.parent.eulerAngles.x, base.transform.parent.eulerAngles.y, 0f);
	}
}

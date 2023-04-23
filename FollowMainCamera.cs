using System;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class FollowMainCamera : MonoBehaviour
{
	// Token: 0x06000962 RID: 2402 RVA: 0x0006AF2C File Offset: 0x0006912C
	private void LateUpdate()
	{
		Camera mainCamera = GameManager.GetMainCamera();
		base.transform.position = mainCamera.transform.position;
		if (this.followRotation)
		{
			base.transform.rotation = mainCamera.transform.rotation;
		}
	}

	// Token: 0x04000A4B RID: 2635
	public bool followRotation;
}

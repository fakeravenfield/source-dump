using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D0 RID: 1488
	public class FaceCurrentCamera : MonoBehaviour
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		private void LateUpdate()
		{
			this.FaceCamera();
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		private void Update()
		{
			this.FaceCamera();
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000F4BDC File Offset: 0x000F2DDC
		private void FaceCamera()
		{
			if (Camera.current)
			{
				Vector3 worldPosition = 2f * base.transform.position - Camera.current.transform.position;
				base.transform.LookAt(worldPosition);
			}
		}
	}
}

using System;
using UnityEngine;

namespace MapMagicDemo
{
	// Token: 0x020004AC RID: 1196
	public class NearPlaneIncrease : MonoBehaviour
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x000C4DE4 File Offset: 0x000C2FE4
		private void Update()
		{
			int num = (int)(Mathf.Max(Camera.main.transform.position.x, Camera.main.transform.position.z) / 1000f);
			if (num != this.oldDistFactor)
			{
				Camera.main.nearClipPlane = Mathf.Max(0.66f, (float)num / 25f);
				this.oldDistFactor = num;
			}
		}

		// Token: 0x04001E76 RID: 7798
		private int oldDistFactor;
	}
}

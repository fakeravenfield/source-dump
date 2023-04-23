using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D6 RID: 1494
	public class ScaleLineWithDistance : MonoBehaviour, IAllowInPrefabRenderer
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x000F5328 File Offset: 0x000F3528
		private void LateUpdate()
		{
			if (!this.line)
			{
				this.line = base.GetComponentInChildren<LineRenderer>();
			}
			Camera camera = MapEditor.instance.GetCamera();
			Vector3 position = camera.WorldToScreenPoint(base.transform.position) + new Vector3(0f, 1f, 0f);
			Vector3 position2 = base.transform.position;
			float num = (camera.ScreenToWorldPoint(position) - position2).magnitude * this.multiplier;
			if (this.maxWidth > 0f)
			{
				num = Mathf.Min(num, this.maxWidth);
			}
			if (this.minWidth > 0f)
			{
				num = Mathf.Max(num, this.minWidth);
			}
			this.line.widthMultiplier = num;
		}

		// Token: 0x040024F4 RID: 9460
		private const float VIEWPORT_VERTICAL_SIZE = 560f;

		// Token: 0x040024F5 RID: 9461
		public float multiplier = 1f;

		// Token: 0x040024F6 RID: 9462
		public float maxWidth;

		// Token: 0x040024F7 RID: 9463
		public float minWidth = 0.08f;

		// Token: 0x040024F8 RID: 9464
		private LineRenderer line;
	}
}

using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000603 RID: 1539
	public class ScaleGizmo : TransformGizmo
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x0001B419 File Offset: 0x00019619
		public void Reset()
		{
			this.deltaScale = Vector3.zero;
		}

		// Token: 0x04002585 RID: 9605
		public Vector3 deltaScale;
	}
}

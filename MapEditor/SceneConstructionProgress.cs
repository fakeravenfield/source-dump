using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005E8 RID: 1512
	public class SceneConstructionProgress : EventArgs
	{
		// Token: 0x060026DA RID: 9946 RVA: 0x0001ADFB File Offset: 0x00018FFB
		public SceneConstructionProgress(string status, float progress)
		{
			this.status = status;
			this.progress = Mathf.Clamp01(progress);
		}

		// Token: 0x04002517 RID: 9495
		public readonly string status;

		// Token: 0x04002518 RID: 9496
		public readonly float progress;
	}
}

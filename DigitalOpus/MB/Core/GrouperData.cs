using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000420 RID: 1056
	[Serializable]
	public class GrouperData
	{
		// Token: 0x04001BF4 RID: 7156
		public bool clusterOnLMIndex;

		// Token: 0x04001BF5 RID: 7157
		public bool clusterByLODLevel;

		// Token: 0x04001BF6 RID: 7158
		public Vector3 origin;

		// Token: 0x04001BF7 RID: 7159
		public Vector3 cellSize;

		// Token: 0x04001BF8 RID: 7160
		public int pieNumSegments = 4;

		// Token: 0x04001BF9 RID: 7161
		public Vector3 pieAxis = Vector3.up;

		// Token: 0x04001BFA RID: 7162
		public int height = 1;

		// Token: 0x04001BFB RID: 7163
		public float maxDistBetweenClusters = 1f;

		// Token: 0x04001BFC RID: 7164
		public bool includeCellsWithOnlyOneRenderer = true;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005A RID: 90
[Serializable]
public class PaverRoad
{
	// Token: 0x04000132 RID: 306
	public List<PaverRoad.Segment> segments;

	// Token: 0x0200005B RID: 91
	[Serializable]
	public class Segment
	{
		// Token: 0x04000133 RID: 307
		public Vector3 position;

		// Token: 0x04000134 RID: 308
		public Vector3 forwardPosition;
	}
}

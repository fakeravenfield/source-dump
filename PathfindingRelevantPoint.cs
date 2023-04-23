using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class PathfindingRelevantPoint : MonoBehaviour
{
	// Token: 0x04001108 RID: 4360
	public PathfindingRelevantPoint.Type type;

	// Token: 0x02000247 RID: 583
	public enum Type
	{
		// Token: 0x0400110A RID: 4362
		Ground,
		// Token: 0x0400110B RID: 4363
		Water
	}
}

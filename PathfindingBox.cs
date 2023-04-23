using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class PathfindingBox : MonoBehaviour
{
	// Token: 0x040010FB RID: 4347
	public PathfindingBox.Type type;

	// Token: 0x040010FC RID: 4348
	public bool tiled = true;

	// Token: 0x040010FD RID: 4349
	[HideInInspector]
	[Obsolete]
	public bool automaticCellSize = true;

	// Token: 0x040010FE RID: 4350
	public float cellSize = 1f;

	// Token: 0x040010FF RID: 4351
	public float characterRadius = 2f;

	// Token: 0x04001100 RID: 4352
	public float climbHeight = 0.5f;

	// Token: 0x04001101 RID: 4353
	public float coverPointSpacing = 0.05f;

	// Token: 0x04001102 RID: 4354
	public PathfindingBox[] blockers;

	// Token: 0x04001103 RID: 4355
	[Range(0f, 90f)]
	public float maxSlope = 35f;

	// Token: 0x02000245 RID: 581
	public enum Type
	{
		// Token: 0x04001105 RID: 4357
		Infantry,
		// Token: 0x04001106 RID: 4358
		Car,
		// Token: 0x04001107 RID: 4359
		Boat
	}
}

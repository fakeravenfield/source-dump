using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class TerrainTexturer : MonoBehaviour
{
	// Token: 0x04001868 RID: 6248
	public AnimationCurve cliffTextureFalloff;

	// Token: 0x04001869 RID: 6249
	public bool oceanFloor;

	// Token: 0x0400186A RID: 6250
	public float oceanFloorHeight = 5f;

	// Token: 0x0400186B RID: 6251
	public float oceanFloorSmear = 3f;

	// Token: 0x0400186C RID: 6252
	public bool patchy;

	// Token: 0x0400186D RID: 6253
	public AnimationCurve patchiness;

	// Token: 0x0400186E RID: 6254
	public float patchinessFrequency = 10f;

	// Token: 0x0400186F RID: 6255
	[Range(0f, 128f)]
	public int detailAmount = 4;

	// Token: 0x04001870 RID: 6256
	[Range(0f, 1f)]
	public float rubbleAmount = 0.1f;

	// Token: 0x04001871 RID: 6257
	[Range(0f, 1f)]
	public float otherDetailChance = 0.1f;
}

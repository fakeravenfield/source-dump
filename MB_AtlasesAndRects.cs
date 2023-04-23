using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000036 RID: 54
[Serializable]
public class MB_AtlasesAndRects
{
	// Token: 0x04000099 RID: 153
	public Texture2D[] atlases;

	// Token: 0x0400009A RID: 154
	[NonSerialized]
	public List<MB_MaterialAndUVRect> mat2rect_map;

	// Token: 0x0400009B RID: 155
	public string[] texPropertyNames;
}

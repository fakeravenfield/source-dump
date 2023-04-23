using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
[Serializable]
public class MB_MultiMaterial
{
	// Token: 0x0400009C RID: 156
	public Material combinedMaterial;

	// Token: 0x0400009D RID: 157
	public bool considerMeshUVs;

	// Token: 0x0400009E RID: 158
	public List<Material> sourceMaterials = new List<Material>();
}

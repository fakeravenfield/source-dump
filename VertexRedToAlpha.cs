using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class VertexRedToAlpha : MonoBehaviour
{
	// Token: 0x06000983 RID: 2435 RVA: 0x0006B5A0 File Offset: 0x000697A0
	private void Awake()
	{
		Mesh mesh = base.GetComponent<MeshFilter>().mesh;
		Color[] colors = mesh.colors;
		for (int i = 0; i < mesh.colors.Length; i++)
		{
			colors[i].a = colors[i].r;
		}
		mesh.colors = colors;
	}
}

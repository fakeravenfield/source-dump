using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[Serializable]
public class ActorAccessory
{
	// Token: 0x06000256 RID: 598 RVA: 0x00003C03 File Offset: 0x00001E03
	public ActorAccessory(Mesh mesh, Material[] materials)
	{
		this.mesh = mesh;
		this.materials = materials;
	}

	// Token: 0x0400022D RID: 557
	public Mesh mesh;

	// Token: 0x0400022E RID: 558
	public Material[] materials;
}

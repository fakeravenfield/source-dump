using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class CustomActorModel : MonoBehaviour
{
	// Token: 0x06000346 RID: 838 RVA: 0x0004CDE8 File Offset: 0x0004AFE8
	public void FixShaders()
	{
		if (this.materials != null)
		{
			Material[] array = this.materials;
			for (int i = 0; i < array.Length; i++)
			{
				FixBundleShaders.FixMaterialShader(array[i]);
			}
		}
		if (this.armMaterials != null)
		{
			Material[] array = this.armMaterials;
			for (int i = 0; i < array.Length; i++)
			{
				FixBundleShaders.FixMaterialShader(array[i]);
			}
		}
		if (this.kickLegMaterials != null)
		{
			Material[] array = this.kickLegMaterials;
			for (int i = 0; i < array.Length; i++)
			{
				FixBundleShaders.FixMaterialShader(array[i]);
			}
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0004CE68 File Offset: 0x0004B068
	public ActorSkin ToActorSkin()
	{
		return new ActorSkin
		{
			characterSkin = new ActorSkin.MeshSkin(this.actorMesh, this.materials, this.actorMaterial),
			armSkin = new ActorSkin.MeshSkin(this.armMesh, this.armMaterials, this.armActorMaterial),
			kickLegSkin = new ActorSkin.MeshSkin(this.kickLegMesh, this.kickLegMaterials, this.kickLegActorMaterial)
		};
	}

	// Token: 0x040002CA RID: 714
	public SpawnPoint.Team team;

	// Token: 0x040002CB RID: 715
	public Mesh actorMesh;

	// Token: 0x040002CC RID: 716
	public Material[] materials;

	// Token: 0x040002CD RID: 717
	public int actorMaterial;

	// Token: 0x040002CE RID: 718
	public Mesh armMesh;

	// Token: 0x040002CF RID: 719
	public Material[] armMaterials;

	// Token: 0x040002D0 RID: 720
	public int armActorMaterial;

	// Token: 0x040002D1 RID: 721
	public Mesh kickLegMesh;

	// Token: 0x040002D2 RID: 722
	public Material[] kickLegMaterials;

	// Token: 0x040002D3 RID: 723
	public int kickLegActorMaterial;
}

using System;
using Lua.Wrapper;
using UnityEngine;

// Token: 0x02000074 RID: 116
[Serializable]
public class ActorSkin
{
	// Token: 0x06000341 RID: 833 RVA: 0x000041F2 File Offset: 0x000023F2
	public override string ToString()
	{
		return this.name;
	}

	// Token: 0x040002BD RID: 701
	public string name = "New Skin";

	// Token: 0x040002BE RID: 702
	public ActorSkin.MeshSkin characterSkin;

	// Token: 0x040002BF RID: 703
	public ActorSkin.MeshSkin armSkin;

	// Token: 0x040002C0 RID: 704
	public ActorSkin.MeshSkin kickLegSkin;

	// Token: 0x040002C1 RID: 705
	public ActorSkin.RigSettings rigSettings = new ActorSkin.RigSettings();

	// Token: 0x040002C2 RID: 706
	[NonSerialized]
	public ModInformation sourceMod;

	// Token: 0x02000075 RID: 117
	[Serializable]
	public class MeshSkin
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00004218 File Offset: 0x00002418
		public MeshSkin(Mesh mesh, Material[] materials, int teamMaterial)
		{
			this.mesh = mesh;
			this.materials = materials;
			this.teamMaterial = teamMaterial;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000423C File Offset: 0x0000243C
		public void Apply(SkinnedMeshRenderer renderer, WTeam team)
		{
			ActorManager.ApplyOverrideMeshSkin(renderer, this, (int)team);
		}

		// Token: 0x040002C3 RID: 707
		public Mesh mesh;

		// Token: 0x040002C4 RID: 708
		public Material[] materials;

		// Token: 0x040002C5 RID: 709
		public int teamMaterial = -1;
	}

	// Token: 0x02000076 RID: 118
	[Serializable]
	public class RigSettings
	{
		// Token: 0x040002C6 RID: 710
		public ActorSkin.RigSettings.RigVersion version;

		// Token: 0x02000077 RID: 119
		public enum RigVersion
		{
			// Token: 0x040002C8 RID: 712
			Unity_5,
			// Token: 0x040002C9 RID: 713
			Unity_2020
		}
	}
}

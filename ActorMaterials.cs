using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class ActorMaterials
{
	// Token: 0x0600033C RID: 828 RVA: 0x0004CC2C File Offset: 0x0004AE2C
	public ActorMaterials(ActorSkin.MeshSkin skin, Material teamMaterial)
	{
		Material[] materials = skin.materials;
		if (skin.teamMaterial >= 0 && skin.teamMaterial <= materials.Length - 1)
		{
			materials[skin.teamMaterial] = teamMaterial;
		}
		this.instancedMaterials = new Material[materials.Length];
		this.defaultEmission = new Color[materials.Length];
		this.hasEmissionProperty = new bool[materials.Length];
		this.hasEmissionMap = new bool[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].EnableKeyword("_EMISSION");
			FixBundleShaders.FixMaterialShader(materials[i]);
			Material material = new Material(materials[i]);
			material.name = "Actor Managed " + material.name;
			this.hasEmissionProperty[i] = material.HasProperty(ActorMaterials.EMISSION_MATERIAL_PROPERTY_ID);
			this.hasEmissionMap[i] = (this.hasEmissionProperty[i] && material.GetTexture(ActorMaterials.EMISSION_MAP_MATERIAL_PROPERTY_ID) != null);
			if (this.hasEmissionProperty[i])
			{
				material.EnableKeyword("_EMISSION");
				this.defaultEmission[i] = material.GetColor(ActorMaterials.EMISSION_MATERIAL_PROPERTY_ID);
			}
			this.instancedMaterials[i] = material;
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0004CD54 File Offset: 0x0004AF54
	public void SetGlowColor(Color emissionMapTint, Color solidTint)
	{
		for (int i = 0; i < this.instancedMaterials.Length; i++)
		{
			if (this.hasEmissionProperty[i])
			{
				Color value = this.hasEmissionMap[i] ? emissionMapTint : solidTint;
				this.instancedMaterials[i].SetColor(ActorMaterials.EMISSION_MATERIAL_PROPERTY_ID, value);
			}
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0004CDA0 File Offset: 0x0004AFA0
	public void ResetGlowColor()
	{
		for (int i = 0; i < this.instancedMaterials.Length; i++)
		{
			if (this.hasEmissionProperty[i])
			{
				this.instancedMaterials[i].SetColor(ActorMaterials.EMISSION_MATERIAL_PROPERTY_ID, this.defaultEmission[i]);
			}
		}
	}

	// Token: 0x040002B6 RID: 694
	public static int EMISSION_MATERIAL_PROPERTY_ID;

	// Token: 0x040002B7 RID: 695
	public static int EMISSION_MAP_MATERIAL_PROPERTY_ID;

	// Token: 0x040002B8 RID: 696
	public Material[] instancedMaterials;

	// Token: 0x040002B9 RID: 697
	public Color[] defaultEmission;

	// Token: 0x040002BA RID: 698
	public bool[] hasEmissionProperty;

	// Token: 0x040002BB RID: 699
	public bool[] hasEmissionMap;
}

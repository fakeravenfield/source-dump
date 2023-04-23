using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000133 RID: 307
public class ChangeEmissionAtNight : MonoBehaviour
{
	// Token: 0x060008BC RID: 2236 RVA: 0x00068DDC File Offset: 0x00066FDC
	public void Start()
	{
		if (!GameManager.GameParameters().nightMode)
		{
			return;
		}
		foreach (MaterialTarget materialTarget in this.targets)
		{
			Material sharedMaterial = materialTarget.GetSharedMaterial();
			if (sharedMaterial == null)
			{
				return;
			}
			if (!ChangeEmissionAtNight.tweakedMaterials.ContainsKey(sharedMaterial))
			{
				Material material = new Material(sharedMaterial);
				material.name = sharedMaterial.name + " night tweak";
				for (int j = 0; j < ChangeEmissionAtNight.PROPERTIES.Length; j++)
				{
					int nameID = ChangeEmissionAtNight.PROPERTIES[j];
					if (material.HasProperty(nameID))
					{
						Color value = this.multiplyEmissionColor * sharedMaterial.GetColor(nameID);
						material.SetColor(nameID, value);
						break;
					}
				}
				ChangeEmissionAtNight.tweakedMaterials.Add(sharedMaterial, material);
			}
			Material material2 = ChangeEmissionAtNight.tweakedMaterials[sharedMaterial];
			materialTarget.SetMaterial(material2);
		}
		Graphic[] array2 = this.uiTargets;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].color *= this.multiplyEmissionColor;
		}
	}

	// Token: 0x04000967 RID: 2407
	private static readonly int[] PROPERTIES = new int[]
	{
		Shader.PropertyToID("_TintColor"),
		Shader.PropertyToID("_EmissionColor")
	};

	// Token: 0x04000968 RID: 2408
	public static Dictionary<Material, Material> tweakedMaterials;

	// Token: 0x04000969 RID: 2409
	public Graphic[] uiTargets;

	// Token: 0x0400096A RID: 2410
	public MaterialTarget[] targets;

	// Token: 0x0400096B RID: 2411
	public Color multiplyEmissionColor = Color.gray;
}

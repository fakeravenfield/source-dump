using System;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class TerrainNormalmapGenerator : MonoBehaviour
{
	// Token: 0x060015FC RID: 5628 RVA: 0x00011640 File Offset: 0x0000F840
	private void Start()
	{
		this.GenerateAndApply();
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x0009E874 File Offset: 0x0009CA74
	public void GenerateAndApply()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainData terrainData = component.terrainData;
		int num = terrainData.heightmapResolution - 1;
		this.normalMap = new Texture2D(num, num, TextureFormat.RGBA32, false, true);
		this.normalMap.wrapMode = TextureWrapMode.Clamp;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		float num2 = component.terrainData.size.x / (float)terrainData.heightmapResolution;
		float num3 = component.terrainData.size.z / (float)terrainData.heightmapResolution;
		Color[] array = new Color[num * num];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				int num4 = Mathf.Max(0, i - 1);
				int num5 = Mathf.Max(0, j - 1);
				int num6 = i + 1;
				int num7 = j + 1;
				Vector3 rhs = new Vector3(2f * num2, (heights[j, num6] - heights[j, num4]) * component.terrainData.size.y, 0f);
				Vector3 normalized = Vector3.Cross(new Vector3(0f, (heights[num7, i] - heights[num5, i]) * component.terrainData.size.y, 2f * num3), rhs).normalized;
				array[i + j * num] = new Color(0.5f + 0.5f * normalized.x, 0.5f + 0.5f * normalized.z, normalized.y);
			}
		}
		this.normalMap.SetPixels(array);
		this.normalMap.Apply();
		component.materialTemplate.SetTexture("_Normalmap", this.normalMap);
	}

	// Token: 0x04001867 RID: 6247
	public Texture2D normalMap;
}

using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class PileTexturer : MonoBehaviour
{
	// Token: 0x060001AD RID: 429 RVA: 0x00044960 File Offset: 0x00042B60
	public void ApplyTexture()
	{
		Terrain[] terrains = UnityEngine.Object.FindObjectsOfType<Terrain>();
		this.ApplyTexture(terrains);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0004497C File Offset: 0x00042B7C
	public void ApplyTexture(Terrain[] terrains)
	{
		Bounds bounds = new Bounds(base.transform.position, base.transform.localScale);
		foreach (Terrain terrain in terrains)
		{
			Bounds bounds2 = terrain.terrainData.bounds;
			bounds2.center += terrain.transform.position;
			if (bounds2.Intersects(bounds))
			{
				this.ApplyTextureToTerrain(terrain);
			}
		}
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000449F8 File Offset: 0x00042BF8
	private void ClearTexture(Terrain terrain)
	{
		TerrainData terrainData = terrain.terrainData;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
		for (int i = 0; i < terrainData.alphamapWidth; i++)
		{
			for (int j = 0; j < terrainData.alphamapHeight; j++)
			{
				for (int k = 0; k < terrainData.alphamapLayers; k++)
				{
					alphamaps[i, j, k] = ((k == 0) ? 1f : 0f);
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphamaps);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00044A7C File Offset: 0x00042C7C
	private void ApplyTextureToTerrain(Terrain terrain)
	{
		TerrainData terrainData = terrain.terrainData;
		Vector3 vector = base.transform.position - base.transform.localScale / 2f - terrain.transform.position;
		Vector3 vector2 = base.transform.position + base.transform.localScale / 2f - terrain.transform.position;
		Vector3 scale = new Vector3(1f / terrainData.size.x, 1f / terrainData.size.y, 1f / terrainData.size.z);
		vector.Scale(scale);
		vector2.Scale(scale);
		vector = Vector3.Max(Vector3.zero, vector);
		vector2 = Vector3.Min(Vector3.one, vector2);
		Vector3 vector3 = vector2 - vector;
		int x = Mathf.RoundToInt(vector.x * (float)terrainData.alphamapWidth);
		int y = Mathf.RoundToInt(vector.z * (float)terrainData.alphamapHeight);
		int num = Mathf.RoundToInt(vector3.x * (float)terrainData.alphamapWidth);
		int num2 = Mathf.RoundToInt(vector3.z * (float)terrainData.alphamapHeight);
		int xBase = Mathf.RoundToInt(vector.x * (float)terrainData.detailWidth);
		int yBase = Mathf.RoundToInt(vector.z * (float)terrainData.detailHeight);
		int num3 = Mathf.RoundToInt(vector3.x * (float)terrainData.detailWidth);
		int num4 = Mathf.RoundToInt(vector3.z * (float)terrainData.detailHeight);
		float[,,] alphamaps = terrainData.GetAlphamaps(x, y, num, num2);
		new float[num2, num];
		Vector2 vector4 = new Vector2(vector3.x / (float)num, vector3.z / (float)num2);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				float num5 = vector.x + (float)i * vector4.x;
				float num6 = vector.z + (float)j * vector4.y;
				for (int k = 0; k < terrainData.alphamapLayers; k++)
				{
					float num7 = alphamaps[j, i, k];
					if (k < this.layers.Length)
					{
						PileTexturer.Layer layer = this.layers[k];
						switch (layer.type)
						{
						case PileTexturer.Layer.Type.Set:
							num7 = layer.outputGain;
							break;
						case PileTexturer.Layer.Type.Slope:
							num7 = layer.curve.Evaluate(layer.inputGain * terrainData.GetSteepness(num5, num6)) * layer.outputGain;
							break;
						case PileTexturer.Layer.Type.Height:
						{
							float num8 = layer.inputGain * terrainData.GetInterpolatedHeight(num5, num6);
							num8 += this.heightOffset;
							num7 = layer.curve.Evaluate(num8) * layer.outputGain;
							break;
						}
						}
						UnityEngine.Random.InitState(layer.noiseSeed);
						float num9 = UnityEngine.Random.Range(-1000f, 1000f);
						float num10 = UnityEngine.Random.Range(-1000f, 1000f);
						num7 *= 1f + layer.outputNoise * 2f * (Mathf.PerlinNoise(num9 + layer.noiseFrequency * num5, num10 + layer.noiseFrequency * num6) - 0.5f);
					}
					num7 = Mathf.Clamp01(num7);
					for (int l = 0; l < k; l++)
					{
						alphamaps[j, i, l] = Mathf.Clamp01(alphamaps[j, i, l] - num7);
					}
					alphamaps[j, i, k] = num7;
				}
				float num11 = 0f;
				for (int m = 0; m < terrainData.alphamapLayers; m++)
				{
					num11 += alphamaps[j, i, m];
				}
				num11 = Mathf.Max(num11, 0.01f);
				for (int n = 0; n < terrainData.alphamapLayers; n++)
				{
					alphamaps[j, i, n] /= num11;
				}
			}
		}
		terrainData.SetAlphamaps(x, y, alphamaps);
		int num12 = terrainData.detailPrototypes.Length;
		for (int num13 = 0; num13 < num12; num13++)
		{
			PileTexturer.DetailLayer detailLayer = this.detailLayers[num13];
			int[,] detailLayer2 = terrainData.GetDetailLayer(xBase, yBase, num3, num4, num13);
			int layerIndex = detailLayer.layerIndex;
			for (int num14 = 0; num14 < num3; num14++)
			{
				for (int num15 = 0; num15 < num4; num15++)
				{
					int num16 = Mathf.Clamp((int)((float)num14 * ((float)num / (float)num3)), 0, num);
					int num17 = Mathf.Clamp((int)((float)num15 * ((float)num / (float)num4)), 0, num2);
					float time = alphamaps[num16, num17, layerIndex];
					detailLayer2[num14, num15] = Mathf.RoundToInt(detailLayer.densityCurve.Evaluate(time) * detailLayer.densityMultiplier);
				}
			}
			terrainData.SetDetailLayer(xBase, yBase, num13, detailLayer2);
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00044F70 File Offset: 0x00043170
	private float Kernel(float[,] kernel, float centerX, float centerY, float deltaX, float deltaY, TerrainData data)
	{
		int num = -kernel.GetLength(0) / 2;
		int num2 = -kernel.GetLength(1) / 2;
		float num3 = 0f;
		for (int i = 0; i < kernel.GetLength(0); i++)
		{
			for (int j = 0; j < kernel.GetLength(1); j++)
			{
				float x = centerX + (float)(i - num) * deltaX;
				float y = centerY + (float)(j - num2) * deltaY;
				num3 += data.GetInterpolatedHeight(x, y) * kernel[i, j];
			}
		}
		return num3;
	}

	// Token: 0x04000135 RID: 309
	public PileTexturer.Layer[] layers;

	// Token: 0x04000136 RID: 310
	public PileTexturer.DetailLayer[] detailLayers;

	// Token: 0x04000137 RID: 311
	[NonSerialized]
	public float heightOffset;

	// Token: 0x0200005D RID: 93
	[Serializable]
	public class Layer
	{
		// Token: 0x04000138 RID: 312
		public PileTexturer.Layer.Type type;

		// Token: 0x04000139 RID: 313
		public float inputGain = 1f;

		// Token: 0x0400013A RID: 314
		public AnimationCurve curve;

		// Token: 0x0400013B RID: 315
		public float outputGain = 1f;

		// Token: 0x0400013C RID: 316
		public float outputNoise;

		// Token: 0x0400013D RID: 317
		public float noiseFrequency = 10f;

		// Token: 0x0400013E RID: 318
		public int noiseSeed;

		// Token: 0x0200005E RID: 94
		public enum Type
		{
			// Token: 0x04000140 RID: 320
			Keep,
			// Token: 0x04000141 RID: 321
			Set,
			// Token: 0x04000142 RID: 322
			Slope,
			// Token: 0x04000143 RID: 323
			Height
		}
	}

	// Token: 0x0200005F RID: 95
	[Serializable]
	public class DetailLayer
	{
		// Token: 0x04000144 RID: 324
		public int layerIndex;

		// Token: 0x04000145 RID: 325
		public AnimationCurve densityCurve;

		// Token: 0x04000146 RID: 326
		public float densityMultiplier = 1f;
	}
}

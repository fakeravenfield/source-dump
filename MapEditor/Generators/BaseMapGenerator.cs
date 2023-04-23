using System;
using System.Collections;
using UnityEngine;

namespace MapEditor.Generators
{
	// Token: 0x02000737 RID: 1847
	public abstract class BaseMapGenerator : MonoBehaviour
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x0001FE10 File Offset: 0x0001E010
		public IEnumerator Generate(Terrain terrain)
		{
			this.progress = 0f;
			this.isDone = false;
			int resolution = terrain.terrainData.heightmapResolution;
			float upscaleAmount = (float)((resolution - 1) / 512);
			UnityEngine.Random.InitState(this.seed);
			this.noiseOffset = new Vector2(UnityEngine.Random.Range(0f, 100f), UnityEngine.Random.Range(0f, 100f));
			float[,] heights = new float[513, 513];
			yield return this.GenerateHeights(heights, 513, upscaleAmount);
			if (resolution != 513)
			{
				float[,] array = new float[resolution, resolution];
				this.ScaleHeights(heights, array, 513, resolution);
				terrain.terrainData.SetHeights(0, 0, array);
			}
			else
			{
				terrain.terrainData.SetHeights(0, 0, heights);
			}
			this.progress = 1f;
			this.isDone = true;
			yield break;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0001FE26 File Offset: 0x0001E026
		protected float SampleNoise(Vector2 position, float frequency)
		{
			return Mathf.PerlinNoise(this.noiseOffset.x + position.x * frequency * 0.1f, this.noiseOffset.y + position.y * frequency * 0.1f);
		}

		// Token: 0x06002E4D RID: 11853
		public abstract IEnumerator GenerateHeights(float[,] heights, int resolution, float upscaleAmount);

		// Token: 0x06002E4E RID: 11854 RVA: 0x00108748 File Offset: 0x00106948
		public void ScaleHeights(float[,] source, float[,] destination, int sourceRes, int destRes)
		{
			float num = (float)sourceRes / (float)destRes;
			for (int i = 0; i < destRes; i++)
			{
				for (int j = 0; j < destRes; j++)
				{
					float num2 = num * (float)i;
					int num3 = (int)num2;
					num2 -= (float)num3;
					int num4 = Mathf.Min(num3 + 1, sourceRes - 1);
					float num5 = num * (float)j;
					int num6 = (int)num5;
					num5 -= (float)num6;
					int num7 = Mathf.Min(num6 + 1, sourceRes - 1);
					float a = source[num3, num6];
					float b = source[num4, num6];
					float b2 = source[num4, num7];
					float a2 = source[num3, num7];
					float a3 = Mathf.Lerp(a, b, num2);
					float b3 = Mathf.Lerp(a2, b2, num2);
					destination[i, j] = Mathf.Lerp(a3, b3, num5);
				}
			}
		}

		// Token: 0x04002A59 RID: 10841
		public const int GENERATION_RESOLUTION = 513;

		// Token: 0x04002A5A RID: 10842
		private const float BASE_NOISE_FREQUENCY = 0.1f;

		// Token: 0x04002A5B RID: 10843
		public TerrainLayer[] layers;

		// Token: 0x04002A5C RID: 10844
		[NonSerialized]
		public float progress;

		// Token: 0x04002A5D RID: 10845
		[NonSerialized]
		public bool isDone;

		// Token: 0x04002A5E RID: 10846
		[NonSerialized]
		public int seed;

		// Token: 0x04002A5F RID: 10847
		private Vector2 noiseOffset;
	}
}

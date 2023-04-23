using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020005B9 RID: 1465
	public class WeldTerrains
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x000F309C File Offset: 0x000F129C
		public static void WeldHeights(float[,] heights, Terrain prevX, Terrain nextZ, Terrain nextX, Terrain prevZ, int margin)
		{
			int length = heights.GetLength(0);
			if (margin == 0)
			{
				return;
			}
			float[,] array = new float[length, length];
			if (prevX != null && prevX.terrainData.heightmapResolution == length)
			{
				float[,] heights2 = prevX.terrainData.GetHeights(length - 1, 0, 1, length);
				for (int i = 0; i < length; i++)
				{
					float num = heights2[i, 0] - (heights[i, 0] + array[i, 0]);
					array[i, 0] = num;
					for (int j = 1; j < margin; j++)
					{
						float num2 = 1f - 1f * (float)j / (float)margin;
						num2 = 3f * num2 * num2 - 2f * num2 * num2 * num2;
						array[i, j] += num * num2;
					}
				}
			}
			if (nextX != null && nextX.terrainData.heightmapResolution == length)
			{
				float[,] heights3 = nextX.terrainData.GetHeights(0, 0, 1, length);
				for (int k = 0; k < length; k++)
				{
					float num3 = heights3[k, 0] - heights[k, length - 1] - array[k, length - 1];
					for (int l = length - margin; l < length; l++)
					{
						float num4 = 1f - 1f * (float)(length - l - 1) / (float)margin;
						num4 = 3f * num4 * num4 - 2f * num4 * num4 * num4;
						array[k, l] += num3 * num4;
					}
				}
			}
			if (prevZ != null && prevZ.terrainData.heightmapResolution == length)
			{
				float[,] heights4 = prevZ.terrainData.GetHeights(0, length - 1, length, 1);
				for (int m = 0; m < length; m++)
				{
					float num5 = heights4[0, m] - heights[0, m] - array[0, m];
					float num6 = Mathf.Min(Mathf.Clamp01(1f * (float)m / (float)margin), Mathf.Clamp01(1f - 1f * (float)(m - (length - 1 - margin)) / (float)margin));
					float p = 2E+09f;
					if (num6 > 0.0001f)
					{
						p = 1f / num6;
					}
					for (int n = 0; n < margin; n++)
					{
						float num7 = 1f - 1f * (float)n / (float)margin;
						if (num6 < 0.999f)
						{
							num7 = Mathf.Pow(num7, p);
						}
						num7 = 3f * num7 * num7 - 2f * num7 * num7 * num7;
						array[n, m] += num5 * num7;
					}
				}
			}
			if (nextZ != null && nextZ.terrainData.heightmapResolution == length)
			{
				float[,] heights5 = nextZ.terrainData.GetHeights(0, 0, length, 1);
				for (int num8 = 0; num8 < length; num8++)
				{
					float num9 = heights5[0, num8] - heights[length - 1, num8] - array[length - 1, num8];
					float num10 = Mathf.Min(Mathf.Clamp01(1f * (float)num8 / (float)margin), Mathf.Clamp01(1f - 1f * (float)(num8 - (length - 1 - margin)) / (float)margin));
					float p2 = 2E+09f;
					if (num10 > 0.0001f)
					{
						p2 = 1f / num10;
					}
					for (int num11 = length - margin; num11 < length; num11++)
					{
						float num12 = 1f - 1f * (float)(length - num11 - 1) / (float)margin;
						if (num10 < 0.999f)
						{
							num12 = Mathf.Pow(num12, p2);
						}
						num12 = 3f * num12 * num12 - 2f * num12 * num12 * num12;
						array[num11, num8] += num9 * num12;
					}
				}
			}
			for (int num13 = 0; num13 < length; num13++)
			{
				for (int num14 = 0; num14 < length; num14++)
				{
					heights[num13, num14] += array[num13, num14];
				}
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000F34C0 File Offset: 0x000F16C0
		public static void WeldSplats(float[,,] splats, Terrain prevX, Terrain nextZ, Terrain nextX, Terrain prevZ, int margin)
		{
			if (margin == 0)
			{
				return;
			}
			int length = splats.GetLength(0);
			int length2 = splats.GetLength(2);
			float[] array = new float[length2];
			if (prevX != null && prevX.terrainData.alphamapResolution == length && prevX.terrainData.alphamapLayers == length2)
			{
				float[,,] alphamaps = prevX.terrainData.GetAlphamaps(length - 1, 0, 1, length);
				for (int i = 0; i < length; i++)
				{
					for (int j = 0; j < length2; j++)
					{
						array[j] = alphamaps[i, 0, j];
					}
					float num = Mathf.Min(Mathf.Clamp01(1f * (float)i / (float)margin), Mathf.Clamp01(1f - 1f * (float)(i - (length - 1 - margin)) / (float)margin));
					float p = 2E+09f;
					if (num > 0.0001f)
					{
						p = 1f / num;
					}
					for (int k = 0; k < margin; k++)
					{
						float num2 = 1f - 1f * (float)k / (float)margin;
						if (num < 0.999f)
						{
							num2 = Mathf.Pow(num2, p);
						}
						num2 = 3f * num2 * num2 - 2f * num2 * num2 * num2;
						for (int l = 0; l < length2; l++)
						{
							splats[i, k, l] = array[l] * num2 + splats[i, k, l] * (1f - num2);
						}
					}
				}
			}
			if (nextX != null && nextX.terrainData.alphamapResolution == length && nextX.terrainData.alphamapLayers == length2)
			{
				float[,,] alphamaps2 = nextX.terrainData.GetAlphamaps(0, 0, 1, length);
				for (int m = 0; m < length; m++)
				{
					for (int n = 0; n < length2; n++)
					{
						array[n] = alphamaps2[m, 0, n];
					}
					float num3 = Mathf.Min(Mathf.Clamp01(1f * (float)m / (float)margin), Mathf.Clamp01(1f - 1f * (float)(m - (length - 1 - margin)) / (float)margin));
					float p2 = 2E+09f;
					if (num3 > 0.0001f)
					{
						p2 = 1f / num3;
					}
					for (int num4 = length - margin; num4 < length; num4++)
					{
						float num5 = 1f - 1f * (float)(length - num4 - 1) / (float)margin;
						if (num3 < 0.999f)
						{
							num5 = Mathf.Pow(num5, p2);
						}
						num5 = 3f * num5 * num5 - 2f * num5 * num5 * num5;
						for (int num6 = 0; num6 < length2; num6++)
						{
							splats[m, num4, num6] = array[num6] * num5 + splats[m, num4, num6] * (1f - num5);
						}
					}
				}
			}
			if (prevZ != null && prevZ.terrainData.alphamapResolution == length && prevZ.terrainData.alphamapLayers == length2)
			{
				float[,,] alphamaps3 = prevZ.terrainData.GetAlphamaps(0, length - 1, length, 1);
				for (int num7 = 0; num7 < length; num7++)
				{
					for (int num8 = 0; num8 < length2; num8++)
					{
						array[num8] = alphamaps3[0, num7, num8];
					}
					float num9 = Mathf.Min(Mathf.Clamp01(1f * (float)num7 / (float)margin), Mathf.Clamp01(1f - 1f * (float)(num7 - (length - 1 - margin)) / (float)margin));
					float p3 = 2E+09f;
					if (num9 > 0.0001f)
					{
						p3 = 1f / num9;
					}
					for (int num10 = 0; num10 < margin; num10++)
					{
						float num11 = 1f - 1f * (float)num10 / (float)margin;
						if (num9 < 0.999f)
						{
							num11 = Mathf.Pow(num11, p3);
						}
						num11 = 3f * num11 * num11 - 2f * num11 * num11 * num11;
						for (int num12 = 0; num12 < length2; num12++)
						{
							splats[num10, num7, num12] = array[num12] * num11 + splats[num10, num7, num12] * (1f - num11);
						}
					}
				}
			}
			if (nextZ != null && nextZ.terrainData.alphamapResolution == length && nextZ.terrainData.alphamapLayers == length2)
			{
				float[,,] alphamaps4 = nextZ.terrainData.GetAlphamaps(0, 0, length, 1);
				for (int num13 = 0; num13 < length; num13++)
				{
					for (int num14 = 0; num14 < length2; num14++)
					{
						array[num14] = alphamaps4[0, num13, num14];
					}
					float num15 = Mathf.Min(Mathf.Clamp01(1f * (float)num13 / (float)margin), Mathf.Clamp01(1f - 1f * (float)(num13 - (length - 1 - margin)) / (float)margin));
					float p4 = 2E+09f;
					if (num15 > 0.0001f)
					{
						p4 = 1f / num15;
					}
					for (int num16 = length - margin; num16 < length; num16++)
					{
						float num17 = 1f - 1f * (float)(length - num16 - 1) / (float)margin;
						if (num15 < 0.999f)
						{
							num17 = Mathf.Pow(num17, p4);
						}
						num17 = 3f * num17 * num17 - 2f * num17 * num17 * num17;
						for (int num18 = 0; num18 < length2; num18++)
						{
							splats[num16, num13, num18] = array[num18] * num17 + splats[num16, num13, num18] * (1f - num17);
						}
					}
				}
			}
		}
	}
}

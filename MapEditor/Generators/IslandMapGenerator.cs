using System;
using System.Collections;
using UnityEngine;

namespace MapEditor.Generators
{
	// Token: 0x0200073B RID: 1851
	public class IslandMapGenerator : BaseMapGenerator
	{
		// Token: 0x06002E5E RID: 11870 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		public override IEnumerator GenerateHeights(float[,] heights, int resolution, float upscaleAmount)
		{
			Vector2 center = new Vector2((float)(resolution / 2), (float)(resolution / 2));
			float islandCoastDistance = (float)resolution / 2.4f;
			float islandOceanDistance = (float)resolution / 2f;
			float inlandDistance = islandCoastDistance;
			float inlandMaxDistance = inlandDistance / 2f;
			int num6;
			for (int i = 0; i < resolution; i = num6 + 1)
			{
				for (int j = 0; j < resolution; j++)
				{
					Vector2 vector = new Vector2((float)i, (float)j);
					float num = Vector2.Distance(vector, center);
					float num2 = upscaleAmount * 40f * base.SampleNoise(vector, 0.3f);
					float num3 = Mathf.InverseLerp(islandOceanDistance, islandCoastDistance, num + num2);
					float num4 = Mathf.InverseLerp(inlandDistance, inlandMaxDistance, num) * num3 * base.SampleNoise(vector, 0.07f);
					num4 = num4 * num4 * base.SampleNoise(vector, 0.35f);
					float num5 = Mathf.SmoothStep(0f, 0.08f, num3) + num4 * 0.2f * upscaleAmount;
					heights[i, j] = num5;
				}
				if (i % 16 == 0)
				{
					this.progress = (float)i / (float)resolution;
					yield return 0;
				}
				num6 = i;
			}
			yield break;
		}

		// Token: 0x04002A6D RID: 10861
		private const float COAST_HEIGHT = 0.08f;

		// Token: 0x04002A6E RID: 10862
		private const float COAST_NOISE_AMPLITUDE = 40f;

		// Token: 0x04002A6F RID: 10863
		private const float COAST_NOISE_FREQUENCY = 0.3f;

		// Token: 0x04002A70 RID: 10864
		private const float INLAND_NOISE_FREQUENCY = 0.07f;

		// Token: 0x04002A71 RID: 10865
		private const float INLAND_NOISE_DETAIL_FREQUENCY = 0.35f;

		// Token: 0x04002A72 RID: 10866
		private const float INLAND_NOISE_AMPLITUDE = 0.2f;
	}
}

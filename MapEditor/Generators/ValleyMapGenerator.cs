using System;
using System.Collections;
using UnityEngine;

namespace MapEditor.Generators
{
	// Token: 0x0200073D RID: 1853
	public class ValleyMapGenerator : BaseMapGenerator
	{
		// Token: 0x06002E66 RID: 11878 RVA: 0x0001FEEF File Offset: 0x0001E0EF
		public override IEnumerator GenerateHeights(float[,] heights, int resolution, float upscaleAmount)
		{
			int num8;
			for (int i = 0; i < resolution; i = num8 + 1)
			{
				for (int j = 0; j < resolution; j++)
				{
					Vector2 position = new Vector2((float)i, (float)j);
					float num = base.SampleNoise(position, 0.13f);
					num *= num;
					float num2 = upscaleAmount * 0.25f * Mathf.Clamp01(this.plateauCurve.Evaluate(num) - 0.05f * base.SampleNoise(position, 0.29f * upscaleAmount));
					float f = base.SampleNoise(position, 0.29f * upscaleAmount) * base.SampleNoise(position, 0.35f * upscaleAmount) * 12f;
					float num3 = Mathf.Clamp01(0.2f - num2 * 20f);
					float num4 = Mathf.Lerp(0.01f, 0.03f, base.SampleNoise(position, 0.17f * upscaleAmount));
					float num5 = (1f - Mathf.Abs(Mathf.Sin(f))) * num3 * num4;
					float num6 = Mathf.SmoothStep(0f, 1f, base.SampleNoise(position, 0.07f * upscaleAmount)) * 0.05f;
					float num7 = 0.05f + num2 + num5 + num6;
					heights[i, j] = num7;
				}
				if (i % 16 == 0)
				{
					this.progress = (float)i / (float)resolution;
					yield return 0;
				}
				num8 = i;
			}
			yield break;
		}

		// Token: 0x04002A7F RID: 10879
		private const float BASE_HEIGHT = 0.05f;

		// Token: 0x04002A80 RID: 10880
		private const float PILLAR_NOISE_FREQUENCY = 0.13f;

		// Token: 0x04002A81 RID: 10881
		private const float PILLAR_NOISE_DETAIL_FREQUENCY = 0.29f;

		// Token: 0x04002A82 RID: 10882
		private const float PILLAR_NOISE_AMPLITUDE = 0.25f;

		// Token: 0x04002A83 RID: 10883
		private const float DUNES_NOISE_FREQUENCY = 0.29f;

		// Token: 0x04002A84 RID: 10884
		private const float DUNES_NOISE_DETAIL_FREQUENCY = 0.35f;

		// Token: 0x04002A85 RID: 10885
		private const float DUNES_PHASE_FREQUENCY = 12f;

		// Token: 0x04002A86 RID: 10886
		private const float DUNES_HEIGHT_NOISE_FREQUENCY = 0.17f;

		// Token: 0x04002A87 RID: 10887
		private const float DUNES_AMPLITUDE_MIN = 0.01f;

		// Token: 0x04002A88 RID: 10888
		private const float DUNES_AMPLITUDE_MAX = 0.03f;

		// Token: 0x04002A89 RID: 10889
		private const float HEIGHT_NOISE_FREUQUENCY = 0.07f;

		// Token: 0x04002A8A RID: 10890
		private const float HEIGHT_NOISE_AMPLITUDE = 0.05f;

		// Token: 0x04002A8B RID: 10891
		public AnimationCurve plateauCurve;
	}
}

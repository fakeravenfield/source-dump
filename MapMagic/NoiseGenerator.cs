using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004B9 RID: 1209
	[GeneratorMenu(menu = "Map", name = "Noise (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class NoiseGenerator : Generator
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x0001664C File Offset: 0x0001484C
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0001665C File Offset: 0x0001485C
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000C74F4 File Offset: 0x000C56F4
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (matrix != null)
			{
				matrix = matrix.Copy(null);
			}
			if (matrix == null)
			{
				matrix = chunk.defaultMatrix;
			}
			Matrix mask = (Matrix)this.maskIn.GetObject(chunk);
			if (!this.enabled)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			if (chunk.stop)
			{
				return;
			}
			NoiseGenerator.Noise(matrix, this.size, this.intensity, this.bias, this.detail, this.offset, this.seed, mask);
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000C759C File Offset: 0x000C579C
		public static void Noise(Matrix matrix, float size, float intensity = 1f, float bias = 0f, float detail = 0.5f, Vector2 offset = default(Vector2), int seed = 12345, Matrix mask = null)
		{
			int num = (int)(4096f / (float)matrix.rect.size.x);
			int num2 = ((int)offset.x + MapMagic.instance.seed + seed * 7) % 77777;
			int num3 = ((int)offset.y + MapMagic.instance.seed + seed * 3) % 73333;
			int num4 = 1;
			float num5 = size;
			for (int i = 0; i < 100; i++)
			{
				num5 /= 2f;
				if (num5 < 1f)
				{
					break;
				}
				num4++;
			}
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int j = min.x; j < max.x; j++)
			{
				for (int k = min.z; k < max.z; k++)
				{
					float num6 = 0.5f;
					float num7 = size * 10f;
					float num8 = 1f;
					for (int l = 0; l < num4; l++)
					{
						float num9 = Mathf.PerlinNoise((float)((j + num2 + 1000 * (l + 1)) * num) / (num7 + 1f), (float)((k + num3 + 100 * l) * num) / (num7 + 1f));
						num9 = (num9 - 0.5f) * num8 + 0.5f;
						if (num9 > 0.5f)
						{
							num6 = 1f - 2f * (1f - num6) * (1f - num9);
						}
						else
						{
							num6 = 2f * num9 * num6;
						}
						num7 *= 0.5f;
						num8 *= detail;
					}
					num6 *= intensity;
					num6 -= 0f * (1f - bias) + (intensity - 1f) * bias;
					if (num6 < 0f)
					{
						num6 = 0f;
					}
					if (num6 > 1f)
					{
						num6 = 1f;
					}
					if (mask == null)
					{
						int num10 = j;
						int num11 = k;
						matrix[num10, num11] += num6;
					}
					else
					{
						int num11 = j;
						int num10 = k;
						matrix[num11, num10] += num6 * mask[j, k];
					}
				}
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000C77E8 File Offset: 0x000C59E8
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.6f;
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.bias, "Bias", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.size, "Size", default(Rect), 1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.detail, "Detail", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.offset, "Offset", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001EFA RID: 7930
		public int seed = 12345;

		// Token: 0x04001EFB RID: 7931
		public float intensity = 1f;

		// Token: 0x04001EFC RID: 7932
		public float bias;

		// Token: 0x04001EFD RID: 7933
		public float size = 200f;

		// Token: 0x04001EFE RID: 7934
		public float detail = 0.5f;

		// Token: 0x04001EFF RID: 7935
		public Vector2 offset = new Vector2(0f, 0f);

		// Token: 0x04001F00 RID: 7936
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F01 RID: 7937
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F02 RID: 7938
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);
	}
}

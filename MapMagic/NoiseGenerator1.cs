using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004D8 RID: 1240
	[GeneratorMenu(menu = "Map", name = "Noise", disengageable = true)]
	[Serializable]
	public class NoiseGenerator1 : Generator
	{
		// Token: 0x06001F12 RID: 7954 RVA: 0x00016B56 File Offset: 0x00014D56
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00016B66 File Offset: 0x00014D66
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000CAC98 File Offset: 0x000C8E98
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
			Matrix matrix2 = (Matrix)this.maskIn.GetObject(chunk);
			if (!this.enabled)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			if (chunk.stop)
			{
				return;
			}
			Noise noise = new Noise(this.size, matrix.rect.size.x, MapMagic.instance.seed + this.seed * 7, MapMagic.instance.seed + this.seed * 3);
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = noise.Fractal(i + (int)this.offset.x, j + (int)this.offset.y, this.detail);
					num *= this.intensity;
					num -= 0f * (1f - this.bias) + (this.intensity - 1f) * this.bias;
					if (num < 0f)
					{
						num = 0f;
					}
					if (num > 1f)
					{
						num = 1f;
					}
					if (matrix2 == null)
					{
						Matrix matrix3 = matrix;
						int num2 = i;
						int num3 = j;
						matrix3[num2, num3] += num;
					}
					else
					{
						Matrix matrix3 = matrix;
						int num3 = i;
						int num2 = j;
						matrix3[num3, num2] += num * matrix2[i, j];
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000CAE8C File Offset: 0x000C908C
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

		// Token: 0x04001F89 RID: 8073
		public int seed = 12345;

		// Token: 0x04001F8A RID: 8074
		public float intensity = 1f;

		// Token: 0x04001F8B RID: 8075
		public float bias;

		// Token: 0x04001F8C RID: 8076
		public float size = 200f;

		// Token: 0x04001F8D RID: 8077
		public float detail = 0.5f;

		// Token: 0x04001F8E RID: 8078
		public Vector2 offset = new Vector2(0f, 0f);

		// Token: 0x04001F8F RID: 8079
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F90 RID: 8080
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F91 RID: 8081
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);
	}
}

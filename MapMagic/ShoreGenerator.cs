using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000509 RID: 1289
	[GeneratorMenu(menu = "Map", name = "Shore", disengageable = true)]
	[Serializable]
	public class ShoreGenerator : Generator
	{
		// Token: 0x06002048 RID: 8264 RVA: 0x000173B2 File Offset: 0x000155B2
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.heightIn;
			yield return this.maskIn;
			yield return this.ridgeNoiseIn;
			yield break;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000173C2 File Offset: 0x000155C2
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.heightOut;
			yield return this.sandOut;
			yield break;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000D0B6C File Offset: 0x000CED6C
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.heightIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null)
			{
				this.heightOut.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = new Matrix(matrix.rect, null);
			Matrix matrix3 = (Matrix)this.ridgeNoiseIn.GetObject(chunk);
			Matrix matrix4 = new Matrix(matrix.rect, null);
			float num = this.beachLevel / (float)MapMagic.instance.terrainHeight;
			float num2 = (this.beachLevel + this.beachSize) / (float)MapMagic.instance.terrainHeight;
			float num3 = this.ridgeMinGlobal / (float)MapMagic.instance.terrainHeight;
			float num4 = this.ridgeMaxGlobal / (float)MapMagic.instance.terrainHeight;
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num5 = matrix[i, j];
					float num6 = num5;
					if (num5 > num && num5 < num2)
					{
						num6 = num;
					}
					float value = 0f;
					if (num5 <= num2)
					{
						value = 1f;
					}
					float num7 = 0f;
					if (matrix3 != null)
					{
						num7 = matrix3[i, j];
					}
					float num8 = num3 * (1f - num7) + num4 * num7;
					if (num5 >= num2 && num5 <= num2 + num8)
					{
						float num9 = (num5 - num2) / num8;
						num9 = Mathf.Sqrt(num9);
						num9 = 3f * num9 * num9 - 2f * num9 * num9 * num9;
						num6 = num * (1f - num9) + num5 * num9;
						value = 1f - num9;
					}
					num6 = num6 * this.intensity + num5 * (1f - this.intensity);
					matrix2[i, j] = num6;
					matrix4[i, j] = value;
				}
			}
			Matrix matrix5 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix5 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix5);
			}
			if (chunk.stop)
			{
				return;
			}
			this.heightOut.SetObject(chunk, matrix2);
			this.sandOut.SetObject(chunk, matrix4);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000D0DC4 File Offset: 0x000CEFC4
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.heightIn.DrawIcon(this.layout, "Height", false);
			this.heightOut.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.sandOut.DrawIcon(this.layout, "Sand");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.ridgeNoiseIn.DrawIcon(this.layout, "Ridge Noise", false);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.beachLevel, "Water Level", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.beachSize, "Beach Size", default(Rect), 0.0001f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.ridgeMinGlobal, "Ridge Step Min", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.ridgeMaxGlobal, "Ridge Step Max", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x0400209A RID: 8346
		public Generator.Input heightIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400209B RID: 8347
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400209C RID: 8348
		public Generator.Input ridgeNoiseIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400209D RID: 8349
		public Generator.Output heightOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400209E RID: 8350
		public Generator.Output sandOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400209F RID: 8351
		public float intensity = 1f;

		// Token: 0x040020A0 RID: 8352
		public float beachLevel = 20f;

		// Token: 0x040020A1 RID: 8353
		public float beachSize = 10f;

		// Token: 0x040020A2 RID: 8354
		public float ridgeMinGlobal = 2f;

		// Token: 0x040020A3 RID: 8355
		public float ridgeMaxGlobal = 10f;
	}
}

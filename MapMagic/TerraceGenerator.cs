using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000500 RID: 1280
	[GeneratorMenu(menu = "Map", name = "Terrace", disengageable = true)]
	[Serializable]
	public class TerraceGenerator : Generator
	{
		// Token: 0x06002009 RID: 8201 RVA: 0x00017256 File Offset: 0x00015456
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00017266 File Offset: 0x00015466
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000CF2D8 File Offset: 0x000CD4D8
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || this.num <= 1 || matrix == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = matrix.Copy(null);
			float[] array = new float[this.num];
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + 12345, 100);
			float num = 1f / (float)(this.num - 1);
			for (int i = 1; i < this.num; i++)
			{
				array[i] = array[i - 1] + num;
			}
			for (int j = 0; j < 10; j++)
			{
				for (int k = 1; k < this.num - 1; k++)
				{
					float num2 = instanceRandom.Random(array[k - 1], array[k + 1]);
					array[k] = array[k] * this.uniformity + num2 * (1f - this.uniformity);
				}
			}
			if (chunk.stop)
			{
				return;
			}
			for (int l = 0; l < matrix2.count; l++)
			{
				float num3 = matrix2.array[l];
				if (num3 <= 0.999f)
				{
					int num4 = 0;
					int num5 = 0;
					while (num5 < this.num - 1 && array[num4 + 1] <= num3 && num4 + 1 != this.num)
					{
						num4++;
						num5++;
					}
					float num6 = array[num4 + 1] - array[num4];
					float num7 = (num3 - array[num4]) / num6;
					float num8 = 3f * num7 * num7 - 2f * num7 * num7 * num7;
					num8 = (num8 - 0.5f) * 2f;
					bool flag = num8 < 0f;
					num8 = Mathf.Abs(num8);
					num8 = Mathf.Pow(num8, 1f - this.steepness);
					if (flag)
					{
						num8 = -num8;
					}
					num8 = num8 / 2f + 0.5f;
					matrix2.array[l] = (array[num4] * (1f - num8) + array[num4 + 1] * num8) * this.intensity + matrix2.array[l] * (1f - this.intensity);
				}
			}
			Matrix matrix3 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix3 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix3);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000CF544 File Offset: 0x000CD744
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.num, "Treads Num", default(Rect), 2f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.uniformity, "Uniformity", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.steepness, "Steepness", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002065 RID: 8293
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002066 RID: 8294
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002067 RID: 8295
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002068 RID: 8296
		public int seed = 12345;

		// Token: 0x04002069 RID: 8297
		public int num = 10;

		// Token: 0x0400206A RID: 8298
		public float uniformity = 0.5f;

		// Token: 0x0400206B RID: 8299
		public float steepness = 0.5f;

		// Token: 0x0400206C RID: 8300
		public float intensity = 1f;
	}
}

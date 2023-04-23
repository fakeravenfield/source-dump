using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004F5 RID: 1269
	[GeneratorMenu(menu = "Map", name = "Blur", disengageable = true)]
	[Serializable]
	public class BlurGenerator : Generator
	{
		// Token: 0x06001FC6 RID: 8134 RVA: 0x00017071 File Offset: 0x00015271
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00017081 File Offset: 0x00015281
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000CE2E0 File Offset: 0x000CC4E0
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = matrix.Copy(null);
			if (this.loss != 1)
			{
				for (int i = 0; i < this.iterations; i++)
				{
					matrix2.Blur(null, 0.666f, false, false, true, true, null);
				}
			}
			for (int j = this.loss; j > 1; j /= 2)
			{
				matrix2.LossBlur(j, true, true, null);
			}
			for (int k = 0; k < this.iterations; k++)
			{
				matrix2.Blur(null, 1f, false, false, true, true, null);
			}
			if (this.intensity < 0.9999f)
			{
				Matrix.Blend(matrix, matrix2, this.intensity);
			}
			Matrix matrix3 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix3 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix3);
			}
			if (this.safeBorders != 0)
			{
				Matrix.SafeBorders(matrix, matrix2, this.safeBorders);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000CE3FC File Offset: 0x000CC5FC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.iterations, "Iterations", default(Rect), 1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.loss, "Loss", default(Rect), 1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002037 RID: 8247
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002038 RID: 8248
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002039 RID: 8249
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400203A RID: 8250
		public int iterations = 1;

		// Token: 0x0400203B RID: 8251
		public float intensity = 1f;

		// Token: 0x0400203C RID: 8252
		public int loss = 1;

		// Token: 0x0400203D RID: 8253
		public int safeBorders = 5;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic.Operators
{
	// Token: 0x020005BA RID: 1466
	[GeneratorMenu(menu = "Operators", name = "Height Threshold", disengageable = true)]
	[Serializable]
	public class MMOpHeightThreshold : Generator
	{
		// Token: 0x060025FA RID: 9722 RVA: 0x0001A3C0 File Offset: 0x000185C0
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0001A3D0 File Offset: 0x000185D0
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000F3A6C File Offset: 0x000F1C6C
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (matrix == null || chunk.stop)
			{
				return;
			}
			if (!this.enabled)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = new Matrix(matrix.rect, null);
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = this.above ? (matrix[i, j] - this.threshold) : (this.threshold - matrix[i, j]);
					if (this.smear <= 0.001f)
					{
						matrix2[i, j] = ((num > 0f) ? 1f : 0f);
					}
					else
					{
						float num2 = 0.5f + num / this.smear;
						if (num2 > 1f)
						{
							num2 = 1f;
						}
						else if (num2 < 0f)
						{
							num2 = 0f;
						}
						matrix2[i, j] = num2;
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000F3BC4 File Offset: 0x000F1DC4
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, null, false);
			this.output.DrawIcon(this.layout, null);
			this.layout.Field<float>(ref this.threshold, "Threshold", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.smear, "Smear", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.CheckButton(ref this.above, "Above", default(Rect), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x0400248D RID: 9357
		public Generator.Input input = new Generator.Input("Input", Generator.InoutType.Map, false, true);

		// Token: 0x0400248E RID: 9358
		public Generator.Output output = new Generator.Output("Output", Generator.InoutType.Map);

		// Token: 0x0400248F RID: 9359
		public float threshold = 0.1f;

		// Token: 0x04002490 RID: 9360
		public float smear;

		// Token: 0x04002491 RID: 9361
		public bool above;
	}
}

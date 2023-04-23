using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004E9 RID: 1257
	[GeneratorMenu(menu = "Map", name = "Invert", disengageable = true)]
	[Serializable]
	public class InvertGenerator : Generator
	{
		// Token: 0x06001F78 RID: 8056 RVA: 0x00016E27 File Offset: 0x00015027
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00016E37 File Offset: 0x00015037
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000CD3E4 File Offset: 0x000CB5E4
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
			Matrix matrix2 = new Matrix(matrix.rect, null);
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = this.level - matrix[i, j];
					matrix2[i, j] = ((num > 0f) ? num : 0f);
				}
			}
			if (chunk.stop)
			{
				return;
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

		// Token: 0x06001F7B RID: 8059 RVA: 0x000CD4EC File Offset: 0x000CB6EC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Field<float>(ref this.level, "Level", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001FE8 RID: 8168
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FE9 RID: 8169
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001FEA RID: 8170
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FEB RID: 8171
		public float level = 1f;
	}
}

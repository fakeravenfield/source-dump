using System;
using System.Collections.Generic;

namespace MapMagic.Operators
{
	// Token: 0x020005BD RID: 1469
	[GeneratorMenu(menu = "Operators", name = "Negate", disengageable = true)]
	[Serializable]
	public class MMOpNegate : Generator
	{
		// Token: 0x0600260F RID: 9743 RVA: 0x0001A46B File Offset: 0x0001866B
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0001A47B File Offset: 0x0001867B
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000F3E60 File Offset: 0x000F2060
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
					matrix2[i, j] = -matrix[i, j];
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000F3F28 File Offset: 0x000F2128
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, null, false);
			this.output.DrawIcon(this.layout, null);
		}

		// Token: 0x0400249A RID: 9370
		public Generator.Input input = new Generator.Input("Input", Generator.InoutType.Map, false, true);

		// Token: 0x0400249B RID: 9371
		public Generator.Output output = new Generator.Output("Output", Generator.InoutType.Map);
	}
}

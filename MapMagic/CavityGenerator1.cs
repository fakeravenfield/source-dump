using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004F8 RID: 1272
	[GeneratorMenu(menu = "Map", name = "Cavity", disengageable = true)]
	[Serializable]
	public class CavityGenerator1 : Generator
	{
		// Token: 0x06001FDB RID: 8155 RVA: 0x000170E5 File Offset: 0x000152E5
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000170F5 File Offset: 0x000152F5
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000CE840 File Offset: 0x000CCA40
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
			Func<float, float, float, float> blurFn = delegate(float prev, float curr, float next)
			{
				float num = curr - (next + prev) / 2f;
				return num * num * (float)((num > 0f) ? 1 : -1) * this.intensity * 100000f;
			};
			matrix2.Blur(blurFn, 1f, true, false, true, true, matrix);
			if (chunk.stop)
			{
				return;
			}
			matrix2.RemoveBorders();
			if (chunk.stop)
			{
				return;
			}
			if (this.type == CavityGenerator1.CavityType.Concave)
			{
				matrix2.Invert();
			}
			if (chunk.stop)
			{
				return;
			}
			if (!this.normalize)
			{
				matrix2.Clamp01();
			}
			if (chunk.stop)
			{
				return;
			}
			matrix2.Spread(this.spread, 4, null);
			if (chunk.stop)
			{
				return;
			}
			matrix2.Clamp01();
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000CE920 File Offset: 0x000CCB20
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<CavityGenerator1.CavityType>(ref this.type, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.spread, "Spread", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Par(3, default(Layout.Val), default(Layout.Val));
			this.layout.Toggle(ref this.normalize, "Normalize", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
			this.layout.Par(15, default(Layout.Val), default(Layout.Val));
			this.layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val));
			this.layout.Label("Convex + Concave", this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.LowerLeft, false, null);
		}

		// Token: 0x04002046 RID: 8262
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002047 RID: 8263
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002048 RID: 8264
		public CavityGenerator1.CavityType type;

		// Token: 0x04002049 RID: 8265
		public float intensity = 1f;

		// Token: 0x0400204A RID: 8266
		public float spread = 0.5f;

		// Token: 0x0400204B RID: 8267
		public bool normalize = true;

		// Token: 0x020004F9 RID: 1273
		public enum CavityType
		{
			// Token: 0x0400204D RID: 8269
			Convex,
			// Token: 0x0400204E RID: 8270
			Concave
		}
	}
}

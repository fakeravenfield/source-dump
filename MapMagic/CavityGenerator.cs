using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004BE RID: 1214
	[GeneratorMenu(menu = "Map", name = "Cavity (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class CavityGenerator : Generator
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x00016724 File Offset: 0x00014924
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00016734 File Offset: 0x00014934
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.convexOut;
			yield return this.concaveOut;
			yield break;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000C8158 File Offset: 0x000C6358
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (chunk.stop || !this.enabled || matrix == null)
			{
				return;
			}
			Matrix matrix2 = new Matrix(matrix.rect, null);
			Matrix matrix3 = new Matrix(matrix.rect, null);
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
			matrix2.Spread(this.spread, 4, matrix3);
			if (chunk.stop)
			{
				return;
			}
			for (int i = 0; i < matrix2.count; i++)
			{
				matrix3.array[i] = 0f;
				if (matrix2.array[i] < 0f)
				{
					matrix3.array[i] = -matrix2.array[i];
					matrix2.array[i] = 0f;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.convexOut.SetObject(chunk, matrix2);
			this.concaveOut.SetObject(chunk, matrix3);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000C826C File Offset: 0x000C646C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.convexOut.DrawIcon(this.layout, "Convex");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.concaveOut.DrawIcon(this.layout, "Concave");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.spread, "Spread", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F18 RID: 7960
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F19 RID: 7961
		public Generator.Output convexOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F1A RID: 7962
		public Generator.Output concaveOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F1B RID: 7963
		public float intensity = 1f;

		// Token: 0x04001F1C RID: 7964
		public float spread = 0.5f;
	}
}

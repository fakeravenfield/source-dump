using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004C1 RID: 1217
	[GeneratorMenu(menu = "Map", name = "Slope (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class SlopeGenerator : Generator
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x00016798 File Offset: 0x00014998
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000167A8 File Offset: 0x000149A8
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000C85E8 File Offset: 0x000C67E8
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (chunk.stop || !this.enabled || matrix == null)
			{
				return;
			}
			Matrix matrix2 = new Matrix(matrix.rect, null);
			float dist = this.range;
			float start = this.steepness - dist / 4f;
			start /= (float)MapMagic.instance.terrainHeight;
			dist /= (float)MapMagic.instance.terrainHeight;
			Func<float, float, float, float> blurFn = delegate(float prev, float curr, float next)
			{
				float num = prev - curr;
				if (num < 0f)
				{
					num = -num;
				}
				float num2 = next - curr;
				if (num2 < 0f)
				{
					num2 = -num2;
				}
				float num3 = (((num > num2) ? num : num2) * 1.8f - start) / dist;
				if (num3 < 0f)
				{
					num3 = 0f;
				}
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				return num3;
			};
			matrix2.Blur(blurFn, 1f, true, false, true, true, matrix);
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000C86B8 File Offset: 0x000C68B8
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.steepness, "Steepness", default(Rect), 0f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.range, "Range", default(Rect), 0.1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F25 RID: 7973
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F26 RID: 7974
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F27 RID: 7975
		public float steepness = 2.5f;

		// Token: 0x04001F28 RID: 7976
		public float range = 0.3f;
	}
}

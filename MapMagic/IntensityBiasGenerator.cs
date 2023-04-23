using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004E6 RID: 1254
	[GeneratorMenu(menu = "Map", name = "Intensity/Bias", disengageable = true)]
	[Serializable]
	public class IntensityBiasGenerator : Generator
	{
		// Token: 0x06001F63 RID: 8035 RVA: 0x00016D7C File Offset: 0x00014F7C
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00016D8C File Offset: 0x00014F8C
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000CCFF0 File Offset: 0x000CB1F0
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
			for (int i = 0; i < matrix2.count; i++)
			{
				float num = matrix2.array[i];
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
				matrix2.array[i] = num;
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

		// Token: 0x06001F66 RID: 8038 RVA: 0x000CD0F0 File Offset: 0x000CB2F0
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.bias, "Bias", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001FDB RID: 8155
		public float intensity = 1f;

		// Token: 0x04001FDC RID: 8156
		public float bias;

		// Token: 0x04001FDD RID: 8157
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FDE RID: 8158
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FDF RID: 8159
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004EF RID: 1263
	[GeneratorMenu(menu = "Map", name = "Blend", disengageable = true)]
	[Serializable]
	public class BlendGenerator : Generator
	{
		// Token: 0x06001FA2 RID: 8098 RVA: 0x00016F46 File Offset: 0x00015146
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.baseInput;
			yield return this.blendInput;
			yield return this.maskInput;
			yield break;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00016F56 File Offset: 0x00015156
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000CDC40 File Offset: 0x000CBE40
		public static Func<float, float, float> GetAlgorithm(BlendGenerator.Algorithm algorithm)
		{
			switch (algorithm)
			{
			case BlendGenerator.Algorithm.mix:
				return (float a, float b) => b;
			case BlendGenerator.Algorithm.add:
				return (float a, float b) => a + b;
			case BlendGenerator.Algorithm.subtract:
				return (float a, float b) => a - b;
			case BlendGenerator.Algorithm.multiply:
				return (float a, float b) => a * b;
			case BlendGenerator.Algorithm.divide:
				return (float a, float b) => a / b;
			case BlendGenerator.Algorithm.difference:
				return (float a, float b) => Mathf.Abs(a - b);
			case BlendGenerator.Algorithm.min:
				return (float a, float b) => Mathf.Min(a, b);
			case BlendGenerator.Algorithm.max:
				return (float a, float b) => Mathf.Max(a, b);
			case BlendGenerator.Algorithm.overlay:
				return delegate(float a, float b)
				{
					if (a > 0.5f)
					{
						return 1f - 2f * (1f - a) * (1f - b);
					}
					return 2f * a * b;
				};
			case BlendGenerator.Algorithm.hardLight:
				return delegate(float a, float b)
				{
					if (b > 0.5f)
					{
						return 1f - 2f * (1f - a) * (1f - b);
					}
					return 2f * a * b;
				};
			case BlendGenerator.Algorithm.softLight:
				return (float a, float b) => (1f - 2f * b) * a * a + 2f * b * a;
			default:
				return (float a, float b) => b;
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000CDE04 File Offset: 0x000CC004
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.baseInput.GetObject(chunk);
			Matrix matrix2 = (Matrix)this.blendInput.GetObject(chunk);
			Matrix matrix3 = (Matrix)this.maskInput.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix2 == null || matrix == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			matrix = matrix.Copy(null);
			if (this.guiAlgorithm != BlendGenerator.GuiAlgorithm.none)
			{
				this.algorithm = (BlendGenerator.Algorithm)this.guiAlgorithm;
				this.guiAlgorithm = BlendGenerator.GuiAlgorithm.none;
			}
			Func<float, float, float> func = BlendGenerator.GetAlgorithm(this.algorithm);
			for (int i = 0; i < matrix.array.Length; i++)
			{
				float num = ((matrix3 == null) ? 1f : matrix3.array[i]) * this.opacity;
				float num2 = matrix.array[i];
				float arg = matrix2.array[i];
				matrix.array[i] = num2 * (1f - num) + func(num2, arg) * num;
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000CDF20 File Offset: 0x000CC120
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.baseInput.DrawIcon(this.layout, "Base", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.blendInput.DrawIcon(this.layout, "Blend", true);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskInput.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			if (this.guiAlgorithm != BlendGenerator.GuiAlgorithm.none)
			{
				this.algorithm = (BlendGenerator.Algorithm)this.guiAlgorithm;
				this.guiAlgorithm = BlendGenerator.GuiAlgorithm.none;
			}
			this.layout.Field<BlendGenerator.Algorithm>(ref this.algorithm, "Algorithm", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.opacity, "Opacity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002002 RID: 8194
		public Generator.Input baseInput = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002003 RID: 8195
		public Generator.Input blendInput = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002004 RID: 8196
		public Generator.Input maskInput = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002005 RID: 8197
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002006 RID: 8198
		public BlendGenerator.Algorithm algorithm;

		// Token: 0x04002007 RID: 8199
		public float opacity = 1f;

		// Token: 0x04002008 RID: 8200
		public BlendGenerator.GuiAlgorithm guiAlgorithm;

		// Token: 0x020004F0 RID: 1264
		public enum Algorithm
		{
			// Token: 0x0400200A RID: 8202
			mix,
			// Token: 0x0400200B RID: 8203
			add,
			// Token: 0x0400200C RID: 8204
			subtract,
			// Token: 0x0400200D RID: 8205
			multiply,
			// Token: 0x0400200E RID: 8206
			divide,
			// Token: 0x0400200F RID: 8207
			difference,
			// Token: 0x04002010 RID: 8208
			min,
			// Token: 0x04002011 RID: 8209
			max,
			// Token: 0x04002012 RID: 8210
			overlay,
			// Token: 0x04002013 RID: 8211
			hardLight,
			// Token: 0x04002014 RID: 8212
			softLight
		}

		// Token: 0x020004F1 RID: 1265
		public enum GuiAlgorithm
		{
			// Token: 0x04002016 RID: 8214
			mix,
			// Token: 0x04002017 RID: 8215
			add,
			// Token: 0x04002018 RID: 8216
			subtract,
			// Token: 0x04002019 RID: 8217
			multiply,
			// Token: 0x0400201A RID: 8218
			divide,
			// Token: 0x0400201B RID: 8219
			difference,
			// Token: 0x0400201C RID: 8220
			min,
			// Token: 0x0400201D RID: 8221
			max,
			// Token: 0x0400201E RID: 8222
			overlay,
			// Token: 0x0400201F RID: 8223
			hardLight,
			// Token: 0x04002020 RID: 8224
			softLight,
			// Token: 0x04002021 RID: 8225
			none
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000506 RID: 1286
	[Serializable]
	public class NoiseMaskGenerator : Generator
	{
		// Token: 0x06002033 RID: 8243 RVA: 0x0001733E File Offset: 0x0001553E
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.inputIn;
			yield break;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0001734E File Offset: 0x0001554E
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.maskedOut;
			yield return this.invMaskedOut;
			yield break;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000D0490 File Offset: 0x000CE690
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.inputIn.GetObject(chunk);
			Matrix defaultMatrix = chunk.defaultMatrix;
			Matrix defaultMatrix2 = chunk.defaultMatrix;
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null)
			{
				this.maskedOut.SetObject(chunk, matrix);
				return;
			}
			NoiseGenerator.Noise(defaultMatrix, this.size, 1f, 0.5f, 0.5f, this.offset, 12345, null);
			if (chunk.stop)
			{
				return;
			}
			Curve curve = new Curve(this.curve);
			for (int i = 0; i < defaultMatrix.array.Length; i++)
			{
				defaultMatrix.array[i] = curve.Evaluate(defaultMatrix.array[i]);
			}
			if (chunk.stop)
			{
				return;
			}
			for (int j = 0; j < defaultMatrix.array.Length; j++)
			{
				defaultMatrix2.array[j] = 1f - defaultMatrix.array[j];
			}
			if (chunk.stop)
			{
				return;
			}
			if (matrix != null)
			{
				for (int k = 0; k < defaultMatrix.array.Length; k++)
				{
					defaultMatrix.array[k] = matrix.array[k] * defaultMatrix.array[k] * this.opacity + matrix.array[k] * (1f - this.opacity);
				}
				for (int l = 0; l < defaultMatrix2.array.Length; l++)
				{
					defaultMatrix2.array[l] = matrix.array[l] * defaultMatrix2.array[l] * this.opacity + matrix.array[l] * (1f - this.opacity);
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.maskedOut.SetObject(chunk, defaultMatrix);
			this.invMaskedOut.SetObject(chunk, defaultMatrix2);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000D0658 File Offset: 0x000CE858
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.inputIn.DrawIcon(this.layout, "Input", false);
			this.maskedOut.DrawIcon(this.layout, "Masked");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.invMaskedOut.DrawIcon(this.layout, "InvMasked");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			Rect cursor = this.layout.cursor;
			this.layout.rightMargin = 90;
			this.layout.fieldSize = 0.75f;
			this.layout.Field<float>(ref this.opacity, "A", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.size, "S", default(Rect), 1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.offset, "O", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.cursor = cursor;
			this.layout.rightMargin = this.layout.margin;
			this.layout.margin = (int)this.layout.field.width - 85 - this.layout.margin * 2;
			this.layout.Par(53, default(Layout.Val), default(Layout.Val));
			this.layout.Curve(this.curve, this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Color), null);
		}

		// Token: 0x0400208B RID: 8331
		public Generator.Input inputIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400208C RID: 8332
		public Generator.Output maskedOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400208D RID: 8333
		public Generator.Output invMaskedOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400208E RID: 8334
		public float opacity = 1f;

		// Token: 0x0400208F RID: 8335
		public float size = 200f;

		// Token: 0x04002090 RID: 8336
		public Vector2 offset = new Vector2(0f, 0f);

		// Token: 0x04002091 RID: 8337
		public AnimationCurve curve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 1f, 1f),
			new Keyframe(1f, 1f, 1f, 1f)
		});
	}
}

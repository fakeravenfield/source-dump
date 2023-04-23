using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004FC RID: 1276
	[GeneratorMenu(menu = "Map", name = "Slope", disengageable = true)]
	[Serializable]
	public class SlopeGenerator1 : Generator
	{
		// Token: 0x06001FF1 RID: 8177 RVA: 0x00017196 File Offset: 0x00015396
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000171A6 File Offset: 0x000153A6
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000CED68 File Offset: 0x000CCF68
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
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)MapMagic.instance.resolution;
			float num2 = Mathf.Tan((this.steepness.x - this.range / 2f) * 0.017453292f) * num / (float)MapMagic.instance.terrainHeight;
			float num3 = Mathf.Tan((this.steepness.x + this.range / 2f) * 0.017453292f) * num / (float)MapMagic.instance.terrainHeight;
			float num4 = Mathf.Tan((this.steepness.y - this.range / 2f) * 0.017453292f) * num / (float)MapMagic.instance.terrainHeight;
			float num5 = Mathf.Tan((this.steepness.y + this.range / 2f) * 0.017453292f) * num / (float)MapMagic.instance.terrainHeight;
			if (this.steepness.y - this.range / 2f > 89.9f)
			{
				num4 = 20000000f;
			}
			if (this.steepness.y + this.range / 2f > 89.9f)
			{
				num5 = 20000000f;
			}
			if (this.steepness.x < 0.0001f)
			{
				num2 = 0f;
				num3 = 0f;
			}
			Func<float, float, float, float> blurFn = delegate(float prev, float curr, float next)
			{
				float num10 = prev - curr;
				if (num10 < 0f)
				{
					num10 = -num10;
				}
				float num11 = next - curr;
				if (num11 < 0f)
				{
					num11 = -num11;
				}
				if (num10 <= num11)
				{
					return num11;
				}
				return num10;
			};
			matrix2.Blur(blurFn, 1f, false, true, true, true, matrix);
			for (int i = 0; i < matrix2.array.Length; i++)
			{
				float num6 = matrix2.array[i];
				if (this.steepness.x < 0.0001f)
				{
					matrix2.array[i] = 1f - (num6 - num4) / (num5 - num4);
				}
				else
				{
					float num7 = (num6 - num2) / (num3 - num2);
					float num8 = 1f - (num6 - num4) / (num5 - num4);
					float num9 = (num7 > num8) ? num8 : num7;
					if (num9 < 0f)
					{
						num9 = 0f;
					}
					if (num9 > 1f)
					{
						num9 = 1f;
					}
					matrix2.array[i] = num9;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000CF000 File Offset: 0x000CD200
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.6f;
			this.layout.Field<Vector2>(ref this.steepness, "Steepness", default(Rect), 0f, 90f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.range, "Range", default(Rect), 0.1f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002057 RID: 8279
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002058 RID: 8280
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002059 RID: 8281
		public Vector2 steepness = new Vector2(45f, 90f);

		// Token: 0x0400205A RID: 8282
		public float range = 5f;
	}
}

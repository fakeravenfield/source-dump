using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000527 RID: 1319
	[GeneratorMenu(menu = "Objects", name = "Blob", disengageable = true)]
	[Serializable]
	public class BlobGenerator : Generator
	{
		// Token: 0x06002114 RID: 8468 RVA: 0x000178D5 File Offset: 0x00015AD5
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.objectsIn;
			yield return this.canvasIn;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000178E5 File Offset: 0x00015AE5
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000D44B0 File Offset: 0x000D26B0
		public static void DrawBlob(Matrix canvas, Vector2 pos, float val, float radius, AnimationCurve fallof, float noiseAmount = 0f, float noiseSize = 20f)
		{
			CoordRect c = new CoordRect((float)((int)(pos.x - radius - 1f)), (float)((int)(pos.y - radius - 1f)), radius * 2f + 2f, radius * 2f + 2f);
			Curve curve = new Curve(fallof);
			Noise noise = new Noise(noiseSize, MapMagic.instance.resolution, MapMagic.instance.seed * 7, MapMagic.instance.seed * 3);
			CoordRect coordRect = CoordRect.Intersect(canvas.rect, c);
			Coord center = c.Center;
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = Coord.Distance(center, i, j);
					float num2 = curve.Evaluate(1f - num / radius);
					float num3 = num2;
					if (noiseAmount > 0.001f)
					{
						float num4 = num2;
						if (num2 > 0.5f)
						{
							num4 = 1f - num2;
						}
						num3 += (noise.Fractal(i, j, 0.5f) * 2f - 1f) * num4 * noiseAmount;
					}
					canvas[i, j] = val * num3 + canvas[i, j] * (1f - num3);
				}
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000D4628 File Offset: 0x000D2828
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.objectsIn.GetObject(chunk);
			Matrix matrix = (Matrix)this.canvasIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash == null)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2;
			if (matrix != null)
			{
				matrix2 = matrix.Copy(null);
			}
			else
			{
				matrix2 = chunk.defaultMatrix;
			}
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				float num = this.radius * (1f - this.sizeFactor) + this.radius * spatialObject.size * this.sizeFactor;
				num = num / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
				BlobGenerator.DrawBlob(matrix2, spatialObject.pos, this.intensity, num, this.fallof, this.noiseAmount, this.noiseSize);
			}
			Matrix matrix3 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix3 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix3);
			}
			if (this.safeBorders != 0)
			{
				Matrix.SafeBorders(matrix, matrix2, this.safeBorders);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix2);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000D4784 File Offset: 0x000D2984
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.objectsIn.DrawIcon(this.layout, "Objects", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.canvasIn.DrawIcon(this.layout, "Canvas", false);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.margin = 5;
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.radius, "Radius", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Label("Fallof:", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			Rect cursor = this.layout.cursor;
			this.layout.Par(53, default(Layout.Val), default(Layout.Val));
			this.layout.Curve(this.fallof, this.layout.Inset(80, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Color), null);
			this.layout.cursor = cursor;
			this.layout.margin = 86;
			this.layout.fieldSize = 0.8f;
			this.layout.Label("Noise", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			this.layout.Field<float>(ref this.noiseAmount, "A", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.noiseSize, "S", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002129 RID: 8489
		public Generator.Input objectsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x0400212A RID: 8490
		public Generator.Input canvasIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400212B RID: 8491
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400212C RID: 8492
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400212D RID: 8493
		public float intensity = 1f;

		// Token: 0x0400212E RID: 8494
		public float radius = 10f;

		// Token: 0x0400212F RID: 8495
		public float sizeFactor;

		// Token: 0x04002130 RID: 8496
		public AnimationCurve fallof = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 1f, 1f),
			new Keyframe(1f, 1f, 1f, 1f)
		});

		// Token: 0x04002131 RID: 8497
		public float noiseAmount = 0.1f;

		// Token: 0x04002132 RID: 8498
		public float noiseSize = 100f;

		// Token: 0x04002133 RID: 8499
		public int safeBorders;
	}
}

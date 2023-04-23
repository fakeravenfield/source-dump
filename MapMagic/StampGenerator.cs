using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004C5 RID: 1221
	[GeneratorMenu(menu = "Objects", name = "Stamp (legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class StampGenerator : Generator
	{
		// Token: 0x06001E9F RID: 7839 RVA: 0x00016842 File Offset: 0x00014A42
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.objectsIn;
			yield return this.canvasIn;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00016852 File Offset: 0x00014A52
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000C89B4 File Offset: 0x000C6BB4
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.objectsIn.GetObject(chunk);
			Matrix matrix = (Matrix)this.canvasIn.GetObject(chunk);
			if (chunk.stop || spatialHash == null)
			{
				return;
			}
			if (!this.enabled)
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
			float num = this.radius;
			if (this.sizeFactor > 1E-05f)
			{
				float num2 = 0f;
				foreach (SpatialObject spatialObject in spatialHash.AllObjs())
				{
					if (spatialObject.size > num2)
					{
						num2 = spatialObject.size;
					}
				}
				num2 = num2 / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
				num = this.radius * (1f - this.sizeFactor) + this.radius * num2 * this.sizeFactor;
			}
			Matrix matrix3 = new Matrix(new CoordRect(0f, 0f, num * 2f + 2f, num * 2f + 2f), null);
			Matrix matrix4 = new Matrix(new CoordRect(0f, 0f, num * 2f + 2f, num * 2f + 2f), null);
			foreach (SpatialObject spatialObject2 in spatialHash.AllObjs())
			{
				float num3 = this.radius * (1f - this.sizeFactor) + this.radius * spatialObject2.size * this.sizeFactor;
				num3 = num3 / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
				CoordRect newRect = new CoordRect(0f, 0f, num3 * 2f + 2f, num3 * 2f + 2f);
				matrix3.ChangeRect(newRect);
				matrix4.ChangeRect(newRect);
				matrix3.rect.offset = new Coord((int)(spatialObject2.pos.x - num3 - 1f), (int)(spatialObject2.pos.y - num3 - 1f));
				matrix4.rect.offset = new Coord((int)(spatialObject2.pos.x - num3 - 1f), (int)(spatialObject2.pos.y - num3 - 1f));
				CoordRect coordRect = CoordRect.Intersect(matrix3.rect, matrix2.rect);
				Coord min = coordRect.Min;
				Coord max = coordRect.Max;
				for (int i = min.x; i < max.x; i++)
				{
					for (int j = min.z; j < max.z; j++)
					{
						float num4 = Mathf.Sqrt(((float)i - spatialObject2.pos.x + 0.5f) * ((float)i - spatialObject2.pos.x + 0.5f) + ((float)j - spatialObject2.pos.y + 0.5f) * ((float)j - spatialObject2.pos.y + 0.5f));
						float num5 = 1f - num4 / num3;
						if (num5 < 0f || num4 > num3)
						{
							num5 = 0f;
						}
						matrix4[i, j] = num5;
					}
				}
				Curve curve = new Curve(this.curve);
				for (int k = 0; k < matrix4.array.Length; k++)
				{
					matrix4.array[k] = curve.Evaluate(matrix4.array[k]);
				}
				if (this.useNoise)
				{
					NoiseGenerator.Noise(matrix3, this.noiseSize, 0.5f, 0f, 0.5f, Vector2.zero, 12345, null);
					for (int l = min.x; l < max.x; l++)
					{
						for (int m = min.z; m < max.z; m++)
						{
							float num6 = matrix4[l, m];
							if (num6 >= 0.0001f)
							{
								float num7 = matrix3[l, m];
								if (num6 < 0.5f)
								{
									num7 *= num6 * 2f;
								}
								else
								{
									num7 = 1f - (1f - num7) * (1f - num6) * 2f;
								}
								matrix4[l, m] = num7 * this.noiseAmount + num6 * (1f - this.noiseAmount);
							}
						}
					}
				}
				for (int n = min.x; n < max.x; n++)
				{
					for (int num8 = min.z; num8 < max.z; num8++)
					{
						float num9 = matrix4[n, num8];
						matrix2[n, num8] = (this.maxHeight ? 1f : spatialObject2.height) * num9 + matrix2[n, num8] * (1f - num9);
					}
				}
			}
			Matrix matrix5 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix5 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix5);
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

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000C8F80 File Offset: 0x000C7180
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
			this.layout.Field<float>(ref this.radius, "Radius", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Label("Fallof:", default(Rect), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			Rect cursor = this.layout.cursor;
			this.layout.Par(53, default(Layout.Val), default(Layout.Val));
			this.layout.Curve(this.curve, this.layout.Inset(80, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Color), null);
			this.layout.cursor = cursor;
			int margin = this.layout.margin;
			this.layout.margin = 86;
			this.layout.fieldSize = 0.8f;
			this.layout.Toggle(ref this.useNoise, "Noise", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
			this.layout.Field<float>(ref this.noiseAmount, "A", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.noiseSize, "S", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.margin = margin;
			this.layout.fieldSize = 0.5f;
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Toggle(ref this.maxHeight, "Use Maximum Height", default(Rect), default(Layout.Val), default(Layout.Val), null, null, null);
		}

		// Token: 0x04001F33 RID: 7987
		public Generator.Input objectsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04001F34 RID: 7988
		public Generator.Input canvasIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F35 RID: 7989
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F36 RID: 7990
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F37 RID: 7991
		public float radius = 10f;

		// Token: 0x04001F38 RID: 7992
		public AnimationCurve curve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 1f, 1f),
			new Keyframe(1f, 1f, 1f, 1f)
		});

		// Token: 0x04001F39 RID: 7993
		public bool useNoise;

		// Token: 0x04001F3A RID: 7994
		public float noiseAmount = 0.1f;

		// Token: 0x04001F3B RID: 7995
		public float noiseSize = 100f;

		// Token: 0x04001F3C RID: 7996
		public bool maxHeight = true;

		// Token: 0x04001F3D RID: 7997
		public float sizeFactor;

		// Token: 0x04001F3E RID: 7998
		public int safeBorders;
	}
}

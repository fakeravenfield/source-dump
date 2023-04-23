using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004D1 RID: 1233
	[GeneratorMenu(menu = "Map", name = "Simple Form (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class SimpleForm : Generator
	{
		// Token: 0x06001EED RID: 7917 RVA: 0x00016A3B File Offset: 0x00014C3B
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000CA1A4 File Offset: 0x000C83A4
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (!this.enabled || chunk.stop)
			{
				return;
			}
			CoordRect rect = new CoordRect((int)(this.offset.x * (float)MapMagic.instance.resolution / (float)MapMagic.instance.terrainSize), (int)(this.offset.y * (float)MapMagic.instance.resolution / (float)MapMagic.instance.terrainSize), (int)((float)MapMagic.instance.resolution * this.scale), (int)((float)MapMagic.instance.resolution * this.scale));
			Matrix matrix = new Matrix(rect, null);
			float num = 1f / (float)matrix.rect.size.x;
			Coord center = rect.Center;
			float num2 = (float)matrix.rect.size.x / 2f;
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			switch (this.type)
			{
			case SimpleForm.FormType.GradientX:
				for (int i = min.x; i < max.x; i++)
				{
					for (int j = min.z; j < max.z; j++)
					{
						matrix[i, j] = (float)i * num;
					}
				}
				break;
			case SimpleForm.FormType.GradientZ:
				for (int k = min.x; k < max.x; k++)
				{
					for (int l = min.z; l < max.z; l++)
					{
						matrix[k, l] = (float)l * num;
					}
				}
				break;
			case SimpleForm.FormType.Pyramid:
				for (int m = min.x; m < max.x; m++)
				{
					for (int n = min.z; n < max.z; n++)
					{
						float num3 = (float)m * num;
						if (num3 > 1f - num3)
						{
							num3 = 1f - num3;
						}
						float num4 = (float)n * num;
						if (num4 > 1f - num4)
						{
							num4 = 1f - num4;
						}
						matrix[m, n] = ((num3 < num4) ? (num3 * 2f) : (num4 * 2f));
					}
				}
				break;
			case SimpleForm.FormType.Cone:
				for (int num5 = min.x; num5 < max.x; num5++)
				{
					for (int num6 = min.z; num6 < max.z; num6++)
					{
						float num7 = 1f - Coord.Distance(new Coord(num5, num6), center) / num2;
						if (num7 < 0f)
						{
							num7 = 0f;
						}
						matrix[num5, num6] = num7;
					}
				}
				break;
			}
			Matrix defaultMatrix = chunk.defaultMatrix;
			defaultMatrix.Replicate(matrix, this.tile);
			defaultMatrix.Multiply(this.intensity);
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000CA490 File Offset: 0x000C8690
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.62f;
			this.layout.Field<SimpleForm.FormType>(ref this.type, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.scale, "Scale", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.offset, "Offset", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.tile)
			{
				this.wrap = Matrix.WrapMode.Tile;
			}
			this.tile = false;
			this.layout.Field<Matrix.WrapMode>(ref this.wrap, "Wrap Mode", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F6C RID: 8044
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F6D RID: 8045
		public SimpleForm.FormType type = SimpleForm.FormType.Cone;

		// Token: 0x04001F6E RID: 8046
		public float intensity = 1f;

		// Token: 0x04001F6F RID: 8047
		public float scale = 1f;

		// Token: 0x04001F70 RID: 8048
		public Vector2 offset;

		// Token: 0x04001F71 RID: 8049
		public bool tile;

		// Token: 0x04001F72 RID: 8050
		public Matrix.WrapMode wrap;

		// Token: 0x020004D2 RID: 1234
		public enum FormType
		{
			// Token: 0x04001F74 RID: 8052
			GradientX,
			// Token: 0x04001F75 RID: 8053
			GradientZ,
			// Token: 0x04001F76 RID: 8054
			Pyramid,
			// Token: 0x04001F77 RID: 8055
			Cone
		}
	}
}

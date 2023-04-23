using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004DF RID: 1247
	[GeneratorMenu(menu = "Map", name = "Simple Form", disengageable = true)]
	[Serializable]
	public class SimpleForm1 : Generator
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x00016C3E File Offset: 0x00014E3E
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000CBD88 File Offset: 0x000C9F88
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (!this.enabled || chunk.stop)
			{
				return;
			}
			Matrix defaultMatrix = chunk.defaultMatrix;
			Coord min = defaultMatrix.rect.Min;
			Coord max = defaultMatrix.rect.Max;
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)defaultMatrix.rect.size.x;
			float num2 = (float)defaultMatrix.rect.size.x;
			Vector2 a = new Vector2((float)defaultMatrix.rect.size.x / 2f, (float)defaultMatrix.rect.size.z / 2f);
			float num3 = (float)defaultMatrix.rect.size.x / 2f;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num4 = ((float)i - this.offset.x / num) / this.scale;
					float num5 = ((float)j - this.offset.y / num) / this.scale;
					if (this.wrap == Matrix.WrapMode.Once && (num4 < 0f || num4 >= num2 || num5 < 0f || num5 >= num2))
					{
						defaultMatrix[i, j] = 0f;
					}
					else
					{
						if (this.wrap == Matrix.WrapMode.Clamp)
						{
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							if (num4 >= num2)
							{
								num4 = num2 - 1f;
							}
							if (num5 < 0f)
							{
								num5 = 0f;
							}
							if (num5 >= num2)
							{
								num5 = num2 - 1f;
							}
						}
						else if (this.wrap == Matrix.WrapMode.Tile)
						{
							num4 %= num2;
							if (num4 < 0f)
							{
								num4 = num2 + num4;
							}
							num5 %= num2;
							if (num5 < 0f)
							{
								num5 = num2 + num5;
							}
						}
						else if (this.wrap == Matrix.WrapMode.PingPong)
						{
							num4 %= num2 * 2f;
							if (num4 < 0f)
							{
								num4 = num2 * 2f + num4;
							}
							if (num4 >= num2)
							{
								num4 = num2 * 2f - num4 - 1f;
							}
							num5 %= num2 * 2f;
							if (num5 < 0f)
							{
								num5 = num2 * 2f + num5;
							}
							if (num5 >= num2)
							{
								num5 = num2 * 2f - num5 - 1f;
							}
						}
						float num6 = 0f;
						switch (this.type)
						{
						case SimpleForm1.FormType.GradientX:
							num6 = num4 / num2;
							break;
						case SimpleForm1.FormType.GradientZ:
							num6 = num5 / num2;
							break;
						case SimpleForm1.FormType.Pyramid:
						{
							float num7 = num4 / num2;
							if (num7 > 1f - num7)
							{
								num7 = 1f - num7;
							}
							float num8 = num5 / num2;
							if (num8 > 1f - num8)
							{
								num8 = 1f - num8;
							}
							num6 = ((num7 < num8) ? (num7 * 2f) : (num8 * 2f));
							break;
						}
						case SimpleForm1.FormType.Cone:
							num6 = 1f - (a - new Vector2(num4, num5)).magnitude / num3;
							if (num6 < 0f)
							{
								num6 = 0f;
							}
							break;
						}
						defaultMatrix[i, j] = num6 * this.intensity;
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000CC100 File Offset: 0x000CA300
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.62f;
			this.layout.Field<SimpleForm1.FormType>(ref this.type, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.scale, "Scale", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.offset, "Offset", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Matrix.WrapMode>(ref this.wrap, "Wrap Mode", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001FB0 RID: 8112
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001FB1 RID: 8113
		public SimpleForm1.FormType type = SimpleForm1.FormType.Cone;

		// Token: 0x04001FB2 RID: 8114
		public float intensity = 1f;

		// Token: 0x04001FB3 RID: 8115
		public float scale = 1f;

		// Token: 0x04001FB4 RID: 8116
		public Vector2 offset;

		// Token: 0x04001FB5 RID: 8117
		public Matrix.WrapMode wrap;

		// Token: 0x020004E0 RID: 1248
		public enum FormType
		{
			// Token: 0x04001FB7 RID: 8119
			GradientX,
			// Token: 0x04001FB8 RID: 8120
			GradientZ,
			// Token: 0x04001FB9 RID: 8121
			Pyramid,
			// Token: 0x04001FBA RID: 8122
			Cone
		}
	}
}

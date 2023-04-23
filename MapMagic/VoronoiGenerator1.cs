using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004DB RID: 1243
	[GeneratorMenu(menu = "Map", name = "Voronoi", disengageable = true)]
	[Serializable]
	public class VoronoiGenerator1 : Generator
	{
		// Token: 0x06001F27 RID: 7975 RVA: 0x00016BCA File Offset: 0x00014DCA
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00016BDA File Offset: 0x00014DDA
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000CB3F8 File Offset: 0x000C95F8
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.input.GetObject(chunk);
			if (matrix != null)
			{
				matrix = matrix.Copy(null);
			}
			if (matrix == null)
			{
				matrix = chunk.defaultMatrix;
			}
			Matrix matrix2 = (Matrix)this.maskIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || this.intensity == 0f || this.cellCount == 0)
			{
				this.output.SetObject(chunk, matrix);
				return;
			}
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + this.seed, 100);
			float num = 1f * (float)matrix.rect.size.x / (float)this.cellCount;
			Matrix2<Vector3> matrix3 = new Matrix2<Vector3>(new CoordRect(0, 0, this.cellCount + 2, this.cellCount + 2), null);
			matrix3.rect.offset = new Coord(-1, -1);
			float num2 = this.intensity * (float)this.cellCount / (float)matrix.rect.size.x * 26f;
			Coord coord = new Coord((int)((float)matrix.rect.offset.x / num), (int)((float)matrix.rect.offset.z / num));
			for (int i = -1; i < matrix3.rect.size.x - 1; i++)
			{
				for (int j = -1; j < matrix3.rect.size.z - 1; j++)
				{
					Vector3 a = new Vector3((float)i + instanceRandom.CoordinateRandom(i + coord.x, j + coord.z), 0f, (float)j + instanceRandom.NextCoordinateRandom());
					Vector3 a2 = new Vector3((float)i + 0.5f, 0f, (float)j + 0.5f);
					Vector3 vector = a * (1f - this.uniformity) + a2 * this.uniformity;
					vector = vector * num + new Vector3((float)matrix.rect.offset.x, 0f, (float)matrix.rect.offset.z);
					vector.y = instanceRandom.NextCoordinateRandom();
					matrix3[i, j] = vector;
				}
			}
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int k = min.x; k < max.x; k++)
			{
				for (int l = min.z; l < max.z; l++)
				{
					Coord coord2 = new Coord((int)((float)(k - matrix.rect.offset.x) / num), (int)((float)(l - matrix.rect.offset.z) / num));
					float num3 = 200000000f;
					float num4 = 200000000f;
					float num5 = 0f;
					for (int m = -1; m <= 1; m++)
					{
						for (int n = -1; n <= 1; n++)
						{
							Coord c = new Coord(coord2.x + m, coord2.z + n);
							Vector3 vector2 = matrix3[c];
							float num6 = ((float)k - vector2.x) * ((float)k - vector2.x) + ((float)l - vector2.z) * ((float)l - vector2.z);
							if (num6 < num3)
							{
								num4 = num3;
								num3 = num6;
								num5 = vector2.y;
							}
							else if (num6 < num4)
							{
								num4 = num6;
							}
						}
					}
					float num7 = 0f;
					switch (this.blendType)
					{
					case VoronoiGenerator1.BlendType.flat:
						num7 = num5;
						break;
					case VoronoiGenerator1.BlendType.closest:
						num7 = num3 / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator1.BlendType.secondClosest:
						num7 = num4 / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator1.BlendType.cellular:
						num7 = (num4 - num3) / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator1.BlendType.organic:
						num7 = (num4 + num3) / 2f / (float)(MapMagic.instance.resolution * 16);
						break;
					}
					if (matrix2 == null)
					{
						Matrix matrix4 = matrix;
						int num8 = k;
						int num9 = l;
						matrix4[num8, num9] += num7 * num2;
					}
					else
					{
						Matrix matrix4 = matrix;
						int num9 = k;
						int num8 = l;
						matrix4[num9, num8] += num7 * num2 * matrix2[k, l];
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000CB8A0 File Offset: 0x000C9AA0
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.5f;
			this.layout.Field<VoronoiGenerator1.BlendType>(ref this.blendType, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.cellCount, "Cell Count", default(Rect), 1f, 128f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.cellCount = Mathf.ClosestPowerOfTwo(this.cellCount);
			this.layout.Field<float>(ref this.uniformity, "Uniformity", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F9A RID: 8090
		public float intensity = 1f;

		// Token: 0x04001F9B RID: 8091
		public int cellCount = 16;

		// Token: 0x04001F9C RID: 8092
		public float uniformity;

		// Token: 0x04001F9D RID: 8093
		public int seed = 12345;

		// Token: 0x04001F9E RID: 8094
		public VoronoiGenerator1.BlendType blendType = VoronoiGenerator1.BlendType.cellular;

		// Token: 0x04001F9F RID: 8095
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FA0 RID: 8096
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001FA1 RID: 8097
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x020004DC RID: 1244
		public enum BlendType
		{
			// Token: 0x04001FA3 RID: 8099
			flat,
			// Token: 0x04001FA4 RID: 8100
			closest,
			// Token: 0x04001FA5 RID: 8101
			secondClosest,
			// Token: 0x04001FA6 RID: 8102
			cellular,
			// Token: 0x04001FA7 RID: 8103
			organic
		}
	}
}

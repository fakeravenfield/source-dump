using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004B5 RID: 1205
	[GeneratorMenu(menu = "Map", name = "Voronoi (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class VoronoiGenerator : Generator
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x000165D8 File Offset: 0x000147D8
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000165E8 File Offset: 0x000147E8
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000C6B84 File Offset: 0x000C4D84
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
					float num2 = 200000000f;
					float num3 = 200000000f;
					float num4 = 0f;
					for (int m = -1; m <= 1; m++)
					{
						for (int n = -1; n <= 1; n++)
						{
							Coord c = new Coord(coord2.x + m, coord2.z + n);
							Vector3 vector2 = matrix3[c];
							float num5 = ((float)k - vector2.x) * ((float)k - vector2.x) + ((float)l - vector2.z) * ((float)l - vector2.z);
							if (num5 < num2)
							{
								num3 = num2;
								num2 = num5;
								num4 = vector2.y;
							}
							else if (num5 < num3)
							{
								num3 = num5;
							}
						}
					}
					float num6 = 0f;
					switch (this.blendType)
					{
					case VoronoiGenerator.BlendType.flat:
						num6 = num4;
						break;
					case VoronoiGenerator.BlendType.closest:
						num6 = num2 / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator.BlendType.secondClosest:
						num6 = num3 / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator.BlendType.cellular:
						num6 = (num3 - num2) / (float)(MapMagic.instance.resolution * 16);
						break;
					case VoronoiGenerator.BlendType.organic:
						num6 = (num3 + num2) / 2f / (float)(MapMagic.instance.resolution * 16);
						break;
					}
					if (matrix2 == null)
					{
						Matrix matrix4 = matrix;
						int num7 = k;
						int num8 = l;
						matrix4[num7, num8] += num6 * this.intensity;
					}
					else
					{
						Matrix matrix4 = matrix;
						int num8 = k;
						int num7 = l;
						matrix4[num8, num7] += num6 * this.intensity * matrix2[k, l];
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000C700C File Offset: 0x000C520C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.5f;
			this.layout.Field<VoronoiGenerator.BlendType>(ref this.blendType, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.intensity, "Intensity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.cellCount, "Cell Count", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.cellCount = Mathf.ClosestPowerOfTwo(this.cellCount);
			this.layout.Field<float>(ref this.uniformity, "Uniformity", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001EE4 RID: 7908
		public float intensity = 1f;

		// Token: 0x04001EE5 RID: 7909
		public int cellCount = 16;

		// Token: 0x04001EE6 RID: 7910
		public float uniformity;

		// Token: 0x04001EE7 RID: 7911
		public int seed = 12345;

		// Token: 0x04001EE8 RID: 7912
		public VoronoiGenerator.BlendType blendType = VoronoiGenerator.BlendType.cellular;

		// Token: 0x04001EE9 RID: 7913
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001EEA RID: 7914
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001EEB RID: 7915
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x020004B6 RID: 1206
		public enum BlendType
		{
			// Token: 0x04001EED RID: 7917
			flat,
			// Token: 0x04001EEE RID: 7918
			closest,
			// Token: 0x04001EEF RID: 7919
			secondClosest,
			// Token: 0x04001EF0 RID: 7920
			cellular,
			// Token: 0x04001EF1 RID: 7921
			organic
		}
	}
}

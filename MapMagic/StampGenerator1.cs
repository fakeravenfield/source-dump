using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000524 RID: 1316
	[GeneratorMenu(menu = "Objects", name = "Stamp", disengageable = true)]
	[Serializable]
	public class StampGenerator1 : Generator
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x00017861 File Offset: 0x00015A61
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.positionsIn;
			yield return this.canvasIn;
			yield return this.stampIn;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00017871 File Offset: 0x00015A71
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000D3C54 File Offset: 0x000D1E54
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.stampIn.GetObject(chunk);
			Matrix matrix2 = (Matrix)this.canvasIn.GetObject(chunk);
			SpatialHash spatialHash = (SpatialHash)this.positionsIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null || spatialHash == null)
			{
				this.output.SetObject(chunk, matrix2);
				return;
			}
			Matrix matrix3 = null;
			if (matrix2 == null)
			{
				matrix3 = chunk.defaultMatrix;
			}
			else
			{
				matrix3 = matrix2.Copy(null);
			}
			Func<float, float, float> algorithm = BlendGenerator.GetAlgorithm(this.guiAlgorithm);
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				float num = this.radius * (1f - this.sizeFactor) + this.radius * spatialObject.size * this.sizeFactor;
				num = num / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
				float num2 = num * 2f / (float)matrix.rect.size.x;
				Vector2 vector = spatialObject.pos - new Vector2(num, num);
				Vector2 vector2 = spatialObject.pos + new Vector2(num, num);
				Vector2 vector3 = new Vector2(num * 2f, num * 2f);
				CoordRect coordRect = CoordRect.Intersect(new CoordRect(vector.x, vector.y, vector3.x, vector3.y), matrix3.rect);
				Coord min = coordRect.Min;
				Coord max = coordRect.Max;
				for (int i = min.x; i < max.x; i++)
				{
					for (int j = min.z; j < max.z; j++)
					{
						Vector2 vector4 = new Vector2(1f * ((float)i - vector.x) / (vector2.x - vector.x), 1f * ((float)j - vector.y) / (vector2.y - vector.y));
						float num3 = matrix.CheckGet((int)(vector4.x * (float)matrix.rect.size.x + (float)matrix.rect.offset.x), (int)(vector4.y * (float)matrix.rect.size.z + (float)matrix.rect.offset.z));
						matrix3[i, j] = algorithm(matrix3[i, j], num3 * num2);
					}
				}
			}
			Matrix matrix4 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix4 != null)
			{
				Matrix.Mask(matrix2, matrix3, matrix4);
			}
			if (this.safeBorders != 0)
			{
				Matrix.SafeBorders(matrix2, matrix3, this.safeBorders);
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, matrix3);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000D3F70 File Offset: 0x000D2170
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.positionsIn.DrawIcon(this.layout, "Positions", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.canvasIn.DrawIcon(this.layout, "Canvas", false);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.stampIn.DrawIcon(this.layout, "Stamp", true);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.5f;
			this.layout.Field<BlendGenerator.Algorithm>(ref this.guiAlgorithm, "Algorithm", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.radius, "Radius", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002118 RID: 8472
		public Generator.Input stampIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002119 RID: 8473
		public Generator.Input canvasIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400211A RID: 8474
		public Generator.Input positionsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x0400211B RID: 8475
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x0400211C RID: 8476
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400211D RID: 8477
		public BlendGenerator.Algorithm guiAlgorithm;

		// Token: 0x0400211E RID: 8478
		public float radius = 1f;

		// Token: 0x0400211F RID: 8479
		public float sizeFactor = 1f;

		// Token: 0x04002120 RID: 8480
		public int safeBorders;
	}
}

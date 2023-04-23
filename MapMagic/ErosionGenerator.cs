using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000503 RID: 1283
	[GeneratorMenu(menu = "Map", name = "Erosion", disengageable = true)]
	[Serializable]
	public class ErosionGenerator : Generator
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x000172CA File Offset: 0x000154CA
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.heightIn;
			yield return this.maskIn;
			yield break;
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000172DA File Offset: 0x000154DA
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.heightOut;
			yield return this.cliffOut;
			yield return this.sedimentOut;
			yield break;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000CF99C File Offset: 0x000CDB9C
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.heightIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || this.iterations <= 0 || matrix == null)
			{
				this.heightOut.SetObject(chunk, matrix);
				return;
			}
			Matrix matrix2 = new Matrix(matrix.rect, null);
			Matrix matrix3 = new Matrix(matrix.rect, null);
			Matrix matrix4 = new Matrix(matrix.rect, null);
			int num = 10;
			Matrix matrix5 = new Matrix(matrix.rect.offset - num, matrix.rect.size + num * 2, null);
			matrix5.Fill(matrix, true);
			Matrix matrix6 = new Matrix(matrix5.rect, null);
			Matrix matrix7 = new Matrix(matrix5.rect, null);
			Matrix torrents = new Matrix(matrix5.rect, null);
			int[] stepsArray = new int[1000001];
			int[] heightsInt = new int[matrix5.count];
			int[] order = new int[matrix5.count];
			for (int i = 0; i < this.iterations; i++)
			{
				if (chunk.stop)
				{
					return;
				}
				Erosion.ErosionIteration(matrix5, matrix6, matrix7, matrix5.rect, this.terrainDurability, this.erosionAmount, this.sedimentAmount, this.fluidityIterations, this.ruffle, torrents, null, stepsArray, heightsInt, order);
				Coord min = matrix2.rect.Min;
				Coord max = matrix2.rect.Max;
				for (int j = min.x; j < max.x; j++)
				{
					for (int k = min.z; k < max.z; k++)
					{
						Matrix matrix8 = matrix3;
						int num2 = j;
						int num3 = k;
						matrix8[num2, num3] += matrix6[j, k] * this.cliffOpacity * 30f;
						matrix8 = matrix4;
						num3 = j;
						num2 = k;
						matrix8[num3, num2] += matrix7[j, k] * this.sedimentOpacity;
					}
				}
			}
			matrix2.Fill(matrix5, false);
			Matrix matrix9 = (Matrix)this.maskIn.GetObject(chunk);
			if (matrix9 != null)
			{
				Matrix.Mask(matrix, matrix2, matrix9);
				Matrix.Mask(null, matrix3, matrix9);
				Matrix.Mask(null, matrix4, matrix9);
			}
			if (this.safeBorders != 0)
			{
				Matrix.SafeBorders(matrix, matrix2, this.safeBorders);
				Matrix.SafeBorders(null, matrix3, this.safeBorders);
				Matrix.SafeBorders(null, matrix4, this.safeBorders);
			}
			if (chunk.stop)
			{
				return;
			}
			this.heightOut.SetObject(chunk, matrix2);
			this.cliffOut.SetObject(chunk, matrix3);
			this.sedimentOut.SetObject(chunk, matrix4);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000CFC5C File Offset: 0x000CDE5C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.heightIn.DrawIcon(this.layout, "Heights", true);
			this.heightOut.DrawIcon(this.layout, "Heights");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.maskIn.DrawIcon(this.layout, "Mask", false);
			this.cliffOut.DrawIcon(this.layout, "Cliff");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.sedimentOut.DrawIcon(this.layout, "Sediment");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Par(30, default(Layout.Val), default(Layout.Val));
			this.layout.Label("Generating erosion takes significant amount of time", this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, true, 9, default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			this.layout.Par(2, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.iterations, "Iterations", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.terrainDurability, "Durability", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.erosionAmount, "Erosion", default(Rect), 0f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sedimentAmount, "Sediment", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.fluidityIterations, "Fluidity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.ruffle, "Ruffle", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<int>(ref this.safeBorders, "Safe Borders", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.cliffOpacity, "Cliff Opacity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sedimentOpacity, "Sediment Opacity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002075 RID: 8309
		public Generator.Input heightIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002076 RID: 8310
		public Generator.Input maskIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002077 RID: 8311
		public Generator.Output heightOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002078 RID: 8312
		public Generator.Output cliffOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04002079 RID: 8313
		public Generator.Output sedimentOut = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x0400207A RID: 8314
		public int iterations = 5;

		// Token: 0x0400207B RID: 8315
		public float terrainDurability = 0.9f;

		// Token: 0x0400207C RID: 8316
		public float erosionAmount = 1f;

		// Token: 0x0400207D RID: 8317
		public float sedimentAmount = 0.75f;

		// Token: 0x0400207E RID: 8318
		public int fluidityIterations = 3;

		// Token: 0x0400207F RID: 8319
		public float ruffle = 0.4f;

		// Token: 0x04002080 RID: 8320
		public int safeBorders = 10;

		// Token: 0x04002081 RID: 8321
		public float cliffOpacity = 1f;

		// Token: 0x04002082 RID: 8322
		public float sedimentOpacity = 1f;
	}
}

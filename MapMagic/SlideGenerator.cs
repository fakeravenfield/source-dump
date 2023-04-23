using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000530 RID: 1328
	[GeneratorMenu(menu = "Objects", name = "Slide", disengageable = true)]
	[Serializable]
	public class SlideGenerator : Generator
	{
		// Token: 0x06002154 RID: 8532 RVA: 0x00017A31 File Offset: 0x00015C31
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.stratumIn;
			yield break;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00017A41 File Offset: 0x00015C41
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000D6238 File Offset: 0x000D4438
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.input.GetObject(chunk);
			SpatialHash defaultSpatialHash = chunk.defaultSpatialHash;
			Matrix matrix = (Matrix)this.stratumIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null || spatialHash == null)
			{
				this.output.SetObject(chunk, spatialHash);
				return;
			}
			spatialHash = spatialHash.Copy();
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)MapMagic.instance.resolution;
			float num2 = Mathf.Tan(this.stopSlope * 0.017453292f) * num / (float)MapMagic.instance.terrainHeight;
			for (int i = 0; i < spatialHash.cells.Length; i++)
			{
				SpatialHash.Cell cell = spatialHash.cells[i];
				for (int j = cell.objs.Count - 1; j >= 0; j--)
				{
					SpatialObject spatialObject = cell.objs[j];
					if (chunk.stop)
					{
						return;
					}
					Vector2 vector = spatialObject.pos;
					bool flag = true;
					for (int k = 0; k < this.iterations; k++)
					{
						int num3 = (int)vector.x;
						if (vector.x < 0f)
						{
							num3--;
						}
						int num4 = (int)vector.y;
						if (vector.y < 0f)
						{
							num4--;
						}
						float num5 = matrix[num3, num4];
						float num6 = matrix[num3 + 1, num4];
						float num7 = matrix[num3, num4 + 1];
						float num8 = matrix[num3 + 1, num4 + 1];
						float num9 = num7 - num8;
						float num10 = num5 - num6;
						float num11 = num6 - num8;
						float num12 = num5 - num7;
						float num13 = (num9 > 0f) ? num9 : (-num9);
						float num14 = (num10 > 0f) ? num10 : (-num10);
						float num15 = (num13 > num14) ? num13 : num14;
						float num16 = (num11 > 0f) ? num11 : (-num11);
						float num17 = (num12 > 0f) ? num12 : (-num12);
						float num18 = (num16 > num17) ? num16 : num17;
						if (((num15 > num18) ? num15 : num18) >= num2)
						{
							Vector2 a = new Vector2((num9 + num10) / 2f, (num11 + num12) / 2f);
							vector += a * ((float)MapMagic.instance.terrainHeight * this.moveFactor);
							flag = (vector.x > spatialHash.offset.x + 1f && vector.x < spatialHash.offset.x + spatialHash.size - 1.01f && vector.y > spatialHash.offset.y + 1f && vector.y < spatialHash.offset.y + spatialHash.size - 1.01f);
							if (!flag)
							{
								break;
							}
						}
					}
					if (flag)
					{
						spatialObject.pos = vector;
						defaultSpatialHash.Add(spatialObject);
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultSpatialHash);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000D655C File Offset: 0x000D475C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.stratumIn.DrawIcon(this.layout, "Height", true);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.iterations, "Iterations", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.moveFactor, "Move Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.stopSlope, "Stop Slope", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04002161 RID: 8545
		public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04002162 RID: 8546
		public Generator.Input stratumIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002163 RID: 8547
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x04002164 RID: 8548
		public int smooth;

		// Token: 0x04002165 RID: 8549
		public int iterations = 10;

		// Token: 0x04002166 RID: 8550
		public float moveFactor = 3f;

		// Token: 0x04002167 RID: 8551
		public float stopSlope = 15f;
	}
}

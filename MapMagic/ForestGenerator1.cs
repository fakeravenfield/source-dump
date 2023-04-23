using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200052D RID: 1325
	[GeneratorMenu(menu = "Objects", name = "Forest", disengageable = true)]
	[Serializable]
	public class ForestGenerator1 : Generator
	{
		// Token: 0x0600213F RID: 8511 RVA: 0x000179BD File Offset: 0x00015BBD
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.seedlingsIn;
			yield return this.otherTreesIn;
			yield return this.soilIn;
			yield break;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000179CD File Offset: 0x00015BCD
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.treesOut;
			yield break;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000D5690 File Offset: 0x000D3890
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.seedlingsIn.GetObject(chunk);
			SpatialHash spatialHash2 = (SpatialHash)this.otherTreesIn.GetObject(chunk);
			Matrix matrix = (Matrix)this.soilIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash == null)
			{
				this.treesOut.SetObject(chunk, spatialHash);
				return;
			}
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + 12345 + chunk.coord.x * 1000 + chunk.coord.z, 100);
			int num = (int)Mathf.Sqrt(this.density * 10000f);
			float num2 = spatialHash.size / (float)num;
			float num3 = 1f * (float)MapMagic.instance.resolution / (float)num;
			Matrix matrix2 = new Matrix(new CoordRect(0, 0, num, num), null);
			Matrix matrix3 = new Matrix(new CoordRect(0, 0, num, num), null);
			if (spatialHash2 != null)
			{
				foreach (SpatialObject spatialObject in spatialHash2.AllObjs())
				{
					matrix3[(int)((spatialObject.pos.x - spatialHash.offset.x) / num2 + 0.01f), (int)((spatialObject.pos.y - spatialHash.offset.y) / num2 + 0.01f)] = 1f;
				}
			}
			for (int i = 0; i < this.years; i++)
			{
				foreach (object obj in spatialHash)
				{
					SpatialObject spatialObject2 = (SpatialObject)obj;
					int x = (int)((spatialObject2.pos.x - spatialHash.offset.x) / num2 + 0.01f);
					int z = (int)((spatialObject2.pos.y - spatialHash.offset.y) / num2 + 0.01f);
					if (matrix3[x, z] <= 0.01f)
					{
						matrix2[x, z] = this.reproductiveAge + 1f;
					}
				}
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						float num4 = matrix2[j, k];
						if (num4 >= 0.5f)
						{
							num4 = (matrix2[j, k] = num4 + 1f);
							float num5 = this.survivalRate;
							if (matrix != null)
							{
								int num6 = (int)((float)j * num3);
								if (num6 < 0)
								{
									num6--;
								}
								num6 += matrix.rect.offset.x;
								int num7 = (int)((float)k * num3);
								if (num7 < 0)
								{
									num7--;
								}
								num7 += matrix.rect.offset.z;
								num5 *= matrix[num6, num7];
							}
							if (num4 > this.lifeAge || instanceRandom.CoordinateRandom(j, k) > num5)
							{
								matrix2[j, k] = 0f;
							}
							if (num4 > this.reproductiveAge && instanceRandom.Random() < this.fecundity)
							{
								int x2 = (int)((instanceRandom.Random() * 2f - 1f) * this.seedDist / num2) + j;
								int z2 = (int)((instanceRandom.Random() * 2f - 1f) * this.seedDist / num2) + k;
								if (matrix2.rect.CheckInRange(x2, z2) && matrix2[x2, z2] < 0.5f && matrix3[x2, z2] < 0.01f)
								{
									matrix2[x2, z2] = 1f;
								}
							}
						}
					}
				}
			}
			SpatialHash defaultSpatialHash = chunk.defaultSpatialHash;
			for (int l = 0; l < num; l++)
			{
				for (int m = 0; m < num; m++)
				{
					Vector2 vector = new Vector2((float)l * num2 + defaultSpatialHash.offset.x, (float)m * num2 + defaultSpatialHash.offset.y);
					vector += new Vector2(instanceRandom.CoordinateRandom(l, m) * num2, instanceRandom.CoordinateRandom(m, l) * num2);
					if (!defaultSpatialHash.IsAnyObjInRange(vector, num2 / 2f) && vector.x >= defaultSpatialHash.offset.x + 1.001f && vector.y >= defaultSpatialHash.offset.y + 1.001f && vector.x <= defaultSpatialHash.offset.x + defaultSpatialHash.size - 1.001f && vector.y <= defaultSpatialHash.offset.y + defaultSpatialHash.size - 1.001f && matrix2[l, m] > 0.5f)
					{
						defaultSpatialHash.Add(vector, 0f, 0f, matrix2[l, m], -1);
					}
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.treesOut.SetObject(chunk, defaultSpatialHash);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000D5BEC File Offset: 0x000D3DEC
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.seedlingsIn.DrawIcon(this.layout, "Seedlings", true);
			this.treesOut.DrawIcon(this.layout, "Trees");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.otherTreesIn.DrawIcon(this.layout, "Other Trees", false);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.soilIn.DrawIcon(this.layout, "Soil", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.years, "Years", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.density, "Density", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.fecundity, "Fecundity", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.seedDist, "Seed Dist", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.reproductiveAge, "Reproductive Age", default(Rect), -200000000f, this.lifeAge, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.survivalRate, "Survival Rate", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.lifeAge, "Max Age", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x0400214E RID: 8526
		public Generator.Input seedlingsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x0400214F RID: 8527
		public Generator.Input otherTreesIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04002150 RID: 8528
		public Generator.Input soilIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04002151 RID: 8529
		public Generator.Output treesOut = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x04002152 RID: 8530
		public int years = 50;

		// Token: 0x04002153 RID: 8531
		public float density = 3f;

		// Token: 0x04002154 RID: 8532
		public float fecundity = 0.5f;

		// Token: 0x04002155 RID: 8533
		public float seedDist = 10f;

		// Token: 0x04002156 RID: 8534
		public float reproductiveAge = 10f;

		// Token: 0x04002157 RID: 8535
		public float survivalRate = 0.5f;

		// Token: 0x04002158 RID: 8536
		public float lifeAge = 100f;
	}
}

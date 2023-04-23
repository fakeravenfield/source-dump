using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000521 RID: 1313
	[GeneratorMenu(menu = "Objects", name = "Propagate", disengageable = true)]
	[Serializable]
	public class PropagateGenerator : Generator
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x000177ED File Offset: 0x000159ED
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000177FD File Offset: 0x000159FD
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000D3530 File Offset: 0x000D1730
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.input.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash == null)
			{
				this.output.SetObject(chunk, spatialHash);
				return;
			}
			SpatialHash defaultSpatialHash = chunk.defaultSpatialHash;
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + this.seed + chunk.coord.x * 1000 + chunk.coord.z, 100);
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				float num = this.growth.x + instanceRandom.CoordinateRandom(spatialObject.id) * (this.growth.y - this.growth.x);
				num = num * (1f - this.sizeFactor) + num * spatialObject.size * this.sizeFactor;
				num = Mathf.Round(num);
				int num2 = 0;
				while ((float)num2 < num)
				{
					float f = instanceRandom.CoordinateRandom(spatialObject.id, num2 * 2) * 3.1415927f * 2f;
					Vector2 a = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
					float num3 = this.distance.x + instanceRandom.CoordinateRandom(spatialObject.id, num2 * 2 + 1) * (this.distance.y - this.distance.x);
					num3 = num3 * (1f - this.sizeFactor) + num3 * spatialObject.size * this.sizeFactor;
					num3 = num3 / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
					Vector2 vector = spatialObject.pos + a * num3;
					if (vector.x <= defaultSpatialHash.offset.x + 1.01f)
					{
						vector.x = defaultSpatialHash.offset.x + 1.01f;
					}
					if (vector.y <= defaultSpatialHash.offset.y + 1.01f)
					{
						vector.y = defaultSpatialHash.offset.y + 1.01f;
					}
					if (vector.x >= defaultSpatialHash.offset.x + defaultSpatialHash.size - 1.01f)
					{
						vector.x = defaultSpatialHash.offset.x + defaultSpatialHash.size - 1.01f;
					}
					if (vector.y >= defaultSpatialHash.offset.y + defaultSpatialHash.size - 1.01f)
					{
						vector.y = defaultSpatialHash.offset.y + defaultSpatialHash.size - 1.01f;
					}
					defaultSpatialHash.Add(vector, spatialObject.height, spatialObject.rotation, spatialObject.size, spatialObject.id + num2);
					num2++;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultSpatialHash);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000D3858 File Offset: 0x000D1A58
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.65f;
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.growth, "Growth", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.distance, "Distance", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x0400210A RID: 8458
		public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x0400210B RID: 8459
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x0400210C RID: 8460
		public int seed = 12345;

		// Token: 0x0400210D RID: 8461
		public Vector2 growth = new Vector2(1f, 2f);

		// Token: 0x0400210E RID: 8462
		public Vector2 distance = new Vector2(1f, 10f);

		// Token: 0x0400210F RID: 8463
		public float sizeFactor;
	}
}

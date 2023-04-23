using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200050C RID: 1292
	[GeneratorMenu(menu = "Objects", name = "Scatter", disengageable = true)]
	[Serializable]
	public class ScatterGenerator : Generator
	{
		// Token: 0x0600205D RID: 8285 RVA: 0x00017426 File Offset: 0x00015626
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.probability;
			yield break;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00017436 File Offset: 0x00015636
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000D132C File Offset: 0x000CF52C
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.probability.GetObject(chunk);
			SpatialHash defaultSpatialHash = chunk.defaultSpatialHash;
			if (!this.enabled)
			{
				this.output.SetObject(chunk, defaultSpatialHash);
				return;
			}
			if (chunk.stop)
			{
				return;
			}
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + this.seed + chunk.coord.x * 1000 + chunk.coord.z, 100);
			int num = (int)(this.uniformity * 100f);
			int num2 = 0;
			while ((float)num2 < this.count)
			{
				Vector2 p = Vector3.zero;
				float num3 = 0f;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = new Vector2(defaultSpatialHash.offset.x + 1f + instanceRandom.Random() * (defaultSpatialHash.size - 2.01f), defaultSpatialHash.offset.y + 1f + instanceRandom.Random() * (defaultSpatialHash.size - 2.01f));
					if (matrix == null || matrix[vector] >= instanceRandom.Random() + 0.0001f)
					{
						float num4 = defaultSpatialHash.MinDist(vector, true);
						if (num4 > num3)
						{
							num3 = num4;
							p = vector;
						}
					}
				}
				if (num3 > 0.001f)
				{
					defaultSpatialHash.Add(p, 0f, 0f, 1f, -1);
				}
				num2++;
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultSpatialHash);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000D14B8 File Offset: 0x000CF6B8
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.probability.DrawIcon(this.layout, "Probability", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.count, "Count", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.uniformity, "Uniformity", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x040020AC RID: 8364
		public Generator.Input probability = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040020AD RID: 8365
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x040020AE RID: 8366
		public int seed = 12345;

		// Token: 0x040020AF RID: 8367
		public float count = 10f;

		// Token: 0x040020B0 RID: 8368
		public float uniformity = 0.1f;
	}
}

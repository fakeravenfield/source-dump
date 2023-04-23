using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000513 RID: 1299
	[GeneratorMenu(menu = "Objects", name = "Clean Up", disengageable = true)]
	[Serializable]
	public class CleanUpGenerator : Generator
	{
		// Token: 0x06002087 RID: 8327 RVA: 0x0001750E File Offset: 0x0001570E
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.mask;
			yield return this.input;
			yield break;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0001751E File Offset: 0x0001571E
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000D1FD0 File Offset: 0x000D01D0
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix matrix = (Matrix)this.mask.GetObject(chunk);
			SpatialHash spatialHash = (SpatialHash)this.input.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || matrix == null)
			{
				this.output.SetObject(chunk, spatialHash);
				return;
			}
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + this.seed + chunk.coord.x * 1000 + chunk.coord.z, 100);
			SpatialHash spatialHash2 = new SpatialHash(spatialHash.offset, spatialHash.size, spatialHash.resolution);
			foreach (SpatialObject spatialObject in spatialHash.AllObjs())
			{
				if (matrix[spatialObject.pos] >= 0.0001f && matrix[spatialObject.pos] > instanceRandom.Random())
				{
					spatialHash2.Add(spatialObject);
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, spatialHash2);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000D20F8 File Offset: 0x000D02F8
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", false);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.mask.DrawIcon(this.layout, "Mask", true);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x040020CD RID: 8397
		public Generator.Input mask = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040020CE RID: 8398
		public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x040020CF RID: 8399
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x040020D0 RID: 8400
		public int seed = 12345;
	}
}

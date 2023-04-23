using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200050F RID: 1295
	[GeneratorMenu(menu = "Objects", name = "Adjust", disengageable = true)]
	[Serializable]
	public class AdjustGenerator : Generator
	{
		// Token: 0x06002072 RID: 8306 RVA: 0x0001749A File Offset: 0x0001569A
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield return this.intensity;
			yield break;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000174AA File Offset: 0x000156AA
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000D1814 File Offset: 0x000CFA14
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.input.GetObject(chunk);
			if (spatialHash == null)
			{
				return;
			}
			SpatialHash spatialHash2 = spatialHash.Copy();
			Matrix matrix = (Matrix)this.intensity.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash2 == null)
			{
				this.output.SetObject(chunk, spatialHash2);
				return;
			}
			spatialHash2 = spatialHash2.Copy();
			InstanceRandom instanceRandom = new InstanceRandom(MapMagic.instance.seed + this.seed + chunk.coord.x * 1000 + chunk.coord.z, 1000);
			foreach (SpatialObject spatialObject in spatialHash2.AllObjs())
			{
				float num = 1f;
				if (matrix != null)
				{
					num = matrix[spatialObject.pos];
				}
				if (this.type == AdjustGenerator.Type.relative)
				{
					spatialObject.size *= instanceRandom.CoordinateRandom(spatialObject.id + 2, this.scale) * num;
					num = num * (1f - this.sizeFactor) + num * spatialObject.size * this.sizeFactor;
					spatialObject.height += instanceRandom.CoordinateRandom(spatialObject.id, this.height) * num / (float)MapMagic.instance.terrainHeight;
					spatialObject.rotation += instanceRandom.CoordinateRandom(spatialObject.id + 1, this.rotation) * num;
				}
				else
				{
					spatialObject.size = instanceRandom.CoordinateRandom(spatialObject.id + 2, this.scale) * num;
					num = num * (1f - this.sizeFactor) + num * spatialObject.size * this.sizeFactor;
					spatialObject.height = instanceRandom.CoordinateRandom(spatialObject.id, this.height) * num / (float)MapMagic.instance.terrainHeight;
					spatialObject.rotation = instanceRandom.CoordinateRandom(spatialObject.id + 1, this.rotation) * num;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, spatialHash2);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000D1A70 File Offset: 0x000CFC70
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Input", true);
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.intensity.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.7f;
			this.layout.Field<int>(ref this.seed, "Seed", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<AdjustGenerator.Type>(ref this.type, "Type", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.height, "Height", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.rotation, "Rotation", default(Rect), -360f, 360f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Vector2>(ref this.scale, "Scale", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x040020B9 RID: 8377
		public Generator.Input input = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x040020BA RID: 8378
		public Generator.Input intensity = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040020BB RID: 8379
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x040020BC RID: 8380
		public int seed = 12345;

		// Token: 0x040020BD RID: 8381
		public AdjustGenerator.Type type = AdjustGenerator.Type.relative;

		// Token: 0x040020BE RID: 8382
		public Vector2 height = Vector2.zero;

		// Token: 0x040020BF RID: 8383
		public Vector2 rotation = Vector2.zero;

		// Token: 0x040020C0 RID: 8384
		public Vector2 scale = Vector2.one;

		// Token: 0x040020C1 RID: 8385
		public float sizeFactor;

		// Token: 0x02000510 RID: 1296
		public enum Type
		{
			// Token: 0x040020C3 RID: 8387
			absolute,
			// Token: 0x040020C4 RID: 8388
			relative
		}
	}
}

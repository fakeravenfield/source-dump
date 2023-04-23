using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200051B RID: 1307
	[GeneratorMenu(menu = "Objects", name = "Subtract", disengageable = true)]
	[Serializable]
	public class SubtractGenerator : Generator
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x00017695 File Offset: 0x00015895
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.minuendIn;
			yield return this.subtrahendIn;
			yield break;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000176A5 File Offset: 0x000158A5
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.minuendOut;
			yield break;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000D2D90 File Offset: 0x000D0F90
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.minuendIn.GetObject(chunk);
			SpatialHash spatialHash2 = (SpatialHash)this.subtrahendIn.GetObject(chunk);
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || spatialHash2 == null || spatialHash2.Count == 0 || spatialHash == null)
			{
				this.minuendOut.SetObject(chunk, spatialHash);
				return;
			}
			SpatialHash spatialHash3 = spatialHash.Copy();
			float num = 1f * (float)MapMagic.instance.terrainSize / (float)MapMagic.instance.resolution;
			float num2 = this.distance / num;
			foreach (SpatialObject spatialObject in spatialHash2.AllObjs())
			{
				float range = num2 * (1f - this.sizeFactor) + num2 * spatialObject.size * this.sizeFactor;
				spatialHash3.RemoveObjsInRange(spatialObject.pos, range);
			}
			if (chunk.stop)
			{
				return;
			}
			this.minuendOut.SetObject(chunk, spatialHash3);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000D2EA4 File Offset: 0x000D10A4
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.minuendIn.DrawIcon(this.layout, "Input", false);
			this.minuendOut.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.subtrahendIn.DrawIcon(this.layout, "Subtrahend", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.distance, "Distance", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<float>(ref this.sizeFactor, "Size Factor", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x040020F1 RID: 8433
		public Generator.Input minuendIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x040020F2 RID: 8434
		public Generator.Input subtrahendIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x040020F3 RID: 8435
		public Generator.Output minuendOut = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x040020F4 RID: 8436
		public float distance = 1f;

		// Token: 0x040020F5 RID: 8437
		public float sizeFactor;
	}
}

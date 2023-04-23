using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004CB RID: 1227
	[GeneratorMenu(menu = "Legacy", name = "Subtract (Legacy)", disengageable = true, disabled = true)]
	[Serializable]
	public class SubtractGenerator0 : Generator
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000168F0 File Offset: 0x00014AF0
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.minuendIn;
			yield return this.subtrahendIn;
			yield break;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00016900 File Offset: 0x00014B00
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.minuendOut;
			yield break;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000C99F8 File Offset: 0x000C7BF8
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash spatialHash = (SpatialHash)this.minuendIn.GetObject(chunk);
			SpatialHash spatialHash2 = (SpatialHash)this.subtrahendIn.GetObject(chunk);
			if (chunk.stop || spatialHash == null)
			{
				return;
			}
			if (!this.enabled || spatialHash2 == null || spatialHash2.Count == 0)
			{
				this.minuendOut.SetObject(chunk, spatialHash);
				return;
			}
			SpatialHash spatialHash3 = new SpatialHash(spatialHash.offset, spatialHash.size, spatialHash.resolution);
			float num = this.distance / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
			float num2 = 0f;
			foreach (SpatialObject spatialObject in spatialHash2.AllObjs())
			{
				if (spatialObject.size > num2)
				{
					num2 = spatialObject.size;
				}
			}
			num2 = num2 / (float)MapMagic.instance.terrainSize * (float)MapMagic.instance.resolution;
			float range = num * (1f - this.sizeFactor) + num * num2 * this.sizeFactor;
			foreach (SpatialObject spatialObject2 in spatialHash.AllObjs())
			{
				bool flag = false;
				foreach (SpatialObject spatialObject3 in spatialHash2.ObjsInRange(spatialObject2.pos, range))
				{
					if ((spatialObject2.pos - spatialObject3.pos).magnitude < num * (1f - this.sizeFactor) + num * spatialObject3.size * this.sizeFactor)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					spatialHash3.Add(spatialObject2);
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.minuendOut.SetObject(chunk, spatialHash3);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000C9C0C File Offset: 0x000C7E0C
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

		// Token: 0x04001F54 RID: 8020
		public Generator.Input minuendIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04001F55 RID: 8021
		public Generator.Input subtrahendIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04001F56 RID: 8022
		public Generator.Output minuendOut = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x04001F57 RID: 8023
		public float distance = 1f;

		// Token: 0x04001F58 RID: 8024
		public float sizeFactor;
	}
}

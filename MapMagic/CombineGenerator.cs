using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200051E RID: 1310
	[GeneratorMenu(menu = "Objects", name = "Combine", disengageable = true)]
	[Serializable]
	public class CombineGenerator : Generator
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x00017740 File Offset: 0x00015940
		public override IEnumerable<Generator.Input> Inputs()
		{
			int num;
			for (int i = 0; i < this.inputs.Length; i = num + 1)
			{
				yield return this.inputs[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00017750 File Offset: 0x00015950
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000D3198 File Offset: 0x000D1398
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			if (chunk.stop)
			{
				return;
			}
			if (!this.enabled || this.inputs.Length == 0)
			{
				this.output.SetObject(chunk, null);
				return;
			}
			SpatialHash defaultSpatialHash = chunk.defaultSpatialHash;
			for (int i = 0; i < this.inputs.Length; i++)
			{
				if (chunk.stop)
				{
					return;
				}
				SpatialHash spatialHash = (SpatialHash)this.inputs[i].GetObject(chunk);
				if (spatialHash != null)
				{
					defaultSpatialHash.Add(spatialHash);
				}
			}
			this.output.SetObject(chunk, defaultSpatialHash);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000D321C File Offset: 0x000D141C
		public override void OnGUI()
		{
			if (this.inputs.Length >= 1)
			{
				this.layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.inputs[0].DrawIcon(this.layout, "Input", false);
				this.output.DrawIcon(this.layout, "Output");
			}
			for (int i = 1; i < this.inputs.Length; i++)
			{
				this.layout.Par(20, default(Layout.Val), default(Layout.Val));
				this.inputs[i].DrawIcon(this.layout, null, false);
			}
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<int>(ref this.inputsNum, "Inputs Count", default(Rect), 2f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.inputsNum < 2)
			{
				this.inputsNum = 2;
			}
			if (this.inputsNum != this.inputs.Length)
			{
				if (this.inputsNum > this.inputs.Length)
				{
					for (int j = 0; j < this.inputsNum - this.inputs.Length; j++)
					{
						ArrayTools.Add<Generator.Input>(ref this.inputs, this.inputsNum, new Generator.Input(Generator.InoutType.Objects));
					}
					return;
				}
				ArrayTools.Resize<Generator.Input>(ref this.inputs, this.inputsNum, null);
			}
		}

		// Token: 0x040020FE RID: 8446
		public Generator.Input[] inputs = new Generator.Input[]
		{
			new Generator.Input(Generator.InoutType.Objects),
			new Generator.Input(Generator.InoutType.Objects)
		};

		// Token: 0x040020FF RID: 8447
		public Generator.Output output = new Generator.Output(Generator.InoutType.Objects);

		// Token: 0x04002100 RID: 8448
		public int inputsNum = 2;
	}
}

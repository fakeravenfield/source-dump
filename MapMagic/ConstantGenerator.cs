using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004D6 RID: 1238
	[GeneratorMenu(menu = "Map", name = "Constant", disengageable = true)]
	[Serializable]
	public class ConstantGenerator : Generator
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x00016B08 File Offset: 0x00014D08
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000CAAD8 File Offset: 0x000C8CD8
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			Matrix defaultMatrix = chunk.defaultMatrix;
			if (!this.enabled)
			{
				this.output.SetObject(chunk, defaultMatrix);
				return;
			}
			defaultMatrix.Fill(this.level);
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, defaultMatrix);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000CAB24 File Offset: 0x000C8D24
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.output.DrawIcon(this.layout, "Output");
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<float>(ref this.level, "Value", default(Rect), -200000000f, 1f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F83 RID: 8067
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x04001F84 RID: 8068
		public float level;
	}
}

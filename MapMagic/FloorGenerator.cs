using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004CE RID: 1230
	[GeneratorMenu(menu = "Objects", name = "Floor (Outdated)", disengageable = true, disabled = true)]
	[Serializable]
	public class FloorGenerator : Generator
	{
		// Token: 0x06001ED8 RID: 7896 RVA: 0x0001699B File Offset: 0x00014B9B
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.objsIn;
			yield return this.substrateIn;
			yield break;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000169AB File Offset: 0x00014BAB
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.objsOut;
			yield break;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000C9F00 File Offset: 0x000C8100
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			SpatialHash obj = (SpatialHash)this.objsIn.GetObject(chunk);
			this.objsOut.SetObject(chunk, obj);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000C9F2C File Offset: 0x000C812C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.objsIn.DrawIcon(this.layout, "Input", true);
			this.objsOut.DrawIcon(this.layout, "Output");
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.substrateIn.DrawIcon(this.layout, "Height", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Par(55, default(Layout.Val), default(Layout.Val));
			this.layout.Label("Floor Generator is outdated. To floor objects to terrain use \"Relative Height\" toggle in Object Output toggle.", this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, true, 9, default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
		}

		// Token: 0x04001F61 RID: 8033
		public Generator.Input objsIn = new Generator.Input(Generator.InoutType.Objects);

		// Token: 0x04001F62 RID: 8034
		public Generator.Input substrateIn = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F63 RID: 8035
		public Generator.Output objsOut = new Generator.Output(Generator.InoutType.Objects);
	}
}

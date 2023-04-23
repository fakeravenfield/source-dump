using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000579 RID: 1401
	[GeneratorMenu(menu = "", name = "Biome", disengageable = true, priority = 3)]
	[Serializable]
	public class Biome : Generator, Generator.IOutput
	{
		// Token: 0x060023C7 RID: 9159 RVA: 0x00018F49 File Offset: 0x00017149
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.mask;
			yield break;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Generate(Chunk chunk, Biome biome = null)
		{
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000E5D98 File Offset: 0x000E3F98
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.mask.DrawIcon(this.layout, "Mask", false);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.fieldSize = 0.7f;
			this.layout.margin = 3;
			this.layout.Field<GeneratorsAsset>(ref this.data, "Data", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			if (this.data == null)
			{
				if (this.layout.Button("Create", this.layout.Inset(0.5f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null))
				{
					this.data = ScriptableObject.CreateInstance<GeneratorsAsset>();
					return;
				}
			}
			else if (this.layout.Button("Edit", this.layout.Inset(0.5f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, null))
			{
				MapMagic.instance.guiGens = this.data;
			}
		}

		// Token: 0x040022F6 RID: 8950
		public Generator.Input mask = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040022F7 RID: 8951
		public GeneratorsAsset data;
	}
}

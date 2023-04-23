using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004C8 RID: 1224
	[GeneratorMenu(menu = "Output", name = "Preview", disengageable = true, disabled = true)]
	[Serializable]
	public class PreviewOutput1 : Generator, Generator.IOutput
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x000168B6 File Offset: 0x00014AB6
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000C969C File Offset: 0x000C789C
		public override void OnGUI()
		{
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			this.input.DrawIcon(this.layout, "Matrix", true);
			this.layout.Par(5, default(Layout.Val), default(Layout.Val));
			this.layout.Field<bool>(ref this.onTerrain, "On Terrain", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<bool>(ref this.inWindow, "In Window", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Color>(ref this.whites, "Whites", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			this.layout.Field<Color>(ref this.blacks, "Blacks", default(Rect), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
		}

		// Token: 0x04001F47 RID: 8007
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x04001F48 RID: 8008
		public bool onTerrain;

		// Token: 0x04001F49 RID: 8009
		public bool inWindow;

		// Token: 0x04001F4A RID: 8010
		public Color blacks = new Color(1f, 0f, 0f, 0f);

		// Token: 0x04001F4B RID: 8011
		public Color oldBlacks;

		// Token: 0x04001F4C RID: 8012
		public Color whites = new Color(0f, 1f, 0f, 0f);

		// Token: 0x04001F4D RID: 8013
		public Color oldWhites;

		// Token: 0x04001F4E RID: 8014
		public SplatPrototype redPrototype = new SplatPrototype();

		// Token: 0x04001F4F RID: 8015
		public SplatPrototype greenPrototype = new SplatPrototype();

		// Token: 0x020004C9 RID: 1225
		// (Invoke) Token: 0x06001EB8 RID: 7864
		public delegate void RefreshWindow(object obj);
	}
}

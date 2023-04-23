using System;

namespace MapEditor
{
	// Token: 0x020006D5 RID: 1749
	public class LevelInfoUI : WindowBase
	{
		// Token: 0x06002C0C RID: 11276 RVA: 0x0001E3C7 File Offset: 0x0001C5C7
		protected override void OnShow()
		{
			base.OnShow();
			this.mapNameText.SetText(MapEditor.instance.mapDisplayName);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		protected override void OnHide()
		{
			base.OnHide();
			MapEditor.instance.mapDisplayName = this.mapNameText.GetText();
		}

		// Token: 0x040028A6 RID: 10406
		public InputWithText mapNameText;
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006CA RID: 1738
	public class HelpUI : WindowBase
	{
		// Token: 0x06002BE4 RID: 11236 RVA: 0x0001E263 File Offset: 0x0001C463
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.label.text = this.content.text;
		}

		// Token: 0x04002885 RID: 10373
		public TextAsset content;

		// Token: 0x04002886 RID: 10374
		public Text label;
	}
}

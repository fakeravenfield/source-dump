using System;
using TMPro;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x0200099A RID: 2458
	[Wrapper(typeof(TextMeshProUGUI))]
	public static class WTextMeshProUGUI
	{
		// Token: 0x06003E74 RID: 15988 RVA: 0x0002A36D File Offset: 0x0002856D
		[Getter]
		public static string GetText(TextMeshProUGUI self)
		{
			return self.text;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0002A375 File Offset: 0x00028575
		[Setter]
		public static void SetText(TextMeshProUGUI self, string text)
		{
			self.text = text;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00008D0C File Offset: 0x00006F0C
		[Getter]
		public static Graphic GetGraphic(TextMeshProUGUI self)
		{
			return self;
		}
	}
}

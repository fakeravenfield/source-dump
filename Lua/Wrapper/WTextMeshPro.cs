using System;
using TMPro;
using UnityEngine.UI;

namespace Lua.Wrapper
{
	// Token: 0x02000999 RID: 2457
	[Wrapper(typeof(TextMeshPro))]
	public static class WTextMeshPro
	{
		// Token: 0x06003E71 RID: 15985 RVA: 0x0002A36D File Offset: 0x0002856D
		[Getter]
		public static string GetText(TextMeshPro self)
		{
			return self.text;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0002A375 File Offset: 0x00028575
		[Setter]
		public static void SetText(TextMeshPro self, string text)
		{
			self.text = text;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00008D0C File Offset: 0x00006F0C
		[Getter]
		public static Graphic GetGraphic(TextMeshPro self)
		{
			return self;
		}
	}
}

using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200097F RID: 2431
	[Name("Overlay")]
	public static class WOverlay
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x00029CB1 File Offset: 0x00027EB1
		public static void ShowMessage(string text)
		{
			OverlayUi.ShowOverlayText(text, 3.5f);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00029CBE File Offset: 0x00027EBE
		public static void ShowMessage(string text, float duration)
		{
			duration = Mathf.Clamp(duration, 3.5f, 30f);
			OverlayUi.ShowOverlayText(text, duration);
		}
	}
}

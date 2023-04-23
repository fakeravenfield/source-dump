using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1F RID: 2591
	[Proxy(typeof(WOverlay))]
	public class WOverlayProxy : IProxy
	{
		// Token: 0x0600527E RID: 21118 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0003CD6D File Offset: 0x0003AF6D
		public static void ShowMessage(string text)
		{
			WOverlay.ShowMessage(text);
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0003CD75 File Offset: 0x0003AF75
		public static void ShowMessage(string text, float duration)
		{
			WOverlay.ShowMessage(text, duration);
		}
	}
}

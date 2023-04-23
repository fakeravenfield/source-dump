using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009F6 RID: 2550
	[Proxy(typeof(Screen))]
	public class ScreenProxy : IProxy
	{
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0002A06C File Offset: 0x0002826C
		public static float height
		{
			get
			{
				return (float)Screen.height;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x0002A074 File Offset: 0x00028274
		public static float width
		{
			get
			{
				return (float)Screen.width;
			}
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x0003899B File Offset: 0x00036B9B
		public static void LockCursor()
		{
			WScreen.LockCursor();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x000389A2 File Offset: 0x00036BA2
		public static void UnlockCursor()
		{
			WScreen.UnlockCursor();
		}
	}
}

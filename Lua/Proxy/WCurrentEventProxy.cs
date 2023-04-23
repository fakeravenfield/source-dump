using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A18 RID: 2584
	[Proxy(typeof(WCurrentEvent))]
	public class WCurrentEventProxy : IProxy
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06005233 RID: 21043 RVA: 0x0003CA50 File Offset: 0x0003AC50
		public static bool isConsumed
		{
			get
			{
				return WCurrentEvent.isConsumed;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x0003CA57 File Offset: 0x0003AC57
		public static DynValue listenerData
		{
			get
			{
				return WCurrentEvent.listenerData;
			}
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0003CA5E File Offset: 0x0003AC5E
		public static void Consume()
		{
			WCurrentEvent.Consume();
		}
	}
}

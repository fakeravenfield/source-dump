using System;
using MoonSharp.Interpreter;

namespace Lua.Wrapper
{
	// Token: 0x02000966 RID: 2406
	[Name("CurrentEvent")]
	public static class WCurrentEvent
	{
		// Token: 0x06003D6B RID: 15723 RVA: 0x00029883 File Offset: 0x00027A83
		[Doc("Consume the current event.[..] Consuming an event stops any built in game behaviour from reacting to the event. If a previous Ravenscript callback consumed the event, the ``CurrentEvent.isConsumed`` flag will be set to true.")]
		public static void Consume()
		{
			if (ValueMonitor.IsInvokingCallbackEvent())
			{
				return;
			}
			RavenscriptManager.events.GetCurrentEvent().Consume();
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x0002989C File Offset: 0x00027A9C
		[Doc("Returns true if the current event has been consumed.")]
		public static bool isConsumed
		{
			get
			{
				return !ValueMonitor.IsInvokingCallbackEvent() && RavenscriptManager.events.GetCurrentEvent().isConsumed;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000298B6 File Offset: 0x00027AB6
		[Doc("Returns the listener data assigned to the current event listener.")]
		public static DynValue listenerData
		{
			get
			{
				if (ValueMonitor.IsInvokingCallbackEvent())
				{
					return ValueMonitor.invokingMonitor.monitorData;
				}
				return RavenscriptManager.events.GetCurrentEvent().GetCurrentInvokingListenerData();
			}
		}
	}
}

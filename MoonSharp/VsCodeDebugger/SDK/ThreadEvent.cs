using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000789 RID: 1929
	public class ThreadEvent : Event
	{
		// Token: 0x06002F71 RID: 12145 RVA: 0x00020A55 File Offset: 0x0001EC55
		public ThreadEvent(string reasn, int tid) : base("thread", new
		{
			reason = reasn,
			threadId = tid
		})
		{
		}
	}
}

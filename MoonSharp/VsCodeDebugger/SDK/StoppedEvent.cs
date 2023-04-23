using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000786 RID: 1926
	public class StoppedEvent : Event
	{
		// Token: 0x06002F6E RID: 12142 RVA: 0x00020A1F File Offset: 0x0001EC1F
		public StoppedEvent(int tid, string reasn, string txt = null) : base("stopped", new
		{
			threadId = tid,
			reason = reasn,
			text = txt
		})
		{
		}
	}
}

using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000787 RID: 1927
	public class ExitedEvent : Event
	{
		// Token: 0x06002F6F RID: 12143 RVA: 0x00020A34 File Offset: 0x0001EC34
		public ExitedEvent(int exCode) : base("exited", new
		{
			exitCode = exCode
		})
		{
		}
	}
}

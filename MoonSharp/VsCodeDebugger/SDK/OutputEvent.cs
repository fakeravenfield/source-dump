using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078A RID: 1930
	public class OutputEvent : Event
	{
		// Token: 0x06002F72 RID: 12146 RVA: 0x00020A69 File Offset: 0x0001EC69
		public OutputEvent(string cat, string outpt) : base("output", new
		{
			category = cat,
			output = outpt
		})
		{
		}
	}
}

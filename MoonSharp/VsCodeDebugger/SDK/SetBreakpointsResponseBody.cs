using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000792 RID: 1938
	public class SetBreakpointsResponseBody : ResponseBody
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x00020BC2 File Offset: 0x0001EDC2
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x00020BCA File Offset: 0x0001EDCA
		public Breakpoint[] breakpoints { get; private set; }

		// Token: 0x06002F8C RID: 12172 RVA: 0x00020BD3 File Offset: 0x0001EDD3
		public SetBreakpointsResponseBody(List<Breakpoint> bpts = null)
		{
			if (bpts == null)
			{
				this.breakpoints = new Breakpoint[0];
				return;
			}
			this.breakpoints = bpts.ToArray<Breakpoint>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078D RID: 1933
	public class StackTraceResponseBody : ResponseBody
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x00020AA5 File Offset: 0x0001ECA5
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x00020AAD File Offset: 0x0001ECAD
		public StackFrame[] stackFrames { get; private set; }

		// Token: 0x06002F79 RID: 12153 RVA: 0x00020AB6 File Offset: 0x0001ECB6
		public StackTraceResponseBody(List<StackFrame> frames = null)
		{
			if (frames == null)
			{
				this.stackFrames = new StackFrame[0];
				return;
			}
			this.stackFrames = frames.ToArray<StackFrame>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000790 RID: 1936
	public class ThreadsResponseBody : ResponseBody
	{
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x00020B44 File Offset: 0x0001ED44
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public Thread[] threads { get; private set; }

		// Token: 0x06002F82 RID: 12162 RVA: 0x00020B55 File Offset: 0x0001ED55
		public ThreadsResponseBody(List<Thread> vars = null)
		{
			if (vars == null)
			{
				this.threads = new Thread[0];
				return;
			}
			this.threads = vars.ToArray<Thread>();
		}
	}
}

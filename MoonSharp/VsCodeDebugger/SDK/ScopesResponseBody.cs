using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078E RID: 1934
	public class ScopesResponseBody : ResponseBody
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x00020ADA File Offset: 0x0001ECDA
		// (set) Token: 0x06002F7B RID: 12155 RVA: 0x00020AE2 File Offset: 0x0001ECE2
		public Scope[] scopes { get; private set; }

		// Token: 0x06002F7C RID: 12156 RVA: 0x00020AEB File Offset: 0x0001ECEB
		public ScopesResponseBody(List<Scope> scps = null)
		{
			if (scps == null)
			{
				this.scopes = new Scope[0];
				return;
			}
			this.scopes = scps.ToArray<Scope>();
		}
	}
}

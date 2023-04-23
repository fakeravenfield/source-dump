using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000794 RID: 1940
	public class ProtocolMessage
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x00020C8E File Offset: 0x0001EE8E
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x00020C96 File Offset: 0x0001EE96
		public string type { get; private set; }

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00020C9F File Offset: 0x0001EE9F
		public ProtocolMessage(string typ)
		{
			this.type = typ;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00020CAE File Offset: 0x0001EEAE
		public ProtocolMessage(string typ, int sq)
		{
			this.type = typ;
			this.seq = sq;
		}

		// Token: 0x04002B79 RID: 11129
		public int seq;
	}
}

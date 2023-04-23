using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000784 RID: 1924
	public class Breakpoint
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x000209D9 File Offset: 0x0001EBD9
		// (set) Token: 0x06002F69 RID: 12137 RVA: 0x000209E1 File Offset: 0x0001EBE1
		public bool verified { get; private set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x000209EA File Offset: 0x0001EBEA
		// (set) Token: 0x06002F6B RID: 12139 RVA: 0x000209F2 File Offset: 0x0001EBF2
		public int line { get; private set; }

		// Token: 0x06002F6C RID: 12140 RVA: 0x000209FB File Offset: 0x0001EBFB
		public Breakpoint(bool verified, int line)
		{
			this.verified = verified;
			this.line = line;
		}
	}
}

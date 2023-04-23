using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078C RID: 1932
	public class ErrorResponseBody : ResponseBody
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x00020A85 File Offset: 0x0001EC85
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x00020A8D File Offset: 0x0001EC8D
		public Message error { get; private set; }

		// Token: 0x06002F76 RID: 12150 RVA: 0x00020A96 File Offset: 0x0001EC96
		public ErrorResponseBody(Message error)
		{
			this.error = error;
		}
	}
}

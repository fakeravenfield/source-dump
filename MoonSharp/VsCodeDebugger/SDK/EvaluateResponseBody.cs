using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000791 RID: 1937
	public class EvaluateResponseBody : ResponseBody
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00020B79 File Offset: 0x0001ED79
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x00020B81 File Offset: 0x0001ED81
		public string result { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x00020B8A File Offset: 0x0001ED8A
		// (set) Token: 0x06002F86 RID: 12166 RVA: 0x00020B92 File Offset: 0x0001ED92
		public string type { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x00020B9B File Offset: 0x0001ED9B
		// (set) Token: 0x06002F88 RID: 12168 RVA: 0x00020BA3 File Offset: 0x0001EDA3
		public int variablesReference { get; private set; }

		// Token: 0x06002F89 RID: 12169 RVA: 0x00020BAC File Offset: 0x0001EDAC
		public EvaluateResponseBody(string value, int reff = 0)
		{
			this.result = value;
			this.variablesReference = reff;
		}
	}
}

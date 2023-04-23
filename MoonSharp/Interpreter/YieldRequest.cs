using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C2 RID: 1986
	public class YieldRequest
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x0002223B File Offset: 0x0002043B
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x00022243 File Offset: 0x00020443
		public bool Forced { get; internal set; }

		// Token: 0x04002C2B RID: 11307
		public DynValue[] ReturnValues;
	}
}

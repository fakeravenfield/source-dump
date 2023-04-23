using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007BD RID: 1981
	public class TailCallData
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x00021FBA File Offset: 0x000201BA
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x00021FC2 File Offset: 0x000201C2
		public DynValue Function { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x00021FCB File Offset: 0x000201CB
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x00021FD3 File Offset: 0x000201D3
		public DynValue[] Args { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x00021FDC File Offset: 0x000201DC
		// (set) Token: 0x0600314A RID: 12618 RVA: 0x00021FE4 File Offset: 0x000201E4
		public CallbackFunction Continuation { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x00021FED File Offset: 0x000201ED
		// (set) Token: 0x0600314C RID: 12620 RVA: 0x00021FF5 File Offset: 0x000201F5
		public CallbackFunction ErrorHandler { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x00021FFE File Offset: 0x000201FE
		// (set) Token: 0x0600314E RID: 12622 RVA: 0x00022006 File Offset: 0x00020206
		public DynValue ErrorHandlerBeforeUnwind { get; set; }
	}
}

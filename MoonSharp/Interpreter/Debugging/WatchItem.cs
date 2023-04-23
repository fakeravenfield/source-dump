using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008CA RID: 2250
	public class WatchItem
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x00026688 File Offset: 0x00024888
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x00026690 File Offset: 0x00024890
		public int Address { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x00026699 File Offset: 0x00024899
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000266A1 File Offset: 0x000248A1
		public int BasePtr { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000266AA File Offset: 0x000248AA
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x000266B2 File Offset: 0x000248B2
		public int RetAddress { get; set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000266BB File Offset: 0x000248BB
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x000266C3 File Offset: 0x000248C3
		public string Name { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000266CC File Offset: 0x000248CC
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x000266D4 File Offset: 0x000248D4
		public DynValue Value { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000266DD File Offset: 0x000248DD
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000266E5 File Offset: 0x000248E5
		public SymbolRef LValue { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000266EE File Offset: 0x000248EE
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000266F6 File Offset: 0x000248F6
		public bool IsError { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000266FF File Offset: 0x000248FF
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x00026707 File Offset: 0x00024907
		public SourceRef Location { get; set; }

		// Token: 0x060038D6 RID: 14550 RVA: 0x00126030 File Offset: 0x00124230
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
			{
				this.Address,
				this.BasePtr,
				this.RetAddress,
				this.Name ?? "(null)",
				(this.Value != null) ? this.Value.ToString() : "(null)",
				(this.LValue != null) ? this.LValue.ToString() : "(null)"
			});
		}
	}
}

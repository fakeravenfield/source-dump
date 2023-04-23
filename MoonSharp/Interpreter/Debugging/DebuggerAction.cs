using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C4 RID: 2244
	public class DebuggerAction
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000264A8 File Offset: 0x000246A8
		// (set) Token: 0x06003886 RID: 14470 RVA: 0x000264B0 File Offset: 0x000246B0
		public DebuggerAction.ActionType Action { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000264B9 File Offset: 0x000246B9
		// (set) Token: 0x06003888 RID: 14472 RVA: 0x000264C1 File Offset: 0x000246C1
		public DateTime TimeStampUTC { get; private set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x000264CA File Offset: 0x000246CA
		// (set) Token: 0x0600388A RID: 14474 RVA: 0x000264D2 File Offset: 0x000246D2
		public int SourceID { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x000264DB File Offset: 0x000246DB
		// (set) Token: 0x0600388C RID: 14476 RVA: 0x000264E3 File Offset: 0x000246E3
		public int SourceLine { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x000264EC File Offset: 0x000246EC
		// (set) Token: 0x0600388E RID: 14478 RVA: 0x000264F4 File Offset: 0x000246F4
		public int SourceCol { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000264FD File Offset: 0x000246FD
		// (set) Token: 0x06003890 RID: 14480 RVA: 0x00026505 File Offset: 0x00024705
		public int[] Lines { get; set; }

		// Token: 0x06003891 RID: 14481 RVA: 0x0002650E File Offset: 0x0002470E
		public DebuggerAction()
		{
			this.TimeStampUTC = DateTime.UtcNow;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x00026521 File Offset: 0x00024721
		public TimeSpan Age
		{
			get
			{
				return DateTime.UtcNow - this.TimeStampUTC;
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x00125A74 File Offset: 0x00123C74
		public override string ToString()
		{
			if (this.Action == DebuggerAction.ActionType.ToggleBreakpoint || this.Action == DebuggerAction.ActionType.SetBreakpoint || this.Action == DebuggerAction.ActionType.ClearBreakpoint)
			{
				return string.Format("{0} {1}:({2},{3})", new object[]
				{
					this.Action,
					this.SourceID,
					this.SourceLine,
					this.SourceCol
				});
			}
			return this.Action.ToString();
		}

		// Token: 0x020008C5 RID: 2245
		public enum ActionType
		{
			// Token: 0x04002FAA RID: 12202
			ByteCodeStepIn,
			// Token: 0x04002FAB RID: 12203
			ByteCodeStepOver,
			// Token: 0x04002FAC RID: 12204
			ByteCodeStepOut,
			// Token: 0x04002FAD RID: 12205
			StepIn,
			// Token: 0x04002FAE RID: 12206
			StepOver,
			// Token: 0x04002FAF RID: 12207
			StepOut,
			// Token: 0x04002FB0 RID: 12208
			Run,
			// Token: 0x04002FB1 RID: 12209
			ToggleBreakpoint,
			// Token: 0x04002FB2 RID: 12210
			SetBreakpoint,
			// Token: 0x04002FB3 RID: 12211
			ClearBreakpoint,
			// Token: 0x04002FB4 RID: 12212
			ResetBreakpoints,
			// Token: 0x04002FB5 RID: 12213
			Refresh,
			// Token: 0x04002FB6 RID: 12214
			HardRefresh,
			// Token: 0x04002FB7 RID: 12215
			None
		}
	}
}

using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A7 RID: 2215
	internal class RuntimeScopeBlock
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x0002577D File Offset: 0x0002397D
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x00025785 File Offset: 0x00023985
		public int From { get; internal set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x0002578E File Offset: 0x0002398E
		// (set) Token: 0x0600377C RID: 14204 RVA: 0x00025796 File Offset: 0x00023996
		public int To { get; internal set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x0002579F File Offset: 0x0002399F
		// (set) Token: 0x0600377E RID: 14206 RVA: 0x000257A7 File Offset: 0x000239A7
		public int ToInclusive { get; internal set; }

		// Token: 0x0600377F RID: 14207 RVA: 0x000257B0 File Offset: 0x000239B0
		public override string ToString()
		{
			return string.Format("ScopeBlock : {0} -> {1} --> {2}", this.From, this.To, this.ToInclusive);
		}
	}
}

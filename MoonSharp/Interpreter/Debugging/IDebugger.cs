using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C7 RID: 2247
	public interface IDebugger
	{
		// Token: 0x06003894 RID: 14484
		DebuggerCaps GetDebuggerCaps();

		// Token: 0x06003895 RID: 14485
		void SetDebugService(DebugService debugService);

		// Token: 0x06003896 RID: 14486
		void SetSourceCode(SourceCode sourceCode);

		// Token: 0x06003897 RID: 14487
		void SetByteCode(string[] byteCode);

		// Token: 0x06003898 RID: 14488
		bool IsPauseRequested();

		// Token: 0x06003899 RID: 14489
		bool SignalRuntimeException(ScriptRuntimeException ex);

		// Token: 0x0600389A RID: 14490
		DebuggerAction GetAction(int ip, SourceRef sourceref);

		// Token: 0x0600389B RID: 14491
		void SignalExecutionEnded();

		// Token: 0x0600389C RID: 14492
		void Update(WatchType watchType, IEnumerable<WatchItem> items);

		// Token: 0x0600389D RID: 14493
		List<DynamicExpression> GetWatchItems();

		// Token: 0x0600389E RID: 14494
		void RefreshBreakpoints(IEnumerable<SourceRef> refs);
	}
}

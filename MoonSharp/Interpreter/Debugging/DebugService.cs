using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C3 RID: 2243
	public sealed class DebugService : IScriptPrivateResource
	{
		// Token: 0x06003881 RID: 14465 RVA: 0x00026472 File Offset: 0x00024672
		internal DebugService(Script script, Processor processor)
		{
			this.OwnerScript = script;
			this.m_Processor = processor;
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x00026488 File Offset: 0x00024688
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x00026490 File Offset: 0x00024690
		public Script OwnerScript { get; private set; }

		// Token: 0x06003884 RID: 14468 RVA: 0x00026499 File Offset: 0x00024699
		public HashSet<int> ResetBreakPoints(SourceCode src, HashSet<int> lines)
		{
			return this.m_Processor.ResetBreakPoints(src, lines);
		}

		// Token: 0x04002FA1 RID: 12193
		private Processor m_Processor;
	}
}

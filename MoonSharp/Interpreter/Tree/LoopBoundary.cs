using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007EB RID: 2027
	internal class LoopBoundary : ILoop
	{
		// Token: 0x060032A9 RID: 12969 RVA: 0x00022F5F File Offset: 0x0002115F
		public void CompileBreak(ByteCode bc)
		{
			throw new InternalErrorException("CompileBreak called on LoopBoundary");
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x0000476F File Offset: 0x0000296F
		public bool IsBoundary()
		{
			return true;
		}
	}
}

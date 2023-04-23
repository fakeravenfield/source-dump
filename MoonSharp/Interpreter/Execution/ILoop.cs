using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A5 RID: 2213
	internal interface ILoop
	{
		// Token: 0x06003776 RID: 14198
		void CompileBreak(ByteCode bc);

		// Token: 0x06003777 RID: 14199
		bool IsBoundary();
	}
}

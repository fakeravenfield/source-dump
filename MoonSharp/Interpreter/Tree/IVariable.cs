using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007E5 RID: 2021
	internal interface IVariable
	{
		// Token: 0x06003278 RID: 12920
		void CompileAssignment(ByteCode bc, int stackofs, int tupleidx);
	}
}

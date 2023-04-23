using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007EA RID: 2026
	internal class Loop : ILoop
	{
		// Token: 0x060032A6 RID: 12966 RVA: 0x00022F28 File Offset: 0x00021128
		public void CompileBreak(ByteCode bc)
		{
			bc.Emit_Exit(this.Scope);
			this.BreakJumps.Add(bc.Emit_Jump(OpCode.Jump, -1, 0));
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x0000257D File Offset: 0x0000077D
		public bool IsBoundary()
		{
			return false;
		}

		// Token: 0x04002CE3 RID: 11491
		public RuntimeScopeBlock Scope;

		// Token: 0x04002CE4 RID: 11492
		public List<Instruction> BreakJumps = new List<Instruction>();
	}
}

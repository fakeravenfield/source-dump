using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F2 RID: 2034
	internal class EmptyStatement : Statement
	{
		// Token: 0x060032C6 RID: 12998 RVA: 0x00022FFE File Offset: 0x000211FE
		public EmptyStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Compile(ByteCode bc)
		{
		}
	}
}

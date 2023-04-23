using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200089F RID: 2207
	[Flags]
	internal enum InstructionFieldUsage
	{
		// Token: 0x04002EDF RID: 11999
		None = 0,
		// Token: 0x04002EE0 RID: 12000
		Symbol = 1,
		// Token: 0x04002EE1 RID: 12001
		SymbolList = 2,
		// Token: 0x04002EE2 RID: 12002
		Name = 4,
		// Token: 0x04002EE3 RID: 12003
		Value = 8,
		// Token: 0x04002EE4 RID: 12004
		NumVal = 16,
		// Token: 0x04002EE5 RID: 12005
		NumVal2 = 32,
		// Token: 0x04002EE6 RID: 12006
		NumValAsCodeAddress = 32784
	}
}

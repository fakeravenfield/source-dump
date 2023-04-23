using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008AD RID: 2221
	[Flags]
	internal enum CallStackItemFlags
	{
		// Token: 0x04002F0B RID: 12043
		None = 0,
		// Token: 0x04002F0C RID: 12044
		EntryPoint = 1,
		// Token: 0x04002F0D RID: 12045
		ResumeEntryPoint = 3,
		// Token: 0x04002F0E RID: 12046
		CallEntryPoint = 5,
		// Token: 0x04002F0F RID: 12047
		TailCall = 16,
		// Token: 0x04002F10 RID: 12048
		MethodCall = 32
	}
}

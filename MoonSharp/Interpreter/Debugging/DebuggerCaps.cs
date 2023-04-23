using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C6 RID: 2246
	[Flags]
	public enum DebuggerCaps
	{
		// Token: 0x04002FB9 RID: 12217
		CanDebugSourceCode = 1,
		// Token: 0x04002FBA RID: 12218
		CanDebugByteCode = 2,
		// Token: 0x04002FBB RID: 12219
		HasLineBasedBreakpoints = 4
	}
}

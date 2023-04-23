using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008AE RID: 2222
	internal sealed class ExecutionState
	{
		// Token: 0x04002F11 RID: 12049
		public FastStack<DynValue> ValueStack = new FastStack<DynValue>(131072);

		// Token: 0x04002F12 RID: 12050
		public FastStack<CallStackItem> ExecutionStack = new FastStack<CallStackItem>(131072);

		// Token: 0x04002F13 RID: 12051
		public int InstructionPtr;

		// Token: 0x04002F14 RID: 12052
		public CoroutineState State = CoroutineState.NotStarted;
	}
}

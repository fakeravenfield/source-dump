using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008AC RID: 2220
	internal class CallStackItem
	{
		// Token: 0x04002EFE RID: 12030
		public int Debug_EntryPoint;

		// Token: 0x04002EFF RID: 12031
		public SymbolRef[] Debug_Symbols;

		// Token: 0x04002F00 RID: 12032
		public SourceRef CallingSourceRef;

		// Token: 0x04002F01 RID: 12033
		public CallbackFunction ClrFunction;

		// Token: 0x04002F02 RID: 12034
		public CallbackFunction Continuation;

		// Token: 0x04002F03 RID: 12035
		public CallbackFunction ErrorHandler;

		// Token: 0x04002F04 RID: 12036
		public DynValue ErrorHandlerBeforeUnwind;

		// Token: 0x04002F05 RID: 12037
		public int BasePointer;

		// Token: 0x04002F06 RID: 12038
		public int ReturnAddress;

		// Token: 0x04002F07 RID: 12039
		public DynValue[] LocalScope;

		// Token: 0x04002F08 RID: 12040
		public ClosureContext ClosureScope;

		// Token: 0x04002F09 RID: 12041
		public CallStackItemFlags Flags;
	}
}

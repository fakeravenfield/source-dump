using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A6 RID: 2214
	internal class LoopTracker
	{
		// Token: 0x04002EEC RID: 12012
		public FastStack<ILoop> Loops = new FastStack<ILoop>(16384);
	}
}

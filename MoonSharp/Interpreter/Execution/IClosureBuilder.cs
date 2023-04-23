using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A4 RID: 2212
	internal interface IClosureBuilder
	{
		// Token: 0x06003775 RID: 14197
		SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol);
	}
}

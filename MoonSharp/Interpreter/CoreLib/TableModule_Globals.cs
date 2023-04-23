using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008EB RID: 2283
	[MoonSharpModule]
	public class TableModule_Globals
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x0002749F File Offset: 0x0002569F
		[MoonSharpModuleMethod]
		public static DynValue unpack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.unpack(executionContext, args);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000274A8 File Offset: 0x000256A8
		[MoonSharpModuleMethod]
		public static DynValue pack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.pack(executionContext, args);
		}
	}
}

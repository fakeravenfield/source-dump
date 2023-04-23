using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E3 RID: 2275
	[MoonSharpModule]
	public class MetaTableModule
	{
		// Token: 0x060039D9 RID: 14809 RVA: 0x00128688 File Offset: 0x00126888
		[MoonSharpModuleMethod]
		public static DynValue setmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "setmetatable", DataType.Table, false);
			DynValue dynValue2 = args.AsType(1, "setmetatable", DataType.Table, true);
			if (executionContext.GetMetamethod(dynValue, "__metatable") != null)
			{
				throw new ScriptRuntimeException("cannot change a protected metatable");
			}
			dynValue.Table.MetaTable = dynValue2.Table;
			return dynValue;
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x001286E0 File Offset: 0x001268E0
		[MoonSharpModuleMethod]
		public static DynValue getmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			Table table = null;
			if (dynValue.Type.CanHaveTypeMetatables())
			{
				table = executionContext.GetScript().GetTypeMetatable(dynValue.Type);
			}
			if (dynValue.Type == DataType.Table)
			{
				table = dynValue.Table.MetaTable;
			}
			if (table == null)
			{
				return DynValue.Nil;
			}
			if (table.RawGet("__metatable") != null)
			{
				return table.Get("__metatable");
			}
			return DynValue.NewTable(table);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x00128754 File Offset: 0x00126954
		[MoonSharpModuleMethod]
		public static DynValue rawget(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawget", DataType.Table, false);
			DynValue key = args[1];
			return dynValue.Table.Get(key);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x00128784 File Offset: 0x00126984
		[MoonSharpModuleMethod]
		public static DynValue rawset(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawset", DataType.Table, false);
			DynValue key = args[1];
			dynValue.Table.Set(key, args[2]);
			return dynValue;
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x001287BC File Offset: 0x001269BC
		[MoonSharpModuleMethod]
		public static DynValue rawequal(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			object obj = args[0];
			DynValue obj2 = args[1];
			return DynValue.NewBoolean(obj.Equals(obj2));
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x001287E4 File Offset: 0x001269E4
		[MoonSharpModuleMethod]
		public static DynValue rawlen(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.String && args[0].Type != DataType.Table)
			{
				throw ScriptRuntimeException.BadArgument(0, "rawlen", "table or string", args[0].Type.ToErrorTypeString(), false);
			}
			return args[0].GetLength();
		}
	}
}

using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007B0 RID: 1968
	public static class LuaTypeExtensions
	{
		// Token: 0x06003099 RID: 12441 RVA: 0x000216AE File Offset: 0x0001F8AE
		public static bool CanHaveTypeMetatables(this DataType type)
		{
			return type < DataType.Table;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0010E124 File Offset: 0x0010C324
		public static string ToErrorTypeString(this DataType type)
		{
			switch (type)
			{
			case DataType.Nil:
				return "nil";
			case DataType.Void:
				return "no value";
			case DataType.Boolean:
				return "boolean";
			case DataType.Number:
				return "number";
			case DataType.String:
				return "string";
			case DataType.Function:
				return "function";
			case DataType.Table:
				return "table";
			case DataType.UserData:
				return "userdata";
			case DataType.Thread:
				return "coroutine";
			case DataType.ClrFunction:
				return "function";
			}
			return string.Format("internal<{0}>", type.ToLuaDebuggerString());
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000216B4 File Offset: 0x0001F8B4
		public static string ToLuaDebuggerString(this DataType type)
		{
			return type.ToString().ToLowerInvariant();
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0010E1BC File Offset: 0x0010C3BC
		public static string ToLuaTypeString(this DataType type)
		{
			switch (type)
			{
			case DataType.Nil:
			case DataType.Void:
				return "nil";
			case DataType.Boolean:
				return "boolean";
			case DataType.Number:
				return "number";
			case DataType.String:
				return "string";
			case DataType.Function:
				return "function";
			case DataType.Table:
				return "table";
			case DataType.UserData:
				return "userdata";
			case DataType.Thread:
				return "thread";
			case DataType.ClrFunction:
				return "function";
			}
			throw new ScriptRuntimeException("Unexpected LuaType {0}", new object[]
			{
				type
			});
		}

		// Token: 0x04002BEB RID: 11243
		internal const DataType MaxMetaTypes = DataType.Table;

		// Token: 0x04002BEC RID: 11244
		internal const DataType MaxConvertibleTypes = DataType.ClrFunction;
	}
}

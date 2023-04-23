using System;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008DF RID: 2271
	[MoonSharpModule(Namespace = "json")]
	public class JsonModule
	{
		// Token: 0x0600398D RID: 14733 RVA: 0x00127F84 File Offset: 0x00126184
		[MoonSharpModuleMethod]
		public static DynValue parse(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				result = DynValue.NewTable(JsonTableConverter.JsonToTable(args.AsType(0, "parse", DataType.String, false).String, executionContext.GetScript()));
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x00127FD0 File Offset: 0x001261D0
		[MoonSharpModuleMethod]
		public static DynValue serialize(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				result = DynValue.NewString(args.AsType(0, "serialize", DataType.Table, false).Table.TableToJson());
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x00128014 File Offset: 0x00126214
		[MoonSharpModuleMethod]
		public static DynValue isnull(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			return DynValue.NewBoolean(JsonNull.IsJsonNull(dynValue) || dynValue.IsNil());
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x00026DB4 File Offset: 0x00024FB4
		[MoonSharpModuleMethod]
		public static DynValue @null(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return JsonNull.Create();
		}
	}
}

using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008DA RID: 2266
	[MoonSharpModule(Namespace = "dynamic")]
	public class DynamicModule
	{
		// Token: 0x06003967 RID: 14695 RVA: 0x00026C52 File Offset: 0x00024E52
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			UserData.RegisterType<DynamicModule.DynamicExprWrapper>(InteropAccessMode.HideMembers, null);
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x001275EC File Offset: 0x001257EC
		[MoonSharpModuleMethod]
		public static DynValue eval(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				if (args[0].Type == DataType.UserData)
				{
					UserData userData = args[0].UserData;
					if (!(userData.Object is DynamicModule.DynamicExprWrapper))
					{
						throw ScriptRuntimeException.BadArgument(0, "dynamic.eval", "A userdata was passed, but was not a previously prepared expression.");
					}
					result = ((DynamicModule.DynamicExprWrapper)userData.Object).Expr.Evaluate(executionContext);
				}
				else
				{
					DynValue dynValue = args.AsType(0, "dynamic.eval", DataType.String, false);
					result = executionContext.GetScript().CreateDynamicExpression(dynValue.String).Evaluate(executionContext);
				}
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x0012768C File Offset: 0x0012588C
		[MoonSharpModuleMethod]
		public static DynValue prepare(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				DynValue dynValue = args.AsType(0, "dynamic.prepare", DataType.String, false);
				DynamicExpression expr = executionContext.GetScript().CreateDynamicExpression(dynValue.String);
				result = UserData.Create(new DynamicModule.DynamicExprWrapper
				{
					Expr = expr
				});
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x020008DB RID: 2267
		private class DynamicExprWrapper
		{
			// Token: 0x04002FF3 RID: 12275
			public DynamicExpression Expr;
		}
	}
}

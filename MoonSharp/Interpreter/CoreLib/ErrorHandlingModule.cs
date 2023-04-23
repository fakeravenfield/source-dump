using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008DC RID: 2268
	[MoonSharpModule]
	public class ErrorHandlingModule
	{
		// Token: 0x0600396C RID: 14700 RVA: 0x00026C5C File Offset: 0x00024E5C
		[MoonSharpModuleMethod]
		public static DynValue pcall(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.SetErrorHandlerStrategy("pcall", executionContext, args, null);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x001276E8 File Offset: 0x001258E8
		private static DynValue SetErrorHandlerStrategy(string funcName, ScriptExecutionContext executionContext, CallbackArguments args, DynValue handlerBeforeUnwind)
		{
			DynValue function = args[0];
			DynValue[] array = new DynValue[args.Count - 1];
			for (int i = 1; i < args.Count; i++)
			{
				array[i - 1] = args[i];
			}
			if (args[0].Type == DataType.ClrFunction)
			{
				try
				{
					DynValue dynValue = args[0].Callback.Invoke(executionContext, array, false);
					if (dynValue.Type == DataType.TailCallRequest)
					{
						if (dynValue.TailCallData.Continuation != null || dynValue.TailCallData.ErrorHandler != null)
						{
							throw new ScriptRuntimeException("the function passed to {0} cannot be called directly by {0}. wrap in a script function instead.", new object[]
							{
								funcName
							});
						}
						return DynValue.NewTailCallReq(new TailCallData
						{
							Args = dynValue.TailCallData.Args,
							Function = dynValue.TailCallData.Function,
							Continuation = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_continuation), funcName),
							ErrorHandler = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_onerror), funcName),
							ErrorHandlerBeforeUnwind = handlerBeforeUnwind
						});
					}
					else
					{
						if (dynValue.Type == DataType.YieldRequest)
						{
							throw new ScriptRuntimeException("the function passed to {0} cannot be called directly by {0}. wrap in a script function instead.", new object[]
							{
								funcName
							});
						}
						return DynValue.NewTupleNested(new DynValue[]
						{
							DynValue.True,
							dynValue
						});
					}
				}
				catch (ScriptRuntimeException ex)
				{
					executionContext.PerformMessageDecorationBeforeUnwind(handlerBeforeUnwind, ex);
					return DynValue.NewTupleNested(new DynValue[]
					{
						DynValue.False,
						DynValue.NewString(ex.DecoratedMessage)
					});
				}
			}
			if (args[0].Type != DataType.Function)
			{
				return DynValue.NewTupleNested(new DynValue[]
				{
					DynValue.False,
					DynValue.NewString("attempt to " + funcName + " a non-function")
				});
			}
			return DynValue.NewTailCallReq(new TailCallData
			{
				Args = array,
				Function = function,
				Continuation = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_continuation), funcName),
				ErrorHandler = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_onerror), funcName),
				ErrorHandlerBeforeUnwind = handlerBeforeUnwind
			});
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00127904 File Offset: 0x00125B04
		private static DynValue MakeReturnTuple(bool retstatus, CallbackArguments args)
		{
			DynValue[] array = new DynValue[args.Count + 1];
			for (int i = 0; i < args.Count; i++)
			{
				array[i + 1] = args[i];
			}
			array[0] = DynValue.NewBoolean(retstatus);
			return DynValue.NewTuple(array);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00026C6B File Offset: 0x00024E6B
		public static DynValue pcall_continuation(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(true, args);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00026C74 File Offset: 0x00024E74
		public static DynValue pcall_onerror(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(false, args);
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x0012794C File Offset: 0x00125B4C
		[MoonSharpModuleMethod]
		public static DynValue xpcall(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			List<DynValue> list = new List<DynValue>();
			for (int i = 0; i < args.Count; i++)
			{
				if (i != 1)
				{
					list.Add(args[i]);
				}
			}
			DynValue handlerBeforeUnwind = null;
			if (args[1].Type == DataType.Function || args[1].Type == DataType.ClrFunction)
			{
				handlerBeforeUnwind = args[1];
			}
			else if (args[1].Type != DataType.Nil)
			{
				args.AsType(1, "xpcall", DataType.Function, false);
			}
			return ErrorHandlingModule.SetErrorHandlerStrategy("xpcall", executionContext, new CallbackArguments(list, false), handlerBeforeUnwind);
		}
	}
}

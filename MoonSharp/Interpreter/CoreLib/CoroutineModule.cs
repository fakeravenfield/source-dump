using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008D8 RID: 2264
	[MoonSharpModule(Namespace = "coroutine")]
	public class CoroutineModule
	{
		// Token: 0x06003953 RID: 14675 RVA: 0x00126D60 File Offset: 0x00124F60
		[MoonSharpModuleMethod]
		public static DynValue create(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.Function && args[0].Type != DataType.ClrFunction)
			{
				args.AsType(0, "create", DataType.Function, false);
			}
			return executionContext.GetScript().CreateCoroutine(args[0]);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x00126DB0 File Offset: 0x00124FB0
		[MoonSharpModuleMethod]
		public static DynValue wrap(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.Function && args[0].Type != DataType.ClrFunction)
			{
				args.AsType(0, "wrap", DataType.Function, false);
			}
			DynValue additionalData = CoroutineModule.create(executionContext, args);
			DynValue dynValue = DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(CoroutineModule.__wrap_wrapper), null);
			dynValue.Callback.AdditionalData = additionalData;
			return dynValue;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x00026C14 File Offset: 0x00024E14
		public static DynValue __wrap_wrapper(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ((DynValue)executionContext.AdditionalData).Coroutine.Resume(args.GetArray(0));
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x00126E14 File Offset: 0x00125014
		[MoonSharpModuleMethod]
		public static DynValue resume(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "resume", DataType.Thread, false);
			DynValue result;
			try
			{
				DynValue dynValue2 = dynValue.Coroutine.Resume(args.GetArray(1));
				List<DynValue> list = new List<DynValue>();
				list.Add(DynValue.True);
				if (dynValue2.Type == DataType.Tuple)
				{
					for (int i = 0; i < dynValue2.Tuple.Length; i++)
					{
						DynValue dynValue3 = dynValue2.Tuple[i];
						if (i == dynValue2.Tuple.Length - 1 && dynValue3.Type == DataType.Tuple)
						{
							list.AddRange(dynValue3.Tuple);
						}
						else
						{
							list.Add(dynValue3);
						}
					}
				}
				else
				{
					list.Add(dynValue2);
				}
				result = DynValue.NewTuple(list.ToArray());
			}
			catch (ScriptRuntimeException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.False,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x00026C32 File Offset: 0x00024E32
		[MoonSharpModuleMethod]
		public static DynValue yield(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewYieldReq(args.GetArray(0));
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00126EFC File Offset: 0x001250FC
		[MoonSharpModuleMethod]
		public static DynValue running(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Coroutine callingCoroutine = executionContext.GetCallingCoroutine();
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewCoroutine(callingCoroutine),
				DynValue.NewBoolean(callingCoroutine.State == CoroutineState.Main)
			});
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x00126F38 File Offset: 0x00125138
		[MoonSharpModuleMethod]
		public static DynValue status(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "status", DataType.Thread, false);
			Coroutine callingCoroutine = executionContext.GetCallingCoroutine();
			CoroutineState state = dynValue.Coroutine.State;
			switch (state)
			{
			case CoroutineState.Main:
			case CoroutineState.Running:
				if (dynValue.Coroutine != callingCoroutine)
				{
					return DynValue.NewString("normal");
				}
				return DynValue.NewString("running");
			case CoroutineState.NotStarted:
			case CoroutineState.Suspended:
			case CoroutineState.ForceSuspended:
				return DynValue.NewString("suspended");
			case CoroutineState.Dead:
				return DynValue.NewString("dead");
			default:
				throw new InternalErrorException("Unexpected coroutine state {0}", new object[]
				{
					state
				});
			}
		}
	}
}

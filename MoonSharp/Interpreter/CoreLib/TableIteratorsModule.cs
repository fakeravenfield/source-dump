using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E8 RID: 2280
	[MoonSharpModule]
	public class TableIteratorsModule
	{
		// Token: 0x06003A0E RID: 14862 RVA: 0x0012976C File Offset: 0x0012796C
		[MoonSharpModuleMethod]
		public static DynValue ipairs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue result;
			if ((result = executionContext.GetMetamethodTailCall(dynValue, "__ipairs", args.GetArray(0))) == null)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(TableIteratorsModule.__next_i), null),
					dynValue,
					DynValue.NewNumber(0.0)
				});
			}
			return result;
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x001297D0 File Offset: 0x001279D0
		[MoonSharpModuleMethod]
		public static DynValue pairs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue result;
			if ((result = executionContext.GetMetamethodTailCall(dynValue, "__pairs", args.GetArray(0))) == null)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(TableIteratorsModule.next), null),
					dynValue
				});
			}
			return result;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00129820 File Offset: 0x00127A20
		[MoonSharpModuleMethod]
		public static DynValue next(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "next", DataType.Table, false);
			DynValue v = args[1];
			TablePair? tablePair = dynValue.Table.NextKey(v);
			if (tablePair != null)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					tablePair.Value.Key,
					tablePair.Value.Value
				});
			}
			throw new ScriptRuntimeException("invalid key to 'next'");
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x00129894 File Offset: 0x00127A94
		public static DynValue __next_i(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "!!next_i!!", DataType.Table, false);
			int num = (int)args.AsType(1, "!!next_i!!", DataType.Number, false).Number + 1;
			DynValue dynValue2 = dynValue.Table.Get(num);
			if (dynValue2.Type != DataType.Nil)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewNumber((double)num),
					dynValue2
				});
			}
			return DynValue.Nil;
		}
	}
}

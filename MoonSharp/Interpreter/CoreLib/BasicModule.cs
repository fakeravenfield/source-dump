using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008D5 RID: 2261
	[MoonSharpModule]
	public class BasicModule
	{
		// Token: 0x06003930 RID: 14640 RVA: 0x00026A55 File Offset: 0x00024C55
		[MoonSharpModuleMethod]
		public static DynValue type(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args.Count < 1)
			{
				throw ScriptRuntimeException.BadArgumentValueExpected(0, "type");
			}
			return DynValue.NewString(args[0].Type.ToLuaTypeString());
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x0012657C File Offset: 0x0012477C
		[MoonSharpModuleMethod]
		public static DynValue assert(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue dynValue2 = args[1];
			if (dynValue.CastToBool())
			{
				return DynValue.NewTupleNested(args.GetArray(0));
			}
			if (dynValue2.IsNil())
			{
				throw new ScriptRuntimeException("assertion failed!");
			}
			throw new ScriptRuntimeException(dynValue2.ToPrintString());
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x001265CC File Offset: 0x001247CC
		[MoonSharpModuleMethod]
		public static DynValue collectgarbage(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string text = args[0].CastToString();
			if (text == null || text == "collect" || text == "restart")
			{
				GC.Collect(2, GCCollectionMode.Forced);
			}
			return DynValue.Nil;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00026A82 File Offset: 0x00024C82
		[MoonSharpModuleMethod]
		public static DynValue error(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			throw new ScriptRuntimeException(args.AsType(0, "error", DataType.String, false).String);
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00126610 File Offset: 0x00124810
		[MoonSharpModuleMethod]
		public static DynValue tostring(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args.Count < 1)
			{
				throw ScriptRuntimeException.BadArgumentValueExpected(0, "tostring");
			}
			DynValue dynValue = args[0];
			DynValue metamethodTailCall = executionContext.GetMetamethodTailCall(dynValue, "__tostring", new DynValue[]
			{
				dynValue
			});
			if (metamethodTailCall == null || metamethodTailCall.IsNil())
			{
				return DynValue.NewString(dynValue.ToPrintString());
			}
			metamethodTailCall.TailCallData.Continuation = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(BasicModule.__tostring_continuation), "__tostring");
			return metamethodTailCall;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x0012668C File Offset: 0x0012488C
		private static DynValue __tostring_continuation(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0].ToScalar();
			if (dynValue.IsNil())
			{
				return dynValue;
			}
			if (dynValue.Type != DataType.String)
			{
				throw new ScriptRuntimeException("'tostring' must return a string");
			}
			return dynValue;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x001266C8 File Offset: 0x001248C8
		[MoonSharpModuleMethod]
		public static DynValue select(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.String || !(args[0].String == "#"))
			{
				int num = (int)args.AsType(0, "select", DataType.Number, false).Number;
				List<DynValue> list = new List<DynValue>();
				if (num > 0)
				{
					for (int i = num; i < args.Count; i++)
					{
						list.Add(args[i]);
					}
				}
				else
				{
					if (num >= 0)
					{
						throw ScriptRuntimeException.BadArgumentIndexOutOfRange("select", 0);
					}
					num = args.Count + num;
					if (num < 1)
					{
						throw ScriptRuntimeException.BadArgumentIndexOutOfRange("select", 0);
					}
					for (int j = num; j < args.Count; j++)
					{
						list.Add(args[j]);
					}
				}
				return DynValue.NewTupleNested(list.ToArray());
			}
			if (args[args.Count - 1].Type == DataType.Tuple)
			{
				return DynValue.NewNumber((double)(args.Count - 1 + args[args.Count - 1].Tuple.Length));
			}
			return DynValue.NewNumber((double)(args.Count - 1));
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x001267DC File Offset: 0x001249DC
		[MoonSharpModuleMethod]
		public static DynValue tonumber(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args.Count < 1)
			{
				throw ScriptRuntimeException.BadArgumentValueExpected(0, "tonumber");
			}
			DynValue dynValue = args[0];
			DynValue dynValue2 = args.AsType(1, "tonumber", DataType.Number, true);
			if (!dynValue2.IsNil())
			{
				DynValue dynValue3;
				if (args[0].Type != DataType.Number)
				{
					dynValue3 = args.AsType(0, "tonumber", DataType.String, false);
				}
				else
				{
					dynValue3 = DynValue.NewString(args[0].Number.ToString(CultureInfo.InvariantCulture));
				}
				int num = (int)dynValue2.Number;
				uint num2 = 0U;
				if (num == 2 || num == 8 || num == 10 || num == 16)
				{
					num2 = Convert.ToUInt32(dynValue3.String.Trim(), num);
				}
				else
				{
					if (num >= 10 || num <= 2)
					{
						throw new ScriptRuntimeException("bad argument #2 to 'tonumber' (base out of range)");
					}
					string text = dynValue3.String.Trim();
					for (int i = 0; i < text.Length; i++)
					{
						int num3 = (int)(text[i] - '0');
						if (num3 < 0 || num3 >= num)
						{
							throw new ScriptRuntimeException("bad argument #1 to 'tonumber' (invalid character)");
						}
						num2 = (uint)((ulong)num2 * (ulong)((long)num)) + (uint)num3;
					}
				}
				return DynValue.NewNumber(num2);
			}
			if (dynValue.Type == DataType.Number)
			{
				return dynValue;
			}
			if (dynValue.Type != DataType.String)
			{
				return DynValue.Nil;
			}
			double num4;
			if (double.TryParse(dynValue.String, NumberStyles.Any, CultureInfo.InvariantCulture, out num4))
			{
				return DynValue.NewNumber(num4);
			}
			return DynValue.Nil;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x0012694C File Offset: 0x00124B4C
		[MoonSharpModuleMethod]
		public static DynValue print(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < args.Count; i++)
			{
				DynValue dynValue = args[i];
				if (args[i].IsVoid())
				{
					break;
				}
				if (i != 0)
				{
					stringBuilder.Append('\t');
				}
				stringBuilder.Append(args.AsStringUsingMeta(executionContext, i, "print"));
			}
			executionContext.GetScript().Options.DebugPrint(stringBuilder.ToString());
			return DynValue.Nil;
		}
	}
}

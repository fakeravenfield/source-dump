using System;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008E1 RID: 2273
	[MoonSharpModule(Namespace = "math")]
	public class MathModule
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x00026E03 File Offset: 0x00025003
		private static Random GetRandom(Script s)
		{
			return (s.Registry.Get("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM").UserData.Object as AnonWrapper<Random>).Value;
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x00128324 File Offset: 0x00126524
		private static void SetRandom(Script s, Random random)
		{
			DynValue value = UserData.Create(new AnonWrapper<Random>(random));
			s.Registry.Set("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM", value);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x00026E29 File Offset: 0x00025029
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			MathModule.SetRandom(globalTable.OwnerScript, new Random());
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x00128350 File Offset: 0x00126550
		private static DynValue exec1(CallbackArguments args, string funcName, Func<double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number));
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x0012837C File Offset: 0x0012657C
		private static DynValue exec2(CallbackArguments args, string funcName, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.Number));
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x001283B8 File Offset: 0x001265B8
		private static DynValue exec2n(CallbackArguments args, string funcName, double defVal, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, true);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.IsNil() ? defVal : dynValue2.Number));
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x00128400 File Offset: 0x00126600
		private static DynValue execaccum(CallbackArguments args, string funcName, Func<double, double, double> func)
		{
			double num = double.NaN;
			if (args.Count == 0)
			{
				throw new ScriptRuntimeException("bad argument #1 to '{0}' (number expected, got no value)", new object[]
				{
					funcName
				});
			}
			for (int i = 0; i < args.Count; i++)
			{
				DynValue dynValue = args.AsType(i, funcName, DataType.Number, false);
				if (i == 0)
				{
					num = dynValue.Number;
				}
				else
				{
					num = func(num, dynValue.Number);
				}
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x00026E3B File Offset: 0x0002503B
		[MoonSharpModuleMethod]
		public static DynValue abs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "abs", (double d) => Math.Abs(d));
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x00026E67 File Offset: 0x00025067
		[MoonSharpModuleMethod]
		public static DynValue acos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "acos", (double d) => Math.Acos(d));
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x00026E93 File Offset: 0x00025093
		[MoonSharpModuleMethod]
		public static DynValue asin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "asin", (double d) => Math.Asin(d));
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x00026EBF File Offset: 0x000250BF
		[MoonSharpModuleMethod]
		public static DynValue atan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "atan", (double d) => Math.Atan(d));
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x00026EEB File Offset: 0x000250EB
		[MoonSharpModuleMethod]
		public static DynValue atan2(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "atan2", (double d1, double d2) => Math.Atan2(d1, d2));
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x00026F17 File Offset: 0x00025117
		[MoonSharpModuleMethod]
		public static DynValue ceil(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "ceil", (double d) => Math.Ceiling(d));
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x00026F43 File Offset: 0x00025143
		[MoonSharpModuleMethod]
		public static DynValue cos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cos", (double d) => Math.Cos(d));
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x00026F6F File Offset: 0x0002516F
		[MoonSharpModuleMethod]
		public static DynValue cosh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cosh", (double d) => Math.Cosh(d));
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x00026F9B File Offset: 0x0002519B
		[MoonSharpModuleMethod]
		public static DynValue deg(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "deg", (double d) => d * 180.0 / 3.141592653589793);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x00026FC7 File Offset: 0x000251C7
		[MoonSharpModuleMethod]
		public static DynValue exp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "exp", (double d) => Math.Exp(d));
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x00026FF3 File Offset: 0x000251F3
		[MoonSharpModuleMethod]
		public static DynValue floor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "floor", (double d) => Math.Floor(d));
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0002701F File Offset: 0x0002521F
		[MoonSharpModuleMethod]
		public static DynValue fmod(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "fmod", (double d1, double d2) => Math.IEEERemainder(d1, d2));
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x00128470 File Offset: 0x00126670
		[MoonSharpModuleMethod]
		public static DynValue frexp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			long num = BitConverter.DoubleToInt64Bits(args.AsType(0, "frexp", DataType.Number, false).Number);
			bool flag = num < 0L;
			int num2 = (int)(num >> 52 & 2047L);
			long num3 = num & 4503599627370495L;
			if (num2 == 0)
			{
				num2++;
			}
			else
			{
				num3 |= 4503599627370496L;
			}
			num2 -= 1075;
			if (num3 == 0L)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewNumber(0.0),
					DynValue.NewNumber(0.0)
				});
			}
			while ((num3 & 1L) == 0L)
			{
				num3 >>= 1;
				num2++;
			}
			double num4 = (double)num3;
			double num5 = (double)num2;
			while (num4 >= 1.0)
			{
				num4 /= 2.0;
				num5 += 1.0;
			}
			if (flag)
			{
				num4 = -num4;
			}
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewNumber(num4),
				DynValue.NewNumber(num5)
			});
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0002704B File Offset: 0x0002524B
		[MoonSharpModuleMethod]
		public static DynValue ldexp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "ldexp", (double d1, double d2) => d1 * Math.Pow(2.0, d2));
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x00027077 File Offset: 0x00025277
		[MoonSharpModuleMethod]
		public static DynValue log(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2n(args, "log", 2.718281828459045, (double d1, double d2) => Math.Log(d1, d2));
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000270AC File Offset: 0x000252AC
		[MoonSharpModuleMethod]
		public static DynValue max(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "max", (double d1, double d2) => Math.Max(d1, d2));
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000270D8 File Offset: 0x000252D8
		[MoonSharpModuleMethod]
		public static DynValue min(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "min", (double d1, double d2) => Math.Min(d1, d2));
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x00128564 File Offset: 0x00126764
		[MoonSharpModuleMethod]
		public static DynValue modf(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "modf", DataType.Number, false);
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewNumber(Math.Floor(dynValue.Number)),
				DynValue.NewNumber(dynValue.Number - Math.Floor(dynValue.Number))
			});
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00027104 File Offset: 0x00025304
		[MoonSharpModuleMethod]
		public static DynValue pow(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "pow", (double d1, double d2) => Math.Pow(d1, d2));
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x00027130 File Offset: 0x00025330
		[MoonSharpModuleMethod]
		public static DynValue rad(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "rad", (double d) => d * 3.141592653589793 / 180.0);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x001285B8 File Offset: 0x001267B8
		[MoonSharpModuleMethod]
		public static DynValue random(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "random", DataType.Number, true);
			DynValue dynValue2 = args.AsType(1, "random", DataType.Number, true);
			Random random = MathModule.GetRandom(executionContext.GetScript());
			double num;
			if (dynValue.IsNil() && dynValue2.IsNil())
			{
				num = random.NextDouble();
			}
			else
			{
				int num2 = dynValue2.IsNil() ? 1 : ((int)dynValue2.Number);
				int num3 = (int)dynValue.Number;
				if (num2 < num3)
				{
					num = (double)random.Next(num2, num3 + 1);
				}
				else
				{
					num = (double)random.Next(num3, num2 + 1);
				}
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x00128650 File Offset: 0x00126850
		[MoonSharpModuleMethod]
		public static DynValue randomseed(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "randomseed", DataType.Number, false);
			MathModule.SetRandom(executionContext.GetScript(), new Random((int)dynValue.Number));
			return DynValue.Nil;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0002715C File Offset: 0x0002535C
		[MoonSharpModuleMethod]
		public static DynValue sin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sin", (double d) => Math.Sin(d));
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x00027188 File Offset: 0x00025388
		[MoonSharpModuleMethod]
		public static DynValue sinh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sinh", (double d) => Math.Sinh(d));
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000271B4 File Offset: 0x000253B4
		[MoonSharpModuleMethod]
		public static DynValue sqrt(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sqrt", (double d) => Math.Sqrt(d));
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000271E0 File Offset: 0x000253E0
		[MoonSharpModuleMethod]
		public static DynValue tan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tan", (double d) => Math.Tan(d));
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0002720C File Offset: 0x0002540C
		[MoonSharpModuleMethod]
		public static DynValue tanh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tanh", (double d) => Math.Tanh(d));
		}

		// Token: 0x04002FF7 RID: 12279
		[MoonSharpModuleConstant]
		public const double pi = 3.141592653589793;

		// Token: 0x04002FF8 RID: 12280
		[MoonSharpModuleConstant]
		public const double huge = 1.7976931348623157E+308;
	}
}

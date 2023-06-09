﻿using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020008D6 RID: 2262
	[MoonSharpModule(Namespace = "bit32")]
	public class Bit32Module
	{
		// Token: 0x0600393A RID: 14650 RVA: 0x00026A9C File Offset: 0x00024C9C
		private static uint ToUInt32(DynValue v)
		{
			return (uint)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x00026AC1 File Offset: 0x00024CC1
		private static int ToInt32(DynValue v)
		{
			return (int)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x00026AE6 File Offset: 0x00024CE6
		private static uint NBitMask(int bits)
		{
			if (bits <= 0)
			{
				return 0U;
			}
			if (bits >= 32)
			{
				return Bit32Module.MASKS[31];
			}
			return Bit32Module.MASKS[bits - 1];
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x001269C8 File Offset: 0x00124BC8
		public static uint Bitwise(string funcName, CallbackArguments args, Func<uint, uint, uint> accumFunc)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, funcName, DataType.Number, false));
			for (int i = 1; i < args.Count; i++)
			{
				uint arg = Bit32Module.ToUInt32(args.AsType(i, funcName, DataType.Number, false));
				num = accumFunc(num, arg);
			}
			return num;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x00126A10 File Offset: 0x00124C10
		[MoonSharpModuleMethod]
		public static DynValue extract(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "extract", DataType.Number, false));
			DynValue dynValue = args.AsType(1, "extract", DataType.Number, false);
			DynValue dynValue2 = args.AsType(2, "extract", DataType.Number, true);
			int num2 = (int)dynValue.Number;
			int num3 = dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number);
			Bit32Module.ValidatePosWidth("extract", 2, num2, num3);
			return DynValue.NewNumber(num >> (num2 & 31) & Bit32Module.NBitMask(num3));
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x00126A88 File Offset: 0x00124C88
		[MoonSharpModuleMethod]
		public static DynValue replace(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "replace", DataType.Number, false));
			uint num2 = Bit32Module.ToUInt32(args.AsType(1, "replace", DataType.Number, false));
			DynValue dynValue = args.AsType(2, "replace", DataType.Number, false);
			DynValue dynValue2 = args.AsType(3, "replace", DataType.Number, true);
			int num3 = (int)dynValue.Number;
			int num4 = dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number);
			Bit32Module.ValidatePosWidth("replace", 3, num3, num4);
			uint num5 = Bit32Module.NBitMask(num4) << num3;
			uint num6 = num & ~num5;
			num2 &= num5;
			return DynValue.NewNumber(num6 | num2);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x00126B20 File Offset: 0x00124D20
		private static void ValidatePosWidth(string func, int argPos, int pos, int width)
		{
			if (pos > 31 || pos + width > 31)
			{
				throw new ScriptRuntimeException("trying to access non-existent bits");
			}
			if (pos < 0)
			{
				throw new ScriptRuntimeException("bad argument #{1} to '{0}' (field cannot be negative)", new object[]
				{
					func,
					argPos
				});
			}
			if (width <= 0)
			{
				throw new ScriptRuntimeException("bad argument #{1} to '{0}' (width must be positive)", new object[]
				{
					func,
					argPos + 1
				});
			}
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00126B8C File Offset: 0x00124D8C
		[MoonSharpModuleMethod]
		public static DynValue arshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			int num = Bit32Module.ToInt32(args.AsType(0, "arshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "arshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num <<= -num2;
			}
			else
			{
				num >>= num2;
			}
			return DynValue.NewNumber((double)num);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x00126BE0 File Offset: 0x00124DE0
		[MoonSharpModuleMethod]
		public static DynValue rshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "rshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "rshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num <<= -num2;
			}
			else
			{
				num >>= num2;
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x00126C34 File Offset: 0x00124E34
		[MoonSharpModuleMethod]
		public static DynValue lshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "lshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "lshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num >>= -num2;
			}
			else
			{
				num <<= num2;
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x00026B05 File Offset: 0x00024D05
		[MoonSharpModuleMethod]
		public static DynValue band(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("band", args, (uint x, uint y) => x & y));
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x00026B38 File Offset: 0x00024D38
		[MoonSharpModuleMethod]
		public static DynValue btest(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewBoolean(Bit32Module.Bitwise("btest", args, (uint x, uint y) => x & y) > 0U);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x00026B6C File Offset: 0x00024D6C
		[MoonSharpModuleMethod]
		public static DynValue bor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bor", args, (uint x, uint y) => x | y));
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00026B9F File Offset: 0x00024D9F
		[MoonSharpModuleMethod]
		public static DynValue bnot(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(~Bit32Module.ToUInt32(args.AsType(0, "bnot", DataType.Number, false)));
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x00026BBC File Offset: 0x00024DBC
		[MoonSharpModuleMethod]
		public static DynValue bxor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bxor", args, (uint x, uint y) => x ^ y));
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x00126C88 File Offset: 0x00124E88
		[MoonSharpModuleMethod]
		public static DynValue lrotate(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "lrotate", DataType.Number, false));
			int num2 = (int)args.AsType(1, "lrotate", DataType.Number, false).Number % 32;
			if (num2 < 0)
			{
				num = (num >> -num2 | num << 32 + num2);
			}
			else
			{
				num = (num << num2 | num >> 32 - num2);
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00126CF4 File Offset: 0x00124EF4
		[MoonSharpModuleMethod]
		public static DynValue rrotate(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "rrotate", DataType.Number, false));
			int num2 = (int)args.AsType(1, "rrotate", DataType.Number, false).Number % 32;
			if (num2 < 0)
			{
				num = (num << -num2 | num >> 32 + num2);
			}
			else
			{
				num = (num >> num2 | num << 32 - num2);
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x04002FED RID: 12269
		private static readonly uint[] MASKS = new uint[]
		{
			1U,
			3U,
			7U,
			15U,
			31U,
			63U,
			127U,
			255U,
			511U,
			1023U,
			2047U,
			4095U,
			8191U,
			16383U,
			32767U,
			65535U,
			131071U,
			262143U,
			524287U,
			1048575U,
			2097151U,
			4194303U,
			8388607U,
			16777215U,
			33554431U,
			67108863U,
			134217727U,
			268435455U,
			536870911U,
			1073741823U,
			2147483647U,
			uint.MaxValue
		};
	}
}

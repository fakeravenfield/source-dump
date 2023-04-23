using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x0200088A RID: 2186
	internal static class NumericConversions
	{
		// Token: 0x060036C1 RID: 14017 RVA: 0x0011EB68 File Offset: 0x0011CD68
		static NumericConversions()
		{
			NumericConversions.NumericTypes = new HashSet<Type>(NumericConversions.NumericTypesOrdered);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x0011EC24 File Offset: 0x0011CE24
		internal static object DoubleToType(Type type, double d)
		{
			type = (Nullable.GetUnderlyingType(type) ?? type);
			if (type == typeof(double))
			{
				return d;
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)Math.IEEERemainder(d, Math.Pow(2.0, 8.0));
			}
			if (type == typeof(byte))
			{
				return (byte)Math.IEEERemainder(d, Math.Pow(2.0, 8.0));
			}
			if (type == typeof(short))
			{
				return (short)Math.IEEERemainder(d, Math.Pow(2.0, 16.0));
			}
			if (type == typeof(ushort))
			{
				return (ushort)Math.IEEERemainder(d, Math.Pow(2.0, 16.0));
			}
			if (type == typeof(int))
			{
				return (int)Math.IEEERemainder(d, Math.Pow(2.0, 32.0));
			}
			if (type == typeof(uint))
			{
				return (uint)Math.IEEERemainder(d, Math.Pow(2.0, 32.0));
			}
			if (type == typeof(long))
			{
				return (long)Math.IEEERemainder(d, Math.Pow(2.0, 64.0));
			}
			if (type == typeof(ulong))
			{
				return (ulong)Math.IEEERemainder(d, Math.Pow(2.0, 64.0));
			}
			if (type == typeof(float))
			{
				return (float)d;
			}
			if (type == typeof(decimal))
			{
				return (decimal)d;
			}
			return d;
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x0011EE48 File Offset: 0x0011D048
		internal static double TypeToDouble(Type type, object d)
		{
			if (type == typeof(double))
			{
				return (double)d;
			}
			if (type == typeof(sbyte))
			{
				return (double)((sbyte)d);
			}
			if (type == typeof(byte))
			{
				return (double)((byte)d);
			}
			if (type == typeof(short))
			{
				return (double)((short)d);
			}
			if (type == typeof(ushort))
			{
				return (double)((ushort)d);
			}
			if (type == typeof(int))
			{
				return (double)((int)d);
			}
			if (type == typeof(uint))
			{
				return (uint)d;
			}
			if (type == typeof(long))
			{
				return (double)((long)d);
			}
			if (type == typeof(ulong))
			{
				return (ulong)d;
			}
			if (type == typeof(float))
			{
				return (double)((float)d);
			}
			if (type == typeof(decimal))
			{
				return (double)((decimal)d);
			}
			return (double)d;
		}

		// Token: 0x04002E94 RID: 11924
		internal static readonly HashSet<Type> NumericTypes;

		// Token: 0x04002E95 RID: 11925
		internal static readonly Type[] NumericTypesOrdered = new Type[]
		{
			typeof(double),
			typeof(decimal),
			typeof(float),
			typeof(long),
			typeof(int),
			typeof(short),
			typeof(sbyte),
			typeof(ulong),
			typeof(uint),
			typeof(ushort),
			typeof(byte)
		};
	}
}

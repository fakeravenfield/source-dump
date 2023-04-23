using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007D1 RID: 2001
	public static class LinqHelpers
	{
		// Token: 0x060031F0 RID: 12784 RVA: 0x0010F9E0 File Offset: 0x0010DBE0
		public static IEnumerable<T> Convert<T>(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v.ToObject<T>();
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0010FA30 File Offset: 0x0010DC30
		public static IEnumerable<DynValue> OfDataType(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00022963 File Offset: 0x00020B63
		public static IEnumerable<object> AsObjects(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject();
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x0002298A File Offset: 0x00020B8A
		public static IEnumerable<T> AsObjects<T>(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject<T>();
		}
	}
}

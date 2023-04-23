using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008D1 RID: 2257
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x0600390D RID: 14605 RVA: 0x00026931 File Offset: 0x00024B31
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x00026937 File Offset: 0x00024B37
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}
}

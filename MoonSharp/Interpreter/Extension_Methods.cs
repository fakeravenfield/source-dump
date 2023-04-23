using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007A3 RID: 1955
	internal static class Extension_Methods
	{
		// Token: 0x06003033 RID: 12339 RVA: 0x0010D88C File Offset: 0x0010BA8C
		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return default(TValue);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0010D8B0 File Offset: 0x0010BAB0
		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> creator)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = creator();
				dictionary.Add(key, tvalue);
			}
			return tvalue;
		}
	}
}

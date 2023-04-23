using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008D0 RID: 2256
	internal class MultiDictionary<K, V>
	{
		// Token: 0x06003904 RID: 14596 RVA: 0x000268BB File Offset: 0x00024ABB
		public MultiDictionary()
		{
			this.m_Map = new Dictionary<K, List<V>>();
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000268DA File Offset: 0x00024ADA
		public MultiDictionary(IEqualityComparer<K> eqComparer)
		{
			this.m_Map = new Dictionary<K, List<V>>(eqComparer);
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x001262D8 File Offset: 0x001244D8
		public bool Add(K key, V value)
		{
			List<V> list;
			if (this.m_Map.TryGetValue(key, out list))
			{
				list.Add(value);
				return false;
			}
			list = new List<V>();
			list.Add(value);
			this.m_Map.Add(key, list);
			return true;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x0012631C File Offset: 0x0012451C
		public IEnumerable<V> Find(K key)
		{
			List<V> result;
			if (this.m_Map.TryGetValue(key, out result))
			{
				return result;
			}
			return this.m_DefaultRet;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000268FA File Offset: 0x00024AFA
		public bool ContainsKey(K key)
		{
			return this.m_Map.ContainsKey(key);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x00026908 File Offset: 0x00024B08
		public IEnumerable<K> Keys
		{
			get
			{
				return this.m_Map.Keys;
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x00026915 File Offset: 0x00024B15
		public void Clear()
		{
			this.m_Map.Clear();
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00026922 File Offset: 0x00024B22
		public void Remove(K key)
		{
			this.m_Map.Remove(key);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x00126344 File Offset: 0x00124544
		public bool RemoveValue(K key, V value)
		{
			List<V> list;
			if (this.m_Map.TryGetValue(key, out list))
			{
				list.Remove(value);
				if (list.Count == 0)
				{
					this.Remove(key);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002FDF RID: 12255
		private Dictionary<K, List<V>> m_Map;

		// Token: 0x04002FE0 RID: 12256
		private V[] m_DefaultRet = new V[0];
	}
}

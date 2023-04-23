using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008CF RID: 2255
	internal class LinkedListIndex<TKey, TValue>
	{
		// Token: 0x060038FD RID: 14589 RVA: 0x0002687F File Offset: 0x00024A7F
		public LinkedListIndex(LinkedList<TValue> linkedList)
		{
			this.m_LinkedList = linkedList;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00126200 File Offset: 0x00124400
		public LinkedListNode<TValue> Find(TKey key)
		{
			if (this.m_Map == null)
			{
				return null;
			}
			LinkedListNode<TValue> result;
			if (this.m_Map.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x0012622C File Offset: 0x0012442C
		public TValue Set(TKey key, TValue value)
		{
			LinkedListNode<TValue> linkedListNode = this.Find(key);
			if (linkedListNode == null)
			{
				this.Add(key, value);
				return default(TValue);
			}
			TValue value2 = linkedListNode.Value;
			linkedListNode.Value = value;
			return value2;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00126268 File Offset: 0x00124468
		public void Add(TKey key, TValue value)
		{
			LinkedListNode<TValue> value2 = this.m_LinkedList.AddLast(value);
			if (this.m_Map == null)
			{
				this.m_Map = new Dictionary<TKey, LinkedListNode<TValue>>();
			}
			this.m_Map.Add(key, value2);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001262A4 File Offset: 0x001244A4
		public bool Remove(TKey key)
		{
			LinkedListNode<TValue> linkedListNode = this.Find(key);
			if (linkedListNode != null)
			{
				this.m_LinkedList.Remove(linkedListNode);
				return this.m_Map.Remove(key);
			}
			return false;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x0002688E File Offset: 0x00024A8E
		public bool ContainsKey(TKey key)
		{
			return this.m_Map != null && this.m_Map.ContainsKey(key);
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000268A6 File Offset: 0x00024AA6
		public void Clear()
		{
			if (this.m_Map != null)
			{
				this.m_Map.Clear();
			}
		}

		// Token: 0x04002FDD RID: 12253
		private LinkedList<TValue> m_LinkedList;

		// Token: 0x04002FDE RID: 12254
		private Dictionary<TKey, LinkedListNode<TValue>> m_Map;
	}
}

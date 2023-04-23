using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008CD RID: 2253
	internal class FastStack<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060038D8 RID: 14552 RVA: 0x00026710 File Offset: 0x00024910
		public FastStack(int maxCapacity)
		{
			this.m_Storage = new T[maxCapacity];
		}

		// Token: 0x17000501 RID: 1281
		public T this[int index]
		{
			get
			{
				return this.m_Storage[index];
			}
			set
			{
				this.m_Storage[index] = value;
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x001260C4 File Offset: 0x001242C4
		public T Push(T item)
		{
			T[] storage = this.m_Storage;
			int headIdx = this.m_HeadIdx;
			this.m_HeadIdx = headIdx + 1;
			storage[headIdx] = item;
			return item;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x00026741 File Offset: 0x00024941
		public void Expand(int size)
		{
			this.m_HeadIdx += size;
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x00026751 File Offset: 0x00024951
		private void Zero(int from, int to)
		{
			Array.Clear(this.m_Storage, from, to - from + 1);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x001260F0 File Offset: 0x001242F0
		private void Zero(int index)
		{
			this.m_Storage[index] = default(T);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x00026764 File Offset: 0x00024964
		public T Peek(int idxofs = 0)
		{
			return this.m_Storage[this.m_HeadIdx - 1 - idxofs];
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0002677B File Offset: 0x0002497B
		public void Set(int idxofs, T item)
		{
			this.m_Storage[this.m_HeadIdx - 1 - idxofs] = item;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00026793 File Offset: 0x00024993
		public void CropAtCount(int p)
		{
			this.RemoveLast(this.Count - p);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x00126114 File Offset: 0x00124314
		public void RemoveLast(int cnt = 1)
		{
			if (cnt == 1)
			{
				this.m_HeadIdx--;
				this.m_Storage[this.m_HeadIdx] = default(T);
				return;
			}
			int headIdx = this.m_HeadIdx;
			this.m_HeadIdx -= cnt;
			this.Zero(this.m_HeadIdx, headIdx);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x00126170 File Offset: 0x00124370
		public T Pop()
		{
			this.m_HeadIdx--;
			T result = this.m_Storage[this.m_HeadIdx];
			this.m_Storage[this.m_HeadIdx] = default(T);
			return result;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000267A3 File Offset: 0x000249A3
		public void Clear()
		{
			Array.Clear(this.m_Storage, 0, this.m_Storage.Length);
			this.m_HeadIdx = 0;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000267C0 File Offset: 0x000249C0
		public int Count
		{
			get
			{
				return this.m_HeadIdx;
			}
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00024714 File Offset: 0x00022914
		int IList<!0>.IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x00024714 File Offset: 0x00022914
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00024714 File Offset: 0x00022914
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000503 RID: 1283
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value;
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000267DB File Offset: 0x000249DB
		void ICollection<!0>.Add(T item)
		{
			this.Push(item);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000267E5 File Offset: 0x000249E5
		void ICollection<!0>.Clear()
		{
			this.Clear();
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00024714 File Offset: 0x00022914
		bool ICollection<!0>.Contains(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x00024714 File Offset: 0x00022914
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x000267ED File Offset: 0x000249ED
		int ICollection<!0>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x0000257D File Offset: 0x0000077D
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00024714 File Offset: 0x00022914
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00024714 File Offset: 0x00022914
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x00024714 File Offset: 0x00022914
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002FDB RID: 12251
		private T[] m_Storage;

		// Token: 0x04002FDC RID: 12252
		private int m_HeadIdx;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008D2 RID: 2258
	internal class Slice<T> : IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>
	{
		// Token: 0x06003910 RID: 14608 RVA: 0x0002693F File Offset: 0x00024B3F
		public Slice(IList<T> list, int from, int length, bool reversed)
		{
			this.m_SourceList = list;
			this.m_From = from;
			this.m_Length = length;
			this.m_Reversed = reversed;
		}

		// Token: 0x17000507 RID: 1287
		public T this[int index]
		{
			get
			{
				return this.m_SourceList[this.CalcRealIndex(index)];
			}
			set
			{
				this.m_SourceList[this.CalcRealIndex(index)] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x0002698D File Offset: 0x00024B8D
		public int From
		{
			get
			{
				return this.m_From;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x00026995 File Offset: 0x00024B95
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x0002699D File Offset: 0x00024B9D
		public bool Reversed
		{
			get
			{
				return this.m_Reversed;
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000269A5 File Offset: 0x00024BA5
		private int CalcRealIndex(int index)
		{
			if (index < 0 || index >= this.m_Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.m_Reversed)
			{
				return this.m_From + this.m_Length - index - 1;
			}
			return this.m_From + index;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000269E1 File Offset: 0x00024BE1
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_Length; i = num + 1)
			{
				yield return this.m_SourceList[this.CalcRealIndex(i)];
				num = i;
			}
			yield break;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000269F0 File Offset: 0x00024BF0
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_Length; i = num + 1)
			{
				yield return this.m_SourceList[this.CalcRealIndex(i)];
				num = i;
			}
			yield break;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x0012637C File Offset: 0x0012457C
		public T[] ToArray()
		{
			T[] array = new T[this.m_Length];
			for (int i = 0; i < this.m_Length; i++)
			{
				array[i] = this.m_SourceList[this.CalcRealIndex(i)];
			}
			return array;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x001263C0 File Offset: 0x001245C0
		public List<T> ToList()
		{
			List<T> list = new List<T>(this.m_Length);
			for (int i = 0; i < this.m_Length; i++)
			{
				list.Add(this.m_SourceList[this.CalcRealIndex(i)]);
			}
			return list;
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x00126404 File Offset: 0x00124604
		public int IndexOf(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				T t = this[i];
				if (t.Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000269FF File Offset: 0x00024BFF
		public void Insert(int index, T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000269FF File Offset: 0x00024BFF
		public void RemoveAt(int index)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000269FF File Offset: 0x00024BFF
		public void Add(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000269FF File Offset: 0x00024BFF
		public void Clear()
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x00026A0B File Offset: 0x00024C0B
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x00126444 File Offset: 0x00124644
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this[i];
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x0000476F File Offset: 0x0000296F
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000269FF File Offset: 0x00024BFF
		public bool Remove(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x04002FE1 RID: 12257
		private IList<T> m_SourceList;

		// Token: 0x04002FE2 RID: 12258
		private int m_From;

		// Token: 0x04002FE3 RID: 12259
		private int m_Length;

		// Token: 0x04002FE4 RID: 12260
		private bool m_Reversed;
	}
}

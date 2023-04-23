using System;
using System.Collections;
using System.Collections.Generic;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045A RID: 1114
	public class PriorityQueue<TPriority, TValue> : ICollection<KeyValuePair<TPriority, TValue>>, IEnumerable<KeyValuePair<TPriority, TValue>>, IEnumerable
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x0001506E File Offset: 0x0001326E
		public PriorityQueue() : this(Comparer<TPriority>.Default)
		{
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0001507B File Offset: 0x0001327B
		public PriorityQueue(int capacity) : this(capacity, Comparer<TPriority>.Default)
		{
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00015089 File Offset: 0x00013289
		public PriorityQueue(int capacity, IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(capacity);
			this._comparer = comparer;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000150AD File Offset: 0x000132AD
		public PriorityQueue(IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>();
			this._comparer = comparer;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000150D0 File Offset: 0x000132D0
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data) : this(data, Comparer<TPriority>.Default)
		{
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000B849C File Offset: 0x000B669C
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data, IComparer<TPriority> comparer)
		{
			if (data == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._comparer = comparer;
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(data);
			for (int i = this._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				this.HeapifyFromBeginningToEnd(i);
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000150DE File Offset: 0x000132DE
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2)
		{
			if (pq1 == null || pq2 == null)
			{
				throw new ArgumentNullException();
			}
			if (pq1._comparer != pq2._comparer)
			{
				throw new InvalidOperationException("Priority queues to be merged must have equal comparers");
			}
			return PriorityQueue<TPriority, TValue>.MergeQueues(pq1, pq2, pq1._comparer);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000B84F0 File Offset: 0x000B66F0
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2, IComparer<TPriority> comparer)
		{
			if (pq1 == null || pq2 == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			PriorityQueue<TPriority, TValue> priorityQueue = new PriorityQueue<TPriority, TValue>(pq1.Count + pq2.Count, pq1._comparer);
			priorityQueue._baseHeap.AddRange(pq1._baseHeap);
			priorityQueue._baseHeap.AddRange(pq2._baseHeap);
			for (int i = priorityQueue._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				priorityQueue.HeapifyFromBeginningToEnd(i);
			}
			return priorityQueue;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00015112 File Offset: 0x00013312
		public void Enqueue(TPriority priority, TValue value)
		{
			this.Insert(priority, value);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0001511C File Offset: 0x0001331C
		public KeyValuePair<TPriority, TValue> Dequeue()
		{
			if (!this.IsEmpty)
			{
				KeyValuePair<TPriority, TValue> result = this._baseHeap[0];
				this.DeleteRoot();
				return result;
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000B856C File Offset: 0x000B676C
		public TValue DequeueValue()
		{
			return this.Dequeue().Value;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00015143 File Offset: 0x00013343
		public KeyValuePair<TPriority, TValue> Peek()
		{
			if (!this.IsEmpty)
			{
				return this._baseHeap[0];
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000B8588 File Offset: 0x000B6788
		public TValue PeekValue()
		{
			return this.Peek().Value;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00015164 File Offset: 0x00013364
		public bool IsEmpty
		{
			get
			{
				return this._baseHeap.Count == 0;
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000B85A4 File Offset: 0x000B67A4
		private void ExchangeElements(int pos1, int pos2)
		{
			KeyValuePair<TPriority, TValue> value = this._baseHeap[pos1];
			this._baseHeap[pos1] = this._baseHeap[pos2];
			this._baseHeap[pos2] = value;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000B85E4 File Offset: 0x000B67E4
		private void Insert(TPriority priority, TValue value)
		{
			KeyValuePair<TPriority, TValue> item = new KeyValuePair<TPriority, TValue>(priority, value);
			this._baseHeap.Add(item);
			this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000B861C File Offset: 0x000B681C
		private int HeapifyFromEndToBeginning(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return -1;
			}
			while (pos > 0)
			{
				int num = (pos - 1) / 2;
				if (this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[pos].Key) <= 0)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
			return pos;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000B8688 File Offset: 0x000B6888
		private void DeleteRoot()
		{
			if (this._baseHeap.Count <= 1)
			{
				this._baseHeap.Clear();
				return;
			}
			this._baseHeap[0] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			this.HeapifyFromBeginningToEnd(0);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000B86F4 File Offset: 0x000B68F4
		private void HeapifyFromBeginningToEnd(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return;
			}
			for (;;)
			{
				int num = pos;
				int num2 = 2 * pos + 1;
				int num3 = 2 * pos + 2;
				if (num2 < this._baseHeap.Count && this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num2].Key) > 0)
				{
					num = num2;
				}
				if (num3 < this._baseHeap.Count && this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num3].Key) > 0)
				{
					num = num3;
				}
				if (num == pos)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00015174 File Offset: 0x00013374
		public void Add(KeyValuePair<TPriority, TValue> item)
		{
			this.Enqueue(item.Key, item.Value);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0001518A File Offset: 0x0001338A
		public void Clear()
		{
			this._baseHeap.Clear();
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00015197 File Offset: 0x00013397
		public bool Contains(KeyValuePair<TPriority, TValue> item)
		{
			return this._baseHeap.Contains(item);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000B87C0 File Offset: 0x000B69C0
		public bool TryFindValue(TPriority item, out TValue foundVersion)
		{
			for (int i = 0; i < this._baseHeap.Count; i++)
			{
				if (this._comparer.Compare(item, this._baseHeap[i].Key) == 0)
				{
					foundVersion = this._baseHeap[i].Value;
					return true;
				}
			}
			foundVersion = default(TValue);
			return false;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000151A5 File Offset: 0x000133A5
		public int Count
		{
			get
			{
				return this._baseHeap.Count;
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000151B2 File Offset: 0x000133B2
		public void CopyTo(KeyValuePair<TPriority, TValue>[] array, int arrayIndex)
		{
			this._baseHeap.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0000257D File Offset: 0x0000077D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000B882C File Offset: 0x000B6A2C
		public bool Remove(KeyValuePair<TPriority, TValue> item)
		{
			int num = this._baseHeap.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this._baseHeap[num] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			if (this.HeapifyFromEndToBeginning(num) == num)
			{
				this.HeapifyFromBeginningToEnd(num);
			}
			return true;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000151C1 File Offset: 0x000133C1
		public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
		{
			return this._baseHeap.GetEnumerator();
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000151D3 File Offset: 0x000133D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001D26 RID: 7462
		public List<KeyValuePair<TPriority, TValue>> _baseHeap;

		// Token: 0x04001D27 RID: 7463
		private IComparer<TPriority> _comparer;
	}
}

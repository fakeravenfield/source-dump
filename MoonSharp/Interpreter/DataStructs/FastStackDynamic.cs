using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x020008CE RID: 2254
	internal class FastStackDynamic<T> : List<T>
	{
		// Token: 0x060038F4 RID: 14580 RVA: 0x000267F5 File Offset: 0x000249F5
		public FastStackDynamic(int startingCapacity) : base(startingCapacity)
		{
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000267FE File Offset: 0x000249FE
		public void Set(int idxofs, T item)
		{
			base[base.Count - 1 - idxofs] = item;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00026811 File Offset: 0x00024A11
		public T Push(T item)
		{
			base.Add(item);
			return item;
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x001261B8 File Offset: 0x001243B8
		public void Expand(int size)
		{
			for (int i = 0; i < size; i++)
			{
				base.Add(default(T));
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x001261E0 File Offset: 0x001243E0
		public void Zero(int index)
		{
			base[index] = default(T);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x0002681B File Offset: 0x00024A1B
		public T Peek(int idxofs = 0)
		{
			return base[base.Count - 1 - idxofs];
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x0002682D File Offset: 0x00024A2D
		public void CropAtCount(int p)
		{
			this.RemoveLast(base.Count - p);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0002683D File Offset: 0x00024A3D
		public void RemoveLast(int cnt = 1)
		{
			if (cnt == 1)
			{
				base.RemoveAt(base.Count - 1);
				return;
			}
			base.RemoveRange(base.Count - cnt, cnt);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x00026861 File Offset: 0x00024A61
		public T Pop()
		{
			T result = base[base.Count - 1];
			base.RemoveAt(base.Count - 1);
			return result;
		}
	}
}

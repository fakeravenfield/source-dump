using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000595 RID: 1429
	public abstract class ObjectGrid<T> : ISerializationCallbackReceiver where T : class
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x00019B55 File Offset: 0x00017D55
		public bool initialized
		{
			get
			{
				return this.grid != null;
			}
		}

		// Token: 0x06002512 RID: 9490
		public abstract T Construct();

		// Token: 0x06002513 RID: 9491
		public abstract void OnCreate(T obj, Coord coord);

		// Token: 0x06002514 RID: 9492
		public abstract void OnMove(T obj, Coord newCoord);

		// Token: 0x06002515 RID: 9493
		public abstract void OnRemove(T obj);

		// Token: 0x06002516 RID: 9494 RVA: 0x000EE770 File Offset: 0x000EC970
		public virtual void OnBeforeSerialize()
		{
			if (this.serializedObjects.Length != this.grid.Count)
			{
				this.serializedObjects = new T[this.grid.Count];
				this.serializedHashes = new int[this.grid.Count];
			}
			int num = 0;
			foreach (KeyValuePair<int, T> keyValuePair in this.grid)
			{
				this.serializedObjects[num] = keyValuePair.Value;
				this.serializedHashes[num] = keyValuePair.Key;
				num++;
			}
			num = 0;
			if (this.serializedNailedHashes.Length != this.nailedHashes.Count)
			{
				this.serializedNailedHashes = new int[this.nailedHashes.Count];
			}
			foreach (int num2 in this.nailedHashes)
			{
				this.serializedNailedHashes[num] = num2;
				num++;
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000EE89C File Offset: 0x000ECA9C
		public virtual void OnAfterDeserialize()
		{
			this.grid = new Dictionary<int, T>();
			for (int i = 0; i < this.serializedObjects.Length; i++)
			{
				this.grid.Add(this.serializedHashes[i], this.serializedObjects[i]);
			}
			this.nailedHashes = new HashSet<int>();
			for (int j = 0; j < this.serializedNailedHashes.Length; j++)
			{
				this.nailedHashes.Add(this.serializedNailedHashes[j]);
			}
		}

		// Token: 0x17000303 RID: 771
		public T this[int x, int z]
		{
			get
			{
				return this[new Coord(x, z)];
			}
		}

		// Token: 0x17000304 RID: 772
		public T this[Coord c]
		{
			get
			{
				int key = c.ToInt();
				if (this.grid.ContainsKey(key))
				{
					return this.grid[key];
				}
				return default(T);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000EE950 File Offset: 0x000ECB50
		public bool IsNailed(Coord coord)
		{
			int item = coord.ToInt();
			return this.nailedHashes.Contains(item);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000EE978 File Offset: 0x000ECB78
		public void Nail(Coord coord)
		{
			int num = coord.ToInt();
			if (this.nailedHashes.Contains(num))
			{
				return;
			}
			T t = default(T);
			if (this.grid.ContainsKey(num))
			{
				t = this.grid[num];
			}
			bool flag = t == null;
			if (flag)
			{
				t = this.Construct();
			}
			this.nailedHashes.Add(num);
			if (!this.grid.ContainsKey(num))
			{
				this.grid.Add(coord.ToInt(), t);
			}
			if (flag)
			{
				this.OnCreate(t, coord);
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000EEA08 File Offset: 0x000ECC08
		public void Unnail(Coord coord, bool remove = true)
		{
			int num = coord.ToInt();
			if (!this.nailedHashes.Contains(num))
			{
				return;
			}
			this.nailedHashes.Remove(num);
			if (remove && this.grid.ContainsKey(num))
			{
				T t = this.grid[num];
				if (t != null)
				{
					this.OnRemove(t);
				}
				this.grid.Remove(num);
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00019B6F File Offset: 0x00017D6F
		public IEnumerable<T> NailedObjects()
		{
			foreach (int key in this.nailedHashes)
			{
				yield return this.grid[key];
			}
			HashSet<int>.Enumerator enumerator = default(HashSet<int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00019B7F File Offset: 0x00017D7F
		public IEnumerable<T> Objects()
		{
			foreach (KeyValuePair<int, T> keyValuePair in this.grid)
			{
				yield return keyValuePair.Value;
			}
			Dictionary<int, T>.Enumerator enumerator = default(Dictionary<int, T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00019B8F File Offset: 0x00017D8F
		public IEnumerable<Coord> Coords()
		{
			foreach (KeyValuePair<int, T> keyValuePair in this.grid)
			{
				yield return keyValuePair.Key.ToCoord();
			}
			Dictionary<int, T>.Enumerator enumerator = default(Dictionary<int, T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00019B9F File Offset: 0x00017D9F
		public IEnumerable<T> ObjectsFromCoord(Coord coord)
		{
			int counter = 0;
			foreach (Coord coord2 in coord.DistanceArea(20000))
			{
				int key = coord2.ToInt();
				if (this.grid.ContainsKey(key))
				{
					yield return this.grid[key];
					int num = counter;
					counter = num + 1;
				}
				if (counter >= this.grid.Count)
				{
					break;
				}
			}
			IEnumerator<Coord> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00019BB6 File Offset: 0x00017DB6
		public IEnumerable<T> ObjectsFromCoords(Coord[] coords)
		{
			int counter = 0;
			foreach (Coord coord in Coord.MultiDistanceArea(coords, 20000))
			{
				int key = coord.ToInt();
				if (this.grid.ContainsKey(key))
				{
					yield return this.grid[key];
					int num = counter;
					counter = num + 1;
				}
				if (counter >= this.grid.Count)
				{
					break;
				}
			}
			IEnumerator<Coord> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000EEA74 File Offset: 0x000ECC74
		public T GetClosestObj(Coord coord)
		{
			if (this.grid.Count == 0)
			{
				return default(T);
			}
			using (IEnumerator<T> enumerator = this.ObjectsFromCoord(coord).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return default(T);
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00019BCD File Offset: 0x00017DCD
		public int Count
		{
			get
			{
				return this.grid.Count;
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00019BDA File Offset: 0x00017DDA
		public virtual void Deploy(CoordRect rect, bool allowMove = true)
		{
			this.Deploy(rect, rect.Center, allowMove);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000EEAE0 File Offset: 0x000ECCE0
		public virtual void Deploy(CoordRect rect, Coord center, bool allowMove = true)
		{
			Dictionary<int, T> dictionary = new Dictionary<int, T>();
			foreach (int key in this.nailedHashes)
			{
				if (!this.grid.ContainsKey(key))
				{
					Debug.Log("Could not find nailed object");
				}
				T t = this.grid[key];
				if (t != null)
				{
					dictionary.Add(key, t);
				}
				this.grid.Remove(key);
			}
			Coord coord = rect.Min - this.stockMargin;
			Coord coord2 = rect.Max + this.stockMargin;
			for (int i = coord.x; i < coord2.x; i++)
			{
				for (int j = coord.z; j < coord2.z; j++)
				{
					int key2 = new Coord(i, j).ToInt();
					if (this.grid.ContainsKey(key2))
					{
						T t2 = this.grid[key2];
						if (t2 != null)
						{
							dictionary.Add(key2, t2);
						}
						this.grid.Remove(key2);
					}
				}
			}
			foreach (Coord coord3 in center.DistanceArea(rect))
			{
				int key3 = coord3.ToInt();
				if (!dictionary.ContainsKey(key3))
				{
					T t3 = default(T);
					if (this.grid.Count != 0 && allowMove)
					{
						t3 = this.grid.First<KeyValuePair<int, T>>().Value;
						this.OnMove(t3, coord3);
						this.grid.Remove(this.grid.First<KeyValuePair<int, T>>().Key);
					}
					else
					{
						t3 = this.Construct();
						this.OnCreate(t3, coord3);
					}
					dictionary.Add(key3, t3);
				}
			}
			foreach (KeyValuePair<int, T> keyValuePair in this.grid)
			{
				this.OnRemove(keyValuePair.Value);
			}
			this.grid = dictionary;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000EED44 File Offset: 0x000ECF44
		public virtual void Deploy(CoordRect[] rects, Coord[] centers, bool allowMove = true)
		{
			Dictionary<int, T> dictionary = new Dictionary<int, T>();
			foreach (int key in this.nailedHashes)
			{
				if (!this.grid.ContainsKey(key))
				{
					Debug.Log("Could not find nailed object");
				}
				T t = this.grid[key];
				if (t != null)
				{
					dictionary.Add(key, t);
				}
				this.grid.Remove(key);
			}
			for (int i = 0; i < centers.Length; i++)
			{
				CoordRect coordRect = rects[i];
				Coord min = coordRect.Min;
				Coord max = coordRect.Max;
				for (int j = min.x; j < max.x; j++)
				{
					for (int k = min.z; k < max.z; k++)
					{
						int key2 = new Coord(j, k).ToInt();
						if (this.grid.ContainsKey(key2))
						{
							T t2 = this.grid[key2];
							if (t2 != null)
							{
								dictionary.Add(key2, t2);
							}
							this.grid.Remove(key2);
						}
					}
				}
			}
			Stack<T> stack = new Stack<T>();
			Stack<int> stack2 = new Stack<int>();
			if (this.grid.Count != 0)
			{
				foreach (Coord coord in Coord.MultiDistanceArea(centers, 20000000))
				{
					int num = coord.ToInt();
					if (this.grid.ContainsKey(num))
					{
						if (this.grid[num] != null)
						{
							stack.Push(this.grid[num]);
							stack2.Push(num);
						}
						this.grid.Remove(num);
						if (this.grid.Count == 0)
						{
							break;
						}
					}
				}
			}
			for (int l = 0; l < centers.Length; l++)
			{
				CoordRect rect = rects[l];
				Coord coord2 = centers[l];
				foreach (Coord coord3 in coord2.DistanceArea(rect))
				{
					int key3 = coord3.ToInt();
					if (!dictionary.ContainsKey(key3))
					{
						T t3 = default(T);
						if (stack.Count != 0 && allowMove)
						{
							t3 = stack.Pop();
							stack2.Pop();
							this.OnMove(t3, coord3);
						}
						else
						{
							t3 = this.Construct();
							this.OnCreate(t3, coord3);
						}
						dictionary.Add(key3, t3);
					}
				}
			}
			while (stack.Count > 0)
			{
				T value = stack.Pop();
				int key4 = stack2.Pop();
				dictionary.Add(key4, value);
			}
			this.grid = dictionary;
		}

		// Token: 0x040023C0 RID: 9152
		public Dictionary<int, T> grid = new Dictionary<int, T>();

		// Token: 0x040023C1 RID: 9153
		public HashSet<int> nailedHashes = new HashSet<int>();

		// Token: 0x040023C2 RID: 9154
		public int stockMargin = 1;

		// Token: 0x040023C3 RID: 9155
		[SerializeField]
		private T[] serializedObjects = new T[0];

		// Token: 0x040023C4 RID: 9156
		[SerializeField]
		private int[] serializedHashes = new int[0];

		// Token: 0x040023C5 RID: 9157
		[SerializeField]
		private int[] serializedNailedHashes = new int[0];
	}
}

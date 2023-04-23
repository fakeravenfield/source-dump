using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000592 RID: 1426
	public class Matrix2<T> : ICloneable
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x0000256A File Offset: 0x0000076A
		public Matrix2()
		{
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000EDB10 File Offset: 0x000EBD10
		public Matrix2(int x, int z, T[] array = null)
		{
			this.rect = new CoordRect(0, 0, x, z);
			this.count = x * z;
			if (array != null && array.Length < this.count)
			{
				Debug.Log("Array length: " + array.Length.ToString() + " is lower then matrix capacity: " + this.count.ToString());
			}
			if (array != null && array.Length >= this.count)
			{
				this.array = array;
				return;
			}
			this.array = new T[this.count];
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000EDB9C File Offset: 0x000EBD9C
		public Matrix2(CoordRect rect, T[] array = null)
		{
			this.rect = rect;
			this.count = rect.size.x * rect.size.z;
			if (array != null && array.Length < this.count)
			{
				Debug.Log("Array length: " + array.Length.ToString() + " is lower then matrix capacity: " + this.count.ToString());
			}
			if (array != null && array.Length >= this.count)
			{
				this.array = array;
				return;
			}
			this.array = new T[this.count];
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000EDC34 File Offset: 0x000EBE34
		public Matrix2(Coord offset, Coord size, T[] array = null)
		{
			this.rect = new CoordRect(offset, size);
			this.count = this.rect.size.x * this.rect.size.z;
			if (array != null && array.Length < this.count)
			{
				Debug.Log("Array length: " + array.Length.ToString() + " is lower then matrix capacity: " + this.count.ToString());
			}
			if (array != null && array.Length >= this.count)
			{
				this.array = array;
				return;
			}
			this.array = new T[this.count];
		}

		// Token: 0x170002FF RID: 767
		public T this[int x, int z]
		{
			get
			{
				return this.array[(z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x];
			}
			set
			{
				this.array[(z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x] = value;
			}
		}

		// Token: 0x17000300 RID: 768
		public T this[Coord c]
		{
			get
			{
				return this.array[(c.z - this.rect.offset.z) * this.rect.size.x + c.x - this.rect.offset.x];
			}
			set
			{
				this.array[(c.z - this.rect.offset.z) * this.rect.size.x + c.x - this.rect.offset.x] = value;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000EDE30 File Offset: 0x000EC030
		public T CheckGet(int x, int z)
		{
			if (x >= this.rect.offset.x && x < this.rect.offset.x + this.rect.size.x && z >= this.rect.offset.z && z < this.rect.offset.z + this.rect.size.z)
			{
				return this.array[(z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x];
			}
			return default(T);
		}

		// Token: 0x17000301 RID: 769
		public T this[Vector2 pos]
		{
			get
			{
				int num = (int)(pos.x + 0.5f);
				if (pos.x < 0f)
				{
					num--;
				}
				int num2 = (int)(pos.y + 0.5f);
				if (pos.y < 0f)
				{
					num2--;
				}
				return this.array[(num2 - this.rect.offset.z) * this.rect.size.x + num - this.rect.offset.x];
			}
			set
			{
				int num = (int)(pos.x + 0.5f);
				if (pos.x < 0f)
				{
					num--;
				}
				int num2 = (int)(pos.y + 0.5f);
				if (pos.y < 0f)
				{
					num2--;
				}
				this.array[(num2 - this.rect.offset.z) * this.rect.size.x + num - this.rect.offset.x] = value;
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000EE018 File Offset: 0x000EC218
		public void Clear()
		{
			for (int i = 0; i < this.array.Length; i++)
			{
				this.array[i] = default(T);
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000EE050 File Offset: 0x000EC250
		public void ChangeRect(CoordRect newRect)
		{
			this.rect = newRect;
			this.count = newRect.size.x * newRect.size.z;
			if (this.array.Length < this.count)
			{
				this.array = new T[this.count];
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00019AB0 File Offset: 0x00017CB0
		public virtual object Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000EE0A4 File Offset: 0x000EC2A4
		public Matrix2<T> Clone(Matrix2<T> result)
		{
			if (result == null)
			{
				result = new Matrix2<T>(this.rect, null);
			}
			result.rect = this.rect;
			result.pos = this.pos;
			result.count = this.count;
			if (result.array.Length != this.array.Length)
			{
				result.array = new T[this.array.Length];
			}
			for (int i = 0; i < this.array.Length; i++)
			{
				result.array[i] = this.array[i];
			}
			return result;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000EE138 File Offset: 0x000EC338
		public void Fill(T v)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = v;
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000EE164 File Offset: 0x000EC364
		public void Fill(Matrix2<T> m, bool removeBorders = false)
		{
			CoordRect centerRect = CoordRect.Intersect(this.rect, m.rect);
			Coord min = centerRect.Min;
			Coord max = centerRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					this[i, j] = m[i, j];
				}
			}
			if (removeBorders)
			{
				this.RemoveBorders(centerRect);
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00019AB9 File Offset: 0x00017CB9
		public void SetPos(int x, int z)
		{
			this.pos = (z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000EE1E0 File Offset: 0x000EC3E0
		public void SetPos(int x, int z, int s)
		{
			this.pos = (z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x + s * this.rect.size.x * this.rect.size.z;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00019AF7 File Offset: 0x00017CF7
		public void MoveX()
		{
			this.pos++;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00019B07 File Offset: 0x00017D07
		public void MoveZ()
		{
			this.pos += this.rect.size.x;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00019B26 File Offset: 0x00017D26
		public void MovePrevX()
		{
			this.pos--;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00019B36 File Offset: 0x00017D36
		public void MovePrevZ()
		{
			this.pos -= this.rect.size.x;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000EE250 File Offset: 0x000EC450
		public void RemoveBorders()
		{
			Coord min = this.rect.Min;
			Coord coord = this.rect.Max - 1;
			for (int i = min.x; i <= coord.x; i++)
			{
				this.SetPos(i, min.z);
				this.array[this.pos] = this.array[this.pos + this.rect.size.x];
			}
			for (int j = min.x; j <= coord.x; j++)
			{
				this.SetPos(j, coord.z);
				this.array[this.pos] = this.array[this.pos - this.rect.size.x];
			}
			for (int k = min.z; k <= coord.z; k++)
			{
				this.SetPos(min.x, k);
				this.array[this.pos] = this.array[this.pos + 1];
			}
			for (int l = min.z; l <= coord.z; l++)
			{
				this.SetPos(coord.x, l);
				this.array[this.pos] = this.array[this.pos - 1];
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000EE3C0 File Offset: 0x000EC5C0
		public void RemoveBorders(int borderMinX, int borderMinZ, int borderMaxX, int borderMaxZ)
		{
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			if (borderMinZ != 0)
			{
				for (int i = min.x; i < max.x; i++)
				{
					T value = this[i, min.z + borderMinZ];
					for (int j = min.z; j < min.z + borderMinZ; j++)
					{
						this[i, j] = value;
					}
				}
			}
			if (borderMaxZ != 0)
			{
				for (int k = min.x; k < max.x; k++)
				{
					T value2 = this[k, max.z - borderMaxZ];
					for (int l = max.z - borderMaxZ; l < max.z; l++)
					{
						this[k, l] = value2;
					}
				}
			}
			if (borderMinX != 0)
			{
				for (int m = min.z; m < max.z; m++)
				{
					T value3 = this[min.x + borderMinX, m];
					for (int n = min.x; n < min.x + borderMinX; n++)
					{
						this[n, m] = value3;
					}
				}
			}
			if (borderMaxX != 0)
			{
				for (int num = min.z; num < max.z; num++)
				{
					T value4 = this[max.x - borderMaxX, num];
					for (int num2 = max.x - borderMaxX; num2 < max.x; num2++)
					{
						this[num2, num] = value4;
					}
				}
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000EE53C File Offset: 0x000EC73C
		public void RemoveBorders(CoordRect centerRect)
		{
			this.RemoveBorders(Mathf.Max(0, centerRect.offset.x - this.rect.offset.x), Mathf.Max(0, centerRect.offset.z - this.rect.offset.z), Mathf.Max(0, this.rect.Max.x - centerRect.Max.x + 1), Mathf.Max(0, this.rect.Max.z - centerRect.Max.z + 1));
		}

		// Token: 0x040023B6 RID: 9142
		public T[] array;

		// Token: 0x040023B7 RID: 9143
		public CoordRect rect;

		// Token: 0x040023B8 RID: 9144
		public int pos;

		// Token: 0x040023B9 RID: 9145
		public int count;
	}
}

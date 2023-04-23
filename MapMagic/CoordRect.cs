using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000590 RID: 1424
	[Serializable]
	public struct CoordRect : CustomSerialization.IStruct
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000197D7 File Offset: 0x000179D7
		public bool isZero
		{
			get
			{
				return this.size.x == 0 || this.size.z == 0;
			}
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000197F6 File Offset: 0x000179F6
		public CoordRect(Coord offset, Coord size)
		{
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00019806 File Offset: 0x00017A06
		public CoordRect(int offsetX, int offsetZ, int sizeX, int sizeZ)
		{
			this.offset = new Coord(offsetX, offsetZ);
			this.size = new Coord(sizeX, sizeZ);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00019823 File Offset: 0x00017A23
		public CoordRect(float offsetX, float offsetZ, float sizeX, float sizeZ)
		{
			this.offset = new Coord((int)offsetX, (int)offsetZ);
			this.size = new Coord((int)sizeX, (int)sizeZ);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00019844 File Offset: 0x00017A44
		public CoordRect(Rect r)
		{
			this.offset = new Coord((int)r.x, (int)r.y);
			this.size = new Coord((int)r.width, (int)r.height);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0001987C File Offset: 0x00017A7C
		public int GetPos(int x, int z)
		{
			return (z - this.offset.z) * this.size.x + x - this.offset.x;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000198A5 File Offset: 0x00017AA5
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x000198B8 File Offset: 0x00017AB8
		public Coord Max
		{
			get
			{
				return this.offset + this.size;
			}
			set
			{
				this.offset = value - this.size;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000198CC File Offset: 0x00017ACC
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x000198D4 File Offset: 0x00017AD4
		public Coord Min
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000198DD File Offset: 0x00017ADD
		public Coord Center
		{
			get
			{
				return this.offset + this.size / 2;
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000198F6 File Offset: 0x00017AF6
		public static bool operator >(CoordRect c1, CoordRect c2)
		{
			return c1.size > c2.size;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00019909 File Offset: 0x00017B09
		public static bool operator <(CoordRect c1, CoordRect c2)
		{
			return c1.size < c2.size;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0001991C File Offset: 0x00017B1C
		public static bool operator ==(CoordRect c1, CoordRect c2)
		{
			return c1.offset == c2.offset && c1.size == c2.size;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00019944 File Offset: 0x00017B44
		public static bool operator !=(CoordRect c1, CoordRect c2)
		{
			return c1.offset != c2.offset || c1.size != c2.size;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0001996C File Offset: 0x00017B6C
		public static CoordRect operator *(CoordRect c, int s)
		{
			return new CoordRect(c.offset * s, c.size * s);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0001998B File Offset: 0x00017B8B
		public static CoordRect operator *(CoordRect c, float s)
		{
			return new CoordRect(c.offset * s, c.size * s);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000199AA File Offset: 0x00017BAA
		public static CoordRect operator /(CoordRect c, int s)
		{
			return new CoordRect(c.offset / s, c.size / s);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000199C9 File Offset: 0x00017BC9
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000ED31C File Offset: 0x000EB51C
		public override int GetHashCode()
		{
			return this.offset.x * 100000000 + this.offset.z * 1000000 + this.size.x * 1000 + this.size.z;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000199DC File Offset: 0x00017BDC
		public void Round(int val, bool inscribed = false)
		{
			this.offset.Round(val, inscribed);
			this.size.Round(val, !inscribed);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000199FB File Offset: 0x00017BFB
		public void Round(CoordRect r, bool inscribed = false)
		{
			this.offset.Round(r.offset, inscribed);
			this.size.Round(r.size, !inscribed);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000ED36C File Offset: 0x000EB56C
		public void Clamp(Coord min, Coord max)
		{
			Coord max2 = this.Max;
			this.offset = Coord.Max(min, this.offset);
			this.size = Coord.Min(max - this.offset, max2 - this.offset);
			this.size.ClampPositive();
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00019A24 File Offset: 0x00017C24
		public static CoordRect Intersect(CoordRect c1, CoordRect c2)
		{
			c1.Clamp(c2.Min, c2.Max);
			return c1;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000ED3C0 File Offset: 0x000EB5C0
		public Coord CoordByNum(int num)
		{
			int num2 = num / this.size.x;
			return new Coord(num - num2 * this.size.x + this.offset.x, num2 + this.offset.z);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000ED408 File Offset: 0x000EB608
		public bool CheckInRange(int x, int z)
		{
			return x - this.offset.x >= 0 && x - this.offset.x < this.size.x && z - this.offset.z >= 0 && z - this.offset.z < this.size.z;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000ED46C File Offset: 0x000EB66C
		public bool CheckInRange(Coord coord)
		{
			return coord.x >= this.offset.x && coord.x < this.offset.x + this.size.x && coord.z >= this.offset.z && coord.z < this.offset.z + this.size.z;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000ED4E0 File Offset: 0x000EB6E0
		public bool CheckInRangeAndBounds(int x, int z)
		{
			return x > this.offset.x && x < this.offset.x + this.size.x - 1 && z > this.offset.z && z < this.offset.z + this.size.z - 1;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000ED544 File Offset: 0x000EB744
		public bool CheckInRangeAndBounds(Coord coord)
		{
			return coord.x > this.offset.x && coord.x < this.offset.x + this.size.x - 1 && coord.z > this.offset.z && coord.z < this.offset.z + this.size.z - 1;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000ED5BC File Offset: 0x000EB7BC
		public bool Divisible(float factor)
		{
			return (float)this.offset.x % factor == 0f && (float)this.offset.z % factor == 0f && (float)this.size.x % factor == 0f && (float)this.size.z % factor == 0f;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000ED620 File Offset: 0x000EB820
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				": offsetX:",
				this.offset.x.ToString(),
				" offsetZ:",
				this.offset.z.ToString(),
				" sizeX:",
				this.size.x.ToString(),
				" sizeZ:",
				this.size.z.ToString()
			});
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000ED6B8 File Offset: 0x000EB8B8
		public Vector2 ToWorldspace(Coord coord, Rect worldRect)
		{
			return new Vector2(1f * (float)(coord.x - this.offset.x) / (float)this.size.x * worldRect.width + worldRect.x, 1f * (float)(coord.z - this.offset.z) / (float)this.size.z * worldRect.height + worldRect.y);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000ED738 File Offset: 0x000EB938
		public Coord ToLocalspace(Vector2 pos, Rect worldRect)
		{
			return new Coord((int)((pos.x - worldRect.x) / worldRect.width * (float)this.size.x + (float)this.offset.x), (int)((pos.y - worldRect.y) / worldRect.height * (float)this.size.z + (float)this.offset.z));
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00019A3C File Offset: 0x00017C3C
		public IEnumerable<Coord> Cells(int cellSize)
		{
			Coord min = this.offset / cellSize;
			Coord max = (this.Max - 1) / cellSize + 1;
			int num;
			for (int x = min.x; x < max.x; x = num + 1)
			{
				for (int z = min.z; z < max.z; z = num + 1)
				{
					yield return new Coord(x, z);
					num = z;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00019A58 File Offset: 0x00017C58
		public CoordRect Expand(int n)
		{
			return new CoordRect(this.offset - n, this.size + n * 2);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000ED7AC File Offset: 0x000EB9AC
		public CoordRect Approximate(int val)
		{
			CoordRect coordRect = default(CoordRect);
			coordRect.size.x = (this.size.x / val + 1) * val;
			coordRect.size.z = (this.size.z / val + 1) * val;
			coordRect.offset.x = this.offset.x - (coordRect.size.x - this.size.x) / 2;
			coordRect.offset.z = this.offset.z - (coordRect.size.z - this.size.z) / 2;
			coordRect.offset.x = (int)((float)(coordRect.offset.x / val) + 0.5f) * val;
			coordRect.offset.z = (int)((float)(coordRect.offset.z / val) + 0.5f) * val;
			return coordRect;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000ED8A4 File Offset: 0x000EBAA4
		public string Encode()
		{
			return string.Concat(new string[]
			{
				"offsetX=",
				this.offset.x.ToString(),
				" offsetZ=",
				this.offset.z.ToString(),
				" sizeX=",
				this.size.x.ToString(),
				" sizeZ=",
				this.size.z.ToString()
			});
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000ED928 File Offset: 0x000EBB28
		public void Decode(string[] lineMembers)
		{
			this.offset.x = (int)lineMembers[2].Parse(typeof(int));
			this.offset.z = (int)lineMembers[3].Parse(typeof(int));
			this.size.x = (int)lineMembers[4].Parse(typeof(int));
			this.size.z = (int)lineMembers[5].Parse(typeof(int));
		}

		// Token: 0x040023A9 RID: 9129
		public Coord offset;

		// Token: 0x040023AA RID: 9130
		public Coord size;
	}
}

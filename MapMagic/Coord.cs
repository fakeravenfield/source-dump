using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200058A RID: 1418
	[Serializable]
	public struct Coord : CustomSerialization.IStruct
	{
		// Token: 0x06002472 RID: 9330 RVA: 0x00019201 File Offset: 0x00017401
		public Coord(int x, int z)
		{
			this.x = x;
			this.z = z;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00019211 File Offset: 0x00017411
		public static bool operator >(Coord c1, Coord c2)
		{
			return c1.x > c2.x && c1.z > c2.z;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00019231 File Offset: 0x00017431
		public static bool operator <(Coord c1, Coord c2)
		{
			return c1.x < c2.x && c1.z < c2.z;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00019251 File Offset: 0x00017451
		public static bool operator ==(Coord c1, Coord c2)
		{
			return c1.x == c2.x && c1.z == c2.z;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00019271 File Offset: 0x00017471
		public static bool operator !=(Coord c1, Coord c2)
		{
			return c1.x != c2.x || c1.z != c2.z;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00019294 File Offset: 0x00017494
		public static Coord operator +(Coord c, int s)
		{
			return new Coord(c.x + s, c.z + s);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000192AB File Offset: 0x000174AB
		public static Coord operator +(Coord c1, Coord c2)
		{
			return new Coord(c1.x + c2.x, c1.z + c2.z);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000192CC File Offset: 0x000174CC
		public static Coord operator -(Coord c, int s)
		{
			return new Coord(c.x - s, c.z - s);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000192E3 File Offset: 0x000174E3
		public static Coord operator -(Coord c1, Coord c2)
		{
			return new Coord(c1.x - c2.x, c1.z - c2.z);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00019304 File Offset: 0x00017504
		public static Coord operator *(Coord c, int s)
		{
			return new Coord(c.x * s, c.z * s);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0001931B File Offset: 0x0001751B
		public static Vector2 operator *(Coord c, Vector2 s)
		{
			return new Vector2((float)c.x * s.x, (float)c.z * s.y);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0001933E File Offset: 0x0001753E
		public static Vector3 operator *(Coord c, Vector3 s)
		{
			return new Vector3((float)c.x * s.x, s.y, (float)c.z * s.z);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00019367 File Offset: 0x00017567
		public static Coord operator *(Coord c, float s)
		{
			return new Coord((int)((float)c.x * s), (int)((float)c.z * s));
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00019382 File Offset: 0x00017582
		public static Coord operator /(Coord c, int s)
		{
			return new Coord(c.x / s, c.z / s);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00019399 File Offset: 0x00017599
		public static Coord operator /(Coord c, float s)
		{
			return new Coord((int)((float)c.x / s), (int)((float)c.z / s));
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000193B4 File Offset: 0x000175B4
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000193C7 File Offset: 0x000175C7
		public override int GetHashCode()
		{
			return this.x * 10000000 + this.z;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000193DC File Offset: 0x000175DC
		public int Minimal
		{
			get
			{
				return Mathf.Min(this.x, this.z);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000193EF File Offset: 0x000175EF
		public int SqrMagnitude
		{
			get
			{
				return this.x * this.x + this.z * this.z;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x0001940C File Offset: 0x0001760C
		public Vector3 vector3
		{
			get
			{
				return new Vector3((float)this.x, 0f, (float)this.z);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00019426 File Offset: 0x00017626
		public static Coord zero
		{
			get
			{
				return new Coord(0, 0);
			}
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000EC6C0 File Offset: 0x000EA8C0
		public void Round(int val, bool ceil = false)
		{
			this.x = (ceil ? Mathf.CeilToInt(1f * (float)this.x / (float)val) : Mathf.FloorToInt(1f * (float)this.x / (float)val)) * val;
			this.z = (ceil ? Mathf.CeilToInt(1f * (float)this.z / (float)val) : Mathf.FloorToInt(1f * (float)this.z / (float)val)) * val;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000EC73C File Offset: 0x000EA93C
		public void Round(Coord c, bool ceil = false)
		{
			this.x = (ceil ? Mathf.FloorToInt(1f * (float)this.x / (float)c.x) : Mathf.CeilToInt(1f * (float)this.x / (float)c.x)) * c.x;
			this.z = (ceil ? Mathf.FloorToInt(1f * (float)this.z / (float)c.z) : Mathf.CeilToInt(1f * (float)this.z / (float)c.z)) * c.z;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0001942F File Offset: 0x0001762F
		public void ClampPositive()
		{
			this.x = Mathf.Max(0, this.x);
			this.z = Mathf.Max(0, this.z);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000EC7D8 File Offset: 0x000EA9D8
		public void ClampByRect(CoordRect rect)
		{
			if (this.x < rect.offset.x)
			{
				this.x = rect.offset.x;
			}
			if (this.x >= rect.offset.x + rect.size.x)
			{
				this.x = rect.offset.x + rect.size.x - 1;
			}
			if (this.z < rect.offset.z)
			{
				this.z = rect.offset.z;
			}
			if (this.z >= rect.offset.z + rect.size.z)
			{
				this.z = rect.offset.z + rect.size.z - 1;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00019455 File Offset: 0x00017655
		public static Coord Min(Coord c1, Coord c2)
		{
			return new Coord(Mathf.Min(c1.x, c2.x), Mathf.Min(c1.z, c2.z));
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0001947E File Offset: 0x0001767E
		public static Coord Max(Coord c1, Coord c2)
		{
			return new Coord(Mathf.Max(c1.x, c2.x), Mathf.Max(c1.z, c2.z));
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000194A7 File Offset: 0x000176A7
		public static float Distance(Coord c1, Coord c2)
		{
			return Mathf.Sqrt((float)((c1.x - c2.x) * (c1.x - c2.x) + (c1.z - c2.z) * (c1.z - c2.z)));
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000194E6 File Offset: 0x000176E6
		public static float Distance(Coord c1, int x, int z)
		{
			return Mathf.Sqrt((float)((c1.x - x) * (c1.x - x) + (c1.z - z) * (c1.z - z)));
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000EC8AC File Offset: 0x000EAAAC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				" x:",
				this.x.ToString(),
				" z:",
				this.z.ToString()
			});
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00019511 File Offset: 0x00017711
		public IEnumerable<Coord> DistanceStep(int i, int dist)
		{
			yield return new Coord(this.x - i, this.z - dist);
			yield return new Coord(this.x - dist, this.z + i);
			yield return new Coord(this.x + i, this.z + dist);
			yield return new Coord(this.x + dist, this.z - i);
			yield return new Coord(this.x + i + 1, this.z - dist);
			yield return new Coord(this.x - dist, this.z - i - 1);
			yield return new Coord(this.x - i - 1, this.z + dist);
			yield return new Coord(this.x + dist, this.z + i + 1);
			yield break;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00019534 File Offset: 0x00017734
		public IEnumerable<Coord> DistancePerimeter(int dist)
		{
			int num;
			for (int i = 0; i < dist; i = num + 1)
			{
				foreach (Coord coord in this.DistanceStep(i, dist))
				{
					yield return coord;
				}
				IEnumerator<Coord> enumerator = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00019550 File Offset: 0x00017750
		public IEnumerable<Coord> DistanceArea(int maxDist)
		{
			yield return ref this;
			int num;
			for (int i = 0; i < maxDist; i = num + 1)
			{
				foreach (Coord coord in this.DistancePerimeter(i))
				{
					yield return coord;
				}
				IEnumerator<Coord> enumerator = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0001956C File Offset: 0x0001776C
		public IEnumerable<Coord> DistanceArea(CoordRect rect)
		{
			int maxDist = Mathf.Max(new int[]
			{
				this.x - rect.offset.x,
				rect.Max.x - this.x,
				this.z - rect.offset.z,
				rect.Max.z - this.z
			}) + 1;
			if (rect.CheckInRange(ref this))
			{
				yield return ref this;
			}
			int num;
			for (int i = 0; i < maxDist; i = num + 1)
			{
				foreach (Coord coord in this.DistancePerimeter(i))
				{
					if (rect.CheckInRange(coord))
					{
						yield return coord;
					}
				}
				IEnumerator<Coord> enumerator = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00019588 File Offset: 0x00017788
		public static IEnumerable<Coord> MultiDistanceArea(Coord[] coords, int maxDist)
		{
			int num;
			for (int c = 0; c < coords.Length; c = num + 1)
			{
				yield return coords[c];
				num = c;
			}
			for (int c = 0; c < maxDist; c = num + 1)
			{
				for (int i = 0; i < c; i = num + 1)
				{
					for (int c2 = 0; c2 < coords.Length; c2 = num + 1)
					{
						foreach (Coord coord in coords[c2].DistanceStep(i, c))
						{
							yield return coord;
						}
						IEnumerator<Coord> enumerator = null;
						num = c2;
					}
					num = i;
				}
				num = c;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0001959F File Offset: 0x0001779F
		public Vector3 ToVector3(float cellSize)
		{
			return new Vector3((float)this.x * cellSize, 0f, (float)this.z * cellSize);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000195BD File Offset: 0x000177BD
		public Vector2 ToVector2(float cellSize)
		{
			return new Vector2((float)this.x * cellSize, (float)this.z * cellSize);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000195D6 File Offset: 0x000177D6
		public Rect ToRect(float cellSize)
		{
			return new Rect((float)this.x * cellSize, (float)this.z * cellSize, cellSize, cellSize);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000195F1 File Offset: 0x000177F1
		public string Encode()
		{
			return "x=" + this.x.ToString() + " z=" + this.z.ToString();
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00019618 File Offset: 0x00017818
		public void Decode(string[] lineMembers)
		{
			this.x = (int)lineMembers[2].Parse(typeof(int));
			this.z = (int)lineMembers[3].Parse(typeof(int));
		}

		// Token: 0x04002377 RID: 9079
		public int x;

		// Token: 0x04002378 RID: 9080
		public int z;
	}
}

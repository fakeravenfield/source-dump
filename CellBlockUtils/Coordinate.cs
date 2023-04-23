using System;
using UnityEngine;

namespace CellBlockUtils
{
	// Token: 0x02000A2F RID: 2607
	[Serializable]
	public struct Coordinate
	{
		// Token: 0x060053BF RID: 21439 RVA: 0x0003DC93 File Offset: 0x0003BE93
		public Coordinate(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0003DCAA File Offset: 0x0003BEAA
		public Coordinate(Vector3 vector)
		{
			this.x = Mathf.RoundToInt(vector.x);
			this.y = Mathf.RoundToInt(vector.y);
			this.z = Mathf.RoundToInt(vector.z);
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0003DCDF File Offset: 0x0003BEDF
		public Vector3 Vector3()
		{
			return new Vector3((float)this.x, (float)this.y, (float)this.z);
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0003DCFB File Offset: 0x0003BEFB
		public static Coordinate operator +(Coordinate c1, Coordinate c2)
		{
			return new Coordinate(c1.x + c2.x, c1.y + c2.y, c1.z + c2.z);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0003DD29 File Offset: 0x0003BF29
		public static Coordinate operator -(Coordinate c1, Coordinate c2)
		{
			return new Coordinate(c1.x - c2.x, c1.y - c2.y, c1.z - c2.z);
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0003DD57 File Offset: 0x0003BF57
		public static Coordinate operator *(Coordinate c1, float m)
		{
			return new Coordinate(Mathf.RoundToInt((float)c1.x * m), Mathf.RoundToInt((float)c1.y * m), Mathf.RoundToInt((float)c1.z * m));
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0003DD88 File Offset: 0x0003BF88
		public static bool operator ==(Coordinate c1, Coordinate c2)
		{
			return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z;
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0003DDB6 File Offset: 0x0003BFB6
		public static bool operator !=(Coordinate c1, Coordinate c2)
		{
			return !(c1 == c2);
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0003DDC2 File Offset: 0x0003BFC2
		public static int ManhattanDistance(Coordinate c1, Coordinate c2)
		{
			return Mathf.Abs(c1.x - c2.x) + Mathf.Abs(c1.y - c2.y) + Mathf.Abs(c1.z - c2.z);
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0003DDFC File Offset: 0x0003BFFC
		public static float Distance(Coordinate c1, Coordinate c2)
		{
			return UnityEngine.Vector3.Distance(c1.Vector3(), c2.Vector3());
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0013995C File Offset: 0x00137B5C
		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Coordinate))
			{
				return false;
			}
			Coordinate coordinate = (Coordinate)obj;
			return this.x == coordinate.x && this.y == coordinate.y && this.z == coordinate.z;
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0003DE11 File Offset: 0x0003C011
		public override int GetHashCode()
		{
			return this.x ^ this.y ^ this.z;
		}

		// Token: 0x040032AB RID: 12971
		public static Coordinate Zero = new Coordinate(0, 0, 0);

		// Token: 0x040032AC RID: 12972
		public int x;

		// Token: 0x040032AD RID: 12973
		public int y;

		// Token: 0x040032AE RID: 12974
		public int z;
	}
}

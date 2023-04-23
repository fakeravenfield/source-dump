using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200059C RID: 1436
	public class InstanceRandom
	{
		// Token: 0x06002560 RID: 9568 RVA: 0x000EF9A4 File Offset: 0x000EDBA4
		public InstanceRandom(int s, int lutLength = 100)
		{
			this.seed = s;
			this.lut = new float[lutLength];
			for (int i = 0; i < this.lut.Length; i++)
			{
				this.lut[i] = this.Random();
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00019E67 File Offset: 0x00018067
		public float Random()
		{
			this.seed = 214013 * this.seed + 2531011;
			return (float)(this.seed >> 16 & 32767) / 32768f;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00019E97 File Offset: 0x00018097
		public float Random(float min, float max)
		{
			this.seed = 214013 * this.seed + 2531011;
			return (float)(this.seed >> 16 & 32767) / 32768f * (max - min) + min;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000EF9EC File Offset: 0x000EDBEC
		public float Random(Vector2 scope)
		{
			this.seed = 214013 * this.seed + 2531011;
			return (float)(this.seed >> 16 & 32767) / 32768f * (scope.y - scope.x) + scope.x;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000EFA3C File Offset: 0x000EDC3C
		public int RandomToInt(float val)
		{
			int num = (int)val;
			if (val - (float)num > this.Random())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000EFA60 File Offset: 0x000EDC60
		public float MultipleRandom(int steps)
		{
			float num = this.Random();
			return (1f - Mathf.Pow(num, (float)(steps + 1))) / (1f - num) - 1f;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00019ECD File Offset: 0x000180CD
		public float CoordinateRandom(int x)
		{
			this.current = x % this.lut.Length;
			return this.lut[this.current];
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00019EEC File Offset: 0x000180EC
		public float CoordinateRandom(int x, Vector2 scope)
		{
			this.current = x % this.lut.Length;
			return this.lut[this.current] * (scope.y - scope.x) + scope.x;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000EFA94 File Offset: 0x000EDC94
		public float CoordinateRandom(int x, int z)
		{
			z += 991;
			x += 1999;
			this.current = x * x % 5453 + Mathf.Abs(z * x % 2677) + z * z % 1871;
			this.current %= this.lut.Length;
			return this.lut[this.current];
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00019F20 File Offset: 0x00018120
		public float NextCoordinateRandom()
		{
			this.current++;
			this.current %= this.lut.Length;
			return this.lut[this.current];
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000EFB00 File Offset: 0x000EDD00
		public static float Fractal(int x, int z, float size, float detail = 0.5f)
		{
			float num = 0.5f;
			float num2 = size;
			float num3 = 1f;
			int num4 = 1;
			for (int i = 0; i < 100; i++)
			{
				num2 /= 2f;
				if (num2 < 1f)
				{
					break;
				}
				num4++;
			}
			num2 = size;
			for (int j = 0; j < num4; j++)
			{
				float num5 = Mathf.PerlinNoise((float)x / (num2 + 1f), (float)z / (num2 + 1f));
				num5 = (num5 - 0.5f) * num3 + 0.5f;
				if (num5 > 0.5f)
				{
					num = 1f - 2f * (1f - num) * (1f - num5);
				}
				else
				{
					num = 2f * num5 * num;
				}
				num2 *= 0.5f;
				num3 *= detail;
			}
			if (num < 0f)
			{
				num = 0f;
			}
			if (num > 1f)
			{
				num = 1f;
			}
			return num;
		}

		// Token: 0x040023EA RID: 9194
		private int seed;

		// Token: 0x040023EB RID: 9195
		private float[] lut;

		// Token: 0x040023EC RID: 9196
		private int current;
	}
}

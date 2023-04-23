using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000594 RID: 1428
	public class Noise
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x000EE5E0 File Offset: 0x000EC7E0
		public Noise(float size, int resolution, int seedX, int seedZ)
		{
			this.size = size;
			this.resolution = resolution;
			this.seedX = seedX % 77777;
			this.seedZ = seedZ % 73333;
			this.iterations = 1;
			float num = size;
			for (int i = 0; i < 100; i++)
			{
				num /= 2f;
				if (num < 1f)
				{
					break;
				}
				this.iterations++;
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000EE650 File Offset: 0x000EC850
		public float Fractal(int x, int z, float detail = 0.5f)
		{
			float num = 0.5f;
			float num2 = this.size;
			float num3 = 1f;
			float num4 = 1f * (float)x / (float)this.resolution * 512f;
			float num5 = 1f * (float)z / (float)this.resolution * 512f;
			for (int i = 0; i < this.iterations; i++)
			{
				float num6 = 8f / (num2 * 10f + 1f);
				float num7 = Mathf.PerlinNoise((num4 + (float)this.seedX + (float)(1000 * (i + 1))) * num6, (num5 + (float)this.seedZ + (float)(100 * i)) * num6);
				num7 = (num7 - 0.5f) * num3 + 0.5f;
				if (num7 > 0.5f)
				{
					num = 1f - 2f * (1f - num) * (1f - num7);
				}
				else
				{
					num = 2f * num7 * num;
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

		// Token: 0x040023BB RID: 9147
		private int iterations;

		// Token: 0x040023BC RID: 9148
		public int seedX;

		// Token: 0x040023BD RID: 9149
		public int seedZ;

		// Token: 0x040023BE RID: 9150
		public int resolution;

		// Token: 0x040023BF RID: 9151
		public float size;
	}
}

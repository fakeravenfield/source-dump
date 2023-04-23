using System;

namespace Lzf
{
	// Token: 0x02000361 RID: 865
	public static class LZF
	{
		// Token: 0x0600161E RID: 5662 RVA: 0x0009F374 File Offset: 0x0009D574
		public static byte[] Compress(byte[] input)
		{
			byte[] array = new byte[input.Length * 2];
			int newSize = LZF.Compress(input, input.Length, array, array.Length);
			Array.Resize<byte>(ref array, newSize);
			return array;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0009F3A4 File Offset: 0x0009D5A4
		public static byte[] Decompress(byte[] input)
		{
			byte[] array = new byte[input.Length * 16];
			int newSize = LZF.Decompress(input, input.Length, array, array.Length);
			Array.Resize<byte>(ref array, newSize);
			return array;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0009F3D4 File Offset: 0x0009D5D4
		public static int Compress(byte[] input, int inputLength, byte[] output, int outputLength)
		{
			Array.Clear(LZF.HashTable, 0, 16384);
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = (uint)((int)input[(int)num] << 8 | (int)input[(int)(num + 1U)]);
			int num4 = 0;
			for (;;)
			{
				if ((ulong)num < (ulong)((long)(inputLength - 2)))
				{
					num3 = (num3 << 8 | (uint)input[(int)(num + 2U)]);
					long num5 = (long)((ulong)((num3 ^ num3 << 5) >> (int)(10U - num3 * 5U) & 16383U));
					long num6 = LZF.HashTable[(int)(checked((IntPtr)num5))];
					LZF.HashTable[(int)(checked((IntPtr)num5))] = (long)((ulong)num);
					long num7;
					if ((num7 = (long)((ulong)num - (ulong)num6 - 1UL)) < 8192L && (ulong)(num + 4U) < (ulong)((long)inputLength) && num6 > 0L && input[(int)(checked((IntPtr)num6))] == input[(int)num] && input[(int)(checked((IntPtr)(unchecked(num6 + 1L))))] == input[(int)(num + 1U)] && input[(int)(checked((IntPtr)(unchecked(num6 + 2L))))] == input[(int)(num + 2U)])
					{
						uint num8 = 2U;
						uint num9 = (uint)(inputLength - (int)num - (int)num8);
						num9 = ((num9 > 264U) ? 264U : num9);
						if ((ulong)num2 + (ulong)((long)num4) + 1UL + 3UL >= (ulong)((long)outputLength))
						{
							break;
						}
						do
						{
							num8 += 1U;
						}
						while (num8 < num9 && input[(int)(checked((IntPtr)(unchecked(num6 + (long)((ulong)num8)))))] == input[(int)(num + num8)]);
						if (num4 != 0)
						{
							output[(int)num2++] = (byte)(num4 - 1);
							num4 = -num4;
							do
							{
								output[(int)num2++] = input[(int)(checked((IntPtr)(unchecked((ulong)num + (ulong)((long)num4)))))];
							}
							while (++num4 != 0);
						}
						num8 -= 2U;
						num += 1U;
						if (num8 < 7U)
						{
							output[(int)num2++] = (byte)((num7 >> 8) + (long)((ulong)((ulong)num8 << 5)));
						}
						else
						{
							output[(int)num2++] = (byte)((num7 >> 8) + 224L);
							output[(int)num2++] = (byte)(num8 - 7U);
						}
						output[(int)num2++] = (byte)num7;
						num += num8 - 1U;
						num3 = (uint)((int)input[(int)num] << 8 | (int)input[(int)(num + 1U)]);
						num3 = (num3 << 8 | (uint)input[(int)(num + 2U)]);
						LZF.HashTable[(int)((num3 ^ num3 << 5) >> (int)(10U - num3 * 5U) & 16383U)] = (long)((ulong)num);
						num += 1U;
						num3 = (num3 << 8 | (uint)input[(int)(num + 2U)]);
						LZF.HashTable[(int)((num3 ^ num3 << 5) >> (int)(10U - num3 * 5U) & 16383U)] = (long)((ulong)num);
						num += 1U;
						continue;
					}
				}
				else if ((ulong)num == (ulong)((long)inputLength))
				{
					goto IL_252;
				}
				num4++;
				num += 1U;
				if ((long)num4 == 32L)
				{
					if ((ulong)(num2 + 1U + 32U) >= (ulong)((long)outputLength))
					{
						return 0;
					}
					output[(int)num2++] = 31;
					num4 = -num4;
					do
					{
						output[(int)num2++] = input[(int)(checked((IntPtr)(unchecked((ulong)num + (ulong)((long)num4)))))];
					}
					while (++num4 != 0);
				}
			}
			return 0;
			IL_252:
			if (num4 != 0)
			{
				if ((ulong)num2 + (ulong)((long)num4) + 1UL >= (ulong)((long)outputLength))
				{
					return 0;
				}
				output[(int)num2++] = (byte)(num4 - 1);
				num4 = -num4;
				do
				{
					output[(int)num2++] = input[(int)(checked((IntPtr)(unchecked((ulong)num + (ulong)((long)num4)))))];
				}
				while (++num4 != 0);
			}
			return (int)num2;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0009F674 File Offset: 0x0009D874
		public static int Decompress(byte[] input, int inputLength, byte[] output, int outputLength)
		{
			uint num = 0U;
			uint num2 = 0U;
			for (;;)
			{
				uint num3 = (uint)input[(int)num++];
				if (num3 < 32U)
				{
					num3 += 1U;
					if ((ulong)(num2 + num3) > (ulong)((long)outputLength))
					{
						break;
					}
					do
					{
						output[(int)num2++] = input[(int)num++];
					}
					while ((num3 -= 1U) != 0U);
				}
				else
				{
					uint num4 = num3 >> 5;
					int num5 = (int)(num2 - ((num3 & 31U) << 8) - 1U);
					if (num4 == 7U)
					{
						num4 += (uint)input[(int)num++];
					}
					num5 -= (int)input[(int)num++];
					if ((ulong)(num2 + num4 + 2U) > (ulong)((long)outputLength))
					{
						return 0;
					}
					if (num5 < 0)
					{
						return 0;
					}
					output[(int)num2++] = output[num5++];
					output[(int)num2++] = output[num5++];
					do
					{
						output[(int)num2++] = output[num5++];
					}
					while ((num4 -= 1U) != 0U);
				}
				if ((ulong)num >= (ulong)((long)inputLength))
				{
					return (int)num2;
				}
			}
			return 0;
		}

		// Token: 0x04001880 RID: 6272
		private static readonly long[] HashTable = new long[16384];

		// Token: 0x04001881 RID: 6273
		private const uint HLOG = 14U;

		// Token: 0x04001882 RID: 6274
		private const uint HSIZE = 16384U;

		// Token: 0x04001883 RID: 6275
		private const uint MAX_LIT = 32U;

		// Token: 0x04001884 RID: 6276
		private const uint MAX_OFF = 8192U;

		// Token: 0x04001885 RID: 6277
		private const uint MAX_REF = 264U;
	}
}

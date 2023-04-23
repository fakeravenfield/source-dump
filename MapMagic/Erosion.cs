using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000560 RID: 1376
	public static class Erosion
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x000DF344 File Offset: 0x000DD544
		public static void ErosionIteration(Matrix heights, Matrix erosion, Matrix sedimentSum, CoordRect area = default(CoordRect), float erosionDurability = 0.9f, float erosionAmount = 1f, float sedimentAmount = 0.5f, int erosionFluidityIterations = 3, float ruffle = 0.1f, Matrix torrents = null, Matrix sediments = null, int[] stepsArray = null, int[] heightsInt = null, int[] order = null)
		{
			if (area.isZero)
			{
				area = heights.rect;
			}
			int count = heights.count;
			int num = 12345;
			int num2 = 1000000;
			if (heightsInt == null)
			{
				heightsInt = new int[count];
			}
			for (int i = 0; i < heights.count; i++)
			{
				heightsInt[i] = (int)(Mathf.Clamp01(heights.array[i]) * (float)num2);
			}
			if (order == null)
			{
				order = new int[count];
			}
			order = ArrayTools.Order(heightsInt, order, heights.count, 1000000, stepsArray);
			for (int j = 0; j < heights.count; j++)
			{
				int num3 = order[j];
				Coord coord = heights.rect.CoordByNum(num3);
				if (!area.CheckInRangeAndBounds(coord))
				{
					order[j] = -1;
				}
			}
			if (torrents == null)
			{
				torrents = new Matrix(heights.rect, null);
			}
			torrents.ChangeRect(heights.rect);
			torrents.Fill(1f);
			for (int k = count - 1; k >= 0; k--)
			{
				int num4 = order[k];
				if (num4 >= 0)
				{
					float[] array = heights.array;
					int num5 = num4;
					int x = heights.rect.size.x;
					float num6 = array[num5];
					float num7 = array[num5 - 1];
					float num8 = array[num5 + 1];
					float num9 = array[num5 - x];
					float num10 = array[num5 + x];
					float num11 = array[num5 - 1 - x];
					float num12 = array[num5 + 1 - x];
					float num13 = array[num5 - 1 + x];
					float num14 = array[num5 + 1 + x];
					float num15 = num6 - num6;
					float num16 = num6 - num7;
					float num17 = num6 - num8;
					float num18 = num6 - num9;
					float num19 = num6 - num10;
					float num20 = num6 - num11;
					float num21 = num6 - num12;
					float num22 = num6 - num13;
					float num23 = num6 - num14;
					num15 = ((num15 > 0f) ? num15 : 0f);
					num16 = ((num16 > 0f) ? num16 : 0f);
					num17 = ((num17 > 0f) ? num17 : 0f);
					num18 = ((num18 > 0f) ? num18 : 0f);
					num19 = ((num19 > 0f) ? num19 : 0f);
					num20 = ((num20 > 0f) ? num20 : 0f);
					num21 = ((num21 > 0f) ? num21 : 0f);
					num22 = ((num22 > 0f) ? num22 : 0f);
					num23 = ((num23 > 0f) ? num23 : 0f);
					float num24 = 0f;
					float num25 = 0f;
					float num26 = 0f;
					float num27 = 0f;
					float num28 = 0f;
					float num29 = 0f;
					float num30 = 0f;
					float num31 = 0f;
					float num32 = 0f;
					float num33 = num15 + num16 + num17 + num18 + num19 + num20 + num21 + num22 + num23;
					if (num33 > 1E-05f)
					{
						num24 = num15 / num33;
						num25 = num16 / num33;
						num26 = num17 / num33;
						num27 = num18 / num33;
						num28 = num19 / num33;
						num29 = num20 / num33;
						num30 = num21 / num33;
						num31 = num22 / num33;
						num32 = num23 / num33;
					}
					float num34 = torrents.array[num5];
					if (num34 > 2E+09f)
					{
						num34 = 2E+09f;
					}
					float[] array2 = torrents.array;
					array2[num5] += num34 * num24;
					array2[num5 - 1] += num34 * num25;
					array2[num5 + 1] += num34 * num26;
					array2[num5 - x] += num34 * num27;
					array2[num5 + x] += num34 * num28;
					array2[num5 - 1 - x] += num34 * num29;
					array2[num5 + 1 - x] += num34 * num30;
					array2[num5 - 1 + x] += num34 * num31;
					array2[num5 + 1 + x] += num34 * num32;
				}
			}
			if (sediments == null)
			{
				sediments = new Matrix(heights.rect, null);
			}
			else
			{
				sediments.ChangeRect(heights.rect);
			}
			sediments.Clear();
			for (int l = count - 1; l >= 0; l--)
			{
				int num35 = order[l];
				if (num35 >= 0)
				{
					float[] array3 = heights.array;
					int num36 = num35;
					int x2 = heights.rect.size.x;
					float num37 = array3[num36];
					float num38 = array3[num36 - 1];
					float num39 = array3[num36 + 1];
					float num40 = array3[num36 - x2];
					float num41 = array3[num36 + x2];
					float num42 = num37;
					if (num38 < num42)
					{
						num42 = num38;
					}
					if (num39 < num42)
					{
						num42 = num39;
					}
					if (num40 < num42)
					{
						num42 = num40;
					}
					if (num41 < num42)
					{
						num42 = num41;
					}
					float num43 = (num37 + num42) / 2f;
					if (num37 >= num43)
					{
						float num44 = num37 - num43;
						float num45 = num44 * (torrents.array[num35] - 1f) * (1f - erosionDurability);
						if (num44 > num45)
						{
							num44 = num45;
						}
						num44 *= erosionAmount;
						heights.array[num35] -= num44;
						sediments.array[num35] += num44 * sedimentAmount;
						if (erosion != null)
						{
							erosion.array[num35] += num44;
						}
					}
				}
			}
			for (int m = 0; m < erosionFluidityIterations; m++)
			{
				for (int n = count - 1; n >= 0; n--)
				{
					int num46 = order[n];
					if (num46 >= 0)
					{
						float[] array4 = heights.array;
						int x3 = heights.rect.size.x;
						float num47 = array4[num46];
						float num48 = array4[num46 - 1];
						float num49 = array4[num46 + 1];
						float num50 = array4[num46 - x3];
						float num51 = array4[num46 + x3];
						float[] array5 = sediments.array;
						float num52 = array5[num46];
						float num53 = array5[num46 - 1];
						float num54 = array5[num46 + 1];
						float num55 = array5[num46 - x3];
						float num56 = array5[num46 + x3];
						float num57 = num52 + num53 + num54 + num55 + num56;
						if (num57 >= 1E-05f)
						{
							num55 = (num56 = (num54 = (num53 = (num52 = num57 / 5f))));
							float num58 = (num47 + num52 + num53 + num48) / 2f;
							if (num47 + num52 > num48 + num53)
							{
								float num59 = num52 + num47 - num58;
								if (num59 > num52)
								{
									num59 = num52;
								}
								num52 -= num59;
								num53 += num59;
							}
							else
							{
								float num60 = num53 + num48 - num58;
								if (num60 > num53)
								{
									num60 = num53;
								}
								num53 -= num60;
								num52 += num60;
							}
							num58 = (num48 + num53 + num54 + num49) / 2f;
							if (num48 + num53 > num49 + num54)
							{
								float num61 = num53 + num48 - num58;
								if (num61 > num53)
								{
									num61 = num53;
								}
								num53 -= num61;
								num54 += num61;
							}
							else
							{
								float num62 = num54 + num49 - num58;
								if (num62 > num54)
								{
									num62 = num54;
								}
								num54 -= num62;
								num53 += num62;
							}
							num58 = (num47 + num52 + num54 + num49) / 2f;
							if (num47 + num52 > num49 + num54)
							{
								float num63 = num52 + num47 - num58;
								if (num63 > num52)
								{
									num63 = num52;
								}
								num52 -= num63;
								num54 += num63;
							}
							else
							{
								float num64 = num54 + num49 - num58;
								if (num64 > num54)
								{
									num64 = num54;
								}
								num54 -= num64;
								num52 += num64;
							}
							num58 = (num47 + num52 + num55 + num50) / 2f;
							if (num47 + num52 > num50 + num55)
							{
								float num65 = num52 + num47 - num58;
								if (num65 > num52)
								{
									num65 = num52;
								}
								num52 -= num65;
								num55 += num65;
							}
							else
							{
								float num66 = num55 + num50 - num58;
								if (num66 > num55)
								{
									num66 = num55;
								}
								num55 -= num66;
								num52 += num66;
							}
							num58 = (num51 + num56 + num55 + num50) / 2f;
							if (num51 + num56 > num50 + num55)
							{
								float num67 = num56 + num51 - num58;
								if (num67 > num56)
								{
									num67 = num56;
								}
								num56 -= num67;
								num55 += num67;
							}
							else
							{
								float num68 = num55 + num50 - num58;
								if (num68 > num55)
								{
									num68 = num55;
								}
								num55 -= num68;
								num56 += num68;
							}
							num58 = (num47 + num52 + num55 + num50) / 2f;
							if (num47 + num52 > num50 + num55)
							{
								float num69 = num52 + num47 - num58;
								if (num69 > num52)
								{
									num69 = num52;
								}
								num52 -= num69;
								num55 += num69;
							}
							else
							{
								float num70 = num55 + num50 - num58;
								if (num70 > num55)
								{
									num70 = num55;
								}
								num55 -= num70;
								num52 += num70;
							}
							num58 = (num48 + num53 + num55 + num50) / 2f;
							if (num48 + num53 > num50 + num55)
							{
								float num71 = num53 + num48 - num58;
								if (num71 > num53)
								{
									num71 = num53;
								}
								num53 -= num71;
								num55 += num71;
							}
							else
							{
								float num72 = num55 + num50 - num58;
								if (num72 > num55)
								{
									num72 = num55;
								}
								num55 -= num72;
								num53 += num72;
							}
							num58 = (num49 + num54 + num56 + num51) / 2f;
							if (num49 + num54 > num51 + num56)
							{
								float num73 = num54 + num49 - num58;
								if (num73 > num54)
								{
									num73 = num54;
								}
								num54 -= num73;
								num56 += num73;
							}
							else
							{
								float num74 = num56 + num51 - num58;
								if (num74 > num56)
								{
									num74 = num56;
								}
								num56 -= num74;
								num54 += num74;
							}
							num58 = (num48 + num53 + num56 + num51) / 2f;
							if (num48 + num53 > num51 + num56)
							{
								float num75 = num53 + num48 - num58;
								if (num75 > num53)
								{
									num75 = num53;
								}
								num53 -= num75;
								num56 += num75;
							}
							else
							{
								float num76 = num56 + num51 - num58;
								if (num76 > num56)
								{
									num76 = num56;
								}
								num56 -= num76;
								num53 += num76;
							}
							num58 = (num49 + num54 + num55 + num50) / 2f;
							if (num49 + num54 > num50 + num55)
							{
								float num77 = num54 + num49 - num58;
								if (num77 > num54)
								{
									num77 = num54;
								}
								num54 -= num77;
								num55 += num77;
							}
							else
							{
								float num78 = num55 + num50 - num58;
								if (num78 > num55)
								{
									num78 = num55;
								}
								num55 -= num78;
								num54 += num78;
							}
							float[] array6 = sediments.array;
							array6[num46] = num52;
							array6[num46 - 1] = num53;
							array6[num46 + 1] = num54;
							array6[num46 - x3] = num55;
							array6[num46 + x3] = num56;
							if (sedimentSum != null)
							{
								float[] array7 = sedimentSum.array;
								array7[num46] += num52;
								array7[num46 - 1] += num53;
								array7[num46 + 1] += num54;
								array7[num46 - x3] += num55;
								array7[num46 + x3] += num56;
							}
						}
					}
				}
			}
			for (int num79 = count - 1; num79 >= 0; num79--)
			{
				heights.array[num79] += sediments.array[num79];
				num = 214013 * num + 2531011;
				float num80 = (float)(num >> 16 & 32767) / 32768f;
				int num81 = order[num79];
				if (num81 >= 0)
				{
					float[] array8 = heights.array;
					int x4 = heights.rect.size.x;
					float num82 = array8[num81];
					float num83 = array8[num81 - 1];
					float num84 = array8[num81 + 1];
					float num85 = array8[num81 - x4];
					float num86 = array8[num81 + x4];
					float num87 = sediments.array[num81];
					if (num87 > 0.0001f)
					{
						float num88 = num87 / 2f;
						if (num88 > 0.75f)
						{
							num88 = 0.75f;
						}
						heights.array[num81] = num82 * (1f - num88) + (num83 + num84 + num85 + num86) / 4f * num88;
					}
					else
					{
						float num89 = num83;
						if (num84 > num89)
						{
							num89 = num84;
						}
						if (num85 > num89)
						{
							num89 = num85;
						}
						if (num86 > num89)
						{
							num89 = num86;
						}
						float num90 = num83;
						if (num84 < num90)
						{
							num90 = num84;
						}
						if (num85 < num90)
						{
							num90 = num85;
						}
						if (num86 < num90)
						{
							num90 = num86;
						}
						float num91 = num80 * (num89 - num90) + num90;
						heights.array[num81] = heights.array[num81] * (1f - ruffle) + num91 * ruffle;
					}
				}
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000DFF44 File Offset: 0x000DE144
		private static void LevelCells(float hX, float hz, ref float sX, ref float sz)
		{
			float num = (hX + sX + sz + hz) / 2f;
			if (hX + sX > hz + sz)
			{
				float num2 = sX + hX - num;
				if (num2 > sX)
				{
					num2 = sX;
				}
				sX -= num2;
				sz += num2;
				return;
			}
			float num3 = sz + hz - num;
			if (num3 > sz)
			{
				num3 = sz;
			}
			sz -= num3;
			sX += num3;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0001845A File Offset: 0x0001665A
		private static void LevelCells(float h1, float h2, float h3, ref float s1, ref float s2, ref float s3)
		{
			Erosion.LevelCells(h1, h2, ref s1, ref s2);
			Erosion.LevelCells(h2, h3, ref s2, ref s3);
			Erosion.LevelCells(h3, h1, ref s3, ref s1);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0000296E File Offset: 0x00000B6E
		private static void LevelCells(float h, float hx, float hX, float hz, float hZ, ref float s, ref float sx, ref float sX, ref float sz, ref float sZ)
		{
		}

		// Token: 0x02000561 RID: 1377
		private struct Cross
		{
			// Token: 0x060022B0 RID: 8880 RVA: 0x0001847B File Offset: 0x0001667B
			public Cross(float c, float px, float nx, float pz, float nz)
			{
				this.c = c;
				this.px = px;
				this.nx = nx;
				this.pz = pz;
				this.nz = nz;
			}

			// Token: 0x060022B1 RID: 8881 RVA: 0x000184A2 File Offset: 0x000166A2
			public Cross(Erosion.Cross src)
			{
				this.c = src.c;
				this.px = src.px;
				this.nx = src.nx;
				this.pz = src.pz;
				this.nz = src.nz;
			}

			// Token: 0x060022B2 RID: 8882 RVA: 0x000DFFA4 File Offset: 0x000DE1A4
			public Cross(Erosion.Cross c1, Erosion.Cross c2)
			{
				this.c = c1.c * c2.c;
				this.px = c1.px * c2.px;
				this.nx = c1.nx * c2.nx;
				this.pz = c1.pz * c2.pz;
				this.nz = c1.nz * c2.nz;
			}

			// Token: 0x060022B3 RID: 8883 RVA: 0x000184E0 File Offset: 0x000166E0
			public Cross(float[] m, int sizeX, int sizeZ, int i)
			{
				this.px = m[i - 1];
				this.c = m[i];
				this.nx = m[i + 1];
				this.pz = m[i - sizeX];
				this.nz = m[i + sizeX];
			}

			// Token: 0x060022B4 RID: 8884 RVA: 0x000E0010 File Offset: 0x000DE210
			public Cross(bool[] m, int sizeX, int sizeZ, int i)
			{
				this.px = (m[i - 1] ? 1f : 0f);
				this.c = (m[i] ? 1f : 0f);
				this.nx = (m[i + 1] ? 1f : 0f);
				this.pz = (m[i - sizeX] ? 1f : 0f);
				this.nz = (m[i + sizeX] ? 1f : 0f);
			}

			// Token: 0x060022B5 RID: 8885 RVA: 0x000E00A0 File Offset: 0x000DE2A0
			public Cross(Matrix m, int i)
			{
				this.px = m.array[i - 1];
				this.c = m.array[i];
				this.nx = m.array[i + 1];
				this.pz = m.array[i - m.rect.size.x];
				this.nz = m.array[i + m.rect.size.x];
			}

			// Token: 0x060022B6 RID: 8886 RVA: 0x0001851C File Offset: 0x0001671C
			public void ToMatrix(float[] m, int sizeX, int sizeZ, int i)
			{
				m[i - 1] = this.px;
				m[i] = this.c;
				m[i + 1] = this.nx;
				m[i - sizeX] = this.pz;
				m[i + sizeX] = this.nz;
			}

			// Token: 0x060022B7 RID: 8887 RVA: 0x000E011C File Offset: 0x000DE31C
			public void ToMatrix(Matrix m, int i)
			{
				m.array[i - 1] = this.px;
				m.array[i] = this.c;
				m.array[i + 1] = this.nx;
				m.array[i - m.rect.size.x] = this.pz;
				m.array[i + m.rect.size.x] = this.nz;
			}

			// Token: 0x060022B8 RID: 8888 RVA: 0x000E0198 File Offset: 0x000DE398
			public void Percent()
			{
				float num = this.c + this.px + this.nx + this.pz + this.nz;
				if (num > 1E-05f)
				{
					this.c /= num;
					this.px /= num;
					this.nx /= num;
					this.pz /= num;
					this.nz /= num;
					return;
				}
				this.c = 0f;
				this.px = 0f;
				this.nx = 0f;
				this.pz = 0f;
				this.nz = 0f;
			}

			// Token: 0x060022B9 RID: 8889 RVA: 0x000E0250 File Offset: 0x000DE450
			public void ClampPositive()
			{
				this.c = ((this.c < 0f) ? 0f : this.c);
				this.px = ((this.px < 0f) ? 0f : this.px);
				this.nx = ((this.nx < 0f) ? 0f : this.nx);
				this.pz = ((this.pz < 0f) ? 0f : this.pz);
				this.nz = ((this.nz < 0f) ? 0f : this.nz);
			}

			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x060022BA RID: 8890 RVA: 0x00018558 File Offset: 0x00016758
			public float max
			{
				get
				{
					return Mathf.Max(Mathf.Max(Mathf.Max(this.px, this.nx), Mathf.Max(this.pz, this.nz)), this.c);
				}
			}

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x060022BB RID: 8891 RVA: 0x0001858C File Offset: 0x0001678C
			public float min
			{
				get
				{
					return Mathf.Min(Mathf.Min(Mathf.Min(this.px, this.nx), Mathf.Min(this.pz, this.nz)), this.c);
				}
			}

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x060022BC RID: 8892 RVA: 0x000185C0 File Offset: 0x000167C0
			public float sum
			{
				get
				{
					return this.c + this.px + this.nx + this.pz + this.nz;
				}
			}

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x060022BD RID: 8893 RVA: 0x000185E4 File Offset: 0x000167E4
			public float avg
			{
				get
				{
					return (this.c + this.px + this.nx + this.pz + this.nz) / 5f;
				}
			}

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x060022BE RID: 8894 RVA: 0x0001860E File Offset: 0x0001680E
			public float avgAround
			{
				get
				{
					return (this.px + this.nx + this.pz + this.nz) / 4f;
				}
			}

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x060022BF RID: 8895 RVA: 0x00018631 File Offset: 0x00016831
			public float maxAround
			{
				get
				{
					return Mathf.Max(Mathf.Max(this.px, this.nx), Mathf.Max(this.pz, this.nz));
				}
			}

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0001865A File Offset: 0x0001685A
			public float minAround
			{
				get
				{
					return Mathf.Min(Mathf.Min(this.px, this.nx), Mathf.Min(this.pz, this.nz));
				}
			}

			// Token: 0x060022C1 RID: 8897 RVA: 0x000E0300 File Offset: 0x000DE500
			public void Multiply(Erosion.Cross c2)
			{
				this.c *= c2.c;
				this.px *= c2.px;
				this.nx *= c2.nx;
				this.pz *= c2.pz;
				this.nz *= c2.nz;
			}

			// Token: 0x060022C2 RID: 8898 RVA: 0x000E036C File Offset: 0x000DE56C
			public void Multiply(float f)
			{
				this.c *= f;
				this.px *= f;
				this.nx *= f;
				this.pz *= f;
				this.nz *= f;
			}

			// Token: 0x060022C3 RID: 8899 RVA: 0x000E03C0 File Offset: 0x000DE5C0
			public void Add(Erosion.Cross c2)
			{
				this.c += c2.c;
				this.px += c2.px;
				this.nx += c2.nx;
				this.pz += c2.pz;
				this.nz += c2.nz;
			}

			// Token: 0x060022C4 RID: 8900 RVA: 0x000E042C File Offset: 0x000DE62C
			public void Divide(Erosion.Cross c2)
			{
				this.c /= c2.c;
				this.px /= c2.px;
				this.nx /= c2.nx;
				this.pz /= c2.pz;
				this.nz /= c2.nz;
			}

			// Token: 0x060022C5 RID: 8901 RVA: 0x000E0498 File Offset: 0x000DE698
			public void Divide(float f)
			{
				this.c /= f;
				this.px /= f;
				this.nx /= f;
				this.pz /= f;
				this.nz /= f;
			}

			// Token: 0x060022C6 RID: 8902 RVA: 0x000E04EC File Offset: 0x000DE6EC
			public void Subtract(float f)
			{
				this.c -= f;
				this.px -= f;
				this.nx -= f;
				this.pz -= f;
				this.nz -= f;
			}

			// Token: 0x060022C7 RID: 8903 RVA: 0x000E0540 File Offset: 0x000DE740
			public void SubtractInverse(float f)
			{
				this.c = f - this.c;
				this.px = f - this.px;
				this.nx = f - this.nx;
				this.pz = f - this.pz;
				this.nz = f - this.nz;
			}

			// Token: 0x060022C8 RID: 8904 RVA: 0x000E0594 File Offset: 0x000DE794
			public float GetMultipliedMax(Erosion.Cross c2)
			{
				return Mathf.Max(Mathf.Max(Mathf.Max(this.px * c2.px, this.nx * c2.nx), Mathf.Max(this.pz * c2.pz, this.nz * c2.nz)), this.c * c2.c);
			}

			// Token: 0x060022C9 RID: 8905 RVA: 0x000E05F8 File Offset: 0x000DE7F8
			public float GetMultipliedSum(Erosion.Cross c2)
			{
				return this.c * c2.c + this.px * c2.px + this.nx * c2.nx + this.pz * c2.pz + this.nz * c2.nz;
			}

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x060022CA RID: 8906 RVA: 0x000E064C File Offset: 0x000DE84C
			public bool isNaN
			{
				get
				{
					return float.IsNaN(this.c) || float.IsNaN(this.px) || float.IsNaN(this.pz) || float.IsNaN(this.nx) || float.IsNaN(this.nz);
				}
			}

			// Token: 0x170002BA RID: 698
			public float this[int n]
			{
				get
				{
					switch (n)
					{
					case 0:
						return this.c;
					case 1:
						return this.px;
					case 2:
						return this.nx;
					case 3:
						return this.pz;
					case 4:
						return this.nz;
					default:
						return this.c;
					}
				}
				set
				{
					switch (n)
					{
					case 0:
						this.c = value;
						return;
					case 1:
						this.px = value;
						return;
					case 2:
						this.nx = value;
						return;
					case 3:
						this.pz = value;
						return;
					case 4:
						this.nz = value;
						return;
					default:
						this.c = value;
						return;
					}
				}
			}

			// Token: 0x060022CD RID: 8909 RVA: 0x000E0748 File Offset: 0x000DE948
			public void SortByHeight(int[] sorted)
			{
				for (int i = 0; i < 5; i++)
				{
					sorted[i] = i;
				}
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (this[sorted[k]] > this[sorted[k + 1]])
						{
							int num = sorted[k];
							sorted[k] = sorted[k + 1];
							sorted[k + 1] = num;
						}
					}
				}
			}

			// Token: 0x060022CE RID: 8910 RVA: 0x00018683 File Offset: 0x00016883
			public IEnumerable<int> Sorted()
			{
				float _c = this.c;
				float _px = this.px;
				float _nx = this.nx;
				float _pz = this.pz;
				float _nz = this.nz;
				if (this.c > this.px && this.c > this.nx && this.c > this.pz && this.c > this.nz)
				{
					_c = 0f;
					yield return 0;
					if (this.px > this.nx && this.px > this.pz && this.px > this.nz)
					{
						_px = 0f;
						yield return 1;
					}
					else if (this.nx > this.px && this.nx > this.pz && this.nx > this.nz)
					{
						_nx = 0f;
						yield return 2;
					}
					else if (this.pz > this.px && this.pz > this.nx && this.pz > this.nz)
					{
						_pz = 0f;
						yield return 3;
					}
					else if (this.nz > this.px && this.nz > this.nx && this.nz > this.pz)
					{
						_nz = 0f;
						yield return 4;
					}
				}
				if (this.px > this.c && this.px > this.nx && this.px > this.pz && this.px > this.nz)
				{
					_px = 0f;
					yield return 1;
					if (this.c > this.nx && this.c > this.pz && this.c > this.nz)
					{
						_c = 0f;
						yield return 0;
					}
					else if (this.nx > this.c && this.nx > this.pz && this.nx > this.nz)
					{
						_nx = 0f;
						yield return 2;
					}
					else if (this.pz > this.c && this.pz > this.nx && this.pz > this.nz)
					{
						_pz = 0f;
						yield return 3;
					}
					else if (this.nz > this.c && this.nz > this.nx && this.nz > this.pz)
					{
						_nz = 0f;
						yield return 4;
					}
				}
				if (this.nx > this.c && this.nx > this.px && this.nx > this.pz && this.nx > this.nz)
				{
					_nx = 0f;
					yield return 2;
					if (this.c > this.px && this.c > this.pz && this.c > this.nz)
					{
						_c = 0f;
						yield return 0;
					}
					else if (this.px > this.c && this.px > this.pz && this.px > this.nz)
					{
						_px = 0f;
						yield return 1;
					}
					else if (this.pz > this.c && this.pz > this.px && this.pz > this.nz)
					{
						_pz = 0f;
						yield return 3;
					}
					else if (this.nz > this.c && this.nz > this.px && this.nz > this.pz)
					{
						_nz = 0f;
						yield return 4;
					}
				}
				if (this.pz > this.c && this.pz > this.px && this.pz > this.nx && this.pz > this.nz)
				{
					_pz = 0f;
					yield return 3;
					if (this.c > this.px && this.c > this.nx && this.c > this.nz)
					{
						_c = 0f;
						yield return 0;
					}
					else if (this.px > this.c && this.px > this.nx && this.px > this.nz)
					{
						_px = 0f;
						yield return 1;
					}
					else if (this.nx > this.c && this.nx > this.px && this.nx > this.nz)
					{
						_nx = 0f;
						yield return 2;
					}
					else if (this.nz > this.c && this.nz > this.px && this.nz > this.nx)
					{
						_nz = 0f;
						yield return 4;
					}
				}
				if (this.nz > this.c && this.nz > this.px && this.nz > this.nx && this.nz > this.pz)
				{
					_nz = 0f;
					yield return 4;
					if (this.c > this.px && this.c > this.nx && this.c > this.pz)
					{
						_c = 0f;
						yield return 0;
					}
					else if (this.px > this.c && this.px > this.nx && this.px > this.pz)
					{
						_px = 0f;
						yield return 1;
					}
					else if (this.nx > this.c && this.nx > this.px && this.nx > this.pz)
					{
						_nx = 0f;
						yield return 2;
					}
					else if (this.pz > this.c && this.pz > this.px && this.pz > this.nx)
					{
						_pz = 0f;
						yield return 3;
					}
				}
				int num;
				for (int i = 0; i < 3; i = num + 1)
				{
					if (_c > _px && _c > _nx && _c > _pz && _c > _nz)
					{
						_c = 0f;
						yield return 0;
					}
					else if (_px > _c && _px > _nx && _px > _pz && _px > _nz)
					{
						_px = 0f;
						yield return 1;
					}
					else if (_nx > _c && _nx > _px && _nx > _pz && _nx > _nz)
					{
						_nx = 0f;
						yield return 2;
					}
					else if (_pz > _c && _pz > _px && _pz > _nx && _pz > _nz)
					{
						_pz = 0f;
						yield return 3;
					}
					else if (_nz > _c && _nz > _px && _nz > _nx && _nz > _pz)
					{
						_nz = 0f;
						yield return 4;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x060022CF RID: 8911 RVA: 0x000E07A8 File Offset: 0x000DE9A8
			public static Erosion.Cross operator +(Erosion.Cross c1, Erosion.Cross c2)
			{
				return new Erosion.Cross(c1.c + c2.c, c1.px + c2.px, c1.nx + c2.nx, c1.pz + c2.pz, c1.nz + c2.nz);
			}

			// Token: 0x060022D0 RID: 8912 RVA: 0x00018698 File Offset: 0x00016898
			public static Erosion.Cross operator +(Erosion.Cross c1, float f)
			{
				return new Erosion.Cross(c1.c + f, c1.px + f, c1.nx + f, c1.pz + f, c1.nz + f);
			}

			// Token: 0x060022D1 RID: 8913 RVA: 0x000E07FC File Offset: 0x000DE9FC
			public static Erosion.Cross operator -(Erosion.Cross c1, Erosion.Cross c2)
			{
				return new Erosion.Cross(c1.c - c2.c, c1.px - c2.px, c1.nx - c2.nx, c1.pz - c2.pz, c1.nz - c2.nz);
			}

			// Token: 0x060022D2 RID: 8914 RVA: 0x000186C7 File Offset: 0x000168C7
			public static Erosion.Cross operator -(float f, Erosion.Cross c2)
			{
				return new Erosion.Cross(f - c2.c, f - c2.px, f - c2.nx, f - c2.pz, f - c2.nz);
			}

			// Token: 0x060022D3 RID: 8915 RVA: 0x000186F6 File Offset: 0x000168F6
			public static Erosion.Cross operator -(Erosion.Cross c1, float f)
			{
				return new Erosion.Cross(c1.c - f, c1.px - f, c1.nx - f, c1.pz - f, c1.nz - f);
			}

			// Token: 0x060022D4 RID: 8916 RVA: 0x000E0850 File Offset: 0x000DEA50
			public static Erosion.Cross operator *(Erosion.Cross c1, Erosion.Cross c2)
			{
				return new Erosion.Cross(c1.c * c2.c, c1.px * c2.px, c1.nx * c2.nx, c1.pz * c2.pz, c1.nz * c2.nz);
			}

			// Token: 0x060022D5 RID: 8917 RVA: 0x00018725 File Offset: 0x00016925
			public static Erosion.Cross operator *(float f, Erosion.Cross c2)
			{
				return new Erosion.Cross(f * c2.c, f * c2.px, f * c2.nx, f * c2.pz, f * c2.nz);
			}

			// Token: 0x060022D6 RID: 8918 RVA: 0x00018754 File Offset: 0x00016954
			public static Erosion.Cross operator *(Erosion.Cross c1, float f)
			{
				return new Erosion.Cross(c1.c * f, c1.px * f, c1.nx * f, c1.pz * f, c1.nz * f);
			}

			// Token: 0x060022D7 RID: 8919 RVA: 0x000E08A4 File Offset: 0x000DEAA4
			public static Erosion.Cross operator /(Erosion.Cross c1, float f)
			{
				if (f > 1E-05f)
				{
					return new Erosion.Cross(c1.c / f, c1.px / f, c1.nx / f, c1.pz / f, c1.nz / f);
				}
				return new Erosion.Cross(0f, 0f, 0f, 0f, 0f);
			}

			// Token: 0x060022D8 RID: 8920 RVA: 0x000E0908 File Offset: 0x000DEB08
			public Erosion.Cross PercentObsolete()
			{
				float num = this.c + this.px + this.nx + this.pz + this.nz;
				if (num > 1E-05f)
				{
					return new Erosion.Cross(this.c / num, this.px / num, this.nx / num, this.pz / num, this.nz / num);
				}
				return new Erosion.Cross(0f, 0f, 0f, 0f, 0f);
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x000E098C File Offset: 0x000DEB8C
			public Erosion.Cross ClampPositiveObsolete()
			{
				return new Erosion.Cross((this.c < 0f) ? 0f : this.c, (this.px < 0f) ? 0f : this.px, (this.nx < 0f) ? 0f : this.nx, (this.pz < 0f) ? 0f : this.pz, (this.nz < 0f) ? 0f : this.nz);
			}

			// Token: 0x04002279 RID: 8825
			public float c;

			// Token: 0x0400227A RID: 8826
			public float px;

			// Token: 0x0400227B RID: 8827
			public float nx;

			// Token: 0x0400227C RID: 8828
			public float pz;

			// Token: 0x0400227D RID: 8829
			public float nz;
		}

		// Token: 0x02000563 RID: 1379
		private struct MooreCross
		{
			// Token: 0x060022E2 RID: 8930 RVA: 0x000E18B4 File Offset: 0x000DFAB4
			public MooreCross(float c, float px, float nx, float pz, float nz, float pxpz, float nxpz, float pxnz, float nxnz)
			{
				this.c = c;
				this.px = px;
				this.nx = nx;
				this.pz = pz;
				this.nz = nz;
				this.pxpz = pxpz;
				this.nxpz = nxpz;
				this.pxnz = pxnz;
				this.nxnz = nxnz;
			}

			// Token: 0x060022E3 RID: 8931 RVA: 0x000E1908 File Offset: 0x000DFB08
			public MooreCross(Erosion.MooreCross src)
			{
				this.c = src.c;
				this.px = src.px;
				this.nx = src.nx;
				this.pz = src.pz;
				this.nz = src.nz;
				this.pxpz = src.pxpz;
				this.nxpz = src.nxpz;
				this.pxnz = src.pxnz;
				this.nxnz = src.nxnz;
			}

			// Token: 0x060022E4 RID: 8932 RVA: 0x000E1984 File Offset: 0x000DFB84
			public MooreCross(float[] m, int sizeX, int sizeZ, int i)
			{
				this.px = m[i - 1];
				this.c = m[i];
				this.nx = m[i + 1];
				this.pz = m[i - sizeX];
				this.nz = m[i + sizeX];
				this.pxpz = m[i - 1 - sizeX];
				this.nxpz = m[i + 1 - sizeX];
				this.pxnz = m[i - 1 + sizeX];
				this.nxnz = m[i + 1 + sizeX];
			}

			// Token: 0x060022E5 RID: 8933 RVA: 0x000E1A04 File Offset: 0x000DFC04
			public MooreCross(Matrix m, int i)
			{
				this.px = m.array[i - 1];
				this.c = m.array[i];
				this.nx = m.array[i + 1];
				this.pz = m.array[i - m.rect.size.x];
				this.nz = m.array[i + m.rect.size.x];
				this.pxpz = m.array[i - 1 - m.rect.size.x];
				this.nxpz = m.array[i + 1 - m.rect.size.x];
				this.pxnz = m.array[i - 1 + m.rect.size.x];
				this.nxnz = m.array[i + 1 + m.rect.size.x];
			}

			// Token: 0x060022E6 RID: 8934 RVA: 0x000E1B04 File Offset: 0x000DFD04
			public void ToMatrix(float[] m, int sizeX, int sizeZ, int i)
			{
				m[i - 1] = this.px;
				m[i] = this.c;
				m[i + 1] = this.nx;
				m[i - sizeX] = this.pz;
				m[i + sizeX] = this.nz;
				m[i - 1 - sizeX] = this.pxpz;
				m[i + 1 - sizeX] = this.nxpz;
				m[i - 1 + sizeX] = this.pxnz;
				m[i + 1 + sizeX] = this.nxnz;
			}

			// Token: 0x060022E7 RID: 8935 RVA: 0x000E1B84 File Offset: 0x000DFD84
			public void ToMatrix(Matrix m, int i)
			{
				m.array[i - 1] = this.px;
				m.array[i] = this.c;
				m.array[i + 1] = this.nx;
				m.array[i - m.rect.size.x] = this.pz;
				m.array[i + m.rect.size.x] = this.nz;
				m.array[i - 1 - m.rect.size.x] = this.pxpz;
				m.array[i + 1 - m.rect.size.x] = this.nxpz;
				m.array[i - 1 + m.rect.size.x] = this.pxnz;
				m.array[i + 1 + m.rect.size.x] = this.nxnz;
			}

			// Token: 0x060022E8 RID: 8936 RVA: 0x000E1C84 File Offset: 0x000DFE84
			public void Percent()
			{
				float num = this.c + this.px + this.nx + this.pz + this.nz + this.pxpz + this.nxpz + this.pxnz + this.nxnz;
				if (num > 1E-05f)
				{
					this.c /= num;
					this.px /= num;
					this.nx /= num;
					this.pz /= num;
					this.nz /= num;
					this.pxpz /= num;
					this.nxpz /= num;
					this.pxnz /= num;
					this.nxnz /= num;
					return;
				}
				this.c = 0f;
				this.px = 0f;
				this.nx = 0f;
				this.pz = 0f;
				this.nz = 0f;
				this.pxpz = 0f;
				this.nxpz = 0f;
				this.pxnz = 0f;
				this.nxnz = 0f;
			}

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x060022E9 RID: 8937 RVA: 0x000E1DBC File Offset: 0x000DFFBC
			public bool isNaN
			{
				get
				{
					return float.IsNaN(this.c) || float.IsNaN(this.px) || float.IsNaN(this.pz) || float.IsNaN(this.nx) || float.IsNaN(this.nz) || float.IsNaN(this.pxpz) || float.IsNaN(this.pxnz) || float.IsNaN(this.nxpz) || float.IsNaN(this.nxnz);
				}
			}

			// Token: 0x060022EA RID: 8938 RVA: 0x000E1E40 File Offset: 0x000E0040
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"MooreCross: ",
					this.c.ToString(),
					", ",
					this.px.ToString(),
					", ",
					this.pz.ToString(),
					", ",
					this.nx.ToString(),
					", ",
					this.nz.ToString(),
					", ",
					this.pxpz.ToString(),
					", ",
					this.nxpz.ToString(),
					", ",
					this.pxnz.ToString(),
					", ",
					this.nxnz.ToString()
				});
			}

			// Token: 0x060022EB RID: 8939 RVA: 0x000E1F28 File Offset: 0x000E0128
			public void ClampPositive()
			{
				this.c = ((this.c < 0f) ? 0f : this.c);
				this.px = ((this.px < 0f) ? 0f : this.px);
				this.nx = ((this.nx < 0f) ? 0f : this.nx);
				this.pz = ((this.pz < 0f) ? 0f : this.pz);
				this.nz = ((this.nz < 0f) ? 0f : this.nz);
				this.pxpz = ((this.pxpz < 0f) ? 0f : this.pxpz);
				this.nxpz = ((this.nxpz < 0f) ? 0f : this.nxpz);
				this.pxnz = ((this.pxnz < 0f) ? 0f : this.pxnz);
				this.nxnz = ((this.nxnz < 0f) ? 0f : this.nxnz);
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x060022EC RID: 8940 RVA: 0x000187BA File Offset: 0x000169BA
			public float max
			{
				get
				{
					return Mathf.Max(Mathf.Max(Mathf.Max(this.px, this.nx), Mathf.Max(this.pz, this.nz)), this.c);
				}
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x060022ED RID: 8941 RVA: 0x000187EE File Offset: 0x000169EE
			public float min
			{
				get
				{
					return Mathf.Min(Mathf.Min(Mathf.Min(this.px, this.nx), Mathf.Min(this.pz, this.nz)), this.c);
				}
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x060022EE RID: 8942 RVA: 0x00018822 File Offset: 0x00016A22
			public float sum
			{
				get
				{
					return this.c + this.px + this.nx + this.pz + this.nz;
				}
			}

			// Token: 0x060022EF RID: 8943 RVA: 0x000E2058 File Offset: 0x000E0258
			public void Multiply(float f)
			{
				this.c *= f;
				this.px *= f;
				this.nx *= f;
				this.pz *= f;
				this.nz *= f;
				this.pxpz *= f;
				this.nxpz *= f;
				this.pxnz *= f;
				this.nxnz *= f;
			}

			// Token: 0x060022F0 RID: 8944 RVA: 0x000E20E4 File Offset: 0x000E02E4
			public void Add(float f)
			{
				this.c += f;
				this.px += f;
				this.nx += f;
				this.pz += f;
				this.nz += f;
				this.pxpz += f;
				this.nxpz += f;
				this.pxnz += f;
				this.nxnz += f;
			}

			// Token: 0x060022F1 RID: 8945 RVA: 0x000E2170 File Offset: 0x000E0370
			public void Add(Erosion.MooreCross c2)
			{
				this.c += c2.c;
				this.px += c2.px;
				this.nx += c2.nx;
				this.pz += c2.pz;
				this.nz += c2.nz;
				this.pxpz += c2.pxpz;
				this.nxpz += c2.nxpz;
				this.pxnz += c2.pxnz;
				this.nxnz += c2.nxnz;
			}

			// Token: 0x060022F2 RID: 8946 RVA: 0x000E2228 File Offset: 0x000E0428
			public void Subtract(float f)
			{
				this.c -= f;
				this.px -= f;
				this.nx -= f;
				this.pz -= f;
				this.nz -= f;
				this.pxpz -= f;
				this.nxpz -= f;
				this.pxnz -= f;
				this.nxnz -= f;
			}

			// Token: 0x060022F3 RID: 8947 RVA: 0x000E22B4 File Offset: 0x000E04B4
			public void SubtractInverse(float f)
			{
				this.c = f - this.c;
				this.px = f - this.px;
				this.nx = f - this.nx;
				this.pz = f - this.pz;
				this.nz = f - this.nz;
				this.pxpz = f - this.pxpz;
				this.nxpz = f - this.nxpz;
				this.pxnz = f - this.pxnz;
				this.nxnz = f - this.nxnz;
			}

			// Token: 0x060022F4 RID: 8948 RVA: 0x000E2340 File Offset: 0x000E0540
			public static Erosion.MooreCross operator +(Erosion.MooreCross c1, Erosion.MooreCross c2)
			{
				return new Erosion.MooreCross(c1.c + c2.c, c1.px + c2.px, c1.nx + c2.nx, c1.pz + c2.pz, c1.nz + c2.nz, c1.pxpz + c2.pxpz, c1.nxpz + c2.nxpz, c1.pxnz + c2.pxnz, c1.nxnz + c2.nxnz);
			}

			// Token: 0x060022F5 RID: 8949 RVA: 0x000E23C8 File Offset: 0x000E05C8
			public static Erosion.MooreCross operator +(Erosion.MooreCross c1, float f)
			{
				return new Erosion.MooreCross(c1.c + f, c1.px + f, c1.nx + f, c1.pz + f, c1.nz + f, c1.pxpz + f, c1.nxpz + f, c1.pxnz + f, c1.nxnz + f);
			}

			// Token: 0x060022F6 RID: 8950 RVA: 0x000E2424 File Offset: 0x000E0624
			public static Erosion.MooreCross operator -(Erosion.MooreCross c1, Erosion.MooreCross c2)
			{
				return new Erosion.MooreCross(c1.c - c2.c, c1.px - c2.px, c1.nx - c2.nx, c1.pz - c2.pz, c1.nz - c2.nz, c1.pxpz - c2.pxpz, c1.nxpz - c2.nxpz, c1.pxnz - c2.pxnz, c1.nxnz - c2.nxnz);
			}

			// Token: 0x060022F7 RID: 8951 RVA: 0x000E24AC File Offset: 0x000E06AC
			public static Erosion.MooreCross operator -(float f, Erosion.MooreCross c2)
			{
				return new Erosion.MooreCross(f - c2.c, f - c2.px, f - c2.nx, f - c2.pz, f - c2.nz, f - c2.pxpz, f - c2.nxpz, f - c2.pxnz, f - c2.nxnz);
			}

			// Token: 0x060022F8 RID: 8952 RVA: 0x000E2508 File Offset: 0x000E0708
			public static Erosion.MooreCross operator -(Erosion.MooreCross c1, float f)
			{
				return new Erosion.MooreCross(c1.c - f, c1.px - f, c1.nx - f, c1.pz - f, c1.nz - f, c1.pxpz - f, c1.nxpz - f, c1.pxnz - f, c1.nxnz - f);
			}

			// Token: 0x060022F9 RID: 8953 RVA: 0x000E2564 File Offset: 0x000E0764
			public static Erosion.MooreCross operator *(Erosion.MooreCross c1, Erosion.MooreCross c2)
			{
				return new Erosion.MooreCross(c1.c * c2.c, c1.px * c2.px, c1.nx * c2.nx, c1.pz * c2.pz, c1.nz * c2.nz, c1.pxpz * c2.pxpz, c1.nxpz * c2.nxpz, c1.pxnz * c2.pxnz, c1.nxnz * c2.nxnz);
			}

			// Token: 0x060022FA RID: 8954 RVA: 0x000E25EC File Offset: 0x000E07EC
			public static Erosion.MooreCross operator *(float f, Erosion.MooreCross c2)
			{
				return new Erosion.MooreCross(f * c2.c, f * c2.px, f * c2.nx, f * c2.pz, f * c2.nz, f * c2.pxpz, f * c2.nxpz, f * c2.pxnz, f * c2.nxnz);
			}

			// Token: 0x060022FB RID: 8955 RVA: 0x000E2648 File Offset: 0x000E0848
			public static Erosion.MooreCross operator *(Erosion.MooreCross c1, float f)
			{
				return new Erosion.MooreCross(c1.c * f, c1.px * f, c1.nx * f, c1.pz * f, c1.nz * f, c1.pxpz * f, c1.nxpz * f, c1.pxnz * f, c1.nxnz * f);
			}

			// Token: 0x060022FC RID: 8956 RVA: 0x000E26A4 File Offset: 0x000E08A4
			public static Erosion.MooreCross operator /(Erosion.MooreCross c1, float f)
			{
				if (f > 1E-05f)
				{
					return new Erosion.MooreCross(c1.c / f, c1.px / f, c1.nx / f, c1.pz / f, c1.nz / f, c1.pxpz / f, c1.nxpz / f, c1.pxnz / f, c1.nxnz / f);
				}
				return new Erosion.MooreCross(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
			}

			// Token: 0x060022FD RID: 8957 RVA: 0x000E273C File Offset: 0x000E093C
			public Erosion.MooreCross PercentObsolete()
			{
				float num = this.c + this.px + this.nx + this.pz + this.nz + this.pxpz + this.nxpz + this.pxnz + this.nxnz;
				if (num > 1E-05f)
				{
					return new Erosion.MooreCross(this.c / num, this.px / num, this.nx / num, this.pz / num, this.nz / num, this.pxpz / num, this.nxpz / num, this.pxnz / num, this.nxnz / num);
				}
				return new Erosion.MooreCross(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
			}

			// Token: 0x060022FE RID: 8958 RVA: 0x000E2810 File Offset: 0x000E0A10
			public Erosion.MooreCross ClampPositiveObsolete()
			{
				return new Erosion.MooreCross((this.c < 0f) ? 0f : this.c, (this.px < 0f) ? 0f : this.px, (this.nx < 0f) ? 0f : this.nx, (this.pz < 0f) ? 0f : this.pz, (this.nz < 0f) ? 0f : this.nz, (this.pxpz < 0f) ? 0f : this.pxpz, (this.nxpz < 0f) ? 0f : this.nxpz, (this.pxnz < 0f) ? 0f : this.pxnz, (this.nxnz < 0f) ? 0f : this.nxnz);
			}

			// Token: 0x04002289 RID: 8841
			public float c;

			// Token: 0x0400228A RID: 8842
			public float px;

			// Token: 0x0400228B RID: 8843
			public float nx;

			// Token: 0x0400228C RID: 8844
			public float pxpz;

			// Token: 0x0400228D RID: 8845
			public float nxpz;

			// Token: 0x0400228E RID: 8846
			public float pz;

			// Token: 0x0400228F RID: 8847
			public float nz;

			// Token: 0x04002290 RID: 8848
			public float pxnz;

			// Token: 0x04002291 RID: 8849
			public float nxnz;
		}
	}
}

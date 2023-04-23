using System;
using System.IO;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000587 RID: 1415
	[Serializable]
	public class Matrix : Matrix2<float>
	{
		// Token: 0x06002435 RID: 9269 RVA: 0x000E8DA0 File Offset: 0x000E6FA0
		public float GetInterpolatedValue(Vector2 pos)
		{
			int num = Mathf.FloorToInt(pos.x);
			int num2 = Mathf.FloorToInt(pos.y);
			float num3 = pos.x - (float)num;
			float num4 = pos.y - (float)num2;
			float num5 = base[num, num2];
			float num6 = base[num + 1, num2];
			float num7 = num5 * (1f - num3) + num6 * num3;
			float num8 = base[num, num2 + 1];
			float num9 = base[num + 1, num2 + 1];
			float num10 = num8 * (1f - num3) + num9 * num3;
			return num7 * (1f - num4) + num10 * num4;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000E8E30 File Offset: 0x000E7030
		public float GetAveragedValue(int x, int z, int steps)
		{
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < steps; i++)
			{
				for (int j = 0; j < steps; j++)
				{
					if (x + i < this.rect.offset.x + this.rect.size.x && z + j < this.rect.offset.z + this.rect.size.z)
					{
						num += base[x + i, z + j];
						num2++;
					}
				}
			}
			return num / (float)num2;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0001918D File Offset: 0x0001738D
		public Matrix()
		{
			this.array = new float[0];
			this.rect = new CoordRect(0, 0, 0, 0);
			this.count = 0;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000E8EC0 File Offset: 0x000E70C0
		public Matrix(CoordRect rect, float[] array = null)
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
			this.array = new float[this.count];
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000E8F58 File Offset: 0x000E7158
		public Matrix(Coord offset, Coord size, float[] array = null)
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
			this.array = new float[this.count];
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000191B7 File Offset: 0x000173B7
		public override object Clone()
		{
			return this.Copy(null);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000E9000 File Offset: 0x000E7200
		public Matrix Copy(Matrix result = null)
		{
			if (result == null)
			{
				result = new Matrix(this.rect, null);
			}
			result.rect = this.rect;
			result.pos = this.pos;
			result.count = this.count;
			if (result.array.Length != this.array.Length)
			{
				result.array = new float[this.array.Length];
			}
			for (int i = 0; i < this.array.Length; i++)
			{
				result.array[i] = this.array[i];
			}
			return result;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000E908C File Offset: 0x000E728C
		public bool[] InRect(CoordRect area = default(CoordRect))
		{
			Matrix2<bool> matrix = new Matrix2<bool>(this.rect, null);
			CoordRect coordRect = CoordRect.Intersect(this.rect, area);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					matrix[i, j] = true;
				}
			}
			return matrix.array;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000E9108 File Offset: 0x000E7308
		public void Fill(float[,] array, CoordRect arrayRect)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, arrayRect);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					base[i, j] = array[j - arrayRect.offset.z, i - arrayRect.offset.x];
				}
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000E918C File Offset: 0x000E738C
		public void Pour(float[,] array, CoordRect arrayRect)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, arrayRect);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					array[j - arrayRect.offset.z, i - arrayRect.offset.x] = base[i, j];
				}
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000E9210 File Offset: 0x000E7410
		public void Pour(float[,,] array, int channel, CoordRect arrayRect)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, arrayRect);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					array[j - arrayRect.offset.z, i - arrayRect.offset.x, channel] = base[i, j];
				}
			}
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000E9298 File Offset: 0x000E7498
		public float[,] ReadHeighmap(TerrainData data, float height = 1f)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, new CoordRect(0, 0, data.heightmapResolution, data.heightmapResolution));
			float[,] heights = data.GetHeights(coordRect.offset.x, coordRect.offset.z, coordRect.size.x, coordRect.size.z);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					base[i, j] = heights[j - min.z, i - min.x] * height;
				}
			}
			base.RemoveBorders(coordRect);
			return heights;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000E9368 File Offset: 0x000E7568
		public void WriteHeightmap(TerrainData data, float[,] array = null, float brushFallof = 0.5f, bool delayLod = false)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, new CoordRect(0, 0, data.heightmapResolution, data.heightmapResolution));
			if (array == null || array.Length != coordRect.size.x * coordRect.size.z)
			{
				array = new float[coordRect.size.z, coordRect.size.x];
			}
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = this.Fallof(i, j, brushFallof);
					if (!Mathf.Approximately(num, 0f))
					{
						array[j - min.z, i - min.x] = base[i, j] * num + array[j - min.z, i - min.x] * (1f - num);
					}
				}
			}
			if (delayLod)
			{
				data.SetHeightsDelayLOD(coordRect.offset.x, coordRect.offset.z, array);
				return;
			}
			data.SetHeights(coordRect.offset.x, coordRect.offset.z, array);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000E94B4 File Offset: 0x000E76B4
		public float[,,] ReadSplatmap(TerrainData data, int channel, float[,,] array = null)
		{
			CoordRect coordRect = CoordRect.Intersect(this.rect, new CoordRect(0, 0, data.alphamapResolution, data.alphamapResolution));
			if (array == null)
			{
				array = data.GetAlphamaps(coordRect.offset.x, coordRect.offset.z, coordRect.size.x, coordRect.size.z);
			}
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					base[i, j] = array[j - min.z, i - min.x, channel];
				}
			}
			base.RemoveBorders(coordRect);
			return array;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000E9580 File Offset: 0x000E7780
		public static void AddSplatmaps(TerrainData data, Matrix[] matrices, int[] channels, float[] opacity, float[,,] array = null, float brushFallof = 0.5f)
		{
			int alphamapLayers = data.alphamapLayers;
			bool[] array2 = new bool[alphamapLayers];
			for (int i = 0; i < channels.Length; i++)
			{
				array2[channels[i]] = true;
			}
			float[] array3 = new float[alphamapLayers];
			Coord size = new Coord(data.alphamapResolution, data.alphamapResolution);
			CoordRect coordRect = CoordRect.Intersect(new CoordRect(new Coord(0, 0), size), matrices[0].rect);
			if (array == null)
			{
				array = data.GetAlphamaps(coordRect.offset.x, coordRect.offset.z, coordRect.size.x, coordRect.size.z);
			}
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int j = min.x; j < max.x; j++)
			{
				for (int k = min.z; k < max.z; k++)
				{
					float num = matrices[0].Fallof(j, k, brushFallof);
					if (!Mathf.Approximately(num, 0f))
					{
						for (int l = 0; l < alphamapLayers; l++)
						{
							array3[l] = array[k - min.z, j - min.x, l];
						}
						for (int m = 0; m < matrices.Length; m++)
						{
							matrices[m][j, k] = Mathf.Max(0f, matrices[m][j, k] - array3[channels[m]]);
						}
						for (int n = 0; n < matrices.Length; n++)
						{
							Matrix matrix = matrices[n];
							int num2 = j;
							int num3 = k;
							matrix[num2, num3] *= num * opacity[n];
						}
						float num4 = 0f;
						for (int num5 = 0; num5 < matrices.Length; num5++)
						{
							num4 += matrices[num5][j, k];
						}
						if (num4 > 1f)
						{
							foreach (Matrix matrix in matrices)
							{
								int num3 = j;
								int num2 = k;
								matrix[num3, num2] /= num4;
							}
							num4 = 1f;
						}
						float num7 = 1f - num4;
						for (int num8 = 0; num8 < alphamapLayers; num8++)
						{
							array3[num8] *= num7;
						}
						for (int num9 = 0; num9 < matrices.Length; num9++)
						{
							array3[channels[num9]] += matrices[num9][j, k];
						}
						for (int num10 = 0; num10 < alphamapLayers; num10++)
						{
							array[k - min.z, j - min.x, num10] = array3[num10];
						}
					}
				}
			}
			data.SetAlphamaps(coordRect.offset.x, coordRect.offset.z, array);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000E9864 File Offset: 0x000E7A64
		public void ToTexture(Texture2D texture = null, Color[] colors = null, float rangeMin = 0f, float rangeMax = 1f, bool resizeTexture = false)
		{
			if (texture == null)
			{
				texture = new Texture2D(this.rect.size.x, this.rect.size.z);
			}
			if (resizeTexture)
			{
				texture.Resize(this.rect.size.x, this.rect.size.z);
			}
			Coord size = new Coord(texture.width, texture.height);
			CoordRect coordRect = CoordRect.Intersect(new CoordRect(new Coord(0, 0), size), this.rect);
			if (colors == null || colors.Length != coordRect.size.x * coordRect.size.z)
			{
				colors = new Color[coordRect.size.x * coordRect.size.z];
			}
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = (base[i, j] - rangeMin) / (rangeMax - rangeMin) * 256f;
					int num2 = (int)num;
					float num3 = num - (float)num2;
					float num4 = (float)num2 / 256f;
					float num5 = (float)(num2 + 1) / 256f;
					int num6 = i - min.x;
					int num7 = j - min.z;
					colors[num7 * (max.x - min.x) + num6] = new Color(num4, (num3 > 0.333f) ? num5 : num4, (num3 > 0.666f) ? num5 : num4);
				}
			}
			texture.SetPixels(coordRect.offset.x, coordRect.offset.z, coordRect.size.x, coordRect.size.z, colors);
			texture.Apply();
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000E9A4C File Offset: 0x000E7C4C
		public void FromTexture(Texture2D texture)
		{
			CoordRect coordRect = CoordRect.Intersect(new CoordRect(0, 0, texture.width, texture.height), this.rect);
			Color[] pixels = texture.GetPixels(coordRect.offset.x, coordRect.offset.z, coordRect.size.x, coordRect.size.z);
			Coord min = coordRect.Min;
			Coord max = coordRect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					int num = i - min.x;
					int num2 = j - min.z;
					Color color = pixels[num2 * (max.x - min.x) + num];
					base[i, j] = (color.r + color.g + color.b) / 3f;
				}
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000E9B4C File Offset: 0x000E7D4C
		public void FromTextureTiled(Texture2D texture)
		{
			Color[] pixels = texture.GetPixels();
			int width = texture.width;
			int height = texture.height;
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					int num = i % width;
					if (num < 0)
					{
						num += width;
					}
					int num2 = j % height;
					if (num2 < 0)
					{
						num2 += height;
					}
					Color color = pixels[num2 * width + num];
					base[i, j] = (color.r + color.g + color.b) / 3f;
				}
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000E9C18 File Offset: 0x000E7E18
		public Texture2D SimpleToTexture(Texture2D texture = null, Color[] colors = null, float rangeMin = 0f, float rangeMax = 1f, string savePath = null)
		{
			if (texture == null)
			{
				texture = new Texture2D(this.rect.size.x, this.rect.size.z);
			}
			if (texture.width != this.rect.size.x || texture.height != this.rect.size.z)
			{
				texture.Resize(this.rect.size.x, this.rect.size.z);
			}
			if (colors == null || colors.Length != this.rect.size.x * this.rect.size.z)
			{
				colors = new Color[this.rect.size.x * this.rect.size.z];
			}
			for (int i = 0; i < this.count; i++)
			{
				float num = this.array[i];
				num -= rangeMin;
				num /= rangeMax - rangeMin;
				colors[i] = new Color(num, num, num);
			}
			texture.SetPixels(colors);
			texture.Apply();
			return texture;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000E9D40 File Offset: 0x000E7F40
		public void SimpleFromTexture(Texture2D texture)
		{
			base.ChangeRect(new CoordRect(this.rect.offset.x, this.rect.offset.z, texture.width, texture.height));
			Color[] pixels = texture.GetPixels();
			for (int i = 0; i < this.count; i++)
			{
				Color color = pixels[i];
				this.array[i] = (color.r + color.g + color.b) / 3f;
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000E9DC8 File Offset: 0x000E7FC8
		public void ImportRaw(string path)
		{
			FileStream fileStream = new FileInfo(path).Open(FileMode.Open, FileAccess.Read);
			int num = (int)Mathf.Sqrt((float)(fileStream.Length / 2L));
			byte[] array = new byte[num * num * 2];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			base.ChangeRect(new CoordRect(0, 0, num, num));
			int num2 = 0;
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = max.z - 1; i >= min.z; i--)
			{
				for (int j = min.x; j < max.x; j++)
				{
					base[j, i] = ((float)array[num2 + 1] * 256f + (float)array[num2]) / 65535f;
					num2 += 2;
				}
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000E9E98 File Offset: 0x000E8098
		public void Replicate(Matrix source, bool tile = false)
		{
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					if (source.rect.CheckInRange(i, j))
					{
						base[i, j] = source[i, j];
					}
					else if (tile)
					{
						int num = i - source.rect.offset.x;
						int num2 = j - source.rect.offset.z;
						int num3 = num % source.rect.size.x;
						int num4 = num2 % source.rect.size.z;
						if (num3 < 0)
						{
							num3 += source.rect.size.x;
						}
						if (num4 < 0)
						{
							num4 += source.rect.size.z;
						}
						int x = num3 + source.rect.offset.x;
						int z = num4 + source.rect.offset.z;
						base[i, j] = source[x, z];
					}
				}
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000E9FE0 File Offset: 0x000E81E0
		public float GetArea(int x, int z, int range)
		{
			if (range == 0)
			{
				if (x < this.rect.offset.x)
				{
					x = this.rect.offset.x;
				}
				if (x >= this.rect.offset.x + this.rect.size.x)
				{
					x = this.rect.offset.x + this.rect.size.x - 1;
				}
				if (z < this.rect.offset.z)
				{
					z = this.rect.offset.z;
				}
				if (z >= this.rect.offset.z + this.rect.size.z)
				{
					z = this.rect.offset.z + this.rect.size.z - 1;
				}
				return this.array[(z - this.rect.offset.z) * this.rect.size.x + x - this.rect.offset.x];
			}
			float num = 0f;
			int num2 = 0;
			for (int i = x - range; i <= x + range; i++)
			{
				if (i >= this.rect.offset.x && i < this.rect.offset.x + this.rect.size.x)
				{
					for (int j = z - range; j <= z + range; j++)
					{
						if (j >= this.rect.offset.z && j < this.rect.offset.z + this.rect.size.z)
						{
							num += this.array[(j - this.rect.offset.z) * this.rect.size.x + i - this.rect.offset.x];
							num2++;
						}
					}
				}
			}
			return num / (float)num2;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000EA1FC File Offset: 0x000E83FC
		public float GetInterpolated(float x, float z, Matrix.WrapMode wrap = Matrix.WrapMode.Once)
		{
			if (wrap == Matrix.WrapMode.Once && (x < (float)this.rect.offset.x || x >= (float)(this.rect.offset.x + this.rect.size.x) || z < (float)this.rect.offset.z || z >= (float)(this.rect.offset.z + this.rect.size.z)))
			{
				return 0f;
			}
			int num = (int)x;
			if (x < 0f)
			{
				num--;
			}
			int num2 = num + 1;
			int num3 = (int)z;
			if (z < 0f)
			{
				num3--;
			}
			int num4 = num3 + 1;
			int num5 = num - this.rect.offset.x;
			int num6 = num2 - this.rect.offset.x;
			int num7 = num3 - this.rect.offset.z;
			int num8 = num4 - this.rect.offset.z;
			if (wrap == Matrix.WrapMode.Clamp || wrap == Matrix.WrapMode.Once)
			{
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num5 >= this.rect.size.x)
				{
					num5 = this.rect.size.x - 1;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num6 >= this.rect.size.x)
				{
					num6 = this.rect.size.x - 1;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num7 >= this.rect.size.z)
				{
					num7 = this.rect.size.z - 1;
				}
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num8 >= this.rect.size.z)
				{
					num8 = this.rect.size.z - 1;
				}
			}
			else if (wrap == Matrix.WrapMode.Tile)
			{
				num5 %= this.rect.size.x;
				if (num5 < 0)
				{
					num5 = this.rect.size.x + num5;
				}
				num7 %= this.rect.size.z;
				if (num7 < 0)
				{
					num7 = this.rect.size.z + num7;
				}
				num6 %= this.rect.size.x;
				if (num6 < 0)
				{
					num6 = this.rect.size.x + num6;
				}
				num8 %= this.rect.size.z;
				if (num8 < 0)
				{
					num8 = this.rect.size.z + num8;
				}
			}
			else if (wrap == Matrix.WrapMode.PingPong)
			{
				num5 %= this.rect.size.x * 2;
				if (num5 < 0)
				{
					num5 = this.rect.size.x * 2 + num5;
				}
				if (num5 >= this.rect.size.x)
				{
					num5 = this.rect.size.x * 2 - num5 - 1;
				}
				num7 %= this.rect.size.z * 2;
				if (num7 < 0)
				{
					num7 = this.rect.size.z * 2 + num7;
				}
				if (num7 >= this.rect.size.z)
				{
					num7 = this.rect.size.z * 2 - num7 - 1;
				}
				num6 %= this.rect.size.x * 2;
				if (num6 < 0)
				{
					num6 = this.rect.size.x * 2 + num6;
				}
				if (num6 >= this.rect.size.x)
				{
					num6 = this.rect.size.x * 2 - num6 - 1;
				}
				num8 %= this.rect.size.z * 2;
				if (num8 < 0)
				{
					num8 = this.rect.size.z * 2 + num8;
				}
				if (num8 >= this.rect.size.z)
				{
					num8 = this.rect.size.z * 2 - num8 - 1;
				}
			}
			float num9 = this.array[num7 * this.rect.size.x + num5];
			float num10 = this.array[num7 * this.rect.size.x + num6];
			float num11 = this.array[num8 * this.rect.size.x + num5];
			float num12 = this.array[num8 * this.rect.size.x + num6];
			float num13 = x - (float)num;
			float num14 = z - (float)num3;
			float num15 = num9 * (1f - num13) + num10 * num13;
			float num16 = num11 * (1f - num13) + num12 * num13;
			return num15 * (1f - num14) + num16 * num14;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000EA6C0 File Offset: 0x000E88C0
		public Matrix Resize(CoordRect newRect, Matrix result = null)
		{
			if (result == null)
			{
				result = new Matrix(newRect, null);
			}
			else
			{
				result.ChangeRect(newRect);
			}
			Coord min = result.rect.Min;
			Coord max = result.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float x = 1f * (float)(i - result.rect.offset.x) / (float)result.rect.size.x * (float)this.rect.size.x + (float)this.rect.offset.x;
					float z = 1f * (float)(j - result.rect.offset.z) / (float)result.rect.size.z * (float)this.rect.size.z + (float)this.rect.offset.z;
					result[i, j] = this.GetInterpolated(x, z, Matrix.WrapMode.Once);
				}
			}
			return result;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000191C0 File Offset: 0x000173C0
		public Matrix Downscale(int factor, Matrix result = null)
		{
			return this.Resize(this.rect / factor, result);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000191D5 File Offset: 0x000173D5
		public Matrix Upscale(int factor, Matrix result = null)
		{
			return this.Resize(this.rect * factor, result);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000EA7E8 File Offset: 0x000E89E8
		public Matrix BlurredUpscale(int factor)
		{
			Matrix matrix = new Matrix(this.rect, new float[this.count * factor]);
			Matrix matrix2 = new Matrix(this.rect, new float[this.count * factor]);
			matrix.Fill(this, false);
			int num = Mathf.RoundToInt(Mathf.Sqrt((float)factor));
			for (int i = 0; i < num; i++)
			{
				matrix.Resize(matrix.rect * 2, matrix2);
				matrix.ChangeRect(matrix2.rect);
				matrix.Fill(matrix2, false);
				matrix.Blur(null, 0.5f, false, false, true, true, null);
			}
			return matrix;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000EA884 File Offset: 0x000E8A84
		public Matrix OutdatedResize(CoordRect newRect, float smoothness = 1f, Matrix result = null)
		{
			int num = newRect.size.x / this.rect.size.x;
			int num2 = this.rect.size.x / newRect.size.x;
			if (num > 1 && !newRect.Divisible((float)num))
			{
				string[] array = new string[6];
				array[0] = "Matrix rect ";
				int num3 = 1;
				CoordRect coordRect = this.rect;
				array[num3] = coordRect.ToString();
				array[2] = " could not be upscaled to ";
				int num4 = 3;
				coordRect = newRect;
				array[num4] = coordRect.ToString();
				array[4] = " with factor ";
				array[5] = num.ToString();
				Debug.LogError(string.Concat(array));
			}
			if (num2 > 1 && !this.rect.Divisible((float)num2))
			{
				string[] array2 = new string[6];
				array2[0] = "Matrix rect ";
				int num5 = 1;
				CoordRect coordRect = this.rect;
				array2[num5] = coordRect.ToString();
				array2[2] = " could not be downscaled to ";
				int num6 = 3;
				coordRect = newRect;
				array2[num6] = coordRect.ToString();
				array2[4] = " with factor ";
				array2[5] = num2.ToString();
				Debug.LogError(string.Concat(array2));
			}
			if (num > 1)
			{
				result = this.OutdatedUpscale(num, result);
			}
			if (num2 > 1)
			{
				result = this.OutdatedDownscale(num2, smoothness, result);
			}
			if (num <= 1 && num2 <= 1)
			{
				return this.Copy(result);
			}
			return result;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000EA9D4 File Offset: 0x000E8BD4
		public Matrix OutdatedUpscale(int factor, Matrix result = null)
		{
			if (result == null)
			{
				result = new Matrix(this.rect * factor, null);
			}
			result.ChangeRect(this.rect * factor);
			if (factor == 1)
			{
				return this.Copy(result);
			}
			Coord min = this.rect.Min;
			Coord coord = this.rect.Max - 1;
			float num = 1f / (float)factor;
			for (int i = min.x; i < coord.x; i++)
			{
				for (int j = min.z; j < coord.z; j++)
				{
					float a = base[i, j];
					float a2 = base[i + 1, j];
					float b = base[i, j + 1];
					float b2 = base[i + 1, j + 1];
					for (int k = 0; k < factor; k++)
					{
						for (int l = 0; l < factor; l++)
						{
							float t = (float)k * num;
							float t2 = (float)l * num;
							float a3 = Mathf.Lerp(a, b, t2);
							float b3 = Mathf.Lerp(a2, b2, t2);
							result[i * factor + k, j * factor + l] = Mathf.Lerp(a3, b3, t);
						}
					}
				}
			}
			result.RemoveBorders(0, 0, factor + 1, factor + 1);
			return result;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000EAB24 File Offset: 0x000E8D24
		public float OutdatedGetInterpolated(float x, float z)
		{
			int num = (int)x;
			int num2 = (int)(x + 1f);
			if (num2 >= this.rect.offset.x + this.rect.size.x)
			{
				num2 = this.rect.offset.x + this.rect.size.x - 1;
			}
			int num3 = (int)z;
			int num4 = (int)(z + 1f);
			if (num4 >= this.rect.offset.z + this.rect.size.z)
			{
				num4 = this.rect.offset.z + this.rect.size.z - 1;
			}
			float num5 = x - (float)num;
			float num6 = z - (float)num3;
			int num7 = (num3 - this.rect.offset.z) * this.rect.size.x + num - this.rect.offset.x;
			float num8 = this.array[num7];
			float num9 = this.array[(num3 - this.rect.offset.z) * this.rect.size.x + num2 - this.rect.offset.x];
			float num10 = this.array[(num4 - this.rect.offset.z) * this.rect.size.x + num - this.rect.offset.x];
			float num11 = this.array[(num4 - this.rect.offset.z) * this.rect.size.x + num2 - this.rect.offset.x];
			float num12 = num8 * (1f - num5) + num9 * num5;
			float num13 = num10 * (1f - num5) + num11 * num5;
			return num12 * (1f - num6) + num13 * num6;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000EAD14 File Offset: 0x000E8F14
		public Matrix TestResize(CoordRect newRect)
		{
			Matrix matrix = new Matrix(newRect, null);
			Coord min = matrix.rect.Min;
			Coord max = matrix.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float x = 1f * (float)(i - matrix.rect.offset.x) / (float)matrix.rect.size.x * (float)this.rect.size.x + (float)this.rect.offset.x;
					float z = 1f * (float)(j - matrix.rect.offset.z) / (float)matrix.rect.size.z * (float)this.rect.size.z + (float)this.rect.offset.z;
					matrix[i, j] = this.OutdatedGetInterpolated(x, z);
				}
			}
			return matrix;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000EAE34 File Offset: 0x000E9034
		public Matrix OutdatedDownscale(int factor = 2, float smoothness = 1f, Matrix result = null)
		{
			if (!this.rect.Divisible((float)factor))
			{
				string str = "Matrix rect ";
				CoordRect rect = this.rect;
				Debug.LogError(str + rect.ToString() + " could not be downscaled with factor " + factor.ToString());
			}
			if (result == null)
			{
				result = new Matrix(this.rect / factor, null);
			}
			result.ChangeRect(this.rect / factor);
			if (factor == 1)
			{
				return this.Copy(result);
			}
			Coord min = this.rect.Min;
			Coord min2 = result.rect.Min;
			Coord max = result.rect.Max;
			if (smoothness < 0.0001f)
			{
				for (int i = min2.x; i < max.x; i++)
				{
					for (int j = min2.z; j < max.z; j++)
					{
						int x = (i - min2.x) * factor + min.x;
						int z = (j - min2.z) * factor + min.z;
						result[i, j] = base[x, z];
					}
				}
			}
			else
			{
				for (int k = min2.x; k < max.x; k++)
				{
					for (int l = min2.z; l < max.z; l++)
					{
						int num = (k - min2.x) * factor + min.x;
						int num2 = (l - min2.z) * factor + min.z;
						float num3 = 0f;
						for (int m = num; m < num + factor; m++)
						{
							for (int n = num2; n < num2 + factor; n++)
							{
								num3 += base[m, n];
							}
						}
						result[k, l] = num3 / (float)(factor * factor) * smoothness + base[num, num2] * (1f - smoothness);
					}
				}
			}
			return result;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000EB024 File Offset: 0x000E9224
		public void Spread(float strength = 0.5f, int iterations = 4, Matrix copy = null)
		{
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = Mathf.Clamp(this.array[i], -1f, 1f);
			}
			if (copy == null)
			{
				copy = this.Copy(null);
			}
			else
			{
				for (int j = 0; j < this.count; j++)
				{
					copy.array[j] = this.array[j];
				}
			}
			for (int k = 0; k < iterations; k++)
			{
				for (int l = min.x; l < max.x; l++)
				{
					float num = base[l, min.z];
					base.SetPos(l, min.z);
					for (int m = min.z + 1; m < max.z; m++)
					{
						num = (num + this.array[this.pos]) / 2f;
						this.array[this.pos] = num;
						this.pos += this.rect.size.x;
					}
					num = base[l, max.z - 1];
					base.SetPos(l, max.z - 1);
					for (int n = max.z - 2; n >= min.z; n--)
					{
						num = (num + this.array[this.pos]) / 2f;
						this.array[this.pos] = num;
						this.pos -= this.rect.size.x;
					}
				}
				for (int num2 = min.z; num2 < max.z; num2++)
				{
					float num = base[min.x, num2];
					base.SetPos(min.x, num2);
					for (int num3 = min.x + 1; num3 < max.x; num3++)
					{
						num = (num + this.array[this.pos]) / 2f;
						this.array[this.pos] = num;
						this.pos++;
					}
					num = base[max.x - 1, num2];
					base.SetPos(max.x - 1, num2);
					for (int num4 = max.x - 2; num4 >= min.x; num4--)
					{
						num = (num + this.array[this.pos]) / 2f;
						this.array[this.pos] = num;
						this.pos--;
					}
				}
			}
			for (int num5 = 0; num5 < this.count; num5++)
			{
				this.array[num5] = copy.array[num5] + this.array[num5] * 2f * strength;
			}
			float num6 = Mathf.Sqrt((float)iterations);
			for (int num7 = 0; num7 < this.count; num7++)
			{
				this.array[num7] /= num6;
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000EB358 File Offset: 0x000E9558
		public void Spread(Func<float, float, float> spreadFn = null, int iterations = 4)
		{
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = 0; i < iterations; i++)
			{
				for (int j = min.x; j < max.x; j++)
				{
					float num = base[j, min.z];
					base.SetPos(j, min.z);
					for (int k = min.z + 1; k < max.z; k++)
					{
						num = spreadFn(num, this.array[this.pos]);
						this.array[this.pos] = num;
						this.pos += this.rect.size.x;
					}
					num = base[j, max.z - 1];
					base.SetPos(j, max.z - 1);
					for (int l = max.z - 2; l >= min.z; l--)
					{
						num = spreadFn(num, this.array[this.pos]);
						this.array[this.pos] = num;
						this.pos -= this.rect.size.x;
					}
				}
				for (int m = min.z; m < max.z; m++)
				{
					float num = base[min.x, m];
					base.SetPos(min.x, m);
					for (int n = min.x + 1; n < max.x; n++)
					{
						num = spreadFn(num, this.array[this.pos]);
						this.array[this.pos] = num;
						this.pos++;
					}
					num = base[max.x - 1, m];
					base.SetPos(max.x - 1, m);
					for (int num2 = max.x - 2; num2 >= min.x; num2--)
					{
						num = spreadFn(num, this.array[this.pos]);
						this.array[this.pos] = num;
						this.pos--;
					}
				}
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000EB5A4 File Offset: 0x000E97A4
		public void Blur(Func<float, float, float, float> blurFn = null, float intensity = 0.666f, bool additive = false, bool takemax = false, bool horizontal = true, bool vertical = true, Matrix reference = null)
		{
			if (reference == null)
			{
				reference = this;
			}
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			if (horizontal)
			{
				for (int i = min.z; i < max.z; i++)
				{
					base.SetPos(min.x, i);
					float num = reference[min.x, i];
					float num2 = num;
					float num3 = num;
					for (int j = min.x; j < max.x; j++)
					{
						num = num2;
						num2 = num3;
						if (j < max.x - 1)
						{
							num3 = reference.array[this.pos + 1];
						}
						float num4;
						if (blurFn == null)
						{
							num4 = (num + num3) / 2f;
						}
						else
						{
							num4 = blurFn(num, num2, num3);
						}
						num4 = num2 * (1f - intensity) + num4 * intensity;
						if (additive)
						{
							this.array[this.pos] += num4;
						}
						else
						{
							this.array[this.pos] = num4;
						}
						this.pos++;
					}
				}
			}
			if (vertical)
			{
				for (int k = min.x; k < max.x; k++)
				{
					base.SetPos(k, min.z);
					float num5 = reference[k, min.z];
					float num6 = num5;
					for (int l = min.z; l < max.z; l++)
					{
						float num7 = num6;
						num6 = num5;
						if (l < max.z - 1)
						{
							num5 = reference.array[this.pos + this.rect.size.x];
						}
						float num8;
						if (blurFn == null)
						{
							num8 = (num7 + num5) / 2f;
						}
						else
						{
							num8 = blurFn(num7, num6, num5);
						}
						num8 = num6 * (1f - intensity) + num8 * intensity;
						if (additive)
						{
							this.array[this.pos] += num8;
						}
						else if (takemax)
						{
							if (num8 > this.array[this.pos])
							{
								this.array[this.pos] = num8;
							}
						}
						else
						{
							this.array[this.pos] = num8;
						}
						this.pos += this.rect.size.x;
					}
				}
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000EB810 File Offset: 0x000E9A10
		public void LossBlur(int step = 2, bool horizontal = true, bool vertical = true, Matrix reference = null)
		{
			if (reference == null)
			{
				reference = this;
			}
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			int num = step + step / 2;
			if (horizontal)
			{
				for (int i = min.z; i < max.z; i++)
				{
					base.SetPos(min.x, i);
					float num2 = 0f;
					int num3 = 0;
					float num4 = this.array[this.pos];
					float num5 = this.array[this.pos];
					for (int j = min.x; j < max.x + num; j++)
					{
						if (j < max.x)
						{
							num2 += reference.array[this.pos];
						}
						num3++;
						if (j % step == 0)
						{
							num5 = num4;
							if (j < max.x)
							{
								num4 = num2 / (float)num3;
							}
							num2 = 0f;
							num3 = 0;
						}
						if (j - num >= min.x)
						{
							float num6 = 1f * (float)(j % step) / (float)step;
							if (num6 < 0f)
							{
								num6 += 1f;
							}
							this.array[this.pos - num] = num4 * num6 + num5 * (1f - num6);
						}
						this.pos++;
					}
				}
			}
			if (vertical)
			{
				for (int k = min.x; k < max.x; k++)
				{
					base.SetPos(k, min.z);
					float num7 = 0f;
					int num8 = 0;
					float num9 = this.array[this.pos];
					float num10 = this.array[this.pos];
					for (int l = min.z; l < max.z + num; l++)
					{
						if (l < max.z)
						{
							num7 += reference.array[this.pos];
						}
						num8++;
						if (l % step == 0)
						{
							num10 = num9;
							if (l < max.z)
							{
								num9 = num7 / (float)num8;
							}
							num7 = 0f;
							num8 = 0;
						}
						if (l - num >= min.z)
						{
							float num11 = 1f * (float)(l % step) / (float)step;
							if (num11 < 0f)
							{
								num11 += 1f;
							}
							this.array[this.pos - num * this.rect.size.x] = num9 * num11 + num10 * (1f - num11);
						}
						this.pos += this.rect.size.x;
					}
				}
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000EBAA8 File Offset: 0x000E9CA8
		public static void BlendLayers(Matrix[] matrices, float[] opacity = null)
		{
			Coord min = matrices[0].rect.Min;
			Coord max = matrices[0].rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = 0f;
					for (int k = matrices.Length - 1; k >= 0; k--)
					{
						float num2 = matrices[k][i, j];
						if (opacity != null)
						{
							num2 *= opacity[k];
						}
						float num3 = Mathf.Clamp01(num + num2 - 1f);
						matrices[k][i, j] = num2 - num3;
						num += num2 - num3;
					}
				}
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000EBB60 File Offset: 0x000E9D60
		public static void NormalizeLayers(Matrix[] matrices, float[] opacity)
		{
			Coord min = matrices[0].rect.Min;
			Coord max = matrices[0].rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					float num = 0f;
					for (int k = 0; k < matrices.Length; k++)
					{
						num += matrices[k][i, j];
					}
					if (num > 1f)
					{
						foreach (Matrix matrix in matrices)
						{
							int x = i;
							int z = j;
							matrix[x, z] /= num;
						}
					}
				}
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000EBC24 File Offset: 0x000E9E24
		public float Fallof(int x, int z, float fallof)
		{
			if (fallof < 0f)
			{
				return 1f;
			}
			float num = (float)this.rect.size.x / 2f - 1f;
			float num2 = ((float)x - ((float)this.rect.offset.x + num)) / num;
			float num3 = (float)this.rect.size.z / 2f - 1f;
			float num4 = ((float)z - ((float)this.rect.offset.z + num3)) / num3;
			float num5 = Mathf.Sqrt(num2 * num2 + num4 * num4);
			float num6 = Mathf.Clamp01((1f - num5) / (1f - fallof));
			return 3f * num6 * num6 - 2f * num6 * num6 * num6;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000EBCE8 File Offset: 0x000E9EE8
		public void FillEmpty()
		{
			Coord min = this.rect.Min;
			Coord max = this.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				float num = 0f;
				for (int j = min.z; j < max.z; j++)
				{
					float num2 = this.array[(j - this.rect.offset.z) * this.rect.size.x + i - this.rect.offset.x];
					if (num2 > 0.0001f)
					{
						num = num2;
					}
					else if (num > 0.0001f)
					{
						this.array[(j - this.rect.offset.z) * this.rect.size.x + i - this.rect.offset.x] = num;
					}
				}
			}
			for (int k = min.z; k < max.z; k++)
			{
				float num = 0f;
				for (int l = min.x; l < max.x; l++)
				{
					float num3 = this.array[(k - this.rect.offset.z) * this.rect.size.x + l - this.rect.offset.x];
					if (num3 > 0.0001f)
					{
						num = num3;
					}
					else if (num > 0.0001f)
					{
						this.array[(k - this.rect.offset.z) * this.rect.size.x + l - this.rect.offset.x] = num;
					}
				}
			}
			for (int m = min.x; m < max.x; m++)
			{
				float num = 0f;
				for (int n = max.z - 1; n > min.z; n--)
				{
					float num4 = this.array[(n - this.rect.offset.z) * this.rect.size.x + m - this.rect.offset.x];
					if (num4 > 0.0001f)
					{
						num = num4;
					}
					else if (num > 0.0001f)
					{
						this.array[(n - this.rect.offset.z) * this.rect.size.x + m - this.rect.offset.x] = num;
					}
				}
			}
			for (int num5 = min.z; num5 < max.z; num5++)
			{
				float num = 0f;
				for (int num6 = max.x - 1; num6 >= min.x; num6--)
				{
					float num7 = this.array[(num5 - this.rect.offset.z) * this.rect.size.x + num6 - this.rect.offset.x];
					if (num7 > 0.0001f)
					{
						num = num7;
					}
					else if (num > 0.0001f)
					{
						this.array[(num5 - this.rect.offset.z) * this.rect.size.x + num6 - this.rect.offset.x] = num;
					}
				}
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000EC088 File Offset: 0x000EA288
		public static void Blend(Matrix src, Matrix dst, float factor)
		{
			if (dst.rect != src.rect)
			{
				Debug.LogError("Matrix Blend: maps have different sizes");
			}
			for (int i = 0; i < dst.count; i++)
			{
				dst.array[i] = dst.array[i] * factor + src.array[i] * (1f - factor);
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000EC0E8 File Offset: 0x000EA2E8
		public static void Mask(Matrix src, Matrix dst, Matrix mask)
		{
			if (src != null && (dst.rect != src.rect || dst.rect != mask.rect))
			{
				Debug.LogError("Matrix Mask: maps have different sizes");
			}
			for (int i = 0; i < dst.count; i++)
			{
				float num = mask.array[i];
				if (num <= 1f && num >= 0f)
				{
					dst.array[i] = dst.array[i] * num + ((src == null) ? 0f : (src.array[i] * (1f - num)));
				}
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000EC180 File Offset: 0x000EA380
		public static void SafeBorders(Matrix src, Matrix dst, int safeBorders)
		{
			Coord min = dst.rect.Min;
			Coord max = dst.rect.Max;
			for (int i = min.x; i < max.x; i++)
			{
				for (int j = min.z; j < max.z; j++)
				{
					int num = Mathf.Min(Mathf.Min(i - min.x, max.x - i), Mathf.Min(j - min.z, max.z - j));
					float num2 = 1f * (float)num / (float)safeBorders;
					if (num2 <= 1f)
					{
						dst[i, j] = dst[i, j] * num2 + ((src == null) ? 0f : (src[i, j] * (1f - num2)));
					}
				}
			}
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000EC254 File Offset: 0x000EA454
		public void Add(Matrix add)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] += add.array[i];
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000EC28C File Offset: 0x000EA48C
		public void Add(Matrix add, Matrix mask)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] += add.array[i] * mask.array[i];
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000EC2CC File Offset: 0x000EA4CC
		public void Add(float add)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] += add;
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000EC2FC File Offset: 0x000EA4FC
		public void Subtract(Matrix m)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] -= m.array[i];
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000EC334 File Offset: 0x000EA534
		public void InvSubtract(Matrix m)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = m.array[i] - this.array[i];
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000EC36C File Offset: 0x000EA56C
		public void ClampSubtract(Matrix m)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = Mathf.Clamp01(this.array[i] - m.array[i]);
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000EC3A8 File Offset: 0x000EA5A8
		public void Multiply(Matrix m)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] *= m.array[i];
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000EC3E0 File Offset: 0x000EA5E0
		public void Multiply(float m)
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] *= m;
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000EC410 File Offset: 0x000EA610
		public bool CheckRange(float min, float max)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.array[i] < min || this.array[i] > max)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000EC448 File Offset: 0x000EA648
		public void Invert()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = -this.array[i];
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000EC478 File Offset: 0x000EA678
		public void InvertOne()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.array[i] = 1f - this.array[i];
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000EC4AC File Offset: 0x000EA6AC
		public void Clamp01()
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.array[i] > 1f)
				{
					this.array[i] = 1f;
				}
				else if (this.array[i] < 0f)
				{
					this.array[i] = 0f;
				}
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000EC504 File Offset: 0x000EA704
		public bool IsEmpty(float delta = 0.0001f)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.array[i] > delta)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x02000588 RID: 1416
		public enum WrapMode
		{
			// Token: 0x0400236C RID: 9068
			Once,
			// Token: 0x0400236D RID: 9069
			Clamp,
			// Token: 0x0400236E RID: 9070
			Tile,
			// Token: 0x0400236F RID: 9071
			PingPong
		}

		// Token: 0x02000589 RID: 1417
		public class Stacker
		{
			// Token: 0x0600246E RID: 9326 RVA: 0x000EC530 File Offset: 0x000EA730
			public Stacker(CoordRect smallRect, CoordRect bigRect)
			{
				this.smallRect = smallRect;
				this.bigRect = bigRect;
				this.isDownscaled = false;
				if (bigRect == smallRect)
				{
					this.upscaled = (this.downscaled = new Matrix(bigRect, null));
					return;
				}
				this.downscaled = new Matrix(smallRect, null);
				this.upscaled = new Matrix(bigRect, null);
				this.difference = new Matrix(bigRect, null);
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x0600246F RID: 9327 RVA: 0x000191EA File Offset: 0x000173EA
			public Matrix matrix
			{
				get
				{
					if (this.isDownscaled)
					{
						return this.downscaled;
					}
					return this.upscaled;
				}
			}

			// Token: 0x06002470 RID: 9328 RVA: 0x000EC5A8 File Offset: 0x000EA7A8
			public void ToSmall()
			{
				if (this.bigRect == this.smallRect)
				{
					return;
				}
				this.downscaled = this.upscaled.OutdatedResize(this.smallRect, 1f, this.downscaled);
				if (this.preserveDetail)
				{
					this.difference = this.downscaled.OutdatedResize(this.bigRect, 1f, this.difference);
					this.difference.Blur(null, 0.666f, false, false, true, true, null);
					this.difference.InvSubtract(this.upscaled);
				}
				this.isDownscaled = true;
			}

			// Token: 0x06002471 RID: 9329 RVA: 0x000EC644 File Offset: 0x000EA844
			public void ToBig()
			{
				if (this.bigRect == this.smallRect)
				{
					return;
				}
				this.upscaled = this.downscaled.OutdatedResize(this.bigRect, 1f, this.upscaled);
				this.upscaled.Blur(null, 0.666f, false, false, true, true, null);
				if (this.preserveDetail)
				{
					this.upscaled.Add(this.difference);
				}
				this.isDownscaled = false;
			}

			// Token: 0x04002370 RID: 9072
			public CoordRect smallRect;

			// Token: 0x04002371 RID: 9073
			public CoordRect bigRect;

			// Token: 0x04002372 RID: 9074
			public bool preserveDetail = true;

			// Token: 0x04002373 RID: 9075
			private Matrix downscaled;

			// Token: 0x04002374 RID: 9076
			private Matrix upscaled;

			// Token: 0x04002375 RID: 9077
			private Matrix difference;

			// Token: 0x04002376 RID: 9078
			private bool isDownscaled;
		}
	}
}

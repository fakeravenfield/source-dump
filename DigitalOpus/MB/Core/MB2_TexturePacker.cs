using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000464 RID: 1124
	public abstract class MB2_TexturePacker
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x000B9F3C File Offset: 0x000B813C
		public static int RoundToNearestPositivePowerOfTwo(int x)
		{
			int num = (int)Mathf.Pow(2f, (float)Mathf.RoundToInt(Mathf.Log((float)x) / Mathf.Log(2f)));
			if (num == 0 || num == 1)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000B9F78 File Offset: 0x000B8178
		public static int CeilToNearestPowerOfTwo(int x)
		{
			int num = (int)Mathf.Pow(2f, Mathf.Ceil(Mathf.Log((float)x) / Mathf.Log(2f)));
			if (num == 0 || num == 1)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06001C5A RID: 7258
		public abstract AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, int maxDimensionX, int maxDimensionY, int padding);

		// Token: 0x06001C5B RID: 7259
		public abstract AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionX, int maxDimensionY, bool doMultiAtlas);

		// Token: 0x06001C5C RID: 7260 RVA: 0x000B9FB4 File Offset: 0x000B81B4
		internal bool ScaleAtlasToFitMaxDim(Vector2 rootWH, List<MB2_TexturePacker.Image> images, int maxDimensionX, int maxDimensionY, AtlasPadding padding, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY, ref int outW, ref int outH, out float padX, out float padY, out int newMinSizeX, out int newMinSizeY)
		{
			newMinSizeX = minImageSizeX;
			newMinSizeY = minImageSizeY;
			bool result = false;
			padX = (float)padding.leftRight / (float)outW;
			if (rootWH.x > (float)maxDimensionX)
			{
				padX = (float)padding.leftRight / (float)maxDimensionX;
				float num = (float)maxDimensionX / rootWH.x;
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Packing exceeded atlas width shrinking to " + num.ToString());
				}
				for (int i = 0; i < images.Count; i++)
				{
					MB2_TexturePacker.Image image = images[i];
					if ((float)image.w * num < (float)masterImageSizeX)
					{
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							Debug.Log("Small images are being scaled to zero. Will need to redo packing with larger minTexSizeX.");
						}
						result = true;
						newMinSizeX = Mathf.CeilToInt((float)minImageSizeX / num);
					}
					int num2 = (int)((float)(image.x + image.w) * num);
					image.x = (int)(num * (float)image.x);
					image.w = num2 - image.x;
				}
				outW = maxDimensionX;
			}
			padY = (float)padding.topBottom / (float)outH;
			if (rootWH.y > (float)maxDimensionY)
			{
				padY = (float)padding.topBottom / (float)maxDimensionY;
				float num3 = (float)maxDimensionY / rootWH.y;
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Packing exceeded atlas height shrinking to " + num3.ToString());
				}
				for (int j = 0; j < images.Count; j++)
				{
					MB2_TexturePacker.Image image2 = images[j];
					if ((float)image2.h * num3 < (float)masterImageSizeY)
					{
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							Debug.Log("Small images are being scaled to zero. Will need to redo packing with larger minTexSizeY.");
						}
						result = true;
						newMinSizeY = Mathf.CeilToInt((float)minImageSizeY / num3);
					}
					int num4 = (int)((float)(image2.y + image2.h) * num3);
					image2.y = (int)(num3 * (float)image2.y);
					image2.h = num4 - image2.y;
				}
				outH = maxDimensionY;
			}
			return result;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000BA190 File Offset: 0x000B8390
		public void normalizeRects(AtlasPackingResult rr, AtlasPadding padding)
		{
			for (int i = 0; i < rr.rects.Length; i++)
			{
				rr.rects[i].x = (rr.rects[i].x + (float)padding.leftRight) / (float)rr.atlasX;
				rr.rects[i].y = (rr.rects[i].y + (float)padding.topBottom) / (float)rr.atlasY;
				rr.rects[i].width = (rr.rects[i].width - (float)(padding.leftRight * 2)) / (float)rr.atlasX;
				rr.rects[i].height = (rr.rects[i].height - (float)(padding.topBottom * 2)) / (float)rr.atlasY;
			}
		}

		// Token: 0x04001D42 RID: 7490
		public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x04001D43 RID: 7491
		public bool atlasMustBePowerOfTwo = true;

		// Token: 0x02000465 RID: 1125
		internal enum NodeType
		{
			// Token: 0x04001D45 RID: 7493
			Container,
			// Token: 0x04001D46 RID: 7494
			maxDim,
			// Token: 0x04001D47 RID: 7495
			regular
		}

		// Token: 0x02000466 RID: 1126
		internal class PixRect
		{
			// Token: 0x06001C5F RID: 7263 RVA: 0x0000256A File Offset: 0x0000076A
			public PixRect()
			{
			}

			// Token: 0x06001C60 RID: 7264 RVA: 0x000155EA File Offset: 0x000137EA
			public PixRect(int xx, int yy, int ww, int hh)
			{
				this.x = xx;
				this.y = yy;
				this.w = ww;
				this.h = hh;
			}

			// Token: 0x06001C61 RID: 7265 RVA: 0x000BA284 File Offset: 0x000B8484
			public override string ToString()
			{
				return string.Format("x={0},y={1},w={2},h={3}", new object[]
				{
					this.x,
					this.y,
					this.w,
					this.h
				});
			}

			// Token: 0x04001D48 RID: 7496
			public int x;

			// Token: 0x04001D49 RID: 7497
			public int y;

			// Token: 0x04001D4A RID: 7498
			public int w;

			// Token: 0x04001D4B RID: 7499
			public int h;
		}

		// Token: 0x02000467 RID: 1127
		internal class Image
		{
			// Token: 0x06001C62 RID: 7266 RVA: 0x0001560F File Offset: 0x0001380F
			public Image(int id, int tw, int th, AtlasPadding padding, int minImageSizeX, int minImageSizeY)
			{
				this.imgId = id;
				this.w = Mathf.Max(tw + padding.leftRight * 2, minImageSizeX);
				this.h = Mathf.Max(th + padding.topBottom * 2, minImageSizeY);
			}

			// Token: 0x06001C63 RID: 7267 RVA: 0x000BA2DC File Offset: 0x000B84DC
			public Image(MB2_TexturePacker.Image im)
			{
				this.imgId = im.imgId;
				this.w = im.w;
				this.h = im.h;
				this.x = im.x;
				this.y = im.y;
			}

			// Token: 0x04001D4C RID: 7500
			public int imgId;

			// Token: 0x04001D4D RID: 7501
			public int w;

			// Token: 0x04001D4E RID: 7502
			public int h;

			// Token: 0x04001D4F RID: 7503
			public int x;

			// Token: 0x04001D50 RID: 7504
			public int y;
		}

		// Token: 0x02000468 RID: 1128
		internal class ImgIDComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06001C64 RID: 7268 RVA: 0x0001564E File Offset: 0x0001384E
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.imgId > y.imgId)
				{
					return 1;
				}
				if (x.imgId == y.imgId)
				{
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x02000469 RID: 1129
		internal class ImageHeightComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06001C66 RID: 7270 RVA: 0x00015671 File Offset: 0x00013871
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.h > y.h)
				{
					return -1;
				}
				if (x.h == y.h)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x0200046A RID: 1130
		internal class ImageWidthComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06001C68 RID: 7272 RVA: 0x00015694 File Offset: 0x00013894
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.w > y.w)
				{
					return -1;
				}
				if (x.w == y.w)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x0200046B RID: 1131
		internal class ImageAreaComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06001C6A RID: 7274 RVA: 0x000BA32C File Offset: 0x000B852C
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				int num = x.w * x.h;
				int num2 = y.w * y.h;
				if (num > num2)
				{
					return -1;
				}
				if (num == num2)
				{
					return 0;
				}
				return 1;
			}
		}
	}
}

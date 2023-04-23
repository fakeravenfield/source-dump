using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200046F RID: 1135
	public class MB2_TexturePackerHorizontalVert : MB2_TexturePacker
	{
		// Token: 0x06001C81 RID: 7297 RVA: 0x000BBB18 File Offset: 0x000B9D18
		public override AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, int maxDimensionX, int maxDimensionY, int padding)
		{
			List<AtlasPadding> list = new List<AtlasPadding>();
			for (int i = 0; i < imgWidthHeights.Count; i++)
			{
				AtlasPadding item = default(AtlasPadding);
				if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.horizontal)
				{
					item.leftRight = 0;
					item.topBottom = 8;
				}
				else
				{
					item.leftRight = 8;
					item.topBottom = 0;
				}
				list.Add(item);
			}
			return this.GetRects(imgWidthHeights, list, maxDimensionX, maxDimensionY, false);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000BBB80 File Offset: 0x000B9D80
		public override AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionX, int maxDimensionY, bool doMultiAtlas)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < paddings.Count; i++)
			{
				num = Mathf.Max(num, paddings[i].leftRight);
				num2 = Mathf.Max(num2, paddings[i].topBottom);
			}
			if (doMultiAtlas)
			{
				if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
				{
					return this._GetRectsMultiAtlasVertical(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, 2 + num * 2, 2 + num2 * 2, 2 + num * 2, 2 + num2 * 2);
				}
				return this._GetRectsMultiAtlasHorizontal(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, 2 + num * 2, 2 + num2 * 2, 2 + num * 2, 2 + num2 * 2);
			}
			else
			{
				AtlasPackingResult atlasPackingResult = this._GetRectsSingleAtlas(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, 2 + num * 2, 2 + num2 * 2, 2 + num * 2, 2 + num2 * 2, 0);
				if (atlasPackingResult == null)
				{
					return null;
				}
				return new AtlasPackingResult[]
				{
					atlasPackingResult
				};
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000BBC48 File Offset: 0x000B9E48
		private AtlasPackingResult _GetRectsSingleAtlas(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionX, int maxDimensionY, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY, int recursionDepth)
		{
			AtlasPackingResult atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
			List<Rect> list = new List<Rect>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			List<MB2_TexturePacker.Image> list2 = new List<MB2_TexturePacker.Image>();
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Packing rects for: " + imgWidthHeights.Count.ToString());
			}
			for (int i = 0; i < imgWidthHeights.Count; i++)
			{
				MB2_TexturePacker.Image image = new MB2_TexturePacker.Image(i, (int)imgWidthHeights[i].x, (int)imgWidthHeights[i].y, paddings[i], minImageSizeX, minImageSizeY);
				if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
				{
					image.h -= paddings[i].topBottom * 2;
					image.x = num;
					image.y = 0;
					list.Add(new Rect((float)image.w, (float)image.h, (float)num, 0f));
					num += image.w;
					num2 = Mathf.Max(num2, image.h);
				}
				else
				{
					image.w -= paddings[i].leftRight * 2;
					image.y = num;
					image.x = 0;
					list.Add(new Rect((float)image.w, (float)image.h, 0f, (float)num));
					num += image.h;
					num3 = Mathf.Max(num3, image.w);
				}
				list2.Add(image);
			}
			Vector2 vector;
			if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
			{
				vector = new Vector2((float)num, (float)num2);
			}
			else
			{
				vector = new Vector2((float)num3, (float)num);
			}
			int num4 = (int)vector.x;
			int num5 = (int)vector.y;
			if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
			{
				if (this.atlasMustBePowerOfTwo)
				{
					num4 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num4), maxDimensionX);
				}
				else
				{
					num4 = Mathf.Min(num4, maxDimensionX);
				}
			}
			else if (this.atlasMustBePowerOfTwo)
			{
				num5 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num5), maxDimensionY);
			}
			else
			{
				num5 = Mathf.Min(num5, maxDimensionY);
			}
			float num6;
			float num7;
			int num8;
			int num9;
			if (!base.ScaleAtlasToFitMaxDim(vector, list2, maxDimensionX, maxDimensionY, paddings[0], minImageSizeX, minImageSizeY, masterImageSizeX, masterImageSizeY, ref num4, ref num5, out num6, out num7, out num8, out num9))
			{
				atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
				atlasPackingResult.rects = new Rect[list2.Count];
				atlasPackingResult.srcImgIdxs = new int[list2.Count];
				atlasPackingResult.atlasX = num4;
				atlasPackingResult.atlasY = num5;
				for (int j = 0; j < list2.Count; j++)
				{
					MB2_TexturePacker.Image image2 = list2[j];
					Rect rect;
					if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
					{
						rect = (atlasPackingResult.rects[j] = new Rect((float)image2.x / (float)num4 + num6, (float)image2.y / (float)num5, (float)image2.w / (float)num4 - num6 * 2f, this.stretchImagesToEdges ? 1f : ((float)image2.h / (float)num5)));
					}
					else
					{
						rect = (atlasPackingResult.rects[j] = new Rect((float)image2.x / (float)num4, (float)image2.y / (float)num5 + num7, this.stretchImagesToEdges ? 1f : ((float)image2.w / (float)num4), (float)image2.h / (float)num5 - num7 * 2f));
					}
					atlasPackingResult.srcImgIdxs[j] = image2.imgId;
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug(string.Concat(new string[]
						{
							"Image: ",
							j.ToString(),
							" imgID=",
							image2.imgId.ToString(),
							" x=",
							(rect.x * (float)num4).ToString(),
							" y=",
							(rect.y * (float)num5).ToString(),
							" w=",
							(rect.width * (float)num4).ToString(),
							" h=",
							(rect.height * (float)num5).ToString(),
							" padding=",
							paddings[j].ToString(),
							" outW=",
							num4.ToString(),
							" outH=",
							num5.ToString()
						}), Array.Empty<object>());
					}
				}
				atlasPackingResult.CalcUsedWidthAndHeight();
				return atlasPackingResult;
			}
			Debug.Log("Packing failed returning null atlas result");
			return null;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000BC0F8 File Offset: 0x000BA2F8
		private AtlasPackingResult[] _GetRectsMultiAtlasVertical(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionPassedX, int maxDimensionPassedY, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY)
		{
			List<AtlasPackingResult> list = new List<AtlasPackingResult>();
			int num = 0;
			int num2 = 0;
			int atlasX = 0;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Packing rects for: " + imgWidthHeights.Count.ToString());
			}
			List<MB2_TexturePacker.Image> list2 = new List<MB2_TexturePacker.Image>();
			for (int i = 0; i < imgWidthHeights.Count; i++)
			{
				MB2_TexturePacker.Image image = new MB2_TexturePacker.Image(i, (int)imgWidthHeights[i].x, (int)imgWidthHeights[i].y, paddings[i], minImageSizeX, minImageSizeY);
				image.h -= paddings[i].topBottom * 2;
				list2.Add(image);
			}
			list2.Sort(new MB2_TexturePacker.ImageWidthComparer());
			List<MB2_TexturePacker.Image> list3 = new List<MB2_TexturePacker.Image>();
			List<Rect> list4 = new List<Rect>();
			int spaceRemaining = maxDimensionPassedX;
			while (list2.Count > 0 || list3.Count > 0)
			{
				MB2_TexturePacker.Image image2 = this.PopLargestThatFits(list2, spaceRemaining, maxDimensionPassedX, list3.Count == 0);
				if (image2 == null)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("Atlas filled creating a new atlas ");
					}
					AtlasPackingResult atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
					atlasPackingResult.atlasX = atlasX;
					atlasPackingResult.atlasY = num2;
					Rect[] array = new Rect[list3.Count];
					int[] array2 = new int[list3.Count];
					for (int j = 0; j < list3.Count; j++)
					{
						Rect rect = new Rect((float)list3[j].x, (float)list3[j].y, (float)list3[j].w, (float)(this.stretchImagesToEdges ? num2 : list3[j].h));
						array[j] = rect;
						array2[j] = list3[j].imgId;
					}
					atlasPackingResult.rects = array;
					atlasPackingResult.srcImgIdxs = array2;
					atlasPackingResult.CalcUsedWidthAndHeight();
					list3.Clear();
					list4.Clear();
					num = 0;
					num2 = 0;
					list.Add(atlasPackingResult);
					spaceRemaining = maxDimensionPassedX;
				}
				else
				{
					image2.x = num;
					image2.y = 0;
					list3.Add(image2);
					list4.Add(new Rect((float)num, 0f, (float)image2.w, (float)image2.h));
					num += image2.w;
					num2 = Mathf.Max(num2, image2.h);
					atlasX = num;
					spaceRemaining = maxDimensionPassedX - num;
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				int num3 = list[k].atlasX;
				int num4 = Mathf.Min(list[k].atlasY, maxDimensionPassedY);
				if (this.atlasMustBePowerOfTwo)
				{
					num3 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num3), maxDimensionPassedX);
				}
				else
				{
					num3 = Mathf.Min(num3, maxDimensionPassedX);
				}
				list[k].atlasX = num3;
				float num5;
				float num6;
				int num7;
				int num8;
				base.ScaleAtlasToFitMaxDim(new Vector2((float)list[k].atlasX, (float)list[k].atlasY), list3, maxDimensionPassedX, maxDimensionPassedY, paddings[0], minImageSizeX, minImageSizeY, masterImageSizeX, masterImageSizeY, ref num3, ref num4, out num5, out num6, out num7, out num8);
			}
			for (int l = 0; l < list.Count; l++)
			{
				base.normalizeRects(list[l], paddings[l]);
				list[l].CalcUsedWidthAndHeight();
			}
			return list.ToArray();
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000BC460 File Offset: 0x000BA660
		private AtlasPackingResult[] _GetRectsMultiAtlasHorizontal(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionPassedX, int maxDimensionPassedY, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY)
		{
			List<AtlasPackingResult> list = new List<AtlasPackingResult>();
			int num = 0;
			int atlasY = 0;
			int num2 = 0;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Packing rects for: " + imgWidthHeights.Count.ToString());
			}
			List<MB2_TexturePacker.Image> list2 = new List<MB2_TexturePacker.Image>();
			for (int i = 0; i < imgWidthHeights.Count; i++)
			{
				MB2_TexturePacker.Image image = new MB2_TexturePacker.Image(i, (int)imgWidthHeights[i].x, (int)imgWidthHeights[i].y, paddings[i], minImageSizeX, minImageSizeY);
				image.w -= paddings[i].leftRight * 2;
				list2.Add(image);
			}
			list2.Sort(new MB2_TexturePacker.ImageHeightComparer());
			List<MB2_TexturePacker.Image> list3 = new List<MB2_TexturePacker.Image>();
			List<Rect> list4 = new List<Rect>();
			int spaceRemaining = maxDimensionPassedY;
			while (list2.Count > 0 || list3.Count > 0)
			{
				MB2_TexturePacker.Image image2 = this.PopLargestThatFits(list2, spaceRemaining, maxDimensionPassedY, list3.Count == 0);
				if (image2 == null)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("Atlas filled creating a new atlas ");
					}
					AtlasPackingResult atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
					atlasPackingResult.atlasX = num2;
					atlasPackingResult.atlasY = atlasY;
					Rect[] array = new Rect[list3.Count];
					int[] array2 = new int[list3.Count];
					for (int j = 0; j < list3.Count; j++)
					{
						Rect rect = new Rect((float)list3[j].x, (float)list3[j].y, (float)(this.stretchImagesToEdges ? num2 : list3[j].w), (float)list3[j].h);
						array[j] = rect;
						array2[j] = list3[j].imgId;
					}
					atlasPackingResult.rects = array;
					atlasPackingResult.srcImgIdxs = array2;
					list3.Clear();
					list4.Clear();
					num = 0;
					atlasY = 0;
					list.Add(atlasPackingResult);
					spaceRemaining = maxDimensionPassedY;
				}
				else
				{
					image2.x = 0;
					image2.y = num;
					list3.Add(image2);
					list4.Add(new Rect(0f, (float)num, (float)image2.w, (float)image2.h));
					num += image2.h;
					num2 = Mathf.Max(num2, image2.w);
					atlasY = num;
					spaceRemaining = maxDimensionPassedY - num;
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				int num3 = list[k].atlasY;
				int num4 = Mathf.Min(list[k].atlasX, maxDimensionPassedX);
				if (this.atlasMustBePowerOfTwo)
				{
					num3 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num3), maxDimensionPassedY);
				}
				else
				{
					num3 = Mathf.Min(num3, maxDimensionPassedY);
				}
				list[k].atlasY = num3;
				float num5;
				float num6;
				int num7;
				int num8;
				base.ScaleAtlasToFitMaxDim(new Vector2((float)list[k].atlasX, (float)list[k].atlasY), list3, maxDimensionPassedX, maxDimensionPassedY, paddings[0], minImageSizeX, minImageSizeY, masterImageSizeX, masterImageSizeY, ref num4, ref num3, out num5, out num6, out num7, out num8);
			}
			for (int l = 0; l < list.Count; l++)
			{
				base.normalizeRects(list[l], paddings[l]);
				list[l].CalcUsedWidthAndHeight();
			}
			return list.ToArray();
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000BC7C4 File Offset: 0x000BA9C4
		private MB2_TexturePacker.Image PopLargestThatFits(List<MB2_TexturePacker.Image> images, int spaceRemaining, int maxDim, bool emptyAtlas)
		{
			if (images.Count == 0)
			{
				return null;
			}
			int num;
			if (this.packingOrientation == MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical)
			{
				num = images[0].w;
			}
			else
			{
				num = images[0].h;
			}
			if (images.Count > 0 && num >= maxDim)
			{
				if (emptyAtlas)
				{
					MB2_TexturePacker.Image result = images[0];
					images.RemoveAt(0);
					return result;
				}
				return null;
			}
			else
			{
				int num2 = 0;
				while (num2 < images.Count && num >= spaceRemaining)
				{
					num2++;
				}
				if (num2 < images.Count)
				{
					MB2_TexturePacker.Image result2 = images[num2];
					images.RemoveAt(num2);
					return result2;
				}
				return null;
			}
		}

		// Token: 0x04001D62 RID: 7522
		public MB2_TexturePackerHorizontalVert.TexturePackingOrientation packingOrientation;

		// Token: 0x04001D63 RID: 7523
		public bool stretchImagesToEdges = true;

		// Token: 0x02000470 RID: 1136
		public enum TexturePackingOrientation
		{
			// Token: 0x04001D65 RID: 7525
			horizontal,
			// Token: 0x04001D66 RID: 7526
			vertical
		}
	}
}

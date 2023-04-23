using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000495 RID: 1173
	internal class MB3_TextureCombinerPackerMeshBakerHorizontalVertical : MB3_TextureCombinerPackerMeshBaker
	{
		// Token: 0x06001D90 RID: 7568 RVA: 0x00015F72 File Offset: 0x00014172
		public MB3_TextureCombinerPackerMeshBakerHorizontalVertical(MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection ad)
		{
			this._atlasDirection = ad;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000C12BC File Offset: 0x000BF4BC
		public override AtlasPackingResult[] CalculateAtlasRectangles(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			MB3_TextureCombinerPackerMeshBakerHorizontalVertical.IPipeline pipeline;
			if (this._atlasDirection == MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection.horizontal)
			{
				pipeline = new MB3_TextureCombinerPackerMeshBakerHorizontalVertical.HorizontalPipeline();
			}
			else
			{
				pipeline = new MB3_TextureCombinerPackerMeshBakerHorizontalVertical.VerticalPipeline();
			}
			if (this._atlasDirection == MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection.horizontal)
			{
				if (!data._useMaxAtlasWidthOverride)
				{
					int num = 2;
					for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
					{
						MB_TexSet mb_TexSet = data.distinctMaterialTextures[i];
						int num2;
						if (data._fixOutOfBoundsUVs)
						{
							num2 = (int)mb_TexSet.GetMaxRawTextureHeightWidth().x;
						}
						else
						{
							num2 = mb_TexSet.idealWidth;
						}
						if (mb_TexSet.idealWidth > num)
						{
							num = num2;
						}
					}
					if (LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("Calculated max atlas width: " + num.ToString());
					}
					data._maxAtlasWidth = num;
				}
			}
			else if (!data._useMaxAtlasHeightOverride)
			{
				int num3 = 2;
				for (int j = 0; j < data.distinctMaterialTextures.Count; j++)
				{
					MB_TexSet mb_TexSet2 = data.distinctMaterialTextures[j];
					int num4;
					if (data._fixOutOfBoundsUVs)
					{
						num4 = (int)mb_TexSet2.GetMaxRawTextureHeightWidth().y;
					}
					else
					{
						num4 = mb_TexSet2.idealHeight;
					}
					if (mb_TexSet2.idealHeight > num3)
					{
						num3 = num4;
					}
				}
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Calculated max atlas height: " + num3.ToString());
				}
				data._maxAtlasHeight = num3;
			}
			List<MB_TexSet> list = new List<MB_TexSet>();
			List<MB_TexSet> list2 = new List<MB_TexSet>();
			for (int k = 0; k < data.distinctMaterialTextures.Count; k++)
			{
				pipeline.SortTexSetIntoBins(data.distinctMaterialTextures[k], list, list2, data._maxAtlasWidth, data._maxAtlasHeight);
			}
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("Splitting list of distinctMaterialTextures numHorizontalVertical={0} numRegular={1} maxAtlasWidth={2} maxAtlasHeight={3}", new object[]
				{
					list.Count,
					list2.Count,
					data._maxAtlasWidth,
					data._maxAtlasHeight
				}));
			}
			AtlasPackingResult[] array;
			if (list.Count > 0)
			{
				MB2_PackingAlgorithmEnum packingAlg = pipeline.GetPackingAlg();
				List<Vector2> list3 = new List<Vector2>();
				for (int l = 0; l < list.Count; l++)
				{
					list[l].SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(pipeline.GetEdge2EdgeTreatment());
					list3.Add(new Vector2((float)list[l].idealWidth, (float)list[l].idealHeight));
				}
				MB2_TexturePacker mb2_TexturePacker = MB3_TextureCombinerPipeline.CreateTexturePacker(packingAlg);
				mb2_TexturePacker.atlasMustBePowerOfTwo = false;
				List<AtlasPadding> list4 = new List<AtlasPadding>();
				for (int m = 0; m < list3.Count; m++)
				{
					AtlasPadding item = default(AtlasPadding);
					pipeline.InitializeAtlasPadding(ref item, data._atlasPadding);
					list4.Add(item);
				}
				mb2_TexturePacker.LOG_LEVEL = MB2_LogLevel.trace;
				array = mb2_TexturePacker.GetRects(list3, list4, data._maxAtlasWidth, data._maxAtlasHeight, false);
				if (LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Packed {0} textures with edgeToEdge tiling into an atlas of size {1} by {2} usedW {3} usedH {4}", new object[]
					{
						list.Count,
						array[0].atlasX,
						array[0].atlasY,
						array[0].usedW,
						array[0].usedH
					}));
				}
			}
			else
			{
				array = new AtlasPackingResult[0];
			}
			AtlasPackingResult[] array2;
			if (list2.Count > 0)
			{
				List<Vector2> list5 = new List<Vector2>();
				for (int n = 0; n < list2.Count; n++)
				{
					list5.Add(new Vector2((float)list2[n].idealWidth, (float)list2[n].idealHeight));
				}
				MB2_TexturePacker mb2_TexturePacker = MB3_TextureCombinerPipeline.CreateTexturePacker(MB2_PackingAlgorithmEnum.MeshBakerTexturePacker);
				mb2_TexturePacker.atlasMustBePowerOfTwo = false;
				List<AtlasPadding> list6 = new List<AtlasPadding>();
				for (int num5 = 0; num5 < list5.Count; num5++)
				{
					list6.Add(new AtlasPadding
					{
						topBottom = data._atlasPadding,
						leftRight = data._atlasPadding
					});
				}
				int usedHorizontalVertWidth = 0;
				int usedHorizontalVertHeight = 0;
				if (array.Length != 0)
				{
					usedHorizontalVertHeight = array[0].atlasY;
					usedHorizontalVertWidth = array[0].atlasX;
				}
				int maxDimensionX;
				int maxDimensionY;
				pipeline.GetExtraRoomForRegularAtlas(usedHorizontalVertWidth, usedHorizontalVertHeight, data._maxAtlasWidth, data._maxAtlasHeight, out maxDimensionX, out maxDimensionY);
				array2 = mb2_TexturePacker.GetRects(list5, list6, maxDimensionX, maxDimensionY, false);
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log(string.Format("Packed {0} textures without edgeToEdge tiling into an atlas of size {1} by {2} usedW {3} usedH {4}", new object[]
					{
						list2.Count,
						array2[0].atlasX,
						array2[0].atlasY,
						array2[0].usedW,
						array2[0].usedH
					}));
				}
			}
			else
			{
				array2 = new AtlasPackingResult[0];
			}
			AtlasPackingResult atlasPackingResult = null;
			if (array.Length == 0 && array2.Length == 0)
			{
				return null;
			}
			if (array.Length != 0 && array2.Length != 0)
			{
				atlasPackingResult = MB3_TextureCombinerPackerMeshBakerHorizontalVertical.MergeAtlasPackingResultStackBonA(array[0], array2[0], data._maxAtlasWidth, data._maxAtlasHeight, true, pipeline);
			}
			else if (array.Length != 0)
			{
				atlasPackingResult = array[0];
			}
			else if (array2.Length != 0)
			{
				atlasPackingResult = array2[0];
			}
			list.AddRange(list2);
			data.distinctMaterialTextures = list;
			AtlasPackingResult[] result;
			if (atlasPackingResult != null)
			{
				result = new AtlasPackingResult[]
				{
					atlasPackingResult
				};
			}
			else
			{
				result = new AtlasPackingResult[0];
			}
			return result;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00015F81 File Offset: 0x00014181
		public static AtlasPackingResult TestStackRectanglesHorizontal(AtlasPackingResult a, AtlasPackingResult b, int maxHeightDim, int maxWidthDim, bool stretchBToAtlasWidth)
		{
			return MB3_TextureCombinerPackerMeshBakerHorizontalVertical.MergeAtlasPackingResultStackBonA(a, b, maxWidthDim, maxHeightDim, stretchBToAtlasWidth, new MB3_TextureCombinerPackerMeshBakerHorizontalVertical.HorizontalPipeline());
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00015F93 File Offset: 0x00014193
		public static AtlasPackingResult TestStackRectanglesVertical(AtlasPackingResult a, AtlasPackingResult b, int maxHeightDim, int maxWidthDim, bool stretchBToAtlasWidth)
		{
			return MB3_TextureCombinerPackerMeshBakerHorizontalVertical.MergeAtlasPackingResultStackBonA(a, b, maxWidthDim, maxHeightDim, stretchBToAtlasWidth, new MB3_TextureCombinerPackerMeshBakerHorizontalVertical.VerticalPipeline());
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000C17C8 File Offset: 0x000BF9C8
		private static AtlasPackingResult MergeAtlasPackingResultStackBonA(AtlasPackingResult a, AtlasPackingResult b, int maxWidthDim, int maxHeightDim, bool stretchBToAtlasWidth, MB3_TextureCombinerPackerMeshBakerHorizontalVertical.IPipeline pipeline)
		{
			Rect rect;
			Rect rect2;
			int atlasX;
			int atlasY;
			pipeline.MergeAtlasPackingResultStackBonAInternal(a, b, out rect, out rect2, stretchBToAtlasWidth, maxWidthDim, maxHeightDim, out atlasX, out atlasY);
			Rect[] array = new Rect[a.rects.Length + b.rects.Length];
			AtlasPadding[] array2 = new AtlasPadding[a.rects.Length + b.rects.Length];
			int[] array3 = new int[a.rects.Length + b.rects.Length];
			Array.Copy(a.padding, array2, a.padding.Length);
			Array.Copy(b.padding, 0, array2, a.padding.Length, b.padding.Length);
			Array.Copy(a.srcImgIdxs, array3, a.srcImgIdxs.Length);
			Array.Copy(b.srcImgIdxs, 0, array3, a.srcImgIdxs.Length, b.srcImgIdxs.Length);
			Array.Copy(a.rects, array, a.rects.Length);
			for (int i = 0; i < a.rects.Length; i++)
			{
				Rect rect3 = a.rects[i];
				rect3.x = rect.x + rect3.x * rect.width;
				rect3.y = rect.y + rect3.y * rect.height;
				rect3.width *= rect.width;
				rect3.height *= rect.height;
				array[i] = rect3;
				array3[i] = a.srcImgIdxs[i];
			}
			for (int j = 0; j < b.rects.Length; j++)
			{
				Rect rect4 = b.rects[j];
				rect4.x = rect2.x + rect4.x * rect2.width;
				rect4.y = rect2.y + rect4.y * rect2.height;
				rect4.width *= rect2.width;
				rect4.height *= rect2.height;
				array[a.rects.Length + j] = rect4;
				array3[a.rects.Length + j] = b.srcImgIdxs[j];
			}
			AtlasPackingResult atlasPackingResult = new AtlasPackingResult(array2);
			atlasPackingResult.atlasX = atlasX;
			atlasPackingResult.atlasY = atlasY;
			atlasPackingResult.padding = array2;
			atlasPackingResult.rects = array;
			atlasPackingResult.srcImgIdxs = array3;
			atlasPackingResult.CalcUsedWidthAndHeight();
			return atlasPackingResult;
		}

		// Token: 0x04001E19 RID: 7705
		private MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection _atlasDirection;

		// Token: 0x02000496 RID: 1174
		private interface IPipeline
		{
			// Token: 0x06001D95 RID: 7573
			MB2_PackingAlgorithmEnum GetPackingAlg();

			// Token: 0x06001D96 RID: 7574
			void SortTexSetIntoBins(MB_TexSet texSet, List<MB_TexSet> horizontalVert, List<MB_TexSet> regular, int maxAtlasWidth, int maxAtlasHeight);

			// Token: 0x06001D97 RID: 7575
			MB_TextureTilingTreatment GetEdge2EdgeTreatment();

			// Token: 0x06001D98 RID: 7576
			void InitializeAtlasPadding(ref AtlasPadding padding, int paddingValue);

			// Token: 0x06001D99 RID: 7577
			void MergeAtlasPackingResultStackBonAInternal(AtlasPackingResult a, AtlasPackingResult b, out Rect AatlasToFinal, out Rect BatlasToFinal, bool stretchBToAtlasWidth, int maxWidthDim, int maxHeightDim, out int atlasX, out int atlasY);

			// Token: 0x06001D9A RID: 7578
			void GetExtraRoomForRegularAtlas(int usedHorizontalVertWidth, int usedHorizontalVertHeight, int maxAtlasWidth, int maxAtlasHeight, out int atlasRegularMaxWidth, out int atlasRegularMaxHeight);
		}

		// Token: 0x02000497 RID: 1175
		private class VerticalPipeline : MB3_TextureCombinerPackerMeshBakerHorizontalVertical.IPipeline
		{
			// Token: 0x06001D9B RID: 7579 RVA: 0x00015FA5 File Offset: 0x000141A5
			public MB2_PackingAlgorithmEnum GetPackingAlg()
			{
				return MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical;
			}

			// Token: 0x06001D9C RID: 7580 RVA: 0x00015FA8 File Offset: 0x000141A8
			public void SortTexSetIntoBins(MB_TexSet texSet, List<MB_TexSet> horizontalVert, List<MB_TexSet> regular, int maxAtlasWidth, int maxAtlasHeight)
			{
				if (texSet.idealHeight >= maxAtlasHeight && texSet.ts[0].GetEncapsulatingSamplingRect().height >= 1.0)
				{
					horizontalVert.Add(texSet);
					return;
				}
				regular.Add(texSet);
			}

			// Token: 0x06001D9D RID: 7581 RVA: 0x00015FE0 File Offset: 0x000141E0
			public MB_TextureTilingTreatment GetEdge2EdgeTreatment()
			{
				return MB_TextureTilingTreatment.edgeToEdgeY;
			}

			// Token: 0x06001D9E RID: 7582 RVA: 0x00015FE3 File Offset: 0x000141E3
			public void InitializeAtlasPadding(ref AtlasPadding padding, int paddingValue)
			{
				padding.topBottom = 0;
				padding.leftRight = paddingValue;
			}

			// Token: 0x06001D9F RID: 7583 RVA: 0x000C1A48 File Offset: 0x000BFC48
			public void MergeAtlasPackingResultStackBonAInternal(AtlasPackingResult a, AtlasPackingResult b, out Rect AatlasToFinal, out Rect BatlasToFinal, bool stretchBToAtlasWidth, int maxWidthDim, int maxHeightDim, out int atlasX, out int atlasY)
			{
				float num = (float)(a.usedW + b.usedW);
				if (num > (float)maxWidthDim)
				{
					float num2 = (float)maxWidthDim / num;
					float num3 = (float)Mathf.FloorToInt((float)a.usedW * num2) / (float)maxWidthDim;
					num2 = num3;
					float width = 1f - num3;
					AatlasToFinal = new Rect(0f, 0f, num2, 1f);
					BatlasToFinal = new Rect(num3, 0f, width, 1f);
				}
				else
				{
					float num4 = (float)a.usedW / num;
					AatlasToFinal = new Rect(0f, 0f, num4, 1f);
					BatlasToFinal = new Rect(num4, 0f, (float)b.usedW / num, 1f);
				}
				if (a.atlasX > b.atlasX)
				{
					if (!stretchBToAtlasWidth)
					{
						BatlasToFinal.width = (float)b.atlasX / (float)a.atlasX;
					}
				}
				else if (b.atlasX > a.atlasX)
				{
					AatlasToFinal.width = (float)a.atlasX / (float)b.atlasX;
				}
				atlasX = a.usedW + b.usedW;
				atlasY = Mathf.Max(a.usedH, b.usedH);
			}

			// Token: 0x06001DA0 RID: 7584 RVA: 0x00015FF3 File Offset: 0x000141F3
			public void GetExtraRoomForRegularAtlas(int usedHorizontalVertWidth, int usedHorizontalVertHeight, int maxAtlasWidth, int maxAtlasHeight, out int atlasRegularMaxWidth, out int atlasRegularMaxHeight)
			{
				atlasRegularMaxWidth = maxAtlasWidth - usedHorizontalVertWidth;
				atlasRegularMaxHeight = maxAtlasHeight;
			}
		}

		// Token: 0x02000498 RID: 1176
		private class HorizontalPipeline : MB3_TextureCombinerPackerMeshBakerHorizontalVertical.IPipeline
		{
			// Token: 0x06001DA2 RID: 7586 RVA: 0x00015FE0 File Offset: 0x000141E0
			public MB2_PackingAlgorithmEnum GetPackingAlg()
			{
				return MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal;
			}

			// Token: 0x06001DA3 RID: 7587 RVA: 0x00016000 File Offset: 0x00014200
			public void SortTexSetIntoBins(MB_TexSet texSet, List<MB_TexSet> horizontalVert, List<MB_TexSet> regular, int maxAtlasWidth, int maxAtlasHeight)
			{
				if (texSet.idealWidth >= maxAtlasWidth && texSet.ts[0].GetEncapsulatingSamplingRect().width >= 1.0)
				{
					horizontalVert.Add(texSet);
					return;
				}
				regular.Add(texSet);
			}

			// Token: 0x06001DA4 RID: 7588 RVA: 0x00016038 File Offset: 0x00014238
			public MB_TextureTilingTreatment GetEdge2EdgeTreatment()
			{
				return MB_TextureTilingTreatment.edgeToEdgeX;
			}

			// Token: 0x06001DA5 RID: 7589 RVA: 0x0001603B File Offset: 0x0001423B
			public void InitializeAtlasPadding(ref AtlasPadding padding, int paddingValue)
			{
				padding.topBottom = paddingValue;
				padding.leftRight = 0;
			}

			// Token: 0x06001DA6 RID: 7590 RVA: 0x000C1B7C File Offset: 0x000BFD7C
			public void MergeAtlasPackingResultStackBonAInternal(AtlasPackingResult a, AtlasPackingResult b, out Rect AatlasToFinal, out Rect BatlasToFinal, bool stretchBToAtlasWidth, int maxWidthDim, int maxHeightDim, out int atlasX, out int atlasY)
			{
				float num = (float)(a.usedH + b.usedH);
				if (num > (float)maxHeightDim)
				{
					float num2 = (float)maxHeightDim / num;
					float num3 = (float)Mathf.FloorToInt((float)a.usedH * num2) / (float)maxHeightDim;
					num2 = num3;
					float height = 1f - num3;
					AatlasToFinal = new Rect(0f, 0f, 1f, num2);
					BatlasToFinal = new Rect(0f, num3, 1f, height);
				}
				else
				{
					float num4 = (float)a.usedH / num;
					AatlasToFinal = new Rect(0f, 0f, 1f, num4);
					BatlasToFinal = new Rect(0f, num4, 1f, (float)b.usedH / num);
				}
				if (a.atlasX > b.atlasX)
				{
					if (!stretchBToAtlasWidth)
					{
						BatlasToFinal.width = (float)b.atlasX / (float)a.atlasX;
					}
				}
				else if (b.atlasX > a.atlasX)
				{
					AatlasToFinal.width = (float)a.atlasX / (float)b.atlasX;
				}
				atlasX = Mathf.Max(a.usedW, b.usedW);
				atlasY = a.usedH + b.usedH;
			}

			// Token: 0x06001DA7 RID: 7591 RVA: 0x0001604B File Offset: 0x0001424B
			public void GetExtraRoomForRegularAtlas(int usedHorizontalVertWidth, int usedHorizontalVertHeight, int maxAtlasWidth, int maxAtlasHeight, out int atlasRegularMaxWidth, out int atlasRegularMaxHeight)
			{
				atlasRegularMaxWidth = maxAtlasWidth;
				atlasRegularMaxHeight = maxAtlasHeight - usedHorizontalVertHeight;
			}
		}

		// Token: 0x02000499 RID: 1177
		public enum AtlasDirection
		{
			// Token: 0x04001E1B RID: 7707
			horizontal,
			// Token: 0x04001E1C RID: 7708
			vertical
		}
	}
}

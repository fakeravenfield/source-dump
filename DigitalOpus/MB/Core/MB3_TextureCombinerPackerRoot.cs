using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000472 RID: 1138
	internal abstract class MB3_TextureCombinerPackerRoot : MB_ITextureCombinerPacker
	{
		// Token: 0x06001C8B RID: 7307 RVA: 0x000BC850 File Offset: 0x000BAA50
		internal static void CreateTemporaryTexturesForAtlas(List<MB_TexSet> distinctMaterialTextures, MB3_TextureCombiner combiner, int propIdx, MB3_TextureCombinerPipeline.TexturePipelineData data)
		{
			for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
			{
				MB_TexSet mb_TexSet = data.distinctMaterialTextures[i];
				if (mb_TexSet.ts[propIdx].isNull)
				{
					Color colorForTemporaryTexture = data.nonTexturePropertyBlender.GetColorForTemporaryTexture(mb_TexSet.matsAndGOs.mats[0].mat, data.texPropertyNames[propIdx]);
					mb_TexSet.CreateColoredTexToReplaceNull(data.texPropertyNames[propIdx].name, propIdx, data._fixOutOfBoundsUVs, combiner, colorForTemporaryTexture);
				}
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000BC8E0 File Offset: 0x000BAAE0
		public static AtlasPackingResult[] CalculateAtlasRectanglesStatic(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
			{
				list.Add(new Vector2((float)data.distinctMaterialTextures[i].idealWidth, (float)data.distinctMaterialTextures[i].idealHeight));
			}
			MB2_TexturePacker mb2_TexturePacker = MB3_TextureCombinerPipeline.CreateTexturePacker(data._packingAlgorithm);
			mb2_TexturePacker.atlasMustBePowerOfTwo = data._meshBakerTexturePackerForcePowerOfTwo;
			List<AtlasPadding> list2 = new List<AtlasPadding>();
			for (int j = 0; j < list.Count; j++)
			{
				AtlasPadding item = default(AtlasPadding);
				item.topBottom = data._atlasPadding;
				item.leftRight = data._atlasPadding;
				if (data._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal)
				{
					item.leftRight = 0;
				}
				if (data._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical)
				{
					item.topBottom = 0;
				}
				list2.Add(item);
			}
			return mb2_TexturePacker.GetRects(list, list2, data._maxAtlasWidth, data._maxAtlasHeight, doMultiAtlas);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00015753 File Offset: 0x00013953
		public static void MakeProceduralTexturesReadable(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			Debug.LogError("TODO this should be done as close to textures being used as possible due to memory issues.");
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0001575F File Offset: 0x0001395F
		public virtual IEnumerator ConvertTexturesToReadableFormats(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
			{
				for (int j = 0; j < data.texPropertyNames.Count; j++)
				{
					MeshBakerMaterialTexture meshBakerMaterialTexture = data.distinctMaterialTextures[i].ts[j];
					if (!meshBakerMaterialTexture.isNull && textureEditorMethods != null)
					{
						Texture texture2D = meshBakerMaterialTexture.GetTexture2D();
						if (progressInfo != null)
						{
							progressInfo(string.Format("Convert texture {0} to readable format ", texture2D), 0.5f);
						}
						textureEditorMethods.AddTextureFormat((Texture2D)texture2D, data.texPropertyNames[j].isNormalMap);
					}
				}
			}
			yield break;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0001577D File Offset: 0x0001397D
		public virtual AtlasPackingResult[] CalculateAtlasRectangles(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			return MB3_TextureCombinerPackerRoot.CalculateAtlasRectanglesStatic(data, doMultiAtlas, LOG_LEVEL);
		}

		// Token: 0x06001C90 RID: 7312
		public abstract IEnumerator CreateAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, AtlasPackingResult packedAtlasRects, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL);
	}
}

using System;
using System.Collections;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200049A RID: 1178
	internal class MB3_TextureCombinerPackerOneTextureInAtlas : MB_ITextureCombinerPacker
	{
		// Token: 0x06001DA9 RID: 7593 RVA: 0x00016058 File Offset: 0x00014258
		public IEnumerator ConvertTexturesToReadableFormats(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			yield break;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000C1CB0 File Offset: 0x000BFEB0
		public AtlasPackingResult[] CalculateAtlasRectangles(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Only one image per atlas. Will re-use original texture");
			}
			AtlasPackingResult[] array = new AtlasPackingResult[1];
			AtlasPadding[] pds = new AtlasPadding[]
			{
				new AtlasPadding(data._atlasPadding)
			};
			array[0] = new AtlasPackingResult(pds);
			array[0].rects = new Rect[1];
			array[0].srcImgIdxs = new int[1];
			array[0].rects[0] = new Rect(0f, 0f, 1f, 1f);
			MeshBakerMaterialTexture meshBakerMaterialTexture = null;
			if (data.distinctMaterialTextures[0].ts.Length != 0)
			{
				meshBakerMaterialTexture = data.distinctMaterialTextures[0].ts[0];
			}
			if (meshBakerMaterialTexture == null || meshBakerMaterialTexture.isNull)
			{
				array[0].atlasX = 16;
				array[0].atlasY = 16;
				array[0].usedW = 16;
				array[0].usedH = 16;
			}
			else
			{
				array[0].atlasX = meshBakerMaterialTexture.width;
				array[0].atlasY = meshBakerMaterialTexture.height;
				array[0].usedW = meshBakerMaterialTexture.width;
				array[0].usedH = meshBakerMaterialTexture.height;
			}
			return array;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00016060 File Offset: 0x00014260
		public IEnumerator CreateAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, AtlasPackingResult packedAtlasRects, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Only one image per atlas. Will re-use original texture");
			}
			for (int i = 0; i < data.numAtlases; i++)
			{
				MeshBakerMaterialTexture meshBakerMaterialTexture = data.distinctMaterialTextures[0].ts[i];
				atlases[i] = meshBakerMaterialTexture.GetTexture2D();
				data.resultMaterial.SetTexture(data.texPropertyNames[i].name, atlases[i]);
				data.resultMaterial.SetTextureScale(data.texPropertyNames[i].name, Vector2.one);
				data.resultMaterial.SetTextureOffset(data.texPropertyNames[i].name, Vector2.zero);
			}
			yield break;
		}
	}
}

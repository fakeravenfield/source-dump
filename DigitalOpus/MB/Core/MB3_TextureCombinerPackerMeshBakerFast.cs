using System;
using System.Collections;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000492 RID: 1170
	internal class MB3_TextureCombinerPackerMeshBakerFast : MB_ITextureCombinerPacker
	{
		// Token: 0x06001D80 RID: 7552 RVA: 0x00015EFF File Offset: 0x000140FF
		public IEnumerator ConvertTexturesToReadableFormats(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			yield break;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0001577D File Offset: 0x0001397D
		public virtual AtlasPackingResult[] CalculateAtlasRectangles(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			return MB3_TextureCombinerPackerRoot.CalculateAtlasRectanglesStatic(data, doMultiAtlas, LOG_LEVEL);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00015F07 File Offset: 0x00014107
		public IEnumerator CreateAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, AtlasPackingResult packedAtlasRects, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			Rect[] rects = packedAtlasRects.rects;
			int atlasX = packedAtlasRects.atlasX;
			int atlasY = packedAtlasRects.atlasY;
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Generated atlas will be " + atlasX.ToString() + "x" + atlasY.ToString());
			}
			GameObject gameObject = null;
			try
			{
				gameObject = new GameObject("MBrenderAtlasesGO");
				MB3_AtlasPackerRenderTexture mb3_AtlasPackerRenderTexture = gameObject.AddComponent<MB3_AtlasPackerRenderTexture>();
				gameObject.AddComponent<Camera>();
				if (data._considerNonTextureProperties && LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Blend Non-Texture Properties has limited functionality when used with Mesh Baker Texture Packer Fast.");
				}
				for (int i = 0; i < data.numAtlases; i++)
				{
					Texture2D texture2D;
					if (!MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(i, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
					{
						texture2D = null;
						if (LOG_LEVEL >= MB2_LogLevel.debug)
						{
							Debug.Log("Not creating atlas for " + data.texPropertyNames[i].name + " because textures are null and default value parameters are the same.");
						}
					}
					else
					{
						GC.Collect();
						MB3_TextureCombinerPackerRoot.CreateTemporaryTexturesForAtlas(data.distinctMaterialTextures, combiner, i, data);
						if (progressInfo != null)
						{
							progressInfo("Creating Atlas '" + data.texPropertyNames[i].name + "'", 0.01f);
						}
						if (LOG_LEVEL >= MB2_LogLevel.debug)
						{
							Debug.Log("About to render " + data.texPropertyNames[i].name + " isNormal=" + data.texPropertyNames[i].isNormalMap.ToString());
						}
						mb3_AtlasPackerRenderTexture.LOG_LEVEL = LOG_LEVEL;
						mb3_AtlasPackerRenderTexture.width = atlasX;
						mb3_AtlasPackerRenderTexture.height = atlasY;
						mb3_AtlasPackerRenderTexture.padding = data._atlasPadding;
						mb3_AtlasPackerRenderTexture.rects = rects;
						mb3_AtlasPackerRenderTexture.textureSets = data.distinctMaterialTextures;
						mb3_AtlasPackerRenderTexture.indexOfTexSetToRender = i;
						mb3_AtlasPackerRenderTexture.texPropertyName = data.texPropertyNames[i];
						mb3_AtlasPackerRenderTexture.isNormalMap = data.texPropertyNames[i].isNormalMap;
						mb3_AtlasPackerRenderTexture.fixOutOfBoundsUVs = data._fixOutOfBoundsUVs;
						mb3_AtlasPackerRenderTexture.considerNonTextureProperties = data._considerNonTextureProperties;
						mb3_AtlasPackerRenderTexture.resultMaterialTextureBlender = data.nonTexturePropertyBlender;
						texture2D = mb3_AtlasPackerRenderTexture.OnRenderAtlas(combiner);
						if (LOG_LEVEL >= MB2_LogLevel.debug)
						{
							Debug.Log(string.Concat(new string[]
							{
								"Saving atlas ",
								data.texPropertyNames[i].name,
								" w=",
								texture2D.width.ToString(),
								" h=",
								texture2D.height.ToString(),
								" id=",
								texture2D.GetInstanceID().ToString()
							}));
						}
					}
					atlases[i] = texture2D;
					if (progressInfo != null)
					{
						progressInfo("Saving atlas: '" + data.texPropertyNames[i].name + "'", 0.04f);
					}
					if (data._saveAtlasesAsAssets && textureEditorMethods != null)
					{
						textureEditorMethods.SaveAtlasToAssetDatabase(atlases[i], data.texPropertyNames[i], i, data.resultMaterial);
					}
					else
					{
						data.resultMaterial.SetTexture(data.texPropertyNames[i].name, atlases[i]);
					}
					data.resultMaterial.SetTextureOffset(data.texPropertyNames[i].name, Vector2.zero);
					data.resultMaterial.SetTextureScale(data.texPropertyNames[i].name, Vector2.one);
					combiner._destroyTemporaryTextures(data.texPropertyNames[i].name);
				}
				yield break;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				yield break;
			}
			finally
			{
				if (gameObject != null)
				{
					MB_Utility.Destroy(gameObject);
				}
			}
			yield break;
		}
	}
}

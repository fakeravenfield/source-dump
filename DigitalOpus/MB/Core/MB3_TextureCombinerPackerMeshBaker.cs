using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200048F RID: 1167
	internal class MB3_TextureCombinerPackerMeshBaker : MB3_TextureCombinerPackerRoot
	{
		// Token: 0x06001D71 RID: 7537 RVA: 0x00015E8C File Offset: 0x0001408C
		public override IEnumerator CreateAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, AtlasPackingResult packedAtlasRects, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			Rect[] uvRects = packedAtlasRects.rects;
			int atlasSizeX = packedAtlasRects.atlasX;
			int atlasSizeY = packedAtlasRects.atlasY;
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Generated atlas will be " + atlasSizeX.ToString() + "x" + atlasSizeY.ToString());
			}
			int num3;
			for (int propIdx = 0; propIdx < data.numAtlases; propIdx = num3 + 1)
			{
				ShaderTextureProperty property = data.texPropertyNames[propIdx];
				Texture2D texture2D;
				if (!MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(propIdx, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
				{
					texture2D = null;
					if (LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("=== Not creating atlas for " + property.name + " because textures are null and default value parameters are the same.");
					}
				}
				else
				{
					if (LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("=== Creating atlas for " + property.name);
					}
					GC.Collect();
					MB3_TextureCombinerPackerRoot.CreateTemporaryTexturesForAtlas(data.distinctMaterialTextures, combiner, propIdx, data);
					Color[][] atlasPixels = new Color[atlasSizeY][];
					for (int i = 0; i < atlasPixels.Length; i++)
					{
						atlasPixels[i] = new Color[atlasSizeX];
					}
					bool isNormalMap = false;
					if (property.isNormalMap)
					{
						isNormalMap = true;
					}
					for (int texSetIdx = 0; texSetIdx < data.distinctMaterialTextures.Count; texSetIdx = num3 + 1)
					{
						MB_TexSet mb_TexSet = data.distinctMaterialTextures[texSetIdx];
						MeshBakerMaterialTexture meshBakerMaterialTexture = mb_TexSet.ts[propIdx];
						string text = "Creating Atlas '" + property.name + "' texture " + meshBakerMaterialTexture.GetTexName();
						if (progressInfo != null)
						{
							progressInfo(text, 0.01f);
						}
						if (LOG_LEVEL >= MB2_LogLevel.trace)
						{
							Debug.Log(string.Format("Adding texture {0} to atlas {1} for texSet {2} srcMat {3}", new object[]
							{
								meshBakerMaterialTexture.GetTexName(),
								property.name,
								texSetIdx,
								mb_TexSet.matsAndGOs.mats[0].GetMaterialName()
							}));
						}
						Rect rect = uvRects[texSetIdx];
						Texture2D texture2D2 = mb_TexSet.ts[propIdx].GetTexture2D();
						int targX = Mathf.RoundToInt(rect.x * (float)atlasSizeX);
						int targY = Mathf.RoundToInt(rect.y * (float)atlasSizeY);
						int num = Mathf.RoundToInt(rect.width * (float)atlasSizeX);
						int num2 = Mathf.RoundToInt(rect.height * (float)atlasSizeY);
						if (num == 0 || num2 == 0)
						{
							string str = "Image in atlas has no height or width ";
							Rect rect2 = rect;
							Debug.LogError(str + rect2.ToString());
						}
						if (progressInfo != null)
						{
							progressInfo(text + " set ReadWrite flag", 0.01f);
						}
						if (textureEditorMethods != null)
						{
							textureEditorMethods.SetReadWriteFlag(texture2D2, true, true);
						}
						if (progressInfo != null)
						{
							progressInfo(text + "Copying to atlas: '" + meshBakerMaterialTexture.GetTexName() + "'", 0.02f);
						}
						DRect encapsulatingSamplingRect = mb_TexSet.ts[propIdx].GetEncapsulatingSamplingRect();
						yield return MB3_TextureCombinerPackerMeshBaker.CopyScaledAndTiledToAtlas(mb_TexSet.ts[propIdx], mb_TexSet, property, encapsulatingSamplingRect, targX, targY, num, num2, packedAtlasRects.padding[texSetIdx], atlasPixels, isNormalMap, data, combiner, progressInfo, LOG_LEVEL);
						num3 = texSetIdx;
					}
					yield return data.numAtlases;
					if (progressInfo != null)
					{
						progressInfo("Applying changes to atlas: '" + property.name + "'", 0.03f);
					}
					texture2D = new Texture2D(atlasSizeX, atlasSizeY, TextureFormat.ARGB32, true);
					for (int j = 0; j < atlasPixels.Length; j++)
					{
						texture2D.SetPixels(0, j, atlasSizeX, 1, atlasPixels[j]);
					}
					texture2D.Apply();
					if (LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log(string.Concat(new string[]
						{
							"Saving atlas ",
							property.name,
							" w=",
							texture2D.width.ToString(),
							" h=",
							texture2D.height.ToString()
						}));
					}
					atlasPixels = null;
				}
				atlases[propIdx] = texture2D;
				if (progressInfo != null)
				{
					progressInfo("Saving atlas: '" + property.name + "'", 0.04f);
				}
				new Stopwatch().Start();
				if (data._saveAtlasesAsAssets && textureEditorMethods != null)
				{
					textureEditorMethods.SaveAtlasToAssetDatabase(atlases[propIdx], data.texPropertyNames[propIdx], propIdx, data.resultMaterial);
				}
				else
				{
					data.resultMaterial.SetTexture(data.texPropertyNames[propIdx].name, atlases[propIdx]);
				}
				data.resultMaterial.SetTextureOffset(data.texPropertyNames[propIdx].name, Vector2.zero);
				data.resultMaterial.SetTextureScale(data.texPropertyNames[propIdx].name, Vector2.one);
				combiner._destroyTemporaryTextures(data.texPropertyNames[propIdx].name);
				property = null;
				num3 = propIdx;
			}
			yield break;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		internal static IEnumerator CopyScaledAndTiledToAtlas(MeshBakerMaterialTexture source, MB_TexSet sourceMaterial, ShaderTextureProperty shaderPropertyName, DRect srcSamplingRect, int targX, int targY, int targW, int targH, AtlasPadding padding, Color[][] atlasPixels, bool isNormalMap, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, ProgressUpdateDelegate progressInfo = null, MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info)
		{
			Texture2D texture2D = source.GetTexture2D();
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("CopyScaledAndTiledToAtlas: {0} inAtlasX={1} inAtlasY={2} inAtlasW={3} inAtlasH={4} paddX={5} paddY={6} srcSamplingRect={7}", new object[]
				{
					texture2D,
					targX,
					targY,
					targW,
					targH,
					padding.leftRight,
					padding.topBottom,
					srcSamplingRect
				}));
			}
			float num = (float)targW;
			float num2 = (float)targH;
			float num3 = (float)srcSamplingRect.width;
			float num4 = (float)srcSamplingRect.height;
			float num5 = (float)srcSamplingRect.x;
			float num6 = (float)srcSamplingRect.y;
			int w = (int)num;
			int h = (int)num2;
			if (data._considerNonTextureProperties)
			{
				texture2D = combiner._createTextureCopy(shaderPropertyName.name, texture2D);
				texture2D = data.nonTexturePropertyBlender.TintTextureWithTextureCombiner(texture2D, sourceMaterial, shaderPropertyName);
			}
			for (int k = 0; k < w; k++)
			{
				if (progressInfo != null && w > 0)
				{
					progressInfo("CopyScaledAndTiledToAtlas " + ((float)k / (float)w * 100f).ToString("F0"), 0.2f);
				}
				for (int l = 0; l < h; l++)
				{
					float u = (float)k / num * num3 + num5;
					float v = (float)l / num2 * num4 + num6;
					atlasPixels[targY + l][targX + k] = texture2D.GetPixelBilinear(u, v);
				}
			}
			for (int m = 0; m < w; m++)
			{
				for (int n = 1; n <= padding.topBottom; n++)
				{
					atlasPixels[targY - n][targX + m] = atlasPixels[targY][targX + m];
					atlasPixels[targY + h - 1 + n][targX + m] = atlasPixels[targY + h - 1][targX + m];
				}
			}
			for (int num7 = 0; num7 < h; num7++)
			{
				for (int num8 = 1; num8 <= padding.leftRight; num8++)
				{
					atlasPixels[targY + num7][targX - num8] = atlasPixels[targY + num7][targX];
					atlasPixels[targY + num7][targX + w + num8 - 1] = atlasPixels[targY + num7][targX + w - 1];
				}
			}
			int num9;
			for (int i = 1; i <= padding.leftRight; i = num9 + 1)
			{
				for (int j = 1; j <= padding.topBottom; j = num9 + 1)
				{
					atlasPixels[targY - j][targX - i] = atlasPixels[targY][targX];
					atlasPixels[targY + h - 1 + j][targX - i] = atlasPixels[targY + h - 1][targX];
					atlasPixels[targY + h - 1 + j][targX + w + i - 1] = atlasPixels[targY + h - 1][targX + w - 1];
					atlasPixels[targY - j][targX + w + i - 1] = atlasPixels[targY][targX + w - 1];
					yield return null;
					num9 = j;
				}
				yield return null;
				num9 = i;
			}
			yield break;
		}
	}
}

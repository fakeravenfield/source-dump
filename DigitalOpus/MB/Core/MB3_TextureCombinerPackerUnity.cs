using System;
using System.Collections;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200049D RID: 1181
	internal class MB3_TextureCombinerPackerUnity : MB3_TextureCombinerPackerRoot
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x000160AD File Offset: 0x000142AD
		public override AtlasPackingResult[] CalculateAtlasRectangles(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doMultiAtlas, MB2_LogLevel LOG_LEVEL)
		{
			return new AtlasPackingResult[]
			{
				new AtlasPackingResult(new AtlasPadding[0])
			};
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000160C3 File Offset: 0x000142C3
		public override IEnumerator CreateAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, AtlasPackingResult packedAtlasRects, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			Rect[] array = packedAtlasRects.rects;
			long num = 0L;
			int w = 1;
			int h = 1;
			array = null;
			for (int i = 0; i < data.numAtlases; i++)
			{
				ShaderTextureProperty shaderTextureProperty = data.texPropertyNames[i];
				Texture2D texture2D;
				if (!MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(i, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
				{
					texture2D = null;
				}
				else
				{
					if (LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.LogWarning("Beginning loop " + i.ToString() + " num temporary textures " + combiner._getNumTemporaryTextures().ToString());
					}
					MB3_TextureCombinerPackerRoot.CreateTemporaryTexturesForAtlas(data.distinctMaterialTextures, combiner, i, data);
					Texture2D[] array2 = new Texture2D[data.distinctMaterialTextures.Count];
					for (int j = 0; j < data.distinctMaterialTextures.Count; j++)
					{
						MB_TexSet mb_TexSet = data.distinctMaterialTextures[j];
						int idealWidth = mb_TexSet.idealWidth;
						int idealHeight = mb_TexSet.idealHeight;
						Texture2D texture2D2 = mb_TexSet.ts[i].GetTexture2D();
						if (progressInfo != null)
						{
							string str = "Adjusting for scale and offset ";
							Texture2D texture2D3 = texture2D2;
							progressInfo(str + ((texture2D3 != null) ? texture2D3.ToString() : null), 0.01f);
						}
						if (textureEditorMethods != null)
						{
							textureEditorMethods.SetReadWriteFlag(texture2D2, true, true);
						}
						texture2D2 = MB3_TextureCombinerPackerUnity.GetAdjustedForScaleAndOffset2(shaderTextureProperty.name, mb_TexSet.ts[i], mb_TexSet.obUVoffset, mb_TexSet.obUVscale, data, combiner, LOG_LEVEL);
						if (texture2D2.width != idealWidth || texture2D2.height != idealHeight)
						{
							if (progressInfo != null)
							{
								string str2 = "Resizing texture '";
								Texture2D texture2D4 = texture2D2;
								progressInfo(str2 + ((texture2D4 != null) ? texture2D4.ToString() : null) + "'", 0.01f);
							}
							if (LOG_LEVEL >= MB2_LogLevel.debug)
							{
								Debug.LogWarning(string.Concat(new string[]
								{
									"Copying and resizing texture ",
									shaderTextureProperty.name,
									" from ",
									texture2D2.width.ToString(),
									"x",
									texture2D2.height.ToString(),
									" to ",
									idealWidth.ToString(),
									"x",
									idealHeight.ToString()
								}));
							}
							texture2D2 = combiner._resizeTexture(shaderTextureProperty.name, texture2D2, idealWidth, idealHeight);
						}
						num += (long)(texture2D2.width * texture2D2.height);
						if (data._considerNonTextureProperties)
						{
							texture2D2 = combiner._createTextureCopy(shaderTextureProperty.name, texture2D2);
							data.nonTexturePropertyBlender.TintTextureWithTextureCombiner(texture2D2, data.distinctMaterialTextures[j], shaderTextureProperty);
						}
						array2[j] = texture2D2;
					}
					if (textureEditorMethods != null)
					{
						textureEditorMethods.CheckBuildSettings(num);
					}
					if (Math.Sqrt((double)num) > 3500.0 && LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("The maximum possible atlas size is 4096. Textures may be shrunk");
					}
					texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, true);
					if (progressInfo != null)
					{
						progressInfo("Packing texture atlas " + shaderTextureProperty.name, 0.25f);
					}
					if (i == 0)
					{
						if (progressInfo != null)
						{
							progressInfo("Estimated min size of atlases: " + Math.Sqrt((double)num).ToString("F0"), 0.1f);
						}
						if (LOG_LEVEL >= MB2_LogLevel.info)
						{
							Debug.Log("Estimated atlas minimum size:" + Math.Sqrt((double)num).ToString("F0"));
						}
						int maximumAtlasSize = 4096;
						array = texture2D.PackTextures(array2, data._atlasPadding, maximumAtlasSize, false);
						if (LOG_LEVEL >= MB2_LogLevel.info)
						{
							Debug.Log("After pack textures atlas size " + texture2D.width.ToString() + " " + texture2D.height.ToString());
						}
						w = texture2D.width;
						h = texture2D.height;
						texture2D.Apply();
					}
					else
					{
						if (progressInfo != null)
						{
							progressInfo("Copying Textures Into: " + shaderTextureProperty.name, 0.1f);
						}
						texture2D = MB3_TextureCombinerPackerUnity._copyTexturesIntoAtlas(array2, data._atlasPadding, array, w, h, combiner);
					}
				}
				atlases[i] = texture2D;
				if (data._saveAtlasesAsAssets && textureEditorMethods != null)
				{
					textureEditorMethods.SaveAtlasToAssetDatabase(atlases[i], shaderTextureProperty, i, data.resultMaterial);
				}
				data.resultMaterial.SetTextureOffset(shaderTextureProperty.name, Vector2.zero);
				data.resultMaterial.SetTextureScale(shaderTextureProperty.name, Vector2.one);
				combiner._destroyTemporaryTextures(shaderTextureProperty.name);
				GC.Collect();
			}
			packedAtlasRects.rects = array;
			yield break;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000C1EF0 File Offset: 0x000C00F0
		internal static Texture2D _copyTexturesIntoAtlas(Texture2D[] texToPack, int padding, Rect[] rs, int w, int h, MB3_TextureCombiner combiner)
		{
			Texture2D texture2D = new Texture2D(w, h, TextureFormat.ARGB32, true);
			MB_Utility.setSolidColor(texture2D, Color.clear);
			for (int i = 0; i < rs.Length; i++)
			{
				Rect rect = rs[i];
				Texture2D texture2D2 = texToPack[i];
				Texture2D texture2D3 = null;
				int x = Mathf.RoundToInt(rect.x * (float)w);
				int y = Mathf.RoundToInt(rect.y * (float)h);
				int num = Mathf.RoundToInt(rect.width * (float)w);
				int num2 = Mathf.RoundToInt(rect.height * (float)h);
				if (texture2D2.width != num && texture2D2.height != num2)
				{
					texture2D2 = (texture2D3 = MB_Utility.resampleTexture(texture2D2, num, num2));
				}
				texture2D.SetPixels(x, y, num, num2, texture2D2.GetPixels());
				if (texture2D3 != null)
				{
					MB_Utility.Destroy(texture2D3);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000C1FCC File Offset: 0x000C01CC
		internal static Texture2D GetAdjustedForScaleAndOffset2(string propertyName, MeshBakerMaterialTexture source, Vector2 obUVoffset, Vector2 obUVscale, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_LogLevel LOG_LEVEL)
		{
			Texture2D texture2D = source.GetTexture2D();
			if (source.matTilingRect.x == 0.0 && source.matTilingRect.y == 0.0 && source.matTilingRect.width == 1.0 && source.matTilingRect.height == 1.0)
			{
				if (!data._fixOutOfBoundsUVs)
				{
					return texture2D;
				}
				if (obUVoffset.x == 0f && obUVoffset.y == 0f && obUVscale.x == 1f && obUVscale.y == 1f)
				{
					return texture2D;
				}
			}
			Vector2 adjustedForScaleAndOffset2Dimensions = MB3_TextureCombinerPipeline.GetAdjustedForScaleAndOffset2Dimensions(source, obUVoffset, obUVscale, data, LOG_LEVEL);
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				string[] array = new string[6];
				array[0] = "GetAdjustedForScaleAndOffset2: ";
				int num = 1;
				Texture2D texture2D2 = texture2D;
				array[num] = ((texture2D2 != null) ? texture2D2.ToString() : null);
				array[2] = " ";
				int num2 = 3;
				Vector2 vector = obUVoffset;
				array[num2] = vector.ToString();
				array[4] = " ";
				int num3 = 5;
				vector = obUVscale;
				array[num3] = vector.ToString();
				Debug.LogWarning(string.Concat(array));
			}
			float x = adjustedForScaleAndOffset2Dimensions.x;
			float y = adjustedForScaleAndOffset2Dimensions.y;
			float num4 = (float)source.matTilingRect.width;
			float num5 = (float)source.matTilingRect.height;
			float num6 = (float)source.matTilingRect.x;
			float num7 = (float)source.matTilingRect.y;
			if (data._fixOutOfBoundsUVs)
			{
				num4 *= obUVscale.x;
				num5 *= obUVscale.y;
				num6 = (float)(source.matTilingRect.x * (double)obUVscale.x + (double)obUVoffset.x);
				num7 = (float)(source.matTilingRect.y * (double)obUVscale.y + (double)obUVoffset.y);
			}
			Texture2D texture2D3 = combiner._createTemporaryTexture(propertyName, (int)x, (int)y, TextureFormat.ARGB32, true);
			for (int i = 0; i < texture2D3.width; i++)
			{
				for (int j = 0; j < texture2D3.height; j++)
				{
					float u = (float)i / x * num4 + num6;
					float v = (float)j / y * num5 + num7;
					texture2D3.SetPixel(i, j, texture2D.GetPixelBilinear(u, v));
				}
			}
			texture2D3.Apply();
			return texture2D3;
		}
	}
}

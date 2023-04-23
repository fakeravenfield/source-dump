using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200049F RID: 1183
	public class MB3_TextureCombinerPipeline
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x000C2768 File Offset: 0x000C0968
		internal static bool _ShouldWeCreateAtlasForThisProperty(int propertyIndex, bool considerNonTextureProperties, MB3_TextureCombinerPipeline.CreateAtlasForProperty[] allTexturesAreNullAndSameColor)
		{
			MB3_TextureCombinerPipeline.CreateAtlasForProperty createAtlasForProperty = allTexturesAreNullAndSameColor[propertyIndex];
			if (considerNonTextureProperties)
			{
				return !createAtlasForProperty.allNonTexturePropsAreSame || !createAtlasForProperty.allTexturesAreNull;
			}
			return !createAtlasForProperty.allTexturesAreNull;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000C27A0 File Offset: 0x000C09A0
		internal static bool _CollectPropertyNames(MB3_TextureCombinerPipeline.TexturePipelineData data, MB2_LogLevel LOG_LEVEL)
		{
			int i;
			Predicate<ShaderTextureProperty> <>9__0;
			int l;
			for (i = 0; i < data.texPropertyNames.Count; i = l + 1)
			{
				List<ShaderTextureProperty> customShaderPropNames = data._customShaderPropNames;
				Predicate<ShaderTextureProperty> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((ShaderTextureProperty x) => x.name.Equals(data.texPropertyNames[i].name)));
				}
				ShaderTextureProperty shaderTextureProperty = customShaderPropNames.Find(match);
				if (shaderTextureProperty != null)
				{
					data._customShaderPropNames.Remove(shaderTextureProperty);
				}
				l = i;
			}
			Material resultMaterial = data.resultMaterial;
			if (resultMaterial == null)
			{
				Debug.LogError("Please assign a result material. The combined mesh will use this material.");
				return false;
			}
			string str = "";
			for (int j = 0; j < MB3_TextureCombinerPipeline.shaderTexPropertyNames.Length; j++)
			{
				if (resultMaterial.HasProperty(MB3_TextureCombinerPipeline.shaderTexPropertyNames[j].name))
				{
					str = str + ", " + MB3_TextureCombinerPipeline.shaderTexPropertyNames[j].name;
					if (!data.texPropertyNames.Contains(MB3_TextureCombinerPipeline.shaderTexPropertyNames[j]))
					{
						data.texPropertyNames.Add(MB3_TextureCombinerPipeline.shaderTexPropertyNames[j]);
					}
					if (resultMaterial.GetTextureOffset(MB3_TextureCombinerPipeline.shaderTexPropertyNames[j].name) != new Vector2(0f, 0f) && LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result material has non-zero offset. This is may be incorrect.");
					}
					if (resultMaterial.GetTextureScale(MB3_TextureCombinerPipeline.shaderTexPropertyNames[j].name) != new Vector2(1f, 1f) && LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result material should have tiling of 1,1");
					}
				}
			}
			for (int k = 0; k < data._customShaderPropNames.Count; k++)
			{
				if (resultMaterial.HasProperty(data._customShaderPropNames[k].name))
				{
					str = str + ", " + data._customShaderPropNames[k].name;
					data.texPropertyNames.Add(data._customShaderPropNames[k]);
					if (resultMaterial.GetTextureOffset(data._customShaderPropNames[k].name) != new Vector2(0f, 0f) && LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result material has non-zero offset. This is probably incorrect.");
					}
					if (resultMaterial.GetTextureScale(data._customShaderPropNames[k].name) != new Vector2(1f, 1f) && LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result material should probably have tiling of 1,1.");
					}
				}
				else if (LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Result material shader does not use property " + data._customShaderPropNames[k].name + " in the list of custom shader property names");
				}
			}
			return true;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000C2A88 File Offset: 0x000C0C88
		internal static bool _ShouldWeCreateAtlasForThisProperty(int propertyIndex, MB3_TextureCombinerPipeline.CreateAtlasForProperty[] allTexturesAreNullAndSameColor, MB3_TextureCombinerPipeline.TexturePipelineData data)
		{
			MB3_TextureCombinerPipeline.CreateAtlasForProperty createAtlasForProperty = allTexturesAreNullAndSameColor[propertyIndex];
			if (data._considerNonTextureProperties)
			{
				return !createAtlasForProperty.allNonTexturePropsAreSame || !createAtlasForProperty.allTexturesAreNull;
			}
			return !createAtlasForProperty.allTexturesAreNull;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000C2AC4 File Offset: 0x000C0CC4
		public static Texture GetTextureConsideringStandardShaderKeywords(string shaderName, Material mat, string propertyName)
		{
			if ((!shaderName.Equals("Standard") && !shaderName.Equals("Standard (Specular setup)") && !shaderName.Equals("Standard (Roughness setup")) || !propertyName.Equals("_EmissionMap"))
			{
				return mat.GetTexture(propertyName);
			}
			if (mat.IsKeywordEnabled("_EMISSION"))
			{
				return mat.GetTexture(propertyName);
			}
			return null;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00016117 File Offset: 0x00014317
		internal static IEnumerator __Step1_CollectDistinctMatTexturesAndUsedObjects(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, List<GameObject> usedObjsToMesh, MB2_LogLevel LOG_LEVEL)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = false;
			Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
			for (int i = 0; i < data.allObjsToMesh.Count; i++)
			{
				GameObject gameObject = data.allObjsToMesh[i];
				if (progressInfo != null)
				{
					string str = "Collecting textures for ";
					GameObject gameObject2 = gameObject;
					progressInfo(str + ((gameObject2 != null) ? gameObject2.ToString() : null), (float)i / (float)data.allObjsToMesh.Count / 2f);
				}
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					string str2 = "Collecting textures for object ";
					GameObject gameObject3 = gameObject;
					Debug.Log(str2 + ((gameObject3 != null) ? gameObject3.ToString() : null));
				}
				if (gameObject == null)
				{
					Debug.LogError("The list of objects to mesh contained nulls.");
					result.success = false;
					yield break;
				}
				Mesh mesh = MB_Utility.GetMesh(gameObject);
				if (mesh == null)
				{
					Debug.LogError("Object " + gameObject.name + " in the list of objects to mesh has no mesh.");
					result.success = false;
					yield break;
				}
				Material[] gomaterials = MB_Utility.GetGOMaterials(gameObject);
				if (gomaterials.Length == 0)
				{
					Debug.LogError("Object " + gameObject.name + " in the list of objects has no materials.");
					result.success = false;
					yield break;
				}
				MB_Utility.MeshAnalysisResult[] array;
				if (!dictionary.TryGetValue(mesh.GetInstanceID(), out array))
				{
					array = new MB_Utility.MeshAnalysisResult[mesh.subMeshCount];
					for (int j = 0; j < mesh.subMeshCount; j++)
					{
						MB_Utility.hasOutOfBoundsUVs(mesh, ref array[j], j, 0);
						if (data._normalizeTexelDensity)
						{
							array[j].submeshArea = MB3_TextureCombinerPipeline.GetSubmeshArea(mesh, j);
						}
						if (data._fixOutOfBoundsUVs && !array[j].hasUVs)
						{
							array[j].uvRect = new Rect(0f, 0f, 1f, 1f);
							string str3 = "Mesh for object ";
							GameObject gameObject4 = gameObject;
							Debug.LogWarning(str3 + ((gameObject4 != null) ? gameObject4.ToString() : null) + " has no UV channel but 'consider UVs' is enabled. Assuming UVs will be generated filling 0,0,1,1 rectangle.");
						}
					}
					dictionary.Add(mesh.GetInstanceID(), array);
				}
				if (data._fixOutOfBoundsUVs && LOG_LEVEL >= MB2_LogLevel.trace)
				{
					string[] array2 = new string[8];
					array2[0] = "Mesh Analysis for object ";
					int num = 1;
					GameObject gameObject5 = gameObject;
					array2[num] = ((gameObject5 != null) ? gameObject5.ToString() : null);
					array2[2] = " numSubmesh=";
					array2[3] = array.Length.ToString();
					array2[4] = " HasOBUV=";
					array2[5] = array[0].hasOutOfBoundsUVs.ToString();
					array2[6] = " UVrectSubmesh0=";
					int num2 = 7;
					Rect uvRect = array[0].uvRect;
					array2[num2] = uvRect.ToString();
					Debug.Log(string.Concat(array2));
				}
				for (int k = 0; k < gomaterials.Length; k++)
				{
					if (progressInfo != null)
					{
						progressInfo(string.Format("Collecting textures for {0} submesh {1}", gameObject, k), (float)i / (float)data.allObjsToMesh.Count / 2f);
					}
					Material material = gomaterials[k];
					if (data.allowedMaterialsFilter == null || data.allowedMaterialsFilter.Contains(material))
					{
						flag = (flag || array[k].hasOutOfBoundsUVs);
						if (material.name.Contains("(Instance)"))
						{
							Debug.LogError("The sharedMaterial on object " + gameObject.name + " has been 'Instanced'. This was probably caused by a script accessing the meshRender.material property in the editor.  The material to UV Rectangle mapping will be incorrect. To fix this recreate the object from its prefab or re-assign its material from the correct asset.");
							result.success = false;
							yield break;
						}
						if (data._fixOutOfBoundsUVs && !MB_Utility.AreAllSharedMaterialsDistinct(gomaterials) && LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning("Object " + gameObject.name + " uses the same material on multiple submeshes. This may generate strange resultAtlasesAndRects especially when used with fix out of bounds uvs. Try duplicating the material.");
						}
						MeshBakerMaterialTexture[] array3 = new MeshBakerMaterialTexture[data.texPropertyNames.Count];
						for (int l = 0; l < data.texPropertyNames.Count; l++)
						{
							Texture texture = null;
							Vector2 matTilingScale = Vector2.one;
							Vector2 matTilingOffset = Vector2.zero;
							float texelDens = 0f;
							if (material.HasProperty(data.texPropertyNames[l].name))
							{
								Texture textureConsideringStandardShaderKeywords = MB3_TextureCombinerPipeline.GetTextureConsideringStandardShaderKeywords(data.resultMaterial.shader.name, material, data.texPropertyNames[l].name);
								if (textureConsideringStandardShaderKeywords != null)
								{
									if (!(textureConsideringStandardShaderKeywords is Texture2D))
									{
										Debug.LogError("Object " + gameObject.name + " in the list of objects to mesh uses a Texture that is not a Texture2D. Cannot build atlases.");
										result.success = false;
										yield break;
									}
									texture = textureConsideringStandardShaderKeywords;
									TextureFormat format = ((Texture2D)texture).format;
									bool flag2 = false;
									if (!Application.isPlaying && textureEditorMethods != null)
									{
										flag2 = textureEditorMethods.IsNormalMap((Texture2D)texture);
									}
									if ((format != TextureFormat.ARGB32 && format != TextureFormat.RGBA32 && format != TextureFormat.BGRA32 && format != TextureFormat.RGB24 && format != TextureFormat.Alpha8) || flag2)
									{
										if (Application.isPlaying && data._packingAlgorithm != MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Fast)
										{
											Debug.LogWarning(string.Concat(new string[]
											{
												"Object ",
												gameObject.name,
												" in the list of objects to mesh uses Texture ",
												texture.name,
												" uses format ",
												format.ToString(),
												" that is not in: ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT. These textures cannot be resized at runtime. Try changing texture format. If format says 'compressed' try changing it to 'truecolor'"
											}));
											result.success = false;
											yield break;
										}
										texture = (Texture2D)material.GetTexture(data.texPropertyNames[l].name);
									}
								}
								if (texture != null && data._normalizeTexelDensity)
								{
									if (array[l].submeshArea == 0f)
									{
										texelDens = 0f;
									}
									else
									{
										texelDens = (float)(texture.width * texture.height) / array[l].submeshArea;
									}
								}
								matTilingScale = material.GetTextureScale(data.texPropertyNames[l].name);
								matTilingOffset = material.GetTextureOffset(data.texPropertyNames[l].name);
							}
							array3[l] = new MeshBakerMaterialTexture(texture, matTilingOffset, matTilingScale, texelDens);
						}
						data.nonTexturePropertyBlender.CollectAverageValuesOfNonTextureProperties(data.resultMaterial, material);
						Vector2 vector = new Vector2(array[k].uvRect.width, array[k].uvRect.height);
						Vector2 vector2 = new Vector2(array[k].uvRect.x, array[k].uvRect.y);
						MB_TextureTilingTreatment treatment = MB_TextureTilingTreatment.none;
						if (data._fixOutOfBoundsUVs)
						{
							treatment = MB_TextureTilingTreatment.considerUVs;
						}
						MB_TexSet setOfTexs = new MB_TexSet(array3, vector2, vector, treatment);
						MatAndTransformToMerged item = new MatAndTransformToMerged(new DRect(vector2, vector), data._fixOutOfBoundsUVs, material);
						setOfTexs.matsAndGOs.mats.Add(item);
						MB_TexSet mb_TexSet = data.distinctMaterialTextures.Find((MB_TexSet x) => x.IsEqual(setOfTexs, data._fixOutOfBoundsUVs, data.nonTexturePropertyBlender));
						if (mb_TexSet != null)
						{
							setOfTexs = mb_TexSet;
						}
						else
						{
							data.distinctMaterialTextures.Add(setOfTexs);
						}
						if (!setOfTexs.matsAndGOs.mats.Contains(item))
						{
							setOfTexs.matsAndGOs.mats.Add(item);
						}
						if (!setOfTexs.matsAndGOs.gos.Contains(gameObject))
						{
							setOfTexs.matsAndGOs.gos.Add(gameObject);
							if (!usedObjsToMesh.Contains(gameObject))
							{
								usedObjsToMesh.Add(gameObject);
							}
						}
					}
				}
			}
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("Step1_CollectDistinctTextures collected {0} sets of textures fixOutOfBoundsUV={1} considerNonTextureProperties={2}", data.distinctMaterialTextures.Count, data._fixOutOfBoundsUVs, data._considerNonTextureProperties));
			}
			if (data.distinctMaterialTextures.Count == 0)
			{
				Debug.LogError("None of the source object materials matched any of the allowed materials for this submesh.");
				result.success = false;
				yield break;
			}
			new MB3_TextureCombinerMerging(data._considerNonTextureProperties, data.nonTexturePropertyBlender, data._fixOutOfBoundsUVs, LOG_LEVEL).MergeOverlappingDistinctMaterialTexturesAndCalcMaterialSubrects(data.distinctMaterialTextures);
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Total time Step1_CollectDistinctTextures " + stopwatch.ElapsedMilliseconds.ToString("f5"));
			}
			yield break;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000C2B24 File Offset: 0x000C0D24
		private static MB3_TextureCombinerPipeline.CreateAtlasForProperty[] CalculateAllTexturesAreNullAndSameColor(MB3_TextureCombinerPipeline.TexturePipelineData data, MB2_LogLevel LOG_LEVEL)
		{
			MB3_TextureCombinerPipeline.CreateAtlasForProperty[] array = new MB3_TextureCombinerPipeline.CreateAtlasForProperty[data.texPropertyNames.Count];
			for (int i = 0; i < data.texPropertyNames.Count; i++)
			{
				MeshBakerMaterialTexture meshBakerMaterialTexture = data.distinctMaterialTextures[0].ts[i];
				Color rhs = Color.black;
				if (data._considerNonTextureProperties)
				{
					rhs = data.nonTexturePropertyBlender.GetColorAsItWouldAppearInAtlasIfNoTexture(data.distinctMaterialTextures[0].matsAndGOs.mats[0].mat, data.texPropertyNames[i]);
				}
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int j = 0; j < data.distinctMaterialTextures.Count; j++)
				{
					if (!data.distinctMaterialTextures[j].ts[i].isNull)
					{
						num++;
					}
					if (meshBakerMaterialTexture.AreTexturesEqual(data.distinctMaterialTextures[j].ts[i]))
					{
						num2++;
					}
					if (data._considerNonTextureProperties && data.nonTexturePropertyBlender.GetColorAsItWouldAppearInAtlasIfNoTexture(data.distinctMaterialTextures[j].matsAndGOs.mats[0].mat, data.texPropertyNames[i]) == rhs)
					{
						num3++;
					}
				}
				array[i].allTexturesAreNull = (num == 0);
				array[i].allTexturesAreSame = (num2 == data.distinctMaterialTextures.Count);
				array[i].allNonTexturePropsAreSame = (num3 == data.distinctMaterialTextures.Count);
				if (LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("AllTexturesAreNullAndSameColor prop: {0} createAtlas:{1}  val:{2}", data.texPropertyNames[i].name, MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(i, data._considerNonTextureProperties, array), array[i]));
				}
			}
			return array;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0001614C File Offset: 0x0001434C
		internal static IEnumerator CalculateIdealSizesForTexturesInAtlasAndPadding(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB2_EditorMethodsInterface textureEditorMethods, MB2_LogLevel LOG_LEVEL)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			MeshBakerMaterialTexture.readyToBuildAtlases = true;
			data.allTexturesAreNullAndSameColor = MB3_TextureCombinerPipeline.CalculateAllTexturesAreNullAndSameColor(data, LOG_LEVEL);
			int num = data._atlasPadding;
			if (data.distinctMaterialTextures.Count == 1 && !data._fixOutOfBoundsUVs && !data._considerNonTextureProperties)
			{
				if (LOG_LEVEL >= MB2_LogLevel.info)
				{
					Debug.Log("All objects use the same textures in this set of atlases. Original textures will be reused instead of creating atlases.");
				}
				num = 0;
				data.distinctMaterialTextures[0].SetThisIsOnlyTexSetInAtlasTrue();
				data.distinctMaterialTextures[0].SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(MB_TextureTilingTreatment.edgeToEdgeXY);
			}
			for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
			{
				if (LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Calculating ideal sizes for texSet TexSet " + i.ToString() + " of " + data.distinctMaterialTextures.Count.ToString());
				}
				MB_TexSet mb_TexSet = data.distinctMaterialTextures[i];
				mb_TexSet.idealWidth = 1;
				mb_TexSet.idealHeight = 1;
				int num2 = 1;
				int num3 = 1;
				for (int j = 0; j < data.texPropertyNames.Count; j++)
				{
					if (MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(j, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
					{
						MeshBakerMaterialTexture meshBakerMaterialTexture = mb_TexSet.ts[j];
						if (LOG_LEVEL >= MB2_LogLevel.trace)
						{
							Debug.Log(string.Format("Calculating ideal size for texSet {0} property {1}", i, data.texPropertyNames[j].name));
						}
						if (!meshBakerMaterialTexture.matTilingRect.size.Equals(Vector2.one) && data.distinctMaterialTextures.Count > 1 && LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Texture ",
								meshBakerMaterialTexture.GetTexName(),
								"is tiled by ",
								meshBakerMaterialTexture.matTilingRect.size.ToString(),
								" tiling will be baked into a texture with maxSize:",
								data._maxTilingBakeSize.ToString()
							}));
						}
						if (!mb_TexSet.obUVscale.Equals(Vector2.one) && data.distinctMaterialTextures.Count > 1 && data._fixOutOfBoundsUVs && LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Texture ",
								meshBakerMaterialTexture.GetTexName(),
								" has out of bounds UVs that effectively tile by ",
								mb_TexSet.obUVscale.ToString(),
								" tiling will be baked into a texture with maxSize:",
								data._maxTilingBakeSize.ToString()
							}));
						}
						if (meshBakerMaterialTexture.isNull)
						{
							mb_TexSet.SetEncapsulatingRect(j, data._fixOutOfBoundsUVs);
							if (LOG_LEVEL >= MB2_LogLevel.trace)
							{
								Debug.Log(string.Format("No source texture creating a 16x16 texture for {0} texSet {1} srcMat {2}", data.texPropertyNames[j].name, i, mb_TexSet.matsAndGOs.mats[0].GetMaterialName()));
							}
						}
						if (!meshBakerMaterialTexture.isNull)
						{
							Vector2 adjustedForScaleAndOffset2Dimensions = MB3_TextureCombinerPipeline.GetAdjustedForScaleAndOffset2Dimensions(meshBakerMaterialTexture, mb_TexSet.obUVoffset, mb_TexSet.obUVscale, data, LOG_LEVEL);
							if ((int)(adjustedForScaleAndOffset2Dimensions.x * adjustedForScaleAndOffset2Dimensions.y) > num2 * num3)
							{
								if (LOG_LEVEL >= MB2_LogLevel.trace)
								{
									string[] array = new string[8];
									array[0] = "    matTex ";
									array[1] = meshBakerMaterialTexture.GetTexName();
									array[2] = " ";
									int num4 = 3;
									Vector2 vector = adjustedForScaleAndOffset2Dimensions;
									array[num4] = vector.ToString();
									array[4] = " has a bigger size than ";
									array[5] = num2.ToString();
									array[6] = " ";
									array[7] = num3.ToString();
									Debug.Log(string.Concat(array));
								}
								num2 = (int)adjustedForScaleAndOffset2Dimensions.x;
								num3 = (int)adjustedForScaleAndOffset2Dimensions.y;
							}
						}
					}
				}
				if (data._resizePowerOfTwoTextures)
				{
					if (num2 <= num * 5)
					{
						Debug.LogWarning(string.Format("Some of the textures have widths close to the size of the padding. It is not recommended to use _resizePowerOfTwoTextures with widths this small.", mb_TexSet.ToString()));
					}
					if (num3 <= num * 5)
					{
						Debug.LogWarning(string.Format("Some of the textures have heights close to the size of the padding. It is not recommended to use _resizePowerOfTwoTextures with heights this small.", mb_TexSet.ToString()));
					}
					if (MB3_TextureCombinerPipeline.IsPowerOfTwo(num2))
					{
						num2 -= num * 2;
					}
					if (MB3_TextureCombinerPipeline.IsPowerOfTwo(num3))
					{
						num3 -= num * 2;
					}
					if (num2 < 1)
					{
						num2 = 1;
					}
					if (num3 < 1)
					{
						num3 = 1;
					}
				}
				if (LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("    Ideal size is " + num2.ToString() + " " + num3.ToString());
				}
				mb_TexSet.idealWidth = num2;
				mb_TexSet.idealHeight = num3;
			}
			data._atlasPadding = num;
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Total time Step2 Calculate Ideal Sizes part1: " + stopwatch.Elapsed.ToString());
			}
			yield break;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000C2D00 File Offset: 0x000C0F00
		internal static AtlasPackingResult[] RunTexturePackerOnly(MB3_TextureCombinerPipeline.TexturePipelineData data, bool doSplitIntoMultiAtlasIfTooBig, MB_ITextureCombinerPacker texturePacker, MB2_LogLevel LOG_LEVEL)
		{
			AtlasPackingResult[] array = texturePacker.CalculateAtlasRectangles(data, doSplitIntoMultiAtlasIfTooBig, LOG_LEVEL);
			for (int i = 0; i < array.Length; i++)
			{
				List<MatsAndGOs> list = new List<MatsAndGOs>();
				array[i].data = list;
				for (int j = 0; j < array[i].srcImgIdxs.Length; j++)
				{
					MB_TexSet mb_TexSet = data.distinctMaterialTextures[array[i].srcImgIdxs[j]];
					list.Add(mb_TexSet.matsAndGOs);
				}
			}
			return array;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000C2D70 File Offset: 0x000C0F70
		internal static MB_ITextureCombinerPacker CreatePacker(bool onlyOneTextureInAtlasReuseTextures, MB2_PackingAlgorithmEnum packingAlgorithm)
		{
			if (onlyOneTextureInAtlasReuseTextures)
			{
				return new MB3_TextureCombinerPackerOneTextureInAtlas();
			}
			if (packingAlgorithm == MB2_PackingAlgorithmEnum.UnitysPackTextures)
			{
				return new MB3_TextureCombinerPackerUnity();
			}
			if (packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal)
			{
				if (MB3_TextureCombinerPipeline.USE_EXPERIMENTAL_HOIZONTALVERTICAL)
				{
					return new MB3_TextureCombinerPackerMeshBakerHorizontalVertical(MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection.horizontal);
				}
				return new MB3_TextureCombinerPackerMeshBaker();
			}
			else if (packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical)
			{
				if (MB3_TextureCombinerPipeline.USE_EXPERIMENTAL_HOIZONTALVERTICAL)
				{
					return new MB3_TextureCombinerPackerMeshBakerHorizontalVertical(MB3_TextureCombinerPackerMeshBakerHorizontalVertical.AtlasDirection.vertical);
				}
				return new MB3_TextureCombinerPackerMeshBaker();
			}
			else
			{
				if (packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker)
				{
					return new MB3_TextureCombinerPackerMeshBaker();
				}
				return new MB3_TextureCombinerPackerMeshBakerFast();
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000C2DD0 File Offset: 0x000C0FD0
		internal static IEnumerator __Step3_BuildAndSaveAtlasesAndStoreResults(MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, ProgressUpdateDelegate progressInfo, MB3_TextureCombinerPipeline.TexturePipelineData data, MB3_TextureCombiner combiner, MB_ITextureCombinerPacker packer, AtlasPackingResult atlasPackingResult, MB2_EditorMethodsInterface textureEditorMethods, MB_AtlasesAndRects resultAtlasesAndRects, StringBuilder report, MB2_LogLevel LOG_LEVEL)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			GC.Collect();
			Texture2D[] atlases = new Texture2D[data.numAtlases];
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("time Step 3 Create And Save Atlases part 1 " + sw.Elapsed.ToString());
			}
			yield return packer.CreateAtlases(progressInfo, data, combiner, atlasPackingResult, atlases, textureEditorMethods, LOG_LEVEL);
			float num = (float)sw.ElapsedMilliseconds;
			data.nonTexturePropertyBlender.AdjustNonTextureProperties(data.resultMaterial, data.texPropertyNames, data.distinctMaterialTextures, textureEditorMethods);
			if (data.distinctMaterialTextures.Count > 0)
			{
				data.distinctMaterialTextures[0].AdjustResultMaterialNonTextureProperties(data.resultMaterial, data.texPropertyNames);
			}
			if (progressInfo != null)
			{
				progressInfo("Building Report", 0.7f);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("---- Atlases ------");
			for (int i = 0; i < data.numAtlases; i++)
			{
				if (atlases[i] != null)
				{
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"Created Atlas For: ",
						data.texPropertyNames[i].name,
						" h=",
						atlases[i].height.ToString(),
						" w=",
						atlases[i].width.ToString()
					}));
				}
				else if (!MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(i, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
				{
					stringBuilder.AppendLine("Did not create atlas for " + data.texPropertyNames[i].name + " because all source textures were null.");
				}
			}
			report.Append(stringBuilder.ToString());
			List<MB_MaterialAndUVRect> list = new List<MB_MaterialAndUVRect>();
			for (int j = 0; j < data.distinctMaterialTextures.Count; j++)
			{
				MB_TexSet mb_TexSet = data.distinctMaterialTextures[j];
				List<MatAndTransformToMerged> mats = mb_TexSet.matsAndGOs.mats;
				Rect samplingEncapsulatingRect;
				Rect srcUVsamplingRect;
				mb_TexSet.GetRectsForTextureBakeResults(out samplingEncapsulatingRect, out srcUVsamplingRect);
				for (int k = 0; k < mats.Count; k++)
				{
					Rect materialTilingRectForTextureBakerResults = mb_TexSet.GetMaterialTilingRectForTextureBakerResults(k);
					MB_MaterialAndUVRect item = new MB_MaterialAndUVRect(mats[k].mat, atlasPackingResult.rects[j], mb_TexSet.allTexturesUseSameMatTiling, materialTilingRectForTextureBakerResults, samplingEncapsulatingRect, srcUVsamplingRect, mb_TexSet.tilingTreatment, mats[k].objName);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			resultAtlasesAndRects.atlases = atlases;
			resultAtlasesAndRects.texPropertyNames = ShaderTextureProperty.GetNames(data.texPropertyNames);
			resultAtlasesAndRects.mat2rect_map = list;
			if (progressInfo != null)
			{
				progressInfo("Restoring Texture Formats & Read Flags", 0.8f);
			}
			combiner._destroyAllTemporaryTextures();
			if (textureEditorMethods != null)
			{
				textureEditorMethods.RestoreReadFlagsAndFormats(progressInfo);
			}
			if (report != null && LOG_LEVEL >= MB2_LogLevel.info)
			{
				Debug.Log(report.ToString());
			}
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Time Step 3 Create And Save Atlases part 3 " + ((float)sw.ElapsedMilliseconds - num).ToString("f5"));
			}
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Total time Step 3 Create And Save Atlases " + sw.Elapsed.ToString());
			}
			yield break;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000C2E28 File Offset: 0x000C1028
		internal static StringBuilder GenerateReport(MB3_TextureCombinerPipeline.TexturePipelineData data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (data.numAtlases > 0)
			{
				stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Report");
				for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
				{
					MB_TexSet mb_TexSet = data.distinctMaterialTextures[i];
					stringBuilder.AppendLine("----------");
					stringBuilder.Append(string.Concat(new string[]
					{
						"This set of textures will be resized to:",
						mb_TexSet.idealWidth.ToString(),
						"x",
						mb_TexSet.idealHeight.ToString(),
						"\n"
					}));
					for (int j = 0; j < mb_TexSet.ts.Length; j++)
					{
						if (!mb_TexSet.ts[j].isNull)
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"   [",
								data.texPropertyNames[j].name,
								" ",
								mb_TexSet.ts[j].GetTexName(),
								" ",
								mb_TexSet.ts[j].width.ToString(),
								"x",
								mb_TexSet.ts[j].height.ToString(),
								"]"
							}));
							if (mb_TexSet.ts[j].matTilingRect.size != Vector2.one || mb_TexSet.ts[j].matTilingRect.min != Vector2.zero)
							{
								stringBuilder.AppendFormat(" material scale {0} offset{1} ", mb_TexSet.ts[j].matTilingRect.size.ToString("G4"), mb_TexSet.ts[j].matTilingRect.min.ToString("G4"));
							}
							if (mb_TexSet.obUVscale != Vector2.one || mb_TexSet.obUVoffset != Vector2.zero)
							{
								stringBuilder.AppendFormat(" obUV scale {0} offset{1} ", mb_TexSet.obUVscale.ToString("G4"), mb_TexSet.obUVoffset.ToString("G4"));
							}
							stringBuilder.AppendLine("");
						}
						else
						{
							stringBuilder.Append("   [" + data.texPropertyNames[j].name + " null ");
							if (!MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(j, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
							{
								stringBuilder.Append("no atlas will be created all textures null]\n");
							}
							else
							{
								stringBuilder.AppendFormat("a 16x16 texture will be created]\n", Array.Empty<object>());
							}
						}
					}
					stringBuilder.AppendLine("");
					stringBuilder.Append("Materials using:");
					for (int k = 0; k < mb_TexSet.matsAndGOs.mats.Count; k++)
					{
						stringBuilder.Append(mb_TexSet.matsAndGOs.mats[k].mat.name + ", ");
					}
					stringBuilder.AppendLine("");
				}
			}
			return stringBuilder;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000C315C File Offset: 0x000C135C
		internal static MB2_TexturePacker CreateTexturePacker(MB2_PackingAlgorithmEnum _packingAlgorithm)
		{
			if (_packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker)
			{
				return new MB2_TexturePackerRegular();
			}
			if (_packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Fast)
			{
				return new MB2_TexturePackerRegular();
			}
			if (_packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal)
			{
				return new MB2_TexturePackerHorizontalVert
				{
					packingOrientation = MB2_TexturePackerHorizontalVert.TexturePackingOrientation.horizontal
				};
			}
			if (_packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical)
			{
				return new MB2_TexturePackerHorizontalVert
				{
					packingOrientation = MB2_TexturePackerHorizontalVert.TexturePackingOrientation.vertical
				};
			}
			Debug.LogError("packing algorithm must be one of the MeshBaker options to create a Texture Packer");
			return null;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000C31AC File Offset: 0x000C13AC
		internal static Vector2 GetAdjustedForScaleAndOffset2Dimensions(MeshBakerMaterialTexture source, Vector2 obUVoffset, Vector2 obUVscale, MB3_TextureCombinerPipeline.TexturePipelineData data, MB2_LogLevel LOG_LEVEL)
		{
			if (source.matTilingRect.x == 0.0 && source.matTilingRect.y == 0.0 && source.matTilingRect.width == 1.0 && source.matTilingRect.height == 1.0)
			{
				if (!data._fixOutOfBoundsUVs)
				{
					return new Vector2((float)source.width, (float)source.height);
				}
				if (obUVoffset.x == 0f && obUVoffset.y == 0f && obUVscale.x == 1f && obUVscale.y == 1f)
				{
					return new Vector2((float)source.width, (float)source.height);
				}
			}
			if (LOG_LEVEL >= MB2_LogLevel.debug)
			{
				string[] array = new string[6];
				array[0] = "GetAdjustedForScaleAndOffset2Dimensions: ";
				array[1] = source.GetTexName();
				array[2] = " ";
				int num = 3;
				Vector2 vector = obUVoffset;
				array[num] = vector.ToString();
				array[4] = " ";
				int num2 = 5;
				vector = obUVscale;
				array[num2] = vector.ToString();
				Debug.Log(string.Concat(array));
			}
			Rect rect = source.GetEncapsulatingSamplingRect().GetRect();
			float num3 = rect.width * (float)source.width;
			float num4 = rect.height * (float)source.height;
			if (num3 > (float)data._maxTilingBakeSize)
			{
				num3 = (float)data._maxTilingBakeSize;
			}
			if (num4 > (float)data._maxTilingBakeSize)
			{
				num4 = (float)data._maxTilingBakeSize;
			}
			if (num3 < 1f)
			{
				num3 = 1f;
			}
			if (num4 < 1f)
			{
				num4 = 1f;
			}
			return new Vector2(num3, num4);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000C334C File Offset: 0x000C154C
		internal static Color32 ConvertNormalFormatFromUnity_ToStandard(Color32 c)
		{
			Vector3 zero = Vector3.zero;
			zero.x = (float)c.a * 2f - 1f;
			zero.y = (float)c.g * 2f - 1f;
			zero.z = Mathf.Sqrt(1f - zero.x * zero.x - zero.y * zero.y);
			return new Color32
			{
				a = 1,
				r = (byte)((zero.x + 1f) * 0.5f),
				g = (byte)((zero.y + 1f) * 0.5f),
				b = (byte)((zero.z + 1f) * 0.5f)
			};
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000C3420 File Offset: 0x000C1620
		internal static float GetSubmeshArea(Mesh m, int submeshIdx)
		{
			if (submeshIdx >= m.subMeshCount || submeshIdx < 0)
			{
				return 0f;
			}
			Vector3[] vertices = m.vertices;
			int[] indices = m.GetIndices(submeshIdx);
			float num = 0f;
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3 b = vertices[indices[i]];
				Vector3 a = vertices[indices[i + 1]];
				Vector3 a2 = vertices[indices[i + 2]];
				num += Vector3.Cross(a - b, a2 - b).magnitude / 2f;
			}
			return num;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00016163 File Offset: 0x00014363
		internal static bool IsPowerOfTwo(int x)
		{
			return (x & x - 1) == 0;
		}

		// Token: 0x04001E2D RID: 7725
		public static bool USE_EXPERIMENTAL_HOIZONTALVERTICAL = true;

		// Token: 0x04001E2E RID: 7726
		public static ShaderTextureProperty[] shaderTexPropertyNames = new ShaderTextureProperty[]
		{
			new ShaderTextureProperty("_MainTex", false),
			new ShaderTextureProperty("_BumpMap", true),
			new ShaderTextureProperty("_Normal", true),
			new ShaderTextureProperty("_BumpSpecMap", false),
			new ShaderTextureProperty("_DecalTex", false),
			new ShaderTextureProperty("_Detail", false),
			new ShaderTextureProperty("_GlossMap", false),
			new ShaderTextureProperty("_Illum", false),
			new ShaderTextureProperty("_LightTextureB0", false),
			new ShaderTextureProperty("_ParallaxMap", false),
			new ShaderTextureProperty("_ShadowOffset", false),
			new ShaderTextureProperty("_TranslucencyMap", false),
			new ShaderTextureProperty("_SpecMap", false),
			new ShaderTextureProperty("_SpecGlossMap", false),
			new ShaderTextureProperty("_TranspMap", false),
			new ShaderTextureProperty("_MetallicGlossMap", false),
			new ShaderTextureProperty("_OcclusionMap", false),
			new ShaderTextureProperty("_EmissionMap", false),
			new ShaderTextureProperty("_DetailMask", false)
		};

		// Token: 0x020004A0 RID: 1184
		public struct CreateAtlasForProperty
		{
			// Token: 0x06001DD6 RID: 7638 RVA: 0x0001616D File Offset: 0x0001436D
			public override string ToString()
			{
				return string.Format("AllTexturesNull={0} areSame={1} nonTexPropsAreSame={2}", this.allTexturesAreNull, this.allTexturesAreSame, this.allNonTexturePropsAreSame);
			}

			// Token: 0x04001E2F RID: 7727
			public bool allTexturesAreNull;

			// Token: 0x04001E30 RID: 7728
			public bool allTexturesAreSame;

			// Token: 0x04001E31 RID: 7729
			public bool allNonTexturePropsAreSame;
		}

		// Token: 0x020004A1 RID: 1185
		internal class TexturePipelineData
		{
			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0001619A File Offset: 0x0001439A
			internal int numAtlases
			{
				get
				{
					if (this.texPropertyNames != null)
					{
						return this.texPropertyNames.Count;
					}
					return 0;
				}
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x000161B1 File Offset: 0x000143B1
			internal bool OnlyOneTextureInAtlasReuseTextures()
			{
				return this.distinctMaterialTextures != null && this.distinctMaterialTextures.Count == 1 && this.distinctMaterialTextures[0].thisIsOnlyTexSetInAtlas && !this._fixOutOfBoundsUVs && !this._considerNonTextureProperties;
			}

			// Token: 0x04001E32 RID: 7730
			internal MB2_TextureBakeResults _textureBakeResults;

			// Token: 0x04001E33 RID: 7731
			internal int _atlasPadding = 1;

			// Token: 0x04001E34 RID: 7732
			internal int _maxAtlasWidth = 1;

			// Token: 0x04001E35 RID: 7733
			internal int _maxAtlasHeight = 1;

			// Token: 0x04001E36 RID: 7734
			internal bool _useMaxAtlasHeightOverride;

			// Token: 0x04001E37 RID: 7735
			internal bool _useMaxAtlasWidthOverride;

			// Token: 0x04001E38 RID: 7736
			internal bool _resizePowerOfTwoTextures;

			// Token: 0x04001E39 RID: 7737
			internal bool _fixOutOfBoundsUVs;

			// Token: 0x04001E3A RID: 7738
			internal int _maxTilingBakeSize = 1024;

			// Token: 0x04001E3B RID: 7739
			internal bool _saveAtlasesAsAssets;

			// Token: 0x04001E3C RID: 7740
			internal MB2_PackingAlgorithmEnum _packingAlgorithm;

			// Token: 0x04001E3D RID: 7741
			internal bool _meshBakerTexturePackerForcePowerOfTwo = true;

			// Token: 0x04001E3E RID: 7742
			internal List<ShaderTextureProperty> _customShaderPropNames = new List<ShaderTextureProperty>();

			// Token: 0x04001E3F RID: 7743
			internal bool _normalizeTexelDensity;

			// Token: 0x04001E40 RID: 7744
			internal bool _considerNonTextureProperties;

			// Token: 0x04001E41 RID: 7745
			internal MB3_TextureCombinerNonTextureProperties nonTexturePropertyBlender;

			// Token: 0x04001E42 RID: 7746
			internal List<MB_TexSet> distinctMaterialTextures;

			// Token: 0x04001E43 RID: 7747
			internal List<GameObject> allObjsToMesh;

			// Token: 0x04001E44 RID: 7748
			internal List<Material> allowedMaterialsFilter;

			// Token: 0x04001E45 RID: 7749
			internal List<ShaderTextureProperty> texPropertyNames;

			// Token: 0x04001E46 RID: 7750
			internal MB3_TextureCombinerPipeline.CreateAtlasForProperty[] allTexturesAreNullAndSameColor;

			// Token: 0x04001E47 RID: 7751
			internal Material resultMaterial;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	public class MB3_TextureCombiner
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x000157BC File Offset: 0x000139BC
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x000157C4 File Offset: 0x000139C4
		public MB2_TextureBakeResults textureBakeResults
		{
			get
			{
				return this._textureBakeResults;
			}
			set
			{
				this._textureBakeResults = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x000157CD File Offset: 0x000139CD
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x000157D5 File Offset: 0x000139D5
		public int atlasPadding
		{
			get
			{
				return this._atlasPadding;
			}
			set
			{
				this._atlasPadding = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x000157DE File Offset: 0x000139DE
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x000157E6 File Offset: 0x000139E6
		public int maxAtlasSize
		{
			get
			{
				return this._maxAtlasSize;
			}
			set
			{
				this._maxAtlasSize = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x000157EF File Offset: 0x000139EF
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x000157F7 File Offset: 0x000139F7
		public virtual int maxAtlasWidthOverride
		{
			get
			{
				return this._maxAtlasWidthOverride;
			}
			set
			{
				this._maxAtlasWidthOverride = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00015800 File Offset: 0x00013A00
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x00015808 File Offset: 0x00013A08
		public virtual int maxAtlasHeightOverride
		{
			get
			{
				return this._maxAtlasHeightOverride;
			}
			set
			{
				this._maxAtlasHeightOverride = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00015811 File Offset: 0x00013A11
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x00015819 File Offset: 0x00013A19
		public virtual bool useMaxAtlasWidthOverride
		{
			get
			{
				return this._useMaxAtlasWidthOverride;
			}
			set
			{
				this._useMaxAtlasWidthOverride = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00015822 File Offset: 0x00013A22
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0001582A File Offset: 0x00013A2A
		public virtual bool useMaxAtlasHeightOverride
		{
			get
			{
				return this._useMaxAtlasHeightOverride;
			}
			set
			{
				this._useMaxAtlasHeightOverride = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00015833 File Offset: 0x00013A33
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0001583B File Offset: 0x00013A3B
		public bool resizePowerOfTwoTextures
		{
			get
			{
				return this._resizePowerOfTwoTextures;
			}
			set
			{
				this._resizePowerOfTwoTextures = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00015844 File Offset: 0x00013A44
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0001584C File Offset: 0x00013A4C
		public bool fixOutOfBoundsUVs
		{
			get
			{
				return this._fixOutOfBoundsUVs;
			}
			set
			{
				this._fixOutOfBoundsUVs = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00015855 File Offset: 0x00013A55
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x0001585D File Offset: 0x00013A5D
		public int maxTilingBakeSize
		{
			get
			{
				return this._maxTilingBakeSize;
			}
			set
			{
				this._maxTilingBakeSize = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00015866 File Offset: 0x00013A66
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0001586E File Offset: 0x00013A6E
		public bool saveAtlasesAsAssets
		{
			get
			{
				return this._saveAtlasesAsAssets;
			}
			set
			{
				this._saveAtlasesAsAssets = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00015877 File Offset: 0x00013A77
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0001587F File Offset: 0x00013A7F
		public MB2_PackingAlgorithmEnum packingAlgorithm
		{
			get
			{
				return this._packingAlgorithm;
			}
			set
			{
				this._packingAlgorithm = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x00015888 File Offset: 0x00013A88
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x00015890 File Offset: 0x00013A90
		public bool meshBakerTexturePackerForcePowerOfTwo
		{
			get
			{
				return this._meshBakerTexturePackerForcePowerOfTwo;
			}
			set
			{
				this._meshBakerTexturePackerForcePowerOfTwo = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00015899 File Offset: 0x00013A99
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x000158A1 File Offset: 0x00013AA1
		public List<ShaderTextureProperty> customShaderPropNames
		{
			get
			{
				return this._customShaderPropNames;
			}
			set
			{
				this._customShaderPropNames = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000158AA File Offset: 0x00013AAA
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x000158B2 File Offset: 0x00013AB2
		public bool considerNonTextureProperties
		{
			get
			{
				return this._considerNonTextureProperties;
			}
			set
			{
				this._considerNonTextureProperties = value;
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000BCB28 File Offset: 0x000BAD28
		public static void RunCorutineWithoutPause(IEnumerator cor, int recursionDepth)
		{
			if (recursionDepth == 0)
			{
				MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning = true;
			}
			if (recursionDepth > 20)
			{
				Debug.LogError("Recursion Depth Exceeded.");
				return;
			}
			while (cor.MoveNext())
			{
				object obj = cor.Current;
				if (!(obj is YieldInstruction) && obj != null && obj is IEnumerator)
				{
					MB3_TextureCombiner.RunCorutineWithoutPause((IEnumerator)cor.Current, recursionDepth + 1);
				}
			}
			if (recursionDepth == 0)
			{
				MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning = false;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000BCB8C File Offset: 0x000BAD8C
		public bool CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods = null, List<AtlasPackingResult> packingResults = null, bool onlyPackRects = false, bool splitAtlasWhenPackingIfTooBig = false)
		{
			MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult combineTexturesIntoAtlasesCoroutineResult = new MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult();
			MB3_TextureCombiner.RunCorutineWithoutPause(this._CombineTexturesIntoAtlases(progressInfo, combineTexturesIntoAtlasesCoroutineResult, resultAtlasesAndRects, resultMaterial, objsToMesh, allowedMaterialsFilter, textureEditorMethods, packingResults, onlyPackRects, splitAtlasWhenPackingIfTooBig), 0);
			return combineTexturesIntoAtlasesCoroutineResult.success;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000BCBC4 File Offset: 0x000BADC4
		public IEnumerator CombineTexturesIntoAtlasesCoroutine(ProgressUpdateDelegate progressInfo, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods = null, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult coroutineResult = null, float maxTimePerFrame = 0.01f, List<AtlasPackingResult> packingResults = null, bool onlyPackRects = false, bool splitAtlasWhenPackingIfTooBig = false)
		{
			if (!MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning && (MBVersion.GetMajorVersion() < 5 || (MBVersion.GetMajorVersion() == 5 && MBVersion.GetMinorVersion() < 3)))
			{
				Debug.LogError("Running the texture combiner as a coroutine only works in Unity 5.3 and higher");
				yield return null;
			}
			coroutineResult.success = true;
			coroutineResult.isFinished = false;
			if (maxTimePerFrame <= 0f)
			{
				Debug.LogError("maxTimePerFrame must be a value greater than zero");
				coroutineResult.isFinished = true;
				yield break;
			}
			yield return this._CombineTexturesIntoAtlases(progressInfo, coroutineResult, resultAtlasesAndRects, resultMaterial, objsToMesh, allowedMaterialsFilter, textureEditorMethods, packingResults, onlyPackRects, splitAtlasWhenPackingIfTooBig);
			coroutineResult.isFinished = true;
			yield break;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000BCC34 File Offset: 0x000BAE34
		private IEnumerator _CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods, List<AtlasPackingResult> atlasPackingResult, bool onlyPackRects, bool splitAtlasWhenPackingIfTooBig)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			try
			{
				this._temporaryTextures.Clear();
				MeshBakerMaterialTexture.readyToBuildAtlases = false;
				if (textureEditorMethods != null)
				{
					textureEditorMethods.Clear();
					textureEditorMethods.OnPreTextureBake();
				}
				if (objsToMesh == null || objsToMesh.Count == 0)
				{
					Debug.LogError("No meshes to combine. Please assign some meshes to combine.");
					result.success = false;
					yield break;
				}
				if (this._atlasPadding < 0)
				{
					Debug.LogError("Atlas padding must be zero or greater.");
					result.success = false;
					yield break;
				}
				if (this._maxTilingBakeSize < 2 || this._maxTilingBakeSize > 4096)
				{
					Debug.LogError("Invalid value for max tiling bake size.");
					result.success = false;
					yield break;
				}
				for (int i = 0; i < objsToMesh.Count; i++)
				{
					Material[] gomaterials = MB_Utility.GetGOMaterials(objsToMesh[i]);
					for (int j = 0; j < gomaterials.Length; j++)
					{
						if (gomaterials[j] == null)
						{
							string str = "Game object ";
							GameObject gameObject = objsToMesh[i];
							Debug.LogError(str + ((gameObject != null) ? gameObject.ToString() : null) + " has a null material");
							result.success = false;
							yield break;
						}
					}
				}
				if (progressInfo != null)
				{
					progressInfo("Collecting textures for " + objsToMesh.Count.ToString() + " meshes.", 0.01f);
				}
				MB3_TextureCombinerPipeline.TexturePipelineData texturePipelineData = this.LoadPipelineData(resultMaterial, new List<ShaderTextureProperty>(), objsToMesh, allowedMaterialsFilter, new List<MB_TexSet>());
				if (!MB3_TextureCombinerPipeline._CollectPropertyNames(texturePipelineData, this.LOG_LEVEL))
				{
					result.success = false;
					yield break;
				}
				if (this._fixOutOfBoundsUVs && (this._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal || this._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical) && this.LOG_LEVEL >= MB2_LogLevel.info)
				{
					Debug.LogWarning("'Consider Mesh UVs' is enabled but packing algorithm is MeshBakerTexturePacker_Horizontal or MeshBakerTexturePacker_Vertical. It is recommended to use these packers without using 'Consider Mesh UVs'");
				}
				texturePipelineData.nonTexturePropertyBlender.LoadTextureBlendersIfNeeded(texturePipelineData.resultMaterial);
				if (onlyPackRects)
				{
					yield return this.__RunTexturePackerOnly(result, texturePipelineData, splitAtlasWhenPackingIfTooBig, textureEditorMethods, atlasPackingResult);
				}
				else
				{
					yield return this.__CombineTexturesIntoAtlases(progressInfo, result, resultAtlasesAndRects, texturePipelineData, splitAtlasWhenPackingIfTooBig, textureEditorMethods);
				}
			}
			finally
			{
				this._destroyAllTemporaryTextures();
				this._restoreProceduralMaterials();
				if (textureEditorMethods != null)
				{
					textureEditorMethods.RestoreReadFlagsAndFormats(progressInfo);
					textureEditorMethods.OnPostTextureBake();
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("===== Done creating atlases for " + ((resultMaterial != null) ? resultMaterial.ToString() : null) + " Total time to create atlases " + sw.Elapsed.ToString());
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000BCC9C File Offset: 0x000BAE9C
		private MB3_TextureCombinerPipeline.TexturePipelineData LoadPipelineData(Material resultMaterial, List<ShaderTextureProperty> texPropertyNames, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, List<MB_TexSet> distinctMaterialTextures)
		{
			MB3_TextureCombinerPipeline.TexturePipelineData texturePipelineData = new MB3_TextureCombinerPipeline.TexturePipelineData();
			texturePipelineData._textureBakeResults = this._textureBakeResults;
			texturePipelineData._atlasPadding = this._atlasPadding;
			if (this._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Vertical && this._useMaxAtlasHeightOverride)
			{
				texturePipelineData._maxAtlasHeight = this._maxAtlasHeightOverride;
				texturePipelineData._useMaxAtlasHeightOverride = true;
			}
			else
			{
				texturePipelineData._maxAtlasHeight = this._maxAtlasSize;
			}
			if (this._packingAlgorithm == MB2_PackingAlgorithmEnum.MeshBakerTexturePacker_Horizontal && this._useMaxAtlasWidthOverride)
			{
				texturePipelineData._maxAtlasWidth = this._maxAtlasWidthOverride;
				texturePipelineData._useMaxAtlasWidthOverride = true;
			}
			else
			{
				texturePipelineData._maxAtlasWidth = this._maxAtlasSize;
			}
			texturePipelineData._resizePowerOfTwoTextures = this._resizePowerOfTwoTextures;
			texturePipelineData._fixOutOfBoundsUVs = this._fixOutOfBoundsUVs;
			texturePipelineData._maxTilingBakeSize = this._maxTilingBakeSize;
			texturePipelineData._saveAtlasesAsAssets = this._saveAtlasesAsAssets;
			texturePipelineData._packingAlgorithm = this._packingAlgorithm;
			texturePipelineData._meshBakerTexturePackerForcePowerOfTwo = this._meshBakerTexturePackerForcePowerOfTwo;
			texturePipelineData._customShaderPropNames = this._customShaderPropNames;
			texturePipelineData._normalizeTexelDensity = this._normalizeTexelDensity;
			texturePipelineData._considerNonTextureProperties = this._considerNonTextureProperties;
			texturePipelineData.nonTexturePropertyBlender = new MB3_TextureCombinerNonTextureProperties(this.LOG_LEVEL, this._considerNonTextureProperties);
			texturePipelineData.resultMaterial = resultMaterial;
			texturePipelineData.distinctMaterialTextures = distinctMaterialTextures;
			texturePipelineData.allObjsToMesh = objsToMesh;
			texturePipelineData.allowedMaterialsFilter = allowedMaterialsFilter;
			texturePipelineData.texPropertyNames = texPropertyNames;
			return texturePipelineData;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000158BB File Offset: 0x00013ABB
		private IEnumerator __CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB_AtlasesAndRects resultAtlasesAndRects, MB3_TextureCombinerPipeline.TexturePipelineData data, bool splitAtlasWhenPackingIfTooBig, MB2_EditorMethodsInterface textureEditorMethods)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"__CombineTexturesIntoAtlases texture properties in shader:",
					data.texPropertyNames.Count.ToString(),
					" objsToMesh:",
					data.allObjsToMesh.Count.ToString(),
					" _fixOutOfBoundsUVs:",
					data._fixOutOfBoundsUVs.ToString()
				}));
			}
			if (progressInfo != null)
			{
				progressInfo("Collecting textures ", 0.01f);
			}
			List<GameObject> usedObjsToMesh = new List<GameObject>();
			yield return MB3_TextureCombinerPipeline.__Step1_CollectDistinctMatTexturesAndUsedObjects(progressInfo, result, data, this, textureEditorMethods, usedObjsToMesh, this.LOG_LEVEL);
			if (!result.success)
			{
				yield break;
			}
			if (MB3_MeshCombiner.EVAL_VERSION)
			{
				bool flag = true;
				for (int i = 0; i < data.distinctMaterialTextures.Count; i++)
				{
					for (int j = 0; j < data.distinctMaterialTextures[i].matsAndGOs.mats.Count; j++)
					{
						if (!data.distinctMaterialTextures[i].matsAndGOs.mats[j].mat.shader.name.EndsWith("Diffuse") && !data.distinctMaterialTextures[i].matsAndGOs.mats[j].mat.shader.name.EndsWith("Bumped Diffuse"))
						{
							Debug.LogError("The free version of Mesh Baker only works with Diffuse and Bumped Diffuse Shaders. The full version can be used with any shader. Material " + data.distinctMaterialTextures[i].matsAndGOs.mats[j].mat.name + " uses shader " + data.distinctMaterialTextures[i].matsAndGOs.mats[j].mat.shader.name);
							flag = false;
						}
					}
				}
				if (!flag)
				{
					result.success = false;
					yield break;
				}
			}
			yield return MB3_TextureCombinerPipeline.CalculateIdealSizesForTexturesInAtlasAndPadding(progressInfo, result, data, this, textureEditorMethods, this.LOG_LEVEL);
			if (!result.success)
			{
				yield break;
			}
			StringBuilder report = MB3_TextureCombinerPipeline.GenerateReport(data);
			MB_ITextureCombinerPacker texturePaker = MB3_TextureCombinerPipeline.CreatePacker(data.OnlyOneTextureInAtlasReuseTextures(), data._packingAlgorithm);
			yield return texturePaker.ConvertTexturesToReadableFormats(progressInfo, result, data, this, textureEditorMethods, this.LOG_LEVEL);
			if (!result.success)
			{
				yield break;
			}
			AtlasPackingResult[] array = texturePaker.CalculateAtlasRectangles(data, splitAtlasWhenPackingIfTooBig, this.LOG_LEVEL);
			yield return MB3_TextureCombinerPipeline.__Step3_BuildAndSaveAtlasesAndStoreResults(result, progressInfo, data, this, texturePaker, array[0], textureEditorMethods, resultAtlasesAndRects, report, this.LOG_LEVEL);
			yield break;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000158F7 File Offset: 0x00013AF7
		private IEnumerator __RunTexturePackerOnly(MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult result, MB3_TextureCombinerPipeline.TexturePipelineData data, bool splitAtlasWhenPackingIfTooBig, MB2_EditorMethodsInterface textureEditorMethods, List<AtlasPackingResult> packingResult)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"__RunTexturePacker texture properties in shader:",
					data.texPropertyNames.Count.ToString(),
					" objsToMesh:",
					data.allObjsToMesh.Count.ToString(),
					" _fixOutOfBoundsUVs:",
					data._fixOutOfBoundsUVs.ToString()
				}));
			}
			List<GameObject> usedObjsToMesh = new List<GameObject>();
			yield return MB3_TextureCombinerPipeline.__Step1_CollectDistinctMatTexturesAndUsedObjects(null, result, data, this, textureEditorMethods, usedObjsToMesh, this.LOG_LEVEL);
			if (!result.success)
			{
				yield break;
			}
			data.allTexturesAreNullAndSameColor = new MB3_TextureCombinerPipeline.CreateAtlasForProperty[data.texPropertyNames.Count];
			yield return MB3_TextureCombinerPipeline.CalculateIdealSizesForTexturesInAtlasAndPadding(null, result, data, this, textureEditorMethods, this.LOG_LEVEL);
			if (!result.success)
			{
				yield break;
			}
			MB_ITextureCombinerPacker texturePacker = MB3_TextureCombinerPipeline.CreatePacker(data.OnlyOneTextureInAtlasReuseTextures(), data._packingAlgorithm);
			AtlasPackingResult[] array = MB3_TextureCombinerPipeline.RunTexturePackerOnly(data, splitAtlasWhenPackingIfTooBig, texturePacker, this.LOG_LEVEL);
			for (int i = 0; i < array.Length; i++)
			{
				packingResult.Add(array[i]);
			}
			yield break;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0001592B File Offset: 0x00013B2B
		internal int _getNumTemporaryTextures()
		{
			return this._temporaryTextures.Count;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000BCDD4 File Offset: 0x000BAFD4
		public Texture2D _createTemporaryTexture(string propertyName, int w, int h, TextureFormat texFormat, bool mipMaps)
		{
			Texture2D texture2D = new Texture2D(w, h, texFormat, mipMaps);
			texture2D.name = string.Format("tmp{0}_{1}x{2}", this._temporaryTextures.Count, w, h);
			MB_Utility.setSolidColor(texture2D, Color.clear);
			MB3_TextureCombiner.TemporaryTexture item = new MB3_TextureCombiner.TemporaryTexture(propertyName, texture2D);
			this._temporaryTextures.Add(item);
			return texture2D;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000BCE3C File Offset: 0x000BB03C
		internal Texture2D _createTextureCopy(string propertyName, Texture2D t)
		{
			Texture2D texture2D = MB_Utility.createTextureCopy(t);
			texture2D.name = string.Format("tmpCopy{0}_{1}x{2}", this._temporaryTextures.Count, texture2D.width, texture2D.height);
			MB3_TextureCombiner.TemporaryTexture item = new MB3_TextureCombiner.TemporaryTexture(propertyName, texture2D);
			this._temporaryTextures.Add(item);
			return texture2D;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000BCE9C File Offset: 0x000BB09C
		internal Texture2D _resizeTexture(string propertyName, Texture2D t, int w, int h)
		{
			Texture2D texture2D = MB_Utility.resampleTexture(t, w, h);
			texture2D.name = string.Format("tmpResampled{0}_{1}x{2}", this._temporaryTextures.Count, w, h);
			MB3_TextureCombiner.TemporaryTexture item = new MB3_TextureCombiner.TemporaryTexture(propertyName, texture2D);
			this._temporaryTextures.Add(item);
			return texture2D;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000BCEF8 File Offset: 0x000BB0F8
		internal void _destroyAllTemporaryTextures()
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Destroying " + this._temporaryTextures.Count.ToString() + " temporary textures");
			}
			for (int i = 0; i < this._temporaryTextures.Count; i++)
			{
				MB_Utility.Destroy(this._temporaryTextures[i].texture);
			}
			this._temporaryTextures.Clear();
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000BCF6C File Offset: 0x000BB16C
		internal void _destroyTemporaryTextures(string propertyName)
		{
			int num = 0;
			for (int i = this._temporaryTextures.Count - 1; i >= 0; i--)
			{
				if (this._temporaryTextures[i].property.Equals(propertyName))
				{
					num++;
					MB_Utility.Destroy(this._temporaryTextures[i].texture);
					this._temporaryTextures.RemoveAt(i);
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Destroying ",
					num.ToString(),
					" temporary textures ",
					propertyName,
					" num remaining ",
					this._temporaryTextures.Count.ToString()
				}));
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0000296E File Offset: 0x00000B6E
		public void _restoreProceduralMaterials()
		{
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000BD028 File Offset: 0x000BB228
		public void SuggestTreatment(List<GameObject> objsToMesh, Material[] resultMaterials, List<ShaderTextureProperty> _customShaderPropNames)
		{
			this._customShaderPropNames = _customShaderPropNames;
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
			for (int i = 0; i < objsToMesh.Count; i++)
			{
				GameObject gameObject = objsToMesh[i];
				if (!(gameObject == null))
				{
					Material[] gomaterials = MB_Utility.GetGOMaterials(objsToMesh[i]);
					if (gomaterials.Length > 1)
					{
						stringBuilder.AppendFormat("\nObject {0} uses {1} materials. Possible treatments:\n", objsToMesh[i].name, gomaterials.Length);
						stringBuilder.AppendFormat("  1) Collapse the submeshes together into one submesh in the combined mesh. Each of the original submesh materials will map to a different UV rectangle in the atlas(es) used by the combined material.\n", Array.Empty<object>());
						stringBuilder.AppendFormat("  2) Use the multiple materials feature to map submeshes in the source mesh to submeshes in the combined mesh.\n", Array.Empty<object>());
					}
					Mesh mesh = MB_Utility.GetMesh(gameObject);
					MB_Utility.MeshAnalysisResult[] array;
					if (!dictionary.TryGetValue(mesh.GetInstanceID(), out array))
					{
						array = new MB_Utility.MeshAnalysisResult[mesh.subMeshCount];
						MB_Utility.doSubmeshesShareVertsOrTris(mesh, ref array[0]);
						for (int j = 0; j < mesh.subMeshCount; j++)
						{
							MB_Utility.hasOutOfBoundsUVs(mesh, ref array[j], j, 0);
							array[j].hasOverlappingSubmeshTris = array[0].hasOverlappingSubmeshTris;
							array[j].hasOverlappingSubmeshVerts = array[0].hasOverlappingSubmeshVerts;
						}
						dictionary.Add(mesh.GetInstanceID(), array);
					}
					for (int k = 0; k < gomaterials.Length; k++)
					{
						if (array[k].hasOutOfBoundsUVs)
						{
							DRect drect = new DRect(array[k].uvRect);
							stringBuilder.AppendFormat("\nObject {0} submesh={1} material={2} uses UVs outside the range 0,0 .. 1,1 to create tiling that tiles the box {3},{4} .. {5},{6}. This is a problem because the UVs outside the 0,0 .. 1,1 rectangle will pick up neighboring textures in the atlas. Possible Treatments:\n", new object[]
							{
								gameObject,
								k,
								gomaterials[k],
								drect.x.ToString("G4"),
								drect.y.ToString("G4"),
								(drect.x + drect.width).ToString("G4"),
								(drect.y + drect.height).ToString("G4")
							});
							stringBuilder.AppendFormat("    1) Ignore the problem. The tiling may not affect result significantly.\n", Array.Empty<object>());
							stringBuilder.AppendFormat("    2) Use the 'fix out of bounds UVs' feature to bake the tiling and scale the UVs to fit in the 0,0 .. 1,1 rectangle.\n", Array.Empty<object>());
							stringBuilder.AppendFormat("    3) Use the Multiple Materials feature to map the material on this submesh to its own submesh in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n", Array.Empty<object>());
							stringBuilder.AppendFormat("    4) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n", Array.Empty<object>());
						}
					}
					if (array[0].hasOverlappingSubmeshVerts)
					{
						stringBuilder.AppendFormat("\nObject {0} has submeshes that share vertices. This is a problem because each vertex can have only one UV coordinate and may be required to map to different positions in the various atlases that are generated. Possible treatments:\n", objsToMesh[i]);
						stringBuilder.AppendFormat(" 1) Ignore the problem. The vertices may not affect the result.\n", Array.Empty<object>());
						stringBuilder.AppendFormat(" 2) Use the Multiple Materials feature to map the submeshs that overlap to their own submeshs in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n", Array.Empty<object>());
						stringBuilder.AppendFormat(" 3) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n", Array.Empty<object>());
					}
				}
			}
			Dictionary<Material, List<GameObject>> dictionary2 = new Dictionary<Material, List<GameObject>>();
			for (int l = 0; l < objsToMesh.Count; l++)
			{
				if (objsToMesh[l] != null)
				{
					Material[] gomaterials2 = MB_Utility.GetGOMaterials(objsToMesh[l]);
					for (int m = 0; m < gomaterials2.Length; m++)
					{
						if (gomaterials2[m] != null)
						{
							List<GameObject> list;
							if (!dictionary2.TryGetValue(gomaterials2[m], out list))
							{
								list = new List<GameObject>();
								dictionary2.Add(gomaterials2[m], list);
							}
							if (!list.Contains(objsToMesh[l]))
							{
								list.Add(objsToMesh[l]);
							}
						}
					}
				}
			}
			for (int n = 0; n < resultMaterials.Length; n++)
			{
				string shaderName = (resultMaterials[n] != null) ? "None" : resultMaterials[n].shader.name;
				MB3_TextureCombinerPipeline.TexturePipelineData texturePipelineData = this.LoadPipelineData(resultMaterials[n], new List<ShaderTextureProperty>(), objsToMesh, new List<Material>(), new List<MB_TexSet>());
				MB3_TextureCombinerPipeline._CollectPropertyNames(texturePipelineData, this.LOG_LEVEL);
				foreach (Material material in dictionary2.Keys)
				{
					for (int num = 0; num < texturePipelineData.texPropertyNames.Count; num++)
					{
						if (material.HasProperty(texturePipelineData.texPropertyNames[num].name))
						{
							Texture textureConsideringStandardShaderKeywords = MB3_TextureCombinerPipeline.GetTextureConsideringStandardShaderKeywords(shaderName, material, texturePipelineData.texPropertyNames[num].name);
							if (textureConsideringStandardShaderKeywords != null)
							{
								Vector2 textureOffset = material.GetTextureOffset(texturePipelineData.texPropertyNames[num].name);
								Vector3 vector = material.GetTextureScale(texturePipelineData.texPropertyNames[num].name);
								if (textureOffset.x < 0f || textureOffset.x + vector.x > 1f || textureOffset.y < 0f || textureOffset.y + vector.y > 1f)
								{
									stringBuilder.AppendFormat("\nMaterial {0} used by objects {1} uses texture {2} that is tiled (scale={3} offset={4}). If there is more than one texture in the atlas  then Mesh Baker will bake the tiling into the atlas. If the baked tiling is large then quality can be lost. Possible treatments:\n", new object[]
									{
										material,
										this.PrintList(dictionary2[material]),
										textureConsideringStandardShaderKeywords,
										vector,
										textureOffset
									});
									stringBuilder.AppendFormat("  1) Use the baked tiling.\n", Array.Empty<object>());
									stringBuilder.AppendFormat("  2) Use the Multiple Materials feature to map the material on this object/submesh to its own submesh in the combined mesh. No other materials should map to this submesh. The original material can be applied to this submesh.\n", Array.Empty<object>());
									stringBuilder.AppendFormat("  3) Combine only meshes that use the same (or subset of) the set of textures on this mesh. The original material can be applied to the result.\n", Array.Empty<object>());
								}
							}
						}
					}
				}
			}
			string message;
			if (stringBuilder.Length == 0)
			{
				message = "====== No problems detected. These meshes should combine well ====\n  If there are problems with the combined meshes please report the problem to digitalOpus.ca so we can improve Mesh Baker.";
			}
			else
			{
				message = "====== There are possible problems with these meshes that may prevent them from combining well. TREATMENT SUGGESTIONS (copy and paste to text editor if too big) =====\n" + stringBuilder.ToString();
			}
			Debug.Log(message);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x000BD5D8 File Offset: 0x000BB7D8
		private string PrintList(List<GameObject> gos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < gos.Count; i++)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				GameObject gameObject = gos[i];
				stringBuilder2.Append(((gameObject != null) ? gameObject.ToString() : null) + ",");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001D6E RID: 7534
		public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x04001D6F RID: 7535
		[SerializeField]
		protected MB2_TextureBakeResults _textureBakeResults;

		// Token: 0x04001D70 RID: 7536
		[SerializeField]
		protected int _atlasPadding = 1;

		// Token: 0x04001D71 RID: 7537
		[SerializeField]
		protected int _maxAtlasSize = 1;

		// Token: 0x04001D72 RID: 7538
		[SerializeField]
		protected int _maxAtlasWidthOverride = 4096;

		// Token: 0x04001D73 RID: 7539
		[SerializeField]
		protected int _maxAtlasHeightOverride = 4096;

		// Token: 0x04001D74 RID: 7540
		[SerializeField]
		protected bool _useMaxAtlasWidthOverride;

		// Token: 0x04001D75 RID: 7541
		[SerializeField]
		protected bool _useMaxAtlasHeightOverride;

		// Token: 0x04001D76 RID: 7542
		[SerializeField]
		protected bool _resizePowerOfTwoTextures;

		// Token: 0x04001D77 RID: 7543
		[SerializeField]
		protected bool _fixOutOfBoundsUVs;

		// Token: 0x04001D78 RID: 7544
		[SerializeField]
		protected int _maxTilingBakeSize = 1024;

		// Token: 0x04001D79 RID: 7545
		[SerializeField]
		protected bool _saveAtlasesAsAssets;

		// Token: 0x04001D7A RID: 7546
		[SerializeField]
		protected MB2_PackingAlgorithmEnum _packingAlgorithm;

		// Token: 0x04001D7B RID: 7547
		[SerializeField]
		protected bool _meshBakerTexturePackerForcePowerOfTwo = true;

		// Token: 0x04001D7C RID: 7548
		[SerializeField]
		protected List<ShaderTextureProperty> _customShaderPropNames = new List<ShaderTextureProperty>();

		// Token: 0x04001D7D RID: 7549
		[SerializeField]
		protected bool _normalizeTexelDensity;

		// Token: 0x04001D7E RID: 7550
		[SerializeField]
		protected bool _considerNonTextureProperties;

		// Token: 0x04001D7F RID: 7551
		private List<MB3_TextureCombiner.TemporaryTexture> _temporaryTextures = new List<MB3_TextureCombiner.TemporaryTexture>();

		// Token: 0x04001D80 RID: 7552
		public static bool _RunCorutineWithoutPauseIsRunning;

		// Token: 0x02000476 RID: 1142
		private class TemporaryTexture
		{
			// Token: 0x06001CCC RID: 7372 RVA: 0x00015938 File Offset: 0x00013B38
			public TemporaryTexture(string prop, Texture2D tex)
			{
				this.property = prop;
				this.texture = tex;
			}

			// Token: 0x04001D81 RID: 7553
			internal string property;

			// Token: 0x04001D82 RID: 7554
			internal Texture2D texture;
		}

		// Token: 0x02000477 RID: 1143
		public class CombineTexturesIntoAtlasesCoroutineResult
		{
			// Token: 0x04001D83 RID: 7555
			public bool success = true;

			// Token: 0x04001D84 RID: 7556
			public bool isFinished;
		}
	}
}

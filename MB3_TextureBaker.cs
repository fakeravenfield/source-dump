using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class MB3_TextureBaker : MB3_MeshBakerRoot
{
	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600013F RID: 319 RVA: 0x00003022 File Offset: 0x00001222
	// (set) Token: 0x06000140 RID: 320 RVA: 0x0000302A File Offset: 0x0000122A
	public override MB2_TextureBakeResults textureBakeResults
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

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000141 RID: 321 RVA: 0x00003033 File Offset: 0x00001233
	// (set) Token: 0x06000142 RID: 322 RVA: 0x0000303B File Offset: 0x0000123B
	public virtual int atlasPadding
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

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000143 RID: 323 RVA: 0x00003044 File Offset: 0x00001244
	// (set) Token: 0x06000144 RID: 324 RVA: 0x0000304C File Offset: 0x0000124C
	public virtual int maxAtlasSize
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

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000145 RID: 325 RVA: 0x00003055 File Offset: 0x00001255
	// (set) Token: 0x06000146 RID: 326 RVA: 0x0000305D File Offset: 0x0000125D
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

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000147 RID: 327 RVA: 0x00003066 File Offset: 0x00001266
	// (set) Token: 0x06000148 RID: 328 RVA: 0x0000306E File Offset: 0x0000126E
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

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000149 RID: 329 RVA: 0x00003077 File Offset: 0x00001277
	// (set) Token: 0x0600014A RID: 330 RVA: 0x0000307F File Offset: 0x0000127F
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

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600014B RID: 331 RVA: 0x00003088 File Offset: 0x00001288
	// (set) Token: 0x0600014C RID: 332 RVA: 0x00003090 File Offset: 0x00001290
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

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600014D RID: 333 RVA: 0x00003099 File Offset: 0x00001299
	// (set) Token: 0x0600014E RID: 334 RVA: 0x000030A1 File Offset: 0x000012A1
	public virtual bool resizePowerOfTwoTextures
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

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600014F RID: 335 RVA: 0x000030AA File Offset: 0x000012AA
	// (set) Token: 0x06000150 RID: 336 RVA: 0x000030B2 File Offset: 0x000012B2
	public virtual bool fixOutOfBoundsUVs
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

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000151 RID: 337 RVA: 0x000030BB File Offset: 0x000012BB
	// (set) Token: 0x06000152 RID: 338 RVA: 0x000030C3 File Offset: 0x000012C3
	public virtual int maxTilingBakeSize
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

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000153 RID: 339 RVA: 0x000030CC File Offset: 0x000012CC
	// (set) Token: 0x06000154 RID: 340 RVA: 0x000030D4 File Offset: 0x000012D4
	public virtual MB2_PackingAlgorithmEnum packingAlgorithm
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

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000155 RID: 341 RVA: 0x000030DD File Offset: 0x000012DD
	// (set) Token: 0x06000156 RID: 342 RVA: 0x000030E5 File Offset: 0x000012E5
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

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000157 RID: 343 RVA: 0x000030EE File Offset: 0x000012EE
	// (set) Token: 0x06000158 RID: 344 RVA: 0x000030F6 File Offset: 0x000012F6
	public virtual List<ShaderTextureProperty> customShaderProperties
	{
		get
		{
			return this._customShaderProperties;
		}
		set
		{
			this._customShaderProperties = value;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000159 RID: 345 RVA: 0x000030FF File Offset: 0x000012FF
	// (set) Token: 0x0600015A RID: 346 RVA: 0x00003107 File Offset: 0x00001307
	public virtual List<string> customShaderPropNames
	{
		get
		{
			return this._customShaderPropNames_Depricated;
		}
		set
		{
			this._customShaderPropNames_Depricated = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600015B RID: 347 RVA: 0x00003110 File Offset: 0x00001310
	// (set) Token: 0x0600015C RID: 348 RVA: 0x00003118 File Offset: 0x00001318
	public virtual bool doMultiMaterial
	{
		get
		{
			return this._doMultiMaterial;
		}
		set
		{
			this._doMultiMaterial = value;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600015D RID: 349 RVA: 0x00003121 File Offset: 0x00001321
	// (set) Token: 0x0600015E RID: 350 RVA: 0x00003129 File Offset: 0x00001329
	public virtual bool doMultiMaterialSplitAtlasesIfTooBig
	{
		get
		{
			return this._doMultiMaterialSplitAtlasesIfTooBig;
		}
		set
		{
			this._doMultiMaterialSplitAtlasesIfTooBig = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600015F RID: 351 RVA: 0x00003132 File Offset: 0x00001332
	// (set) Token: 0x06000160 RID: 352 RVA: 0x0000313A File Offset: 0x0000133A
	public virtual bool doMultiMaterialSplitAtlasesIfOBUVs
	{
		get
		{
			return this._doMultiMaterialSplitAtlasesIfOBUVs;
		}
		set
		{
			this._doMultiMaterialSplitAtlasesIfOBUVs = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000161 RID: 353 RVA: 0x00003143 File Offset: 0x00001343
	// (set) Token: 0x06000162 RID: 354 RVA: 0x0000314B File Offset: 0x0000134B
	public virtual Material resultMaterial
	{
		get
		{
			return this._resultMaterial;
		}
		set
		{
			this._resultMaterial = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000163 RID: 355 RVA: 0x00003154 File Offset: 0x00001354
	// (set) Token: 0x06000164 RID: 356 RVA: 0x0000315C File Offset: 0x0000135C
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

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000165 RID: 357 RVA: 0x00003165 File Offset: 0x00001365
	// (set) Token: 0x06000166 RID: 358 RVA: 0x0000316D File Offset: 0x0000136D
	public bool doSuggestTreatment
	{
		get
		{
			return this._doSuggestTreatment;
		}
		set
		{
			this._doSuggestTreatment = value;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000167 RID: 359 RVA: 0x00003176 File Offset: 0x00001376
	public MB3_TextureBaker.CreateAtlasesCoroutineResult CoroutineResult
	{
		get
		{
			return this._coroutineResult;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000317E File Offset: 0x0000137E
	public override List<GameObject> GetObjectsToCombine()
	{
		if (this.objsToMesh == null)
		{
			this.objsToMesh = new List<GameObject>();
		}
		return this.objsToMesh;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00003199 File Offset: 0x00001399
	public MB_AtlasesAndRects[] CreateAtlases()
	{
		return this.CreateAtlases(null, false, null);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000031A4 File Offset: 0x000013A4
	public IEnumerator CreateAtlasesCoroutine(ProgressUpdateDelegate progressInfo, MB3_TextureBaker.CreateAtlasesCoroutineResult coroutineResult, bool saveAtlasesAsAssets = false, MB2_EditorMethodsInterface editorMethods = null, float maxTimePerFrame = 0.01f)
	{
		MBVersionConcrete mbversionConcrete = new MBVersionConcrete();
		if (!MB3_TextureCombiner._RunCorutineWithoutPauseIsRunning && (mbversionConcrete.GetMajorVersion() < 5 || (mbversionConcrete.GetMajorVersion() == 5 && mbversionConcrete.GetMinorVersion() < 3)))
		{
			Debug.LogError("Running the texture combiner as a coroutine only works in Unity 5.3 and higher");
			coroutineResult.success = false;
			yield break;
		}
		this.OnCombinedTexturesCoroutineAtlasesAndRects = null;
		if (maxTimePerFrame <= 0f)
		{
			Debug.LogError("maxTimePerFrame must be a value greater than zero");
			coroutineResult.isFinished = true;
			yield break;
		}
		MB2_ValidationLevel validationLevel = Application.isPlaying ? MB2_ValidationLevel.quick : MB2_ValidationLevel.robust;
		if (!MB3_MeshBakerRoot.DoCombinedValidate(this, MB_ObjsToCombineTypes.dontCare, null, validationLevel))
		{
			coroutineResult.isFinished = true;
			yield break;
		}
		if (this._doMultiMaterial && !this._ValidateResultMaterials())
		{
			coroutineResult.isFinished = true;
			yield break;
		}
		if (!this._doMultiMaterial)
		{
			if (this._resultMaterial == null)
			{
				Debug.LogError("Combined Material is null please create and assign a result material.");
				coroutineResult.isFinished = true;
				yield break;
			}
			Shader shader = this._resultMaterial.shader;
			for (int j = 0; j < this.objsToMesh.Count; j++)
			{
				foreach (Material material in MB_Utility.GetGOMaterials(this.objsToMesh[j]))
				{
					if (material != null && material.shader != shader)
					{
						string[] array = new string[5];
						array[0] = "Game object ";
						int num = 1;
						GameObject gameObject = this.objsToMesh[j];
						array[num] = ((gameObject != null) ? gameObject.ToString() : null);
						array[2] = " does not use shader ";
						int num2 = 3;
						Shader shader2 = shader;
						array[num2] = ((shader2 != null) ? shader2.ToString() : null);
						array[4] = " it may not have the required textures. If not small solid color textures will be generated.";
						Debug.LogWarning(string.Concat(array));
					}
				}
			}
		}
		MB3_TextureCombiner combiner = this.CreateAndConfigureTextureCombiner();
		combiner.saveAtlasesAsAssets = saveAtlasesAsAssets;
		int num3 = 1;
		if (this._doMultiMaterial)
		{
			num3 = this.resultMaterials.Length;
		}
		this.OnCombinedTexturesCoroutineAtlasesAndRects = new MB_AtlasesAndRects[num3];
		for (int l = 0; l < this.OnCombinedTexturesCoroutineAtlasesAndRects.Length; l++)
		{
			this.OnCombinedTexturesCoroutineAtlasesAndRects[l] = new MB_AtlasesAndRects();
		}
		int num4;
		for (int i = 0; i < this.OnCombinedTexturesCoroutineAtlasesAndRects.Length; i = num4 + 1)
		{
			List<Material> allowedMaterialsFilter = null;
			Material resultMaterial;
			if (this._doMultiMaterial)
			{
				allowedMaterialsFilter = this.resultMaterials[i].sourceMaterials;
				resultMaterial = this.resultMaterials[i].combinedMaterial;
				combiner.fixOutOfBoundsUVs = this.resultMaterials[i].considerMeshUVs;
			}
			else
			{
				resultMaterial = this._resultMaterial;
			}
			MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult coroutineResult2 = new MB3_TextureCombiner.CombineTexturesIntoAtlasesCoroutineResult();
			yield return combiner.CombineTexturesIntoAtlasesCoroutine(progressInfo, this.OnCombinedTexturesCoroutineAtlasesAndRects[i], resultMaterial, this.objsToMesh, allowedMaterialsFilter, editorMethods, coroutineResult2, maxTimePerFrame, null, false, false);
			coroutineResult.success = coroutineResult2.success;
			if (!coroutineResult.success)
			{
				coroutineResult.isFinished = true;
				yield break;
			}
			coroutineResult2 = null;
			num4 = i;
		}
		this.unpackMat2RectMap(this.textureBakeResults);
		this.textureBakeResults.doMultiMaterial = this._doMultiMaterial;
		if (this._doMultiMaterial)
		{
			this.textureBakeResults.resultMaterials = this.resultMaterials;
		}
		else
		{
			MB_MultiMaterial[] array2 = new MB_MultiMaterial[]
			{
				new MB_MultiMaterial()
			};
			array2[0].combinedMaterial = this._resultMaterial;
			array2[0].considerMeshUVs = this._fixOutOfBoundsUVs;
			array2[0].sourceMaterials = new List<Material>();
			for (int m = 0; m < this.textureBakeResults.materialsAndUVRects.Length; m++)
			{
				array2[0].sourceMaterials.Add(this.textureBakeResults.materialsAndUVRects[m].material);
			}
			this.textureBakeResults.resultMaterials = array2;
		}
		MB3_MeshBakerCommon[] componentsInChildren = base.GetComponentsInChildren<MB3_MeshBakerCommon>();
		for (int n = 0; n < componentsInChildren.Length; n++)
		{
			componentsInChildren[n].textureBakeResults = this.textureBakeResults;
		}
		if (this.LOG_LEVEL >= MB2_LogLevel.info)
		{
			Debug.Log("Created Atlases");
		}
		coroutineResult.isFinished = true;
		if (coroutineResult.success && this.onBuiltAtlasesSuccess != null)
		{
			this.onBuiltAtlasesSuccess();
		}
		if (!coroutineResult.success && this.onBuiltAtlasesFail != null)
		{
			this.onBuiltAtlasesFail();
		}
		yield break;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00041EC8 File Offset: 0x000400C8
	public MB_AtlasesAndRects[] CreateAtlases(ProgressUpdateDelegate progressInfo, bool saveAtlasesAsAssets = false, MB2_EditorMethodsInterface editorMethods = null)
	{
		MB_AtlasesAndRects[] array = null;
		try
		{
			this._coroutineResult = new MB3_TextureBaker.CreateAtlasesCoroutineResult();
			MB3_TextureCombiner.RunCorutineWithoutPause(this.CreateAtlasesCoroutine(progressInfo, this._coroutineResult, saveAtlasesAsAssets, editorMethods, 1000f), 0);
			if (this._coroutineResult.success && this.textureBakeResults != null)
			{
				array = this.OnCombinedTexturesCoroutineAtlasesAndRects;
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		finally
		{
			if (saveAtlasesAsAssets && array != null)
			{
				foreach (MB_AtlasesAndRects mb_AtlasesAndRects in array)
				{
					if (mb_AtlasesAndRects != null && mb_AtlasesAndRects.atlases != null)
					{
						for (int j = 0; j < mb_AtlasesAndRects.atlases.Length; j++)
						{
							if (mb_AtlasesAndRects.atlases[j] != null)
							{
								if (editorMethods != null)
								{
									editorMethods.Destroy(mb_AtlasesAndRects.atlases[j]);
								}
								else
								{
									MB_Utility.Destroy(mb_AtlasesAndRects.atlases[j]);
								}
							}
						}
					}
				}
			}
		}
		return array;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00041FAC File Offset: 0x000401AC
	private void unpackMat2RectMap(MB2_TextureBakeResults tbr)
	{
		List<Material> list = new List<Material>();
		List<MB_MaterialAndUVRect> list2 = new List<MB_MaterialAndUVRect>();
		List<Rect> list3 = new List<Rect>();
		for (int i = 0; i < this.OnCombinedTexturesCoroutineAtlasesAndRects.Length; i++)
		{
			List<MB_MaterialAndUVRect> mat2rect_map = this.OnCombinedTexturesCoroutineAtlasesAndRects[i].mat2rect_map;
			if (mat2rect_map != null)
			{
				for (int j = 0; j < mat2rect_map.Count; j++)
				{
					list2.Add(mat2rect_map[j]);
					list.Add(mat2rect_map[j].material);
					list3.Add(mat2rect_map[j].atlasRect);
				}
			}
		}
		tbr.version = MB2_TextureBakeResults.VERSION;
		tbr.materialsAndUVRects = list2.ToArray();
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00042058 File Offset: 0x00040258
	public MB3_TextureCombiner CreateAndConfigureTextureCombiner()
	{
		return new MB3_TextureCombiner
		{
			LOG_LEVEL = this.LOG_LEVEL,
			atlasPadding = this._atlasPadding,
			maxAtlasSize = this._maxAtlasSize,
			maxAtlasHeightOverride = this._maxAtlasHeightOverride,
			maxAtlasWidthOverride = this._maxAtlasWidthOverride,
			useMaxAtlasHeightOverride = this._useMaxAtlasHeightOverride,
			useMaxAtlasWidthOverride = this._useMaxAtlasWidthOverride,
			customShaderPropNames = this._customShaderProperties,
			fixOutOfBoundsUVs = this._fixOutOfBoundsUVs,
			maxTilingBakeSize = this._maxTilingBakeSize,
			packingAlgorithm = this._packingAlgorithm,
			meshBakerTexturePackerForcePowerOfTwo = this._meshBakerTexturePackerForcePowerOfTwo,
			resizePowerOfTwoTextures = this._resizePowerOfTwoTextures,
			considerNonTextureProperties = this._considerNonTextureProperties
		};
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00042114 File Offset: 0x00040314
	public static void ConfigureNewMaterialToMatchOld(Material newMat, Material original)
	{
		if (original == null)
		{
			string str = "Original material is null, could not copy properties to ";
			string str2 = (newMat != null) ? newMat.ToString() : null;
			string str3 = ". Setting shader to ";
			Shader shader = newMat.shader;
			Debug.LogWarning(str + str2 + str3 + ((shader != null) ? shader.ToString() : null));
			return;
		}
		newMat.shader = original.shader;
		newMat.CopyPropertiesFromMaterial(original);
		ShaderTextureProperty[] shaderTexPropertyNames = MB3_TextureCombinerPipeline.shaderTexPropertyNames;
		for (int i = 0; i < shaderTexPropertyNames.Length; i++)
		{
			Vector2 one = Vector2.one;
			Vector2 zero = Vector2.zero;
			if (newMat.HasProperty(shaderTexPropertyNames[i].name))
			{
				newMat.SetTextureOffset(shaderTexPropertyNames[i].name, zero);
				newMat.SetTextureScale(shaderTexPropertyNames[i].name, one);
			}
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000421C0 File Offset: 0x000403C0
	private string PrintSet(HashSet<Material> s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Material material in s)
		{
			StringBuilder stringBuilder2 = stringBuilder;
			Material material2 = material;
			stringBuilder2.Append(((material2 != null) ? material2.ToString() : null) + ",");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00042234 File Offset: 0x00040434
	private bool _ValidateResultMaterials()
	{
		HashSet<Material> hashSet = new HashSet<Material>();
		for (int i = 0; i < this.objsToMesh.Count; i++)
		{
			if (this.objsToMesh[i] != null)
			{
				Material[] gomaterials = MB_Utility.GetGOMaterials(this.objsToMesh[i]);
				for (int j = 0; j < gomaterials.Length; j++)
				{
					if (gomaterials[j] != null)
					{
						hashSet.Add(gomaterials[j]);
					}
				}
			}
		}
		HashSet<Material> hashSet2 = new HashSet<Material>();
		for (int k = 0; k < this.resultMaterials.Length; k++)
		{
			for (int l = k + 1; l < this.resultMaterials.Length; l++)
			{
				if (this.resultMaterials[k].combinedMaterial == this.resultMaterials[l].combinedMaterial)
				{
					Debug.LogError(string.Format("Source To Combined Mapping: Submesh {0} and Submesh {1} use the same combined material. These should be different", k, l));
					return false;
				}
			}
			MB_MultiMaterial mb_MultiMaterial = this.resultMaterials[k];
			if (mb_MultiMaterial.combinedMaterial == null)
			{
				Debug.LogError("Combined Material is null please create and assign a result material.");
				return false;
			}
			Shader shader = mb_MultiMaterial.combinedMaterial.shader;
			for (int m = 0; m < mb_MultiMaterial.sourceMaterials.Count; m++)
			{
				if (mb_MultiMaterial.sourceMaterials[m] == null)
				{
					Debug.LogError("There are null entries in the list of Source Materials");
					return false;
				}
				if (shader != mb_MultiMaterial.sourceMaterials[m].shader)
				{
					string[] array = new string[5];
					array[0] = "Source material ";
					int num = 1;
					Material material = mb_MultiMaterial.sourceMaterials[m];
					array[num] = ((material != null) ? material.ToString() : null);
					array[2] = " does not use shader ";
					int num2 = 3;
					Shader shader2 = shader;
					array[num2] = ((shader2 != null) ? shader2.ToString() : null);
					array[4] = " it may not have the required textures. If not empty textures will be generated.";
					Debug.LogWarning(string.Concat(array));
				}
				if (hashSet2.Contains(mb_MultiMaterial.sourceMaterials[m]))
				{
					string str = "A Material ";
					Material material2 = mb_MultiMaterial.sourceMaterials[m];
					Debug.LogError(str + ((material2 != null) ? material2.ToString() : null) + " appears more than once in the list of source materials in the source material to combined mapping. Each source material must be unique.");
					return false;
				}
				hashSet2.Add(mb_MultiMaterial.sourceMaterials[m]);
			}
		}
		if (hashSet.IsProperSubsetOf(hashSet2))
		{
			hashSet2.ExceptWith(hashSet);
			Debug.LogWarning("There are materials in the mapping that are not used on your source objects: " + this.PrintSet(hashSet2));
		}
		if (this.resultMaterials != null && this.resultMaterials.Length != 0 && hashSet2.IsProperSubsetOf(hashSet))
		{
			hashSet.ExceptWith(hashSet2);
			Debug.LogError("There are materials on the objects to combine that are not in the mapping: " + this.PrintSet(hashSet));
			return false;
		}
		return true;
	}

	// Token: 0x040000D2 RID: 210
	public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	protected MB2_TextureBakeResults _textureBakeResults;

	// Token: 0x040000D4 RID: 212
	[SerializeField]
	protected int _atlasPadding = 1;

	// Token: 0x040000D5 RID: 213
	[SerializeField]
	protected int _maxAtlasSize = 4096;

	// Token: 0x040000D6 RID: 214
	[SerializeField]
	protected bool _useMaxAtlasWidthOverride;

	// Token: 0x040000D7 RID: 215
	[SerializeField]
	protected int _maxAtlasWidthOverride = 4096;

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	protected bool _useMaxAtlasHeightOverride;

	// Token: 0x040000D9 RID: 217
	[SerializeField]
	protected int _maxAtlasHeightOverride = 4096;

	// Token: 0x040000DA RID: 218
	[SerializeField]
	protected bool _resizePowerOfTwoTextures;

	// Token: 0x040000DB RID: 219
	[SerializeField]
	protected bool _fixOutOfBoundsUVs;

	// Token: 0x040000DC RID: 220
	[SerializeField]
	protected int _maxTilingBakeSize = 1024;

	// Token: 0x040000DD RID: 221
	[SerializeField]
	protected MB2_PackingAlgorithmEnum _packingAlgorithm = MB2_PackingAlgorithmEnum.MeshBakerTexturePacker;

	// Token: 0x040000DE RID: 222
	[SerializeField]
	protected bool _meshBakerTexturePackerForcePowerOfTwo = true;

	// Token: 0x040000DF RID: 223
	[SerializeField]
	protected List<ShaderTextureProperty> _customShaderProperties = new List<ShaderTextureProperty>();

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	protected List<string> _customShaderPropNames_Depricated = new List<string>();

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	protected bool _doMultiMaterial;

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	protected bool _doMultiMaterialSplitAtlasesIfTooBig = true;

	// Token: 0x040000E3 RID: 227
	[SerializeField]
	protected bool _doMultiMaterialSplitAtlasesIfOBUVs = true;

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	protected Material _resultMaterial;

	// Token: 0x040000E5 RID: 229
	[SerializeField]
	protected bool _considerNonTextureProperties;

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	protected bool _doSuggestTreatment = true;

	// Token: 0x040000E7 RID: 231
	private MB3_TextureBaker.CreateAtlasesCoroutineResult _coroutineResult;

	// Token: 0x040000E8 RID: 232
	public MB_MultiMaterial[] resultMaterials = new MB_MultiMaterial[0];

	// Token: 0x040000E9 RID: 233
	public List<GameObject> objsToMesh;

	// Token: 0x040000EA RID: 234
	public MB3_TextureBaker.OnCombinedTexturesCoroutineSuccess onBuiltAtlasesSuccess;

	// Token: 0x040000EB RID: 235
	public MB3_TextureBaker.OnCombinedTexturesCoroutineFail onBuiltAtlasesFail;

	// Token: 0x040000EC RID: 236
	public MB_AtlasesAndRects[] OnCombinedTexturesCoroutineAtlasesAndRects;

	// Token: 0x0200004B RID: 75
	// (Invoke) Token: 0x06000173 RID: 371
	public delegate void OnCombinedTexturesCoroutineSuccess();

	// Token: 0x0200004C RID: 76
	// (Invoke) Token: 0x06000177 RID: 375
	public delegate void OnCombinedTexturesCoroutineFail();

	// Token: 0x0200004D RID: 77
	public class CreateAtlasesCoroutineResult
	{
		// Token: 0x040000ED RID: 237
		public bool success = true;

		// Token: 0x040000EE RID: 238
		public bool isFinished;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class MB2_TextureBakeResults : ScriptableObject
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002D5A File Offset: 0x00000F5A
	public static int VERSION
	{
		get
		{
			return 3252;
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00002D61 File Offset: 0x00000F61
	public MB2_TextureBakeResults()
	{
		this.version = MB2_TextureBakeResults.VERSION;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00040B60 File Offset: 0x0003ED60
	private void OnEnable()
	{
		if (this.version < 3251)
		{
			for (int i = 0; i < this.materialsAndUVRects.Length; i++)
			{
				this.materialsAndUVRects[i].allPropsUseSameTiling = true;
			}
		}
		this.version = MB2_TextureBakeResults.VERSION;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00040BA8 File Offset: 0x0003EDA8
	public static MB2_TextureBakeResults CreateForMaterialsOnRenderer(GameObject[] gos, List<Material> matsOnTargetRenderer)
	{
		HashSet<Material> hashSet = new HashSet<Material>(matsOnTargetRenderer);
		for (int i = 0; i < gos.Length; i++)
		{
			if (gos[i] == null)
			{
				Debug.LogError(string.Format("Game object {0} in list of objects to add was null", i));
				return null;
			}
			Material[] gomaterials = MB_Utility.GetGOMaterials(gos[i]);
			if (gomaterials.Length == 0)
			{
				Debug.LogError(string.Format("Game object {0} in list of objects to add no renderer", i));
				return null;
			}
			for (int j = 0; j < gomaterials.Length; j++)
			{
				if (!hashSet.Contains(gomaterials[j]))
				{
					hashSet.Add(gomaterials[j]);
				}
			}
		}
		Material[] array = new Material[hashSet.Count];
		hashSet.CopyTo(array);
		MB2_TextureBakeResults mb2_TextureBakeResults = (MB2_TextureBakeResults)ScriptableObject.CreateInstance(typeof(MB2_TextureBakeResults));
		List<MB_MaterialAndUVRect> list = new List<MB_MaterialAndUVRect>();
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k] != null)
			{
				MB_MaterialAndUVRect item = new MB_MaterialAndUVRect(array[k], new Rect(0f, 0f, 1f, 1f), true, new Rect(0f, 0f, 1f, 1f), new Rect(0f, 0f, 1f, 1f), new Rect(0f, 0f, 0f, 0f), MB_TextureTilingTreatment.none, "");
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}
		mb2_TextureBakeResults.resultMaterials = new MB_MultiMaterial[list.Count];
		for (int l = 0; l < list.Count; l++)
		{
			mb2_TextureBakeResults.resultMaterials[l] = new MB_MultiMaterial();
			List<Material> list2 = new List<Material>();
			list2.Add(list[l].material);
			mb2_TextureBakeResults.resultMaterials[l].sourceMaterials = list2;
			mb2_TextureBakeResults.resultMaterials[l].combinedMaterial = list[l].material;
			mb2_TextureBakeResults.resultMaterials[l].considerMeshUVs = false;
		}
		if (array.Length == 1)
		{
			mb2_TextureBakeResults.doMultiMaterial = false;
		}
		else
		{
			mb2_TextureBakeResults.doMultiMaterial = true;
		}
		mb2_TextureBakeResults.materialsAndUVRects = list.ToArray();
		return mb2_TextureBakeResults;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00040DD4 File Offset: 0x0003EFD4
	public bool DoAnyResultMatsUseConsiderMeshUVs()
	{
		if (this.resultMaterials == null)
		{
			return false;
		}
		for (int i = 0; i < this.resultMaterials.Length; i++)
		{
			if (this.resultMaterials[i].considerMeshUVs)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00040E10 File Offset: 0x0003F010
	public bool ContainsMaterial(Material m)
	{
		for (int i = 0; i < this.materialsAndUVRects.Length; i++)
		{
			if (this.materialsAndUVRects[i].material == m)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00040E48 File Offset: 0x0003F048
	public string GetDescription()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Shaders:\n");
		HashSet<Shader> hashSet = new HashSet<Shader>();
		if (this.materialsAndUVRects != null)
		{
			for (int i = 0; i < this.materialsAndUVRects.Length; i++)
			{
				if (this.materialsAndUVRects[i].material != null)
				{
					hashSet.Add(this.materialsAndUVRects[i].material.shader);
				}
			}
		}
		foreach (Shader shader in hashSet)
		{
			stringBuilder.Append("  ").Append(shader.name).AppendLine();
		}
		stringBuilder.Append("Materials:\n");
		if (this.materialsAndUVRects != null)
		{
			for (int j = 0; j < this.materialsAndUVRects.Length; j++)
			{
				if (this.materialsAndUVRects[j].material != null)
				{
					stringBuilder.Append("  ").Append(this.materialsAndUVRects[j].material.name).AppendLine();
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00040F80 File Offset: 0x0003F180
	public static bool IsMeshAndMaterialRectEnclosedByAtlasRect(MB_TextureTilingTreatment tilingTreatment, Rect uvR, Rect sourceMaterialTiling, Rect samplingEncapsulatinRect, MB2_LogLevel logLevel)
	{
		Rect rect = default(Rect);
		rect = MB3_UVTransformUtility.CombineTransforms(ref uvR, ref sourceMaterialTiling);
		if (logLevel >= MB2_LogLevel.trace && logLevel >= MB2_LogLevel.trace)
		{
			Debug.Log(string.Concat(new string[]
			{
				"IsMeshAndMaterialRectEnclosedByAtlasRect Rect in atlas uvR=",
				uvR.ToString("f5"),
				" sourceMaterialTiling=",
				sourceMaterialTiling.ToString("f5"),
				"Potential Rect (must fit in encapsulating) ",
				rect.ToString("f5"),
				" encapsulating=",
				samplingEncapsulatinRect.ToString("f5"),
				" tilingTreatment=",
				tilingTreatment.ToString()
			}));
		}
		if (tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeX)
		{
			if (MB3_UVTransformUtility.LineSegmentContainsShifted(samplingEncapsulatinRect.y, samplingEncapsulatinRect.height, rect.y, rect.height))
			{
				return true;
			}
		}
		else if (tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeY)
		{
			if (MB3_UVTransformUtility.LineSegmentContainsShifted(samplingEncapsulatinRect.x, samplingEncapsulatinRect.width, rect.x, rect.width))
			{
				return true;
			}
		}
		else
		{
			if (tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeXY)
			{
				return true;
			}
			if (MB3_UVTransformUtility.RectContainsShifted(ref samplingEncapsulatinRect, ref rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040000A7 RID: 167
	public int version;

	// Token: 0x040000A8 RID: 168
	public MB_MaterialAndUVRect[] materialsAndUVRects;

	// Token: 0x040000A9 RID: 169
	public MB_MultiMaterial[] resultMaterials;

	// Token: 0x040000AA RID: 170
	public bool doMultiMaterial;

	// Token: 0x0200003A RID: 58
	public class Material2AtlasRectangleMapper
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0004109C File Offset: 0x0003F29C
		public Material2AtlasRectangleMapper(MB2_TextureBakeResults res)
		{
			this.tbr = res;
			this.matsAndSrcUVRect = res.materialsAndUVRects;
			this.numTimesMatAppearsInAtlas = new int[this.matsAndSrcUVRect.Length];
			for (int i = 0; i < this.matsAndSrcUVRect.Length; i++)
			{
				if (this.numTimesMatAppearsInAtlas[i] <= 1)
				{
					int num = 1;
					for (int j = i + 1; j < this.matsAndSrcUVRect.Length; j++)
					{
						if (this.matsAndSrcUVRect[i].material == this.matsAndSrcUVRect[j].material)
						{
							num++;
						}
					}
					this.numTimesMatAppearsInAtlas[i] = num;
					if (num > 1)
					{
						for (int k = i + 1; k < this.matsAndSrcUVRect.Length; k++)
						{
							if (this.matsAndSrcUVRect[i].material == this.matsAndSrcUVRect[k].material)
							{
								this.numTimesMatAppearsInAtlas[k] = num;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00041184 File Offset: 0x0003F384
		public bool TryMapMaterialToUVRect(Material mat, Mesh m, int submeshIdx, int idxInResultMats, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache, Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisCache, out MB_TextureTilingTreatment tilingTreatment, out Rect rectInAtlas, out Rect encapsulatingRectOut, out Rect sourceMaterialTilingOut, ref string errorMsg, MB2_LogLevel logLevel)
		{
			if (this.tbr.version < MB2_TextureBakeResults.VERSION)
			{
				this.UpgradeToCurrentVersion(this.tbr);
			}
			tilingTreatment = MB_TextureTilingTreatment.unknown;
			if (this.tbr.materialsAndUVRects.Length == 0)
			{
				errorMsg = "The 'Texture Bake Result' needs to be re-baked to be compatible with this version of Mesh Baker. Please re-bake using the MB3_TextureBaker.";
				rectInAtlas = default(Rect);
				encapsulatingRectOut = default(Rect);
				sourceMaterialTilingOut = default(Rect);
				return false;
			}
			if (mat == null)
			{
				rectInAtlas = default(Rect);
				encapsulatingRectOut = default(Rect);
				sourceMaterialTilingOut = default(Rect);
				errorMsg = string.Format("Mesh {0} Had no material on submesh {1} cannot map to a material in the atlas", m.name, submeshIdx);
				return false;
			}
			if (submeshIdx >= m.subMeshCount)
			{
				errorMsg = "Submesh index is greater than the number of submeshes";
				rectInAtlas = default(Rect);
				encapsulatingRectOut = default(Rect);
				sourceMaterialTilingOut = default(Rect);
				return false;
			}
			int num = -1;
			for (int i = 0; i < this.matsAndSrcUVRect.Length; i++)
			{
				if (mat == this.matsAndSrcUVRect[i].material)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				rectInAtlas = default(Rect);
				encapsulatingRectOut = default(Rect);
				sourceMaterialTilingOut = default(Rect);
				errorMsg = string.Format("Material {0} could not be found in the Texture Bake Result", mat.name);
				return false;
			}
			if (!this.tbr.resultMaterials[idxInResultMats].considerMeshUVs)
			{
				if (this.numTimesMatAppearsInAtlas[num] != 1)
				{
					Debug.LogError("There is a problem with this TextureBakeResults. FixOutOfBoundsUVs is false and a material appears more than once.");
				}
				MB_MaterialAndUVRect mb_MaterialAndUVRect = this.matsAndSrcUVRect[num];
				rectInAtlas = mb_MaterialAndUVRect.atlasRect;
				tilingTreatment = mb_MaterialAndUVRect.tilingTreatment;
				encapsulatingRectOut = mb_MaterialAndUVRect.GetEncapsulatingRect();
				sourceMaterialTilingOut = mb_MaterialAndUVRect.GetMaterialTilingRect();
				return true;
			}
			MB_Utility.MeshAnalysisResult[] array;
			if (!meshAnalysisCache.TryGetValue(m.GetInstanceID(), out array))
			{
				array = new MB_Utility.MeshAnalysisResult[m.subMeshCount];
				for (int j = 0; j < m.subMeshCount; j++)
				{
					MB_Utility.hasOutOfBoundsUVs(meshChannelCache.GetUv0Raw(m), m, ref array[j], j);
				}
				meshAnalysisCache.Add(m.GetInstanceID(), array);
			}
			bool flag = false;
			Rect samplingEncapsulatinRect = new Rect(0f, 0f, 0f, 0f);
			Rect allPropsUseSameTiling_sourceMaterialTiling = new Rect(0f, 0f, 0f, 0f);
			if (logLevel >= MB2_LogLevel.trace)
			{
				Debug.Log(string.Format("Trying to find a rectangle in atlas capable of holding tiled sampling rect for mesh {0} using material {1} meshUVrect={2}", m, mat, array[submeshIdx].uvRect.ToString("f5")));
			}
			for (int k = num; k < this.matsAndSrcUVRect.Length; k++)
			{
				MB_MaterialAndUVRect mb_MaterialAndUVRect2 = this.matsAndSrcUVRect[k];
				if (mb_MaterialAndUVRect2.material == mat)
				{
					if (mb_MaterialAndUVRect2.allPropsUseSameTiling)
					{
						samplingEncapsulatinRect = mb_MaterialAndUVRect2.allPropsUseSameTiling_samplingEncapsulatinRect;
						allPropsUseSameTiling_sourceMaterialTiling = mb_MaterialAndUVRect2.allPropsUseSameTiling_sourceMaterialTiling;
					}
					else
					{
						samplingEncapsulatinRect = mb_MaterialAndUVRect2.propsUseDifferntTiling_srcUVsamplingRect;
						allPropsUseSameTiling_sourceMaterialTiling = new Rect(0f, 0f, 1f, 1f);
					}
					if (MB2_TextureBakeResults.IsMeshAndMaterialRectEnclosedByAtlasRect(mb_MaterialAndUVRect2.tilingTreatment, array[submeshIdx].uvRect, allPropsUseSameTiling_sourceMaterialTiling, samplingEncapsulatinRect, logLevel))
					{
						if (logLevel >= MB2_LogLevel.trace)
						{
							Debug.Log("Found rect in atlas capable of containing tiled sampling rect for mesh " + ((m != null) ? m.ToString() : null) + " at idx=" + k.ToString());
						}
						num = k;
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				MB_MaterialAndUVRect mb_MaterialAndUVRect3 = this.matsAndSrcUVRect[num];
				rectInAtlas = mb_MaterialAndUVRect3.atlasRect;
				tilingTreatment = mb_MaterialAndUVRect3.tilingTreatment;
				encapsulatingRectOut = mb_MaterialAndUVRect3.GetEncapsulatingRect();
				sourceMaterialTilingOut = mb_MaterialAndUVRect3.GetMaterialTilingRect();
				return true;
			}
			rectInAtlas = default(Rect);
			encapsulatingRectOut = default(Rect);
			sourceMaterialTilingOut = default(Rect);
			errorMsg = string.Format("Could not find a tiled rectangle in the atlas capable of containing the uv and material tiling on mesh {0} for material {1}. Was this mesh included when atlases were baked?", m.name, mat);
			return false;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00041510 File Offset: 0x0003F710
		private void UpgradeToCurrentVersion(MB2_TextureBakeResults tbr)
		{
			if (tbr.version < 3252)
			{
				for (int i = 0; i < tbr.materialsAndUVRects.Length; i++)
				{
					tbr.materialsAndUVRects[i].allPropsUseSameTiling = true;
				}
			}
		}

		// Token: 0x040000AB RID: 171
		private MB2_TextureBakeResults tbr;

		// Token: 0x040000AC RID: 172
		private int[] numTimesMatAppearsInAtlas;

		// Token: 0x040000AD RID: 173
		private MB_MaterialAndUVRect[] matsAndSrcUVRect;
	}
}

using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000042 RID: 66
public abstract class MB3_MeshBakerCommon : MB3_MeshBakerRoot
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000116 RID: 278
	public abstract MB3_MeshCombiner meshCombiner { get; }

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000117 RID: 279 RVA: 0x00002EC6 File Offset: 0x000010C6
	// (set) Token: 0x06000118 RID: 280 RVA: 0x00002ED3 File Offset: 0x000010D3
	public override MB2_TextureBakeResults textureBakeResults
	{
		get
		{
			return this.meshCombiner.textureBakeResults;
		}
		set
		{
			this.meshCombiner.textureBakeResults = value;
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0004172C File Offset: 0x0003F92C
	public override List<GameObject> GetObjectsToCombine()
	{
		if (!this.useObjsToMeshFromTexBaker)
		{
			if (this.objsToMesh == null)
			{
				this.objsToMesh = new List<GameObject>();
			}
			return this.objsToMesh;
		}
		MB3_TextureBaker component = base.gameObject.GetComponent<MB3_TextureBaker>();
		if (component == null)
		{
			component = base.gameObject.transform.parent.GetComponent<MB3_TextureBaker>();
		}
		if (component != null)
		{
			return component.GetObjectsToCombine();
		}
		Debug.LogWarning("Use Objects To Mesh From Texture Baker was checked but no texture baker");
		return new List<GameObject>();
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000417A8 File Offset: 0x0003F9A8
	public void EnableDisableSourceObjectRenderers(bool show)
	{
		for (int i = 0; i < this.GetObjectsToCombine().Count; i++)
		{
			GameObject gameObject = this.GetObjectsToCombine()[i];
			if (gameObject != null)
			{
				Renderer renderer = MB_Utility.GetRenderer(gameObject);
				if (renderer != null)
				{
					renderer.enabled = show;
				}
				LODGroup componentInParent = renderer.GetComponentInParent<LODGroup>();
				if (componentInParent != null)
				{
					bool flag = true;
					LOD[] lods = componentInParent.GetLODs();
					for (int j = 0; j < lods.Length; j++)
					{
						for (int k = 0; k < lods[j].renderers.Length; k++)
						{
							if (lods[j].renderers[k] != renderer)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						componentInParent.enabled = show;
					}
				}
			}
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00002EE1 File Offset: 0x000010E1
	public virtual void ClearMesh()
	{
		this.meshCombiner.ClearMesh();
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00002EEE File Offset: 0x000010EE
	public virtual void DestroyMesh()
	{
		this.meshCombiner.DestroyMesh();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00002EFB File Offset: 0x000010FB
	public virtual void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
	{
		this.meshCombiner.DestroyMeshEditor(editorMethods);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00002F09 File Offset: 0x00001109
	public virtual int GetNumObjectsInCombined()
	{
		return this.meshCombiner.GetNumObjectsInCombined();
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00002F16 File Offset: 0x00001116
	public virtual int GetNumVerticesFor(GameObject go)
	{
		return this.meshCombiner.GetNumVerticesFor(go);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00041878 File Offset: 0x0003FA78
	public MB3_TextureBaker GetTextureBaker()
	{
		MB3_TextureBaker component = base.GetComponent<MB3_TextureBaker>();
		if (component != null)
		{
			return component;
		}
		if (base.transform.parent != null)
		{
			return base.transform.parent.GetComponent<MB3_TextureBaker>();
		}
		return null;
	}

	// Token: 0x06000121 RID: 289
	public abstract bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true);

	// Token: 0x06000122 RID: 290
	public abstract bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource = true);

	// Token: 0x06000123 RID: 291 RVA: 0x00002F24 File Offset: 0x00001124
	public virtual void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.Apply(uv2GenerationMethod);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000418BC File Offset: 0x0003FABC
	public virtual void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool uv2, bool uv3, bool uv4, bool colors, bool bones = false, bool blendShapesFlag = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.Apply(triangles, vertices, normals, tangents, uvs, uv2, uv3, uv4, colors, bones, blendShapesFlag, uv2GenerationMethod);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00002F4D File Offset: 0x0000114D
	public virtual bool CombinedMeshContains(GameObject go)
	{
		return this.meshCombiner.CombinedMeshContains(go);
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00041904 File Offset: 0x0003FB04
	public virtual void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false, bool updateColors = false, bool updateSkinningInfo = false)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.UpdateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo, false);
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00002F5B File Offset: 0x0000115B
	public virtual void UpdateSkinnedMeshApproximateBounds()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBounds();
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00002F70 File Offset: 0x00001170
	public virtual void UpdateSkinnedMeshApproximateBoundsFromBones()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBones();
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00002F85 File Offset: 0x00001185
	public virtual void UpdateSkinnedMeshApproximateBoundsFromBounds()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBounds();
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0004194C File Offset: 0x0003FB4C
	protected virtual bool _ValidateForUpdateSkinnedMeshBounds()
	{
		if (this.meshCombiner.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
		{
			Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
			return false;
		}
		if (this.meshCombiner.resultSceneObject == null)
		{
			Debug.LogWarning("Result Scene Object does not exist. No point in calling UpdateSkinnedMeshApproximateBounds.");
			return false;
		}
		if (this.meshCombiner.resultSceneObject.GetComponentInChildren<SkinnedMeshRenderer>() == null)
		{
			Debug.LogWarning("No SkinnedMeshRenderer on result scene object.");
			return false;
		}
		return true;
	}

	// Token: 0x040000BE RID: 190
	public List<GameObject> objsToMesh;

	// Token: 0x040000BF RID: 191
	public bool useObjsToMeshFromTexBaker = true;

	// Token: 0x040000C0 RID: 192
	public bool clearBuffersAfterBake = true;

	// Token: 0x040000C1 RID: 193
	public string bakeAssetsInPlaceFolderPath;

	// Token: 0x040000C2 RID: 194
	[HideInInspector]
	public GameObject resultPrefab;
}

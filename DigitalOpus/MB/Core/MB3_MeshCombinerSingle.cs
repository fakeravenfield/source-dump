using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200044E RID: 1102
	[Serializable]
	public class MB3_MeshCombinerSingle : MB3_MeshCombiner
	{
		// Token: 0x17000181 RID: 385
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x000B14A8 File Offset: 0x000AF6A8
		public override MB2_TextureBakeResults textureBakeResults
		{
			set
			{
				if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && this._textureBakeResults != value && this._textureBakeResults != null && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("If Texture Bake Result is changed then objects currently in combined mesh may be invalid.");
				}
				this._textureBakeResults = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x00014C91 File Offset: 0x00012E91
		public override MB_RenderType renderType
		{
			set
			{
				if (value == MB_RenderType.skinnedMeshRenderer && this._renderType == MB_RenderType.meshRenderer && this.boneWeights.Length != this.verts.Length)
				{
					Debug.LogError("Can't set the render type to SkinnedMeshRenderer without clearing the mesh first. Try deleteing the CombinedMesh scene object.");
				}
				this._renderType = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x00014CC2 File Offset: 0x00012EC2
		public override GameObject resultSceneObject
		{
			set
			{
				if (this._resultSceneObject != value)
				{
					this._targetRenderer = null;
					if (this._mesh != null && this._LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result Scene Object was changed when this mesh baker component had a reference to a mesh. If mesh is being used by another object make sure to reset the mesh to none before baking to avoid overwriting the other mesh.");
					}
				}
				this._resultSceneObject = value;
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00014D01 File Offset: 0x00012F01
		private MB3_MeshCombinerSingle.MB_DynamicGameObject instance2Combined_MapGet(GameObject gameObjectID)
		{
			return this._instance2combined_map[gameObjectID];
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00014D0F File Offset: 0x00012F0F
		private void instance2Combined_MapAdd(GameObject gameObjectID, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
		{
			this._instance2combined_map.Add(gameObjectID, dgo);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00014D1E File Offset: 0x00012F1E
		private void instance2Combined_MapRemove(GameObject gameObjectID)
		{
			this._instance2combined_map.Remove(gameObjectID);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00014D2D File Offset: 0x00012F2D
		private bool instance2Combined_MapTryGetValue(GameObject gameObjectID, out MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
		{
			return this._instance2combined_map.TryGetValue(gameObjectID, out dgo);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00014D3C File Offset: 0x00012F3C
		private int instance2Combined_MapCount()
		{
			return this._instance2combined_map.Count;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00014D49 File Offset: 0x00012F49
		private void instance2Combined_MapClear()
		{
			this._instance2combined_map.Clear();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00014D56 File Offset: 0x00012F56
		private bool instance2Combined_MapContainsKey(GameObject gameObjectID)
		{
			return this._instance2combined_map.ContainsKey(gameObjectID);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000B14FC File Offset: 0x000AF6FC
		private bool InstanceID2DGO(int instanceID, out MB3_MeshCombinerSingle.MB_DynamicGameObject dgoGameObject)
		{
			for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
			{
				if (this.mbDynamicObjectsInCombinedMesh[i].instanceID == instanceID)
				{
					dgoGameObject = this.mbDynamicObjectsInCombinedMesh[i];
					return true;
				}
			}
			dgoGameObject = null;
			return false;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00014D64 File Offset: 0x00012F64
		public override int GetNumObjectsInCombined()
		{
			return this.mbDynamicObjectsInCombinedMesh.Count;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00014D71 File Offset: 0x00012F71
		public override List<GameObject> GetObjectsInCombined()
		{
			List<GameObject> list = new List<GameObject>();
			list.AddRange(this.objectsInCombinedMesh);
			return list;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00014D84 File Offset: 0x00012F84
		public Mesh GetMesh()
		{
			if (this._mesh == null)
			{
				this._mesh = this.NewMesh();
			}
			return this._mesh;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00014DA6 File Offset: 0x00012FA6
		public void SetMesh(Mesh m)
		{
			if (m == null)
			{
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.AssignedByUser;
			}
			else
			{
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.NoMesh;
			}
			this._mesh = m;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00014DC8 File Offset: 0x00012FC8
		public Transform[] GetBones()
		{
			return this.bones;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public override int GetLightmapIndex()
		{
			if (this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout || this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
			{
				return this.lightmapIndex;
			}
			return -1;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00014DEB File Offset: 0x00012FEB
		public override int GetNumVerticesFor(GameObject go)
		{
			return this.GetNumVerticesFor(go.GetInstanceID());
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000B1548 File Offset: 0x000AF748
		public override int GetNumVerticesFor(int instanceID)
		{
			MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = null;
			this.InstanceID2DGO(instanceID, out mb_DynamicGameObject);
			if (mb_DynamicGameObject != null)
			{
				return mb_DynamicGameObject.numVerts;
			}
			return -1;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000B156C File Offset: 0x000AF76C
		public override Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap()
		{
			Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> dictionary = new Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue>();
			for (int i = 0; i < this.blendShapesInCombined.Length; i++)
			{
				MB3_MeshCombiner.MBBlendShapeValue mbblendShapeValue = new MB3_MeshCombiner.MBBlendShapeValue();
				mbblendShapeValue.combinedMeshGameObject = this._targetRenderer.gameObject;
				mbblendShapeValue.blendShapeIndex = i;
				dictionary.Add(new MB3_MeshCombiner.MBBlendShapeKey(this.blendShapesInCombined[i].gameObjectID, this.blendShapesInCombined[i].indexInSource), mbblendShapeValue);
			}
			return dictionary;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000B15D8 File Offset: 0x000AF7D8
		private bool _Initialize(int numResultMats)
		{
			if (this.mbDynamicObjectsInCombinedMesh.Count == 0)
			{
				this.lightmapIndex = -1;
			}
			if (this._mesh == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("_initialize Creating new Mesh", Array.Empty<object>());
				}
				this._mesh = this.GetMesh();
			}
			if (this.instance2Combined_MapCount() != this.mbDynamicObjectsInCombinedMesh.Count)
			{
				this.instance2Combined_MapClear();
				for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
				{
					if (this.mbDynamicObjectsInCombinedMesh[i] != null)
					{
						if (this.mbDynamicObjectsInCombinedMesh[i].gameObject == null)
						{
							Debug.LogError("This MeshBaker contains information from a previous bake that is incomlete. It may have been baked by a previous version of Mesh Baker. If you are trying to update/modify a previously baked combined mesh. Try doing the original bake.");
							return false;
						}
						this.instance2Combined_MapAdd(this.mbDynamicObjectsInCombinedMesh[i].gameObject, this.mbDynamicObjectsInCombinedMesh[i]);
					}
				}
				this.boneWeights = this._mesh.boneWeights;
			}
			if (this.objectsInCombinedMesh.Count == 0 && this.submeshTris.Length != numResultMats)
			{
				this.submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[numResultMats];
				for (int j = 0; j < this.submeshTris.Length; j++)
				{
					this.submeshTris[j] = new MB3_MeshCombinerSingle.SerializableIntArray(0);
				}
			}
			if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && this.mbDynamicObjectsInCombinedMesh[0].indexesOfBonesUsed.Length == 0 && this.renderType == MB_RenderType.skinnedMeshRenderer && this.boneWeights.Length != 0)
			{
				for (int k = 0; k < this.mbDynamicObjectsInCombinedMesh.Count; k++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[k];
					HashSet<int> hashSet = new HashSet<int>();
					for (int l = mb_DynamicGameObject.vertIdx; l < mb_DynamicGameObject.vertIdx + mb_DynamicGameObject.numVerts; l++)
					{
						if (this.boneWeights[l].weight0 > 0f)
						{
							hashSet.Add(this.boneWeights[l].boneIndex0);
						}
						if (this.boneWeights[l].weight1 > 0f)
						{
							hashSet.Add(this.boneWeights[l].boneIndex1);
						}
						if (this.boneWeights[l].weight2 > 0f)
						{
							hashSet.Add(this.boneWeights[l].boneIndex2);
						}
						if (this.boneWeights[l].weight3 > 0f)
						{
							hashSet.Add(this.boneWeights[l].boneIndex3);
						}
					}
					mb_DynamicGameObject.indexesOfBonesUsed = new int[hashSet.Count];
					hashSet.CopyTo(mb_DynamicGameObject.indexesOfBonesUsed);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Baker used old systems that duplicated bones. Upgrading to new system by building indexesOfBonesUsed");
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log(string.Format("_initialize numObjsInCombined={0}", this.mbDynamicObjectsInCombinedMesh.Count));
			}
			return true;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000B18C8 File Offset: 0x000AFAC8
		private bool _collectMaterialTriangles(Mesh m, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Material[] sharedMaterials, OrderedDictionary sourceMats2submeshIdx_map)
		{
			int num = m.subMeshCount;
			if (sharedMaterials.Length < num)
			{
				num = sharedMaterials.Length;
			}
			dgo._tmpSubmeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[num];
			dgo.targetSubmeshIdxs = new int[num];
			for (int i = 0; i < num; i++)
			{
				if (this._textureBakeResults.doMultiMaterial)
				{
					if (!sourceMats2submeshIdx_map.Contains(sharedMaterials[i]))
					{
						string str = "Object ";
						string name = dgo.name;
						string str2 = " has a material that was not found in the result materials maping. ";
						Material material = sharedMaterials[i];
						Debug.LogError(str + name + str2 + ((material != null) ? material.ToString() : null));
						return false;
					}
					dgo.targetSubmeshIdxs[i] = (int)sourceMats2submeshIdx_map[sharedMaterials[i]];
				}
				else
				{
					dgo.targetSubmeshIdxs[i] = 0;
				}
				dgo._tmpSubmeshTris[i] = new MB3_MeshCombinerSingle.SerializableIntArray();
				dgo._tmpSubmeshTris[i].data = m.GetTriangles(i);
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new string[]
					{
						"Collecting triangles for: ",
						dgo.name,
						" submesh:",
						i.ToString(),
						" maps to submesh:",
						dgo.targetSubmeshIdxs[i].ToString(),
						" added:",
						dgo._tmpSubmeshTris[i].data.Length.ToString()
					}), new object[]
					{
						this.LOG_LEVEL
					});
				}
			}
			return true;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000B1A2C File Offset: 0x000AFC2C
		private bool _collectOutOfBoundsUVRects2(Mesh m, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Material[] sharedMaterials, OrderedDictionary sourceMats2submeshIdx_map, Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisResults, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
		{
			if (this._textureBakeResults == null)
			{
				Debug.LogError("Need to bake textures into combined material");
				return false;
			}
			MB_Utility.MeshAnalysisResult[] array;
			if (meshAnalysisResults.TryGetValue(m.GetInstanceID(), out array))
			{
				dgo.obUVRects = new Rect[sharedMaterials.Length];
				for (int i = 0; i < dgo.obUVRects.Length; i++)
				{
					dgo.obUVRects[i] = array[i].uvRect;
				}
			}
			else
			{
				int subMeshCount = m.subMeshCount;
				int num = subMeshCount;
				if (sharedMaterials.Length < subMeshCount)
				{
					num = sharedMaterials.Length;
				}
				dgo.obUVRects = new Rect[num];
				array = new MB_Utility.MeshAnalysisResult[subMeshCount];
				for (int j = 0; j < subMeshCount; j++)
				{
					int num2 = dgo.targetSubmeshIdxs[j];
					if (this._textureBakeResults.resultMaterials[num2].considerMeshUVs)
					{
						MB_Utility.hasOutOfBoundsUVs(meshChannelCache.GetUv0Raw(m), m, ref array[j], j);
						Rect uvRect = array[j].uvRect;
						if (j < num)
						{
							dgo.obUVRects[j] = uvRect;
						}
					}
				}
				meshAnalysisResults.Add(m.GetInstanceID(), array);
			}
			return true;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000B1B44 File Offset: 0x000AFD44
		private bool _validateTextureBakeResults()
		{
			if (this._textureBakeResults == null)
			{
				Debug.LogError("Texture Bake Results is null. Can't combine meshes.");
				return false;
			}
			if (this._textureBakeResults.materialsAndUVRects == null || this._textureBakeResults.materialsAndUVRects.Length == 0)
			{
				Debug.LogError("Texture Bake Results has no materials in material to sourceUVRect map. Try baking materials. Can't combine meshes.");
				return false;
			}
			if (this._textureBakeResults.resultMaterials == null || this._textureBakeResults.resultMaterials.Length == 0)
			{
				Debug.LogError("Texture Bake Results has no result materials. Try baking materials. Can't combine meshes.");
				return false;
			}
			return true;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000B1BBC File Offset: 0x000AFDBC
		private bool _validateMeshFlags()
		{
			if (this.mbDynamicObjectsInCombinedMesh.Count > 0 && ((!this._doNorm && this.doNorm) || (!this._doTan && this.doTan) || (!this._doCol && this.doCol) || (!this._doUV && this.doUV) || (!this._doUV3 && this.doUV3) || (!this._doUV4 && this.doUV4)))
			{
				Debug.LogError("The channels have changed. There are already objects in the combined mesh that were added with a different set of channels.");
				return false;
			}
			this._doNorm = this.doNorm;
			this._doTan = this.doTan;
			this._doCol = this.doCol;
			this._doUV = this.doUV;
			this._doUV3 = this.doUV3;
			this._doUV4 = this.doUV4;
			return true;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000B1C8C File Offset: 0x000AFE8C
		private bool _showHide(GameObject[] goToShow, GameObject[] goToHide)
		{
			if (goToShow == null)
			{
				goToShow = this.empty;
			}
			if (goToHide == null)
			{
				goToHide = this.empty;
			}
			int numResultMats = this._textureBakeResults.resultMaterials.Length;
			if (!this._Initialize(numResultMats))
			{
				return false;
			}
			for (int i = 0; i < goToHide.Length; i++)
			{
				if (!this.instance2Combined_MapContainsKey(goToHide[i]))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						string str = "Trying to hide an object ";
						GameObject gameObject = goToHide[i];
						Debug.LogWarning(str + ((gameObject != null) ? gameObject.ToString() : null) + " that is not in combined mesh. Did you initially bake with 'clear buffers after bake' enabled?");
					}
					return false;
				}
			}
			for (int j = 0; j < goToShow.Length; j++)
			{
				if (!this.instance2Combined_MapContainsKey(goToShow[j]))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						string str2 = "Trying to show an object ";
						GameObject gameObject2 = goToShow[j];
						Debug.LogWarning(str2 + ((gameObject2 != null) ? gameObject2.ToString() : null) + " that is not in combined mesh. Did you initially bake with 'clear buffers after bake' enabled?");
					}
					return false;
				}
			}
			for (int k = 0; k < goToHide.Length; k++)
			{
				this._instance2combined_map[goToHide[k]].show = false;
			}
			for (int l = 0; l < goToShow.Length; l++)
			{
				this._instance2combined_map[goToShow[l]].show = true;
			}
			return true;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000B1DA0 File Offset: 0x000AFFA0
		private bool _addToCombined(GameObject[] goToAdd, int[] goToDelete, bool disableRendererInSource)
		{
			Stopwatch stopwatch = null;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
			}
			if (!this._validateTextureBakeResults())
			{
				return false;
			}
			if (!this._validateMeshFlags())
			{
				return false;
			}
			if (!this.ValidateTargRendererAndMeshAndResultSceneObj())
			{
				return false;
			}
			if (this.outputOption != MB2_OutputOptions.bakeMeshAssetsInPlace && this.renderType == MB_RenderType.skinnedMeshRenderer && (this._targetRenderer == null || !(this._targetRenderer is SkinnedMeshRenderer)))
			{
				Debug.LogError("Target renderer must be set and must be a SkinnedMeshRenderer");
				return false;
			}
			if (this._doBlendShapes && this.renderType != MB_RenderType.skinnedMeshRenderer)
			{
				Debug.LogError("If doBlendShapes is set then RenderType must be skinnedMeshRenderer.");
				return false;
			}
			GameObject[] _goToAdd;
			if (goToAdd == null)
			{
				_goToAdd = this.empty;
			}
			else
			{
				_goToAdd = (GameObject[])goToAdd.Clone();
			}
			int[] array;
			if (goToDelete == null)
			{
				array = this.emptyIDs;
			}
			else
			{
				array = (int[])goToDelete.Clone();
			}
			if (this._mesh == null)
			{
				this.DestroyMesh();
			}
			MB2_TextureBakeResults.Material2AtlasRectangleMapper material2AtlasRectangleMapper = new MB2_TextureBakeResults.Material2AtlasRectangleMapper(this.textureBakeResults);
			int num = this._textureBakeResults.resultMaterials.Length;
			if (!this._Initialize(num))
			{
				return false;
			}
			if (this.submeshTris.Length != num)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"The number of submeshes ",
					this.submeshTris.Length.ToString(),
					" in the combined mesh was not equal to the number of result materials ",
					num.ToString(),
					" in the Texture Bake Result"
				}));
				return false;
			}
			if (this._mesh.vertexCount > 0 && this._instance2combined_map.Count == 0)
			{
				Debug.LogWarning("There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug(string.Concat(new string[]
				{
					"==== Calling _addToCombined objs adding:",
					_goToAdd.Length.ToString(),
					" objs deleting:",
					array.Length.ToString(),
					" fixOutOfBounds:",
					this.textureBakeResults.DoAnyResultMatsUseConsiderMeshUVs().ToString(),
					" doMultiMaterial:",
					this.textureBakeResults.doMultiMaterial.ToString(),
					" disableRenderersInSource:",
					disableRendererInSource.ToString()
				}), new object[]
				{
					this.LOG_LEVEL
				});
			}
			if (this._textureBakeResults.resultMaterials == null || this._textureBakeResults.resultMaterials.Length == 0)
			{
				Debug.LogError("No resultMaterials in this TextureBakeResults. Try baking textures.");
				return false;
			}
			OrderedDictionary orderedDictionary = this.BuildSourceMatsToSubmeshIdxMap(num);
			if (orderedDictionary == null)
			{
				return false;
			}
			int num2 = 0;
			int[] array2 = new int[num];
			int num3 = 0;
			List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] array3 = null;
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> hashSet2 = new HashSet<MB3_MeshCombinerSingle.BoneAndBindpose>();
			if (this.renderType == MB_RenderType.skinnedMeshRenderer && array.Length != 0)
			{
				array3 = this._buildBoneIdx2dgoMap();
			}
			for (int i3 = 0; i3 < array.Length; i3++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = null;
				this.InstanceID2DGO(array[i3], out mb_DynamicGameObject);
				if (mb_DynamicGameObject != null)
				{
					num2 += mb_DynamicGameObject.numVerts;
					num3 += mb_DynamicGameObject.numBlendShapes;
					if (this.renderType == MB_RenderType.skinnedMeshRenderer)
					{
						for (int j = 0; j < mb_DynamicGameObject.indexesOfBonesUsed.Length; j++)
						{
							if (array3[mb_DynamicGameObject.indexesOfBonesUsed[j]].Contains(mb_DynamicGameObject))
							{
								array3[mb_DynamicGameObject.indexesOfBonesUsed[j]].Remove(mb_DynamicGameObject);
								if (array3[mb_DynamicGameObject.indexesOfBonesUsed[j]].Count == 0)
								{
									hashSet.Add(mb_DynamicGameObject.indexesOfBonesUsed[j]);
								}
							}
						}
					}
					for (int k = 0; k < mb_DynamicGameObject.submeshNumTris.Length; k++)
					{
						array2[k] += mb_DynamicGameObject.submeshNumTris[k];
					}
				}
				else if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Trying to delete an object that is not in combined mesh");
				}
			}
			List<MB3_MeshCombinerSingle.MB_DynamicGameObject> list = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();
			Dictionary<int, MB_Utility.MeshAnalysisResult[]> dictionary = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
			MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache = new MB3_MeshCombinerSingle.MeshChannelsCache(this);
			int num4 = 0;
			int[] array4 = new int[num];
			int num5 = 0;
			Dictionary<Transform, int> dictionary2 = new Dictionary<Transform, int>();
			for (int l = 0; l < this.bones.Length; l++)
			{
				dictionary2.Add(this.bones[l], l);
			}
			int i = 0;
			Predicate<int> <>9__0;
			while (i < _goToAdd.Length)
			{
				if (!this.instance2Combined_MapContainsKey(_goToAdd[i]))
				{
					goto IL_43D;
				}
				int[] array5 = array;
				Predicate<int> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((int o) => o == _goToAdd[i].GetInstanceID()));
				}
				if (Array.FindIndex<int>(array5, match) != -1)
				{
					goto IL_43D;
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Object " + _goToAdd[i].name + " has already been added");
				}
				_goToAdd[i] = null;
				IL_8F5:
				int i2 = i;
				i = i2 + 1;
				continue;
				IL_43D:
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject2 = new MB3_MeshCombinerSingle.MB_DynamicGameObject();
				GameObject gameObject = _goToAdd[i];
				Material[] gomaterials = MB_Utility.GetGOMaterials(gameObject);
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Getting {0} shared materials for {1}", gomaterials.Length, gameObject));
				}
				if (gomaterials == null)
				{
					Debug.LogError("Object " + gameObject.name + " does not have a Renderer");
					_goToAdd[i] = null;
					return false;
				}
				Mesh mesh = MB_Utility.GetMesh(gameObject);
				if (mesh == null)
				{
					Debug.LogError("Object " + gameObject.name + " MeshFilter or SkinedMeshRenderer had no mesh");
					_goToAdd[i] = null;
					return false;
				}
				if (MBVersion.IsRunningAndMeshNotReadWriteable(mesh))
				{
					Debug.LogError("Object " + gameObject.name + " Mesh Importer has read/write flag set to 'false'. This needs to be set to 'true' in order to read data from this mesh.");
					_goToAdd[i] = null;
					return false;
				}
				MB_TextureTilingTreatment[] array6 = new MB_TextureTilingTreatment[gomaterials.Length];
				Rect[] array7 = new Rect[gomaterials.Length];
				Rect[] array8 = new Rect[gomaterials.Length];
				Rect[] array9 = new Rect[gomaterials.Length];
				string message = "";
				for (int m = 0; m < gomaterials.Length; m++)
				{
					object obj = orderedDictionary[gomaterials[m]];
					if (obj == null)
					{
						string[] array10 = new string[5];
						array10[0] = "Source object ";
						array10[1] = gameObject.name;
						array10[2] = " used a material ";
						int num6 = 3;
						Material material = gomaterials[m];
						array10[num6] = ((material != null) ? material.ToString() : null);
						array10[4] = " that was not in the baked materials.";
						Debug.LogError(string.Concat(array10));
						return false;
					}
					int idxInResultMats = (int)obj;
					if (!material2AtlasRectangleMapper.TryMapMaterialToUVRect(gomaterials[m], mesh, m, idxInResultMats, meshChannelsCache, dictionary, out array6[m], out array7[m], out array8[m], out array9[m], ref message, this.LOG_LEVEL))
					{
						Debug.LogError(message);
						_goToAdd[i] = null;
						return false;
					}
				}
				if (!(_goToAdd[i] != null))
				{
					goto IL_8F5;
				}
				list.Add(mb_DynamicGameObject2);
				mb_DynamicGameObject2.name = string.Format("{0} {1}", _goToAdd[i].ToString(), _goToAdd[i].GetInstanceID());
				mb_DynamicGameObject2.instanceID = _goToAdd[i].GetInstanceID();
				mb_DynamicGameObject2.gameObject = _goToAdd[i];
				mb_DynamicGameObject2.uvRects = array7;
				mb_DynamicGameObject2.encapsulatingRect = array8;
				mb_DynamicGameObject2.sourceMaterialTiling = array9;
				mb_DynamicGameObject2.numVerts = mesh.vertexCount;
				if (this._doBlendShapes)
				{
					mb_DynamicGameObject2.numBlendShapes = mesh.blendShapeCount;
				}
				Renderer renderer = MB_Utility.GetRenderer(gameObject);
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					this._CollectBonesToAddForDGO(mb_DynamicGameObject2, dictionary2, hashSet, hashSet2, renderer, meshChannelsCache);
				}
				if (this.lightmapIndex == -1)
				{
					this.lightmapIndex = renderer.lightmapIndex;
				}
				if (this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
				{
					if (this.lightmapIndex != renderer.lightmapIndex && this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Object " + gameObject.name + " has a different lightmap index. Lightmapping will not work.");
					}
					if (!MBVersion.GetActive(gameObject) && this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Object " + gameObject.name + " is inactive. Can only get lightmap index of active objects.");
					}
					if (renderer.lightmapIndex == -1 && this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Object " + gameObject.name + " does not have an index to a lightmap.");
					}
				}
				mb_DynamicGameObject2.lightmapIndex = renderer.lightmapIndex;
				mb_DynamicGameObject2.lightmapTilingOffset = MBVersion.GetLightmapTilingOffset(renderer);
				if (!this._collectMaterialTriangles(mesh, mb_DynamicGameObject2, gomaterials, orderedDictionary))
				{
					return false;
				}
				mb_DynamicGameObject2.meshSize = renderer.bounds.size;
				mb_DynamicGameObject2.submeshNumTris = new int[num];
				mb_DynamicGameObject2.submeshTriIdxs = new int[num];
				if (this.textureBakeResults.DoAnyResultMatsUseConsiderMeshUVs() && !this._collectOutOfBoundsUVRects2(mesh, mb_DynamicGameObject2, gomaterials, orderedDictionary, dictionary, meshChannelsCache))
				{
					return false;
				}
				num4 += mb_DynamicGameObject2.numVerts;
				num5 += mb_DynamicGameObject2.numBlendShapes;
				for (int n = 0; n < mb_DynamicGameObject2._tmpSubmeshTris.Length; n++)
				{
					array4[mb_DynamicGameObject2.targetSubmeshIdxs[n]] += mb_DynamicGameObject2._tmpSubmeshTris[n].data.Length;
				}
				mb_DynamicGameObject2.invertTriangles = this.IsMirrored(gameObject.transform.localToWorldMatrix);
				goto IL_8F5;
			}
			for (int num7 = 0; num7 < _goToAdd.Length; num7++)
			{
				if (_goToAdd[num7] != null && disableRendererInSource)
				{
					MB_Utility.DisableRendererInSource(_goToAdd[num7]);
					if (this.LOG_LEVEL == MB2_LogLevel.trace)
					{
						Debug.Log("Disabling renderer on " + _goToAdd[num7].name + " id=" + _goToAdd[num7].GetInstanceID().ToString());
					}
				}
			}
			int num8 = this.verts.Length + num4 - num2;
			int num9 = this.bindPoses.Length + hashSet2.Count - hashSet.Count;
			int[] array11 = new int[num];
			int num10 = this.blendShapes.Length + num5 - num3;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Verts adding:",
					num4.ToString(),
					" deleting:",
					num2.ToString(),
					" submeshes:",
					array11.Length.ToString(),
					" bones:",
					num9.ToString(),
					" blendShapes:",
					num10.ToString()
				}));
			}
			for (int num11 = 0; num11 < array11.Length; num11++)
			{
				array11[num11] = this.submeshTris[num11].data.Length + array4[num11] - array2[num11];
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new string[]
					{
						"    submesh :",
						num11.ToString(),
						" already contains:",
						this.submeshTris[num11].data.Length.ToString(),
						" tris to be Added:",
						array4[num11].ToString(),
						" tris to be Deleted:",
						array2[num11].ToString()
					}), Array.Empty<object>());
				}
			}
			if (num8 >= MBVersion.MaxMeshVertexCount())
			{
				Debug.LogError("Cannot add objects. Resulting mesh will have more than " + MBVersion.MaxMeshVertexCount().ToString() + " vertices. Try using a Multi-MeshBaker component. This will split the combined mesh into several meshes. You don't have to re-configure the MB2_TextureBaker. Just remove the MB2_MeshBaker component and add a MB2_MultiMeshBaker component.");
				return false;
			}
			Vector3[] destinationArray = null;
			Vector4[] destinationArray2 = null;
			Vector2[] destinationArray3 = null;
			Vector2[] destinationArray4 = null;
			Vector2[] array12 = null;
			Vector2[] array13 = null;
			Color[] array14 = null;
			MB3_MeshCombinerSingle.MBBlendShape[] array15 = null;
			Vector3[] array16 = new Vector3[num8];
			if (this._doNorm)
			{
				destinationArray = new Vector3[num8];
			}
			if (this._doTan)
			{
				destinationArray2 = new Vector4[num8];
			}
			if (this._doUV)
			{
				destinationArray3 = new Vector2[num8];
			}
			if (this._doUV3)
			{
				array12 = new Vector2[num8];
			}
			if (this._doUV4)
			{
				array13 = new Vector2[num8];
			}
			if (this.doUV2())
			{
				destinationArray4 = new Vector2[num8];
			}
			if (this._doCol)
			{
				array14 = new Color[num8];
			}
			if (this._doBlendShapes)
			{
				array15 = new MB3_MeshCombinerSingle.MBBlendShape[num10];
			}
			BoneWeight[] array17 = new BoneWeight[num8];
			Matrix4x4[] array18 = new Matrix4x4[num9];
			Transform[] array19 = new Transform[num9];
			MB3_MeshCombinerSingle.SerializableIntArray[] array20 = new MB3_MeshCombinerSingle.SerializableIntArray[num];
			for (int num12 = 0; num12 < array20.Length; num12++)
			{
				array20[num12] = new MB3_MeshCombinerSingle.SerializableIntArray(array11[num12]);
			}
			for (int num13 = 0; num13 < array.Length; num13++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject3 = null;
				this.InstanceID2DGO(array[num13], out mb_DynamicGameObject3);
				if (mb_DynamicGameObject3 != null)
				{
					mb_DynamicGameObject3._beingDeleted = true;
				}
			}
			this.mbDynamicObjectsInCombinedMesh.Sort();
			int num14 = 0;
			int num15 = 0;
			int[] array21 = new int[num];
			int num16 = 0;
			for (int num17 = 0; num17 < this.mbDynamicObjectsInCombinedMesh.Count; num17++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject4 = this.mbDynamicObjectsInCombinedMesh[num17];
				if (!mb_DynamicGameObject4._beingDeleted)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Copying obj in combined arrays idx:" + num17.ToString(), new object[]
						{
							this.LOG_LEVEL
						});
					}
					Array.Copy(this.verts, mb_DynamicGameObject4.vertIdx, array16, num14, mb_DynamicGameObject4.numVerts);
					if (this._doNorm)
					{
						Array.Copy(this.normals, mb_DynamicGameObject4.vertIdx, destinationArray, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doTan)
					{
						Array.Copy(this.tangents, mb_DynamicGameObject4.vertIdx, destinationArray2, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doUV)
					{
						Array.Copy(this.uvs, mb_DynamicGameObject4.vertIdx, destinationArray3, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doUV3)
					{
						Array.Copy(this.uv3s, mb_DynamicGameObject4.vertIdx, array12, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doUV4)
					{
						Array.Copy(this.uv4s, mb_DynamicGameObject4.vertIdx, array13, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this.doUV2())
					{
						Array.Copy(this.uv2s, mb_DynamicGameObject4.vertIdx, destinationArray4, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doCol)
					{
						Array.Copy(this.colors, mb_DynamicGameObject4.vertIdx, array14, num14, mb_DynamicGameObject4.numVerts);
					}
					if (this._doBlendShapes)
					{
						Array.Copy(this.blendShapes, mb_DynamicGameObject4.blendShapeIdx, array15, num15, mb_DynamicGameObject4.numBlendShapes);
					}
					if (this.renderType == MB_RenderType.skinnedMeshRenderer)
					{
						Array.Copy(this.boneWeights, mb_DynamicGameObject4.vertIdx, array17, num14, mb_DynamicGameObject4.numVerts);
					}
					for (int num18 = 0; num18 < num; num18++)
					{
						int[] data = this.submeshTris[num18].data;
						int num19 = mb_DynamicGameObject4.submeshTriIdxs[num18];
						int num20 = mb_DynamicGameObject4.submeshNumTris[num18];
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							MB2_Log.LogDebug(string.Concat(new string[]
							{
								"    Adjusting submesh triangles submesh:",
								num18.ToString(),
								" startIdx:",
								num19.ToString(),
								" num:",
								num20.ToString(),
								" nsubmeshTris:",
								array20.Length.ToString(),
								" targSubmeshTidx:",
								array21.Length.ToString()
							}), new object[]
							{
								this.LOG_LEVEL
							});
						}
						for (int num21 = num19; num21 < num19 + num20; num21++)
						{
							data[num21] -= num16;
						}
						Array.Copy(data, num19, array20[num18].data, array21[num18], num20);
					}
					mb_DynamicGameObject4.vertIdx = num14;
					mb_DynamicGameObject4.blendShapeIdx = num15;
					for (int num22 = 0; num22 < array21.Length; num22++)
					{
						mb_DynamicGameObject4.submeshTriIdxs[num22] = array21[num22];
						array21[num22] += mb_DynamicGameObject4.submeshNumTris[num22];
					}
					num15 += mb_DynamicGameObject4.numBlendShapes;
					num14 += mb_DynamicGameObject4.numVerts;
				}
				else
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Not copying obj: " + num17.ToString(), new object[]
						{
							this.LOG_LEVEL
						});
					}
					num16 += mb_DynamicGameObject4.numVerts;
				}
			}
			if (this.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				this._CopyBonesWeAreKeepingToNewBonesArrayAndAdjustBWIndexes(hashSet, hashSet2, array19, array18, array17, num2);
			}
			for (int num23 = this.mbDynamicObjectsInCombinedMesh.Count - 1; num23 >= 0; num23--)
			{
				if (this.mbDynamicObjectsInCombinedMesh[num23]._beingDeleted)
				{
					this.instance2Combined_MapRemove(this.mbDynamicObjectsInCombinedMesh[num23].gameObject);
					this.objectsInCombinedMesh.RemoveAt(num23);
					this.mbDynamicObjectsInCombinedMesh.RemoveAt(num23);
				}
			}
			this.verts = array16;
			if (this._doNorm)
			{
				this.normals = destinationArray;
			}
			if (this._doTan)
			{
				this.tangents = destinationArray2;
			}
			if (this._doUV)
			{
				this.uvs = destinationArray3;
			}
			if (this._doUV3)
			{
				this.uv3s = array12;
			}
			if (this._doUV4)
			{
				this.uv4s = array13;
			}
			if (this.doUV2())
			{
				this.uv2s = destinationArray4;
			}
			if (this._doCol)
			{
				this.colors = array14;
			}
			if (this._doBlendShapes)
			{
				this.blendShapes = array15;
			}
			if (this.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				this.boneWeights = array17;
			}
			int num24 = this.bones.Length - hashSet.Count;
			this.bindPoses = array18;
			this.bones = array19;
			this.submeshTris = array20;
			int num25 = 0;
			foreach (MB3_MeshCombinerSingle.BoneAndBindpose boneAndBindpose in hashSet2)
			{
				array19[num24 + num25] = boneAndBindpose.bone;
				array18[num24 + num25] = boneAndBindpose.bindPose;
				num25++;
			}
			for (int num26 = 0; num26 < list.Count; num26++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject5 = list[num26];
				GameObject gameObject2 = _goToAdd[num26];
				int num27 = num14;
				int index = num15;
				Mesh mesh2 = MB_Utility.GetMesh(gameObject2);
				Matrix4x4 localToWorldMatrix = gameObject2.transform.localToWorldMatrix;
				Matrix4x4 matrix4x = localToWorldMatrix;
				matrix4x[0, 3] = (matrix4x[1, 3] = (matrix4x[2, 3] = 0f));
				array16 = meshChannelsCache.GetVertices(mesh2);
				Vector3[] array22 = null;
				Vector4[] array23 = null;
				if (this._doNorm)
				{
					array22 = meshChannelsCache.GetNormals(mesh2);
				}
				if (this._doTan)
				{
					array23 = meshChannelsCache.GetTangents(mesh2);
				}
				if (this.renderType != MB_RenderType.skinnedMeshRenderer)
				{
					for (int num28 = 0; num28 < array16.Length; num28++)
					{
						int num29 = num27 + num28;
						this.verts[num27 + num28] = localToWorldMatrix.MultiplyPoint3x4(array16[num28]);
						if (this._doNorm)
						{
							this.normals[num29] = matrix4x.MultiplyPoint3x4(array22[num28]);
							this.normals[num29] = this.normals[num29].normalized;
						}
						if (this._doTan)
						{
							float w = array23[num28].w;
							Vector3 v = matrix4x.MultiplyPoint3x4(array23[num28]);
							v.Normalize();
							this.tangents[num29] = v;
							this.tangents[num29].w = w;
						}
					}
				}
				else
				{
					if (this._doNorm)
					{
						array22.CopyTo(this.normals, num27);
					}
					if (this._doTan)
					{
						array23.CopyTo(this.tangents, num27);
					}
					array16.CopyTo(this.verts, num27);
				}
				int num30 = mesh2.subMeshCount;
				if (mb_DynamicGameObject5.uvRects.Length < num30)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + mb_DynamicGameObject5.name + " has more submeshes than materials", Array.Empty<object>());
					}
					num30 = mb_DynamicGameObject5.uvRects.Length;
				}
				else if (mb_DynamicGameObject5.uvRects.Length > num30 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + mb_DynamicGameObject5.name + " has fewer submeshes than materials");
				}
				if (this._doUV)
				{
					this._copyAndAdjustUVsFromMesh(mb_DynamicGameObject5, mesh2, num27, meshChannelsCache);
				}
				if (this.doUV2())
				{
					this._copyAndAdjustUV2FromMesh(mb_DynamicGameObject5, mesh2, num27, meshChannelsCache);
				}
				if (this._doUV3)
				{
					array12 = meshChannelsCache.GetUv3(mesh2);
					array12.CopyTo(this.uv3s, num27);
				}
				if (this._doUV4)
				{
					array13 = meshChannelsCache.GetUv4(mesh2);
					array13.CopyTo(this.uv4s, num27);
				}
				if (this._doCol)
				{
					array14 = meshChannelsCache.GetColors(mesh2);
					array14.CopyTo(this.colors, num27);
				}
				if (this._doBlendShapes)
				{
					array15 = meshChannelsCache.GetBlendShapes(mesh2, mb_DynamicGameObject5.instanceID);
					array15.CopyTo(this.blendShapes, index);
				}
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					Renderer renderer2 = MB_Utility.GetRenderer(gameObject2);
					this._AddBonesToNewBonesArrayAndAdjustBWIndexes(mb_DynamicGameObject5, renderer2, num27, array19, array17, meshChannelsCache);
				}
				for (int num31 = 0; num31 < array21.Length; num31++)
				{
					mb_DynamicGameObject5.submeshTriIdxs[num31] = array21[num31];
				}
				for (int num32 = 0; num32 < mb_DynamicGameObject5._tmpSubmeshTris.Length; num32++)
				{
					int[] data2 = mb_DynamicGameObject5._tmpSubmeshTris[num32].data;
					for (int num33 = 0; num33 < data2.Length; num33++)
					{
						data2[num33] += num27;
					}
					if (mb_DynamicGameObject5.invertTriangles)
					{
						for (int num34 = 0; num34 < data2.Length; num34 += 3)
						{
							int num35 = data2[num34];
							data2[num34] = data2[num34 + 1];
							data2[num34 + 1] = num35;
						}
					}
					int num36 = mb_DynamicGameObject5.targetSubmeshIdxs[num32];
					data2.CopyTo(this.submeshTris[num36].data, array21[num36]);
					mb_DynamicGameObject5.submeshNumTris[num36] += data2.Length;
					array21[num36] += data2.Length;
				}
				mb_DynamicGameObject5.vertIdx = num14;
				mb_DynamicGameObject5.blendShapeIdx = num15;
				this.instance2Combined_MapAdd(gameObject2, mb_DynamicGameObject5);
				this.objectsInCombinedMesh.Add(gameObject2);
				this.mbDynamicObjectsInCombinedMesh.Add(mb_DynamicGameObject5);
				num14 += array16.Length;
				if (this._doBlendShapes)
				{
					num15 += array15.Length;
				}
				for (int num37 = 0; num37 < mb_DynamicGameObject5._tmpSubmeshTris.Length; num37++)
				{
					mb_DynamicGameObject5._tmpSubmeshTris[num37] = null;
				}
				mb_DynamicGameObject5._tmpSubmeshTris = null;
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new string[]
					{
						"Added to combined:",
						mb_DynamicGameObject5.name,
						" verts:",
						array16.Length.ToString(),
						" bindPoses:",
						array18.Length.ToString()
					}), new object[]
					{
						this.LOG_LEVEL
					});
				}
			}
			if (this.lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects)
			{
				this._copyUV2unchangedToSeparateRects();
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug("===== _addToCombined completed. Verts in buffer: " + this.verts.Length.ToString() + " time(ms): " + stopwatch.ElapsedMilliseconds.ToString(), new object[]
				{
					this.LOG_LEVEL
				});
			}
			return true;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000B34E8 File Offset: 0x000B16E8
		private void _copyAndAdjustUVsFromMesh(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Mesh mesh, int vertsIdx, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache)
		{
			Vector2[] uv0Raw = meshChannelsCache.GetUv0Raw(mesh);
			int[] array = new int[uv0Raw.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			bool flag = false;
			for (int j = 0; j < dgo.targetSubmeshIdxs.Length; j++)
			{
				int[] array2;
				if (dgo._tmpSubmeshTris != null)
				{
					array2 = dgo._tmpSubmeshTris[j].data;
				}
				else
				{
					array2 = mesh.GetTriangles(j);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Build UV transform for mesh {0} submesh {1} encapsulatingRect {2}", dgo.name, j, dgo.encapsulatingRect[j]));
				}
				Rect rect = MB3_TextureCombinerMerging.BuildTransformMeshUV2AtlasRect(this.textureBakeResults.resultMaterials[dgo.targetSubmeshIdxs[j]].considerMeshUVs, dgo.uvRects[j], (dgo.obUVRects == null) ? new Rect(0f, 0f, 1f, 1f) : dgo.obUVRects[j], dgo.sourceMaterialTiling[j], dgo.encapsulatingRect[j]);
				foreach (int num in array2)
				{
					if (array[num] == -1)
					{
						array[num] = j;
						Vector2 vector = uv0Raw[num];
						vector.x = rect.x + vector.x * rect.width;
						vector.y = rect.y + vector.y * rect.height;
						this.uvs[vertsIdx + num] = vector;
					}
					if (array[num] != j)
					{
						flag = true;
					}
				}
			}
			if (flag && this.LOG_LEVEL >= MB2_LogLevel.warn)
			{
				Debug.LogWarning(dgo.name + "has submeshes which share verticies. Adjusted uvs may not map correctly in combined atlas.");
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log(string.Format("_copyAndAdjustUVsFromMesh copied {0} verts", uv0Raw.Length));
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000B36D8 File Offset: 0x000B18D8
		private void _copyAndAdjustUV2FromMesh(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Mesh mesh, int vertsIdx, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelsCache)
		{
			Vector2[] uv = meshChannelsCache.GetUv2(mesh);
			if (this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
			{
				Vector4 lightmapTilingOffset = dgo.lightmapTilingOffset;
				Vector2 vector = new Vector2(lightmapTilingOffset.x, lightmapTilingOffset.y);
				Vector2 a = new Vector2(lightmapTilingOffset.z, lightmapTilingOffset.w);
				for (int i = 0; i < uv.Length; i++)
				{
					Vector2 b;
					b.x = vector.x * uv[i].x;
					b.y = vector.y * uv[i].y;
					this.uv2s[vertsIdx + i] = a + b;
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("_copyAndAdjustUV2FromMesh copied and modify for preserve current lightmapping " + uv.Length.ToString());
					return;
				}
			}
			else
			{
				uv.CopyTo(this.uv2s, vertsIdx);
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("_copyAndAdjustUV2FromMesh copied without modifying " + uv.Length.ToString());
				}
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00014DF9 File Offset: 0x00012FF9
		public override void UpdateSkinnedMeshApproximateBounds()
		{
			this.UpdateSkinnedMeshApproximateBoundsFromBounds();
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000B37DC File Offset: 0x000B19DC
		public override void UpdateSkinnedMeshApproximateBoundsFromBones()
		{
			if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
				}
				return;
			}
			if (this.bones.Length == 0)
			{
				if (this.verts.Length != 0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("No bones in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			if (this._targetRenderer == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			if (!this._targetRenderer.GetType().Equals(typeof(SkinnedMeshRenderer)))
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, (SkinnedMeshRenderer)this.targetRenderer);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000B3894 File Offset: 0x000B1A94
		public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
		{
			if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBoundsFromBounds when output type is bakeMeshAssetsInPlace");
				}
				return;
			}
			if (this.verts.Length == 0 || this.mbDynamicObjectsInCombinedMesh.Count == 0)
			{
				if (this.verts.Length != 0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Nothing in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			if (this._targetRenderer == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			if (!this._targetRenderer.GetType().Equals(typeof(SkinnedMeshRenderer)))
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objectsInCombinedMesh, (SkinnedMeshRenderer)this.targetRenderer);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00014E01 File Offset: 0x00013001
		private int _getNumBones(Renderer r)
		{
			if (this.renderType != MB_RenderType.skinnedMeshRenderer)
			{
				return 0;
			}
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).bones.Length;
			}
			if (r is MeshRenderer)
			{
				return 1;
			}
			Debug.LogError("Could not _getNumBones. Object does not have a renderer");
			return 0;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00014E39 File Offset: 0x00013039
		private Transform[] _getBones(Renderer r)
		{
			return MBVersion.GetBones(r);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000B395C File Offset: 0x000B1B5C
		public override void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
		{
			bool flag = false;
			if (this.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				flag = true;
			}
			this.Apply(true, true, this._doNorm, this._doTan, this._doUV, this.doUV2(), this._doUV3, this._doUV4, this.doCol, flag, this.doBlendShapes, uv2GenerationMethod);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000B39B0 File Offset: 0x000B1BB0
		public virtual void ApplyShowHide()
		{
			if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
			{
				return;
			}
			if (this._mesh != null)
			{
				if (this.renderType == MB_RenderType.meshRenderer)
				{
					MBVersion.MeshClear(this._mesh, true);
					this._mesh.vertices = this.verts;
				}
				MB3_MeshCombinerSingle.SerializableIntArray[] submeshTrisWithShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
				if (this.textureBakeResults.doMultiMaterial)
				{
					int numNonZeroLengthSubmeshTris = this._mesh.subMeshCount = this._numNonZeroLengthSubmeshTris(submeshTrisWithShowHideApplied);
					int num = 0;
					for (int i = 0; i < submeshTrisWithShowHideApplied.Length; i++)
					{
						if (submeshTrisWithShowHideApplied[i].data.Length != 0)
						{
							this._mesh.SetTriangles(submeshTrisWithShowHideApplied[i].data, num);
							num++;
						}
					}
					this._updateMaterialsOnTargetRenderer(submeshTrisWithShowHideApplied, numNonZeroLengthSubmeshTris);
				}
				else
				{
					this._mesh.triangles = submeshTrisWithShowHideApplied[0].data;
				}
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					if (this.verts.Length == 0)
					{
						this.targetRenderer.enabled = false;
					}
					else
					{
						this.targetRenderer.enabled = true;
					}
					bool updateWhenOffscreen = ((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = true;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = updateWhenOffscreen;
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("ApplyShowHide");
					return;
				}
			}
			else
			{
				Debug.LogError("Need to add objects to this meshbaker before calling ApplyShowHide");
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000B3B04 File Offset: 0x000B1D04
		public override void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool uv2, bool uv3, bool uv4, bool colors, bool bones = false, bool blendShapesFlag = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
		{
			Stopwatch stopwatch = null;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
			}
			if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
			{
				return;
			}
			if (this._mesh != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Apply called tri={0} vert={1} norm={2} tan={3} uv={4} col={5} uv3={6} uv4={7} uv2={8} bone={9} blendShape{10} meshID={11}", new object[]
					{
						triangles,
						vertices,
						normals,
						tangents,
						uvs,
						colors,
						uv3,
						uv4,
						uv2,
						bones,
						this.blendShapes,
						this._mesh.GetInstanceID()
					}));
				}
				if (triangles || this._mesh.vertexCount != this.verts.Length)
				{
					bool justClearTriangles = triangles && !vertices && !normals && !tangents && !uvs && !colors && !uv3 && !uv4 && !uv2 && !bones;
					MBVersion.SetMeshIndexFormatAndClearMesh(this._mesh, this.verts.Length, vertices, justClearTriangles);
				}
				if (vertices)
				{
					Vector3[] array = this.verts;
					if (this.verts.Length != 0)
					{
						if (this._recenterVertsToBoundsCenter && this._renderType == MB_RenderType.meshRenderer)
						{
							array = new Vector3[this.verts.Length];
							Vector3 vector = this.verts[0];
							Vector3 vector2 = this.verts[0];
							for (int i = 1; i < this.verts.Length; i++)
							{
								Vector3 vector3 = this.verts[i];
								if (vector.x < vector3.x)
								{
									vector.x = vector3.x;
								}
								if (vector.y < vector3.y)
								{
									vector.y = vector3.y;
								}
								if (vector.z < vector3.z)
								{
									vector.z = vector3.z;
								}
								if (vector2.x > vector3.x)
								{
									vector2.x = vector3.x;
								}
								if (vector2.y > vector3.y)
								{
									vector2.y = vector3.y;
								}
								if (vector2.z > vector3.z)
								{
									vector2.z = vector3.z;
								}
							}
							Vector3 vector4 = (vector + vector2) / 2f;
							for (int j = 0; j < this.verts.Length; j++)
							{
								array[j] = this.verts[j] - vector4;
							}
							this.targetRenderer.transform.position = vector4;
						}
						else
						{
							this.targetRenderer.transform.position = Vector3.zero;
						}
					}
					this._mesh.vertices = array;
				}
				if (triangles && this._textureBakeResults)
				{
					if (this._textureBakeResults == null)
					{
						Debug.LogError("Texture Bake Result was not set.");
					}
					else
					{
						MB3_MeshCombinerSingle.SerializableIntArray[] submeshTrisWithShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
						int numNonZeroLengthSubmeshTris = this._mesh.subMeshCount = this._numNonZeroLengthSubmeshTris(submeshTrisWithShowHideApplied);
						int num = 0;
						for (int k = 0; k < submeshTrisWithShowHideApplied.Length; k++)
						{
							if (submeshTrisWithShowHideApplied[k].data.Length != 0)
							{
								this._mesh.SetTriangles(submeshTrisWithShowHideApplied[k].data, num);
								num++;
							}
						}
						this._updateMaterialsOnTargetRenderer(submeshTrisWithShowHideApplied, numNonZeroLengthSubmeshTris);
					}
				}
				if (normals)
				{
					if (this._doNorm)
					{
						this._mesh.normals = this.normals;
					}
					else
					{
						Debug.LogError("normal flag was set in Apply but MeshBaker didn't generate normals");
					}
				}
				if (tangents)
				{
					if (this._doTan)
					{
						this._mesh.tangents = this.tangents;
					}
					else
					{
						Debug.LogError("tangent flag was set in Apply but MeshBaker didn't generate tangents");
					}
				}
				if (uvs)
				{
					if (this._doUV)
					{
						this._mesh.uv = this.uvs;
					}
					else
					{
						Debug.LogError("uv flag was set in Apply but MeshBaker didn't generate uvs");
					}
				}
				if (colors)
				{
					if (this._doCol)
					{
						this._mesh.colors = this.colors;
					}
					else
					{
						Debug.LogError("color flag was set in Apply but MeshBaker didn't generate colors");
					}
				}
				if (uv3)
				{
					if (this._doUV3)
					{
						MBVersion.MeshAssignUV3(this._mesh, this.uv3s);
					}
					else
					{
						Debug.LogError("uv3 flag was set in Apply but MeshBaker didn't generate uv3s");
					}
				}
				if (uv4)
				{
					if (this._doUV4)
					{
						MBVersion.MeshAssignUV4(this._mesh, this.uv4s);
					}
					else
					{
						Debug.LogError("uv4 flag was set in Apply but MeshBaker didn't generate uv4s");
					}
				}
				if (uv2)
				{
					if (this.doUV2())
					{
						this._mesh.uv2 = this.uv2s;
					}
					else
					{
						Debug.LogError("uv2 flag was set in Apply but lightmapping option was set to " + this.lightmapOption.ToString());
					}
				}
				bool flag = false;
				if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
				{
					if (uv2GenerationMethod != null)
					{
						uv2GenerationMethod(this._mesh, this.uv2UnwrappingParamsHardAngle, this.uv2UnwrappingParamsPackMargin);
						if (this.LOG_LEVEL >= MB2_LogLevel.trace)
						{
							Debug.Log("generating new UV2 layout for the combined mesh ");
						}
					}
					else
					{
						Debug.LogError("No GenerateUV2Delegate method was supplied. UV2 cannot be generated.");
					}
					flag = true;
				}
				else if (this.renderType == MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("UV2 cannot be generated for SkinnedMeshRenderer objects.");
				}
				if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && !flag)
				{
					Debug.LogError("Failed to generate new UV2 layout. Only works in editor.");
				}
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					if (this.verts.Length == 0)
					{
						this.targetRenderer.enabled = false;
					}
					else
					{
						this.targetRenderer.enabled = true;
					}
					bool updateWhenOffscreen = ((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = true;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = updateWhenOffscreen;
				}
				if (bones)
				{
					this._mesh.bindposes = this.bindPoses;
					this._mesh.boneWeights = this.boneWeights;
				}
				if (blendShapesFlag && (MBVersion.GetMajorVersion() > 5 || (MBVersion.GetMajorVersion() == 5 && MBVersion.GetMinorVersion() >= 3)))
				{
					if (this.blendShapesInCombined.Length != this.blendShapes.Length)
					{
						this.blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[this.blendShapes.Length];
					}
					Vector3[] array2 = new Vector3[this.verts.Length];
					Vector3[] array3 = new Vector3[this.verts.Length];
					Vector3[] array4 = new Vector3[this.verts.Length];
					MBVersion.ClearBlendShapes(this._mesh);
					for (int l = 0; l < this.blendShapes.Length; l++)
					{
						MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.instance2Combined_MapGet(this.blendShapes[l].gameObject);
						if (mb_DynamicGameObject != null)
						{
							for (int m = 0; m < this.blendShapes[l].frames.Length; m++)
							{
								MB3_MeshCombinerSingle.MBBlendShapeFrame mbblendShapeFrame = this.blendShapes[l].frames[m];
								int vertIdx = mb_DynamicGameObject.vertIdx;
								Array.Copy(mbblendShapeFrame.vertices, 0, array2, vertIdx, this.blendShapes[l].frames[m].vertices.Length);
								Array.Copy(mbblendShapeFrame.normals, 0, array3, vertIdx, this.blendShapes[l].frames[m].normals.Length);
								Array.Copy(mbblendShapeFrame.tangents, 0, array4, vertIdx, this.blendShapes[l].frames[m].tangents.Length);
								MBVersion.AddBlendShapeFrame(this._mesh, this.blendShapes[l].name + this.blendShapes[l].gameObjectID.ToString(), mbblendShapeFrame.frameWeight, array2, array3, array4);
								this._ZeroArray(array2, vertIdx, this.blendShapes[l].frames[m].vertices.Length);
								this._ZeroArray(array3, vertIdx, this.blendShapes[l].frames[m].normals.Length);
								this._ZeroArray(array4, vertIdx, this.blendShapes[l].frames[m].tangents.Length);
							}
						}
						else
						{
							Debug.LogError("InstanceID in blend shape that was not in instance2combinedMap");
						}
						this.blendShapesInCombined[l] = this.blendShapes[l];
					}
					((SkinnedMeshRenderer)this._targetRenderer).sharedMesh = null;
					((SkinnedMeshRenderer)this._targetRenderer).sharedMesh = this._mesh;
				}
				if (triangles || vertices)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.trace)
					{
						Debug.Log("recalculating bounds on mesh.");
					}
					this._mesh.RecalculateBounds();
				}
				if (this._optimizeAfterBake && !Application.isPlaying)
				{
					MBVersion.OptimizeMesh(this._mesh);
				}
			}
			else
			{
				Debug.LogError("Need to add objects to this meshbaker before calling Apply or ApplyAll");
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Apply Complete time: " + stopwatch.ElapsedMilliseconds.ToString() + " vertices: " + this._mesh.vertexCount.ToString());
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000B43D8 File Offset: 0x000B25D8
		private int _numNonZeroLengthSubmeshTris(MB3_MeshCombinerSingle.SerializableIntArray[] subTris)
		{
			int num = 0;
			for (int i = 0; i < subTris.Length; i++)
			{
				if (subTris[i].data.Length != 0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000B4408 File Offset: 0x000B2608
		private void _updateMaterialsOnTargetRenderer(MB3_MeshCombinerSingle.SerializableIntArray[] subTris, int numNonZeroLengthSubmeshTris)
		{
			if (subTris.Length != this.textureBakeResults.resultMaterials.Length)
			{
				Debug.LogError("Mismatch between number of submeshes and number of result materials");
			}
			Material[] array = new Material[numNonZeroLengthSubmeshTris];
			int num = 0;
			for (int i = 0; i < subTris.Length; i++)
			{
				if (subTris[i].data.Length != 0)
				{
					array[num] = this._textureBakeResults.resultMaterials[i].combinedMaterial;
					num++;
				}
			}
			this.targetRenderer.materials = array;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000B4478 File Offset: 0x000B2678
		public MB3_MeshCombinerSingle.SerializableIntArray[] GetSubmeshTrisWithShowHideApplied()
		{
			bool flag = false;
			for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
			{
				if (!this.mbDynamicObjectsInCombinedMesh[i].show)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				int[] array = new int[this.submeshTris.Length];
				MB3_MeshCombinerSingle.SerializableIntArray[] array2 = new MB3_MeshCombinerSingle.SerializableIntArray[this.submeshTris.Length];
				for (int j = 0; j < this.mbDynamicObjectsInCombinedMesh.Count; j++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[j];
					if (mb_DynamicGameObject.show)
					{
						for (int k = 0; k < mb_DynamicGameObject.submeshNumTris.Length; k++)
						{
							array[k] += mb_DynamicGameObject.submeshNumTris[k];
						}
					}
				}
				for (int l = 0; l < array2.Length; l++)
				{
					array2[l] = new MB3_MeshCombinerSingle.SerializableIntArray(array[l]);
				}
				int[] array3 = new int[array2.Length];
				for (int m = 0; m < this.mbDynamicObjectsInCombinedMesh.Count; m++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject2 = this.mbDynamicObjectsInCombinedMesh[m];
					if (mb_DynamicGameObject2.show)
					{
						for (int n = 0; n < this.submeshTris.Length; n++)
						{
							int[] data = this.submeshTris[n].data;
							int num = mb_DynamicGameObject2.submeshTriIdxs[n];
							int num2 = num + mb_DynamicGameObject2.submeshNumTris[n];
							for (int num3 = num; num3 < num2; num3++)
							{
								array2[n].data[array3[n]] = data[num3];
								array3[n]++;
							}
						}
					}
				}
				return array2;
			}
			return this.submeshTris;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000B4610 File Offset: 0x000B2810
		public override bool UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV2 = false, bool updateUV3 = false, bool updateUV4 = false, bool updateColors = false, bool updateSkinningInfo = false)
		{
			return this._updateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000B4638 File Offset: 0x000B2838
		private bool _updateGameObjects(GameObject[] gos, bool recalcBounds, bool updateVertices, bool updateNormals, bool updateTangents, bool updateUV, bool updateUV2, bool updateUV3, bool updateUV4, bool updateColors, bool updateSkinningInfo)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("UpdateGameObjects called on " + gos.Length.ToString() + " objects.");
			}
			int numResultMats = 1;
			if (this.textureBakeResults.doMultiMaterial)
			{
				numResultMats = this.textureBakeResults.resultMaterials.Length;
			}
			if (!this._Initialize(numResultMats))
			{
				return false;
			}
			if (this._mesh.vertexCount > 0 && this._instance2combined_map.Count == 0)
			{
				Debug.LogWarning("There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
			}
			bool flag = true;
			MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache = new MB3_MeshCombinerSingle.MeshChannelsCache(this);
			MB2_TextureBakeResults.Material2AtlasRectangleMapper mat2rect_map = null;
			OrderedDictionary orderedDictionary = null;
			Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisResultsCache = null;
			if (updateUV)
			{
				orderedDictionary = this.BuildSourceMatsToSubmeshIdxMap(numResultMats);
				if (orderedDictionary == null)
				{
					return false;
				}
				mat2rect_map = new MB2_TextureBakeResults.Material2AtlasRectangleMapper(this.textureBakeResults);
				meshAnalysisResultsCache = new Dictionary<int, MB_Utility.MeshAnalysisResult[]>();
			}
			for (int i = 0; i < gos.Length; i++)
			{
				flag = (flag && this._updateGameObject(gos[i], updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo, meshChannelCache, meshAnalysisResultsCache, orderedDictionary, mat2rect_map));
			}
			if (recalcBounds)
			{
				this._mesh.RecalculateBounds();
			}
			return flag;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000B473C File Offset: 0x000B293C
		private bool _updateGameObject(GameObject go, bool updateVertices, bool updateNormals, bool updateTangents, bool updateUV, bool updateUV2, bool updateUV3, bool updateUV4, bool updateColors, bool updateSkinningInfo, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache, Dictionary<int, MB_Utility.MeshAnalysisResult[]> meshAnalysisResultsCache, OrderedDictionary sourceMats2submeshIdx_map, MB2_TextureBakeResults.Material2AtlasRectangleMapper mat2rect_map)
		{
			MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = null;
			if (!this.instance2Combined_MapTryGetValue(go, out mb_DynamicGameObject))
			{
				Debug.LogError("Object " + go.name + " has not been added");
				return false;
			}
			Mesh mesh = MB_Utility.GetMesh(go);
			if (mb_DynamicGameObject.numVerts != mesh.vertexCount)
			{
				Debug.LogError("Object " + go.name + " source mesh has been modified since being added. To update it must have the same number of verts");
				return false;
			}
			if (this._doUV && updateUV)
			{
				Material[] gomaterials = MB_Utility.GetGOMaterials(go);
				MB_TextureTilingTreatment[] array = new MB_TextureTilingTreatment[gomaterials.Length];
				Rect[] array2 = new Rect[gomaterials.Length];
				Rect[] array3 = new Rect[gomaterials.Length];
				Rect[] array4 = new Rect[gomaterials.Length];
				string message = "";
				int num = Mathf.Min(mesh.subMeshCount, gomaterials.Length);
				if (num != mb_DynamicGameObject.targetSubmeshIdxs.Length)
				{
					Debug.LogError(string.Format("Error updating object {0} in the combined mesh. The object has a different number of materials/submeshes than it did when previously baked. Try using AddDelete instead of Update.", go.name));
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					object obj = sourceMats2submeshIdx_map[gomaterials[i]];
					if (obj == null)
					{
						string[] array5 = new string[5];
						array5[0] = "Source object ";
						array5[1] = go.name;
						array5[2] = " used a material ";
						int num2 = 3;
						Material material = gomaterials[i];
						array5[num2] = ((material != null) ? material.ToString() : null);
						array5[4] = " that was not in the baked materials.";
						Debug.LogError(string.Concat(array5));
						return false;
					}
					int num3 = (int)obj;
					if (num3 != mb_DynamicGameObject.targetSubmeshIdxs[i])
					{
						Debug.LogError(string.Format("Update failed for object {0}. Material {1} is mapped to a different submesh in the combined mesh than the previous material. This is not supported. Try using AddDelete.", go.name, gomaterials[i]));
						return false;
					}
					if (!mat2rect_map.TryMapMaterialToUVRect(gomaterials[i], mesh, i, num3, meshChannelCache, meshAnalysisResultsCache, out array[i], out array2[i], out array3[i], out array4[i], ref message, this.LOG_LEVEL))
					{
						Debug.LogError(message);
						return false;
					}
				}
				mb_DynamicGameObject.uvRects = array2;
				mb_DynamicGameObject.encapsulatingRect = array3;
				mb_DynamicGameObject.sourceMaterialTiling = array4;
				this._copyAndAdjustUVsFromMesh(mb_DynamicGameObject, mesh, mb_DynamicGameObject.vertIdx, meshChannelCache);
			}
			if (this.doUV2() && updateUV2)
			{
				this._copyAndAdjustUV2FromMesh(mb_DynamicGameObject, mesh, mb_DynamicGameObject.vertIdx, meshChannelCache);
			}
			if (this.renderType == MB_RenderType.skinnedMeshRenderer && updateSkinningInfo)
			{
				Renderer renderer = MB_Utility.GetRenderer(go);
				BoneWeight[] array6 = meshChannelCache.GetBoneWeights(renderer, mb_DynamicGameObject.numVerts);
				Transform[] array7 = this._getBones(renderer);
				int num4 = mb_DynamicGameObject.vertIdx;
				bool flag = false;
				for (int j = 0; j < array6.Length; j++)
				{
					if (array7[array6[j].boneIndex0] != this.bones[this.boneWeights[num4].boneIndex0])
					{
						flag = true;
						break;
					}
					this.boneWeights[num4].weight0 = array6[j].weight0;
					this.boneWeights[num4].weight1 = array6[j].weight1;
					this.boneWeights[num4].weight2 = array6[j].weight2;
					this.boneWeights[num4].weight3 = array6[j].weight3;
					num4++;
				}
				if (flag)
				{
					Debug.LogError("Detected that some of the boneweights reference different bones than when initial added. Boneweights must reference the same bones " + mb_DynamicGameObject.name);
				}
			}
			Matrix4x4 localToWorldMatrix = go.transform.localToWorldMatrix;
			if (updateVertices)
			{
				Vector3[] vertices = meshChannelCache.GetVertices(mesh);
				for (int k = 0; k < vertices.Length; k++)
				{
					this.verts[mb_DynamicGameObject.vertIdx + k] = localToWorldMatrix.MultiplyPoint3x4(vertices[k]);
				}
			}
			localToWorldMatrix[0, 3] = (localToWorldMatrix[1, 3] = (localToWorldMatrix[2, 3] = 0f));
			if (this._doNorm && updateNormals)
			{
				Vector3[] array8 = meshChannelCache.GetNormals(mesh);
				for (int l = 0; l < array8.Length; l++)
				{
					int num5 = mb_DynamicGameObject.vertIdx + l;
					this.normals[num5] = localToWorldMatrix.MultiplyPoint3x4(array8[l]);
					this.normals[num5] = this.normals[num5].normalized;
				}
			}
			if (this._doTan && updateTangents)
			{
				Vector4[] array9 = meshChannelCache.GetTangents(mesh);
				for (int m = 0; m < array9.Length; m++)
				{
					int num6 = mb_DynamicGameObject.vertIdx + m;
					float w = array9[m].w;
					Vector3 v = localToWorldMatrix.MultiplyPoint3x4(array9[m]);
					v.Normalize();
					this.tangents[num6] = v;
					this.tangents[num6].w = w;
				}
			}
			if (this._doCol && updateColors)
			{
				Color[] array10 = meshChannelCache.GetColors(mesh);
				for (int n = 0; n < array10.Length; n++)
				{
					this.colors[mb_DynamicGameObject.vertIdx + n] = array10[n];
				}
			}
			if (this._doUV3 && updateUV3)
			{
				Vector2[] uv = meshChannelCache.GetUv3(mesh);
				for (int num7 = 0; num7 < uv.Length; num7++)
				{
					this.uv3s[mb_DynamicGameObject.vertIdx + num7] = uv[num7];
				}
			}
			if (this._doUV4 && updateUV4)
			{
				Vector2[] uv2 = meshChannelCache.GetUv4(mesh);
				for (int num8 = 0; num8 < uv2.Length; num8++)
				{
					this.uv4s[mb_DynamicGameObject.vertIdx + num8] = uv2[num8];
				}
			}
			return true;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00014E41 File Offset: 0x00013041
		public bool ShowHideGameObjects(GameObject[] toShow, GameObject[] toHide)
		{
			if (this.textureBakeResults == null)
			{
				Debug.LogError("TextureBakeResults must be set.");
				return false;
			}
			return this._showHide(toShow, toHide);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000B4CB8 File Offset: 0x000B2EB8
		public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true)
		{
			int[] array = null;
			if (deleteGOs != null)
			{
				array = new int[deleteGOs.Length];
				for (int i = 0; i < deleteGOs.Length; i++)
				{
					if (deleteGOs[i] == null)
					{
						Debug.LogError("The " + i.ToString() + "th object on the list of objects to delete is 'Null'");
					}
					else
					{
						array[i] = deleteGOs[i].GetInstanceID();
					}
				}
			}
			return this.AddDeleteGameObjectsByID(gos, array, disableRendererInSource);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000B4D20 File Offset: 0x000B2F20
		public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
		{
			if (this.validationLevel > MB2_ValidationLevel.none)
			{
				if (gos != null)
				{
					for (int i = 0; i < gos.Length; i++)
					{
						if (gos[i] == null)
						{
							Debug.LogError("The " + i.ToString() + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element.");
							return false;
						}
						if (this.validationLevel >= MB2_ValidationLevel.robust)
						{
							for (int j = i + 1; j < gos.Length; j++)
							{
								if (gos[i] == gos[j])
								{
									string str = "GameObject ";
									GameObject gameObject = gos[i];
									Debug.LogError(str + ((gameObject != null) ? gameObject.ToString() : null) + " appears twice in list of game objects to add");
									return false;
								}
							}
						}
					}
				}
				if (deleteGOinstanceIDs != null && this.validationLevel >= MB2_ValidationLevel.robust)
				{
					for (int k = 0; k < deleteGOinstanceIDs.Length; k++)
					{
						for (int l = k + 1; l < deleteGOinstanceIDs.Length; l++)
						{
							if (deleteGOinstanceIDs[k] == deleteGOinstanceIDs[l])
							{
								Debug.LogError("GameObject " + deleteGOinstanceIDs[k].ToString() + "appears twice in list of game objects to delete");
								return false;
							}
						}
					}
				}
			}
			if (this._usingTemporaryTextureBakeResult && gos != null && gos.Length != 0)
			{
				MB_Utility.Destroy(this._textureBakeResults);
				this._textureBakeResults = null;
				this._usingTemporaryTextureBakeResult = false;
			}
			if (this._textureBakeResults == null && gos != null && gos.Length != 0 && gos[0] != null && !this._CreateTemporaryTextrueBakeResult(gos, this.GetMaterialsOnTargetRenderer()))
			{
				return false;
			}
			this.BuildSceneMeshObject(gos, false);
			if (!this._addToCombined(gos, deleteGOinstanceIDs, disableRendererInSource))
			{
				Debug.LogError("Failed to add/delete objects to combined mesh");
				return false;
			}
			if (this.targetRenderer != null)
			{
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)this.targetRenderer;
					skinnedMeshRenderer.sharedMesh = this._mesh;
					skinnedMeshRenderer.bones = this.bones;
					this.UpdateSkinnedMeshApproximateBoundsFromBounds();
				}
				this.targetRenderer.lightmapIndex = this.GetLightmapIndex();
			}
			return true;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00014E65 File Offset: 0x00013065
		public override bool CombinedMeshContains(GameObject go)
		{
			return this.objectsInCombinedMesh.Contains(go);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000B4EE0 File Offset: 0x000B30E0
		public override void ClearBuffers()
		{
			this.verts = new Vector3[0];
			this.normals = new Vector3[0];
			this.tangents = new Vector4[0];
			this.uvs = new Vector2[0];
			this.uv2s = new Vector2[0];
			this.uv3s = new Vector2[0];
			this.uv4s = new Vector2[0];
			this.colors = new Color[0];
			this.bones = new Transform[0];
			this.bindPoses = new Matrix4x4[0];
			this.boneWeights = new BoneWeight[0];
			this.submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[0];
			this.blendShapes = new MB3_MeshCombinerSingle.MBBlendShape[0];
			if (this.blendShapesInCombined == null)
			{
				this.blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[0];
			}
			else
			{
				for (int i = 0; i < this.blendShapesInCombined.Length; i++)
				{
					this.blendShapesInCombined[i].frames = new MB3_MeshCombinerSingle.MBBlendShapeFrame[0];
				}
			}
			this.mbDynamicObjectsInCombinedMesh.Clear();
			this.objectsInCombinedMesh.Clear();
			this.instance2Combined_MapClear();
			if (this._usingTemporaryTextureBakeResult)
			{
				MB_Utility.Destroy(this._textureBakeResults);
				this._textureBakeResults = null;
				this._usingTemporaryTextureBakeResult = false;
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				MB2_Log.LogDebug("ClearBuffers called", Array.Empty<object>());
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00014E73 File Offset: 0x00013073
		private Mesh NewMesh()
		{
			if (Application.isPlaying)
			{
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.CreatedAtRuntime;
			}
			else
			{
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.CreatedInEditor;
			}
			return new Mesh();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00014E91 File Offset: 0x00013091
		public override void ClearMesh()
		{
			if (this._mesh != null)
			{
				MBVersion.MeshClear(this._mesh, false);
			}
			else
			{
				this._mesh = this.NewMesh();
			}
			this.ClearBuffers();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00014EC1 File Offset: 0x000130C1
		public override void DisposeRuntimeCreated()
		{
			if (Application.isPlaying)
			{
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.AssignedByUser;
				if (this._meshBirth == MB3_MeshCombinerSingle.MeshCreationConditions.CreatedAtRuntime)
				{
					UnityEngine.Object.Destroy(this._mesh);
				}
				else if (this._meshBirth == MB3_MeshCombinerSingle.MeshCreationConditions.AssignedByUser)
				{
					this._mesh = null;
				}
				this.ClearBuffers();
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000B501C File Offset: 0x000B321C
		public override void DestroyMesh()
		{
			if (this._mesh != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Destroying Mesh", Array.Empty<object>());
				}
				MB_Utility.Destroy(this._mesh);
				this._meshBirth = MB3_MeshCombinerSingle.MeshCreationConditions.NoMesh;
			}
			this.ClearBuffers();
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000B5068 File Offset: 0x000B3268
		public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
		{
			if (this._mesh != null && editorMethods != null && !Application.isPlaying)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Destroying Mesh", Array.Empty<object>());
				}
				editorMethods.Destroy(this._mesh);
			}
			this.ClearBuffers();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000B50B8 File Offset: 0x000B32B8
		public bool ValidateTargRendererAndMeshAndResultSceneObj()
		{
			if (this._resultSceneObject == null)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Result Scene Object was not set.");
				}
				return false;
			}
			if (this._targetRenderer == null)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Target Renderer was not set.");
				}
				return false;
			}
			if (this._targetRenderer.transform.parent != this._resultSceneObject.transform)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Target Renderer game object is not a child of Result Scene Object was not set.");
				}
				return false;
			}
			if (this._renderType == MB_RenderType.skinnedMeshRenderer && !(this._targetRenderer is SkinnedMeshRenderer))
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Render Type is skinned mesh renderer but Target Renderer is not.");
				}
				return false;
			}
			if (this._renderType == MB_RenderType.meshRenderer)
			{
				if (!(this._targetRenderer is MeshRenderer))
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Render Type is mesh renderer but Target Renderer is not.");
					}
					return false;
				}
				MeshFilter component = this._targetRenderer.GetComponent<MeshFilter>();
				if (this._mesh != component.sharedMesh)
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Target renderer mesh is not equal to mesh.");
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000B51CC File Offset: 0x000B33CC
		private OrderedDictionary BuildSourceMatsToSubmeshIdxMap(int numResultMats)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			for (int i = 0; i < numResultMats; i++)
			{
				MB_MultiMaterial mb_MultiMaterial = this._textureBakeResults.resultMaterials[i];
				for (int j = 0; j < mb_MultiMaterial.sourceMaterials.Count; j++)
				{
					if (mb_MultiMaterial.sourceMaterials[j] == null)
					{
						Debug.LogError("Found null material in source materials for combined mesh materials " + i.ToString());
						return null;
					}
					if (!orderedDictionary.Contains(mb_MultiMaterial.sourceMaterials[j]))
					{
						orderedDictionary.Add(mb_MultiMaterial.sourceMaterials[j], i);
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000B5270 File Offset: 0x000B3470
		internal static Renderer BuildSceneHierarchPreBake(MB3_MeshCombinerSingle mom, GameObject root, Mesh m, bool createNewChild = false, GameObject[] objsToBeAdded = null)
		{
			if (mom._LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("Building Scene Hierarchy createNewChild=" + createNewChild.ToString());
			}
			MeshFilter meshFilter = null;
			MeshRenderer meshRenderer = null;
			SkinnedMeshRenderer skinnedMeshRenderer = null;
			Transform transform = null;
			if (root == null)
			{
				Debug.LogError("root was null.");
				return null;
			}
			if (mom.textureBakeResults == null)
			{
				Debug.LogError("textureBakeResults must be set.");
				return null;
			}
			if (root.GetComponent<Renderer>() != null)
			{
				Debug.LogError("root game object cannot have a renderer component");
				return null;
			}
			if (!createNewChild)
			{
				if (mom.targetRenderer != null && mom.targetRenderer.transform.parent == root.transform)
				{
					transform = mom.targetRenderer.transform;
				}
				else
				{
					Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>();
					if (componentsInChildren.Length == 1)
					{
						if (componentsInChildren[0].transform.parent != root.transform)
						{
							Debug.LogError("Target Renderer is not an immediate child of Result Scene Object. Try using a game object with no children as the Result Scene Object..");
						}
						transform = componentsInChildren[0].transform;
					}
				}
			}
			if (transform != null && transform.parent != root.transform)
			{
				transform = null;
			}
			if (transform == null)
			{
				transform = new GameObject(mom.name + "-mesh")
				{
					transform = 
					{
						parent = root.transform
					}
				}.transform;
			}
			transform.parent = root.transform;
			GameObject gameObject = transform.gameObject;
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				if (component != null)
				{
					MB_Utility.Destroy(component);
				}
				MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
				if (component2 != null)
				{
					MB_Utility.Destroy(component2);
				}
				skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (skinnedMeshRenderer == null)
				{
					skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
				}
			}
			else
			{
				SkinnedMeshRenderer component3 = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (component3 != null)
				{
					MB_Utility.Destroy(component3);
				}
				meshFilter = gameObject.GetComponent<MeshFilter>();
				if (meshFilter == null)
				{
					meshFilter = gameObject.AddComponent<MeshFilter>();
				}
				meshRenderer = gameObject.GetComponent<MeshRenderer>();
				if (meshRenderer == null)
				{
					meshRenderer = gameObject.AddComponent<MeshRenderer>();
				}
			}
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				skinnedMeshRenderer.bones = mom.GetBones();
				bool updateWhenOffscreen = skinnedMeshRenderer.updateWhenOffscreen;
				skinnedMeshRenderer.updateWhenOffscreen = true;
				skinnedMeshRenderer.updateWhenOffscreen = updateWhenOffscreen;
			}
			MB3_MeshCombinerSingle._ConfigureSceneHierarch(mom, root, meshRenderer, meshFilter, skinnedMeshRenderer, m, objsToBeAdded);
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				return skinnedMeshRenderer;
			}
			return meshRenderer;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000B54BC File Offset: 0x000B36BC
		public static void BuildPrefabHierarchy(MB3_MeshCombinerSingle mom, GameObject instantiatedPrefabRoot, Mesh m, bool createNewChild = false, GameObject[] objsToBeAdded = null)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = null;
			MeshRenderer meshRenderer = null;
			MeshFilter meshFilter = null;
			Transform transform = new GameObject(mom.name + "-mesh")
			{
				transform = 
				{
					parent = instantiatedPrefabRoot.transform
				}
			}.transform;
			transform.parent = instantiatedPrefabRoot.transform;
			GameObject gameObject = transform.gameObject;
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				if (component != null)
				{
					MB_Utility.Destroy(component);
				}
				MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
				if (component2 != null)
				{
					MB_Utility.Destroy(component2);
				}
				skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (skinnedMeshRenderer == null)
				{
					skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
				}
			}
			else
			{
				SkinnedMeshRenderer component3 = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (component3 != null)
				{
					MB_Utility.Destroy(component3);
				}
				meshFilter = gameObject.GetComponent<MeshFilter>();
				if (meshFilter == null)
				{
					meshFilter = gameObject.AddComponent<MeshFilter>();
				}
				meshRenderer = gameObject.GetComponent<MeshRenderer>();
				if (meshRenderer == null)
				{
					meshRenderer = gameObject.AddComponent<MeshRenderer>();
				}
			}
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				skinnedMeshRenderer.bones = mom.GetBones();
				bool updateWhenOffscreen = skinnedMeshRenderer.updateWhenOffscreen;
				skinnedMeshRenderer.updateWhenOffscreen = true;
				skinnedMeshRenderer.updateWhenOffscreen = updateWhenOffscreen;
				skinnedMeshRenderer.sharedMesh = m;
			}
			MB3_MeshCombinerSingle._ConfigureSceneHierarch(mom, instantiatedPrefabRoot, meshRenderer, meshFilter, skinnedMeshRenderer, m, objsToBeAdded);
			if (mom.targetRenderer != null)
			{
				Material[] array = new Material[mom.targetRenderer.sharedMaterials.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = mom.targetRenderer.sharedMaterials[i];
				}
				if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					skinnedMeshRenderer.sharedMaterial = null;
					skinnedMeshRenderer.sharedMaterials = array;
					return;
				}
				meshRenderer.sharedMaterial = null;
				meshRenderer.sharedMaterials = array;
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000B565C File Offset: 0x000B385C
		private static void _ConfigureSceneHierarch(MB3_MeshCombinerSingle mom, GameObject root, MeshRenderer mr, MeshFilter mf, SkinnedMeshRenderer smr, Mesh m, GameObject[] objsToBeAdded = null)
		{
			GameObject gameObject;
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				gameObject = smr.gameObject;
				smr.lightmapIndex = mom.GetLightmapIndex();
			}
			else
			{
				gameObject = mr.gameObject;
				mf.sharedMesh = m;
				mr.lightmapIndex = mom.GetLightmapIndex();
			}
			if (mom.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping || mom.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
			{
				gameObject.isStatic = true;
			}
			if (objsToBeAdded != null && objsToBeAdded.Length != 0 && objsToBeAdded[0] != null)
			{
				bool flag = true;
				bool flag2 = true;
				string tag = objsToBeAdded[0].tag;
				int layer = objsToBeAdded[0].layer;
				for (int i = 0; i < objsToBeAdded.Length; i++)
				{
					if (objsToBeAdded[i] != null)
					{
						if (!objsToBeAdded[i].tag.Equals(tag))
						{
							flag = false;
						}
						if (objsToBeAdded[i].layer != layer)
						{
							flag2 = false;
						}
					}
				}
				if (flag)
				{
					root.tag = tag;
					gameObject.tag = tag;
				}
				if (flag2)
				{
					root.layer = layer;
					gameObject.layer = layer;
				}
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000B575C File Offset: 0x000B395C
		public void BuildSceneMeshObject(GameObject[] gos = null, bool createNewChild = false)
		{
			if (this._resultSceneObject == null)
			{
				this._resultSceneObject = new GameObject("CombinedMesh-" + base.name);
			}
			this._targetRenderer = MB3_MeshCombinerSingle.BuildSceneHierarchPreBake(this, this._resultSceneObject, this.GetMesh(), createNewChild, gos);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000B57AC File Offset: 0x000B39AC
		private bool IsMirrored(Matrix4x4 tm)
		{
			Vector3 lhs = tm.GetRow(0);
			Vector3 rhs = tm.GetRow(1);
			Vector3 rhs2 = tm.GetRow(2);
			lhs.Normalize();
			rhs.Normalize();
			rhs2.Normalize();
			return Vector3.Dot(Vector3.Cross(lhs, rhs), rhs2) < 0f;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000B5810 File Offset: 0x000B3A10
		public override void CheckIntegrity()
		{
			if (!MB_Utility.DO_INTEGRITY_CHECKS)
			{
				return;
			}
			if (this.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[i];
					HashSet<int> hashSet = new HashSet<int>();
					HashSet<int> hashSet2 = new HashSet<int>();
					for (int j = mb_DynamicGameObject.vertIdx; j < mb_DynamicGameObject.vertIdx + mb_DynamicGameObject.numVerts; j++)
					{
						hashSet.Add(this.boneWeights[j].boneIndex0);
						hashSet.Add(this.boneWeights[j].boneIndex1);
						hashSet.Add(this.boneWeights[j].boneIndex2);
						hashSet.Add(this.boneWeights[j].boneIndex3);
					}
					for (int k = 0; k < mb_DynamicGameObject.indexesOfBonesUsed.Length; k++)
					{
						hashSet2.Add(mb_DynamicGameObject.indexesOfBonesUsed[k]);
					}
					hashSet2.ExceptWith(hashSet);
					if (hashSet2.Count > 0)
					{
						Debug.LogError("The bone indexes were not the same. " + hashSet.Count.ToString() + " " + hashSet2.Count.ToString());
					}
					for (int l = 0; l < mb_DynamicGameObject.indexesOfBonesUsed.Length; l++)
					{
						if (l < 0 || l > this.bones.Length)
						{
							Debug.LogError("Bone index was out of bounds.");
						}
					}
					if (this.renderType == MB_RenderType.skinnedMeshRenderer && mb_DynamicGameObject.indexesOfBonesUsed.Length < 1)
					{
						Debug.Log("DGO had no bones");
					}
				}
			}
			if (this.doBlendShapes && this.renderType != MB_RenderType.skinnedMeshRenderer)
			{
				Debug.LogError("Blend shapes can only be used with skinned meshes.");
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000B59C0 File Offset: 0x000B3BC0
		private void _ZeroArray(Vector3[] arr, int idx, int length)
		{
			int num = idx + length;
			for (int i = idx; i < num; i++)
			{
				arr[i] = Vector3.zero;
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000B59EC File Offset: 0x000B3BEC
		private List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] _buildBoneIdx2dgoMap()
		{
			List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[] array = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>[this.bones.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();
			}
			for (int j = 0; j < this.mbDynamicObjectsInCombinedMesh.Count; j++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[j];
				for (int k = 0; k < mb_DynamicGameObject.indexesOfBonesUsed.Length; k++)
				{
					array[mb_DynamicGameObject.indexesOfBonesUsed[k]].Add(mb_DynamicGameObject);
				}
			}
			return array;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000B5A6C File Offset: 0x000B3C6C
		private void _CollectBonesToAddForDGO(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Dictionary<Transform, int> bone2idx, HashSet<int> boneIdxsToDelete, HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> bonesToAdd, Renderer r, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
		{
			Matrix4x4[] array = dgo._tmpCachedBindposes = meshChannelCache.GetBindposes(r);
			BoneWeight[] array2 = dgo._tmpCachedBoneWeights = meshChannelCache.GetBoneWeights(r, dgo.numVerts);
			Transform[] array3 = dgo._tmpCachedBones = this._getBones(r);
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < array2.Length; i++)
			{
				hashSet.Add(array2[i].boneIndex0);
				hashSet.Add(array2[i].boneIndex1);
				hashSet.Add(array2[i].boneIndex2);
				hashSet.Add(array2[i].boneIndex3);
			}
			int[] array4 = new int[hashSet.Count];
			hashSet.CopyTo(array4);
			for (int j = 0; j < array4.Length; j++)
			{
				bool flag = false;
				int num = array4[j];
				int num2;
				if (bone2idx.TryGetValue(array3[num], out num2) && array3[num] == this.bones[num2] && !boneIdxsToDelete.Contains(num2) && array[num] == this.bindPoses[num2])
				{
					flag = true;
				}
				if (!flag)
				{
					MB3_MeshCombinerSingle.BoneAndBindpose item = new MB3_MeshCombinerSingle.BoneAndBindpose(array3[num], array[num]);
					if (!bonesToAdd.Contains(item))
					{
						bonesToAdd.Add(item);
					}
				}
			}
			dgo._tmpIndexesOfSourceBonesUsed = array4;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000B5BDC File Offset: 0x000B3DDC
		private void _CopyBonesWeAreKeepingToNewBonesArrayAndAdjustBWIndexes(HashSet<int> boneIdxsToDeleteHS, HashSet<MB3_MeshCombinerSingle.BoneAndBindpose> bonesToAdd, Transform[] nbones, Matrix4x4[] nbindPoses, BoneWeight[] nboneWeights, int totalDeleteVerts)
		{
			if (boneIdxsToDeleteHS.Count > 0)
			{
				int[] array = new int[boneIdxsToDeleteHS.Count];
				boneIdxsToDeleteHS.CopyTo(array);
				Array.Sort<int>(array);
				int[] array2 = new int[this.bones.Length];
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < this.bones.Length; i++)
				{
					if (num2 < array.Length && array[num2] == i)
					{
						num2++;
						array2[i] = -1;
					}
					else
					{
						array2[i] = num;
						nbones[num] = this.bones[i];
						nbindPoses[num] = this.bindPoses[i];
						num++;
					}
				}
				int num3 = this.boneWeights.Length - totalDeleteVerts;
				for (int j = 0; j < num3; j++)
				{
					nboneWeights[j].boneIndex0 = array2[nboneWeights[j].boneIndex0];
					nboneWeights[j].boneIndex1 = array2[nboneWeights[j].boneIndex1];
					nboneWeights[j].boneIndex2 = array2[nboneWeights[j].boneIndex2];
					nboneWeights[j].boneIndex3 = array2[nboneWeights[j].boneIndex3];
				}
				for (int k = 0; k < this.mbDynamicObjectsInCombinedMesh.Count; k++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[k];
					for (int l = 0; l < mb_DynamicGameObject.indexesOfBonesUsed.Length; l++)
					{
						mb_DynamicGameObject.indexesOfBonesUsed[l] = array2[mb_DynamicGameObject.indexesOfBonesUsed[l]];
					}
				}
				return;
			}
			Array.Copy(this.bones, nbones, this.bones.Length);
			Array.Copy(this.bindPoses, nbindPoses, this.bindPoses.Length);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000B5D94 File Offset: 0x000B3F94
		private void _AddBonesToNewBonesArrayAndAdjustBWIndexes(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Renderer r, int vertsIdx, Transform[] nbones, BoneWeight[] nboneWeights, MB3_MeshCombinerSingle.MeshChannelsCache meshChannelCache)
		{
			Transform[] tmpCachedBones = dgo._tmpCachedBones;
			Matrix4x4[] tmpCachedBindposes = dgo._tmpCachedBindposes;
			BoneWeight[] tmpCachedBoneWeights = dgo._tmpCachedBoneWeights;
			int[] array = new int[tmpCachedBones.Length];
			for (int i = 0; i < dgo._tmpIndexesOfSourceBonesUsed.Length; i++)
			{
				int num = dgo._tmpIndexesOfSourceBonesUsed[i];
				for (int j = 0; j < nbones.Length; j++)
				{
					if (tmpCachedBones[num] == nbones[j] && tmpCachedBindposes[num] == this.bindPoses[j])
					{
						array[num] = j;
						break;
					}
				}
			}
			for (int k = 0; k < tmpCachedBoneWeights.Length; k++)
			{
				int num2 = vertsIdx + k;
				nboneWeights[num2].boneIndex0 = array[tmpCachedBoneWeights[k].boneIndex0];
				nboneWeights[num2].boneIndex1 = array[tmpCachedBoneWeights[k].boneIndex1];
				nboneWeights[num2].boneIndex2 = array[tmpCachedBoneWeights[k].boneIndex2];
				nboneWeights[num2].boneIndex3 = array[tmpCachedBoneWeights[k].boneIndex3];
				nboneWeights[num2].weight0 = tmpCachedBoneWeights[k].weight0;
				nboneWeights[num2].weight1 = tmpCachedBoneWeights[k].weight1;
				nboneWeights[num2].weight2 = tmpCachedBoneWeights[k].weight2;
				nboneWeights[num2].weight3 = tmpCachedBoneWeights[k].weight3;
			}
			for (int l = 0; l < dgo._tmpIndexesOfSourceBonesUsed.Length; l++)
			{
				dgo._tmpIndexesOfSourceBonesUsed[l] = array[dgo._tmpIndexesOfSourceBonesUsed[l]];
			}
			dgo.indexesOfBonesUsed = dgo._tmpIndexesOfSourceBonesUsed;
			dgo._tmpIndexesOfSourceBonesUsed = null;
			dgo._tmpCachedBones = null;
			dgo._tmpCachedBindposes = null;
			dgo._tmpCachedBoneWeights = null;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000B5F7C File Offset: 0x000B417C
		private void _copyUV2unchangedToSeparateRects()
		{
			int padding = 16;
			List<Vector2> list = new List<Vector2>();
			float num = 1E+11f;
			float num2 = 0f;
			for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
			{
				float magnitude = this.mbDynamicObjectsInCombinedMesh[i].meshSize.magnitude;
				if (magnitude > num2)
				{
					num2 = magnitude;
				}
				if (magnitude < num)
				{
					num = magnitude;
				}
			}
			float num3 = 1000f;
			float num4 = 10f;
			float num5 = 0f;
			float num6;
			if (num2 - num > num3 - num4)
			{
				num6 = (num3 - num4) / (num2 - num);
				num5 = num4 - num * num6;
			}
			else
			{
				num6 = num3 / num2;
			}
			for (int j = 0; j < this.mbDynamicObjectsInCombinedMesh.Count; j++)
			{
				float num7 = this.mbDynamicObjectsInCombinedMesh[j].meshSize.magnitude;
				num7 = num7 * num6 + num5;
				Vector2 item = Vector2.one * num7;
				list.Add(item);
			}
			AtlasPackingResult[] rects = new MB2_TexturePackerRegular
			{
				atlasMustBePowerOfTwo = false
			}.GetRects(list, 8192, 8192, padding);
			for (int k = 0; k < this.mbDynamicObjectsInCombinedMesh.Count; k++)
			{
				MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[k];
				float x;
				float num8 = x = this.uv2s[mb_DynamicGameObject.vertIdx].x;
				float y;
				float num9 = y = this.uv2s[mb_DynamicGameObject.vertIdx].y;
				int num10 = mb_DynamicGameObject.vertIdx + mb_DynamicGameObject.numVerts;
				for (int l = mb_DynamicGameObject.vertIdx; l < num10; l++)
				{
					if (this.uv2s[l].x < x)
					{
						x = this.uv2s[l].x;
					}
					if (this.uv2s[l].x > num8)
					{
						num8 = this.uv2s[l].x;
					}
					if (this.uv2s[l].y < y)
					{
						y = this.uv2s[l].y;
					}
					if (this.uv2s[l].y > num9)
					{
						num9 = this.uv2s[l].y;
					}
				}
				Rect rect = rects[0].rects[k];
				for (int m = mb_DynamicGameObject.vertIdx; m < num10; m++)
				{
					float num11 = num8 - x;
					float num12 = num9 - y;
					if (num11 == 0f)
					{
						num11 = 1f;
					}
					if (num12 == 0f)
					{
						num12 = 1f;
					}
					this.uv2s[m].x = (this.uv2s[m].x - x) / num11 * rect.width + rect.x;
					this.uv2s[m].y = (this.uv2s[m].y - y) / num12 * rect.height + rect.y;
				}
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000B62A4 File Offset: 0x000B44A4
		public override List<Material> GetMaterialsOnTargetRenderer()
		{
			List<Material> list = new List<Material>();
			if (this._targetRenderer != null)
			{
				list.AddRange(this._targetRenderer.sharedMaterials);
			}
			return list;
		}

		// Token: 0x04001CC4 RID: 7364
		[SerializeField]
		protected List<GameObject> objectsInCombinedMesh = new List<GameObject>();

		// Token: 0x04001CC5 RID: 7365
		[SerializeField]
		private int lightmapIndex = -1;

		// Token: 0x04001CC6 RID: 7366
		[SerializeField]
		private List<MB3_MeshCombinerSingle.MB_DynamicGameObject> mbDynamicObjectsInCombinedMesh = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();

		// Token: 0x04001CC7 RID: 7367
		private Dictionary<GameObject, MB3_MeshCombinerSingle.MB_DynamicGameObject> _instance2combined_map = new Dictionary<GameObject, MB3_MeshCombinerSingle.MB_DynamicGameObject>();

		// Token: 0x04001CC8 RID: 7368
		[SerializeField]
		private Vector3[] verts = new Vector3[0];

		// Token: 0x04001CC9 RID: 7369
		[SerializeField]
		private Vector3[] normals = new Vector3[0];

		// Token: 0x04001CCA RID: 7370
		[SerializeField]
		private Vector4[] tangents = new Vector4[0];

		// Token: 0x04001CCB RID: 7371
		[SerializeField]
		private Vector2[] uvs = new Vector2[0];

		// Token: 0x04001CCC RID: 7372
		[SerializeField]
		private Vector2[] uv2s = new Vector2[0];

		// Token: 0x04001CCD RID: 7373
		[SerializeField]
		private Vector2[] uv3s = new Vector2[0];

		// Token: 0x04001CCE RID: 7374
		[SerializeField]
		private Vector2[] uv4s = new Vector2[0];

		// Token: 0x04001CCF RID: 7375
		[SerializeField]
		private Color[] colors = new Color[0];

		// Token: 0x04001CD0 RID: 7376
		[SerializeField]
		private Matrix4x4[] bindPoses = new Matrix4x4[0];

		// Token: 0x04001CD1 RID: 7377
		[SerializeField]
		private Transform[] bones = new Transform[0];

		// Token: 0x04001CD2 RID: 7378
		[SerializeField]
		internal MB3_MeshCombinerSingle.MBBlendShape[] blendShapes = new MB3_MeshCombinerSingle.MBBlendShape[0];

		// Token: 0x04001CD3 RID: 7379
		[SerializeField]
		internal MB3_MeshCombinerSingle.MBBlendShape[] blendShapesInCombined = new MB3_MeshCombinerSingle.MBBlendShape[0];

		// Token: 0x04001CD4 RID: 7380
		[SerializeField]
		private MB3_MeshCombinerSingle.SerializableIntArray[] submeshTris = new MB3_MeshCombinerSingle.SerializableIntArray[0];

		// Token: 0x04001CD5 RID: 7381
		[SerializeField]
		private MB3_MeshCombinerSingle.MeshCreationConditions _meshBirth;

		// Token: 0x04001CD6 RID: 7382
		[SerializeField]
		private Mesh _mesh;

		// Token: 0x04001CD7 RID: 7383
		private BoneWeight[] boneWeights = new BoneWeight[0];

		// Token: 0x04001CD8 RID: 7384
		private GameObject[] empty = new GameObject[0];

		// Token: 0x04001CD9 RID: 7385
		private int[] emptyIDs = new int[0];

		// Token: 0x0200044F RID: 1103
		public enum MeshCreationConditions
		{
			// Token: 0x04001CDB RID: 7387
			NoMesh,
			// Token: 0x04001CDC RID: 7388
			CreatedInEditor,
			// Token: 0x04001CDD RID: 7389
			CreatedAtRuntime,
			// Token: 0x04001CDE RID: 7390
			AssignedByUser
		}

		// Token: 0x02000450 RID: 1104
		[Serializable]
		public class SerializableIntArray
		{
			// Token: 0x06001BAA RID: 7082 RVA: 0x0000256A File Offset: 0x0000076A
			public SerializableIntArray()
			{
			}

			// Token: 0x06001BAB RID: 7083 RVA: 0x00014EFD File Offset: 0x000130FD
			public SerializableIntArray(int len)
			{
				this.data = new int[len];
			}

			// Token: 0x04001CDF RID: 7391
			public int[] data;
		}

		// Token: 0x02000451 RID: 1105
		[Serializable]
		public class MB_DynamicGameObject : IComparable<MB3_MeshCombinerSingle.MB_DynamicGameObject>
		{
			// Token: 0x06001BAC RID: 7084 RVA: 0x00014F11 File Offset: 0x00013111
			public int CompareTo(MB3_MeshCombinerSingle.MB_DynamicGameObject b)
			{
				return this.vertIdx - b.vertIdx;
			}

			// Token: 0x04001CE0 RID: 7392
			public int instanceID;

			// Token: 0x04001CE1 RID: 7393
			public GameObject gameObject;

			// Token: 0x04001CE2 RID: 7394
			public string name;

			// Token: 0x04001CE3 RID: 7395
			public int vertIdx;

			// Token: 0x04001CE4 RID: 7396
			public int blendShapeIdx;

			// Token: 0x04001CE5 RID: 7397
			public int numVerts;

			// Token: 0x04001CE6 RID: 7398
			public int numBlendShapes;

			// Token: 0x04001CE7 RID: 7399
			public int[] indexesOfBonesUsed = new int[0];

			// Token: 0x04001CE8 RID: 7400
			public int lightmapIndex = -1;

			// Token: 0x04001CE9 RID: 7401
			public Vector4 lightmapTilingOffset = new Vector4(1f, 1f, 0f, 0f);

			// Token: 0x04001CEA RID: 7402
			public Vector3 meshSize = Vector3.one;

			// Token: 0x04001CEB RID: 7403
			public bool show = true;

			// Token: 0x04001CEC RID: 7404
			public bool invertTriangles;

			// Token: 0x04001CED RID: 7405
			public int[] submeshTriIdxs;

			// Token: 0x04001CEE RID: 7406
			public int[] submeshNumTris;

			// Token: 0x04001CEF RID: 7407
			public int[] targetSubmeshIdxs;

			// Token: 0x04001CF0 RID: 7408
			public Rect[] uvRects;

			// Token: 0x04001CF1 RID: 7409
			public Rect[] encapsulatingRect;

			// Token: 0x04001CF2 RID: 7410
			public Rect[] sourceMaterialTiling;

			// Token: 0x04001CF3 RID: 7411
			public Rect[] obUVRects;

			// Token: 0x04001CF4 RID: 7412
			public bool _beingDeleted;

			// Token: 0x04001CF5 RID: 7413
			public int _triangleIdxAdjustment;

			// Token: 0x04001CF6 RID: 7414
			[NonSerialized]
			public MB3_MeshCombinerSingle.SerializableIntArray[] _tmpSubmeshTris;

			// Token: 0x04001CF7 RID: 7415
			[NonSerialized]
			public Transform[] _tmpCachedBones;

			// Token: 0x04001CF8 RID: 7416
			[NonSerialized]
			public Matrix4x4[] _tmpCachedBindposes;

			// Token: 0x04001CF9 RID: 7417
			[NonSerialized]
			public BoneWeight[] _tmpCachedBoneWeights;

			// Token: 0x04001CFA RID: 7418
			[NonSerialized]
			public int[] _tmpIndexesOfSourceBonesUsed;
		}

		// Token: 0x02000452 RID: 1106
		public class MeshChannels
		{
			// Token: 0x04001CFB RID: 7419
			public Vector3[] vertices;

			// Token: 0x04001CFC RID: 7420
			public Vector3[] normals;

			// Token: 0x04001CFD RID: 7421
			public Vector4[] tangents;

			// Token: 0x04001CFE RID: 7422
			public Vector2[] uv0raw;

			// Token: 0x04001CFF RID: 7423
			public Vector2[] uv0modified;

			// Token: 0x04001D00 RID: 7424
			public Vector2[] uv2;

			// Token: 0x04001D01 RID: 7425
			public Vector2[] uv3;

			// Token: 0x04001D02 RID: 7426
			public Vector2[] uv4;

			// Token: 0x04001D03 RID: 7427
			public Color[] colors;

			// Token: 0x04001D04 RID: 7428
			public BoneWeight[] boneWeights;

			// Token: 0x04001D05 RID: 7429
			public Matrix4x4[] bindPoses;

			// Token: 0x04001D06 RID: 7430
			public int[] triangles;

			// Token: 0x04001D07 RID: 7431
			public MB3_MeshCombinerSingle.MBBlendShape[] blendShapes;
		}

		// Token: 0x02000453 RID: 1107
		[Serializable]
		public class MBBlendShapeFrame
		{
			// Token: 0x04001D08 RID: 7432
			public float frameWeight;

			// Token: 0x04001D09 RID: 7433
			public Vector3[] vertices;

			// Token: 0x04001D0A RID: 7434
			public Vector3[] normals;

			// Token: 0x04001D0B RID: 7435
			public Vector3[] tangents;
		}

		// Token: 0x02000454 RID: 1108
		[Serializable]
		public class MBBlendShape
		{
			// Token: 0x04001D0C RID: 7436
			public int gameObjectID;

			// Token: 0x04001D0D RID: 7437
			public GameObject gameObject;

			// Token: 0x04001D0E RID: 7438
			public string name;

			// Token: 0x04001D0F RID: 7439
			public int indexInSource;

			// Token: 0x04001D10 RID: 7440
			public MB3_MeshCombinerSingle.MBBlendShapeFrame[] frames;
		}

		// Token: 0x02000455 RID: 1109
		public class MeshChannelsCache
		{
			// Token: 0x06001BB1 RID: 7089 RVA: 0x00014F20 File Offset: 0x00013120
			public MeshChannelsCache(MB3_MeshCombinerSingle mcs)
			{
				this.mc = mcs;
			}

			// Token: 0x06001BB2 RID: 7090 RVA: 0x000B642C File Offset: 0x000B462C
			internal Vector3[] GetVertices(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.vertices == null)
				{
					meshChannels.vertices = m.vertices;
				}
				return meshChannels.vertices;
			}

			// Token: 0x06001BB3 RID: 7091 RVA: 0x000B6480 File Offset: 0x000B4680
			internal Vector3[] GetNormals(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.normals == null)
				{
					meshChannels.normals = this._getMeshNormals(m);
				}
				return meshChannels.normals;
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x000B64D8 File Offset: 0x000B46D8
			internal Vector4[] GetTangents(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.tangents == null)
				{
					meshChannels.tangents = this._getMeshTangents(m);
				}
				return meshChannels.tangents;
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x000B6530 File Offset: 0x000B4730
			internal Vector2[] GetUv0Raw(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.uv0raw == null)
				{
					meshChannels.uv0raw = this._getMeshUVs(m);
				}
				return meshChannels.uv0raw;
			}

			// Token: 0x06001BB6 RID: 7094 RVA: 0x000B6588 File Offset: 0x000B4788
			internal Vector2[] GetUv0Modified(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.uv0modified == null)
				{
					meshChannels.uv0modified = null;
				}
				return meshChannels.uv0modified;
			}

			// Token: 0x06001BB7 RID: 7095 RVA: 0x000B65D8 File Offset: 0x000B47D8
			internal Vector2[] GetUv2(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.uv2 == null)
				{
					meshChannels.uv2 = this._getMeshUV2s(m);
				}
				return meshChannels.uv2;
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x000B6630 File Offset: 0x000B4830
			internal Vector2[] GetUv3(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.uv3 == null)
				{
					meshChannels.uv3 = MBVersion.GetMeshUV3orUV4(m, true, this.mc.LOG_LEVEL);
				}
				return meshChannels.uv3;
			}

			// Token: 0x06001BB9 RID: 7097 RVA: 0x000B6690 File Offset: 0x000B4890
			internal Vector2[] GetUv4(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.uv4 == null)
				{
					meshChannels.uv4 = MBVersion.GetMeshUV3orUV4(m, false, this.mc.LOG_LEVEL);
				}
				return meshChannels.uv4;
			}

			// Token: 0x06001BBA RID: 7098 RVA: 0x000B66F0 File Offset: 0x000B48F0
			internal Color[] GetColors(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.colors == null)
				{
					meshChannels.colors = this._getMeshColors(m);
				}
				return meshChannels.colors;
			}

			// Token: 0x06001BBB RID: 7099 RVA: 0x000B6748 File Offset: 0x000B4948
			internal Matrix4x4[] GetBindposes(Renderer r)
			{
				Mesh mesh = MB_Utility.GetMesh(r.gameObject);
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(mesh.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(mesh.GetInstanceID(), meshChannels);
				}
				if (meshChannels.bindPoses == null)
				{
					meshChannels.bindPoses = MB3_MeshCombinerSingle.MeshChannelsCache._getBindPoses(r);
				}
				return meshChannels.bindPoses;
			}

			// Token: 0x06001BBC RID: 7100 RVA: 0x000B67A8 File Offset: 0x000B49A8
			internal BoneWeight[] GetBoneWeights(Renderer r, int numVertsInMeshBeingAdded)
			{
				Mesh mesh = MB_Utility.GetMesh(r.gameObject);
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(mesh.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(mesh.GetInstanceID(), meshChannels);
				}
				if (meshChannels.boneWeights == null)
				{
					meshChannels.boneWeights = MB3_MeshCombinerSingle.MeshChannelsCache._getBoneWeights(r, numVertsInMeshBeingAdded);
				}
				return meshChannels.boneWeights;
			}

			// Token: 0x06001BBD RID: 7101 RVA: 0x000B680C File Offset: 0x000B4A0C
			internal int[] GetTriangles(Mesh m)
			{
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.triangles == null)
				{
					meshChannels.triangles = m.triangles;
				}
				return meshChannels.triangles;
			}

			// Token: 0x06001BBE RID: 7102 RVA: 0x000B6860 File Offset: 0x000B4A60
			internal MB3_MeshCombinerSingle.MBBlendShape[] GetBlendShapes(Mesh m, int gameObjectID)
			{
				if (MBVersion.GetMajorVersion() <= 5 && (MBVersion.GetMajorVersion() != 5 || MBVersion.GetMinorVersion() < 3))
				{
					return new MB3_MeshCombinerSingle.MBBlendShape[0];
				}
				MB3_MeshCombinerSingle.MeshChannels meshChannels;
				if (!this.meshID2MeshChannels.TryGetValue(m.GetInstanceID(), out meshChannels))
				{
					meshChannels = new MB3_MeshCombinerSingle.MeshChannels();
					this.meshID2MeshChannels.Add(m.GetInstanceID(), meshChannels);
				}
				if (meshChannels.blendShapes == null)
				{
					MB3_MeshCombinerSingle.MBBlendShape[] array = new MB3_MeshCombinerSingle.MBBlendShape[m.blendShapeCount];
					int vertexCount = m.vertexCount;
					for (int i = 0; i < array.Length; i++)
					{
						MB3_MeshCombinerSingle.MBBlendShape mbblendShape = array[i] = new MB3_MeshCombinerSingle.MBBlendShape();
						mbblendShape.frames = new MB3_MeshCombinerSingle.MBBlendShapeFrame[MBVersion.GetBlendShapeFrameCount(m, i)];
						mbblendShape.name = m.GetBlendShapeName(i);
						mbblendShape.indexInSource = i;
						mbblendShape.gameObjectID = gameObjectID;
						for (int j = 0; j < mbblendShape.frames.Length; j++)
						{
							MB3_MeshCombinerSingle.MBBlendShapeFrame mbblendShapeFrame = mbblendShape.frames[j] = new MB3_MeshCombinerSingle.MBBlendShapeFrame();
							mbblendShapeFrame.frameWeight = MBVersion.GetBlendShapeFrameWeight(m, i, j);
							mbblendShapeFrame.vertices = new Vector3[vertexCount];
							mbblendShapeFrame.normals = new Vector3[vertexCount];
							mbblendShapeFrame.tangents = new Vector3[vertexCount];
							MBVersion.GetBlendShapeFrameVertices(m, i, j, mbblendShapeFrame.vertices, mbblendShapeFrame.normals, mbblendShapeFrame.tangents);
						}
					}
					meshChannels.blendShapes = array;
					return meshChannels.blendShapes;
				}
				MB3_MeshCombinerSingle.MBBlendShape[] array2 = new MB3_MeshCombinerSingle.MBBlendShape[meshChannels.blendShapes.Length];
				for (int k = 0; k < array2.Length; k++)
				{
					array2[k] = new MB3_MeshCombinerSingle.MBBlendShape();
					array2[k].name = meshChannels.blendShapes[k].name;
					array2[k].indexInSource = meshChannels.blendShapes[k].indexInSource;
					array2[k].frames = meshChannels.blendShapes[k].frames;
					array2[k].gameObjectID = gameObjectID;
				}
				return array2;
			}

			// Token: 0x06001BBF RID: 7103 RVA: 0x000B6A44 File Offset: 0x000B4C44
			private Color[] _getMeshColors(Mesh m)
			{
				Color[] array = m.colors;
				if (array.Length == 0)
				{
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no colors. Generating", Array.Empty<object>());
					}
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have colors. Generating an array of white colors");
					}
					array = new Color[m.vertexCount];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Color.white;
					}
				}
				return array;
			}

			// Token: 0x06001BC0 RID: 7104 RVA: 0x000B6AEC File Offset: 0x000B4CEC
			private Vector3[] _getMeshNormals(Mesh m)
			{
				Vector3[] normals = m.normals;
				if (normals.Length == 0)
				{
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no normals. Generating", Array.Empty<object>());
					}
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have normals. Generating normals.");
					}
					Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(m);
					mesh.RecalculateNormals();
					normals = mesh.normals;
					MB_Utility.Destroy(mesh);
				}
				return normals;
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x000B6B84 File Offset: 0x000B4D84
			private Vector4[] _getMeshTangents(Mesh m)
			{
				Vector4[] array = m.tangents;
				if (array.Length == 0)
				{
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no tangents. Generating", Array.Empty<object>());
					}
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have tangents. Generating tangents.");
					}
					Vector3[] vertices = m.vertices;
					Vector2[] uv0Raw = this.GetUv0Raw(m);
					Vector3[] normals = this._getMeshNormals(m);
					array = new Vector4[m.vertexCount];
					for (int i = 0; i < m.subMeshCount; i++)
					{
						int[] triangles = m.GetTriangles(i);
						this._generateTangents(triangles, vertices, uv0Raw, normals, array);
					}
				}
				return array;
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x000B6C54 File Offset: 0x000B4E54
			private Vector2[] _getMeshUVs(Mesh m)
			{
				Vector2[] array = m.uv;
				if (array.Length == 0)
				{
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no uvs. Generating", Array.Empty<object>());
					}
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have uvs. Generating uvs.");
					}
					array = new Vector2[m.vertexCount];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._HALF_UV;
					}
				}
				return array;
			}

			// Token: 0x06001BC3 RID: 7107 RVA: 0x000B6CFC File Offset: 0x000B4EFC
			private Vector2[] _getMeshUV2s(Mesh m)
			{
				Vector2[] array = m.uv2;
				if (array.Length == 0)
				{
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Mesh " + ((m != null) ? m.ToString() : null) + " has no uv2s. Generating", Array.Empty<object>());
					}
					if (this.mc.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Mesh " + ((m != null) ? m.ToString() : null) + " didn't have uv2s. Generating uv2s.");
					}
					if (this.mc._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects)
					{
						Debug.LogError("Mesh " + ((m != null) ? m.ToString() : null) + " did not have a UV2 channel. Nothing to copy when trying to copy UV2 to separate rects. The combined mesh will not lightmap properly. Try using generate new uv2 layout.");
					}
					array = new Vector2[m.vertexCount];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._HALF_UV;
					}
				}
				return array;
			}

			// Token: 0x06001BC4 RID: 7108 RVA: 0x000B6DD4 File Offset: 0x000B4FD4
			public static Matrix4x4[] _getBindPoses(Renderer r)
			{
				if (r is SkinnedMeshRenderer)
				{
					return ((SkinnedMeshRenderer)r).sharedMesh.bindposes;
				}
				if (r is MeshRenderer)
				{
					Matrix4x4 identity = Matrix4x4.identity;
					return new Matrix4x4[]
					{
						identity
					};
				}
				Debug.LogError("Could not _getBindPoses. Object does not have a renderer");
				return null;
			}

			// Token: 0x06001BC5 RID: 7109 RVA: 0x000B6E24 File Offset: 0x000B5024
			public static BoneWeight[] _getBoneWeights(Renderer r, int numVertsInMeshBeingAdded)
			{
				if (r is SkinnedMeshRenderer)
				{
					return ((SkinnedMeshRenderer)r).sharedMesh.boneWeights;
				}
				if (r is MeshRenderer)
				{
					BoneWeight boneWeight = default(BoneWeight);
					boneWeight.boneIndex0 = (boneWeight.boneIndex1 = (boneWeight.boneIndex2 = (boneWeight.boneIndex3 = 0)));
					boneWeight.weight0 = 1f;
					boneWeight.weight1 = (boneWeight.weight2 = (boneWeight.weight3 = 0f));
					BoneWeight[] array = new BoneWeight[numVertsInMeshBeingAdded];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = boneWeight;
					}
					return array;
				}
				Debug.LogError("Could not _getBoneWeights. Object does not have a renderer");
				return null;
			}

			// Token: 0x06001BC6 RID: 7110 RVA: 0x000B6EE4 File Offset: 0x000B50E4
			private void _generateTangents(int[] triangles, Vector3[] verts, Vector2[] uvs, Vector3[] normals, Vector4[] outTangents)
			{
				int num = triangles.Length;
				int num2 = verts.Length;
				Vector3[] array = new Vector3[num2];
				Vector3[] array2 = new Vector3[num2];
				for (int i = 0; i < num; i += 3)
				{
					int num3 = triangles[i];
					int num4 = triangles[i + 1];
					int num5 = triangles[i + 2];
					Vector3 vector = verts[num3];
					Vector3 vector2 = verts[num4];
					Vector3 vector3 = verts[num5];
					Vector2 vector4 = uvs[num3];
					Vector2 vector5 = uvs[num4];
					Vector2 vector6 = uvs[num5];
					float num6 = vector2.x - vector.x;
					float num7 = vector3.x - vector.x;
					float num8 = vector2.y - vector.y;
					float num9 = vector3.y - vector.y;
					float num10 = vector2.z - vector.z;
					float num11 = vector3.z - vector.z;
					float num12 = vector5.x - vector4.x;
					float num13 = vector6.x - vector4.x;
					float num14 = vector5.y - vector4.y;
					float num15 = vector6.y - vector4.y;
					float num16 = num12 * num15 - num13 * num14;
					if (num16 == 0f)
					{
						Debug.LogError("Could not compute tangents. All UVs need to form a valid triangles in UV space. If any UV triangles are collapsed, tangents cannot be generated.");
						return;
					}
					float num17 = 1f / num16;
					Vector3 b = new Vector3((num15 * num6 - num14 * num7) * num17, (num15 * num8 - num14 * num9) * num17, (num15 * num10 - num14 * num11) * num17);
					Vector3 b2 = new Vector3((num12 * num7 - num13 * num6) * num17, (num12 * num9 - num13 * num8) * num17, (num12 * num11 - num13 * num10) * num17);
					array[num3] += b;
					array[num4] += b;
					array[num5] += b;
					array2[num3] += b2;
					array2[num4] += b2;
					array2[num5] += b2;
				}
				for (int j = 0; j < num2; j++)
				{
					Vector3 vector7 = normals[j];
					Vector3 vector8 = array[j];
					Vector3 normalized = (vector8 - vector7 * Vector3.Dot(vector7, vector8)).normalized;
					outTangents[j] = new Vector4(normalized.x, normalized.y, normalized.z);
					outTangents[j].w = ((Vector3.Dot(Vector3.Cross(vector7, vector8), array2[j]) < 0f) ? -1f : 1f);
				}
			}

			// Token: 0x04001D11 RID: 7441
			private MB3_MeshCombinerSingle mc;

			// Token: 0x04001D12 RID: 7442
			protected Dictionary<int, MB3_MeshCombinerSingle.MeshChannels> meshID2MeshChannels = new Dictionary<int, MB3_MeshCombinerSingle.MeshChannels>();

			// Token: 0x04001D13 RID: 7443
			private Vector2 _HALF_UV = new Vector2(0.5f, 0.5f);
		}

		// Token: 0x02000456 RID: 1110
		public struct BoneAndBindpose
		{
			// Token: 0x06001BC7 RID: 7111 RVA: 0x00014F4F File Offset: 0x0001314F
			public BoneAndBindpose(Transform t, Matrix4x4 bp)
			{
				this.bone = t;
				this.bindPose = bp;
			}

			// Token: 0x06001BC8 RID: 7112 RVA: 0x00014F5F File Offset: 0x0001315F
			public override bool Equals(object obj)
			{
				return obj is MB3_MeshCombinerSingle.BoneAndBindpose && this.bone == ((MB3_MeshCombinerSingle.BoneAndBindpose)obj).bone && this.bindPose == ((MB3_MeshCombinerSingle.BoneAndBindpose)obj).bindPose;
			}

			// Token: 0x06001BC9 RID: 7113 RVA: 0x00014F9C File Offset: 0x0001319C
			public override int GetHashCode()
			{
				return this.bone.GetInstanceID() % int.MaxValue ^ (int)this.bindPose[0, 0];
			}

			// Token: 0x04001D14 RID: 7444
			public Transform bone;

			// Token: 0x04001D15 RID: 7445
			public Matrix4x4 bindPose;
		}
	}
}

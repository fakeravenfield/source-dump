using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	public class MB3_MultiMeshCombiner : MB3_MeshCombiner
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00014AB8 File Offset: 0x00012CB8
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x000B71E4 File Offset: 0x000B53E4
		public override MB2_LogLevel LOG_LEVEL
		{
			get
			{
				return this._LOG_LEVEL;
			}
			set
			{
				this._LOG_LEVEL = value;
				for (int i = 0; i < this.meshCombiners.Count; i++)
				{
					this.meshCombiners[i].combinedMesh.LOG_LEVEL = value;
				}
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00014AC9 File Offset: 0x00012CC9
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x000B7228 File Offset: 0x000B5428
		public override MB2_ValidationLevel validationLevel
		{
			get
			{
				return this._validationLevel;
			}
			set
			{
				this._validationLevel = value;
				for (int i = 0; i < this.meshCombiners.Count; i++)
				{
					this.meshCombiners[i].combinedMesh.validationLevel = this._validationLevel;
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00014FD5 File Offset: 0x000131D5
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x000B7270 File Offset: 0x000B5470
		public int maxVertsInMesh
		{
			get
			{
				return this._maxVertsInMesh;
			}
			set
			{
				if (this.obj2MeshCombinerMap.Count > 0)
				{
					return;
				}
				if (value < 3)
				{
					Debug.LogError("Max verts in mesh must be greater than three.");
					return;
				}
				if (value > MBVersion.MaxMeshVertexCount())
				{
					Debug.LogError("Meshes in unity cannot have more than " + MBVersion.MaxMeshVertexCount().ToString() + " vertices.");
					return;
				}
				this._maxVertsInMesh = value;
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00014FDD File Offset: 0x000131DD
		public override int GetNumObjectsInCombined()
		{
			return this.obj2MeshCombinerMap.Count;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000B72CC File Offset: 0x000B54CC
		public override int GetNumVerticesFor(GameObject go)
		{
			MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
			if (this.obj2MeshCombinerMap.TryGetValue(go.GetInstanceID(), out combinedMesh))
			{
				return combinedMesh.combinedMesh.GetNumVerticesFor(go);
			}
			return -1;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000B7300 File Offset: 0x000B5500
		public override int GetNumVerticesFor(int gameObjectID)
		{
			MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
			if (this.obj2MeshCombinerMap.TryGetValue(gameObjectID, out combinedMesh))
			{
				return combinedMesh.combinedMesh.GetNumVerticesFor(gameObjectID);
			}
			return -1;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000B7330 File Offset: 0x000B5530
		public override List<GameObject> GetObjectsInCombined()
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				list.AddRange(this.meshCombiners[i].combinedMesh.GetObjectsInCombined());
			}
			return list;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00014FEA File Offset: 0x000131EA
		public override int GetLightmapIndex()
		{
			if (this.meshCombiners.Count > 0)
			{
				return this.meshCombiners[0].combinedMesh.GetLightmapIndex();
			}
			return -1;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00015012 File Offset: 0x00013212
		public override bool CombinedMeshContains(GameObject go)
		{
			return this.obj2MeshCombinerMap.ContainsKey(go.GetInstanceID());
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000B1B44 File Offset: 0x000AFD44
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

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000B7378 File Offset: 0x000B5578
		public override void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].isDirty)
				{
					this.meshCombiners[i].combinedMesh.Apply(uv2GenerationMethod);
					this.meshCombiners[i].isDirty = false;
				}
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000B73D8 File Offset: 0x000B55D8
		public override void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool uv2, bool uv3, bool uv4, bool colors, bool bones = false, bool blendShapesFlag = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].isDirty)
				{
					this.meshCombiners[i].combinedMesh.Apply(triangles, vertices, normals, tangents, uvs, uv2, uv3, uv4, colors, bones, blendShapesFlag, uv2GenerationMethod);
					this.meshCombiners[i].isDirty = false;
				}
			}
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000B744C File Offset: 0x000B564C
		public override void UpdateSkinnedMeshApproximateBounds()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBounds();
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000B7488 File Offset: 0x000B5688
		public override void UpdateSkinnedMeshApproximateBoundsFromBones()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBones();
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000B74C4 File Offset: 0x000B56C4
		public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBounds();
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000B7500 File Offset: 0x000B5700
		public override bool UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV2 = false, bool updateUV3 = false, bool updateUV4 = false, bool updateColors = false, bool updateSkinningInfo = false)
		{
			if (gos == null)
			{
				Debug.LogError("list of game objects cannot be null");
				return false;
			}
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].gosToUpdate.Clear();
			}
			for (int j = 0; j < gos.Length; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
				this.obj2MeshCombinerMap.TryGetValue(gos[j].GetInstanceID(), out combinedMesh);
				if (combinedMesh != null)
				{
					combinedMesh.gosToUpdate.Add(gos[j]);
				}
				else
				{
					string str = "Object ";
					GameObject gameObject = gos[j];
					Debug.LogWarning(str + ((gameObject != null) ? gameObject.ToString() : null) + " is not in the combined mesh.");
				}
			}
			bool flag = true;
			for (int k = 0; k < this.meshCombiners.Count; k++)
			{
				if (this.meshCombiners[k].gosToUpdate.Count > 0)
				{
					this.meshCombiners[k].isDirty = true;
					GameObject[] gos2 = this.meshCombiners[k].gosToUpdate.ToArray();
					flag = (flag && this.meshCombiners[k].combinedMesh.UpdateGameObjects(gos2, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV2, updateUV3, updateUV4, updateColors, updateSkinningInfo));
				}
			}
			return flag;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000B4CB8 File Offset: 0x000B2EB8
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

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000B763C File Offset: 0x000B583C
		public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource = true)
		{
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
			if (!this._validate(gos, deleteGOinstanceIDs))
			{
				return false;
			}
			this._distributeAmongBakers(gos, deleteGOinstanceIDs);
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug(string.Concat(new string[]
				{
					"MB2_MultiMeshCombiner.AddDeleteGameObjects numCombinedMeshes: ",
					this.meshCombiners.Count.ToString(),
					" added:",
					(gos != null) ? gos.ToString() : null,
					" deleted:",
					(deleteGOinstanceIDs != null) ? deleteGOinstanceIDs.ToString() : null,
					" disableRendererInSource:",
					disableRendererInSource.ToString(),
					" maxVertsPerCombined:",
					this._maxVertsInMesh.ToString()
				}), Array.Empty<object>());
			}
			return this._bakeStep1(gos, deleteGOinstanceIDs, disableRendererInSource);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000B775C File Offset: 0x000B595C
		private bool _validate(GameObject[] gos, int[] deleteGOinstanceIDs)
		{
			if (this._validationLevel == MB2_ValidationLevel.none)
			{
				return true;
			}
			if (this._maxVertsInMesh < 3)
			{
				Debug.LogError("Invalid value for maxVertsInMesh=" + this._maxVertsInMesh.ToString());
			}
			this._validateTextureBakeResults();
			if (gos != null)
			{
				for (int i = 0; i < gos.Length; i++)
				{
					if (gos[i] == null)
					{
						Debug.LogError("The " + i.ToString() + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element.");
						return false;
					}
					if (this._validationLevel >= MB2_ValidationLevel.robust)
					{
						for (int j = i + 1; j < gos.Length; j++)
						{
							if (gos[i] == gos[j])
							{
								string str = "GameObject ";
								GameObject gameObject = gos[i];
								Debug.LogError(str + ((gameObject != null) ? gameObject.ToString() : null) + "appears twice in list of game objects to add");
								return false;
							}
						}
						if (this.obj2MeshCombinerMap.ContainsKey(gos[i].GetInstanceID()))
						{
							bool flag = false;
							if (deleteGOinstanceIDs != null)
							{
								for (int k = 0; k < deleteGOinstanceIDs.Length; k++)
								{
									if (deleteGOinstanceIDs[k] == gos[i].GetInstanceID())
									{
										flag = true;
									}
								}
							}
							if (!flag)
							{
								string str2 = "GameObject ";
								GameObject gameObject2 = gos[i];
								Debug.LogError(str2 + ((gameObject2 != null) ? gameObject2.ToString() : null) + " is already in the combined mesh " + gos[i].GetInstanceID().ToString());
								return false;
							}
						}
					}
				}
			}
			if (deleteGOinstanceIDs != null && this._validationLevel >= MB2_ValidationLevel.robust)
			{
				for (int l = 0; l < deleteGOinstanceIDs.Length; l++)
				{
					for (int m = l + 1; m < deleteGOinstanceIDs.Length; m++)
					{
						if (deleteGOinstanceIDs[l] == deleteGOinstanceIDs[m])
						{
							Debug.LogError("GameObject " + deleteGOinstanceIDs[l].ToString() + "appears twice in list of game objects to delete");
							return false;
						}
					}
					if (!this.obj2MeshCombinerMap.ContainsKey(deleteGOinstanceIDs[l]))
					{
						Debug.LogWarning("GameObject with instance ID " + deleteGOinstanceIDs[l].ToString() + " on the list of objects to delete is not in the combined mesh.");
					}
				}
			}
			return true;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000B793C File Offset: 0x000B5B3C
		private void _distributeAmongBakers(GameObject[] gos, int[] deleteGOinstanceIDs)
		{
			if (gos == null)
			{
				gos = MB3_MultiMeshCombiner.empty;
			}
			if (deleteGOinstanceIDs == null)
			{
				deleteGOinstanceIDs = MB3_MultiMeshCombiner.emptyIDs;
			}
			if (this.resultSceneObject == null)
			{
				this.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
			}
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].extraSpace = this._maxVertsInMesh - this.meshCombiners[i].combinedMesh.GetMesh().vertexCount;
			}
			for (int j = 0; j < deleteGOinstanceIDs.Length; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
				if (this.obj2MeshCombinerMap.TryGetValue(deleteGOinstanceIDs[j], out combinedMesh))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("MB2_MultiMeshCombiner.Removing " + deleteGOinstanceIDs[j].ToString() + " from meshCombiner " + this.meshCombiners.IndexOf(combinedMesh).ToString(), Array.Empty<object>());
					}
					combinedMesh.numVertsInListToDelete += combinedMesh.combinedMesh.GetNumVerticesFor(deleteGOinstanceIDs[j]);
					combinedMesh.gosToDelete.Add(deleteGOinstanceIDs[j]);
				}
				else
				{
					Debug.LogWarning("Object " + deleteGOinstanceIDs[j].ToString() + " in the list of objects to delete is not in the combined mesh.");
				}
			}
			for (int k = 0; k < gos.Length; k++)
			{
				GameObject gameObject = gos[k];
				int vertexCount = MB_Utility.GetMesh(gameObject).vertexCount;
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh2 = null;
				int l = 0;
				while (l < this.meshCombiners.Count)
				{
					if (this.meshCombiners[l].extraSpace + this.meshCombiners[l].numVertsInListToDelete - this.meshCombiners[l].numVertsInListToAdd > vertexCount)
					{
						combinedMesh2 = this.meshCombiners[l];
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							string str = "MB2_MultiMeshCombiner.Added ";
							GameObject gameObject2 = gos[k];
							MB2_Log.LogDebug(str + ((gameObject2 != null) ? gameObject2.ToString() : null) + " to combinedMesh " + l.ToString(), new object[]
							{
								this.LOG_LEVEL
							});
							break;
						}
						break;
					}
					else
					{
						l++;
					}
				}
				if (combinedMesh2 == null)
				{
					combinedMesh2 = new MB3_MultiMeshCombiner.CombinedMesh(this.maxVertsInMesh, this._resultSceneObject, this._LOG_LEVEL);
					this._setMBValues(combinedMesh2.combinedMesh);
					this.meshCombiners.Add(combinedMesh2);
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("MB2_MultiMeshCombiner.Created new combinedMesh", Array.Empty<object>());
					}
				}
				combinedMesh2.gosToAdd.Add(gameObject);
				combinedMesh2.numVertsInListToAdd += vertexCount;
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000B7BD8 File Offset: 0x000B5DD8
		private bool _bakeStep1(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = this.meshCombiners[i];
				if (combinedMesh.combinedMesh.targetRenderer == null)
				{
					combinedMesh.combinedMesh.resultSceneObject = this._resultSceneObject;
					combinedMesh.combinedMesh.BuildSceneMeshObject(gos, true);
					if (this._LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("BuildSO combiner {0} goID {1} targetRenID {2} meshID {3}", new object[]
						{
							i,
							combinedMesh.combinedMesh.targetRenderer.gameObject.GetInstanceID(),
							combinedMesh.combinedMesh.targetRenderer.GetInstanceID(),
							combinedMesh.combinedMesh.GetMesh().GetInstanceID()
						});
					}
				}
				else if (combinedMesh.combinedMesh.targetRenderer.transform.parent != this.resultSceneObject.transform)
				{
					Debug.LogError("targetRender objects must be children of resultSceneObject");
					return false;
				}
				if (combinedMesh.gosToAdd.Count > 0 || combinedMesh.gosToDelete.Count > 0)
				{
					combinedMesh.combinedMesh.AddDeleteGameObjectsByID(combinedMesh.gosToAdd.ToArray(), combinedMesh.gosToDelete.ToArray(), disableRendererInSource);
					if (this._LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Baked combiner {0} obsAdded {1} objsRemoved {2} goID {3} targetRenID {4} meshID {5}", new object[]
						{
							i,
							combinedMesh.gosToAdd.Count,
							combinedMesh.gosToDelete.Count,
							combinedMesh.combinedMesh.targetRenderer.gameObject.GetInstanceID(),
							combinedMesh.combinedMesh.targetRenderer.GetInstanceID(),
							combinedMesh.combinedMesh.GetMesh().GetInstanceID()
						});
					}
				}
				Renderer targetRenderer = combinedMesh.combinedMesh.targetRenderer;
				Mesh mesh = combinedMesh.combinedMesh.GetMesh();
				if (targetRenderer is MeshRenderer)
				{
					targetRenderer.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
				}
				else
				{
					((SkinnedMeshRenderer)targetRenderer).sharedMesh = mesh;
				}
			}
			for (int j = 0; j < this.meshCombiners.Count; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh2 = this.meshCombiners[j];
				for (int k = 0; k < combinedMesh2.gosToDelete.Count; k++)
				{
					this.obj2MeshCombinerMap.Remove(combinedMesh2.gosToDelete[k]);
				}
			}
			for (int l = 0; l < this.meshCombiners.Count; l++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh3 = this.meshCombiners[l];
				for (int m = 0; m < combinedMesh3.gosToAdd.Count; m++)
				{
					this.obj2MeshCombinerMap.Add(combinedMesh3.gosToAdd[m].GetInstanceID(), combinedMesh3);
				}
				if (combinedMesh3.gosToAdd.Count > 0 || combinedMesh3.gosToDelete.Count > 0)
				{
					combinedMesh3.gosToDelete.Clear();
					combinedMesh3.gosToAdd.Clear();
					combinedMesh3.numVertsInListToDelete = 0;
					combinedMesh3.numVertsInListToAdd = 0;
					combinedMesh3.isDirty = true;
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				string text = "Meshes in combined:";
				for (int n = 0; n < this.meshCombiners.Count; n++)
				{
					text = string.Concat(new string[]
					{
						text,
						" mesh",
						n.ToString(),
						"(",
						this.meshCombiners[n].combinedMesh.GetObjectsInCombined().Count.ToString(),
						")\n"
					});
				}
				text = text + "children in result: " + this.resultSceneObject.transform.childCount.ToString();
				MB2_Log.LogDebug(text, new object[]
				{
					this.LOG_LEVEL
				});
			}
			return this.meshCombiners.Count > 0;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000B7FF8 File Offset: 0x000B61F8
		public override Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap()
		{
			Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> dictionary = new Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue>();
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				for (int j = 0; j < this.meshCombiners[i].combinedMesh.blendShapes.Length; j++)
				{
					MB3_MeshCombinerSingle.MBBlendShape mbblendShape = this.meshCombiners[i].combinedMesh.blendShapes[j];
					MB3_MeshCombiner.MBBlendShapeValue mbblendShapeValue = new MB3_MeshCombiner.MBBlendShapeValue();
					mbblendShapeValue.combinedMeshGameObject = this.meshCombiners[i].combinedMesh.targetRenderer.gameObject;
					mbblendShapeValue.blendShapeIndex = j;
					dictionary.Add(new MB3_MeshCombiner.MBBlendShapeKey(mbblendShape.gameObjectID, mbblendShape.indexInSource), mbblendShapeValue);
				}
			}
			return dictionary;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000B80B4 File Offset: 0x000B62B4
		public override void ClearBuffers()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.ClearBuffers();
			}
			this.obj2MeshCombinerMap.Clear();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00015025 File Offset: 0x00013225
		public override void ClearMesh()
		{
			this.DestroyMesh();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000B80F8 File Offset: 0x000B62F8
		public override void DisposeRuntimeCreated()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.DisposeRuntimeCreated();
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000B8134 File Offset: 0x000B6334
		public override void DestroyMesh()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].combinedMesh.targetRenderer != null)
				{
					MB_Utility.Destroy(this.meshCombiners[i].combinedMesh.targetRenderer.gameObject);
				}
				this.meshCombiners[i].combinedMesh.DestroyMesh();
			}
			this.obj2MeshCombinerMap.Clear();
			this.meshCombiners.Clear();
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000B81C4 File Offset: 0x000B63C4
		public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].combinedMesh.targetRenderer != null)
				{
					editorMethods.Destroy(this.meshCombiners[i].combinedMesh.targetRenderer.gameObject);
				}
				this.meshCombiners[i].combinedMesh.ClearMesh();
			}
			this.obj2MeshCombinerMap.Clear();
			this.meshCombiners.Clear();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000B8254 File Offset: 0x000B6454
		private void _setMBValues(MB3_MeshCombinerSingle targ)
		{
			targ.validationLevel = this._validationLevel;
			targ.renderType = this.renderType;
			targ.outputOption = MB2_OutputOptions.bakeIntoSceneObject;
			targ.lightmapOption = this.lightmapOption;
			targ.textureBakeResults = this.textureBakeResults;
			targ.doNorm = this.doNorm;
			targ.doTan = this.doTan;
			targ.doCol = this.doCol;
			targ.doUV = this.doUV;
			targ.doUV3 = this.doUV3;
			targ.doUV4 = this.doUV4;
			targ.doBlendShapes = this.doBlendShapes;
			targ.optimizeAfterBake = base.optimizeAfterBake;
			targ.recenterVertsToBoundsCenter = this.recenterVertsToBoundsCenter;
			targ.uv2UnwrappingParamsHardAngle = this.uv2UnwrappingParamsHardAngle;
			targ.uv2UnwrappingParamsPackMargin = this.uv2UnwrappingParamsPackMargin;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000B831C File Offset: 0x000B651C
		public override List<Material> GetMaterialsOnTargetRenderer()
		{
			HashSet<Material> hashSet = new HashSet<Material>();
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				hashSet.UnionWith(this.meshCombiners[i].combinedMesh.GetMaterialsOnTargetRenderer());
			}
			return new List<Material>(hashSet);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000B8368 File Offset: 0x000B6568
		public override void CheckIntegrity()
		{
			if (!MB_Utility.DO_INTEGRITY_CHECKS)
			{
				return;
			}
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.CheckIntegrity();
			}
		}

		// Token: 0x04001D19 RID: 7449
		private static GameObject[] empty = new GameObject[0];

		// Token: 0x04001D1A RID: 7450
		private static int[] emptyIDs = new int[0];

		// Token: 0x04001D1B RID: 7451
		public Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh> obj2MeshCombinerMap = new Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh>();

		// Token: 0x04001D1C RID: 7452
		[SerializeField]
		public List<MB3_MultiMeshCombiner.CombinedMesh> meshCombiners = new List<MB3_MultiMeshCombiner.CombinedMesh>();

		// Token: 0x04001D1D RID: 7453
		[SerializeField]
		private int _maxVertsInMesh = 65535;

		// Token: 0x02000459 RID: 1113
		[Serializable]
		public class CombinedMesh
		{
			// Token: 0x06001BEF RID: 7151 RVA: 0x000B83AC File Offset: 0x000B65AC
			public CombinedMesh(int maxNumVertsInMesh, GameObject resultSceneObject, MB2_LogLevel ll)
			{
				this.combinedMesh = new MB3_MeshCombinerSingle();
				this.combinedMesh.resultSceneObject = resultSceneObject;
				this.combinedMesh.LOG_LEVEL = ll;
				this.extraSpace = maxNumVertsInMesh;
				this.numVertsInListToDelete = 0;
				this.numVertsInListToAdd = 0;
				this.gosToAdd = new List<GameObject>();
				this.gosToDelete = new List<int>();
				this.gosToUpdate = new List<GameObject>();
			}

			// Token: 0x06001BF0 RID: 7152 RVA: 0x000B8420 File Offset: 0x000B6620
			public bool isEmpty()
			{
				List<GameObject> list = new List<GameObject>();
				list.AddRange(this.combinedMesh.GetObjectsInCombined());
				for (int i = 0; i < this.gosToDelete.Count; i++)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].GetInstanceID() == this.gosToDelete[i])
						{
							list.RemoveAt(j);
							break;
						}
					}
				}
				return list.Count == 0;
			}

			// Token: 0x04001D1E RID: 7454
			public MB3_MeshCombinerSingle combinedMesh;

			// Token: 0x04001D1F RID: 7455
			public int extraSpace = -1;

			// Token: 0x04001D20 RID: 7456
			public int numVertsInListToDelete;

			// Token: 0x04001D21 RID: 7457
			public int numVertsInListToAdd;

			// Token: 0x04001D22 RID: 7458
			public List<GameObject> gosToAdd;

			// Token: 0x04001D23 RID: 7459
			public List<int> gosToDelete;

			// Token: 0x04001D24 RID: 7460
			public List<GameObject> gosToUpdate;

			// Token: 0x04001D25 RID: 7461
			public bool isDirty;
		}
	}
}

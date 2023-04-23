using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200044A RID: 1098
	[Serializable]
	public abstract class MB3_MeshCombiner
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0000257D File Offset: 0x0000077D
		public static bool EVAL_VERSION
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x00014AB8 File Offset: 0x00012CB8
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public virtual MB2_LogLevel LOG_LEVEL
		{
			get
			{
				return this._LOG_LEVEL;
			}
			set
			{
				this._LOG_LEVEL = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x00014AC9 File Offset: 0x00012CC9
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00014AD1 File Offset: 0x00012CD1
		public virtual MB2_ValidationLevel validationLevel
		{
			get
			{
				return this._validationLevel;
			}
			set
			{
				this._validationLevel = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00014ADA File Offset: 0x00012CDA
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x00014AE2 File Offset: 0x00012CE2
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x00014AEB File Offset: 0x00012CEB
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x00014AF3 File Offset: 0x00012CF3
		public virtual MB2_TextureBakeResults textureBakeResults
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00014AFC File Offset: 0x00012CFC
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x00014B04 File Offset: 0x00012D04
		public virtual GameObject resultSceneObject
		{
			get
			{
				return this._resultSceneObject;
			}
			set
			{
				this._resultSceneObject = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00014B0D File Offset: 0x00012D0D
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x00014B15 File Offset: 0x00012D15
		public virtual Renderer targetRenderer
		{
			get
			{
				return this._targetRenderer;
			}
			set
			{
				if (this._targetRenderer != null && this._targetRenderer != value)
				{
					Debug.LogWarning("Previous targetRenderer was not null. Combined mesh may be being used by more than one Renderer");
				}
				this._targetRenderer = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00014B44 File Offset: 0x00012D44
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00014B4C File Offset: 0x00012D4C
		public virtual MB_RenderType renderType
		{
			get
			{
				return this._renderType;
			}
			set
			{
				this._renderType = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00014B55 File Offset: 0x00012D55
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00014B5D File Offset: 0x00012D5D
		public virtual MB2_OutputOptions outputOption
		{
			get
			{
				return this._outputOption;
			}
			set
			{
				this._outputOption = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00014B66 File Offset: 0x00012D66
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x00014B6E File Offset: 0x00012D6E
		public virtual MB2_LightmapOptions lightmapOption
		{
			get
			{
				return this._lightmapOption;
			}
			set
			{
				this._lightmapOption = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00014B77 File Offset: 0x00012D77
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x00014B7F File Offset: 0x00012D7F
		public virtual bool doNorm
		{
			get
			{
				return this._doNorm;
			}
			set
			{
				this._doNorm = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00014B88 File Offset: 0x00012D88
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x00014B90 File Offset: 0x00012D90
		public virtual bool doTan
		{
			get
			{
				return this._doTan;
			}
			set
			{
				this._doTan = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00014B99 File Offset: 0x00012D99
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x00014BA1 File Offset: 0x00012DA1
		public virtual bool doCol
		{
			get
			{
				return this._doCol;
			}
			set
			{
				this._doCol = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00014BAA File Offset: 0x00012DAA
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00014BB2 File Offset: 0x00012DB2
		public virtual bool doUV
		{
			get
			{
				return this._doUV;
			}
			set
			{
				this._doUV = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0000257D File Offset: 0x0000077D
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual bool doUV1
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00014BBB File Offset: 0x00012DBB
		public virtual bool doUV2()
		{
			return this._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged || this._lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping || this._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged_to_separate_rects;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00014BD9 File Offset: 0x00012DD9
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00014BE1 File Offset: 0x00012DE1
		public virtual bool doUV3
		{
			get
			{
				return this._doUV3;
			}
			set
			{
				this._doUV3 = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00014BEA File Offset: 0x00012DEA
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x00014BF2 File Offset: 0x00012DF2
		public virtual bool doUV4
		{
			get
			{
				return this._doUV4;
			}
			set
			{
				this._doUV4 = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00014BFB File Offset: 0x00012DFB
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x00014C03 File Offset: 0x00012E03
		public virtual bool doBlendShapes
		{
			get
			{
				return this._doBlendShapes;
			}
			set
			{
				this._doBlendShapes = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00014C0C File Offset: 0x00012E0C
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x00014C14 File Offset: 0x00012E14
		public virtual bool recenterVertsToBoundsCenter
		{
			get
			{
				return this._recenterVertsToBoundsCenter;
			}
			set
			{
				this._recenterVertsToBoundsCenter = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x00014C1D File Offset: 0x00012E1D
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x00014C25 File Offset: 0x00012E25
		public bool optimizeAfterBake
		{
			get
			{
				return this._optimizeAfterBake;
			}
			set
			{
				this._optimizeAfterBake = value;
			}
		}

		// Token: 0x06001B42 RID: 6978
		public abstract int GetLightmapIndex();

		// Token: 0x06001B43 RID: 6979
		public abstract void ClearBuffers();

		// Token: 0x06001B44 RID: 6980
		public abstract void ClearMesh();

		// Token: 0x06001B45 RID: 6981
		public abstract void DisposeRuntimeCreated();

		// Token: 0x06001B46 RID: 6982
		public abstract void DestroyMesh();

		// Token: 0x06001B47 RID: 6983
		public abstract void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods);

		// Token: 0x06001B48 RID: 6984
		public abstract List<GameObject> GetObjectsInCombined();

		// Token: 0x06001B49 RID: 6985
		public abstract int GetNumObjectsInCombined();

		// Token: 0x06001B4A RID: 6986
		public abstract int GetNumVerticesFor(GameObject go);

		// Token: 0x06001B4B RID: 6987
		public abstract int GetNumVerticesFor(int instanceID);

		// Token: 0x06001B4C RID: 6988
		public abstract Dictionary<MB3_MeshCombiner.MBBlendShapeKey, MB3_MeshCombiner.MBBlendShapeValue> BuildSourceBlendShapeToCombinedIndexMap();

		// Token: 0x06001B4D RID: 6989 RVA: 0x00014C2E File Offset: 0x00012E2E
		public virtual void Apply()
		{
			this.Apply(null);
		}

		// Token: 0x06001B4E RID: 6990
		public abstract void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod);

		// Token: 0x06001B4F RID: 6991
		public abstract void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool uv2, bool uv3, bool uv4, bool colors, bool bones = false, bool blendShapeFlag = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null);

		// Token: 0x06001B50 RID: 6992
		public abstract bool UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV2 = false, bool updateUV3 = false, bool updateUV4 = false, bool updateColors = false, bool updateSkinningInfo = false);

		// Token: 0x06001B51 RID: 6993
		public abstract bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true);

		// Token: 0x06001B52 RID: 6994
		public abstract bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource);

		// Token: 0x06001B53 RID: 6995
		public abstract bool CombinedMeshContains(GameObject go);

		// Token: 0x06001B54 RID: 6996
		public abstract void UpdateSkinnedMeshApproximateBounds();

		// Token: 0x06001B55 RID: 6997
		public abstract void UpdateSkinnedMeshApproximateBoundsFromBones();

		// Token: 0x06001B56 RID: 6998
		public abstract void CheckIntegrity();

		// Token: 0x06001B57 RID: 6999
		public abstract void UpdateSkinnedMeshApproximateBoundsFromBounds();

		// Token: 0x06001B58 RID: 7000 RVA: 0x000B1248 File Offset: 0x000AF448
		public static void UpdateSkinnedMeshApproximateBoundsFromBonesStatic(Transform[] bs, SkinnedMeshRenderer smr)
		{
			Vector3 position = bs[0].position;
			Vector3 position2 = bs[0].position;
			for (int i = 1; i < bs.Length; i++)
			{
				Vector3 position3 = bs[i].position;
				if (position3.x < position2.x)
				{
					position2.x = position3.x;
				}
				if (position3.y < position2.y)
				{
					position2.y = position3.y;
				}
				if (position3.z < position2.z)
				{
					position2.z = position3.z;
				}
				if (position3.x > position.x)
				{
					position.x = position3.x;
				}
				if (position3.y > position.y)
				{
					position.y = position3.y;
				}
				if (position3.z > position.z)
				{
					position.z = position3.z;
				}
			}
			Vector3 v = (position + position2) / 2f;
			Vector3 v2 = position - position2;
			Matrix4x4 worldToLocalMatrix = smr.worldToLocalMatrix;
			Bounds localBounds = new Bounds(worldToLocalMatrix * v, worldToLocalMatrix * v2);
			smr.localBounds = localBounds;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000B1390 File Offset: 0x000AF590
		public static void UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(List<GameObject> objectsInCombined, SkinnedMeshRenderer smr)
		{
			Bounds bounds = default(Bounds);
			Bounds localBounds = default(Bounds);
			if (MB_Utility.GetBounds(objectsInCombined[0], out bounds))
			{
				localBounds = bounds;
				for (int i = 1; i < objectsInCombined.Count; i++)
				{
					if (!MB_Utility.GetBounds(objectsInCombined[i], out bounds))
					{
						Debug.LogError("Could not get bounds. Not updating skinned mesh bounds");
						return;
					}
					localBounds.Encapsulate(bounds);
				}
				smr.localBounds = localBounds;
				return;
			}
			Debug.LogError("Could not get bounds. Not updating skinned mesh bounds");
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00014C37 File Offset: 0x00012E37
		protected virtual bool _CreateTemporaryTextrueBakeResult(GameObject[] gos, List<Material> matsOnTargetRenderer)
		{
			if (this.GetNumObjectsInCombined() > 0)
			{
				Debug.LogError("Can't add objects if there are already objects in combined mesh when 'Texture Bake Result' is not set. Perhaps enable 'Clear Buffers After Bake'");
				return false;
			}
			this._usingTemporaryTextureBakeResult = true;
			this._textureBakeResults = MB2_TextureBakeResults.CreateForMaterialsOnRenderer(gos, matsOnTargetRenderer);
			return true;
		}

		// Token: 0x06001B5B RID: 7003
		public abstract List<Material> GetMaterialsOnTargetRenderer();

		// Token: 0x04001CAB RID: 7339
		[SerializeField]
		protected MB2_LogLevel _LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x04001CAC RID: 7340
		[SerializeField]
		protected MB2_ValidationLevel _validationLevel = MB2_ValidationLevel.robust;

		// Token: 0x04001CAD RID: 7341
		[SerializeField]
		protected string _name;

		// Token: 0x04001CAE RID: 7342
		[SerializeField]
		protected MB2_TextureBakeResults _textureBakeResults;

		// Token: 0x04001CAF RID: 7343
		[SerializeField]
		protected GameObject _resultSceneObject;

		// Token: 0x04001CB0 RID: 7344
		[SerializeField]
		protected Renderer _targetRenderer;

		// Token: 0x04001CB1 RID: 7345
		[SerializeField]
		protected MB_RenderType _renderType;

		// Token: 0x04001CB2 RID: 7346
		[SerializeField]
		protected MB2_OutputOptions _outputOption;

		// Token: 0x04001CB3 RID: 7347
		[SerializeField]
		protected MB2_LightmapOptions _lightmapOption = MB2_LightmapOptions.ignore_UV2;

		// Token: 0x04001CB4 RID: 7348
		[SerializeField]
		protected bool _doNorm = true;

		// Token: 0x04001CB5 RID: 7349
		[SerializeField]
		protected bool _doTan = true;

		// Token: 0x04001CB6 RID: 7350
		[SerializeField]
		protected bool _doCol;

		// Token: 0x04001CB7 RID: 7351
		[SerializeField]
		protected bool _doUV = true;

		// Token: 0x04001CB8 RID: 7352
		[SerializeField]
		protected bool _doUV3;

		// Token: 0x04001CB9 RID: 7353
		[SerializeField]
		protected bool _doUV4;

		// Token: 0x04001CBA RID: 7354
		[SerializeField]
		protected bool _doBlendShapes;

		// Token: 0x04001CBB RID: 7355
		[SerializeField]
		protected bool _recenterVertsToBoundsCenter;

		// Token: 0x04001CBC RID: 7356
		[SerializeField]
		public bool _optimizeAfterBake = true;

		// Token: 0x04001CBD RID: 7357
		[SerializeField]
		public float uv2UnwrappingParamsHardAngle = 60f;

		// Token: 0x04001CBE RID: 7358
		[SerializeField]
		public float uv2UnwrappingParamsPackMargin = 0.005f;

		// Token: 0x04001CBF RID: 7359
		protected bool _usingTemporaryTextureBakeResult;

		// Token: 0x0200044B RID: 1099
		// (Invoke) Token: 0x06001B5E RID: 7006
		public delegate void GenerateUV2Delegate(Mesh m, float hardAngle, float packMargin);

		// Token: 0x0200044C RID: 1100
		public class MBBlendShapeKey
		{
			// Token: 0x06001B61 RID: 7009 RVA: 0x00014C63 File Offset: 0x00012E63
			public MBBlendShapeKey(int srcSkinnedMeshRenderGameObjectID, int blendShapeIndexInSource)
			{
				this.gameObjecID = srcSkinnedMeshRenderGameObjectID;
				this.blendShapeIndexInSrc = blendShapeIndexInSource;
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x000B1468 File Offset: 0x000AF668
			public override bool Equals(object obj)
			{
				if (!(obj is MB3_MeshCombiner.MBBlendShapeKey) || obj == null)
				{
					return false;
				}
				MB3_MeshCombiner.MBBlendShapeKey mbblendShapeKey = (MB3_MeshCombiner.MBBlendShapeKey)obj;
				return this.gameObjecID == mbblendShapeKey.gameObjecID && this.blendShapeIndexInSrc == mbblendShapeKey.blendShapeIndexInSrc;
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x00014C79 File Offset: 0x00012E79
			public override int GetHashCode()
			{
				return (23 * 31 + this.gameObjecID) * 31 + this.blendShapeIndexInSrc;
			}

			// Token: 0x04001CC0 RID: 7360
			public int gameObjecID;

			// Token: 0x04001CC1 RID: 7361
			public int blendShapeIndexInSrc;
		}

		// Token: 0x0200044D RID: 1101
		public class MBBlendShapeValue
		{
			// Token: 0x04001CC2 RID: 7362
			public GameObject combinedMeshGameObject;

			// Token: 0x04001CC3 RID: 7363
			public int blendShapeIndex;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200054E RID: 1358
	[SelectionBase]
	[ExecuteInEditMode]
	public class MapMagic : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06002232 RID: 8754 RVA: 0x000DB0A0 File Offset: 0x000D92A0
		// (remove) Token: 0x06002233 RID: 8755 RVA: 0x000DB0D4 File Offset: 0x000D92D4
		public static event MapMagic.ApplyEvent OnApply;

		// Token: 0x06002234 RID: 8756 RVA: 0x000180E5 File Offset: 0x000162E5
		public static void CallOnApply(Terrain terrain, object obj)
		{
			if (MapMagic.OnApply != null)
			{
				MapMagic.OnApply(terrain, obj);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06002235 RID: 8757 RVA: 0x000DB108 File Offset: 0x000D9308
		// (remove) Token: 0x06002236 RID: 8758 RVA: 0x000DB13C File Offset: 0x000D933C
		public static event MapMagic.ChangeEvent OnGenerateStarted;

		// Token: 0x06002237 RID: 8759 RVA: 0x000180FA File Offset: 0x000162FA
		public static void CallOnGenerateStarted(Terrain terrain)
		{
			if (MapMagic.OnGenerateStarted != null)
			{
				MapMagic.OnGenerateStarted(terrain);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06002238 RID: 8760 RVA: 0x000DB170 File Offset: 0x000D9370
		// (remove) Token: 0x06002239 RID: 8761 RVA: 0x000DB1A4 File Offset: 0x000D93A4
		public static event MapMagic.ChangeEvent OnGenerateCompleted;

		// Token: 0x0600223A RID: 8762 RVA: 0x0001810E File Offset: 0x0001630E
		public static void CallOnGenerateCompleted(Terrain terrain)
		{
			if (MapMagic.OnGenerateCompleted != null)
			{
				MapMagic.OnGenerateCompleted(terrain);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600223B RID: 8763 RVA: 0x000DB1D8 File Offset: 0x000D93D8
		// (remove) Token: 0x0600223C RID: 8764 RVA: 0x000DB20C File Offset: 0x000D940C
		public static event MapMagic.ChangeEvent OnApplyCompleted;

		// Token: 0x0600223D RID: 8765 RVA: 0x00018122 File Offset: 0x00016322
		public static void CallOnApplyCompleted(Terrain terrain)
		{
			if (MapMagic.OnApplyCompleted != null)
			{
				MapMagic.OnApplyCompleted(terrain);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600223E RID: 8766 RVA: 0x000DB240 File Offset: 0x000D9440
		// (remove) Token: 0x0600223F RID: 8767 RVA: 0x000DB274 File Offset: 0x000D9474
		public static event MapMagic.RepaintWindowAction RepaintWindow;

		// Token: 0x06002240 RID: 8768 RVA: 0x00018136 File Offset: 0x00016336
		public static void CallRepaintWindow()
		{
			if (MapMagic.instance.isEditor && MapMagic.RepaintWindow != null)
			{
				MapMagic.RepaintWindow();
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0000257D File Offset: 0x0000077D
		public bool isPrefab
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0000257D File Offset: 0x0000077D
		public bool isEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00018155 File Offset: 0x00016355
		public void OnEnable()
		{
			MapMagic.instance = UnityEngine.Object.FindObjectOfType<MapMagic>();
			this.terrains.CheckEmpty();
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0000296E File Offset: 0x00000B6E
		public void OnDisable()
		{
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000DB2A8 File Offset: 0x000D94A8
		public void Update()
		{
			if (this.isPrefab)
			{
				return;
			}
			if (MapMagic.instance != null && MapMagic.instance != this)
			{
				Debug.LogError("MapMagic object already present in scene. Disabling duplicate");
				base.enabled = false;
				return;
			}
			if (this.gens == null)
			{
				if (this.serializer == null || this.serializer.entities == null || this.serializer.entities.Count == 0)
				{
					Debug.Log("MapMagic: Could not find the proper graph data. It the data file was changed externally reload the scene, otherwise create the new one in the General Settings tab.");
					return;
				}
				Debug.Log("MapMagic: Loading outdated scene format. Please check node consistency and re-save the scene.");
				this.LoadOldNonAssetData();
				this.serializer = null;
			}
			this.camPoses = this.GetCamPoses(this.camPoses);
			if (this.camPoses.Length == 0)
			{
				return;
			}
			if (this.guiDebug && !this.isEditor)
			{
				for (int i = 0; i < this.camPoses.Length; i++)
				{
					base.transform.TransformPoint(this.camPoses[i]).DrawDebug((float)this.generateRange, Color.yellow);
					base.transform.TransformPoint(this.camPoses[i]).DrawDebug((float)this.enableRange, Color.green);
				}
			}
			if ((float)this.terrainSize < 0.1f)
			{
				return;
			}
			if (!this.isEditor && this.generateInfinite)
			{
				this.terrains.Deploy(this.camPoses, true);
			}
			this.runningThreadsCount = 0;
			bool flag = false;
			foreach (Chunk chunk in this.terrains.Objects())
			{
				if (chunk.running)
				{
					this.runningThreadsCount++;
				}
				if (chunk.start)
				{
					flag = true;
				}
			}
			if (flag)
			{
				foreach (TextureInput textureInput in this.gens.GeneratorsOfType<TextureInput>(true, true))
				{
					textureInput.CheckLoadTexture();
				}
			}
			int count = this.terrains.Count;
			if (this.chunksArray == null || this.chunksArray.Length != count)
			{
				this.chunksArray = new Chunk[count];
			}
			if (this.distsArray == null || this.distsArray.Length != count)
			{
				this.distsArray = new float[count];
			}
			int num = 0;
			foreach (Chunk chunk2 in this.terrains.Objects())
			{
				Vector3 min = new Vector3((float)(chunk2.coord.x * MapMagic.instance.terrainSize), 0f, (float)(chunk2.coord.z * MapMagic.instance.terrainSize)) + base.transform.position;
				float num2 = 200000000f;
				for (int j = 0; j < this.camPoses.Length; j++)
				{
					float num3 = this.camPoses[j].RectangularDistToRect(min, (float)MapMagic.instance.terrainSize);
					if (num3 < num2)
					{
						num2 = num3;
					}
				}
				this.chunksArray[num] = chunk2;
				this.distsArray[num] = num2;
				num++;
			}
			Array.Sort<float, Chunk>(this.distsArray, this.chunksArray);
			for (int k = 0; k < this.chunksArray.Length; k++)
			{
				this.chunksArray[k].Update(this.distsArray[k]);
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000DB634 File Offset: 0x000D9834
		public Vector3[] GetCamPoses(Vector3[] camPoses = null)
		{
			if (this.isEditor)
			{
				camPoses = new Vector3[1];
			}
			else
			{
				GameObject[] array = null;
				if (this.genAroundObjsTag && this.genAroundTag != null && this.genAroundTag.Length != 0)
				{
					array = GameObject.FindGameObjectsWithTag(this.genAroundTag);
				}
				int num = 0;
				if (this.genAroundMainCam)
				{
					num++;
				}
				if (array != null)
				{
					num += array.Length;
				}
				if (num == 0)
				{
					Debug.LogError("No Main Camera to deploy MapMagic");
					return new Vector3[0];
				}
				if (camPoses == null || num != camPoses.Length)
				{
					camPoses = new Vector3[num];
				}
				int num2 = 0;
				if (this.genAroundMainCam)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = UnityEngine.Object.FindObjectOfType<Camera>();
					}
					camPoses[0] = camera.transform.position;
					num2++;
				}
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						camPoses[i + num2] = array[i].transform.position;
					}
				}
			}
			for (int j = 0; j < camPoses.Length; j++)
			{
				camPoses[j] = base.transform.InverseTransformPoint(camPoses[j]);
			}
			return camPoses;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000DB750 File Offset: 0x000D9950
		public void OnDrawGizmos()
		{
			if (this.previewOutput != null)
			{
				foreach (Chunk chunk in this.terrains.Objects())
				{
					chunk.Preview(false);
				}
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000DB7A8 File Offset: 0x000D99A8
		public void ForceGenerate()
		{
			foreach (Chunk chunk in this.terrains.Objects())
			{
				chunk.stop = true;
				chunk.results.Clear();
				chunk.ready.Clear();
			}
			this.applyRunning = false;
			this.terrains.start = true;
			this.Update();
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0001816C File Offset: 0x0001636C
		public void Generate()
		{
			this.terrains.start = true;
			this.Update();
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00018180 File Offset: 0x00016380
		public IEnumerable<float> GenerateAsync()
		{
			this.Generate();
			while (this.terrains.start)
			{
				yield return 0f;
			}
			while (!this.terrains.complete)
			{
				yield return this.terrains.progress;
			}
			yield return 1f;
			yield break;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000DB828 File Offset: 0x000D9A28
		public void LoadOldNonAssetData()
		{
			this.serializer.ClearLinks();
			MapMagic.GeneratorsList generatorsList = new MapMagic.GeneratorsList();
			generatorsList = (MapMagic.GeneratorsList)this.serializer.Retrieve(0);
			this.serializer.ClearLinks();
			this.gens = ScriptableObject.CreateInstance<GeneratorsAsset>();
			this.gens.list = generatorsList.list;
		}

		// Token: 0x040021F2 RID: 8690
		public static readonly int version = 15;

		// Token: 0x040021F3 RID: 8691
		public TerrainGrid terrains = new TerrainGrid();

		// Token: 0x040021F4 RID: 8692
		public GeneratorsAsset gens;

		// Token: 0x040021F5 RID: 8693
		[NonSerialized]
		public int runningThreadsCount;

		// Token: 0x040021F6 RID: 8694
		[NonSerialized]
		public bool applyRunning;

		// Token: 0x040021F7 RID: 8695
		public int seed = 12345;

		// Token: 0x040021F8 RID: 8696
		public int terrainSize = 1000;

		// Token: 0x040021F9 RID: 8697
		public int terrainHeight = 200;

		// Token: 0x040021FA RID: 8698
		public int resolution = 512;

		// Token: 0x040021FB RID: 8699
		public int lodResolution = 128;

		// Token: 0x040021FC RID: 8700
		public static MapMagic instance = null;

		// Token: 0x040021FD RID: 8701
		public int mouseButton = -1;

		// Token: 0x040021FE RID: 8702
		public bool generateInfinite = true;

		// Token: 0x040021FF RID: 8703
		public int generateRange = 350;

		// Token: 0x04002200 RID: 8704
		public int enableRange = 300;

		// Token: 0x04002205 RID: 8709
		[NonSerialized]
		public Generator previewGenerator;

		// Token: 0x04002206 RID: 8710
		[NonSerialized]
		public Generator.Output previewOutput;

		// Token: 0x04002207 RID: 8711
		public bool multiThreaded = true;

		// Token: 0x04002208 RID: 8712
		public int maxThreads = 2;

		// Token: 0x04002209 RID: 8713
		public bool instantGenerate = true;

		// Token: 0x0400220A RID: 8714
		public bool saveIntermediate = true;

		// Token: 0x0400220B RID: 8715
		public int heightWeldMargins = 5;

		// Token: 0x0400220C RID: 8716
		public int splatsWeldMargins = 2;

		// Token: 0x0400220D RID: 8717
		public bool hideWireframe = true;

		// Token: 0x0400220E RID: 8718
		public bool hideFarTerrains = true;

		// Token: 0x0400220F RID: 8719
		public bool copyLayersTags = true;

		// Token: 0x04002210 RID: 8720
		public bool copyComponents = true;

		// Token: 0x04002211 RID: 8721
		public bool genAroundMainCam = true;

		// Token: 0x04002212 RID: 8722
		public bool genAroundObjsTag;

		// Token: 0x04002213 RID: 8723
		public string genAroundTag;

		// Token: 0x04002214 RID: 8724
		public int pixelError = 1;

		// Token: 0x04002215 RID: 8725
		public int baseMapDist = 1000;

		// Token: 0x04002216 RID: 8726
		public bool castShadows;

		// Token: 0x04002217 RID: 8727
		public Material terrainMaterial;

		// Token: 0x04002218 RID: 8728
		public bool detailDraw = true;

		// Token: 0x04002219 RID: 8729
		public float detailDistance = 80f;

		// Token: 0x0400221A RID: 8730
		public float detailDensity = 1f;

		// Token: 0x0400221B RID: 8731
		public float treeDistance = 1000f;

		// Token: 0x0400221C RID: 8732
		public float treeBillboardStart = 200f;

		// Token: 0x0400221D RID: 8733
		public float treeFadeLength = 5f;

		// Token: 0x0400221E RID: 8734
		public int treeFullLod = 150;

		// Token: 0x0400221F RID: 8735
		public float windSpeed = 0.5f;

		// Token: 0x04002220 RID: 8736
		public float windSize = 0.5f;

		// Token: 0x04002221 RID: 8737
		public float windBending = 0.5f;

		// Token: 0x04002222 RID: 8738
		public Color grassTint = Color.gray;

		// Token: 0x04002223 RID: 8739
		public int selected;

		// Token: 0x04002224 RID: 8740
		public GeneratorsAsset guiGens;

		// Token: 0x04002225 RID: 8741
		public Vector2 guiScroll = new Vector2(0f, 0f);

		// Token: 0x04002226 RID: 8742
		public float guiZoom = 1f;

		// Token: 0x04002227 RID: 8743
		[NonSerialized]
		public Layout layout;

		// Token: 0x04002228 RID: 8744
		[NonSerialized]
		public Layout toolbarLayout;

		// Token: 0x04002229 RID: 8745
		public bool guiGenerators = true;

		// Token: 0x0400222A RID: 8746
		public bool guiSettings;

		// Token: 0x0400222B RID: 8747
		public bool guiTerrainSettings;

		// Token: 0x0400222C RID: 8748
		public bool guiTreesGrassSettings;

		// Token: 0x0400222D RID: 8749
		public bool guiDebug;

		// Token: 0x0400222E RID: 8750
		public bool guiAbout;

		// Token: 0x0400222F RID: 8751
		public GameObject sceneRedrawObject;

		// Token: 0x04002230 RID: 8752
		public int guiGeneratorWidth = 160;

		// Token: 0x04002231 RID: 8753
		public Dictionary<Type, long> guiDebugProcessTimes = new Dictionary<Type, long>();

		// Token: 0x04002232 RID: 8754
		public Dictionary<Type, long> guiDebugApplyTimes = new Dictionary<Type, long>();

		// Token: 0x04002234 RID: 8756
		public bool setDirty;

		// Token: 0x04002235 RID: 8757
		private Vector3[] camPoses;

		// Token: 0x04002236 RID: 8758
		private Chunk[] chunksArray;

		// Token: 0x04002237 RID: 8759
		private float[] distsArray;

		// Token: 0x04002238 RID: 8760
		public Serializer serializer;

		// Token: 0x0200054F RID: 1359
		// (Invoke) Token: 0x0600224F RID: 8783
		public delegate void ApplyEvent(Terrain terrain, object obj);

		// Token: 0x02000550 RID: 1360
		// (Invoke) Token: 0x06002253 RID: 8787
		public delegate void ChangeEvent(Terrain terrain);

		// Token: 0x02000551 RID: 1361
		// (Invoke) Token: 0x06002257 RID: 8791
		public delegate void RepaintWindowAction();

		// Token: 0x02000552 RID: 1362
		[Serializable]
		public class GeneratorsList
		{
			// Token: 0x04002239 RID: 8761
			public Generator[] list = new Generator[0];

			// Token: 0x0400223A RID: 8762
			public Generator[] outputs = new Generator[0];
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000555 RID: 1365
	[Serializable]
	public class Chunk
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x00018280 File Offset: 0x00016480
		public bool running
		{
			get
			{
				return this.thread != null && this.thread.IsAlive;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00018297 File Offset: 0x00016497
		public bool complete
		{
			get
			{
				return (!this.start && !this.running && this.apply.Count == 0 && !this.clear) || this.locked;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000DC008 File Offset: 0x000DA208
		public void CheckSetNeighbors()
		{
			TerrainGrid terrains = MapMagic.instance.terrains;
			if (this.oldNeig_x != terrains.GetTerrain(this.coord.x - 1, this.coord.z, false) || this.oldNeig_Z != terrains.GetTerrain(this.coord.x, this.coord.z + 1, false) || this.oldNeig_X != terrains.GetTerrain(this.coord.x + 1, this.coord.z, false) || this.oldNeig_z != terrains.GetTerrain(this.coord.x, this.coord.z - 1, false))
			{
				this.terrain.SetNeighbors(terrains.GetTerrain(this.coord.x - 1, this.coord.z, false), terrains.GetTerrain(this.coord.x, this.coord.z + 1, false), terrains.GetTerrain(this.coord.x + 1, this.coord.z, false), terrains.GetTerrain(this.coord.x, this.coord.z - 1, false));
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000DC160 File Offset: 0x000DA360
		public CoordRect defaultRect
		{
			get
			{
				int resolution = MapMagic.instance.resolution;
				return new CoordRect(this.coord.x * resolution, this.coord.z * resolution, resolution, resolution);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000182C6 File Offset: 0x000164C6
		public Matrix defaultMatrix
		{
			get
			{
				return new Matrix(this.defaultRect, null);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000DC19C File Offset: 0x000DA39C
		public SpatialHash defaultSpatialHash
		{
			get
			{
				return new SpatialHash(new Vector2((float)(this.coord.x * MapMagic.instance.resolution), (float)(this.coord.z * MapMagic.instance.resolution)), (float)MapMagic.instance.resolution, 16);
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000DC1F0 File Offset: 0x000DA3F0
		public void Update(float camDist)
		{
			if (this.terrain == null)
			{
				MapMagic.instance.terrains.Unnail(this.coord, true);
				return;
			}
			if (((!MapMagic.instance.hideFarTerrains || camDist < (float)MapMagic.instance.enableRange) && this.complete) || MapMagic.instance.isEditor)
			{
				if (!this.terrain.gameObject.activeSelf)
				{
					this.terrain.gameObject.SetActive(true);
				}
			}
			else if (this.terrain.gameObject.activeSelf)
			{
				this.terrain.gameObject.SetActive(false);
			}
			this.CheckSetNeighbors();
			if (camDist < (float)MapMagic.instance.generateRange && !this.running && this.clear)
			{
				this.start = true;
			}
			if (this.start)
			{
				MapMagic.CallOnGenerateStarted(this.terrain);
				if (!MapMagic.instance.multiThreaded)
				{
					this.ThreadFn(null);
				}
				else if (this.running)
				{
					this.stop = true;
				}
				else if (MapMagic.instance.runningThreadsCount < MapMagic.instance.maxThreads)
				{
					this.thread = new Thread(new ParameterizedThreadStart(this.ThreadFn));
					this.thread.IsBackground = true;
					this.thread.Start(MapMagic.instance.gens);
					MapMagic.instance.runningThreadsCount++;
				}
			}
			if (!MapMagic.instance.applyRunning && this.queuedApply && !this.start && !this.stop && !this.running)
			{
				if (MapMagic.instance.isEditor)
				{
					IEnumerator enumerator = this.ApplyRoutine();
					while (enumerator.MoveNext())
					{
					}
					return;
				}
				MapMagic.instance.StartCoroutine(this.ApplyRoutine());
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000DC3B8 File Offset: 0x000DA5B8
		public void ThreadFn(object obj)
		{
			if (this.locked)
			{
				return;
			}
			object obj2 = this.locker;
			lock (obj2)
			{
				do
				{
					this.stop = false;
					this.start = false;
					this.clear = false;
					this.apply.Clear();
					this.numReady = 0;
					this.numJobs = 0;
					MapMagic.instance.guiDebugProcessTimes.Clear();
					MapMagic.instance.guiDebugApplyTimes.Clear();
					try
					{
						HashSet<Generator> hashSet = new HashSet<Generator>();
						foreach (Generator.IOutput output in MapMagic.instance.gens.OutputGenerators(true, true))
						{
							((Generator)output).CountPriorsRecursive(this, hashSet);
						}
						this.numJobs = hashSet.Count<Generator>();
						foreach (Generator.IOutput output2 in MapMagic.instance.gens.OutputGenerators(true, true))
						{
							((Generator)output2).CheckClearRecursive(this);
						}
						if (MapMagic.instance.previewOutput != null)
						{
							MapMagic.instance.previewGenerator.CheckClearRecursive(this);
						}
						this.numReady = this.ready.Count<Generator>();
						HashSet<Type> hashSet2 = new HashSet<Type>();
						foreach (Biome biome in MapMagic.instance.gens.GeneratorsOfType<Biome>(true, true))
						{
							if (!this.ready.Contains(biome) && biome.data != null && biome.mask.linkGen != null)
							{
								foreach (Generator.IOutput output3 in biome.data.OutputGenerators(true, false))
								{
									Generator generator = (Generator)output3;
									hashSet2.Add(generator.GetType());
								}
							}
						}
						foreach (Generator generator2 in MapMagic.instance.gens.OutputGenerators(true, true).ToArray<Generator.IOutput>())
						{
							if (!this.ready.Contains(generator2))
							{
								hashSet2.Add(generator2.GetType());
								generator2.GenerateWithPriors(this, null);
							}
						}
						if (MapMagic.instance.previewOutput != null)
						{
							MapMagic.instance.previewGenerator.GenerateWithPriors(this, null);
							if (!this.stop)
							{
								this.previewObject = this.results[MapMagic.instance.previewOutput];
							}
						}
						if (hashSet2.Contains(typeof(HeightOutput)))
						{
							hashSet2.Add(typeof(TreesOutput));
							hashSet2.Add(typeof(ObjectOutput));
						}
						if (hashSet2.Contains(typeof(HeightOutput)))
						{
							HeightOutput.Process(this);
						}
						if (hashSet2.Contains(typeof(SplatOutput)))
						{
							SplatOutput.Process(this);
						}
						if (hashSet2.Contains(typeof(ObjectOutput)))
						{
							ObjectOutput.Process(this);
						}
						if (hashSet2.Contains(typeof(TreesOutput)))
						{
							TreesOutput.Process(this);
						}
						if (hashSet2.Contains(typeof(GrassOutput)))
						{
							GrassOutput.Process(this);
						}
					}
					catch (Exception ex)
					{
						string str = "Generate Thread Error:\n";
						Exception ex2 = ex;
						Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
					}
				}
				while (this.start);
				this.queuedApply = true;
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000182D4 File Offset: 0x000164D4
		public IEnumerator ApplyRoutine()
		{
			MapMagic.CallOnGenerateCompleted(this.terrain);
			MapMagic.instance.applyRunning = true;
			foreach (KeyValuePair<Type, object> keyValuePair in this.apply)
			{
				MapMagic.CallOnApply(this.terrain, keyValuePair.Value);
				IEnumerator e = null;
				Type key = keyValuePair.Key;
				if (key == typeof(HeightOutput))
				{
					e = HeightOutput.Apply(this);
				}
				else if (key == typeof(SplatOutput))
				{
					e = SplatOutput.Apply(this);
				}
				else if (key == typeof(ObjectOutput))
				{
					e = ObjectOutput.Apply(this);
				}
				else if (key == typeof(TreesOutput))
				{
					e = TreesOutput.Apply(this);
				}
				else if (key == typeof(GrassOutput))
				{
					e = GrassOutput.Apply(this);
				}
				while (e.MoveNext())
				{
					if (this.terrain == null)
					{
						yield break;
					}
					yield return null;
				}
				e = null;
			}
			Dictionary<Type, object>.Enumerator enumerator = default(Dictionary<Type, object>.Enumerator);
			HashSet<Type> existingOutputTypes = MapMagic.instance.gens.GetExistingOutputTypes(true, true);
			if (!existingOutputTypes.Contains(typeof(HeightOutput)) && this.terrain.terrainData.heightmapResolution != 33)
			{
				HeightOutput.Purge(this);
			}
			if (!existingOutputTypes.Contains(typeof(SplatOutput)) && this.terrain.terrainData.alphamapResolution != 16)
			{
				SplatOutput.Purge(this);
			}
			if (!existingOutputTypes.Contains(typeof(ObjectOutput)) && this.terrain.transform.childCount != 0)
			{
				ObjectOutput.Purge(this);
			}
			if (!existingOutputTypes.Contains(typeof(TreesOutput)) && this.terrain.terrainData.treeInstanceCount != 0)
			{
				TreesOutput.Purge(this);
			}
			if (!existingOutputTypes.Contains(typeof(GrassOutput)) && this.terrain.terrainData.detailResolution != 16)
			{
				GrassOutput.Purge(this);
			}
			if (this.terrain.terrainData.terrainLayers.Length == 0)
			{
				this.ClearSplats();
			}
			this.apply.Clear();
			if (!MapMagic.instance.isEditor || !MapMagic.instance.saveIntermediate)
			{
				this.results.Clear();
				this.ready.Clear();
			}
			MapMagic.instance.applyRunning = false;
			if (MapMagic.instance.copyLayersTags)
			{
				GameObject gameObject = this.terrain.gameObject;
				gameObject.layer = MapMagic.instance.gameObject.layer;
				gameObject.isStatic = MapMagic.instance.gameObject.isStatic;
				try
				{
					gameObject.tag = MapMagic.instance.gameObject.tag;
				}
				catch
				{
					Debug.LogError("MapMagic: could not copy object tag");
				}
			}
			if (MapMagic.instance.copyComponents)
			{
				GameObject gameObject2 = this.terrain.gameObject;
				MonoBehaviour[] components = MapMagic.instance.GetComponents<MonoBehaviour>();
				for (int i = 0; i < components.Length; i++)
				{
					if (!(components[i] is MapMagic) && !(components[i] == null) && this.terrain.gameObject.GetComponent(components[i].GetType()) == null)
					{
						Extensions.CopyComponent(components[i], gameObject2);
					}
				}
			}
			MapMagic.CallOnApplyCompleted(this.terrain);
			this.queuedApply = false;
			if (MapMagic.instance.previewOutput != null)
			{
				this.Preview(true);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000DC7C8 File Offset: 0x000DA9C8
		public void SetSettings(MapMagic magic)
		{
			this.terrain.heightmapPixelError = (float)magic.pixelError;
			this.terrain.basemapDistance = (float)magic.baseMapDist;
			this.terrain.castShadows = magic.castShadows;
			if (magic.terrainMaterial != null)
			{
				this.terrain.materialTemplate = magic.terrainMaterial;
				this.terrain.materialType = Terrain.MaterialType.Custom;
			}
			else
			{
				this.terrain.materialType = Terrain.MaterialType.BuiltInStandard;
			}
			this.terrain.drawTreesAndFoliage = magic.detailDraw;
			this.terrain.detailObjectDistance = magic.detailDistance;
			this.terrain.detailObjectDensity = magic.detailDensity;
			this.terrain.treeDistance = magic.treeDistance;
			this.terrain.treeBillboardDistance = magic.treeBillboardStart;
			this.terrain.treeCrossFadeLength = magic.treeFadeLength;
			this.terrain.treeMaximumFullLODCount = magic.treeFullLod;
			this.terrain.terrainData.wavingGrassSpeed = magic.windSpeed;
			this.terrain.terrainData.wavingGrassAmount = magic.windSize;
			this.terrain.terrainData.wavingGrassStrength = magic.windBending;
			this.terrain.terrainData.wavingGrassTint = magic.grassTint;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000DC914 File Offset: 0x000DAB14
		public void ClearSplats()
		{
			SplatPrototype[] splats = new SplatPrototype[]
			{
				new SplatPrototype
				{
					texture = Extensions.ColorTexture(2, 2, new Color(0.5f, 0.5f, 0.5f, 0f))
				}
			};
			this.terrain.terrainData.terrainLayers = UpgradeTerrainSplats.Upgrade(splats);
			float[,,] array = new float[16, 16, 1];
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					array[j, i, 0] = 1f;
				}
			}
			this.terrain.terrainData.alphamapResolution = 16;
			this.terrain.terrainData.SetAlphamaps(0, 0, array);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000DC9C4 File Offset: 0x000DABC4
		public void Preview(bool forceRefresh = false)
		{
			if (Chunk.redPrototype == null)
			{
				Chunk.redPrototype = new SplatPrototype();
			}
			if (Chunk.redPrototype.texture == null)
			{
				Chunk.redPrototype.texture = Extensions.ColorTexture(2, 2, new Color(1f, 0f, 0f, 0f));
			}
			if (Chunk.greenPrototype == null)
			{
				Chunk.greenPrototype = new SplatPrototype();
			}
			if (Chunk.greenPrototype.texture == null)
			{
				Chunk.greenPrototype.texture = Extensions.ColorTexture(2, 2, new Color(0f, 1f, 0f, 0f));
			}
			SplatPrototype[] array = this.terrain.terrainData.splatPrototypes;
			if (this.previewObject is Matrix && forceRefresh)
			{
				if (array == null || array.Length < 2)
				{
					array = new SplatPrototype[Mathf.Max(array.Length, 2)];
				}
				array[0] = Chunk.redPrototype;
				array[1] = Chunk.greenPrototype;
				Matrix matrix = (Matrix)this.previewObject;
				float[,,] array2 = new float[matrix.rect.size.x, matrix.rect.size.z, Mathf.Max(array.Length, 2)];
				for (int i = 0; i < matrix.rect.size.x; i++)
				{
					for (int j = 0; j < matrix.rect.size.z; j++)
					{
						array2[j, i, 0] = 1f - matrix[i + matrix.rect.offset.x, j + matrix.rect.offset.z];
						array2[j, i, 1] = matrix[i + matrix.rect.offset.x, j + matrix.rect.offset.z];
					}
				}
				TerrainData terrainData = this.terrain.terrainData;
				terrainData.terrainLayers = UpgradeTerrainSplats.Upgrade(array);
				terrainData.alphamapResolution = array2.GetLength(0);
				terrainData.SetAlphamaps(0, 0, array2);
			}
			if (this.previewObject is SpatialHash)
			{
				try
				{
					float num = 1f * (float)MapMagic.instance.terrainSize / (float)MapMagic.instance.resolution;
					foreach (object obj in ((SpatialHash)this.previewObject))
					{
						SpatialObject spatialObject = (SpatialObject)obj;
						float num2 = 0f;
						if (this.heights != null)
						{
							num2 = this.heights.GetInterpolated(spatialObject.pos.x, spatialObject.pos.y, Matrix.WrapMode.Once);
						}
						Vector3 vector = new Vector3(spatialObject.pos.x * num, (spatialObject.height + num2) * (float)MapMagic.instance.terrainHeight, spatialObject.pos.y * num);
						Gizmos.color = Color.white;
						Gizmos.DrawLine(vector + new Vector3(spatialObject.size / 2f, 0f, 0f), vector - new Vector3(spatialObject.size / 2f, 0f, 0f));
						Gizmos.DrawLine(vector + new Vector3(0f, 0f, spatialObject.size / 2f), vector - new Vector3(0f, 0f, spatialObject.size / 2f));
						Vector3 from = vector;
						foreach (Vector3 vector2 in vector.CircleAround(spatialObject.size / 2f, 12, true))
						{
							Gizmos.DrawLine(from, vector2);
							from = vector2;
						}
						Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
						Gizmos.DrawLine(new Vector3(vector.x, 0f, vector.z), new Vector3(vector.x, (float)MapMagic.instance.terrainHeight, vector.z));
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400223F RID: 8767
		public Coord coord;

		// Token: 0x04002240 RID: 8768
		public Terrain terrain;

		// Token: 0x04002241 RID: 8769
		public TransformPool[] pools;

		// Token: 0x04002242 RID: 8770
		[NonSerialized]
		public Thread thread;

		// Token: 0x04002243 RID: 8771
		public bool locked;

		// Token: 0x04002244 RID: 8772
		public bool start;

		// Token: 0x04002245 RID: 8773
		public bool stop;

		// Token: 0x04002246 RID: 8774
		public bool clear = true;

		// Token: 0x04002247 RID: 8775
		public bool queuedApply;

		// Token: 0x04002248 RID: 8776
		[NonSerialized]
		public int numReady;

		// Token: 0x04002249 RID: 8777
		[NonSerialized]
		public int numJobs;

		// Token: 0x0400224A RID: 8778
		[NonSerialized]
		public HashSet<Generator> ready = new HashSet<Generator>();

		// Token: 0x0400224B RID: 8779
		[NonSerialized]
		public Dictionary<Generator.Output, object> results = new Dictionary<Generator.Output, object>();

		// Token: 0x0400224C RID: 8780
		[NonSerialized]
		public Dictionary<Type, object> apply = new Dictionary<Type, object>();

		// Token: 0x0400224D RID: 8781
		[NonSerialized]
		public Matrix heights;

		// Token: 0x0400224E RID: 8782
		[NonSerialized]
		public object previewObject;

		// Token: 0x0400224F RID: 8783
		[NonSerialized]
		public Stopwatch timer;

		// Token: 0x04002250 RID: 8784
		[NonSerialized]
		private Terrain oldNeig_x;

		// Token: 0x04002251 RID: 8785
		[NonSerialized]
		private Terrain oldNeig_X;

		// Token: 0x04002252 RID: 8786
		[NonSerialized]
		private Terrain oldNeig_z;

		// Token: 0x04002253 RID: 8787
		[NonSerialized]
		private Terrain oldNeig_Z;

		// Token: 0x04002254 RID: 8788
		public object locker = new object();

		// Token: 0x04002255 RID: 8789
		private static SplatPrototype redPrototype = new SplatPrototype();

		// Token: 0x04002256 RID: 8790
		private static SplatPrototype greenPrototype = new SplatPrototype();
	}
}

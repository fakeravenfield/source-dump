using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020005B4 RID: 1460
	[Serializable]
	public class TerrainGrid : ObjectGrid<Chunk>
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000F25BC File Offset: 0x000F07BC
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x000F2620 File Offset: 0x000F0820
		public bool start
		{
			get
			{
				bool flag = true;
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					flag = (flag && keyValuePair.Value.start);
				}
				return flag;
			}
			set
			{
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					keyValuePair.Value.start = value;
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x000F267C File Offset: 0x000F087C
		// (set) Token: 0x060025DB RID: 9691 RVA: 0x000F26E0 File Offset: 0x000F08E0
		public bool stop
		{
			get
			{
				bool flag = true;
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					flag = (flag && keyValuePair.Value.stop);
				}
				return flag;
			}
			set
			{
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					keyValuePair.Value.stop = value;
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000F273C File Offset: 0x000F093C
		public bool running
		{
			get
			{
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					if (keyValuePair.Value.running)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000F27A0 File Offset: 0x000F09A0
		public bool complete
		{
			get
			{
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.grid)
				{
					if (!keyValuePair.Value.complete)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000F2804 File Offset: 0x000F0A04
		public float progress
		{
			get
			{
				int num = this.grid.Sum((KeyValuePair<int, Chunk> kv) => kv.Value.numJobs);
				return (float)this.grid.Sum((KeyValuePair<int, Chunk> kv) => kv.Value.numReady) / (float)num;
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0001A2BF File Offset: 0x000184BF
		public override Chunk Construct()
		{
			return new Chunk();
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000F286C File Offset: 0x000F0A6C
		public override void OnCreate(Chunk chunk, Coord coord)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "Terrain " + coord.x.ToString() + "," + coord.z.ToString();
			gameObject.transform.parent = MapMagic.instance.transform;
			gameObject.transform.localPosition = coord.ToVector3((float)MapMagic.instance.terrainSize);
			chunk.terrain = gameObject.AddComponent<Terrain>();
			TerrainCollider terrainCollider = gameObject.AddComponent<TerrainCollider>();
			TerrainData terrainData = new TerrainData();
			chunk.terrain.terrainData = terrainData;
			terrainCollider.terrainData = terrainData;
			terrainData.size = new Vector3((float)MapMagic.instance.terrainSize, (float)MapMagic.instance.terrainHeight, (float)MapMagic.instance.terrainSize);
			chunk.coord = coord;
			chunk.SetSettings(MapMagic.instance);
			chunk.clear = true;
			MapMagic.CallRepaintWindow();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000F2954 File Offset: 0x000F0B54
		public override void OnMove(Chunk chunk, Coord newCoord)
		{
			chunk.coord = newCoord;
			chunk.terrain.transform.localPosition = newCoord.ToVector3((float)MapMagic.instance.terrainSize);
			chunk.stop = true;
			MapMagic.instance.StopAllCoroutines();
			MapMagic.instance.applyRunning = false;
			chunk.results.Clear();
			chunk.ready.Clear();
			chunk.heights = null;
			chunk.clear = true;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0001A2C6 File Offset: 0x000184C6
		public override void OnRemove(Chunk chunk)
		{
			chunk.stop = true;
			MapMagic.instance.StopAllCoroutines();
			MapMagic.instance.applyRunning = false;
			if (chunk.terrain != null)
			{
				UnityEngine.Object.DestroyImmediate(chunk.terrain.gameObject);
			}
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0001A302 File Offset: 0x00018502
		public void Deploy(Vector3 pos, bool allowMove = true)
		{
			this.Deploy(new Vector3[]
			{
				pos
			}, allowMove);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000F29CC File Offset: 0x000F0BCC
		public void Deploy(Vector3[] poses, bool allowMove = true)
		{
			bool flag = false;
			if (this.prevRects == null || this.prevRects.Length != poses.Length)
			{
				flag = true;
				this.prevRects = new CoordRect[poses.Length];
				this.currRects = new CoordRect[poses.Length];
				this.prevCenters = new Coord[poses.Length];
				this.currCenters = new Coord[poses.Length];
			}
			for (int i = 0; i < poses.Length; i++)
			{
				Vector3 pos = poses[i];
				this.currCenters[i] = pos.RoundToCoord((float)MapMagic.instance.terrainSize);
				this.currRects[i] = pos.ToCoordRect((float)MapMagic.instance.generateRange, (float)MapMagic.instance.terrainSize);
				if (this.currRects[i] != this.prevRects[i])
				{
					flag = true;
				}
				this.prevRects[i] = this.currRects[i];
				this.prevCenters[i] = this.currCenters[i];
			}
			if (!flag)
			{
				return;
			}
			base.Deploy(this.currRects, this.currCenters, allowMove);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000F2AF4 File Offset: 0x000F0CF4
		public virtual void Reset()
		{
			foreach (Chunk chunk in base.Objects())
			{
				if (chunk != null)
				{
					this.OnRemove(chunk);
				}
			}
			this.grid = new Dictionary<int, Chunk>();
			HashSet<int> nailedHashes = this.nailedHashes;
			this.nailedHashes = new HashSet<int>();
			foreach (int hash in nailedHashes)
			{
				base.Nail(hash.ToCoord());
			}
			this.Deploy(this.prevRects, this.prevCenters, true);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000F2BB4 File Offset: 0x000F0DB4
		public void CheckEmpty()
		{
			foreach (Chunk chunk in base.Objects())
			{
				if (chunk == null || chunk.terrain == null || chunk.terrain.terrainData == null)
				{
					this.Reset();
					break;
				}
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000F2C28 File Offset: 0x000F0E28
		public Terrain GetTerrain(int x, int z, bool onlyComplete = true)
		{
			Terrain result = null;
			Chunk chunk = MapMagic.instance.terrains[x, z];
			if (chunk != null && (chunk.complete || !onlyComplete))
			{
				result = chunk.terrain;
			}
			return result;
		}

		// Token: 0x04002476 RID: 9334
		public CoordRect[] prevRects = new CoordRect[0];

		// Token: 0x04002477 RID: 9335
		public CoordRect[] currRects = new CoordRect[0];

		// Token: 0x04002478 RID: 9336
		public Coord[] currCenters = new Coord[0];

		// Token: 0x04002479 RID: 9337
		public Coord[] prevCenters = new Coord[0];
	}
}

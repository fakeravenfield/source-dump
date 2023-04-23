using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class SpawnPointNeighborManager : MonoBehaviour
{
	// Token: 0x06000C4D RID: 3149 RVA: 0x000788C8 File Offset: 0x00076AC8
	public static SpawnPointNeighborManager.SpawnPointNeighbor GetNeighborInfo(SpawnPoint from, SpawnPoint to)
	{
		foreach (SpawnPointNeighborManager.SpawnPointNeighbor spawnPointNeighbor in SpawnPointNeighborManager.instance.neighbors)
		{
			if (spawnPointNeighbor.a == from && spawnPointNeighbor.b == to)
			{
				return spawnPointNeighbor;
			}
			if (!spawnPointNeighbor.oneWay && spawnPointNeighbor.a == to && spawnPointNeighbor.b == from)
			{
				return spawnPointNeighbor;
			}
		}
		return null;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00078938 File Offset: 0x00076B38
	public static void SetupNeighbors(SpawnPoint spawnPoint)
	{
		spawnPoint.allNeighbors = new List<SpawnPoint>();
		spawnPoint.outgoingNeighbors = new List<SpawnPoint>();
		spawnPoint.incomingNeighbors = new List<SpawnPoint>();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (SpawnPointNeighborManager.SpawnPointNeighbor spawnPointNeighbor in SpawnPointNeighborManager.instance.neighbors)
		{
			if (SpawnPointNeighborManager.ValidateSpawnPointNeighbor(spawnPointNeighbor))
			{
				if (spawnPointNeighbor.a == spawnPoint)
				{
					spawnPoint.allNeighbors.Add(spawnPointNeighbor.b);
					spawnPoint.outgoingNeighbors.Add(spawnPointNeighbor.b);
					if (!spawnPointNeighbor.oneWay)
					{
						spawnPoint.incomingNeighbors.Add(spawnPointNeighbor.b);
					}
					num++;
					if (spawnPointNeighbor.landConnection)
					{
						num3++;
					}
					if (spawnPointNeighbor.waterConnection)
					{
						num2++;
					}
				}
				else if (spawnPointNeighbor.b == spawnPoint)
				{
					spawnPoint.allNeighbors.Add(spawnPointNeighbor.a);
					spawnPoint.incomingNeighbors.Add(spawnPointNeighbor.a);
					if (!spawnPointNeighbor.oneWay)
					{
						spawnPoint.outgoingNeighbors.Add(spawnPointNeighbor.a);
						num++;
						if (spawnPointNeighbor.landConnection)
						{
							num3++;
						}
						if (spawnPointNeighbor.waterConnection)
						{
							num2++;
						}
					}
				}
			}
		}
		spawnPoint.nAirConnections = num;
		spawnPoint.nWaterConnections = num2;
		spawnPoint.nLandConnections = num3;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00078A90 File Offset: 0x00076C90
	public static List<SpawnPoint> GetNeighborsIncludeDisabled(SpawnPoint spawnPoint)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPointNeighborManager.SpawnPointNeighbor spawnPointNeighbor in SpawnPointNeighborManager.instance.neighbors)
		{
			if (spawnPointNeighbor.a != null && spawnPointNeighbor.b != null)
			{
				if (spawnPointNeighbor.a == spawnPoint)
				{
					list.Add(spawnPointNeighbor.b);
				}
				else if (spawnPointNeighbor.b == spawnPoint)
				{
					list.Add(spawnPointNeighbor.a);
				}
			}
		}
		return list;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0000A14F File Offset: 0x0000834F
	public static List<SpawnPoint> GetActiveNeighbors(SpawnPoint spawnPoint)
	{
		List<SpawnPoint> neighborsIncludeDisabled = SpawnPointNeighborManager.GetNeighborsIncludeDisabled(spawnPoint);
		neighborsIncludeDisabled.RemoveAll((SpawnPoint s) => !s.isActiveAndEnabled);
		return neighborsIncludeDisabled;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0000A17D File Offset: 0x0000837D
	public static bool HasLandConnection(SpawnPoint a, SpawnPoint b)
	{
		return a == null || b == null || SpawnPointNeighborManager.instance.hasLandConnection[a][b];
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0000A1A9 File Offset: 0x000083A9
	public static bool HasWaterConnection(SpawnPoint a, SpawnPoint b)
	{
		return a == null || b == null || SpawnPointNeighborManager.instance.hasWaterConnection[a][b];
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0000A1D5 File Offset: 0x000083D5
	public static bool IsConnectedByNavmesh(SpawnPoint a, SpawnPoint b, PathfindingBox.Type type)
	{
		return SpawnPointNeighborManager.GetClosestNavmeshArea(a, type) == SpawnPointNeighborManager.GetClosestNavmeshArea(b, type);
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0000A07F File Offset: 0x0000827F
	public static uint GetClosestNavmeshArea(SpawnPoint a, PathfindingBox.Type type)
	{
		if (type == PathfindingBox.Type.Boat)
		{
			return a.closestBoatNavmeshArea;
		}
		if (type == PathfindingBox.Type.Car)
		{
			return a.closestCarNavmeshArea;
		}
		return a.closestInfantryNavmeshArea;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00078B14 File Offset: 0x00076D14
	public static SpawnPointConnectionGraph GetConnectionGraphByNavmesh(SpawnPoint spawnPoint, PathfindingBox.Type type)
	{
		SpawnPointConnectionGraph spawnPointConnectionGraph = new SpawnPointConnectionGraph(spawnPoint, SpawnPointNeighborManager.GetClosestNavmeshArea(spawnPoint, type));
		foreach (SpawnPoint spawnPoint2 in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint2.gameObject.activeInHierarchy && !(spawnPoint2 == spawnPoint) && SpawnPointNeighborManager.IsConnectedByNavmesh(spawnPoint, spawnPoint2, type))
			{
				spawnPointConnectionGraph.Add(spawnPoint2);
			}
		}
		return spawnPointConnectionGraph;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00078B74 File Offset: 0x00076D74
	public static List<SpawnPointConnectionGraph> GetSortedConnectionGraphsByNavmesh(PathfindingBox.Type type)
	{
		List<SpawnPointConnectionGraph> list = new List<SpawnPointConnectionGraph>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (SpawnPointConnectionGraph spawnPointConnectionGraph in list)
				{
					if (SpawnPointNeighborManager.IsConnectedByNavmesh(spawnPointConnectionGraph.GetRoot(), spawnPoint, type))
					{
						spawnPointConnectionGraph.Add(spawnPoint);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(new SpawnPointConnectionGraph(spawnPoint, SpawnPointNeighborManager.GetClosestNavmeshArea(spawnPoint, type)));
				}
			}
		}
		list.Sort((SpawnPointConnectionGraph a, SpawnPointConnectionGraph b) => b.spawns.Count.CompareTo(a.spawns.Count));
		return list;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0000A1E7 File Offset: 0x000083E7
	private static bool ValidateSpawnPointNeighbor(SpawnPointNeighborManager.SpawnPointNeighbor neighbor)
	{
		return neighbor.a != null && neighbor.b != null && neighbor.a.isActiveAndEnabled && neighbor.b.isActiveAndEnabled;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0000A21F File Offset: 0x0000841F
	private void Awake()
	{
		SpawnPointNeighborManager.instance = this;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0000A227 File Offset: 0x00008427
	public void StartGame()
	{
		this.SetupSpawnPointNeighbors();
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00078C4C File Offset: 0x00076E4C
	private void SetupSpawnPointNeighbors()
	{
		SpawnPoint[] array = UnityEngine.Object.FindObjectsOfType<SpawnPoint>();
		SpawnPoint[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			SpawnPointNeighborManager.SetupNeighbors(array2[i]);
		}
		this.hasLandConnection = this.SetupConnections(array, new SpawnPointNeighborManager.CanTraverseDelegate(this.CanTraverseLand));
		this.hasWaterConnection = this.SetupConnections(array, new SpawnPointNeighborManager.CanTraverseDelegate(this.CanTraverseWater));
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00078CAC File Offset: 0x00076EAC
	private Dictionary<SpawnPoint, Dictionary<SpawnPoint, bool>> SetupConnections(SpawnPoint[] allSpawns, SpawnPointNeighborManager.CanTraverseDelegate CanTraverse)
	{
		Dictionary<SpawnPoint, Dictionary<SpawnPoint, bool>> dictionary = new Dictionary<SpawnPoint, Dictionary<SpawnPoint, bool>>();
		foreach (SpawnPoint spawnPoint in allSpawns)
		{
			dictionary.Add(spawnPoint, new Dictionary<SpawnPoint, bool>());
			foreach (SpawnPoint spawnPoint2 in allSpawns)
			{
				dictionary[spawnPoint].Add(spawnPoint2, spawnPoint == spawnPoint2);
			}
		}
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint3 in allSpawns)
		{
			if (!list.Contains(spawnPoint3))
			{
				this.lastTraversedSpawns = new List<SpawnPoint>();
				this.TraverseSpawn(spawnPoint3, CanTraverse);
				foreach (SpawnPoint spawnPoint4 in this.lastTraversedSpawns)
				{
					foreach (SpawnPoint spawnPoint5 in this.lastTraversedSpawns)
					{
						if (spawnPoint4 != spawnPoint5)
						{
							dictionary[spawnPoint4][spawnPoint5] = true;
						}
					}
				}
				list.AddRange(this.lastTraversedSpawns);
			}
		}
		return dictionary;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00078E00 File Offset: 0x00077000
	private bool CanTraverseLand(SpawnPoint current, SpawnPoint next)
	{
		foreach (SpawnPointNeighborManager.SpawnPointNeighbor spawnPointNeighbor in this.neighbors)
		{
			if ((spawnPointNeighbor.a == current && spawnPointNeighbor.b == next) || (spawnPointNeighbor.b == current && spawnPointNeighbor.a == next))
			{
				return spawnPointNeighbor.landConnection;
			}
		}
		return false;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00078E68 File Offset: 0x00077068
	private bool CanTraverseWater(SpawnPoint current, SpawnPoint next)
	{
		foreach (SpawnPointNeighborManager.SpawnPointNeighbor spawnPointNeighbor in this.neighbors)
		{
			if ((spawnPointNeighbor.a == current && spawnPointNeighbor.b == next) || (spawnPointNeighbor.b == current && spawnPointNeighbor.a == next))
			{
				return spawnPointNeighbor.waterConnection;
			}
		}
		return false;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00078ED0 File Offset: 0x000770D0
	private void TraverseSpawn(SpawnPoint spawnPoint, SpawnPointNeighborManager.CanTraverseDelegate CanTraverse)
	{
		foreach (SpawnPoint spawnPoint2 in spawnPoint.allNeighbors)
		{
			if (!this.lastTraversedSpawns.Contains(spawnPoint2) && CanTraverse(spawnPoint, spawnPoint2))
			{
				this.lastTraversedSpawns.Add(spawnPoint2);
				this.TraverseSpawn(spawnPoint2, CanTraverse);
			}
		}
	}

	// Token: 0x04000D5E RID: 3422
	public static SpawnPointNeighborManager instance;

	// Token: 0x04000D5F RID: 3423
	public SpawnPointNeighborManager.SpawnPointNeighbor[] neighbors;

	// Token: 0x04000D60 RID: 3424
	private Dictionary<SpawnPoint, Dictionary<SpawnPoint, bool>> hasLandConnection;

	// Token: 0x04000D61 RID: 3425
	private Dictionary<SpawnPoint, Dictionary<SpawnPoint, bool>> hasWaterConnection;

	// Token: 0x04000D62 RID: 3426
	private List<SpawnPoint> lastTraversedSpawns;

	// Token: 0x020001CA RID: 458
	// (Invoke) Token: 0x06000C61 RID: 3169
	private delegate bool CanTraverseDelegate(SpawnPoint current, SpawnPoint next);

	// Token: 0x020001CB RID: 459
	[Serializable]
	public class SpawnPointNeighbor
	{
		// Token: 0x04000D63 RID: 3427
		public SpawnPoint a;

		// Token: 0x04000D64 RID: 3428
		public SpawnPoint b;

		// Token: 0x04000D65 RID: 3429
		public bool landConnection = true;

		// Token: 0x04000D66 RID: 3430
		public bool waterConnection = true;

		// Token: 0x04000D67 RID: 3431
		public bool oneWay;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MapEditor;
using Pathfinding;
using Pathfinding.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200009A RID: 154
public class PathfindingManager : MonoBehaviour
{
	// Token: 0x060004D2 RID: 1234 RVA: 0x0000296E File Offset: 0x00000B6E
	public static void RegisterDeath(Vector3 point)
	{
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0000500F File Offset: 0x0000320F
	private void Awake()
	{
		PathfindingManager.instance = this;
		this.pathfinder = base.GetComponent<AstarPath>();
		this.pathfinder.threadCount = ThreadCount.AutomaticHighLoad;
		this.pathfinder.logPathResults = PathLog.None;
		this.pathfinder.fullGetNearestSearch = true;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00057630 File Offset: 0x00055830
	private void Update()
	{
		if (!this.renderingNavmeshes)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.IncrementNavMeshRenderIndex(1);
			return;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.IncrementNavMeshRenderIndex(-1);
			return;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.NavMeshRenderNextType();
			return;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.NavMeshRenderPreviousType();
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00005048 File Offset: 0x00003248
	public static PathfindingBox.Type GetTypeFromGraphName(NavGraph graph)
	{
		if (graph.name[0] == 'B')
		{
			return PathfindingBox.Type.Boat;
		}
		if (graph.name[0] == 'C')
		{
			return PathfindingBox.Type.Car;
		}
		return PathfindingBox.Type.Infantry;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0000506F File Offset: 0x0000326F
	public static int GetMaskFromGraphType(PathfindingBox.Type type)
	{
		if (type == PathfindingBox.Type.Boat)
		{
			return PathfindingManager.boatGraphMask;
		}
		if (type == PathfindingBox.Type.Car)
		{
			return PathfindingManager.carGraphMask;
		}
		return PathfindingManager.infantryGraphMask;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00057694 File Offset: 0x00055894
	public static void PushAlternatePathNode(GraphNode node, PathfindingBox.Type graphType, int team)
	{
		if (node.Tag == 31U || node.Tag == 3U)
		{
			return;
		}
		if (node.Tag >= 4U && node.Tag <= 6U)
		{
			return;
		}
		uint newTag = (team == 0) ? 4U : 5U;
		if (graphType == PathfindingBox.Type.Boat)
		{
			PathfindingManager.instance.boatAlternativePathSet.UpdateTag(newTag, node);
			return;
		}
		if (graphType == PathfindingBox.Type.Car)
		{
			PathfindingManager.instance.carAlternativePathSet.UpdateTag(newTag, node);
			return;
		}
		PathfindingManager.instance.infantryAlternativePathSet.UpdateTag(newTag, node);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00057710 File Offset: 0x00055910
	public static bool FindLadderNodes(Ladder ladder)
	{
		foreach (NavGraph navGraph in PathfindingManager.instance.pathfinder.graphs)
		{
			if (navGraph != null)
			{
				PointGraph pointGraph = navGraph as PointGraph;
				if (pointGraph != null)
				{
					for (int j = 0; j < pointGraph.nodeCount; j++)
					{
						if (pointGraph.nodes[j].Tag == 31U)
						{
							Vector3 a = (Vector3)pointGraph.nodes[j].position;
							bool flag;
							if (Vector3.Distance(a, ladder.GetBottomExitPosition()) < 1f)
							{
								flag = true;
							}
							else
							{
								if (Vector3.Distance(a, ladder.GetTopExitPosition()) >= 1f)
								{
									goto IL_14D;
								}
								flag = false;
							}
							Vector3 b = flag ? ladder.GetTopExitPosition() : ladder.GetBottomExitPosition();
							foreach (Connection connection in pointGraph.nodes[j].connections)
							{
								if (connection.node.Tag == 31U && Vector3.Distance((Vector3)connection.node.position, b) < 1f)
								{
									if (flag)
									{
										ladder.bottomNode = pointGraph.nodes[j];
										ladder.topNode = connection.node;
									}
									else
									{
										ladder.bottomNode = connection.node;
										ladder.topNode = pointGraph.nodes[j];
									}
									return true;
								}
							}
						}
						IL_14D:;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0005788C File Offset: 0x00055A8C
	public void SetupGraphMasks()
	{
		PathfindingManager.infantryGraphMask = 0;
		PathfindingManager.carGraphMask = 0;
		PathfindingManager.boatGraphMask = 0;
		for (int i = 0; i < this.pathfinder.graphs.Length; i++)
		{
			if (this.pathfinder.graphs[i] != null)
			{
				NavGraph navGraph = this.pathfinder.graphs[i];
				if (navGraph.name[0] == 'I')
				{
					PathfindingManager.infantryGraphMask |= 1 << i;
				}
				else if (navGraph.name[0] == 'C')
				{
					PathfindingManager.carGraphMask |= 1 << i;
				}
				else if (navGraph.name[0] == 'B')
				{
					PathfindingManager.boatGraphMask |= 1 << i;
				}
			}
		}
		Debug.Log("Infantry graph mask: " + PathfindingManager.infantryGraphMask.ToString());
		Debug.Log("Car graph mask: " + PathfindingManager.carGraphMask.ToString());
		Debug.Log("Boat graph mask: " + PathfindingManager.boatGraphMask.ToString());
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0000508A File Offset: 0x0000328A
	private IEnumerator PenalizeNode(GraphNode node)
	{
		node.Penalty += 50000U;
		yield return new WaitForSeconds(60f);
		node.Penalty -= 50000U;
		yield break;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00057998 File Offset: 0x00055B98
	public void SetupPathfindingVolumes()
	{
		this.pathfinder = base.GetComponent<AstarPath>();
		this.ClearGraphs();
		PathfindingBox[] array = UnityEngine.Object.FindObjectsOfType<PathfindingBox>();
		this.graphToBox = new Dictionary<RecastGraph, PathfindingBox>();
		foreach (PathfindingBox box in array)
		{
			this.AddRecastGraph(box);
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000579E4 File Offset: 0x00055BE4
	public void FindAndScan()
	{
		foreach (Progress progress in this.FindAndScanAsync())
		{
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00005099 File Offset: 0x00003299
	public IEnumerable<Progress> FindAndScanAsync()
	{
		this.SetupPathfindingVolumes();
		WaterLevel.ForceFindWaterVolumesForPathfinding();
		this.SetupGraphMasks();
		if (this.waterRecastMeshPrefab == null)
		{
			Debug.LogError("Water recast mesh is null, no boat navmeshes will be generated!");
		}
		this.instantiatedRecastWaterMeshes = new List<GameObject>();
		this.SetupScanHelpers();
		Debug.Log("Scanning graphs...");
		foreach (Progress progress in this.ScanGraphsAsync())
		{
			yield return progress;
		}
		IEnumerator<Progress> enumerator = null;
		Debug.Log("Finding unwalkable nodes...");
		this.PruneGraphNodes();
		Debug.Log("Scan Complete! Took " + this.pathfinder.lastScanTime.ToString());
		foreach (GameObject obj in this.instantiatedRecastWaterMeshes)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.instantiatedRecastWaterMeshes = null;
		yield break;
		yield break;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x000050A9 File Offset: 0x000032A9
	private IEnumerable<Progress> ScanGraphsAsync()
	{
		GameObject blockerPrefab = (GameObject)Resources.Load("Pathfinding Blocker");
		int graphCount = this.graphToBox.Keys.Count;
		int graphIndex = -1;
		foreach (RecastGraph recastGraph in this.graphToBox.Keys)
		{
			int i = graphIndex;
			graphIndex = i + 1;
			PathfindingBox pathfindingBox = this.graphToBox[recastGraph];
			Debug.Log("Scanning " + recastGraph.name);
			List<GameObject> blockers = new List<GameObject>();
			foreach (PathfindingBox pathfindingBox2 in pathfindingBox.blockers)
			{
				if (!(pathfindingBox2 == null))
				{
					Debug.Log("Adding blocker: " + pathfindingBox2.gameObject.name);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(blockerPrefab, pathfindingBox2.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject.transform.localRotation = Quaternion.identity;
					blockers.Add(gameObject);
				}
			}
			foreach (Progress progress in this.pathfinder.ScanAsync(recastGraph))
			{
				float progress2 = progress.progress / (float)graphCount + 1f / (float)graphCount * (float)graphIndex;
				string description = progress.description.Replace("Scanning graph 1 of 1", string.Format("Scanning graph {0} of {1}", graphIndex + 1, graphCount));
				yield return new Progress(progress2, description);
			}
			IEnumerator<Progress> enumerator2 = null;
			foreach (GameObject obj in blockers)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			blockers = null;
		}
		Dictionary<RecastGraph, PathfindingBox>.KeyCollection.Enumerator enumerator = default(Dictionary<RecastGraph, PathfindingBox>.KeyCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00057A2C File Offset: 0x00055C2C
	private void SetupScanHelpers()
	{
		try
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.waterRecastMeshPrefab);
			gameObject.transform.position = new Vector3(0f, WaterLevel.instance.transform.position.y, 0f);
			this.instantiatedRecastWaterMeshes.Add(gameObject);
			foreach (WaterVolume waterVolume in WaterLevel.instance.waterVolumes)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.waterRecastMeshPrefab);
				gameObject2.transform.SetParent(waterVolume.transform);
				gameObject2.transform.localPosition = new Vector3(0f, 0.5f, 0f);
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				gameObject2.transform.SetParent(null);
				this.instantiatedRecastWaterMeshes.Add(gameObject2);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00057B78 File Offset: 0x00055D78
	private void ClearGraphs()
	{
		Debug.Log("ClearGraphs()");
		foreach (NavGraph navGraph in this.pathfinder.graphs)
		{
			if (navGraph != null)
			{
				this.pathfinder.data.RemoveGraph(navGraph);
			}
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00057BC4 File Offset: 0x00055DC4
	private void AddRecastGraph(PathfindingBox box)
	{
		RecastGraph recastGraph = (RecastGraph)this.pathfinder.data.AddGraph(typeof(RecastGraph));
		this.graphToBox.Add(recastGraph, box);
		recastGraph.name = box.type.ToString() + " graph - " + box.gameObject.name;
		Vector3 localScale = box.transform.localScale;
		localScale.x = Mathf.Abs(localScale.x);
		localScale.y = Mathf.Abs(localScale.y);
		localScale.z = Mathf.Abs(localScale.z);
		recastGraph.forcedBoundsCenter = box.transform.position;
		recastGraph.forcedBoundsSize = localScale;
		recastGraph.rotation = box.transform.eulerAngles;
		recastGraph.relevantGraphSurfaceMode = RecastGraph.RelevantGraphSurfaceMode.DoNotRequire;
		recastGraph.rasterizeMeshes = false;
		recastGraph.rasterizeColliders = true;
		int intVal = 1;
		if (box.type == PathfindingBox.Type.Car)
		{
			intVal = 4097;
		}
		else if (box.type == PathfindingBox.Type.Boat)
		{
			intVal = 4113;
		}
		recastGraph.mask = intVal;
		recastGraph.useTiles = box.tiled;
		int num = 128;
		int num2;
		int num7;
		do
		{
			num2 = num;
			num = num2 * 2;
			int num3 = (int)(box.transform.localScale.x / box.cellSize + 0.5f);
			int num4 = (int)(box.transform.localScale.z / box.cellSize + 0.5f);
			int num5 = (num3 + num2 - 1) / num2;
			int num6 = (num4 + num2 - 1) / num2;
			num7 = num5 * num6;
		}
		while (num7 > 2048);
		Debug.Log("Tile size: " + num2.ToString() + " @ tilenumber=" + num7.ToString());
		recastGraph.editorTileSize = num2;
		recastGraph.minRegionSize = 10f;
		recastGraph.cellSize = box.cellSize;
		recastGraph.characterRadius = box.characterRadius;
		recastGraph.maxSlope = ((box.type == PathfindingBox.Type.Boat) ? 1f : box.maxSlope);
		recastGraph.walkableClimb = ((box.type == PathfindingBox.Type.Boat) ? 0.01f : box.climbHeight);
		recastGraph.maxEdgeLength = 20f;
		recastGraph.showMeshSurface = true;
		recastGraph.drawGizmos = false;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00057DF8 File Offset: 0x00055FF8
	private void PruneGraphNodes()
	{
		PathfindingRelevantPoint[] array = UnityEngine.Object.FindObjectsOfType<PathfindingRelevantPoint>();
		string str = "Relevant points: ";
		int i = array.Length;
		Debug.Log(str + i.ToString());
		List<uint> relevantAreas = new List<uint>();
		this.foundRelevantPoints = new Dictionary<RecastGraph, List<PathfindingRelevantPoint>>();
		foreach (NavGraph navGraph in this.pathfinder.graphs)
		{
			if (navGraph != null && !(navGraph.GetType() != typeof(RecastGraph)))
			{
				RecastGraph key = (RecastGraph)navGraph;
				this.foundRelevantPoints.Add(key, new List<PathfindingRelevantPoint>());
				bool flag = PathfindingManager.GetTypeFromGraphName(navGraph) != PathfindingBox.Type.Boat;
				foreach (PathfindingRelevantPoint pathfindingRelevantPoint in array)
				{
					if (pathfindingRelevantPoint.type == PathfindingRelevantPoint.Type.Ground == flag)
					{
						NNInfoInternal nearest = navGraph.GetNearest(pathfindingRelevantPoint.transform.position);
						try
						{
							if (Vector3.Distance(nearest.clampedPosition, pathfindingRelevantPoint.transform.position) < 20f)
							{
								relevantAreas.Add(nearest.node.Area);
								this.foundRelevantPoints[key].Add(pathfindingRelevantPoint);
							}
						}
						catch (Exception)
						{
							Debug.LogWarning("No graph point found near relevant pathfinding point at " + pathfindingRelevantPoint.transform.position.ToString());
						}
					}
				}
			}
		}
		Action<GraphNode> <>9__0;
		foreach (NavGraph navGraph2 in this.pathfinder.graphs)
		{
			if (navGraph2 != null && !(navGraph2.GetType() != typeof(RecastGraph)))
			{
				RecastGraph recastGraph = (RecastGraph)navGraph2;
				NavGraph navGraph3 = navGraph2;
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node != null)
						{
							node.Walkable = relevantAreas.Contains(node.Area);
						}
					});
				}
				navGraph3.GetNodes(action);
			}
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00057FF0 File Offset: 0x000561F0
	public void SetupCustomLevelPathfinding()
	{
		CustomGraphCache customGraphCache = UnityEngine.Object.FindObjectOfType<CustomGraphCache>();
		if (customGraphCache != null && customGraphCache.cache != null && customGraphCache.cacheCoverPoints != null)
		{
			Debug.Log("Loading bundled graphcache");
			this.LoadCustomGraphCache(customGraphCache.cache);
			this.LoadCustomCoverPoints(customGraphCache.cacheCoverPoints);
			return;
		}
		Debug.Log("No bundled graphcache found, scanning pathfinding");
		this.ScanCustomLevel();
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0005805C File Offset: 0x0005625C
	public void GenerateLandingZones()
	{
		foreach (SpawnPoint spawnPoint in UnityEngine.Object.FindObjectsOfType<SpawnPoint>())
		{
			if (!spawnPoint.HasCustomHelicopterLandingZone())
			{
				try
				{
					this.GenerateSpawnPointLandingZones(spawnPoint);
				}
				catch (Exception ex)
				{
					string str = "Could not generate helicopter landing zone: ";
					Exception ex2 = ex;
					Debug.LogWarning(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x000580C4 File Offset: 0x000562C4
	private void GenerateSpawnPointLandingZones(SpawnPoint spawnPoint)
	{
		GraphNode graphNode = this.FindClosestNode(spawnPoint.transform.position, PathfindingBox.Type.Infantry, -1);
		if (graphNode == null)
		{
			throw new Exception("Could not find an infantry node close to spawnpoint " + ((spawnPoint != null) ? spawnPoint.ToString() : null));
		}
		List<GraphNode> traversalNodes = new List<GraphNode>(1024);
		traversalNodes.Add(graphNode);
		Debug.DrawLine(spawnPoint.transform.position, (Vector3)graphNode.position + Vector3.up * 5f, Color.red, 100f);
		Vector3 vector = (Vector3)graphNode.position;
		float num = 0f;
		bool flag = true;
		Collider[] results = new Collider[128];
		int num2 = 0;
		Action<GraphNode> <>9__0;
		while (num2 < 512 && num2 < traversalNodes.Count)
		{
			GraphNode graphNode2 = traversalNodes[num2];
			if (flag)
			{
				GraphNode graphNode3 = graphNode2;
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode newNode)
					{
						if (newNode.Tag != 3U && !traversalNodes.Contains(newNode))
						{
							traversalNodes.Add(newNode);
						}
					});
				}
				graphNode3.GetConnections(action);
				flag = (traversalNodes.Count < 512);
			}
			float num3 = (graphNode2.SurfaceArea() - 1000000f) * 1E-05f;
			if (num3 >= 0f)
			{
				Vector3 vector2 = (Vector3)graphNode2.position;
				if ((vector2 - spawnPoint.transform.position).ToGround().magnitude < 200f + spawnPoint.GetCaptureRange())
				{
					TriangleMeshNode triangleMeshNode = graphNode2 as TriangleMeshNode;
					RaycastHit raycastHit;
					if (triangleMeshNode != null && Physics.Raycast(new Ray(vector2 + new Vector3(0f, 6f, 0f), Vector3.down), out raycastHit, 20f, 2232321))
					{
						vector2 = raycastHit.point;
						float num4 = (PathfindingManager.GetNodeNormal(triangleMeshNode).y - 0.95f) * 10f;
						if (num4 >= 0f)
						{
							float num5 = num4 * num3;
							if (num5 > num)
							{
								new Ray(vector2, Vector3.up);
								Vector3 center = vector2 + new Vector3(0f, 4f, 0f);
								Vector3 halfExtents = new Vector3(10f, 1f, 10f);
								Vector3 vector3 = vector2 + new Vector3(0f, 10.2f, 0f);
								Vector3 point = vector3 + new Vector3(0f, 1000f, 0f);
								bool flag2 = Physics.OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, 3280897) == 0;
								bool flag3 = Physics.OverlapCapsuleNonAlloc(vector3, point, 10f, results, 3280897) == 0;
								if (flag2 && flag3)
								{
									float num6 = 0f;
									if ((!WaterLevel.IsInWater(vector2, out num6) || num6 <= 0.4f) && this.CastLandingZoneCircleRays(vector2))
									{
										vector = vector2;
										num = num5;
									}
								}
							}
						}
					}
				}
			}
			num2++;
		}
		if (num > 0f)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(vector + new Vector3(0f, 50f, 0f), Vector3.down), out raycastHit, 100f, 2232321))
			{
				vector = raycastHit.point;
			}
			spawnPoint.SetGeneratedHelicopterLandingZone(vector);
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00058408 File Offset: 0x00056608
	private bool CastLandingZoneCircleRays(Vector3 landingPoint)
	{
		for (int i = 0; i < 8; i++)
		{
			float f = (float)i / 8f * 2f * 3.1415927f;
			Vector3 a = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f));
			Ray ray = new Ray(landingPoint + a * 6f + new Vector3(0f, 3f, 0f), Vector3.down);
			bool flag = Physics.Raycast(ray, 4.5f, 2232321);
			Debug.DrawRay(ray.origin, ray.direction * 4.5f, flag ? Color.green : Color.red, 100f);
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x000584D8 File Offset: 0x000566D8
	public static Vector3 GetNodeNormal(TriangleMeshNode node)
	{
		Vector3 b = (Vector3)node.GetVertex(0);
		Vector3 a = (Vector3)node.GetVertex(1);
		Vector3 a2 = (Vector3)node.GetVertex(2);
		Vector3 vector = Vector3.Cross(a - b, a2 - b).normalized;
		if (vector.y < 0f)
		{
			vector = -vector;
		}
		return vector;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0005853C File Offset: 0x0005673C
	public void ScanCustomLevel()
	{
		try
		{
			foreach (Progress progress in this.ScanCustomLevelAsync())
			{
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x000050B9 File Offset: 0x000032B9
	public IEnumerable<Progress> ScanCustomLevelAsync()
	{
		this.SetupImplicitRelevantPathfindingPoints();
		foreach (Progress progress in this.FindAndScanAsync())
		{
			yield return progress;
		}
		IEnumerator<Progress> enumerator = null;
		base.GetComponent<CoverPlacer>().Generate();
		yield break;
		yield break;
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00058598 File Offset: 0x00056798
	private void SetupImplicitRelevantPathfindingPoints()
	{
		SpawnPoint[] array = UnityEngine.Object.FindObjectsOfType<SpawnPoint>();
		VehicleSpawner[] array2 = UnityEngine.Object.FindObjectsOfType<VehicleSpawner>();
		foreach (SpawnPoint spawnPoint in array)
		{
			if (spawnPoint.isRelevantPathfindingPoint)
			{
				this.AddRelevantPathfindingPoint(spawnPoint.transform.position + Vector3.up, PathfindingRelevantPoint.Type.Ground);
			}
		}
		foreach (VehicleSpawner vehicleSpawner in array2)
		{
			if (vehicleSpawner.VehiclePrefabIsWatercraft())
			{
				this.AddRelevantPathfindingPoint(vehicleSpawner.transform.position + Vector3.up, PathfindingRelevantPoint.Type.Water);
			}
		}
		MeoCapturePoint[] array5 = UnityEngine.Object.FindObjectsOfType<MeoCapturePoint>();
		MeoVehicleSpawner[] array6 = UnityEngine.Object.FindObjectsOfType<MeoVehicleSpawner>();
		foreach (MeoCapturePoint meoCapturePoint in array5)
		{
			this.AddRelevantPathfindingPoint(meoCapturePoint.transform.position + Vector3.up, PathfindingRelevantPoint.Type.Ground);
		}
		foreach (MeoVehicleSpawner meoVehicleSpawner in array6)
		{
			if (VehicleSpawner.TypeIsWatercraft(meoVehicleSpawner.vehicle))
			{
				this.AddRelevantPathfindingPoint(meoVehicleSpawner.transform.position + Vector3.up, PathfindingRelevantPoint.Type.Water);
			}
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x000050C9 File Offset: 0x000032C9
	private void AddRelevantPathfindingPoint(Vector3 position, PathfindingRelevantPoint.Type type)
	{
		new GameObject("Relevant Pathfinding Point")
		{
			transform = 
			{
				position = position
			}
		}.AddComponent<PathfindingRelevantPoint>().type = type;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000586AC File Offset: 0x000568AC
	public void GenerateNavmeshCache(string filePath, bool editorSettings)
	{
		this.navMeshLog = "Navmesh cache saved to " + filePath;
		this.navMeshError = false;
		try
		{
			SerializeSettings serializeSettings = new SerializeSettings();
			serializeSettings.nodes = true;
			if (editorSettings)
			{
				serializeSettings.editorSettings = true;
			}
			byte[] bytes = this.pathfinder.data.SerializeGraphs(serializeSettings);
			File.WriteAllBytes(filePath, bytes);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			this.navMeshLog = "Unable to write navmesh cache file, " + ex.Message;
			this.navMeshError = true;
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000050EC File Offset: 0x000032EC
	public void GenerateCoverPointList(string filePath)
	{
		CoverPointList.Serialize(base.GetComponentsInChildren<CoverPoint>(), filePath);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000050FB File Offset: 0x000032FB
	public void GenerateCoverPointList(Stream stream)
	{
		CoverPointList.Serialize(base.GetComponentsInChildren<CoverPoint>(), stream);
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00005109 File Offset: 0x00003309
	public void LoadCustomGraphCache(TextAsset cacheAsset)
	{
		this.pathfinder.data.file_cachedStartup = cacheAsset;
		this.pathfinder.data.LoadFromCache();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0000512C File Offset: 0x0000332C
	public void LoadCustomCoverPoints(TextAsset cacheAsset)
	{
		CoverPointList.Deserialize(base.transform, cacheAsset);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0000513B File Offset: 0x0000333B
	public void LoadCustomCoverPoints(Stream stream)
	{
		CoverPointList.Deserialize(base.transform, stream);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0005873C File Offset: 0x0005693C
	public void ApplyAvoidanceBoxes()
	{
		AvoidanceBox[] avoidanceBoxes = UnityEngine.Object.FindObjectsOfType<AvoidanceBox>();
		if (GameManager.instance.generateNavCache)
		{
			this.avoidanceNodes = new List<GraphNode>();
		}
		NavGraph[] graphs = this.pathfinder.data.graphs;
		for (int i = 0; i < graphs.Length; i++)
		{
			NavGraph navGraph = graphs[i];
			if (navGraph != null)
			{
				PathfindingBox.Type graphType = PathfindingManager.GetTypeFromGraphName(navGraph);
				navGraph.GetNodes(delegate(GraphNode node)
				{
					AvoidanceBox[] avoidanceBoxes;
					foreach (AvoidanceBox avoidanceBox in avoidanceBoxes)
					{
						if ((avoidanceBox.applyToAllTypes || avoidanceBox.type == graphType) && avoidanceBox.Contains((Vector3)node.position))
						{
							if (avoidanceBox.unwalkable)
							{
								node.Walkable = false;
							}
							else
							{
								node.Penalty += avoidanceBox.penalty;
							}
							if (GameManager.instance.generateNavCache)
							{
								this.avoidanceNodes.Add(node);
							}
						}
					}
				});
			}
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000587D0 File Offset: 0x000569D0
	public List<GraphNode> GetGraphNodesInBoxTransform(Transform transform, PathfindingBox.Type type)
	{
		List<GraphNode> nodes = new List<GraphNode>();
		Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
		Action<GraphNode> <>9__0;
		foreach (NavGraph navGraph in this.pathfinder.data.graphs)
		{
			if (navGraph != null && PathfindingManager.GetTypeFromGraphName(navGraph) == type)
			{
				NavGraph navGraph2 = navGraph;
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node.Walkable)
						{
							Vector3 vector = worldToLocalMatrix.MultiplyPoint((Vector3)node.position);
							if (Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f)
							{
								nodes.Add(node);
							}
						}
					});
				}
				navGraph2.GetNodes(action);
			}
		}
		return nodes;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00058858 File Offset: 0x00056A58
	public GraphNode FindClosestNode(Vector3 position, PathfindingBox.Type type, int tagMask = -1)
	{
		NNConstraint nnconstraint = new NNConstraint();
		nnconstraint.graphMask = PathfindingManager.GetMaskFromGraphType(type);
		nnconstraint.constrainDistance = false;
		nnconstraint.constrainWalkability = true;
		nnconstraint.walkable = true;
		nnconstraint.tags = tagMask;
		nnconstraint.constrainTags = (tagMask != -1);
		return AstarPath.active.GetNearest(position, nnconstraint).node;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x000588B8 File Offset: 0x00056AB8
	public void FindClosestNavmeshesToSpawnPoints()
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			GraphNode graphNode = this.FindClosestNode(spawnPoint.transform.position, PathfindingBox.Type.Infantry, -1);
			if (graphNode != null)
			{
				spawnPoint.closestInfantryNavmeshArea = graphNode.Area;
			}
			graphNode = this.FindClosestNode(spawnPoint.transform.position, PathfindingBox.Type.Car, -1);
			if (graphNode != null)
			{
				spawnPoint.closestCarNavmeshArea = graphNode.Area;
			}
			graphNode = this.FindClosestNode(spawnPoint.transform.position, PathfindingBox.Type.Boat, -1);
			if (graphNode != null)
			{
				spawnPoint.closestBoatNavmeshArea = graphNode.Area;
			}
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0005894C File Offset: 0x00056B4C
	public static bool ShouldEjectFromAircraftAtPosition(Vector3 position)
	{
		GraphNode graphNode = PathfindingManager.instance.FindClosestNode(position, PathfindingBox.Type.Infantry, -1);
		if (graphNode != null)
		{
			Vector3 b = (Vector3)graphNode.position;
			Vector3 vector = position - b;
			return vector.ToGround().magnitude * 2f < vector.y;
		}
		return false;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00005149 File Offset: 0x00003349
	public bool IsRenderingNavMesh()
	{
		return this.renderingNavmeshes;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0005899C File Offset: 0x00056B9C
	public void HideNavmeshes()
	{
		if (this.displayedCoverPoints != null)
		{
			foreach (GameObject obj in this.displayedCoverPoints)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.displayedCoverPoints.Clear();
		}
		if (this.navMeshRenderObjects != null)
		{
			foreach (GameObject obj2 in this.navMeshRenderObjects)
			{
				UnityEngine.Object.Destroy(obj2);
			}
			this.navMeshRenderObjects.Clear();
		}
		if (this.navMeshRenderObjectsByType != null)
		{
			this.navMeshRenderObjectsByType.Clear();
		}
		if (this.nextColorIndex != null)
		{
			this.nextColorIndex.Clear();
		}
		if (this.areaColor != null)
		{
			this.areaColor.Clear();
		}
		this.renderingNavmeshes = false;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00058A94 File Offset: 0x00056C94
	public void DisplayNavmeshes()
	{
		Debug.Log("Converting navmeshes into meshes for rendering");
		this.displayedCoverPoints = new List<GameObject>();
		this.navMeshRenderObjectsByType = new Dictionary<PathfindingBox.Type, List<GameObject>>();
		this.navMeshRenderObjects = new List<GameObject>();
		this.renderingNavmeshes = true;
		this.navMeshRenderObjectsByType.Add(PathfindingBox.Type.Boat, new List<GameObject>());
		this.navMeshRenderObjectsByType.Add(PathfindingBox.Type.Car, new List<GameObject>());
		this.navMeshRenderObjectsByType.Add(PathfindingBox.Type.Infantry, new List<GameObject>());
		this.nextColorIndex = new Dictionary<PathfindingBox.Type, int>(3);
		this.nextColorIndex.Add(PathfindingBox.Type.Boat, 0);
		this.nextColorIndex.Add(PathfindingBox.Type.Car, 0);
		this.nextColorIndex.Add(PathfindingBox.Type.Infantry, 0);
		this.areaColor = new Dictionary<uint, Color>();
		foreach (NavGraph navGraph in this.pathfinder.data.graphs)
		{
			if (navGraph != null && navGraph.GetType() == typeof(RecastGraph))
			{
				this.AddRecastGraphRenderObject((RecastGraph)navGraph);
			}
		}
		foreach (NavGraph navGraph2 in this.pathfinder.data.graphs)
		{
			if (navGraph2 != null && navGraph2.GetType() == typeof(PointGraph))
			{
				this.AddPointGraphRenderObject((PointGraph)navGraph2);
			}
		}
		foreach (CoverPoint coverPoint in base.GetComponentsInChildren<CoverPoint>())
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.coverPointDisplayPrefab, coverPoint.transform.position, coverPoint.transform.rotation);
			this.displayedCoverPoints.Add(gameObject);
			Renderer component = gameObject.GetComponent<Renderer>();
			component.material.color = ((coverPoint.type == CoverPoint.Type.Crouch) ? Color.blue : Color.green);
			component.GetComponentInChildren<TextMesh>().text = coverPoint.coverage.ToString();
		}
		this.navMeshRenderObjectIndex = 0;
		this.navMeshRenderType = PathfindingBox.Type.Infantry;
		this.NavMeshRenderType();
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00058C7C File Offset: 0x00056E7C
	private Color GetAreaColor(uint area, PathfindingBox.Type type)
	{
		if (!this.areaColor.ContainsKey(area))
		{
			this.areaColor.Add(area, PathfindingManager.AREA_COLORS[this.nextColorIndex[type] % PathfindingManager.AREA_COLORS.Length]);
			Dictionary<PathfindingBox.Type, int> dictionary = this.nextColorIndex;
			int num = dictionary[type];
			dictionary[type] = num + 1;
		}
		return this.areaColor[area];
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00058CE8 File Offset: 0x00056EE8
	private void AddRecastGraphRenderObject(RecastGraph recastGraph)
	{
		Mesh mesh = new Mesh
		{
			indexFormat = IndexFormat.UInt32
		};
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Color> colors = new List<Color>();
		PathfindingBox.Type graphType = PathfindingManager.GetTypeFromGraphName(recastGraph);
		recastGraph.GetNodes(delegate(GraphNode obj)
		{
			MeshNode meshNode = (MeshNode)obj;
			vertices.Add((Vector3)meshNode.GetVertex(0));
			vertices.Add((Vector3)meshNode.GetVertex(1));
			vertices.Add((Vector3)meshNode.GetVertex(2));
			Color item = Color.black;
			if (meshNode.Walkable)
			{
				item = this.GetAreaColor(meshNode.Area, graphType);
				if (this.avoidanceNodes != null && this.avoidanceNodes.Contains(obj))
				{
					item = Color.red;
				}
			}
			colors.Add(item);
			colors.Add(item);
			colors.Add(item);
			triangles.Add(vertices.Count - 3);
			triangles.Add(vertices.Count - 2);
			triangles.Add(vertices.Count - 1);
		});
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.colors = colors.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		GameObject gameObject = new GameObject(recastGraph.name);
		gameObject.transform.position += Vector3.up * 0.3f;
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = this.meshRenderMaterial;
		foreach (PathfindingRelevantPoint pathfindingRelevantPoint in this.foundRelevantPoints[recastGraph])
		{
			GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject2.transform.position = pathfindingRelevantPoint.transform.position;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localScale = new Vector3(3f, 3f, 3f);
			Vector3 vector = recastGraph.GetNearest(pathfindingRelevantPoint.transform.position).clampedPosition - pathfindingRelevantPoint.transform.position;
			GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			gameObject3.transform.position = pathfindingRelevantPoint.transform.position + vector * 0.5f;
			gameObject3.transform.rotation = Quaternion.LookRotation(Vector3.Cross(vector, Vector3.one), vector);
			gameObject3.transform.localScale = new Vector3(1f, vector.magnitude / 2f, 1f);
			gameObject3.transform.parent = gameObject.transform;
		}
		this.navMeshRenderObjects.Add(gameObject);
		this.navMeshRenderObjectsByType[PathfindingManager.GetTypeFromGraphName(recastGraph)].Add(gameObject);
		gameObject.SetActive(false);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00058F58 File Offset: 0x00057158
	private void AddPointGraphRenderObject(PointGraph pointGraph)
	{
		Mesh mesh = new Mesh();
		mesh.indexFormat = IndexFormat.UInt32;
		List<Vector3> vertices = new List<Vector3>();
		List<int> indices = new List<int>();
		List<Color> colors = new List<Color>();
		PathfindingBox.Type graphType = PathfindingBox.Type.Boat;
		pointGraph.GetNodes(delegate(GraphNode node)
		{
			int rootIndex = vertices.Count;
			vertices.Add((Vector3)node.position);
			Color c = node.Walkable ? this.GetAreaColor(node.Area, graphType) : Color.black;
			colors.Add(c);
			node.GetConnections(delegate(GraphNode connectedNode)
			{
				int count = vertices.Count;
				vertices.Add((Vector3)connectedNode.position);
				indices.Add(rootIndex);
				indices.Add(count);
				colors.Add(c);
			});
		});
		mesh.vertices = vertices.ToArray();
		mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
		mesh.colors = colors.ToArray();
		mesh.RecalculateBounds();
		GameObject gameObject = new GameObject(pointGraph.name);
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = this.meshRenderMaterial;
		this.navMeshRenderObjects.Add(gameObject);
		this.navMeshRenderObjectsByType[PathfindingBox.Type.Boat].Add(gameObject);
		this.navMeshRenderObjectsByType[PathfindingBox.Type.Car].Add(gameObject);
		this.navMeshRenderObjectsByType[PathfindingBox.Type.Infantry].Add(gameObject);
		gameObject.SetActive(false);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00059068 File Offset: 0x00057268
	private void IncrementNavMeshRenderIndex(int d)
	{
		foreach (GameObject gameObject in this.navMeshRenderObjects)
		{
			gameObject.SetActive(false);
		}
		this.navMeshRenderObjectIndex += d;
		if (this.navMeshRenderObjectIndex >= this.navMeshRenderObjects.Count)
		{
			this.navMeshRenderObjectIndex = 0;
		}
		else if (this.navMeshRenderObjectIndex < 0)
		{
			this.navMeshRenderObjectIndex = this.navMeshRenderObjects.Count - 1;
		}
		this.navMeshRenderObjects[this.navMeshRenderObjectIndex].SetActive(true);
		this.navMeshDrawingInfo = this.navMeshRenderObjects[this.navMeshRenderObjectIndex].name;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00005151 File Offset: 0x00003351
	private void NavMeshRenderNextType()
	{
		if (this.navMeshRenderType == PathfindingBox.Type.Boat)
		{
			this.navMeshRenderType = PathfindingBox.Type.Infantry;
		}
		else if (this.navMeshRenderType == PathfindingBox.Type.Infantry)
		{
			this.navMeshRenderType = PathfindingBox.Type.Car;
		}
		else
		{
			this.navMeshRenderType = PathfindingBox.Type.Boat;
		}
		this.NavMeshRenderType();
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00005183 File Offset: 0x00003383
	private void NavMeshRenderPreviousType()
	{
		if (this.navMeshRenderType == PathfindingBox.Type.Boat)
		{
			this.navMeshRenderType = PathfindingBox.Type.Car;
		}
		else if (this.navMeshRenderType == PathfindingBox.Type.Infantry)
		{
			this.navMeshRenderType = PathfindingBox.Type.Boat;
		}
		else
		{
			this.navMeshRenderType = PathfindingBox.Type.Infantry;
		}
		this.NavMeshRenderType();
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00059134 File Offset: 0x00057334
	private void NavMeshRenderType()
	{
		foreach (GameObject gameObject in this.navMeshRenderObjects)
		{
			gameObject.SetActive(false);
		}
		foreach (GameObject gameObject2 in this.navMeshRenderObjectsByType[this.navMeshRenderType])
		{
			gameObject2.SetActive(true);
		}
		this.navMeshDrawingInfo = "All " + this.navMeshRenderType.ToString() + " navmesh graphs";
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000591F8 File Offset: 0x000573F8
	private void OnGUI()
	{
		bool flag = this.renderingNavmeshes;
		bool flag2 = GameManager.instance && GameManager.instance.generateNavCache;
		if (flag || flag2)
		{
			Rect position = new Rect(0f, 0f, 1000f, 200f);
			if (MapEditor.IsAvailable())
			{
				position = new Rect(235f, 35f, 1000f, 200f);
			}
			GUI.BeginGroup(position);
			if (flag)
			{
				GUI.color = Color.black;
				GUI.Label(new Rect(6f, 1f, 1000f, 50f), this.navMeshDrawingInfo);
				GUI.color = Color.white;
				GUI.Label(new Rect(5f, 0f, 1000f, 50f), this.navMeshDrawingInfo);
				GUI.color = Color.black;
				GUI.Label(new Rect(6f, 21f, 1000f, 200f), this.navMeshLog);
				GUI.color = (this.navMeshError ? Color.red : Color.white);
				GUI.Label(new Rect(5f, 20f, 1000f, 200f), this.navMeshLog);
			}
			else if (flag2)
			{
				GUI.color = Color.black;
				GUI.Label(new Rect(10f, 10f, 1000f, 200f), "SCANNING PATHFINDING & GENERATING NAVMESH. THIS MAY TAKE A WHILE!");
			}
			GUI.EndGroup();
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000051B5 File Offset: 0x000033B5
	public static bool HasAnyBoatGraphs()
	{
		return PathfindingManager.boatGraphMask != 0;
	}

	// Token: 0x040004C8 RID: 1224
	public const int DEFAULT_TAG = 0;

	// Token: 0x040004C9 RID: 1225
	public const uint STEEP_TAG = 1U;

	// Token: 0x040004CA RID: 1226
	public const uint SHALLOW_WATER_TAG = 2U;

	// Token: 0x040004CB RID: 1227
	public const uint UNDERWATER_TAG = 3U;

	// Token: 0x040004CC RID: 1228
	public const uint ALTERNATIVE_PATH_PENALTY_TEAM0_TAG = 4U;

	// Token: 0x040004CD RID: 1229
	public const uint ALTERNATIVE_PATH_PENALTY_TEAM1_TAG = 5U;

	// Token: 0x040004CE RID: 1230
	public const uint ALTERNATIVE_PATH_PENALTY_ALL_TEAMS_TAG = 6U;

	// Token: 0x040004CF RID: 1231
	public const uint LADDER_TAG = 31U;

	// Token: 0x040004D0 RID: 1232
	public static readonly Color[] AREA_COLORS = new Color[]
	{
		Color.white,
		Color.blue,
		Color.green,
		Color.yellow,
		Color.magenta,
		Color.cyan
	};

	// Token: 0x040004D1 RID: 1233
	private const float RELEVANT_POINT_MAX_DISTANCE = 20f;

	// Token: 0x040004D2 RID: 1234
	private const float LADDER_POINT_NODE_DISTANCE = 1f;

	// Token: 0x040004D3 RID: 1235
	private const int NUMBER_ALTERNATIVE_PATH_NODES_TRACKED_INFANTRY = 1024;

	// Token: 0x040004D4 RID: 1236
	private const int NUMBER_ALTERNATIVE_PATH_NODES_TRACKED_CAR = 512;

	// Token: 0x040004D5 RID: 1237
	private const int NUMBER_ALTERNATIVE_PATH_NODES_TRACKED_BOAT = 512;

	// Token: 0x040004D6 RID: 1238
	private const int WATER_LAYER = 4;

	// Token: 0x040004D7 RID: 1239
	private const int INFANTRY_LAYER_MASK = 1;

	// Token: 0x040004D8 RID: 1240
	private const int CAR_LAYER_MASK = 4097;

	// Token: 0x040004D9 RID: 1241
	private const int BOAT_LAYER_MASK = 4113;

	// Token: 0x040004DA RID: 1242
	private const int GENERATE_LANDING_ZONE_MAX_NODE_TRAVERSALS = 512;

	// Token: 0x040004DB RID: 1243
	private const float GENERATE_LANDING_ZONE_MAX_DISTANCE = 200f;

	// Token: 0x040004DC RID: 1244
	private const float GENERATE_LANDING_ZONE_SPHERECAST_RADIUS = 10f;

	// Token: 0x040004DD RID: 1245
	private const float GENERATE_LANDING_ZONE_MIN_NORMAL_Y = 0.95f;

	// Token: 0x040004DE RID: 1246
	private const float GENERATE_LANDING_ZONE_MIN_AREA = 1000000f;

	// Token: 0x040004DF RID: 1247
	private const float AREA_SCORE_MULTIPLIER = 1E-05f;

	// Token: 0x040004E0 RID: 1248
	private const float NORMAL_SCORE_MULTIPLIER = 10f;

	// Token: 0x040004E1 RID: 1249
	private const float GENERATE_LANDING_ZONE_MAX_WATER_DEPTH = 0.4f;

	// Token: 0x040004E2 RID: 1250
	private const int GENERATE_LANDING_ZONE_CLEAR_MASK = 3280897;

	// Token: 0x040004E3 RID: 1251
	private const int GENERATE_LANDING_ZONE_CIRCLE_RAYS = 8;

	// Token: 0x040004E4 RID: 1252
	private const float GENERATE_LANDING_ZONE_CIRCLE_RAYS_RADIUS = 6f;

	// Token: 0x040004E5 RID: 1253
	private const float GENERATE_LANDING_ZONE_CIRCLE_RAYS_OFFSET = 3f;

	// Token: 0x040004E6 RID: 1254
	private const float GENERATE_LANDING_ZONE_CIRCLE_RAYS_RANGE = 4.5f;

	// Token: 0x040004E7 RID: 1255
	private const float BOAT_MAX_SLOPE = 1f;

	// Token: 0x040004E8 RID: 1256
	private const int TILE_SIZE = 128;

	// Token: 0x040004E9 RID: 1257
	private const int MAX_NUMBER_OF_TILES = 2048;

	// Token: 0x040004EA RID: 1258
	private const uint DEATH_PENALTY_AMOUNT = 50000U;

	// Token: 0x040004EB RID: 1259
	private const float DEATH_PENALTY_DURATION = 60f;

	// Token: 0x040004EC RID: 1260
	public static int infantryGraphMask = 1;

	// Token: 0x040004ED RID: 1261
	public static int carGraphMask = 1;

	// Token: 0x040004EE RID: 1262
	public static int boatGraphMask = 1;

	// Token: 0x040004EF RID: 1263
	public static PathfindingManager instance;

	// Token: 0x040004F0 RID: 1264
	public GameObject coverPointDisplayPrefab;

	// Token: 0x040004F1 RID: 1265
	public GameObject waterRecastMeshPrefab;

	// Token: 0x040004F2 RID: 1266
	private List<GraphNode> avoidanceNodes;

	// Token: 0x040004F3 RID: 1267
	private List<GameObject> displayedCoverPoints;

	// Token: 0x040004F4 RID: 1268
	public Material meshRenderMaterial;

	// Token: 0x040004F5 RID: 1269
	private bool renderingNavmeshes;

	// Token: 0x040004F6 RID: 1270
	private List<GameObject> navMeshRenderObjects;

	// Token: 0x040004F7 RID: 1271
	private List<GameObject> ladderLinks;

	// Token: 0x040004F8 RID: 1272
	private Dictionary<PathfindingBox.Type, List<GameObject>> navMeshRenderObjectsByType;

	// Token: 0x040004F9 RID: 1273
	private int navMeshRenderObjectIndex;

	// Token: 0x040004FA RID: 1274
	private PathfindingBox.Type navMeshRenderType;

	// Token: 0x040004FB RID: 1275
	private string navMeshDrawingInfo = "";

	// Token: 0x040004FC RID: 1276
	private string navMeshLog = "";

	// Token: 0x040004FD RID: 1277
	private bool navMeshError;

	// Token: 0x040004FE RID: 1278
	private RecastGraph infantryGraph;

	// Token: 0x040004FF RID: 1279
	[NonSerialized]
	public AstarPath pathfinder;

	// Token: 0x04000500 RID: 1280
	[NonSerialized]
	public Dictionary<RecastGraph, PathfindingBox> graphToBox;

	// Token: 0x04000501 RID: 1281
	private Dictionary<RecastGraph, List<PathfindingRelevantPoint>> foundRelevantPoints;

	// Token: 0x04000502 RID: 1282
	private Dictionary<uint, Color> areaColor;

	// Token: 0x04000503 RID: 1283
	private Dictionary<PathfindingBox.Type, int> nextColorIndex;

	// Token: 0x04000504 RID: 1284
	private PathfindingManager.AlternativePathNodeSet infantryAlternativePathSet = new PathfindingManager.AlternativePathNodeSet(1024);

	// Token: 0x04000505 RID: 1285
	private PathfindingManager.AlternativePathNodeSet carAlternativePathSet = new PathfindingManager.AlternativePathNodeSet(512);

	// Token: 0x04000506 RID: 1286
	private PathfindingManager.AlternativePathNodeSet boatAlternativePathSet = new PathfindingManager.AlternativePathNodeSet(512);

	// Token: 0x04000507 RID: 1287
	private List<GameObject> instantiatedRecastWaterMeshes;

	// Token: 0x0200009B RID: 155
	public struct AlternativePathNodeSet
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x000051BF File Offset: 0x000033BF
		public AlternativePathNodeSet(int nNodes)
		{
			this.nodes = new PathfindingManager.AlternativePathPenaltyNode[nNodes];
			this.nextIndex = 0;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000051D4 File Offset: 0x000033D4
		public void UpdateTag(uint newTag, GraphNode node)
		{
			this.nodes[this.nextIndex].UpdateTag(newTag, node);
			this.nextIndex = (this.nextIndex + 1) % this.nodes.Length;
		}

		// Token: 0x04000508 RID: 1288
		public PathfindingManager.AlternativePathPenaltyNode[] nodes;

		// Token: 0x04000509 RID: 1289
		private int nextIndex;
	}

	// Token: 0x0200009C RID: 156
	public struct AlternativePathPenaltyNode
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00005205 File Offset: 0x00003405
		public void UpdateTag(uint newTag, GraphNode node)
		{
			if (this.node != null)
			{
				this.node.Tag = this.previousTag;
			}
			this.previousTag = node.Tag;
			node.Tag = newTag;
			this.node = node;
		}

		// Token: 0x0400050A RID: 1290
		public uint previousTag;

		// Token: 0x0400050B RID: 1291
		public GraphNode node;
	}
}

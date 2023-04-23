using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003C8 RID: 968
	public class SpecOpsPatrolGenerator
	{
		// Token: 0x06001813 RID: 6163 RVA: 0x00012A98 File Offset: 0x00010C98
		public SpecOpsPatrolGenerator(SpecOpsMode specOps)
		{
			this.specOps = specOps;
			this.PreprocessNavmesh();
			this.PopulateWaypoints();
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00012AB3 File Offset: 0x00010CB3
		public void PopulateWaypoints()
		{
			this.waypointQueriesLeft = new Dictionary<PathfindingBox.Type, int>(3);
			this.waypoints = new Dictionary<PathfindingBox.Type, List<Vector3>>(3);
			this.PopulateWaypointsOfType(PathfindingBox.Type.Infantry);
			this.PopulateWaypointsOfType(PathfindingBox.Type.Car);
			this.PopulateWaypointsOfType(PathfindingBox.Type.Boat);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000A4168 File Offset: 0x000A2368
		private void PopulateWaypointsOfType(PathfindingBox.Type type)
		{
			this.waypointQueriesLeft.Add(type, 0);
			this.waypoints.Add(type, new List<Vector3>());
			foreach (SpawnPointConnectionGraph spawnPointConnectionGraph in SpawnPointNeighborManager.GetSortedConnectionGraphsByNavmesh(type))
			{
				foreach (SpecOpsScenario specOpsScenario in this.specOps.activeScenarios)
				{
					if (spawnPointConnectionGraph.Contains(specOpsScenario.spawn))
					{
						this.PopulateWaypointsOfGraph(spawnPointConnectionGraph, type);
						return;
					}
				}
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000A422C File Offset: 0x000A242C
		private void PopulateWaypointsOfGraph(SpawnPointConnectionGraph graph, PathfindingBox.Type type)
		{
			int maskFromGraphType = PathfindingManager.GetMaskFromGraphType(type);
			OnPathDelegate <>9__0;
			for (int i = 0; i < graph.spawns.Count; i++)
			{
				SpawnPoint spawnPoint = graph.spawns[i];
				for (int j = i + 1; j < graph.spawns.Count; j++)
				{
					SpawnPoint spawnPoint2 = graph.spawns[j];
					Vector3 position = spawnPoint.transform.position;
					Vector3 position2 = spawnPoint2.transform.position;
					OnPathDelegate callback;
					if ((callback = <>9__0) == null)
					{
						callback = (<>9__0 = delegate(Path p)
						{
							this.OnWaypointPathCompleted(p, type);
						});
					}
					Path path = ABPath.Construct(position, position2, callback);
					path.nnConstraint.graphMask = maskFromGraphType;
					path.nnConstraint.constrainWalkability = true;
					path.nnConstraint.walkable = true;
					path.nnConstraint.constrainDistance = false;
					path.nnConstraint.constrainArea = true;
					path.nnConstraint.area = (int)graph.areaId;
					if (type != PathfindingBox.Type.Boat)
					{
						path.nnConstraint.constrainTags = true;
						path.nnConstraint.tags = -9;
						path.enabledTags = path.nnConstraint.tags;
					}
					AstarPath.StartPath(path, false);
					Dictionary<PathfindingBox.Type, int> dictionary = this.waypointQueriesLeft;
					PathfindingBox.Type type2 = type;
					int num = dictionary[type2];
					dictionary[type2] = num + 1;
				}
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000A43AC File Offset: 0x000A25AC
		private void OnWaypointPathCompleted(Path path, PathfindingBox.Type type)
		{
			Dictionary<PathfindingBox.Type, int> dictionary = this.waypointQueriesLeft;
			int num = dictionary[type];
			dictionary[type] = num - 1;
			if (!path.error && path.vectorPath.Count > 10)
			{
				for (int i = 0; i < SpecOpsPatrolGenerator.WAYPOINT_NORMALIZED_PATH_DISTANCES.Length; i++)
				{
					int index = Mathf.FloorToInt((SpecOpsPatrolGenerator.WAYPOINT_NORMALIZED_PATH_DISTANCES[i] + UnityEngine.Random.Range(-0.2f, 0.2f)) * (float)path.vectorPath.Count);
					this.waypoints[type].Add(path.vectorPath[index]);
				}
			}
			if (this.AllWaypointQueriesDone())
			{
				this.CreatePatrolPaths();
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00012AE2 File Offset: 0x00010CE2
		private bool AllWaypointQueriesDone()
		{
			return this.waypointQueriesLeft[PathfindingBox.Type.Boat] <= 0 && this.waypointQueriesLeft[PathfindingBox.Type.Car] <= 0 && this.waypointQueriesLeft[PathfindingBox.Type.Infantry] <= 0;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00012B16 File Offset: 0x00010D16
		private void CreatePatrolPaths()
		{
			this.ClusterWaypoints();
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000A4450 File Offset: 0x000A2650
		private void ClusterWaypoints()
		{
			foreach (PathfindingBox.Type type in this.waypoints.Keys)
			{
				List<Vector3> list = this.waypoints[type];
				if (list.Count != 0)
				{
					bool flag = type == PathfindingBox.Type.Infantry;
					this.RemoveFarawayWaypoints(list, 300f);
					this.DecimateWaypoints(list, !flag);
					List<Vector3> list2 = this.ConstructWaypointPath(list);
					if (list2.Count > 1)
					{
						this.specOps.RegisterPatrol(type, list2);
					}
				}
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000A44F4 File Offset: 0x000A26F4
		private void RemoveFarawayWaypoints(List<Vector3> waypoints, float threshold)
		{
			waypoints.RemoveAll((Vector3 w) => this.DistanceFromActiveScenarioOrPlayerSpawn(w) > threshold);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000A4528 File Offset: 0x000A2728
		private float DistanceFromActiveScenarioOrPlayerSpawn(Vector3 position)
		{
			float num = float.MaxValue;
			foreach (SpecOpsScenario specOpsScenario in this.specOps.activeScenarios)
			{
				num = Mathf.Min(num, Vector3.Distance(position, specOpsScenario.spawn.transform.position));
			}
			return num;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000A45A0 File Offset: 0x000A27A0
		private void DecimateWaypoints(List<Vector3> waypoints, bool biasAwayFromCenter)
		{
			int num = Mathf.Max(waypoints.Count / 3, 4);
			int num2 = waypoints.Count - num;
			Vector3 vector = Vector3.zero;
			foreach (Vector3 b in waypoints)
			{
				vector += b;
			}
			vector /= (float)waypoints.Count;
			for (int i = 0; i < num2; i++)
			{
				int num3 = -1;
				int index = -1;
				float num4 = float.MaxValue;
				for (int j = 0; j < waypoints.Count; j++)
				{
					for (int k = j + 1; k < waypoints.Count; k++)
					{
						float num5 = Vector3.Distance(waypoints[j], waypoints[k]);
						if (num5 < num4)
						{
							num3 = j;
							index = k;
							num4 = num5;
						}
					}
				}
				if (num3 == -1)
				{
					return;
				}
				if (biasAwayFromCenter == Vector3.Distance(waypoints[num3], vector) < Vector3.Distance(waypoints[index], vector))
				{
					waypoints.RemoveAt(num3);
				}
				else
				{
					waypoints.RemoveAt(index);
				}
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000A46D0 File Offset: 0x000A28D0
		private List<Vector3> ConstructWaypointPath(List<Vector3> waypoints)
		{
			List<int> list = ConvexHull.ComputeXZ(waypoints);
			List<int> list2 = new List<int>();
			for (int l = 0; l < waypoints.Count; l++)
			{
				if (!list.Contains(l))
				{
					list2.Add(l);
				}
			}
			while (list2.Count > 0)
			{
				int item = 0;
				int index = 0;
				float num = float.MaxValue;
				foreach (int num2 in list2)
				{
					for (int j = 0; j < list.Count; j++)
					{
						int num3 = (j + 1) % list.Count;
						int index2 = list[j];
						int index3 = list[num3];
						float num4 = Vector3.Distance(SMath.LineSegmentVsPointClosest(waypoints[index2], waypoints[index3], waypoints[num2]), waypoints[num2]);
						if (num4 < num)
						{
							item = num2;
							index = num3;
							num = num4;
						}
					}
				}
				list2.Remove(item);
				list.Insert(index, item);
			}
			for (int k = 0; k < list.Count; k++)
			{
				int num5 = (k + 1) % list.Count;
			}
			return (from i in list
			select waypoints[i]).ToList<Vector3>();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000A4858 File Offset: 0x000A2A58
		public void PreprocessNavmesh()
		{
			uint tag = (this.specOps.defendingTeam == 0) ? 4U : 5U;
			Dictionary<TriangleMeshNode, Vector3> nodeNormal = new Dictionary<TriangleMeshNode, Vector3>();
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in PathfindingManager.instance.pathfinder.graphs)
			{
				if (navGraph is RecastGraph)
				{
					NavGraph navGraph2 = navGraph;
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							TriangleMeshNode triangleMeshNode3 = node as TriangleMeshNode;
							if (triangleMeshNode3 != null && node.Tag <= 1U)
							{
								Vector3 nodeNormal = PathfindingManager.GetNodeNormal(triangleMeshNode3);
								nodeNormal.Add(triangleMeshNode3, nodeNormal);
							}
						});
					}
					navGraph2.GetNodes(action);
				}
			}
			foreach (TriangleMeshNode triangleMeshNode in nodeNormal.Keys)
			{
				Vector3 vector = nodeNormal[triangleMeshNode];
				bool flag = vector.y < 0.97f;
				int num = 0;
				int num2 = 0;
				if (flag)
				{
					Connection[] connections = triangleMeshNode.connections;
					for (int i = 0; i < connections.Length; i++)
					{
						TriangleMeshNode triangleMeshNode2 = connections[i].node as TriangleMeshNode;
						if (triangleMeshNode2 != null && nodeNormal.ContainsKey(triangleMeshNode2))
						{
							float num3 = Vector3.Dot(nodeNormal[triangleMeshNode2], vector);
							num2++;
							if (num3 >= 0.98f)
							{
								num++;
							}
						}
					}
					if (num2 >= 2 && num >= num2 - 1)
					{
						flag = false;
					}
				}
				if (flag)
				{
					triangleMeshNode.Tag = tag;
				}
			}
		}

		// Token: 0x040019F7 RID: 6647
		public const float NORMAL_Y_PENALTY_THRESHOLD = 0.97f;

		// Token: 0x040019F8 RID: 6648
		public const float SMOOTHNESS_THRESHOLD = 0.98f;

		// Token: 0x040019F9 RID: 6649
		public const float WAYPOINT_NORMALIZED_PATH_DISTANCE_RANDOM = 0.2f;

		// Token: 0x040019FA RID: 6650
		public static readonly float[] WAYPOINT_NORMALIZED_PATH_DISTANCES = new float[]
		{
			0.3f,
			0.7f
		};

		// Token: 0x040019FB RID: 6651
		private const float REMOVE_WAYPOINTS_FARAWAY_THRESHOLD = 300f;

		// Token: 0x040019FC RID: 6652
		private SpecOpsMode specOps;

		// Token: 0x040019FD RID: 6653
		private Dictionary<PathfindingBox.Type, int> waypointQueriesLeft;

		// Token: 0x040019FE RID: 6654
		private Dictionary<PathfindingBox.Type, List<Vector3>> waypoints;
	}
}

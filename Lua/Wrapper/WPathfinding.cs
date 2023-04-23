using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000980 RID: 2432
	[Name("Pathfinding")]
	public static class WPathfinding
	{
		// Token: 0x06003DCE RID: 15822 RVA: 0x0012EFB4 File Offset: 0x0012D1B4
		[Doc("Finds all pathfinding nodes of a given type within a sphere[..]")]
		public static IList<WPathfindingNode> FindNodes(Vector3 center, float radius, WPathfindingNodeType type, [Doc("Include nodes that are located under water?")] bool underWater)
		{
			List<WPathfindingNode> nodes = new List<WPathfindingNode>();
			AstarPath active = AstarPath.active;
			PathfindingBox.Type type2 = type.ToPathfindingBoxType();
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in active.data.graphs)
			{
				if (navGraph != null && PathfindingManager.GetTypeFromGraphName(navGraph) == type2)
				{
					NavGraph navGraph2 = navGraph;
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							if (!underWater && node.Tag == 3U)
							{
								return;
							}
							Vector3 b = (Vector3)node.position;
							if (Vector3.Distance(center, b) < radius)
							{
								nodes.Add(node);
							}
						});
					}
					navGraph2.GetNodes(action);
				}
			}
			return nodes;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00029CD9 File Offset: 0x00027ED9
		[Doc("Finds all infantry pathfinding nodes within the given sphere.")]
		public static IList<WPathfindingNode> FindNodes(Vector3 center, float radius)
		{
			return WPathfinding.FindNodes(center, radius, WPathfindingNodeType.Infantry, false);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x0012F050 File Offset: 0x0012D250
		[Doc("Finds the pathfinding node nearest to the given position[..]")]
		[return: Doc("A pathfinding node or nil if none is found.")]
		public static WPathfindingNode FindNearestNode(Vector3 position, WPathfindingNodeType type, [Doc("Include nodes that are located under water?")] bool underWater)
		{
			GraphNode closestNode = null;
			float minDistance = float.MaxValue;
			PathfindingBox.Type type2 = type.ToPathfindingBoxType();
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in AstarPath.active.data.graphs)
			{
				if (navGraph != null && PathfindingManager.GetTypeFromGraphName(navGraph) == type2)
				{
					NavGraph navGraph2 = navGraph;
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							if (!underWater && node.Tag == 3U)
							{
								return;
							}
							Vector3 b = (Vector3)node.position;
							float num = Vector3.Distance(position, b);
							if (num < minDistance)
							{
								minDistance = num;
								closestNode = node;
							}
						});
					}
					navGraph2.GetNodes(action);
				}
			}
			return closestNode;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x0012F0F0 File Offset: 0x0012D2F0
		[Doc("Finds the pathfinding node nearest to the given position[..]")]
		[return: Doc("A pathfinding node or nil if none is found.")]
		public static WPathfindingNode FindNearestNode(Vector3 position)
		{
			float num = float.MaxValue;
			GraphNode other = null;
			NavGraph[] graphs = AstarPath.active.data.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				GraphNode node = graphs[i].GetNearest(position).node;
				Vector3 b = (Vector3)node.position;
				float num2 = Vector3.Distance(position, b);
				if (num2 < num)
				{
					num = num2;
					other = node;
				}
			}
			return other;
		}
	}
}

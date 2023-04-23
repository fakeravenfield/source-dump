using System;
using Pathfinding;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000983 RID: 2435
	[Name("PathfindingNode")]
	public class WPathfindingNode
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x00029CE4 File Offset: 0x00027EE4
		// (set) Token: 0x06003DD7 RID: 15831 RVA: 0x00029CEC File Offset: 0x00027EEC
		public Vector3 position { get; private set; }

		// Token: 0x06003DD8 RID: 15832 RVA: 0x00029CF5 File Offset: 0x00027EF5
		public WPathfindingNode(GraphNode node)
		{
			this.node = node;
			this.position = (Vector3)this.node.position;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00029D1A File Offset: 0x00027F1A
		public Vector3 RandomPointOnSurface()
		{
			return this.node.RandomPointOnSurface();
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x00029D27 File Offset: 0x00027F27
		public static implicit operator WPathfindingNode(GraphNode other)
		{
			return new WPathfindingNode(other);
		}

		// Token: 0x0400312F RID: 12591
		private readonly GraphNode node;
	}
}

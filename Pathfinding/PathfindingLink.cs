using System;
using System.Collections.Generic;
using MapEditor;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000360 RID: 864
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link2.php")]
	public class PathfindingLink : GraphModifier
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x0009EAA0 File Offset: 0x0009CCA0
		public static PathfindingLink GetNodeLink(GraphNode node)
		{
			PathfindingLink result;
			PathfindingLink.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00006955 File Offset: 0x00004B55
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00011648 File Offset: 0x0000F848
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00011650 File Offset: 0x0000F850
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0001165D File Offset: 0x0000F85D
		[ShowInMapEditor(name = "End")]
		public Vector3 MeEndPosition
		{
			get
			{
				return this.end.position;
			}
			set
			{
				this.end.position = value;
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0001166B File Offset: 0x0000F86B
		public virtual bool IsOk()
		{
			return this.EndTransform != null && this.StartTransform != null;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00011689 File Offset: 0x0000F889
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x00011691 File Offset: 0x0000F891
		public PointNode startNode { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0001169A File Offset: 0x0000F89A
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x000116A2 File Offset: 0x0000F8A2
		public PointNode endNode { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x000116AB File Offset: 0x0000F8AB
		[Obsolete("Use startNode instead (lowercase s)")]
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x000116B3 File Offset: 0x0000F8B3
		[Obsolete("Use endNode instead (lowercase e)")]
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000116BB File Offset: 0x0000F8BB
		public virtual Vector3 StartPosition()
		{
			return this.StartTransform.position;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000116C8 File Offset: 0x0000F8C8
		public virtual Vector3 EndPosition()
		{
			return this.EndTransform.position;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000116D5 File Offset: 0x0000F8D5
		public override void OnPostScan()
		{
			this.InternalOnPostScan();
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnNodeCreated(PointNode node)
		{
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0009EABC File Offset: 0x0009CCBC
		public void InternalOnPostScan()
		{
			if (!this.IsOk())
			{
				return;
			}
			if (AstarPath.active.data.pointGraph == null)
			{
				(AstarPath.active.data.AddGraph(typeof(PointGraph)) as PointGraph).name = "PointGraph (used for node links)";
			}
			if (this.startNode != null && this.startNode.Destroyed)
			{
				PathfindingLink.reference.Remove(this.startNode);
				this.startNode = null;
			}
			if (this.endNode != null && this.endNode.Destroyed)
			{
				PathfindingLink.reference.Remove(this.endNode);
				this.endNode = null;
			}
			if (this.startNode == null)
			{
				this.startNode = AstarPath.active.data.pointGraph.AddNode((Int3)this.StartPosition());
			}
			if (this.endNode == null)
			{
				this.endNode = AstarPath.active.data.pointGraph.AddNode((Int3)this.EndPosition());
			}
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.OnNodeCreated(this.startNode);
			this.OnNodeCreated(this.endNode);
			this.postScanCalled = true;
			PathfindingLink.reference[this.startNode] = this;
			PathfindingLink.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0009EC34 File Offset: 0x0009CE34
		public override void OnGraphsPostUpdate()
		{
			if (AstarPath.active.isScanning)
			{
				return;
			}
			if (this.connectedNode1 != null && this.connectedNode1.Destroyed)
			{
				this.connectedNode1 = null;
			}
			if (this.connectedNode2 != null && this.connectedNode2.Destroyed)
			{
				this.connectedNode2 = null;
			}
			if (!this.postScanCalled)
			{
				this.OnPostScan();
				return;
			}
			this.Apply(false);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000116DD File Offset: 0x0000F8DD
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0009EC9C File Offset: 0x0009CE9C
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				PathfindingLink.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				PathfindingLink.reference.Remove(this.endNode);
			}
			if (this.startNode != null && this.endNode != null)
			{
				this.startNode.RemoveConnection(this.endNode);
				this.endNode.RemoveConnection(this.startNode);
				if (this.connectedNode1 != null && this.connectedNode2 != null)
				{
					this.startNode.RemoveConnection(this.connectedNode1);
					this.connectedNode1.RemoveConnection(this.startNode);
					this.endNode.RemoveConnection(this.connectedNode2);
					this.connectedNode2.RemoveConnection(this.endNode);
				}
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000116E5 File Offset: 0x0000F8E5
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000116EE File Offset: 0x0000F8EE
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			if (Application.isPlaying)
			{
				this.Apply(true);
				if (AstarPath.active != null)
				{
					AstarPath.active.FloodFill();
				}
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0009ED70 File Offset: 0x0009CF70
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			uint graphIndex = this.startNode.GraphIndex;
			if (this.type == PathfindingBox.Type.Infantry)
			{
				none.graphMask = PathfindingManager.infantryGraphMask;
			}
			else if (this.type == PathfindingBox.Type.Car)
			{
				none.graphMask = PathfindingManager.carGraphMask;
			}
			else
			{
				none.graphMask = PathfindingManager.boatGraphMask;
			}
			this.startNode.SetPosition((Int3)this.StartPosition());
			this.endNode.SetPosition((Int3)this.EndPosition());
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartPosition() - this.EndPosition())).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			if (this.connectedNode1 == null || forceNewCheck)
			{
				NNInfo nearest = AstarPath.active.GetNearest(this.StartPosition(), none);
				this.connectedNode1 = nearest.node;
				this.clamped1 = nearest.position;
			}
			if (this.connectedNode2 == null || forceNewCheck)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.EndPosition(), none);
				this.connectedNode2 = nearest2.node;
				this.clamped2 = nearest2.position;
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartPosition())).costMagnitude * this.costFactor));
			if (!this.oneWay)
			{
				this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndPosition())).costMagnitude * this.costFactor));
			}
			if (!this.oneWay)
			{
				this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartPosition())).costMagnitude * this.costFactor));
			}
			this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndPosition())).costMagnitude * this.costFactor));
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00011715 File Offset: 0x0000F915
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0001171E File Offset: 0x0000F91E
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0009EFEC File Offset: 0x0009D1EC
		public void OnDrawGizmos(bool selected)
		{
			Color color = selected ? PathfindingLink.GizmosColorSelected : PathfindingLink.GizmosColor;
			if (this.IsOk())
			{
				Draw.Gizmos.CircleXZ(this.StartPosition(), 0.4f, color, 0f, 6.2831855f);
				Draw.Gizmos.CircleXZ(this.EndPosition(), 0.4f, color, 0f, 6.2831855f);
				Draw.Gizmos.Bezier(this.StartPosition(), this.EndPosition(), color);
				if (selected)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, this.EndPosition() - this.StartPosition()).normalized;
					Draw.Gizmos.Bezier(this.StartPosition() + normalized * 0.1f, this.EndPosition() + normalized * 0.1f, color);
					Draw.Gizmos.Bezier(this.StartPosition() - normalized * 0.1f, this.EndPosition() - normalized * 0.1f, color);
				}
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0009F104 File Offset: 0x0009D304
		internal static void SerializeReferences(GraphSerializationContext ctx)
		{
			List<PathfindingLink> modifiersOfType = GraphModifier.GetModifiersOfType<PathfindingLink>();
			ctx.writer.Write(modifiersOfType.Count);
			foreach (PathfindingLink pathfindingLink in modifiersOfType)
			{
				ctx.writer.Write(pathfindingLink.uniqueID);
				ctx.SerializeNodeReference(pathfindingLink.startNode);
				ctx.SerializeNodeReference(pathfindingLink.endNode);
				ctx.SerializeNodeReference(pathfindingLink.connectedNode1);
				ctx.SerializeNodeReference(pathfindingLink.connectedNode2);
				ctx.SerializeVector3(pathfindingLink.clamped1);
				ctx.SerializeVector3(pathfindingLink.clamped2);
				ctx.writer.Write(pathfindingLink.postScanCalled);
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0009F1CC File Offset: 0x0009D3CC
		internal static void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				ulong key = ctx.reader.ReadUInt64();
				GraphNode graphNode = ctx.DeserializeNodeReference();
				GraphNode graphNode2 = ctx.DeserializeNodeReference();
				GraphNode graphNode3 = ctx.DeserializeNodeReference();
				GraphNode graphNode4 = ctx.DeserializeNodeReference();
				Vector3 vector = ctx.DeserializeVector3();
				Vector3 vector2 = ctx.DeserializeVector3();
				bool flag = ctx.reader.ReadBoolean();
				GraphModifier graphModifier;
				if (!GraphModifier.usedIDs.TryGetValue(key, out graphModifier))
				{
					throw new Exception("Tried to deserialize a PathfindingLink reference, but the link could not be found in the scene.\nIf a PathfindingLink is included in serialized graph data, the same PathfindingLink component must be present in the scene when loading the graph data.");
				}
				PathfindingLink pathfindingLink = graphModifier as PathfindingLink;
				if (!(pathfindingLink != null))
				{
					throw new Exception("Tried to deserialize a PathfindingLink reference, but the link was not of the correct type or it has been destroyed.\nIf a PathfindingLink is included in serialized graph data, the same PathfindingLink component must be present in the scene when loading the graph data.");
				}
				if (graphNode != null)
				{
					PathfindingLink.reference[graphNode] = pathfindingLink;
				}
				if (graphNode2 != null)
				{
					PathfindingLink.reference[graphNode2] = pathfindingLink;
				}
				if (pathfindingLink.startNode != null)
				{
					PathfindingLink.reference.Remove(pathfindingLink.startNode);
				}
				if (pathfindingLink.endNode != null)
				{
					PathfindingLink.reference.Remove(pathfindingLink.endNode);
				}
				pathfindingLink.startNode = (graphNode as PointNode);
				pathfindingLink.endNode = (graphNode2 as PointNode);
				pathfindingLink.connectedNode1 = graphNode3;
				pathfindingLink.connectedNode2 = graphNode4;
				pathfindingLink.postScanCalled = flag;
				pathfindingLink.clamped1 = vector;
				pathfindingLink.clamped2 = vector2;
			}
		}

		// Token: 0x04001872 RID: 6258
		protected static Dictionary<GraphNode, PathfindingLink> reference = new Dictionary<GraphNode, PathfindingLink>();

		// Token: 0x04001873 RID: 6259
		public Transform end;

		// Token: 0x04001874 RID: 6260
		public float costFactor = 1f;

		// Token: 0x04001875 RID: 6261
		public bool oneWay;

		// Token: 0x04001876 RID: 6262
		public PathfindingBox.Type type;

		// Token: 0x04001879 RID: 6265
		private GraphNode connectedNode1;

		// Token: 0x0400187A RID: 6266
		private GraphNode connectedNode2;

		// Token: 0x0400187B RID: 6267
		private Vector3 clamped1;

		// Token: 0x0400187C RID: 6268
		private Vector3 clamped2;

		// Token: 0x0400187D RID: 6269
		private bool postScanCalled;

		// Token: 0x0400187E RID: 6270
		private static readonly Color GizmosColor = new Color(0.80784315f, 0.53333336f, 0.1882353f, 0.5f);

		// Token: 0x0400187F RID: 6271
		private static readonly Color GizmosColorSelected = new Color(0.92156863f, 0.48235294f, 0.1254902f, 1f);
	}
}

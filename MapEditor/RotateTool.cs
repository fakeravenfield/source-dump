using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200067A RID: 1658
	public class RotateTool : TransformTool
	{
		// Token: 0x06002A32 RID: 10802 RVA: 0x00016038 File Offset: 0x00014238
		public override MapEditor.Action GetAction()
		{
			return MapEditor.Action.Rotate;
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public override TransformGizmo GetGizmo()
		{
			return this.gizmo;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.gizmo = MeGizmos.CreateRotateGizmo(base.transform);
			this.initialRotations = new Dictionary<Transform, Quaternion>();
			this.initialPositions = new Dictionary<Transform, Vector3>();
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000FEFB0 File Offset: 0x000FD1B0
		protected override void StartTransform(Selection selection)
		{
			this.gizmo.Reset();
			this.initialRotations.Clear();
			this.initialPositions.Clear();
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				this.initialRotations.Add(selectableObject.transform, selectableObject.transform.rotation);
				this.initialPositions.Add(selectableObject.transform, selectableObject.transform.position);
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0001CFD7 File Offset: 0x0001B1D7
		protected override UndoableAction CreateUndoableAction(Selection selection)
		{
			return new RestoreRotationAction(selection);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000FF030 File Offset: 0x000FD230
		protected override bool ModifyTransform(Selection selection)
		{
			Quaternion accumulatedRotation = this.gizmo.accumulatedRotation;
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				Vector3 position = this.gizmo.transform.position;
				Vector3 point = this.initialPositions[selectableObject.transform] - position;
				selectableObject.transform.rotation = accumulatedRotation * this.initialRotations[selectableObject.transform];
				selectableObject.transform.position = position + accumulatedRotation * point;
			}
			return accumulatedRotation != Quaternion.identity;
		}

		// Token: 0x0400276F RID: 10095
		private RotateGizmo gizmo;

		// Token: 0x04002770 RID: 10096
		private Dictionary<Transform, Quaternion> initialRotations;

		// Token: 0x04002771 RID: 10097
		private Dictionary<Transform, Vector3> initialPositions;
	}
}

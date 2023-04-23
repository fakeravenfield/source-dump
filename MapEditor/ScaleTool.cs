using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200067B RID: 1659
	public class ScaleTool : TransformTool
	{
		// Token: 0x06002A39 RID: 10809 RVA: 0x00015FA5 File Offset: 0x000141A5
		public override MapEditor.Action GetAction()
		{
			return MapEditor.Action.Scale;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		public override TransformGizmo GetGizmo()
		{
			return this.gizmo;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0001CFEF File Offset: 0x0001B1EF
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.gizmo = MeGizmos.CreateScaleGizmo(base.transform);
			this.initialScales = new Dictionary<Transform, Vector3>();
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000FF0D8 File Offset: 0x000FD2D8
		protected override void StartTransform(Selection selection)
		{
			this.gizmo.Reset();
			this.initialScales.Clear();
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				this.initialScales.Add(selectableObject.transform, selectableObject.transform.localScale);
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0001D013 File Offset: 0x0001B213
		protected override UndoableAction CreateUndoableAction(Selection selection)
		{
			return new RestoreScaleAction(selection);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000FF134 File Offset: 0x000FD334
		protected override bool ModifyTransform(Selection selection)
		{
			Vector3 vector = base.SnapToGrid(this.gizmo.deltaScale);
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				Vector3 p = this.initialScales[selectableObject.transform];
				selectableObject.transform.localScale = base.SnapToGrid(p) + vector;
			}
			return vector != Vector3.zero;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0001D01B File Offset: 0x0001B21B
		protected override Quaternion OrientGizmo(Selection selection)
		{
			return base.RotationOfSelection(selection);
		}

		// Token: 0x04002772 RID: 10098
		private ScaleGizmo gizmo;

		// Token: 0x04002773 RID: 10099
		private Dictionary<Transform, Vector3> initialScales;
	}
}

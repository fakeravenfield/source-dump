using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000686 RID: 1670
	public class TranslateTool : TransformTool
	{
		// Token: 0x06002A80 RID: 10880 RVA: 0x0000476F File Offset: 0x0000296F
		public override MapEditor.Action GetAction()
		{
			return MapEditor.Action.Translate;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0001D34D File Offset: 0x0001B54D
		public override TransformGizmo GetGizmo()
		{
			return this.gizmo;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x0001D355 File Offset: 0x0001B555
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.gizmo = MeGizmos.CreateTranslateGizmo(base.transform);
			this.initialPositions = new Dictionary<Transform, Vector3>();
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000FF824 File Offset: 0x000FDA24
		protected override void StartTransform(Selection selection)
		{
			this.initialGizmoPosition = this.gizmo.transform.position;
			this.lastGizmoPosition = this.gizmo.transform.position;
			this.accumulatedTranslation = Vector3.zero;
			this.heightAboveGround = (from obj in selection.GetObjects()
			select MapEditorTerrain.instance.HeightAboveGroundOrWater(obj.transform.position)).ToArray<float?>();
			this.initialPositions.Clear();
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				this.initialPositions.Add(selectableObject.transform, selectableObject.transform.position);
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x0001D379 File Offset: 0x0001B579
		protected override UndoableAction CreateUndoableAction(Selection selection)
		{
			return new RestorePositionAction(selection);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x0001D381 File Offset: 0x0001B581
		protected override bool ModifyTransform(Selection selection)
		{
			if (this.gizmo.IsFollowingGround())
			{
				return this.FollowGround(selection);
			}
			return this.Drag(selection);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000FF8E0 File Offset: 0x000FDAE0
		protected bool Drag(Selection selection)
		{
			Vector3 vector = this.gizmo.transform.position - this.lastGizmoPosition;
			this.accumulatedTranslation += vector;
			Vector3 b = base.SnapToGrid(this.accumulatedTranslation);
			foreach (SelectableObject selectableObject in selection.GetObjects())
			{
				Vector3 p = this.initialPositions[selectableObject.transform];
				selectableObject.transform.position = base.SnapToGrid(p) + b;
			}
			this.gizmo.transform.position = base.SnapToGrid(this.initialGizmoPosition) + b;
			this.lastGizmoPosition = this.gizmo.transform.position;
			return vector != Vector3.zero;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000FF9B4 File Offset: 0x000FDBB4
		protected bool FollowGround(Selection selection)
		{
			float waterLevel = MapEditorTerrain.instance.GetWaterLevel();
			Vector3 vector = this.gizmo.transform.position - this.lastGizmoPosition;
			this.accumulatedTranslation = base.SnapToGrid(this.accumulatedTranslation + vector);
			Vector3 b = this.accumulatedTranslation;
			SelectableObject[] objects = selection.GetObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				SelectableObject selectableObject = objects[i];
				float? num = this.heightAboveGround[i];
				if (MeInput.instance.SnapToGroundModifier())
				{
					num = new float?(0f);
				}
				if (num != null)
				{
					Vector3 p = this.initialPositions[selectableObject.transform];
					Vector3 vector2 = base.SnapToGrid(p) + b;
					float? num2 = MapEditorTerrain.instance.TerrainElevation(vector2);
					if (num2 != null)
					{
						float num3 = Mathf.Max(num2.Value, waterLevel);
						vector2.y = num.Value + num3;
						selectableObject.transform.position = base.SnapToGrid(vector2);
					}
				}
			}
			this.gizmo.transform.position = base.SnapToGrid(this.initialGizmoPosition) + b;
			this.lastGizmoPosition = this.gizmo.transform.position;
			return vector != Vector3.zero;
		}

		// Token: 0x04002798 RID: 10136
		private TranslateGizmo gizmo;

		// Token: 0x04002799 RID: 10137
		private Dictionary<Transform, Vector3> initialPositions;

		// Token: 0x0400279A RID: 10138
		private Vector3 initialGizmoPosition;

		// Token: 0x0400279B RID: 10139
		private Vector3 lastGizmoPosition;

		// Token: 0x0400279C RID: 10140
		private Vector3 accumulatedTranslation;

		// Token: 0x0400279D RID: 10141
		private float?[] heightAboveGround;
	}
}

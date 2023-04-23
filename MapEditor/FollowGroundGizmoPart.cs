using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005FF RID: 1535
	public class FollowGroundGizmoPart : TransformGizmoPart
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x0001B2BD File Offset: 0x000194BD
		protected override void Start()
		{
			this.editorTerrain = this.editor.GetEditorTerrain();
			base.Start();
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000F7EF4 File Offset: 0x000F60F4
		protected void StartNewTransform(Selection selection)
		{
			this.heightAboveGround = (from obj in selection.GetObjects()
			select this.editorTerrain.HeightAboveGroundOrWater(obj.transform.position)).ToList<float?>();
			this.initialPositions.Clear();
			this.initialGizmoPosition = base.gizmo.transform.position;
			this.accumulatedTranslation = Vector3.zero;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0001B2D6 File Offset: 0x000194D6
		protected UndoableAction CreateUndoableAction(Selection selection)
		{
			this.undoAction = new RestorePositionAction(selection);
			return this.undoAction;
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000F7F50 File Offset: 0x000F6150
		protected void ModifyTransform(Selection selection)
		{
			Ray ray = this.input.CursorToSceneRay();
			bool flag;
			bool flag2;
			Vector3? vector = this.editorTerrain.RayCast(ray, out flag, out flag2);
			float waterLevel = this.editorTerrain.GetWaterLevel();
			if (vector != null && (flag || flag2))
			{
				Vector3 b = vector.Value - base.gizmo.transform.position;
				this.accumulatedTranslation = base.SnapToGrid(this.accumulatedTranslation + b);
				Vector3 b2 = this.accumulatedTranslation;
				SelectableObject[] objects = selection.GetObjects();
				for (int i = 0; i < objects.Length; i++)
				{
					SelectableObject selectableObject = objects[i];
					float? num = this.heightAboveGround[i];
					if (this.input.SnapToGroundModifier())
					{
						num = new float?(0f);
					}
					if (num != null)
					{
						if (!this.initialPositions.ContainsKey(selectableObject.transform))
						{
							this.initialPositions.Add(selectableObject.transform, selectableObject.transform.position);
						}
						Vector3 p = this.initialPositions[selectableObject.transform];
						Vector3 vector2 = base.SnapToGrid(p) + b2;
						float? num2 = this.editorTerrain.TerrainElevation(vector2);
						if (num2 != null)
						{
							float num3 = Mathf.Max(num2.Value, waterLevel);
							vector2.y = num.Value + num3;
							selectableObject.transform.position = base.SnapToGrid(vector2);
						}
					}
				}
				base.gizmo.transform.position = base.SnapToGrid(this.initialGizmoPosition) + b2;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000F8100 File Offset: 0x000F6300
		protected override void UpdateTransform()
		{
			Ray ray = this.input.CursorToSceneRay();
			bool flag;
			bool flag2;
			Vector3? vector = this.editorTerrain.RayCast(ray, out flag, out flag2);
			this.editorTerrain.GetWaterLevel();
			if (vector != null && (flag || flag2))
			{
				Vector3 b = vector.Value - base.gizmo.transform.position;
				base.gizmo.transform.position += b;
			}
		}

		// Token: 0x04002577 RID: 9591
		private MapEditorTerrain editorTerrain;

		// Token: 0x04002578 RID: 9592
		private UndoableAction undoAction;

		// Token: 0x04002579 RID: 9593
		private List<float?> heightAboveGround;

		// Token: 0x0400257A RID: 9594
		private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

		// Token: 0x0400257B RID: 9595
		private Vector3 initialGizmoPosition;

		// Token: 0x0400257C RID: 9596
		private Vector3 accumulatedTranslation;
	}
}

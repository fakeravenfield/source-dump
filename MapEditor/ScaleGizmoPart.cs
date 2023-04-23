using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000604 RID: 1540
	public class ScaleGizmoPart : TransformGizmoPart
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x0001B426 File Offset: 0x00019626
		private new ScaleGizmo gizmo
		{
			get
			{
				return base.gizmo as ScaleGizmo;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000F82A8 File Offset: 0x000F64A8
		private Vector2 CursorSensitivity()
		{
			Vector3 position = this.gizmo.transform.position;
			Vector3 vector = base.Forward();
			Vector3 position3;
			Vector3 position2 = (position3 = this.input.ProjectCursorToPlane(vector, position)) + vector;
			Camera camera = this.editor.GetCamera();
			Vector2 a = camera.WorldToScreenPoint(position3);
			Vector2 b = camera.WorldToScreenPoint(position2);
			return (a - b).normalized;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0001B433 File Offset: 0x00019633
		public override void Select()
		{
			this.cursorSensitivity = this.CursorSensitivity();
			base.Select();
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000F831C File Offset: 0x000F651C
		protected override void UpdateTransform()
		{
			Vector2 vector = Vector2.Scale(this.cursorSensitivity, this.input.DragObjectInSceneAxis());
			float d = this.gizmo.InputSensitivity() * this.input.ScaleObjectSensitivity();
			Vector3 b = this.forward * -(vector.x + vector.y) * d;
			this.gizmo.deltaScale += b;
		}

		// Token: 0x04002586 RID: 9606
		private Vector2 cursorSensitivity;
	}
}

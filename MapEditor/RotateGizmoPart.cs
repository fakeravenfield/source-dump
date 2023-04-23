using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000602 RID: 1538
	public class RotateGizmoPart : TransformGizmoPart
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0001B3CE File Offset: 0x000195CE
		private new RotateGizmo gizmo
		{
			get
			{
				return base.gizmo as RotateGizmo;
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000F8180 File Offset: 0x000F6380
		private Vector2 CursorSensitivity(Vector3 rotationAxis)
		{
			Vector3 position = this.gizmo.transform.position;
			Vector3 vector = this.input.ProjectCursorToPlane(rotationAxis, position);
			Vector3 b = Vector3.Cross((vector - position).normalized, rotationAxis);
			Vector3 position2 = vector;
			Vector3 position3 = vector + b;
			Camera camera = this.editor.GetCamera();
			Vector2 a = camera.WorldToScreenPoint(position2);
			Vector2 b2 = camera.WorldToScreenPoint(position3);
			return (a - b2).normalized;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0001B3DB File Offset: 0x000195DB
		public override void Select()
		{
			this.rotationAxis = this.gizmo.transform.TransformDirection(this.forward);
			this.cursorSensitivity = this.CursorSensitivity(this.rotationAxis);
			base.Select();
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000F8208 File Offset: 0x000F6408
		protected override void UpdateTransform()
		{
			Vector2 vector = Vector2.Scale(this.cursorSensitivity, this.input.DragObjectInSceneAxis());
			float f = (vector.x + vector.y) * this.input.RotateObjectSensitivity();
			Vector3 axis = this.rotationAxis * Mathf.Sign(f);
			Quaternion lhs = Quaternion.AngleAxis(Mathf.Abs(f), axis);
			this.gizmo.accumulatedRotation = lhs * this.gizmo.accumulatedRotation;
			this.gizmo.transform.rotation = lhs * this.gizmo.transform.rotation;
		}

		// Token: 0x04002583 RID: 9603
		private Vector3 rotationAxis;

		// Token: 0x04002584 RID: 9604
		private Vector2 cursorSensitivity;
	}
}

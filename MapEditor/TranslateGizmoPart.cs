using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200060B RID: 1547
	public class TranslateGizmoPart : TransformGizmoPart
	{
		// Token: 0x060027A1 RID: 10145 RVA: 0x000F8B2C File Offset: 0x000F6D2C
		private Vector2 CursorSensitivity()
		{
			Vector3 position = base.gizmo.transform.position;
			Vector3 vector = base.Forward();
			Vector3 position3;
			Vector3 position2 = (position3 = this.input.ProjectCursorToPlane(vector, position)) + vector;
			Camera camera = this.editor.GetCamera();
			Vector2 a = camera.WorldToScreenPoint(position3);
			Vector2 b = camera.WorldToScreenPoint(position2);
			return (a - b).normalized;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0001B5C8 File Offset: 0x000197C8
		public override void Select()
		{
			this.cursorSensitivity = this.CursorSensitivity();
			base.Select();
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000F8BA0 File Offset: 0x000F6DA0
		protected override void UpdateTransform()
		{
			Vector2 vector = Vector2.Scale(this.cursorSensitivity, this.input.DragObjectInSceneAxis());
			float d = base.gizmo.InputSensitivity() * this.input.TranslateObjectSensitivity();
			Vector3 b = base.Forward() * -(vector.x + vector.y) * d;
			base.gizmo.transform.position += b;
		}

		// Token: 0x040025A0 RID: 9632
		private Vector2 cursorSensitivity;
	}
}

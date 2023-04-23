using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003EA RID: 1002
	public class CommandRoomSwivelCamera : CommandRoomCamera
	{
		// Token: 0x06001917 RID: 6423 RVA: 0x000A85DC File Offset: 0x000A67DC
		protected override void UpdateTransform()
		{
			if (CommandRoomMainCamera.instance.isLocked)
			{
				return;
			}
			Vector2 vector = Input.mousePosition;
			Vector2 vector2 = new Vector2(Mathf.Clamp((vector.x / (float)Screen.width - 0.5f) * 2f, -1f, 1f), Mathf.Clamp((vector.y / (float)Screen.height - 0.5f) * 2f, -1f, 1f));
			base.transform.localRotation = this.originLocalRotation * Quaternion.Euler(-this.turnVertical * vector2.y, this.turnHorizontal * vector2.x, 0f);
		}

		// Token: 0x04001AEC RID: 6892
		public float turnHorizontal = 40f;

		// Token: 0x04001AED RID: 6893
		public float turnVertical = 20f;
	}
}

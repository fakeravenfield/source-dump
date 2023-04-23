using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003E9 RID: 1001
	public class CommandRoomPanCamera : CommandRoomCamera
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x000136F0 File Offset: 0x000118F0
		private void Start()
		{
			if (this.panTransform == null)
			{
				this.panTransform = base.transform;
			}
			this.originLocalPosition = this.panTransform.localPosition;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000A8390 File Offset: 0x000A6590
		protected override void UpdateTransform()
		{
			Vector2 vector = Input.mousePosition;
			Vector2 vector2 = Vector2.zero;
			if (vector.x < 10f)
			{
				vector2.x = -1f;
			}
			else if (vector.x > (float)Screen.width - 10f)
			{
				vector2.x = 1f;
			}
			if (vector.y < 10f)
			{
				vector2.y = -1f;
			}
			else if (vector.y > (float)Screen.height - 10f)
			{
				vector2.y = 1f;
			}
			Vector2 b = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			vector2 += b;
			vector2.x = Mathf.Clamp(vector2.x, -1f, 1f);
			vector2.y = Mathf.Clamp(vector2.y, -1f, 1f);
			this.panOffset += vector2 * this.panSpeed * Time.deltaTime;
			this.panOffset.x = Mathf.Clamp(this.panOffset.x, -this.maxPanHorizontal, this.maxPanHorizontal);
			this.panOffset.y = Mathf.Clamp(this.panOffset.y, -this.maxPanVertical, this.maxPanVertical);
			this.panTransform.localPosition = this.originLocalPosition + new Vector3(this.panOffset.x, this.panOffset.y, 0f);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000A852C File Offset: 0x000A672C
		public override void OnActivated()
		{
			base.OnActivated();
			Ray ray = new Ray(CommandRoomMainCamera.instance.hoverRayPoint, -base.transform.forward);
			UnityEngine.Plane plane = new UnityEngine.Plane(this.panTransform.forward, this.panTransform.position);
			float distance = 0f;
			if (plane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = this.panTransform.worldToLocalMatrix.MultiplyPoint(point);
				this.panOffset.x = vector.x;
				this.panOffset.y = vector.y;
				this.UpdateTransform();
			}
		}

		// Token: 0x04001AE6 RID: 6886
		private const float EDGE_PAN_SIZE = 10f;

		// Token: 0x04001AE7 RID: 6887
		public float maxPanHorizontal = 2f;

		// Token: 0x04001AE8 RID: 6888
		public float maxPanVertical = 2f;

		// Token: 0x04001AE9 RID: 6889
		public float panSpeed = 1f;

		// Token: 0x04001AEA RID: 6890
		public Transform panTransform;

		// Token: 0x04001AEB RID: 6891
		private Vector2 panOffset = Vector3.zero;
	}
}

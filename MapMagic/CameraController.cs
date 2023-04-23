using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004AD RID: 1197
	public class CameraController : MonoBehaviour
	{
		// Token: 0x06001E03 RID: 7683 RVA: 0x00016352 File Offset: 0x00014552
		public void Start()
		{
			if (this.cam == null)
			{
				this.cam = Camera.main;
			}
			this.pivot = this.cam.transform.position;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000C4E54 File Offset: 0x000C3054
		public void LateUpdate()
		{
			if (this.lockCursor)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			if (Input.GetMouseButton(this.rotateMouseButton) || this.lockCursor)
			{
				this.rotationY += Input.GetAxis("Mouse X") * this.sensitivity;
				this.rotationX -= Input.GetAxis("Mouse Y") * this.sensitivity;
				this.rotationX = Mathf.Min(this.rotationX, 89.9f);
			}
			if (this.hero != null)
			{
				this.pivot = this.hero.position + new Vector3(0f, this.elevation, 0f);
			}
			if (this.movable)
			{
				if (Input.GetKey(KeyCode.W))
				{
					this.pivot += base.transform.forward * this.velocity * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.S))
				{
					this.pivot -= base.transform.forward * this.velocity * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.D))
				{
					this.pivot += base.transform.right * this.velocity * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.A))
				{
					this.pivot -= base.transform.right * this.velocity * Time.deltaTime;
				}
			}
			if (this.follow > 1E-06f)
			{
				float target = (this.cam.transform.position - this.oldPos).Angle();
				float num = Mathf.DeltaAngle(this.rotationY, target);
				if (Mathf.Abs(num) > this.follow * Time.deltaTime)
				{
					this.rotationY += (float)((num > 0f) ? 1 : -1) * this.follow * Time.deltaTime;
				}
				else
				{
					this.rotationY = target;
				}
			}
			this.oldPos = this.cam.transform.position;
			this.cam.transform.localEulerAngles = new Vector3(this.rotationX, this.rotationY, 0f);
			this.cam.transform.position = this.pivot;
		}

		// Token: 0x04001E77 RID: 7799
		public Camera cam;

		// Token: 0x04001E78 RID: 7800
		public Transform hero;

		// Token: 0x04001E79 RID: 7801
		public bool movable;

		// Token: 0x04001E7A RID: 7802
		public float velocity = 4f;

		// Token: 0x04001E7B RID: 7803
		public float follow = 0.1f;

		// Token: 0x04001E7C RID: 7804
		private Vector3 pivot = new Vector3(0f, 0f, 0f);

		// Token: 0x04001E7D RID: 7805
		public int rotateMouseButton;

		// Token: 0x04001E7E RID: 7806
		public bool lockCursor;

		// Token: 0x04001E7F RID: 7807
		public float elevation = 1.5f;

		// Token: 0x04001E80 RID: 7808
		public float sensitivity = 1f;

		// Token: 0x04001E81 RID: 7809
		public float rotationX;

		// Token: 0x04001E82 RID: 7810
		public float rotationY = 190f;

		// Token: 0x04001E83 RID: 7811
		private Vector3 oldPos;
	}
}

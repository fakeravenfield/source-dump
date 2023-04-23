using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D1 RID: 1489
	public class FlyingCamera : CameraBase
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x000F4C2C File Offset: 0x000F2E2C
		protected override void Start()
		{
			base.Start();
			this.angles = this.LimitCameraAngles(base.transform.rotation.eulerAngles);
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		protected override void Update()
		{
			base.Update();
			this.RotateCamera();
			this.MoveCamera();
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000F4C60 File Offset: 0x000F2E60
		private void RotateCamera()
		{
			if (this.input.RotateCamera())
			{
				Vector2 vector = this.input.RotateCameraAxis();
				this.accumulatedInput += new Vector3(-vector.y, vector.x, 0f);
				Vector3 b = this.accumulatedInput * this.input.RotateCameraSensitivity();
				this.angles = this.LimitCameraAngles(this.initialAngles + b);
			}
			else
			{
				this.accumulatedInput = Vector3.zero;
				this.initialAngles = this.angles;
			}
			base.transform.rotation = Quaternion.Euler(this.angles);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		private void MoveCamera()
		{
			if (this.input.MoveCamera())
			{
				float d = this.input.MoveCameraSpeed();
				Vector3 vector = this.input.MoveCameraDirection();
				vector = base.transform.TransformDirection(vector);
				base.transform.position += vector * d * Time.deltaTime;
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000F4D74 File Offset: 0x000F2F74
		private Vector3 RollOverEulerAngles(Vector3 a)
		{
			float num = a.x - (float)Mathf.FloorToInt(a.x / 360f) * 360f;
			float num2 = a.y - (float)Mathf.FloorToInt(a.y / 360f) * 360f;
			float num3 = a.z - (float)Mathf.FloorToInt(a.z / 360f) * 360f;
			num = ((num >= 180f) ? (num - 360f) : num);
			num2 = ((num2 >= 180f) ? (num2 - 360f) : num2);
			num3 = ((num3 >= 180f) ? (num3 - 360f) : num3);
			return new Vector3(num, num2, num3);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000F4E24 File Offset: 0x000F3024
		private Vector3 LimitCameraAngles(Vector3 a)
		{
			a = this.RollOverEulerAngles(a);
			float x = Mathf.Clamp(a.x, -90f, 90f);
			float y = a.y;
			float z = 0f;
			return new Vector3(x, y, z);
		}

		// Token: 0x040024E5 RID: 9445
		private Vector3 angles;

		// Token: 0x040024E6 RID: 9446
		private Vector3 initialAngles;

		// Token: 0x040024E7 RID: 9447
		private Vector3 accumulatedInput;
	}
}

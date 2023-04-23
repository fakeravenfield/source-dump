using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D4 RID: 1492
	[ExecuteInEditMode]
	public class OrthographicCamera : CameraBase
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x0001AB1F File Offset: 0x00018D1F
		protected override void Start()
		{
			base.Start();
			this.zoom = this.ConvertSizeToZoom(this.camera.orthographicSize);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0001AB3E File Offset: 0x00018D3E
		protected override void Update()
		{
			base.Update();
			this.ZoomCamera();
			this.MoveCamera();
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000F5100 File Offset: 0x000F3300
		private void ZoomCamera()
		{
			base.GetComponent<Camera>();
			if (this.input.RotateCamera())
			{
				Vector2 vector = this.input.RotateCameraAxis();
				this.accumulatedInput += vector.x;
				float num = this.accumulatedInput * this.input.ZoomCameraSensitivity();
				this.zoom = Mathf.Clamp01(this.initialZoom + num);
			}
			else
			{
				this.accumulatedInput = 0f;
				this.initialZoom = this.zoom;
			}
			this.camera.orthographicSize = this.ConvertZoomToSize(this.zoom);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000F5198 File Offset: 0x000F3398
		private void MoveCamera()
		{
			if (this.input.MoveCamera())
			{
				float d = this.input.MoveCameraSpeed();
				Vector3 vector = this.input.MoveCameraDirection();
				vector = new Vector3(vector.x, 0f, vector.z);
				base.transform.position += vector * d * Time.deltaTime;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0001AB52 File Offset: 0x00018D52
		private float ConvertZoomToSize(float z)
		{
			z = Mathf.Clamp01(z);
			return Mathf.Clamp(this.zoomToSizeCurve.Evaluate(z) * 100f, 0.1f, 100f);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000F520C File Offset: 0x000F340C
		private float ConvertSizeToZoom(float s)
		{
			int num = 0;
			while ((float)num < 100f)
			{
				float num2 = (float)num / 99f;
				float num3 = (float)(num + 1) / 99f;
				float num4 = this.ConvertZoomToSize(num2);
				float num5 = this.ConvertZoomToSize(num3);
				if (num4 < s && s <= num5)
				{
					return (num2 + num3) / 2f;
				}
				num++;
			}
			return 0.5f;
		}

		// Token: 0x040024E8 RID: 9448
		public AnimationCurve zoomToSizeCurve;

		// Token: 0x040024E9 RID: 9449
		private const float MIN_SIZE = 0.1f;

		// Token: 0x040024EA RID: 9450
		private const float MAX_SIZE = 100f;

		// Token: 0x040024EB RID: 9451
		private float zoom;

		// Token: 0x040024EC RID: 9452
		private float initialZoom;

		// Token: 0x040024ED RID: 9453
		private float accumulatedInput;
	}
}

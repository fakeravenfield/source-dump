using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D7 RID: 1495
	public class ScaleWithDistance : MonoBehaviour
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x0001AC16 File Offset: 0x00018E16
		private void Awake()
		{
			this.initialScale = base.transform.localScale;
			this.LateUpdate();
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0001AC2F File Offset: 0x00018E2F
		private void OnEnable()
		{
			this.LateUpdate();
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000F53EC File Offset: 0x000F35EC
		private void LateUpdate()
		{
			Camera camera = MapEditor.instance.GetCamera();
			Vector3 vector = this.ScaleRelativeToCameraDistance(camera, this.initialScale);
			vector = this.ScaleRelativeToVerticalViewport(camera, vector);
			if (camera.orthographic)
			{
				vector = this.ScaleRelativeToOrthographicSize(camera, vector);
			}
			if (this.maxSize > 0f)
			{
				vector = Vector3.Min(vector, Vector3.one * this.maxSize);
			}
			base.transform.localScale = vector;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000F545C File Offset: 0x000F365C
		private Vector3 ScaleRelativeToCameraDistance(Camera camera, Vector3 localScale)
		{
			UnityEngine.Plane plane = new UnityEngine.Plane(camera.transform.forward, camera.transform.position);
			float distanceToPoint = plane.GetDistanceToPoint(base.transform.position);
			return localScale * distanceToPoint * this.distanceScaleFactor;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0001AC37 File Offset: 0x00018E37
		private Vector3 ScaleRelativeToOrthographicSize(Camera camera, Vector3 localScale)
		{
			return localScale * camera.orthographicSize * this.orthographicFactor;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0001AC50 File Offset: 0x00018E50
		private Vector3 ScaleRelativeToVerticalViewport(Camera camera, Vector3 localScale)
		{
			return localScale * 560f / (float)camera.pixelHeight;
		}

		// Token: 0x040024F9 RID: 9465
		private const float VIEWPORT_VERTICAL_SIZE = 560f;

		// Token: 0x040024FA RID: 9466
		[Range(0f, 1f)]
		public float distanceScaleFactor = 0.003f;

		// Token: 0x040024FB RID: 9467
		[Range(0f, 1f)]
		public float orthographicFactor = 0.03f;

		// Token: 0x040024FC RID: 9468
		public float maxSize;

		// Token: 0x040024FD RID: 9469
		private Vector3 initialScale;
	}
}

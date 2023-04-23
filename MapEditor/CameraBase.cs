using System;
using cakeslice;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005CF RID: 1487
	public abstract class CameraBase : MonoBehaviour
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x000F49EC File Offset: 0x000F2BEC
		protected virtual void Start()
		{
			this.input = MeInput.instance;
			this.camera = base.GetComponent<Camera>();
			this.gizmoCamera = this.AddGizmoCamera(this.camera);
			this.previewCamera = this.AddPreviewCamera(this.camera);
			this.AddOutlineEffect(this.camera);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000F4A44 File Offset: 0x000F2C44
		protected virtual void Update()
		{
			this.gizmoCamera.orthographicSize = this.camera.orthographicSize;
			this.previewCamera.orthographicSize = this.camera.orthographicSize;
			this.previewCamera.projectionMatrix = this.camera.projectionMatrix;
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000F4A94 File Offset: 0x000F2C94
		private OutlineEffect AddOutlineEffect(Camera sourceCamera)
		{
			OutlineEffect outlineEffect = base.gameObject.AddComponent<OutlineEffect>();
			outlineEffect.SetSourceCamera(sourceCamera);
			outlineEffect.lineColor0 = new Color(1f, 0.73f, 0f);
			outlineEffect.lineThickness = 1.92f;
			outlineEffect.lineIntensity = 1.6f;
			outlineEffect.fillAmount = 0.19f;
			return outlineEffect;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000F4AF0 File Offset: 0x000F2CF0
		private Camera AddGizmoCamera(Camera sourceCamera)
		{
			sourceCamera.cullingMask &= ~Layers.GetGizmoPartLayerMask();
			Camera camera = new GameObject("Gizmo Camera")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<Camera>();
			camera.CopyFrom(sourceCamera);
			camera.cullingMask = Layers.GetGizmoPartLayerMask();
			camera.depth = sourceCamera.depth + 1f;
			camera.clearFlags = CameraClearFlags.Depth;
			return camera;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000F4B5C File Offset: 0x000F2D5C
		private Camera AddPreviewCamera(Camera sourceCamera)
		{
			sourceCamera.cullingMask &= ~Layers.GetPreviewLayerMask();
			Camera camera = new GameObject("Preview Camera")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<Camera>();
			camera.CopyFrom(sourceCamera);
			camera.cullingMask = Layers.GetPreviewLayerMask();
			camera.depth = sourceCamera.depth + 1f;
			camera.clearFlags = CameraClearFlags.Color;
			base.gameObject.AddComponent<PreviewEffect>().previewCamera = camera;
			return camera;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		public Camera GetCamera()
		{
			return this.camera;
		}

		// Token: 0x040024E1 RID: 9441
		protected MeInput input;

		// Token: 0x040024E2 RID: 9442
		protected Camera camera;

		// Token: 0x040024E3 RID: 9443
		private Camera gizmoCamera;

		// Token: 0x040024E4 RID: 9444
		private Camera previewCamera;
	}
}

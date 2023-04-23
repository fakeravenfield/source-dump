using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D5 RID: 1493
	[RequireComponent(typeof(Camera))]
	public class PreviewEffect : MonoBehaviour
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x000F5264 File Offset: 0x000F3464
		private void Start()
		{
			this.previewCamera.enabled = false;
			this.previewShader = new Material(Shader.Find("MapEditor/PreviewEffect"));
			this.previewShader.renderQueue = 3000;
			this.previewShader.SetFloat("_Alpha", 0.6f);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0001AB7D File Offset: 0x00018D7D
		private void OnPreRender()
		{
			this.CreateRenderTextureIfNecessary();
			this.previewCamera.depthTextureMode = DepthTextureMode.Depth;
			this.previewCamera.SetTargetBuffers(this.colorTexture.colorBuffer, this.depthTexture.depthBuffer);
			this.previewCamera.Render();
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0001ABBD File Offset: 0x00018DBD
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			this.previewShader.SetTexture("_PreviewTex", this.colorTexture);
			this.previewShader.SetTexture("_DepthTex", this.depthTexture);
			Graphics.Blit(source, destination, this.previewShader);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000F52B8 File Offset: 0x000F34B8
		private void CreateRenderTextureIfNecessary()
		{
			int pixelWidth = this.previewCamera.pixelWidth;
			int pixelHeight = this.previewCamera.pixelHeight;
			if (this.colorTexture == null || this.colorTexture.width != pixelWidth || this.colorTexture.height != pixelHeight)
			{
				this.colorTexture = new RenderTexture(pixelWidth, pixelHeight, 0, RenderTextureFormat.Default);
				this.depthTexture = new RenderTexture(pixelWidth, pixelHeight, 24, RenderTextureFormat.Depth);
			}
		}

		// Token: 0x040024EE RID: 9454
		public Camera previewCamera;

		// Token: 0x040024EF RID: 9455
		private const string SHADER_NAME = "MapEditor/PreviewEffect";

		// Token: 0x040024F0 RID: 9456
		private const float PREVIEW_ALPHA = 0.6f;

		// Token: 0x040024F1 RID: 9457
		private Material previewShader;

		// Token: 0x040024F2 RID: 9458
		public RenderTexture colorTexture;

		// Token: 0x040024F3 RID: 9459
		public RenderTexture depthTexture;
	}
}

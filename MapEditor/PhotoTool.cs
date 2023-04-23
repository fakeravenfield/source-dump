using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000676 RID: 1654
	public class PhotoTool : AbstractTool
	{
		// Token: 0x06002A1C RID: 10780 RVA: 0x000FE8E8 File Offset: 0x000FCAE8
		protected override void OnInitialize()
		{
			this.camera = base.GetComponent<Camera>();
			this.iconRenderTexture = new RenderTexture(485, 300, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			this.iconTexture = new Texture2D(485, 300, TextureFormat.ARGB32, false);
			this.viewportOverlay = this.edgeCanvas.GetComponentInChildren<RawImage>();
			this.shutterAudio = this.edgeCanvas.GetComponentInChildren<AudioSource>();
			this.hintText = this.edgeCanvas.GetComponentInChildren<Text>();
			base.OnInitialize();
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000FE96C File Offset: 0x000FCB6C
		protected override void OnActivate()
		{
			this.armed = MapEditor.HasDescriptorFilePath();
			this.hintText.text = (this.armed ? "PRESS SPACE TO TAKE PHOTO" : "PLEASE SAVE YOUR MAP BEFORE TAKING PHOTO");
			this.edgeCanvas.SetActive(true);
			Color targetColor = this.armed ? new Color(1f, 1f, 1f, 0f) : new Color(0.5f, 0f, 0f, 0.4f);
			this.viewportOverlay.CrossFadeColor(targetColor, 0f, true, true);
			base.OnActivate();
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0001CEF2 File Offset: 0x0001B0F2
		protected override void OnDeactivate()
		{
			this.armed = false;
			this.edgeCanvas.SetActive(false);
			if (base.isInitialized)
			{
				this.camera.fieldOfView = 60f;
			}
			base.OnDeactivate();
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000FEA08 File Offset: 0x000FCC08
		protected override void Update()
		{
			base.Update();
			if (this.armed)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.CaptureIcon();
				}
				this.camera.fieldOfView = Mathf.Clamp(this.camera.fieldOfView - Input.mouseScrollDelta.y, 2f, 90f);
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000FEA64 File Offset: 0x000FCC64
		private void CaptureIcon()
		{
			float fieldOfView = this.camera.fieldOfView;
			this.camera.fieldOfView = fieldOfView * 0.52f;
			this.camera.targetTexture = this.iconRenderTexture;
			this.camera.Render();
			this.camera.targetTexture = null;
			this.camera.fieldOfView = fieldOfView;
			if (MapEditor.HasDescriptorFilePath())
			{
				try
				{
					string path = PhotoTool.DescriptorIconFilePath();
					RenderTexture.active = this.iconRenderTexture;
					this.iconTexture.ReadPixels(new Rect(0f, 0f, 485f, 300f), 0, 0);
					this.iconTexture.Apply();
					byte[] bytes = this.iconTexture.EncodeToPNG();
					File.WriteAllBytes(path, bytes);
				}
				catch (Exception ex)
				{
					MessageUI.ShowMessage(string.Format("An error occurred while trying to save the map icon.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
					Debug.LogException(ex);
					return;
				}
			}
			if (this.armed)
			{
				this.shutterAudio.Play();
				this.viewportOverlay.CrossFadeAlpha(1f, 0f, true);
				this.viewportOverlay.CrossFadeAlpha(0f, 0.2f, true);
				SystemMessagesUI.ShowMessage("Map icon was saved");
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0001CF25 File Offset: 0x0001B125
		public static string DescriptorIconFilePath()
		{
			return MapEditor.descriptorFilePath + ".png";
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0001CF36 File Offset: 0x0001B136
		public static bool DescriptorHasIconFile()
		{
			return MapEditor.HasDescriptorFilePath() && File.Exists(PhotoTool.DescriptorIconFilePath());
		}

		// Token: 0x0400275B RID: 10075
		private const int ICON_WIDTH = 485;

		// Token: 0x0400275C RID: 10076
		private const int ICON_HEIGHT = 300;

		// Token: 0x0400275D RID: 10077
		private const float ICON_ASPECT_RATIO = 1.61667f;

		// Token: 0x0400275E RID: 10078
		private const float DEFAULT_FOV = 60f;

		// Token: 0x0400275F RID: 10079
		public GameObject edgeCanvas;

		// Token: 0x04002760 RID: 10080
		private RawImage viewportOverlay;

		// Token: 0x04002761 RID: 10081
		private AudioSource shutterAudio;

		// Token: 0x04002762 RID: 10082
		private Camera camera;

		// Token: 0x04002763 RID: 10083
		private bool armed;

		// Token: 0x04002764 RID: 10084
		private RenderTexture iconRenderTexture;

		// Token: 0x04002765 RID: 10085
		private Texture2D iconTexture;

		// Token: 0x04002766 RID: 10086
		private Text hintText;
	}
}

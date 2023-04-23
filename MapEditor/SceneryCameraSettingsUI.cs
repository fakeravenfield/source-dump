using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200070C RID: 1804
	public class SceneryCameraSettingsUI : WindowBase
	{
		// Token: 0x06002D2B RID: 11563 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.editor = MapEditor.instance;
			this.sceneryCamera = this.editor.GetSceneryCamera();
			this.buttonSelect.onClick.AddListener(new UnityAction(this.SelectCamera));
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		protected override void OnShow()
		{
			base.OnShow();
			this.previewImage.texture = this.sceneryCamera.renderTexture;
			this.sceneryCamera.Render();
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0001F11D File Offset: 0x0001D31D
		private void SelectCamera()
		{
			this.editor.SetSelection(new Selection(new SelectableObject[]
			{
				this.sceneryCamera.GetSelectableObject()
			}));
			MeTools.instance.SwitchToTranslateTool();
		}

		// Token: 0x0400299B RID: 10651
		public RawImage previewImage;

		// Token: 0x0400299C RID: 10652
		public Button buttonSelect;

		// Token: 0x0400299D RID: 10653
		private MapEditor editor;

		// Token: 0x0400299E RID: 10654
		private MeoSceneryCamera sceneryCamera;
	}
}

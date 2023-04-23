using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006EB RID: 1771
	public class MinimapSettingsUI : WindowBase
	{
		// Token: 0x06002C7F RID: 11391 RVA: 0x00103740 File Offset: 0x00101940
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.editor = MapEditor.instance;
			this.sliderFov.SetRange(0f, 90f);
			this.sliderFov.onValueChanged.AddListener(new UnityAction<float>(this.FovChanged));
			this.buttonCenter.onClick.AddListener(new UnityAction(this.Center));
			this.buttonCenterTerrain.onClick.AddListener(new UnityAction(this.CenterTerrain));
			this.buttonCenterObjects.onClick.AddListener(new UnityAction(this.CenterObjects));
			this.minimap = MinimapCamera.instance;
			this.minimapImage.texture = this.minimap.GetTexture();
			this.minimapImage.color = Color.white;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0001E97F File Offset: 0x0001CB7F
		protected override void OnShow()
		{
			base.OnShow();
			this.sliderFov.SetValue(this.minimap.camera.fieldOfView);
			this.Refresh();
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		protected override void OnHide()
		{
			base.OnHide();
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		private void Refresh()
		{
			this.refresh = true;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x00103814 File Offset: 0x00101A14
		private void Update()
		{
			if (this.buttonN.IsPressed())
			{
				this.minimap.transform.position += new Vector3(0f, 0f, 1f) * 30f * Time.deltaTime;
				this.Refresh();
			}
			if (this.buttonS.IsPressed())
			{
				this.minimap.transform.position -= new Vector3(0f, 0f, 1f) * 30f * Time.deltaTime;
				this.Refresh();
			}
			if (this.buttonE.IsPressed())
			{
				this.minimap.transform.position += new Vector3(1f, 0f, 0f) * 30f * Time.deltaTime;
				this.Refresh();
			}
			if (this.buttonW.IsPressed())
			{
				this.minimap.transform.position -= new Vector3(1f, 0f, 0f) * 30f * Time.deltaTime;
				this.Refresh();
			}
			if (this.refresh)
			{
				this.refresh = false;
				this.minimap.Render();
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0001E9B9 File Offset: 0x0001CBB9
		private void FovChanged(float fov)
		{
			this.minimap.camera.fieldOfView = fov;
			this.Refresh();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00103994 File Offset: 0x00101B94
		private void Center()
		{
			MeoCapturePoint[] capturePoints = this.editor.FindObjectsToSave<MeoCapturePoint>();
			this.minimap.AdjustZoomAndPosition(this.editor.GetEditorTerrain(), capturePoints);
			this.sliderFov.SetValue(this.minimap.camera.fieldOfView);
			this.Refresh();
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		private void CenterTerrain()
		{
			this.minimap.AdjustZoomAndPosition(this.editor.GetEditorTerrain(), null);
			this.sliderFov.SetValue(this.minimap.camera.fieldOfView);
			this.Refresh();
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x001039E8 File Offset: 0x00101BE8
		private void CenterObjects()
		{
			MeoCapturePoint[] capturePoints = this.editor.FindObjectsToSave<MeoCapturePoint>();
			this.minimap.AdjustZoomAndPosition(null, capturePoints);
			this.sliderFov.SetValue(this.minimap.camera.fieldOfView);
			this.Refresh();
		}

		// Token: 0x040028FF RID: 10495
		public RawImage minimapImage;

		// Token: 0x04002900 RID: 10496
		public ExtendedButton buttonN;

		// Token: 0x04002901 RID: 10497
		public ExtendedButton buttonE;

		// Token: 0x04002902 RID: 10498
		public ExtendedButton buttonS;

		// Token: 0x04002903 RID: 10499
		public ExtendedButton buttonW;

		// Token: 0x04002904 RID: 10500
		public SliderWithInput sliderFov;

		// Token: 0x04002905 RID: 10501
		public Button buttonCenter;

		// Token: 0x04002906 RID: 10502
		public Button buttonCenterTerrain;

		// Token: 0x04002907 RID: 10503
		public Button buttonCenterObjects;

		// Token: 0x04002908 RID: 10504
		private MapEditor editor;

		// Token: 0x04002909 RID: 10505
		private MinimapCamera minimap;

		// Token: 0x0400290A RID: 10506
		private bool refresh;
	}
}

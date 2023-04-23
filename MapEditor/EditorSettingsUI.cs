using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006C3 RID: 1731
	public class EditorSettingsUI : WindowBase
	{
		// Token: 0x06002BAE RID: 11182 RVA: 0x00102098 File Offset: 0x00100298
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.editor = MapEditor.instance;
			this.sliderGridSize.SetRange(0.01f, 10f);
			this.sliderGridSize.valueFormat = "0.0";
			this.sliderAutosaveInterval.SetRange(0f, 30f);
			this.sliderAutosaveInterval.valueFormat = "0";
			this.buttonOk.onClick.AddListener(new UnityAction(this.ButtonOkClicked));
			this.buttonCancel.onClick.AddListener(new UnityAction(this.ButtonCancelClicked));
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0001E02A File Offset: 0x0001C22A
		protected override void OnShow()
		{
			base.OnShow();
			this.sliderGridSize.SetValue(this.editor.GetGridSize());
			this.sliderAutosaveInterval.SetValue(this.editor.autosaveInterval / 60f);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0001E064 File Offset: 0x0001C264
		private void ButtonOkClicked()
		{
			this.editor.SetGridSize(this.sliderGridSize.GetValue());
			this.editor.autosaveInterval = this.sliderAutosaveInterval.GetValue() * 60f;
			base.Hide();
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0001E09E File Offset: 0x0001C29E
		private void ButtonCancelClicked()
		{
			base.Hide();
		}

		// Token: 0x04002854 RID: 10324
		public SliderWithInput sliderGridSize;

		// Token: 0x04002855 RID: 10325
		public SliderWithInput sliderAutosaveInterval;

		// Token: 0x04002856 RID: 10326
		public Button buttonOk;

		// Token: 0x04002857 RID: 10327
		public Button buttonCancel;

		// Token: 0x04002858 RID: 10328
		private MapEditor editor;
	}
}

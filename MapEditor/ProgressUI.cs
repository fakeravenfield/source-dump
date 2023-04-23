using System;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006F3 RID: 1779
	public class ProgressUI : WindowBase
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x0001EC3A File Offset: 0x0001CE3A
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.editor = MapEditor.instance;
			this.frame.closeButton.gameObject.SetActive(false);
			this.SetStatus("");
			this.SetProgress(0f);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0001EC79 File Offset: 0x0001CE79
		protected override void OnHide()
		{
			base.OnHide();
			this.SetStatus("");
			this.SetProgress(0f);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0001EC97 File Offset: 0x0001CE97
		public void SetStatus(string text)
		{
			this.statusLabel.text = text;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0001ECA5 File Offset: 0x0001CEA5
		public void SetProgress(float progress)
		{
			progress = Mathf.Clamp01(progress);
			this.progressElement.flexibleWidth = progress;
			this.unprogressElement.flexibleWidth = 1f - progress;
		}

		// Token: 0x04002935 RID: 10549
		public Text statusLabel;

		// Token: 0x04002936 RID: 10550
		public LayoutElement progressElement;

		// Token: 0x04002937 RID: 10551
		public LayoutElement unprogressElement;

		// Token: 0x04002938 RID: 10552
		private MapEditor editor;
	}
}

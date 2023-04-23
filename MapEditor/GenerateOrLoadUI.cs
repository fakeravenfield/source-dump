using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006C9 RID: 1737
	public class GenerateOrLoadUI : MonoBehaviour
	{
		// Token: 0x06002BDC RID: 11228 RVA: 0x001026DC File Offset: 0x001008DC
		private void Start()
		{
			this.generateDialog.onCancel.AddListener(new UnityAction(this.GenerateDialogCanceled));
			this.buttonLoad.onClick.AddListener(new UnityAction(this.ButtonLoadClicked));
			this.buttonExit.onClick.AddListener(new UnityAction(this.ButtonExitClicked));
			this.buttonGenerate.onClick.AddListener(new UnityAction(this.ButtonGenerateClicked));
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0001E230 File Offset: 0x0001C430
		private void ButtonLoadClicked()
		{
			this.loadDialog.Show();
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0000E16E File Offset: 0x0000C36E
		private void ButtonExitClicked()
		{
			GameManager.ReturnToMenu();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x0001E23D File Offset: 0x0001C43D
		private void ButtonGenerateClicked()
		{
			this.Hide();
			this.generateDialog.Show();
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0001E250 File Offset: 0x0001C450
		private void GenerateDialogCanceled()
		{
			this.generateDialog.Hide();
			this.Show();
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0000969C File Offset: 0x0000789C
		private void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		private void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x04002880 RID: 10368
		public GenerateMapUI generateDialog;

		// Token: 0x04002881 RID: 10369
		public LoadMapUI loadDialog;

		// Token: 0x04002882 RID: 10370
		public Button buttonLoad;

		// Token: 0x04002883 RID: 10371
		public Button buttonGenerate;

		// Token: 0x04002884 RID: 10372
		public Button buttonExit;
	}
}

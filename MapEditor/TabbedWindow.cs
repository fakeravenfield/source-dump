using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B5 RID: 1717
	public class TabbedWindow : WindowBase
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x0010159C File Offset: 0x000FF79C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.applyButton.onClick.AddListener(new UnityAction(this.OnApply));
			WindowBase[] array = this.tabs;
			for (int i = 0; i < array.Length; i++)
			{
				WindowBase tab = array[i];
				Button button = this.tabListView.Add(tab.initialTitle, delegate
				{
					this.ShowTab(tab);
				});
				this.tabPrefab.titleText = button.GetComponentInChildren<Text>();
				tab.transform.SetParent(this.tabContainer, false);
				tab.windowPrefab = this.tabPrefab;
				tab.Hide();
				UnityEngine.Object.Destroy(tab.GetComponent<ContentSizeFitter>());
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0001DC0F File Offset: 0x0001BE0F
		private void OnApply()
		{
			if (this.currentApplyWindow != null)
			{
				this.currentApplyWindow.OnApplyClicked();
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00101674 File Offset: 0x000FF874
		public void ShowWithTab<T>()
		{
			base.Show();
			foreach (WindowBase windowBase in this.tabs)
			{
				if (windowBase.GetComponentInChildren<T>() != null)
				{
					this.ShowTab(windowBase);
					return;
				}
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x001016B8 File Offset: 0x000FF8B8
		private void ShowTab(WindowBase selectedTab)
		{
			WindowBase[] array = this.tabs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Hide();
			}
			if (selectedTab)
			{
				selectedTab.Show();
			}
			if (selectedTab != null)
			{
				this.currentApplyWindow = (selectedTab as IApplyButtonWindow);
				this.applyButton.gameObject.SetActive(this.currentApplyWindow != null);
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0001DC24 File Offset: 0x0001BE24
		protected override void OnHide()
		{
			base.OnHide();
			this.ShowTab(null);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0001DC33 File Offset: 0x0001BE33
		protected override void OnShow()
		{
			base.OnShow();
			this.applyButton.gameObject.SetActive(false);
			this.ShowTab(this.tabs[0]);
		}

		// Token: 0x04002822 RID: 10274
		public WindowFrame tabPrefab;

		// Token: 0x04002823 RID: 10275
		public RectTransform tabContainer;

		// Token: 0x04002824 RID: 10276
		public ListView tabListView;

		// Token: 0x04002825 RID: 10277
		public WindowBase[] tabs;

		// Token: 0x04002826 RID: 10278
		public IApplyButtonWindow currentApplyWindow;

		// Token: 0x04002827 RID: 10279
		public Button applyButton;
	}
}

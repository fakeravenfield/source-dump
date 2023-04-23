using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006C2 RID: 1730
	public class WindowFrame : MonoBehaviour
	{
		// Token: 0x06002BA9 RID: 11177 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		private void Awake()
		{
			if (this.closeButton)
			{
				this.closeButton.onClick.AddListener(new UnityAction(this.CloseButtonClicked));
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0010202C File Offset: 0x0010022C
		public void SetContent(WindowBase content)
		{
			Utils.DestroyChildren(this.containerRect.gameObject);
			RectTransform component = content.GetComponent<RectTransform>();
			component.SetParent(this.containerRect, false);
			component.localPosition = Vector3.zero;
			component.gameObject.SetActive(true);
			this.content = content;
			base.gameObject.name = content.name;
			this.SetTitle(content.initialTitle);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0001E00F File Offset: 0x0001C20F
		public void SetTitle(string title)
		{
			this.titleText.text = title;
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0001E01D File Offset: 0x0001C21D
		private void CloseButtonClicked()
		{
			this.content.Hide();
		}

		// Token: 0x04002850 RID: 10320
		public Text titleText;

		// Token: 0x04002851 RID: 10321
		public Button closeButton;

		// Token: 0x04002852 RID: 10322
		public RectTransform containerRect;

		// Token: 0x04002853 RID: 10323
		private WindowBase content;
	}
}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200070E RID: 1806
	public abstract class SidebarBase : MonoBehaviour
	{
		// Token: 0x06002D38 RID: 11576 RVA: 0x0001F1A1 File Offset: 0x0001D3A1
		protected void Initialize()
		{
			if (!this.isInitialize)
			{
				this.isInitialize = true;
				this.DoInitialize();
			}
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		protected virtual void DoInitialize()
		{
			this.editor = MapEditor.instance;
			this.editorUI = this.editor.GetEditorUI();
			this.toggleExpandButton.onClick.AddListener(new UnityAction(this.Toggle));
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0001F1F2 File Offset: 0x0001D3F2
		protected void Start()
		{
			this.Initialize();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Update()
		{
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0001F1FA File Offset: 0x0001D3FA
		public bool IsExpanded()
		{
			return this.panel.activeSelf;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0001F207 File Offset: 0x0001D407
		public virtual void Expand()
		{
			base.enabled = true;
			this.panel.SetActive(true);
			this.expandIndicatorImage.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0001F240 File Offset: 0x0001D440
		public virtual void Collapse()
		{
			base.enabled = false;
			this.panel.SetActive(false);
			this.expandIndicatorImage.transform.localScale = new Vector3(1f, -1f, 1f);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0001F279 File Offset: 0x0001D479
		public void Toggle()
		{
			if (this.IsExpanded())
			{
				this.Collapse();
				return;
			}
			this.Expand();
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0000969C File Offset: 0x0000789C
		public virtual void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0001F290 File Offset: 0x0001D490
		public virtual void Show()
		{
			base.gameObject.SetActive(true);
			this.Initialize();
		}

		// Token: 0x040029A1 RID: 10657
		public GameObject panel;

		// Token: 0x040029A2 RID: 10658
		public Button toggleExpandButton;

		// Token: 0x040029A3 RID: 10659
		public Image expandIndicatorImage;

		// Token: 0x040029A4 RID: 10660
		protected MapEditor editor;

		// Token: 0x040029A5 RID: 10661
		protected MapEditorUI editorUI;

		// Token: 0x040029A6 RID: 10662
		private bool isInitialize;
	}
}

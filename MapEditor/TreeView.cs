using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006BA RID: 1722
	public class TreeView : MonoBehaviour
	{
		// Token: 0x06002B6F RID: 11119 RVA: 0x00101910 File Offset: 0x000FFB10
		private void Awake()
		{
			if (this.expandButton)
			{
				this.expandButton.onClick.AddListener(new UnityAction(this.ExpandButtonClicked));
			}
			this.containerSizeFitter = this.itemContainer.GetComponent<ContentSizeFitter>();
			this.SwitchAppearance();
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00101960 File Offset: 0x000FFB60
		private void Update()
		{
			if (this.layoutFrames > 0)
			{
				int num = this.layoutFrames - 1;
				this.layoutFrames = num;
				if (num == 0)
				{
					if (this.containerSizeFitter)
					{
						this.containerSizeFitter.enabled = false;
					}
					UtilsUI.EnableLayoutGroups(base.gameObject, false);
				}
			}
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0001DD18 File Offset: 0x0001BF18
		private void ExpandButtonClicked()
		{
			this.Toggle();
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0001DD20 File Offset: 0x0001BF20
		private bool IsChild(TreeView item)
		{
			return item.transform.parent == this.itemContainer.transform;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0001DD3D File Offset: 0x0001BF3D
		private bool HasChildren()
		{
			return this.itemContainer.transform.childCount > 0;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x001019B0 File Offset: 0x000FFBB0
		private void SwitchAppearance()
		{
			bool flag = this.forceCategoryAppearance || this.HasChildren();
			if (this.itemText && this.categoryText)
			{
				this.itemText.gameObject.SetActive(!flag);
				this.categoryText.gameObject.SetActive(flag);
			}
			if (this.expandButton)
			{
				float y = (float)(this.IsExpanded() ? 1 : -1);
				this.expandButton.transform.localScale = new Vector3(1f, y, 1f);
				this.expandButton.gameObject.SetActive(flag);
			}
			if (this.itemText)
			{
				this.itemText.text = this.text;
			}
			if (this.categoryText)
			{
				int childCount = this.itemContainer.childCount;
				this.categoryText.text = string.Format("{0} ({1})", this.text, childCount).ToUpper();
			}
			this.UpdateLayout();
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x00101AC0 File Offset: 0x000FFCC0
		private void UpdateLayout()
		{
			if (this.parent)
			{
				this.parent.UpdateLayout();
				return;
			}
			UtilsUI.EnableLayoutGroups(base.gameObject, true);
			if (this.containerSizeFitter)
			{
				this.containerSizeFitter.enabled = true;
			}
			this.layoutFrames = 10;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0001DD52 File Offset: 0x0001BF52
		public bool IsExpanded()
		{
			return this.itemContainer.gameObject.activeSelf;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public void Expand()
		{
			this.itemContainer.gameObject.SetActive(true);
			this.SwitchAppearance();
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0001DD7D File Offset: 0x0001BF7D
		public void Collapse()
		{
			this.itemContainer.gameObject.SetActive(false);
			this.SwitchAppearance();
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0001DD96 File Offset: 0x0001BF96
		public void Toggle()
		{
			if (this.IsExpanded())
			{
				this.Collapse();
				return;
			}
			this.Expand();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0001DDAD File Offset: 0x0001BFAD
		public void RenderAsCategory()
		{
			this.forceCategoryAppearance = true;
			this.SwitchAppearance();
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		public void SetText(string text)
		{
			this.text = text;
			this.SwitchAppearance();
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0001DDCB File Offset: 0x0001BFCB
		public string GetText()
		{
			return this.text;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0001DDD3 File Offset: 0x0001BFD3
		public TreeView GetParent()
		{
			return this.parent;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0001DDDB File Offset: 0x0001BFDB
		public TreeView[] GetAllItems()
		{
			return (from v in base.GetComponentsInChildren<TreeView>()
			where v && v != this
			select v).ToArray<TreeView>();
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0001DDF9 File Offset: 0x0001BFF9
		public void RegisterOnClick(UnityAction callback)
		{
			if (this.interactButton)
			{
				this.interactButton.onClick.AddListener(callback);
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00101B14 File Offset: 0x000FFD14
		public TreeView Add(string text)
		{
			TreeView treeView;
			if (this.itemPrefab == this.itemPrefab.itemPrefab)
			{
				TreeView original = this.itemPrefab;
				this.itemPrefab = null;
				treeView = UnityEngine.Object.Instantiate<TreeView>(original, this.itemContainer);
				treeView.itemPrefab = original;
				this.itemPrefab = original;
			}
			else
			{
				treeView = UnityEngine.Object.Instantiate<TreeView>(this.itemPrefab, this.itemContainer);
			}
			treeView.parent = this;
			treeView.SetText(text);
			this.SwitchAppearance();
			return treeView;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0001DE19 File Offset: 0x0001C019
		public void Remove(TreeView item)
		{
			if (this.IsChild(item))
			{
				UnityEngine.Object.Destroy(item.gameObject);
				this.SwitchAppearance();
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0001DE35 File Offset: 0x0001C035
		public void Clear()
		{
			Utils.DestroyChildren(this.itemContainer.gameObject);
			this.SwitchAppearance();
		}

		// Token: 0x04002837 RID: 10295
		public RectTransform itemContainer;

		// Token: 0x04002838 RID: 10296
		public TreeView itemPrefab;

		// Token: 0x04002839 RID: 10297
		public Text itemText;

		// Token: 0x0400283A RID: 10298
		public Text categoryText;

		// Token: 0x0400283B RID: 10299
		public Button interactButton;

		// Token: 0x0400283C RID: 10300
		public Button expandButton;

		// Token: 0x0400283D RID: 10301
		private TreeView parent;

		// Token: 0x0400283E RID: 10302
		private string text;

		// Token: 0x0400283F RID: 10303
		private bool forceCategoryAppearance;

		// Token: 0x04002840 RID: 10304
		private const int LAYOUT_FRAMES = 10;

		// Token: 0x04002841 RID: 10305
		private int layoutFrames;

		// Token: 0x04002842 RID: 10306
		private ContentSizeFitter containerSizeFitter;
	}
}

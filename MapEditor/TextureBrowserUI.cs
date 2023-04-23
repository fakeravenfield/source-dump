using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000714 RID: 1812
	public class TextureBrowserUI : WindowBase
	{
		// Token: 0x06002D65 RID: 11621 RVA: 0x001060C4 File Offset: 0x001042C4
		protected override void Awake()
		{
			base.Awake();
			this.buttonOK.onClick.AddListener(delegate()
			{
				this.Close(TextureBrowserUI.DialogResult.Ok);
			});
			this.buttonCancel.onClick.AddListener(delegate()
			{
				this.Close(TextureBrowserUI.DialogResult.Cancel);
			});
			this.inputSearch.onTextChanged.AddListener(new UnityAction<string>(this.SetSearchQuery));
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0001F402 File Offset: 0x0001D602
		protected override void OnShow()
		{
			base.OnShow();
			this.CreateTextureList();
			this.ClearSearchQuery();
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x0010612C File Offset: 0x0010432C
		private void CreateTextureList()
		{
			this.listView.Clear();
			this.listItems.Clear();
			TextureAsset[] array = (from t in AssetTable.GetAllTextures()
			orderby t.texture.name
			select t).ToArray<TextureAsset>();
			this.AddTexture(TextureAsset.empty, "(none)");
			foreach (TextureAsset textureAsset in array)
			{
				this.AddTexture(textureAsset, textureAsset.texture.name);
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x001061B8 File Offset: 0x001043B8
		private void AddTexture(TextureAsset asset, string name)
		{
			name = FuzzySearch.GetDisplayName(name);
			Button button = this.listView.Add(name, null);
			button.GetComponentInChildren<RawImage>().texture = asset.texture;
			button.onClick.AddListener(delegate()
			{
				this.SelectTexture(asset);
			});
			this.listItems.Add(button);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x0001F416 File Offset: 0x0001D616
		private void SelectTexture(TextureAsset asset)
		{
			this.selectedTexture = asset;
			this.Close(TextureBrowserUI.DialogResult.Ok);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x00106228 File Offset: 0x00104428
		private void ClearSearchQuery()
		{
			foreach (Button button in this.listItems)
			{
				button.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x00106280 File Offset: 0x00104480
		private void SetSearchQuery(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				this.ClearSearchQuery();
				return;
			}
			Dictionary<Button, int> scored = new Dictionary<Button, int>(this.listItems.Count);
			foreach (Button button in this.listItems)
			{
				scored.Add(button, FuzzySearch.GetScore(query, button.GetText()));
				button.gameObject.SetActive(false);
			}
			this.listView.SortItems((Button item) => scored[item]);
			foreach (Button button2 in this.listItems)
			{
				button2.gameObject.SetActive(scored[button2] > 0);
			}
			this.scrollBar.value = 1f;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0001F426 File Offset: 0x0001D626
		private void Close(TextureBrowserUI.DialogResult result)
		{
			this.dialogResult = result;
			base.Hide();
			if (this.onClose != null)
			{
				this.onClose(result);
				this.onClose = null;
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x0001F450 File Offset: 0x0001D650
		public TextureAsset GetSelectedTexture()
		{
			return this.selectedTexture;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0001F458 File Offset: 0x0001D658
		public TextureBrowserUI.DialogResult GetDialogResult()
		{
			return this.dialogResult;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x0001F460 File Offset: 0x0001D660
		public void SetCallback(TextureBrowserUI.OnCloseCallback onClose)
		{
			this.onClose = onClose;
		}

		// Token: 0x040029CC RID: 10700
		public ListView listView;

		// Token: 0x040029CD RID: 10701
		public Button buttonOK;

		// Token: 0x040029CE RID: 10702
		public Button buttonCancel;

		// Token: 0x040029CF RID: 10703
		public InputWithText inputSearch;

		// Token: 0x040029D0 RID: 10704
		public Scrollbar scrollBar;

		// Token: 0x040029D1 RID: 10705
		private TextureBrowserUI.DialogResult dialogResult;

		// Token: 0x040029D2 RID: 10706
		private TextureAsset selectedTexture;

		// Token: 0x040029D3 RID: 10707
		private TextureBrowserUI.OnCloseCallback onClose;

		// Token: 0x040029D4 RID: 10708
		private List<Button> listItems = new List<Button>();

		// Token: 0x02000715 RID: 1813
		public enum DialogResult
		{
			// Token: 0x040029D6 RID: 10710
			Ok,
			// Token: 0x040029D7 RID: 10711
			Cancel
		}

		// Token: 0x02000716 RID: 1814
		// (Invoke) Token: 0x06002D74 RID: 11636
		public delegate void OnCloseCallback(TextureBrowserUI.DialogResult dialogResult);
	}
}

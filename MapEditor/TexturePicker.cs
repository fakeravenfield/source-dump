using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006B8 RID: 1720
	public class TexturePicker : MonoBehaviour
	{
		// Token: 0x06002B67 RID: 11111 RVA: 0x00101818 File Offset: 0x000FFA18
		private void Start()
		{
			MapEditor instance = MapEditor.instance;
			this.textureBrowser = instance.GetEditorUI().textureBrowser;
			this.buttonPick.onClick.AddListener(new UnityAction(this.PickTexture));
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0001DCC3 File Offset: 0x0001BEC3
		private void PickTexture()
		{
			this.textureBrowser.SetCallback(new TextureBrowserUI.OnCloseCallback(this.TextureBrowserClosed));
			this.textureBrowser.Show();
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x00101858 File Offset: 0x000FFA58
		private void TextureBrowserClosed(TextureBrowserUI.DialogResult result)
		{
			if (result == TextureBrowserUI.DialogResult.Ok)
			{
				TextureAsset textureAsset = this.textureBrowser.GetSelectedTexture();
				if (this.selectedTexture != textureAsset)
				{
					this.SetTexture(textureAsset);
					this.onTextureChanged.Invoke(textureAsset);
				}
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0001DCE7 File Offset: 0x0001BEE7
		public TextureAsset GetTexture()
		{
			return this.selectedTexture;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x00101898 File Offset: 0x000FFA98
		public void SetTexture(TextureAsset texture)
		{
			this.selectedTexture = texture;
			if (texture != TextureAsset.empty)
			{
				this.textTextureName.text = texture.texture.name;
				this.imagePreview.texture = texture.texture;
				return;
			}
			MapEditor instance = MapEditor.instance;
			this.textTextureName.text = "(none)";
			this.imagePreview.texture = instance.materialList.missingAlbedoTexture;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0001DCEF File Offset: 0x0001BEEF
		public void SetDescription(string description)
		{
			this.textDescription.text = description;
		}

		// Token: 0x0400282F RID: 10287
		public const string NO_TEXTURE_NAME = "(none)";

		// Token: 0x04002830 RID: 10288
		public Text textDescription;

		// Token: 0x04002831 RID: 10289
		public Text textTextureName;

		// Token: 0x04002832 RID: 10290
		public RawImage imagePreview;

		// Token: 0x04002833 RID: 10291
		public Button buttonPick;

		// Token: 0x04002834 RID: 10292
		[NonSerialized]
		public TexturePicker.TextureChangedEvent onTextureChanged = new TexturePicker.TextureChangedEvent();

		// Token: 0x04002835 RID: 10293
		private TextureBrowserUI textureBrowser;

		// Token: 0x04002836 RID: 10294
		private TextureAsset selectedTexture;

		// Token: 0x020006B9 RID: 1721
		public class TextureChangedEvent : UnityEvent<TextureAsset>
		{
		}
	}
}

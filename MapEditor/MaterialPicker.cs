using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006AF RID: 1711
	public class MaterialPicker : MonoBehaviour
	{
		// Token: 0x06002B36 RID: 11062 RVA: 0x001010E4 File Offset: 0x000FF2E4
		private void Start()
		{
			MapEditor instance = MapEditor.instance;
			this.materialBrowser = instance.GetEditorUI().materialBrowser;
			this.buttonPick.onClick.AddListener(new UnityAction(this.PickTexture));
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00101124 File Offset: 0x000FF324
		private void PickTexture()
		{
			this.materialBrowser.SetMode(MaterialBrowserUI.Mode.Select);
			this.materialBrowser.SetCallback(new MaterialBrowserUI.OnCloseCallback(this.MaterialBrowserClosed));
			this.materialBrowser.Show();
			this.materialBrowser.SelectMaterial(this.selectedMaterial);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00101170 File Offset: 0x000FF370
		private void MaterialBrowserClosed(MaterialBrowserUI.DialogResult result)
		{
			if (result == MaterialBrowserUI.DialogResult.Ok)
			{
				MapEditorMaterial mapEditorMaterial = this.materialBrowser.GetSelectedMaterial();
				if (this.selectedMaterial != mapEditorMaterial)
				{
					this.SetMaterial(mapEditorMaterial);
					this.onMaterialChanged.Invoke(this.selectedMaterial);
				}
			}
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0001DB19 File Offset: 0x0001BD19
		public MapEditorMaterial GetTexture()
		{
			return this.selectedMaterial;
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x001011B0 File Offset: 0x000FF3B0
		public void SetMaterial(MapEditorMaterial material)
		{
			this.selectedMaterial = material;
			if (material != null)
			{
				this.textMaterialName.text = material.name;
				this.imagePreview.texture = material.GetAlbedoTexture();
				return;
			}
			MapEditor instance = MapEditor.instance;
			this.textMaterialName.text = "(none)";
			this.imagePreview.texture = instance.materialList.defaultAlbedoTexture;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0001DB21 File Offset: 0x0001BD21
		public void SetDescription(string description)
		{
			this.textDescription.text = description;
		}

		// Token: 0x04002808 RID: 10248
		public Text textDescription;

		// Token: 0x04002809 RID: 10249
		public Text textMaterialName;

		// Token: 0x0400280A RID: 10250
		public RawImage imagePreview;

		// Token: 0x0400280B RID: 10251
		public Button buttonPick;

		// Token: 0x0400280C RID: 10252
		[NonSerialized]
		public MaterialPicker.MaterialChangedEvent onMaterialChanged = new MaterialPicker.MaterialChangedEvent();

		// Token: 0x0400280D RID: 10253
		private MaterialBrowserUI materialBrowser;

		// Token: 0x0400280E RID: 10254
		private MapEditorMaterial selectedMaterial;

		// Token: 0x020006B0 RID: 1712
		public class MaterialChangedEvent : UnityEvent<MapEditorMaterial>
		{
		}
	}
}

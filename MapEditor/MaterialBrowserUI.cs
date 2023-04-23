using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006DA RID: 1754
	public class MaterialBrowserUI : WindowBase
	{
		// Token: 0x06002C31 RID: 11313 RVA: 0x00103194 File Offset: 0x00101394
		protected override void Awake()
		{
			base.Awake();
			this.inputName.onTextChanged.AddListener(new UnityAction<string>(this.MaterialNameChanged));
			this.albedoPicker.onTextureChanged.AddListener(new UnityAction<TextureAsset>(this.AlbedoTextureChanged));
			this.normalPicker.onTextureChanged.AddListener(new UnityAction<TextureAsset>(this.NormalTextureChanged));
			this.buttonAdd.onClick.AddListener(new UnityAction(this.CreateNewMaterial));
			this.buttonRemove.onClick.AddListener(new UnityAction(this.RemoveSelectedMaterial));
			this.buttonClose.onClick.AddListener(delegate()
			{
				this.Close(MaterialBrowserUI.DialogResult.Ok);
			});
			this.buttonSelect.onClick.AddListener(delegate()
			{
				this.Close(MaterialBrowserUI.DialogResult.Ok);
			});
			this.buttonCancel.onClick.AddListener(delegate()
			{
				this.Close(MaterialBrowserUI.DialogResult.Cancel);
			});
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x00103288 File Offset: 0x00101488
		protected override void OnShow()
		{
			base.OnShow();
			this.buttonClose.gameObject.SetActive(this.mode == MaterialBrowserUI.Mode.Browse);
			this.buttonSelect.gameObject.SetActive(this.mode == MaterialBrowserUI.Mode.Select);
			this.buttonCancel.gameObject.SetActive(this.mode == MaterialBrowserUI.Mode.Select);
			if (this.materialList == null)
			{
				this.materialList = MapEditor.instance.materialList;
			}
			this.RefreshMaterialList();
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		private void Close(MaterialBrowserUI.DialogResult result)
		{
			this.dialogResult = result;
			base.Hide();
			if (this.onClose != null)
			{
				this.onClose(result);
				this.onClose = null;
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0001E626 File Offset: 0x0001C826
		public MaterialBrowserUI.DialogResult GetDialogResult()
		{
			return this.dialogResult;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0001E62E File Offset: 0x0001C82E
		public void SetCallback(MaterialBrowserUI.OnCloseCallback onClose)
		{
			this.onClose = onClose;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0001E637 File Offset: 0x0001C837
		public void SetMode(MaterialBrowserUI.Mode mode)
		{
			this.mode = mode;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0001E640 File Offset: 0x0001C840
		public MapEditorMaterial GetSelectedMaterial()
		{
			if (!this.IsMaterialSelected())
			{
				return null;
			}
			return this.selection.material;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0010330C File Offset: 0x0010150C
		public void SelectMaterial(MapEditorMaterial material)
		{
			MaterialBrowserUI.MaterialInfo materialInfo = this.materials.Find((MaterialBrowserUI.MaterialInfo m) => m.material == material);
			if (materialInfo != null)
			{
				this.SelectMaterial(materialInfo);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x00103348 File Offset: 0x00101548
		private void RefreshMaterialList()
		{
			this.listView.Clear();
			this.materials.Clear();
			foreach (MapEditorMaterial material in this.materialList)
			{
				this.AddToListView(material);
			}
			if (this.materials.Any<MaterialBrowserUI.MaterialInfo>())
			{
				this.materials.First<MaterialBrowserUI.MaterialInfo>().item.isOn = true;
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x001033D0 File Offset: 0x001015D0
		private void AddToListView(MapEditorMaterial material)
		{
			MaterialBrowserUI.MaterialInfo info = new MaterialBrowserUI.MaterialInfo();
			Toggle toggle = this.listView.Add(material.name, delegate
			{
				this.SelectMaterial(info);
			});
			RawImage componentInChildren = toggle.GetComponentInChildren<RawImage>();
			componentInChildren.texture = material.GetAlbedoTexture();
			info.material = material;
			info.item = toggle;
			info.preview = componentInChildren;
			this.materials.Add(info);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0010345C File Offset: 0x0010165C
		private void CreateNewMaterial()
		{
			MapEditorMaterial material = this.materialList.CreateDefaultMaterial();
			this.AddToListView(material);
			this.SelectMaterial(material);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x00103484 File Offset: 0x00101684
		private void RemoveSelectedMaterial()
		{
			if (this.IsMaterialSelected())
			{
				MapEditorMaterial material = this.selection.material;
				if (MapEditorTerrain.IsMaterialInUse(material))
				{
					MessageUI.ShowMessage("This material cannot be removed. It is used on the terrain.", "Remove material", null);
					return;
				}
				if (MePrimitiveObject.IsMaterialInUse(material))
				{
					MessageUI.ShowQuestion("This material is used by objects in this level. Removing it will leave these object with a missing (pink) material.\r\n\r\nAre you sure you want to remove this material?", "Remove material", delegate(MessageUI msg)
					{
						if (msg.GetDialogResult() == MessageUI.DialogResult.Yes)
						{
							this.RemoveMaterial(material);
						}
					});
					return;
				}
				this.RemoveMaterial(material);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0010350C File Offset: 0x0010170C
		private void RemoveMaterial(MapEditorMaterial material)
		{
			RemoveMaterialAction action = new RemoveMaterialAction(this.materialList, material);
			MapEditor.instance.AddUndoableAction(action);
			this.materialList.Remove(this.selection.material);
			this.selection = null;
			this.RefreshMaterialList();
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00103554 File Offset: 0x00101754
		private void SelectMaterial(MaterialBrowserUI.MaterialInfo selection)
		{
			this.selection = selection;
			if (this.IsMaterialSelected())
			{
				selection.item.isOn = true;
				this.inputName.SetText(this.selection.material.name);
				this.albedoPicker.SetTexture(this.selection.material.GetAlbedoAsset());
				this.normalPicker.SetTexture(this.selection.material.GetNormalAsset());
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0001E657 File Offset: 0x0001C857
		private void MaterialNameChanged(string newName)
		{
			if (this.IsMaterialSelected())
			{
				this.selection.material.name = newName;
				this.selection.item.SetText(newName);
			}
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0001E683 File Offset: 0x0001C883
		private void AlbedoTextureChanged(TextureAsset textureAsset)
		{
			if (this.IsMaterialSelected())
			{
				this.selection.material.SetAlbedoAsset(textureAsset);
				this.selection.preview.texture = this.selection.material.GetAlbedoTexture();
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0001E6BE File Offset: 0x0001C8BE
		private void NormalTextureChanged(TextureAsset textureAsset)
		{
			if (this.IsMaterialSelected())
			{
				this.selection.material.SetNormalAsset(textureAsset);
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0001E6D9 File Offset: 0x0001C8D9
		private bool IsMaterialSelected()
		{
			return this.selection != null && this.selection.material != null;
		}

		// Token: 0x040028C3 RID: 10435
		public ListViewToggle listView;

		// Token: 0x040028C4 RID: 10436
		public InputWithText inputName;

		// Token: 0x040028C5 RID: 10437
		public TexturePicker albedoPicker;

		// Token: 0x040028C6 RID: 10438
		public TexturePicker normalPicker;

		// Token: 0x040028C7 RID: 10439
		public Button buttonClose;

		// Token: 0x040028C8 RID: 10440
		public Button buttonAdd;

		// Token: 0x040028C9 RID: 10441
		public Button buttonSelect;

		// Token: 0x040028CA RID: 10442
		public Button buttonCancel;

		// Token: 0x040028CB RID: 10443
		public Button buttonRemove;

		// Token: 0x040028CC RID: 10444
		private MaterialBrowserUI.Mode mode;

		// Token: 0x040028CD RID: 10445
		private MaterialBrowserUI.DialogResult dialogResult;

		// Token: 0x040028CE RID: 10446
		private MaterialBrowserUI.OnCloseCallback onClose;

		// Token: 0x040028CF RID: 10447
		private MaterialBrowserUI.MaterialInfo selection;

		// Token: 0x040028D0 RID: 10448
		private MaterialList materialList;

		// Token: 0x040028D1 RID: 10449
		private List<MaterialBrowserUI.MaterialInfo> materials = new List<MaterialBrowserUI.MaterialInfo>();

		// Token: 0x020006DB RID: 1755
		public enum Mode
		{
			// Token: 0x040028D3 RID: 10451
			Browse,
			// Token: 0x040028D4 RID: 10452
			Select
		}

		// Token: 0x020006DC RID: 1756
		public enum DialogResult
		{
			// Token: 0x040028D6 RID: 10454
			Ok,
			// Token: 0x040028D7 RID: 10455
			Cancel
		}

		// Token: 0x020006DD RID: 1757
		// (Invoke) Token: 0x06002C48 RID: 11336
		public delegate void OnCloseCallback(MaterialBrowserUI.DialogResult dialogResult);

		// Token: 0x020006DE RID: 1758
		private class MaterialInfo
		{
			// Token: 0x040028D8 RID: 10456
			public MapEditorMaterial material;

			// Token: 0x040028D9 RID: 10457
			public Toggle item;

			// Token: 0x040028DA RID: 10458
			public RawImage preview;
		}
	}
}

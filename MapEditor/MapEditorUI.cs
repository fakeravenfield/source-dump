using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006D9 RID: 1753
	public class MapEditorUI : MonoBehaviour
	{
		// Token: 0x06002C1C RID: 11292 RVA: 0x0001B231 File Offset: 0x00019431
		private void Awake()
		{
			Utils.SetChildrenActive(base.gameObject, true);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x00103048 File Offset: 0x00101248
		private void Start()
		{
			MeTools.instance.onToolChanged.AddListener(new UnityAction(this.ToolChanged));
			this.hierarchySidebar.Show();
			this.hierarchySidebar.Collapse();
			this.nightVisionIndicator.GetComponentInChildren<Text>().text = string.Format("NIGHT VISION ({0})", SteelInput.GetInput(SteelInput.KeyBinds.Goggles).PositiveLabel());
			this.nightVisionIndicator.SetActive(false);
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x0001E45C File Offset: 0x0001C65C
		private void Update()
		{
			this.nightVisionIndicator.SetActive(NightVision.isEnabled);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x0001E46E File Offset: 0x0001C66E
		private void ToolChanged()
		{
			if (MeTools.instance.IsCurrent<IShowPropertiesSidebarUI>())
			{
				this.ShowOnlyPropertiesSidebar();
				return;
			}
			if (MeTools.instance.IsCurrent<IShowTerrainSidebar>())
			{
				this.ShowOnlyTerrainSidebar();
				return;
			}
			this.HideAll();
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x001030BC File Offset: 0x001012BC
		public void HideAll()
		{
			this.terrainSidebar.Hide();
			this.propertiesSidebar.Hide();
			this.assetBrowser.Hide();
			this.saveMapWindow.Hide();
			this.loadMapWindow.Hide();
			this.editorSettingsWindow.Hide();
			this.helpWindow.Hide();
			this.levelDetailsWindow.Hide();
			this.colorPickerWindow.Hide();
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0001E49C File Offset: 0x0001C69C
		public void ShowOnlySaveDialog()
		{
			this.HideAll();
			this.saveMapWindow.Show();
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0001E4AF File Offset: 0x0001C6AF
		public void ShowOnlyLoadDialog()
		{
			this.HideAll();
			this.loadMapWindow.SetMode(SceneConstructor.Mode.Edit);
			this.loadMapWindow.Show();
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		public void ShowOnlyPlayDialog()
		{
			this.HideAll();
			if (MapEditor.HasDescriptorFilePath())
			{
				MessageUI.ShowQuestion("Save map? Changes are lost otherwise.", "Save before play", delegate(MessageUI msg)
				{
					if (msg.GetDialogResult() == MessageUI.DialogResult.Yes)
					{
						this.SaveThenPlayMap();
						return;
					}
					if (msg.GetDialogResult() == MessageUI.DialogResult.No)
					{
						this.PlayMap(MapEditor.descriptorFilePath);
					}
				});
				return;
			}
			this.SaveThenPlayMap();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0001E502 File Offset: 0x0001C702
		private void SaveThenPlayMap()
		{
			this.saveMapWindow.SetCallback(delegate(SaveMapUI.DialogResult result, string filePath)
			{
				if (result == SaveMapUI.DialogResult.Saved)
				{
					this.PlayMap(filePath);
				}
			});
			this.saveMapWindow.Show();
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0010312C File Offset: 0x0010132C
		private void PlayMap(string filePath)
		{
			try
			{
				SceneConstructor.GotoLoadingScreen(filePath, SceneConstructor.Mode.PlayTest, this.toolsMenu.toggleNight.isOn);
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage(string.Format("An error occurred while trying to load the map descriptor.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
				Debug.LogException(ex);
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0001E526 File Offset: 0x0001C726
		public void ShowOnlyAssetBrowser()
		{
			this.HideAll();
			this.assetBrowser.Show();
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0001E539 File Offset: 0x0001C739
		public void ShowOnlyMaterialBrowser()
		{
			this.HideAll();
			this.materialBrowser.SetMode(MaterialBrowserUI.Mode.Browse);
			this.materialBrowser.Show();
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0001E558 File Offset: 0x0001C758
		private void ShowOnlyTerrainSidebar()
		{
			this.HideAll();
			this.terrainSidebar.Show();
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0001E56B File Offset: 0x0001C76B
		private void ShowOnlyPropertiesSidebar()
		{
			this.HideAll();
			this.propertiesSidebar.Show();
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0001E57E File Offset: 0x0001C77E
		public void ShowOnlyLevelDetails()
		{
			this.HideAll();
			this.levelDetailsWindow.Show();
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0001E591 File Offset: 0x0001C791
		public void ShowOnlyLevelDetails<T>()
		{
			this.ShowOnlyLevelDetails();
			this.levelDetailsWindow.ShowWithTab<T>();
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public void ShowOnlyEditorSettings()
		{
			this.HideAll();
			this.editorSettingsWindow.Show();
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
		public void ShowOnlyHelp()
		{
			this.HideAll();
			this.helpWindow.Show();
		}

		// Token: 0x040028B2 RID: 10418
		public ToolsMenuUI toolsMenu;

		// Token: 0x040028B3 RID: 10419
		public TextureBrowserUI textureBrowser;

		// Token: 0x040028B4 RID: 10420
		public MaterialBrowserUI materialBrowser;

		// Token: 0x040028B5 RID: 10421
		public AssetBrowserUI assetBrowser;

		// Token: 0x040028B6 RID: 10422
		public TerrainSidebarUI terrainSidebar;

		// Token: 0x040028B7 RID: 10423
		public PropertiesSidebarUI propertiesSidebar;

		// Token: 0x040028B8 RID: 10424
		public HierarchySidebarUI hierarchySidebar;

		// Token: 0x040028B9 RID: 10425
		public SaveMapUI saveMapWindow;

		// Token: 0x040028BA RID: 10426
		public LoadMapUI loadMapWindow;

		// Token: 0x040028BB RID: 10427
		public EditorSettingsUI editorSettingsWindow;

		// Token: 0x040028BC RID: 10428
		public HelpUI helpWindow;

		// Token: 0x040028BD RID: 10429
		public SelectionRectangle selectionRectangle;

		// Token: 0x040028BE RID: 10430
		public ProgressUI progressWindow;

		// Token: 0x040028BF RID: 10431
		public SystemMessagesUI systemMessages;

		// Token: 0x040028C0 RID: 10432
		public TabbedWindow levelDetailsWindow;

		// Token: 0x040028C1 RID: 10433
		public ColorPickerUI colorPickerWindow;

		// Token: 0x040028C2 RID: 10434
		public GameObject nightVisionIndicator;
	}
}

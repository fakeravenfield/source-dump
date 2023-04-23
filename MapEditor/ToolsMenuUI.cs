using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200071A RID: 1818
	public class ToolsMenuUI : MonoBehaviour
	{
		// Token: 0x06002D7E RID: 11646 RVA: 0x00106398 File Offset: 0x00104598
		private void Start()
		{
			this.editor = MapEditor.instance;
			this.editorUI = this.editor.GetEditorUI();
			MeTools.instance.onToolChanged.AddListener(new UnityAction(this.ToolChanged));
			this.buttonSave.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlySaveDialog));
			this.buttonLoad.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyLoadDialog));
			this.buttonPlay.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyPlayDialog));
			this.toggleNavMesh.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleNavMesh));
			this.toggleNight.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleNight));
			this.toggleMute.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleMute));
			this.buttonLevelDetails.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyLevelDetails));
			this.buttonEditorSettings.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyEditorSettings));
			this.buttonExit.onClick.AddListener(new UnityAction(this.ExitButtonClicked));
			this.buttonOrientation.onClick.AddListener(new UnityAction(this.ToggleToolOrientation));
			this.toggleSelect.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToNoopTool(on);
			});
			this.toggleTranslate.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToTranslateTool(on);
			});
			this.toggleRotate.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToRotateTool(on);
			});
			this.toggleScale.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToScaleTool(on);
			});
			this.toggleTerrain.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToTerrainTool(on);
			});
			this.togglePlace.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToPlaceTool(on);
			});
			this.togglePhoto.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToPhotoMode(on);
			});
			this.buttonMaterials.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyMaterialBrowser));
			this.buttonAssets.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyAssetBrowser));
			this.buttonHelp.onClick.AddListener(new UnityAction(this.editorUI.ShowOnlyHelp));
			this.SetToolOrientation(MeTools.Orientation.Global);
			this.toggleMute.isOn = this.editor.IsAudioMuted();
			this.toggleNight.isOn = GameManager.GameParameters().nightMode;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00106668 File Offset: 0x00104868
		private void ToolChanged()
		{
			if (MeTools.instance.IsCurrent<NoopTool>())
			{
				this.toggleSelect.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<TranslateTool>())
			{
				this.toggleTranslate.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<RotateTool>())
			{
				this.toggleRotate.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<ScaleTool>())
			{
				this.toggleScale.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<TerrainTool>())
			{
				this.toggleTerrain.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<PlaceTool>())
			{
				this.togglePlace.isOn = true;
				return;
			}
			if (MeTools.instance.IsCurrent<PhotoTool>())
			{
				this.togglePhoto.isOn = true;
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		private void SwitchToNoopTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<NoopTool>())
			{
				MeTools.instance.SwitchToNoopTool();
			}
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x0001F4E3 File Offset: 0x0001D6E3
		private void SwitchToTranslateTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<TranslateTool>())
			{
				MeTools.instance.SwitchToTranslateTool();
			}
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x0001F4FE File Offset: 0x0001D6FE
		private void SwitchToRotateTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<RotateTool>())
			{
				MeTools.instance.SwitchToRotateTool();
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0001F519 File Offset: 0x0001D719
		private void SwitchToScaleTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<ScaleTool>())
			{
				MeTools.instance.SwitchToScaleTool();
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x0001F534 File Offset: 0x0001D734
		private void SwitchToTerrainTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<TerrainTool>())
			{
				MeTools.instance.SwitchToTerrainHeightTool();
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x0001F54F File Offset: 0x0001D74F
		private void SwitchToPlaceTool(bool doIt)
		{
			if (doIt && !MeTools.instance.IsCurrent<PlaceTool>())
			{
				MeTools.instance.SwitchToNoopTool();
				this.editorUI.ShowOnlyAssetBrowser();
			}
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0001F575 File Offset: 0x0001D775
		public void SwitchToPhotoMode(bool doIt = true)
		{
			if (doIt && !MeTools.instance.IsCurrent<PhotoTool>())
			{
				MeTools.instance.SwitchToPhotoTool();
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0001F590 File Offset: 0x0001D790
		private void ExitButtonClicked()
		{
			MessageUI.ShowQuestion("Are you sure? Any changes are lost.", "Exit", delegate(MessageUI msg)
			{
				if (msg.GetDialogResult() == MessageUI.DialogResult.Yes)
				{
					GameManager.ReturnToMenu();
				}
			});
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		private void ToggleToolOrientation()
		{
			if (MeTools.instance.GetOrientation() == MeTools.Orientation.Global)
			{
				this.SetToolOrientation(MeTools.Orientation.WithSelection);
				return;
			}
			this.SetToolOrientation(MeTools.Orientation.Global);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0001F5DD File Offset: 0x0001D7DD
		private void SetToolOrientation(MeTools.Orientation orientation)
		{
			if (orientation == MeTools.Orientation.Global)
			{
				this.buttonOrientation.SetText("GLOBAL");
			}
			else
			{
				this.buttonOrientation.SetText("LOCAL");
			}
			MeTools.instance.SetOrientation(orientation);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0001F60F File Offset: 0x0001D80F
		private void ToggleNavMesh(bool isOn)
		{
			this.editor.HideNavMesh();
			if (isOn)
			{
				base.StartCoroutine(this.GenerateNavMesh());
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0001F62C File Offset: 0x0001D82C
		private IEnumerator GenerateNavMesh()
		{
			yield return this.editor.GenerateNavMeshAsync();
			this.editor.ShowNavMesh();
			yield break;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0001F63B File Offset: 0x0001D83B
		private void ToggleNight(bool isOn)
		{
			if (isOn)
			{
				this.editor.NightTime();
				return;
			}
			this.editor.DayTime();
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0001F657 File Offset: 0x0001D857
		private void ToggleMute(bool isOn)
		{
			this.editor.MuteAudio(isOn);
		}

		// Token: 0x040029DD RID: 10717
		public Button buttonSave;

		// Token: 0x040029DE RID: 10718
		public Button buttonLoad;

		// Token: 0x040029DF RID: 10719
		public Button buttonPlay;

		// Token: 0x040029E0 RID: 10720
		public Button buttonLevelDetails;

		// Token: 0x040029E1 RID: 10721
		public Toggle toggleNavMesh;

		// Token: 0x040029E2 RID: 10722
		public Toggle toggleNight;

		// Token: 0x040029E3 RID: 10723
		public Toggle toggleMute;

		// Token: 0x040029E4 RID: 10724
		public Button buttonEditorSettings;

		// Token: 0x040029E5 RID: 10725
		public Button buttonExit;

		// Token: 0x040029E6 RID: 10726
		public Button buttonOrientation;

		// Token: 0x040029E7 RID: 10727
		public Toggle toggleSelect;

		// Token: 0x040029E8 RID: 10728
		public Toggle toggleTranslate;

		// Token: 0x040029E9 RID: 10729
		public Toggle toggleRotate;

		// Token: 0x040029EA RID: 10730
		public Toggle toggleScale;

		// Token: 0x040029EB RID: 10731
		public Toggle toggleTerrain;

		// Token: 0x040029EC RID: 10732
		public Toggle togglePlace;

		// Token: 0x040029ED RID: 10733
		public Toggle togglePhoto;

		// Token: 0x040029EE RID: 10734
		public Button buttonMaterials;

		// Token: 0x040029EF RID: 10735
		public Button buttonAssets;

		// Token: 0x040029F0 RID: 10736
		public Button buttonHelp;

		// Token: 0x040029F1 RID: 10737
		private MapEditor editor;

		// Token: 0x040029F2 RID: 10738
		private MapEditorUI editorUI;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000711 RID: 1809
	public class TerrainSidebarUI : SidebarBase
	{
		// Token: 0x06002D4D RID: 11597 RVA: 0x00105C54 File Offset: 0x00103E54
		protected override void DoInitialize()
		{
			base.DoInitialize();
			this.editorTerrain = this.editor.GetEditorTerrain();
			this.heightBrush = MeTools.instance.terrainHeightTool;
			this.alphaBrush = MeTools.instance.terrainAlphaTool;
			this.toggleRaise.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToRaiseOperation(on);
			});
			this.toggleFlatten.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToFlattenOperation(on);
			});
			this.toggleSmooth.onValueChanged.AddListener(delegate(bool on)
			{
				this.SwitchToSmoothOperation(on);
			});
			this.sliderBrushSize.SetRange(1f, 1000f, 100f);
			this.sliderBrushIntensity.SetRange(1f, 100f);
			this.sliderBrushSize.SetValue(20f);
			this.sliderBrushIntensity.SetValue(50f);
			this.sliderBrushSize.onValueChanged.AddListener(new UnityAction<float>(this.BrushSizeChanged));
			this.sliderBrushIntensity.onValueChanged.AddListener(new UnityAction<float>(this.BrushIntensityChanged));
			this.buttonAddLayer.onClick.AddListener(new UnityAction(this.AddLayer));
			this.buttonRemoveLayer.onClick.AddListener(new UnityAction(this.RemoveLayer));
			this.buttonEditLayerMaterial.onClick.AddListener(new UnityAction(this.EditLayerMaterial));
			this.buttonAutoPaint.onClick.AddListener(new UnityAction(this.RunPileTexturer));
			this.toggleRaise.isOn = true;
			this.SwitchToRaiseOperation(true);
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		public override void Show()
		{
			base.Show();
			base.Initialize();
			this.BuildLayerList();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x00105DF8 File Offset: 0x00103FF8
		private void BuildLayerList()
		{
			this.listViewLayers.Clear();
			using (IEnumerator<TerrainAlphamap.Layer> enumerator = this.editorTerrain.GetAlphamap().GetLayers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TerrainAlphamap.Layer layer = enumerator.Current;
					string name = layer.GetName();
					this.listViewLayers.Add(name, delegate
					{
						this.SelectLayer(layer);
					});
				}
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x0001F310 File Offset: 0x0001D510
		private void SelectLayer(TerrainAlphamap.Layer layer)
		{
			MeTools.instance.SwitchToTerrainAlphaTool();
			this.alphaBrush.SetActiveLayer(layer);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0001F328 File Offset: 0x0001D528
		public void BrushSizeChanged(float value)
		{
			this.heightBrush.SetSize(value);
			this.alphaBrush.SetSize(value);
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x00105E8C File Offset: 0x0010408C
		public void BrushIntensityChanged(float value)
		{
			float intensity = value / this.sliderBrushIntensity.GetMaximumValue();
			this.heightBrush.SetIntensity(intensity);
			this.alphaBrush.SetIntensity(intensity);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0001F342 File Offset: 0x0001D542
		private void SwitchToRaiseOperation(bool doIt)
		{
			if (doIt)
			{
				MeTools.instance.SwitchToTerrainHeightTool();
				this.heightBrush.SetOperation(TerrainHeightTool.Operation.Raise);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0001F35D File Offset: 0x0001D55D
		private void SwitchToSmoothOperation(bool doIt)
		{
			if (doIt)
			{
				MeTools.instance.SwitchToTerrainHeightTool();
				this.heightBrush.SetOperation(TerrainHeightTool.Operation.Smooth);
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0001F378 File Offset: 0x0001D578
		private void SwitchToFlattenOperation(bool doIt)
		{
			if (doIt)
			{
				MeTools.instance.SwitchToTerrainHeightTool();
				this.heightBrush.SetOperation(TerrainHeightTool.Operation.Flatten);
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0001F393 File Offset: 0x0001D593
		private void SwitchToAlphaBrush(bool doIt)
		{
			if (doIt)
			{
				MeTools.instance.SwitchToTerrainAlphaTool();
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x0001F3A2 File Offset: 0x0001D5A2
		private void AddLayer()
		{
			MessageUI.ShowPrompt("Enter a new material name", "Add layer", "New layer", delegate(MessageUI msg)
			{
				if (msg.GetDialogResult() == MessageUI.DialogResult.Ok)
				{
					string inputText = msg.GetInputText();
					MapEditorMaterial mapEditorMaterial = this.editor.materialList.CreateDefaultMaterial();
					mapEditorMaterial.name = inputText;
					TerrainAlphamap alphamap = this.editorTerrain.GetAlphamap();
					RestoreTerrainAlphaAction action = new RestoreTerrainAlphaAction(alphamap);
					this.editor.AddUndoableAction(action);
					alphamap.CreateLayer(mapEditorMaterial);
					this.BuildLayerList();
					this.editorUI.materialBrowser.SetMode(MaterialBrowserUI.Mode.Browse);
					this.editorUI.materialBrowser.Show();
					this.editorUI.materialBrowser.SelectMaterial(mapEditorMaterial);
				}
			});
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00105EC0 File Offset: 0x001040C0
		private void RemoveLayer()
		{
			TerrainAlphamap.Layer layer = this.alphaBrush.GetActiveLayer();
			if (this.editorTerrain.CanRemoveLayer(layer))
			{
				MessageUI.ShowQuestion("Are you sure you want to remove this layer?", "Remove layer", delegate(MessageUI msg)
				{
					if (msg.GetDialogResult() == MessageUI.DialogResult.Yes)
					{
						TerrainAlphamap alphamap = this.editorTerrain.GetAlphamap();
						RestoreTerrainAlphaAction action = new RestoreTerrainAlphaAction(alphamap);
						this.editor.AddUndoableAction(action);
						alphamap.RemoveLayer(layer);
						this.BuildLayerList();
					}
				});
				return;
			}
			MessageUI.ShowMessage("This layer cannot be removed.", "Remove layer", null);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x00105F2C File Offset: 0x0010412C
		private void EditLayerMaterial()
		{
			TerrainAlphamap.Layer activeLayer = this.alphaBrush.GetActiveLayer();
			if (activeLayer != null)
			{
				MaterialBrowserUI materialBrowser = this.editorUI.materialBrowser;
				materialBrowser.SetMode(MaterialBrowserUI.Mode.Browse);
				materialBrowser.SetCallback(delegate(MaterialBrowserUI.DialogResult _)
				{
					this.BuildLayerList();
				});
				materialBrowser.Show();
				materialBrowser.SelectMaterial(activeLayer.GetMaterial());
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00105F80 File Offset: 0x00104180
		private void RunPileTexturer()
		{
			TerrainAlphamap alphamap = this.editorTerrain.GetAlphamap();
			RestoreTerrainAlphaAction action = new RestoreTerrainAlphaAction(alphamap);
			this.editor.AddUndoableAction(action);
			float waterOffsetFromOriginal = this.editorTerrain.biomeContainer.GetWaterOffsetFromOriginal();
			PileTexturer pileTexturer = this.editorTerrain.biomeContainer.GetPileTexturer();
			pileTexturer.heightOffset = -waterOffsetFromOriginal;
			alphamap.ApplyPileTexturer(pileTexturer);
		}

		// Token: 0x040029BB RID: 10683
		public Toggle toggleRaise;

		// Token: 0x040029BC RID: 10684
		public Toggle toggleFlatten;

		// Token: 0x040029BD RID: 10685
		public Toggle toggleSmooth;

		// Token: 0x040029BE RID: 10686
		public SliderWithInput sliderBrushSize;

		// Token: 0x040029BF RID: 10687
		public SliderWithInput sliderBrushIntensity;

		// Token: 0x040029C0 RID: 10688
		public Button buttonAddLayer;

		// Token: 0x040029C1 RID: 10689
		public Button buttonRemoveLayer;

		// Token: 0x040029C2 RID: 10690
		public Button buttonEditLayerMaterial;

		// Token: 0x040029C3 RID: 10691
		public Button buttonAutoPaint;

		// Token: 0x040029C4 RID: 10692
		public ListView listViewLayers;

		// Token: 0x040029C5 RID: 10693
		private MapEditorTerrain editorTerrain;

		// Token: 0x040029C6 RID: 10694
		private TerrainHeightTool heightBrush;

		// Token: 0x040029C7 RID: 10695
		private TerrainAlphaTool alphaBrush;
	}
}

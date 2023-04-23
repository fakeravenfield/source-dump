using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005D8 RID: 1496
	public class GeneratedSceneConstructor : ISceneConstructor
	{
		// Token: 0x060026B2 RID: 9906 RVA: 0x0001AC87 File Offset: 0x00018E87
		public GeneratedSceneConstructor(BiomeContainer biomeContainer, BiomeAsset biomeAsset)
		{
			this.biomeContainer = biomeContainer;
			this.biomeAsset = biomeAsset;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0001AC9D File Offset: 0x00018E9D
		public void StartSceneConstruction()
		{
			this.biomeContainer.gameObject.SetActive(false);
			MapEditorAssistant.instance.lateAwake.gameObject.SetActive(false);
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x0001ACC5 File Offset: 0x00018EC5
		public void EndSceneConstruction()
		{
			this.biomeContainer.gameObject.SetActive(true);
			MapEditorAssistant.instance.lateAwake.gameObject.SetActive(true);
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0001ACED File Offset: 0x00018EED
		public IEnumerable<SceneConstructionProgress> ConstructSceneAsync()
		{
			MapEditorAssistant instance = MapEditorAssistant.instance;
			MapEditor.descriptorFilePath = "";
			MapEditor editor = instance.editor;
			MapEditorTerrain editorTerrain = editor.editorTerrain;
			MinimapCamera minimapCamera = instance.minimapCamera;
			Transform lateAwake = instance.lateAwake;
			UnityEngine.Object.DestroyImmediate(editorTerrain.biomeContainer.gameObject);
			editorTerrain.biomeContainer = this.biomeContainer;
			editorTerrain.biomeAsset = this.biomeAsset;
			TerrainAlphamap alphamap = editorTerrain.GetAlphamap();
			alphamap.InitializeFromTerrain();
			MapEditorMaterial[] materials = (from l in alphamap.GetLayers()
			select l.GetMaterial()).ToArray<MapEditorMaterial>();
			editor.materialList.AddRange(materials);
			minimapCamera.ResizeToIncludeAllTerrain(this.biomeContainer.GetTerrain());
			yield return new SceneConstructionProgress("Done", 1f);
			yield break;
		}

		// Token: 0x040024FE RID: 9470
		private BiomeContainer biomeContainer;

		// Token: 0x040024FF RID: 9471
		private BiomeAsset biomeAsset;
	}
}

using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000766 RID: 1894
	[Serializable]
	public struct TerrainDataV1
	{
		// Token: 0x06002EDA RID: 11994 RVA: 0x00109DAC File Offset: 0x00107FAC
		public TerrainDataV1(MapEditorTerrain editorTerrain)
		{
			this.size = editorTerrain.GetTerrainSize();
			this.waterLevel = editorTerrain.GetWaterLevel();
			this.biome = new AssetIdDataV1(editorTerrain.biomeAsset.id);
			this.heightmap = new HeightmapDataV1(editorTerrain.GetHeightmap());
			this.alphamap = new AlphamapDataV1(editorTerrain.GetAlphamap());
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00020462 File Offset: 0x0001E662
		private BiomeTerrainSettings GetBiomeTerrainSettings()
		{
			return new BiomeTerrainSettings(this.size, this.heightmap.resolution, this.alphamap.resolution, false);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x00109E0C File Offset: 0x0010800C
		public void CopyTo(MapEditorTerrain editorTerrain, MaterialList materialList)
		{
			BiomeTerrainSettings biomeTerrainSettings = this.GetBiomeTerrainSettings();
			editorTerrain.biomeAsset = AssetTable.GetBiome(this.biome);
			editorTerrain.biomeContainer.Populate(editorTerrain.biomeAsset, biomeTerrainSettings);
			this.heightmap.CopyTo(editorTerrain.GetHeightmap());
			this.alphamap.CopyTo(editorTerrain.GetAlphamap(), materialList);
			FixBundleShaders.ApplyTerrainFallbackMaterial(editorTerrain.GetTerrain());
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00109E78 File Offset: 0x00108078
		public void CopyTo(BiomeContainer biomeContainer, MaterialList materialList)
		{
			BiomeTerrainSettings biomeTerrainSettings = this.GetBiomeTerrainSettings();
			BiomeAsset asset = AssetTable.GetBiome(this.biome);
			biomeContainer.Populate(asset, biomeTerrainSettings);
			Terrain terrain = biomeContainer.GetTerrain();
			this.heightmap.CopyTo(new TerrainHeightmap(terrain));
			this.alphamap.CopyTo(new TerrainAlphamap(terrain), materialList);
			FixBundleShaders.ApplyTerrainFallbackMaterial(terrain);
		}

		// Token: 0x04002B05 RID: 11013
		public int size;

		// Token: 0x04002B06 RID: 11014
		public float waterLevel;

		// Token: 0x04002B07 RID: 11015
		public AssetIdDataV1 biome;

		// Token: 0x04002B08 RID: 11016
		public HeightmapDataV1 heightmap;

		// Token: 0x04002B09 RID: 11017
		public AlphamapDataV1 alphamap;
	}
}

using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200075C RID: 1884
	[Serializable]
	public struct MaterialDataV1
	{
		// Token: 0x06002EB2 RID: 11954 RVA: 0x001097B8 File Offset: 0x001079B8
		public MaterialDataV1(MapEditorMaterial editorMaterial)
		{
			this.guid = editorMaterial.guid;
			this.name = editorMaterial.name;
			this.albedoTexture = new AssetIdDataV1(editorMaterial.GetAlbedoAsset().id);
			this.normalTexture = new AssetIdDataV1(editorMaterial.GetNormalAsset().id);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0002020C File Offset: 0x0001E40C
		public MapEditorMaterial ToEditorMaterial(MaterialList materialList)
		{
			return new MapEditorMaterial(materialList, this.name, this.guid, AssetTable.GetTexture(this.albedoTexture), AssetTable.GetTexture(this.normalTexture));
		}

		// Token: 0x04002ACF RID: 10959
		public string guid;

		// Token: 0x04002AD0 RID: 10960
		public string name;

		// Token: 0x04002AD1 RID: 10961
		public AssetIdDataV1 albedoTexture;

		// Token: 0x04002AD2 RID: 10962
		public AssetIdDataV1 normalTexture;
	}
}

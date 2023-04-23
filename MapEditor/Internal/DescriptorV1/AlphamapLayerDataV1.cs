using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200076A RID: 1898
	[Serializable]
	public class AlphamapLayerDataV1
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x0002049A File Offset: 0x0001E69A
		public AlphamapLayerDataV1(TerrainAlphamap.Layer layer)
		{
			this.material = layer.GetMaterial().guid;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000204B3 File Offset: 0x0001E6B3
		public MapEditorMaterial FindMaterial(MaterialList materialList)
		{
			return materialList.FindOrCreate(this.material);
		}

		// Token: 0x04002B11 RID: 11025
		public string material;
	}
}

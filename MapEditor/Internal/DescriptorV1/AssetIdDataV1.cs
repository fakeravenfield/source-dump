using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public struct AssetIdDataV1
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x000200F4 File Offset: 0x0001E2F4
		public AssetIdDataV1(AssetId id)
		{
			this.guid = id.guid;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x00020102 File Offset: 0x0001E302
		public static implicit operator AssetId(AssetIdDataV1 id)
		{
			return new AssetId(id.guid);
		}

		// Token: 0x04002AA9 RID: 10921
		public string guid;
	}
}

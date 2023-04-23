using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200075F RID: 1887
	[Serializable]
	public struct SpawnPointDataV1
	{
		// Token: 0x06002EBC RID: 11964 RVA: 0x00020272 File Offset: 0x0001E472
		public SpawnPointDataV1(MeoSpawnPoint sp)
		{
			this.name = sp.name;
			this.type = SpawnPointDataV1.SPAWN.Encode(sp.spawnType);
			this.transform = new PositionRotationDataV1(sp.transform);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000202A7 File Offset: 0x0001E4A7
		public void CopyTo(MeoSpawnPoint sp)
		{
			sp.name = this.name;
			sp.spawnType = this.GetSpawnType();
			this.transform.CopyTo(sp.transform);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000202D2 File Offset: 0x0001E4D2
		public MeoSpawnPoint.SpawnType GetSpawnType()
		{
			return SpawnPointDataV1.SPAWN.Decode(this.type);
		}

		// Token: 0x04002AE0 RID: 10976
		private static readonly EnumEncoder<MeoSpawnPoint.SpawnType> SPAWN = new EnumEncoder<MeoSpawnPoint.SpawnType>();

		// Token: 0x04002AE1 RID: 10977
		public string name;

		// Token: 0x04002AE2 RID: 10978
		public string type;

		// Token: 0x04002AE3 RID: 10979
		public PositionRotationDataV1 transform;
	}
}

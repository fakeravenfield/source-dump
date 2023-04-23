using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000764 RID: 1892
	[Serializable]
	public struct TurretSpawnerDataV1
	{
		// Token: 0x06002ED0 RID: 11984 RVA: 0x0002039B File Offset: 0x0001E59B
		public TurretSpawnerDataV1(MeoTurretSpawner tp)
		{
			this.name = tp.name;
			this.transform = new PositionRotationDataV1(tp.transform);
			this.turret = TurretSpawnerDataV1.TURRET.Encode(tp.turret);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public void CopyTo(MeoTurretSpawner tp)
		{
			tp.name = this.name;
			this.transform.CopyTo(tp.transform);
			tp.turret = this.GetTurretType();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000203FB File Offset: 0x0001E5FB
		public void CopyTo(TurretSpawner tp)
		{
			tp.name = this.name;
			this.transform.CopyTo(tp.transform);
			tp.typeToSpawn = this.GetTurretType();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00020426 File Offset: 0x0001E626
		public TurretSpawner.TurretSpawnType GetTurretType()
		{
			return TurretSpawnerDataV1.TURRET.Decode(this.turret);
		}

		// Token: 0x04002AFA RID: 11002
		private static readonly EnumEncoder<TurretSpawner.TurretSpawnType> TURRET = new EnumEncoder<TurretSpawner.TurretSpawnType>();

		// Token: 0x04002AFB RID: 11003
		public string name;

		// Token: 0x04002AFC RID: 11004
		public PositionRotationDataV1 transform;

		// Token: 0x04002AFD RID: 11005
		public string turret;
	}
}

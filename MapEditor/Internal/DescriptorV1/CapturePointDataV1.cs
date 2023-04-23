using System;
using System.Linq;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200075D RID: 1885
	[Serializable]
	public struct CapturePointDataV1
	{
		// Token: 0x06002EB4 RID: 11956 RVA: 0x0010980C File Offset: 0x00107A0C
		public CapturePointDataV1(MeoCapturePoint cp)
		{
			this.transform = new PositionRotationDataV1(cp.transform);
			this.name = cp.name;
			this.shortName = cp.shortName;
			this.owner = CapturePointDataV1.TEAM.Encode(cp.owner);
			this.protectRange = cp.protectRange;
			this.captureRange = cp.captureRange;
			this.captureCeiling = cp.captureCeiling;
			this.captureFloor = cp.captureFloor;
			this.captureRate = cp.captureRate;
			this.spawnPoints = (from sp in cp.GetSpawnPoints()
			select new SpawnPointDataV1(sp)).ToArray<SpawnPointDataV1>();
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x001098CC File Offset: 0x00107ACC
		public void CopyTo(MeoCapturePoint cp)
		{
			this.transform.CopyTo(cp.transform);
			cp.name = this.name;
			cp.shortName = this.shortName;
			cp.owner = this.GetOwner();
			cp.protectRange = this.protectRange;
			cp.captureRange = this.captureRange;
			cp.captureCeiling = this.captureCeiling;
			cp.captureFloor = this.captureFloor;
			cp.captureRate = this.captureRate;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0010994C File Offset: 0x00107B4C
		public void CopyTo(CapturePoint cp)
		{
			this.transform.CopyTo(cp.transform);
			cp.name = this.name;
			cp.defaultOwner = this.GetOwner();
			cp.shortName = this.shortName;
			cp.protectRange = this.protectRange;
			cp.captureRange = this.captureRange;
			cp.captureCeiling = this.captureCeiling;
			cp.captureFloor = this.captureFloor;
			cp.captureRate = this.captureRate;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00020240 File Offset: 0x0001E440
		public SpawnPoint.Team GetOwner()
		{
			return CapturePointDataV1.TEAM.Decode(this.owner);
		}

		// Token: 0x04002AD3 RID: 10963
		private static readonly EnumEncoder<SpawnPoint.Team> TEAM = new EnumEncoder<SpawnPoint.Team>();

		// Token: 0x04002AD4 RID: 10964
		public PositionRotationDataV1 transform;

		// Token: 0x04002AD5 RID: 10965
		public string name;

		// Token: 0x04002AD6 RID: 10966
		public string shortName;

		// Token: 0x04002AD7 RID: 10967
		public string owner;

		// Token: 0x04002AD8 RID: 10968
		public float protectRange;

		// Token: 0x04002AD9 RID: 10969
		public float captureRange;

		// Token: 0x04002ADA RID: 10970
		public float captureCeiling;

		// Token: 0x04002ADB RID: 10971
		public float captureFloor;

		// Token: 0x04002ADC RID: 10972
		public float captureRate;

		// Token: 0x04002ADD RID: 10973
		public SpawnPointDataV1[] spawnPoints;
	}
}

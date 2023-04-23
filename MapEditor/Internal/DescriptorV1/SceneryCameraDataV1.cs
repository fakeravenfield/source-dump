using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000756 RID: 1878
	[Serializable]
	public struct SceneryCameraDataV1
	{
		// Token: 0x06002EA4 RID: 11940 RVA: 0x0002014D File Offset: 0x0001E34D
		public SceneryCameraDataV1(MeoSceneryCamera camera)
		{
			this.transform = new PositionRotationDataV1(camera.transform);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00020160 File Offset: 0x0001E360
		public void CopyTo(MeoSceneryCamera meo)
		{
			this.transform.CopyTo(meo.transform);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00020160 File Offset: 0x0001E360
		public void CopyTo(SceneryCamera sc)
		{
			this.transform.CopyTo(sc.transform);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00020173 File Offset: 0x0001E373
		public bool IsValid()
		{
			return !this.transform.IsZero();
		}

		// Token: 0x04002AAE RID: 10926
		public PositionRotationDataV1 transform;
	}
}

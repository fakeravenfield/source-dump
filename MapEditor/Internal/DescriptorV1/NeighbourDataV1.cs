using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	public struct NeighbourDataV1
	{
		// Token: 0x06002EC0 RID: 11968 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public NeighbourDataV1(MeoCapturePoint.Neighbour neighbour, int capturePointA, int capturePointB)
		{
			this.capturePointA = capturePointA;
			this.capturePointB = capturePointB;
			this.landConnection = neighbour.landConnection;
			this.waterConnection = neighbour.waterConnection;
			this.oneWay = neighbour.oneWay;
		}

		// Token: 0x04002AE4 RID: 10980
		public int capturePointA;

		// Token: 0x04002AE5 RID: 10981
		public int capturePointB;

		// Token: 0x04002AE6 RID: 10982
		public bool landConnection;

		// Token: 0x04002AE7 RID: 10983
		public bool waterConnection;

		// Token: 0x04002AE8 RID: 10984
		public bool oneWay;
	}
}

using System;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000462 RID: 1122
	[Serializable]
	public struct AtlasPadding
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x000155A5 File Offset: 0x000137A5
		public AtlasPadding(int p)
		{
			this.topBottom = p;
			this.leftRight = p;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000155B5 File Offset: 0x000137B5
		public AtlasPadding(int px, int py)
		{
			this.topBottom = py;
			this.leftRight = px;
		}

		// Token: 0x04001D38 RID: 7480
		public int topBottom;

		// Token: 0x04001D39 RID: 7481
		public int leftRight;
	}
}

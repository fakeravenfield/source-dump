using System;

namespace Campaign
{
	// Token: 0x020003DF RID: 991
	[Serializable]
	public struct ResourceAmount
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x00012F93 File Offset: 0x00011193
		public ResourceAmount(ResourceType type, int amount)
		{
			this.type = type;
			this.amount = amount;
		}

		// Token: 0x04001A7B RID: 6779
		public ResourceType type;

		// Token: 0x04001A7C RID: 6780
		public int amount;
	}
}

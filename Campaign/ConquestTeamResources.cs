using System;

namespace Campaign
{
	// Token: 0x020003E3 RID: 995
	public class ConquestTeamResources
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x000A793C File Offset: 0x000A5B3C
		public void Clear()
		{
			for (int i = 0; i < 16; i++)
			{
				this.resourceGain[i] = 0;
				this.availableResources[i] = 0;
			}
		}

		// Token: 0x04001AAF RID: 6831
		public const int RESOURCES_MAX_COUNT = 16;

		// Token: 0x04001AB0 RID: 6832
		public int[] resourceGain = new int[16];

		// Token: 0x04001AB1 RID: 6833
		public int[] availableResources = new int[16];
	}
}

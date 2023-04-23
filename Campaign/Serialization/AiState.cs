using System;
using Campaign.Tech;

namespace Campaign.Serialization
{
	// Token: 0x0200041A RID: 1050
	[Serializable]
	public class AiState
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x000AC778 File Offset: 0x000AA978
		public TechTreeEntry GetTargetTech()
		{
			if (this.techTarget == null)
			{
				return null;
			}
			TechTreeEntry result;
			try
			{
				result = CampaignBase.instance.aiTeamTechTrees[this.techTarget.techTreeIndex].entries[this.techTarget.entryIndex];
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00014211 File Offset: 0x00012411
		public bool HasTargetTech()
		{
			return this.GetTargetTech() != null;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0001421C File Offset: 0x0001241C
		public void SetTargetTech(TechTreeEntry entry)
		{
			if (entry == null)
			{
				this.techTarget = null;
				return;
			}
			this.techTarget = new AiState.TechTreeTarget(CampaignBase.instance.aiTeamTechTrees.IndexOf(entry.techTree), entry.techTree.entries.IndexOf(entry));
		}

		// Token: 0x04001BE1 RID: 7137
		public AiState.TechTreeTarget techTarget;

		// Token: 0x0200041B RID: 1051
		[Serializable]
		public class TechTreeTarget
		{
			// Token: 0x06001A31 RID: 6705 RVA: 0x0001425A File Offset: 0x0001245A
			public TechTreeTarget()
			{
				this.techTreeIndex = -1;
				this.entryIndex = -1;
			}

			// Token: 0x06001A32 RID: 6706 RVA: 0x00014270 File Offset: 0x00012470
			public TechTreeTarget(int techTreeIndex, int entryIndex)
			{
				this.techTreeIndex = techTreeIndex;
				this.entryIndex = entryIndex;
			}

			// Token: 0x04001BE2 RID: 7138
			public int techTreeIndex;

			// Token: 0x04001BE3 RID: 7139
			public int entryIndex;
		}
	}
}

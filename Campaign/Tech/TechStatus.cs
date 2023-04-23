using System;
using System.Collections.Generic;

namespace Campaign.Tech
{
	// Token: 0x02000417 RID: 1047
	[Serializable]
	public class TechStatus
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x000141C8 File Offset: 0x000123C8
		public bool HasTechId(string techId)
		{
			return string.IsNullOrEmpty(techId) || this.unlockedTechIds.Contains(techId);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000141E0 File Offset: 0x000123E0
		public void AddTechId(string techId)
		{
			this.unlockedTechIds.Add(techId);
		}

		// Token: 0x04001BD1 RID: 7121
		public HashSet<string> unlockedTechIds = new HashSet<string>();
	}
}

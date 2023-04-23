using System;
using UnityEngine;

namespace Campaign.AI
{
	// Token: 0x020003FF RID: 1023
	public struct ConquestActionDecisionData
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00013E4F File Offset: 0x0001204F
		public static ConquestActionDecisionData DontCare
		{
			get
			{
				return new ConquestActionDecisionData(0f);
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00013E5B File Offset: 0x0001205B
		public ConquestActionDecisionData(float score)
		{
			this.score = score;
			this.min = 1;
			this.max = 3;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00013E72 File Offset: 0x00012072
		public static ConquestActionDecisionData Join(ConquestActionDecisionData a, ConquestActionDecisionData b)
		{
			if (a.score > b.score)
			{
				a.score += b.score;
				return a;
			}
			b.score += a.score;
			return b;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00013EA7 File Offset: 0x000120A7
		public bool IsValid()
		{
			return this.max > 0;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000AA8AC File Offset: 0x000A8AAC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"score=",
				this.score.ToString(),
				", min=",
				this.min.ToString(),
				", max=",
				this.max.ToString()
			});
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00013EB2 File Offset: 0x000120B2
		public void ClampMinMax()
		{
			this.min = Mathf.Clamp(this.min, 1, 3);
			this.max = Mathf.Clamp(this.max, this.min, 3);
		}

		// Token: 0x04001B7C RID: 7036
		public float score;

		// Token: 0x04001B7D RID: 7037
		public int min;

		// Token: 0x04001B7E RID: 7038
		public int max;
	}
}

using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003E4 RID: 996
	[Serializable]
	public class LevelClickableConnection
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x00013493 File Offset: 0x00011693
		public LevelClickableConnection()
		{
			this.isEnabled = true;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000134A9 File Offset: 0x000116A9
		public void SetIndicator(GameObject indicator)
		{
			this.indicator = indicator;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000134B2 File Offset: 0x000116B2
		public LevelClickableConnection(LevelClickable a, LevelClickable b)
		{
			this.a = a;
			this.b = b;
			this.isEnabled = true;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000A7968 File Offset: 0x000A5B68
		public void UpdateIndicator()
		{
			bool active = this.isEnabled;
			this.indicator.SetActive(active);
		}

		// Token: 0x04001AB2 RID: 6834
		public LevelClickable a;

		// Token: 0x04001AB3 RID: 6835
		public LevelClickable b;

		// Token: 0x04001AB4 RID: 6836
		private GameObject indicator;

		// Token: 0x04001AB5 RID: 6837
		[NonSerialized]
		private bool isEnabled = true;
	}
}

using System;
using TMPro;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003F2 RID: 1010
	public class BattalionLabel : MonoBehaviour
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x00013AF0 File Offset: 0x00011CF0
		private void Awake()
		{
			this.text = base.GetComponentInChildren<TextMeshProUGUI>();
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000A8F54 File Offset: 0x000A7154
		public void UpdateLabel()
		{
			int battalionCountOfTeam = ConquestCampaign.instance.GetBattalionCountOfTeam(ConquestCampaign.instance.playerTeam);
			this.SetText(string.Format("{0} / {1}", battalionCountOfTeam, ConquestCampaign.instance.maxBattalions));
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00013AFE File Offset: 0x00011CFE
		internal void SetText(string text)
		{
			this.text.text = "<sprite index=0> " + text;
		}

		// Token: 0x04001B1C RID: 6940
		private TextMeshProUGUI text;
	}
}

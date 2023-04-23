using System;
using TMPro;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003F5 RID: 1013
	public class ResourceLabel : MonoBehaviour
	{
		// Token: 0x0600196F RID: 6511 RVA: 0x00013BDE File Offset: 0x00011DDE
		private void Awake()
		{
			this.text = base.GetComponentInChildren<TextMeshProUGUI>();
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000A91B0 File Offset: 0x000A73B0
		public void UpdateLabel()
		{
			ConquestTeamResources conquestTeamResources = CampaignBase.cachedState.resources[CampaignBase.instance.playerTeam];
			int num = (int)this.resourceType;
			this.SetText(conquestTeamResources.availableResources[num].ToString() + "  (+" + conquestTeamResources.resourceGain[num].ToString() + ")");
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00013BEC File Offset: 0x00011DEC
		internal void SetText(string text)
		{
			this.text.text = ConquestCampaign.GetResourceSpriteTag(this.resourceType) + " " + text;
		}

		// Token: 0x04001B2C RID: 6956
		public ResourceType resourceType;

		// Token: 0x04001B2D RID: 6957
		private TextMeshProUGUI text;
	}
}

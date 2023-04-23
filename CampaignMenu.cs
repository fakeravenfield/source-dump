using System;
using Campaign;
using Campaign.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020001DE RID: 478
public class CampaignMenu : MonoBehaviour
{
	// Token: 0x06000CC5 RID: 3269 RVA: 0x0007A164 File Offset: 0x00078364
	private void Start()
	{
		CampaignState campaignState = null;
		if (CampaignBase.cachedState == null)
		{
			if (!CampaignState.AutoSaveFileExists())
			{
				goto IL_25;
			}
			try
			{
				campaignState = CampaignState.DeserializeAutoSave();
				goto IL_25;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				goto IL_25;
			}
		}
		campaignState = CampaignBase.cachedState;
		IL_25:
		if (campaignState == null)
		{
			this.continueButton.interactable = false;
			this.continueButton.GetComponentInChildren<Text>().color = Color.gray;
			return;
		}
		CampaignBase.cachedState = campaignState;
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0000A734 File Offset: 0x00008934
	public void ContinueConquest()
	{
		this.LoadCampaignStartScene();
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0000A73C File Offset: 0x0000893C
	public void NewConquest()
	{
		CampaignBase.cachedState = null;
		this.LoadCampaignStartScene();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0000A74A File Offset: 0x0000894A
	private void LoadCampaignStartScene()
	{
		SceneManager.LoadScene("CommandRoom");
	}

	// Token: 0x04000DC5 RID: 3525
	public Button continueButton;
}

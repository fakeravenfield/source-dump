using System;
using Steamworks;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class FeaturedModsPanel : MonoBehaviour
{
	// Token: 0x06000CDB RID: 3291 RVA: 0x0007A6B4 File Offset: 0x000788B4
	public void Load()
	{
		if (this.loaded || !GameManager.IsConnectedToSteam())
		{
			return;
		}
		this.loaded = true;
		this.featuredQuery = GameManager.instance.steamworks.CreateUGCQuery(new SteamworksWrapper.DelOnUGCQueryDone(this.OnUGCQueryDone));
		GameManager.instance.steamworks.AddRequiredTagUGCQuery(this.featuredQuery, "Featured");
		GameManager.instance.steamworks.SendUGCQueryRequest(this.featuredQuery);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0007A728 File Offset: 0x00078928
	private void OnUGCQueryDone(bool ok, SteamworksWrapper.UGCQueryResult[] results)
	{
		ulong num = 0UL;
		if (ok)
		{
			for (int i = results.Length - 1; i >= 0; i--)
			{
				SteamworksWrapper.UGCQueryResult ugcqueryResult = results[i];
				num ^= ugcqueryResult.details.m_nPublishedFileId.m_PublishedFileId;
				if (!ugcqueryResult.details.m_bBanned)
				{
					this.AddModItem(ugcqueryResult);
				}
			}
		}
		this.intHash = (int)num;
		if (PlayerPrefs.HasKey("FeaturedModsHash"))
		{
			if (PlayerPrefs.GetInt("FeaturedModsHash") != this.intHash)
			{
				this.MarkNewMods();
				return;
			}
		}
		else
		{
			this.MarkNewMods();
		}
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0000A83B File Offset: 0x00008A3B
	private void MarkNewMods()
	{
		this.newLabel.SetActive(true);
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0000A849 File Offset: 0x00008A49
	private void OnEnable()
	{
		this.notConnectedPanel.SetActive(!GameManager.IsConnectedToSteam());
		if (this.intHash != -1)
		{
			PlayerPrefs.SetInt("FeaturedModsHash", this.intHash);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0007A7B0 File Offset: 0x000789B0
	private void AddModItem(SteamworksWrapper.UGCQueryResult result)
	{
		Transform child;
		if (this.lastVerticalPanel == null)
		{
			this.lastVerticalPanel = UnityEngine.Object.Instantiate<GameObject>(this.verticalPanelPrefab, this.panelParent);
			child = this.lastVerticalPanel.transform.GetChild(0);
		}
		else
		{
			child = this.lastVerticalPanel.transform.GetChild(1);
			this.lastVerticalPanel = null;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.modPanelPrefab, child).GetComponent<FeaturedModPanel>().Setup(result);
	}

	// Token: 0x04000DDB RID: 3547
	public Transform panelParent;

	// Token: 0x04000DDC RID: 3548
	public GameObject verticalPanelPrefab;

	// Token: 0x04000DDD RID: 3549
	public GameObject modPanelPrefab;

	// Token: 0x04000DDE RID: 3550
	public GameObject newLabel;

	// Token: 0x04000DDF RID: 3551
	public GameObject notConnectedPanel;

	// Token: 0x04000DE0 RID: 3552
	private bool loaded;

	// Token: 0x04000DE1 RID: 3553
	private UGCQueryHandle_t featuredQuery;

	// Token: 0x04000DE2 RID: 3554
	private GameObject lastVerticalPanel;

	// Token: 0x04000DE3 RID: 3555
	private int intHash = -1;
}

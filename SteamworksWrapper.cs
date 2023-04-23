using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class SteamworksWrapper
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x0000D618 File Offset: 0x0000B818
	public SteamworksWrapper()
	{
		this.queryUGCCallResultHandler = new Dictionary<UGCQueryHandle_t, CallResult<SteamUGCQueryCompleted_t>>();
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0000D636 File Offset: 0x0000B836
	public bool Initialize()
	{
		this.isInitialized = SteamAPI.Init();
		if (this.isInitialized)
		{
			this.RegisterCallbacks();
			this.input.Initialize();
		}
		this.SignalStateChanged();
		return this.isInitialized;
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0008AFEC File Offset: 0x000891EC
	public void RegisterCallbacks()
	{
		Debug.Log("Registering callbacks");
		this.createItemResult = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.OnCreateItemResult));
		this.submitItemResult = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.OnSubmitItemResult));
		this.itemInstalled = Callback<ItemInstalled_t>.Create(new Callback<ItemInstalled_t>.DispatchDelegate(this.OnItemInstalledInt));
		this.remotePublishedFileSubscribed = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(new Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate(this.OnRemotePublishedFileSubscribedInt));
		this.remotePublishedFileUnsubscribed = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(new Callback<RemoteStoragePublishedFileUnsubscribed_t>.DispatchDelegate(this.OnRemotePublishedFileUnsubscribedInt));
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0000D669 File Offset: 0x0000B869
	private void SignalStateChanged()
	{
		if (this.OnStateChanged != null)
		{
			this.OnStateChanged();
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0000D67E File Offset: 0x0000B87E
	public bool HasCurrentItem()
	{
		return this.currentItem != null;
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0000D689 File Offset: 0x0000B889
	public void Shutdown()
	{
		SteamAPI.Shutdown();
		this.isInitialized = false;
		this.SignalStateChanged();
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0000D69D File Offset: 0x0000B89D
	private void CheckInitialized()
	{
		if (!this.isInitialized)
		{
			throw new Exception("SteamworksWrapper is not initialized.");
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0000D6B2 File Offset: 0x0000B8B2
	public void Update()
	{
		if (this.isInitialized)
		{
			SteamAPI.RunCallbacks();
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
	public string Username()
	{
		this.CheckInitialized();
		return SteamFriends.GetPersonaName();
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0000D6CE File Offset: 0x0000B8CE
	private void OnItemInstalledInt(ItemInstalled_t p)
	{
		if (p.m_unAppID == SteamUtils.GetAppID() && this.OnItemInstalled != null)
		{
			this.OnItemInstalled(p.m_nPublishedFileId);
		}
		this.SignalStateChanged();
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0000D701 File Offset: 0x0000B901
	private void OnRemotePublishedFileSubscribedInt(RemoteStoragePublishedFileSubscribed_t p)
	{
		if (p.m_nAppID == SteamUtils.GetAppID() && this.OnRemotePublishedFileSubscribed != null)
		{
			this.OnRemotePublishedFileSubscribed(p.m_nPublishedFileId);
		}
		this.SignalStateChanged();
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0000D734 File Offset: 0x0000B934
	private void OnRemotePublishedFileUnsubscribedInt(RemoteStoragePublishedFileUnsubscribed_t p)
	{
		if (p.m_nAppID == SteamUtils.GetAppID() && this.OnRemotePublishedFileUnsubscribed != null)
		{
			this.OnRemotePublishedFileUnsubscribed(p.m_nPublishedFileId);
		}
		this.SignalStateChanged();
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0008B078 File Offset: 0x00089278
	public CSteamID[] GetAllFriends()
	{
		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
		if (friendCount < 0)
		{
			throw new Exception("Could not get friend count");
		}
		CSteamID[] array = new CSteamID[friendCount];
		for (int i = 0; i < friendCount; i++)
		{
			array[i] = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
		}
		return array;
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0000D767 File Offset: 0x0000B967
	public string GetSteamIdNickName(CSteamID id)
	{
		return SteamFriends.GetFriendPersonaName(id);
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x0008B0C0 File Offset: 0x000892C0
	public void CreateWorkshopItem()
	{
		this.CheckInitialized();
		Debug.Log("Creating item");
		SteamAPICall_t hAPICall = SteamUGC.CreateItem(SteamUtils.GetAppID(), EWorkshopFileType.k_EWorkshopFileTypeFirst);
		this.createItemResult.Set(hAPICall, null);
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0008B0F8 File Offset: 0x000892F8
	private void OnCreateItemResult(CreateItemResult_t p, bool ioFailure)
	{
		bool ok = true;
		this.lastResult = p.m_eResult;
		if (ioFailure)
		{
			this.SetError("Create Item failed, IO Failure!");
			ok = false;
		}
		else if (p.m_eResult != EResult.k_EResultOK)
		{
			this.SetError("Create Item failed, error: " + p.m_eResult.ToString());
			ok = false;
		}
		else
		{
			this.needsToAcceptWorkshopLegalAgreement = p.m_bUserNeedsToAcceptWorkshopLegalAgreement;
			this.currentItem = new SteamworksWrapper.WorkshopItem(p.m_nPublishedFileId);
			this.currentItem.title = "My new item";
		}
		if (this.OnCreateItemDone != null)
		{
			this.OnCreateItemDone(ok, p.m_nPublishedFileId);
		}
		this.SignalStateChanged();
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0000D76F File Offset: 0x0000B96F
	public void SetCurrentItemId(ulong id)
	{
		this.SetCurrentItem(new SteamworksWrapper.WorkshopItem(new PublishedFileId_t(id)));
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0000D782 File Offset: 0x0000B982
	public void SetCurrentItem(SteamworksWrapper.WorkshopItem item)
	{
		this.CheckInitialized();
		this.currentItem = item;
		this.SignalStateChanged();
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0008B1A4 File Offset: 0x000893A4
	public void SubmitCurrentItem(string contentPath, string changeNote)
	{
		this.CheckInitialized();
		if (!this.HasCurrentItem())
		{
			throw new Exception("Unable to submit item, there is no current item");
		}
		Debug.Log("Submitting item #" + this.currentItem.itemId.m_PublishedFileId.ToString() + " from path " + contentPath);
		UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this.currentItem.itemId);
		if (!string.IsNullOrEmpty(this.currentItem.title))
		{
			SteamUGC.SetItemTitle(ugcupdateHandle_t, this.currentItem.title);
		}
		if (!string.IsNullOrEmpty(this.currentItem.description))
		{
			SteamUGC.SetItemDescription(ugcupdateHandle_t, this.currentItem.description);
		}
		if (!string.IsNullOrEmpty(this.currentItem.previewImagePath))
		{
			SteamUGC.SetItemPreview(ugcupdateHandle_t, this.currentItem.previewImagePath);
		}
		if (this.currentItem.tags != null && this.currentItem.tags.Count > 0)
		{
			SteamUGC.SetItemTags(ugcupdateHandle_t, this.currentItem.tags);
		}
		if (this.currentItem.visibility != SteamworksWrapper.WorkshopItem.Visibility.NoChange)
		{
			ERemoteStoragePublishedFileVisibility eVisibility;
			if (this.currentItem.visibility == SteamworksWrapper.WorkshopItem.Visibility.Private)
			{
				eVisibility = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
			}
			else if (this.currentItem.visibility == SteamworksWrapper.WorkshopItem.Visibility.FriendsOnly)
			{
				eVisibility = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly;
			}
			else
			{
				eVisibility = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
			}
			SteamUGC.SetItemVisibility(ugcupdateHandle_t, eVisibility);
		}
		SteamUGC.SetItemContent(ugcupdateHandle_t, contentPath);
		SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, changeNote);
		this.submitItemResult.Set(hAPICall, null);
		this.isUpdatingItem = true;
		this.lastUpdateHandle = ugcupdateHandle_t;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0000D797 File Offset: 0x0000B997
	public bool IsUploadingItem()
	{
		return this.isUpdatingItem;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0008B310 File Offset: 0x00089510
	public bool IsPreparingContentUpload()
	{
		ulong num;
		ulong num2;
		EItemUpdateStatus itemUpdateProgress = SteamUGC.GetItemUpdateProgress(this.lastUpdateHandle, out num, out num2);
		return itemUpdateProgress == EItemUpdateStatus.k_EItemUpdateStatusPreparingContent || itemUpdateProgress == EItemUpdateStatus.k_EItemUpdateStatusPreparingConfig;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0008B338 File Offset: 0x00089538
	public float GetUploadProgress()
	{
		ulong num;
		ulong num2;
		SteamUGC.GetItemUpdateProgress(this.lastUpdateHandle, out num, out num2);
		if (num2 == 0UL)
		{
			return 0f;
		}
		return Mathf.Clamp01(num / 1000UL / (num2 / 1000UL));
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0008B378 File Offset: 0x00089578
	private void OnSubmitItemResult(SubmitItemUpdateResult_t p, bool ioFailure)
	{
		bool ok = true;
		this.lastResult = p.m_eResult;
		this.isUpdatingItem = false;
		if (ioFailure)
		{
			this.SetError("Submit Item failed, IO Failure!");
			ok = false;
		}
		else if (p.m_eResult != EResult.k_EResultOK)
		{
			this.SetError("Submit Item failed, error: " + p.m_eResult.ToString());
			ok = false;
		}
		else
		{
			this.needsToAcceptWorkshopLegalAgreement = p.m_bUserNeedsToAcceptWorkshopLegalAgreement;
			Debug.Log("Item was successfully uploaded!");
		}
		if (this.OnSubmitItemDone != null)
		{
			this.OnSubmitItemDone(ok);
		}
		this.SignalStateChanged();
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0008B40C File Offset: 0x0008960C
	public UGCQueryHandle_t CreateUGCQuery(SteamworksWrapper.DelOnUGCQueryDone ManagedCallback)
	{
		this.CheckInitialized();
		UGCQueryHandle_t ugcqueryHandle_t = SteamUGC.CreateQueryAllUGCRequest(EUGCQuery.k_EUGCQuery_RankedByVotesUp, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items_ReadyToUse, new AppId_t(636480U), new AppId_t(636480U), 1U);
		CallResult<SteamUGCQueryCompleted_t> value = CallResult<SteamUGCQueryCompleted_t>.Create(delegate(SteamUGCQueryCompleted_t p, bool ioFailure)
		{
			this.OnSteamUGCQueryCompleted(p, ioFailure, ManagedCallback);
		});
		this.queryUGCCallResultHandler.Add(ugcqueryHandle_t, value);
		return ugcqueryHandle_t;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0000D79F File Offset: 0x0000B99F
	public void AddRequiredTagUGCQuery(UGCQueryHandle_t query, string tag)
	{
		SteamUGC.AddRequiredTag(query, tag);
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x0008B470 File Offset: 0x00089670
	public void SendUGCQueryRequest(UGCQueryHandle_t query)
	{
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(query);
		this.queryUGCCallResultHandler[query].Set(hAPICall, null);
		this.queryUGCCallResultHandler.Remove(query);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0008B4A4 File Offset: 0x000896A4
	public void QuickQueryItemInfo(PublishedFileId_t[] fileIds, SteamworksWrapper.DelOnUGCQueryDone ManagedCallback)
	{
		this.CheckInitialized();
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(fileIds, (uint)fileIds.Length);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(delegate(SteamUGCQueryCompleted_t p, bool ioFailure)
		{
			this.OnSteamUGCQueryCompleted(p, ioFailure, ManagedCallback);
		});
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		callResult.Set(hAPICall, null);
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0008B4F4 File Offset: 0x000896F4
	private void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t p, bool ioFailure, SteamworksWrapper.DelOnUGCQueryDone ManagedCallback)
	{
		bool ok = true;
		this.lastResult = p.m_eResult;
		SteamworksWrapper.UGCQueryResult[] array = null;
		if (ioFailure)
		{
			this.SetError("Item query failed, IO Failure!");
			ok = false;
		}
		else if (p.m_eResult != EResult.k_EResultOK)
		{
			this.SetError("Item query failed, error: " + p.m_eResult.ToString());
			ok = false;
		}
		else
		{
			array = new SteamworksWrapper.UGCQueryResult[p.m_unNumResultsReturned];
			for (uint num = 0U; num < p.m_unNumResultsReturned; num += 1U)
			{
				SteamUGC.GetQueryUGCResult(p.m_handle, num, out array[(int)num].details);
				SteamUGC.GetQueryUGCPreviewURL(p.m_handle, num, out array[(int)num].previewImageURL, 1024U);
			}
		}
		ManagedCallback(ok, array);
		this.SignalStateChanged();
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
	public void SubscribeToItem(PublishedFileId_t fileId)
	{
		SteamUGC.SubscribeItem(fileId);
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0000D7B2 File Offset: 0x0000B9B2
	public uint GetItemState(PublishedFileId_t fileId)
	{
		return SteamUGC.GetItemState(fileId);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0008B5B8 File Offset: 0x000897B8
	public uint FetchSubscribedItems()
	{
		this.CheckInitialized();
		uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
		this.subscribedItems = new PublishedFileId_t[numSubscribedItems];
		return SteamUGC.GetSubscribedItems(this.subscribedItems, numSubscribedItems);
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0000D7BA File Offset: 0x0000B9BA
	public bool HasFetchedSubscribedItems()
	{
		this.CheckInitialized();
		return this.subscribedItems != null;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0008B5EC File Offset: 0x000897EC
	public bool AllSubscribedItemsAreInstalled()
	{
		this.CheckInitialized();
		if (!this.HasFetchedSubscribedItems())
		{
			throw new Exception("No subscribed items list has been fetched");
		}
		foreach (PublishedFileId_t itemId in this.subscribedItems)
		{
			if (!this.IsSubscribedItemInstalled(itemId))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0000D7CB File Offset: 0x0000B9CB
	public bool IsSubscribedItemInstalled(PublishedFileId_t itemId)
	{
		return (SteamUGC.GetItemState(itemId) & 4U) > 0U;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0008B63C File Offset: 0x0008983C
	public string[] GetSubscribedItemPaths()
	{
		this.CheckInitialized();
		if (!this.HasFetchedSubscribedItems())
		{
			throw new Exception("No subscribed items list has been fetched");
		}
		string[] array = new string[this.subscribedItems.Length];
		for (int i = 0; i < this.subscribedItems.Length; i++)
		{
			ulong num;
			string text;
			uint num2;
			SteamUGC.GetItemInstallInfo(this.subscribedItems[i], out num, out text, 0U, out num2);
			array[i] = text;
		}
		return array;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0008B6A8 File Offset: 0x000898A8
	public string GetSubscribedItemPath(PublishedFileId_t id)
	{
		uint cchFolderSize = 4096U;
		ulong num;
		string result;
		uint num2;
		SteamUGC.GetItemInstallInfo(id, out num, out result, cchFolderSize, out num2);
		return result;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
	public string GetSteamNick()
	{
		return SteamFriends.GetPersonaName();
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0000D7DF File Offset: 0x0000B9DF
	public ulong GetSteamId()
	{
		return SteamUser.GetSteamID().m_SteamID;
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0000D7EB File Offset: 0x0000B9EB
	public void DropCurrentItem()
	{
		this.currentItem = null;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
	private void SetError(string error)
	{
		Debug.LogError(error);
		this.errorMessage = error;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0000D803 File Offset: 0x0000BA03
	public void OpenCommunityFilePage(PublishedFileId_t itemId)
	{
		this.OpenUrl("steam://url/CommunityFilePage/" + itemId.m_PublishedFileId.ToString(), true);
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0000D822 File Offset: 0x0000BA22
	public void OpenUrl(string url, bool inSteamOverlay = true)
	{
		if (inSteamOverlay)
		{
			this.CheckInitialized();
			SteamFriends.ActivateGameOverlayToWebPage(url, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
			return;
		}
		Application.OpenURL(url);
	}

	// Token: 0x0400123E RID: 4670
	public const uint APP_ID = 636480U;

	// Token: 0x0400123F RID: 4671
	public const string WORKSHOP_TERMS_OF_SERVICE_URL = "http://steamcommunity.com/sharedfiles/workshoplegalagreement";

	// Token: 0x04001240 RID: 4672
	public const string COMMUNITY_FILE_PAGE_URL = "steam://url/CommunityFilePage/";

	// Token: 0x04001241 RID: 4673
	public SteamInputWrapper input = new SteamInputWrapper();

	// Token: 0x04001242 RID: 4674
	public bool isInitialized;

	// Token: 0x04001243 RID: 4675
	protected CallResult<CreateItemResult_t> createItemResult;

	// Token: 0x04001244 RID: 4676
	protected CallResult<SubmitItemUpdateResult_t> submitItemResult;

	// Token: 0x04001245 RID: 4677
	protected Callback<ItemInstalled_t> itemInstalled;

	// Token: 0x04001246 RID: 4678
	protected Callback<RemoteStoragePublishedFileSubscribed_t> remotePublishedFileSubscribed;

	// Token: 0x04001247 RID: 4679
	protected Callback<RemoteStoragePublishedFileUnsubscribed_t> remotePublishedFileUnsubscribed;

	// Token: 0x04001248 RID: 4680
	public SteamworksWrapper.DelOnStateChanged OnStateChanged;

	// Token: 0x04001249 RID: 4681
	public SteamworksWrapper.DelOnCreateItemDone OnCreateItemDone;

	// Token: 0x0400124A RID: 4682
	public SteamworksWrapper.DelOnSubmitItemDone OnSubmitItemDone;

	// Token: 0x0400124B RID: 4683
	public SteamworksWrapper.DelOnItemInstalled OnItemInstalled;

	// Token: 0x0400124C RID: 4684
	public SteamworksWrapper.DelOnRemotePublishedFileSubscribed OnRemotePublishedFileSubscribed;

	// Token: 0x0400124D RID: 4685
	public SteamworksWrapper.DelOnRemotePublishedFileUnsubscribed OnRemotePublishedFileUnsubscribed;

	// Token: 0x0400124E RID: 4686
	public bool needsToAcceptWorkshopLegalAgreement;

	// Token: 0x0400124F RID: 4687
	public SteamworksWrapper.WorkshopItem currentItem;

	// Token: 0x04001250 RID: 4688
	public EResult lastResult;

	// Token: 0x04001251 RID: 4689
	public string errorMessage;

	// Token: 0x04001252 RID: 4690
	public PublishedFileId_t[] subscribedItems;

	// Token: 0x04001253 RID: 4691
	private bool isUpdatingItem;

	// Token: 0x04001254 RID: 4692
	private UGCUpdateHandle_t lastUpdateHandle;

	// Token: 0x04001255 RID: 4693
	private Dictionary<UGCQueryHandle_t, CallResult<SteamUGCQueryCompleted_t>> queryUGCCallResultHandler;

	// Token: 0x02000269 RID: 617
	// (Invoke) Token: 0x060010F4 RID: 4340
	public delegate void DelOnStateChanged();

	// Token: 0x0200026A RID: 618
	// (Invoke) Token: 0x060010F8 RID: 4344
	public delegate void DelOnCreateItemDone(bool ok, PublishedFileId_t itemId);

	// Token: 0x0200026B RID: 619
	// (Invoke) Token: 0x060010FC RID: 4348
	public delegate void DelOnSubmitItemDone(bool ok);

	// Token: 0x0200026C RID: 620
	// (Invoke) Token: 0x06001100 RID: 4352
	public delegate void DelOnUGCQueryDone(bool ok, SteamworksWrapper.UGCQueryResult[] details);

	// Token: 0x0200026D RID: 621
	// (Invoke) Token: 0x06001104 RID: 4356
	public delegate void DelOnItemInstalled(PublishedFileId_t itemId);

	// Token: 0x0200026E RID: 622
	// (Invoke) Token: 0x06001108 RID: 4360
	public delegate void DelOnRemotePublishedFileSubscribed(PublishedFileId_t itemId);

	// Token: 0x0200026F RID: 623
	// (Invoke) Token: 0x0600110C RID: 4364
	public delegate void DelOnRemotePublishedFileUnsubscribed(PublishedFileId_t itemId);

	// Token: 0x02000270 RID: 624
	public class WorkshopItem
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x0000D83B File Offset: 0x0000BA3B
		public WorkshopItem(PublishedFileId_t itemId)
		{
			this.itemId = itemId;
			this.visibility = SteamworksWrapper.WorkshopItem.Visibility.NoChange;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0008B6CC File Offset: 0x000898CC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"WokshopItem #",
				this.itemId.m_PublishedFileId.ToString(),
				": ",
				this.title,
				", ",
				this.description,
				", "
			});
		}

		// Token: 0x04001256 RID: 4694
		public PublishedFileId_t itemId;

		// Token: 0x04001257 RID: 4695
		public string title;

		// Token: 0x04001258 RID: 4696
		public string description;

		// Token: 0x04001259 RID: 4697
		public string previewImagePath;

		// Token: 0x0400125A RID: 4698
		public List<string> tags;

		// Token: 0x0400125B RID: 4699
		public SteamworksWrapper.WorkshopItem.Visibility visibility;

		// Token: 0x02000271 RID: 625
		public enum Visibility
		{
			// Token: 0x0400125D RID: 4701
			NoChange,
			// Token: 0x0400125E RID: 4702
			Private,
			// Token: 0x0400125F RID: 4703
			FriendsOnly,
			// Token: 0x04001260 RID: 4704
			Public
		}
	}

	// Token: 0x02000272 RID: 626
	public struct UGCQueryResult
	{
		// Token: 0x04001261 RID: 4705
		public SteamUGCDetails_t details;

		// Token: 0x04001262 RID: 4706
		public string previewImageURL;
	}
}

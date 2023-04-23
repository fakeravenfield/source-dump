using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Lua;
using MapEditor;
using Steamworks;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;

// Token: 0x02000219 RID: 537
public class ModManager : MonoBehaviour
{
	// Token: 0x06000E4E RID: 3662 RVA: 0x0000B82C File Offset: 0x00009A2C
	public static IEnumerable<GameObject> AllVehiclePrefabs()
	{
		foreach (VehicleSpawner.VehicleSpawnType key in VehicleSpawner.ALL_VEHICLE_TYPES)
		{
			List<GameObject> list = ModManager.instance.vehiclePrefabs[key];
			foreach (GameObject gameObject in list)
			{
				yield return gameObject;
			}
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		}
		VehicleSpawner.VehicleSpawnType[] array = null;
		yield break;
		yield break;
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0000B835 File Offset: 0x00009A35
	public static IEnumerable<GameObject> AllTurretPrefabs()
	{
		foreach (TurretSpawner.TurretSpawnType key in TurretSpawner.ALL_TURRET_TYPES)
		{
			List<GameObject> list = ModManager.instance.turretPrefabs[key];
			foreach (GameObject gameObject in list)
			{
				yield return gameObject;
			}
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		}
		TurretSpawner.TurretSpawnType[] array = null;
		yield break;
		yield break;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0000B83E File Offset: 0x00009A3E
	public static string ModStagingPath()
	{
		if (!string.IsNullOrEmpty(ModManager.instance.modStagingPathOverride))
		{
			return ModManager.instance.modStagingPathOverride;
		}
		return Application.dataPath + "/Mods";
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0007F468 File Offset: 0x0007D668
	public static GameObject GetVehiclePrefabByName(string name, ModInformation prioritizedMod = null)
	{
		GameObject result = null;
		string text = name.ToLowerInvariant();
		if (!ModManager.instance.vehiclePrefabName.ContainsKey(text))
		{
			Debug.Log("Could not find prefab of name " + text);
			return null;
		}
		foreach (GameObject gameObject in ModManager.instance.vehiclePrefabName[text])
		{
			if (ModManager.GetVehiclePrefabSourceMod(gameObject) == prioritizedMod)
			{
				return gameObject;
			}
			result = gameObject;
		}
		return result;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0007F500 File Offset: 0x0007D700
	public static GameObject GetTurretPrefabByName(string name, ModInformation prioritizedMod = null)
	{
		GameObject result = null;
		string text = name.ToLowerInvariant();
		if (!ModManager.instance.turretPrefabName.ContainsKey(text))
		{
			Debug.Log("Could not find prefab of name " + text);
			return null;
		}
		foreach (GameObject gameObject in ModManager.instance.turretPrefabName[text])
		{
			if (ModManager.GetVehiclePrefabSourceMod(gameObject) == prioritizedMod)
			{
				return gameObject;
			}
			result = gameObject;
		}
		return result;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0007F598 File Offset: 0x0007D798
	public static ModInformation GetVehiclePrefabSourceMod(GameObject prefab)
	{
		IVehicleContentProvider vehicleContentProvider = ModManager.GetVehiclePrefabProviders(prefab).FirstOrDefault<IVehicleContentProvider>();
		ModInformation modInformation = (vehicleContentProvider != null) ? vehicleContentProvider.GetSourceMod() : null;
		if (modInformation == null)
		{
			return ModInformation.OfficialContent;
		}
		return modInformation;
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0000B86B File Offset: 0x00009A6B
	public static List<IVehicleContentProvider> GetVehiclePrefabProviders(GameObject prefab)
	{
		if (ModManager.instance.vehiclePrefabContentProviders.ContainsKey(prefab))
		{
			return ModManager.instance.vehiclePrefabContentProviders[prefab];
		}
		return new List<IVehicleContentProvider>(0);
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0000B896 File Offset: 0x00009A96
	private static void UpdatePathOverrideFlag()
	{
		if (!ModManager.ModStagingPath().ToLowerInvariant().EndsWith("steamapps/common/ravenfield/ravenfield_data/mods"))
		{
			UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Mods/OverridePathProvider") as GameObject, GameManager.instance.transform);
		}
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0000B8D0 File Offset: 0x00009AD0
	private void Awake()
	{
		ModManager.instance = this;
		ModManager.engineVersion = new ModManager.EngineVersionInfo(Application.unityVersion);
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0007F5C8 File Offset: 0x0007D7C8
	private void LoadDummyAsset()
	{
		string text = Application.streamingAssetsPath + "/dummy_win.assetbundle";
		Debug.Log("Loading dummy asset at " + text);
		try
		{
			AssetBundle.LoadFromFile(text).Unload(false);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		Debug.Log("Dummy asset loading done");
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0007F624 File Offset: 0x0007D824
	public void OnGameManagerStart()
	{
		if (GameManager.IsConnectedToSteam())
		{
			GameManager.instance.steamworks.OnItemInstalled = new SteamworksWrapper.DelOnItemInstalled(this.OnItemInstalled);
			GameManager.instance.steamworks.OnRemotePublishedFileSubscribed = new SteamworksWrapper.DelOnRemotePublishedFileSubscribed(this.OnRemotePublishedFileSubscribed);
			GameManager.instance.steamworks.OnRemotePublishedFileUnsubscribed = new SteamworksWrapper.DelOnRemotePublishedFileUnsubscribed(this.OnRemotePublishedFileUnsubscribed);
		}
		ModManager.UpdatePathOverrideFlag();
		foreach (MutatorEntry mutatorEntry in this.builtInMutators)
		{
			mutatorEntry.sourceMod = ModInformation.OfficialContent;
			this.SetupMutatorPrefabs(mutatorEntry, ModContentInformation.VanillaContent);
		}
		ActorSkin[] array = this.builtInActorSkins;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].sourceMod = ModInformation.OfficialContent;
		}
		this.SetupVanillaVehicleNames();
		this.LoadDummyAsset();
		this.ReloadMods();
		if (GameManager.IsInMainMenu())
		{
			if (GameManager.IsTestingContentMod())
			{
				Debug.Log("Mod manager: Testing content mod");
				this.contentHasFinishedLoading = true;
				return;
			}
			if (this.noContentMods)
			{
				Debug.Log("Mod manager: No content mods");
				this.ClearLoadedContentBundles();
				this.FinalizeLoadedModContent();
				return;
			}
			Debug.Log("Mod manager: Load mods normally");
			this.ReloadModContent();
		}
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x0007F768 File Offset: 0x0007D968
	private void OnRemotePublishedFileSubscribed(PublishedFileId_t itemId)
	{
		if (!this.ShouldAutoLoadContent())
		{
			return;
		}
		string str = "Subscribed to item #";
		PublishedFileId_t publishedFileId_t = itemId;
		Debug.Log(str + publishedFileId_t.ToString());
		ModInformation modInformation = this.AddWorkshopItemAsMod(itemId);
		ModPage modPage = UnityEngine.Object.FindObjectOfType<ModPage>();
		if (modPage != null)
		{
			modPage.AddPanelForMod(modInformation);
		}
		if (modInformation.HasLoadedContent())
		{
			this.ContentChanged();
		}
		GameManager.instance.steamworks.QuickQueryItemInfo(new PublishedFileId_t[]
		{
			itemId
		}, new SteamworksWrapper.DelOnUGCQueryDone(this.OnItemInfoQueryDone));
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0000B8E7 File Offset: 0x00009AE7
	private void OnRemotePublishedFileUnsubscribed(PublishedFileId_t itemId)
	{
		if (!this.ShouldAutoLoadContent())
		{
			return;
		}
		Debug.Log("Unsubscribed to an item, reload all mods");
		this.ReloadMods();
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0007F7F4 File Offset: 0x0007D9F4
	private void OnItemInfoQueryDone(bool ok, SteamworksWrapper.UGCQueryResult[] results)
	{
		if (!ok)
		{
			return;
		}
		foreach (SteamworksWrapper.UGCQueryResult ugcqueryResult in results)
		{
			if (this.modWithitemId.ContainsKey(ugcqueryResult.details.m_nPublishedFileId))
			{
				ModInformation modInformation = this.modWithitemId[ugcqueryResult.details.m_nPublishedFileId];
				SteamUGCDetails_t details = ugcqueryResult.details;
				string rgchTitle = details.m_rgchTitle;
				details = ugcqueryResult.details;
				modInformation.UpdateInfo(rgchTitle, details.m_rgchDescription);
			}
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0007F86C File Offset: 0x0007DA6C
	private void OnItemInstalled(PublishedFileId_t id)
	{
		Debug.Log("Workshop item was installed: #" + id.m_PublishedFileId.ToString());
		if (!this.ShouldAutoLoadContent() && this.mods == null)
		{
			return;
		}
		foreach (ModInformation modInformation in this.mods)
		{
			if (modInformation.IsWorkshopItem() && modInformation.workshopItemId == id)
			{
				modInformation.ItemDownloadComplete();
				Debug.Log(string.Concat(new string[]
				{
					"Autoloading mod ",
					modInformation.title,
					" (item #",
					modInformation.workshopItemId.m_PublishedFileId.ToString(),
					")"
				}));
				if (modInformation.TryLoadContent())
				{
					this.ContentChanged();
				}
				break;
			}
		}
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0000B903 File Offset: 0x00009B03
	private bool ShouldAutoLoadContent()
	{
		return !GameManager.IsIngame();
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0007F958 File Offset: 0x0007DB58
	public bool ReloadMods()
	{
		base.StopAllCoroutines();
		this.SetupModInformationList();
		using (List<ModInformation>.Enumerator enumerator = this.mods.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.HasLoadedContent())
				{
					return false;
				}
			}
		}
		ModPage modPage = UnityEngine.Object.FindObjectOfType<ModPage>();
		if (modPage != null)
		{
			modPage.ReloadPanels();
		}
		this.ContentChanged();
		return true;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0007F9D8 File Offset: 0x0007DBD8
	public void SetupModInformationList()
	{
		this.mods = new List<ModInformation>();
		this.modWithitemId = new Dictionary<PublishedFileId_t, ModInformation>();
		if (Directory.Exists(MapDescriptor.DATA_PATH))
		{
			ModInformation modInformation = new ModInformation(MapDescriptor.DATA_PATH);
			modInformation.hideInModList = true;
			this.mods.Add(modInformation);
		}
		string text = ModManager.ModStagingPath();
		if (Directory.Exists(text))
		{
			string[] directories = Directory.GetDirectories(text);
			foreach (string path in directories)
			{
				this.mods.Add(new ModInformation(path));
			}
			ScriptConsole.instance.LogInfo("Found {0} mod(s) at staging path: {1}", new object[]
			{
				directories.Length,
				text
			});
		}
		else
		{
			ModManager.HandleModException(new Exception(string.Format("Could not open mod staging directory at {0}", text)));
		}
		if (GameManager.IsConnectedToSteam() && !this.noWorkshopMods)
		{
			GameManager.instance.steamworks.FetchSubscribedItems();
			foreach (PublishedFileId_t itemId in GameManager.instance.steamworks.subscribedItems)
			{
				this.AddWorkshopItemAsMod(itemId);
			}
			this.QueryWorkshopItemInformation();
		}
		this.mods.Sort();
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0007FB0C File Offset: 0x0007DD0C
	private ModInformation AddWorkshopItemAsMod(PublishedFileId_t itemId)
	{
		ModInformation modInformation = new ModInformation(itemId);
		this.modWithitemId.Add(modInformation.workshopItemId, modInformation);
		this.mods.Add(modInformation);
		return modInformation;
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x0007FB40 File Offset: 0x0007DD40
	public void QueryWorkshopItemInformation()
	{
		List<PublishedFileId_t> list = new List<PublishedFileId_t>();
		if (this.mods != null)
		{
			foreach (ModInformation modInformation in this.mods)
			{
				if (modInformation.IsWorkshopItem())
				{
					list.Add(modInformation.workshopItemId);
				}
			}
		}
		GameManager.instance.steamworks.QuickQueryItemInfo(list.ToArray(), new SteamworksWrapper.DelOnUGCQueryDone(this.OnItemInfoQueryDone));
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0000B90D File Offset: 0x00009B0D
	public void ContentChanged()
	{
		this.contentVersion += 1U;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0007FBD0 File Offset: 0x0007DDD0
	public List<ModInformation> GetActiveMods()
	{
		List<ModInformation> list = new List<ModInformation>();
		foreach (ModInformation modInformation in this.mods)
		{
			if (modInformation.IsActive())
			{
				list.Add(modInformation);
			}
		}
		return list;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0000B91D File Offset: 0x00009B1D
	public static void HandleModException(Exception e)
	{
		if (GameManager.IsTestingContentMod())
		{
			ScriptConsole.instance.LogException(e);
		}
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0000B931 File Offset: 0x00009B31
	public static void LoadTextureFile(FileInfo file, ModInformation targetMod)
	{
		ModManager.instance.StartCoroutine(ModManager.instance.LoadTextureFileRoutine(file, targetMod));
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0000B94A File Offset: 0x00009B4A
	private IEnumerator LoadTextureFileRoutine(FileInfo file, ModInformation targetMod)
	{
		string text = "file://" + file.FullName;
		Debug.Log("Loading image file into texture: url=" + text);
		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(text))
		{
			yield return request.SendWebRequest();
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(request.error);
			}
			else
			{
				targetMod.iconTexture = DownloadHandlerTexture.GetContent(request);
			}
		}
		UnityWebRequest request = null;
		yield break;
		yield break;
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0007FC34 File Offset: 0x0007DE34
	public void ReloadModContent()
	{
		this.ClearLoadedContentBundles();
		this.contentHasFinishedLoading = false;
		VehicleSwitch.requireReload = true;
		int nWorkers = Mathf.Clamp(SystemInfo.processorCount, 1, 4);
		LoadContentDialog.ShowBarPanel(nWorkers);
		base.StartCoroutine(this.LoadAllModContent(nWorkers));
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0000B960 File Offset: 0x00009B60
	private void ClearLoadedContentBundles()
	{
		this.textAssetSources = new List<ModManager.TextAssetSource>();
		this.loadedMutators = new List<MutatorEntry>(this.builtInMutators);
		this.vehicleContentProviders = new List<IVehicleContentProvider>();
		this.ClearRegisteredPrefabs();
		this.onlyOfficialContent = true;
		this.SetupVanillaVehicleNames();
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0007FC78 File Offset: 0x0007DE78
	private void SetupVanillaVehicleNames()
	{
		this.vehiclePrefabName = new Dictionary<string, List<GameObject>>();
		this.turretPrefabName = new Dictionary<string, List<GameObject>>();
		foreach (GameObject gameObject in ActorManager.instance.defaultVehiclePrefabs)
		{
			this.AddVehicleNamePrefabLookup(this.vehiclePrefabName, gameObject.GetComponent<Vehicle>());
		}
		foreach (GameObject gameObject2 in ActorManager.instance.defaultTurretPrefabs)
		{
			this.AddVehicleNamePrefabLookup(this.turretPrefabName, gameObject2.GetComponent<Vehicle>());
		}
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0007FCFC File Offset: 0x0007DEFC
	private void AddVehicleNamePrefabLookup(Dictionary<string, List<GameObject>> lookupDictionary, Vehicle vehicle)
	{
		string key = vehicle.name.ToLowerInvariant();
		if (!lookupDictionary.ContainsKey(key))
		{
			lookupDictionary.Add(key, new List<GameObject>());
		}
		lookupDictionary[key].Add(vehicle.gameObject);
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0007FD3C File Offset: 0x0007DF3C
	private void ClearRegisteredPrefabs()
	{
		this.vehiclePrefabs = new Dictionary<VehicleSpawner.VehicleSpawnType, List<GameObject>>();
		foreach (object obj in Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType)))
		{
			VehicleSpawner.VehicleSpawnType key = (VehicleSpawner.VehicleSpawnType)obj;
			this.vehiclePrefabs.Add(key, new List<GameObject>());
		}
		this.turretPrefabs = new Dictionary<TurretSpawner.TurretSpawnType, List<GameObject>>();
		foreach (object obj2 in Enum.GetValues(typeof(TurretSpawner.TurretSpawnType)))
		{
			TurretSpawner.TurretSpawnType key2 = (TurretSpawner.TurretSpawnType)obj2;
			this.turretPrefabs.Add(key2, new List<GameObject>());
		}
		this.vehiclePrefabContentProviders = new Dictionary<GameObject, List<IVehicleContentProvider>>();
		this.actorSkins = new List<ActorSkin>(this.builtInActorSkins);
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x0000B99C File Offset: 0x00009B9C
	public static bool RequestDiskRead()
	{
		if (!ModManager.instance.workerIsReadingDisk)
		{
			ModManager.instance.workerIsReadingDisk = true;
			return true;
		}
		return false;
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x0000B9B8 File Offset: 0x00009BB8
	public static void ReleaseDiskRead()
	{
		ModManager.instance.workerIsReadingDisk = false;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0000B9C5 File Offset: 0x00009BC5
	public static bool RequestBundleLoad()
	{
		if (!ModManager.instance.workerIsLoadingBundle)
		{
			ModManager.instance.workerIsLoadingBundle = true;
			ModManager.instance.beforeBundleLoadTextAssets = Resources.FindObjectsOfTypeAll<TextAsset>();
			return true;
		}
		return false;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0000B9F0 File Offset: 0x00009BF0
	public static void ReleaseBundleLoad()
	{
		ModManager.instance.workerIsLoadingBundle = false;
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0007FE38 File Offset: 0x0007E038
	public static void ReleaseBundleLoadAndAssignRSBundleID(int bundleId)
	{
		ModManager.ReleaseBundleLoad();
		if (bundleId == -1)
		{
			return;
		}
		foreach (TextAsset textAsset in Resources.FindObjectsOfTypeAll<TextAsset>().Except(ModManager.instance.beforeBundleLoadTextAssets))
		{
			GameManager.DebugVerbose("Loaded text asset: {0}, assigning bundleID: {1}", new object[]
			{
				textAsset.name,
				bundleId
			});
			ModManager.instance.textAssetSources.Add(new ModManager.TextAssetSource
			{
				textAsset = new WeakReference(textAsset),
				bundleId = bundleId
			});
		}
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0000B9FD File Offset: 0x00009BFD
	private IEnumerator LoadAllModContent(int nWorkers)
	{
		WeaponManager.ClearContentModWeapons();
		this.ClearRegisteredPrefabs();
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		List<ModInformation> activeMods = this.GetActiveMods();
		List<FileInfo> contentBundleFiles = new List<FileInfo>();
		Dictionary<FileInfo, ModInformation> sourceMod = new Dictionary<FileInfo, ModInformation>();
		this.processedContentBytes = 0L;
		long totalContentBytes = 0L;
		foreach (ModInformation modInformation in activeMods)
		{
			foreach (FileInfo fileInfo in modInformation.content.GetGameContent())
			{
				contentBundleFiles.Add(fileInfo);
				sourceMod.Add(fileInfo, modInformation);
				totalContentBytes += fileInfo.Length;
			}
		}
		contentBundleFiles.Sort((FileInfo a, FileInfo b) => b.Length.CompareTo(a.Length));
		Queue<FileInfo> remainingFiles = new Queue<FileInfo>(contentBundleFiles);
		LoadModWorker[] workers = new LoadModWorker[nWorkers];
		for (int l = 0; l < nWorkers; l++)
		{
			LoadModWorker loadModWorker = new LoadModWorker
			{
				state = LoadModWorker.State.Idle
			};
			workers[l] = loadModWorker;
			LoadContentDialog.RegisterWorker(l, loadModWorker);
		}
		Debug.LogFormat("Loading mod content using {0} worker threads, mod folder count: {1}, file count: {2}", new object[]
		{
			nWorkers,
			activeMods.Count,
			contentBundleFiles.Count
		});
		ModManager.ReleaseDiskRead();
		ModManager.ReleaseBundleLoad();
		while (remainingFiles.Count > 0)
		{
			for (int j = 0; j < nWorkers; j++)
			{
				if (workers[j].IsReadyToAcceptNewItem())
				{
					FileInfo fileInfo2 = remainingFiles.Dequeue();
					ModInformation modInformation2 = sourceMod[fileInfo2];
					GameManager.DebugVerbose("Dispatching LoadModWorker #{0}: {1}:{2}", new object[]
					{
						j,
						modInformation2,
						fileInfo2
					});
					base.StartCoroutine(workers[j].LoadContentFileAsync(fileInfo2, modInformation2));
					if (remainingFiles.Count == 0)
					{
						break;
					}
				}
			}
			int num = 0;
			for (int k = 0; k < nWorkers; k++)
			{
				if (workers[k].IsIdle())
				{
					num++;
				}
			}
			float num2 = (float)this.processedContentBytes / (float)totalContentBytes;
			LoadContentDialog.SetProgress(0.01f + num2 * 0.99f);
			yield return 0;
		}
		int i;
		Func<bool> <>9__1;
		int i2;
		for (i = 0; i < nWorkers; i = i2 + 1)
		{
			Func<bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = (() => workers[i].IsReadyToAcceptNewItem()));
			}
			yield return new WaitUntil(predicate);
			i2 = i;
		}
		double totalSeconds = stopwatch.Elapsed.TotalSeconds;
		ScriptConsole.instance.LogInfo("Mod Content loaded in {0:0.0}s using {1} threads", new object[]
		{
			totalSeconds,
			nWorkers
		});
		GC.Collect();
		int num3 = (int)(Profiler.GetTotalAllocatedMemoryLong() / 1000000L);
		int num4 = (int)(Profiler.GetTotalReservedMemoryLong() / 1000000L);
		int num5 = (int)(totalContentBytes / 1000000L);
		int systemMemorySize = SystemInfo.systemMemorySize;
		float num6 = (float)num3 / (float)num4;
		int systemMemorySize2 = SystemInfo.systemMemorySize;
		Debug.LogFormat("Loaded {0} mod files, total file size on disk: {1} MB.", new object[]
		{
			contentBundleFiles.Count,
			num5,
			SystemInfo.systemMemorySize
		});
		Debug.LogFormat("Memory usage: {0}/{1} MB ({2:0.0}%). System memory size: {3} MB", new object[]
		{
			num3,
			num4,
			num6 * 100f,
			systemMemorySize
		});
		if (systemMemorySize > 0)
		{
			int num7 = systemMemorySize - 1000;
			float num8 = (float)num3 / (float)num7;
			if (contentBundleFiles.Count > 0 && num8 > 0.9f)
			{
				LoadContentDialog.AppendError(string.Format("You are loading a lot of heavy mods at one time, so game performance may suffer. You can unsubscribe from a few mods to solve this issue. Memory in use: {0} MB, Available system memory: {1} MB", num3, systemMemorySize));
			}
		}
		int num9 = 0;
		foreach (ModInformation modInformation3 in activeMods)
		{
			num9 = Mathf.Max(modInformation3.requiredGameVersion, num9);
		}
		bool flag = !GameManager.instance.isBeta && num9 > GameManager.instance.buildNumber;
		LoadContentDialog.instance.showVersionWarning = flag;
		if (LoadContentDialog.hasError)
		{
			LoadContentDialog.ShowErrorPanel();
		}
		else if (flag)
		{
			LoadContentDialog.ShowVersionPanel();
		}
		else
		{
			LoadContentDialog.Hide();
		}
		this.FinalizeLoadedModContent();
		yield break;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0000BA13 File Offset: 0x00009C13
	public void ClearContentModData()
	{
		WeaponManager.ClearContentModWeapons();
		this.ClearLoadedContentBundles();
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0007FEE8 File Offset: 0x0007E0E8
	public ModInformation LoadSingleModContentBundle(string filePath)
	{
		ModInformation modInformation = new ModInformation(Path.GetDirectoryName(filePath));
		FileInfo bundleFile = new FileInfo(filePath);
		new LoadModWorker().LoadContentFile(bundleFile, modInformation);
		return modInformation;
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0000BA20 File Offset: 0x00009C20
	public void FinalizeLoadedModContent()
	{
		WeaponManager.instance.CalculateWeaponNameHashes();
		this.SortContent();
		this.contentHasFinishedLoading = true;
		GameManager.instance.OnAllContentLoaded();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0007FF18 File Offset: 0x0007E118
	private void SortContent()
	{
		WeaponManager.instance.SortWeaponEntries();
		foreach (MutatorEntry mutatorEntry in this.loadedMutators)
		{
			if (mutatorEntry.sourceMod == null)
			{
				Debug.LogErrorFormat("Could not determine source mod of mutator {0}", new object[]
				{
					mutatorEntry.name
				});
				mutatorEntry.sourceMod = ModInformation.Unknown;
			}
		}
		this.loadedMutators.Sort(new Comparison<MutatorEntry>(this.SortMutators));
		foreach (ActorSkin actorSkin in this.actorSkins)
		{
			if (actorSkin.sourceMod == null)
			{
				Debug.LogErrorFormat("Could not determine source mod of skin {0}", new object[]
				{
					actorSkin.name
				});
				actorSkin.sourceMod = ModInformation.Unknown;
			}
		}
		this.actorSkins.Sort(new Comparison<ActorSkin>(this.SortSkins));
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0000BA43 File Offset: 0x00009C43
	private int SortMutators(MutatorEntry a, MutatorEntry b)
	{
		return a.sourceMod.CompareTo(b.sourceMod);
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0000BA56 File Offset: 0x00009C56
	private int SortSkins(ActorSkin a, ActorSkin b)
	{
		return a.sourceMod.CompareTo(b.sourceMod);
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00080030 File Offset: 0x0007E230
	public static ModManager.EngineVersionInfo ExtractBundleEditorVersion(string assetBundlePath)
	{
		FileStream fileStream = File.OpenRead(assetBundlePath);
		string versionString = "";
		try
		{
			fileStream.Seek(18L, SeekOrigin.Begin);
			int count = fileStream.Read(ModManager.bundleHeaderBuffer, 0, 16);
			versionString = Encoding.ASCII.GetString(ModManager.bundleHeaderBuffer, 0, count);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		finally
		{
			fileStream.Close();
		}
		return new ModManager.EngineVersionInfo(versionString);
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x0000BA69 File Offset: 0x00009C69
	public static void OnAsyncWorkerJobDone(FileInfo file, LoadModWorker.State state)
	{
		ModManager.instance.processedContentBytes += file.Length;
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x000800A8 File Offset: 0x0007E2A8
	public static void OnWorkerContentObjectReady(ModContentInformation contentInfo)
	{
		try
		{
			ModManager.instance.LoadModContentFromObject(contentInfo);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			LoadContentDialog.AppendError(string.Format("- {0}: Could not load content mod at {1} version={2}, details: {3}", new object[]
			{
				contentInfo.sourceMod.ToString(),
				contentInfo.bundlePath,
				contentInfo.versionInfo,
				ex.Message
			}));
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00080120 File Offset: 0x0007E320
	public static void PreprocessContentModPrefab(GameObject prefab, ModContentInformation contentInfo)
	{
		try
		{
			if (contentInfo.requiresShaderReload)
			{
				FixBundleShaders.FixAllRendererShadersRecursive(prefab.transform);
			}
			foreach (ScriptedBehaviour scriptedBehaviour in prefab.GetComponentsInChildren<ScriptedBehaviour>(true))
			{
				scriptedBehaviour.sourceModPath = contentInfo.sourceMod.path;
				TextAsset source = scriptedBehaviour.source;
				if (!ModManager.instance.TextAssetSourceIsRegistered(source))
				{
					ModManager.instance.textAssetSources.Add(new ModManager.TextAssetSource
					{
						textAsset = new WeakReference(source),
						bundleId = contentInfo.bundleId
					});
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x000801C8 File Offset: 0x0007E3C8
	private void LoadModContentFromObject(ModContentInformation contentInfo)
	{
		this.onlyOfficialContent = false;
		GameObject contentObject = contentInfo.contentObject;
		WeaponContentMod component = contentObject.GetComponent<WeaponContentMod>();
		VehicleContentMod component2 = contentObject.GetComponent<VehicleContentMod>();
		ActorSkinContentMod component3 = contentObject.GetComponent<ActorSkinContentMod>();
		MutatorContentMod component4 = contentObject.GetComponent<MutatorContentMod>();
		RequireGameVersion component5 = contentObject.GetComponent<RequireGameVersion>();
		if (component != null)
		{
			contentInfo.sourceMod.weaponContentMods.Add(component);
			this.LoadWeaponContent(component, contentInfo);
		}
		if (component2 != null)
		{
			contentInfo.sourceMod.vehicleContentMods.Add(component2);
			this.LoadVehicleContent(component2, contentInfo);
		}
		if (component3 != null)
		{
			contentInfo.sourceMod.skinContentMods.Add(component3);
			this.LoadSkinContent(component3, contentInfo);
		}
		if (component4 != null)
		{
			contentInfo.sourceMod.mutatorContentMods.Add(component4);
			this.LoadMutatorContent(component4, contentInfo);
		}
		if (component5 != null)
		{
			contentInfo.sourceMod.requiredGameVersion = Mathf.Max(component5.version, contentInfo.sourceMod.requiredGameVersion);
		}
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000802BC File Offset: 0x0007E4BC
	private void LoadWeaponContent(WeaponContentMod weaponContent, ModContentInformation contentInfo)
	{
		foreach (WeaponManager.WeaponEntry weaponEntry in weaponContent.weaponEntries)
		{
			if (this.WeaponEntryIsOk(weaponEntry, contentInfo))
			{
				ModManager.PreprocessWeaponEntryPrefab(weaponEntry, contentInfo);
				WeaponManager.instance.allWeapons.Add(weaponEntry);
			}
		}
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0008032C File Offset: 0x0007E52C
	public static bool IsParentToTransform(Transform parent, Transform match)
	{
		Transform transform = match;
		while (transform != null)
		{
			if (transform == parent)
			{
				return true;
			}
			transform = transform.parent;
		}
		return false;
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0008035C File Offset: 0x0007E55C
	public static void PreprocessWeaponEntryPrefab(WeaponManager.WeaponEntry entry, ModContentInformation contentInfo)
	{
		Weapon component = entry.prefab.GetComponent<Weapon>();
		ModManager.PreprocessContentModPrefab(component.gameObject, contentInfo);
		if (entry.usableByAiAllowOverride)
		{
			entry.usableByAi = true;
		}
		entry.isAvailableByDefault = true;
		if (entry.slot == WeaponManager.WeaponSlot.Primary && component.configuration.effInfantry == Weapon.Effectiveness.Yes)
		{
			component.configuration.effInfantry = Weapon.Effectiveness.Preferred;
		}
		if (component.thirdPersonTransform == null)
		{
			if (entry.usableByAi)
			{
				LoadContentDialog.AppendError(string.Concat(new string[]
				{
					"- ",
					contentInfo.sourceMod.ToString(),
					": Third Person Transform not setup on ",
					entry.name,
					", disabling AI from using this weapon."
				}));
			}
			entry.usableByAi = false;
			entry.usableByAiAllowOverride = false;
		}
		else if (component.cullInThirdPerson != null)
		{
			bool flag = false;
			for (int i = 0; i < component.cullInThirdPerson.Length; i++)
			{
				GameObject gameObject = component.cullInThirdPerson[i];
				if (gameObject == null)
				{
					flag = true;
				}
				else if (ModManager.IsParentToTransform(gameObject.transform, component.thirdPersonTransform))
				{
					flag = true;
					LoadContentDialog.AppendError("- " + contentInfo.sourceMod.ToString() + ": CullInThirdPerson array contains third person transform on " + entry.name);
					component.cullInThirdPerson[i] = null;
				}
			}
			if (flag)
			{
				component.cullInThirdPerson = (from t in component.cullInThirdPerson
				where t != null
				select t).ToArray<GameObject>();
			}
		}
		if (entry.distance == WeaponManager.WeaponEntry.Distance.Auto)
		{
			if (component.IsMeleeWeapon() || component.configuration.effectiveRange < 120f)
			{
				entry.distance = WeaponManager.WeaponEntry.Distance.Short;
			}
			else if (component.configuration.effectiveRange < 500f)
			{
				entry.distance = WeaponManager.WeaponEntry.Distance.Mid;
			}
			else
			{
				entry.distance = WeaponManager.WeaponEntry.Distance.Far;
			}
		}
		component.hasAnyAttachedColliders = (component.GetComponentInChildren<Collider>() != null);
		entry.sourceMod = contentInfo.sourceMod;
		entry.UpdateUiSpriteFromPrefab();
		GameManager.SetupRecursiveLayer(component.transform, 15);
		GameManager.SetupWeaponPrefab(component, null, contentInfo);
		foreach (Weapon weapon in component.GetComponentsInChildren<Weapon>())
		{
			if (!(weapon == component))
			{
				GameManager.SetupWeaponPrefab(weapon, null, contentInfo);
			}
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00080590 File Offset: 0x0007E790
	private void LoadVehicleContent(VehicleContentMod vehicleContentMod, ModContentInformation contentInfo)
	{
		vehicleContentMod.sourceMod = contentInfo.sourceMod;
		Array values = Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));
		Array values2 = Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));
		foreach (IVehicleContentProvider vehicleContentProvider in vehicleContentMod.AllContentProviders())
		{
			this.vehicleContentProviders.Add(vehicleContentProvider);
			VehicleContentMod.Variant variant = vehicleContentProvider as VehicleContentMod.Variant;
			if (variant != null)
			{
				variant.sourceContentMod = vehicleContentMod;
			}
			foreach (object obj in values)
			{
				VehicleSpawner.VehicleSpawnType spawnType = (VehicleSpawner.VehicleSpawnType)obj;
				GameObject vehiclePrefab = vehicleContentProvider.GetVehiclePrefab(spawnType);
				if (vehiclePrefab != null)
				{
					Vehicle component = vehiclePrefab.GetComponent<Vehicle>();
					if (this.VehicleIsOk(vehiclePrefab, component, contentInfo))
					{
						this.OnVehiclePrefabLoaded(spawnType, vehiclePrefab);
						if (!this.vehiclePrefabContentProviders.ContainsKey(vehiclePrefab))
						{
							this.vehiclePrefabContentProviders.Add(vehiclePrefab, new List<IVehicleContentProvider>());
						}
						this.vehiclePrefabContentProviders[vehiclePrefab].Add(vehicleContentProvider);
						this.AddVehicleNamePrefabLookup(this.vehiclePrefabName, component);
						GameManager.SetupVehiclePrefab(vehiclePrefab, contentInfo);
					}
				}
			}
			foreach (object obj2 in values2)
			{
				TurretSpawner.TurretSpawnType spawnType2 = (TurretSpawner.TurretSpawnType)obj2;
				GameObject vehiclePrefab2 = vehicleContentProvider.GetVehiclePrefab(spawnType2);
				if (vehiclePrefab2 != null)
				{
					Vehicle component2 = vehiclePrefab2.GetComponent<Vehicle>();
					if (this.VehicleIsOk(vehiclePrefab2, component2, contentInfo))
					{
						this.OnTurretPrefabLoaded(spawnType2, vehiclePrefab2);
						if (!this.vehiclePrefabContentProviders.ContainsKey(vehiclePrefab2))
						{
							this.vehiclePrefabContentProviders.Add(vehiclePrefab2, new List<IVehicleContentProvider>());
						}
						this.vehiclePrefabContentProviders[vehiclePrefab2].Add(vehicleContentProvider);
						this.AddVehicleNamePrefabLookup(this.turretPrefabName, component2);
						GameManager.SetupVehiclePrefab(vehiclePrefab2, contentInfo);
					}
				}
			}
		}
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x000807DC File Offset: 0x0007E9DC
	private void OnGenericPrefabLoaded<T>(T type, GameObject prefab, Dictionary<T, List<GameObject>> prefabsMap)
	{
		List<GameObject> list = prefabsMap[type];
		if (!list.Contains(prefab))
		{
			list.Add(prefab);
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0000BA82 File Offset: 0x00009C82
	private void OnVehiclePrefabLoaded(VehicleSpawner.VehicleSpawnType spawnType, GameObject prefab)
	{
		this.OnGenericPrefabLoaded<VehicleSpawner.VehicleSpawnType>(spawnType, prefab, this.vehiclePrefabs);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0000BA92 File Offset: 0x00009C92
	private void OnTurretPrefabLoaded(TurretSpawner.TurretSpawnType spawnType, GameObject prefab)
	{
		this.OnGenericPrefabLoaded<TurretSpawner.TurretSpawnType>(spawnType, prefab, this.turretPrefabs);
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00080804 File Offset: 0x0007EA04
	private void LoadSkinContent(ActorSkinContentMod skinContentMod, ModContentInformation contentInfo)
	{
		if (contentInfo.requiresShaderReload)
		{
			foreach (ActorSkin actorSkin in skinContentMod.skins)
			{
				Material[] materials = actorSkin.characterSkin.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					FixBundleShaders.FixMaterialShader(materials[i]);
				}
				materials = actorSkin.armSkin.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					FixBundleShaders.FixMaterialShader(materials[i]);
				}
				materials = actorSkin.kickLegSkin.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					FixBundleShaders.FixMaterialShader(materials[i]);
				}
			}
		}
		foreach (ActorSkin actorSkin2 in skinContentMod.skins)
		{
			actorSkin2.sourceMod = contentInfo.sourceMod;
			this.actorSkins.Add(actorSkin2);
			RetargetSkin.Retarget(actorSkin2);
		}
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00080924 File Offset: 0x0007EB24
	private void LoadMutatorContent(MutatorContentMod mutatorContentMod, ModContentInformation contentInfo)
	{
		foreach (MutatorEntry mutatorEntry in mutatorContentMod.mutators)
		{
			UnityEngine.Object mutatorPrefab = mutatorEntry.mutatorPrefab;
			mutatorEntry.sourceMod = contentInfo.sourceMod;
			if (mutatorPrefab != null)
			{
				mutatorEntry.isEnabled = GameManager.IsTestingContentMod();
				this.SetupMutatorPrefabs(mutatorEntry, contentInfo);
				this.loadedMutators.Add(mutatorEntry);
			}
			else
			{
				LoadContentDialog.AppendError("- " + contentInfo.sourceMod.ToString() + ": Could not load mutator prefab from mutator entry " + mutatorEntry.name);
			}
		}
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x000809D0 File Offset: 0x0007EBD0
	public void SetupMutatorPrefabs(MutatorEntry mutator, ModContentInformation contentInfo)
	{
		HashSet<ScriptedBehaviour> hashSet = new HashSet<ScriptedBehaviour>();
		Queue<ScriptedBehaviour> queue = new Queue<ScriptedBehaviour>();
		ScriptedBehaviour[] componentsInChildren = mutator.mutatorPrefab.GetComponentsInChildren<ScriptedBehaviour>();
		ModManager.PreprocessContentModPrefab(mutator.mutatorPrefab, contentInfo);
		foreach (ScriptedBehaviour item in componentsInChildren)
		{
			queue.Enqueue(item);
			hashSet.Add(item);
		}
		while (queue.Count > 0)
		{
			ScriptedBehaviour scriptedBehaviour = queue.Dequeue();
			scriptedBehaviour.sourceMutator = mutator;
			ModManager.PreprocessContentModPrefab(scriptedBehaviour.gameObject, contentInfo);
			NamedTarget[] targets = scriptedBehaviour.targets;
			for (int i = 0; i < targets.Length; i++)
			{
				GameObject gameObject = targets[i].value as GameObject;
				if (gameObject != null)
				{
					foreach (ScriptedBehaviour item2 in gameObject.GetComponentsInChildren<ScriptedBehaviour>())
					{
						if (!hashSet.Contains(item2))
						{
							queue.Enqueue(item2);
							hashSet.Add(item2);
						}
					}
					ModManager.PreprocessContentModPrefab(gameObject, contentInfo);
				}
			}
		}
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00080AC4 File Offset: 0x0007ECC4
	private bool VehicleIsOk(GameObject prefab, Vehicle vehicle, ModContentInformation contentInfo)
	{
		if (vehicle == null)
		{
			LoadContentDialog.AppendError(string.Concat(new string[]
			{
				"- ",
				contentInfo.sourceMod.ToString(),
				": Did not load vehicle prefab ",
				prefab.name,
				", the prefab has no Vehicle component."
			}));
			return false;
		}
		if (vehicle.GetComponentsInChildren<Vehicle>().Length > 1)
		{
			LoadContentDialog.AppendError(string.Concat(new string[]
			{
				"- ",
				contentInfo.sourceMod.ToString(),
				": Did not load vehicle prefab ",
				vehicle.name,
				", the prefab has multiple Vehicle components."
			}));
			return false;
		}
		return true;
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00080B68 File Offset: 0x0007ED68
	private bool WeaponEntryIsOk(WeaponManager.WeaponEntry entry, ModContentInformation contentInfo)
	{
		if (entry.prefab == null || entry.prefab.GetComponent<Weapon>() == null)
		{
			LoadContentDialog.AppendError(string.Concat(new string[]
			{
				"- ",
				contentInfo.sourceMod.ToString(),
				": Did not load broken weapon entry ",
				entry.name,
				", the entry has no Weapon prefab."
			}));
			return false;
		}
		Weapon component = entry.prefab.GetComponent<Weapon>();
		for (int i = 0; i < component.configuration.muzzles.Length; i++)
		{
			if (component.configuration.muzzles[i] == null)
			{
				LoadContentDialog.AppendError(string.Concat(new string[]
				{
					"- ",
					contentInfo.sourceMod.ToString(),
					": Did not load broken weapon entry ",
					entry.name,
					", the weapon has one or more null muzzles."
				}));
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00080C54 File Offset: 0x0007EE54
	public int? GetTextAssetBundleId(TextAsset asset)
	{
		if (this.textAssetSources == null)
		{
			return null;
		}
		foreach (ModManager.TextAssetSource textAssetSource in this.textAssetSources)
		{
			if (textAssetSource.textAsset.IsAlive)
			{
				TextAsset textAsset = (TextAsset)textAssetSource.textAsset.Target;
				if (textAsset != null && textAsset && textAsset == asset)
				{
					return new int?(textAssetSource.bundleId);
				}
			}
		}
		return null;
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00080D04 File Offset: 0x0007EF04
	public bool TextAssetSourceIsRegistered(TextAsset asset)
	{
		foreach (ModManager.TextAssetSource textAssetSource in this.textAssetSources)
		{
			if (textAssetSource.textAsset.IsAlive && (TextAsset)textAssetSource.textAsset.Target == asset)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0000BAA2 File Offset: 0x00009CA2
	public static IEnumerable<MutatorEntry> GetAllEnabledMutators()
	{
		return from m in ModManager.instance.loadedMutators
		where m.isEnabled
		select m;
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x00080D7C File Offset: 0x0007EF7C
	public static void SpawnAllEnabledMutatorPrefabs()
	{
		if (ModManager.instance.loadedMutators == null)
		{
			return;
		}
		try
		{
			foreach (MutatorEntry mutatorEntry in ModManager.GetAllEnabledMutators())
			{
				UnityEngine.Object.Instantiate<GameObject>(mutatorEntry.mutatorPrefab);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x04000F3C RID: 3900
	private const int RUNTIME_EXPECTED_RAM_USAGE_MEGABYTES = 1000;

	// Token: 0x04000F3D RID: 3901
	public static ModManager instance;

	// Token: 0x04000F3E RID: 3902
	public static ModManager.EngineVersionInfo engineVersion;

	// Token: 0x04000F3F RID: 3903
	private Dictionary<PublishedFileId_t, ModInformation> modWithitemId;

	// Token: 0x04000F40 RID: 3904
	public List<ModInformation> mods;

	// Token: 0x04000F41 RID: 3905
	public List<MutatorEntry> loadedMutators;

	// Token: 0x04000F42 RID: 3906
	public List<MutatorEntry> builtInMutators;

	// Token: 0x04000F43 RID: 3907
	public ActorSkin[] builtInActorSkins;

	// Token: 0x04000F44 RID: 3908
	[NonSerialized]
	public uint contentVersion;

	// Token: 0x04000F45 RID: 3909
	[NonSerialized]
	public string modStagingPathOverride;

	// Token: 0x04000F46 RID: 3910
	[NonSerialized]
	public bool onlyOfficialContent;

	// Token: 0x04000F47 RID: 3911
	[NonSerialized]
	public bool strictModVersionFilter;

	// Token: 0x04000F48 RID: 3912
	private ProfilerMarker PM_LoadModContent = new ProfilerMarker("Load Mod Content");

	// Token: 0x04000F49 RID: 3913
	private ProfilerMarker PM_LoadWeaponContent = new ProfilerMarker("WeaponContent");

	// Token: 0x04000F4A RID: 3914
	private ProfilerMarker PM_LoadVehicleContent = new ProfilerMarker("VehicleContent");

	// Token: 0x04000F4B RID: 3915
	private ProfilerMarker PM_LoadMutatorContent = new ProfilerMarker("MutatorContent");

	// Token: 0x04000F4C RID: 3916
	private ProfilerMarker PM_LoadSkinContent = new ProfilerMarker("SkinContent");

	// Token: 0x04000F4D RID: 3917
	private List<ModManager.TextAssetSource> textAssetSources;

	// Token: 0x04000F4E RID: 3918
	[NonSerialized]
	public bool noContentMods;

	// Token: 0x04000F4F RID: 3919
	[NonSerialized]
	public bool noWorkshopMods;

	// Token: 0x04000F50 RID: 3920
	[NonSerialized]
	public bool contentHasFinishedLoading;

	// Token: 0x04000F51 RID: 3921
	[NonSerialized]
	public List<IVehicleContentProvider> vehicleContentProviders;

	// Token: 0x04000F52 RID: 3922
	[NonSerialized]
	public Dictionary<VehicleSpawner.VehicleSpawnType, List<GameObject>> vehiclePrefabs;

	// Token: 0x04000F53 RID: 3923
	[NonSerialized]
	public Dictionary<TurretSpawner.TurretSpawnType, List<GameObject>> turretPrefabs;

	// Token: 0x04000F54 RID: 3924
	[NonSerialized]
	public List<ActorSkin> actorSkins;

	// Token: 0x04000F55 RID: 3925
	[NonSerialized]
	private Dictionary<GameObject, List<IVehicleContentProvider>> vehiclePrefabContentProviders = new Dictionary<GameObject, List<IVehicleContentProvider>>();

	// Token: 0x04000F56 RID: 3926
	[NonSerialized]
	public Dictionary<string, List<GameObject>> vehiclePrefabName = new Dictionary<string, List<GameObject>>();

	// Token: 0x04000F57 RID: 3927
	[NonSerialized]
	public Dictionary<string, List<GameObject>> turretPrefabName = new Dictionary<string, List<GameObject>>();

	// Token: 0x04000F58 RID: 3928
	private const string DUMMY_ASSET_NAME = "dummy_win.assetbundle";

	// Token: 0x04000F59 RID: 3929
	private const int MIN_LOAD_WORKERS = 1;

	// Token: 0x04000F5A RID: 3930
	private const int MAX_LOAD_WORKERS = 4;

	// Token: 0x04000F5B RID: 3931
	private TextAsset[] beforeBundleLoadTextAssets;

	// Token: 0x04000F5C RID: 3932
	private bool workerIsReadingDisk;

	// Token: 0x04000F5D RID: 3933
	private bool workerIsLoadingBundle;

	// Token: 0x04000F5E RID: 3934
	private long processedContentBytes;

	// Token: 0x04000F5F RID: 3935
	private const int BUNDLE_HEADER_VERSION_OFFSET_BYTES = 18;

	// Token: 0x04000F60 RID: 3936
	private const int BUNDLE_HEADER_VERSION_LENGTH_BYTES = 16;

	// Token: 0x04000F61 RID: 3937
	private static byte[] bundleHeaderBuffer = new byte[16];

	// Token: 0x0200021A RID: 538
	private struct TextAssetSource
	{
		// Token: 0x04000F62 RID: 3938
		public WeakReference textAsset;

		// Token: 0x04000F63 RID: 3939
		public int bundleId;
	}

	// Token: 0x0200021B RID: 539
	public struct EngineVersionInfo
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public static ModManager.EngineVersionInfo CurrentPlayer
		{
			get
			{
				return new ModManager.EngineVersionInfo(Application.unityVersion);
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00080E74 File Offset: 0x0007F074
		public EngineVersionInfo(string versionString)
		{
			this.majorVersion = 0;
			this.minorVersion = 0;
			this.patch = "";
			try
			{
				string[] array = versionString.Split(new char[]
				{
					'.'
				});
				this.majorVersion = int.Parse(array[0]);
				this.minorVersion = int.Parse(array[1]);
				this.patch = array[2].Split(new char[1])[0];
			}
			catch
			{
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public override string ToString()
		{
			if (this.IsUnknown())
			{
				return "Unknown version";
			}
			return string.Format("{0}.{1}.{2}", this.majorVersion, this.minorVersion, this.patch);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0000BB22 File Offset: 0x00009D22
		public bool IsUnknown()
		{
			return this.majorVersion == 0;
		}

		// Token: 0x04000F64 RID: 3940
		public int majorVersion;

		// Token: 0x04000F65 RID: 3941
		public int minorVersion;

		// Token: 0x04000F66 RID: 3942
		public string patch;
	}
}

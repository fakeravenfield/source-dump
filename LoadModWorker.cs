using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lua;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class LoadModWorker
{
	// Token: 0x06000E23 RID: 3619 RVA: 0x0000B590 File Offset: 0x00009790
	public bool IsIdle()
	{
		return this.IsReadyToAcceptNewItem() || this.state == LoadModWorker.State.AwaitDisk;
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0000B5A5 File Offset: 0x000097A5
	public bool IsReadyToAcceptNewItem()
	{
		return this.state == LoadModWorker.State.Success || this.state == LoadModWorker.State.Idle || this.state == LoadModWorker.State.Failed;
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0000B5C3 File Offset: 0x000097C3
	private static bool PassesStrictVersionFilter(ModManager.EngineVersionInfo version)
	{
		return version.majorVersion == ModManager.engineVersion.majorVersion;
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0007E990 File Offset: 0x0007CB90
	public float GetProgress()
	{
		switch (this.state)
		{
		case LoadModWorker.State.AwaitDisk:
			return 0.01f;
		case LoadModWorker.State.ReadDisk:
			return 0.01f + this.readDiskRequest.progress * 0.49f;
		case LoadModWorker.State.AwaitLoadContent:
			return 0.5f;
		case LoadModWorker.State.LoadContent:
			return 0.5f + this.loadBundleRequest.progress * 0.48f;
		case LoadModWorker.State.Failed:
		case LoadModWorker.State.Success:
			return 1f;
		default:
			return 0f;
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0000B5D7 File Offset: 0x000097D7
	public IEnumerator LoadContentFileAsync(FileInfo bundleFile, ModInformation sourceMod)
	{
		this.currentFile = bundleFile;
		this.currentFileName = this.currentFile.Name;
		this.state = LoadModWorker.State.AwaitDisk;
		string assetBundlePath = bundleFile.FullName;
		ModContentInformation contentInfo = new ModContentInformation(assetBundlePath, sourceMod);
		if (ModManager.instance.strictModVersionFilter && !LoadModWorker.PassesStrictVersionFilter(contentInfo.versionInfo))
		{
			Debug.Log(string.Format("LoadModWorker: Skipping {0} from {1} as its' engine version ({2} does not match the game's engine version.", bundleFile.Name, sourceMod, contentInfo.versionInfo));
			this.EndJob(LoadModWorker.State.Failed);
			yield break;
		}
		Debug.Log(string.Format("LoadModWorker: Loading {0} from {1}", bundleFile.Name, sourceMod));
		GameManager.DebugVerbose("Loading file {0}, version={1} reloadShaders={2}", new object[]
		{
			assetBundlePath,
			contentInfo.versionInfo,
			contentInfo.requiresShaderReload
		});
		yield return new WaitUntil(new Func<bool>(ModManager.RequestDiskRead));
		this.state = LoadModWorker.State.ReadDisk;
		this.readDiskRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
		yield return this.readDiskRequest;
		this.state = LoadModWorker.State.AwaitLoadContent;
		ModManager.ReleaseDiskRead();
		AssetBundle bundle = this.readDiskRequest.assetBundle;
		if (bundle == null)
		{
			LoadContentDialog.AppendError(string.Format("- {0}: Could not load content mod at {1} (was another .rfc file of the same name already loaded?)", sourceMod.ToString(), assetBundlePath));
			this.EndJob(LoadModWorker.State.Failed);
			yield break;
		}
		contentInfo.AssignBundleID(bundle);
		yield return new WaitUntil(new Func<bool>(ModManager.RequestBundleLoad));
		this.state = LoadModWorker.State.LoadContent;
		this.loadBundleRequest = bundle.LoadAssetAsync<GameObject>(bundle.GetAllAssetNames()[0]);
		yield return this.loadBundleRequest;
		GameObject gameObject = this.loadBundleRequest.asset as GameObject;
		if (gameObject == null)
		{
			LoadContentDialog.AppendError(string.Format("- {0}: Could not load content mod at {1}. The mod did not contain a main asset content game object.", sourceMod.ToString(), assetBundlePath));
			ModManager.ReleaseBundleLoad();
			this.EndJob(LoadModWorker.State.Failed);
			yield break;
		}
		ModManager.ReleaseBundleLoadAndAssignRSBundleID(contentInfo.bundleId);
		contentInfo.contentObject = gameObject;
		string text = bundleFile.FullName + ".patch";
		if (File.Exists(text))
		{
			GameManager.DebugVerbose("Loading patch data {0}", new object[]
			{
				text
			});
			try
			{
				contentInfo.patchData = PatchData.Deserialize(text);
			}
			catch (Exception exception)
			{
				Debug.LogError("Could not load patch data");
				Debug.LogException(exception);
			}
		}
		ModManager.OnWorkerContentObjectReady(contentInfo);
		bundle.Unload(false);
		this.EndJob(LoadModWorker.State.Success);
		yield break;
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0000B5F4 File Offset: 0x000097F4
	private void EndJob(LoadModWorker.State state)
	{
		this.state = state;
		ModManager.OnAsyncWorkerJobDone(this.currentFile, state);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0007EA10 File Offset: 0x0007CC10
	public void LoadMod(ModInformation mod)
	{
		List<FileInfo> gameContent = mod.content.GetGameContent();
		for (int i = 0; i < gameContent.Count; i++)
		{
			this.LoadContentFile(gameContent[i], mod);
		}
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0007EA48 File Offset: 0x0007CC48
	public void LoadContentFile(FileInfo bundleFile, ModInformation mod)
	{
		string fullName = bundleFile.FullName;
		Debug.LogFormat("Loading {0}", new object[]
		{
			fullName
		});
		ModContentInformation modContentInformation = new ModContentInformation(fullName, mod);
		string message = string.Format("Loading file {0}, version={1} reloadShaders={2}", fullName, modContentInformation.versionInfo, modContentInformation.requiresShaderReload);
		ScriptConsole.instance.LogInfo(message);
		AssetBundle assetBundle = AssetBundle.LoadFromFile(fullName);
		modContentInformation.AssignBundleID(assetBundle);
		ModManager.ReleaseBundleLoad();
		ModManager.RequestBundleLoad();
		GameObject contentObject = assetBundle.LoadAsset<GameObject>(assetBundle.GetAllAssetNames()[0]);
		ModManager.ReleaseBundleLoadAndAssignRSBundleID(modContentInformation.bundleId);
		modContentInformation.contentObject = contentObject;
		ModManager.OnWorkerContentObjectReady(modContentInformation);
		assetBundle.Unload(false);
		this.state = LoadModWorker.State.Success;
	}

	// Token: 0x04000F0A RID: 3850
	public LoadModWorker.State state;

	// Token: 0x04000F0B RID: 3851
	private FileInfo currentFile;

	// Token: 0x04000F0C RID: 3852
	public string currentFileName = "";

	// Token: 0x04000F0D RID: 3853
	private AssetBundleCreateRequest readDiskRequest;

	// Token: 0x04000F0E RID: 3854
	private AssetBundleRequest loadBundleRequest;

	// Token: 0x02000215 RID: 533
	public enum State
	{
		// Token: 0x04000F10 RID: 3856
		Idle,
		// Token: 0x04000F11 RID: 3857
		AwaitDisk,
		// Token: 0x04000F12 RID: 3858
		ReadDisk,
		// Token: 0x04000F13 RID: 3859
		AwaitLoadContent,
		// Token: 0x04000F14 RID: 3860
		LoadContent,
		// Token: 0x04000F15 RID: 3861
		Failed,
		// Token: 0x04000F16 RID: 3862
		Success
	}
}

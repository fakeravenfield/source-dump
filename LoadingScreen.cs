using System;
using System.Collections;
using System.Collections.Generic;
using MapEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020001EC RID: 492
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06000D35 RID: 3381 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
	public void Start()
	{
		this.fadeInOverlay.gameObject.SetActive(true);
		this.LoadDecorationResource();
		this.entry = GameManager.instance.lastMapEntry;
		this.title.text = "LOADING " + this.entry.name;
		PostProcessingManager.ApplyMenuProfile();
		this.SetProgress(0f);
		this.SetStatus("Finding Map");
		if (!this.entry.isCustomMap)
		{
			base.StartCoroutine(this.LoadBuiltInLevelCoroutine());
			return;
		}
		if (this.entry.IsRFLBundle())
		{
			base.StartCoroutine(this.LoadAssetBundleLevelCoroutine());
			return;
		}
		base.StartCoroutine(this.LoadMapDescriptorCoroutine());
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0007C184 File Offset: 0x0007A384
	private void LoadDecorationResource()
	{
		int num = UnityEngine.Random.Range(0, this.decorationResources.Length);
		UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(this.decorationResources[num]));
		this.OnGraphicsElementLoaded();
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0000AB98 File Offset: 0x00008D98
	private void OnGraphicsElementLoaded()
	{
		this.fadeInOverlay.CrossFadeAlpha(0f, 4f, true);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0000ABB0 File Offset: 0x00008DB0
	private IEnumerator LoadMapDescriptorCoroutine()
	{
		GameManager.instance.levelBundleContentInfo = ModContentInformation.VanillaContent;
		foreach (SceneConstructionProgress sceneConstructionProgress in SceneConstructor.LoadingScreenWork(this.entry))
		{
			this.SetStatus(sceneConstructionProgress.status);
			this.SetProgress(sceneConstructionProgress.progress);
			yield return null;
		}
		IEnumerator<SceneConstructionProgress> enumerator = null;
		if (base.gameObject.scene.isLoaded)
		{
			Debug.Log("Unloading loading screen (from itself)");
			AsyncOperation unload = SceneManager.UnloadSceneAsync(base.gameObject.scene);
			while (unload.isDone)
			{
				yield return null;
			}
			unload = null;
		}
		yield break;
		yield break;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0007C1BC File Offset: 0x0007A3BC
	private AsyncOperation LoadSceneAsync(string name)
	{
		Debug.Log("Loading scene " + name);
		AsyncOperation asyncOperation;
		if (this.entry.nightVersion)
		{
			asyncOperation = SceneManager.LoadSceneAsync(name + " night", LoadSceneMode.Single);
			if (asyncOperation != null)
			{
				return asyncOperation;
			}
			Debug.Log("Could not load night version scene, reverting to normal version.");
		}
		asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
		if (asyncOperation != null)
		{
			return asyncOperation;
		}
		Debug.Log("Could not load normal version of scene, falling back to island.");
		return SceneManager.LoadSceneAsync("island", LoadSceneMode.Single);
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0000ABBF File Offset: 0x00008DBF
	private IEnumerator LoadAssetBundleLevelCoroutine()
	{
		string bundlePath = this.entry.sceneName;
		AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
		this.SetStatus("Unpacking Data");
		while (!bundleRequest.isDone)
		{
			this.SetProgress(bundleRequest.progress * 0.5f);
			yield return null;
		}
		string[] allScenePaths = bundleRequest.assetBundle.GetAllScenePaths();
		GameManager.instance.levelBundle = bundleRequest.assetBundle;
		GameManager.instance.levelBundleContentInfo = new ModContentInformation(bundlePath, null);
		int num = 0;
		int num2 = int.MaxValue;
		for (int i = 0; i < allScenePaths.Length; i++)
		{
			if (allScenePaths[i].Length < num2)
			{
				num = i;
				num2 = allScenePaths[i].Length;
			}
		}
		AsyncOperation asyncLoad = this.LoadSceneAsync(allScenePaths[num]);
		this.SetStatus("Starting Map");
		while (!asyncLoad.isDone)
		{
			this.SetProgress(0.5f + 0.5f * asyncLoad.progress);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0000ABCE File Offset: 0x00008DCE
	private IEnumerator LoadBuiltInLevelCoroutine()
	{
		GameManager.instance.levelBundleContentInfo = ModContentInformation.VanillaContent;
		AsyncOperation asyncLoad = this.LoadSceneAsync(this.entry.sceneName);
		this.SetStatus("Starting Map");
		while (!asyncLoad.isDone)
		{
			this.SetProgress(asyncLoad.progress);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0000ABDD File Offset: 0x00008DDD
	private void SetProgress(float progress)
	{
		this.loadingBar.rectTransform.anchorMax = new Vector2(progress, 1f);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0000ABFA File Offset: 0x00008DFA
	private void SetStatus(string text)
	{
		this.status.text = text;
	}

	// Token: 0x04000E3D RID: 3645
	private const string NIGHT_VERSION_SUFFIX = " night";

	// Token: 0x04000E3E RID: 3646
	private const string FALLBACK_SCENE_NAME = "island";

	// Token: 0x04000E3F RID: 3647
	public RawImage loadingBar;

	// Token: 0x04000E40 RID: 3648
	public RawImage fadeInOverlay;

	// Token: 0x04000E41 RID: 3649
	public Text title;

	// Token: 0x04000E42 RID: 3650
	public Text status;

	// Token: 0x04000E43 RID: 3651
	public string[] decorationResources;

	// Token: 0x04000E44 RID: 3652
	private InstantActionMaps.MapEntry entry;
}

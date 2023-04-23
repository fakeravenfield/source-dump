using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000157 RID: 343
public class PostProcessingManager : MonoBehaviour
{
	// Token: 0x06000933 RID: 2355 RVA: 0x00008192 File Offset: 0x00006392
	public void Awake()
	{
		PostProcessingManager.instance = this;
		base.enabled = true;
		this.aoVolume.gameObject.layer = 29;
		this.postProcessingCamera = base.GetComponent<Camera>();
		this.registeredWorldCameras = new HashSet<Camera>();
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x000081CA File Offset: 0x000063CA
	private static bool IsEnabled()
	{
		return PostProcessingManager.instance != null && PostProcessingManager.instance.enabled;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x000081E5 File Offset: 0x000063E5
	public static void OnSceneLoaded()
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		PostProcessingManager.ApplyMenuProfile();
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0006A768 File Offset: 0x00068968
	public static void ApplyMenuProfile()
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		try
		{
			PostProcessingManager.instance.globalVolume.profile = PostProcessingManager.instance.menuProfile;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x000081F4 File Offset: 0x000063F4
	public static void StartGame()
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		PostProcessingManager.instance.ApplyOptions();
		PostProcessingManager.FindAndApplyLevelColorGrading(GameManager.GameParameters().nightMode);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0006A7B0 File Offset: 0x000689B0
	public static void FindAndApplyLevelColorGrading(bool isNight)
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		PostProcessingManager.instance.globalVolume.profile = PostProcessingManager.instance.ingameProfile;
		LevelColorGrading.GradingData gradingData = LevelColorGrading.GetDefaultPreset(isNight);
		LevelColorGrading levelColorGrading = UnityEngine.Object.FindObjectOfType<LevelColorGrading>();
		if (levelColorGrading != null)
		{
			LevelColorGrading.GradingData presetData = levelColorGrading.GetPresetData(isNight);
			if (presetData != null)
			{
				gradingData = presetData;
			}
		}
		PostProcessingManager.ApplyLevelGrading(gradingData, PostProcessingManager.instance.globalVolume.profile);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0006A818 File Offset: 0x00068A18
	private static void ApplyLevelGrading(LevelColorGrading.GradingData gradingData, PostProcessProfile profile)
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		if (gradingData != null)
		{
			ColorGrading setting = profile.GetSetting<ColorGrading>();
			Bloom setting2 = profile.GetSetting<Bloom>();
			setting.temperature.Override(gradingData.temperature);
			setting.tint.Override(gradingData.tint);
			setting.hueShift.Override(gradingData.hueShift);
			setting.saturation.Override(gradingData.saturation);
			setting.brightness.Override(gradingData.brightness);
			setting.contrast.Override(gradingData.contrast);
			setting2.threshold.Override(gradingData.bloomThreshold);
			setting2.intensity.Override(gradingData.bloomIntensity);
		}
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00008217 File Offset: 0x00006417
	private static FloatParameter OverrideFloat(float value)
	{
		return new FloatParameter
		{
			overrideState = true,
			value = value
		};
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0000822C File Offset: 0x0000642C
	public static void OnOptionsApplied()
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		PostProcessingManager.instance.ApplyOptions();
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0006A8C8 File Offset: 0x00068AC8
	private void ApplyOptions()
	{
		if (!PostProcessingManager.instance.enabled)
		{
			return;
		}
		int dropdown = Options.GetDropdown(OptionDropdown.Id.AntiAliasing);
		if (dropdown == 0)
		{
			this.finalLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
		}
		else if (dropdown == 1)
		{
			this.finalLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
		}
		else
		{
			SubpixelMorphologicalAntialiasing.Quality quality = (SubpixelMorphologicalAntialiasing.Quality)(dropdown - 2);
			this.finalLayer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
			this.finalLayer.subpixelMorphologicalAntialiasing.quality = quality;
		}
		bool toggle = Options.GetToggle(OptionToggle.Id.AmbientOcclusion);
		AmbientOcclusion setting = this.aoProfile.GetSetting<AmbientOcclusion>();
		setting.active = toggle;
		setting.enabled = new BoolParameter
		{
			value = toggle,
			overrideState = true
		};
		this.SetBloomSettings(this.globalVolume.profile);
		this.SetBloomSettings(this.nightVisionProfile);
		bool toggle2 = Options.GetToggle(OptionToggle.Id.ColorCorrection);
		ColorGrading setting2 = this.globalVolume.profile.GetSetting<ColorGrading>();
		setting2.active = toggle2;
		setting2.enabled = new BoolParameter
		{
			value = toggle2,
			overrideState = true
		};
		this.postProcessingCamera.allowHDR = Options.GetToggle(OptionToggle.Id.HDR);
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0006A9C4 File Offset: 0x00068BC4
	private void SetBloomSettings(PostProcessProfile profile)
	{
		int dropdown = Options.GetDropdown(OptionDropdown.Id.Bloom);
		Bloom setting = profile.GetSetting<Bloom>();
		setting.active = (dropdown > 0);
		setting.enabled = new BoolParameter
		{
			value = (dropdown > 0),
			overrideState = true
		};
		setting.fastMode = new BoolParameter
		{
			value = (dropdown == 1),
			overrideState = true
		};
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00008240 File Offset: 0x00006440
	private RenderTexture CreateScreenBufferTexture(RenderTextureFormat format)
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0, format);
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture.Create();
		return renderTexture;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0006AA20 File Offset: 0x00068C20
	public static void RegisterWorldCamera(Camera camera)
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		if (!PostProcessingManager.instance.registeredWorldCameras.Add(camera))
		{
			return;
		}
		camera.allowHDR = Options.GetToggle(OptionToggle.Id.HDR);
		PostProcessLayer postProcessLayer = camera.gameObject.AddComponent<PostProcessLayer>();
		postProcessLayer.volumeTrigger = camera.transform;
		postProcessLayer.volumeLayer = new LayerMask
		{
			value = 536870912
		};
		postProcessLayer.Init(PostProcessingManager.instance.ppResources);
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00008261 File Offset: 0x00006461
	public static void RegisterFirstPersonViewModelCamera(Camera camera)
	{
		if (!PostProcessingManager.IsEnabled())
		{
			return;
		}
		camera.allowHDR = Options.GetToggle(OptionToggle.Id.HDR);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00008277 File Offset: 0x00006477
	public void Dispose()
	{
		PostProcessingManager.instance = null;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00008277 File Offset: 0x00006477
	private void OnDestroy()
	{
		PostProcessingManager.instance = null;
	}

	// Token: 0x04000A0E RID: 2574
	public const int LAYER_POST_PROCESSING = 28;

	// Token: 0x04000A0F RID: 2575
	public const int LAYER_AO_ONLY = 29;

	// Token: 0x04000A10 RID: 2576
	private const int AO_ONLY_LAYER_MASK = 536870912;

	// Token: 0x04000A11 RID: 2577
	public static PostProcessingManager instance;

	// Token: 0x04000A12 RID: 2578
	public PostProcessResources ppResources;

	// Token: 0x04000A13 RID: 2579
	public PostProcessProfile ingameProfile;

	// Token: 0x04000A14 RID: 2580
	public PostProcessProfile menuProfile;

	// Token: 0x04000A15 RID: 2581
	public PostProcessProfile aoProfile;

	// Token: 0x04000A16 RID: 2582
	public PostProcessProfile nightVisionProfile;

	// Token: 0x04000A17 RID: 2583
	public PostProcessLayer finalLayer;

	// Token: 0x04000A18 RID: 2584
	public Material blitMaterial;

	// Token: 0x04000A19 RID: 2585
	public Material blitAddMaterial;

	// Token: 0x04000A1A RID: 2586
	public Material blitNormalMaterial;

	// Token: 0x04000A1B RID: 2587
	public Material blitMaskedNormalMaterial;

	// Token: 0x04000A1C RID: 2588
	public PostProcessVolume aoVolume;

	// Token: 0x04000A1D RID: 2589
	public PostProcessVolume globalVolume;

	// Token: 0x04000A1E RID: 2590
	private CommandBuffer blitWorldDepth;

	// Token: 0x04000A1F RID: 2591
	private CommandBuffer blitFirstPersonViewModelDepth;

	// Token: 0x04000A20 RID: 2592
	private CommandBuffer insertDepthTexture;

	// Token: 0x04000A21 RID: 2593
	private Camera postProcessingCamera;

	// Token: 0x04000A22 RID: 2594
	private HashSet<Camera> registeredWorldCameras;

	// Token: 0x04000A23 RID: 2595
	private RenderTexture depthTexture;

	// Token: 0x04000A24 RID: 2596
	private RenderTexture normalTexture;
}

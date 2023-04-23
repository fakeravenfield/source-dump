using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x020002CC RID: 716
public class Options : MonoBehaviour
{
	// Token: 0x06001300 RID: 4864 RVA: 0x0000F104 File Offset: 0x0000D304
	public static bool GetToggle(OptionToggle.Id id)
	{
		return Options.instance.toggleOptions[id].value;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x0000F11B File Offset: 0x0000D31B
	public static int GetDropdown(OptionDropdown.Id id)
	{
		return Options.instance.dropdownOptions[id].value;
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0000F132 File Offset: 0x0000D332
	public static float GetSlider(OptionSlider.Id id)
	{
		return Options.instance.sliderOptions[id].value;
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x0000F149 File Offset: 0x0000D349
	public static float GetScaledMouseSensitivity(OptionSlider.Id id)
	{
		return Mathf.Pow(Options.GetSlider(id), 2f);
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00091B6C File Offset: 0x0008FD6C
	public static void Show()
	{
		Options.instance.EnableCanvas(Options.instance.canvas);
		Options.instance.OpenTab(0);
		Options.instance.background.enabled = GameManager.IsIngame();
		Selectable[] array = Options.instance.disableIngameSelectables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].interactable = !GameManager.IsIngame();
		}
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x00091BD8 File Offset: 0x0008FDD8
	public static void Hide()
	{
		Options.instance.DisableCanvas(Options.instance.canvas);
		foreach (Canvas canvas in Options.instance.tabCanvases)
		{
			Options.instance.DisableCanvas(canvas);
		}
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x0000F15B File Offset: 0x0000D35B
	public static bool IsOpen()
	{
		return Options.instance.canvas.enabled;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0000F16C File Offset: 0x0000D36C
	public static void SaveAndClose()
	{
		Options.instance.Save();
		Options.Hide();
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0000F17D File Offset: 0x0000D37D
	public static bool IsInitialized()
	{
		return Options.instance != null;
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0000F18A File Offset: 0x0000D38A
	public void EnableCanvas(Canvas canvas)
	{
		this.SetCanvasEnabledFlag(canvas, true);
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0000F194 File Offset: 0x0000D394
	public void DisableCanvas(Canvas canvas)
	{
		this.SetCanvasEnabledFlag(canvas, false);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0000F19E File Offset: 0x0000D39E
	private void SetCanvasEnabledFlag(Canvas canvas, bool flag)
	{
		if (!this.canvasRaycaster.ContainsKey(canvas))
		{
			this.canvasRaycaster.Add(canvas, canvas.GetComponent<GraphicRaycaster>());
		}
		this.canvasRaycaster[canvas].enabled = flag;
		canvas.enabled = flag;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x00091C24 File Offset: 0x0008FE24
	public void LoadInputPreset(int index)
	{
		if (index == 0)
		{
			return;
		}
		int num = index - 1;
		SteelInput.LoadPreset(this.inputPresets[num]);
		SteelInputBinder[] array = this.inputBinders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateLabels();
		}
		this.inputPresetDropdown.value = 0;
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x00091C70 File Offset: 0x0008FE70
	public void OpenTab(int tabIndex)
	{
		for (int i = 0; i <= 2; i++)
		{
			this.SetCanvasEnabledFlag(this.tabCanvases[i], i == tabIndex);
		}
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x00091C9C File Offset: 0x0008FE9C
	private void Awake()
	{
		if (Options.instance != null)
		{
			UnityEngine.Object.Destroy(Options.instance.gameObject);
		}
		Options.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.tabCanvases = new Canvas[]
		{
			this.gameOptions,
			this.inputOptions,
			this.videoOptions
		};
		Canvas[] array = this.tabCanvases;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(true);
		}
		this.inputBinders = base.GetComponentsInChildren<SteelInputBinder>();
		this.dropdowns = base.GetComponentsInChildren<OptionDropdown>();
		this.toggles = base.GetComponentsInChildren<OptionToggle>();
		this.sliders = base.GetComponentsInChildren<OptionSlider>();
		this.resolutionOption = base.GetComponentInChildren<OptionResolution>();
		this.dropdownOptions = new Dictionary<OptionDropdown.Id, OptionDropdown>();
		this.toggleOptions = new Dictionary<OptionToggle.Id, OptionToggle>();
		this.sliderOptions = new Dictionary<OptionSlider.Id, OptionSlider>();
		foreach (OptionDropdown optionDropdown in this.dropdowns)
		{
			this.dropdownOptions.Add(optionDropdown.id, optionDropdown);
		}
		foreach (OptionToggle optionToggle in this.toggles)
		{
			this.toggleOptions.Add(optionToggle.id, optionToggle);
		}
		foreach (OptionSlider optionSlider in this.sliders)
		{
			this.sliderOptions.Add(optionSlider.id, optionSlider);
		}
		if (!SteelInput.IsInitialized())
		{
			SteelInput.Initialize();
			bool flag = SteelInput.LoadUserKeybinds();
			if (flag)
			{
				Debug.Log("Loaded SteelInput user keybinds");
			}
			if (!flag)
			{
				SteelInput.LoadPreset(this.defaultInputPreset);
				SteelInput.SaveConfiguration();
			}
			else
			{
				SteelInput.ReplaceMissingBindsWithPreset(this.defaultInputPreset);
			}
		}
		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			Text text = this.botnamesText;
			text.text += "ravenfield.app/Contents/botnames.txt";
		}
		else
		{
			Text text2 = this.botnamesText;
			text2.text += "ravenfield/ravenfield_Data/botnames.txt";
		}
		Options.Hide();
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0000F1D9 File Offset: 0x0000D3D9
	public void ApplyAtEndOfFrame()
	{
		base.StartCoroutine(this.ApplyAtStartup());
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
	private IEnumerator ApplyAtStartup()
	{
		yield return new WaitForEndOfFrame();
		this.Apply(true);
		yield break;
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x00091E90 File Offset: 0x00090090
	public void Save()
	{
		SteelInput.SaveConfiguration();
		bool flag = !GameManager.IsIngame() && (this.toggleOptions[OptionToggle.Id.FullScreen].valueChanged || this.resolutionOption.valueChanged);
		bool flag2 = !GameManager.IsIngame() && this.dropdownOptions[OptionDropdown.Id.Quality].valueChanged;
		OptionDropdown[] array = this.dropdowns;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Save();
		}
		OptionToggle[] array2 = this.toggles;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Save();
		}
		OptionSlider[] array3 = this.sliders;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].Save();
		}
		this.resolutionOption.Save();
		if (flag)
		{
			this.resolutionOption.Apply();
		}
		if (flag2)
		{
			Debug.Log("Applying quality change");
			QualitySettings.SetQualityLevel(Options.GetDropdown(OptionDropdown.Id.Quality), true);
		}
		this.Apply(false);
		if (GameManager.IsIngame())
		{
			Options.Hide();
			return;
		}
		this.applyFeedbackAction.Start();
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0000F1F7 File Offset: 0x0000D3F7
	private void Update()
	{
		if (Options.instance.canvas.enabled)
		{
			this.applyFeedbackText.enabled = !this.applyFeedbackAction.TrueDone();
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x00091F98 File Offset: 0x00090198
	private void Apply(bool startup)
	{
		IngameUi.showHitmarkers = Options.GetToggle(OptionToggle.Id.Hitmarkers);
		this.audioMixer.SetFloat("volume", this.VolumeDB(Options.GetSlider(OptionSlider.Id.SfxVolume)));
		this.musicAudioMixer.SetFloat("volume", this.VolumeDB(Options.GetSlider(OptionSlider.Id.MusicVolume)));
		this.musicStingAudioMixer.SetFloat("volume", this.VolumeDB(Options.GetSlider(OptionSlider.Id.MusicStingVolume)));
		Options.maxDrawDistance = ((Options.GetDropdown(OptionDropdown.Id.DrawDistance) == 0) ? 2000f : 4000f);
		SteelInput.AXIS_BUTTON_DEADZONE = Mathf.Clamp(Options.GetSlider(OptionSlider.Id.JoystickDeadzone), 0f, 0.99f);
		PostEffectsBase[] array = UnityEngine.Object.FindObjectsOfType<PostEffectsBase>();
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		AiActorController.LoadParameters(Options.GetDropdown(OptionDropdown.Id.Difficulty));
		if (startup)
		{
			this.weatherObjects = GameObject.FindGameObjectsWithTag("Weather");
		}
		if (this.weatherObjects != null)
		{
			foreach (GameObject gameObject in this.weatherObjects)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(Options.GetToggle(OptionToggle.Id.WeatherEffects));
				}
			}
		}
		SteelInput.SetJoystickBindingEnabled(Options.GetToggle(OptionToggle.Id.AllowJoystickBinds));
		MuzzleFlashManager.UpdateIntensity();
		if (startup)
		{
			foreach (Terrain terrain in UnityEngine.Object.FindObjectsOfType<Terrain>())
			{
				int dropdown = Options.GetDropdown(OptionDropdown.Id.VegetationDensity);
				int dropdown2 = Options.GetDropdown(OptionDropdown.Id.VegetationDrawDistance);
				int dropdown3 = Options.GetDropdown(OptionDropdown.Id.TerrainQuality);
				FixBundleShaders.ApplyTerrainFallbackMaterial(terrain);
				if (dropdown3 == 0)
				{
					terrain.heightmapPixelError = Mathf.Max(terrain.heightmapPixelError, 20f) * 3f;
				}
				else if (dropdown3 == 1)
				{
					terrain.heightmapPixelError = Mathf.Max(terrain.heightmapPixelError, 10f) * 2f;
				}
				else
				{
					terrain.basemapDistance = 999999f;
					terrain.drawInstanced = true;
					terrain.heightmapPixelError = Mathf.Min(terrain.heightmapPixelError, 5f);
				}
				if (dropdown == 0)
				{
					terrain.detailObjectDensity = 0f;
					terrain.detailObjectDistance = 0f;
				}
				else if (dropdown == 1)
				{
					terrain.detailObjectDensity *= 0.1f;
				}
				else if (dropdown == 2)
				{
					terrain.detailObjectDensity *= 0.6f;
				}
				if (dropdown2 == 0)
				{
					terrain.detailObjectDistance *= 0.15f;
				}
				else if (dropdown2 == 1)
				{
					terrain.detailObjectDistance *= 0.5f;
				}
			}
		}
		if (GameManager.GameParameters().bloodExplosions)
		{
			BloodParticle.BLOOD_PARTICLE_SETTING = BloodParticle.BloodParticleType.BloodExplosions;
		}
		else
		{
			BloodParticle.BLOOD_PARTICLE_SETTING = (BloodParticle.BloodParticleType)Options.GetDropdown(OptionDropdown.Id.BloodSplats);
		}
		GameManager.instance.OnOptionsApplied();
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x0000F223 File Offset: 0x0000D423
	private float VolumeDB(float volume)
	{
		return -(Mathf.Pow(80f, 1f - volume) - 1f);
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0000F23D File Offset: 0x0000D43D
	public void OnDeadzoneValueChanged()
	{
		SteelInput.AXIS_BUTTON_DEADZONE = this.deadzoneSlider.value;
	}

	// Token: 0x04001469 RID: 5225
	public const float DRAW_DISTANCE_REDUCED = 2000f;

	// Token: 0x0400146A RID: 5226
	public const float DRAW_DISTANCE_FULL = 4000f;

	// Token: 0x0400146B RID: 5227
	private const int MAX_TABS = 2;

	// Token: 0x0400146C RID: 5228
	private const int TAB_GAME = 0;

	// Token: 0x0400146D RID: 5229
	private const int TAB_INPUT = 1;

	// Token: 0x0400146E RID: 5230
	private const int TAB_VIDEO = 2;

	// Token: 0x0400146F RID: 5231
	public const int DIFFICULTY_HARD = 0;

	// Token: 0x04001470 RID: 5232
	public const int DIFFICULTY_EASY = 1;

	// Token: 0x04001471 RID: 5233
	public const int TERRAIN_MAX_QUALITY = 1;

	// Token: 0x04001472 RID: 5234
	public const int VEGETATION_DENSITY_MAX_QUALITY = 3;

	// Token: 0x04001473 RID: 5235
	public const int VEGETATION_DISTANCE_MAX_QUALITY = 2;

	// Token: 0x04001474 RID: 5236
	public static Options instance;

	// Token: 0x04001475 RID: 5237
	public static float maxDrawDistance = 4000f;

	// Token: 0x04001476 RID: 5238
	public Slider deadzoneSlider;

	// Token: 0x04001477 RID: 5239
	public Canvas gameOptions;

	// Token: 0x04001478 RID: 5240
	public Canvas inputOptions;

	// Token: 0x04001479 RID: 5241
	public Canvas videoOptions;

	// Token: 0x0400147A RID: 5242
	public RawImage background;

	// Token: 0x0400147B RID: 5243
	public UnityEngine.Audio.AudioMixer audioMixer;

	// Token: 0x0400147C RID: 5244
	public UnityEngine.Audio.AudioMixer musicAudioMixer;

	// Token: 0x0400147D RID: 5245
	public UnityEngine.Audio.AudioMixer musicStingAudioMixer;

	// Token: 0x0400147E RID: 5246
	public Text applyFeedbackText;

	// Token: 0x0400147F RID: 5247
	public SteelInputPreset defaultInputPreset;

	// Token: 0x04001480 RID: 5248
	public SteelInputPreset[] inputPresets;

	// Token: 0x04001481 RID: 5249
	private SteelInputBinder[] inputBinders;

	// Token: 0x04001482 RID: 5250
	public Dropdown inputPresetDropdown;

	// Token: 0x04001483 RID: 5251
	public Selectable[] disableIngameSelectables;

	// Token: 0x04001484 RID: 5252
	public Text botnamesText;

	// Token: 0x04001485 RID: 5253
	private Canvas[] tabCanvases;

	// Token: 0x04001486 RID: 5254
	private Canvas canvas;

	// Token: 0x04001487 RID: 5255
	private OptionDropdown[] dropdowns;

	// Token: 0x04001488 RID: 5256
	private OptionToggle[] toggles;

	// Token: 0x04001489 RID: 5257
	private OptionSlider[] sliders;

	// Token: 0x0400148A RID: 5258
	private OptionResolution resolutionOption;

	// Token: 0x0400148B RID: 5259
	private Dictionary<OptionDropdown.Id, OptionDropdown> dropdownOptions;

	// Token: 0x0400148C RID: 5260
	private Dictionary<OptionToggle.Id, OptionToggle> toggleOptions;

	// Token: 0x0400148D RID: 5261
	private Dictionary<OptionSlider.Id, OptionSlider> sliderOptions;

	// Token: 0x0400148E RID: 5262
	private TimedAction applyFeedbackAction = new TimedAction(3f, false);

	// Token: 0x0400148F RID: 5263
	private GameObject[] weatherObjects;

	// Token: 0x04001490 RID: 5264
	private Dictionary<Canvas, GraphicRaycaster> canvasRaycaster = new Dictionary<Canvas, GraphicRaycaster>();
}

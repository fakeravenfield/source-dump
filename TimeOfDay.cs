using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020001D2 RID: 466
public class TimeOfDay : MonoBehaviour
{
	// Token: 0x06000C7E RID: 3198 RVA: 0x0000A314 File Offset: 0x00008514
	private void Awake()
	{
		TimeOfDay.instance = this;
		if (this.isMapEditorScene)
		{
			this.InstantiateSkyboxes();
		}
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0000A32A File Offset: 0x0000852A
	public void InstantiateSkyboxes()
	{
		if (!this.skyboxesInstantiated)
		{
			this.skyboxesInstantiated = true;
			this.editorDayAtmosphere.InstantiateSkyboxMaterial();
			this.nightAtmosphere.InstantiateSkyboxMaterial();
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0000A353 File Offset: 0x00008553
	public Light GetSunlight()
	{
		return base.transform.Find("Day").gameObject.GetComponentInChildren<Light>();
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0000A36F File Offset: 0x0000856F
	public Light GetMoonlight()
	{
		return base.transform.Find("Night").gameObject.GetComponentInChildren<Light>();
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0000A38B File Offset: 0x0000858B
	public bool IsDay()
	{
		return base.transform.Find("Day").gameObject.activeSelf;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00079074 File Offset: 0x00077274
	public void StartGame()
	{
		try
		{
			if (GameManager.GameParameters().nightMode)
			{
				this.ApplyNight();
			}
			else if (this.isMapEditorScene)
			{
				this.ApplyMapEditorDay();
			}
			else
			{
				this.ApplyDay(null);
			}
		}
		catch (Exception)
		{
		}
		this.defaultAmbience = RenderSettings.ambientMode;
		this.lights = UnityEngine.Object.FindObjectsOfType<Light>();
		this.lightIntensity = new Dictionary<Light, float>(this.lights.Length);
		foreach (Light light in this.lights)
		{
			this.lightIntensity.Add(light, light.intensity);
		}
		try
		{
			ReflectionProber.instance.SetupProbes();
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
		try
		{
			Light componentInChildren = base.transform.Find("Day").gameObject.GetComponentInChildren<Light>();
			if (componentInChildren.type == LightType.Directional)
			{
				componentInChildren.shadowBias = 0.35f;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			Light componentInChildren2 = base.transform.Find("Night").gameObject.GetComponentInChildren<Light>();
			if (componentInChildren2.type == LightType.Directional)
			{
				componentInChildren2.shadowBias = 0.35f;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x000791BC File Offset: 0x000773BC
	public void ApplyDay(TimeOfDay.Atmosphere dayAtmosphere = null)
	{
		base.transform.Find("Day").gameObject.SetActive(true);
		base.transform.Find("Night").gameObject.SetActive(false);
		this.atmosphere = ((dayAtmosphere == null) ? TimeOfDay.Atmosphere.FromRenderSettings() : dayAtmosphere);
		if (Application.platform != RuntimePlatform.WindowsPlayer && this.atmosphere.skyboxMaterial != null)
		{
			FixBundleShaders.FixMaterialShader(this.atmosphere.skyboxMaterial);
		}
		this.SetCurrentAtmosphere(this.atmosphere);
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0000A3A7 File Offset: 0x000085A7
	public void ApplyMapEditorDay()
	{
		this.ApplyDay(this.editorDayAtmosphere);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00079248 File Offset: 0x00077448
	public void ApplyNight()
	{
		base.transform.Find("Day").gameObject.SetActive(false);
		base.transform.Find("Night").gameObject.SetActive(true);
		FixBundleShaders.FixMaterialShader(this.nightAtmosphere.skyboxMaterial);
		RenderSettings.ambientMode = AmbientMode.Trilight;
		this.SetCurrentAtmosphere(this.nightAtmosphere);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0000A3B5 File Offset: 0x000085B5
	private void SetCurrentAtmosphere(TimeOfDay.Atmosphere atmosphere)
	{
		this.atmosphere = atmosphere;
		TimeOfDay.ApplyAtmosphere(atmosphere);
		if (this.isMapEditorScene)
		{
			RenderSettings.fog = (atmosphere.fogDensity > 0f);
		}
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000792B0 File Offset: 0x000774B0
	public static void ApplyAtmosphere(TimeOfDay.Atmosphere atmosphere)
	{
		RenderSettings.ambientSkyColor = atmosphere.sky;
		RenderSettings.ambientEquatorColor = atmosphere.equator;
		RenderSettings.ambientGroundColor = atmosphere.ground;
		RenderSettings.fogColor = atmosphere.fog;
		RenderSettings.fogDensity = atmosphere.fogDensity;
		if (atmosphere.skyboxMaterial != null)
		{
			RenderSettings.skybox = new Material(atmosphere.skyboxMaterial);
		}
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00079314 File Offset: 0x00077514
	public void ApplyNightvision()
	{
		try
		{
			TimeOfDay.instance.BlendAtmosphereColor(Color.green, 0.1f, 1.1f);
			ReflectionProber.instance.SwitchToNightVision();
			RenderSettings.fogDensity *= 0.8f;
			RenderSettings.ambientMode = AmbientMode.Trilight;
			foreach (Light light in this.lights)
			{
				light.intensity = this.lightIntensity[light] * 2f + 0.2f;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000793A8 File Offset: 0x000775A8
	private void BlendAtmosphereColor(Color target, float amount, float extraExposure)
	{
		RenderSettings.ambientSkyColor = (1f + extraExposure) * Color.Lerp(this.atmosphere.sky, target, amount);
		RenderSettings.ambientEquatorColor = (1f + extraExposure) * Color.Lerp(this.atmosphere.equator, target, amount);
		RenderSettings.ambientGroundColor = (1f + extraExposure) * Color.Lerp(this.atmosphere.ground, target, amount);
		RenderSettings.fogColor = Color.Lerp(this.atmosphere.fog, target, amount);
		if (RenderSettings.skybox.HasProperty("_SkyTint"))
		{
			RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(this.atmosphere.skyboxMaterial.GetColor("_SkyTint"), target, amount));
			RenderSettings.skybox.SetFloat("_Exposure", this.atmosphere.skyboxMaterial.GetFloat("_Exposure") + extraExposure);
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00079498 File Offset: 0x00077698
	public void ResetAtmosphere()
	{
		try
		{
			this.SetCurrentAtmosphere(this.atmosphere);
			ReflectionProber.instance.Reset();
			RenderSettings.ambientMode = this.defaultAmbience;
			foreach (Light light in this.lights)
			{
				light.intensity = this.lightIntensity[light];
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000D72 RID: 3442
	private const float NIGHT_VISION_AMOUNT = 0.1f;

	// Token: 0x04000D73 RID: 3443
	private const float NIGHT_VISION_EXTRA_EXPOSURE = 1.1f;

	// Token: 0x04000D74 RID: 3444
	private const float NIGHT_VISION_LIGHT_BASE = 0.2f;

	// Token: 0x04000D75 RID: 3445
	private const float NIGHT_VISION_LIGHT_MULTIPLIER = 2f;

	// Token: 0x04000D76 RID: 3446
	private const float NIGHT_VISION_FOG_DENSITY_MULTIPLIER = 0.8f;

	// Token: 0x04000D77 RID: 3447
	public static TimeOfDay instance;

	// Token: 0x04000D78 RID: 3448
	public TimeOfDay.Atmosphere editorDayAtmosphere;

	// Token: 0x04000D79 RID: 3449
	public TimeOfDay.Atmosphere nightAtmosphere;

	// Token: 0x04000D7A RID: 3450
	[NonSerialized]
	public TimeOfDay.Atmosphere atmosphere;

	// Token: 0x04000D7B RID: 3451
	public bool testNight;

	// Token: 0x04000D7C RID: 3452
	public bool isMapEditorScene;

	// Token: 0x04000D7D RID: 3453
	private bool skyboxesInstantiated;

	// Token: 0x04000D7E RID: 3454
	private AmbientMode defaultAmbience;

	// Token: 0x04000D7F RID: 3455
	private Light[] lights;

	// Token: 0x04000D80 RID: 3456
	private Dictionary<Light, float> lightIntensity;

	// Token: 0x020001D3 RID: 467
	[Serializable]
	public class Atmosphere
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x00079508 File Offset: 0x00077708
		public static TimeOfDay.Atmosphere FromRenderSettings()
		{
			return new TimeOfDay.Atmosphere
			{
				sky = RenderSettings.ambientSkyColor,
				equator = RenderSettings.ambientEquatorColor,
				ground = RenderSettings.ambientGroundColor,
				fog = RenderSettings.fogColor,
				fogDensity = RenderSettings.fogDensity,
				skyboxMaterial = RenderSettings.skybox
			};
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0000A3DE File Offset: 0x000085DE
		public Material InstantiateSkyboxMaterial()
		{
			this.skyboxMaterial = UnityEngine.Object.Instantiate<Material>(this.skyboxMaterial);
			return this.skyboxMaterial;
		}

		// Token: 0x04000D81 RID: 3457
		public Color sky;

		// Token: 0x04000D82 RID: 3458
		public Color equator;

		// Token: 0x04000D83 RID: 3459
		public Color ground;

		// Token: 0x04000D84 RID: 3460
		public float fogDensity;

		// Token: 0x04000D85 RID: 3461
		public Color fog;

		// Token: 0x04000D86 RID: 3462
		public Material skyboxMaterial;
	}
}

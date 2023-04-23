using System;
using Lua;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AD RID: 429
public class FixBundleShaders
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000B5F RID: 2911 RVA: 0x000096AA File Offset: 0x000078AA
	private static Material fallbackTerrainMaterial
	{
		get
		{
			if (FixBundleShaders._fallbackTerrainMaterial == null)
			{
				FixBundleShaders._fallbackTerrainMaterial = Resources.Load<Material>("Fallback Terrain Material");
			}
			return FixBundleShaders._fallbackTerrainMaterial;
		}
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x000096CD File Offset: 0x000078CD
	public static void ApplyTerrainFallbackMaterial(Terrain terrain)
	{
		terrain.materialTemplate = FixBundleShaders.fallbackTerrainMaterial;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00074B38 File Offset: 0x00072D38
	public static bool ReloadAssetBundleSceneShadersIfNeeded(ModManager.EngineVersionInfo sceneBundleVersion)
	{
		if (!FixBundleShaders.LoadedContentNeedsShaderReload(sceneBundleVersion))
		{
			return true;
		}
		ScriptConsole.instance.LogInfo("Updating all material shaders in scene, platform={0}, bundleVersion={1}", new object[]
		{
			Application.platform,
			GameManager.instance.levelBundleContentInfo.versionInfo
		});
		return FixBundleShaders.ReloadAllShadersInScene();
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00074B90 File Offset: 0x00072D90
	public static bool ReloadAllShadersInScene()
	{
		Renderer[] array = UnityEngine.Object.FindObjectsOfType<Renderer>();
		bool result = true;
		foreach (Renderer renderer in array)
		{
			try
			{
				if (!FixBundleShaders.FixRendererMaterials(renderer))
				{
					result = false;
				}
			}
			catch (Exception)
			{
			}
		}
		if (!FixBundleShaders.FixMaterialShader(RenderSettings.skybox))
		{
			result = false;
		}
		foreach (Terrain terrain in UnityEngine.Object.FindObjectsOfType<Terrain>())
		{
			if (terrain.materialTemplate == null)
			{
				FixBundleShaders.ApplyTerrainFallbackMaterial(terrain);
				ScriptConsole.instance.LogInfo("Terrain {0} material overridden with {1}", new object[]
				{
					terrain.gameObject.name,
					terrain.materialTemplate
				});
			}
			else if (!FixBundleShaders.FixMaterialShader(terrain.materialTemplate))
			{
				result = false;
			}
			try
			{
				foreach (TreePrototype treePrototype in terrain.terrainData.treePrototypes)
				{
					try
					{
						foreach (Renderer renderer2 in treePrototype.prefab.GetComponentsInChildren<Renderer>())
						{
							if (renderer2.GetType() != typeof(BillboardRenderer) && !FixBundleShaders.FixRendererMaterials(renderer2))
							{
								result = false;
							}
						}
					}
					catch (Exception exception)
					{
						Debug.LogError("Could not assign tree shader. Details:");
						Debug.LogException(exception);
					}
				}
			}
			catch (Exception)
			{
			}
		}
		return result;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00074D00 File Offset: 0x00072F00
	public static bool FixRendererMaterials(Renderer renderer)
	{
		bool result = true;
		try
		{
			foreach (Material material in renderer.sharedMaterials)
			{
				if (material != null && !FixBundleShaders.FixMaterialShader(material))
				{
					result = false;
				}
			}
		}
		catch (Exception exception)
		{
			result = false;
			Debug.LogException(exception);
		}
		return result;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000096DA File Offset: 0x000078DA
	public static bool FixGraphicMaterials(Graphic graphic)
	{
		return FixBundleShaders.FixMaterialShader(graphic.material);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00074D58 File Offset: 0x00072F58
	public static bool FixMaterialShader(Material material)
	{
		if (material == null)
		{
			return false;
		}
		int renderQueue = material.renderQueue;
		GameManager.DebugVerbose("{0} render queue: {1}", new object[]
		{
			material.name,
			renderQueue
		});
		Shader shader = Shader.Find(material.shader.name);
		if (shader == null)
		{
			for (int i = 0; i < FixBundleShaders.FALLBACK_SHADER_FORMAT.Length; i++)
			{
				string text = string.Format(FixBundleShaders.FALLBACK_SHADER_FORMAT[i], material.shader.name);
				shader = Shader.Find(text);
				if (shader != null)
				{
					GameManager.DebugVerbose("Resolved shader '{0}' as '{1}'", new object[]
					{
						material.shader.name,
						text
					});
					break;
				}
			}
		}
		else
		{
			GameManager.DebugVerbose("Resolved shader '{0}'", new object[]
			{
				material.shader.name
			});
		}
		if (shader == null)
		{
			GameManager.DebugVerbose("Could not resolve shader '{0}'", new object[]
			{
				material.shader.name
			});
			return false;
		}
		material.shader = shader;
		if (renderQueue != 2000)
		{
			material.renderQueue = renderQueue;
		}
		return true;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00074E74 File Offset: 0x00073074
	public static bool FixAllRendererShadersRecursive(Transform transform)
	{
		Renderer component = transform.GetComponent<Renderer>();
		Graphic component2 = transform.GetComponent<Graphic>();
		bool flag = true;
		if (component != null)
		{
			flag = FixBundleShaders.FixRendererMaterials(component);
		}
		if (component2 != null)
		{
			flag = (FixBundleShaders.FixGraphicMaterials(component2) && flag);
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			flag = (FixBundleShaders.FixAllRendererShadersRecursive(transform.GetChild(i)) && flag);
		}
		return flag;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000096E7 File Offset: 0x000078E7
	public static bool LoadedContentNeedsShaderReload(ModManager.EngineVersionInfo bundleVersion)
	{
		return FixBundleShaders.PlatformNeedsShaderReload() || FixBundleShaders.BundleVersionRequiresShaderReload(bundleVersion);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000096F8 File Offset: 0x000078F8
	private static bool BundleVersionRequiresShaderReload(ModManager.EngineVersionInfo bundleVersion)
	{
		return bundleVersion.IsUnknown() || bundleVersion.majorVersion != ModManager.engineVersion.majorVersion;
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0000971A File Offset: 0x0000791A
	private static bool PlatformNeedsShaderReload()
	{
		return Application.platform != RuntimePlatform.WindowsPlayer;
	}

	// Token: 0x04000C8A RID: 3210
	private const int DEFAULT_SHADER_RENDER_QUEUE = 2000;

	// Token: 0x04000C8B RID: 3211
	private static readonly string[] FALLBACK_SHADER_FORMAT = new string[]
	{
		"Legacy Shaders/{0}"
	};

	// Token: 0x04000C8C RID: 3212
	private static Material _fallbackTerrainMaterial;
}

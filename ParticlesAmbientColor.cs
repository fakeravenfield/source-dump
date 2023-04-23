using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
[RequireComponent(typeof(ParticleSystem))]
public class ParticlesAmbientColor : MonoBehaviour
{
	// Token: 0x06000930 RID: 2352 RVA: 0x0006A5E4 File Offset: 0x000687E4
	private void Start()
	{
		ParticleSystem.MainModule main = base.GetComponent<ParticleSystem>().main;
		ParticleSystem.MinMaxGradient startColor = main.startColor;
		float sunlightMultiplier = this.sunlightIntensity;
		if (this.useSunlightRaycast && !GameManager.IsInSunlight(base.transform.position))
		{
			sunlightMultiplier = 0f;
		}
		startColor.color = this.BlendColor(startColor.color, sunlightMultiplier);
		startColor.colorMax = this.BlendColor(startColor.colorMax, sunlightMultiplier);
		startColor.colorMin = this.BlendColor(startColor.colorMin, sunlightMultiplier);
		main.startColor = startColor;
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0006A674 File Offset: 0x00068874
	private Color BlendColor(Color c, float sunlightMultiplier)
	{
		Color sceneAmbientColor = GameManager.instance.sceneAmbientColor;
		sceneAmbientColor.r = Mathf.Max(sceneAmbientColor.r, this.minAmbienceColor.r);
		sceneAmbientColor.g = Mathf.Max(sceneAmbientColor.g, this.minAmbienceColor.g);
		sceneAmbientColor.b = Mathf.Max(sceneAmbientColor.b, this.minAmbienceColor.b);
		Color b = c * (this.ambientIntensity * sceneAmbientColor + sunlightMultiplier * GameManager.instance.sceneSunlightColor);
		return Color.Lerp(c, b, this.weight);
	}

	// Token: 0x04000A09 RID: 2569
	public Color minAmbienceColor = new Color(0.05f, 0.05f, 0.05f);

	// Token: 0x04000A0A RID: 2570
	public float weight = 1f;

	// Token: 0x04000A0B RID: 2571
	public float ambientIntensity = 1f;

	// Token: 0x04000A0C RID: 2572
	public float sunlightIntensity = 1f;

	// Token: 0x04000A0D RID: 2573
	public bool useSunlightRaycast;
}

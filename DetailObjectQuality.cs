using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class DetailObjectQuality : MonoBehaviour
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00069888 File Offset: 0x00067A88
	private void Awake()
	{
		DetailObjectQuality.instance = this;
		Terrain component = base.GetComponent<Terrain>();
		this.maxDensity = component.detailObjectDensity;
		this.maxDistance = component.detailObjectDistance;
		this.ApplyQuality();
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00007D26 File Offset: 0x00005F26
	public void ApplyQuality()
	{
		base.GetComponent<Terrain>();
		QualitySettings.GetQualityLevel();
	}

	// Token: 0x04000995 RID: 2453
	public static DetailObjectQuality instance;

	// Token: 0x04000996 RID: 2454
	private float maxDensity;

	// Token: 0x04000997 RID: 2455
	private float maxDistance;
}

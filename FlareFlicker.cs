using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class FlareFlicker : MonoBehaviour
{
	// Token: 0x060008FC RID: 2300 RVA: 0x00069B14 File Offset: 0x00067D14
	private void Awake()
	{
		this.light = base.GetComponent<Light>();
		this.maxIntensity = this.light.intensity + this.flickerAmount;
		this.minIntensity = this.light.intensity - this.flickerAmount;
		this.startTime = Time.time;
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00069B68 File Offset: 0x00067D68
	private void Update()
	{
		float value = Time.time - this.startTime;
		float num = Mathf.InverseLerp(this.fadeOutEndTime, this.fadeOutStartTime, value) * Mathf.InverseLerp(0f, this.fadeInTime, value);
		this.light.intensity = num * Mathf.MoveTowards(this.light.intensity, UnityEngine.Random.Range(this.minIntensity, this.maxIntensity), Time.deltaTime * this.flickerSpeed);
	}

	// Token: 0x040009B5 RID: 2485
	public float flickerAmount;

	// Token: 0x040009B6 RID: 2486
	public float flickerSpeed;

	// Token: 0x040009B7 RID: 2487
	public float fadeOutStartTime = 45f;

	// Token: 0x040009B8 RID: 2488
	public float fadeOutEndTime = 55f;

	// Token: 0x040009B9 RID: 2489
	public float fadeInTime = 5f;

	// Token: 0x040009BA RID: 2490
	private float startTime;

	// Token: 0x040009BB RID: 2491
	private float maxIntensity;

	// Token: 0x040009BC RID: 2492
	private float minIntensity;

	// Token: 0x040009BD RID: 2493
	private Light light;
}

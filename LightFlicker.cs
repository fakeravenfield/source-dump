using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class LightFlicker : MonoBehaviour
{
	// Token: 0x0600091B RID: 2331 RVA: 0x00008091 File Offset: 0x00006291
	private void Awake()
	{
		this.light = base.GetComponent<Light>();
		this.maxIntensity = this.light.intensity + this.flickerAmount;
		this.minIntensity = this.light.intensity - this.flickerAmount;
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x000080CF File Offset: 0x000062CF
	private void Update()
	{
		this.light.intensity = Mathf.MoveTowards(this.light.intensity, UnityEngine.Random.Range(this.minIntensity, this.maxIntensity), Time.deltaTime * this.flickerSpeed);
	}

	// Token: 0x040009EF RID: 2543
	public float flickerAmount;

	// Token: 0x040009F0 RID: 2544
	public float flickerSpeed;

	// Token: 0x040009F1 RID: 2545
	private float maxIntensity;

	// Token: 0x040009F2 RID: 2546
	private float minIntensity;

	// Token: 0x040009F3 RID: 2547
	private Light light;
}

using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class FPLightIntensity : MonoBehaviour
{
	// Token: 0x060008EE RID: 2286 RVA: 0x00069988 File Offset: 0x00067B88
	private void OnPreCull()
	{
		if (!this.isInShadow)
		{
			return;
		}
		this.sunlight = GameManager.instance.sceneSunlight;
		if (this.sunlight != null)
		{
			this.originalColor = this.sunlight.color;
			this.sunlight.color = Color.black;
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00007DE9 File Offset: 0x00005FE9
	private void OnPostRender()
	{
		if (!this.isInShadow)
		{
			return;
		}
		if (this.sunlight != null)
		{
			this.sunlight.color = this.originalColor;
		}
	}

	// Token: 0x040009A6 RID: 2470
	private Light sunlight;

	// Token: 0x040009A7 RID: 2471
	private Color originalColor;

	// Token: 0x040009A8 RID: 2472
	[NonSerialized]
	public bool isInShadow;
}

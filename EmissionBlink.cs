using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class EmissionBlink : MonoBehaviour
{
	// Token: 0x060008E0 RID: 2272 RVA: 0x00007D35 File Offset: 0x00005F35
	private void Start()
	{
		if (EmissionBlink.EMISSION_PROPERTY_ID == 0)
		{
			EmissionBlink.EMISSION_PROPERTY_ID = Shader.PropertyToID("_EmissionColor");
		}
		this.SetEnabled(false);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x000698C0 File Offset: 0x00067AC0
	private void SetEnabled(bool e)
	{
		this.isOn = e;
		Color value = e ? this.onColor : this.offColor;
		this.target.Get().SetColor(EmissionBlink.EMISSION_PROPERTY_ID, value);
		this.blinkAction.StartLifetime(e ? this.onTime : this.offTime);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00007D54 File Offset: 0x00005F54
	private void Update()
	{
		if (this.blinkAction.TrueDone())
		{
			this.SetEnabled(!this.isOn);
		}
	}

	// Token: 0x04000998 RID: 2456
	private static int EMISSION_PROPERTY_ID;

	// Token: 0x04000999 RID: 2457
	public MaterialTarget target;

	// Token: 0x0400099A RID: 2458
	public Color onColor;

	// Token: 0x0400099B RID: 2459
	public Color offColor;

	// Token: 0x0400099C RID: 2460
	public float onTime = 1f;

	// Token: 0x0400099D RID: 2461
	public float offTime = 1f;

	// Token: 0x0400099E RID: 2462
	private TimedAction blinkAction = new TimedAction(1f, false);

	// Token: 0x0400099F RID: 2463
	private bool isOn;
}

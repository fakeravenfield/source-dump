using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000200 RID: 512
public class PulsingGraphic : MonoBehaviour
{
	// Token: 0x06000DA9 RID: 3497 RVA: 0x0000B040 File Offset: 0x00009240
	private void Awake()
	{
		this.graphic = base.GetComponent<Graphic>();
		this.baseColor = this.graphic.color;
		this.startupAction.StartLifetime(this.startupTime);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0007D160 File Offset: 0x0007B360
	private void Update()
	{
		this.baseColor.a = (1f - Mathf.Cos(this.startupAction.Ratio() * 3.1415927f / 2f)) * Mathf.Abs(Mathf.Sin(Time.time * this.pulseSpeed));
		this.graphic.color = this.baseColor;
	}

	// Token: 0x04000EBC RID: 3772
	private Graphic graphic;

	// Token: 0x04000EBD RID: 3773
	private Color baseColor;

	// Token: 0x04000EBE RID: 3774
	public float pulseSpeed = 1f;

	// Token: 0x04000EBF RID: 3775
	public float startupTime = 1f;

	// Token: 0x04000EC0 RID: 3776
	private TimedAction startupAction = new TimedAction(1f, false);
}

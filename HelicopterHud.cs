using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033B RID: 827
public class HelicopterHud : MonoBehaviour
{
	// Token: 0x0600150E RID: 5390 RVA: 0x00099AE4 File Offset: 0x00097CE4
	private void Awake()
	{
		if (this.helicopter == null)
		{
			this.helicopter = base.GetComponentInParent<Helicopter>();
		}
		try
		{
			this.autoHover.getValueDelegate = (() => FpsActorController.instance.helicopterAutoHoverEnabled);
			this.countermeasureIndicator.getValueDelegate = (() => this.helicopter.CountermeasuresAreReady());
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x00099B68 File Offset: 0x00097D68
	private void Update()
	{
		try
		{
			this.autoHover.Update();
			this.countermeasureIndicator.Update();
			Vector3 vector = this.helicopter.Velocity();
			this.horizontalSpeedText.text = HelicopterHud.FormatValueString(vector.ToGround().magnitude);
			this.verticalSpeedText.text = HelicopterHud.FormatValueString(vector.y);
			this.altitudeText.text = HelicopterHud.FormatValueString(this.helicopter.altitude);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x00099C00 File Offset: 0x00097E00
	private static string FormatValueString(float value)
	{
		return Mathf.RoundToInt(value).ToString();
	}

	// Token: 0x040016FB RID: 5883
	public Helicopter helicopter;

	// Token: 0x040016FC RID: 5884
	public PlaneHud.Indicator autoHover;

	// Token: 0x040016FD RID: 5885
	public PlaneHud.Indicator countermeasureIndicator;

	// Token: 0x040016FE RID: 5886
	public Text horizontalSpeedText;

	// Token: 0x040016FF RID: 5887
	public Text verticalSpeedText;

	// Token: 0x04001700 RID: 5888
	public Text altitudeText;
}

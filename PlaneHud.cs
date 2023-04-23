using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033E RID: 830
public class PlaneHud : MonoBehaviour, IAutoUpgradeReference
{
	// Token: 0x06001518 RID: 5400 RVA: 0x00010D0E File Offset: 0x0000EF0E
	public void UpgradeReference(Component previous, Component upgraded)
	{
		if (this.plane == previous)
		{
			this.airplane = (upgraded as Airplane);
		}
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x00099D60 File Offset: 0x00097F60
	private void Awake()
	{
		if (this.airplane == null)
		{
			this.airplane = base.GetComponentInParent<Airplane>();
		}
		try
		{
			this.gearIndicator.getValueDelegate = (() => !this.airplane.gearsRetracted);
			this.airbrakeIndicator.getValueDelegate = (() => this.airplane.airbrake);
			this.countermeasureIndicator.getValueDelegate = (() => this.airplane.CountermeasuresAreReady());
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x00099DE8 File Offset: 0x00097FE8
	private void Update()
	{
		try
		{
			this.gearIndicator.Update();
			this.airbrakeIndicator.Update();
			this.countermeasureIndicator.Update();
			this.speedLabel.text = PlaneHud.FormatValueString(Mathf.Max(0f, this.airplane.LocalVelocity().z));
			this.altitudeLabel.text = PlaneHud.FormatValueString(this.airplane.altitude);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x00099C00 File Offset: 0x00097E00
	private static string FormatValueString(float value)
	{
		return Mathf.RoundToInt(value).ToString();
	}

	// Token: 0x04001708 RID: 5896
	[HideInInspector]
	public global::Plane plane;

	// Token: 0x04001709 RID: 5897
	public Airplane airplane;

	// Token: 0x0400170A RID: 5898
	public PlaneHud.Indicator gearIndicator;

	// Token: 0x0400170B RID: 5899
	public PlaneHud.Indicator airbrakeIndicator;

	// Token: 0x0400170C RID: 5900
	public PlaneHud.Indicator countermeasureIndicator;

	// Token: 0x0400170D RID: 5901
	public Text speedLabel;

	// Token: 0x0400170E RID: 5902
	public Text altitudeLabel;

	// Token: 0x0200033F RID: 831
	// (Invoke) Token: 0x06001521 RID: 5409
	public delegate bool DelGetIndicatorValue();

	// Token: 0x02000340 RID: 832
	[Serializable]
	public struct Indicator
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00099E74 File Offset: 0x00098074
		public void Update()
		{
			bool flag = this.getValueDelegate();
			this.ready.SetActive(flag);
			if (this.waiting != null)
			{
				this.waiting.SetActive(!flag);
			}
		}

		// Token: 0x0400170F RID: 5903
		public GameObject ready;

		// Token: 0x04001710 RID: 5904
		public GameObject waiting;

		// Token: 0x04001711 RID: 5905
		public PlaneHud.DelGetIndicatorValue getValueDelegate;
	}
}

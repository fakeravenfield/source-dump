using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200070F RID: 1807
	public class SkyboxUI : WindowBase
	{
		// Token: 0x06002D43 RID: 11587 RVA: 0x00105854 File Offset: 0x00103A54
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.nightFogDensity.SetRange(0f, 1f);
			this.nightFogDensity.valueFormat = "0.00";
			this.dayFogDensity.SetRange(0f, 1f);
			this.dayFogDensity.valueFormat = "0.00";
			this.sunDirection.SetRange(0f, 360f);
			this.sunPitch.SetRange(0f, 90f);
			this.moonDirection.SetRange(0f, 360f);
			this.moonPitch.SetRange(0f, 90f);
			base.RegisterAllOnValueChangeCallbacks(new DelOnValueChangedCallback(this.OnApply));
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x00105918 File Offset: 0x00103B18
		protected override void OnShow()
		{
			base.OnShow();
			this.isLoadingValues = true;
			TimeOfDay instance = TimeOfDay.instance;
			Light sunlight = instance.GetSunlight();
			Light moonlight = instance.GetMoonlight();
			this.sunlightColor.SetColor(sunlight.color);
			this.moonlightColor.SetColor(moonlight.color);
			this.sunDirection.SetValue(sunlight.transform.eulerAngles.y);
			this.sunPitch.SetValue(sunlight.transform.eulerAngles.x);
			this.moonDirection.SetValue(moonlight.transform.eulerAngles.y);
			this.moonPitch.SetValue(moonlight.transform.eulerAngles.x);
			this.daySkyColor.SetColor(instance.editorDayAtmosphere.sky);
			this.dayEquatorColor.SetColor(instance.editorDayAtmosphere.equator);
			this.dayGroundColor.SetColor(instance.editorDayAtmosphere.ground);
			this.dayFogColor.SetColor(instance.editorDayAtmosphere.fog);
			this.dayFogDensity.SetValue(instance.editorDayAtmosphere.fogDensity / 0.05f);
			this.nightSkyColor.SetColor(instance.nightAtmosphere.sky);
			this.nightEquatorColor.SetColor(instance.nightAtmosphere.equator);
			this.nightGroundColor.SetColor(instance.nightAtmosphere.ground);
			this.nightFogColor.SetColor(instance.nightAtmosphere.fog);
			this.nightFogDensity.SetValue(instance.nightAtmosphere.fogDensity / 0.05f);
			this.isLoadingValues = false;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		protected override void OnHide()
		{
			base.OnHide();
			this.OnApply();
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00105AC4 File Offset: 0x00103CC4
		public void OnApply()
		{
			if (this.isLoadingValues)
			{
				return;
			}
			TimeOfDay instance = TimeOfDay.instance;
			Light sunlight = instance.GetSunlight();
			Light moonlight = instance.GetMoonlight();
			sunlight.color = this.sunlightColor.GetColor();
			moonlight.color = this.moonlightColor.GetColor();
			sunlight.transform.eulerAngles = new Vector3(this.sunPitch.GetValue(), this.sunDirection.GetValue(), 0f);
			moonlight.transform.eulerAngles = new Vector3(this.moonPitch.GetValue(), this.moonDirection.GetValue(), 0f);
			instance.editorDayAtmosphere.sky = this.daySkyColor.GetColor();
			instance.editorDayAtmosphere.equator = this.dayEquatorColor.GetColor();
			instance.editorDayAtmosphere.ground = this.dayGroundColor.GetColor();
			instance.editorDayAtmosphere.fog = this.dayFogColor.GetColor();
			instance.editorDayAtmosphere.fogDensity = this.dayFogDensity.GetValue() * 0.05f;
			instance.nightAtmosphere.sky = this.nightSkyColor.GetColor();
			instance.nightAtmosphere.equator = this.nightEquatorColor.GetColor();
			instance.nightAtmosphere.ground = this.nightGroundColor.GetColor();
			instance.nightAtmosphere.fog = this.nightFogColor.GetColor();
			instance.nightAtmosphere.fogDensity = this.nightFogDensity.GetValue() * 0.05f;
			MapEditor.instance.ReapplyTimeOfDay();
		}

		// Token: 0x040029A7 RID: 10663
		private const float FOG_DENSITY_MULTIPLIER = 0.05f;

		// Token: 0x040029A8 RID: 10664
		public ColorPicker sunlightColor;

		// Token: 0x040029A9 RID: 10665
		public ColorPicker moonlightColor;

		// Token: 0x040029AA RID: 10666
		public SliderWithInput sunDirection;

		// Token: 0x040029AB RID: 10667
		public SliderWithInput sunPitch;

		// Token: 0x040029AC RID: 10668
		public SliderWithInput moonDirection;

		// Token: 0x040029AD RID: 10669
		public SliderWithInput moonPitch;

		// Token: 0x040029AE RID: 10670
		public ColorPicker daySkyColor;

		// Token: 0x040029AF RID: 10671
		public ColorPicker dayEquatorColor;

		// Token: 0x040029B0 RID: 10672
		public ColorPicker dayGroundColor;

		// Token: 0x040029B1 RID: 10673
		public ColorPicker dayFogColor;

		// Token: 0x040029B2 RID: 10674
		public SliderWithInput dayFogDensity;

		// Token: 0x040029B3 RID: 10675
		public ColorPicker nightSkyColor;

		// Token: 0x040029B4 RID: 10676
		public ColorPicker nightEquatorColor;

		// Token: 0x040029B5 RID: 10677
		public ColorPicker nightGroundColor;

		// Token: 0x040029B6 RID: 10678
		public ColorPicker nightFogColor;

		// Token: 0x040029B7 RID: 10679
		public SliderWithInput nightFogDensity;

		// Token: 0x040029B8 RID: 10680
		private bool isLoadingValues;
	}
}

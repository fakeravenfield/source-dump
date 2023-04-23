using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	public struct TimeOfDayDataV1
	{
		// Token: 0x06002EAA RID: 11946 RVA: 0x00109344 File Offset: 0x00107544
		public TimeOfDayDataV1(TimeOfDay tod)
		{
			Light light = tod.GetSunlight();
			Light light2 = tod.GetMoonlight();
			this.sunlight = new ColorDataV1(light.color);
			this.moonlight = new ColorDataV1(light2.color);
			this.sunlightDirection = light.transform.eulerAngles.y;
			this.sunlightPitch = light.transform.eulerAngles.x;
			this.moonlightDirection = light2.transform.eulerAngles.y;
			this.moonlightPitch = light2.transform.eulerAngles.x;
			this.daySkyLight = new ColorDataV1(tod.editorDayAtmosphere.sky);
			this.dayEquatorLight = new ColorDataV1(tod.editorDayAtmosphere.equator);
			this.dayGroundLight = new ColorDataV1(tod.editorDayAtmosphere.ground);
			this.dayFogLight = new ColorDataV1(tod.editorDayAtmosphere.fog);
			this.dayFogDensity = tod.editorDayAtmosphere.fogDensity;
			this.nightSkyLight = new ColorDataV1(tod.nightAtmosphere.sky);
			this.nightEquatorLight = new ColorDataV1(tod.nightAtmosphere.equator);
			this.nightGroundLight = new ColorDataV1(tod.nightAtmosphere.ground);
			this.nightFogLight = new ColorDataV1(tod.nightAtmosphere.fog);
			this.nightFogDensity = tod.nightAtmosphere.fogDensity;
			this.serializedVersion = 1;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x001094B4 File Offset: 0x001076B4
		public void CopyTo(TimeOfDay tod)
		{
			Light light = tod.GetSunlight();
			Light light2 = tod.GetMoonlight();
			light.color = this.sunlight.ToColor();
			light2.color = this.moonlight.ToColor();
			tod.nightAtmosphere.sky = this.nightSkyLight.ToColor();
			tod.nightAtmosphere.equator = this.nightEquatorLight.ToColor();
			tod.nightAtmosphere.ground = this.nightGroundLight.ToColor();
			tod.nightAtmosphere.fog = this.nightFogLight.ToColor();
			tod.nightAtmosphere.fogDensity = Mathf.Clamp01(this.nightFogDensity);
			if (this.serializedVersion == 0)
			{
				tod.nightAtmosphere.fogDensity = 0f;
				tod.editorDayAtmosphere.fogDensity = 0f;
				return;
			}
			tod.editorDayAtmosphere.sky = this.daySkyLight.ToColor();
			tod.editorDayAtmosphere.equator = this.dayEquatorLight.ToColor();
			tod.editorDayAtmosphere.ground = this.dayGroundLight.ToColor();
			tod.editorDayAtmosphere.fog = this.dayFogLight.ToColor();
			tod.editorDayAtmosphere.fogDensity = Mathf.Clamp01(this.dayFogDensity);
			light.transform.eulerAngles = new Vector3(this.sunlightPitch, this.sunlightDirection, 0f);
			light2.transform.eulerAngles = new Vector3(this.moonlightPitch, this.moonlightDirection, 0f);
		}

		// Token: 0x04002AB1 RID: 10929
		public ColorDataV1 sunlight;

		// Token: 0x04002AB2 RID: 10930
		public ColorDataV1 moonlight;

		// Token: 0x04002AB3 RID: 10931
		public float sunlightPitch;

		// Token: 0x04002AB4 RID: 10932
		public float sunlightDirection;

		// Token: 0x04002AB5 RID: 10933
		public float moonlightPitch;

		// Token: 0x04002AB6 RID: 10934
		public float moonlightDirection;

		// Token: 0x04002AB7 RID: 10935
		public ColorDataV1 daySkyLight;

		// Token: 0x04002AB8 RID: 10936
		public ColorDataV1 dayEquatorLight;

		// Token: 0x04002AB9 RID: 10937
		public ColorDataV1 dayGroundLight;

		// Token: 0x04002ABA RID: 10938
		public ColorDataV1 dayFogLight;

		// Token: 0x04002ABB RID: 10939
		public float dayFogDensity;

		// Token: 0x04002ABC RID: 10940
		public ColorDataV1 nightSkyLight;

		// Token: 0x04002ABD RID: 10941
		public ColorDataV1 nightEquatorLight;

		// Token: 0x04002ABE RID: 10942
		public ColorDataV1 nightGroundLight;

		// Token: 0x04002ABF RID: 10943
		public ColorDataV1 nightFogLight;

		// Token: 0x04002AC0 RID: 10944
		public float nightFogDensity;

		// Token: 0x04002AC1 RID: 10945
		public int serializedVersion;
	}
}

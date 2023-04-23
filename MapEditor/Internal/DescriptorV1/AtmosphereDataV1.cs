using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000759 RID: 1881
	[Serializable]
	public struct AtmosphereDataV1
	{
		// Token: 0x06002EAC RID: 11948 RVA: 0x00109638 File Offset: 0x00107838
		public AtmosphereDataV1(LevelColorGrading colorGrading, TimeOfDay tod)
		{
			this.colorGradingPreset = (int)colorGrading.preset;
			this.day = new AtmosphereDataV1.SkyParameters(tod.editorDayAtmosphere.skyboxMaterial);
			this.night = new AtmosphereDataV1.SkyParameters(tod.nightAtmosphere.skyboxMaterial);
			this.serializedVersion = 1;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x00109684 File Offset: 0x00107884
		public void CopyTo(LevelColorGrading colorGrading, TimeOfDay tod)
		{
			if (this.serializedVersion == 0)
			{
				colorGrading.preset = LevelColorGrading.Preset.Default;
				return;
			}
			colorGrading.preset = (LevelColorGrading.Preset)this.colorGradingPreset;
			tod.InstantiateSkyboxes();
			this.day.CopyTo(tod.editorDayAtmosphere.skyboxMaterial);
			this.night.CopyTo(tod.nightAtmosphere.skyboxMaterial);
		}

		// Token: 0x04002AC2 RID: 10946
		public int colorGradingPreset;

		// Token: 0x04002AC3 RID: 10947
		public int serializedVersion;

		// Token: 0x04002AC4 RID: 10948
		public AtmosphereDataV1.SkyParameters day;

		// Token: 0x04002AC5 RID: 10949
		public AtmosphereDataV1.SkyParameters night;

		// Token: 0x0200075A RID: 1882
		[Serializable]
		public struct SkyParameters
		{
			// Token: 0x06002EAE RID: 11950 RVA: 0x001096E0 File Offset: 0x001078E0
			public SkyParameters(Material material)
			{
				this.sunSize = material.GetFloat("_SunSize");
				this.thickness = material.GetFloat("_AtmosphereThickness");
				this.exposure = material.GetFloat("_Exposure");
				this.tint = new ColorDataV1(material.GetColor("_SkyTint"));
				this.ground = new ColorDataV1(material.GetColor("_GroundColor"));
			}

			// Token: 0x06002EAF RID: 11951 RVA: 0x0010974C File Offset: 0x0010794C
			public void CopyTo(Material material)
			{
				material.SetFloat("_SunSize", this.sunSize);
				material.SetFloat("_AtmosphereThickness", this.thickness);
				material.SetFloat("_Exposure", this.exposure);
				material.SetColor("_SkyTint", this.tint.ToColor());
				material.SetColor("_GroundColor", this.ground.ToColor());
			}

			// Token: 0x04002AC6 RID: 10950
			public float exposure;

			// Token: 0x04002AC7 RID: 10951
			public float thickness;

			// Token: 0x04002AC8 RID: 10952
			public float sunSize;

			// Token: 0x04002AC9 RID: 10953
			public ColorDataV1 tint;

			// Token: 0x04002ACA RID: 10954
			public ColorDataV1 ground;
		}
	}
}

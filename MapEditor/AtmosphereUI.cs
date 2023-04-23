using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000694 RID: 1684
	public class AtmosphereUI : WindowBase
	{
		// Token: 0x06002AC6 RID: 10950 RVA: 0x001005D0 File Offset: 0x000FE7D0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.gradingPresets.options = new List<Dropdown.OptionData>();
			foreach (object obj in Enum.GetValues(typeof(LevelColorGrading.Preset)))
			{
				if ((int)obj >= 0)
				{
					this.gradingPresets.options.Add(new Dropdown.OptionData(obj.ToString()));
				}
			}
			this.gradingPresets.value = 0;
			this.dayInputs.InitializeSliderRanges();
			this.nightInputs.InitializeSliderRanges();
			base.RegisterAllOnValueChangeCallbacks(new DelOnValueChangedCallback(this.OnApply));
			this.gradingPresets.onValueChanged.AddListener(delegate(int p)
			{
				this.OnApply();
			});
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x001006B0 File Offset: 0x000FE8B0
		protected override void OnShow()
		{
			base.OnShow();
			this.isLoadingValues = true;
			LevelColorGrading colorGrading = MapEditorAssistant.instance.colorGrading;
			TimeOfDay timeOfDay = MapEditorAssistant.instance.timeOfDay;
			this.gradingPresets.value = (int)colorGrading.preset;
			this.dayInputs.UpdateInputs(timeOfDay.editorDayAtmosphere.skyboxMaterial);
			this.nightInputs.UpdateInputs(timeOfDay.nightAtmosphere.skyboxMaterial);
			this.isLoadingValues = false;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0001D602 File Offset: 0x0001B802
		protected override void OnHide()
		{
			base.OnHide();
			this.OnApply();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00100724 File Offset: 0x000FE924
		public void OnApply()
		{
			if (this.isLoadingValues)
			{
				return;
			}
			LevelColorGrading colorGrading = MapEditorAssistant.instance.colorGrading;
			TimeOfDay timeOfDay = MapEditorAssistant.instance.timeOfDay;
			colorGrading.preset = (LevelColorGrading.Preset)this.gradingPresets.value;
			timeOfDay.InstantiateSkyboxes();
			this.dayInputs.Apply(timeOfDay.editorDayAtmosphere.skyboxMaterial);
			this.nightInputs.Apply(timeOfDay.nightAtmosphere.skyboxMaterial);
			MapEditor.instance.ReapplyTimeOfDay();
		}

		// Token: 0x040027CB RID: 10187
		public Dropdown gradingPresets;

		// Token: 0x040027CC RID: 10188
		public AtmosphereUI.SkyUIInputs dayInputs;

		// Token: 0x040027CD RID: 10189
		public AtmosphereUI.SkyUIInputs nightInputs;

		// Token: 0x040027CE RID: 10190
		private bool isLoadingValues;

		// Token: 0x02000695 RID: 1685
		[Serializable]
		public struct SkyUIInputs : IValueChangeFieldsProvider
		{
			// Token: 0x06002ACC RID: 10956 RVA: 0x0010079C File Offset: 0x000FE99C
			public void InitializeSliderRanges()
			{
				this.exposure.SetRange(0f, 8f);
				this.thickness.SetRange(0f, 5f);
				this.sunSize.SetRange(0f, 1f);
			}

			// Token: 0x06002ACD RID: 10957 RVA: 0x001007E8 File Offset: 0x000FE9E8
			public void UpdateInputs(Material material)
			{
				this.sunSize.SetValue(material.GetFloat("_SunSize"));
				this.thickness.SetValue(material.GetFloat("_AtmosphereThickness"));
				this.exposure.SetValue(material.GetFloat("_Exposure"));
				this.skyTint.SetColor(material.GetColor("_SkyTint"));
				this.groundColor.SetColor(material.GetColor("_GroundColor"));
			}

			// Token: 0x06002ACE RID: 10958 RVA: 0x00100864 File Offset: 0x000FEA64
			public void Apply(Material material)
			{
				material.SetFloat("_SunSize", this.sunSize.GetValue());
				material.SetFloat("_AtmosphereThickness", this.thickness.GetValue());
				material.SetFloat("_Exposure", this.exposure.GetValue());
				material.SetColor("_SkyTint", this.skyTint.GetColor());
				material.SetColor("_GroundColor", this.groundColor.GetColor());
			}

			// Token: 0x040027CF RID: 10191
			public ColorPicker skyTint;

			// Token: 0x040027D0 RID: 10192
			public ColorPicker groundColor;

			// Token: 0x040027D1 RID: 10193
			public SliderWithInput exposure;

			// Token: 0x040027D2 RID: 10194
			public SliderWithInput thickness;

			// Token: 0x040027D3 RID: 10195
			public SliderWithInput sunSize;
		}
	}
}

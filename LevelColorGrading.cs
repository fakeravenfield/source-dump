using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class LevelColorGrading : MonoBehaviour
{
	// Token: 0x06000BF0 RID: 3056 RVA: 0x000772AC File Offset: 0x000754AC
	public static LevelColorGrading.GradingData GetDefaultPreset(bool isNight)
	{
		LevelColorGrading.GradingData gradingData = LevelColorGrading.PRESETS[0];
		if (!isNight)
		{
			return gradingData;
		}
		return LevelColorGrading.GetNightVersionOfPreset(gradingData);
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x000772CC File Offset: 0x000754CC
	public LevelColorGrading.GradingData GetPresetData(bool isNight)
	{
		if (this.preset != LevelColorGrading.Preset.Custom)
		{
			LevelColorGrading.GradingData result;
			try
			{
				int num = (int)this.preset;
				LevelColorGrading.GradingData gradingData = LevelColorGrading.PRESETS[num];
				if (isNight)
				{
					result = LevelColorGrading.GetNightVersionOfPreset(gradingData);
				}
				else
				{
					result = gradingData;
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = null;
			}
			return result;
		}
		if (!isNight)
		{
			return this.day;
		}
		return this.night;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00077330 File Offset: 0x00075530
	public static LevelColorGrading.GradingData GetNightVersionOfPreset(LevelColorGrading.GradingData grading)
	{
		return new LevelColorGrading.GradingData
		{
			temperature = grading.temperature - 5f,
			saturation = grading.saturation - 30f * grading.nightSaturationReductionMultiplier,
			brightness = grading.brightness - 3f,
			contrast = grading.brightness - 10f,
			bloomThreshold = grading.bloomThreshold - 0.1f,
			bloomIntensity = Mathf.Min(5f, grading.bloomIntensity * 4f)
		};
	}

	// Token: 0x04000CFB RID: 3323
	private static LevelColorGrading.GradingData[] PRESETS = new LevelColorGrading.GradingData[]
	{
		new LevelColorGrading.GradingData
		{
			brightness = -3f,
			contrast = 10f,
			bloomIntensity = 1f,
			bloomThreshold = 0.9f
		},
		new LevelColorGrading.GradingData
		{
			saturation = 30f,
			brightness = 1f,
			contrast = 8f,
			bloomIntensity = 1.1f,
			bloomThreshold = 0.8f
		},
		new LevelColorGrading.GradingData
		{
			temperature = -3f,
			saturation = -20f,
			brightness = -10f,
			contrast = 10f,
			bloomIntensity = 1.5f,
			bloomThreshold = 0.9f
		},
		new LevelColorGrading.GradingData
		{
			temperature = -5f,
			saturation = -30f,
			brightness = -40f,
			contrast = 20f,
			bloomIntensity = 2f,
			bloomThreshold = 0.9f
		},
		new LevelColorGrading.GradingData
		{
			temperature = 10f,
			saturation = 10f,
			brightness = -15f,
			contrast = 25f,
			bloomIntensity = 1.1f,
			bloomThreshold = 0.8f
		},
		new LevelColorGrading.GradingData
		{
			temperature = 40f,
			tint = 15f,
			saturation = -15f,
			brightness = -15f,
			contrast = 40f,
			bloomIntensity = 1.3f,
			bloomThreshold = 0.8f
		},
		new LevelColorGrading.GradingData
		{
			temperature = -10f,
			saturation = -5f,
			brightness = 0f,
			contrast = 10f,
			bloomIntensity = 1f,
			bloomThreshold = 0.9f
		},
		new LevelColorGrading.GradingData
		{
			temperature = -20f,
			tint = 6f,
			saturation = -15f,
			brightness = -7f,
			contrast = 30f,
			bloomIntensity = 1f,
			bloomThreshold = 0.9f
		},
		new LevelColorGrading.GradingData
		{
			temperature = -15f,
			tint = 30f,
			hueShift = 10f,
			saturation = 10f,
			brightness = -10f,
			contrast = 10f,
			bloomIntensity = 1.3f,
			bloomThreshold = 0.8f,
			nightSaturationReductionMultiplier = 0.1f
		},
		new LevelColorGrading.GradingData
		{
			temperature = 0f,
			tint = 0f,
			hueShift = 80f,
			saturation = 50f,
			brightness = 0f,
			contrast = 15f,
			bloomIntensity = 2f,
			bloomThreshold = 0.8f,
			nightSaturationReductionMultiplier = 0.1f
		}
	};

	// Token: 0x04000CFC RID: 3324
	public LevelColorGrading.Preset preset;

	// Token: 0x04000CFD RID: 3325
	public LevelColorGrading.GradingData day;

	// Token: 0x04000CFE RID: 3326
	public LevelColorGrading.GradingData night;

	// Token: 0x020001B7 RID: 439
	public enum Preset
	{
		// Token: 0x04000D00 RID: 3328
		Custom = -1,
		// Token: 0x04000D01 RID: 3329
		Default,
		// Token: 0x04000D02 RID: 3330
		Bright,
		// Token: 0x04000D03 RID: 3331
		Muted,
		// Token: 0x04000D04 RID: 3332
		Dark,
		// Token: 0x04000D05 RID: 3333
		HotSand,
		// Token: 0x04000D06 RID: 3334
		ScorchingFire,
		// Token: 0x04000D07 RID: 3335
		CoolIce,
		// Token: 0x04000D08 RID: 3336
		FrozenSolid,
		// Token: 0x04000D09 RID: 3337
		Moody,
		// Token: 0x04000D0A RID: 3338
		Trippy
	}

	// Token: 0x020001B8 RID: 440
	[Serializable]
	public class GradingData
	{
		// Token: 0x04000D0B RID: 3339
		[Range(-100f, 100f)]
		public float temperature;

		// Token: 0x04000D0C RID: 3340
		[Range(-100f, 100f)]
		public float tint;

		// Token: 0x04000D0D RID: 3341
		[Range(-100f, 100f)]
		public float hueShift;

		// Token: 0x04000D0E RID: 3342
		[Range(-100f, 100f)]
		public float saturation;

		// Token: 0x04000D0F RID: 3343
		[Range(-100f, 100f)]
		public float brightness;

		// Token: 0x04000D10 RID: 3344
		[Range(-100f, 100f)]
		public float contrast;

		// Token: 0x04000D11 RID: 3345
		public float bloomIntensity = 1f;

		// Token: 0x04000D12 RID: 3346
		public float bloomThreshold = 0.9f;

		// Token: 0x04000D13 RID: 3347
		[NonSerialized]
		public float nightSaturationReductionMultiplier = 1f;
	}
}

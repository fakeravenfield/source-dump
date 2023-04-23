using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C5 RID: 709
public class OptionDropdown : MonoBehaviour
{
	// Token: 0x060012EB RID: 4843 RVA: 0x0000EF27 File Offset: 0x0000D127
	private void Start()
	{
		this.uiElement = base.GetComponentInChildren<Dropdown>();
		this.Load();
		this.uiElement.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChange));
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0000EF58 File Offset: 0x0000D158
	public void Save()
	{
		PlayerPrefs.SetInt("d_" + this.id.ToString(), this.value);
		this.valueChanged = false;
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x0000EF87 File Offset: 0x0000D187
	protected virtual void OnValueChange(int newValue)
	{
		this.value = newValue;
		this.valueChanged = true;
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x0000EF97 File Offset: 0x0000D197
	private void Load()
	{
		this.value = PlayerPrefs.GetInt("d_" + this.id.ToString(), this.defaultValue);
		this.uiElement.value = this.value;
	}

	// Token: 0x04001429 RID: 5161
	private const string KEY_PREFIX = "d_";

	// Token: 0x0400142A RID: 5162
	public OptionDropdown.Id id;

	// Token: 0x0400142B RID: 5163
	public int defaultValue;

	// Token: 0x0400142C RID: 5164
	[NonSerialized]
	public int value;

	// Token: 0x0400142D RID: 5165
	[NonSerialized]
	public bool valueChanged;

	// Token: 0x0400142E RID: 5166
	private Dropdown uiElement;

	// Token: 0x020002C6 RID: 710
	public enum Id
	{
		// Token: 0x04001430 RID: 5168
		Quality,
		// Token: 0x04001431 RID: 5169
		TerrainQuality,
		// Token: 0x04001432 RID: 5170
		VegetationDensity,
		// Token: 0x04001433 RID: 5171
		VegetationDrawDistance,
		// Token: 0x04001434 RID: 5172
		Difficulty,
		// Token: 0x04001435 RID: 5173
		BotNames,
		// Token: 0x04001436 RID: 5174
		DrawDistance,
		// Token: 0x04001437 RID: 5175
		Bloom,
		// Token: 0x04001438 RID: 5176
		AmbientOcclusionQuality_UNUSED,
		// Token: 0x04001439 RID: 5177
		AntiAliasing,
		// Token: 0x0400143A RID: 5178
		MuzzleFlashLight,
		// Token: 0x0400143B RID: 5179
		BloodSplats
	}
}

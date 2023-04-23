using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002CA RID: 714
public class OptionToggle : MonoBehaviour
{
	// Token: 0x060012FB RID: 4859 RVA: 0x0000F08E File Offset: 0x0000D28E
	private void Start()
	{
		this.uiElement = base.GetComponentInChildren<Toggle>();
		this.Load();
		this.uiElement.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChange));
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x0000F0BF File Offset: 0x0000D2BF
	public void Save()
	{
		PlayerPrefs.SetInt("t_" + this.id.ToString(), this.value ? 1 : 0);
		this.valueChanged = false;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
	protected virtual void OnValueChange(bool newValue)
	{
		this.value = newValue;
		this.valueChanged = true;
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00091B18 File Offset: 0x0008FD18
	private void Load()
	{
		this.value = (PlayerPrefs.GetInt("t_" + this.id.ToString(), this.defaultValue ? 1 : 0) == 1);
		this.uiElement.isOn = this.value;
	}

	// Token: 0x04001451 RID: 5201
	private const string KEY_PREFIX = "t_";

	// Token: 0x04001452 RID: 5202
	public OptionToggle.Id id;

	// Token: 0x04001453 RID: 5203
	public bool defaultValue;

	// Token: 0x04001454 RID: 5204
	[NonSerialized]
	public bool value;

	// Token: 0x04001455 RID: 5205
	[NonSerialized]
	public bool valueChanged;

	// Token: 0x04001456 RID: 5206
	private Toggle uiElement;

	// Token: 0x020002CB RID: 715
	public enum Id
	{
		// Token: 0x04001458 RID: 5208
		Hitmarkers,
		// Token: 0x04001459 RID: 5209
		AutoReload,
		// Token: 0x0400145A RID: 5210
		ToggleAim,
		// Token: 0x0400145B RID: 5211
		ToggleCrouch,
		// Token: 0x0400145C RID: 5212
		ControlHints,
		// Token: 0x0400145D RID: 5213
		FullScreen,
		// Token: 0x0400145E RID: 5214
		WeatherEffects,
		// Token: 0x0400145F RID: 5215
		HDR,
		// Token: 0x04001460 RID: 5216
		ColorCorrection,
		// Token: 0x04001461 RID: 5217
		AllowJoystickBinds,
		// Token: 0x04001462 RID: 5218
		ClothPhysics,
		// Token: 0x04001463 RID: 5219
		AutoLoadLastVehicleMod,
		// Token: 0x04001464 RID: 5220
		KillIndicator,
		// Token: 0x04001465 RID: 5221
		VSync,
		// Token: 0x04001466 RID: 5222
		NeverendingBattles,
		// Token: 0x04001467 RID: 5223
		AmbientOcclusion,
		// Token: 0x04001468 RID: 5224
		ToggleSprint
	}
}

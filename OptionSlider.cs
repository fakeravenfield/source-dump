using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C8 RID: 712
public class OptionSlider : MonoBehaviour
{
	// Token: 0x060012F6 RID: 4854 RVA: 0x0000EFDF File Offset: 0x0000D1DF
	private void Start()
	{
		this.uiElement = base.GetComponentInChildren<Slider>();
		this.Load();
		this.uiElement.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChange));
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x0000F010 File Offset: 0x0000D210
	public void Save()
	{
		PlayerPrefs.SetFloat("s_" + this.id.ToString(), this.value);
		this.valueChanged = false;
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x0000F03F File Offset: 0x0000D23F
	protected virtual void OnValueChange(float newValue)
	{
		this.value = newValue;
		this.valueChanged = true;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0000F04F File Offset: 0x0000D24F
	private void Load()
	{
		this.value = PlayerPrefs.GetFloat("s_" + this.id.ToString(), this.defaultValue);
		this.uiElement.value = this.value;
	}

	// Token: 0x04001440 RID: 5184
	public const string KEY_PREFIX = "s_";

	// Token: 0x04001441 RID: 5185
	public OptionSlider.Id id;

	// Token: 0x04001442 RID: 5186
	public float defaultValue;

	// Token: 0x04001443 RID: 5187
	[NonSerialized]
	public float value;

	// Token: 0x04001444 RID: 5188
	[NonSerialized]
	public bool valueChanged;

	// Token: 0x04001445 RID: 5189
	private Slider uiElement;

	// Token: 0x020002C9 RID: 713
	public enum Id
	{
		// Token: 0x04001447 RID: 5191
		SfxVolume,
		// Token: 0x04001448 RID: 5192
		MusicVolume,
		// Token: 0x04001449 RID: 5193
		Fov,
		// Token: 0x0400144A RID: 5194
		MouseSensitivity,
		// Token: 0x0400144B RID: 5195
		SniperSensitivity,
		// Token: 0x0400144C RID: 5196
		HelicopterSensitivity,
		// Token: 0x0400144D RID: 5197
		PlaneSensitivity,
		// Token: 0x0400144E RID: 5198
		VehicleFov,
		// Token: 0x0400144F RID: 5199
		MusicStingVolume,
		// Token: 0x04001450 RID: 5200
		JoystickDeadzone
	}
}

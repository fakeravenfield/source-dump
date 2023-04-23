using System;

// Token: 0x020002C4 RID: 708
public class AllowJoystickBinds : OptionToggle
{
	// Token: 0x060012E9 RID: 4841 RVA: 0x0000EF09 File Offset: 0x0000D109
	protected override void OnValueChange(bool newValue)
	{
		base.OnValueChange(newValue);
		if (SteelInput.IsInitialized())
		{
			SteelInput.SetJoystickBindingEnabled(newValue);
		}
	}
}

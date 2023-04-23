using System;

// Token: 0x02000301 RID: 769
public struct PlayerActionInput
{
	// Token: 0x06001417 RID: 5143 RVA: 0x0001001F File Offset: 0x0000E21F
	public PlayerActionInput(SteelInput.KeyBinds input, OptionToggle.Id toggle)
	{
		this.input = input;
		this.toggle = toggle;
		this.forceToggle = false;
		this.useToggle = false;
		this.justToggled = false;
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x00010044 File Offset: 0x0000E244
	public PlayerActionInput(SteelInput.KeyBinds input)
	{
		this.input = input;
		this.toggle = OptionToggle.Id.AllowJoystickBinds;
		this.forceToggle = true;
		this.useToggle = false;
		this.justToggled = false;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x00095590 File Offset: 0x00093790
	public void Update()
	{
		this.justToggled = false;
		if ((this.forceToggle || Options.GetToggle(this.toggle)) && SteelInput.GetButtonDown(this.input))
		{
			this.useToggle = !this.useToggle;
			this.justToggled = true;
		}
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0001006A File Offset: 0x0000E26A
	public bool GetInput()
	{
		if (this.forceToggle || Options.GetToggle(this.toggle))
		{
			return this.useToggle;
		}
		return SteelInput.GetButton(this.input);
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x00010093 File Offset: 0x0000E293
	public void Reset()
	{
		this.useToggle = false;
	}

	// Token: 0x0400158D RID: 5517
	private SteelInput.KeyBinds input;

	// Token: 0x0400158E RID: 5518
	private OptionToggle.Id toggle;

	// Token: 0x0400158F RID: 5519
	private bool forceToggle;

	// Token: 0x04001590 RID: 5520
	public bool useToggle;

	// Token: 0x04001591 RID: 5521
	public bool justToggled;
}

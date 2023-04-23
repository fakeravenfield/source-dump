using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class ToggleableItem : Weapon
{
	// Token: 0x060007AE RID: 1966 RVA: 0x00064334 File Offset: 0x00062534
	public virtual void Toggle(bool ignoreCooldown = false)
	{
		if (ignoreCooldown || this.toggleAction.TrueDone())
		{
			this.isToggledOn = !this.isToggledOn;
			this.toggleAction.StartLifetime(this.cooldown);
			if (this.isToggledOn)
			{
				this.ToggleEnable();
				return;
			}
			this.ToggleDisable();
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00006F63 File Offset: 0x00005163
	public override void Drop()
	{
		if (this.isToggledOn)
		{
			this.ToggleDisable();
		}
		base.Drop();
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00006F79 File Offset: 0x00005179
	public virtual void ToggleEnable()
	{
		this.isToggledOn = true;
		this.activatedObject.SetActive(true);
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00006F8E File Offset: 0x0000518E
	public virtual void ToggleDisable()
	{
		this.isToggledOn = false;
		this.activatedObject.SetActive(false);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void CullFpsObjects()
	{
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool IsToggleable()
	{
		return true;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void Unholster()
	{
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void Holster()
	{
	}

	// Token: 0x040007C5 RID: 1989
	public GameObject activatedObject;

	// Token: 0x040007C6 RID: 1990
	public float cooldown = 0.5f;

	// Token: 0x040007C7 RID: 1991
	[NonSerialized]
	public bool isToggledOn;

	// Token: 0x040007C8 RID: 1992
	protected TimedAction toggleAction = new TimedAction(0f, false);
}

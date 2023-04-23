using System;

// Token: 0x02000101 RID: 257
public class ShellLoadedWeapon : Weapon
{
	// Token: 0x0600078F RID: 1935 RVA: 0x00063A38 File Offset: 0x00061C38
	private void NewShell()
	{
		this.ammo++;
		this.RemoveSpareAmmo(1);
		if (this.ammo >= this.configuration.ammo || !base.HasSpareAmmo())
		{
			if (this.animator != null)
			{
				this.animator.SetTrigger("reloadDone");
			}
			base.Invoke("ReloadDone", this.configuration.reloadTime);
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00063AAC File Offset: 0x00061CAC
	public override void Reload(bool overrideHolstered)
	{
		if ((!this.unholstered && !overrideHolstered) || this.reloading)
		{
			return;
		}
		if (this.animator != null)
		{
			this.animator.ResetTrigger("reloadDone");
			base.Reload(false);
			base.CancelInvoke("ReloadDone");
			return;
		}
		this.configuration.reloadTime = this.aiReloadTimePerShell * (float)(this.configuration.ammo - this.ammo);
		base.Reload(false);
	}

	// Token: 0x040007A2 RID: 1954
	public float aiReloadTimePerShell = 0.4f;
}

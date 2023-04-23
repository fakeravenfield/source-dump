using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class Wrench : MeleeWeapon
{
	// Token: 0x06000881 RID: 2177 RVA: 0x00068860 File Offset: 0x00066A60
	protected override void HitVehicle(Vector3 direction, RaycastHit hitInfo)
	{
		try
		{
			Vehicle componentInParent = hitInfo.collider.GetComponentInParent<Vehicle>();
			if (componentInParent.HasDriver() && componentInParent.ownerTeam != base.user.team)
			{
				DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Melee, base.user, this)
				{
					healthDamage = this.repairHealth
				};
				componentInParent.Damage(info);
			}
			else
			{
				if (componentInParent.Repair(this.repairHealth))
				{
					this.audio.PlayOneShot(this.repairSound);
				}
				if (!base.user.aiControlled)
				{
					IngameUi.instance.FlashVehicleBar(componentInParent.GetHealthRatio());
				}
			}
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0000476F File Offset: 0x0000296F
	public override bool CanRepair()
	{
		return true;
	}

	// Token: 0x04000924 RID: 2340
	public float repairHealth = 50f;

	// Token: 0x04000925 RID: 2341
	public AudioClip repairSound;
}

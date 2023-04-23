using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class SecretWeaponTrigger : MonoBehaviour
{
	// Token: 0x0600107D RID: 4221 RVA: 0x00089778 File Offset: 0x00087978
	private void Start()
	{
		if (GameManager.PlayerTeam() == -1)
		{
			base.transform.parent.gameObject.SetActive(false);
		}
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (weaponEntry.name == this.weaponName)
			{
				this.entry = weaponEntry;
			}
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0000D2A6 File Offset: 0x0000B4A6
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == 9)
		{
			this.UnlockEntry(this.entry);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00089800 File Offset: 0x00087A00
	private void UnlockEntry(WeaponManager.WeaponEntry entry)
	{
		if (this.secretSound != null)
		{
			this.secretSound.Play();
		}
		WeaponManager.UnlockSecretHalloweenWeapon();
		OverlayUi.ShowOverlayText("YOU FOUND THE <color=#ff2222ff>" + entry.name + "</color>", 3.5f);
		FpsActorController.instance.actor.EquipNewWeaponEntry(entry, this.autoEquipSlot, true);
	}

	// Token: 0x0400119C RID: 4508
	private const int PLAYER_LAYER = 9;

	// Token: 0x0400119D RID: 4509
	public string weaponName = "";

	// Token: 0x0400119E RID: 4510
	public int autoEquipSlot;

	// Token: 0x0400119F RID: 4511
	public AudioSource secretSound;

	// Token: 0x040011A0 RID: 4512
	public WeaponManager.WeaponEntry entry;
}

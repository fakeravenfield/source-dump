using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class sswt : MonoBehaviour
{
	// Token: 0x0600108E RID: 4238 RVA: 0x0008A18C File Offset: 0x0008838C
	private void Start()
	{
		if (GameManager.PlayerTeam() == -1)
		{
			base.transform.parent.gameObject.SetActive(false);
		}
		using (List<WeaponManager.WeaponEntry>.Enumerator enumerator = WeaponManager.instance.allWeapons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == this.entry.name)
				{
					base.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0000D347 File Offset: 0x0000B547
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == 9)
		{
			this.UnlockEntry(this.entry);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0008A21C File Offset: 0x0008841C
	private void UnlockEntry(WeaponManager.WeaponEntry entry)
	{
		this.secretSound.Play();
		WeaponManager.instance.allWeapons.Add(entry);
		OverlayUi.ShowOverlayText("YOU FOUND THE <color=#2299ffff>" + entry.name + "</color>", 3.5f);
		FpsActorController.instance.actor.EquipNewWeaponEntry(entry, this.autoEquipSlot, true);
		bool hidden = entry.hidden;
		entry.hidden = false;
	}

	// Token: 0x040011C9 RID: 4553
	private const int PLAYER_LAYER = 9;

	// Token: 0x040011CA RID: 4554
	public int autoEquipSlot;

	// Token: 0x040011CB RID: 4555
	public AudioSource secretSound;

	// Token: 0x040011CC RID: 4556
	public WeaponManager.WeaponEntry entry;
}

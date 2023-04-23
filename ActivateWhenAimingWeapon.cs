using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class ActivateWhenAimingWeapon : MonoBehaviour
{
	// Token: 0x06001504 RID: 5380 RVA: 0x000998F8 File Offset: 0x00097AF8
	private void LateUpdate()
	{
		try
		{
			this.objectToActivate.SetActive((this.weapon.unholstered && this.weapon.aiming) != this.invertActivation);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x040016E6 RID: 5862
	public Weapon weapon;

	// Token: 0x040016E7 RID: 5863
	public bool invertActivation;

	// Token: 0x040016E8 RID: 5864
	public GameObject objectToActivate;
}

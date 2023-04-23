using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DF RID: 223
[RequireComponent(typeof(Text))]
public class AmmoCountUiText : MonoBehaviour
{
	// Token: 0x060006B7 RID: 1719 RVA: 0x000064F0 File Offset: 0x000046F0
	private void Start()
	{
		this.text = base.GetComponent<Text>();
		if (this.weapon == null)
		{
			this.weapon = base.GetComponentInParent<Weapon>();
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0005FF10 File Offset: 0x0005E110
	private void LateUpdate()
	{
		if (this.spareAmmo)
		{
			this.text.text = this.weapon.spareAmmo.ToString();
			return;
		}
		this.text.text = this.weapon.ammo.ToString();
	}

	// Token: 0x04000699 RID: 1689
	public bool spareAmmo;

	// Token: 0x0400069A RID: 1690
	private Text text;

	// Token: 0x0400069B RID: 1691
	public Weapon weapon;
}

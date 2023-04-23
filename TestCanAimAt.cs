using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class TestCanAimAt : MonoBehaviour
{
	// Token: 0x06001132 RID: 4402 RVA: 0x0008BA18 File Offset: 0x00089C18
	private void Update()
	{
		if (this.weapon != null)
		{
			Debug.DrawLine(this.weapon.CurrentMuzzle().position, base.transform.position, this.weapon.CanAimAt(base.transform.position) ? Color.green : Color.red);
		}
	}

	// Token: 0x04001272 RID: 4722
	public MountedWeapon weapon;
}

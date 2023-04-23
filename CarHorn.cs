using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class CarHorn : MountedWeapon
{
	// Token: 0x060007B9 RID: 1977 RVA: 0x00006FDA File Offset: 0x000051DA
	protected override bool Shoot(Vector3 direction, bool useMuzzleDirection)
	{
		if (this.configuration.loud)
		{
			base.user.Highlight(4f);
		}
		this.audio.Play();
		this.lastFiredTimestamp = Time.time;
		return true;
	}
}

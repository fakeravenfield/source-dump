using System;
using System.Collections;
using LevelSystem;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class StrandedUfo : KickActivator
{
	// Token: 0x06000C75 RID: 3189 RVA: 0x0000A2C5 File Offset: 0x000084C5
	public override void Trigger()
	{
		base.Trigger();
		GameManager.triggeredUfo = true;
		if (UnityEngine.Object.FindObjectOfType<Ufo>() == null)
		{
			base.StartCoroutine(this.Attack());
		}
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0000A2ED File Offset: 0x000084ED
	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(15f);
		GameManager.instance.UfoAttack();
		yield break;
	}
}

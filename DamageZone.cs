using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class DamageZone : MonoBehaviour
{
	// Token: 0x06000B53 RID: 2899 RVA: 0x0000964C File Offset: 0x0000784C
	private void OnEnable()
	{
		base.StartCoroutine(this.Register());
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x0000965B File Offset: 0x0000785B
	private IEnumerator Register()
	{
		yield return new WaitForSeconds(0.1f);
		ActorManager.RegisterDamageZone(this);
		yield break;
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x0000966A File Offset: 0x0000786A
	private void OnDisable()
	{
		ActorManager.DropDamageZone(this);
	}

	// Token: 0x04000C85 RID: 3205
	public float damagePerSecond = 1000f;

	// Token: 0x04000C86 RID: 3206
	public float balanceDamagePerSecond;
}

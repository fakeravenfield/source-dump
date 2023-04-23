using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class Hurtable : MonoBehaviour
{
	// Token: 0x0600070A RID: 1802 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool Damage(DamageInfo info)
	{
		return false;
	}

	// Token: 0x0400071B RID: 1819
	public int team;
}

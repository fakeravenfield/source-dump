using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class ResupplyCrate : MonoBehaviour
{
	// Token: 0x06000C13 RID: 3091 RVA: 0x00009F3C File Offset: 0x0000813C
	private void Start()
	{
		base.InvokeRepeating("Resupply", UnityEngine.Random.Range(0f, 3f), 3f);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x000058DF File Offset: 0x00003ADF
	private void OnDisable()
	{
		base.CancelInvoke();
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00077C18 File Offset: 0x00075E18
	private void Resupply()
	{
		if (ActorManager.instance.actors == null)
		{
			return;
		}
		foreach (Actor actor in ActorManager.ActorsInRange(base.transform.position, this.range))
		{
			actor.ResupplyAmmo();
			actor.ResupplyHealth();
			actor.MarkAtResupplyCrate();
		}
	}

	// Token: 0x04000D24 RID: 3364
	public float range = 3f;

	// Token: 0x04000D25 RID: 3365
	public const float RESUPPLY_PERIOD_TIME = 3f;
}

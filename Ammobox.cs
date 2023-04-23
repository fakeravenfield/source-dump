using System;

// Token: 0x020000DD RID: 221
public class Ammobox : RigidbodyProjectile
{
	// Token: 0x060006B2 RID: 1714 RVA: 0x000064C3 File Offset: 0x000046C3
	protected override void Awake()
	{
		base.Awake();
		base.InvokeRepeating("Resupply", 3f, 3f);
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0005FE3C File Offset: 0x0005E03C
	private void Resupply()
	{
		foreach (Actor actor in ActorManager.AliveActorsInRange(base.transform.position, 6f))
		{
			actor.ResupplyAmmo();
		}
	}

	// Token: 0x04000697 RID: 1687
	private const float RESUPPLY_RATE = 3f;

	// Token: 0x04000698 RID: 1688
	private const float RESUPPLY_RANGE = 6f;
}

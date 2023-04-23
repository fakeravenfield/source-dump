using System;
using System.Collections.Generic;

// Token: 0x020000EE RID: 238
public class Medipack : RigidbodyProjectile
{
	// Token: 0x0600070C RID: 1804 RVA: 0x000064C3 File Offset: 0x000046C3
	protected override void Awake()
	{
		base.Awake();
		base.InvokeRepeating("Resupply", 3f, 3f);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00061470 File Offset: 0x0005F670
	private void Resupply()
	{
		using (List<Actor>.Enumerator enumerator = ActorManager.AliveActorsInRange(base.transform.position, 6f).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ResupplyHealth())
				{
					this.expireTime -= this.reducedLifetimePerResupply;
				}
			}
		}
	}

	// Token: 0x0400071C RID: 1820
	private const float RESUPPLY_RATE = 3f;

	// Token: 0x0400071D RID: 1821
	private const float RESUPPLY_RANGE = 6f;

	// Token: 0x0400071E RID: 1822
	public float reducedLifetimePerResupply = 5f;
}

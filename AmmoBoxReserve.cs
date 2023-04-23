using System;

// Token: 0x0200024A RID: 586
public class AmmoBoxReserve : ItemPickup
{
	// Token: 0x0600102A RID: 4138 RVA: 0x0000CF6E File Offset: 0x0000B16E
	protected override bool Pickup()
	{
		return FpsActorController.instance.actor.ResupplyAmmo();
	}
}

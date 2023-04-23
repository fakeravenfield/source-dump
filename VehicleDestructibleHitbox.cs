using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class VehicleDestructibleHitbox : Destructible
{
	// Token: 0x060014FB RID: 5371 RVA: 0x00010BE5 File Offset: 0x0000EDE5
	private void Awake()
	{
		this.vehicle = base.GetComponentInParent<Vehicle>();
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00010BF3 File Offset: 0x0000EDF3
	public override void Damage(DamageInfo info)
	{
		if (this.propagateDamage)
		{
			this.vehicle.Damage(info);
		}
		if (this.canBeDestroyed)
		{
			base.Damage(info);
		}
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x00010C18 File Offset: 0x0000EE18
	protected override void SetupActivatedRigidbody(Rigidbody rigidbody)
	{
		rigidbody.velocity = this.vehicle.rigidbody.velocity;
		base.SetupActivatedRigidbody(rigidbody);
	}

	// Token: 0x040016E1 RID: 5857
	public bool canBeDestroyed;

	// Token: 0x040016E2 RID: 5858
	public bool propagateDamage = true;

	// Token: 0x040016E3 RID: 5859
	[NonSerialized]
	public Vehicle vehicle;
}

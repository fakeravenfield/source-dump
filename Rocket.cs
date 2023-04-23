using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
[RequireComponent(typeof(Light))]
public class Rocket : ExplodingProjectile
{
	// Token: 0x0600078A RID: 1930 RVA: 0x00006809 File Offset: 0x00004A09
	protected override void AssignArmorDamage()
	{
		this.armorDamage = Vehicle.ArmorRating.AntiTank;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00006D9A File Offset: 0x00004F9A
	protected override void Awake()
	{
		base.Awake();
		this.light = base.GetComponent<Light>();
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00006DAE File Offset: 0x00004FAE
	public override void StartTravelling()
	{
		base.StartTravelling();
		if (this.light != null)
		{
			this.light.enabled = true;
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00006DD0 File Offset: 0x00004FD0
	protected override bool Explode(Vector3 position, Vector3 up)
	{
		if (this.light != null)
		{
			this.light.enabled = false;
		}
		return base.Explode(position, up);
	}

	// Token: 0x040007A1 RID: 1953
	protected Light light;
}

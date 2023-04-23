using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class Airhorn : MeleeWeapon
{
	// Token: 0x060006B0 RID: 1712 RVA: 0x0005FCF8 File Offset: 0x0005DEF8
	protected override void ShootMeleeRay()
	{
		foreach (Actor actor in ActorManager.AliveActorsInRange(this.CurrentMuzzle().position, this.range))
		{
			Vector3 vector = actor.CenterPosition() - this.CurrentMuzzle().position;
			Vector3 normalized = vector.normalized;
			Ray ray = new Ray(this.CurrentMuzzle().position, normalized);
			if (actor != base.user && Vector3.Dot(normalized.normalized, this.CurrentMuzzle().forward) > this.dotFov && !Physics.Raycast(ray, vector.magnitude, 1))
			{
				DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Melee, base.user, this)
				{
					healthDamage = this.damage,
					balanceDamage = this.balanceDamage,
					point = actor.CenterPosition() - normalized * 0.5f,
					direction = normalized,
					impactForce = normalized * this.force
				};
				actor.Damage(info);
			}
		}
	}

	// Token: 0x04000695 RID: 1685
	private const int STOP_RAY_MASK = 1;

	// Token: 0x04000696 RID: 1686
	public float dotFov = 0.7f;
}

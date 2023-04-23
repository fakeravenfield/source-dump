using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class ProximityFuzeProjectile : ExplodingProjectile
{
	// Token: 0x06000761 RID: 1889 RVA: 0x00006BAA File Offset: 0x00004DAA
	protected override void Awake()
	{
		this.useInfantryTrigger = this.allowedTargetTypes.Contains(Actor.TargetType.Infantry);
		base.Awake();
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00062EBC File Offset: 0x000610BC
	protected override void Update()
	{
		base.Update();
		if (base.enabled)
		{
			if (this.travelDistance > this.autoExplodeDistance)
			{
				base.transform.position += this.velocity.normalized * UnityEngine.Random.Range(-1f, 1f) * this.distanceWobbleGain * this.detonationDistance;
				this.Explode(base.transform.position, this.velocity);
				return;
			}
			foreach (Vehicle vehicle in ActorManager.instance.vehicles)
			{
				if ((this.allowAllTargets || this.allowedTargetTypes.Contains(vehicle.targetType)) && (!(this.killCredit != null) || !this.killCredit.IsSeated() || !(this.killCredit.seat.vehicle == vehicle)) && this.CheckProximityDetonation(vehicle.transform.position))
				{
					return;
				}
			}
			if (this.useInfantryTrigger)
			{
				foreach (Actor actor in ActorManager.instance.actors)
				{
					if (!actor.dead && this.CheckProximityDetonation(actor.CenterPosition()))
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00063054 File Offset: 0x00061254
	private bool CheckProximityDetonation(Vector3 position)
	{
		Vector3 a = base.transform.position - this.velocity * Time.deltaTime;
		Vector3 vector = this.velocity * 2f * Time.deltaTime;
		float num = SMath.LineSegmentVsPointClosestT(a, a + vector, position);
		if (num > 0f && num < 1f)
		{
			Vector3 vector2 = a + vector * num;
			float num2 = Vector3.Distance(vector2, position);
			if (this.travelDistance > this.armDistance && num2 < this.detonationDistance)
			{
				base.transform.position = vector2 + this.velocity.normalized * num2 * UnityEngine.Random.Range(-1f, 1f) * this.distanceWobbleGain;
				this.Explode(vector2, this.velocity);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400077F RID: 1919
	public float detonationDistance = 15f;

	// Token: 0x04000780 RID: 1920
	public float distanceWobbleGain = 1f;

	// Token: 0x04000781 RID: 1921
	public float armDistance = 10f;

	// Token: 0x04000782 RID: 1922
	public float autoExplodeDistance = 300f;

	// Token: 0x04000783 RID: 1923
	public bool allowAllTargets;

	// Token: 0x04000784 RID: 1924
	public List<Actor.TargetType> allowedTargetTypes;

	// Token: 0x04000785 RID: 1925
	private bool useInfantryTrigger;
}

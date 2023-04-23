using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class SquadCluster
{
	// Token: 0x060005BE RID: 1470 RVA: 0x000057F2 File Offset: 0x000039F2
	public SquadCluster(Squad host)
	{
		this.host = host;
		this.children = new HashSet<Squad>();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0005D5FC File Offset: 0x0005B7FC
	public void Update()
	{
		List<Squad> squadsOnTeam = ActorManager.GetSquadsOnTeam(this.host.team);
		float radius = this.GetRadius();
		Vector3 vector = this.host.Leader().actor.Position();
		Vector3 normalized = this.host.Leader().actor.Velocity().normalized;
		this.maxAllowedSpeed = this.host.GetMaxTopSpeed();
		this.restrictSpeed = false;
		if (this.ShouldRestrictSpeed())
		{
			foreach (Squad squad in squadsOnTeam)
			{
				if (squad != this.host)
				{
					Vector3 vector2 = squad.Leader().actor.Position();
					Vector3 normalized2 = squad.Leader().actor.Velocity().normalized;
					bool flag = Vector3.Dot(normalized, normalized2) > 0.6f && Vector3.Distance(vector, vector2) < radius;
					bool flag2 = squad.parentCluster == this;
					if (squad.parentCluster == null && flag)
					{
						squad.JoinParentCluster(this);
					}
					else if (flag2 && !flag)
					{
						squad.LeaveParentCluster();
						flag2 = false;
					}
					if (flag2)
					{
						this.restrictSpeed = true;
						this.maxAllowedSpeed = Mathf.Min(this.maxAllowedSpeed, squad.GetMaxTopSpeed());
						float num = Vector3.Dot(vector2 - vector, normalized) / radius;
						if (num > 0.5f)
						{
							squad.bonusSpeedInParentCluster = -1f;
						}
						else if (num > 0f && num < 0.2f)
						{
							squad.bonusSpeedInParentCluster = 1f;
						}
						else if (num > -0.2f && num < 0f)
						{
							squad.bonusSpeedInParentCluster = -1f;
						}
						else if (num < -0.5f)
						{
							squad.bonusSpeedInParentCluster = 1f;
						}
						else
						{
							squad.bonusSpeedInParentCluster = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0000580C File Offset: 0x00003A0C
	private bool ShouldRestrictSpeed()
	{
		return this.host.closestEnemySpawnPointDistance < 150f || this.host.IsTakingFire();
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0000582D File Offset: 0x00003A2D
	public float GetMaxAllowedSpeed()
	{
		if (!this.restrictSpeed)
		{
			return float.MaxValue;
		}
		return this.maxAllowedSpeed;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0005D80C File Offset: 0x0005BA0C
	public float GetRadius()
	{
		if (this.host.squadVehicle != null)
		{
			return 35f + this.host.squadVehicle.GetAvoidanceCoarseRadius() * 2f + (float)this.children.Count * 5f;
		}
		return 35f + (float)this.children.Count * 5f;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00005843 File Offset: 0x00003A43
	public void AddChild(Squad squad)
	{
		this.children.Add(squad);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00005852 File Offset: 0x00003A52
	public void RemoveChild(Squad squad)
	{
		this.children.Remove(squad);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0005D874 File Offset: 0x0005BA74
	public void Destroy()
	{
		this.host.hostCluster = null;
		foreach (Squad squad in this.children)
		{
			squad.parentCluster = null;
		}
	}

	// Token: 0x040005AB RID: 1451
	private const float BASE_RADIUS = 35f;

	// Token: 0x040005AC RID: 1452
	private const float RADIUS_PER_CHILD_SQUAD = 5f;

	// Token: 0x040005AD RID: 1453
	private const float HEADING_DOT_MIN = 0.6f;

	// Token: 0x040005AE RID: 1454
	private const float KEEP_DISTANCE_FOLLOWER_SPEED_DELTA = 1f;

	// Token: 0x040005AF RID: 1455
	public Squad host;

	// Token: 0x040005B0 RID: 1456
	public HashSet<Squad> children;

	// Token: 0x040005B1 RID: 1457
	private float maxAllowedSpeed;

	// Token: 0x040005B2 RID: 1458
	public bool restrictSpeed;
}

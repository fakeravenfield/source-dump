using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class Order
{
	// Token: 0x060004C6 RID: 1222 RVA: 0x00057200 File Offset: 0x00055400
	public Order(Order.OrderType type, SpawnPoint source, SpawnPoint target, bool enabled)
	{
		this.type = type;
		this.source = source;
		this.target = target;
		this.enabled = enabled;
		this.squadPriotityModifiers = new Dictionary<Squad, int>();
		if (type == Order.OrderType.Attack)
		{
			this.basePriority = 4;
		}
		else if (type == Order.OrderType.Defend)
		{
			this.basePriority = OrderManager.GetBaseDefensePriority(target, false);
		}
		this.requiredVehicleFilter = new VehicleFilter();
		if (source != null && target != null)
		{
			this.canWalk = SpawnPointNeighborManager.HasLandConnection(this.source, this.target);
			bool watercraft = this.target.vehicleFilter.watercraft && SpawnPointNeighborManager.HasWaterConnection(this.source, this.target);
			bool landcraft = this.target.vehicleFilter.landcraft && SpawnPointNeighborManager.HasLandConnection(this.source, this.target);
			bool flag = true;
			this.requiredVehicleFilter.watercraft = watercraft;
			this.requiredVehicleFilter.landcraft = landcraft;
			this.requiredVehicleFilter.air = (flag && this.target.vehicleFilter.air);
			this.requiredVehicleFilter.airFastmover = (flag && this.target.vehicleFilter.airFastmover);
			return;
		}
		this.canWalk = true;
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00004FB2 File Offset: 0x000031B2
	public void SetTargetSquad(Squad squad)
	{
		this.targetSquad = squad;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00004FBB File Offset: 0x000031BB
	public void SetWaypoints(Vector3[] waypoints)
	{
		this.waypoints = waypoints;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00004FC4 File Offset: 0x000031C4
	public void SetOverrideTargetPosition(Vector3 position)
	{
		this.hasOverrideTargetPosition = true;
		this.overrideTargetPosition = position;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00004FD4 File Offset: 0x000031D4
	public void DropOverrideTargetPosition()
	{
		this.hasOverrideTargetPosition = false;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00057354 File Offset: 0x00055554
	public void ModifyPriority(Squad squad, int modifier)
	{
		if (!this.squadPriotityModifiers.ContainsKey(squad))
		{
			this.squadPriotityModifiers.Add(squad, this.modifierMultiplier * modifier);
			return;
		}
		Dictionary<Squad, int> dictionary = this.squadPriotityModifiers;
		dictionary[squad] += this.modifierMultiplier * modifier;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00004FDD File Offset: 0x000031DD
	public void ResetPriorityModifier(Squad squad)
	{
		this.squadPriotityModifiers.Remove(squad);
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x000573A4 File Offset: 0x000555A4
	public int GetPriority()
	{
		int num = this.basePriority;
		foreach (int num2 in this.squadPriotityModifiers.Values)
		{
			num += num2;
		}
		if (this.type == Order.OrderType.Defend && !this.target.IsSafe())
		{
			num += 2;
		}
		return GameModeBase.instance.ModifyOrderPriority(this, num);
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00057428 File Offset: 0x00055628
	public bool IsEnabled()
	{
		if (!this.source.isActiveAndEnabled || !this.target.isActiveAndEnabled)
		{
			return false;
		}
		if (this.target.isGhostSpawn)
		{
			return false;
		}
		if (this.source.isGhostSpawn && this.type != Order.OrderType.Attack && this.type != Order.OrderType.Attack && this.type != Order.OrderType.Roam)
		{
			return false;
		}
		if (this.type == Order.OrderType.Roam)
		{
			return this.source.GetAvailableRoamingVehicle() != null;
		}
		if (this.type == Order.OrderType.Attack)
		{
			return this.enabled && (this.canWalk || this.SourceHasVehiclePassingFilter());
		}
		return this.enabled;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00004FEC File Offset: 0x000031EC
	private bool SourceHasVehiclePassingFilter()
	{
		return this.source.GetAvailableVehicle(this.requiredVehicleFilter, -1) != null;
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00005006 File Offset: 0x00003206
	public void SetEnabled(bool enabled)
	{
		this.enabled = enabled;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000574CC File Offset: 0x000556CC
	public override string ToString()
	{
		if (this.target == null)
		{
			return string.Concat(new string[]
			{
				"Order ",
				this.type.ToString(),
				", prio ",
				this.GetPriority().ToString(),
				" (base ",
				this.basePriority.ToString(),
				" modified by ",
				this.squadPriotityModifiers.Count.ToString(),
				" squads)",
				this.enabled ? "" : " DISABLED"
			});
		}
		return string.Concat(new string[]
		{
			"Order ",
			this.type.ToString(),
			" ",
			this.target.name,
			", prio ",
			this.GetPriority().ToString(),
			" (base ",
			this.basePriority.ToString(),
			" modified by ",
			this.squadPriotityModifiers.Count.ToString(),
			" squads)",
			this.enabled ? "" : " DISABLED"
		});
	}

	// Token: 0x040004B2 RID: 1202
	public Order.OrderType type;

	// Token: 0x040004B3 RID: 1203
	public SpawnPoint source;

	// Token: 0x040004B4 RID: 1204
	public SpawnPoint target;

	// Token: 0x040004B5 RID: 1205
	public Squad targetSquad;

	// Token: 0x040004B6 RID: 1206
	public bool hasOverrideTargetPosition;

	// Token: 0x040004B7 RID: 1207
	public Vector3 overrideTargetPosition;

	// Token: 0x040004B8 RID: 1208
	public int basePriority;

	// Token: 0x040004B9 RID: 1209
	public int modifierMultiplier = 1;

	// Token: 0x040004BA RID: 1210
	private bool enabled;

	// Token: 0x040004BB RID: 1211
	public Vector3[] waypoints;

	// Token: 0x040004BC RID: 1212
	private Dictionary<Squad, int> squadPriotityModifiers;

	// Token: 0x040004BD RID: 1213
	[NonSerialized]
	public VehicleFilter requiredVehicleFilter;

	// Token: 0x040004BE RID: 1214
	[NonSerialized]
	public bool canWalk = true;

	// Token: 0x040004BF RID: 1215
	[NonSerialized]
	public bool isIssuedByPlayer;

	// Token: 0x02000099 RID: 153
	public enum OrderType
	{
		// Token: 0x040004C1 RID: 1217
		Attack,
		// Token: 0x040004C2 RID: 1218
		Defend,
		// Token: 0x040004C3 RID: 1219
		Roam,
		// Token: 0x040004C4 RID: 1220
		Repair,
		// Token: 0x040004C5 RID: 1221
		Move,
		// Token: 0x040004C6 RID: 1222
		PatrolBase,
		// Token: 0x040004C7 RID: 1223
		PatrolWaypoints
	}
}

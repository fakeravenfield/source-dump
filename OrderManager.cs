using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class OrderManager : MonoBehaviour
{
	// Token: 0x060004A9 RID: 1193 RVA: 0x00004F1F File Offset: 0x0000311F
	public static void SetForcePlayerOrders(bool force)
	{
		OrderManager.instance.forcePlayerOrders = force;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00056264 File Offset: 0x00054464
	public static void NewOwnerOfSpawn(SpawnPoint spawn)
	{
		foreach (Order order in OrderManager.instance.ordersBySpawnPoint[spawn])
		{
			if (order.type == Order.OrderType.Attack)
			{
				order.SetEnabled(order.target.owner != spawn.owner);
			}
		}
		foreach (SpawnPoint spawnPoint in spawn.allNeighbors)
		{
			foreach (Order order2 in OrderManager.instance.ordersBySpawnPoint[spawnPoint])
			{
				if (order2.type == Order.OrderType.Attack && order2.target == spawn)
				{
					order2.SetEnabled(spawnPoint.owner != spawn.owner);
				}
			}
		}
		foreach (Order order3 in OrderManager.instance.playerOrders)
		{
			if (order3.type == Order.OrderType.Attack && order3.target == spawn)
			{
				order3.SetEnabled(order3.target.owner != GameManager.PlayerTeam());
			}
		}
		OrderManager.instance.UpdateOrderLists();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00056410 File Offset: 0x00054610
	public static void RefreshAllOrders()
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			OrderManager.instance.UpdateOrders(spawnPoint);
			foreach (Order order in OrderManager.instance.ordersBySpawnPoint[spawnPoint])
			{
				if (order.type == Order.OrderType.Attack)
				{
					order.SetEnabled(order.target.owner != order.source.owner);
				}
			}
		}
		OrderManager.instance.UpdateOrderLists();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x000564C8 File Offset: 0x000546C8
	public static Order GetHighestPriorityOrderForExistingSquad(Squad squad)
	{
		bool flag = !squad.HasSquadVehicle() || squad.squadVehicle.aiUseToDefendPoint;
		OrderManager.orderFilter[Order.OrderType.Roam] = !squad.HasSquadVehicle();
		OrderManager.orderFilter[Order.OrderType.Repair] = (squad.members.Count == 1 && squad.HasRepairTool());
		OrderManager.orderFilter[Order.OrderType.Defend] = flag;
		OrderManager.orderFilter[Order.OrderType.Attack] = true;
		SpawnPoint spawnPoint = ActorManager.ClosestSpawnPointOwnedBy(squad.Leader().actor.Position(), squad.team);
		if (spawnPoint != null)
		{
			Order highestPriorityOrderFromSpawnPoint = OrderManager.GetHighestPriorityOrderFromSpawnPoint(spawnPoint, OrderManager.orderFilter);
			if (highestPriorityOrderFromSpawnPoint != null && highestPriorityOrderFromSpawnPoint.GetPriority() >= 0)
			{
				return highestPriorityOrderFromSpawnPoint;
			}
		}
		List<Order> list = new List<Order>();
		foreach (SpawnPoint spawnPoint2 in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint2.owner == squad.team && squad.CanReachSpawnPoint(spawnPoint2))
			{
				OrderManager.orderFilter[Order.OrderType.Defend] = (flag && (spawnPoint2 == squad.lastReachedSpawnPoint || spawnPoint2.allNeighbors.Contains(squad.lastReachedSpawnPoint)));
				Order highestPriorityOrderFromSpawnPoint2 = OrderManager.GetHighestPriorityOrderFromSpawnPoint(spawnPoint2, OrderManager.orderFilter);
				if (highestPriorityOrderFromSpawnPoint2 != null)
				{
					list.Add(highestPriorityOrderFromSpawnPoint2);
				}
			}
		}
		OrderManager.orderFilter[Order.OrderType.Defend] = flag;
		return OrderManager.GetHighestPriorityOrderFromList(list, OrderManager.orderFilter);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00056624 File Offset: 0x00054824
	public static Order GetHighestPriorityOrder(int team)
	{
		if (team == GameManager.PlayerTeam() && OrderManager.instance.playerOrders.Count > 0)
		{
			foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
			{
				if (spawnPoint.owner == team && OrderManager.instance.roamOrder[spawnPoint] != null && OrderManager.instance.roamOrder[spawnPoint].IsEnabled())
				{
					return OrderManager.instance.roamOrder[spawnPoint];
				}
			}
			Order order = null;
			int num = OrderManager.instance.forcePlayerOrders ? int.MinValue : 0;
			foreach (Order order2 in OrderManager.instance.playerOrders)
			{
				if (order2.IsEnabled() && order2.source.owner == team)
				{
					int priority = order2.GetPriority();
					if (priority > num)
					{
						num = priority;
						order = order2;
					}
				}
			}
			if (order != null)
			{
				return order;
			}
			bool flag = OrderManager.instance.forcePlayerOrders;
		}
		return OrderManager.GetHighestPriorityOrderFromList(OrderManager.instance.ordersOfTeam[team], null);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00056760 File Offset: 0x00054960
	public static Order GetHighestPriorityOrderFromSpawnPoint(SpawnPoint spawn, Dictionary<Order.OrderType, bool> filter = null)
	{
		if (spawn.owner == GameManager.PlayerTeam())
		{
			Order order = null;
			int num = OrderManager.instance.forcePlayerOrders ? int.MinValue : 0;
			foreach (Order order2 in OrderManager.instance.playerOrders)
			{
				if (order2.IsEnabled() && order2.source == spawn && filter[order2.type])
				{
					int priority = order2.GetPriority();
					if (priority > num)
					{
						num = priority;
						order = order2;
					}
				}
			}
			if (order != null)
			{
				return order;
			}
		}
		return OrderManager.GetHighestPriorityOrderFromList(OrderManager.instance.ordersBySpawnPoint[spawn], filter);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00056828 File Offset: 0x00054A28
	private static Order GetHighestPriorityOrderFromList(List<Order> orders, Dictionary<Order.OrderType, bool> filter = null)
	{
		if (filter == null)
		{
			filter = OrderManager.orderFilterAny;
		}
		int num = int.MinValue;
		Order result = null;
		foreach (Order order in orders)
		{
			if (order.IsEnabled() && filter[order.type])
			{
				int priority = order.GetPriority();
				if (priority > num)
				{
					num = priority;
					result = order;
				}
			}
		}
		return result;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00004F2C File Offset: 0x0000312C
	public static void AddPlayerOrder(Order order)
	{
		if (order.type == Order.OrderType.Attack)
		{
			order.SetEnabled(order.target.owner != GameManager.PlayerTeam());
		}
		order.isIssuedByPlayer = true;
		OrderManager.instance.playerOrders.Add(order);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00004F68 File Offset: 0x00003168
	public static void RemovePlayerOrder(Order order)
	{
		OrderManager.instance.playerOrders.Remove(order);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x000568AC File Offset: 0x00054AAC
	public static void SetupDefaultOrders(bool attackOrders = true, bool defenseOrders = true, bool roamOrders = true, bool repairOrders = true)
	{
		foreach (SpawnPoint spawnPoint in UnityEngine.Object.FindObjectsOfType<SpawnPoint>())
		{
			if (defenseOrders)
			{
				OrderManager.AddDefenseOrder(spawnPoint);
			}
			if (repairOrders)
			{
				OrderManager.AddRepairOrder(spawnPoint);
			}
			if (roamOrders)
			{
				OrderManager.AddRoamOrder(spawnPoint);
			}
			if (attackOrders)
			{
				foreach (SpawnPoint target in spawnPoint.outgoingNeighbors)
				{
					OrderManager.AddAttackOrder(spawnPoint, target);
				}
			}
		}
		OrderManager.instance.UpdateOrderLists();
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00056948 File Offset: 0x00054B48
	public static Order AddRoamOrder(SpawnPoint spawn)
	{
		Order order = new Order(Order.OrderType.Roam, spawn, spawn, false);
		OrderManager.instance.ordersBySpawnPoint[spawn].Add(order);
		OrderManager.instance.roamOrder[spawn] = order;
		return order;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00056988 File Offset: 0x00054B88
	public static Order AddRepairOrder(SpawnPoint spawn)
	{
		Order order = new Order(Order.OrderType.Repair, spawn, spawn, false);
		order.basePriority = 1000;
		order.modifierMultiplier *= 1000000;
		OrderManager.instance.ordersBySpawnPoint[spawn].Add(order);
		OrderManager.instance.repairOrder[spawn] = order;
		return order;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x000569E4 File Offset: 0x00054BE4
	public static Order AddDefenseOrder(SpawnPoint spawn)
	{
		Order order = new Order(Order.OrderType.Defend, spawn, spawn, true);
		OrderManager.instance.ordersBySpawnPoint[spawn].Add(order);
		OrderManager.instance.defenseOrder[spawn] = order;
		return order;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00056A24 File Offset: 0x00054C24
	public static Order AddAttackOrder(SpawnPoint source, SpawnPoint target)
	{
		Order order = new Order(Order.OrderType.Attack, source, target, false);
		OrderManager.instance.ordersBySpawnPoint[source].Add(order);
		SpawnPointNeighborManager.SpawnPointNeighbor neighborInfo = SpawnPointNeighborManager.GetNeighborInfo(source, target);
		if (neighborInfo != null)
		{
			if (!neighborInfo.landConnection)
			{
				order.requiredVehicleFilter.landcraft = false;
				order.canWalk = false;
			}
			if (!neighborInfo.waterConnection)
			{
				order.requiredVehicleFilter.watercraft = false;
			}
		}
		return order;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00056A8C File Offset: 0x00054C8C
	public static Order AddMoveOrder(SpawnPoint source, SpawnPoint target)
	{
		Order order = new Order(Order.OrderType.Move, source, target, true);
		OrderManager.instance.ordersBySpawnPoint[source].Add(order);
		return order;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00056ABC File Offset: 0x00054CBC
	public static void RegisterOrder(Order order)
	{
		try
		{
			if (order.source == null)
			{
				throw new Exception("Order does not have a valid source value");
			}
			OrderManager.instance.ordersBySpawnPoint[order.source].Add(order);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00056B18 File Offset: 0x00054D18
	public static SquadOrderPoint CreateOrderPoint(Transform parent, SquadOrderPoint.ObjectiveType type, Texture2D texture)
	{
		SquadOrderPoint component = UnityEngine.Object.Instantiate<GameObject>(OrderManager.instance.pointOrderPrefab, parent).GetComponent<SquadOrderPoint>();
		component.type = type;
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.image.texture = texture;
		return component;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00004F7B File Offset: 0x0000317B
	public static List<Order> GetOrdersOfSpawnPoint(SpawnPoint spawn)
	{
		return OrderManager.instance.ordersBySpawnPoint[spawn];
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00056B70 File Offset: 0x00054D70
	private void Awake()
	{
		OrderManager.instance = this;
		OrderManager.orderFilterAny = new Dictionary<Order.OrderType, bool>();
		OrderManager.orderFilter = new Dictionary<Order.OrderType, bool>();
		foreach (object obj in Enum.GetValues(typeof(Order.OrderType)))
		{
			Order.OrderType key = (Order.OrderType)obj;
			OrderManager.orderFilterAny.Add(key, true);
			OrderManager.orderFilter.Add(key, true);
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00056C00 File Offset: 0x00054E00
	public void StartGame()
	{
		this.ordersBySpawnPoint = new Dictionary<SpawnPoint, List<Order>>();
		this.defenseOrder = new Dictionary<SpawnPoint, Order>();
		this.repairOrder = new Dictionary<SpawnPoint, Order>();
		this.roamOrder = new Dictionary<SpawnPoint, Order>();
		this.playerOrders = new List<Order>();
		this.forcePlayerOrders = false;
		this.ordersOfTeam = new List<Order>[2];
		this.ordersOfTeam[0] = new List<Order>();
		this.ordersOfTeam[1] = new List<Order>();
		foreach (SpawnPoint key in ActorManager.instance.spawnPoints)
		{
			OrderManager.instance.ordersBySpawnPoint[key] = new List<Order>();
		}
		this.updateNextSpawnPointAction.Start();
		this.spawnPointIndex = 0;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00056CB8 File Offset: 0x00054EB8
	private void UpdateOrderLists()
	{
		this.ordersOfTeam[0] = new List<Order>();
		this.ordersOfTeam[1] = new List<Order>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.owner >= 0 && spawnPoint.owner < 2)
			{
				this.ordersOfTeam[spawnPoint.owner].AddRange(this.ordersBySpawnPoint[spawnPoint]);
			}
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00056D2C File Offset: 0x00054F2C
	public static int GetBaseDefensePriority(SpawnPoint spawnPoint, bool hasEnemyNeighbor)
	{
		switch (spawnPoint.defenseStrategy)
		{
		case SpawnPoint.DefenseStrategy.NeverAutoDefend:
			return -9999999;
		case SpawnPoint.DefenseStrategy.AlwaysAutoDefend:
			return 0;
		case SpawnPoint.DefenseStrategy.AlwaysManTurrets:
			if (spawnPoint.HasAnyAvailableTurrets())
			{
				return 0;
			}
			return -100;
		default:
			if (hasEnemyNeighbor)
			{
				return 0;
			}
			return -100;
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00056D74 File Offset: 0x00054F74
	private void UpdateOrders(SpawnPoint spawnPoint)
	{
		bool enabled = false;
		foreach (TurretSpawner turretSpawner in spawnPoint.turretSpawners)
		{
			Vehicle activeTurret = turretSpawner.GetActiveTurret();
			if (activeTurret != null && activeTurret.dead && activeTurret.canBeRepairedAfterDeath)
			{
				enabled = true;
				break;
			}
		}
		int num = 0;
		foreach (SpawnPoint spawnPoint2 in spawnPoint.incomingNeighbors)
		{
			if (spawnPoint2.owner != spawnPoint.owner && spawnPoint2.owner >= 0)
			{
				num++;
			}
		}
		if (this.defenseOrder.ContainsKey(spawnPoint))
		{
			int num2 = Mathf.Min(num, 2);
			bool hasEnemyNeighbor = num > 0;
			this.defenseOrder[spawnPoint].basePriority = OrderManager.GetBaseDefensePriority(spawnPoint, hasEnemyNeighbor) + num2;
		}
		if (this.repairOrder.ContainsKey(spawnPoint))
		{
			this.repairOrder[spawnPoint].SetEnabled(enabled);
		}
		if (this.roamOrder.ContainsKey(spawnPoint) && this.roamOrder[spawnPoint].IsEnabled())
		{
			int num3 = 0;
			using (List<VehicleSpawner>.Enumerator enumerator3 = spawnPoint.vehicleSpawners.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.HasAvailableRoamingVehicle())
					{
						num3++;
					}
				}
			}
			this.roamOrder[spawnPoint].basePriority = 10 + num3 * 5;
		}
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00056F20 File Offset: 0x00055120
	private void Update()
	{
		if (GameManager.instance.ingame)
		{
			if (Input.GetKeyDown(KeyCode.F9))
			{
				this.debug = !this.debug;
			}
			if (this.updateNextSpawnPointAction.TrueDone() && ActorManager.instance.spawnPoints.Length != 0)
			{
				this.spawnPointIndex = (this.spawnPointIndex + 1) % ActorManager.instance.spawnPoints.Length;
				this.UpdateOrders(ActorManager.instance.spawnPoints[this.spawnPointIndex]);
				this.updateNextSpawnPointAction.Start();
				return;
			}
		}
		else
		{
			this.debug = false;
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00056FB4 File Offset: 0x000551B4
	private void OnGUI()
	{
		if (this.debug)
		{
			try
			{
				GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), "");
				int num;
				for (int i = 0; i < 2; i++)
				{
					List<Order> list = this.ordersOfTeam[i];
					list.Sort((Order x, Order y) => y.GetPriority().CompareTo(x.GetPriority()));
					float x2 = (float)((i == 0) ? 3 : (Screen.width / 2));
					GUI.skin.label.alignment = TextAnchor.MiddleLeft;
					GUI.Label(new Rect(x2, 0f, 1000f, 20f), "Team " + i.ToString() + " orders");
					num = 0;
					foreach (Order order in list)
					{
						if (order.IsEnabled())
						{
							GUI.Label(new Rect(x2, (float)(20 + num * 20), 1000f, 20f), order.ToString());
							num++;
						}
					}
				}
				num = 0;
				foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
				{
					GUI.Label(new Rect(0f, (float)(Screen.height - 20 - num * 20), 1000f, 20f), string.Concat(new string[]
					{
						spawnPoint.ToString(),
						" inf area ",
						spawnPoint.GetClosestNavmeshArea(PathfindingBox.Type.Infantry).ToString(),
						", car area ",
						spawnPoint.GetClosestNavmeshArea(PathfindingBox.Type.Car).ToString(),
						", boat area: ",
						spawnPoint.GetClosestNavmeshArea(PathfindingBox.Type.Boat).ToString()
					}));
					num++;
				}
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x04000494 RID: 1172
	public static OrderManager instance;

	// Token: 0x04000495 RID: 1173
	public const int ATTACK_BASE_PRIORITY = 4;

	// Token: 0x04000496 RID: 1174
	public const int DEFENSE_BASE_PRIORITY_NEVER_AUTO_DEFEND = -9999999;

	// Token: 0x04000497 RID: 1175
	public const int DEFENSE_BASE_PRIORITY_NO_ENEMY_NEIGHBOR = -100;

	// Token: 0x04000498 RID: 1176
	public const int DEFENSE_BASE_PRIORITY_WITH_ENEMY_NEIGHBOR = 0;

	// Token: 0x04000499 RID: 1177
	public const int DEFENSE_BASE_PRIORITY_ALWAYS_AUTO_DEFEND = 0;

	// Token: 0x0400049A RID: 1178
	public const int DEFENSE_PRIORITY_PER_ENEMY_NEIGHBOR = 1;

	// Token: 0x0400049B RID: 1179
	public const int DEFENSE_PRIORITY_ENEMY_NEIGHBOR_CAP = 2;

	// Token: 0x0400049C RID: 1180
	public const int DEFENSE_PRIORITY_UNDER_ATTACK_BONUS = 2;

	// Token: 0x0400049D RID: 1181
	public const int REPAIR_BASE_PRIORITY = 1000;

	// Token: 0x0400049E RID: 1182
	public const int REPAIR_MODIFIER_MULTIPLIER = 1000000;

	// Token: 0x0400049F RID: 1183
	public const int ROAM_BASE_PRIORITY = 10;

	// Token: 0x040004A0 RID: 1184
	public const int ROAM_PER_VEHICLE_PRIORITY = 5;

	// Token: 0x040004A1 RID: 1185
	public GameObject pointOrderPrefab;

	// Token: 0x040004A2 RID: 1186
	public Texture2D pointOrderFlagTexture;

	// Token: 0x040004A3 RID: 1187
	public Texture2D pointOrderVehicleTexture;

	// Token: 0x040004A4 RID: 1188
	private List<Order> playerOrders;

	// Token: 0x040004A5 RID: 1189
	private Dictionary<SpawnPoint, List<Order>> ordersBySpawnPoint;

	// Token: 0x040004A6 RID: 1190
	private Dictionary<SpawnPoint, Order> defenseOrder;

	// Token: 0x040004A7 RID: 1191
	private Dictionary<SpawnPoint, Order> repairOrder;

	// Token: 0x040004A8 RID: 1192
	private Dictionary<SpawnPoint, Order> roamOrder;

	// Token: 0x040004A9 RID: 1193
	private List<Order>[] ordersOfTeam;

	// Token: 0x040004AA RID: 1194
	private TimedAction updateNextSpawnPointAction = new TimedAction(0.5f, false);

	// Token: 0x040004AB RID: 1195
	private int spawnPointIndex;

	// Token: 0x040004AC RID: 1196
	private bool forcePlayerOrders;

	// Token: 0x040004AD RID: 1197
	private bool debug;

	// Token: 0x040004AE RID: 1198
	private static Dictionary<Order.OrderType, bool> orderFilterAny;

	// Token: 0x040004AF RID: 1199
	private static Dictionary<Order.OrderType, bool> orderFilter;
}

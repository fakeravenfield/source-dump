using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200097E RID: 2430
	[Wrapper(typeof(Order))]
	[Name("Order")]
	public static class WOrder
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x00029C28 File Offset: 0x00027E28
		[Getter]
		public static Order.OrderType GetType(Order self)
		{
			return self.type;
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00029C30 File Offset: 0x00027E30
		[Getter]
		[Doc("The spawn point this order targets.")]
		public static SpawnPoint GetTargetPoint(Order self)
		{
			return self.target;
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00029C38 File Offset: 0x00027E38
		[Getter]
		[Doc("The spawn point this order originated from.")]
		public static SpawnPoint GetSourcePoint(Order self)
		{
			return self.source;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00029C40 File Offset: 0x00027E40
		[Getter]
		[Doc("Returns true if this order was created by the player via the tactics view.")]
		public static bool GetIsIssuedByPlayer(Order self)
		{
			return self.isIssuedByPlayer;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00029C48 File Offset: 0x00027E48
		[Getter]
		public static int GetBasePriority(Order self)
		{
			return self.basePriority;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00029C50 File Offset: 0x00027E50
		[Setter]
		[Doc("The priority of this order when not assigned to any squads.")]
		public static void SetBasePriority(Order self, int priority)
		{
			self.basePriority = priority;
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x00029C59 File Offset: 0x00027E59
		[Getter]
		[Doc("The current priority of the order, modified by any squads it is currently assigned to.")]
		public static int GetPriority(Order self)
		{
			return self.GetPriority();
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00029C61 File Offset: 0x00027E61
		[Getter]
		public static bool GetHasOverrideTargetPosition(Order self)
		{
			return self.hasOverrideTargetPosition;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x00029C69 File Offset: 0x00027E69
		public static Vector3 GetOverrideTargetPosition(Order self)
		{
			return self.overrideTargetPosition;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x00029C71 File Offset: 0x00027E71
		[Doc("Sets an override target position.[..] Gives full control over exactly where the squad goes when completing the objective.")]
		public static void SetOverrideTargetPosition(Order self, Vector3 position)
		{
			self.SetOverrideTargetPosition(position);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x00029C7A File Offset: 0x00027E7A
		[Doc("Drops the active override target position.")]
		public static void DropOverrideTargetPosition(Order self)
		{
			self.DropOverrideTargetPosition();
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x00029C82 File Offset: 0x00027E82
		public static Order Create(Order.OrderType type, SpawnPoint source, SpawnPoint target)
		{
			return new Order(type, source, target, true);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x00029C8D File Offset: 0x00027E8D
		public static Order Create(Order.OrderType type, SpawnPoint source, SpawnPoint target, Vector3 overridePosition)
		{
			Order order = new Order(type, source, target, true);
			order.SetOverrideTargetPosition(overridePosition);
			return order;
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00029C9F File Offset: 0x00027E9F
		[Doc("Convenience function that creates a move order to the specified override target position.")]
		public static Order CreateMoveOrder(Vector3 targetPosition)
		{
			Order order = new Order(Order.OrderType.Move, null, null, true);
			order.SetOverrideTargetPosition(targetPosition);
			return order;
		}
	}
}

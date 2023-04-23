using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Modes.Scripted
{
	// Token: 0x020003CE RID: 974
	public class ActorSpawner : TriggeredComponent
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x000A4E78 File Offset: 0x000A3078
		protected override void OnTrigger(int triggerIndex)
		{
			Vehicle vehicle = null;
			List<Actor> list = new List<Actor>();
			if (this.spawnVehicle)
			{
				if (this.vehiclePrefab == null)
				{
					int num = (this.team == ActorSpawner.Team.Team0) ? 0 : 1;
					this.vehiclePrefab = GameManager.instance.gameInfo.team[num].vehiclePrefab[this.vehicleSpawnType];
				}
				if (this.vehiclePrefab != null)
				{
					vehicle = UnityEngine.Object.Instantiate<GameObject>(this.vehiclePrefab, base.transform.position, base.transform.rotation).GetComponent<Vehicle>();
				}
			}
			int num2 = this.count;
			if (this.spawnType == ActorSpawner.Type.Player)
			{
				Actor item = ScriptedMode.SpawnPlayer(base.transform.position, this);
				num2--;
				list.Add(item);
			}
			if (num2 <= 0)
			{
				return;
			}
			List<ActorController> list2 = new List<ActorController>();
			for (int i = 0; i < num2; i++)
			{
				Actor actor = ScriptedMode.SpawnBot(base.transform.position, this);
				list2.Add(actor.controller);
				list.Add(actor);
			}
			if (this.spawnType == ActorSpawner.Type.Player)
			{
				using (List<ActorController>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ActorController actorController = enumerator.Current;
						AiActorController a = (AiActorController)actorController;
						FpsActorController.instance.playerSquad.AddMember(a);
					}
					goto IL_1B1;
				}
			}
			Order order = null;
			if (this.order.target != null)
			{
				order = new Order(this.order.type, null, this.order.target, true);
			}
			Squad squad = new Squad(list2, null, order, vehicle, 1f);
			if (this.order.target != null)
			{
				squad.allowRequestNewOrders = this.order.allowRequestNewOrders;
			}
			IL_1B1:
			if (vehicle != null)
			{
				base.StartCoroutine(this.EnterVehicle(list, vehicle));
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00012C30 File Offset: 0x00010E30
		private IEnumerator EnterVehicle(List<Actor> actors, Vehicle vehicle)
		{
			yield return new WaitForEndOfFrame();
			using (List<Actor>.Enumerator enumerator = actors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Actor actor = enumerator.Current;
					actor.EnterVehicle(vehicle);
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x04001A17 RID: 6679
		public ActorSpawner.Type spawnType;

		// Token: 0x04001A18 RID: 6680
		public ActorSpawner.Team team;

		// Token: 0x04001A19 RID: 6681
		public int count = 1;

		// Token: 0x04001A1A RID: 6682
		public WeaponManager.LoadoutSet loadout;

		// Token: 0x04001A1B RID: 6683
		public ActorSpawner.ActorSpawnerOrder order;

		// Token: 0x04001A1C RID: 6684
		public bool spawnVehicle;

		// Token: 0x04001A1D RID: 6685
		public VehicleSpawner.VehicleSpawnType vehicleSpawnType;

		// Token: 0x04001A1E RID: 6686
		public GameObject vehiclePrefab;

		// Token: 0x020003CF RID: 975
		public enum Type
		{
			// Token: 0x04001A20 RID: 6688
			Bot,
			// Token: 0x04001A21 RID: 6689
			Player
		}

		// Token: 0x020003D0 RID: 976
		public enum Team
		{
			// Token: 0x04001A23 RID: 6691
			Team0,
			// Token: 0x04001A24 RID: 6692
			Team1
		}

		// Token: 0x020003D1 RID: 977
		[Serializable]
		public class ActorSpawnerOrder
		{
			// Token: 0x04001A25 RID: 6693
			public Order.OrderType type;

			// Token: 0x04001A26 RID: 6694
			public SpawnPoint target;

			// Token: 0x04001A27 RID: 6695
			public bool allowRequestNewOrders;
		}
	}
}

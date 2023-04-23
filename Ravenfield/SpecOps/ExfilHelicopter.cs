using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A6 RID: 934
	public class ExfilHelicopter
	{
		// Token: 0x0600171F RID: 5919 RVA: 0x00012330 File Offset: 0x00010530
		public ExfilHelicopter(SpecOpsMode specOps)
		{
			this.specOps = specOps;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000A1804 File Offset: 0x0009FA04
		private bool SpawnHelicopter(Vector3 spawnPosition, Vector3 approachDirection)
		{
			GameObject gameObject = GameManager.instance.gameInfo.team[this.specOps.attackingTeam].vehiclePrefab[VehicleSpawner.VehicleSpawnType.TransportHelicopter];
			GameObject original = this.specOps.exfilHelicopterPrefab;
			bool flag = this.CustomHelicopterPrefabIsValid(gameObject);
			if (flag)
			{
				original = gameObject;
			}
			this.helicopter = UnityEngine.Object.Instantiate<GameObject>(original, spawnPosition, Quaternion.LookRotation(-approachDirection)).GetComponent<Helicopter>();
			this.helicopter.isInvulnerable = true;
			this.helicopter.canBeTakenOverByPlayerSquad = false;
			this.helicopter.countermeasuresCooldown = 5f;
			this.helicopter.allowPlayerSeatSwap = false;
			this.helicopter.allowPlayerSeatChange = false;
			foreach (Seat seat in this.helicopter.seats)
			{
				seat.allowUnderwater = true;
			}
			return flag;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000A18FC File Offset: 0x0009FAFC
		public void StartExfiltration(Vector3 landingPoint)
		{
			this.landingPosition = landingPoint;
			Vector3 normalized = ((landingPoint - FpsActorController.instance.actor.Position()).ToGround() * 2f + UnityEngine.Random.insideUnitSphere.ToGround().normalized).normalized;
			Vector3 vector = landingPoint + normalized * 1500f;
			vector.y += 200f;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(vector + new Vector3(0f, 1000f, 0f), Vector3.down), out raycastHit, 2000f, 4194305))
			{
				vector = raycastHit.point;
				vector.y += 200f;
			}
			this.altitudeLimit = ExfilHelicopter.CalculateFlightAltitudeLimit(vector, landingPoint);
			vector.y = this.altitudeLimit;
			Debug.DrawLine(vector, landingPoint, Color.red, 100f);
			bool flag = this.SpawnHelicopter(vector, normalized);
			List<Seat> list = new List<Seat>(16);
			if (!flag)
			{
				for (int i = 0; i < 4; i++)
				{
					list.Add(this.helicopter.seats[i]);
				}
			}
			else
			{
				foreach (Seat seat in this.helicopter.seats)
				{
					if (seat.IsDriverSeat() || seat.HasAnyMountedWeapons())
					{
						list.Add(seat);
					}
				}
			}
			List<Actor> list2 = new List<Actor>();
			for (int j = 0; j < list.Count; j++)
			{
				Seat seat2 = list[j];
				Actor actor = ActorManager.instance.CreateAIActor(this.specOps.attackingTeam);
				actor.SpawnAt(vector + new Vector3(20f, 0f, 0f), Quaternion.identity, null);
				AiActorController aiActorController = actor.controller as AiActorController;
				actor.EnterSeat(seat2, false);
				actor.isInvulnerable = true;
				aiActorController.modifier.canJoinPlayerSquad = false;
				actor.SetModelSkin(ActorManager.instance.defaultActorSkin);
				actor.isIgnored = true;
				if (seat2.IsDriverSeat())
				{
					this.pilotAi = aiActorController;
				}
				list2.Add(actor);
			}
			Order order = new Order(Order.OrderType.Move, null, null, true);
			order.SetOverrideTargetPosition(landingPoint);
			this.squad = new Squad(list2, null, order, this.helicopter, 0f);
			this.squad.allowRequestNewOrders = false;
			this.squad.allowAutoLeaveVehicle = false;
			this.pilotAi.targetFlightHeight = 80f;
			this.helicopter.rigidbody.velocity = -normalized * 20f;
			ObjectiveUi.CreateObjective("Exfiltrate", landingPoint + Vector3.up);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000A1BE8 File Offset: 0x0009FDE8
		public static float CalculateFlightAltitudeLimit(Vector3 spawnPosition, Vector3 landingPosition)
		{
			Vector3 start = landingPosition;
			start.y += 100f;
			float num = landingPosition.y + 40f;
			for (int i = 0; i < 20; i++)
			{
				float num2 = ((float)i + 1f) / 20f;
				num2 = Mathf.Pow(num2, 2f);
				Vector3 end = Vector3.Lerp(landingPosition, spawnPosition, num2);
				RaycastHit raycastHit;
				bool flag = Physics.Linecast(start, end, out raycastHit, 4194305);
				if (flag)
				{
					num = Mathf.Max(num, raycastHit.point.y + 40f);
				}
				Debug.DrawLine(start, end, flag ? Color.red : Color.green, 100f);
			}
			return num;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000A1C98 File Offset: 0x0009FE98
		public void Update()
		{
			if (this.helicopter == null)
			{
				return;
			}
			if (this.helicopter.transform.up.y < 0f)
			{
				this.helicopter.transform.rotation = SMath.LookRotationRespectUp(this.helicopter.transform.forward, Vector3.up);
				this.helicopter.transform.position = this.helicopter.transform.position + new Vector3(0f, 10f, 0f);
				this.helicopter.rigidbody.velocity = Vector3.zero;
				this.helicopter.rigidbody.angularVelocity = Vector3.zero;
			}
			using (List<Seat>.Enumerator enumerator = this.helicopter.seats.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (WaterLevel.IsInWater(enumerator.Current.transform.position))
					{
						this.helicopter.transform.rotation = SMath.LookRotationRespectUp(this.helicopter.transform.forward, Vector3.up);
						this.helicopter.transform.position = this.landingPosition + new Vector3(0f, 5f, 0f);
						this.helicopter.rigidbody.velocity = Vector3.zero;
						this.helicopter.rigidbody.angularVelocity = Vector3.zero;
					}
				}
			}
			if (!this.isCompleted)
			{
				Vector3 position = this.helicopter.transform.position;
				if (!this.onApproach && position.y < this.altitudeLimit)
				{
					position.y = this.altitudeLimit;
					this.helicopter.transform.position = position;
					Vector3 velocity = this.helicopter.rigidbody.velocity;
					velocity.y = 0f;
					this.helicopter.rigidbody.velocity = velocity;
				}
				if (!this.squad.scriptedLanding)
				{
					float magnitude = (this.helicopter.transform.position + this.helicopter.rigidbody.velocity * 3f - this.landingPosition).ToGround().magnitude;
					if (magnitude < 300f && !this.onApproach)
					{
						this.onApproach = true;
						this.pilotAi.targetFlightHeight = 40f;
					}
					if (magnitude < 40f && !this.squad.scriptedLanding)
					{
						this.squad.LandAtPosition(this.landingPosition);
						this.specOps.dialog.OnExfiltrationTouchdown();
					}
				}
				if (this.helicopter.playerIsInside)
				{
					FpsActorController.instance.allowExitVehicle = false;
					FpsActorController.instance.actor.isInvulnerable = true;
					if (this.AllAttackersPickedUp())
					{
						this.CompleteExfiltration();
					}
				}
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000A1FA8 File Offset: 0x000A01A8
		private void CompleteExfiltration()
		{
			this.isCompleted = true;
			this.squad.CancelLanding();
			Order order = new Order(Order.OrderType.Move, null, null, true);
			order.SetOverrideTargetPosition(this.landingPosition + UnityEngine.Random.insideUnitSphere.ToGround().normalized * 5000f);
			this.squad.AssignOrder(order);
			this.specOps.OnExfiltrationLiftOff();
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000A2018 File Offset: 0x000A0218
		private bool AllAttackersPickedUp()
		{
			if (FpsActorController.instance.playerSquad == null)
			{
				return false;
			}
			foreach (ActorController actorController in FpsActorController.instance.playerSquad.members)
			{
				if (!actorController.actor.IsSeated() || actorController.actor.seat.vehicle != this.helicopter)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000A20AC File Offset: 0x000A02AC
		private bool CustomHelicopterPrefabIsValid(GameObject prefab)
		{
			if (prefab == this.specOps.exfilHelicopterPrefab)
			{
				return false;
			}
			bool result;
			try
			{
				if (prefab == null)
				{
					result = false;
				}
				else
				{
					Helicopter component = prefab.GetComponent<Helicopter>();
					if (component == null)
					{
						result = false;
					}
					else
					{
						int num = 0;
						foreach (Seat seat in component.seats)
						{
							if (!(seat == component.seats[0]) && !seat.HasAnyMountedWeapons())
							{
								num++;
							}
						}
						Debug.Log("Found passenger seats: " + num.ToString());
						if (num >= this.specOps.attackerSquad.members.Count)
						{
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x04001942 RID: 6466
		private SpecOpsMode specOps;

		// Token: 0x04001943 RID: 6467
		public const float TARGET_MIN_DISTANCE = 100f;

		// Token: 0x04001944 RID: 6468
		public const float TARGET_MAX_DISTANCE = 300f;

		// Token: 0x04001945 RID: 6469
		private const float SPAWN_AIR_SPEED = 20f;

		// Token: 0x04001946 RID: 6470
		private const float SPAWN_HEIGHT = 200f;

		// Token: 0x04001947 RID: 6471
		private const float SPAWN_DISTANCE = 1500f;

		// Token: 0x04001948 RID: 6472
		private const float FLIGHT_ALTITUDE = 80f;

		// Token: 0x04001949 RID: 6473
		private const float FLIGHT_ALTITUDE_APPROACH = 40f;

		// Token: 0x0400194A RID: 6474
		private const float APPROACH_RANGE = 300f;

		// Token: 0x0400194B RID: 6475
		private const float START_LANDING_RANGE = 40f;

		// Token: 0x0400194C RID: 6476
		private const float START_LANDING_EXTRAPOLATION_TIME = 3f;

		// Token: 0x0400194D RID: 6477
		private const int CALCULATE_ALTITUDE_LIMIT_RAYS = 20;

		// Token: 0x0400194E RID: 6478
		private const int N_CREW_DEFAULT_HELICOPTER = 4;

		// Token: 0x0400194F RID: 6479
		private bool onApproach;

		// Token: 0x04001950 RID: 6480
		private Helicopter helicopter;

		// Token: 0x04001951 RID: 6481
		private Squad squad;

		// Token: 0x04001952 RID: 6482
		private AiActorController pilotAi;

		// Token: 0x04001953 RID: 6483
		private Vector3 landingPosition;

		// Token: 0x04001954 RID: 6484
		private bool isCompleted;

		// Token: 0x04001955 RID: 6485
		private float altitudeLimit;
	}
}

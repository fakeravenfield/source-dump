using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003CD RID: 973
	public class SpecOpsScenario : SpecOpsObjective
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x000A4A08 File Offset: 0x000A2C08
		public virtual void Initialize(SpecOpsMode specOps, SpawnPoint spawn)
		{
			this.spawn = spawn;
			this.specOps = specOps;
			int num = 3;
			if (Options.GetDropdown(OptionDropdown.Id.Difficulty) == 1)
			{
				num = 4;
			}
			else if (Options.GetDropdown(OptionDropdown.Id.Difficulty) == 2)
			{
				num = 6;
			}
			num += UnityEngine.Random.Range(0, 2);
			this.SpawnActors(spawn, specOps.defendingTeam, num);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00012B73 File Offset: 0x00010D73
		public Vector3 SpawnPointObjectivePosition()
		{
			return this.spawn.transform.position + new Vector3(0f, 10f, 0f);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000A4A58 File Offset: 0x000A2C58
		public void SpawnActors(SpawnPoint spawn, int team, int count)
		{
			this.actors = new List<Actor>();
			float maxDetectionDistance = GameManager.GameParameters().nightMode ? 60f : 100f;
			float detectionRate = GameManager.GameParameters().nightMode ? 0.33f : 0.4f;
			for (int i = 0; i < count; i++)
			{
				Actor actor = ActorManager.instance.CreateAIActor(team);
				this.actors.Add(actor);
				actor.SpawnAt(spawn.RandomSpawnPointPosition(), Quaternion.identity, null);
				AiActorController aiActorController = actor.controller as AiActorController;
				aiActorController.modifier.maxDetectionDistance = maxDetectionDistance;
				aiActorController.ActivateSlowTargetDetection(detectionRate);
			}
			Order order = new Order(Order.OrderType.PatrolBase, spawn, spawn, true);
			this.squad = new Squad(this.actors, spawn, order, null, 0f);
			this.squad.allowRequestNewOrders = false;
			this.squad.SetNotAlert(true, true);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00012B9E File Offset: 0x00010D9E
		public bool HasBeenDetected()
		{
			return this.squad.order == null || this.squad.order.type != Order.OrderType.PatrolBase;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool DetectionTest()
		{
			return false;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnDetected()
		{
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00012BC5 File Offset: 0x00010DC5
		public virtual void OnAllDefendersDead()
		{
			this.specOps.dialog.PlayAreaClearDialog(this.spawn);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00012BDD File Offset: 0x00010DDD
		public override bool CompleteObjective()
		{
			if (base.CompleteObjective())
			{
				this.allowRoamDelayAction.Start();
				return true;
			}
			return false;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000A4B38 File Offset: 0x000A2D38
		private void MakeAlert()
		{
			foreach (AiActorController aiActorController in this.squad.aiMembers)
			{
				aiActorController.modifier.maxDetectionDistance = 500f;
				aiActorController.DeactivateSlowTargetDetection();
			}
			this.squad.SetAlert(true);
			if (this.squad.order != null && this.squad.order.type == Order.OrderType.PatrolBase)
			{
				this.squad.AssignOrder(new Order(Order.OrderType.Defend, this.spawn, this.spawn, true));
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000A4BE8 File Offset: 0x000A2DE8
		private void DetectAttackers()
		{
			if (this.detected)
			{
				return;
			}
			this.detected = true;
			this.squad.AssignOrder(new Order(Order.OrderType.Defend, this.spawn, this.spawn, true));
			foreach (AiActorController aiActorController in this.squad.aiMembers)
			{
				aiActorController.modifier.canSprint = true;
			}
			this.MakeAlert();
			this.OnDetected();
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000A4C80 File Offset: 0x000A2E80
		public override void Update()
		{
			base.Update();
			if (!this.squadIsAlert && this.squad.isAlert)
			{
				this.MakeAlert();
				this.squadIsAlert = true;
			}
			if (this.squad != null && this.squad.members.Count > 0)
			{
				if (!this.squad.isAlert)
				{
					this.prepareFireFlareAction.Start();
					if (this.DetectionTest())
					{
						this.squad.SetAlert(true);
					}
				}
				else if (this.prepareFireFlareAction.TrueDone() && !this.specOps.FlareIsOnCooldown() && this.squad.AnyMemberHasSpottedTarget())
				{
					foreach (AiActorController aiActorController in this.squad.aiMembers)
					{
						if (!aiActorController.HasSpottedTarget() && !Physics.Raycast(aiActorController.actor.CenterPosition(), Vector3.up, 2232321f))
						{
							this.specOps.FireFlare(aiActorController.actor, this.spawn);
							break;
						}
					}
				}
				if (!this.squad.allowRequestNewOrders && (this.objective.isCompleted || this.objective.isFailed) && this.allowRoamDelayAction.TrueDone())
				{
					this.squad.allowRequestNewOrders = true;
					this.MakeAlert();
					foreach (AiActorController aiActorController2 in this.squad.aiMembers)
					{
						aiActorController2.modifier.canSprint = false;
					}
					this.squad.RequestNewOrder();
					this.specOps.OnEnemiesStartRoaming(this);
					return;
				}
			}
			else if (!this.allDefendersAreDead)
			{
				this.allDefendersAreDead = true;
				this.OnAllDefendersDead();
			}
		}

		// Token: 0x04001A07 RID: 6663
		private const int ACTOR_COUNT_EASY = 3;

		// Token: 0x04001A08 RID: 6664
		private const int ACTOR_COUNT_NORMAL = 4;

		// Token: 0x04001A09 RID: 6665
		private const int ACTOR_COUNT_HARD = 6;

		// Token: 0x04001A0A RID: 6666
		private const int ACTOR_COUNT_ADDITIONAL = 2;

		// Token: 0x04001A0B RID: 6667
		public List<Actor> actors;

		// Token: 0x04001A0C RID: 6668
		public Squad squad;

		// Token: 0x04001A0D RID: 6669
		public SpawnPoint spawn;

		// Token: 0x04001A0E RID: 6670
		private TimedAction allowRoamDelayAction = new TimedAction(30f, false);

		// Token: 0x04001A0F RID: 6671
		private TimedAction detectAttackersDelayAction = new TimedAction(2f, false);

		// Token: 0x04001A10 RID: 6672
		private TimedAction prepareFireFlareAction = new TimedAction(10f, false);

		// Token: 0x04001A11 RID: 6673
		private bool detectionStarted;

		// Token: 0x04001A12 RID: 6674
		[NonSerialized]
		public bool detected;

		// Token: 0x04001A13 RID: 6675
		private bool anyDefenderHasFired;

		// Token: 0x04001A14 RID: 6676
		private bool allDefendersAreDead;

		// Token: 0x04001A15 RID: 6677
		private bool squadIsAlert;

		// Token: 0x04001A16 RID: 6678
		public bool defenderSpottedByAttackers;
	}
}

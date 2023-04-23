using System;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A1 RID: 929
	public class AssassinateScenario : SpecOpsScenario
	{
		// Token: 0x06001706 RID: 5894 RVA: 0x000A10F0 File Offset: 0x0009F2F0
		public override void Initialize(SpecOpsMode specOps, SpawnPoint spawn)
		{
			base.Initialize(specOps, spawn);
			this.targetActor = this.actors[this.actors.Count - 1];
			this.targetActor.name = "Lt. " + this.targetActor.name;
			this.targetActor.AddAccessory(specOps.commOfficerAccessory);
			this.objective = ObjectiveUi.CreateObjective("Neutralize Comm. Officer", base.SpawnPointObjectivePosition());
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000A116C File Offset: 0x0009F36C
		public override void Update()
		{
			base.Update();
			if (!this.targetActor.dead)
			{
				if (!this.targetIdentified)
				{
					Camera activeCamera = FpsActorController.instance.GetActiveCamera();
					if (Vector3.Dot((this.targetActor.CenterPosition() - activeCamera.transform.position).normalized, activeCamera.transform.forward) <= 0.99f || !ActorManager.ActorCanSeePlayer(this.targetActor) || ActorManager.ActorDistanceToPlayer(this.targetActor) >= 300f)
					{
						this.identifyAction.Start();
					}
					else if (this.identifyAction.TrueDone())
					{
						this.targetIdentified = true;
						this.specOps.OnAssassinationTargetIdentified(this.targetActor);
					}
				}
				if (this.targetIdentified)
				{
					this.objective.SetWorldTarget(this.targetActor.CenterPosition());
				}
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00012279 File Offset: 0x00010479
		public override void OnDetected()
		{
			base.OnDetected();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00012281 File Offset: 0x00010481
		public override bool IsCompletedTest()
		{
			return this.targetActor.dead;
		}

		// Token: 0x04001930 RID: 6448
		private const float IDENTIFY_DISTANCE = 300f;

		// Token: 0x04001931 RID: 6449
		private const float IDENTIFY_DOT = 0.99f;

		// Token: 0x04001932 RID: 6450
		private Actor targetActor;

		// Token: 0x04001933 RID: 6451
		private bool targetIdentified;

		// Token: 0x04001934 RID: 6452
		private TimedAction identifyAction = new TimedAction(1f, false);
	}
}

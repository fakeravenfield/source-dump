using System;
using System.Linq;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A2 RID: 930
	public class ClearScenario : SpecOpsScenario
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x000A1254 File Offset: 0x0009F454
		public override void Initialize(SpecOpsMode specOps, SpawnPoint spawn)
		{
			base.Initialize(specOps, spawn);
			this.attackingTeam = specOps.attackingTeam;
			this.defendingTeam = specOps.defendingTeam;
			CapturePoint capturePoint = spawn as CapturePoint;
			if (capturePoint != null)
			{
				capturePoint.canCapture = true;
			}
			this.objective = ObjectiveUi.CreateObjective("Clear " + spawn.shortName, base.SpawnPointObjectivePosition());
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000A12BC File Offset: 0x0009F4BC
		public override bool DetectionTest()
		{
			CapturePoint capturePoint = this.spawn as CapturePoint;
			return capturePoint != null && capturePoint.owner != this.defendingTeam;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void OnAllDefendersDead()
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000A12F4 File Offset: 0x0009F4F4
		public override bool IsCompletedTest()
		{
			if (this.spawn.owner != this.attackingTeam)
			{
				return this.actors.All((Actor a) => a.dead);
			}
			return true;
		}

		// Token: 0x04001935 RID: 6453
		private int attackingTeam;

		// Token: 0x04001936 RID: 6454
		private int defendingTeam;
	}
}

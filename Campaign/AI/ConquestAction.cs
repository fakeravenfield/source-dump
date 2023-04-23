using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.AI
{
	// Token: 0x020003FD RID: 1021
	public class ConquestAction
	{
		// Token: 0x060019AE RID: 6574 RVA: 0x00013DD5 File Offset: 0x00011FD5
		public ConquestAction(LevelClickable destination)
		{
			this.destination = destination;
			this.minSourceEnemyHQDistance = int.MaxValue;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00013E01 File Offset: 0x00012001
		public bool IsMoveSequence()
		{
			return this.type != ConquestAction.Type.Recruit;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000AA7B0 File Offset: 0x000A89B0
		public void GenerateType(WorldState worldState)
		{
			LevelState levelState = worldState.GetLevelState(this.destination);
			if (this.destination.owner == worldState.theirTeam)
			{
				this.type = ConquestAction.Type.Attack;
				return;
			}
			if (this.destination.owner == -1)
			{
				this.type = ConquestAction.Type.CaptureNeutral;
				return;
			}
			if (levelState.recruitAvailable)
			{
				this.type = ConquestAction.Type.Recruit;
				return;
			}
			if (levelState.enemyArmyDistance <= 1)
			{
				this.type = ConquestAction.Type.Defend;
				return;
			}
			this.type = ConquestAction.Type.Idle;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000AA824 File Offset: 0x000A8A24
		public override string ToString()
		{
			string[] array = new string[8];
			array[0] = "ConquestAction: type=";
			array[1] = this.type.ToString();
			array[2] = ", destination=";
			array[3] = this.destination.displayName;
			array[4] = ", sources=";
			array[5] = this.possibleSources.Count.ToString();
			array[6] = ", decision=";
			int num = 7;
			ConquestActionDecisionData conquestActionDecisionData = this.decision;
			array[num] = conquestActionDecisionData.ToString();
			return string.Concat(array);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00013E0F File Offset: 0x0001200F
		public int GetMaxAvailableSoldiersCanReachDestination()
		{
			return Mathf.Min(this.allAvailableSoldiersCount, 3);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00013E1D File Offset: 0x0001201D
		public bool IsValid()
		{
			return this.type == ConquestAction.Type.Recruit || (this.decision.IsValid() && this.allAvailableSoldiersCount >= this.decision.min);
		}

		// Token: 0x04001B6F RID: 7023
		public LevelClickable destination;

		// Token: 0x04001B70 RID: 7024
		public List<LevelClickable> possibleSources = new List<LevelClickable>();

		// Token: 0x04001B71 RID: 7025
		public List<ConquestMove> moves;

		// Token: 0x04001B72 RID: 7026
		public int allAvailableSoldiersCount;

		// Token: 0x04001B73 RID: 7027
		public ConquestActionDecisionData decision;

		// Token: 0x04001B74 RID: 7028
		public ConquestAction.Type type = ConquestAction.Type.Idle;

		// Token: 0x04001B75 RID: 7029
		public int minSourceEnemyHQDistance;

		// Token: 0x020003FE RID: 1022
		public enum Type
		{
			// Token: 0x04001B77 RID: 7031
			Attack,
			// Token: 0x04001B78 RID: 7032
			Defend,
			// Token: 0x04001B79 RID: 7033
			CaptureNeutral,
			// Token: 0x04001B7A RID: 7034
			Idle,
			// Token: 0x04001B7B RID: 7035
			Recruit
		}
	}
}

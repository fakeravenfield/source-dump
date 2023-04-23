using System;
using UnityEngine;

namespace Campaign.AI
{
	// Token: 0x02000407 RID: 1031
	public static class ConquestAiHeuristics
	{
		// Token: 0x060019E7 RID: 6631 RVA: 0x000AB490 File Offset: 0x000A9690
		public static ConquestActionDecisionData GetControlResourceActionScore(ConquestAction action, WorldState worldState)
		{
			float num = ConquestAiHeuristics.CalculateProductionScore(action.destination);
			bool flag = worldState.HasOverallResourceAdvantage();
			float num2 = flag ? 0.5f : 1f;
			float num3 = flag ? 1f : 0.5f;
			if (action.type == ConquestAction.Type.Defend)
			{
				ConquestActionDecisionData result = ConquestAiHeuristics.BaseDefendDecisionData(action, worldState);
				result.score *= num * num3;
				return result;
			}
			if (action.type == ConquestAction.Type.Attack)
			{
				ConquestActionDecisionData result2 = ConquestAiHeuristics.BaseAttackDecisionData(action, worldState);
				result2.score *= num * num2;
				return result2;
			}
			if (action.type == ConquestAction.Type.CaptureNeutral)
			{
				ConquestActionDecisionData result3 = ConquestAiHeuristics.BaseMovementDecisionData(action, worldState);
				result3.score *= num;
				return result3;
			}
			return ConquestActionDecisionData.DontCare;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000AB538 File Offset: 0x000A9738
		public static ConquestActionDecisionData GetAttackHQActionScore(ConquestAction action, WorldState worldState)
		{
			LevelState levelState = worldState.GetLevelState(action.destination);
			if (levelState.enemyHqDistance >= action.minSourceEnemyHQDistance)
			{
				return ConquestActionDecisionData.DontCare;
			}
			if (levelState.isEnemy && levelState.soldierCount > 0)
			{
				return ConquestAiHeuristics.BaseAttackDecisionData(action, worldState);
			}
			return new ConquestActionDecisionData(50f);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000AB58C File Offset: 0x000A978C
		public static ConquestActionDecisionData GetRecruitActionScore(ConquestAction action, WorldState worldState)
		{
			worldState.GetLevelState(action.destination);
			if (action.type == ConquestAction.Type.Recruit)
			{
				ConquestActionDecisionData result = ConquestAiHeuristics.BaseDefendDecisionData(action, worldState);
				result.score += 1000f - ConquestAiHeuristics.GetResourceWeight(action.destination.recruitCostType, action.destination.recruitCost);
				return result;
			}
			return ConquestActionDecisionData.DontCare;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000AB5EC File Offset: 0x000A97EC
		public static ConquestActionDecisionData BaseAttackDecisionData(ConquestAction action, WorldState worldState)
		{
			ConquestActionDecisionData dontCare = ConquestActionDecisionData.DontCare;
			if (action.destination.owner == worldState.theirTeam)
			{
				int count = action.destination.soldiers.Count;
				int value = action.allAvailableSoldiersCount - count + 3;
				dontCare.score = ConquestAiHeuristics.BASE_ATTACK_SCORE[Mathf.Clamp(value, 0, ConquestAiHeuristics.BASE_ATTACK_SCORE.Length)];
				dontCare.min = count - 1;
			}
			else
			{
				dontCare.score = 100f;
			}
			return dontCare;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000AB664 File Offset: 0x000A9864
		public static ConquestActionDecisionData BaseDefendDecisionData(ConquestAction action, WorldState worldState)
		{
			ConquestActionDecisionData dontCare = ConquestActionDecisionData.DontCare;
			if (action.destination.owner == worldState.myTeam)
			{
				int maxAvailableSoldiersCanReachDestination = action.GetMaxAvailableSoldiersCanReachDestination();
				int num = Mathf.Min(worldState.GetLevelState(action.destination).adjacentEnemyCount, 3);
				int value = num - maxAvailableSoldiersCanReachDestination + 3;
				dontCare.score = ConquestAiHeuristics.BASE_DEFENSE_SCORE[Mathf.Clamp(value, 0, ConquestAiHeuristics.BASE_DEFENSE_SCORE.Length)];
				dontCare.min = num - 1;
			}
			else
			{
				dontCare.score = 0f;
			}
			return dontCare;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00013FE1 File Offset: 0x000121E1
		public static ConquestActionDecisionData BaseMovementDecisionData(ConquestAction action, WorldState worldState)
		{
			return ConquestAiHeuristics.BaseDefendDecisionData(action, worldState);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00013FEA File Offset: 0x000121EA
		public static float BaseResourceWeight(ResourceType type)
		{
			return ConquestAiHeuristics.resourceWeight[(int)type];
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00013FF3 File Offset: 0x000121F3
		public static float GetResourceWeight(ResourceType type, int amount)
		{
			return ConquestAiHeuristics.BaseResourceWeight(type) * (float)amount;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000AB6E4 File Offset: 0x000A98E4
		public static float CalculateProductionScore(LevelClickable level)
		{
			float num = 0f;
			foreach (LevelClickable.ResourceProduction resourceProduction in level.production)
			{
				num += ConquestAiHeuristics.GetResourceWeight(resourceProduction.type, resourceProduction.amount);
			}
			return num;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000AB728 File Offset: 0x000A9928
		// Note: this type is marked as 'beforefieldinit'.
		static ConquestAiHeuristics()
		{
			float[] array = new float[16];
			array[0] = 0.01f;
			array[1] = 0.1f;
			ConquestAiHeuristics.resourceWeight = array;
			ConquestAiHeuristics.BASE_ATTACK_SCORE = new float[]
			{
				0f,
				0f,
				30f,
				80f,
				100f,
				100f,
				100f
			};
			ConquestAiHeuristics.BASE_DEFENSE_SCORE = new float[]
			{
				0f,
				30f,
				70f,
				80f,
				80f,
				80f,
				80f
			};
		}

		// Token: 0x04001B94 RID: 7060
		public static float[] resourceWeight;

		// Token: 0x04001B95 RID: 7061
		public static readonly float[] BASE_ATTACK_SCORE;

		// Token: 0x04001B96 RID: 7062
		private const int BASE_ATTACK_OFFSET = 3;

		// Token: 0x04001B97 RID: 7063
		public static readonly float[] BASE_DEFENSE_SCORE;

		// Token: 0x04001B98 RID: 7064
		private const int BASE_DEFENSE_OFFSET = 3;

		// Token: 0x04001B99 RID: 7065
		private const float BASE_RECRUIT_SCORE = 1000f;
	}
}

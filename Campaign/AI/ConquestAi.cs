using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.AI
{
	// Token: 0x02000400 RID: 1024
	public class ConquestAi
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x000AA908 File Offset: 0x000A8B08
		public ConquestAi(ConquestCampaign conquest)
		{
			this.conquest = conquest;
			this.actionScoreCallbacks = new List<ConquestAi.DelGetActionScore>
			{
				new ConquestAi.DelGetActionScore(ConquestAiHeuristics.GetAttackHQActionScore),
				new ConquestAi.DelGetActionScore(ConquestAiHeuristics.GetControlResourceActionScore),
				new ConquestAi.DelGetActionScore(ConquestAiHeuristics.GetRecruitActionScore)
			};
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000AA964 File Offset: 0x000A8B64
		public void NewTurn()
		{
			this.worldState = new WorldState(this.conquest);
			Debug.Log("AI Resources:");
			for (int i = 0; i < 3; i++)
			{
				ResourceType resourceType = (ResourceType)i;
				Debug.Log(resourceType.ToString() + ": " + this.worldState.myResources.availableResources[i].ToString());
			}
			this.conquest.StartCoroutine(this.Turn());
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00013EDF File Offset: 0x000120DF
		private IEnumerator Turn()
		{
			yield return this.conquest.StartCoroutine(this.UpgradeTech());
			yield return this.conquest.StartCoroutine(this.DoMoves());
			yield break;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00013EEE File Offset: 0x000120EE
		private IEnumerator UpgradeTech()
		{
			while (CampaignBase.instance.TryBuyNextAiTech())
			{
				yield return 0;
			}
			yield break;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00013EF6 File Offset: 0x000120F6
		private IEnumerator DoMoves()
		{
			yield return new WaitForSecondsRealtime(1f);
			while (this.NewMove())
			{
				while (this.conquest.PauseAiExecution())
				{
					yield return new WaitForSeconds(0.1f);
				}
				yield return new WaitForSecondsRealtime(1f);
			}
			this.EndTurn();
			yield break;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00013F05 File Offset: 0x00012105
		public void EndTurn()
		{
			this.conquest.EndAiTurn();
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		private bool NewMove()
		{
			bool result;
			try
			{
				this.worldState = new WorldState(this.conquest);
				if (this.worldState.myHqState == null)
				{
					result = false;
				}
				else
				{
					if (this.currentAction == null)
					{
						List<ConquestAction> list = this.FindAndSortActions();
						Debug.Log("Available actions: " + list.Count.ToString());
						if (list.Count == 0 || list[0].decision.score <= 0f)
						{
							return false;
						}
						this.currentAction = list[0];
						list.RemoveAt(0);
						if (this.currentAction.IsMoveSequence())
						{
							this.currentSequence = this.GenerateActionMoves(this.currentAction, list);
						}
						else
						{
							this.currentSequence = null;
						}
					}
					this.CarryOutCurrentAction();
					result = true;
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000AAACC File Offset: 0x000A8CCC
		private void CarryOutCurrentAction()
		{
			if (this.currentAction.IsMoveSequence())
			{
				string str = "Playing move sequence ";
				ConquestMoveSequence conquestMoveSequence = this.currentSequence;
				Debug.Log(str + ((conquestMoveSequence != null) ? conquestMoveSequence.ToString() : null));
				if (this.currentSequence.IsCompleted())
				{
					this.currentAction = null;
					return;
				}
				ConquestMove move = this.currentSequence.Dequeue();
				this.conquest.CarryOutAiMove(move);
				if (this.currentSequence.IsCompleted())
				{
					this.currentAction = null;
					return;
				}
			}
			else
			{
				if (this.currentAction.type == ConquestAction.Type.Recruit)
				{
					ConquestCampaign.instance.TryRecruit(this.currentAction.destination);
				}
				this.currentAction = null;
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000AAB74 File Offset: 0x000A8D74
		private ConquestMoveSequence GenerateActionMoves(ConquestAction action, List<ConquestAction> sortedActions)
		{
			Debug.Log("Carrying out action: " + ((action != null) ? action.ToString() : null));
			action.moves = new List<ConquestMove>();
			List<ConquestMove.Source> list = new List<ConquestMove.Source>();
			Dictionary<LevelClickable, int> dictionary = new Dictionary<LevelClickable, int>();
			foreach (LevelClickable key in this.conquest.levels)
			{
				dictionary.Add(key, 0);
			}
			int num = Mathf.Min(sortedActions.Count, 6);
			for (int i = 0; i < num; i++)
			{
				ConquestAction conquestAction = sortedActions[i];
				if (conquestAction.decision.score <= 0f)
				{
					break;
				}
				foreach (LevelClickable levelClickable in conquestAction.possibleSources)
				{
					Dictionary<LevelClickable, int> dictionary2 = dictionary;
					LevelClickable key2 = levelClickable;
					dictionary2[key2] += conquestAction.decision.max;
				}
			}
			Dictionary<int, List<LevelClickable>> dictionary3 = new Dictionary<int, List<LevelClickable>>();
			foreach (LevelClickable levelClickable2 in action.possibleSources)
			{
				int key3 = dictionary[levelClickable2];
				if (!dictionary3.ContainsKey(key3))
				{
					dictionary3.Add(key3, new List<LevelClickable>());
				}
				dictionary3[key3].Add(levelClickable2);
			}
			List<int> list2 = new List<int>(dictionary3.Keys);
			list2.Sort();
			int num2 = action.decision.max;
			int num3 = 0;
			foreach (int key4 in list2)
			{
				foreach (LevelClickable levelClickable3 in dictionary3[key4])
				{
					int b = levelClickable3.UntappedSoldierCount();
					int num4 = Mathf.Min(num2, b);
					Debug.Log("Remaining picks = " + num2.ToString());
					string[] array = new string[6];
					array[0] = "Level ";
					int num5 = 1;
					LevelClickable levelClickable4 = levelClickable3;
					array[num5] = ((levelClickable4 != null) ? levelClickable4.ToString() : null);
					array[2] = " at dep ";
					array[3] = key4.ToString();
					array[4] = ": picks=";
					array[5] = num4.ToString();
					Debug.Log(string.Concat(array));
					list.Add(new ConquestMove.Source(levelClickable3, num4));
					num3 += num4;
					num2 = Mathf.Max(0, num2 - num4);
					if (num2 == 0)
					{
						break;
					}
				}
				if (num2 == 0)
				{
					break;
				}
			}
			if (num3 >= action.decision.min)
			{
				return this.FinalizeActionMoves(action, list);
			}
			throw new Exception("Action could not be carried out, not enough untapped soldiers found.");
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000AAE7C File Offset: 0x000A907C
		private ConquestMoveSequence FinalizeActionMoves(ConquestAction action, List<ConquestMove.Source> sources)
		{
			ConquestMoveSequence conquestMoveSequence = new ConquestMoveSequence();
			Dictionary<LevelClickable, LevelClickable> djikstraParents = new Dictionary<LevelClickable, LevelClickable>();
			this.worldState.RunDjikstra((LevelClickable level) => level == action.destination, delegate(LevelClickable node, LevelClickable parent, int distance)
			{
				djikstraParents.Add(node, parent);
			}, this.conquest.aiTeam);
			int num = 0;
			ConquestMove conquestMove = new ConquestMove(action.destination);
			foreach (ConquestMove.Source source in sources)
			{
				LevelClickable levelClickable = source.level;
				while (levelClickable != action.destination)
				{
					if (num++ > 10000)
					{
						throw new Exception("Pathfinding node loop detected!");
					}
					LevelClickable levelClickable2 = djikstraParents[levelClickable];
					if (levelClickable2 == action.destination)
					{
						break;
					}
					ConquestMove conquestMove2 = new ConquestMove(levelClickable2);
					conquestMove2.AddSource(levelClickable, source.count);
					conquestMoveSequence.EnqueueMove(conquestMove2);
					levelClickable = levelClickable2;
				}
				conquestMove.AddSource(levelClickable, source.count);
			}
			conquestMoveSequence.EnqueueMove(conquestMove);
			return conquestMoveSequence;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000AAFC0 File Offset: 0x000A91C0
		private List<ConquestAction> FindAndSortActions()
		{
			List<ConquestAction> list = new List<ConquestAction>();
			foreach (LevelClickable destination in this.worldState.reachableLevels)
			{
				ConquestAction item = this.GenerateAction(destination);
				list.Add(item);
			}
			this.RemoveInvalidActions(list);
			foreach (ConquestAction conquestAction in list)
			{
				conquestAction.decision.ClampMinMax();
			}
			list.Sort((ConquestAction a, ConquestAction b) => b.decision.score.CompareTo(a.decision.score));
			return list;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000AB094 File Offset: 0x000A9294
		private void RemoveInvalidActions(List<ConquestAction> actions)
		{
			int num = actions.RemoveAll((ConquestAction action) => !action.IsValid());
			if (num > 0)
			{
				Debug.Log("Removed " + num.ToString() + " invalid actions");
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000AB0E8 File Offset: 0x000A92E8
		public ConquestAction GenerateAction(LevelClickable destination)
		{
			ConquestAction conquestAction = new ConquestAction(destination);
			conquestAction.GenerateType(this.worldState);
			List<LevelClickable> reachableLevelsFrom = ConquestAi.GetReachableLevelsFrom(destination);
			reachableLevelsFrom.RemoveAll((LevelClickable source) => source.owner != this.conquest.aiTeam || !source.HasAnyUntappedSoldiers());
			conquestAction.possibleSources = reachableLevelsFrom;
			conquestAction.allAvailableSoldiersCount = 0;
			foreach (LevelClickable levelClickable in conquestAction.possibleSources)
			{
				int num = levelClickable.UntappedSoldierCount();
				conquestAction.allAvailableSoldiersCount = Mathf.Min(conquestAction.allAvailableSoldiersCount + num, 3);
				if (num > 0)
				{
					LevelState levelState = this.worldState.GetLevelState(levelClickable);
					conquestAction.minSourceEnemyHQDistance = Mathf.Min(levelState.enemyHqDistance, conquestAction.minSourceEnemyHQDistance);
				}
			}
			ConquestActionDecisionData conquestActionDecisionData = new ConquestActionDecisionData(0f);
			foreach (ConquestAi.DelGetActionScore delGetActionScore in this.actionScoreCallbacks)
			{
				ConquestActionDecisionData conquestActionDecisionData2 = delGetActionScore(conquestAction, this.worldState);
				if (conquestAction.allAvailableSoldiersCount >= conquestActionDecisionData2.min)
				{
					conquestActionDecisionData = ConquestActionDecisionData.Join(conquestActionDecisionData, conquestActionDecisionData2);
				}
			}
			conquestAction.decision = conquestActionDecisionData;
			return conquestAction;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00013F12 File Offset: 0x00012112
		public static List<LevelClickable> GetReachableLevelsFrom(LevelClickable source)
		{
			return ConquestAi.GetReachableLevelsFrom(new List<LevelClickable>(1)
			{
				source
			});
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000AB230 File Offset: 0x000A9430
		public static List<LevelClickable> GetReachableLevelsFrom(List<LevelClickable> sources)
		{
			HashSet<LevelClickable> hashSet = new HashSet<LevelClickable>(sources);
			Queue<LevelClickable> queue = new Queue<LevelClickable>();
			using (List<LevelClickable>.Enumerator enumerator = sources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LevelClickable item = enumerator.Current;
					queue.Enqueue(item);
				}
				goto IL_96;
			}
			IL_3E:
			foreach (LevelClickable levelClickable in queue.Dequeue().GetConnectedLevels())
			{
				if (!hashSet.Contains(levelClickable))
				{
					hashSet.Add(levelClickable);
					if (levelClickable.moveAgain)
					{
						queue.Enqueue(levelClickable);
					}
				}
			}
			IL_96:
			if (queue.Count <= 0)
			{
				return new List<LevelClickable>(hashSet);
			}
			goto IL_3E;
		}

		// Token: 0x04001B7F RID: 7039
		private const int CONSIDER_AHEAD_ACTIONS = 6;

		// Token: 0x04001B80 RID: 7040
		private const float TIME_BEFORE_FIRST_MOVE = 1f;

		// Token: 0x04001B81 RID: 7041
		private const float TIME_BETWEEN_MOVES = 1f;

		// Token: 0x04001B82 RID: 7042
		private ConquestCampaign conquest;

		// Token: 0x04001B83 RID: 7043
		private WorldState worldState;

		// Token: 0x04001B84 RID: 7044
		private ConquestAction currentAction;

		// Token: 0x04001B85 RID: 7045
		private ConquestMoveSequence currentSequence;

		// Token: 0x04001B86 RID: 7046
		private List<ConquestAi.DelGetActionScore> actionScoreCallbacks;

		// Token: 0x02000401 RID: 1025
		// (Invoke) Token: 0x060019CB RID: 6603
		private delegate ConquestActionDecisionData DelGetActionScore(ConquestAction action, WorldState worldState);
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign.AI
{
	// Token: 0x0200040C RID: 1036
	public class WorldState
	{
		// Token: 0x06001A00 RID: 6656 RVA: 0x000ABB3C File Offset: 0x000A9D3C
		public WorldState(ConquestCampaign conquest)
		{
			this.conquest = conquest;
			this.myTeam = conquest.aiTeam;
			this.theirTeam = conquest.playerTeam;
			this.myResources = CampaignBase.cachedState.resources[conquest.aiTeam];
			this.theirResources = CampaignBase.cachedState.resources[conquest.playerTeam];
			for (int i = 0; i < 16; i++)
			{
				this.resourceGainAdvantage[i] = this.myResources.resourceGain[i] - this.theirResources.resourceGain[i];
			}
			this.levelState = new Dictionary<LevelClickable, LevelState>();
			this.allLevelStates = new List<LevelState>();
			this.availableUntappedLevels = new List<LevelState>();
			this.FindReachableLevels();
			foreach (LevelClickable levelClickable in conquest.levels)
			{
				LevelState levelState = new LevelState(this.conquest, levelClickable);
				this.levelState.Add(levelClickable, levelState);
				this.allLevelStates.Add(levelState);
				if (levelState.HasUntappedSoldiers())
				{
					this.availableUntappedLevels.Add(levelState);
				}
				if (levelClickable.isHq && levelClickable.owner == CampaignBase.instance.aiTeam)
				{
					this.myHqState = levelState;
				}
			}
			this.FindLevelDistances();
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000ABCA0 File Offset: 0x000A9EA0
		private void FindReachableLevels()
		{
			List<LevelClickable> list = new List<LevelClickable>();
			foreach (LevelClickable levelClickable in this.conquest.levels)
			{
				if (levelClickable.owner == this.conquest.aiTeam && levelClickable.HasAnyUntappedSoldiers())
				{
					list.Add(levelClickable);
				}
			}
			this.reachableLevels = ConquestAi.GetReachableLevelsFrom(list);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000140B2 File Offset: 0x000122B2
		public LevelState GetLevelState(LevelClickable level)
		{
			return this.levelState[level];
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000ABD28 File Offset: 0x000A9F28
		private void FindLevelDistances()
		{
			int enemyTeam = CampaignBase.instance.playerTeam;
			int myTeam = CampaignBase.instance.aiTeam;
			this.RunDjikstra((LevelClickable level) => level.isHq && level.defaultOwner == (SpawnPoint.Team)myTeam, delegate(LevelClickable level, LevelClickable parent, int distance)
			{
				this.GetLevelState(level).myHqDistance = distance;
			}, this.conquest.aiTeam);
			this.RunDjikstra((LevelClickable level) => level.isHq && level.defaultOwner == (SpawnPoint.Team)enemyTeam, delegate(LevelClickable level, LevelClickable parent, int distance)
			{
				this.GetLevelState(level).enemyHqDistance = distance;
			}, this.conquest.aiTeam);
			this.RunDjikstra((LevelClickable level) => level.owner == myTeam && level.soldiers.Count > 0, delegate(LevelClickable level, LevelClickable parent, int distance)
			{
				this.GetLevelState(level).myArmyDistance = distance;
			}, this.conquest.aiTeam);
			this.RunDjikstra((LevelClickable level) => level.owner == enemyTeam && level.soldiers.Count > 0, delegate(LevelClickable level, LevelClickable parent, int distance)
			{
				this.GetLevelState(level).enemyArmyDistance = distance;
			}, this.conquest.playerTeam);
			this.closestEnemyHQDistance = int.MaxValue;
			foreach (LevelState levelState in this.allLevelStates)
			{
				if (levelState.isMine)
				{
					this.closestEnemyHQDistance = Mathf.Min(this.closestEnemyHQDistance, levelState.enemyHqDistance);
				}
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000ABE70 File Offset: 0x000AA070
		public void RunDjikstra(WorldState.DjikstraIsRootDel IsRoot, WorldState.DjikstraWriteBackDel WriteBack, int team)
		{
			this.djikstraTraversedLevels = new List<LevelClickable>();
			this.djikstraTraversalQueue = new List<LevelClickable>();
			this.djikstraDistance = new Dictionary<LevelClickable, int>();
			this.djikstraParent = new Dictionary<LevelClickable, LevelClickable>();
			foreach (LevelState levelState in this.allLevelStates)
			{
				LevelClickable level = levelState.level;
				if (IsRoot(level))
				{
					this.DjikstraEnqueue(level, null, 0);
				}
			}
			this.TraverseNext(team);
			foreach (LevelState levelState2 in this.allLevelStates)
			{
				LevelClickable level2 = levelState2.level;
				if (this.djikstraDistance.ContainsKey(level2))
				{
					WriteBack(level2, this.djikstraParent[level2], this.djikstraDistance[level2]);
				}
				else
				{
					WriteBack(level2, null, -1);
				}
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000ABF80 File Offset: 0x000AA180
		private int DjikstraMovementPenaltyOfLevel(LevelClickable level, int team)
		{
			if (level.owner != team)
			{
				return 1;
			}
			if (team == this.conquest.aiTeam)
			{
				if (!level.moveAgain)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (team != this.conquest.playerTeam)
				{
					return 1;
				}
				if (!level.isMoveAgainTile)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000ABFD0 File Offset: 0x000AA1D0
		private void TraverseNext(int team)
		{
			if (this.djikstraTraversalQueue.Count == 0)
			{
				return;
			}
			this.djikstraTraversalQueue.Sort((LevelClickable a, LevelClickable b) => this.djikstraDistance[a].CompareTo(this.djikstraDistance[b]));
			LevelClickable levelClickable = this.djikstraTraversalQueue[0];
			int newDistance = this.djikstraDistance[levelClickable] + this.DjikstraMovementPenaltyOfLevel(levelClickable, team);
			this.djikstraTraversalQueue.RemoveAt(0);
			this.djikstraTraversedLevels.Add(levelClickable);
			foreach (LevelClickable level in levelClickable.GetConnectedLevels())
			{
				this.DjikstraEnqueue(level, levelClickable, newDistance);
			}
			this.TraverseNext(team);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000AC088 File Offset: 0x000AA288
		private void DjikstraEnqueue(LevelClickable level, LevelClickable parent, int newDistance)
		{
			if (this.djikstraTraversedLevels.Contains(level))
			{
				return;
			}
			if (!this.djikstraDistance.ContainsKey(level))
			{
				this.djikstraDistance.Add(level, newDistance);
				this.djikstraParent.Add(level, parent);
				this.djikstraTraversalQueue.Add(level);
				return;
			}
			if (newDistance < this.djikstraDistance[level])
			{
				this.djikstraDistance[level] = newDistance;
				this.djikstraParent[level] = parent;
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000AC104 File Offset: 0x000AA304
		public bool HasOverallResourceAdvantage()
		{
			float num = 0f;
			foreach (object obj in Enum.GetValues(typeof(ResourceType)))
			{
				ResourceType resourceType = (ResourceType)obj;
				num += (float)this.resourceGainAdvantage[(int)resourceType] * ConquestAiHeuristics.BaseResourceWeight(resourceType);
			}
			return num > 0f;
		}

		// Token: 0x04001BAF RID: 7087
		private Dictionary<LevelClickable, LevelState> levelState;

		// Token: 0x04001BB0 RID: 7088
		public List<LevelState> allLevelStates;

		// Token: 0x04001BB1 RID: 7089
		public List<LevelState> availableUntappedLevels;

		// Token: 0x04001BB2 RID: 7090
		public List<LevelClickable> reachableLevels;

		// Token: 0x04001BB3 RID: 7091
		public int[] resourceGainAdvantage = new int[16];

		// Token: 0x04001BB4 RID: 7092
		public ConquestTeamResources myResources;

		// Token: 0x04001BB5 RID: 7093
		public ConquestTeamResources theirResources;

		// Token: 0x04001BB6 RID: 7094
		public int myTeam;

		// Token: 0x04001BB7 RID: 7095
		public int theirTeam;

		// Token: 0x04001BB8 RID: 7096
		public LevelState myHqState;

		// Token: 0x04001BB9 RID: 7097
		public int closestEnemyHQDistance;

		// Token: 0x04001BBA RID: 7098
		public ConquestCampaign conquest;

		// Token: 0x04001BBB RID: 7099
		private List<LevelClickable> djikstraTraversedLevels;

		// Token: 0x04001BBC RID: 7100
		private List<LevelClickable> djikstraTraversalQueue;

		// Token: 0x04001BBD RID: 7101
		private Dictionary<LevelClickable, int> djikstraDistance;

		// Token: 0x04001BBE RID: 7102
		private Dictionary<LevelClickable, LevelClickable> djikstraParent;

		// Token: 0x0200040D RID: 1037
		// (Invoke) Token: 0x06001A0B RID: 6667
		public delegate bool DjikstraIsRootDel(LevelClickable levelState);

		// Token: 0x0200040E RID: 1038
		// (Invoke) Token: 0x06001A0F RID: 6671
		public delegate void DjikstraWriteBackDel(LevelClickable levelState, LevelClickable parent, int distance);
	}
}

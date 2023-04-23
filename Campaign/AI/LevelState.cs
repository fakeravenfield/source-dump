using System;
using System.Collections.Generic;

namespace Campaign.AI
{
	// Token: 0x0200040B RID: 1035
	public class LevelState
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x000AB8CC File Offset: 0x000A9ACC
		public LevelState(ConquestCampaign conquest, LevelClickable level)
		{
			this.level = level;
			this.isEnemy = (level.owner == CampaignBase.instance.playerTeam);
			this.isNeutral = (level.owner == -1);
			this.isMine = (!this.isEnemy && !this.isNeutral);
			this.soldierCount = this.level.soldiers.Count;
			this.untappedSoldiers = 0;
			this.recruitAvailable = (this.isMine && level.CanRecruit() && ConquestCampaign.instance.CanAffordTransaction(CampaignBase.instance.aiTeam, level.recruitCostType, level.recruitCost));
			if (this.isMine)
			{
				this.friendlySoldierCount = this.soldierCount;
			}
			if (this.isMine)
			{
				using (List<SoldierMiniature>.Enumerator enumerator = this.level.soldiers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.isTapped)
						{
							this.untappedSoldiers++;
						}
					}
				}
			}
			this.adjacentEnemyCount = 0;
			this.adjacentFriendlyCount = 0;
			this.adjacentUntappedFriendlyCount = 0;
			foreach (LevelClickable levelClickable in this.level.GetConnectedLevels())
			{
				if (levelClickable.owner == CampaignBase.instance.playerTeam)
				{
					this.adjacentEnemyCount += levelClickable.soldiers.Count;
				}
				else if (levelClickable.owner == CampaignBase.instance.aiTeam)
				{
					this.adjacentFriendlyCount += levelClickable.soldiers.Count;
					using (List<SoldierMiniature>.Enumerator enumerator = levelClickable.soldiers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.isTapped)
							{
								this.adjacentUntappedFriendlyCount++;
							}
						}
					}
				}
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00014075 File Offset: 0x00012275
		public float AttackScore()
		{
			return (float)this.adjacentUntappedFriendlyCount - ((float)this.soldierCount + (float)this.adjacentEnemyCount * 0.5f);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000ABAEC File Offset: 0x000A9CEC
		public int DistanceTo(LevelClickable level)
		{
			if (!this.levelDistance.ContainsKey(level))
			{
				throw new Exception("LevelState exception: " + this.level.name + " is not connected to " + level.name);
			}
			return this.levelDistance[level];
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00014094 File Offset: 0x00012294
		public bool CanReachBeforeEnemy()
		{
			return this.myArmyDistance <= this.enemyArmyDistance;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000140A7 File Offset: 0x000122A7
		public bool HasUntappedSoldiers()
		{
			return this.untappedSoldiers > 0;
		}

		// Token: 0x04001B9F RID: 7071
		public LevelClickable level;

		// Token: 0x04001BA0 RID: 7072
		public bool isEnemy;

		// Token: 0x04001BA1 RID: 7073
		public bool isNeutral;

		// Token: 0x04001BA2 RID: 7074
		public bool isMine;

		// Token: 0x04001BA3 RID: 7075
		public int adjacentEnemyCount;

		// Token: 0x04001BA4 RID: 7076
		public int adjacentFriendlyCount;

		// Token: 0x04001BA5 RID: 7077
		public int adjacentUntappedFriendlyCount;

		// Token: 0x04001BA6 RID: 7078
		public int soldierCount;

		// Token: 0x04001BA7 RID: 7079
		public int untappedSoldiers;

		// Token: 0x04001BA8 RID: 7080
		public int friendlySoldierCount;

		// Token: 0x04001BA9 RID: 7081
		public int myArmyDistance;

		// Token: 0x04001BAA RID: 7082
		public int enemyArmyDistance;

		// Token: 0x04001BAB RID: 7083
		public int myHqDistance;

		// Token: 0x04001BAC RID: 7084
		public int enemyHqDistance;

		// Token: 0x04001BAD RID: 7085
		public bool recruitAvailable;

		// Token: 0x04001BAE RID: 7086
		private Dictionary<LevelClickable, int> levelDistance;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003EF RID: 1007
	public class LevelClickable : CommandRoomClickable
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x00013890 File Offset: 0x00011A90
		public bool moveAgain
		{
			get
			{
				return this._moveAgain;
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00013898 File Offset: 0x00011A98
		public void AddConnection(LevelClickable to, LevelClickableConnection via)
		{
			this.connectionLevel.Add(via, to);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000138A7 File Offset: 0x00011AA7
		public IEnumerable<LevelClickable> GetConnectedLevels()
		{
			foreach (LevelClickableConnection key in this.connectionLevel.Keys)
			{
				yield return this.connectionLevel[key];
			}
			Dictionary<LevelClickableConnection, LevelClickable>.KeyCollection.Enumerator enumerator = default(Dictionary<LevelClickableConnection, LevelClickable>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000A886C File Offset: 0x000A6A6C
		public LevelClickableConnection GetConnectionTo(LevelClickable other)
		{
			foreach (LevelClickableConnection levelClickableConnection in this.connectionLevel.Keys)
			{
				if (this.connectionLevel[levelClickableConnection] == other)
				{
					return levelClickableConnection;
				}
			}
			return null;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000A88D8 File Offset: 0x000A6AD8
		public bool HasConnectionTo(LevelClickable other)
		{
			foreach (LevelClickableConnection key in this.connectionLevel.Keys)
			{
				if (this.connectionLevel[key] == other)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000138B7 File Offset: 0x00011AB7
		public bool HasAnySoldiers()
		{
			return this.soldiers.Count > 0;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000A8944 File Offset: 0x000A6B44
		public bool HasAnyPickedUpSoldiers()
		{
			using (List<SoldierMiniature>.Enumerator enumerator = this.soldiers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isPickedUp)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000A89A0 File Offset: 0x000A6BA0
		public List<SoldierMiniature> GetUntappedSoldiers(int count)
		{
			List<SoldierMiniature> list = new List<SoldierMiniature>(count);
			if (count == 0)
			{
				return list;
			}
			foreach (SoldierMiniature soldierMiniature in this.soldiers)
			{
				if (!soldierMiniature.isTapped)
				{
					list.Add(soldierMiniature);
					if (list.Count >= count)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000138C7 File Offset: 0x00011AC7
		public bool HasEmptySoldierSlot()
		{
			return this.soldiers.Count < 3;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000138D7 File Offset: 0x00011AD7
		public bool AddSoldier(SoldierMiniature soldier)
		{
			if (!this.HasEmptySoldierSlot())
			{
				return false;
			}
			this.soldiers.Add(soldier);
			return true;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000138F0 File Offset: 0x00011AF0
		public SoldierMiniature PopSoldier()
		{
			if (!this.HasAnySoldiers())
			{
				return null;
			}
			SoldierMiniature result = this.soldiers[this.soldiers.Count - 1];
			this.soldiers.RemoveAt(this.soldiers.Count - 1);
			return result;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000A8A14 File Offset: 0x000A6C14
		public void ReorderSoldiers()
		{
			SoldierMiniature[] array = this.soldiers.ToArray();
			this.soldiers.Clear();
			foreach (SoldierMiniature soldierMiniature in array)
			{
				soldierMiniature.level = null;
				soldierMiniature.TransitionToLevel(this, false);
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0001392C File Offset: 0x00011B2C
		public void RemoveSoldier(SoldierMiniature soldier)
		{
			this.soldiers.Remove(soldier);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000A8A58 File Offset: 0x000A6C58
		public Transform GetSoldierMiniatureRoot(SoldierMiniature soldier)
		{
			int num = this.soldiers.IndexOf(soldier);
			if (this.soldierRootTransforms == null || this.soldierRootTransforms.Length == 0 || num == -1)
			{
				return base.transform;
			}
			return this.soldierRootTransforms[num % this.soldierRootTransforms.Length];
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0001393B File Offset: 0x00011B3B
		public override void OnClick()
		{
			base.OnClick();
			CampaignBase.instance.OnLevelClicked(this);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0001394E File Offset: 0x00011B4E
		public override void OnRightClick()
		{
			base.OnRightClick();
			CampaignBase.instance.OnLevelRightClicked(this);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00013961 File Offset: 0x00011B61
		public override void OnStartHover()
		{
			base.OnStartHover();
			CampaignBase.instance.OnLevelStartHover(this);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00013974 File Offset: 0x00011B74
		public override void OnEndHover()
		{
			base.OnEndHover();
			CampaignBase.instance.OnLevelEndHover(this);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00013987 File Offset: 0x00011B87
		public override void OnHoverStay()
		{
			base.OnHoverStay();
			CampaignBase.instance.OnLevelHoverStay(this);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0001399A File Offset: 0x00011B9A
		public void HideInfo()
		{
			this.highlightPanel.Hide();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000139A7 File Offset: 0x00011BA7
		public void ShowIdleInfo()
		{
			if (this.owner == CampaignBase.instance.playerTeam && this.remainingRecruits > 0)
			{
				this.highlightPanel.ShowRecruit();
				return;
			}
			this.highlightPanel.Hide();
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000139DB File Offset: 0x00011BDB
		public void ShowFocusInfo()
		{
			this.highlightPanel.ShowFull();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x000A8AA0 File Offset: 0x000A6CA0
		public void OnNewTurn(int team)
		{
			foreach (SoldierMiniature soldierMiniature in this.soldiers)
			{
				if (soldierMiniature.team == team)
				{
					soldierMiniature.PlayWakeUpAnimation();
				}
			}
			if (this.owner == team)
			{
				if (this.isMoveAgainTile)
				{
					this.SetMoveAgainEnabled(true);
				}
				if (this.recruitsPerTurn > 0)
				{
					this.remainingRecruits = this.recruitsPerTurn;
					if (this.remainingRecruits > 0)
					{
						this.highlightPanel.UpdateRecruitCount();
					}
				}
			}
			else
			{
				this.remainingRecruits = 0;
			}
			if (team == CampaignBase.instance.playerTeam)
			{
				this.ShowIdleInfo();
				return;
			}
			this.HideInfo();
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000139E8 File Offset: 0x00011BE8
		public void OnPlayerWantsToRecruit()
		{
			if (this.owner == ConquestCampaign.instance.playerTeam)
			{
				ConquestCampaign.instance.TryRecruit(this);
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00013A08 File Offset: 0x00011C08
		public bool CanRecruit()
		{
			return this.remainingRecruits > 0 && this.soldiers.Count < 3 && ConquestCampaign.instance.GetBattalionCountOfTeam(this.owner) < ConquestCampaign.instance.maxBattalions;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00013A3F File Offset: 0x00011C3F
		public void Recruit()
		{
			this.remainingRecruits--;
			ConquestCampaign.instance.InstantiateSoldierMiniature(this, this.owner, true);
			this.highlightPanel.UpdateRecruitCount();
			if (this.remainingRecruits == 0)
			{
				this.HideInfo();
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000A8B60 File Offset: 0x000A6D60
		public SoldierMiniature PickUpSoldier()
		{
			for (int i = this.soldiers.Count - 1; i >= 0; i--)
			{
				SoldierMiniature soldierMiniature = this.soldiers[i];
				if (!soldierMiniature.isPickedUp && !soldierMiniature.isTapped)
				{
					soldierMiniature.PickUp();
					return soldierMiniature;
				}
			}
			return null;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00013A7B File Offset: 0x00011C7B
		public bool HasAnyUntappedSoldiers()
		{
			return this.UntappedSoldierCount() > 0;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000A8BAC File Offset: 0x000A6DAC
		public int UntappedSoldierCount()
		{
			int num = 0;
			for (int i = this.soldiers.Count - 1; i >= 0; i--)
			{
				if (!this.soldiers[i].isTapped)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000A8BEC File Offset: 0x000A6DEC
		public SoldierMiniature SetDownSoldier()
		{
			for (int i = this.soldiers.Count - 1; i >= 0; i--)
			{
				SoldierMiniature soldierMiniature = this.soldiers[i];
				if (soldierMiniature.isPickedUp)
				{
					soldierMiniature.SetDown();
					return soldierMiniature;
				}
			}
			return null;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000A8C30 File Offset: 0x000A6E30
		public void SetOwner(int team)
		{
			this.owner = team;
			this.blueOwnerIndicator.SetActive(this.showOwnerIndicator && team == 0);
			this.redOwnerIndicator.SetActive(this.showOwnerIndicator && team == 1);
			this.blueOwnerIndicator.layer = 29;
			this.redOwnerIndicator.layer = 29;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000A8C94 File Offset: 0x000A6E94
		protected override void Awake()
		{
			base.Awake();
			this.highlightPanel = UnityEngine.Object.Instantiate<GameObject>(CampaignBase.instance.levelHighlightPanelPrefab, ConquestCampaign.instance.worldMapCanvas.transform).GetComponent<LevelHighlightPanel>();
			this.highlightPanel.Setup(this);
			this.highlightPanel.Hide();
			this.SetMoveAgainEnabled(false);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000A8CF0 File Offset: 0x000A6EF0
		public void SetMoveAgainEnabled(bool enabled)
		{
			bool active = this.isMoveAgainTile && !enabled;
			this._moveAgain = enabled;
			try
			{
				this.moveAgainIndicatorActive.SetActive(enabled);
				this.moveAgainIndicatorInactive.SetActive(active);
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000A8D48 File Offset: 0x000A6F48
		public bool IsProducing(ResourceType type)
		{
			LevelClickable.ResourceProduction[] array = this.production;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000A8D78 File Offset: 0x000A6F78
		public int GetProductionAmount(ResourceType type)
		{
			int num = 0;
			foreach (LevelClickable.ResourceProduction resourceProduction in this.production)
			{
				if (resourceProduction.type == type)
				{
					num += resourceProduction.amount;
				}
			}
			return num;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00013A86 File Offset: 0x00011C86
		public bool HasAnyResourceProduction()
		{
			return this.production.Length != 0;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00013A92 File Offset: 0x00011C92
		public bool HasRecruitment()
		{
			return this.recruitsPerTurn > 0;
		}

		// Token: 0x04001AFA RID: 6906
		public const float RADIUS = 0.08f;

		// Token: 0x04001AFB RID: 6907
		public Sprite displayImage;

		// Token: 0x04001AFC RID: 6908
		public string displayName = "";

		// Token: 0x04001AFD RID: 6909
		public string description = "";

		// Token: 0x04001AFE RID: 6910
		public string sceneName = "";

		// Token: 0x04001AFF RID: 6911
		public GameModeType defaultGameMode = GameModeType.Domination;

		// Token: 0x04001B00 RID: 6912
		public Transform[] soldierRootTransforms;

		// Token: 0x04001B01 RID: 6913
		public GameObject blueOwnerIndicator;

		// Token: 0x04001B02 RID: 6914
		public GameObject redOwnerIndicator;

		// Token: 0x04001B03 RID: 6915
		public bool showOwnerIndicator = true;

		// Token: 0x04001B04 RID: 6916
		public SpawnPoint.Team defaultOwner = SpawnPoint.Team.Neutral;

		// Token: 0x04001B05 RID: 6917
		public int startBattalionCount;

		// Token: 0x04001B06 RID: 6918
		public Transform highlightHoverRoot;

		// Token: 0x04001B07 RID: 6919
		public bool isMoveAgainTile;

		// Token: 0x04001B08 RID: 6920
		public bool isHq;

		// Token: 0x04001B09 RID: 6921
		public int recruitsPerTurn;

		// Token: 0x04001B0A RID: 6922
		public ResourceType recruitCostType;

		// Token: 0x04001B0B RID: 6923
		public int recruitCost;

		// Token: 0x04001B0C RID: 6924
		public LevelClickable.ResourceProduction[] production;

		// Token: 0x04001B0D RID: 6925
		public GameObject moveAgainIndicatorActive;

		// Token: 0x04001B0E RID: 6926
		public GameObject moveAgainIndicatorInactive;

		// Token: 0x04001B0F RID: 6927
		[NonSerialized]
		public int remainingRecruits;

		// Token: 0x04001B10 RID: 6928
		[NonSerialized]
		public LevelHighlightPanel highlightPanel;

		// Token: 0x04001B11 RID: 6929
		[NonSerialized]
		public List<SoldierMiniature> soldiers = new List<SoldierMiniature>(3);

		// Token: 0x04001B12 RID: 6930
		[NonSerialized]
		public int owner = -1;

		// Token: 0x04001B13 RID: 6931
		[NonSerialized]
		private Dictionary<LevelClickableConnection, LevelClickable> connectionLevel = new Dictionary<LevelClickableConnection, LevelClickable>();

		// Token: 0x04001B14 RID: 6932
		[NonSerialized]
		private bool _moveAgain;

		// Token: 0x020003F0 RID: 1008
		[Serializable]
		public class ResourceProduction
		{
			// Token: 0x04001B15 RID: 6933
			public ResourceType type;

			// Token: 0x04001B16 RID: 6934
			public int amount = 1;
		}
	}
}

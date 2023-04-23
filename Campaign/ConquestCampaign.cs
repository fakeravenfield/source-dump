using System;
using System.Collections.Generic;
using Campaign.AI;
using Campaign.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Campaign
{
	// Token: 0x020003E1 RID: 993
	public class ConquestCampaign : CampaignBase
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0001310A File Offset: 0x0001130A
		public new static ConquestCampaign instance
		{
			get
			{
				return CampaignBase.instance as ConquestCampaign;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000A635C File Offset: 0x000A455C
		protected override void Awake()
		{
			base.Awake();
			this.ghostArrow = UnityEngine.Object.Instantiate<GameObject>(this.arrowPrefab);
			MeshRenderer[] componentsInChildren = this.ghostArrow.GetComponentsInChildren<MeshRenderer>();
			this.ghostArrowMaterials = new Material[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.ghostArrowMaterials[i] = componentsInChildren[i].material;
			}
			this.ghostArrow.SetActive(false);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000A63C4 File Offset: 0x000A45C4
		protected override void Start()
		{
			base.Start();
			this.commitButtonLabel = new Dictionary<LevelClickable, Text>();
			this.commitButtonGameObject = new Dictionary<LevelClickable, GameObject>();
			foreach (LevelClickable levelClickable in this.levels)
			{
				Button button = levelClickable.highlightPanel.AddButton("MOVE", new UnityAction(this.CommitPlayerMove));
				Text componentInChildren = button.GetComponentInChildren<Text>();
				this.commitButtonLabel.Add(levelClickable, componentInChildren);
				this.commitButtonGameObject.Add(levelClickable, button.gameObject);
				string text = levelClickable.description;
				string productionText = this.GetProductionText(levelClickable);
				if (!string.IsNullOrEmpty(productionText))
				{
					text = text + "\n\n" + productionText;
				}
				levelClickable.highlightPanel.descriptionText.text = text;
			}
			base.UpdateLevelConnections();
			this.ai = new ConquestAi(this);
			if (CampaignBase.cachedState.turnNumber < 0)
			{
				if (CampaignBase.cachedState.isPlayerTurn)
				{
					this.NewPlayerTurn();
					return;
				}
				this.NewAiTurn();
				return;
			}
			else
			{
				if (CampaignBase.cachedState.isPlayerTurn)
				{
					this.SetPlayerTurn();
					return;
				}
				this.SetAiTurn();
				return;
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00013116 File Offset: 0x00011316
		protected override void OnStateWasLoaded(CampaignState state)
		{
			this.LoadState(state);
			this.UpdateAllResourcesAndGains();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00013125 File Offset: 0x00011325
		protected override void OnNewGame()
		{
			base.OnNewGame();
			this.CreateInitialState();
			this.UpdateAllResourcesAndGains();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000A6504 File Offset: 0x000A4704
		private void LoadState(CampaignState state)
		{
			Dictionary<string, LevelClickable> dictionary = new Dictionary<string, LevelClickable>();
			foreach (LevelClickable levelClickable in this.levels)
			{
				dictionary.Add(levelClickable.gameObject.name, levelClickable);
			}
			foreach (CampaignState.LevelState levelState in state.levels)
			{
				if (levelState.owner > -1 && levelState.owner <= 1)
				{
					dictionary[levelState.objectName].SetOwner(levelState.owner);
					for (int j = 0; j < levelState.battalions; j++)
					{
						this.InstantiateSoldierMiniature(dictionary[levelState.objectName], levelState.owner, false).SetTapped(j < levelState.tappedBattalions);
					}
				}
			}
			if (state.isInBattle)
			{
				this.attackDestination = dictionary[state.battleDestinationLevelClickable];
				this.attackingSoldiers = new List<SoldierMiniature>();
				foreach (string key in state.conquestBattleSourceLevelClickables)
				{
					foreach (SoldierMiniature soldierMiniature in dictionary[key].soldiers)
					{
						if (!soldierMiniature.isTapped)
						{
							this.attackingSoldiers.Add(soldierMiniature);
							soldierMiniature.SetTapped(true);
							break;
						}
					}
				}
				if (!BattleResult.HasResult())
				{
					BattleResult.SetWinner(this.aiTeam);
				}
				this.ResolveBattleResult(BattleResult.latest);
				BattleResult.PopResult();
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000A66CC File Offset: 0x000A48CC
		private void CreateInitialState()
		{
			base.SetDefaultResourceAmounts();
			foreach (LevelClickable levelClickable in this.levels)
			{
				if (levelClickable.defaultOwner != SpawnPoint.Team.Neutral)
				{
					levelClickable.SetOwner((int)levelClickable.defaultOwner);
					for (int i = 0; i < Mathf.Min(levelClickable.startBattalionCount, 3); i++)
					{
						this.InstantiateSoldierMiniature(levelClickable, (int)levelClickable.defaultOwner, false);
					}
				}
			}
			CampaignBase.cachedState.isPlayerTurn = true;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000A6764 File Offset: 0x000A4964
		private void UpdateAllResourcesAndGains()
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					CampaignBase.cachedState.resources[i].resourceGain[j] = 0;
				}
				foreach (LevelClickable levelClickable in this.levels)
				{
					if (levelClickable.owner == i)
					{
						foreach (LevelClickable.ResourceProduction resourceProduction in levelClickable.production)
						{
							CampaignBase.cachedState.resources[i].resourceGain[(int)resourceProduction.type] += resourceProduction.amount;
						}
					}
				}
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000A6838 File Offset: 0x000A4A38
		private void RecieveResources(int team)
		{
			this.UpdateAllResourcesAndGains();
			foreach (object obj in Enum.GetValues(typeof(ResourceType)))
			{
				ResourceType resourceType = (ResourceType)obj;
				CampaignBase.cachedState.resources[team].availableResources[(int)resourceType] += CampaignBase.cachedState.resources[team].resourceGain[(int)resourceType];
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00013139 File Offset: 0x00011339
		public bool TryRecruit(LevelClickable level)
		{
			if (level.CanRecruit() && ConquestCampaign.instance.TryTransaction(level.owner, level.recruitCostType, level.recruitCost))
			{
				level.Recruit();
				this.UpdateResourceLabels();
				return true;
			}
			return false;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000A68C8 File Offset: 0x000A4AC8
		public int GetBattalionCountOfTeam(int team)
		{
			int num = 0;
			foreach (LevelClickable levelClickable in this.levels)
			{
				if (levelClickable.owner == team)
				{
					num += levelClickable.soldiers.Count;
				}
			}
			return num;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00013170 File Offset: 0x00011370
		public override bool OnNothingRightClicked()
		{
			if (CampaignBase.cachedState.isPlayerTurn && this.HasSelectedAnyLevel())
			{
				this.CancelMove();
				return true;
			}
			return false;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000A6930 File Offset: 0x000A4B30
		public bool SetDownSoldierOnOtherLevel(LevelClickable level)
		{
			bool result = false;
			foreach (SoldierMiniature soldierMiniature in this.selectedSoldiers)
			{
				if (soldierMiniature.level != level && this.SetDownSoldierForLevel(soldierMiniature.level))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000A69A0 File Offset: 0x000A4BA0
		public bool SetDownSoldierForLevel(LevelClickable level)
		{
			bool result = false;
			if (level.HasAnyPickedUpSoldiers())
			{
				SoldierMiniature soldierMiniature = level.SetDownSoldier();
				if (soldierMiniature != null)
				{
					this.selectedSoldiers.Remove(soldierMiniature);
				}
				result = true;
			}
			if (!level.HasAnyPickedUpSoldiers())
			{
				this.DeselectLevel(level);
			}
			return result;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000A69E8 File Offset: 0x000A4BE8
		private void SetGhostArrowColor(Color c)
		{
			Material[] array = this.ghostArrowMaterials;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = c;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000A6A14 File Offset: 0x000A4C14
		public override void OnLevelClicked(LevelClickable level)
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			int num = 3;
			if (this.destinationLevel != null && this.destinationLevel.owner == this.playerTeam)
			{
				num = 3 - this.destinationLevel.soldiers.Count;
			}
			if (!this.LevelIsSelected(level) && level != this.destinationLevel)
			{
				if (level.owner == this.playerTeam && level.HasAnyUntappedSoldiers() && (this.ReadyToSelectFirstSource() || (this.ReadyToSetAdditionalSources() && this.CanSetAdditionalSource(level))))
				{
					if (this.selectedSoldiers.Count >= num && !this.SetDownSoldierOnOtherLevel(level))
					{
						return;
					}
					this.SelectLevel(level);
					if (this.ReadyToSetDestination())
					{
						this.ghostArrowHoveringOverSelected = true;
					}
				}
				else if (this.ReadyToSetDestination() && this.CanSetDestination(level))
				{
					this.SetDestinationLevel(level);
				}
			}
			if (this.LevelIsSelected(level))
			{
				if (this.selectedSoldiers.Count >= num && !this.SetDownSoldierOnOtherLevel(level))
				{
					return;
				}
				SoldierMiniature soldierMiniature = level.PickUpSoldier();
				if (soldierMiniature != null)
				{
					this.selectedSoldiers.Add(soldierMiniature);
				}
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0001318F File Offset: 0x0001138F
		public override void OnLevelRightClicked(LevelClickable level)
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			if (this.LevelIsSelected(level))
			{
				this.SetDownSoldierForLevel(level);
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000A6B34 File Offset: 0x000A4D34
		public override void OnLevelStartHover(LevelClickable level)
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			if (this.ReadyToSetDestination())
			{
				if (!this.LevelIsSelected(level))
				{
					CampaignBase.SetArrowTransform(this.ghostArrow.transform, this.selectedLevels[0], level);
					this.SetGhostArrowColor(this.CanSetDestination(level) ? Color.white : new Color(1f, 0.5f, 0.5f, 0.5f));
					this.ghostArrowHoveringOverLevel = true;
					return;
				}
				this.ghostArrowHoveringOverSelected = true;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000A6BBC File Offset: 0x000A4DBC
		public override void OnLevelEndHover(LevelClickable level)
		{
			if (level != this.destinationLevel)
			{
				level.ShowIdleInfo();
			}
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			this.SetGhostArrowColor(new Color(1f, 1f, 1f, 0.5f));
			this.ghostArrowHoveringOverLevel = false;
			this.ghostArrowHoveringOverSelected = false;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000131AF File Offset: 0x000113AF
		public override void OnLevelHoverStay(LevelClickable level)
		{
			if (level != this.destinationLevel)
			{
				this.commitButtonGameObject[level].gameObject.SetActive(false);
				level.ShowFocusInfo();
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000131DC File Offset: 0x000113DC
		public void SelectLevel(LevelClickable level)
		{
			if (this.LevelIsSelected(level))
			{
				return;
			}
			this.selectedLevels.Add(level);
			if (this.destinationLevel != null)
			{
				this.AddArrowForLevel(level);
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000A6C18 File Offset: 0x000A4E18
		public void DeselectLevel(LevelClickable level)
		{
			this.selectedLevels.Remove(level);
			if (this.selectedLevelArrow.ContainsKey(level))
			{
				UnityEngine.Object.Destroy(this.selectedLevelArrow[level]);
				this.selectedLevelArrow.Remove(level);
			}
			if (this.selectedLevels.Count == 0)
			{
				this.CancelMove();
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000A6C74 File Offset: 0x000A4E74
		public void CancelMove()
		{
			foreach (GameObject obj in this.selectedLevelArrow.Values)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.selectedLevels.Clear();
			this.selectedLevelArrow.Clear();
			if (this.destinationLevel != null)
			{
				this.destinationLevel.highlightPanel.Hide();
			}
			this.destinationLevel = null;
			foreach (SoldierMiniature soldierMiniature in this.selectedSoldiers)
			{
				soldierMiniature.SetDown();
			}
			this.selectedSoldiers.Clear();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00013209 File Offset: 0x00011409
		public void CommitPlayerMove()
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			this.MoveSoldiers(this.selectedSoldiers, this.destinationLevel);
			this.CancelMove();
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000A6D50 File Offset: 0x000A4F50
		public void CarryOutAiMove(ConquestMove move)
		{
			Debug.Log("AI Move: " + ((move != null) ? move.ToString() : null));
			List<SoldierMiniature> list = new List<SoldierMiniature>();
			foreach (ConquestMove.Source source in move.GetSources())
			{
				List<SoldierMiniature> untappedSoldiers = source.level.GetUntappedSoldiers(source.count);
				if (source.level == move.destination)
				{
					using (List<SoldierMiniature>.Enumerator enumerator2 = untappedSoldiers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							SoldierMiniature soldierMiniature = enumerator2.Current;
							soldierMiniature.SetTapped(true);
						}
						continue;
					}
				}
				list.AddRange(untappedSoldiers);
			}
			if (list.Count > 0)
			{
				Debug.Log("Moving soldiers: " + list.Count.ToString());
				this.MoveSoldiers(list, move.destination);
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000A6E5C File Offset: 0x000A505C
		public void MoveSoldiers(List<SoldierMiniature> soldiers, LevelClickable destination)
		{
			if (soldiers.Count == 0)
			{
				Debug.LogError("No soldiers were moved");
				return;
			}
			if (soldiers[0].team != destination.owner && destination.soldiers.Count > 0)
			{
				this.SetupBattle(soldiers, destination, CampaignBase.cachedState.isPlayerTurn, true);
				return;
			}
			bool moveAgain = destination.moveAgain;
			destination.SetMoveAgainEnabled(false);
			if (destination.owner != soldiers[0].team)
			{
				destination.SetOwner(soldiers[0].team);
				this.OnLevelWasCaptured(destination);
			}
			foreach (SoldierMiniature soldierMiniature in soldiers)
			{
				LevelClickable level = soldierMiniature.level;
				soldierMiniature.TransitionToLevel(destination, true);
				if (!moveAgain)
				{
					soldierMiniature.SetTapped(true);
				}
				level.ReorderSoldiers();
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000A6F48 File Offset: 0x000A5148
		private void SetupBattle(List<SoldierMiniature> attackers, LevelClickable destination, bool canCancel, bool canAutoResolve)
		{
			this.attackingSoldiers = new List<SoldierMiniature>(attackers);
			this.attackDestination = destination;
			this.attackingSoldierCount = this.attackingSoldiers.Count;
			this.defendingSoldierCount = this.attackDestination.soldiers.Count;
			StartBattleUi.ShowBattleUi(destination, attackers.Count, canCancel, canAutoResolve);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00013230 File Offset: 0x00011430
		public void StartBattle(LevelClickable level)
		{
			CampaignBase.cachedState.SaveConquestAttackSource(this.attackingSoldiers);
			base.StartLevelClickable(level);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000A6FA0 File Offset: 0x000A51A0
		public override void SetGameParameters(GameModeParameters parameters)
		{
			parameters.useConquestRules = true;
			parameters.actorCount = 60;
			parameters.configFlags = false;
			parameters.balance = 0.5f;
			parameters.bloodExplosions = false;
			parameters.gameLength = 1;
			parameters.playerHasAllWeapons = false;
			parameters.playerTeam = this.playerTeam;
			parameters.respawnTime = 10;
			parameters.reverseMode = (this.GetCurrentTeamIndex() == 1);
			ConquestTurnPhase currentTurnPhase = this.GetCurrentTurnPhase();
			if (currentTurnPhase != null)
			{
				parameters.nightMode = currentTurnPhase.nightTime;
			}
			else
			{
				parameters.nightMode = false;
			}
			if (this.GetCurrentTeamIndex() == 0)
			{
				parameters.conquestBattalions[0] = this.attackingSoldierCount;
				parameters.conquestBattalions[1] = this.defendingSoldierCount;
				return;
			}
			parameters.conquestBattalions[0] = this.defendingSoldierCount;
			parameters.conquestBattalions[1] = this.attackingSoldierCount;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000A7068 File Offset: 0x000A5268
		public BattleResult AutoBattle()
		{
			int team = this.attackingSoldiers[0].team;
			int num = 1 - team;
			int num2 = this.attackingSoldiers.Count;
			int num3 = this.attackDestination.soldiers.Count;
			int num4 = 16;
			int num5 = 20;
			if (team == this.aiTeam)
			{
				num4 += 3;
			}
			else
			{
				num5 += 3;
			}
			int num6 = num4;
			int num7 = num5;
			while (num2 > 0 && num3 > 0)
			{
				if (UnityEngine.Random.Range(0, 1001) < 500)
				{
					int num8 = num3;
					num6 -= num8;
					if (num6 <= 0)
					{
						num2--;
						num6 = num4;
					}
				}
				else
				{
					num7 -= num3;
					if (num7 <= 0)
					{
						num3--;
						num7 = num5;
					}
				}
			}
			BattleResult battleResult = new BattleResult();
			if (num2 > 0)
			{
				battleResult.winningTeam = team;
			}
			else
			{
				battleResult.winningTeam = num;
			}
			battleResult.gameModeSupportsBattalions = true;
			battleResult.remainingBattalions[team] = num2;
			battleResult.remainingBattalions[num] = num3;
			return battleResult;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000A7154 File Offset: 0x000A5354
		public void ResolveBattleResult(BattleResult result)
		{
			int currentTeamIndex = this.GetCurrentTeamIndex();
			int num = 1 - currentTeamIndex;
			bool flag = result.winningTeam == currentTeamIndex;
			if (!result.gameModeSupportsBattalions)
			{
				if (flag)
				{
					result.remainingBattalions[currentTeamIndex] = this.attackingSoldiers.Count;
					result.remainingBattalions[num] = 0;
				}
				else
				{
					result.remainingBattalions[num] = this.attackDestination.soldiers.Count;
					result.remainingBattalions[currentTeamIndex] = 0;
				}
			}
			int num2 = this.attackingSoldiers.Count - result.remainingBattalions[currentTeamIndex];
			int num3 = this.attackDestination.soldiers.Count - result.remainingBattalions[num];
			for (int i = 0; i < num2; i++)
			{
				this.attackingSoldiers[0].Die();
				this.attackingSoldiers.RemoveAt(0);
			}
			for (int j = 0; j < num3; j++)
			{
				this.attackDestination.soldiers[0].Die();
			}
			if (flag && !this.attackDestination.HasAnySoldiers())
			{
				this.attackDestination.SetOwner(currentTeamIndex);
				foreach (SoldierMiniature soldierMiniature in this.attackingSoldiers)
				{
					soldierMiniature.TransitionToLevel(this.attackDestination, true);
				}
				this.OnLevelWasCaptured(this.attackDestination);
			}
			else
			{
				this.attackDestination.ReorderSoldiers();
			}
			foreach (SoldierMiniature soldierMiniature2 in this.attackingSoldiers)
			{
				soldierMiniature2.SetTapped(true);
			}
			this.OnBattleResolved();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00013249 File Offset: 0x00011449
		public int GetCurrentTeamIndex()
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return this.aiTeam;
			}
			return this.playerTeam;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00013264 File Offset: 0x00011464
		public bool CanSetDestination(LevelClickable level)
		{
			return this.selectedLevels.Count > 0 && this.selectedLevels[0].HasConnectionTo(level);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00013288 File Offset: 0x00011488
		public bool CanSetAdditionalSource(LevelClickable level)
		{
			return this.destinationLevel != null && this.destinationLevel.HasConnectionTo(level);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000A730C File Offset: 0x000A550C
		public void SetDestinationLevel(LevelClickable level)
		{
			this.destinationLevel = level;
			this.commitButtonGameObject[level].gameObject.SetActive(true);
			if (level.owner == this.aiTeam)
			{
				this.commitButtonLabel[level].text = (level.HasAnySoldiers() ? "ATTACK" : "CAPTURE");
			}
			else if (level.owner == this.playerTeam)
			{
				this.commitButtonLabel[level].text = "MOVE";
			}
			else
			{
				this.commitButtonLabel[level].text = "CAPTURE";
			}
			level.highlightPanel.ShowFull();
			foreach (LevelClickable level2 in this.selectedLevels)
			{
				this.AddArrowForLevel(level2);
			}
			int num = 3;
			if (level.owner == GameManager.PlayerTeam())
			{
				num -= this.destinationLevel.soldiers.Count;
			}
			SoldierMiniature[] array = this.selectedSoldiers.ToArray();
			int num2 = Mathf.Max(0, array.Length - num);
			for (int i = 0; i < num2; i++)
			{
				this.SetDownSoldierForLevel(array[i].level);
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000A7458 File Offset: 0x000A5658
		public void AddArrowForLevel(LevelClickable level)
		{
			if (this.selectedLevelArrow.ContainsKey(level))
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.arrowPrefab);
			CampaignBase.SetArrowTransform(gameObject.transform, level, this.destinationLevel);
			this.selectedLevelArrow.Add(level, gameObject);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000132A6 File Offset: 0x000114A6
		public bool LevelIsSelected(LevelClickable level)
		{
			return this.selectedLevels.Contains(level);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000A74A0 File Offset: 0x000A56A0
		private void UntapAllSoldiers()
		{
			foreach (LevelClickable levelClickable in this.levels)
			{
				foreach (SoldierMiniature soldierMiniature in levelClickable.soldiers)
				{
					soldierMiniature.SetTapped(false);
				}
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000132B4 File Offset: 0x000114B4
		public void EndPlayerTurn()
		{
			if (!CampaignBase.cachedState.isPlayerTurn)
			{
				return;
			}
			this.CancelMove();
			CampaignBase.cachedState.isPlayerTurn = false;
			this.OnEndTurn(this.playerTeam);
			this.NewAiTurn();
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000132E6 File Offset: 0x000114E6
		private void NewAiTurn()
		{
			this.OnNewTurn(this.aiTeam);
			this.SetAiTurn();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000132FA File Offset: 0x000114FA
		private void SetAiTurn()
		{
			this.ai.NewTurn();
			this.UpdateTurnText();
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0001330D File Offset: 0x0001150D
		public void EndAiTurn()
		{
			this.OnEndTurn(this.aiTeam);
			this.NewPlayerTurn();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000A752C File Offset: 0x000A572C
		private void OnNewTurn(int team)
		{
			foreach (LevelClickable levelClickable in this.levels)
			{
				levelClickable.OnNewTurn(team);
			}
			this.RecieveResources(team);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000A7584 File Offset: 0x000A5784
		private void OnEndTurn(int team)
		{
			this.UntapAllSoldiers();
			foreach (LevelClickable levelClickable in this.levels)
			{
				if (levelClickable.isMoveAgainTile)
				{
					levelClickable.SetMoveAgainEnabled(false);
				}
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00013321 File Offset: 0x00011521
		private void SetPlayerTurn()
		{
			CampaignBase.cachedState.isPlayerTurn = true;
			this.UpdateTurnText();
			this.UpdateResourceLabels();
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0001333A File Offset: 0x0001153A
		private void NewPlayerTurn()
		{
			this.IncrementTurnNumber();
			this.OnNewTurn(this.playerTeam);
			this.SetPlayerTurn();
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00013354 File Offset: 0x00011554
		private void IncrementTurnNumber()
		{
			CampaignBase.cachedState.turnNumber++;
			this.UpdateTurnText();
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0001336E File Offset: 0x0001156E
		private bool UsingTurnPhases()
		{
			return this.turnPhases != null && this.turnPhases.Length != 0;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00013384 File Offset: 0x00011584
		private ConquestTurnPhase GetCurrentTurnPhase()
		{
			return this.turnPhases[CampaignBase.cachedState.turnNumber % this.turnPhases.Length];
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000A75E8 File Offset: 0x000A57E8
		private void UpdateTurnText()
		{
			if (this.UsingTurnPhases())
			{
				int num = CampaignBase.cachedState.turnNumber / this.turnPhases.Length + 1;
				ConquestTurnPhase currentTurnPhase = this.GetCurrentTurnPhase();
				this.phaseText.text = string.Concat(new string[]
				{
					this.phaseCycleName,
					" ",
					num.ToString(),
					" - ",
					currentTurnPhase.name
				});
				return;
			}
			this.phaseText.text = "TURN " + CampaignBase.cachedState.turnNumber.ToString();
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000133A0 File Offset: 0x000115A0
		public bool ReadyToSelectFirstSource()
		{
			return this.selectedLevels.Count == 0;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000133B0 File Offset: 0x000115B0
		public bool HasSelectedAnyLevel()
		{
			return this.selectedLevels.Count > 0;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000133C0 File Offset: 0x000115C0
		public bool ReadyToSetDestination()
		{
			return this.selectedLevels.Count == 1 && this.destinationLevel == null;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000133DE File Offset: 0x000115DE
		public bool ReadyToSetAdditionalSources()
		{
			return this.selectedLevels.Count >= 1 && this.destinationLevel != null;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000133FC File Offset: 0x000115FC
		public void OnLevelWasCaptured(LevelClickable level)
		{
			this.UpdateAllResourcesAndGains();
			base.UpdateLevelConnections();
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0001340A File Offset: 0x0001160A
		public void OnBattleResolved()
		{
			this.UpdateAllResourcesAndGains();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000A7684 File Offset: 0x000A5884
		private string GetProductionText(LevelClickable level)
		{
			string text = "";
			string text2 = "";
			if (level.HasRecruitment())
			{
				text = "Recruits per turn:\n";
				text += string.Format("{0}<sprite index=0> at {1}{2}", level.recruitsPerTurn, level.recruitCost, ConquestCampaign.GetResourceSpriteTag(level.recruitCostType));
			}
			if (level.HasAnyResourceProduction())
			{
				text2 = "Production per turn:";
				foreach (LevelClickable.ResourceProduction resourceProduction in level.production)
				{
					text2 += string.Format("\n{0} {1} {2}", resourceProduction.amount, ConquestCampaign.GetResourceName(resourceProduction.type, resourceProduction.amount), ConquestCampaign.GetResourceSpriteTag(resourceProduction.type));
				}
			}
			string text3 = text;
			if (!string.IsNullOrEmpty(text2))
			{
				if (!string.IsNullOrEmpty(text3))
				{
					text3 += "\n\n";
				}
				text3 += text2;
			}
			return text3;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00013412 File Offset: 0x00011612
		public static string GetResourceName(ResourceType type, int amount)
		{
			if (type == ResourceType.Gold)
			{
				return "Gold";
			}
			if (type != ResourceType.Research)
			{
				return "Unknown";
			}
			return "Research";
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000A7770 File Offset: 0x000A5970
		public static string GetResourceSpriteTag(ResourceType type)
		{
			return " <sprite index=" + ((int)(type + 1)).ToString() + ">";
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000A7798 File Offset: 0x000A5998
		private void Update()
		{
			this.ghostArrow.SetActive(this.ReadyToSetDestination() && !this.ghostArrowHoveringOverSelected);
			if (this.endTurnButton != null)
			{
				bool active = CampaignBase.cachedState.isPlayerTurn && !this.HasSelectedAnyLevel() && !StartBattleUi.IsOpen();
				this.endTurnButton.gameObject.SetActive(active);
			}
			if (this.ghostArrow.activeInHierarchy && this.selectedLevels.Count > 0 && !this.ghostArrowHoveringOverLevel)
			{
				UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, this.selectedLevels[0].transform.position);
				Ray ray = CommandRoomMainCamera.instance.camera.ScreenPointToRay(Input.mousePosition);
				float d;
				if (plane.Raycast(ray, out d))
				{
					CampaignBase.SetArrowTransform(this.ghostArrow.transform, this.selectedLevels[0], ray.origin + ray.direction * d);
				}
			}
			if (CampaignBase.cachedState.isPlayerTurn)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.CommitPlayerMove();
				}
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					if (this.destinationLevel != null)
					{
						this.CancelMove();
						return;
					}
					base.AutoSave(false);
					GameManager.ReturnToMenu();
				}
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000A78EC File Offset: 0x000A5AEC
		public SoldierMiniature InstantiateSoldierMiniature(LevelClickable level, int team, bool playAnimation = true)
		{
			if (level.HasEmptySoldierSlot())
			{
				SoldierMiniature component = UnityEngine.Object.Instantiate<GameObject>(this.soldierMiniaturePrefab).GetComponent<SoldierMiniature>();
				component.SetTeam(team);
				component.SetLevel(level);
				if (playAnimation)
				{
					component.PlayDropAnimation(UnityEngine.Random.Range(0.6f, 0.8f));
				}
				return component;
			}
			return null;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0001342E File Offset: 0x0001162E
		public bool PauseAiExecution()
		{
			return StartBattleUi.IsOpen();
		}

		// Token: 0x04001A8F RID: 6799
		public const int MAX_SOLDIERS_PER_MOVE = 3;

		// Token: 0x04001A90 RID: 6800
		public const int MAX_SOLDIERS_PER_LEVEL = 3;

		// Token: 0x04001A91 RID: 6801
		public const int MAX_RESOURCE_INDEX = 16;

		// Token: 0x04001A92 RID: 6802
		private const int AUTO_BATTLE_ATTACKER_BASE_HEALTH = 16;

		// Token: 0x04001A93 RID: 6803
		private const int AUTO_BATTLE_DEFENDER_BASE_HEALTH = 20;

		// Token: 0x04001A94 RID: 6804
		private const int AUTO_BATTLE_AI_BONUS_HEALTH = 3;

		// Token: 0x04001A95 RID: 6805
		public GameObject soldierMiniaturePrefab;

		// Token: 0x04001A96 RID: 6806
		private GameObject ghostArrow;

		// Token: 0x04001A97 RID: 6807
		private Material[] ghostArrowMaterials;

		// Token: 0x04001A98 RID: 6808
		public GameObject arrowPrefab;

		// Token: 0x04001A99 RID: 6809
		public Button endTurnButton;

		// Token: 0x04001A9A RID: 6810
		public int[] endTurnButtonClickableGroups;

		// Token: 0x04001A9B RID: 6811
		public Canvas worldMapCanvas;

		// Token: 0x04001A9C RID: 6812
		public TextMeshProUGUI phaseText;

		// Token: 0x04001A9D RID: 6813
		public string phaseCycleName = "DAY";

		// Token: 0x04001A9E RID: 6814
		public ConquestTurnPhase[] turnPhases;

		// Token: 0x04001A9F RID: 6815
		public int maxBattalions = 15;

		// Token: 0x04001AA0 RID: 6816
		private bool ghostArrowHoveringOverSelected;

		// Token: 0x04001AA1 RID: 6817
		private bool ghostArrowHoveringOverLevel;

		// Token: 0x04001AA2 RID: 6818
		private Dictionary<LevelClickable, GameObject> selectedLevelArrow = new Dictionary<LevelClickable, GameObject>();

		// Token: 0x04001AA3 RID: 6819
		private Dictionary<LevelClickable, Text> commitButtonLabel;

		// Token: 0x04001AA4 RID: 6820
		private Dictionary<LevelClickable, GameObject> commitButtonGameObject;

		// Token: 0x04001AA5 RID: 6821
		private List<LevelClickable> selectedLevels = new List<LevelClickable>();

		// Token: 0x04001AA6 RID: 6822
		private List<SoldierMiniature> selectedSoldiers = new List<SoldierMiniature>();

		// Token: 0x04001AA7 RID: 6823
		private LevelClickable destinationLevel;

		// Token: 0x04001AA8 RID: 6824
		[NonSerialized]
		public List<SoldierMiniature> attackingSoldiers;

		// Token: 0x04001AA9 RID: 6825
		[NonSerialized]
		public LevelClickable attackDestination;

		// Token: 0x04001AAA RID: 6826
		private int attackingSoldierCount;

		// Token: 0x04001AAB RID: 6827
		private int defendingSoldierCount;

		// Token: 0x04001AAC RID: 6828
		private ConquestAi ai;
	}
}

using System;
using System.Collections.Generic;
using Campaign.Serialization;
using Campaign.Tech;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003E0 RID: 992
	public class CampaignBase : MonoBehaviour
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x000A5C4C File Offset: 0x000A3E4C
		protected virtual void Awake()
		{
			CampaignBase.instance = this;
			this.playerTeam = Mathf.Clamp(this.playerTeam, 0, 1);
			this.aiTeam = 1 - this.playerTeam;
			GameManager.instance.isPlayingCampaign = true;
			this.levels = new List<LevelClickable>(UnityEngine.Object.FindObjectsOfType<LevelClickable>());
			this.SetupLevelConnections();
			foreach (LevelClickableConnection connection in this.allLevelConnections)
			{
				this.CreateConnectionIndicator(connection);
			}
			this.FindUiElements();
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00012FA3 File Offset: 0x000111A3
		protected virtual void Start()
		{
			if (CampaignBase.cachedState != null)
			{
				this.OnStateWasLoaded(CampaignBase.cachedState);
				return;
			}
			CampaignBase.cachedState = new CampaignState();
			this.OnNewGame();
			this.CacheState(false);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnStateWasLoaded(CampaignState state)
		{
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000A5CF0 File Offset: 0x000A3EF0
		protected virtual void OnNewGame()
		{
			this.SetDefaultResourceAmounts();
			foreach (string techId in this.aiTeamAvailableTech)
			{
				TechManager.UnlockTechId(this.aiTeam, techId);
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00012FCF File Offset: 0x000111CF
		public void CacheState(bool startBattle)
		{
			CampaignBase.cachedState.CacheState(this, startBattle);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00012FDD File Offset: 0x000111DD
		public void AutoSave(bool startBattle)
		{
			this.CacheState(startBattle);
			CampaignBase.cachedState.SerializeAutoSave();
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000A5D28 File Offset: 0x000A3F28
		protected void SetDefaultResourceAmounts()
		{
			CampaignBase.cachedState.resources[0].Clear();
			CampaignBase.cachedState.resources[1].Clear();
			foreach (ResourceAmount resourceAmount in this.playerTeamStartingResources)
			{
				CampaignBase.cachedState.resources[this.playerTeam].availableResources[(int)resourceAmount.type] = resourceAmount.amount;
			}
			foreach (ResourceAmount resourceAmount2 in this.aiTeamStartingResources)
			{
				CampaignBase.cachedState.resources[this.aiTeam].availableResources[(int)resourceAmount2.type] = resourceAmount2.amount;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00012FF0 File Offset: 0x000111F0
		private void FindUiElements()
		{
			this.resourceLabels = UnityEngine.Object.FindObjectsOfType<ResourceLabel>();
			this.battalionLabels = UnityEngine.Object.FindObjectsOfType<BattalionLabel>();
			this.techTreeUis = UnityEngine.Object.FindObjectsOfType<TechTreeUi>();
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00013013 File Offset: 0x00011213
		public virtual bool TryUnlockTechTreeEntry(int team, TechTreeEntry entry)
		{
			if (TechManager.IsEntryUnlocked(team, entry))
			{
				return true;
			}
			if (!this.TryMultiTransaction(team, entry.GetResourceTransactionAmount()))
			{
				return false;
			}
			TechManager.UnlockEntry(team, entry);
			if (team == this.playerTeam)
			{
				this.UpdateTechTreeUis();
			}
			return true;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000A5DD8 File Offset: 0x000A3FD8
		public bool CanAffordMultiTransaction(int team, int[] resourceTransactionAmount)
		{
			int num = Mathf.Min(resourceTransactionAmount.Length, 16);
			for (int i = 0; i < num; i++)
			{
				if (!this.CanAffordTransaction(team, (ResourceType)i, resourceTransactionAmount[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00013048 File Offset: 0x00011248
		public bool CanAffordTransaction(int team, ResourceType type, int amount)
		{
			return amount <= 0 || CampaignBase.cachedState.resources[team].availableResources[(int)type] >= amount;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000A5E0C File Offset: 0x000A400C
		public bool TryMultiTransaction(int team, int[] resourceTransactionAmount)
		{
			if (!this.CanAffordMultiTransaction(team, resourceTransactionAmount))
			{
				return false;
			}
			int num = Mathf.Min(resourceTransactionAmount.Length, 16);
			for (int i = 0; i < num; i++)
			{
				CampaignBase.cachedState.resources[team].availableResources[i] -= resourceTransactionAmount[i];
			}
			if (team == this.playerTeam)
			{
				this.UpdateResourceLabels();
			}
			return true;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00013069 File Offset: 0x00011269
		public bool TryTransaction(int team, ResourceType type, int amount)
		{
			if (!this.CanAffordTransaction(team, type, amount))
			{
				return false;
			}
			CampaignBase.cachedState.resources[team].availableResources[(int)type] -= amount;
			if (team == this.playerTeam)
			{
				this.UpdateResourceLabels();
			}
			return true;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000A5E6C File Offset: 0x000A406C
		public virtual void UpdateResourceLabels()
		{
			ResourceLabel[] array = this.resourceLabels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UpdateLabel();
			}
			BattalionLabel[] array2 = this.battalionLabels;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].UpdateLabel();
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000A5EB4 File Offset: 0x000A40B4
		public void UpdateTechTreeUis()
		{
			TechTreeUi[] array = this.techTreeUis;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UpdateState();
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000A5EE0 File Offset: 0x000A40E0
		private void SetupLevelConnections()
		{
			this.allLevelConnections = new List<LevelClickableConnection>();
			foreach (LevelClickableConnection connection in this.levelConnections)
			{
				this.ApplyConnection(connection);
			}
			foreach (LevelClickable levelClickable in this.levels)
			{
				bool isMoveAgainTile = levelClickable.isMoveAgainTile;
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000A5F5C File Offset: 0x000A415C
		private void ApplyConnection(LevelClickableConnection connection)
		{
			if (connection.a != null && connection.b != null)
			{
				this.allLevelConnections.Add(connection);
				connection.a.AddConnection(connection.b, connection);
				connection.b.AddConnection(connection.a, connection);
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000A5FB8 File Offset: 0x000A41B8
		private void CreateConnectionIndicator(LevelClickableConnection connection)
		{
			if (connection.a != null && connection.b != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.levelConnectionPrefab);
				CampaignBase.SetArrowTransform(gameObject.transform, connection.a, connection.b);
				connection.SetIndicator(gameObject);
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000A600C File Offset: 0x000A420C
		public void UpdateLevelConnections()
		{
			foreach (LevelClickableConnection levelClickableConnection in this.allLevelConnections)
			{
				levelClickableConnection.UpdateIndicator();
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnLevelClicked(LevelClickable level)
		{
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnLevelRightClicked(LevelClickable level)
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnLevelStartHover(LevelClickable level)
		{
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnLevelEndHover(LevelClickable level)
		{
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnLevelHoverStay(LevelClickable level)
		{
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnNothingClicked()
		{
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool OnNothingRightClicked()
		{
			return false;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000A605C File Offset: 0x000A425C
		public void StartLevelClickable(LevelClickable level)
		{
			InstantActionMaps.MapEntry mapEntry = new InstantActionMaps.MapEntry();
			mapEntry.name = level.displayName;
			mapEntry.sceneName = level.sceneName;
			GameModeParameters gameModeParameters = this.DefaultParameters();
			gameModeParameters.gameModePrefab = GameManager.GetGameModePrefab(level.defaultGameMode);
			this.SetGameParameters(gameModeParameters);
			CampaignBase.cachedState.SaveStartLevelClickable(level);
			this.AutoSave(true);
			GameManager.instance.gameInfo = new GameInfoContainer(this.GetTeamInfo(0), this.GetTeamInfo(1));
			GameManager.StartLevel(mapEntry, gameModeParameters);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000130A4 File Offset: 0x000112A4
		private GameModeParameters DefaultParameters()
		{
			return new GameModeParameters
			{
				gameModePrefab = GameManager.instance.defaultGameModePrefab,
				playerTeam = this.playerTeam,
				playerHasAllWeapons = false
			};
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void SetGameParameters(GameModeParameters parameters)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000130CE File Offset: 0x000112CE
		public virtual TeamInfo GetTeamInfo(int team)
		{
			return TechManager.GenerateTeamInfo(team, (team == this.playerTeam) ? this.playerTeamPrefabs : this.aiTeamPrefabs);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000130ED File Offset: 0x000112ED
		public static Vector3 GetLevelRadiusOffsetPoint(LevelClickable level, Vector3 direction)
		{
			return level.transform.position + direction * 0.08f;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000A60DC File Offset: 0x000A42DC
		public static void SetArrowTransform(Transform arrow, LevelClickable from, LevelClickable to)
		{
			Vector3 normalized = (to.transform.position - from.transform.position).normalized;
			Vector3 levelRadiusOffsetPoint = CampaignBase.GetLevelRadiusOffsetPoint(from, normalized);
			Vector3 levelRadiusOffsetPoint2 = CampaignBase.GetLevelRadiusOffsetPoint(to, -normalized);
			CampaignBase.SetArrowTransformPosition(arrow, levelRadiusOffsetPoint, levelRadiusOffsetPoint2);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000A612C File Offset: 0x000A432C
		public static void SetArrowTransform(Transform arrow, LevelClickable from, Vector3 end)
		{
			Vector3 normalized = (end - from.transform.position).normalized;
			Vector3 levelRadiusOffsetPoint = CampaignBase.GetLevelRadiusOffsetPoint(from, normalized);
			CampaignBase.SetArrowTransformPosition(arrow, levelRadiusOffsetPoint, end);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000A6164 File Offset: 0x000A4364
		public static void SetArrowTransformPosition(Transform arrow, Vector3 start, Vector3 end)
		{
			Vector3 normalized = (end - start).normalized;
			Vector3 forward = end - start;
			start.y += 0.02f;
			arrow.transform.position = start;
			arrow.transform.rotation = Quaternion.LookRotation(forward);
			float magnitude = forward.magnitude;
			arrow.transform.localScale = new Vector3(1f, magnitude, magnitude);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000A61D8 File Offset: 0x000A43D8
		public bool TryBuyNextAiTech()
		{
			if (CampaignBase.instance.aiTeamTechTrees.Count == 0)
			{
				return false;
			}
			TechTreeEntry techTreeEntry = CampaignBase.cachedState.ai.GetTargetTech();
			if (techTreeEntry == null || TechManager.IsEntryUnlocked(CampaignBase.instance.aiTeam, techTreeEntry))
			{
				techTreeEntry = CampaignBase.instance.GetNewAiTargetTech();
				CampaignBase.cachedState.ai.SetTargetTech(techTreeEntry);
			}
			if (techTreeEntry == null)
			{
				return false;
			}
			bool flag = CampaignBase.instance.TryUnlockTechTreeEntry(CampaignBase.instance.aiTeam, techTreeEntry);
			if (flag)
			{
				Debug.Log("AI bought tech " + techTreeEntry.name);
				return flag;
			}
			Debug.Log("AI could not buy tech " + techTreeEntry.name);
			return flag;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000A6280 File Offset: 0x000A4480
		private TechTreeEntry GetNewAiTargetTech()
		{
			int num = UnityEngine.Random.Range(0, this.aiTeamTechTrees.Count);
			TechTree techTree = null;
			for (int i = 0; i < this.aiTeamTechTrees.Count; i++)
			{
				int index = (num + i) % this.aiTeamTechTrees.Count;
				techTree = this.aiTeamTechTrees[index];
				if (!TechManager.IsTechTreeCompleted(this.aiTeam, techTree))
				{
					break;
				}
			}
			return techTree.GetRandomAvailableEntry(this.aiTeam);
		}

		// Token: 0x04001A7D RID: 6781
		public static CampaignState cachedState;

		// Token: 0x04001A7E RID: 6782
		public static CampaignBase instance;

		// Token: 0x04001A7F RID: 6783
		public int playerTeam;

		// Token: 0x04001A80 RID: 6784
		[NonSerialized]
		public int aiTeam = 1;

		// Token: 0x04001A81 RID: 6785
		[NonSerialized]
		public List<LevelClickable> levels;

		// Token: 0x04001A82 RID: 6786
		public LevelClickableConnection[] levelConnections;

		// Token: 0x04001A83 RID: 6787
		public GameObject levelHighlightPanelPrefab;

		// Token: 0x04001A84 RID: 6788
		public GameObject levelConnectionPrefab;

		// Token: 0x04001A85 RID: 6789
		public CampaignTeamPrefabs playerTeamPrefabs;

		// Token: 0x04001A86 RID: 6790
		public CampaignTeamPrefabs aiTeamPrefabs;

		// Token: 0x04001A87 RID: 6791
		public List<TechTree> aiTeamTechTrees;

		// Token: 0x04001A88 RID: 6792
		public string[] aiTeamAvailableTech;

		// Token: 0x04001A89 RID: 6793
		private List<LevelClickableConnection> allLevelConnections;

		// Token: 0x04001A8A RID: 6794
		protected ResourceLabel[] resourceLabels;

		// Token: 0x04001A8B RID: 6795
		protected BattalionLabel[] battalionLabels;

		// Token: 0x04001A8C RID: 6796
		protected TechTreeUi[] techTreeUis;

		// Token: 0x04001A8D RID: 6797
		public ResourceAmount[] playerTeamStartingResources = new ResourceAmount[]
		{
			new ResourceAmount(ResourceType.Gold, 100),
			new ResourceAmount(ResourceType.Research, 5)
		};

		// Token: 0x04001A8E RID: 6798
		public ResourceAmount[] aiTeamStartingResources = new ResourceAmount[]
		{
			new ResourceAmount(ResourceType.Gold, 100),
			new ResourceAmount(ResourceType.Research, 5)
		};
	}
}

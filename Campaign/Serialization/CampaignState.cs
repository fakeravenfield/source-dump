using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Campaign.Tech;
using UnityEngine;

namespace Campaign.Serialization
{
	// Token: 0x0200041C RID: 1052
	[Serializable]
	public class CampaignState
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x000AC7D8 File Offset: 0x000AA9D8
		public void CacheState(CampaignBase campaign, bool startBattle)
		{
			this.levels = new CampaignState.LevelState[campaign.levels.Count];
			for (int i = 0; i < this.levels.Length; i++)
			{
				this.levels[i] = new CampaignState.LevelState(campaign.levels[i]);
			}
			this.isInBattle = startBattle;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000AC834 File Offset: 0x000AAA34
		public void Serialize(string path)
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(CampaignState));
				using (FileStream fileStream = new FileStream(path, FileMode.Create))
				{
					xmlSerializer.Serialize(fileStream, this);
					fileStream.Flush();
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00014286 File Offset: 0x00012486
		public void SerializeAutoSave()
		{
			this.Serialize(CampaignState.GetAutoSavePath());
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000AC898 File Offset: 0x000AAA98
		public static CampaignState Deserialize(string path)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CampaignState));
			CampaignState result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				result = (CampaignState)xmlSerializer.Deserialize(fileStream);
			}
			return result;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00014293 File Offset: 0x00012493
		public static CampaignState DeserializeAutoSave()
		{
			return CampaignState.Deserialize(CampaignState.GetAutoSavePath());
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0001429F File Offset: 0x0001249F
		public static bool AutoSaveFileExists()
		{
			return File.Exists(CampaignState.GetAutoSavePath());
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000142AB File Offset: 0x000124AB
		public static string GetSavePath()
		{
			string text = Application.persistentDataPath + "/Saves/";
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000142C3 File Offset: 0x000124C3
		public static string GetAutoSavePath()
		{
			return CampaignState.GetSavePath() + "autosave.xml";
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000142D4 File Offset: 0x000124D4
		public void SaveStartLevelClickable(LevelClickable level)
		{
			this.battleDestinationLevelClickable = level.gameObject.name;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000AC8E8 File Offset: 0x000AAAE8
		public void SaveConquestAttackSource(List<SoldierMiniature> attackingSoldiers)
		{
			this.conquestBattleSourceLevelClickables = new string[attackingSoldiers.Count];
			for (int i = 0; i < this.conquestBattleSourceLevelClickables.Length; i++)
			{
				this.conquestBattleSourceLevelClickables[i] = attackingSoldiers[i].level.gameObject.name;
			}
		}

		// Token: 0x04001BE4 RID: 7140
		private const string AUTO_SAVE_FILE_NAME = "autosave.xml";

		// Token: 0x04001BE5 RID: 7141
		public bool isPlayerTurn;

		// Token: 0x04001BE6 RID: 7142
		public int turnNumber = -1;

		// Token: 0x04001BE7 RID: 7143
		public CampaignState.LevelState[] levels;

		// Token: 0x04001BE8 RID: 7144
		public bool isInBattle;

		// Token: 0x04001BE9 RID: 7145
		public string battleDestinationLevelClickable = "";

		// Token: 0x04001BEA RID: 7146
		public string[] conquestBattleSourceLevelClickables = new string[0];

		// Token: 0x04001BEB RID: 7147
		public ConquestTeamResources[] resources = new ConquestTeamResources[]
		{
			new ConquestTeamResources(),
			new ConquestTeamResources()
		};

		// Token: 0x04001BEC RID: 7148
		public TechStatus[] teamTechStatus = new TechStatus[]
		{
			new TechStatus(),
			new TechStatus()
		};

		// Token: 0x04001BED RID: 7149
		public AiState ai = new AiState();

		// Token: 0x0200041D RID: 1053
		public struct LevelState
		{
			// Token: 0x06001A3E RID: 6718 RVA: 0x000AC9AC File Offset: 0x000AABAC
			public LevelState(LevelClickable level)
			{
				this.objectName = level.gameObject.name;
				this.owner = level.owner;
				this.battalions = level.soldiers.Count;
				this.tappedBattalions = 0;
				using (List<SoldierMiniature>.Enumerator enumerator = level.soldiers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.isTapped)
						{
							this.tappedBattalions++;
						}
					}
				}
			}

			// Token: 0x04001BEE RID: 7150
			public string objectName;

			// Token: 0x04001BEF RID: 7151
			public int owner;

			// Token: 0x04001BF0 RID: 7152
			public int battalions;

			// Token: 0x04001BF1 RID: 7153
			public int tappedBattalions;
		}
	}
}

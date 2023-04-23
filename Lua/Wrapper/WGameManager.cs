using System;

namespace Lua.Wrapper
{
	// Token: 0x0200096B RID: 2411
	[Name("GameManager")]
	[Doc("Use these methods to access game configuration.")]
	public static class WGameManager
	{
		// Token: 0x06003D7A RID: 15738 RVA: 0x000299A7 File Offset: 0x00027BA7
		[Getter]
		[Doc("Gets the build number.[..] Running this on EA Build 20 returns 20.")]
		public static int GetBuildNumber()
		{
			return GameManager.instance.buildNumber;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000299B3 File Offset: 0x00027BB3
		[Getter]
		[Doc("Returns true if the game build is tagged as beta.[..]")]
		public static bool GetIsBetaBuild()
		{
			return GameManager.instance.isBeta;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000299BF File Offset: 0x00027BBF
		[Getter]
		[Doc("Returns true if the game is considered running on a legitimate copy of the game.[..] Please note that this value can never be considered 100% accurate and may yield false positives and false negatives.")]
		public static bool GetIsLegitimate()
		{
			return GameManager.IsLegitimate();
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000299C6 File Offset: 0x00027BC6
		[Getter]
		[Doc("Returns true while the game is running in mod testing mode.[..] This is useful for reducing startup durations, etc when developing a mod.")]
		public static bool GetIsTestingContentMod()
		{
			return GameManager.IsTestingContentMod();
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000299CD File Offset: 0x00027BCD
		[Doc("Returns the team name.[..]")]
		public static string GetTeamName(WTeam team)
		{
			return GameManager.instance.GetTeamName((int)team);
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000299DA File Offset: 0x00027BDA
		[Doc("Returns the team name with a rich text color tag.[..] Example: ``<color=blue>EAGLE</color>``")]
		public static string GetRichTextColorTeamName(WTeam team)
		{
			return GameManager.instance.GetRichTextColorTeamName((int)team);
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000299E7 File Offset: 0x00027BE7
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000299F3 File Offset: 0x00027BF3
		[Undocumented]
		[Doc("Controls visibility of the game mode hud.")]
		public static bool hudGameModeEnabled
		{
			get
			{
				return GameManager.instance.hudEnabled;
			}
			set
			{
				GameManager.instance.hudEnabled = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x00029A00 File Offset: 0x00027C00
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x00029A0F File Offset: 0x00027C0F
		[Undocumented]
		[Doc("Controls visibility of the player hud.")]
		public static bool hudPlayerEnabled
		{
			get
			{
				return !IngameUi.instance.forceHide;
			}
			set
			{
				IngameUi.SetVisibility(value);
			}
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x00029A17 File Offset: 0x00027C17
		[Getter]
		public static bool GetIsPaused()
		{
			return GameManager.IsPaused();
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x00029A1E File Offset: 0x00027C1E
		[Getter]
		public static string GetCurrentGameModeName()
		{
			if (!GameManager.IsIngame() || GameModeBase.instance == null)
			{
				return "";
			}
			return GameModeBase.instance.GetName();
		}
	}
}

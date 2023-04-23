using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1B RID: 2587
	[Proxy(typeof(WGameManager))]
	public class WGameManagerProxy : IProxy
	{
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06005256 RID: 21078 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		// (set) Token: 0x06005257 RID: 21079 RVA: 0x0003CBD1 File Offset: 0x0003ADD1
		public static bool hudGameModeEnabled
		{
			get
			{
				return WGameManager.hudGameModeEnabled;
			}
			set
			{
				WGameManager.hudGameModeEnabled = value;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06005258 RID: 21080 RVA: 0x0003CBD9 File Offset: 0x0003ADD9
		// (set) Token: 0x06005259 RID: 21081 RVA: 0x0003CBE0 File Offset: 0x0003ADE0
		public static bool hudPlayerEnabled
		{
			get
			{
				return WGameManager.hudPlayerEnabled;
			}
			set
			{
				WGameManager.hudPlayerEnabled = value;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600525A RID: 21082 RVA: 0x0003CBE8 File Offset: 0x0003ADE8
		public static int buildNumber
		{
			get
			{
				return WGameManager.GetBuildNumber();
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600525B RID: 21083 RVA: 0x0003CBEF File Offset: 0x0003ADEF
		public static string currentGameModeName
		{
			get
			{
				return WGameManager.GetCurrentGameModeName();
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600525C RID: 21084 RVA: 0x0003CBF6 File Offset: 0x0003ADF6
		public static bool isBetaBuild
		{
			get
			{
				return WGameManager.GetIsBetaBuild();
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600525D RID: 21085 RVA: 0x0003CBFD File Offset: 0x0003ADFD
		public static bool isLegitimate
		{
			get
			{
				return WGameManager.GetIsLegitimate();
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600525E RID: 21086 RVA: 0x0003CC04 File Offset: 0x0003AE04
		public static bool isPaused
		{
			get
			{
				return WGameManager.GetIsPaused();
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0003CC0B File Offset: 0x0003AE0B
		public static bool isTestingContentMod
		{
			get
			{
				return WGameManager.GetIsTestingContentMod();
			}
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0003CC12 File Offset: 0x0003AE12
		public static string GetRichTextColorTeamName(WTeam team)
		{
			return WGameManager.GetRichTextColorTeamName(team);
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0003CC1A File Offset: 0x0003AE1A
		public static string GetTeamName(WTeam team)
		{
			return WGameManager.GetTeamName(team);
		}
	}
}

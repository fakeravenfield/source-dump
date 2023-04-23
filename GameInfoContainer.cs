using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class GameInfoContainer
{
	// Token: 0x060005E3 RID: 1507 RVA: 0x00005A67 File Offset: 0x00003C67
	public static GameInfoContainer Default()
	{
		return new GameInfoContainer(TeamInfo.Default(), TeamInfo.Default());
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00005A78 File Offset: 0x00003C78
	public GameInfoContainer()
	{
		this.team = new TeamInfo[]
		{
			new TeamInfo(),
			new TeamInfo()
		};
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00005A9C File Offset: 0x00003C9C
	public GameInfoContainer(TeamInfo team0, TeamInfo team1)
	{
		this.team = new TeamInfo[]
		{
			team0,
			team1
		};
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0005DB20 File Offset: 0x0005BD20
	public void LoadOfficial()
	{
		Debug.Log("Load official team info");
		TeamInfo[] array = this.team;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].LoadOfficial();
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0005DB54 File Offset: 0x0005BD54
	public void AdditiveLoadSingleMod(ModInformation mod)
	{
		Debug.Log("Load mod team info: " + mod.title);
		for (int i = 0; i < this.team.Length; i++)
		{
			this.team[i].AdditiveLoadSingleMod(mod, i);
		}
	}

	// Token: 0x040005D0 RID: 1488
	public const int TEAM_EAGLE = 0;

	// Token: 0x040005D1 RID: 1489
	public const int TEAM_RAVEN = 1;

	// Token: 0x040005D2 RID: 1490
	public TeamInfo[] team;
}

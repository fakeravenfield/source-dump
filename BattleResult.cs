using System;

// Token: 0x020000B5 RID: 181
public class BattleResult
{
	// Token: 0x060005DE RID: 1502 RVA: 0x000059E1 File Offset: 0x00003BE1
	public static void SetWinner(int winningTeam)
	{
		BattleResult.latest = new BattleResult();
		BattleResult.latest.winningTeam = winningTeam;
		BattleResult.latest.gameModeSupportsBattalions = false;
		BattleResult.latest.remainingBattalions = new int[2];
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00005A13 File Offset: 0x00003C13
	public static void AppendBattalionResult(int team0Battalions, int team1Battalions)
	{
		BattleResult.latest.gameModeSupportsBattalions = true;
		BattleResult.latest.remainingBattalions[0] = team0Battalions;
		BattleResult.latest.remainingBattalions[1] = team1Battalions;
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00005A3A File Offset: 0x00003C3A
	public static bool HasResult()
	{
		return BattleResult.latest != null;
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00005A44 File Offset: 0x00003C44
	public static void PopResult()
	{
		BattleResult.latest = null;
	}

	// Token: 0x040005CB RID: 1483
	public static BattleResult latest;

	// Token: 0x040005CC RID: 1484
	public const int DRAW = -1;

	// Token: 0x040005CD RID: 1485
	public int winningTeam = -1;

	// Token: 0x040005CE RID: 1486
	public bool gameModeSupportsBattalions;

	// Token: 0x040005CF RID: 1487
	public int[] remainingBattalions = new int[2];
}

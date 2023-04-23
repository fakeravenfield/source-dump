using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class GameModeInfo : MonoBehaviour
{
	// Token: 0x06000A2F RID: 2607 RVA: 0x00008CDB File Offset: 0x00006EDB
	private void Awake()
	{
		GameModeInfo.instance = this;
	}

	// Token: 0x04000B0F RID: 2831
	public SpawnPoint defenderMainBase;

	// Token: 0x04000B10 RID: 2832
	public SpawnPoint defenderBase2;

	// Token: 0x04000B11 RID: 2833
	public SpawnPoint defenderBase3;

	// Token: 0x04000B12 RID: 2834
	public SpawnPoint attackerBase;

	// Token: 0x04000B13 RID: 2835
	public SpawnPoint kingOfTheHillBase;

	// Token: 0x04000B14 RID: 2836
	public static GameModeInfo instance;
}

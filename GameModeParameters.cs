using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
[Serializable]
public class GameModeParameters
{
	// Token: 0x04000B15 RID: 2837
	public GameObject gameModePrefab;

	// Token: 0x04000B16 RID: 2838
	public float balance;

	// Token: 0x04000B17 RID: 2839
	public bool useConquestRules;

	// Token: 0x04000B18 RID: 2840
	public int[] conquestBattalions = new int[2];

	// Token: 0x04000B19 RID: 2841
	public bool playerHasAllWeapons = true;

	// Token: 0x04000B1A RID: 2842
	public bool reverseMode;

	// Token: 0x04000B1B RID: 2843
	public bool nightMode;

	// Token: 0x04000B1C RID: 2844
	public bool halloweenMode;

	// Token: 0x04000B1D RID: 2845
	public bool noVehicles;

	// Token: 0x04000B1E RID: 2846
	public bool noTurrets;

	// Token: 0x04000B1F RID: 2847
	public bool configFlags;

	// Token: 0x04000B20 RID: 2848
	public bool bloodExplosions;

	// Token: 0x04000B21 RID: 2849
	public int playerTeam;

	// Token: 0x04000B22 RID: 2850
	public int respawnTime = 5;

	// Token: 0x04000B23 RID: 2851
	public int actorCount;

	// Token: 0x04000B24 RID: 2852
	public int gameLength = 1;

	// Token: 0x04000B25 RID: 2853
	public int loadedLevelEntry;
}

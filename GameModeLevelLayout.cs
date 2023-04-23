using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class GameModeLevelLayout : MonoBehaviour
{
	// Token: 0x04000CED RID: 3309
	public bool isBenchmark;

	// Token: 0x04000CEE RID: 3310
	public GameModeType gameMode;

	// Token: 0x04000CEF RID: 3311
	public bool useDefaultOwners = true;

	// Token: 0x04000CF0 RID: 3312
	public SpawnPoint[] blueSpawns;

	// Token: 0x04000CF1 RID: 3313
	public SpawnPoint[] redSpawns;

	// Token: 0x04000CF2 RID: 3314
	public SpawnPoint[] ghostSpawns;

	// Token: 0x04000CF3 RID: 3315
	public SpawnPoint[] deactivatedSpawns;
}

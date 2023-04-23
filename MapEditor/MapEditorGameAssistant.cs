using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000614 RID: 1556
	public class MapEditorGameAssistant : MonoBehaviour
	{
		// Token: 0x040025E4 RID: 9700
		public GameObject lateAwake;

		// Token: 0x040025E5 RID: 9701
		public BiomeContainer biomeContainer;

		// Token: 0x040025E6 RID: 9702
		public SpawnPointNeighborManager neighbourManager;

		// Token: 0x040025E7 RID: 9703
		public PathfindingManager pathfindingManager;

		// Token: 0x040025E8 RID: 9704
		public AstarPath astarPath;

		// Token: 0x040025E9 RID: 9705
		public MinimapCamera minimapCamera;

		// Token: 0x040025EA RID: 9706
		public SceneryCamera sceneryCamera;

		// Token: 0x040025EB RID: 9707
		public TimeOfDay timeOfDay;

		// Token: 0x040025EC RID: 9708
		public LevelColorGrading colorGrading;

		// Token: 0x040025ED RID: 9709
		public GameObject capturePointPrefab;

		// Token: 0x040025EE RID: 9710
		public GameObject vehicleSpawnerPrefab;

		// Token: 0x040025EF RID: 9711
		public GameObject turretSpawnerPrefab;
	}
}

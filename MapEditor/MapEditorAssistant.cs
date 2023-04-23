using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MapEditor
{
	// Token: 0x02000613 RID: 1555
	public class MapEditorAssistant : MonoBehaviour
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x0001B91B File Offset: 0x00019B1B
		private void Awake()
		{
			MapEditorAssistant.instance = this;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0001B923 File Offset: 0x00019B23
		public GameObject GetPrefab(AssetId id, out PrefabAsset asset)
		{
			asset = AssetTable.GetPrefab(id);
			if (asset.HasValue())
			{
				return asset.gameObject;
			}
			return this.missingPrefab;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000F97D8 File Offset: 0x000F79D8
		public GameObject GetPrefab(AssetId id)
		{
			PrefabAsset prefabAsset;
			return this.GetPrefab(id, out prefabAsset);
		}

		// Token: 0x040025D2 RID: 9682
		public static MapEditorAssistant instance;

		// Token: 0x040025D3 RID: 9683
		public Transform lateAwake;

		// Token: 0x040025D4 RID: 9684
		public MapEditor editor;

		// Token: 0x040025D5 RID: 9685
		public PathfindingManager pathfindingManager;

		// Token: 0x040025D6 RID: 9686
		public MinimapCamera minimapCamera;

		// Token: 0x040025D7 RID: 9687
		public PathfindingBox pathfindingBoxLand;

		// Token: 0x040025D8 RID: 9688
		public PathfindingBox pathfindingBoxWater;

		// Token: 0x040025D9 RID: 9689
		public TimeOfDay timeOfDay;

		// Token: 0x040025DA RID: 9690
		public EventSystem eventSystem;

		// Token: 0x040025DB RID: 9691
		public GameModeBase defaultGameMode;

		// Token: 0x040025DC RID: 9692
		public LevelColorGrading colorGrading;

		// Token: 0x040025DD RID: 9693
		public GameObject missingPrefab;

		// Token: 0x040025DE RID: 9694
		public GameObject capturePointRenderingPrefab;

		// Token: 0x040025DF RID: 9695
		public GameObject spawnPointRenderingPrefab;

		// Token: 0x040025E0 RID: 9696
		public GameObject vehicleSpawnRenderingPrefab;

		// Token: 0x040025E1 RID: 9697
		public GameObject turretSpawnRenderingPrefab;

		// Token: 0x040025E2 RID: 9698
		public GameObject avoidanceBoxRenderingPrefab;

		// Token: 0x040025E3 RID: 9699
		public GameObject sceneryCameraRenderingPrefab;
	}
}

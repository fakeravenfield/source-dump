using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000639 RID: 1593
	public class MeoSpawnPoint : MapEditorObject
	{
		// Token: 0x060028BF RID: 10431 RVA: 0x0001C10B File Offset: 0x0001A30B
		public override string GetCategoryName()
		{
			return "Spawn Point";
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000FB378 File Offset: 0x000F9578
		public override MapEditorObject Clone()
		{
			MeoSpawnPoint meoSpawnPoint = MeoSpawnPoint.Create(base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoSpawnPoint.transform);
			PropertyProvider.CopyProperties(this, meoSpawnPoint, false);
			return meoSpawnPoint;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0001C112 File Offset: 0x0001A312
		public static MeoSpawnPoint Create(Transform parent = null)
		{
			MeoSpawnPoint meoSpawnPoint = MapEditorObject.Create<MeoSpawnPoint>(MapEditorAssistant.instance.spawnPointRenderingPrefab, null, parent, true);
			meoSpawnPoint.selectableObject.DisableAction(MapEditor.Action.Rotate);
			meoSpawnPoint.selectableObject.DisableAction(MapEditor.Action.Scale);
			return meoSpawnPoint;
		}

		// Token: 0x040026AD RID: 9901
		[ShowInMapEditor(1)]
		[NonSerialized]
		public MeoSpawnPoint.SpawnType spawnType;

		// Token: 0x0200063A RID: 1594
		public enum SpawnType
		{
			// Token: 0x040026AF RID: 9903
			Normal,
			// Token: 0x040026B0 RID: 9904
			Contested
		}
	}
}

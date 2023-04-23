using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200063B RID: 1595
	public class MeoTurretSpawner : MapEditorObject, IPropertyChangeNotify
	{
		// Token: 0x060028C3 RID: 10435 RVA: 0x0001C13E File Offset: 0x0001A33E
		private void Start()
		{
			this.editor = MapEditor.instance;
			this.assistant = base.GetComponentInChildren<MeoTurretSpawnerAssistant>();
			this.SwitchTurretPrefab();
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0001C15D File Offset: 0x0001A35D
		public void OnPropertyChanged()
		{
			this.SwitchTurretPrefab();
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000FB3B0 File Offset: 0x000F95B0
		private void SwitchTurretPrefab()
		{
			GameObject turretPrefab = this.GetTurretPrefab(this.turret);
			this.prefabRenderer.RenderPrefab(turretPrefab, PrefabRenderer.RenderMode.Strict);
			this.selectableObject.GenerateColliders(turretPrefab);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000FB3E8 File Offset: 0x000F95E8
		private GameObject GetTurretPrefab(TurretSpawner.TurretSpawnType type)
		{
			GameObject[] array = this.assistant.turretPrefabs;
			if (array != null && type >= TurretSpawner.TurretSpawnType.MachineGun && type < (TurretSpawner.TurretSpawnType)array.Length)
			{
				return array[(int)type];
			}
			return MapEditorAssistant.instance.missingPrefab;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0001C165 File Offset: 0x0001A365
		public override string GetCategoryName()
		{
			return "Turret Spawner";
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000FB420 File Offset: 0x000F9620
		public override MapEditorObject Clone()
		{
			MeoTurretSpawner meoTurretSpawner = MeoTurretSpawner.Create(base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoTurretSpawner.transform);
			PropertyProvider.CopyProperties(this, meoTurretSpawner, false);
			return meoTurretSpawner;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0001C16C File Offset: 0x0001A36C
		public static MeoTurretSpawner Create(Transform parent = null)
		{
			MeoTurretSpawner meoTurretSpawner = MapEditorObject.Create<MeoTurretSpawner>(MapEditorAssistant.instance.turretSpawnRenderingPrefab, null, parent, false);
			meoTurretSpawner.selectableObject.DisableAction(MapEditor.Action.Scale);
			return meoTurretSpawner;
		}

		// Token: 0x040026B1 RID: 9905
		[ShowInMapEditor(1)]
		[NonSerialized]
		public TurretSpawner.TurretSpawnType turret;

		// Token: 0x040026B2 RID: 9906
		public GameObject[] turretPrefabs;

		// Token: 0x040026B3 RID: 9907
		public MeoTurretSpawnerAssistant assistant;

		// Token: 0x040026B4 RID: 9908
		private MapEditor editor;
	}
}

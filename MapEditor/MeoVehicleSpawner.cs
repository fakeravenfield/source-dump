using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200063D RID: 1597
	public class MeoVehicleSpawner : MapEditorObject, IPropertyChangeNotify
	{
		// Token: 0x060028CC RID: 10444 RVA: 0x0001C18C File Offset: 0x0001A38C
		private void Start()
		{
			this.assistant = base.GetComponentInChildren<MeoVehicleSpawnerAssistant>();
			this.SwitchVehiclePrefab();
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		public float GetSpawnHeight()
		{
			return this.spawnHeight;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		public void OnPropertyChanged()
		{
			this.SwitchVehiclePrefab();
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000FB458 File Offset: 0x000F9658
		private void SwitchVehiclePrefab()
		{
			GameObject vehiclePrefab = this.GetVehiclePrefab(this.vehicle);
			GameObject rendering = this.prefabRenderer.RenderPrefab(vehiclePrefab, PrefabRenderer.RenderMode.Strict);
			GameObject colliders = this.selectableObject.GenerateColliders(vehiclePrefab);
			this.prefabRenderer.SetLayer(new int?(Layers.GetSelectableLayer()));
			this.PlaceRenderingOnGround(rendering, colliders);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000FB4AC File Offset: 0x000F96AC
		private void PlaceRenderingOnGround(GameObject rendering, GameObject colliders)
		{
			Quaternion rotation = rendering.transform.rotation;
			rendering.transform.rotation = Quaternion.identity;
			rendering.transform.localPosition = Vector3.zero;
			float bottomOffset = Utils.GetBottomOffset(rendering);
			this.spawnHeight = -bottomOffset;
			rendering.transform.localPosition = new Vector3(0f, this.spawnHeight, 0f);
			colliders.transform.localPosition = rendering.transform.localPosition;
			rendering.transform.rotation = rotation;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000FB538 File Offset: 0x000F9738
		private GameObject GetVehiclePrefab(VehicleSpawner.VehicleSpawnType type)
		{
			GameObject[] vehiclePrefabs = this.assistant.vehiclePrefabs;
			if (vehiclePrefabs != null && type >= VehicleSpawner.VehicleSpawnType.Jeep && type < (VehicleSpawner.VehicleSpawnType)vehiclePrefabs.Length)
			{
				return vehiclePrefabs[(int)type];
			}
			return MapEditorAssistant.instance.missingPrefab;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public override string GetCategoryName()
		{
			return "Vehicle Spawner";
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000FB570 File Offset: 0x000F9770
		public override MapEditorObject Clone()
		{
			MeoVehicleSpawner meoVehicleSpawner = MeoVehicleSpawner.Create(base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoVehicleSpawner.transform);
			PropertyProvider.CopyProperties(this, meoVehicleSpawner, false);
			return meoVehicleSpawner;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0001C1B7 File Offset: 0x0001A3B7
		public static MeoVehicleSpawner Create(Transform parent = null)
		{
			MeoVehicleSpawner meoVehicleSpawner = MapEditorObject.Create<MeoVehicleSpawner>(MapEditorAssistant.instance.vehicleSpawnRenderingPrefab, null, parent, false);
			meoVehicleSpawner.selectableObject.DisableAction(MapEditor.Action.Scale);
			return meoVehicleSpawner;
		}

		// Token: 0x040026B6 RID: 9910
		[ShowInMapEditor(1)]
		[NonSerialized]
		public VehicleSpawner.VehicleSpawnType vehicle;

		// Token: 0x040026B7 RID: 9911
		[ShowInMapEditor(2)]
		[NonSerialized]
		public VehicleSpawner.RespawnType respawn;

		// Token: 0x040026B8 RID: 9912
		[ShowInMapEditor(3)]
		[Range(0f, 60f)]
		[NonSerialized]
		public float spawnTime = 16f;

		// Token: 0x040026B9 RID: 9913
		private MeoVehicleSpawnerAssistant assistant;

		// Token: 0x040026BA RID: 9914
		private float spawnHeight;

		// Token: 0x040026BB RID: 9915
		private VehicleSpawner.VehicleSpawnType renderedVehicle;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using MapEditor.Internal.DescriptorV1;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MapEditor
{
	// Token: 0x020005F6 RID: 1526
	public class EditorSceneConstructorV1 : ISceneConstructor
	{
		// Token: 0x06002727 RID: 10023 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public EditorSceneConstructorV1(MapDescriptorDataV1 descriptor)
		{
			this.descriptor = descriptor;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0001B0EF File Offset: 0x000192EF
		public void StartSceneConstruction()
		{
			MapEditorAssistant.instance.lateAwake.gameObject.SetActive(false);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000F66DC File Offset: 0x000F48DC
		public void EndSceneConstruction()
		{
			MapEditorAssistant instance = MapEditorAssistant.instance;
			if (this.descriptor.version > 2)
			{
				this.descriptor.timeOfDay.CopyTo(instance.timeOfDay);
				this.descriptor.atmosphereData.CopyTo(instance.colorGrading, instance.timeOfDay);
			}
			EventSystem.current = instance.eventSystem;
			instance.lateAwake.gameObject.SetActive(true);
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0001B106 File Offset: 0x00019306
		public IEnumerable<SceneConstructionProgress> ConstructSceneAsync()
		{
			if (this.descriptor.isAutosave)
			{
				MapEditor.descriptorFilePath = "";
			}
			Debug.LogFormat("Constructing editor scene with {0} objects", new object[]
			{
				this.descriptor.prefabs.Length
			});
			MapEditorAssistant assistant = MapEditorAssistant.instance;
			MapEditor editor = assistant.editor;
			MapEditorTerrain editorTerrain = editor.editorTerrain;
			MaterialList materialList = editor.materialList;
			Transform lateAwake = assistant.lateAwake;
			editor.mapDisplayName = this.descriptor.name;
			yield return new SceneConstructionProgress("Loading materials", 0f);
			materialList.Clear();
			foreach (MaterialDataV1 materialDataV in this.descriptor.materials)
			{
				materialList.Add(materialDataV.ToEditorMaterial(materialList));
			}
			GoPropertyProvider.DeserializeContext deserializeContext = new GoPropertyProvider.DeserializeContext
			{
				materialList = materialList
			};
			yield return new SceneConstructionProgress("Loading terrain", 0.04f);
			this.descriptor.terrain.CopyTo(editorTerrain, materialList);
			if (this.descriptor.minimapCamera.IsValid())
			{
				Camera componentInChildren = assistant.minimapCamera.GetComponentInChildren<Camera>();
				this.descriptor.minimapCamera.CopyTo(assistant.minimapCamera, componentInChildren);
			}
			if (this.descriptor.sceneryCamera.IsValid())
			{
				MeoSceneryCamera meo = MeoSceneryCamera.Create(lateAwake);
				this.descriptor.sceneryCamera.CopyTo(meo);
			}
			this.descriptor.worldData.CopyTo(editorTerrain.biomeContainer);
			yield return new SceneConstructionProgress("Loading prefabs", 0.05f);
			int j;
			for (int i = 0; i < this.descriptor.prefabs.Length; i = j + 1)
			{
				PrefabObjectDataV1 prefabObjectDataV = this.descriptor.prefabs[i];
				MeoPrefab meoPrefab = MeoPrefab.Create(prefabObjectDataV.prefab, lateAwake);
				prefabObjectDataV.CopyTo(meoPrefab, deserializeContext);
				if (this.descriptor.version == 1)
				{
					PrefabAsset prefab = AssetTable.GetPrefab(prefabObjectDataV.prefab);
					if (prefab.gameObject)
					{
						UtilsV1.ConvertV1Transform(prefabObjectDataV, meoPrefab.gameObject, prefab, lateAwake);
					}
				}
				if (i % 10 == 0)
				{
					float num = (float)i / (float)this.descriptor.prefabs.Length;
					yield return new SceneConstructionProgress("Loading prefabs", 0.05f + 0.8f * num);
				}
				j = i;
			}
			yield return new SceneConstructionProgress("Spawning vehicles", 0.85f);
			foreach (VehicleSpawnerDataV1 vehicleSpawnerDataV in this.descriptor.vehicleSpawners)
			{
				MeoVehicleSpawner vs = MeoVehicleSpawner.Create(lateAwake);
				vehicleSpawnerDataV.CopyTo(vs);
			}
			yield return new SceneConstructionProgress("Spawning turrets", 0.86f);
			foreach (TurretSpawnerDataV1 turretSpawnerDataV in this.descriptor.turretSpawners)
			{
				MeoTurretSpawner tp = MeoTurretSpawner.Create(lateAwake);
				turretSpawnerDataV.CopyTo(tp);
			}
			yield return new SceneConstructionProgress("Avoiding boxes", 0.87f);
			if (this.descriptor.avoidanceBoxes != null)
			{
				foreach (AvoidanceBoxDataV1 avoidanceBoxDataV in this.descriptor.avoidanceBoxes)
				{
					MeoAvoidanceBox box = MeoAvoidanceBox.Create(lateAwake);
					avoidanceBoxDataV.CopyTo(box);
				}
			}
			yield return new SceneConstructionProgress("Placing capture points", 0.88f);
			List<MeoCapturePoint> capturePoints = new List<MeoCapturePoint>();
			foreach (CapturePointDataV1 capturePointDataV in this.descriptor.capturePoints)
			{
				MeoCapturePoint meoCapturePoint = MeoCapturePoint.Create(lateAwake);
				capturePointDataV.CopyTo(meoCapturePoint);
				foreach (SpawnPointDataV1 spawnPointDataV in capturePointDataV.spawnPoints)
				{
					MeoSpawnPoint sp = MeoSpawnPoint.Create(lateAwake);
					spawnPointDataV.CopyTo(sp);
				}
				capturePoints.Add(meoCapturePoint);
			}
			yield return new SceneConstructionProgress("Finding neighbours", 0.87f);
			if (this.descriptor.neighbours != null)
			{
				foreach (NeighbourDataV1 neighbourDataV in this.descriptor.neighbours)
				{
					if (neighbourDataV.capturePointA != -1 && neighbourDataV.capturePointB != -1 && neighbourDataV.capturePointA < capturePoints.Count && neighbourDataV.capturePointB < capturePoints.Count)
					{
						MeoCapturePoint meoCapturePoint2 = capturePoints[neighbourDataV.capturePointA];
						MeoCapturePoint cp = capturePoints[neighbourDataV.capturePointB];
						meoCapturePoint2.AddNeighbour(cp, neighbourDataV.landConnection, neighbourDataV.waterConnection, neighbourDataV.oneWay);
					}
					else
					{
						Debug.LogWarningFormat("Dropped connection between CapturePoint {0} and {1}. Only {2} capture points are known.", new object[]
						{
							neighbourDataV.capturePointA,
							neighbourDataV.capturePointB,
							capturePoints.Count
						});
					}
				}
			}
			yield return new SceneConstructionProgress("Loading covers", 0.89f);
			using (MemoryStream memoryStream = this.descriptor.coverPoints.ToStream())
			{
				assistant.pathfindingManager.LoadCustomCoverPoints(memoryStream);
			}
			yield return new SceneConstructionProgress("Done", 1f);
			yield break;
		}

		// Token: 0x04002545 RID: 9541
		private MapDescriptorDataV1 descriptor;
	}
}

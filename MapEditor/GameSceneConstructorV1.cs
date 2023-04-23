using System;
using System.Collections.Generic;
using System.IO;
using MapEditor.Internal.DescriptorV1;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005F8 RID: 1528
	public class GameSceneConstructorV1 : ISceneConstructor
	{
		// Token: 0x06002733 RID: 10035 RVA: 0x0001B140 File Offset: 0x00019340
		public GameSceneConstructorV1(MapDescriptorDataV1 descriptor)
		{
			this.descriptor = descriptor;
			this.instantiatedPrefabs = new Dictionary<PrefabAsset, List<GameObject>>();
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0001B15A File Offset: 0x0001935A
		public void StartSceneConstruction()
		{
			UnityEngine.Object.FindObjectOfType<MapEditorGameAssistant>().lateAwake.gameObject.SetActive(false);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000F6EB0 File Offset: 0x000F50B0
		public void EndSceneConstruction()
		{
			MapEditorGameAssistant mapEditorGameAssistant = UnityEngine.Object.FindObjectOfType<MapEditorGameAssistant>();
			this.BakeMeshes(this.instantiatedPrefabs);
			if (this.descriptor.version > 2)
			{
				this.descriptor.timeOfDay.CopyTo(mapEditorGameAssistant.timeOfDay);
				this.descriptor.atmosphereData.CopyTo(mapEditorGameAssistant.colorGrading, mapEditorGameAssistant.timeOfDay);
			}
			mapEditorGameAssistant.lateAwake.gameObject.SetActive(true);
			PostProcessingManager.FindAndApplyLevelColorGrading(GameManager.GameParameters().nightMode);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0001B171 File Offset: 0x00019371
		public IEnumerable<SceneConstructionProgress> ConstructSceneAsync()
		{
			Debug.LogFormat("Constructing game scene with {0} objects", new object[]
			{
				this.descriptor.prefabs.Length
			});
			MapEditorGameAssistant assistant = UnityEngine.Object.FindObjectOfType<MapEditorGameAssistant>();
			GameObject lateAwake = assistant.lateAwake;
			yield return new SceneConstructionProgress("Loading materials", 0f);
			MaterialList materialList = assistant.GetOrCreateComponent<MaterialList>();
			foreach (MaterialDataV1 materialDataV in this.descriptor.materials)
			{
				materialList.Add(materialDataV.ToEditorMaterial(materialList));
			}
			GoPropertyProvider.DeserializeContext deserializeContext = new GoPropertyProvider.DeserializeContext
			{
				materialList = materialList
			};
			yield return new SceneConstructionProgress("Loading terrain", 0.04f);
			this.descriptor.terrain.CopyTo(assistant.biomeContainer, materialList);
			if (this.descriptor.minimapCamera.IsValid())
			{
				Camera componentInChildren = assistant.minimapCamera.GetComponentInChildren<Camera>();
				this.descriptor.minimapCamera.CopyTo(assistant.minimapCamera, componentInChildren);
			}
			if (this.descriptor.sceneryCamera.IsValid())
			{
				SceneryCamera sceneryCamera = assistant.sceneryCamera;
				this.descriptor.sceneryCamera.CopyTo(sceneryCamera);
			}
			this.descriptor.worldData.CopyTo(assistant.biomeContainer);
			yield return new SceneConstructionProgress("Loading prefabs", 0.05f);
			GameObject temporary = new GameObject("Temporary");
			temporary.transform.parent = lateAwake.transform;
			int j;
			for (int i = 0; i < this.descriptor.prefabs.Length; i = j + 1)
			{
				PrefabObjectDataV1 prefabObjectDataV = this.descriptor.prefabs[i];
				PrefabAsset prefab = AssetTable.GetPrefab(prefabObjectDataV.prefab);
				if (prefab.gameObject == null)
				{
					Debug.LogError("Prefab is missing: " + prefabObjectDataV.prefab.guid);
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab.gameObject, lateAwake.transform);
					gameObject.name = prefabObjectDataV.name;
					prefabObjectDataV.transform.CopyTo(gameObject.transform);
					if (this.descriptor.version == 1)
					{
						UtilsV1.ConvertV1Transform(prefabObjectDataV, gameObject, prefab, lateAwake.transform);
					}
					if (prefabObjectDataV.properties != null && prefabObjectDataV.properties.Length != 0)
					{
						MeoPrefabAssistant componentInChildren2 = gameObject.gameObject.GetComponentInChildren<MeoPrefabAssistant>();
						if (componentInChildren2 && componentInChildren2.serializedObject)
						{
							GoPropertyProvider orCreateComponent = gameObject.GetOrCreateComponent<GoPropertyProvider>();
							orCreateComponent.SetTarget(componentInChildren2.serializedObject);
							prefabObjectDataV.CopyPropertiesTo(orCreateComponent, deserializeContext);
							UnityEngine.Object.Destroy(orCreateComponent);
						}
					}
					if (gameObject != null)
					{
						if (!this.instantiatedPrefabs.ContainsKey(prefab))
						{
							this.instantiatedPrefabs.Add(prefab, new List<GameObject>());
						}
						this.instantiatedPrefabs[prefab].Add(gameObject);
						Stairways[] componentsInChildren = gameObject.GetComponentsInChildren<Stairways>();
						for (j = 0; j < componentsInChildren.Length; j++)
						{
							componentsInChildren[j].Build();
						}
					}
					if (i % 10 == 0)
					{
						float num = (float)i / (float)this.descriptor.prefabs.Length;
						yield return new SceneConstructionProgress("Loading prefabs", 0.05f + 0.8f * num);
					}
				}
				j = i;
			}
			UnityEngine.Object.DestroyImmediate(temporary);
			yield return new SceneConstructionProgress("Spawning vehicles", 0.85f);
			foreach (VehicleSpawnerDataV1 vehicleSpawnerDataV in this.descriptor.vehicleSpawners)
			{
				VehicleSpawner component = UnityEngine.Object.Instantiate<GameObject>(assistant.vehicleSpawnerPrefab, lateAwake.transform).GetComponent<VehicleSpawner>();
				vehicleSpawnerDataV.CopyTo(component);
			}
			yield return new SceneConstructionProgress("Spawning turrets", 0.86f);
			foreach (TurretSpawnerDataV1 turretSpawnerDataV in this.descriptor.turretSpawners)
			{
				TurretSpawner component2 = UnityEngine.Object.Instantiate<GameObject>(assistant.turretSpawnerPrefab, lateAwake.transform).GetComponent<TurretSpawner>();
				turretSpawnerDataV.CopyTo(component2);
			}
			yield return new SceneConstructionProgress("Avoiding boxes", 0.87f);
			if (this.descriptor.avoidanceBoxes != null)
			{
				foreach (AvoidanceBoxDataV1 avoidanceBoxDataV in this.descriptor.avoidanceBoxes)
				{
					GameObject gameObject2 = new GameObject();
					gameObject2.transform.SetParent(lateAwake.transform);
					AvoidanceBox box = gameObject2.AddComponent<AvoidanceBox>();
					avoidanceBoxDataV.CopyTo(box);
				}
			}
			yield return new SceneConstructionProgress("Placing capture points", 0.88f);
			List<CapturePoint> capturePoints = new List<CapturePoint>();
			foreach (CapturePointDataV1 capturePointDataV in this.descriptor.capturePoints)
			{
				CapturePoint component3 = UnityEngine.Object.Instantiate<GameObject>(assistant.capturePointPrefab, lateAwake.transform).GetComponent<CapturePoint>();
				if (component3.spawnpointContainer != null)
				{
					UnityEngine.Object.Destroy(component3.spawnpointContainer.gameObject);
					component3.spawnpointContainer = null;
				}
				if (component3.contestedSpawnpointContainer != null)
				{
					UnityEngine.Object.Destroy(component3.contestedSpawnpointContainer.gameObject);
					component3.contestedSpawnpointContainer = null;
				}
				capturePointDataV.CopyTo(component3);
				capturePoints.Add(component3);
				GameObject gameObject3 = new GameObject("Spawn Container");
				gameObject3.transform.parent = component3.transform;
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localRotation = Quaternion.identity;
				gameObject3.transform.localScale = Vector3.one;
				GameObject gameObject4 = new GameObject("Contested Spawn Container");
				gameObject4.transform.parent = component3.transform;
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.transform.localRotation = Quaternion.identity;
				gameObject4.transform.localScale = Vector3.one;
				component3.spawnpointContainer = gameObject3.transform;
				component3.contestedSpawnpointContainer = gameObject4.transform;
				foreach (SpawnPointDataV1 spawnPointDataV in capturePointDataV.spawnPoints)
				{
					GameObject gameObject5 = new GameObject();
					PositionRotationDataV1 transform = spawnPointDataV.transform;
					transform.CopyTo(gameObject5.transform);
					MeoSpawnPoint.SpawnType spawnType = spawnPointDataV.GetSpawnType();
					if (spawnType != MeoSpawnPoint.SpawnType.Normal)
					{
						if (spawnType == MeoSpawnPoint.SpawnType.Contested)
						{
							gameObject5.transform.parent = component3.contestedSpawnpointContainer;
						}
					}
					else
					{
						gameObject5.transform.parent = component3.spawnpointContainer;
					}
				}
				if (component3.spawnpointContainer != null && component3.spawnpointContainer.transform.childCount == 0)
				{
					UnityEngine.Object.Destroy(component3.spawnpointContainer.gameObject);
					component3.spawnpointContainer = null;
				}
				if (component3.contestedSpawnpointContainer != null && component3.contestedSpawnpointContainer.transform.childCount == 0)
				{
					UnityEngine.Object.Destroy(component3.contestedSpawnpointContainer.gameObject);
					component3.contestedSpawnpointContainer = null;
				}
			}
			yield return new SceneConstructionProgress("Finding neighbours", 0.87f);
			List<SpawnPointNeighborManager.SpawnPointNeighbor> list = new List<SpawnPointNeighborManager.SpawnPointNeighbor>();
			if (this.descriptor.neighbours != null)
			{
				foreach (NeighbourDataV1 neighbourDataV in this.descriptor.neighbours)
				{
					if (neighbourDataV.capturePointA != -1 && neighbourDataV.capturePointB != -1 && neighbourDataV.capturePointA < capturePoints.Count && neighbourDataV.capturePointB < capturePoints.Count)
					{
						SpawnPointNeighborManager.SpawnPointNeighbor item = new SpawnPointNeighborManager.SpawnPointNeighbor
						{
							a = capturePoints[neighbourDataV.capturePointA],
							b = capturePoints[neighbourDataV.capturePointB],
							landConnection = neighbourDataV.landConnection,
							waterConnection = neighbourDataV.waterConnection,
							oneWay = neighbourDataV.oneWay
						};
						list.Add(item);
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
			assistant.neighbourManager.neighbors = list.ToArray();
			yield return new SceneConstructionProgress("Loading covers", 0.89f);
			using (MemoryStream memoryStream = this.descriptor.coverPoints.ToStream())
			{
				assistant.pathfindingManager.LoadCustomCoverPoints(memoryStream);
			}
			yield return new SceneConstructionProgress("Loading pathfinding", 0.95f);
			if (!AstarDataV1.IsNullOrEmpty(this.descriptor.astarPath))
			{
				this.descriptor.astarPath.CopyTo(AstarPath.active);
			}
			yield return new SceneConstructionProgress("Done", 1f);
			yield break;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000F6F30 File Offset: 0x000F5130
		private void BakeMeshes(Dictionary<PrefabAsset, List<GameObject>> instantiatedPrefabs)
		{
			Debug.Log("Baking meshes");
			GameObject staticBatchRoot = new GameObject("Baked Objects");
			List<GameObject> list = new List<GameObject>();
			int num = 0;
			foreach (PrefabAsset prefabAsset in instantiatedPrefabs.Keys)
			{
				if (prefabAsset.canBakeMesh)
				{
					List<GameObject> list2 = instantiatedPrefabs[prefabAsset];
					num += list2.Count;
					list.AddRange(list2);
				}
			}
			StaticBatchingUtility.Combine(list.ToArray(), staticBatchRoot);
			Debug.Log("Baked " + num.ToString() + " prefab instances");
		}

		// Token: 0x04002551 RID: 9553
		private MapDescriptorDataV1 descriptor;

		// Token: 0x04002552 RID: 9554
		private Dictionary<PrefabAsset, List<GameObject>> instantiatedPrefabs;
	}
}

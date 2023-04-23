using System;
using System.IO;
using System.Linq;
using MapEditor.Internal.DescriptorV1;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005F2 RID: 1522
	public class DescriptorConstructorV1
	{
		// Token: 0x06002706 RID: 9990 RVA: 0x0001AF09 File Offset: 0x00019109
		public static MapDescriptorDataV1 ConstructFromScene(MapDescriptorSettings settings)
		{
			return new DescriptorConstructorV1.Implementation(settings).GetDescriptor();
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0000296E File Offset: 0x00000B6E
		public static void GenerateNavMesh()
		{
		}

		// Token: 0x020005F3 RID: 1523
		private class Implementation
		{
			// Token: 0x06002709 RID: 9993 RVA: 0x0001AF16 File Offset: 0x00019116
			public Implementation(MapDescriptorSettings settings)
			{
				this.descriptor = new MapDescriptorDataV1();
				this.editor = MapEditor.instance;
				this.settings = settings;
				this.ConstructFromScene();
			}

			// Token: 0x0600270A RID: 9994 RVA: 0x0001AF42 File Offset: 0x00019142
			public MapDescriptorDataV1 GetDescriptor()
			{
				return this.descriptor;
			}

			// Token: 0x0600270B RID: 9995 RVA: 0x000F635C File Offset: 0x000F455C
			private MapDescriptorDataV1 ConstructFromScene()
			{
				MaterialList materialList = this.editor.materialList;
				MapEditorTerrain editorTerrain = this.editor.GetEditorTerrain();
				MeoCapturePoint[] capturePoints = this.editor.FindObjectsToSave<MeoCapturePoint>();
				MeoPrefab[] prefabObjects = this.editor.FindObjectsToSave<MeoPrefab>();
				MeoVehicleSpawner[] vehicleSpawners = this.editor.FindObjectsToSave<MeoVehicleSpawner>();
				MeoTurretSpawner[] turretSpawner = this.editor.FindObjectsToSave<MeoTurretSpawner>();
				MeoAvoidanceBox[] boxes = this.editor.FindObjectsToSave<MeoAvoidanceBox>();
				MinimapCamera[] minimapCameras = UnityEngine.Object.FindObjectsOfType<MinimapCamera>();
				MeoSceneryCamera sceneryCamera = this.editor.GetSceneryCamera();
				BiomeContainer biomeContainer = editorTerrain.biomeContainer;
				PathfindingManager instance = PathfindingManager.instance;
				AstarPath active = AstarPath.active;
				TimeOfDay instance2 = TimeOfDay.instance;
				LevelColorGrading colorGrading = UnityEngine.Object.FindObjectOfType<LevelColorGrading>();
				MeoCapturePoint.DistributeSpawnPoints();
				this.descriptor.version = 3;
				this.descriptor.name = this.editor.mapDisplayName;
				this.descriptor.isAutosave = this.settings.isAutosave;
				this.descriptor.capturePoints = this.Encode(capturePoints);
				this.descriptor.neighbours = this.EncodeNeighbours(capturePoints);
				this.descriptor.prefabs = this.Encode(prefabObjects);
				this.descriptor.materials = this.Encode(materialList);
				this.descriptor.vehicleSpawners = this.Encode(vehicleSpawners);
				this.descriptor.turretSpawners = this.Encode(turretSpawner);
				this.descriptor.avoidanceBoxes = this.Encode(boxes);
				this.descriptor.terrain = this.Encode(editorTerrain);
				this.descriptor.coverPoints = this.Encode(instance);
				this.descriptor.minimapCamera = this.Encode(minimapCameras);
				this.descriptor.sceneryCamera = this.Encode(sceneryCamera);
				this.descriptor.timeOfDay = this.Encode(instance2);
				this.descriptor.atmosphereData = this.Encode(colorGrading, instance2);
				this.descriptor.worldData = this.Encode(biomeContainer);
				if (this.settings.savePathfinding)
				{
					if (this.settings.generateNavMesh)
					{
						this.editor.GenerateNavMesh();
					}
					this.descriptor.astarPath = this.Encode(active);
				}
				return this.descriptor;
			}

			// Token: 0x0600270C RID: 9996 RVA: 0x0001AF4A File Offset: 0x0001914A
			private CapturePointDataV1[] Encode(MeoCapturePoint[] capturePoints)
			{
				return (from x in capturePoints
				select new CapturePointDataV1(x)).ToArray<CapturePointDataV1>();
			}

			// Token: 0x0600270D RID: 9997 RVA: 0x000F657C File Offset: 0x000F477C
			private NeighbourDataV1[] EncodeNeighbours(MeoCapturePoint[] capturePoints)
			{
				return (from n in capturePoints.SelectMany((MeoCapturePoint cp) => cp.GetNeighbours()).Select(delegate(MeoCapturePoint.Neighbour n)
				{
					int capturePointA = Array.IndexOf<MeoCapturePoint>(capturePoints, n.capturePointA);
					int capturePointB = Array.IndexOf<MeoCapturePoint>(capturePoints, n.capturePointB);
					return new NeighbourDataV1(n, capturePointA, capturePointB);
				})
				where n.capturePointA != -1 && n.capturePointB != -1
				select n).ToArray<NeighbourDataV1>();
			}

			// Token: 0x0600270E RID: 9998 RVA: 0x0001AF76 File Offset: 0x00019176
			private PrefabObjectDataV1[] Encode(MeoPrefab[] prefabObjects)
			{
				return (from x in prefabObjects
				select new PrefabObjectDataV1(x)).ToArray<PrefabObjectDataV1>();
			}

			// Token: 0x0600270F RID: 9999 RVA: 0x0001AFA2 File Offset: 0x000191A2
			private MaterialDataV1[] Encode(MaterialList materialList)
			{
				return (from x in materialList
				select new MaterialDataV1(x)).ToArray<MaterialDataV1>();
			}

			// Token: 0x06002710 RID: 10000 RVA: 0x0001AFCE File Offset: 0x000191CE
			private VehicleSpawnerDataV1[] Encode(MeoVehicleSpawner[] vehicleSpawners)
			{
				return (from x in vehicleSpawners
				select new VehicleSpawnerDataV1(x)).ToArray<VehicleSpawnerDataV1>();
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x0001AFFA File Offset: 0x000191FA
			private TurretSpawnerDataV1[] Encode(MeoTurretSpawner[] turretSpawner)
			{
				return (from x in turretSpawner
				select new TurretSpawnerDataV1(x)).ToArray<TurretSpawnerDataV1>();
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x0001B026 File Offset: 0x00019226
			private AvoidanceBoxDataV1[] Encode(MeoAvoidanceBox[] boxes)
			{
				return (from x in boxes
				select new AvoidanceBoxDataV1(x)).ToArray<AvoidanceBoxDataV1>();
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x0001B052 File Offset: 0x00019252
			private TerrainDataV1 Encode(MapEditorTerrain editorTerrain)
			{
				return new TerrainDataV1(editorTerrain);
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x0001B05A File Offset: 0x0001925A
			private AstarDataV1 Encode(AstarPath astarPath)
			{
				return new AstarDataV1(astarPath);
			}

			// Token: 0x06002715 RID: 10005 RVA: 0x000F65FC File Offset: 0x000F47FC
			private CoverPointsDataV1 Encode(PathfindingManager pathfinding)
			{
				CoverPointsDataV1 result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					PathfindingManager.instance.GenerateCoverPointList(memoryStream);
					memoryStream.Position = 0L;
					result = new CoverPointsDataV1(memoryStream);
				}
				return result;
			}

			// Token: 0x06002716 RID: 10006 RVA: 0x000F6648 File Offset: 0x000F4848
			private MinimapCameraDataV1 Encode(MinimapCamera[] minimapCameras)
			{
				foreach (MinimapCamera minimapCamera in minimapCameras)
				{
					if (minimapCamera.GetType() == typeof(MinimapCamera))
					{
						return new MinimapCameraDataV1(minimapCamera, minimapCamera.camera);
					}
				}
				Debug.LogError("No minimap camera found in scene!");
				return default(MinimapCameraDataV1);
			}

			// Token: 0x06002717 RID: 10007 RVA: 0x0001B062 File Offset: 0x00019262
			private SceneryCameraDataV1 Encode(MeoSceneryCamera camera)
			{
				return new SceneryCameraDataV1(camera);
			}

			// Token: 0x06002718 RID: 10008 RVA: 0x0001B06A File Offset: 0x0001926A
			private TimeOfDayDataV1 Encode(TimeOfDay tod)
			{
				return new TimeOfDayDataV1(tod);
			}

			// Token: 0x06002719 RID: 10009 RVA: 0x0001B072 File Offset: 0x00019272
			private AtmosphereDataV1 Encode(LevelColorGrading colorGrading, TimeOfDay timeOfDay)
			{
				return new AtmosphereDataV1(colorGrading, timeOfDay);
			}

			// Token: 0x0600271A RID: 10010 RVA: 0x0001B07B File Offset: 0x0001927B
			private WorldDataV1 Encode(BiomeContainer biomeContainer)
			{
				return new WorldDataV1(biomeContainer);
			}

			// Token: 0x04002538 RID: 9528
			private MapDescriptorDataV1 descriptor;

			// Token: 0x04002539 RID: 9529
			private MapEditor editor;

			// Token: 0x0400253A RID: 9530
			private MapDescriptorSettings settings;
		}
	}
}

using System;
using System.Reflection;
using MapEditor.Internal.DescriptorV1;

namespace MapEditor
{
	// Token: 0x020005FA RID: 1530
	[Serializable]
	public class MapDescriptorDataV1 : MapDescriptorDataHeader
	{
		// Token: 0x06002740 RID: 10048 RVA: 0x000F7AF8 File Offset: 0x000F5CF8
		public void PostProcessDeserialization()
		{
			foreach (FieldInfo fieldInfo in typeof(MapDescriptorDataV1).GetFields())
			{
				IPostProcessDeserialization postProcessDeserialization = fieldInfo.GetValue(this) as IPostProcessDeserialization;
				if (postProcessDeserialization != null)
				{
					postProcessDeserialization.PostProcessDeserialization();
					fieldInfo.SetValue(this, postProcessDeserialization);
				}
			}
		}

		// Token: 0x0400255E RID: 9566
		public CapturePointDataV1[] capturePoints;

		// Token: 0x0400255F RID: 9567
		public NeighbourDataV1[] neighbours;

		// Token: 0x04002560 RID: 9568
		public MinimapCameraDataV1 minimapCamera;

		// Token: 0x04002561 RID: 9569
		public SceneryCameraDataV1 sceneryCamera;

		// Token: 0x04002562 RID: 9570
		public WorldDataV1 worldData;

		// Token: 0x04002563 RID: 9571
		public TimeOfDayDataV1 timeOfDay;

		// Token: 0x04002564 RID: 9572
		public PrefabObjectDataV1[] prefabs;

		// Token: 0x04002565 RID: 9573
		public MaterialDataV1[] materials;

		// Token: 0x04002566 RID: 9574
		public VehicleSpawnerDataV1[] vehicleSpawners;

		// Token: 0x04002567 RID: 9575
		public TurretSpawnerDataV1[] turretSpawners;

		// Token: 0x04002568 RID: 9576
		public AvoidanceBoxDataV1[] avoidanceBoxes;

		// Token: 0x04002569 RID: 9577
		public TerrainDataV1 terrain;

		// Token: 0x0400256A RID: 9578
		public AstarDataV1 astarPath;

		// Token: 0x0400256B RID: 9579
		public CoverPointsDataV1 coverPoints;

		// Token: 0x0400256C RID: 9580
		public AtmosphereDataV1 atmosphereData;
	}
}

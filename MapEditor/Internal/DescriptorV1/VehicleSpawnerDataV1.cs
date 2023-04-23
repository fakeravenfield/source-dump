using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000763 RID: 1891
	[Serializable]
	public struct VehicleSpawnerDataV1
	{
		// Token: 0x06002ECA RID: 11978 RVA: 0x00109B54 File Offset: 0x00107D54
		public VehicleSpawnerDataV1(MeoVehicleSpawner vs)
		{
			this.name = vs.name;
			this.transform = new PositionRotationDataV1(vs.transform);
			this.vehicle = VehicleSpawnerDataV1.VEHICLE.Encode(vs.vehicle);
			this.respawn = VehicleSpawnerDataV1.RESPAWN.Encode(vs.respawn);
			this.spawnTime = vs.spawnTime;
			this.spawnHeight = vs.GetSpawnHeight();
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00109BC4 File Offset: 0x00107DC4
		public void CopyTo(MeoVehicleSpawner vs)
		{
			vs.name = this.name;
			this.transform.CopyTo(vs.transform);
			vs.vehicle = this.GetVehicleType();
			vs.respawn = this.GetRespawnType();
			vs.spawnTime = this.spawnTime;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00109C14 File Offset: 0x00107E14
		public void CopyTo(VehicleSpawner vs)
		{
			vs.name = this.name;
			this.transform.CopyTo(vs.transform);
			vs.transform.position += Vector3.up * this.spawnHeight;
			vs.typeToSpawn = this.GetVehicleType();
			vs.respawnType = this.GetRespawnType();
			vs.spawnTime = this.spawnTime;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00020361 File Offset: 0x0001E561
		public VehicleSpawner.VehicleSpawnType GetVehicleType()
		{
			return VehicleSpawnerDataV1.VEHICLE.Decode(this.vehicle);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00020373 File Offset: 0x0001E573
		public VehicleSpawner.RespawnType GetRespawnType()
		{
			return VehicleSpawnerDataV1.RESPAWN.Decode(this.respawn);
		}

		// Token: 0x04002AF2 RID: 10994
		private static readonly EnumEncoder<VehicleSpawner.VehicleSpawnType> VEHICLE = new EnumEncoder<VehicleSpawner.VehicleSpawnType>();

		// Token: 0x04002AF3 RID: 10995
		private static readonly EnumEncoder<VehicleSpawner.RespawnType> RESPAWN = new EnumEncoder<VehicleSpawner.RespawnType>();

		// Token: 0x04002AF4 RID: 10996
		public string name;

		// Token: 0x04002AF5 RID: 10997
		public PositionRotationDataV1 transform;

		// Token: 0x04002AF6 RID: 10998
		public string vehicle;

		// Token: 0x04002AF7 RID: 10999
		public string respawn;

		// Token: 0x04002AF8 RID: 11000
		public float spawnTime;

		// Token: 0x04002AF9 RID: 11001
		public float spawnHeight;
	}
}

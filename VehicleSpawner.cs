using System;
using Lua;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class VehicleSpawner : MonoBehaviour
{
	// Token: 0x06000C9A RID: 3226 RVA: 0x0007977C File Offset: 0x0007797C
	private void Awake()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (this.prefab != null)
		{
			GameManager.SetupVehiclePrefab(this.prefab, GameManager.instance.levelBundleContentInfo);
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0000A48A File Offset: 0x0000868A
	private void StartSpawnCountdown()
	{
		base.CancelInvoke();
		base.Invoke("SpawnVehicleWhenClear", this.spawnTime);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x000797C4 File Offset: 0x000779C4
	public void SpawnVehicleWhenClear()
	{
		GameObject gameObject = this.GetPrefab();
		if (gameObject == null)
		{
			base.Invoke("SpawnVehicleWhenClear", 1f);
			return;
		}
		Vehicle component = gameObject.GetComponent<Vehicle>();
		if (component == null)
		{
			Debug.LogError("Vehicle prefab " + gameObject.name + " has no vehicle component");
			base.Invoke("SpawnVehicleWhenClear", 1f);
			return;
		}
		if (this.SpawnIsBlocked(component))
		{
			base.Invoke("SpawnVehicleWhenClear", 1f);
			return;
		}
		this.SpawnVehicle();
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00079850 File Offset: 0x00077A50
	public Vehicle SpawnVehicle()
	{
		GameObject gameObject = this.GetPrefab();
		if (gameObject == null)
		{
			return null;
		}
		this.lastSpawnedVehicle = UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform.position, base.transform.rotation).GetComponent<Vehicle>();
		this.lastSpawnedVehicle.SetSpawner(this);
		this.lastSpawnedVehicleHasBeenUsed = false;
		Vehicle component = this.lastSpawnedVehicle.GetComponent<Vehicle>();
		RavenscriptManager.events.onVehicleSpawn.Invoke(component, this);
		return component;
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0000A4A3 File Offset: 0x000086A3
	public bool SpawnIsBlocked(Vehicle vehicle)
	{
		return Physics.OverlapSphereNonAlloc(base.transform.position, vehicle.GetAvoidanceCoarseRadius(), VehicleSpawner.spawnCollisions, 5376) > 0;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0000A4C8 File Offset: 0x000086C8
	public void VehicleDied(Vehicle vehicle)
	{
		if (this.respawnType == VehicleSpawner.RespawnType.AfterDestroyed)
		{
			this.StartSpawnCountdown();
			return;
		}
		if (this.respawnType == VehicleSpawner.RespawnType.AfterMoved && vehicle == this.lastSpawnedVehicle && !this.lastSpawnedVehicleHasBeenUsed)
		{
			this.StartSpawnCountdown();
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0000A4FE File Offset: 0x000086FE
	public void FirstDriverEntered(Vehicle vehicle)
	{
		if (vehicle == this.lastSpawnedVehicle)
		{
			this.lastSpawnedVehicleHasBeenUsed = true;
			if (this.respawnType == VehicleSpawner.RespawnType.AfterMoved)
			{
				this.StartSpawnCountdown();
			}
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000798C8 File Offset: 0x00077AC8
	public bool HasAvailableVehicle()
	{
		return !this.lastSpawnedVehicleHasBeenUsed && this.lastSpawnedVehicle != null && !this.lastSpawnedVehicle.isLocked && !this.lastSpawnedVehicle.dead && !this.lastSpawnedVehicle.burning && this.lastSpawnedVehicle.IsUnclaimed();
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0000A524 File Offset: 0x00008724
	public bool HasAvailableRoamingVehicle()
	{
		return this.HasAvailableVehicle() && this.lastSpawnedVehicle.aiType == Vehicle.AiType.Roam;
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00079920 File Offset: 0x00077B20
	public bool VehiclePrefabIsAircraft()
	{
		if (this.prefab == null)
		{
			return this.typeToSpawn == VehicleSpawner.VehicleSpawnType.AttackHelicopter || this.typeToSpawn == VehicleSpawner.VehicleSpawnType.TransportHelicopter || this.typeToSpawn == VehicleSpawner.VehicleSpawnType.AttackPlane || this.typeToSpawn == VehicleSpawner.VehicleSpawnType.BomberPlane;
		}
		bool result;
		try
		{
			result = this.prefab.GetComponent<Vehicle>().IsAircraft();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			result = false;
		}
		return result;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00079994 File Offset: 0x00077B94
	public bool VehiclePrefabIsWatercraft()
	{
		if (this.prefab == null)
		{
			return VehicleSpawner.TypeIsWatercraft(this.typeToSpawn);
		}
		bool result;
		try
		{
			result = this.prefab.GetComponent<Vehicle>().IsWatercraft();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			result = false;
		}
		return result;
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0000A53E File Offset: 0x0000873E
	public bool IsTransportType()
	{
		return this.prefab == null && (this.typeToSpawn == VehicleSpawner.VehicleSpawnType.Jeep || this.typeToSpawn == VehicleSpawner.VehicleSpawnType.Quad || this.typeToSpawn == VehicleSpawner.VehicleSpawnType.Rhib);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x0000A56C File Offset: 0x0000876C
	public int GetOwner()
	{
		if (this.spawnPoint == null)
		{
			return 0;
		}
		return this.spawnPoint.owner;
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000799EC File Offset: 0x00077BEC
	public GameObject GetPrefab()
	{
		int owner = this.GetOwner();
		if (owner == -1)
		{
			return null;
		}
		if (this.prefab == null)
		{
			return VehicleSpawner.GetPrefab(owner, this.typeToSpawn);
		}
		return this.prefab;
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0000A589 File Offset: 0x00008789
	public static GameObject GetPrefab(int team, VehicleSpawner.VehicleSpawnType type)
	{
		return GameManager.instance.gameInfo.team[team].vehiclePrefab[type];
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0000A5A7 File Offset: 0x000087A7
	public static bool TypeIsWatercraft(VehicleSpawner.VehicleSpawnType type)
	{
		return type == VehicleSpawner.VehicleSpawnType.Rhib || type == VehicleSpawner.VehicleSpawnType.AttackBoat;
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0000A5B3 File Offset: 0x000087B3
	public static Vehicle SpawnVehicleAt(Vector3 position, Quaternion rotation, int team, VehicleSpawner.VehicleSpawnType type)
	{
		return UnityEngine.Object.Instantiate<GameObject>(VehicleSpawner.GetPrefab(team, type), position, rotation).GetComponent<Vehicle>();
	}

	// Token: 0x04000D92 RID: 3474
	private const int SPAWN_BLOCK_MASK = 5376;

	// Token: 0x04000D93 RID: 3475
	private static Collider[] spawnCollisions = new Collider[1];

	// Token: 0x04000D94 RID: 3476
	public static readonly VehicleSpawner.VehicleSpawnType[] ALL_VEHICLE_TYPES = (VehicleSpawner.VehicleSpawnType[])Enum.GetValues(typeof(VehicleSpawner.VehicleSpawnType));

	// Token: 0x04000D95 RID: 3477
	public float spawnTime = 16f;

	// Token: 0x04000D96 RID: 3478
	public VehicleSpawner.RespawnType respawnType;

	// Token: 0x04000D97 RID: 3479
	public VehicleSpawner.VehicleSpawnType typeToSpawn;

	// Token: 0x04000D98 RID: 3480
	public GameObject prefab;

	// Token: 0x04000D99 RID: 3481
	[NonSerialized]
	public Vehicle lastSpawnedVehicle;

	// Token: 0x04000D9A RID: 3482
	[NonSerialized]
	public bool lastSpawnedVehicleHasBeenUsed;

	// Token: 0x04000D9B RID: 3483
	[NonSerialized]
	public SpawnPoint spawnPoint;

	// Token: 0x04000D9C RID: 3484
	public byte priority;

	// Token: 0x04000D9D RID: 3485
	public bool isRelevantPathfindingPointForBoats = true;

	// Token: 0x020001D7 RID: 471
	public enum VehicleSpawnType
	{
		// Token: 0x04000D9F RID: 3487
		Jeep,
		// Token: 0x04000DA0 RID: 3488
		JeepMachineGun,
		// Token: 0x04000DA1 RID: 3489
		Quad,
		// Token: 0x04000DA2 RID: 3490
		Tank,
		// Token: 0x04000DA3 RID: 3491
		AttackHelicopter,
		// Token: 0x04000DA4 RID: 3492
		AttackPlane,
		// Token: 0x04000DA5 RID: 3493
		Rhib,
		// Token: 0x04000DA6 RID: 3494
		AttackBoat,
		// Token: 0x04000DA7 RID: 3495
		BomberPlane,
		// Token: 0x04000DA8 RID: 3496
		TransportHelicopter,
		// Token: 0x04000DA9 RID: 3497
		Apc
	}

	// Token: 0x020001D8 RID: 472
	public enum RespawnType
	{
		// Token: 0x04000DAB RID: 3499
		AfterDestroyed,
		// Token: 0x04000DAC RID: 3500
		AfterMoved,
		// Token: 0x04000DAD RID: 3501
		Never
	}
}

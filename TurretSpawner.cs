using System;
using Lua;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class TurretSpawner : MonoBehaviour
{
	// Token: 0x06000C90 RID: 3216 RVA: 0x0007955C File Offset: 0x0007775C
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

	// Token: 0x06000C91 RID: 3217 RVA: 0x000795A4 File Offset: 0x000777A4
	public void SpawnTurrets()
	{
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = this.GetPrefab(i);
			if (gameObject != null)
			{
				this.spawnedTurret[i] = UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform.position, base.transform.rotation, base.transform.parent).GetComponent<Vehicle>();
				this.spawnedTurret[i].isTurret = true;
				this.spawnedTurret[i].gameObject.SetActive(false);
			}
		}
		this.hasSpawnedTurrets = true;
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0007962C File Offset: 0x0007782C
	public void ActivateTurret(int team)
	{
		if (!this.hasSpawnedTurrets || team == -1)
		{
			return;
		}
		try
		{
			int num = 1 - team;
			if (this.spawnedTurret[team] != null)
			{
				this.spawnedTurret[team].gameObject.SetActive(true);
				RavenscriptManager.events.onTurretActivated.Invoke(this.spawnedTurret[team], this);
			}
			if (this.spawnedTurret[num] != null)
			{
				foreach (Seat seat in this.spawnedTurret[num].seats)
				{
					if (seat.IsOccupied())
					{
						seat.occupant.LeaveSeat(false);
					}
				}
				this.spawnedTurret[num].gameObject.SetActive(false);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00079718 File Offset: 0x00077918
	public Vehicle GetActiveTurret()
	{
		try
		{
			for (int i = 0; i < 2; i++)
			{
				if (this.spawnedTurret[i] != null && this.spawnedTurret[i].gameObject.activeSelf)
				{
					return this.spawnedTurret[i];
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		return null;
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0000A3F7 File Offset: 0x000085F7
	public GameObject GetPrefab(int team)
	{
		if (this.prefab == null)
		{
			return TurretSpawner.GetPrefab(team, this.typeToSpawn);
		}
		return this.prefab;
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0000A41A File Offset: 0x0000861A
	public static GameObject GetPrefab(int team, TurretSpawner.TurretSpawnType type)
	{
		return GameManager.instance.gameInfo.team[team].turretPrefab[type];
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0000A438 File Offset: 0x00008638
	public bool HasAssignedSpawn()
	{
		return this.spawnPoint != null;
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0000A446 File Offset: 0x00008646
	public static Vehicle SpawnTurretAt(Vector3 position, Quaternion rotation, int team, TurretSpawner.TurretSpawnType type)
	{
		return UnityEngine.Object.Instantiate<GameObject>(TurretSpawner.GetPrefab(team, type), position, rotation).GetComponent<Vehicle>();
	}

	// Token: 0x04000D87 RID: 3463
	public static readonly TurretSpawner.TurretSpawnType[] ALL_TURRET_TYPES = (TurretSpawner.TurretSpawnType[])Enum.GetValues(typeof(TurretSpawner.TurretSpawnType));

	// Token: 0x04000D88 RID: 3464
	public TurretSpawner.TurretSpawnType typeToSpawn;

	// Token: 0x04000D89 RID: 3465
	public GameObject prefab;

	// Token: 0x04000D8A RID: 3466
	[NonSerialized]
	public Vehicle[] spawnedTurret = new Vehicle[2];

	// Token: 0x04000D8B RID: 3467
	public byte priority;

	// Token: 0x04000D8C RID: 3468
	private bool hasSpawnedTurrets;

	// Token: 0x04000D8D RID: 3469
	[NonSerialized]
	public SpawnPoint spawnPoint;

	// Token: 0x020001D5 RID: 469
	public enum TurretSpawnType
	{
		// Token: 0x04000D8F RID: 3471
		MachineGun,
		// Token: 0x04000D90 RID: 3472
		AntiTank,
		// Token: 0x04000D91 RID: 3473
		AntiAir
	}
}

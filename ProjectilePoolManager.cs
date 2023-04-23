using System;
using System.Collections.Generic;
using System.Linq;
using Lua;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class ProjectilePoolManager : MonoBehaviour
{
	// Token: 0x0600073D RID: 1853 RVA: 0x00006A80 File Offset: 0x00004C80
	private void Awake()
	{
		ProjectilePoolManager.instance = this;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00061F8C File Offset: 0x0006018C
	public static Projectile InstantiateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int instanceID = prefab.GetInstanceID();
		Projectile result;
		if (ProjectilePoolManager.instance.IsPooledPrefab(instanceID))
		{
			result = ProjectilePoolManager.instance.pools[instanceID].Instantiate(position, rotation);
		}
		else
		{
			result = UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation).GetComponent<Projectile>();
		}
		return result;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00061FD8 File Offset: 0x000601D8
	public static void CleanupProjectile(Projectile projectile)
	{
		if (ProjectilePoolManager.instance.IsPooledPrefab(projectile.prefabInstanceId))
		{
			projectile.OnReturnedToPool();
			ProjectilePoolManager.instance.pools[projectile.prefabInstanceId].ReturnToPool(projectile);
			return;
		}
		UnityEngine.Object.Destroy(projectile.gameObject);
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00062024 File Offset: 0x00060224
	public void InitializePools(GameInfoContainer gameInfo)
	{
		this.pools = new Dictionary<int, ProjectilePool>();
		this.AnalyzeTeamInfo(gameInfo.team[0]);
		this.AnalyzeTeamInfo(gameInfo.team[1]);
		foreach (ProjectilePool projectilePool in this.pools.Values)
		{
			projectilePool.Initialize(32);
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x000620A4 File Offset: 0x000602A4
	private void AnalyzeTeamInfo(TeamInfo teamInfo)
	{
		this.AnalyzeWeaponEntries(teamInfo.availableWeapons);
		foreach (GameObject vehiclePrefab in teamInfo.AvailableVehiclePrefabs())
		{
			this.AnalyzeVehiclePrefab(vehiclePrefab);
		}
		foreach (GameObject vehiclePrefab2 in teamInfo.AvailableTurretPrefabs())
		{
			this.AnalyzeVehiclePrefab(vehiclePrefab2);
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0006213C File Offset: 0x0006033C
	private void AnalyzeWeaponEntries(IEnumerable<WeaponManager.WeaponEntry> weaponEntries)
	{
		foreach (WeaponManager.WeaponEntry weaponEntry in weaponEntries)
		{
			try
			{
				Weapon component = weaponEntry.prefab.GetComponent<Weapon>();
				this.AnalyzeWeaponAndSubweapons(component);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x000621A8 File Offset: 0x000603A8
	private void AnalyzeVehiclePrefab(GameObject vehiclePrefab)
	{
		try
		{
			foreach (Seat seat in vehiclePrefab.GetComponent<Vehicle>().seats)
			{
				foreach (MountedWeapon weapon in seat.weapons)
				{
					this.AnalyzeWeaponAndSubweapons(weapon);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0006222C File Offset: 0x0006042C
	private void AnalyzeWeaponAndSubweapons(Weapon weapon)
	{
		this.AnalyzeWeapon(weapon);
		foreach (Weapon weapon2 in weapon.alternativeWeapons)
		{
			this.AnalyzeWeapon(weapon2);
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00062288 File Offset: 0x00060488
	private void AnalyzeWeapon(Weapon weapon)
	{
		try
		{
			if (!(weapon.configuration.projectilePrefab == null))
			{
				Projectile component = weapon.configuration.projectilePrefab.GetComponent<Projectile>();
				if (this.ProjectileCanBePooled(component) && !this.pools.ContainsKey(component.gameObject.GetInstanceID()))
				{
					this.pools.Add(component.gameObject.GetInstanceID(), new ProjectilePool(component.gameObject));
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00006A88 File Offset: 0x00004C88
	private bool ProjectileTypeCanBePooled(Type projectileType)
	{
		return ProjectilePoolManager.POOLED_TYPES.Contains(projectileType);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00062318 File Offset: 0x00060518
	private bool ProjectileCanBePooled(Projectile projectile)
	{
		if (projectile == null)
		{
			return false;
		}
		if (!this.ProjectileTypeCanBePooled(projectile.GetType()))
		{
			return false;
		}
		if (projectile.GetComponentInChildren<ScriptedBehaviour>() != null)
		{
			return false;
		}
		ExplodingProjectile explodingProjectile = projectile as ExplodingProjectile;
		return !(explodingProjectile != null) || !(explodingProjectile.activateOnExplosion != null);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00006A95 File Offset: 0x00004C95
	private bool IsPooledPrefab(int instanceId)
	{
		return this.pools.ContainsKey(instanceId);
	}

	// Token: 0x04000744 RID: 1860
	public static ProjectilePoolManager instance;

	// Token: 0x04000745 RID: 1861
	private Dictionary<int, ProjectilePool> pools;

	// Token: 0x04000746 RID: 1862
	private static readonly Type[] POOLED_TYPES = new Type[]
	{
		typeof(Projectile),
		typeof(ExplodingProjectile),
		typeof(Rocket),
		typeof(TargetSeekingMissile)
	};

	// Token: 0x04000747 RID: 1863
	private const int POOL_SIZE = 32;
}

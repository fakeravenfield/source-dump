using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class SpawnPoint : MonoBehaviour
{
	// Token: 0x06000C1B RID: 3099 RVA: 0x00077D04 File Offset: 0x00075F04
	protected virtual void Awake()
	{
		if (this.spawnpointContainer != null)
		{
			foreach (Renderer renderer in this.spawnpointContainer.GetComponentsInChildren<Renderer>())
			{
				renderer.enabled = false;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(renderer.transform.position + Vector3.up * 0.5f, Vector3.down), out raycastHit, 3f, 1))
				{
					renderer.transform.position = raycastHit.point + Vector3.up * 0.5f;
				}
			}
		}
		this.sortedCoverPoints = new List<CoverPoint>();
		this.turretSpawners = new List<TurretSpawner>();
		this.vehicleSpawners = new List<VehicleSpawner>();
		this.landingZones = new List<LandingZone>();
		if (this.spawnpointContainer != null)
		{
			this.nSpawnPoints = this.spawnpointContainer.childCount;
		}
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00077DF0 File Offset: 0x00075FF0
	protected virtual void Start()
	{
		if (OrderManager.instance)
		{
			this.squadOrderPoint = OrderManager.CreateOrderPoint(base.transform, SquadOrderPoint.ObjectiveType.Attack, OrderManager.instance.pointOrderFlagTexture);
			this.squadOrderPoint.targetText = this.shortName;
			this.squadOrderPoint.transform.position += Vector3.up;
			this.squadOrderPoint.maxIssueDistance = 400f;
		}
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00009F9C File Offset: 0x0000819C
	public void FindNearbyStuff()
	{
		this.FindVehicleSpawners();
		this.FindTurrets();
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00009FAA File Offset: 0x000081AA
	public void SetGhost(bool isGhost)
	{
		this.isGhostSpawn = isGhost;
		MinimapUi.UpdateSpawnPointButton(this);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00009FB9 File Offset: 0x000081B9
	public bool HasAnyHelicopterLandingZone()
	{
		return this.hasGeneratedHelicopterLandingZone || this.HasCustomHelicopterLandingZone();
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00009FCB File Offset: 0x000081CB
	public void SetGeneratedHelicopterLandingZone(Vector3 position)
	{
		this.generatedLandingZone = position;
		this.hasGeneratedHelicopterLandingZone = true;
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00009FDB File Offset: 0x000081DB
	public bool HasCustomHelicopterLandingZone()
	{
		return this.helicopterLandingZones != null && this.helicopterLandingZones.Length != 0;
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00009FF1 File Offset: 0x000081F1
	public Vector3 GetHelicopterLandingZone()
	{
		if (this.HasCustomHelicopterLandingZone())
		{
			return this.helicopterLandingZones[UnityEngine.Random.Range(0, this.helicopterLandingZones.Length)].position;
		}
		if (this.hasGeneratedHelicopterLandingZone)
		{
			return this.generatedLandingZone;
		}
		return this.RandomPositionInCaptureZone();
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00077E68 File Offset: 0x00076068
	public void FindCoverPoints()
	{
		foreach (CoverPoint coverPoint in CoverManager.instance.coverPoints)
		{
			if (Vector3.Distance(base.transform.position, coverPoint.transform.position) < this.protectRange)
			{
				this.sortedCoverPoints.Add(coverPoint);
			}
		}
		this.effectiveCoverAgainstNeighbor = new Dictionary<CoverPoint, List<SpawnPoint>>();
		foreach (CoverPoint coverPoint2 in this.sortedCoverPoints)
		{
			this.effectiveCoverAgainstNeighbor.Add(coverPoint2, new List<SpawnPoint>());
			foreach (SpawnPoint spawnPoint in this.allNeighbors)
			{
				if (coverPoint2.CoversPoint(spawnPoint.transform.position) && !coverPoint2.CoversPoint(base.transform.position))
				{
					this.effectiveCoverAgainstNeighbor[coverPoint2].Add(spawnPoint);
				}
			}
		}
		this.sortedCoverPoints.Sort((CoverPoint x, CoverPoint y) => y.coverage.CompareTo(x.coverage));
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00077FC8 File Offset: 0x000761C8
	public CoverPoint GetAvailableCoverPoint()
	{
		CoverPoint result = null;
		foreach (CoverPoint coverPoint in this.sortedCoverPoints)
		{
			if (!coverPoint.taken)
			{
				int count = this.effectiveCoverAgainstNeighbor[coverPoint].Count;
				int num = UnityEngine.Random.Range(0, count);
				for (int i = 0; i < count; i++)
				{
					SpawnPoint spawnPoint = this.effectiveCoverAgainstNeighbor[coverPoint][(i + num) % count];
					if (spawnPoint.owner != this.owner)
					{
						Debug.DrawLine(coverPoint.transform.position + Vector3.up, spawnPoint.transform.position + Vector3.up * 8f, Color.cyan, 100f);
						return coverPoint;
					}
				}
				result = coverPoint;
			}
		}
		return result;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x000780CC File Offset: 0x000762CC
	private void FindVehicleSpawners()
	{
		foreach (VehicleSpawner vehicleSpawner in UnityEngine.Object.FindObjectsOfType<VehicleSpawner>())
		{
			if (vehicleSpawner.enabled && vehicleSpawner.spawnPoint == null && Vector3.Distance(base.transform.position, vehicleSpawner.transform.position) < this.protectRange)
			{
				vehicleSpawner.spawnPoint = this;
				this.vehicleSpawners.Add(vehicleSpawner);
				if (vehicleSpawner.VehiclePrefabIsAircraft())
				{
					this.hasAircraft = true;
				}
				if (vehicleSpawner.VehiclePrefabIsWatercraft())
				{
					this.hasWatercraft = true;
				}
			}
		}
		this.vehicleSpawners.Sort((VehicleSpawner x, VehicleSpawner y) => y.priority.CompareTo(x.priority));
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00078188 File Offset: 0x00076388
	public Vehicle GetAvailableVehicle(VehicleFilter filter, int passengers = -1)
	{
		byte b;
		return this.GetAvailableVehicle(filter, out b, passengers);
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000781A0 File Offset: 0x000763A0
	public Vehicle GetAvailableVehicle(VehicleFilter filter, out byte priority, int passengers = -1)
	{
		priority = 0;
		foreach (VehicleSpawner vehicleSpawner in this.vehicleSpawners)
		{
			if (vehicleSpawner.HasAvailableVehicle() && filter.VehiclePassesFilter(vehicleSpawner.lastSpawnedVehicle) && (passengers == -1 || (vehicleSpawner.lastSpawnedVehicle.EmptySeats() >= passengers && vehicleSpawner.lastSpawnedVehicle.minCrewCount <= passengers)))
			{
				priority = vehicleSpawner.priority;
				return vehicleSpawner.lastSpawnedVehicle;
			}
		}
		return null;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0007823C File Offset: 0x0007643C
	public Vehicle GetAvailableRoamingVehicle()
	{
		foreach (VehicleSpawner vehicleSpawner in this.vehicleSpawners)
		{
			if (vehicleSpawner.HasAvailableRoamingVehicle())
			{
				return vehicleSpawner.lastSpawnedVehicle;
			}
		}
		return null;
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0007829C File Offset: 0x0007649C
	public void FindTurrets()
	{
		foreach (TurretSpawner turretSpawner in UnityEngine.Object.FindObjectsOfType<TurretSpawner>())
		{
			if (turretSpawner.enabled && !turretSpawner.HasAssignedSpawn() && Vector3.Distance(base.transform.position, turretSpawner.transform.position) < this.protectRange)
			{
				this.turretSpawners.Add(turretSpawner);
				turretSpawner.spawnPoint = this;
			}
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00078308 File Offset: 0x00076508
	public bool IsValidDefenseTurret(Vehicle vehicle)
	{
		foreach (TurretSpawner turretSpawner in this.turretSpawners)
		{
			if (turretSpawner.spawnedTurret[0] == vehicle || turretSpawner.spawnedTurret[1] == vehicle)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0007837C File Offset: 0x0007657C
	public bool HasAnyAvailableTurrets()
	{
		foreach (TurretSpawner turretSpawner in this.turretSpawners)
		{
			Vehicle activeTurret = turretSpawner.GetActiveTurret();
			if (activeTurret != null && !activeTurret.dead && !activeTurret.IsFull() && !activeTurret.AllSeatsReserved())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x000783F8 File Offset: 0x000765F8
	public Vehicle GetAvailableTurret()
	{
		foreach (TurretSpawner turretSpawner in this.turretSpawners)
		{
			if (this.defenseStrategy == SpawnPoint.DefenseStrategy.AlwaysManTurrets || turretSpawner.typeToSpawn == TurretSpawner.TurretSpawnType.AntiAir || this.TransformFacesEnemySpawn(turretSpawner.transform))
			{
				Vehicle activeTurret = turretSpawner.GetActiveTurret();
				if (activeTurret != null && !activeTurret.dead && !activeTurret.IsFull() && !activeTurret.AllSeatsReserved())
				{
					return activeTurret;
				}
			}
		}
		return null;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00078494 File Offset: 0x00076694
	public bool TransformFacesEnemySpawn(Transform transform)
	{
		Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.owner != this.owner && worldToLocalMatrix.MultiplyPoint(spawnPoint.transform.position).z > 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0000A02B File Offset: 0x0000822B
	public virtual Vector3 GetSpawnPosition()
	{
		if (this.spawnpointContainer == null)
		{
			return this.GenerateRandomSpawnPosition();
		}
		return this.RandomSpawnPointPosition();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x000784F4 File Offset: 0x000766F4
	public Vector3 GenerateRandomSpawnPosition()
	{
		Vector3 vector = base.transform.position + Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(3f, 0f, 3f));
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(vector + Vector3.up * 3f, Vector3.down), out raycastHit))
		{
			return raycastHit.point;
		}
		return vector;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00078564 File Offset: 0x00076764
	public Vector3 RandomPositionInCaptureZone()
	{
		Vector2 vector = UnityEngine.Random.insideUnitCircle * this.GotoRadius();
		Vector3 vector2 = base.transform.position + new Vector3(vector.x, 0f, vector.y);
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(vector2 + Vector3.up * 3f, Vector3.down), out raycastHit))
		{
			return raycastHit.point;
		}
		return vector2;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x000785DC File Offset: 0x000767DC
	public Vector3 RandomSpawnPointPosition()
	{
		if (this.spawnpointContainer == null)
		{
			return this.GenerateRandomSpawnPosition();
		}
		int childCount = this.spawnpointContainer.childCount;
		if (childCount == 0)
		{
			return this.GenerateRandomSpawnPosition();
		}
		return this.spawnpointContainer.GetChild(UnityEngine.Random.Range(0, childCount)).position;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0000A048 File Offset: 0x00008248
	public virtual Vector3 RandomPatrolPosition()
	{
		if (this.nSpawnPoints > 10)
		{
			return this.RandomSpawnPointPosition();
		}
		return this.RandomPositionInCaptureZone();
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0000476F File Offset: 0x0000296F
	public virtual bool IsSafe()
	{
		return true;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0000A061 File Offset: 0x00008261
	public virtual float GotoRadius()
	{
		return 20f;
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0007862C File Offset: 0x0007682C
	public virtual void SetOwner(int team, bool initialOwner = false)
	{
		int num = this.owner;
		this.owner = team;
		if (!initialOwner)
		{
			OrderManager.NewOwnerOfSpawn(this);
		}
		BattlePlanUi.UpdateTacticsPointColor(this);
		bool flag = team == GameManager.PlayerTeam();
		this.squadOrderPoint.type = (flag ? SquadOrderPoint.ObjectiveType.Defend : SquadOrderPoint.ObjectiveType.Attack);
		if (this.captureAnimation != null && this.captureAnimation.animators != null)
		{
			foreach (Animation animation in this.captureAnimation.animators)
			{
				if (team == 0 && !string.IsNullOrEmpty(this.captureAnimation.blueCapturedAnimation))
				{
					animation.Play(this.captureAnimation.blueCapturedAnimation);
				}
				else if (team == 1 && !string.IsNullOrEmpty(this.captureAnimation.redCapturedAnimation))
				{
					animation.Play(this.captureAnimation.redCapturedAnimation);
				}
				else if (team == -1 && !string.IsNullOrEmpty(this.captureAnimation.neutralCapturedAnimation))
				{
					animation.Play(this.captureAnimation.neutralCapturedAnimation);
				}
			}
		}
		if (this.refreshTurretsOnCaptured && (team == 0 || team == 1))
		{
			foreach (TurretSpawner turretSpawner in this.turretSpawners)
			{
				turretSpawner.ActivateTurret(team);
			}
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00078780 File Offset: 0x00076980
	public virtual bool IsFrontLine()
	{
		using (List<SpawnPoint>.Enumerator enumerator = this.allNeighbors.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.owner != this.owner)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0000A068 File Offset: 0x00008268
	public void AddLandingZone(LandingZone lz)
	{
		this.landingZones.Add(lz);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0000257D File Offset: 0x0000077D
	public virtual bool IsInsideCaptureRange(Vector3 position)
	{
		return false;
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x00004B4B File Offset: 0x00002D4B
	public virtual float GetCaptureRange()
	{
		return 0f;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x000787E0 File Offset: 0x000769E0
	public bool IsInsideProtectRange(Vector3 position)
	{
		Vector3 vector = position - base.transform.position;
		vector.y = 0f;
		return vector.magnitude < this.protectRange;
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0000A076 File Offset: 0x00008276
	public virtual bool IsInsideTransportDropRange(Vector3 position)
	{
		return this.IsInsideProtectRange(position);
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0000A07F File Offset: 0x0000827F
	public uint GetClosestNavmeshArea(PathfindingBox.Type type)
	{
		if (type == PathfindingBox.Type.Boat)
		{
			return this.closestBoatNavmeshArea;
		}
		if (type == PathfindingBox.Type.Car)
		{
			return this.closestCarNavmeshArea;
		}
		return this.closestInfantryNavmeshArea;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0000A09D File Offset: 0x0000829D
	public bool IsNeighborTo(SpawnPoint other)
	{
		return this.allNeighbors.Contains(other);
	}

	// Token: 0x04000D29 RID: 3369
	public SpawnPoint.Team defaultOwner = SpawnPoint.Team.Neutral;

	// Token: 0x04000D2A RID: 3370
	private const float MAX_SQUAD_ORDER_POINT_ISSUE_DISTANCE = 400f;

	// Token: 0x04000D2B RID: 3371
	[NonSerialized]
	public int owner = -1;

	// Token: 0x04000D2C RID: 3372
	[NonSerialized]
	public List<SpawnPoint> allNeighbors;

	// Token: 0x04000D2D RID: 3373
	[NonSerialized]
	public List<SpawnPoint> outgoingNeighbors;

	// Token: 0x04000D2E RID: 3374
	[NonSerialized]
	public List<SpawnPoint> incomingNeighbors;

	// Token: 0x04000D2F RID: 3375
	public Transform spawnpointContainer;

	// Token: 0x04000D30 RID: 3376
	public float protectRange = 60f;

	// Token: 0x04000D31 RID: 3377
	public SpawnPoint.DefenseStrategy defenseStrategy;

	// Token: 0x04000D32 RID: 3378
	public VehicleFilter vehicleFilter;

	// Token: 0x04000D33 RID: 3379
	public string shortName = "SPAWN";

	// Token: 0x04000D34 RID: 3380
	public bool isRelevantPathfindingPoint = true;

	// Token: 0x04000D35 RID: 3381
	public SpawnPoint.CaptureAnimation captureAnimation;

	// Token: 0x04000D36 RID: 3382
	public Transform[] helicopterLandingZones;

	// Token: 0x04000D37 RID: 3383
	private int nSpawnPoints;

	// Token: 0x04000D38 RID: 3384
	private Dictionary<CoverPoint, List<SpawnPoint>> effectiveCoverAgainstNeighbor;

	// Token: 0x04000D39 RID: 3385
	[NonSerialized]
	public List<CoverPoint> sortedCoverPoints;

	// Token: 0x04000D3A RID: 3386
	[NonSerialized]
	public List<TurretSpawner> turretSpawners;

	// Token: 0x04000D3B RID: 3387
	[NonSerialized]
	public List<VehicleSpawner> vehicleSpawners;

	// Token: 0x04000D3C RID: 3388
	[NonSerialized]
	public List<LandingZone> landingZones;

	// Token: 0x04000D3D RID: 3389
	[NonSerialized]
	public uint closestInfantryNavmeshArea = uint.MaxValue;

	// Token: 0x04000D3E RID: 3390
	[NonSerialized]
	public uint closestCarNavmeshArea = uint.MaxValue;

	// Token: 0x04000D3F RID: 3391
	[NonSerialized]
	public uint closestBoatNavmeshArea = uint.MaxValue;

	// Token: 0x04000D40 RID: 3392
	[NonSerialized]
	public bool hasWatercraft;

	// Token: 0x04000D41 RID: 3393
	[NonSerialized]
	public bool hasAircraft;

	// Token: 0x04000D42 RID: 3394
	[NonSerialized]
	public bool refreshTurretsOnCaptured = true;

	// Token: 0x04000D43 RID: 3395
	[NonSerialized]
	public int nAirConnections;

	// Token: 0x04000D44 RID: 3396
	[NonSerialized]
	public int nLandConnections;

	// Token: 0x04000D45 RID: 3397
	[NonSerialized]
	public int nWaterConnections;

	// Token: 0x04000D46 RID: 3398
	protected SquadOrderPoint squadOrderPoint;

	// Token: 0x04000D47 RID: 3399
	private bool hasGeneratedHelicopterLandingZone;

	// Token: 0x04000D48 RID: 3400
	private Vector3 generatedLandingZone;

	// Token: 0x04000D49 RID: 3401
	[NonSerialized]
	public bool isGhostSpawn;

	// Token: 0x020001C3 RID: 451
	public enum DefenseStrategy
	{
		// Token: 0x04000D4B RID: 3403
		Default,
		// Token: 0x04000D4C RID: 3404
		NeverAutoDefend,
		// Token: 0x04000D4D RID: 3405
		AlwaysAutoDefend,
		// Token: 0x04000D4E RID: 3406
		AlwaysManTurrets
	}

	// Token: 0x020001C4 RID: 452
	public enum Team
	{
		// Token: 0x04000D50 RID: 3408
		Neutral = -1,
		// Token: 0x04000D51 RID: 3409
		Blue,
		// Token: 0x04000D52 RID: 3410
		Red
	}

	// Token: 0x020001C5 RID: 453
	[Serializable]
	public class CaptureAnimation
	{
		// Token: 0x04000D53 RID: 3411
		public Animation[] animators;

		// Token: 0x04000D54 RID: 3412
		public string neutralCapturedAnimation;

		// Token: 0x04000D55 RID: 3413
		public string blueCapturedAnimation;

		// Token: 0x04000D56 RID: 3414
		public string redCapturedAnimation;
	}
}

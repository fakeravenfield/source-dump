using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000181 RID: 385
public abstract class GameModeBase : MonoBehaviour
{
	// Token: 0x060009FE RID: 2558 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void OnSurrender()
	{
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00008ADE File Offset: 0x00006CDE
	protected virtual void Awake()
	{
		GameModeBase.instance = this;
		this.startTime = Time.time;
		this.canvas = base.GetComponentInChildren<Canvas>();
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00008AFD File Offset: 0x00006CFD
	public virtual void StartGame()
	{
		this.SetupDefaultLevelLayout();
		if (this.canConfigureFlags && GameManager.GameParameters().configFlags)
		{
			this.spawnLayout = GameModeBase.SpawnLayout.UserDefined;
			return;
		}
		if (!GameManager.IsSpectating())
		{
			this.OpenLoadoutWhileDeadAfterDelay(1f);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00008B36 File Offset: 0x00006D36
	public void SetupDefaultLevelLayout()
	{
		this.ApplyLevelLayout(this.FindLevelLayout(this.gameModeType));
		this.SetupOwnersDefault(true);
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0006D488 File Offset: 0x0006B688
	private void ApplyLevelLayout(GameModeLevelLayout layout)
	{
		try
		{
			if (layout != null)
			{
				this.spawnLayout = GameModeBase.SpawnLayout.GameModeOverride;
				SpawnPoint[] array;
				if (!layout.useDefaultOwners)
				{
					array = ActorManager.instance.spawnPoints;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].defaultOwner = SpawnPoint.Team.Neutral;
					}
				}
				array = layout.blueSpawns;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].defaultOwner = SpawnPoint.Team.Blue;
				}
				array = layout.redSpawns;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].defaultOwner = SpawnPoint.Team.Red;
				}
				array = layout.ghostSpawns;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetGhost(true);
				}
				array = layout.deactivatedSpawns;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].gameObject.SetActive(false);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00008B51 File Offset: 0x00006D51
	protected void OpenLoadoutWhileDeadAfterDelay(float delay)
	{
		base.Invoke("OpenLoadoutWhileDead", delay);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00008B5F File Offset: 0x00006D5F
	protected void OpenLoadoutWhileDead()
	{
		if (FpsActorController.instance.actor.dead)
		{
			LoadoutUi.Show(false);
		}
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00008B78 File Offset: 0x00006D78
	public virtual void SetupOrders()
	{
		OrderManager.SetupDefaultOrders(true, true, true, true);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0006D568 File Offset: 0x0006B768
	public GameModeLevelLayout FindLevelLayout(GameModeType gameMode)
	{
		GameModeLevelLayout[] array = UnityEngine.Object.FindObjectsOfType<GameModeLevelLayout>();
		GameModeLevelLayout result = null;
		foreach (GameModeLevelLayout gameModeLevelLayout in array)
		{
			if (gameModeLevelLayout.isBenchmark)
			{
				if (Benchmark.isRunning)
				{
					return gameModeLevelLayout;
				}
			}
			else if (gameModeLevelLayout.gameMode == gameMode)
			{
				result = gameModeLevelLayout;
			}
		}
		return result;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0006D5AC File Offset: 0x0006B7AC
	protected void SetupOwnersDefault(bool allowConfiguration = true)
	{
		if (allowConfiguration && GameManager.GameParameters().configFlags)
		{
			GameManager.ShowConfigFlagsScreen();
			return;
		}
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			int num = (int)spawnPoint.defaultOwner;
			if (GameManager.GameParameters().configFlags && num == -1)
			{
				num = (GameManager.GameParameters().reverseMode ? 0 : 1);
			}
			if (GameManager.GameParameters().reverseMode)
			{
				if (num == 0)
				{
					num = 1;
				}
				else if (num == 1)
				{
					num = 0;
				}
			}
			spawnPoint.SetOwner(num, true);
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0006D634 File Offset: 0x0006B834
	protected void SetupOwnersAttackDefend(int attackingTeam, int defendingTeam, int defenderBases)
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint == GameModeInfo.instance.attackerBase)
			{
				spawnPoint.SetOwner(attackingTeam, true);
			}
			else if (spawnPoint == GameModeInfo.instance.defenderMainBase)
			{
				spawnPoint.SetOwner(defendingTeam, true);
			}
			else if (spawnPoint == GameModeInfo.instance.defenderBase2 && defenderBases > 1)
			{
				spawnPoint.SetOwner(defendingTeam, true);
			}
			else if (spawnPoint == GameModeInfo.instance.defenderBase3 && defenderBases > 2)
			{
				spawnPoint.SetOwner(defendingTeam, true);
			}
			else
			{
				spawnPoint.SetOwner(-1, true);
			}
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
	{
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0006D6E4 File Offset: 0x0006B8E4
	public virtual void CapturePointChangedOwner(CapturePoint capturePoint, int oldOwner, bool initialOwner)
	{
		if (initialOwner)
		{
			return;
		}
		int owner = capturePoint.owner;
		if (owner >= 0 && owner <= 1)
		{
			OverlayUi.ShowOverlayText(GameManager.instance.GetRichTextColorTeamName(owner) + " CAPTURED " + capturePoint.shortName, 3.5f);
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0006D72C File Offset: 0x0006B92C
	public List<Squad> CreateAiSquads(List<Actor> actors, int squadSize, SpawnPoint spawnPoint)
	{
		List<Squad> list = new List<Squad>();
		List<ActorController> list2 = new List<ActorController>();
		foreach (Actor actor in actors)
		{
			if (actor.aiControlled)
			{
				list2.Add(actor.controller);
				if (list2.Count >= squadSize)
				{
					Squad squad = new Squad(list2, spawnPoint, null, null, 0f);
					squad.SetRandomFormation();
					list.Add(squad);
					list2 = new List<ActorController>();
				}
			}
		}
		if (list2.Count > 0)
		{
			Squad squad2 = new Squad(list2, spawnPoint, null, null, 0f);
			squad2.SetRandomFormation();
			list.Add(squad2);
		}
		return list;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00008B83 File Offset: 0x00006D83
	public virtual void PlayerAcceptedLoadoutFirstTime()
	{
		this.StartRecurringSpawnWave();
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00008B8B File Offset: 0x00006D8B
	public float ElapsedTime()
	{
		return Time.time - this.startTime;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0006D7EC File Offset: 0x0006B9EC
	public virtual int TimeToPlayerRespawn()
	{
		float num = FpsActorController.instance.actor.deathTimestamp + 6f;
		float num2 = Mathf.Max((float)GameManager.GameParameters().respawnTime, 1f);
		float num3 = Time.time + this.respawnWaveAction.Remaining();
		int num4 = 0;
		while (num4 < 10 && num3 <= num)
		{
			num3 += num2;
			num4++;
		}
		return Mathf.CeilToInt(num3 - Time.time);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00008B99 File Offset: 0x00006D99
	public virtual bool ShowRemainingTimeToPlayerSpawn()
	{
		return !this.respawnWaveAction.TrueDone();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x00008BA9 File Offset: 0x00006DA9
	protected void StartRecurringSpawnWave()
	{
		base.StartCoroutine(this.SpawnWaveRoutine());
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00008BB8 File Offset: 0x00006DB8
	public void ForceSpawnDeadActors()
	{
		this.SpawnDeadActorsOfTeam(0, true);
		this.SpawnDeadActorsOfTeam(1, true);
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00008BCA File Offset: 0x00006DCA
	public IEnumerator SpawnWaveRoutine()
	{
		float spawnTime = Mathf.Clamp((float)GameManager.GameParameters().respawnTime, 1f, 120f);
		this.respawnWaveAction = new TimedAction(spawnTime, false);
		for (;;)
		{
			yield return new WaitForSeconds(spawnTime);
			this.OnSpawnWave();
			this.respawnWaveAction.Start();
		}
		yield break;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00008BD9 File Offset: 0x00006DD9
	public virtual void OnSpawnWave()
	{
		this.SpawnDeadActorsOfTeam(0, false);
		this.SpawnDeadActorsOfTeam(1, false);
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0006D858 File Offset: 0x0006BA58
	protected void SpawnDeadActorsOfTeam(int team, bool ignoreDeadTime = true)
	{
		List<Actor> list = new List<Actor>();
		foreach (Actor actor in ActorManager.instance.actors)
		{
			if (actor.team == team && actor.IsReadyToSpawn() && (ignoreDeadTime || actor.deathTimestamp + 6f < Time.time))
			{
				list.Add(actor);
			}
		}
		base.StartCoroutine(this.SpawnActorListDeferred(list, team));
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00008BEB File Offset: 0x00006DEB
	public virtual int ModifyOrderPriority(Order order, int currentPriority)
	{
		return currentPriority;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00008BEE File Offset: 0x00006DEE
	public bool IsDeferredSpawnRunning(int team)
	{
		return this.teamDeferredSpawn[team].IsRunning();
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00008C01 File Offset: 0x00006E01
	public bool IsAnyDeferredSpawnRunning()
	{
		return this.IsDeferredSpawnRunning(0) || this.IsDeferredSpawnRunning(1);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00008C15 File Offset: 0x00006E15
	public IEnumerator SpawnActorListDeferred(List<Actor> actorsToSpawn, int team)
	{
		if (!ActorManager.TeamHasAnySpawnPoint(team))
		{
			yield break;
		}
		foreach (Actor actor in actorsToSpawn)
		{
			if (!actor.aiControlled)
			{
				actorsToSpawn.Remove(actor);
				SpawnPoint spawnPoint = actor.controller.SelectedSpawnPoint();
				if (spawnPoint != null)
				{
					actor.SpawnAt(spawnPoint.GetSpawnPosition(), Quaternion.identity, null);
					this.OnSquadSpawned(FpsActorController.instance.playerSquad);
					break;
				}
				break;
			}
		}
		int num = Mathf.Max(0, this.maxActiveSoldiersAllowed[team] - ActorManager.AliveActorsOnTeam(team).Count);
		if (num < actorsToSpawn.Count)
		{
			actorsToSpawn.RemoveRange(num, actorsToSpawn.Count - num);
		}
		if (actorsToSpawn.Count < Mathf.Min(new int[]
		{
			2,
			ActorManager.instance.GetNumberOfBotsInTeam(team),
			this.maxActiveSoldiersAllowed[team]
		}))
		{
			yield break;
		}
		for (int i = 0; i < actorsToSpawn.Count; i++)
		{
			actorsToSpawn[i].isScheduledToSpawn = true;
		}
		this.teamDeferredSpawn[team].Schedule();
		if (Time.frameCount % 2 == team)
		{
			yield return 0;
		}
		while (actorsToSpawn.Count > 0)
		{
			Order highestPriorityOrder = OrderManager.GetHighestPriorityOrder(team);
			if (highestPriorityOrder == null)
			{
				break;
			}
			SpawnPoint spawnPoint2 = highestPriorityOrder.source;
			Vehicle vehicle = null;
			WeaponManager.WeaponEntry.Distance distance;
			if (highestPriorityOrder.type == Order.OrderType.Roam)
			{
				vehicle = highestPriorityOrder.source.GetAvailableRoamingVehicle();
				distance = ((UnityEngine.Random.Range(0f, 1f) < 0.6f) ? WeaponManager.WeaponEntry.Distance.Mid : WeaponManager.WeaponEntry.Distance.Far);
			}
			else if (highestPriorityOrder.type == Order.OrderType.Attack)
			{
				SpawnPoint spawnPoint3;
				vehicle = this.GetAvailableVehicleForAttack(team, highestPriorityOrder, out spawnPoint3);
				if (vehicle != null)
				{
					spawnPoint2 = spawnPoint3;
				}
				float num2 = Vector3.Distance(highestPriorityOrder.source.transform.position, highestPriorityOrder.target.transform.position) / 2f;
				if (CoverManager.instance.IsInCqcCell(highestPriorityOrder.target.transform.position))
				{
					CapturePoint capturePoint = highestPriorityOrder.target as CapturePoint;
					if (capturePoint != null && capturePoint.isContested)
					{
						num2 *= 0.6f;
					}
				}
				if (num2 < 120f)
				{
					distance = WeaponManager.WeaponEntry.Distance.Short;
				}
				else if (num2 < 500f)
				{
					distance = WeaponManager.WeaponEntry.Distance.Mid;
				}
				else
				{
					distance = WeaponManager.WeaponEntry.Distance.Far;
				}
			}
			else if (CoverManager.instance.IsInCqcCell(highestPriorityOrder.source.transform.position) && UnityEngine.Random.Range(0f, 1f) < 0.25f)
			{
				distance = WeaponManager.WeaponEntry.Distance.Short;
			}
			else
			{
				distance = ((UnityEngine.Random.Range(0f, 1f) < 0.6f) ? WeaponManager.WeaponEntry.Distance.Mid : WeaponManager.WeaponEntry.Distance.Far);
			}
			int num3 = UnityEngine.Random.Range(3, 6);
			if (vehicle != null)
			{
				num3 = vehicle.seats.Count;
			}
			if (highestPriorityOrder.type == Order.OrderType.Repair)
			{
				num3 = 1;
			}
			num3 = Mathf.Min(num3, actorsToSpawn.Count);
			List<ActorController> list = new List<ActorController>();
			for (int j = 0; j < num3; j++)
			{
				Actor actor2 = actorsToSpawn[0];
				actorsToSpawn.RemoveAt(0);
				AiActorController aiActorController = (AiActorController)actor2.controller;
				if (highestPriorityOrder.type == Order.OrderType.Repair)
				{
					aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.Repair;
				}
				else if (highestPriorityOrder.type == Order.OrderType.Defend)
				{
					CapturePoint capturePoint2 = highestPriorityOrder.target as CapturePoint;
					if (capturePoint2 != null && capturePoint2.isUnderAttackByVehicles && UnityEngine.Random.Range(0f, 1f) > 0.5f)
					{
						aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.AntiArmor;
					}
					else
					{
						aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.Normal;
					}
				}
				else if (UnityEngine.Random.Range(0f, 1f) < 0.2f)
				{
					aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.SmokeScreen;
				}
				else
				{
					aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.Normal;
				}
				aiActorController.loadoutStrategy.distance = distance;
				if (aiActorController.loadoutStrategy.distance == WeaponManager.WeaponEntry.Distance.Short && UnityEngine.Random.Range(0f, 1f) < 0.3f)
				{
					aiActorController.loadoutStrategy.distance = WeaponManager.WeaponEntry.Distance.Mid;
				}
				if (aiActorController.loadoutStrategy.distance == WeaponManager.WeaponEntry.Distance.Mid && UnityEngine.Random.Range(0f, 1f) < 0.4f)
				{
					aiActorController.loadoutStrategy.distance = WeaponManager.WeaponEntry.Distance.Far;
				}
				else if (aiActorController.loadoutStrategy.distance == WeaponManager.WeaponEntry.Distance.Far && UnityEngine.Random.Range(0f, 1f) < 0.4f)
				{
					aiActorController.loadoutStrategy.distance = WeaponManager.WeaponEntry.Distance.Mid;
				}
				actor2.SpawnAt(spawnPoint2.GetSpawnPosition(), Quaternion.identity, null);
				list.Add(actor2.controller);
			}
			Squad squad = new Squad(list, highestPriorityOrder.source, highestPriorityOrder, vehicle, 0.3f);
			squad.SetRandomFormation();
			this.OnSquadSpawned(squad);
			yield return 0;
			yield return 0;
		}
		this.teamDeferredSpawn[team].Complete();
		yield break;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0000296E File Offset: 0x00000B6E
	public virtual void OnSquadSpawned(Squad squad)
	{
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00008C32 File Offset: 0x00006E32
	public void SpawnBotActor(Actor actor, Vector3 position, AiActorController.LoadoutPickStrategy loadoutStrategy)
	{
		((AiActorController)actor.controller).loadoutStrategy = loadoutStrategy;
		actor.SpawnAt(position, Quaternion.identity, null);
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0006D8EC File Offset: 0x0006BAEC
	protected Vehicle GetAvailableVehicleForAttack(int team, Order order, out SpawnPoint squadSpawnPoint)
	{
		squadSpawnPoint = order.source;
		Vehicle vehicle = order.source.GetAvailableVehicle(order.requiredVehicleFilter, -1);
		byte b = 0;
		if (vehicle == null)
		{
			foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
			{
				if (spawnPoint.owner == team && spawnPoint != order.source)
				{
					VehicleFilter vehicleFilter = order.requiredVehicleFilter.Clone();
					vehicleFilter.watercraft = (vehicleFilter.watercraft && SpawnPointNeighborManager.HasWaterConnection(spawnPoint, order.target));
					vehicleFilter.landcraft = (vehicleFilter.landcraft && SpawnPointNeighborManager.HasLandConnection(spawnPoint, order.target));
					vehicleFilter.allowOnlyFromFrontlineSpawnUsage = false;
					byte b2;
					Vehicle availableVehicle = spawnPoint.GetAvailableVehicle(vehicleFilter, out b2, -1);
					if (availableVehicle != null && b2 >= b)
					{
						vehicle = availableVehicle;
						squadSpawnPoint = spawnPoint;
					}
				}
			}
		}
		return vehicle;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00008C52 File Offset: 0x00006E52
	public virtual WeaponManager.LoadoutSet GetLoadout(Actor actor)
	{
		return actor.controller.GetLoadout();
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00008C5F File Offset: 0x00006E5F
	public virtual void Win(int winningTeam)
	{
		VictoryUi.EndGame(winningTeam, this.canContinuePlayingAfterGameEnd);
		BattleResult.SetWinner(winningTeam);
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00008C73 File Offset: 0x00006E73
	public virtual string GetName()
	{
		return base.GetType().ToString();
	}

	// Token: 0x04000AF0 RID: 2800
	public static GameModeBase instance;

	// Token: 0x04000AF1 RID: 2801
	private const int MINIMUM_ACTORS_IN_WAVE = 2;

	// Token: 0x04000AF2 RID: 2802
	private const float SPAWN_WAVE_REPEAT_TIME = 4f;

	// Token: 0x04000AF3 RID: 2803
	private const float SMOKE_SCREEN_LOADOUT_CHANCE = 0.2f;

	// Token: 0x04000AF4 RID: 2804
	[NonSerialized]
	public Canvas canvas;

	// Token: 0x04000AF5 RID: 2805
	[NonSerialized]
	public bool allowDefaultRespawn = true;

	// Token: 0x04000AF6 RID: 2806
	public GameModeType gameModeType;

	// Token: 0x04000AF7 RID: 2807
	public string gameModeName = "NEW GAME MODE";

	// Token: 0x04000AF8 RID: 2808
	[NonSerialized]
	public GameModeBase.SpawnLayout spawnLayout;

	// Token: 0x04000AF9 RID: 2809
	public const float MIN_DEAD_TIME = 6f;

	// Token: 0x04000AFA RID: 2810
	public const float AI_MAX_FIRST_SPAWN_TIME = 7f;

	// Token: 0x04000AFB RID: 2811
	private float startTime;

	// Token: 0x04000AFC RID: 2812
	private TimedAction respawnWaveAction = new TimedAction(1f, false);

	// Token: 0x04000AFD RID: 2813
	[NonSerialized]
	public int[] maxActiveSoldiersAllowed = new int[]
	{
		int.MaxValue,
		int.MaxValue
	};

	// Token: 0x04000AFE RID: 2814
	public bool canConfigureFlags = true;

	// Token: 0x04000AFF RID: 2815
	public bool canContinuePlayingAfterGameEnd = true;

	// Token: 0x04000B00 RID: 2816
	private GameModeBase.DeferredTeamSpawn[] teamDeferredSpawn = new GameModeBase.DeferredTeamSpawn[2];

	// Token: 0x02000182 RID: 386
	public enum SpawnLayout
	{
		// Token: 0x04000B02 RID: 2818
		Default,
		// Token: 0x04000B03 RID: 2819
		UserDefined,
		// Token: 0x04000B04 RID: 2820
		GameModeOverride
	}

	// Token: 0x02000183 RID: 387
	private struct DeferredTeamSpawn
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00008C80 File Offset: 0x00006E80
		public void Schedule()
		{
			this.pending += 1;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00008C91 File Offset: 0x00006E91
		public void Complete()
		{
			this.pending -= 1;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00008CA2 File Offset: 0x00006EA2
		public bool IsRunning()
		{
			return this.pending > 0;
		}

		// Token: 0x04000B05 RID: 2821
		public byte pending;
	}
}

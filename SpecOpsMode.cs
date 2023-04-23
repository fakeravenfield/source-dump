using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Ravenfield.SpecOps;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class SpecOpsMode : GameModeBase
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x00008F1D File Offset: 0x0000711D
	protected override void Awake()
	{
		base.Awake();
		this.seeker = base.GetComponent<Seeker>();
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00008F31 File Offset: 0x00007131
	public bool IntroIsDone()
	{
		return this.gameIsRunning && this.introAction.TrueDone();
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
	private void SetDifficultyFlags()
	{
		int dropdown = Options.GetDropdown(OptionDropdown.Id.Difficulty);
		int gameLength = GameManager.GameParameters().gameLength;
		this.nInfantryPatrols = Mathf.Max(1, gameLength);
		this.allowCarPatrol = (gameLength > 0);
		this.allowBoatPatrol = (dropdown >= 1 && gameLength >= 2);
		if (this.tinyMap)
		{
			this.patrolUseApc = (dropdown >= 2 && UnityEngine.Random.Range(0f, 1f) < 0.7f);
		}
		else
		{
			this.patrolUseApc = (dropdown >= 1 && UnityEngine.Random.Range(0f, 1f) < 0.5f);
		}
		if (dropdown >= 2)
		{
			this.patrolUseApc = true;
		}
		this.patrolUseAttackBoat = (dropdown >= 2 && UnityEngine.Random.Range(0f, 1f) < 0.7f);
		this.allowHelicopterPatrol = (dropdown >= 1 && gameLength >= 1);
		int num = gameLength - 1;
		for (int i = 0; i < num; i++)
		{
			this.allowedPatrolObjectives.Add((PathfindingBox.Type)UnityEngine.Random.Range(0, 3));
		}
		this.allowHelicopterExfil = (gameLength > 0 && this.GetHelicopterLandingZones().Count > 0);
		this.nAttackers = GameManager.GameParameters().conquestBattalions[this.attackingTeam] + 1;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0006FC08 File Offset: 0x0006DE08
	public override void SetupOrders()
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			foreach (SpawnPoint spawnPoint2 in spawnPoint.outgoingNeighbors)
			{
				if (SpawnPointNeighborManager.HasLandConnection(spawnPoint, spawnPoint2))
				{
					OrderManager.AddMoveOrder(spawnPoint, spawnPoint2);
				}
			}
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0006FC88 File Offset: 0x0006DE88
	public override void StartGame()
	{
		this.FindMapType();
		this.SetDifficultyFlags();
		if (this.allowHelicopterExfil)
		{
			this.exfil = new ExfilHelicopter(this);
		}
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 0f, Color.black);
		if (GameManager.IsSpectating())
		{
			this.attackingTeam = 0;
			this.defendingTeam = 1;
		}
		else
		{
			this.attackingTeam = GameManager.PlayerTeam();
			this.defendingTeam = 1 - this.attackingTeam;
		}
		this.attackersHaveDefaultSkin = ActorManager.instance.hasDefaultSkin[this.attackingTeam];
		if (this.attackingTeam == 0 && this.attackersHaveDefaultSkin)
		{
			ActorSkin skin = GameManager.GameParameters().nightMode ? this.talonSkinNight : this.talonSkinDay;
			ActorManager.SetGlobalTeamSkin(this.attackingTeam, skin);
		}
		base.StartGame();
		this.dialog = new SpecOpsDialog(this);
		this.allowDefaultRespawn = false;
		if (!this.attackersHaveDefaultSkin)
		{
			this.dialog.UseCustomSkinPortraits();
		}
		this.CreateScenario();
		this.CreateAttackerTeam();
		ScoreboardUi.activeScoreboardType = ScoreboardUi.ActiveScoreboardUI.Objective;
		new SpecOpsPatrolGenerator(this);
		base.StartCoroutine(this.PlayIntroSequence());
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0006FD98 File Offset: 0x0006DF98
	private Dictionary<Vector3, SpawnPoint> GetHelicopterLandingZones()
	{
		Dictionary<Vector3, SpawnPoint> dictionary = new Dictionary<Vector3, SpawnPoint>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.HasAnyHelicopterLandingZone())
			{
				Vector3 helicopterLandingZone = spawnPoint.GetHelicopterLandingZone();
				if (!dictionary.ContainsKey(helicopterLandingZone))
				{
					dictionary.Add(helicopterLandingZone, spawnPoint);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0006FDEC File Offset: 0x0006DFEC
	private void SpawnExfiltration()
	{
		if (this.exfiltrationSpawned || this.exfil == null)
		{
			return;
		}
		this.exfiltrationSpawned = true;
		Dictionary<Vector3, SpawnPoint> helicopterLandingZones = this.GetHelicopterLandingZones();
		List<Vector3> list = helicopterLandingZones.Keys.ToList<Vector3>();
		list.Sort((Vector3 a, Vector3 b) => this.GetLandingZoneScore(b).CompareTo(this.GetLandingZoneScore(a)));
		SpawnPoint spawn = helicopterLandingZones[list[0]];
		this.exfil.StartExfiltration(list[0]);
		this.dialog.OnExfiltrationStart(spawn);
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0006FE60 File Offset: 0x0006E060
	private float GetLandingZoneScore(Vector3 l)
	{
		float magnitude = (FpsActorController.instance.actor.Position() - l).ToGround().magnitude;
		return this.GetOptimizedDistanceScore(magnitude, 100f, 300f);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00008F48 File Offset: 0x00007148
	private float GetOptimizedDistanceScore(float distance, float minDistance, float maxDistance)
	{
		return Mathf.Clamp01(Mathf.InverseLerp(minDistance / 2f, minDistance, distance)) - Mathf.Clamp01(Mathf.InverseLerp(maxDistance, maxDistance * 10f, distance));
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00008F71 File Offset: 0x00007171
	private IEnumerator PlayIntroSequence()
	{
		yield return new WaitForSeconds(0.5f);
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.2f, Color.black);
		yield break;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0006FEA4 File Offset: 0x0006E0A4
	private void FindMapType()
	{
		List<float> list = new List<float>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			foreach (SpawnPoint spawnPoint2 in spawnPoint.outgoingNeighbors)
			{
				list.Add((spawnPoint.transform.position - spawnPoint2.transform.position).ToGround().magnitude);
			}
		}
		try
		{
			list.Sort();
			float num = list[list.Count / 2];
			this.tinyMap = (num < 100f);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0006FF80 File Offset: 0x0006E180
	private void InitiaizeSpawnPoints()
	{
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			spawnPoint.SetOwner(this.defendingTeam, true);
			spawnPoint.SetGhost(true);
			CapturePoint capturePoint = spawnPoint as CapturePoint;
			if (capturePoint != null)
			{
				capturePoint.canCapture = false;
			}
			foreach (VehicleSpawner vehicleSpawner in spawnPoint.vehicleSpawners)
			{
				vehicleSpawner.respawnType = VehicleSpawner.RespawnType.Never;
				if (vehicleSpawner.typeToSpawn == VehicleSpawner.VehicleSpawnType.JeepMachineGun)
				{
					vehicleSpawner.typeToSpawn = VehicleSpawner.VehicleSpawnType.Jeep;
				}
				vehicleSpawner.enabled = vehicleSpawner.IsTransportType();
			}
		}
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00070044 File Offset: 0x0006E244
	private void CreateScenario()
	{
		this.InitiaizeSpawnPoints();
		List<SpawnPoint> availableSpawns = this.SpawnScenarios();
		this.FindAttackerSpawn(availableSpawns);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00070068 File Offset: 0x0006E268
	private List<SpawnPoint> SpawnScenarios()
	{
		List<SpecOpsScenario> list = new List<SpecOpsScenario>
		{
			new DestroyScenario(),
			new AssassinateScenario(),
			new ClearScenario(),
			new SabotageScenario()
		};
		this.activeScenarios = new List<SpecOpsScenario>();
		this.activePatrols = new List<SpecOpsPatrol>();
		this.activeObjectives = new List<SpecOpsObjective>();
		List<SpawnPoint> list2 = new List<SpawnPoint>(ActorManager.instance.spawnPoints);
		int num = GameManager.GameParameters().gameLength + 1;
		int num2 = list2.Count;
		if (this.tinyMap)
		{
			num2--;
		}
		num = Mathf.Min(num, num2);
		this.scenarioAtSpawn = new Dictionary<SpawnPoint, SpecOpsScenario>();
		IEnumerable<SpawnPoint> source = from s in list2
		where s.vehicleSpawners.Any((VehicleSpawner spawner) => spawner.GetPrefab() != null)
		select s;
		IEnumerable<SpawnPoint> source2 = from s in source
		where s.vehicleSpawners.Any((VehicleSpawner spawner) => spawner.GetPrefab() != null && DestroyScenario.IsHighValueType(spawner.typeToSpawn))
		select s;
		List<SpawnPoint> list3 = (source2.Count<SpawnPoint>() > 0) ? source2.ToList<SpawnPoint>() : source.ToList<SpawnPoint>();
		List<SpecOpsScenario> list4 = new List<SpecOpsScenario>();
		int num3 = 0;
		while (num3 < num && list.Count != 0 && list2.Count != 0)
		{
			SpecOpsScenario specOpsScenario = this.PickRandomEntry<SpecOpsScenario>(list);
			list.Remove(specOpsScenario);
			if (specOpsScenario is DestroyScenario)
			{
				if (list3.Count<SpawnPoint>() == 0)
				{
					num++;
				}
				else
				{
					SpawnPoint spawnPoint = this.PickRandomEntry<SpawnPoint>(list3);
					list2.Remove(spawnPoint);
					if (!this.ActivateScenario(specOpsScenario, spawnPoint))
					{
						Debug.LogFormat("Failed to initialize scenario {0}, replace it with a new scenario", new object[]
						{
							specOpsScenario
						});
						num++;
					}
				}
			}
			else
			{
				list4.Add(specOpsScenario);
			}
			num3++;
		}
		foreach (SpecOpsScenario scenario in list4)
		{
			SpawnPoint spawnPoint2 = this.PickRandomEntry<SpawnPoint>(list2);
			list2.Remove(spawnPoint2);
			this.ActivateScenario(scenario, spawnPoint2);
		}
		ObjectiveUi.SortEntries();
		return list2;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00070274 File Offset: 0x0006E474
	public void FireFlare(Actor actor, SpawnPoint spawn)
	{
		if (this.FlareIsOnCooldown())
		{
			return;
		}
		Vector3 position = actor.CenterPosition() + new Vector3(0f, 0.3f, 0f);
		this.activeFlare = UnityEngine.Object.Instantiate<GameObject>(this.flarePrefab, position, Quaternion.identity).GetComponentInChildren<Rigidbody>().gameObject;
		GameManager.PlayReflectionSound(false, false, Weapon.ReflectionSound.Handgun, 1f, position);
		this.flareCooldown.Start();
		if (spawn != null)
		{
			base.StartCoroutine(this.AttractPatrol(position, spawn));
			if (this.allowHelicopterPatrol && !this.hasSpawnedHelicopterPatrol && spawn.HasAnyHelicopterLandingZone())
			{
				this.SpawnHelicopterPatrol(spawn);
			}
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00008F79 File Offset: 0x00007179
	private IEnumerator AttractPatrol(Vector3 position, SpawnPoint spawn)
	{
		yield return new WaitForSeconds(3f);
		List<SpecOpsPatrol> list = new List<SpecOpsPatrol>();
		SpecOpsPatrol specOpsPatrol = null;
		float num = 500f;
		foreach (SpecOpsPatrol specOpsPatrol2 in this.activePatrols)
		{
			if (specOpsPatrol2.squad != null && specOpsPatrol2.squad.members.Count > 0)
			{
				float magnitude = (position - specOpsPatrol2.squad.Leader().actor.Position()).ToGround().magnitude;
				if (magnitude < num)
				{
					num = magnitude;
					specOpsPatrol = specOpsPatrol2;
				}
				if (magnitude < 200f)
				{
					list.Add(specOpsPatrol2);
				}
			}
		}
		if (specOpsPatrol != null && !list.Contains(specOpsPatrol))
		{
			list.Add(specOpsPatrol);
		}
		foreach (SpecOpsPatrol specOpsPatrol3 in list)
		{
			this.EyesHighlightSquad(specOpsPatrol3.squad);
			specOpsPatrol3.InvestigateFlare(spawn);
		}
		if (this.attackingTeam == 0)
		{
			if (list.Count == 1)
			{
				this.dialog.OnPatrolInvestigating(list[0]);
			}
			else if (list.Count > 1)
			{
				this.dialog.OnMultiplePatrolsInvestigating();
			}
		}
		yield break;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0007031C File Offset: 0x0006E51C
	private void SpawnHelicopterPatrol(SpawnPoint spawn)
	{
		this.hasSpawnedHelicopterPatrol = true;
		GameObject gameObject = GameManager.instance.gameInfo.team[this.defendingTeam].vehiclePrefab[VehicleSpawner.VehicleSpawnType.TransportHelicopter];
		if (gameObject == null)
		{
			return;
		}
		Vector3 helicopterLandingZone = spawn.GetHelicopterLandingZone();
		Vector2 normalized = UnityEngine.Random.insideUnitCircle.normalized;
		Vector3 a = new Vector3(normalized.x, 0f, normalized.y);
		Vector3 vector = helicopterLandingZone + a * 1500f;
		float num = ExfilHelicopter.CalculateFlightAltitudeLimit(vector, helicopterLandingZone);
		vector.y = num + 80f;
		Quaternion rotation = Quaternion.LookRotation(-a);
		Vehicle component = UnityEngine.Object.Instantiate<GameObject>(gameObject, vector, rotation).GetComponent<Vehicle>();
		bool nightMode = GameManager.instance.gameModeParameters.nightMode;
		GameObject spotlightGO = null;
		if (nightMode)
		{
			Vector3 position = component.seats[0].transform.position;
			position.y -= 30f;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(position, Vector3.up), out raycastHit, 50f, 4096))
			{
				spotlightGO = UnityEngine.Object.Instantiate<GameObject>(this.spotlightPrefab, component.transform);
				spotlightGO.transform.localRotation = Quaternion.identity;
				spotlightGO.transform.position = raycastHit.point;
				Vector3 localPosition = spotlightGO.transform.localPosition;
				localPosition.x = 0f;
				spotlightGO.transform.localPosition = localPosition;
				this.searchlight = spotlightGO.GetComponentInChildren<Light>();
				this.searchlightDotFOV = Mathf.Cos(this.searchlight.spotAngle * 0.017453292f / 2f);
			}
		}
		float num2 = nightMode ? 0f : 100f;
		List<Actor> list = new List<Actor>();
		for (int i = 0; i < component.seats.Count; i++)
		{
			Actor actor = ActorManager.instance.CreateAIActor(this.defendingTeam);
			actor.SpawnAt(vector, rotation, null);
			actor.EnterSeat(component.seats[i], false);
			AiActorController aiActorController = actor.controller as AiActorController;
			aiActorController.ActivateSlowTargetDetection(0.5f);
			aiActorController.targetFlightHeight = 80f;
			aiActorController.modifier.canSprint = false;
			bool flag = i == 0;
			aiActorController.modifier.maxDetectionDistance = (flag ? num2 : -100000f);
			aiActorController.modifier.ignoreFovCheck = flag;
			list.Add(actor);
		}
		this.heliPilot = list[0];
		Squad helicopterSquad = new Squad(list, spawn, new Order(Order.OrderType.Roam, spawn, spawn, true), component, 0f);
		helicopterSquad.allowRequestNewOrders = false;
		helicopterSquad.LandAtPosition(helicopterLandingZone);
		(this.heliPilot.controller as AiActorController).onDetectedEnemy = delegate()
		{
			Order order = new Order(Order.OrderType.Attack, null, null, true);
			order.targetSquad = this.attackerSquad;
			helicopterSquad.AssignOrder(order);
			if (!this.heliPatrolHasLanded)
			{
				foreach (AiActorController aiActorController2 in helicopterSquad.aiMembers)
				{
					if (aiActorController2.actor.IsSeated() && aiActorController2.actor.seat.HasAnyMountedWeapons())
					{
						aiActorController2.DeactivateSlowTargetDetection();
					}
				}
			}
		};
		component.onDestroyed = delegate()
		{
			if (!this.heliPatrolHasLanded && helicopterSquad.members.Count > 0)
			{
				foreach (AiActorController aiActorController2 in helicopterSquad.aiMembers)
				{
					aiActorController2.modifier.maxDetectionDistance = 500f;
					aiActorController2.modifier.ignoreFovCheck = false;
				}
				helicopterSquad.RequestNewOrder();
				helicopterSquad.allowRequestNewOrders = true;
			}
			if (spotlightGO != null)
			{
				spotlightGO.SetActive(false);
			}
		};
		helicopterSquad.onLandingCompleted = delegate()
		{
			this.StartCoroutine(this.OnHelicopterPatrolLanded(helicopterSquad, spawn));
		};
		base.StartCoroutine(this.NotifyHelicopter(component));
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00008F96 File Offset: 0x00007196
	private IEnumerator NotifyHelicopter(Vehicle vehicle)
	{
		yield return new WaitForSeconds(8f);
		this.dialog.OnIncomingHelicopter(vehicle);
		yield break;
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00008FAC File Offset: 0x000071AC
	private IEnumerator OnHelicopterPatrolLanded(Squad squad, SpawnPoint spawn)
	{
		bool flag = squad.order.type == Order.OrderType.Attack;
		this.heliPatrolHasLanded = true;
		List<ActorController> leavingMembers = (from a in squad.aiMembers
		where !a.actor.IsDriver()
		select a).ToList<ActorController>();
		Squad squad2 = squad.SplitSquad(leavingMembers);
		squad2.allowRequestNewOrders = true;
		squad2.AssignOrder(new Order(Order.OrderType.Defend, spawn, spawn, true));
		squad2.ExitVehicle();
		foreach (AiActorController aiActorController in squad2.aiMembers)
		{
			aiActorController.modifier.maxDetectionDistance = (flag ? 500f : 200f);
		}
		yield return new WaitForSeconds(5f);
		try
		{
			squad.TakeOff();
			AiActorController aiActorController2 = squad.aiMembers[0];
			aiActorController2.modifier.vehicleTopSpeedMultiplier = 0.3f;
			aiActorController2.targetFlightHeight = 40f;
			yield break;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			yield break;
		}
		yield break;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00008FC9 File Offset: 0x000071C9
	public bool FlareIsOnCooldown()
	{
		return !this.flareCooldown.TrueDone();
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0007067C File Offset: 0x0006E87C
	private void FindRandomFlagSpawn(List<SpawnPoint> availableSpawns)
	{
		SpawnPoint spawnPoint = availableSpawns[UnityEngine.Random.Range(0, availableSpawns.Count)];
		this.InitializeAttackerSpawn(spawnPoint.GetSpawnPosition());
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x000706A8 File Offset: 0x0006E8A8
	private void FindAttackerSpawn(List<SpawnPoint> availableSpawns)
	{
		if (this.tinyMap)
		{
			this.FindRandomFlagSpawn(availableSpawns);
			return;
		}
		List<Vector3> list = new List<Vector3>();
		foreach (SpawnPoint spawnPoint2 in availableSpawns)
		{
			for (int i = 0; i < 3; i++)
			{
				list.Add(spawnPoint2.RandomPatrolPosition());
			}
			if (spawnPoint2.HasAnyHelicopterLandingZone())
			{
				list.Add(spawnPoint2.GetHelicopterLandingZone());
			}
		}
		foreach (SpecOpsScenario specOpsScenario in this.activeScenarios)
		{
			if (specOpsScenario.spawn.HasAnyHelicopterLandingZone())
			{
				list.Add(specOpsScenario.spawn.GetHelicopterLandingZone());
			}
		}
		for (int j = 0; j < 8; j++)
		{
			for (int k = 0; k < list.Count; k++)
			{
				Vector3 vector = list[k];
				Vector3 vector2 = vector;
				SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
				for (int l = 0; l < spawnPoints.Length; l++)
				{
					SpawnPoint spawnPoint = spawnPoints[l];
					bool flag = this.activeScenarios.Find((SpecOpsScenario s) => s.spawn == spawnPoint) != null;
					if (!flag)
					{
					}
					float protectRange = spawnPoint.protectRange;
					float num = flag ? 1f : 0.5f;
					Vector3 vector3 = (vector - spawnPoint.transform.position).ToGround();
					float magnitude = vector3.magnitude;
					float num2 = (200f - magnitude) * UnityEngine.Random.Range(0.3f, 0.7f) * num;
					if (num2 > 0f)
					{
						vector2 += vector3.normalized * num2;
					}
				}
				vector2 = (Vector3)PathfindingManager.instance.FindClosestNode(vector2, PathfindingBox.Type.Infantry, -13).position;
				list[k] = vector2;
			}
		}
		list.RemoveAll(delegate(Vector3 position)
		{
			RaycastHit raycastHit2;
			return !Physics.Raycast(new Ray(position + new Vector3(0f, 5f, 0f), Vector3.down), out raycastHit2, 20f, 2232321) || WaterLevel.IsInWater(raycastHit2.point);
		});
		IEnumerable<GraphNode> enumerable = from s in list
		select PathfindingManager.instance.FindClosestNode(s, PathfindingBox.Type.Infantry, -13);
		float num3 = float.MinValue;
		GraphNode graphNode = null;
		foreach (GraphNode graphNode2 in enumerable)
		{
			float num4 = 999999f;
			Vector3 b = (Vector3)graphNode2.position;
			foreach (SpecOpsScenario specOpsScenario2 in this.activeScenarios)
			{
				float magnitude2 = (specOpsScenario2.spawn.transform.position - b).ToGround().magnitude;
				if (magnitude2 < num4)
				{
					num4 = magnitude2;
				}
			}
			float optimizedDistanceScore = this.GetOptimizedDistanceScore(num4, 200f, 300f);
			if (optimizedDistanceScore > num3)
			{
				num3 = optimizedDistanceScore;
				graphNode = graphNode2;
			}
		}
		if (graphNode == null)
		{
			this.FindRandomFlagSpawn(availableSpawns);
			return;
		}
		Vector3 vector4 = (Vector3)graphNode.position;
		Ray ray = new Ray(vector4, Vector3.down);
		ray.origin += new Vector3(0f, 2.5f, 0f);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 5f, 2232321))
		{
			vector4 = raycastHit.point;
		}
		this.InitializeAttackerSpawn(vector4);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00070A7C File Offset: 0x0006EC7C
	private void InitializeAttackerSpawn(Vector3 spawnPosition)
	{
		SpecOpsScenario specOpsScenario = (from a in this.activeScenarios
		orderby Vector3.Distance(a.spawn.transform.position, spawnPosition)
		select a).First<SpecOpsScenario>();
		if (specOpsScenario != null)
		{
			Vector3 v = specOpsScenario.spawn.transform.position - spawnPosition;
			this.attackerSpawnPosition = spawnPosition;
			this.attackerSpawnRotation = Quaternion.LookRotation(v.ToGround());
			this.canPlayIntroCutscene = false;
			float num = 8.75f;
			for (int i = 0; i < 16; i++)
			{
				int num2 = (i % 2 == 0) ? -1 : 1;
				float y = this.attackerSpawnRotation.eulerAngles.y + num * (float)num2 * (float)i;
				Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
				bool flag = false;
				foreach (SpecOpsMode.BoxCheckValues boxCheckValues in SpecOpsMode.INTRO_BOX_CHECKS)
				{
					if (Physics.CheckBox(this.attackerSpawnPosition + quaternion * boxCheckValues.offset, boxCheckValues.halfScale, quaternion, 2232321))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.canPlayIntroCutscene = true;
					this.attackerSpawnRotation = quaternion;
					break;
				}
			}
			if (this.nAttackers != 4)
			{
				this.canPlayIntroCutscene = false;
			}
			RawImage rawImage = MinimapUi.AddGenericBlipWorld(this.insertionBlip, spawnPosition, new Vector2(32f, 32f));
			RawImage rawImage2 = MinimapUi.AddGenericBlipWorld(this.insertionDirectionBlip, spawnPosition, new Vector2(32f, 64f));
			rawImage2.rectTransform.pivot = new Vector2(0.5f, -0.2f);
			rawImage2.rectTransform.localEulerAngles = new Vector3(0f, 0f, MinimapCamera.instance.camera.transform.eulerAngles.y - this.attackerSpawnRotation.eulerAngles.y);
			Color color = ColorScheme.TeamColor(this.attackingTeam);
			if (GameManager.GameParameters().nightMode)
			{
				color = Color.Lerp(color, Color.white, 0.5f);
			}
			else
			{
				color = Color.Lerp(color, Color.black, 0.3f);
			}
			rawImage.color = color;
			rawImage2.color = color;
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00070CC4 File Offset: 0x0006EEC4
	private void OnDrawGizmos()
	{
		foreach (SpecOpsMode.BoxCheckValues boxCheckValues in SpecOpsMode.INTRO_BOX_CHECKS)
		{
			Vector3 vector = this.attackerSpawnPosition + this.attackerSpawnRotation * boxCheckValues.offset;
			Debug.DrawRay(vector - Vector3.up * boxCheckValues.halfScale.y, this.attackerSpawnRotation * Vector3.forward * boxCheckValues.halfScale.z, Color.red, 100f);
			Gizmos.matrix = Matrix4x4.TRS(vector, this.attackerSpawnRotation, boxCheckValues.halfScale);
			Gizmos.color = (Physics.CheckBox(vector, boxCheckValues.halfScale, this.attackerSpawnRotation, 2232321) ? Color.red : Color.green);
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one * 2f);
		}
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00070DB4 File Offset: 0x0006EFB4
	public void RegisterPatrol(PathfindingBox.Type type, List<Vector3> path)
	{
		if (type == PathfindingBox.Type.Infantry)
		{
			for (int i = 0; i < this.nInfantryPatrols; i++)
			{
				SpecOpsPatrol patrol = new SpecOpsPatrol(type, path, this);
				base.StartCoroutine(this.InitializePatrol(patrol));
			}
			return;
		}
		if (type == PathfindingBox.Type.Car && this.allowCarPatrol)
		{
			SpecOpsPatrol patrol2 = new SpecOpsPatrol(type, path, this);
			base.StartCoroutine(this.InitializePatrol(patrol2));
			return;
		}
		if (type == PathfindingBox.Type.Boat && this.allowBoatPatrol)
		{
			SpecOpsPatrol patrol3 = new SpecOpsPatrol(type, path, this);
			base.StartCoroutine(this.InitializePatrol(patrol3));
		}
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00008FD9 File Offset: 0x000071D9
	private IEnumerator InitializePatrol(SpecOpsPatrol patrol)
	{
		yield return 0;
		patrol.Spawn();
		this.activePatrols.Add(patrol);
		if (this.allowedPatrolObjectives.Remove(patrol.type))
		{
			this.InitializePatrolObjective(patrol);
		}
		yield break;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x00008FEF File Offset: 0x000071EF
	private void InitializePatrolObjective(SpecOpsPatrol patrol)
	{
		patrol.InitializeObjective();
		this.activeObjectives.Add(patrol);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00070E34 File Offset: 0x0006F034
	private bool ActivateScenario(SpecOpsScenario scenario, SpawnPoint spawn)
	{
		try
		{
			Debug.LogFormat("Initializing {0} at {1}", new object[]
			{
				scenario,
				spawn.shortName
			});
			scenario.Initialize(this, spawn);
			spawn.SetGhost(false);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			return false;
		}
		this.activeScenarios.Add(scenario);
		this.scenarioAtSpawn.Add(spawn, scenario);
		this.activeObjectives.Add(scenario);
		return true;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00070EB0 File Offset: 0x0006F0B0
	private T PickRandomEntry<T>(List<T> collection)
	{
		int index = UnityEngine.Random.Range(0, collection.Count);
		return collection[index];
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x00070ED4 File Offset: 0x0006F0D4
	public void OnObjectiveCompleted(SpecOpsObjective objective)
	{
		if (!this.gameIsRunning)
		{
			return;
		}
		if (this.activeObjectives.All((SpecOpsObjective o) => o.objective == null || o.objective.isCompleted || o.objective.isFailed))
		{
			this.StartEndgame();
			return;
		}
		SpecOpsScenario specOpsScenario = objective as SpecOpsScenario;
		if (specOpsScenario != null)
		{
			this.dialog.OnScenarioCompleted(specOpsScenario);
		}
		SpecOpsPatrol specOpsPatrol = objective as SpecOpsPatrol;
		if (specOpsPatrol != null)
		{
			this.dialog.OnPatrolNeutralized(specOpsPatrol);
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00009003 File Offset: 0x00007203
	private void StartEndgame()
	{
		if (this.StealthVictoryAllowed())
		{
			base.StartCoroutine(this.StealthVictorySequence());
			return;
		}
		this.SpawnExfiltration();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00070F4C File Offset: 0x0006F14C
	private bool StealthVictoryAllowed()
	{
		if (this.exfil == null)
		{
			return true;
		}
		if (this.attackerSquad.members.Count != this.nAttackers)
		{
			return false;
		}
		if (this.tinyMap)
		{
			return true;
		}
		foreach (SpecOpsScenario specOpsScenario in this.activeScenarios)
		{
			if (specOpsScenario.squad != null && specOpsScenario.squad.members.Count > 0 && specOpsScenario.squad.isAlert)
			{
				return false;
			}
		}
		foreach (SpecOpsPatrol specOpsPatrol in this.activePatrols)
		{
			if (specOpsPatrol.squad != null && specOpsPatrol.squad.members.Count > 0 && specOpsPatrol.squad.isAlert)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00009021 File Offset: 0x00007221
	public bool AllAttackersDead()
	{
		return this.attackerActors.All((Actor a) => a.dead);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0000904D File Offset: 0x0000724D
	public void OnExfiltrationLiftOff()
	{
		base.StartCoroutine(this.ExfiltrationVictorySequence());
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0000905C File Offset: 0x0000725C
	private IEnumerator ExfiltrationVictorySequence()
	{
		foreach (ActorController actorController in this.attackerSquad.members)
		{
			actorController.actor.isInvulnerable = true;
		}
		this.dialog.OnExfiltrationVictory();
		yield return new WaitForSeconds(4f);
		this.Win(this.attackingTeam);
		yield break;
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0000906B File Offset: 0x0000726B
	private IEnumerator StealthVictorySequence()
	{
		Actor[] array = this.attackerActors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isInvulnerable = true;
		}
		yield return new WaitForSeconds(1f);
		this.dialog.OnVictory();
		yield return new WaitForSeconds(11f);
		this.Win(this.attackingTeam);
		yield break;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0000907A File Offset: 0x0000727A
	private IEnumerator DefeatSequence()
	{
		this.dialog.OnPlayerDied();
		yield return new WaitForSeconds(3f);
		EffectUi.FadeOut(EffectUi.FadeType.EyeAndFullScreen, 2f, new Color(0f, 0f, 0f, 0.8f));
		yield return new WaitForSeconds(8f);
		this.Win(this.defendingTeam);
		yield break;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00071064 File Offset: 0x0006F264
	private void Update()
	{
		if (this.gameIsRunning)
		{
			if (!GameManager.IsSpectating() && ActorManager.instance.player.dead)
			{
				this.gameIsRunning = false;
				base.StartCoroutine(this.DefeatSequence());
				return;
			}
			foreach (SpecOpsScenario specOpsScenario in this.activeScenarios)
			{
				specOpsScenario.Update();
			}
			foreach (SpecOpsPatrol specOpsPatrol in this.activePatrols)
			{
				specOpsPatrol.Update();
			}
			if (this.exfil != null)
			{
				this.exfil.Update();
			}
			this.dialog.Update();
			this.UpdateAttackers();
			Camera fpCamera = FpsActorController.instance.fpCamera;
			bool flag = false;
			Vehicle vehicle = null;
			RaycastHit raycastHit;
			if (this.C4IsReady() && fpCamera.enabled && !FpsActorController.instance.actor.dead && !FpsActorController.instance.actor.IsSeated() && !FpsActorController.instance.actor.fallenOver && Physics.Raycast(new Ray(fpCamera.transform.position, fpCamera.transform.forward), out raycastHit, 2f, -12947205) && raycastHit.rigidbody != null)
			{
				vehicle = raycastHit.rigidbody.GetComponent<Vehicle>();
				flag = (vehicle != null && vehicle.isLocked && !vehicle.dead);
			}
			if (flag)
			{
				this.hintID = KeyboardGlyphGenerator.instance.ShowBind(SteelInput.KeyBinds.Use, "Plant Timed Explosive (30s)");
			}
			else
			{
				KeyboardGlyphGenerator.Hide(this.hintID);
			}
			if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Use) && flag)
			{
				this.SpawnC4(fpCamera.transform.position, fpCamera.transform.rotation, vehicle, FpsActorController.instance.actor);
			}
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00009089 File Offset: 0x00007289
	public bool C4IsReady()
	{
		return this.c4CooldownAction.TrueDone();
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0007126C File Offset: 0x0006F46C
	public RemoteDetonatedProjectile SpawnC4(Vector3 position, Quaternion rotation, Vehicle targetVehicle, Actor source)
	{
		RemoteDetonatedProjectile component = UnityEngine.Object.Instantiate<GameObject>(this.c4ProjectilePrefab, position, rotation).GetComponent<RemoteDetonatedProjectile>();
		component.gameObject.AddComponent<TimedDetonation>().delay = 30f;
		component.configuration.damage = targetVehicle.maxHealth * 2f;
		component.killCredit = source;
		this.c4CooldownAction.Start();
		this.dialog.OnC4Planted();
		return component;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x000712D8 File Offset: 0x0006F4D8
	private void UpdateAttackers()
	{
		bool flag = this.activeFlare != null;
		Vector3 b = Vector3.zero;
		if (flag)
		{
			b = this.activeFlare.transform.position;
		}
		Vector3 vector = Vector3.zero;
		Vector3 forward = Vector3.forward;
		if (this.searchlight != null)
		{
			vector = this.searchlight.transform.position;
			forward = this.searchlight.transform.forward;
		}
		foreach (Actor actor in this.attackerActors)
		{
			Vector3 vector2 = actor.CenterPosition();
			float num = 0f;
			if (Vector3.Distance(vector2, b) < 100f)
			{
				num += -150f;
			}
			if (this.searchlight != null && Vector3.Dot((vector2 - vector).normalized, forward) > this.searchlightDotFOV && !Physics.Linecast(vector, vector2, 8392705))
			{
				num += -250f;
			}
			actor.visibilityDistanceModifier = num;
			if (!actor.dead && actor.aiControlled)
			{
				AiActorController aiActorController = actor.controller as AiActorController;
				float num2 = (aiActorController.squad.shouldFollow && !aiActorController.isDefaultMovementOverridden) ? 40f : 15f;
				if (!GameManager.IsSpectating())
				{
					bool isIgnored = !aiActorController.JustFired() && !actor.IsSeated() && ActorManager.ActorDistanceToPlayer(actor) < num2;
					actor.isIgnored = isIgnored;
				}
				if (aiActorController.HasSpottedTarget())
				{
					this.lastHadTargetTimestamp[aiActorController] = Time.time;
				}
				else if (aiActorController.IsAlert() && Time.time - this.lastHadTargetTimestamp[aiActorController] > 20f)
				{
					aiActorController.SetNotAlert(false);
				}
			}
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x000714B0 File Offset: 0x0006F6B0
	private void CreateAttackerTeam()
	{
		this.lastHadTargetTimestamp = new Dictionary<AiActorController, float>();
		this.attackerActors = new Actor[this.nAttackers];
		bool flag = !GameManager.IsSpectating();
		if (flag)
		{
			this.attackerActors[0] = ActorManager.instance.player;
		}
		for (int i = flag ? 1 : 0; i < this.nAttackers; i++)
		{
			Actor actor = ActorManager.instance.CreateAIActor(this.attackingTeam);
			this.attackerActors[i] = actor;
			AiActorController key = actor.controller as AiActorController;
			this.lastHadTargetTimestamp.Add(key, Time.time);
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00071544 File Offset: 0x0006F744
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		if (!GameManager.IsSpectating())
		{
			WeaponManager.LoadoutSet loadout = ActorManager.instance.player.controller.GetLoadout();
			this.forceUsePatriots = (loadout.primary != null && loadout.primary.name == "PATRIOT TAC");
		}
		else
		{
			this.forceUsePatriots = false;
			this.canPlayIntroCutscene = false;
		}
		base.StartCoroutine(this.PlayerSpawnSequence());
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00009096 File Offset: 0x00007296
	private IEnumerator PlayerSpawnSequence()
	{
		if (this.canPlayIntroCutscene && this.forceUsePatriots && this.attackingTeam == 0)
		{
			GameObject cinematicObject = UnityEngine.Object.Instantiate<GameObject>(this.introCinematicPrefab, this.attackerSpawnPosition, this.attackerSpawnRotation);
			Camera componentInChildren = cinematicObject.GetComponentInChildren<Camera>();
			SkinnedMeshRenderer[] componentsInChildren = cinematicObject.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ActorManager.ApplyGlobalTeamSkin(componentsInChildren[i], this.attackingTeam);
			}
			FpsActorController.instance.SetOverrideCamera(componentInChildren);
			EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 1f, Color.black);
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(this.WaitForSecondsSkippable(1.5f));
			if (GameManager.GameParameters().nightMode)
			{
				GameManager.EnableNightVision();
				EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.3f, Color.black);
			}
			yield return base.StartCoroutine(this.WaitForSecondsSkippable(17f));
			EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 2f, Color.black);
			yield return base.StartCoroutine(this.WaitForSecondsSkippable(3f));
			FpsActorController.instance.CancelOverrideCamera();
			UnityEngine.Object.Destroy(cinematicObject);
			cinematicObject = null;
		}
		this.SpawnAttackers();
		this.gameIsRunning = true;
		this.dialog.OnPlayerAssumesControl();
		this.introAction.Start();
		yield break;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000090A5 File Offset: 0x000072A5
	private IEnumerator WaitForSecondsSkippable(float time)
	{
		TimedAction waitAction = new TimedAction(time, false);
		waitAction.Start();
		while (!waitAction.TrueDone() && !SteelInput.GetButtonDown(SteelInput.KeyBinds.Fire) && !SteelInput.GetButtonDown(SteelInput.KeyBinds.Jump) && !SteelInput.GetButtonDown(SteelInput.KeyBinds.OpenLoadout))
		{
			yield return 0;
		}
		yield break;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x000715B0 File Offset: 0x0006F7B0
	private void SpawnAttackers()
	{
		ActorManager.RandomSpawnPointForTeam(this.attackingTeam);
		string format = (this.attackingTeam == 0) ? "TALON-{0}" : "RAVEN-{0}";
		WeaponManager.LoadoutSet loadout = ActorManager.instance.player.controller.GetLoadout();
		WeaponManager.WeaponEntry.LoadoutType type = (loadout.primary != null && loadout.primary.type == WeaponManager.WeaponEntry.LoadoutType.Stealth) ? WeaponManager.WeaponEntry.LoadoutType.Stealth : WeaponManager.WeaponEntry.LoadoutType.Normal;
		for (int i = 0; i < this.attackerActors.Length; i++)
		{
			Actor actor = this.attackerActors[i];
			WeaponManager.LoadoutSet loadoutSet = null;
			if (actor.aiControlled)
			{
				AiActorController aiActorController = actor.controller as AiActorController;
				aiActorController.SetNotAlert(false);
				aiActorController.modifier.ignoreFovCheck = true;
				aiActorController.modifier.aquireTargetAimOffsetMultiplier = 0.3f;
				aiActorController.loadoutStrategy.type = type;
				aiActorController.skill = AiActorController.SkillLevel.Veteran;
				loadoutSet = aiActorController.GetLoadout();
				if (i == 1)
				{
					aiActorController.loadoutStrategy.type = WeaponManager.WeaponEntry.LoadoutType.AntiArmor;
					loadoutSet.gear2 = WeaponManager.GetAiWeaponLargeGear(aiActorController.loadoutStrategy, this.attackingTeam);
				}
				if (this.forceUsePatriots)
				{
					loadoutSet.primary = WeaponManager.GetWeaponEntryByName("PATRIOT TAC", null);
				}
			}
			Vector3 point = this.attackerSpawnPosition;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(point + new Vector3(0f, 2f, 0f), Vector3.down), out raycastHit, 8f, 2232321))
			{
				point = raycastHit.point;
			}
			actor.SpawnAt(point, this.attackerSpawnRotation, loadoutSet);
			actor.name = string.Format(format, i + 1);
			actor.hasHeroArmor = true;
		}
		if (GameManager.IsSpectating())
		{
			this.attackerSquad = new Squad(this.attackerActors, ActorManager.RandomSpawnPointForTeam(this.attackingTeam), null, null, 1f);
		}
		else
		{
			this.attackerSquad = FpsActorController.instance.playerSquad;
			foreach (Actor actor2 in this.attackerActors)
			{
				if (actor2.aiControlled)
				{
					FpsActorController.instance.playerSquad.AddMember(actor2.controller);
				}
			}
		}
		this.attackerSquad.engagement = Squad.EngagementType.OnlyAlerted;
		this.attackerSquad.SetFormationSize(2f, 0.5f);
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x000090B4 File Offset: 0x000072B4
	public void OnIncomingPatrol(SpecOpsPatrol patrol)
	{
		if (this.attackingTeam == 0)
		{
			this.dialog.OnIncomingPatrol(patrol);
			this.EyesHighlightSquad(patrol.squad);
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000090D6 File Offset: 0x000072D6
	public void OnEnemiesStartRoaming(SpecOpsScenario scenario)
	{
		if (this.attackingTeam == 0)
		{
			this.dialog.OnEnemiesStartRoaming(scenario.spawn);
			this.EyesHighlightSquad(scenario.squad);
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000717F0 File Offset: 0x0006F9F0
	private void EyesHighlightSquad(Squad squad)
	{
		foreach (ActorController actorController in squad.members)
		{
			actorController.actor.Highlight(30f);
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x000090FD File Offset: 0x000072FD
	public void OnAssassinationTargetIdentified(Actor actor)
	{
		if (this.attackingTeam == 0)
		{
			this.dialog.OnAssassinationTargetIdentified();
			actor.Highlight(30f);
		}
	}

	// Token: 0x04000B70 RID: 2928
	private const int FACING_SABOTAGE_VEHICLE_RAY_MASK = -12947205;

	// Token: 0x04000B71 RID: 2929
	private const float FACING_SABOTAGE_VEHICLE_MAX_RANGE = 2f;

	// Token: 0x04000B72 RID: 2930
	public const float SABOTAGE_VEHICLE_C4_TIMER = 30f;

	// Token: 0x04000B73 RID: 2931
	private const float EYES_HIGHLIGHT_DURATION = 30f;

	// Token: 0x04000B74 RID: 2932
	private const float INVISIBLE_RANGE_ORDERED = 15f;

	// Token: 0x04000B75 RID: 2933
	private const float INVISIBLE_RANGE_FOLLOWING = 40f;

	// Token: 0x04000B76 RID: 2934
	private const float AUTO_ATTACKER_REMOVE_ALERT_TIMEOUT = 20f;

	// Token: 0x04000B77 RID: 2935
	private const string TEAM_NAME_BLUE = "TALON-{0}";

	// Token: 0x04000B78 RID: 2936
	private const string TEAM_NAME_RED = "RAVEN-{0}";

	// Token: 0x04000B79 RID: 2937
	private const string PATRIOT_SPEC_OPS_NAME = "PATRIOT TAC";

	// Token: 0x04000B7A RID: 2938
	private const int ATTACKER_SPAWN_OPTIMIZATION_ITERATIONS = 8;

	// Token: 0x04000B7B RID: 2939
	private const float ATTACKER_SPAWN_OPTIMIZATION_MIN_DISTANCE_TO_SPAWN = 200f;

	// Token: 0x04000B7C RID: 2940
	private const float ATTACKER_SPAWN_OPTIMIZATION_MAX_DISTANCE_TO_SPAWN = 300f;

	// Token: 0x04000B7D RID: 2941
	private const float ATTACKER_SPAWN_OPTIMIZATION_MIN_DISTANCE_TO_INACTIVE_SPAWN = 20f;

	// Token: 0x04000B7E RID: 2942
	private const float USE_FLAG_SPAWN_MEDIAN_DISTANCE = 100f;

	// Token: 0x04000B7F RID: 2943
	private const int ATTACKER_SPAWN_ALLOWED_NAVMESH_TAG = -13;

	// Token: 0x04000B80 RID: 2944
	private const float ATTACKER_SPAWN_FIND_GEOMETRY_RAY_LENGTH = 5f;

	// Token: 0x04000B81 RID: 2945
	private const float MAX_ATTRACT_RANGE_FLARE = 500f;

	// Token: 0x04000B82 RID: 2946
	private const float FORCE_ATTRACT_RANGE_FLARE = 200f;

	// Token: 0x04000B83 RID: 2947
	private const float FLARE_ATTRACT_PATROLS_DURATION = 3f;

	// Token: 0x04000B84 RID: 2948
	private const float FLARE_INCREASE_VISIBILITY_RANGE = 100f;

	// Token: 0x04000B85 RID: 2949
	private const float FLARE_VISIBILITY_DISTANCE_MODIFIER = -150f;

	// Token: 0x04000B86 RID: 2950
	private const float SPOTLIGHT_VISIBILITY_DISTANCE_MODIFIER = -250f;

	// Token: 0x04000B87 RID: 2951
	private const int BOX_CHECK_ITERATIONS = 16;

	// Token: 0x04000B88 RID: 2952
	private static readonly SpecOpsMode.BoxCheckValues[] INTRO_BOX_CHECKS = new SpecOpsMode.BoxCheckValues[]
	{
		new SpecOpsMode.BoxCheckValues
		{
			offset = new Vector3(0f, 6f, 15f),
			halfScale = new Vector3(2f, 2f, 10f)
		},
		new SpecOpsMode.BoxCheckValues
		{
			offset = new Vector3(0f, 4.5f, 2f),
			halfScale = new Vector3(3f, 2f, 3f)
		},
		new SpecOpsMode.BoxCheckValues
		{
			offset = new Vector3(0f, 6f, 6f),
			halfScale = new Vector3(8f, 1f, 4f)
		},
		new SpecOpsMode.BoxCheckValues
		{
			offset = new Vector3(0f, 3.5f, -2f),
			halfScale = new Vector3(2f, 2f, 3f)
		},
		new SpecOpsMode.BoxCheckValues
		{
			offset = new Vector3(-2f, 4f, -4f),
			halfScale = new Vector3(2f, 2f, 2f)
		}
	};

	// Token: 0x04000B89 RID: 2953
	public const float DETECTION_DISTANCE_DAY = 100f;

	// Token: 0x04000B8A RID: 2954
	public const float DETECTION_DISTANCE_NIGHT = 60f;

	// Token: 0x04000B8B RID: 2955
	public const float DETECTION_DISTANCE_PATROL_MULTIPLIER = 1.2f;

	// Token: 0x04000B8C RID: 2956
	public const float ALERT_DETECTION_DISTANCE = 500f;

	// Token: 0x04000B8D RID: 2957
	public const float DETECTION_DISTANCE_HELICOPTER_PATROL = 200f;

	// Token: 0x04000B8E RID: 2958
	public const float PATROL_ALERT_DISTANCE = 140f;

	// Token: 0x04000B8F RID: 2959
	public const float DETECTION_RATE_HELICOPTER_PATROL = 0.5f;

	// Token: 0x04000B90 RID: 2960
	public const float DETECTION_RATE_DAY = 0.4f;

	// Token: 0x04000B91 RID: 2961
	public const float DETECTION_RATE_NIGHT = 0.33f;

	// Token: 0x04000B92 RID: 2962
	public const float DETECTION_RATE_PATROL_MULTIPLIER = 0.7f;

	// Token: 0x04000B93 RID: 2963
	[NonSerialized]
	public int attackingTeam;

	// Token: 0x04000B94 RID: 2964
	[NonSerialized]
	public int defendingTeam;

	// Token: 0x04000B95 RID: 2965
	[NonSerialized]
	public Actor[] attackerActors;

	// Token: 0x04000B96 RID: 2966
	public ActorSkin talonSkinDay;

	// Token: 0x04000B97 RID: 2967
	public ActorSkin talonSkinNight;

	// Token: 0x04000B98 RID: 2968
	public ActorAccessory commOfficerAccessory;

	// Token: 0x04000B99 RID: 2969
	public GameObject sabotagePrefab;

	// Token: 0x04000B9A RID: 2970
	public GameObject introCinematicPrefab;

	// Token: 0x04000B9B RID: 2971
	public GameObject exfilHelicopterPrefab;

	// Token: 0x04000B9C RID: 2972
	public GameObject c4ProjectilePrefab;

	// Token: 0x04000B9D RID: 2973
	public Texture2D insertionBlip;

	// Token: 0x04000B9E RID: 2974
	public Texture2D insertionDirectionBlip;

	// Token: 0x04000B9F RID: 2975
	public GameObject flarePrefab;

	// Token: 0x04000BA0 RID: 2976
	public GameObject spotlightPrefab;

	// Token: 0x04000BA1 RID: 2977
	public Text plantC4Text;

	// Token: 0x04000BA2 RID: 2978
	[NonSerialized]
	public SpecOpsDialog dialog;

	// Token: 0x04000BA3 RID: 2979
	[NonSerialized]
	public List<SpecOpsScenario> activeScenarios;

	// Token: 0x04000BA4 RID: 2980
	[NonSerialized]
	public List<SpecOpsPatrol> activePatrols;

	// Token: 0x04000BA5 RID: 2981
	[NonSerialized]
	public List<SpecOpsObjective> activeObjectives;

	// Token: 0x04000BA6 RID: 2982
	[NonSerialized]
	public Dictionary<SpawnPoint, SpecOpsScenario> scenarioAtSpawn;

	// Token: 0x04000BA7 RID: 2983
	[NonSerialized]
	public Vector3 attackerSpawnPosition;

	// Token: 0x04000BA8 RID: 2984
	[NonSerialized]
	public Quaternion attackerSpawnRotation;

	// Token: 0x04000BA9 RID: 2985
	private bool gameIsRunning;

	// Token: 0x04000BAA RID: 2986
	[NonSerialized]
	public Seeker seeker;

	// Token: 0x04000BAB RID: 2987
	private bool tinyMap;

	// Token: 0x04000BAC RID: 2988
	private Dictionary<AiActorController, float> lastHadTargetTimestamp;

	// Token: 0x04000BAD RID: 2989
	private int nInfantryPatrols;

	// Token: 0x04000BAE RID: 2990
	private bool allowCarPatrol;

	// Token: 0x04000BAF RID: 2991
	private bool allowBoatPatrol;

	// Token: 0x04000BB0 RID: 2992
	private bool allowHelicopterExfil;

	// Token: 0x04000BB1 RID: 2993
	private List<PathfindingBox.Type> allowedPatrolObjectives = new List<PathfindingBox.Type>();

	// Token: 0x04000BB2 RID: 2994
	[NonSerialized]
	public bool patrolUseApc;

	// Token: 0x04000BB3 RID: 2995
	[NonSerialized]
	public bool patrolUseAttackBoat;

	// Token: 0x04000BB4 RID: 2996
	private bool allowHelicopterPatrol;

	// Token: 0x04000BB5 RID: 2997
	private bool hasSpawnedHelicopterPatrol;

	// Token: 0x04000BB6 RID: 2998
	private bool canPlayIntroCutscene;

	// Token: 0x04000BB7 RID: 2999
	[NonSerialized]
	public Squad attackerSquad;

	// Token: 0x04000BB8 RID: 3000
	[NonSerialized]
	public bool attackersHaveDefaultSkin;

	// Token: 0x04000BB9 RID: 3001
	private bool forceUsePatriots;

	// Token: 0x04000BBA RID: 3002
	private TimedAction c4CooldownAction = new TimedAction(32f, false);

	// Token: 0x04000BBB RID: 3003
	private TimedAction introAction = new TimedAction(20f, false);

	// Token: 0x04000BBC RID: 3004
	private TimedAction flareCooldown = new TimedAction(120f, false);

	// Token: 0x04000BBD RID: 3005
	private ExfilHelicopter exfil;

	// Token: 0x04000BBE RID: 3006
	private bool exfiltrationSpawned;

	// Token: 0x04000BBF RID: 3007
	private GameObject activeFlare;

	// Token: 0x04000BC0 RID: 3008
	private int nAttackers = 4;

	// Token: 0x04000BC1 RID: 3009
	private int hintID;

	// Token: 0x04000BC2 RID: 3010
	private Light searchlight;

	// Token: 0x04000BC3 RID: 3011
	private float searchlightDotFOV;

	// Token: 0x04000BC4 RID: 3012
	private Actor heliPilot;

	// Token: 0x04000BC5 RID: 3013
	private const float HELICOPTER_PATROL_SPAWN_DISTANCE = 1500f;

	// Token: 0x04000BC6 RID: 3014
	private bool heliPatrolHasLanded;

	// Token: 0x0200018F RID: 399
	private struct BoxCheckValues
	{
		// Token: 0x04000BC7 RID: 3015
		public Vector3 offset;

		// Token: 0x04000BC8 RID: 3016
		public Vector3 halfScale;
	}
}

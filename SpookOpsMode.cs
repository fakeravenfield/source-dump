using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019E RID: 414
public class SpookOpsMode : GameModeBase
{
	// Token: 0x06000AF7 RID: 2807 RVA: 0x000724F4 File Offset: 0x000706F4
	protected override void Awake()
	{
		base.Awake();
		SkeletonAltar.used = 0;
		ResupplyCrate[] array = UnityEngine.Object.FindObjectsOfType<ResupplyCrate>();
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i].gameObject);
		}
		this.text = base.GetComponentInChildren<Text>();
		this.seeker = base.GetComponent<Seeker>();
		this.damageSound = this.damageParticles.GetComponent<AudioSource>();
		this.forcefieldRenderer = this.forcefieldTransform.GetComponentInChildren<Renderer>();
		this.forcefieldColor = this.forcefieldRenderer.material.GetColor("_TintColor");
		GameManager.instance.gameModeParameters.noVehicles = true;
		GameManager.instance.gameModeParameters.noTurrets = true;
		GameManager.instance.gameModeParameters.playerTeam = 0;
		GameManager.instance.gameModeParameters.nightMode = true;
		this.waveUi.SetActive(false);
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x000725D0 File Offset: 0x000707D0
	public override void StartGame()
	{
		ActorManager.SetGlobalTeamSkin(1, this.skeletonSkin);
		this.difficulty = Options.GetDropdown(OptionDropdown.Id.Difficulty);
		int num;
		if (this.difficulty == 0)
		{
			this.skeletonCountModifier = 0.5f;
			this.favors++;
			num = 10;
		}
		else if (this.difficulty == 1)
		{
			this.skeletonCountModifier = 0.8f;
			num = 8;
		}
		else
		{
			this.skeletonCountModifier = 1f;
			num = 7;
		}
		GameManager.SetTeamColors(ColorScheme.TeamColor(0), Color.gray);
		this.objectsToCleanupOnStart = new List<GameObject>();
		this.vignette.enabled = true;
		this.blackout.enabled = true;
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			spawnPoints[i].gameObject.SetActive(false);
		}
		Dictionary<int, List<Actor>> dictionary = ActorManager.instance.InitializeAiActors(3, 50);
		this.spawnableSkeletonActors = new Queue<Actor>(50);
		this.skeletonsPendingSpawn = new List<Actor>(50);
		this.waveSkeletons = new List<Actor>();
		this.specialSkeletons = new List<Actor>();
		int num2 = 0;
		foreach (Actor actor in dictionary[1])
		{
			this.spawnableSkeletonActors.Enqueue(actor);
			((AiActorController)actor.controller).modifier = this.skeletonModifier;
			AlternativePath alternativePath = actor.gameObject.AddComponent<AlternativePath>();
			actor.dropsAmmoOnKick = false;
			alternativePath.penalty = 500;
			if (num2 % num == 0)
			{
				this.specialSkeletons.Add(actor);
				actor.AddAccessory(this.specialSkeletonAccessory);
			}
			num2++;
		}
		AstarPath active = AstarPath.active;
		this.FindAvailableSpawnPoints();
		this.skeletonSpawnPositions = new Dictionary<SpawnPoint, List<Vector3>>(this.availableSpawnPoints.Count);
		Debug.Log("Available spawn points: ");
		foreach (SpawnPoint spawnPoint in this.availableSpawnPoints)
		{
			Debug.Log(spawnPoint.shortName);
			this.skeletonSpawnPositions.Add(spawnPoint, new List<Vector3>());
		}
		Debug.Log("---");
		this.infantryGraphs = new List<NavGraph>();
		foreach (NavGraph navGraph in active.data.graphs)
		{
			if (navGraph != null && PathfindingManager.GetTypeFromGraphName(navGraph) == PathfindingBox.Type.Infantry)
			{
				this.infantryGraphs.Add(navGraph);
				navGraph.GetNodes(delegate(GraphNode node)
				{
					if (node.Tag != 3U)
					{
						Vector3 vector = (Vector3)node.position;
						foreach (SpawnPoint spawnPoint2 in this.availableSpawnPoints)
						{
							float num4 = Vector3.Distance(spawnPoint2.transform.position, vector);
							if (num4 > 35f && num4 < 80f)
							{
								this.skeletonSpawnPositions[spawnPoint2].Add(vector);
							}
						}
					}
					return true;
				});
			}
		}
		this.minimapOrbMarker.SetParent(MinimapUi.instance.minimap.rectTransform);
		this.PrepareNextPhase();
		this.playerSpawn = this.currentSpawnPoint;
		int num3 = UnityEngine.Random.Range(0, this.currentSpawnPoint.allNeighbors.Count);
		for (int j = 0; j < this.currentSpawnPoint.allNeighbors.Count; j++)
		{
			SpawnPoint b = this.currentSpawnPoint.allNeighbors[(j + num3) % this.currentSpawnPoint.allNeighbors.Count];
			if (SpawnPointNeighborManager.HasLandConnection(this.currentSpawnPoint, b))
			{
				this.playerSpawn = b;
				break;
			}
		}
		if (this.shouldSpawnJeep)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.transportPrefab, this.playerSpawn.GetSpawnPosition(), Quaternion.identity).GetComponent<Vehicle>().applyAutoDamage = false;
		}
		this.spectatorCamera = UnityEngine.Object.FindObjectOfType<SceneryCamera>().GetComponent<Camera>();
		this.spectatorCamera.transform.eulerAngles.x = 30f;
		this.spectatorCamera.transform.position = this.currentSpawnPoint.transform.position + Vector3.up * 10f - this.spectatorCamera.transform.forward * 80f;
		CameraSway cameraSway = this.spectatorCamera.gameObject.AddComponent<CameraSway>();
		cameraSway.magnitude = 0.5f;
		cameraSway.frequency = 0.02f;
		this.damageParticles.transform.SetParent(FpsActorController.instance.fpCamera.transform);
		this.damageParticles.transform.rotation = Quaternion.identity;
		this.damageParticles.transform.localPosition = new Vector3(0f, 0f, 2f);
		this.introAction.Start();
		base.Invoke("StartText", 4f);
		base.Invoke("SpawnPlayer", 10f);
		base.Invoke("MapHint", 14f);
		base.InvokeRepeating("AutoDamage", 10f, 0.5f);
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0000933C File Offset: 0x0000753C
	private void MapHint()
	{
		OverlayUi.ShowOverlayText("USE THE MAP TO FIND ANOMALIES", 5f);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x00072A98 File Offset: 0x00070C98
	private void AutoDamage()
	{
		if (this.IsPlayingPhase() && this.prepareWaveAction.TrueDone())
		{
			foreach (Actor actor in ActorManager.AliveActorsOnTeam(0).ToArray())
			{
				if (Vector3.Distance(actor.Position(), this.currentSpawnPoint.transform.position) >= 35f)
				{
					DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Unknown, null, null)
					{
						healthDamage = 1f,
						isPiercing = true,
						point = actor.CenterPosition(),
						direction = actor.controller.FacingDirection()
					};
					actor.Damage(info);
					if (!actor.aiControlled)
					{
						PlayerFpParent.instance.ApplyScreenshake(2f, 1);
					}
				}
			}
		}
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0000934D File Offset: 0x0000754D
	private void StartText()
	{
		OverlayUi.ShowOverlayText("INVESTIGATE THE ANOMALY", 3.5f);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00072B68 File Offset: 0x00070D68
	private void FindAvailableSpawnPoints()
	{
		Dictionary<SpawnPoint, List<SpawnPoint>> dictionary = new Dictionary<SpawnPoint, List<SpawnPoint>>();
		List<float> list = new List<float>();
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			dictionary.Add(spawnPoint, new List<SpawnPoint>());
			foreach (SpawnPoint spawnPoint2 in ActorManager.instance.spawnPoints)
			{
				if (spawnPoint == spawnPoint2)
				{
					dictionary[spawnPoint].Add(spawnPoint2);
				}
				else if (SpawnPointNeighborManager.HasLandConnection(spawnPoint, spawnPoint2))
				{
					dictionary[spawnPoint].Add(spawnPoint2);
					list.Add(Vector3.Distance(spawnPoint.transform.position, spawnPoint2.transform.position));
				}
			}
		}
		int num = -1;
		SpawnPoint key = null;
		foreach (SpawnPoint spawnPoint3 in ActorManager.instance.spawnPoints)
		{
			int count = dictionary[spawnPoint3].Count;
			if (count > num)
			{
				num = count;
				key = spawnPoint3;
			}
		}
		this.availableSpawnPoints = dictionary[key];
		list.Sort();
		float num2 = list[list.Count / 2];
		this.shouldSpawnJeep = (num2 > 400f);
		Debug.Log("Median travel distance: " + num2.ToString());
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00072CC8 File Offset: 0x00070EC8
	public void SpawnAmbush(Vector3 center)
	{
		float num = 99999f;
		GraphNode graphNode = null;
		foreach (NavGraph navGraph in this.infantryGraphs)
		{
			GraphNode node = navGraph.GetNearest(center).node;
			Vector3 b = (Vector3)node.position;
			float num2 = Vector3.Distance(center, b);
			if (num2 < num)
			{
				num = num2;
				graphNode = node;
			}
		}
		List<Vector3> list = new List<Vector3>();
		if (graphNode != null)
		{
			List<GraphNode> spawnNodes = new List<GraphNode>();
			spawnNodes.Add(graphNode);
			graphNode.GetConnections(delegate(GraphNode obj)
			{
				spawnNodes.Add(obj);
			});
			for (int i = 0; i < 8; i++)
			{
				list.Add(spawnNodes[i % spawnNodes.Count].RandomPointOnSurface());
			}
		}
		else
		{
			for (int j = 0; j < 8; j++)
			{
				list.Add(center + UnityEngine.Random.insideUnitSphere.ToGround().normalized * 2f);
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			this.SpawnSkeleton(list[k]);
		}
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00072E1C File Offset: 0x0007101C
	private void Update()
	{
		if (this.gameOver && this.gameEndPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
		{
			GameManager.RestartLevel();
		}
		if (!this.introAction.TrueDone())
		{
			Color color = new Color(0f, 0f, 0f, this.introFade.Evaluate(this.introAction.Ratio()));
			this.blackout.color = color;
		}
		else if (this.gameOver)
		{
			if (!this.fadeOutEndScreenAction.TrueDone())
			{
				this.blackout.gameObject.SetActive(true);
				Color color2 = new Color(0f, 0f, 0f, this.fadeOutEndScreenAction.Ratio() * 0.7f);
				this.blackout.color = color2;
			}
			else
			{
				Color color3 = new Color(0f, 0f, 0f, 0.7f);
				this.blackout.color = color3;
			}
		}
		else
		{
			this.blackout.gameObject.SetActive(false);
		}
		if (!this.explosionAction.TrueDone())
		{
			this.forcefieldTransform.localScale = Vector3.one * 35f * this.explosionCurve.Evaluate(this.explosionAction.Ratio());
			this.forcefieldRenderer.material.SetColor("_TintColor", Color.Lerp(this.forcefieldColor, Color.white, Mathf.Clamp((1f - Mathf.Abs(this.explosionAction.Ratio() * 2f - 1f)) * 0.7f, 0f, 0.5f)));
		}
		else if (!this.phaseEnded)
		{
			this.forcefieldTransform.localScale = Vector3.one * 35f;
			this.forcefieldRenderer.material.SetColor("_TintColor", this.forcefieldColor);
		}
		try
		{
			bool flag = (this.currentSpawnPoint.transform.position - FpsActorController.instance.actor.Position()).magnitude < 35f;
			if (this.awaitingNextPhase && flag)
			{
				this.StartPhase();
			}
			bool flag2 = this.IsPlayingPhase() && this.prepareWaveAction.TrueDone() && !flag;
			this.damageParticles.emission.enabled = flag2;
			if (this.damageSound.isPlaying && !flag2)
			{
				this.damageSound.Stop();
			}
			else if (!this.damageSound.isPlaying && flag2)
			{
				this.damageSound.Play();
			}
		}
		catch (Exception)
		{
			Debug.Log(FpsActorController.instance.actor);
			Debug.Log(this.currentSpawnPoint);
		}
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000730F4 File Offset: 0x000712F4
	private void LateUpdate()
	{
		List<Actor> list = new List<Actor>(ActorManager.AliveActorsOnTeam(1));
		float[] array = new float[]
		{
			99999f,
			99999f
		};
		Actor[] array2 = new Actor[2];
		foreach (Actor actor in list)
		{
			float sqrMagnitude = (actor.Position() - GameManager.GetMainCamera().transform.position).sqrMagnitude;
			if (sqrMagnitude < array[0])
			{
				array2[1] = array2[0];
				array[1] = array[0];
				array2[0] = actor;
				array[0] = sqrMagnitude;
			}
			else if (sqrMagnitude < array[1])
			{
				array2[1] = actor;
				array[1] = sqrMagnitude;
			}
		}
		if (array2[0] != null)
		{
			this.skeletonWalkSounds[0].transform.position = array2[0].CenterPosition();
			this.skeletonWalkSounds[0].enabled = true;
			this.skeletonApproachSoundBank.transform.position = array2[0].CenterPosition();
			if (array[0] < 40f && this.skeletonApproachAction.TrueDone())
			{
				this.skeletonApproachSoundBank.PlayRandom();
				this.skeletonApproachAction.StartLifetime(UnityEngine.Random.Range(1.5f, 2.5f));
			}
		}
		else
		{
			this.skeletonWalkSounds[0].enabled = false;
		}
		if (array2[1] != null)
		{
			this.skeletonWalkSounds[1].transform.position = array2[1].CenterPosition();
			this.skeletonWalkSounds[1].enabled = true;
			return;
		}
		this.skeletonWalkSounds[1].enabled = false;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0000935E File Offset: 0x0000755E
	private void EndGame()
	{
		this.gameOver = true;
		base.StopAllCoroutines();
		base.Invoke("FadeOut", 2f);
		base.Invoke("ShowEndScreen", 4f);
		base.Invoke("ShowEndText", 5.5f);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0000939D File Offset: 0x0000759D
	private void FadeOut()
	{
		this.fadeOutEndScreenAction.Start();
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00073298 File Offset: 0x00071498
	private void ShowEndScreen()
	{
		this.gameEndPanel.SetActive(true);
		this.gameEndText.enabled = false;
		this.gameEndTitle.text = (this.gameWon ? "<color=#ffffff>YOU WON</color>" : "<color=#ff0000>YOU DIED</color>");
		if (this.gameWon)
		{
			this.currentPhase = 7;
		}
		string text;
		if (this.difficulty == 0)
		{
			text = "EASY";
		}
		else if (this.difficulty == 1)
		{
			text = "NORMAL";
		}
		else
		{
			text = "HARD";
		}
		this.gameEndText.text = string.Concat(new string[]
		{
			"DIFFICULTY - ",
			text,
			"\n\nWAVES CLEARED - ",
			this.currentPhase.ToString(),
			"/7\n\nSKELETONS PURGED - ",
			this.skeletonKills.ToString(),
			"\n\nALTARS ACTIVATED - ",
			SkeletonAltar.used.ToString(),
			"/",
			this.altarsSpawned.ToString(),
			"\n\n\n\nPRESS ENTER TO RESTART"
		});
		PlayerFpParent.instance.ApplyScreenshake(3f, 2);
		this.gameEndPanel.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x000093AA File Offset: 0x000075AA
	private void ShowEndText()
	{
		this.gameEndPanel.GetComponent<AudioSource>().Play();
		this.gameEndText.enabled = true;
		PlayerFpParent.instance.ApplyScreenshake(4f, 3);
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000733B8 File Offset: 0x000715B8
	private void PreparePhase(SpawnPoint spawnPoint, int skeletonCount, int skeletonTicketCount)
	{
		this.currentSpawnPoint = spawnPoint;
		this.pointHighlighter.transform.position = this.currentSpawnPoint.transform.position;
		this.forcefieldTransform.localScale = Vector3.one * 35f;
		this.orbGameObject.SetActive(true);
		this.skeletonCount = Mathf.RoundToInt(this.skeletonCountModifier * (float)skeletonCount);
		this.targetKillCount = Mathf.RoundToInt(this.skeletonCountModifier * (float)skeletonTicketCount);
		this.killCount = 0;
		this.phaseEnded = false;
		this.awaitingNextPhase = true;
		Vector3 v = MinimapCamera.instance.camera.WorldToViewportPoint(this.currentSpawnPoint.transform.position);
		this.minimapOrbMarker.localScale = Vector3.one;
		this.minimapOrbMarker.anchorMin = v;
		this.minimapOrbMarker.anchorMax = v;
		this.minimapOrbMarker.anchoredPosition = Vector2.zero;
		SpookOpsMode.PickupType pickupType = SpookOpsMode.PICKUPS[this.currentPhase];
		if (pickupType != SpookOpsMode.PickupType.None)
		{
			this.SpawnPickups(pickupType, SpookOpsMode.PICKUP_AMMO_MULTIPLIER[this.currentPhase]);
		}
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x000734D4 File Offset: 0x000716D4
	private void SpawnPickups(SpookOpsMode.PickupType type, float ammoMultiplier)
	{
		if (this.activePickups != null)
		{
			this.objectsToCleanupOnStart.AddRange(this.activePickups);
		}
		this.activePickups = new List<GameObject>();
		Vector3 a = this.currentSpawnPoint.GetSpawnPosition();
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(a + Vector3.up, Vector3.down), out raycastHit, 5f, 1))
		{
			a = raycastHit.point;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.weaponPickupPrefab);
		gameObject.transform.position = this.FindSurfaceRaycast(this.currentSpawnPoint.GetSpawnPosition(), 4f);
		WeaponManager.WeaponEntry[] array;
		if (type == SpookOpsMode.PickupType.Secondary)
		{
			array = this.secondaryPickups;
		}
		else if (type == SpookOpsMode.PickupType.PrimaryEarly)
		{
			array = this.earlyPrimaryPickups;
		}
		else
		{
			array = this.latePrimaryPickups;
		}
		WeaponPickup componentInChildren = gameObject.GetComponentInChildren<WeaponPickup>();
		componentInChildren.entry = array[UnityEngine.Random.Range(0, array.Length)];
		componentInChildren.spareAmmoMultiplier = ammoMultiplier;
		componentInChildren.affectSquadmates = (type == SpookOpsMode.PickupType.PrimaryLate || type == SpookOpsMode.PickupType.PrimaryEarly);
		if (componentInChildren.entry.slot == WeaponManager.WeaponSlot.Primary)
		{
			componentInChildren.slot = 0;
		}
		else if (componentInChildren.entry.slot == WeaponManager.WeaponSlot.Secondary)
		{
			componentInChildren.slot = 1;
		}
		else
		{
			componentInChildren.slot = 4;
		}
		this.activePickups.Add(gameObject);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00073604 File Offset: 0x00071804
	private void StartPhase()
	{
		if (this.objectsToCleanupOnStart != null)
		{
			for (int i = 0; i < this.objectsToCleanupOnStart.Count; i++)
			{
				UnityEngine.Object.Destroy(this.objectsToCleanupOnStart[i]);
			}
		}
		this.objectsToCleanupOnStart.Clear();
		OverlayUi.ShowOverlayText("PREPARE FOR INCOMING WAVE", 5f);
		this.awaitingNextPhase = false;
		this.killCount = 0;
		this.UpdateUi();
		this.prepareWaveAction.Start();
		this.spawnSkeletonsCoroutine = base.StartCoroutine(this.SpawnSkeletons());
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0007368C File Offset: 0x0007188C
	private void UpdateUi()
	{
		this.text.text = string.Concat(new string[]
		{
			"WAVE ",
			(this.currentPhase + 1).ToString(),
			": ",
			this.killCount.ToString(),
			"/",
			this.targetKillCount.ToString()
		});
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x000093D8 File Offset: 0x000075D8
	private void SpawnPlayer()
	{
		this.SpawnPlayerSquad();
		this.vignette.gameObject.SetActive(false);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x000093F1 File Offset: 0x000075F1
	private IEnumerator SpawnSkeletons()
	{
		yield return new WaitForSeconds(15f);
		OverlayUi.ShowOverlayText("WAVE " + (this.currentPhase + 1).ToString(), 3.5f);
		yield return new WaitForSeconds(4f);
		this.waveUi.SetActive(true);
		while (this.IsPlayingPhase())
		{
			if (this.waveSkeletons.Count < this.skeletonCount / 3)
			{
				int num = this.skeletonCount - this.waveSkeletons.Count;
				for (int i = 0; i < num; i++)
				{
					Actor actor = this.SpawnSkeleton(this.GetSkeletonSpawnPoint());
					if (!(actor != null))
					{
						break;
					}
					this.waveSkeletons.Add(actor);
				}
			}
			yield return new WaitForSeconds(5f);
		}
		yield break;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00009400 File Offset: 0x00007600
	private bool IsPlayingPhase()
	{
		return !this.awaitingNextPhase && !this.phaseEnded;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000736F8 File Offset: 0x000718F8
	private void PrepareNextPhase()
	{
		this.currentPhase++;
		SpawnPoint spawnPoint = this.currentSpawnPoint;
		if (this.currentSpawnPoint != null)
		{
			spawnPoint = this.GetNextSpawnPoint();
			if (spawnPoint != this.currentSpawnPoint)
			{
				this.SpawnAltarBetween(this.currentSpawnPoint, spawnPoint);
			}
		}
		else
		{
			spawnPoint = this.availableSpawnPoints[UnityEngine.Random.Range(0, this.availableSpawnPoints.Count)];
		}
		this.PreparePhase(spawnPoint, SpookOpsMode.WAVE_SKELETON_COUNT[this.currentPhase], SpookOpsMode.WAVE_SKELETON_TICKETS[this.currentPhase]);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00073788 File Offset: 0x00071988
	private SpawnPoint GetNextSpawnPoint()
	{
		List<SpawnPoint> neighborsIncludeDisabled = SpawnPointNeighborManager.GetNeighborsIncludeDisabled(this.currentSpawnPoint);
		int num = UnityEngine.Random.Range(0, neighborsIncludeDisabled.Count);
		for (int i = 0; i < neighborsIncludeDisabled.Count; i++)
		{
			SpawnPoint spawnPoint = neighborsIncludeDisabled[(i + num) % neighborsIncludeDisabled.Count];
			if (SpawnPointNeighborManager.HasLandConnection(spawnPoint, this.currentSpawnPoint))
			{
				return spawnPoint;
			}
		}
		Debug.Log("No connection found, cannot spawn altar");
		return this.currentSpawnPoint;
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00009415 File Offset: 0x00007615
	private void SpawnAltarBetween(SpawnPoint current, SpawnPoint next)
	{
		this.seeker.StartPath(current.transform.position, next.transform.position, new OnPathDelegate(this.OnPathCompleted), PathfindingManager.infantryGraphMask);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000737F0 File Offset: 0x000719F0
	private void OnPathCompleted(Path p)
	{
		if (p.error)
		{
			return;
		}
		try
		{
			int count = p.path.Count;
			int minInclusive = Mathf.FloorToInt(0.3f * (float)count);
			int maxExclusive = Mathf.CeilToInt(0.6f * (float)count);
			int num = UnityEngine.Random.Range(minInclusive, maxExclusive);
			GraphNode graphNode = p.path[num];
			GraphNode graphNode2 = p.path[Mathf.Max(0, num - 5)];
			Vector3 v = (Vector3)graphNode.position - (Vector3)graphNode2.position;
			Debug.DrawLine((Vector3)graphNode.position, (Vector3)graphNode2.position, Color.red, 50f);
			List<GraphNode> neighborNodes = new List<GraphNode>();
			graphNode.GetConnections(delegate(GraphNode obj)
			{
				neighborNodes.Add(obj);
			});
			foreach (GraphNode item in p.path)
			{
				neighborNodes.Remove(item);
			}
			if (neighborNodes.Count > 0)
			{
				graphNode = neighborNodes[UnityEngine.Random.Range(0, neighborNodes.Count)];
			}
			Vector3 vector = this.FindSurfaceRaycast(graphNode.RandomPointOnSurface(), 4f);
			Debug.DrawRay(vector, v.ToGround() * 5f, Color.red, 50f);
			GameObject item2 = UnityEngine.Object.Instantiate<GameObject>(this.altarPrefab, vector, Quaternion.LookRotation(v.ToGround()));
			this.objectsToCleanupOnStart.Add(item2);
			this.altarsSpawned++;
		}
		catch (Exception exception)
		{
			Debug.Log("Unable to spawn altar:");
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x0000296E File Offset: 0x00000B6E
	public override void PlayerAcceptedLoadoutFirstTime()
	{
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x000739E4 File Offset: 0x00071BE4
	private void SpawnPlayerSquad()
	{
		foreach (Actor actor in ActorManager.ActorsOnTeam(0))
		{
			actor.SpawnAt(this.playerSpawn.GetSpawnPosition(), Quaternion.identity, null);
			if (actor.aiControlled)
			{
				actor.controller.ChangeToSquad(FpsActorController.instance.playerSquad);
				((AiActorController)actor.controller).skill = AiActorController.SkillLevel.Veteran;
			}
		}
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00073A78 File Offset: 0x00071C78
	private Actor SpawnSkeleton(Vector3 position)
	{
		if (this.spawnableSkeletonActors.Count <= 0)
		{
			return null;
		}
		Actor actor = this.spawnableSkeletonActors.Dequeue();
		base.StartCoroutine(this.SpawnSkeletonCoroutine(actor, position));
		return actor;
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00073AB4 File Offset: 0x00071CB4
	private Vector3 FindSurfaceRaycast(Vector3 position, float rayLength)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(position + Vector3.up * rayLength / 2f, Vector3.down), out raycastHit, rayLength, 1))
		{
			return raycastHit.point;
		}
		return position;
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0000944F File Offset: 0x0000764F
	private IEnumerator SpawnSkeletonCoroutine(Actor actor, Vector3 position)
	{
		this.skeletonsPendingSpawn.Add(actor);
		position = this.FindSurfaceRaycast(position, 4f);
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
		GameObject coffinObject = UnityEngine.Object.Instantiate<GameObject>(this.spawnCoffinPrefab, position, Quaternion.identity);
		coffinObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
		yield return new WaitForSeconds(2.7f);
		Order order = new Order(Order.OrderType.Attack, this.currentSpawnPoint, this.currentSpawnPoint, true);
		this.skeletonsPendingSpawn.Remove(actor);
		actor.SpawnAt(position, Quaternion.identity, null);
		new List<ActorController>();
		new Squad(new List<ActorController>
		{
			actor.controller
		}, order.source, order, null, 0f);
		actor.SetHealth(60f);
		yield return new WaitForSeconds(5f);
		UnityEngine.Object.Destroy(coffinObject);
		yield break;
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00073AFC File Offset: 0x00071CFC
	private void EndPhase()
	{
		this.waveUi.SetActive(false);
		this.phaseEnded = true;
		this.explosionAction.Start();
		this.explosionAudio.Play();
		base.StopAllCoroutines();
		foreach (Actor item in this.skeletonsPendingSpawn)
		{
			this.spawnableSkeletonActors.Enqueue(item);
		}
		this.skeletonsPendingSpawn.Clear();
		base.Invoke("Explode", 1.5f);
		base.Invoke("WaveCompleteMessage", 3f);
		if (this.currentPhase < 6)
		{
			base.Invoke("PrepareNextPhase", 5f);
		}
		base.Invoke("CleanupWaveActorsList", 10f);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00073BD8 File Offset: 0x00071DD8
	private void CleanupWaveActorsList()
	{
		foreach (Actor actor in this.waveSkeletons.ToArray())
		{
			if (!actor.dead)
			{
				actor.Kill(DamageInfo.Default);
			}
		}
		this.waveSkeletons.Clear();
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00073C24 File Offset: 0x00071E24
	private void WaveCompleteMessage()
	{
		if (this.currentPhase < 6)
		{
			OverlayUi.ShowOverlayText("WAVE " + (this.currentPhase + 1).ToString() + " COMPLETED", 3.5f);
			return;
		}
		this.gameWon = true;
		this.EndGame();
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00073C74 File Offset: 0x00071E74
	private void Explode()
	{
		foreach (Actor actor in ActorManager.AliveActorsOnTeam(1).ToArray())
		{
			Vector3 normalized = (actor.transform.position - this.forcefieldTransform.position).normalized;
			actor.Kill(DamageInfo.Default);
		}
		this.orbGameObject.SetActive(false);
		PlayerFpParent.instance.ApplyScreenshake(15f, 5);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00073CE8 File Offset: 0x00071EE8
	public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
	{
		base.ActorDied(actor, position, wasSilentKill);
		if (this.gameOver || wasSilentKill)
		{
			return;
		}
		if (actor.team == 1)
		{
			this.waveSkeletons.Remove(actor);
			this.spawnableSkeletonActors.Enqueue(actor);
			this.skeletonKills++;
			if (this.IsPlayingPhase())
			{
				this.killCount++;
				if (this.killCount >= this.targetKillCount)
				{
					this.EndPhase();
				}
				else
				{
					this.UpdateUi();
				}
			}
		}
		else
		{
			this.favors++;
		}
		if (!actor.aiControlled)
		{
			this.gameWon = false;
			this.EndGame();
		}
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00073D90 File Offset: 0x00071F90
	private Vector3 GetSkeletonSpawnPoint()
	{
		List<Vector3> list = this.skeletonSpawnPositions[this.currentSpawnPoint];
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0000946C File Offset: 0x0000766C
	public override WeaponManager.LoadoutSet GetLoadout(Actor actor)
	{
		if (!actor.aiControlled)
		{
			return this.playerLoadout;
		}
		if (actor.team != 1)
		{
			return this.allyLoadout;
		}
		if (this.specialSkeletons.Contains(actor))
		{
			return this.skeletonSpecialLoadout;
		}
		return this.skeletonLoadout;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0006E164 File Offset: 0x0006C364
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
	}

	// Token: 0x04000BFF RID: 3071
	private const int PLAYER_TEAM = 0;

	// Token: 0x04000C00 RID: 3072
	private const int SPOOKY_TEAM = 1;

	// Token: 0x04000C01 RID: 3073
	private const float MIN_SKELETON_DEAD_TIME = 2f;

	// Token: 0x04000C02 RID: 3074
	private const float MIN_SPAWNPOINT_DISTANCE = 35f;

	// Token: 0x04000C03 RID: 3075
	private const float MAX_SPAWNPOINT_DISTANCE = 80f;

	// Token: 0x04000C04 RID: 3076
	private const float TRIGGER_PHASE_START_DISTANCE = 35f;

	// Token: 0x04000C05 RID: 3077
	private const int AMBUSH_SKELETON_COUNT = 8;

	// Token: 0x04000C06 RID: 3078
	private const float SKELETON_COUNT_MODIFIER_EASY = 0.5f;

	// Token: 0x04000C07 RID: 3079
	private const float SKELETON_COUNT_MODIFIER_MEDIUM = 0.8f;

	// Token: 0x04000C08 RID: 3080
	private static readonly int[] WAVE_SKELETON_COUNT = new int[]
	{
		10,
		15,
		21,
		27,
		30,
		35,
		50
	};

	// Token: 0x04000C09 RID: 3081
	private static readonly int[] WAVE_SKELETON_TICKETS = new int[]
	{
		15,
		30,
		45,
		60,
		80,
		110,
		150
	};

	// Token: 0x04000C0A RID: 3082
	private static readonly SpookOpsMode.PickupType[] PICKUPS = new SpookOpsMode.PickupType[]
	{
		SpookOpsMode.PickupType.None,
		SpookOpsMode.PickupType.PrimaryEarly,
		SpookOpsMode.PickupType.Secondary,
		SpookOpsMode.PickupType.PrimaryEarly,
		SpookOpsMode.PickupType.Secondary,
		SpookOpsMode.PickupType.PrimaryLate,
		SpookOpsMode.PickupType.PrimaryLate
	};

	// Token: 0x04000C0B RID: 3083
	private static readonly float[] PICKUP_AMMO_MULTIPLIER = new float[]
	{
		0.4f,
		0.6f,
		0.8f,
		0.9f,
		1.5f,
		1.1f,
		1.2f
	};

	// Token: 0x04000C0C RID: 3084
	private const int SKELETON_MAX_ACTOR_COUNT = 50;

	// Token: 0x04000C0D RID: 3085
	private const int TEAMMATES = 3;

	// Token: 0x04000C0E RID: 3086
	public WeaponManager.LoadoutSet skeletonLoadout;

	// Token: 0x04000C0F RID: 3087
	public WeaponManager.LoadoutSet skeletonSpecialLoadout;

	// Token: 0x04000C10 RID: 3088
	public WeaponManager.LoadoutSet allyLoadout;

	// Token: 0x04000C11 RID: 3089
	public WeaponManager.LoadoutSet playerLoadout;

	// Token: 0x04000C12 RID: 3090
	public WeaponManager.WeaponEntry[] secondaryPickups;

	// Token: 0x04000C13 RID: 3091
	public WeaponManager.WeaponEntry[] earlyPrimaryPickups;

	// Token: 0x04000C14 RID: 3092
	public WeaponManager.WeaponEntry[] latePrimaryPickups;

	// Token: 0x04000C15 RID: 3093
	public AiActorController.Modifier skeletonModifier;

	// Token: 0x04000C16 RID: 3094
	public ActorAccessory specialSkeletonAccessory;

	// Token: 0x04000C17 RID: 3095
	public Transform pointHighlighter;

	// Token: 0x04000C18 RID: 3096
	public Transform forcefieldTransform;

	// Token: 0x04000C19 RID: 3097
	public GameObject orbGameObject;

	// Token: 0x04000C1A RID: 3098
	public Texture2D squadOrderTexture;

	// Token: 0x04000C1B RID: 3099
	public RectTransform minimapOrbMarker;

	// Token: 0x04000C1C RID: 3100
	public GameObject weaponPickupPrefab;

	// Token: 0x04000C1D RID: 3101
	public GameObject spawnCoffinPrefab;

	// Token: 0x04000C1E RID: 3102
	public GameObject altarPrefab;

	// Token: 0x04000C1F RID: 3103
	public AnimationCurve explosionCurve;

	// Token: 0x04000C20 RID: 3104
	public AudioSource explosionAudio;

	// Token: 0x04000C21 RID: 3105
	public GameObject transportPrefab;

	// Token: 0x04000C22 RID: 3106
	public AudioSource[] skeletonWalkSounds;

	// Token: 0x04000C23 RID: 3107
	public SoundBank skeletonApproachSoundBank;

	// Token: 0x04000C24 RID: 3108
	public GameObject waveUi;

	// Token: 0x04000C25 RID: 3109
	public RawImage vignette;

	// Token: 0x04000C26 RID: 3110
	public RawImage blackout;

	// Token: 0x04000C27 RID: 3111
	public AnimationCurve introFade;

	// Token: 0x04000C28 RID: 3112
	public ParticleSystem damageParticles;

	// Token: 0x04000C29 RID: 3113
	public GameObject gameEndPanel;

	// Token: 0x04000C2A RID: 3114
	public Text gameEndTitle;

	// Token: 0x04000C2B RID: 3115
	public Text gameEndText;

	// Token: 0x04000C2C RID: 3116
	private AudioSource damageSound;

	// Token: 0x04000C2D RID: 3117
	public ActorSkin skeletonSkin;

	// Token: 0x04000C2E RID: 3118
	private Queue<Actor> spawnableSkeletonActors;

	// Token: 0x04000C2F RID: 3119
	private List<Actor> skeletonsPendingSpawn;

	// Token: 0x04000C30 RID: 3120
	private List<Actor> waveSkeletons;

	// Token: 0x04000C31 RID: 3121
	private List<NavGraph> infantryGraphs;

	// Token: 0x04000C32 RID: 3122
	private List<GameObject> activePickups;

	// Token: 0x04000C33 RID: 3123
	private List<GameObject> objectsToCleanupOnStart;

	// Token: 0x04000C34 RID: 3124
	private SpawnPoint playerSpawn;

	// Token: 0x04000C35 RID: 3125
	private Dictionary<SpawnPoint, List<Vector3>> skeletonSpawnPositions;

	// Token: 0x04000C36 RID: 3126
	private List<SpawnPoint> availableSpawnPoints;

	// Token: 0x04000C37 RID: 3127
	private SpawnPoint currentSpawnPoint;

	// Token: 0x04000C38 RID: 3128
	private SquadOrderPoint orderPoint;

	// Token: 0x04000C39 RID: 3129
	[NonSerialized]
	public int favors;

	// Token: 0x04000C3A RID: 3130
	private int skeletonCount;

	// Token: 0x04000C3B RID: 3131
	private int killCount;

	// Token: 0x04000C3C RID: 3132
	private int targetKillCount;

	// Token: 0x04000C3D RID: 3133
	private int currentPhase = -1;

	// Token: 0x04000C3E RID: 3134
	private bool phaseEnded;

	// Token: 0x04000C3F RID: 3135
	private bool awaitingNextPhase = true;

	// Token: 0x04000C40 RID: 3136
	private bool shouldSpawnJeep;

	// Token: 0x04000C41 RID: 3137
	private bool gameOver;

	// Token: 0x04000C42 RID: 3138
	private bool gameWon;

	// Token: 0x04000C43 RID: 3139
	private float skeletonCountModifier = 1f;

	// Token: 0x04000C44 RID: 3140
	private Text text;

	// Token: 0x04000C45 RID: 3141
	private Renderer forcefieldRenderer;

	// Token: 0x04000C46 RID: 3142
	private Color forcefieldColor;

	// Token: 0x04000C47 RID: 3143
	private TimedAction explosionAction = new TimedAction(3f, false);

	// Token: 0x04000C48 RID: 3144
	private TimedAction skeletonApproachAction = new TimedAction(1.5f, false);

	// Token: 0x04000C49 RID: 3145
	private Coroutine spawnSkeletonsCoroutine;

	// Token: 0x04000C4A RID: 3146
	private Seeker seeker;

	// Token: 0x04000C4B RID: 3147
	private Camera spectatorCamera;

	// Token: 0x04000C4C RID: 3148
	private TimedAction introAction = new TimedAction(12f, false);

	// Token: 0x04000C4D RID: 3149
	private TimedAction prepareWaveAction = new TimedAction(15f, false);

	// Token: 0x04000C4E RID: 3150
	private TimedAction fadeOutEndScreenAction = new TimedAction(1f, false);

	// Token: 0x04000C4F RID: 3151
	private List<Actor> specialSkeletons;

	// Token: 0x04000C50 RID: 3152
	private int altarsSpawned;

	// Token: 0x04000C51 RID: 3153
	private int skeletonKills;

	// Token: 0x04000C52 RID: 3154
	private int difficulty;

	// Token: 0x0200019F RID: 415
	private enum PickupType
	{
		// Token: 0x04000C54 RID: 3156
		None,
		// Token: 0x04000C55 RID: 3157
		Secondary,
		// Token: 0x04000C56 RID: 3158
		PrimaryEarly,
		// Token: 0x04000C57 RID: 3159
		PrimaryLate
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class SkirmishMode : GameModeBase
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x0006E60C File Offset: 0x0006C80C
	protected override void Awake()
	{
		base.Awake();
		this.allowDefaultRespawn = false;
		this.blueArrow.SetActive(false);
		this.redArrow.SetActive(false);
		GameModeParameters gameModeParameters = GameManager.GameParameters();
		if (gameModeParameters.useConquestRules)
		{
			this.reinforcementWavesRemaining[0] = Mathf.Clamp(gameModeParameters.conquestBattalions[0] - 1, 0, 5);
			this.reinforcementWavesRemaining[1] = Mathf.Clamp(gameModeParameters.conquestBattalions[1] - 1, 0, 5);
			return;
		}
		int num = gameModeParameters.gameLength;
		if (gameModeParameters.gameLength > 0)
		{
			num++;
		}
		if (gameModeParameters.gameLength == 3)
		{
			num = 5;
		}
		this.reinforcementWavesRemaining[0] = num;
		this.reinforcementWavesRemaining[1] = num;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0006E6B0 File Offset: 0x0006C8B0
	public override void StartGame()
	{
		base.StartGame();
		ActorManager.instance.CreateDefaultAiActors(false);
		foreach (VehicleSpawner vehicleSpawner in UnityEngine.Object.FindObjectsOfType<VehicleSpawner>())
		{
			if (vehicleSpawner.respawnType == VehicleSpawner.RespawnType.AfterDestroyed)
			{
				vehicleSpawner.respawnType = VehicleSpawner.RespawnType.Never;
			}
		}
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			spawnPoint.refreshTurretsOnCaptured = false;
			CapturePoint capturePoint = spawnPoint as CapturePoint;
			if (capturePoint != null)
			{
				capturePoint.captureRate = 5f;
			}
		}
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0006E738 File Offset: 0x0006C938
	public override void SetupOrders()
	{
		OrderManager.SetupDefaultOrders(true, true, true, false);
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			foreach (Order order in OrderManager.GetOrdersOfSpawnPoint(spawnPoints[i]))
			{
				if (order.type == Order.OrderType.Defend)
				{
					order.basePriority = -999999;
				}
			}
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00008E0F File Offset: 0x0000700F
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		this.SpawnEveryone();
		MinimapUi.SetPlayerCanSelectSpawnPoint(false);
		MinimapUi.SelectSpawnPoint(null);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00008E23 File Offset: 0x00007023
	private void SpawnEveryone()
	{
		base.ForceSpawnDeadActors();
		this.UpdateActorUI();
		this.gameStarted = true;
		this.lockDominationAction.Start();
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0006E7BC File Offset: 0x0006C9BC
	private void SetupFlags()
	{
		if (GameModeInfo.instance != null && GameModeInfo.instance.attackerBase != null && GameModeInfo.instance.defenderMainBase != null)
		{
			this.blueStart = GameModeInfo.instance.attackerBase;
			this.redStart = GameModeInfo.instance.defenderMainBase;
		}
		else
		{
			List<SpawnPoint> list = new List<SpawnPoint>();
			List<SpawnPoint> list2 = new List<SpawnPoint>();
			foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
			{
				if (spawnPoint.defaultOwner == SpawnPoint.Team.Blue)
				{
					list.Add(spawnPoint);
				}
				else if (spawnPoint.defaultOwner == SpawnPoint.Team.Red)
				{
					list2.Add(spawnPoint);
				}
			}
			float num = 0f;
			foreach (SpawnPoint spawnPoint2 in list)
			{
				foreach (SpawnPoint spawnPoint3 in list2)
				{
					float magnitude = (spawnPoint2.transform.position - spawnPoint3.transform.position).ToGround().magnitude;
					if (magnitude > num)
					{
						this.blueStart = spawnPoint2;
						this.redStart = spawnPoint3;
						num = magnitude;
					}
				}
			}
		}
		this.blueStart.SetOwner(0, true);
		this.redStart.SetOwner(1, true);
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00008E43 File Offset: 0x00007043
	public override void CapturePointChangedOwner(CapturePoint capturePoint, int oldOwner, bool initialOwner)
	{
		if (this.UpdateFlagUI())
		{
			base.CapturePointChangedOwner(capturePoint, oldOwner, initialOwner);
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00008E56 File Offset: 0x00007056
	private bool AllowDomination()
	{
		return this.gameStarted && this.lockDominationAction.TrueDone();
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0006E950 File Offset: 0x0006CB50
	private void Update()
	{
		this.speedupDomination[0] = (ActorManager.AliveActorsOnTeam(1).Count <= 5 && this.reinforcementWavesRemaining[1] == 0);
		this.speedupDomination[1] = (ActorManager.AliveActorsOnTeam(0).Count <= 5 && this.reinforcementWavesRemaining[0] == 0);
		for (int i = 0; i < 5; i++)
		{
			this.blueReinforcements[i].SetActive(i < this.reinforcementWavesRemaining[0]);
			this.redReinforcements[i].SetActive(i < this.reinforcementWavesRemaining[1]);
		}
		if (this.AllowDomination())
		{
			if (this.dominatingTeam == 0)
			{
				float num = this.fastDomination ? 0.0022f : 0.001f;
				if (this.speedupDomination[0])
				{
					num *= 2f;
				}
				if (this.fullDomination)
				{
					num = 0.015f;
				}
				this.domination = Mathf.MoveTowards(this.domination, 1f, Time.deltaTime * num);
			}
			else if (this.dominatingTeam == 1)
			{
				float num2 = this.fastDomination ? 0.0022f : 0.001f;
				if (this.speedupDomination[1])
				{
					num2 *= 2f;
				}
				if (this.fullDomination)
				{
					num2 = 0.015f;
				}
				this.domination = Mathf.MoveTowards(this.domination, 0f, Time.deltaTime * num2);
			}
			this.blueArrow.SetActive(this.dominatingTeam == 0);
			this.redArrow.SetActive(this.dominatingTeam == 1);
			this.padlock.SetActive(false);
			this.info.gameObject.SetActive(false);
			this.blueBar.anchorMax = new Vector2(this.domination, 1f);
			if (!VictoryUi.GameHasEnded())
			{
				if (this.domination == 1f)
				{
					this.Win(0);
				}
				else if (this.domination == 0f)
				{
					this.Win(1);
				}
			}
		}
		else if (this.gameStarted)
		{
			this.info.text = "UNLOCK IN " + Mathf.CeilToInt(this.lockDominationAction.Remaining()).ToString();
		}
		float num3 = 0.02f * Mathf.Sin(Time.time * 5f);
		this.arrowParent.anchorMin = new Vector2(num3, 0f);
		this.arrowParent.anchorMax = new Vector2(1f + num3, 1f);
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00008E6D File Offset: 0x0000706D
	public float GetNormalizedDominationForTeam(int team)
	{
		if (team != 0)
		{
			return 1f - this.domination;
		}
		return this.domination;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
	private void LateUpdate()
	{
		if (!this.takeOverTargetAction.TrueDone())
		{
			Camera tpCamera = FpsActorController.instance.tpCamera;
			float num = this.takeOverTargetAction.Elapsed() / 2.5f;
			float t = (this.takeOverTargetAction.Elapsed() - 3f) / 1f;
			float num2 = Mathf.Clamp01(this.takeOverTargetAction.Elapsed() - 1.6f);
			Quaternion quaternion = Quaternion.LookRotation(this.takeOverTarget.controller.FacingDirection().ToGround()) * Quaternion.Euler(10f, 0f, 0f);
			float d;
			float fieldOfView;
			if (num < 1f)
			{
				num = Mathf.Clamp01(num);
				d = Mathf.Lerp(50f, 8f, num);
				fieldOfView = Mathf.Lerp(110f, 50f, num);
				tpCamera.transform.position += UnityEngine.Random.insideUnitSphere * Mathf.Clamp01(1f - 2f * num);
			}
			else
			{
				t = Mathf.SmoothStep(0f, 1f, t);
				d = Mathf.Lerp(8f, 3f, t);
				fieldOfView = Mathf.Lerp(50f, 10f, t);
				quaternion = Quaternion.Slerp(quaternion, Quaternion.LookRotation(this.takeOverTarget.controller.FacingDirection()), t);
			}
			tpCamera.transform.rotation = Quaternion.Slerp(tpCamera.transform.rotation, quaternion, num2 * 2f * Time.deltaTime);
			Vector3 b = this.takeOverTarget.CenterPosition() - tpCamera.transform.forward * d + new Vector3(0f, 0.5f, 0f);
			tpCamera.transform.position = Vector3.Lerp(tpCamera.transform.position, b, 5f * Time.deltaTime);
			tpCamera.fieldOfView = fieldOfView;
		}
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0006EDB8 File Offset: 0x0006CFB8
	private bool UpdateFlagUI()
	{
		int num = 0;
		int num2 = 0;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (!spawnPoint.isGhostSpawn)
			{
				if (spawnPoint.owner == 0)
				{
					num++;
				}
				else if (spawnPoint.owner == 1)
				{
					num2++;
				}
			}
		}
		this.blueFlags.text = num.ToString();
		this.redFlags.text = num2.ToString();
		int num3 = -1;
		if (num > num2)
		{
			num3 = 0;
			this.fullDomination = (num == ActorManager.instance.spawnPoints.Length);
		}
		else if (num < num2)
		{
			num3 = 1;
			this.fullDomination = (num2 == ActorManager.instance.spawnPoints.Length);
		}
		this.fastDomination = (Mathf.Abs(num - num2) >= 2);
		if (num3 == this.dominatingTeam || num3 == -1)
		{
			this.dominatingTeam = num3;
			return true;
		}
		if (this.AllowDomination())
		{
			OverlayUi.ShowOverlayText(GameManager.instance.GetRichTextColorTeamName(num3) + " IS DOMINATING", 3.5f);
		}
		this.dominatingTeam = num3;
		return false;
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0006EECC File Offset: 0x0006D0CC
	public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
	{
		if (!wasSilentKill)
		{
			this.UpdateActorUI();
			this.CheckTeamWipeVictory();
			if (!this.spawningReinforcements[actor.team] && this.reinforcementWavesRemaining[actor.team] > 0 && (float)ActorManager.AliveActorsOnTeam(actor.team).Count <= (float)ActorManager.ActorsOnTeam(actor.team).Count / 2.5f)
			{
				int num = 1 - actor.team;
				if (this.reinforcementWavesRemaining[num] != 0)
				{
					if (ActorManager.AliveActorsOnTeam(actor.team).Count < ActorManager.AliveActorsOnTeam(num).Count || (this.GetNormalizedDominationForTeam(actor.team) < 0.25f && this.dominatingTeam != actor.team))
					{
						this.SpawnReinforcementWave(actor.team);
					}
				}
				else
				{
					this.SpawnReinforcementWave(actor.team);
				}
			}
		}
		if (!actor.aiControlled)
		{
			if (!this.spawningReinforcements[actor.team])
			{
				this.takeOverCoroutine = base.StartCoroutine(this.PlayerTakeOverBot());
				return;
			}
			base.StartCoroutine(this.ActivateAirdropCamera());
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00008E85 File Offset: 0x00007085
	private IEnumerator PlayerTakeOverBot()
	{
		if (GameManager.gameOver)
		{
			yield break;
		}
		yield return new WaitForSeconds(4f);
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 1f, Color.black);
		yield return new WaitForSeconds(1f);
		this.audioSource.PlayOneShot(this.satelliteAudio);
		yield return new WaitForSeconds(0.5f);
		List<Actor> list = ActorManager.AliveActorsOnTeam(GameManager.PlayerTeam());
		KillCamera.Hide();
		float num = -999999f;
		Actor x = null;
		foreach (Actor actor in list)
		{
			AiActorController aiActorController = (AiActorController)actor.controller;
			float num2 = actor.health;
			if (actor.fallenOver || actor.IsSwimming() || actor.IsOnLadder())
			{
				num2 -= 100f;
			}
			if (aiActorController.squad != null && aiActorController.squad.order != null && aiActorController.squad.order.type == Order.OrderType.Attack)
			{
				num2 += 100f;
			}
			if (actor.IsSeated())
			{
				if (actor.IsDriver())
				{
					if (actor.seat.vehicle.IsAircraft())
					{
						num2 -= 200f;
					}
					else
					{
						num2 -= 40f;
					}
				}
				else if (actor.seat.vehicle.IsAircraft())
				{
					num2 -= 60f;
				}
				else
				{
					num2 -= 150f;
				}
			}
			else if (aiActorController.HasTargetVehicle())
			{
				num2 -= 300f;
			}
			if (actor.IsHighlighted())
			{
				num2 -= 20f;
			}
			if (num2 > num)
			{
				num = num2;
				x = actor;
			}
		}
		if (x != null)
		{
			FpsActorController.TakeThirdPersonCameraControl();
			this.takeOverTarget = x;
			this.takeOverTarget.isInvulnerable = true;
			this.takeOverTargetAction.Start();
			((AiActorController)this.takeOverTarget.controller).FreezeFacingDirection();
			FpsActorController.instance.tpCamera.transform.position = this.takeOverTarget.CenterPosition() + new Vector3(0f, 50f, 0f);
			FpsActorController.instance.tpCamera.transform.rotation = Quaternion.LookRotation(Vector3.down, this.takeOverTarget.controller.FacingDirection());
			EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.8f, Color.white);
			yield return new WaitForSeconds(3.5f);
			EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 0.8f, Color.black);
			yield return new WaitForSeconds(0.19999999f);
			this.audioSource.PlayOneShot(this.controlBotAudio);
			yield return new WaitForSeconds(0.3f);
			if (!this.takeOverTarget.dead)
			{
				FpsActorController.ReleaseThirdPersonCameraControl();
				this.takeOverTarget.InstantlyReloadCarriedWeapons();
				this.takeOverTarget.isInvulnerable = false;
				ActorManager.PlayerTakeOverBot(this.takeOverTarget);
				this.takeOverCoroutine = null;
			}
			else
			{
				EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.5f, Color.black);
				this.takeOverCoroutine = base.StartCoroutine(this.PlayerTakeOverBot());
			}
			yield break;
		}
		FpsActorController.ReleaseThirdPersonCameraControl();
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 0.5f, Color.black);
		yield break;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00008E94 File Offset: 0x00007094
	private IEnumerator ActivateAirdropCamera()
	{
		yield return new WaitForSeconds(2f);
		if (FpsActorController.instance.actor.dead && this.spawningReinforcements[GameManager.PlayerTeam()] && this.playerAirdopAnimation != null)
		{
			EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 1f, Color.black);
			this.playerAirdopAnimation.ActivateCamera();
		}
		yield break;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00008EA3 File Offset: 0x000070A3
	private void SpawnReinforcementWave(int team)
	{
		base.StartCoroutine(this.SpawnReinforcementsCoroutine(team));
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00008EB3 File Offset: 0x000070B3
	private IEnumerator SpawnReinforcementsCoroutine(int team)
	{
		if (this.spawningReinforcements[team])
		{
			yield break;
		}
		this.spawningReinforcements[team] = true;
		SpawnPoint targetSpawnPoint = null;
		int num = -1;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.vehicleFilter.air || spawnPoint.vehicleFilter.airFastmover)
			{
				int num2 = 0;
				if (spawnPoint.owner == team)
				{
					num2 = spawnPoint.nLandConnections;
				}
				using (List<SpawnPoint>.Enumerator enumerator = spawnPoint.outgoingNeighbors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.owner != team)
						{
							num2++;
						}
					}
				}
				if (num2 > num)
				{
					targetSpawnPoint = spawnPoint;
					num = num2;
				}
			}
		}
		if (targetSpawnPoint == null)
		{
			this.spawningReinforcements[team] = false;
			yield break;
		}
		Vector3 direction = (team == 0) ? Vector3.right : Vector3.left;
		Quaternion rotation = Quaternion.LookRotation(direction);
		AirdropAnimation component = UnityEngine.Object.Instantiate<GameObject>(this.airdropPrefab, targetSpawnPoint.transform.position, rotation).GetComponent<AirdropAnimation>();
		if (team == GameManager.PlayerTeam())
		{
			this.playerAirdopAnimation = component;
			if (this.takeOverCoroutine != null)
			{
				base.StopCoroutine(this.takeOverCoroutine);
				this.takeOverTargetAction.Stop();
				this.takeOverCoroutine = null;
				EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 1f, Color.black);
				base.StartCoroutine(this.ActivateAirdropCamera());
			}
		}
		OverlayUi.ShowOverlayText("INCOMING " + GameManager.instance.GetRichTextColorTeamName(team) + " REINFORCEMENTS", 3.5f);
		yield return new WaitForSeconds(8.5f);
		if (team == GameManager.PlayerTeam() && FpsActorController.instance.actor.dead)
		{
			EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 1f, Color.black);
		}
		yield return new WaitForSeconds(1.5f);
		OverlayUi.ShowOverlayText(GameManager.instance.GetRichTextColorTeamName(team) + " REINFORCEMENTS HAVE ARRIVED", 3.5f);
		this.reinforcementWavesRemaining[team]--;
		List<Actor> list = ActorManager.ActorsOnTeam(team);
		int num3 = list.Count / 2;
		int num4 = num3;
		Vector3 a = targetSpawnPoint.transform.position + new Vector3(0f, 100f, 0f);
		a -= direction * 40f / 2f;
		Vector3 b = a + direction * 40f;
		List<Actor> list2 = new List<Actor>();
		this.spawningReinforcements[team] = false;
		Actor actor = FpsActorController.instance.actor;
		if (actor.dead && actor.team == team)
		{
			actor.SpawnAt((a + b) * 0.5f + new Vector3(0f, 2f, 0f), rotation, null);
			list2.Add(actor);
			num4--;
			actor.KnockOver(direction * 10f);
		}
		foreach (Actor actor2 in list)
		{
			float t = 1f - (float)num4 / (float)num3;
			if (actor2.dead)
			{
				actor2.SpawnAt(Vector3.Lerp(a, b, t), rotation, null);
				actor2.KnockOver(direction * 130f);
				list2.Add(actor2);
				num4--;
				if (num4 == 0)
				{
					break;
				}
			}
		}
		foreach (Squad squad in base.CreateAiSquads(list2, 3, targetSpawnPoint))
		{
			squad.AssignOrder(OrderManager.GetHighestPriorityOrderForExistingSquad(squad));
		}
		this.UpdateActorUI();
		yield break;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0006EFDC File Offset: 0x0006D1DC
	public override void Win(int winningTeam)
	{
		if (this.takeOverCoroutine != null)
		{
			base.StopCoroutine(this.takeOverCoroutine);
			this.takeOverCoroutine = null;
		}
		VictoryUi.EndGame(winningTeam, true);
		BattleResult.SetWinner(winningTeam);
		BattleResult.AppendBattalionResult(this.reinforcementWavesRemaining[0] + 1, this.reinforcementWavesRemaining[1] + 1);
		BattleResult.latest.remainingBattalions[1 - winningTeam] = 0;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0006F03C File Offset: 0x0006D23C
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
		BattleResult.AppendBattalionResult(this.reinforcementWavesRemaining[0] + 1, this.reinforcementWavesRemaining[1] + 1);
		BattleResult.latest.remainingBattalions[num] = 0;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00008EC9 File Offset: 0x000070C9
	public override void OnSquadSpawned(Squad squad)
	{
		base.OnSquadSpawned(squad);
		this.UpdateActorUI();
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0006F080 File Offset: 0x0006D280
	private void UpdateActorUI()
	{
		int count = ActorManager.AliveActorsOnTeam(0).Count;
		int count2 = ActorManager.AliveActorsOnTeam(1).Count;
		this.blueInfantry.text = count.ToString();
		this.redInfantry.text = count2.ToString();
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0006F0CC File Offset: 0x0006D2CC
	private void CheckTeamWipeVictory()
	{
		int count = ActorManager.AliveActorsOnTeam(0).Count;
		int count2 = ActorManager.AliveActorsOnTeam(1).Count;
		if (!VictoryUi.GameHasEnded())
		{
			if (count == 0 && !base.IsDeferredSpawnRunning(0))
			{
				this.Win(1);
				return;
			}
			if (count2 == 0 && !base.IsDeferredSpawnRunning(1))
			{
				this.Win(0);
			}
		}
	}

	// Token: 0x04000B38 RID: 2872
	private const float DOMINATION_RATE_SLOW = 0.001f;

	// Token: 0x04000B39 RID: 2873
	private const float DOMINATION_RATE_FAST = 0.0022f;

	// Token: 0x04000B3A RID: 2874
	private const float DOMINATION_RATE_FULL = 0.015f;

	// Token: 0x04000B3B RID: 2875
	private const float DOMINATION_LOW_ENEMY_COUNT_MULTIPLIER = 2f;

	// Token: 0x04000B3C RID: 2876
	private const int DOMINATION_LOW_ENEMY_COUNT_THRESHOLD = 5;

	// Token: 0x04000B3D RID: 2877
	private const float TAKE_OVER_SEQUENCE_TIME = 4f;

	// Token: 0x04000B3E RID: 2878
	private const float TAKE_OVER_SATELLITE_TIME = 2.5f;

	// Token: 0x04000B3F RID: 2879
	private const float TAKE_OVER_ZOOM_IN_TIME = 1f;

	// Token: 0x04000B40 RID: 2880
	private const float TAKE_OVER_SPIN_START_TIME = 1.6f;

	// Token: 0x04000B41 RID: 2881
	private const float TAKE_OVER_START_DISTANCE = 50f;

	// Token: 0x04000B42 RID: 2882
	private const float AIRDROP_SPAWN_ACTOR_SPAWN_RANGE = 40f;

	// Token: 0x04000B43 RID: 2883
	private const float AIRDROP_SPAWN_ACTOR_SPAWN_HEIGHT = 100f;

	// Token: 0x04000B44 RID: 2884
	private SpawnPoint blueStart;

	// Token: 0x04000B45 RID: 2885
	private SpawnPoint redStart;

	// Token: 0x04000B46 RID: 2886
	public Text blueInfantry;

	// Token: 0x04000B47 RID: 2887
	public Text redInfantry;

	// Token: 0x04000B48 RID: 2888
	public Text blueFlags;

	// Token: 0x04000B49 RID: 2889
	public Text redFlags;

	// Token: 0x04000B4A RID: 2890
	public Text info;

	// Token: 0x04000B4B RID: 2891
	public GameObject blueArrow;

	// Token: 0x04000B4C RID: 2892
	public GameObject redArrow;

	// Token: 0x04000B4D RID: 2893
	public GameObject padlock;

	// Token: 0x04000B4E RID: 2894
	public RectTransform arrowParent;

	// Token: 0x04000B4F RID: 2895
	public RectTransform blueBar;

	// Token: 0x04000B50 RID: 2896
	public AudioSource audioSource;

	// Token: 0x04000B51 RID: 2897
	public AudioClip satelliteAudio;

	// Token: 0x04000B52 RID: 2898
	public AudioClip controlBotAudio;

	// Token: 0x04000B53 RID: 2899
	public GameObject[] blueReinforcements;

	// Token: 0x04000B54 RID: 2900
	public GameObject[] redReinforcements;

	// Token: 0x04000B55 RID: 2901
	public GameObject airdropPrefab;

	// Token: 0x04000B56 RID: 2902
	private int dominatingTeam = -1;

	// Token: 0x04000B57 RID: 2903
	private float domination = 0.5f;

	// Token: 0x04000B58 RID: 2904
	private bool fastDomination;

	// Token: 0x04000B59 RID: 2905
	private bool fullDomination;

	// Token: 0x04000B5A RID: 2906
	private bool gameStarted;

	// Token: 0x04000B5B RID: 2907
	private TimedAction lockDominationAction = new TimedAction(180f, false);

	// Token: 0x04000B5C RID: 2908
	private TimedAction takeOverTargetAction = new TimedAction(4.2f, false);

	// Token: 0x04000B5D RID: 2909
	private Actor takeOverTarget;

	// Token: 0x04000B5E RID: 2910
	private bool[] speedupDomination = new bool[2];

	// Token: 0x04000B5F RID: 2911
	private int[] reinforcementWavesRemaining = new int[2];

	// Token: 0x04000B60 RID: 2912
	private bool[] spawningReinforcements = new bool[2];

	// Token: 0x04000B61 RID: 2913
	private Coroutine takeOverCoroutine;

	// Token: 0x04000B62 RID: 2914
	private AirdropAnimation playerAirdopAnimation;
}

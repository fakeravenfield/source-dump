using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lua;
using Pathfinding;
using Ravenfield.ScriptedMission;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x02000069 RID: 105
public class ActorManager : MonoBehaviour
{
	// Token: 0x060002B5 RID: 693 RVA: 0x00049928 File Offset: 0x00047B28
	public static void Register(Actor actor)
	{
		ActorManager.instance.actors.Add(actor);
		AiActorController aiActorController = actor.controller as AiActorController;
		ActorManager.instance.aiActorControllers.Add(aiActorController);
		ActorManager actorManager = ActorManager.instance;
		int num = actorManager.nextActorIndex;
		actorManager.nextActorIndex = num + 1;
		actor.actorIndex = num;
		ActorManager.instance.actorData.Add(default(ActorData));
		if (!actor.aiControlled)
		{
			ActorManager.instance.player = actor;
			ActorManager.instance.playerActorIndex = actor.actorIndex;
		}
		if (actor.team == 0 || actor.team == 1)
		{
			ActorManager.instance.actorsOnTeam[actor.team].Add(actor);
			int[] array = ActorManager.instance.nextActorIndexTeam;
			int team = actor.team;
			num = array[team];
			array[team] = num + 1;
			int num2 = num;
			actor.teamActorIndex = num2;
			if (actor.aiControlled)
			{
				num2 += ActorManager.instance.randomTeamSeed[actor.team];
				if (num2 % AiActorController.PARAMETERS.SKILL_VETERAN_MOD == 0)
				{
					aiActorController.skill = AiActorController.SkillLevel.Veteran;
				}
				else if (num2 % AiActorController.PARAMETERS.SKILL_NORMAL_MOD == 0)
				{
					aiActorController.skill = AiActorController.SkillLevel.Normal;
				}
				else
				{
					aiActorController.skill = AiActorController.SkillLevel.Beginner;
				}
			}
		}
		ActorManager.instance.ActorNumberChanged();
		RavenscriptManager.events.onActorCreated.Invoke(actor);
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00003C81 File Offset: 0x00001E81
	public static void Drop(Actor actor)
	{
		ActorManager.instance.actors.Remove(actor);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00049A74 File Offset: 0x00047C74
	public static int RegisterTrigger(TriggerVolume volume)
	{
		int count = ActorManager.instance.activeTriggers.Count;
		ActorManager.instance.activeTriggers.Add(volume.data);
		ActorManager.instance.triggerStates.Add(default(TriggerVolumeActorState));
		return count;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00049AC0 File Offset: 0x00047CC0
	public static void RegisterDamageZone(DamageZone damageZone)
	{
		try
		{
			ActorManager.instance.damageZones.Add(damageZone);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00049AF8 File Offset: 0x00047CF8
	public static void DropDamageZone(DamageZone damageZone)
	{
		try
		{
			ActorManager.instance.damageZones.Remove(damageZone);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00003C94 File Offset: 0x00001E94
	public static void RegisterSpeedLimitZone(SpeedLimitZone speedLimitZone)
	{
		ActorManager.instance.speedLimitZones.Add(speedLimitZone);
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00049B30 File Offset: 0x00047D30
	private void Awake()
	{
		ActorManager.instance = this;
		ActorMaterials.EMISSION_MATERIAL_PROPERTY_ID = Shader.PropertyToID("_EmissionColor");
		ActorMaterials.EMISSION_MAP_MATERIAL_PROPERTY_ID = Shader.PropertyToID("_EmissionMap");
		AiActorController.SetupParameters();
		SceneManager.sceneLoaded += this.OnLevelLoaded;
		this.smokeTargets = new List<ActorManager.SmokeTarget>[2];
		this.smokeTargets[0] = new List<ActorManager.SmokeTarget>();
		this.smokeTargets[1] = new List<ActorManager.SmokeTarget>();
		this.smokeTargetUpdateIndex = new int[2];
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00049BAC File Offset: 0x00047DAC
	public static void EnableNightVisionGlow()
	{
		if (ActorManager.instance.actorMaterials == null)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			ActorManager.instance.actorMaterials[i].SetGlowColor(NightVision.GLOW_EMISSION_MAP_COLOR, NightVision.GLOW_SOLID_COLOR);
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00049BF0 File Offset: 0x00047DF0
	public static void DisableNightVisionGlow()
	{
		if (ActorManager.instance.actorMaterials == null)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			ActorManager.instance.actorMaterials[i].ResetGlowColor();
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00049C28 File Offset: 0x00047E28
	public static bool RegisterSmokeTarget(Vector3 position, int team, float lifetime)
	{
		List<ActorManager.SmokeTarget> list = ActorManager.instance.smokeTargets[team];
		foreach (ActorManager.SmokeTarget smokeTarget in list)
		{
			if (!smokeTarget.IsExpired() && Vector3.Distance(smokeTarget.position, position) < 50f)
			{
				return false;
			}
		}
		list.Add(new ActorManager.SmokeTarget(position, team, Time.time + lifetime));
		return true;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00049CB4 File Offset: 0x00047EB4
	public static bool GetSmokeTargetInRange(int team, Vector3 position, float range, out int i, out Vector3 targetPosition)
	{
		targetPosition = Vector3.zero;
		i = 0;
		for (i = 0; i < ActorManager.instance.smokeTargets[team].Count; i++)
		{
			if (!ActorManager.instance.smokeTargets[team][i].deployed)
			{
				targetPosition = ActorManager.instance.smokeTargets[team][i].position;
				if (Vector3.Distance(position, targetPosition) < range)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00003CA6 File Offset: 0x00001EA6
	public static ActorManager.SmokeTarget CompleteSmokeTarget(int team, int index)
	{
		ActorManager.SmokeTarget smokeTarget = ActorManager.instance.smokeTargets[team][index];
		smokeTarget.deployed = true;
		return smokeTarget;
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00049D3C File Offset: 0x00047F3C
	private static ActorNameSet LoadSteamFriendsActorNameSet()
	{
		ActorNameSet result;
		try
		{
			if (!GameManager.instance.steamworks.isInitialized)
			{
				throw new Exception("Steamworks is not initialized");
			}
			CSteamID[] allFriends = GameManager.instance.steamworks.GetAllFriends();
			if (allFriends.Length == 0)
			{
				throw new Exception("Got friend array of length 0");
			}
			ActorNameSet actorNameSet = ScriptableObject.CreateInstance<ActorNameSet>();
			actorNameSet.names = new List<string>();
			bool flag = true;
			for (int i = 0; i < allFriends.Length; i++)
			{
				string steamIdNickName = GameManager.instance.steamworks.GetSteamIdNickName(allFriends[i]);
				actorNameSet.names.Add(steamIdNickName);
				if (!string.IsNullOrEmpty(steamIdNickName))
				{
					flag = false;
				}
			}
			if (flag)
			{
				throw new Exception("All returned names are empty");
			}
			result = actorNameSet;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Could not load steam friend bot names: ");
			Debug.LogWarning(ex.Message);
			result = ActorManager.instance.defaultActorNameSet.Clone();
		}
		return result;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00049E24 File Offset: 0x00048024
	private static ActorNameSet LoadCustomActorNameSet()
	{
		ActorNameSet result;
		try
		{
			StreamReader streamReader = File.OpenText(Application.dataPath + "/botnames.txt");
			ActorNameSet actorNameSet = ScriptableObject.CreateInstance<ActorNameSet>();
			actorNameSet.names = new List<string>();
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (!string.IsNullOrEmpty(text) && text != " ")
				{
					actorNameSet.names.Add(text);
				}
			}
			if (actorNameSet.names.Count == 0)
			{
				throw new Exception("Custom names length is 0");
			}
			result = actorNameSet;
		}
		catch (Exception exception)
		{
			Debug.LogError("Could not load custom bot names: ");
			Debug.LogException(exception);
			ActorNameSet actorNameSet2 = ScriptableObject.CreateInstance<ActorNameSet>();
			actorNameSet2.names = new List<string>(1);
			actorNameSet2.names.Add("NO CUSTOM NAMES :(");
			result = actorNameSet2;
		}
		return result;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00049EEC File Offset: 0x000480EC
	public void LoadActorNameSet()
	{
		int dropdown = Options.GetDropdown(OptionDropdown.Id.BotNames);
		if (dropdown == 1)
		{
			this.actorNameSet = ActorManager.LoadSteamFriendsActorNameSet();
			return;
		}
		if (dropdown == 2)
		{
			this.actorNameSet = ActorManager.LoadCustomActorNameSet();
			return;
		}
		this.actorNameSet = this.defaultActorNameSet.Clone();
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00003CC1 File Offset: 0x00001EC1
	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= this.OnLevelLoaded;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00049F34 File Offset: 0x00048134
	private void Update()
	{
		if (!this.isReady)
		{
			return;
		}
		if (this.aiActorControllers != null)
		{
			this.UpdateAI();
		}
		if (this.spawnPoints != null)
		{
			this.UpdateCapturePoints();
		}
		if (this.activeTriggers != null)
		{
			this.UpdateTriggers();
		}
		if (this.vehicles != null)
		{
			this.UpdateVehicleInteractions();
		}
		if (this.actorsCanSeeEachOther != null)
		{
			this.UpdateActorInteractions();
		}
		if (this.squadsOnTeam != null)
		{
			this.UpdateSquadInteractions();
		}
		if (!GameManager.IsSpectating())
		{
			this.UpdateFootsteps();
		}
		this.UpdateSmokeTargets();
		if (this.damageZones != null)
		{
			for (int i = 0; i < this.damageZones.Count; i++)
			{
				DamageZone damageZone = this.damageZones[i];
				Matrix4x4 worldToLocalMatrix = damageZone.transform.worldToLocalMatrix;
				for (int j = 0; j < this.actors.Count; j++)
				{
					Actor actor = this.actors[j];
					if (!actor.dead)
					{
						Vector3 vector = worldToLocalMatrix.MultiplyPoint(actor.CenterPosition());
						if (Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f)
						{
							DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.DamageZone, null, null)
							{
								healthDamage = damageZone.damagePerSecond * Time.deltaTime,
								balanceDamage = damageZone.balanceDamagePerSecond * Time.deltaTime,
								isPiercing = true,
								point = actor.CenterPosition()
							};
							actor.Damage(info);
						}
					}
				}
			}
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0004A0C4 File Offset: 0x000482C4
	private void UpdateAI()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		int num = this.lastAiUpdateEnd;
		int count = this.aiActorControllers.Count;
		float num2 = Time.smoothDeltaTime / 0.2f;
		int num3 = Mathf.Min(50, Mathf.CeilToInt(num2 * (float)count));
		int num4 = num + num3;
		this.lastAiUpdateEnd = num4 % count;
		int num5 = num3;
		for (int i = 0; i < count; i++)
		{
			int num6 = (num + i) % count;
			if (num6 != ActorManager.instance.playerActorIndex && !this.actorData[num6].dead)
			{
				this.aiActorControllers[num6].TickAiCoroutines();
				num5--;
				this.lastAiUpdateEnd = (num6 + 1) % count;
				if (num5 <= 0)
				{
					break;
				}
			}
		}
		this.ai_ticked = num3 - num5;
		this.ai_maxTickCount = num3;
		this.ai_tickFillRatio = num2;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00003CD4 File Offset: 0x00001ED4
	public static bool AITickIsThrottled()
	{
		return ActorManager.instance.ai_ticked == 50;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0004A19C File Offset: 0x0004839C
	public static string GetAITickStatusString()
	{
		string result;
		try
		{
			result = string.Format("AI Ticks Per Frame: {0}/{1}, Actor Count: {3}, Frame Ratio: {2:P1}", new object[]
			{
				ActorManager.instance.ai_ticked,
				ActorManager.instance.ai_maxTickCount,
				ActorManager.instance.ai_tickFillRatio,
				ActorManager.instance.aiActorControllers.Count
			});
		}
		catch (Exception)
		{
			result = string.Format("NO AI TICKS", Array.Empty<object>());
		}
		return result;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0004A230 File Offset: 0x00048430
	private void UpdateCapturePoints()
	{
		while (this.currentCapturePointUpdateIndex < this.spawnPoints.Length)
		{
			CapturePoint capturePoint = this.spawnPoints[this.currentCapturePointUpdateIndex] as CapturePoint;
			this.currentCapturePointUpdateIndex++;
			if (capturePoint != null && capturePoint.gameObject.activeInHierarchy)
			{
				this.UpdateCaptureStatus(capturePoint);
				return;
			}
		}
		if (this.currentCapturePointUpdateIndex >= this.spawnPoints.Length)
		{
			this.currentCapturePointUpdateIndex = 0;
		}
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0004A2A8 File Offset: 0x000484A8
	private void UpdateCaptureStatus(CapturePoint cp)
	{
		ActorManager.attackingActors.Clear();
		int team0Count;
		int team1Count;
		if (cp.captureVolume != null)
		{
			this.UpdateCapturePointVolume(cp, out team0Count, out team1Count);
		}
		else
		{
			this.UpdateCapturePointCircle(cp, out team0Count, out team1Count);
		}
		cp.UpdateOwner(ActorManager.attackingActors, team0Count, team1Count);
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0004A2F4 File Offset: 0x000484F4
	private void UpdateCapturePointVolume(CapturePoint cp, out int team0Count, out int team1Count)
	{
		TriggerVolume.RuntimeData data = cp.captureVolume.data;
		int owner = cp.owner;
		int pendingOwner = cp.pendingOwner;
		team0Count = 0;
		team1Count = 0;
		for (int i = 0; i < this.actorData.Count; i++)
		{
			ActorData actorData = this.actorData[i];
			if (actorData.canCaptureFlag && data.PointIsInVolume(actorData.position))
			{
				actorData.SetCurrentPoint(cp);
				this.actorData[i] = actorData;
				if (actorData.team == 0)
				{
					team0Count++;
				}
				else
				{
					team1Count++;
				}
				this.RegisterPresentActor(owner, pendingOwner, i, actorData.team);
			}
		}
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0004A3A0 File Offset: 0x000485A0
	private void UpdateCapturePointCircle(CapturePoint cp, out int team0Count, out int team1Count)
	{
		team0Count = 0;
		team1Count = 0;
		Vector3 position = cp.transform.position;
		float num = position.y - cp.captureFloor;
		float num2 = position.y + cp.captureCeiling;
		float num3 = cp.captureRange * cp.captureRange;
		Vector2 a = position.ToVector2XZ();
		int owner = cp.owner;
		int pendingOwner = cp.pendingOwner;
		for (int i = 0; i < this.actorData.Count; i++)
		{
			ActorData actorData = this.actorData[i];
			Vector2 b = actorData.position.ToVector2XZ();
			if (actorData.canCaptureFlag && actorData.position.y >= num && actorData.position.y <= num2 && Vector2.SqrMagnitude(a - b) < num3)
			{
				actorData.SetCurrentPoint(cp);
				this.actorData[i] = actorData;
				if (actorData.team == 0)
				{
					team0Count++;
				}
				else
				{
					team1Count++;
				}
				this.RegisterPresentActor(owner, pendingOwner, i, actorData.team);
			}
		}
	}

	// Token: 0x060002CD RID: 717 RVA: 0x00003CE4 File Offset: 0x00001EE4
	private void RegisterPresentActor(int currentOwner, int pendingOwner, int actorIndex, int actorTeam)
	{
		if ((currentOwner >= 0 && actorTeam != currentOwner) || (currentOwner < 0 && pendingOwner == actorTeam))
		{
			ActorManager.attackingActors.Add(actorIndex);
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0004A4B8 File Offset: 0x000486B8
	private void UpdateTriggers()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		for (int i = 0; i < this.activeTriggers.Count; i++)
		{
			if (this.activeTriggers[i].actorQueryActive)
			{
				this.QueryActorTrigger(i);
			}
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0004A504 File Offset: 0x00048704
	private void QueryActorTrigger(int triggerIndex)
	{
		int num = Mathf.Min(this.actors.Count, 256);
		TriggerVolumeActorState value = this.triggerStates[triggerIndex];
		value.ClearTeamCounters();
		for (int i = 0; i < num; i++)
		{
			Actor actor = this.actors[i];
			bool flag = !actor.dead && this.activeTriggers[triggerIndex].PointIsInVolume(actor.Position());
			bool flag2 = value.UpdateActorFlag(i, flag, actor.team == 0);
			if (!actor.aiControlled && flag2)
			{
				if (flag)
				{
					Debug.Log("Player entered volume");
				}
				else
				{
					Debug.Log("Player exited volume");
				}
			}
		}
		this.triggerStates[triggerIndex] = value;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0004A5C8 File Offset: 0x000487C8
	private void UpdateSmokeTargets()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.smokeTargets[i].Count > 0)
			{
				this.smokeTargetUpdateIndex[i] = (this.smokeTargetUpdateIndex[i] + 1) % this.smokeTargets[i].Count;
				if (this.smokeTargets[i][this.smokeTargetUpdateIndex[i]].IsExpired())
				{
					this.smokeTargets[i].RemoveAt(this.smokeTargetUpdateIndex[i]);
					this.smokeTargetUpdateIndex[i]--;
				}
			}
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00003D03 File Offset: 0x00001F03
	private bool ActorCanBeHeard(Actor actor)
	{
		return actor.IsHighlighted() || (actor.IsSeated() && !actor.seat.vehicle.isTurret);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0004A654 File Offset: 0x00048854
	private void UpdateActorInteractions()
	{
		int num = 0;
		bool flag = false;
		float num2 = 0f;
		int[] array = new int[]
		{
			this.nextActorIndexTeam[0],
			this.nextActorIndexTeam[1]
		};
		int num3 = this.nextActorIndex;
		while (this.interactionIteratorTeam0 < array[0])
		{
			Actor actor = this.actorsOnTeam[0][this.interactionIteratorTeam0];
			bool flag2 = this.ActorCanBeHeard(actor);
			while (this.interactionIteratorTeam1 < array[1])
			{
				Actor actor2 = this.actorsOnTeam[1][this.interactionIteratorTeam1];
				bool flag3 = this.ActorCanBeHeard(actor2);
				bool flag4;
				if (actor.dead || actor2.dead)
				{
					flag4 = false;
				}
				else
				{
					flag4 = ActorManager.DoCanSeeCheck(actor, actor2, out num2);
					this.actorsDistance[actor.teamActorIndex, actor2.teamActorIndex] = num2;
					if (num2 <= 50f)
					{
						if (flag2)
						{
							this.actorCanHearEnemy[actor2.actorIndex] = true;
						}
						if (flag3)
						{
							this.actorCanHearEnemy[actor.actorIndex] = true;
						}
					}
					if (actor.closestEnemyDistancePreShift > num2)
					{
						actor.closestEnemyDistancePreShift = num2;
					}
					if (actor2.closestEnemyDistancePreShift > num2)
					{
						actor2.closestEnemyDistancePreShift = num2;
					}
					num++;
					if (num > this.interactionUpdatesPerFrame)
					{
						flag = true;
					}
				}
				this.actorsCanSeeEachOther[actor.teamActorIndex, actor2.teamActorIndex] = flag4;
				this.UpdateSight(actor, actor2, num2, flag4);
				this.UpdateSight(actor2, actor, num2, flag4);
				this.interactionIteratorTeam1++;
				if (flag && this.interactionIteratorTeam1 < array[1])
				{
					return;
				}
			}
			this.interactionIteratorTeam1 = 0;
			this.interactionIteratorTeam0++;
			if (flag && this.interactionIteratorTeam0 < array[0])
			{
				return;
			}
		}
		if (!GameManager.IsSpectating())
		{
			int num4 = GameManager.PlayerTeam();
			for (int i = 0; i < array[num4]; i++)
			{
				this.teammateActorCanSeePlayer[i] = ActorManager.DoCanSeeCheck(this.actorsOnTeam[num4][i], FpsActorController.instance.actor, out num2);
				this.teammateActorDistanceToPlayer[i] = num2;
			}
		}
		for (int j = 0; j < num3; j++)
		{
			this.actors[j].ShiftClosestEnemyDistance();
			this.nextTargetDistance[j] = float.MaxValue;
			this.actorCanHearEnemy[j] = false;
			this.nextTarget[j] = this.pendingNextTarget[j];
		}
		this.interactionIteratorTeam0 = 0;
		this.interactionIteratorTeam1 = 0;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0004A8C0 File Offset: 0x00048AC0
	private void UpdateSight(Actor source, Actor target, float distance, bool hasLineOfSight)
	{
		if (!source.aiControlled)
		{
			return;
		}
		bool flag = false;
		if (hasLineOfSight)
		{
			float num;
			ActorManager.TargetSight targetSight = this.GetTargetSight(source, target, distance, out num);
			if (targetSight == ActorManager.TargetSight.CanTarget)
			{
				this.pendingNextTarget[source.actorIndex] = target;
				this.nextTargetDistance[source.actorIndex] = num;
			}
			flag = (targetSight > ActorManager.TargetSight.CannotSee);
		}
		if (flag)
		{
			float detectionSpeedMultiplier = 1f;
			if (distance < 25f)
			{
				detectionSpeedMultiplier = 3f;
			}
			else if (target.stance == Actor.Stance.Prone)
			{
				detectionSpeedMultiplier = 0.5f;
			}
			((AiActorController)source.controller).OnSeesEnemy(!target.aiControlled, detectionSpeedMultiplier);
			return;
		}
		if (this.pendingNextTarget[source.actorIndex] == target)
		{
			this.pendingNextTarget[source.actorIndex] = null;
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0004A974 File Offset: 0x00048B74
	private void UpdateVehicleInteractions()
	{
		int num = Mathf.Min(this.interactionIteratorVehicle + 10, this.vehicles.Count);
		Vector3 playerCameraPosition = GameManager.GetPlayerCameraPosition();
		for (int i = this.interactionIteratorVehicle; i < num; i++)
		{
			Vehicle vehicle = this.vehicles[i];
			try
			{
				Vector3 position;
				try
				{
					position = vehicle.targetLockPoint.position;
				}
				catch
				{
					position = vehicle.transform.position;
				}
				vehicle.playerDistance = Vector3.Distance(position, playerCameraPosition);
				vehicle.canSeePlayer = !Physics.Linecast(position + Vector3.up, playerCameraPosition, 8388609);
			}
			catch (Exception exception)
			{
				Debug.LogErrorFormat("Could not update vehicle {0}, exception follows:", new object[]
				{
					vehicle.name
				});
				Debug.LogException(exception);
			}
		}
		if (num == this.vehicles.Count)
		{
			this.interactionIteratorVehicle = 0;
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0004AA6C File Offset: 0x00048C6C
	private void UpdateSquadInteractions()
	{
		int num = 0;
		while (num < 2 && this.squadsOnTeam[num].Count != 0)
		{
			if (this.interactionIteratorSquads[num] >= this.squadsOnTeam[num].Count)
			{
				this.interactionIteratorSquads[num] = 0;
			}
			Squad squad = this.squadsOnTeam[num][this.interactionIteratorSquads[num]];
			this.interactionIteratorSquads[num]++;
			if (squad.CanLeadCluster())
			{
				if (squad.hostCluster == null)
				{
					squad.CreateHostCluster();
				}
				squad.hostCluster.Update();
			}
			else if (squad.hostCluster != null)
			{
				squad.hostCluster.Destroy();
			}
			num++;
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0004AB1C File Offset: 0x00048D1C
	private void UpdateFootsteps()
	{
		if (this.actors == null)
		{
			return;
		}
		Actor[] array = new Actor[8];
		float[] array2 = new float[8];
		for (int i = 0; i < 8; i++)
		{
			array2[i] = float.MaxValue;
		}
		foreach (Actor actor in this.actors)
		{
			actor.muteFootsteps = true;
			if (!actor.dead && actor.moving && actor.aiControlled && !actor.IsSeated())
			{
				float num = ActorManager.ActorDistanceToPlayer(actor);
				for (int j = 0; j < 8; j++)
				{
					if (num < array2[j])
					{
						for (int k = 7; k > j; k--)
						{
							array[k] = array[k - 1];
							array2[k] = array2[k - 1];
						}
						array[j] = actor;
						array2[j] = num;
						break;
					}
				}
			}
		}
		foreach (Actor actor2 in array)
		{
			if (actor2 != null)
			{
				actor2.muteFootsteps = false;
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0004AC4C File Offset: 0x00048E4C
	private static Vector3 GetEyeOrigin(Vector3 actorRootPosition, Actor.Stance stance, out float verticalJitter)
	{
		verticalJitter = 0.1f;
		switch (stance)
		{
		case Actor.Stance.Stand:
			actorRootPosition.y += 1.4f;
			return actorRootPosition;
		case Actor.Stance.Crouch:
			actorRootPosition.y += 0.7f;
			return actorRootPosition;
		case Actor.Stance.Prone:
			verticalJitter = 0f;
			actorRootPosition.y += 0.2f;
			return actorRootPosition;
		default:
			return actorRootPosition;
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0004ACB4 File Offset: 0x00048EB4
	private static bool DoCanSeeCheck(Actor a, Actor b, out float distance)
	{
		Vector3 vector = a.Position();
		Vector3 vector2 = b.Position();
		bool flag = true;
		if (a.aiControlled && ((AiActorController)a.controller).slowTargetDetection)
		{
			flag = false;
		}
		else if (b.aiControlled && ((AiActorController)b.controller).slowTargetDetection)
		{
			flag = false;
		}
		float y;
		vector = ActorManager.GetEyeOrigin(vector, a.stance, out y);
		float y2;
		vector2 = ActorManager.GetEyeOrigin(vector2, b.stance, out y2);
		distance = Vector3.Distance(vector, vector2);
		if (!flag)
		{
			return ActorManager.CanSeeRayTest(a, b, vector, vector2);
		}
		for (int i = 0; i < 2; i++)
		{
			vector += Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(0.05f, y, 0.05f));
			vector2 += Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(0.05f, y2, 0.05f));
			if (ActorManager.CanSeeRayTest(a, b, vector, vector2))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0004ADA0 File Offset: 0x00048FA0
	public static bool CanSeeRayTest(Actor a, Actor b, Vector3 aOrigin, Vector3 bOrigin)
	{
		int layerMask = (a.IsSeated() || b.IsSeated()) ? 8388609 : 8392705;
		return !Physics.Linecast(aOrigin, bOrigin, layerMask);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0004ADD8 File Offset: 0x00048FD8
	private ActorManager.TargetSight GetTargetSight(Actor actor, Actor target, float distance, out float modifiedDistance)
	{
		modifiedDistance = distance + target.visibilityDistanceModifier;
		if (!actor.aiControlled || target.isIgnored)
		{
			return ActorManager.TargetSight.CannotSee;
		}
		AiActorController aiActorController = (AiActorController)actor.controller;
		bool flag = aiActorController.squad != null && aiActorController.squad.attackTarget == target;
		bool flag2 = target.makesProximityMovementNoise && distance < 15f;
		if (!flag && !flag2 && (!aiActorController.CanSeeActorFOV(target) || !aiActorController.CanSeeActorFog(target, modifiedDistance)))
		{
			return ActorManager.TargetSight.CannotSee;
		}
		if (target.IsInBurningVehicle())
		{
			return ActorManager.TargetSight.CanReveal;
		}
		if (flag)
		{
			modifiedDistance = 0f;
		}
		else if (target.IsHighPriorityTarget())
		{
			modifiedDistance *= 0.5f;
		}
		else if (target.parachuteDeployed)
		{
			modifiedDistance += 150f;
		}
		if (modifiedDistance < this.nextTargetDistance[actor.actorIndex] && modifiedDistance <= aiActorController.GetFocusRange(target.GetFocusType()))
		{
			AiActorController.EvaluatedWeaponEffectiveness effectivenessAgainst = aiActorController.GetEffectivenessAgainst(target);
			if (effectivenessAgainst > AiActorController.EvaluatedWeaponEffectiveness.No && (flag || !aiActorController.OnlyAllowPreferredOrHighPriorityTargets() || effectivenessAgainst == AiActorController.EvaluatedWeaponEffectiveness.Preferred || target.IsHighPriorityTarget()))
			{
				return ActorManager.TargetSight.CanTarget;
			}
		}
		return ActorManager.TargetSight.CanReveal;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0004AEE8 File Offset: 0x000490E8
	public static bool ActorsCanSeeEachOther(Actor a, Actor b)
	{
		if (a.team == 0 && b.team == 1)
		{
			return ActorManager.instance.actorsCanSeeEachOther[a.teamActorIndex, b.teamActorIndex];
		}
		return a.team == 1 && b.team == 0 && ActorManager.instance.actorsCanSeeEachOther[b.teamActorIndex, a.teamActorIndex];
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00003D2C File Offset: 0x00001F2C
	public static bool ActorCanSeePlayer(Actor a)
	{
		if (GameManager.IsSpectating())
		{
			return true;
		}
		if (a.team == GameManager.PlayerTeam())
		{
			return ActorManager.instance.teammateActorCanSeePlayer[a.teamActorIndex];
		}
		return ActorManager.ActorsCanSeeEachOther(a, FpsActorController.instance.actor);
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0004AF50 File Offset: 0x00049150
	public static float ActorsDistance(Actor a, Actor b)
	{
		if (a.team == 0 && b.team == 1)
		{
			return ActorManager.instance.actorsDistance[a.teamActorIndex, b.teamActorIndex];
		}
		if (a.team == 1 && b.team == 0)
		{
			return ActorManager.instance.actorsDistance[b.teamActorIndex, a.teamActorIndex];
		}
		throw new Exception("ActorManager.ActorsDistance() not supported for actors with the same team or non-(0, 1) team.");
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0004AFC4 File Offset: 0x000491C4
	public static float ActorDistanceToPlayer(Actor a)
	{
		if (!ActorManager.instance.lookupTablesInitialized)
		{
			return 0f;
		}
		if (a.team == GameManager.PlayerTeam())
		{
			return ActorManager.instance.teammateActorDistanceToPlayer[a.teamActorIndex];
		}
		return ActorManager.ActorsDistance(a, FpsActorController.instance.actor);
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00003D66 File Offset: 0x00001F66
	public static Actor GetNextTargetOfActor(Actor actor)
	{
		if (ActorManager.instance.nextTarget == null)
		{
			return null;
		}
		return ActorManager.instance.nextTarget[actor.actorIndex];
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00003D87 File Offset: 0x00001F87
	private void InitializeLookupTables()
	{
		this.SetupLookupTablesPlayerTeam();
		this.SetupLookupTables1d();
		this.SetupLookupTables2d();
		this.UpdateInteractionTime();
		this.lookupTablesInitialized = true;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0004B014 File Offset: 0x00049214
	private void SetupLookupTablesPlayerTeam()
	{
		if (!GameManager.IsSpectating())
		{
			int num = Mathf.Max(Mathf.NextPowerOfTwo(this.nextActorIndexTeam[GameManager.PlayerTeam()]), 64);
			this.teammateActorCanSeePlayer = new bool[num];
			this.teammateActorDistanceToPlayer = new float[num];
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0004B05C File Offset: 0x0004925C
	private void SetupLookupTables1d()
	{
		int num = Mathf.Max(Mathf.NextPowerOfTwo(this.nextActorIndex), 128);
		this.nextTarget = new Actor[num];
		this.pendingNextTarget = new Actor[num];
		this.nextTargetDistance = new float[num];
		this.actorCanHearEnemy = new bool[num];
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0004B0B0 File Offset: 0x000492B0
	private void SetupLookupTables2d()
	{
		int num = Mathf.Max(Mathf.NextPowerOfTwo(this.nextActorIndexTeam[0]), 64);
		int num2 = Mathf.Max(Mathf.NextPowerOfTwo(this.nextActorIndexTeam[1]), 64);
		this.actorsCanSeeEachOther = new bool[num, num2];
		this.actorsDistance = new float[num, num2];
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0004B104 File Offset: 0x00049304
	private void UpdateInteractionTime()
	{
		int num = this.nextActorIndexTeam[0] * this.nextActorIndexTeam[1];
		this.interactionUpdatesPerFrame = Mathf.Min(Mathf.CeilToInt((float)num / 42f), 200);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0004B140 File Offset: 0x00049340
	private void ActorNumberChanged()
	{
		if (!this.lookupTablesInitialized)
		{
			return;
		}
		bool flag = this.nextActorIndexTeam[0] >= this.actorsCanSeeEachOther.GetLength(0);
		bool flag2 = this.nextActorIndexTeam[1] >= this.actorsCanSeeEachOther.GetLength(1);
		bool flag3 = this.nextActorIndex >= this.pendingNextTarget.Length;
		bool flag4 = (!GameManager.IsSpectating() && GameManager.PlayerTeam() == 0) ? flag : flag2;
		if (flag || flag2)
		{
			bool[,] array = this.actorsCanSeeEachOther;
			float[,] array2 = this.actorsDistance;
			this.SetupLookupTables2d();
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					this.actorsCanSeeEachOther[i, j] = array[i, j];
					this.actorsDistance[i, j] = array2[i, j];
				}
			}
		}
		if (flag3)
		{
			Actor[] array3 = this.nextTarget;
			Actor[] array4 = this.pendingNextTarget;
			float[] array5 = this.nextTargetDistance;
			this.SetupLookupTables1d();
			for (int k = 0; k < array4.Length; k++)
			{
				this.nextTarget[k] = array3[k];
				this.pendingNextTarget[k] = array4[k];
				this.nextTargetDistance[k] = array5[k];
			}
		}
		if (flag4)
		{
			bool[] array6 = this.teammateActorCanSeePlayer;
			float[] array7 = this.teammateActorDistanceToPlayer;
			this.SetupLookupTablesPlayerTeam();
			for (int l = 0; l < array6.Length; l++)
			{
				this.teammateActorCanSeePlayer[l] = array6[l];
				this.teammateActorDistanceToPlayer[l] = array7[l];
			}
		}
		this.UpdateInteractionTime();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00003DA8 File Offset: 0x00001FA8
	public void SetupAllSpawnPointInformation()
	{
		this.spawnPoints = UnityEngine.Object.FindObjectsOfType<SpawnPoint>();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00003DB5 File Offset: 0x00001FB5
	public static void ApplyGlobalTeamSkin(Actor actor)
	{
		ActorManager.ApplyGlobalTeamSkin(actor.skinnedRenderer, actor.animatedBones, actor.team);
		ActorManager.ApplyGlobalTeamSkin(actor.skinnedRendererRagdoll, actor.ragdollBones, actor.team);
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00003DE5 File Offset: 0x00001FE5
	public static void ApplyGlobalTeamSkin(SkinnedMeshRenderer renderer, int team)
	{
		ActorManager.ApplyGlobalTeamSkin(renderer, ActorManager.GetRecursiveBones(renderer.rootBone), team);
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0004B2DC File Offset: 0x000494DC
	private static void ApplyGlobalTeamSkin(SkinnedMeshRenderer renderer, Transform[] bones, int team)
	{
		ActorSkin actorSkin = ActorManager.instance.actorSkin[team];
		ActorMaterials actorMaterials = ActorManager.instance.actorMaterials[team];
		if (actorSkin != null && actorSkin.characterSkin.mesh != null)
		{
			renderer.sharedMesh = actorSkin.characterSkin.mesh;
		}
		renderer.sharedMaterials = actorMaterials.instancedMaterials;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0004B338 File Offset: 0x00049538
	public static Transform[] GetRecursiveBones(Transform rootBone)
	{
		List<Transform> bones = new List<Transform>(32);
		GameManager.RecurseHierarchy(rootBone, delegate(Transform t)
		{
			bones.Add(t);
		});
		return bones.ToArray();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000296E File Offset: 0x00000B6E
	private static void ApplyBoneScales(Transform[] bones, ActorSkin.RigSettings rigSettings)
	{
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00003DF9 File Offset: 0x00001FF9
	public static void ApplyOverrideActorSkin(Actor actor, ActorSkin skin, int team)
	{
		ActorManager.ApplyOverrideActorSkin(actor.skinnedRenderer, actor.animatedBones, skin, team);
		ActorManager.ApplyOverrideActorSkin(actor.skinnedRendererRagdoll, actor.ragdollBones, skin, team);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00003E21 File Offset: 0x00002021
	public static void ApplyOverrideActorSkin(SkinnedMeshRenderer renderer, Transform[] bones, ActorSkin skin, int team)
	{
		ActorManager.ApplyOverrideMeshSkin(renderer, skin.characterSkin, team);
		ActorManager.ApplyBoneScales(bones, skin.rigSettings);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0004B378 File Offset: 0x00049578
	public static void ApplyOverrideMeshSkin(SkinnedMeshRenderer renderer, ActorSkin.MeshSkin skin, int team)
	{
		if (skin.mesh != null)
		{
			renderer.sharedMesh = skin.mesh;
		}
		Material[] array = new Material[skin.materials.Length];
		skin.materials.CopyTo(array, 0);
		if (skin.teamMaterial >= 0 && skin.teamMaterial < array.Length)
		{
			array[skin.teamMaterial] = new Material(ColorScheme.GetActorMaterial(team));
		}
		renderer.materials = array;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0004B3E8 File Offset: 0x000495E8
	public void StartGame()
	{
		Debug.Log("ActorManager Start");
		this.lookupTablesInitialized = false;
		this.SetupAllSpawnPointInformation();
		this.actors = new List<Actor>();
		this.aiActorControllers = new List<AiActorController>();
		this.vehicles = new List<Vehicle>();
		this.ladders = new List<Ladder>();
		this.damageZones = new List<DamageZone>();
		this.speedLimitZones = new List<SpeedLimitZone>();
		this.availableVehiclesBySpawnPoint = new Dictionary<SpawnPoint, List<Vehicle>>();
		this.aliveActors = new Dictionary<int, List<Actor>>();
		this.aliveActors.Add(0, new List<Actor>());
		this.aliveActors.Add(1, new List<Actor>());
		this.team0Bots = 0;
		this.team1Bots = 0;
		this.currentCapturePointUpdateIndex = 0;
		this.lastAiUpdateEnd = 0;
		this.playerActorIndex = -1;
		this.actorSkin = new ActorSkin[2];
		this.actorMaterials = new ActorMaterials[2];
		this.smokeTargets = new List<ActorManager.SmokeTarget>[2];
		this.hasDefaultSkin = new bool[2];
		this.activeTriggers = new List<TriggerVolume.RuntimeData>(16);
		this.triggerStates = new List<TriggerVolumeActorState>(16);
		this.actorData = new List<ActorData>(64);
		this.randomTeamSeed = new int[]
		{
			UnityEngine.Random.Range(0, 10000),
			UnityEngine.Random.Range(0, 10000)
		};
		for (int i = 0; i < 2; i++)
		{
			ActorSkin skin = GameManager.instance.gameInfo.team[i].skin;
			if (skin != null)
			{
				ActorManager.SetGlobalTeamSkin(i, skin);
				this.hasDefaultSkin[i] = false;
			}
			else
			{
				ActorManager.SetGlobalTeamSkin(i, this.defaultActorSkin);
				this.hasDefaultSkin[i] = true;
			}
			this.smokeTargets[i] = new List<ActorManager.SmokeTarget>();
		}
		foreach (SpawnPoint key in this.spawnPoints)
		{
			this.availableVehiclesBySpawnPoint.Add(key, new List<Vehicle>());
		}
		this.InitializeLookupTables();
		this.isReady = true;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0004B5BC File Offset: 0x000497BC
	public static void SetGlobalTeamSkin(int team, ActorSkin skin)
	{
		ActorManager.instance.actorSkin[team] = skin;
		ActorManager.instance.actorMaterials[team] = new ActorMaterials(skin.characterSkin, ColorScheme.GetActorMaterial(team));
		foreach (Actor actor in ActorManager.ActorsOnTeam(team))
		{
			ActorManager.ApplyGlobalTeamSkin(actor);
		}
		if (NightVision.isEnabled)
		{
			ActorManager.EnableNightVisionGlow();
		}
		RuntimePortraitGenerator.ResetOffset();
		RuntimePortraitGenerator.RenderDatabaseTeamPortrait(team);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0004B650 File Offset: 0x00049850
	public void CreateDefaultAiActors(bool ignoreBalance)
	{
		int num = GameManager.GameParameters().actorCount / 2;
		if (!ignoreBalance)
		{
			num = Mathf.RoundToInt(GameManager.GameParameters().balance * (float)GameManager.GameParameters().actorCount);
		}
		int num2 = GameManager.GameParameters().actorCount - num;
		if (GameManager.PlayerTeam() == 0)
		{
			num2--;
		}
		else if (GameManager.PlayerTeam() == 1)
		{
			num--;
		}
		num2 = Mathf.Max(0, num2);
		num = Mathf.Max(0, num);
		this.InitializeAiActors(num2, num);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0004B6C8 File Offset: 0x000498C8
	public Dictionary<int, List<Actor>> InitializeAiActors(int team0Count, int team1Count)
	{
		Mathf.Max(team0Count, 0);
		Mathf.Max(team1Count, 0);
		Dictionary<int, List<Actor>> dictionary = new Dictionary<int, List<Actor>>(2);
		dictionary.Add(0, new List<Actor>(team0Count));
		dictionary.Add(1, new List<Actor>(team1Count));
		for (int i = 0; i < team0Count; i++)
		{
			dictionary[0].Add(this.CreateAIActor(0));
		}
		for (int j = 0; j < team1Count; j++)
		{
			dictionary[1].Add(this.CreateAIActor(1));
		}
		return dictionary;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00003E3C File Offset: 0x0000203C
	public static bool ActorCanHearEnemy(Actor actor)
	{
		return ActorManager.instance.actorCanHearEnemy != null && ActorManager.instance.actorCanHearEnemy[actor.actorIndex];
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0004B748 File Offset: 0x00049948
	public Actor CreateAIActor(int team)
	{
		Actor component = UnityEngine.Object.Instantiate<GameObject>(this.actorPrefab, ActorManager.ACTOR_INSTANTIATE_POSITION, Quaternion.identity).GetComponent<Actor>();
		if (team == 0)
		{
			this.team0Bots++;
		}
		else if (team == 1)
		{
			this.team1Bots++;
		}
		component.SetTeam(team);
		component.deathTimestamp = Time.time;
		if (this.actorNameSet.names.Count == 0)
		{
			this.actorNameSet = this.defaultActorNameSet.Clone();
		}
		int index = UnityEngine.Random.Range(0, this.actorNameSet.names.Count);
		component.name = this.actorNameSet.names[index];
		this.actorNameSet.names.RemoveAt(index);
		ScoreboardUi.AddEntryForActor(component);
		ActorManager.Register(component);
		return component;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00003E5D File Offset: 0x0000205D
	public void OnSetTeam(Actor actor)
	{
		if (actor.team < 0 || actor.team > 1)
		{
			return;
		}
		ActorManager.ApplyGlobalTeamSkin(actor);
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00003E78 File Offset: 0x00002078
	public int GetNumberOfBotsInTeam(int team)
	{
		if (team == 0)
		{
			return this.team0Bots;
		}
		return this.team1Bots;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00003E8A File Offset: 0x0000208A
	public static void SetAlive(Actor actor)
	{
		ActorManager.instance.aliveActors[actor.team].Add(actor);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00003EA7 File Offset: 0x000020A7
	public static void SetDead(Actor actor)
	{
		ActorManager.instance.aliveActors[actor.team].Remove(actor);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00003EC5 File Offset: 0x000020C5
	public static bool AllActorsOnTeamAreDead(int team)
	{
		return ActorManager.AliveActorsOnTeam(team).Count == 0;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00003ED5 File Offset: 0x000020D5
	public static List<Actor> ActorsOnTeam(int team)
	{
		return ActorManager.instance.actorsOnTeam[team];
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00003EE3 File Offset: 0x000020E3
	public static List<Actor> AliveActorsOnTeam(int team)
	{
		return ActorManager.instance.aliveActors[team];
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00003EF5 File Offset: 0x000020F5
	public static SpawnPoint RandomSpawnPoint()
	{
		return ActorManager.instance.spawnPoints[UnityEngine.Random.Range(0, ActorManager.instance.spawnPoints.Length)];
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0004B814 File Offset: 0x00049A14
	public static SpawnPoint RandomSpawnPointForTeam(int team)
	{
		int num = UnityEngine.Random.Range(0, ActorManager.instance.spawnPoints.Length);
		for (int i = 0; i < ActorManager.instance.spawnPoints.Length; i++)
		{
			int num2 = (num + i) % ActorManager.instance.spawnPoints.Length;
			if (ActorManager.instance.spawnPoints[num2].owner == team)
			{
				return ActorManager.instance.spawnPoints[num2];
			}
		}
		return null;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0004B880 File Offset: 0x00049A80
	public static SpawnPoint RandomFrontlineSpawnPointForTeam(int team)
	{
		int num = UnityEngine.Random.Range(0, ActorManager.instance.spawnPoints.Length);
		for (int i = 0; i < ActorManager.instance.spawnPoints.Length; i++)
		{
			int num2 = (num + i) % ActorManager.instance.spawnPoints.Length;
			SpawnPoint spawnPoint = ActorManager.instance.spawnPoints[num2];
			if (spawnPoint.owner == team && (!spawnPoint.IsSafe() || spawnPoint.IsFrontLine()))
			{
				return spawnPoint;
			}
		}
		return ActorManager.RandomSpawnPointForTeam(team);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0004B8F8 File Offset: 0x00049AF8
	public static bool TeamHasAnySpawnPoint(int team)
	{
		SpawnPoint[] array = ActorManager.instance.spawnPoints;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].owner == team)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0004B92C File Offset: 0x00049B2C
	public static SpawnPoint ClosestSpawnPoint(Vector3 position)
	{
		SpawnPoint result = null;
		float num = 9999999f;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			float num2 = Vector3.Distance(position, spawnPoint.transform.position);
			if (num2 < num)
			{
				num = num2;
				result = spawnPoint;
			}
		}
		return result;
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0004B980 File Offset: 0x00049B80
	public static LandingZone ClosestLandingZone(Vector3 position)
	{
		LandingZone result = null;
		float num = 9999999f;
		SpawnPoint[] array = ActorManager.instance.spawnPoints;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (LandingZone landingZone in array[i].landingZones)
			{
				float num2 = Vector3.Distance(landingZone.transform.position, position);
				if (num2 < num)
				{
					num = num2;
					result = landingZone;
				}
			}
		}
		return result;
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0004BA14 File Offset: 0x00049C14
	public static SpawnPoint ClosestSpawnPointOwnedBy(Vector3 position, int team)
	{
		SpawnPoint result = null;
		float num = 9999999f;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (team == spawnPoint.owner)
			{
				float num2 = Vector3.Distance(position, spawnPoint.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = spawnPoint;
				}
			}
		}
		return result;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0004BA74 File Offset: 0x00049C74
	public static SpawnPoint RandomEnemySpawnPoint(int team)
	{
		int num = UnityEngine.Random.Range(0, ActorManager.instance.spawnPoints.Length);
		for (int i = 0; i < ActorManager.instance.spawnPoints.Length; i++)
		{
			int num2 = (num + i) % ActorManager.instance.spawnPoints.Length;
			if (ActorManager.instance.spawnPoints[num2].owner != team)
			{
				return ActorManager.instance.spawnPoints[num2];
			}
		}
		return null;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00003F14 File Offset: 0x00002114
	public static List<Actor> AliveActors()
	{
		return ActorManager.instance.actors.FindAll((Actor a) => !a.dead);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0004BAE0 File Offset: 0x00049CE0
	public static List<Actor> AliveActorsInRange(Vector3 point, float range)
	{
		List<Actor> list = new List<Actor>(8);
		foreach (Actor actor in ActorManager.instance.actors)
		{
			if (!actor.dead && Vector3.Distance(point, actor.Position()) < range)
			{
				list.Add(actor);
			}
		}
		return list;
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0004BB58 File Offset: 0x00049D58
	public static List<Actor> ActorsInRange(Vector3 point, float range)
	{
		List<Actor> list = new List<Actor>(8);
		foreach (Actor actor in ActorManager.instance.actors)
		{
			if (Vector3.Distance(point, actor.Position()) < range)
			{
				list.Add(actor);
			}
		}
		return list;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0004BBC8 File Offset: 0x00049DC8
	public static List<Vehicle> VehiclesInRange(Vector3 point, float range)
	{
		List<Vehicle> list = new List<Vehicle>(ActorManager.instance.vehicles.Count / 4);
		float num = range * range;
		foreach (Vehicle vehicle in ActorManager.instance.vehicles)
		{
			if ((vehicle.transform.position - point).sqrMagnitude < num)
			{
				list.Add(vehicle);
			}
		}
		return list;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0004BC58 File Offset: 0x00049E58
	public static void RegisterProjectile(Projectile p)
	{
		try
		{
			RavenscriptManager.events.onProjectileSpawned.Invoke(p);
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
		Vector3 position = p.transform.position;
		Vector3 normalized = p.velocity.normalized;
		Ray ray = new Ray(position, normalized);
		float num = 9999f;
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 9999f, 1))
		{
			num = raycastHit.distance;
		}
		if (p.killCredit == null)
		{
			return;
		}
		int team = 1 - p.killCredit.team;
		if (p.killCredit.team == -1)
		{
			team = 0;
		}
		foreach (Actor actor in ActorManager.AliveActorsOnTeam(team))
		{
			if (actor.aiControlled && (!actor.IsSeated() || p.armorDamage >= actor.seat.vehicle.armorDamagedBy))
			{
				Vector3 vector = position - actor.Position();
				float d = vector.magnitude / p.configuration.speed;
				vector -= actor.Velocity() * d;
				float num2 = Vector3.Dot(vector, normalized);
				if (Mathf.Abs(num2) <= num + 5f)
				{
					float num3 = -num2 / p.configuration.speed;
					Vector3 b = Physics.gravity * Mathf.Pow(num3, 2f) / 2f;
					Vector3 b2 = position + normalized * p.configuration.speed * num3 + b;
					if (Vector3.Distance(actor.Position() + actor.Velocity() * num3, b2) < 5f)
					{
						ActorManager.instance.StartCoroutine(ActorManager.instance.MarkTakingFire((AiActorController)actor.controller, -normalized, num3));
					}
				}
			}
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00003F44 File Offset: 0x00002144
	private IEnumerator MarkTakingFire(AiActorController ai, Vector3 direction, float duration)
	{
		yield return new WaitForSeconds(duration + AiActorController.PARAMETERS.TAKING_FIRE_REACTION_TIME);
		ai.MarkTakingFireFrom(direction);
		yield break;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0004BE8C File Offset: 0x0004A08C
	public static void PlayerTakeOverBot(Actor targetActor)
	{
		try
		{
			AiActorController aiActorController = (AiActorController)targetActor.controller;
			Actor actor = FpsActorController.instance.actor;
			if (!actor.dead)
			{
				actor.KillSilently();
			}
			WeaponManager.LoadoutSet forcedLoadout = ActorManager.CopyLoadoutOfActor(targetActor);
			actor.SpawnAt(targetActor.transform.position, Quaternion.LookRotation(targetActor.controller.FacingDirection()), forcedLoadout);
			int num = -1;
			for (int i = 0; i < 5; i++)
			{
				if (actor.weapons[i] != null && targetActor.weapons[i] != null)
				{
					actor.weapons[i].ammo = targetActor.weapons[i].ammo;
					actor.weapons[i].spareAmmo = targetActor.weapons[i].spareAmmo;
					if (targetActor.weapons[i] == targetActor.activeWeapon)
					{
						num = i;
					}
				}
			}
			if (targetActor.IsSeated())
			{
				Seat seat = targetActor.seat;
				targetActor.LeaveSeat(false);
				actor.EnterSeat(seat, false);
			}
			else if (targetActor.fallenOver)
			{
				actor.ForceStance(Actor.Stance.Prone);
			}
			else
			{
				actor.ForceStance(targetActor.stance);
			}
			if (num != -1)
			{
				actor.SwitchWeapon(num);
			}
			if (FpsActorController.instance.playerSquad != null && aiActorController.squad != null && aiActorController.isSquadLeader)
			{
				Vehicle squadVehicle = aiActorController.squad.squadVehicle;
				AiActorController[] array = aiActorController.squad.aiMembers.ToArray();
				for (int j = 0; j < array.Length; j++)
				{
					array[j].ChangeToSquad(FpsActorController.instance.playerSquad);
				}
				if (squadVehicle != null)
				{
					FpsActorController.instance.playerSquad.PlayerSquadTakeOverSquadVehicle(squadVehicle);
				}
			}
			if (targetActor.IsOnLadder())
			{
				actor.GetOnLadderAtHeight(targetActor.ladder, targetActor.ladderHeight);
			}
			if (targetActor.parachuteDeployed)
			{
				actor.DeployParachute();
			}
			actor.AmmoChanged();
			targetActor.KillSilently();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0004C090 File Offset: 0x0004A290
	public static WeaponManager.LoadoutSet CopyLoadoutOfActor(Actor actor)
	{
		WeaponManager.LoadoutSet loadoutSet = new WeaponManager.LoadoutSet();
		if (actor.weapons[0] != null)
		{
			loadoutSet.primary = actor.weapons[0].weaponEntry;
		}
		if (actor.weapons[1] != null)
		{
			loadoutSet.secondary = actor.weapons[1].weaponEntry;
		}
		if (actor.weapons[2] != null)
		{
			loadoutSet.gear1 = actor.weapons[2].weaponEntry;
		}
		if (actor.weapons[3] != null)
		{
			loadoutSet.gear2 = actor.weapons[3].weaponEntry;
		}
		if (actor.weapons[4] != null)
		{
			loadoutSet.gear3 = actor.weapons[4].weaponEntry;
		}
		return loadoutSet;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0004C154 File Offset: 0x0004A354
	public static Ladder GetLadderWithNode(GraphNode graphNode)
	{
		PointNode pointNode = (PointNode)graphNode;
		foreach (Ladder ladder in ActorManager.instance.ladders)
		{
			if (ladder.bottomNode == pointNode)
			{
				return ladder;
			}
			if (ladder.topNode == pointNode)
			{
				return ladder;
			}
		}
		return null;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00003F61 File Offset: 0x00002161
	public static void RegisterLadder(Ladder ladder)
	{
		if (ActorManager.instance && ActorManager.instance.ladders != null)
		{
			ActorManager.instance.ladders.Add(ladder);
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0004C1C8 File Offset: 0x0004A3C8
	public static void RegisterVehicle(Vehicle vehicle)
	{
		ActorManager.instance.vehicles.Add(vehicle);
		SpawnPoint spawnPoint = null;
		float num = 1E+09f;
		foreach (SpawnPoint spawnPoint2 in ActorManager.instance.spawnPoints)
		{
			float num2 = Vector3.Distance(spawnPoint2.transform.position, vehicle.transform.position);
			if (num2 < num)
			{
				num = num2;
				spawnPoint = spawnPoint2;
			}
		}
		if (spawnPoint != null)
		{
			ActorManager.instance.availableVehiclesBySpawnPoint[spawnPoint].Add(vehicle);
			vehicle.closestSpawnPoint = spawnPoint;
			return;
		}
		Debug.LogError("Vehicle closest spawn point is null");
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00003F8B File Offset: 0x0000218B
	public static void SetVehicleTaken(Vehicle vehicle)
	{
		if (vehicle.closestSpawnPoint != null)
		{
			ActorManager.instance.availableVehiclesBySpawnPoint[vehicle.closestSpawnPoint].Remove(vehicle);
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00003FB7 File Offset: 0x000021B7
	public static void DropVehicle(Vehicle vehicle)
	{
		ActorManager.instance.vehicles.Remove(vehicle);
		if (vehicle.closestSpawnPoint != null)
		{
			ActorManager.instance.availableVehiclesBySpawnPoint[vehicle.closestSpawnPoint].Remove(vehicle);
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0004C268 File Offset: 0x0004A468
	public static bool Explode(Actor source, Weapon sourceWeapon, Vector3 point, ExplodingProjectile.ExplosionConfiguration configuration, Vehicle.ArmorRating damageRating, bool reduceFriendlyDamage)
	{
		return ActorManager.Explode(new ExplosionInfo
		{
			sourceActor = source,
			sourceWeapon = sourceWeapon,
			point = point,
			configuration = configuration,
			damageRating = damageRating
		}, reduceFriendlyDamage);
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0004C2B0 File Offset: 0x0004A4B0
	public static bool Explode(ExplosionInfo info, bool reduceFriendlyDamage)
	{
		bool result;
		try
		{
			ActorManager.instance.lastExplosion = info;
			ActorManager.instance.OnExplosion.Invoke(info);
			RavenscriptManager.events.onExplosion.Invoke(info.point, info.configuration.damageRange, info.sourceActor);
			RavenscriptManager.events.onExplosionInfo.Invoke(info);
			float range = Mathf.Max(new float[]
			{
				info.configuration.damageRange,
				info.configuration.balanceRange,
				50f
			});
			List<Actor> list = ActorManager.ActorsInRange(info.point, range);
			bool flag = false;
			int num = (info.sourceActor != null) ? info.sourceActor.team : -1;
			float num2 = 0f;
			if (WaterLevel.IsInWater(info.point, out num2) || WaterLevel.IsInWater(info.point - new Vector3(0f, 0.2f, 0f), out num2))
			{
				float num3 = info.point.y + num2;
				if (num3 - info.point.y < 5f)
				{
					Vector3 point = info.point;
					point.y = num3;
					GameManager.CreateSplashEffect(true, point);
				}
			}
			if (info.configuration.damageRange > 3f && info.configuration.damage > 50f)
			{
				MuzzleFlashManager.RegisterLightFlash(info.point, Vector3.Distance(ActorManager.instance.player.Position(), info.point), info.sourceActor != null && !info.sourceActor.aiControlled, 20f);
			}
			foreach (Actor actor in list)
			{
				Vector3 vector = actor.CenterPosition() - info.point;
				float magnitude = vector.magnitude;
				if (!actor.dead && magnitude < 50f)
				{
					AiActorController aiActorController = actor.controller as AiActorController;
					if (aiActorController != null)
					{
						aiActorController.SetAlert();
					}
				}
				if (magnitude <= Mathf.Max(info.configuration.damageRange, info.configuration.balanceRange))
				{
					Vector3 end = actor.Position() + new Vector3(0f, 0.05f, 0f);
					Vector3 end2 = actor.CenterPosition() + new Vector3(0f, 0.5f, 0f);
					if (!actor.aiControlled)
					{
						end2 = FpsActorController.instance.fpCamera.transform.position;
					}
					float num4 = info.configuration.damageFalloff.Evaluate(Mathf.Clamp01(magnitude / info.configuration.damageRange)) * info.configuration.infantryDamageMultiplier;
					float num5 = info.configuration.balanceFalloff.Evaluate(Mathf.Clamp01(magnitude / info.configuration.balanceRange));
					bool flag2 = num == actor.team;
					if (reduceFriendlyDamage && flag2)
					{
						num4 *= 0.2f;
						num5 *= 0.5f;
					}
					if (!actor.dead)
					{
						DamageInfo damageInfo = new DamageInfo(DamageInfo.DamageSourceType.Explosion, info.sourceActor, info.sourceWeapon)
						{
							healthDamage = info.configuration.damage * num4,
							balanceDamage = info.configuration.balanceDamage * num5,
							isSplashDamage = true,
							point = actor.CenterPosition(),
							direction = vector.normalized,
							impactForce = vector.normalized * info.configuration.force * num5
						};
						if (Physics.Linecast(info.point, end, 1) && Physics.Linecast(info.point, end2, 1))
						{
							damageInfo.healthDamage = 0f;
							damageInfo.balanceDamage = Mathf.Clamp(damageInfo.balanceDamage, 0f, 20f);
						}
						actor.Damage(damageInfo);
						flag = true;
					}
					else
					{
						actor.ApplyRigidbodyForce(vector.normalized * info.configuration.force * num5);
					}
				}
			}
			foreach (Vehicle vehicle in ActorManager.instance.vehicles.ToArray())
			{
				try
				{
					if (vehicle.gameObject.activeInHierarchy && vehicle.IsDamagedByRating(info.damageRating))
					{
						Vector3 position = vehicle.transform.position;
						if (Vector3.Distance(position, info.point) < info.configuration.damageRange)
						{
							DamageInfo info2 = ActorManager.EvaluateExplosionDamage(info, position, false);
							info2.healthDamage *= vehicle.GetDamageMultiplier(info.damageRating);
							vehicle.Damage(info2);
							flag = true;
						}
					}
				}
				catch (Exception e)
				{
					ModManager.HandleModException(e);
				}
			}
			result = flag;
		}
		catch (Exception e2)
		{
			ModManager.HandleModException(e2);
			result = false;
		}
		return result;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
	public static DamageInfo EvaluateExplosionDamage(ExplosionInfo info, Vector3 point, bool ignoreLevelGeometry)
	{
		DamageInfo damageInfo = new DamageInfo(DamageInfo.DamageSourceType.Explosion, info.sourceActor, info.sourceWeapon);
		if (info.configuration == null)
		{
			return damageInfo;
		}
		Vector3 vector = point - info.point;
		float magnitude = vector.magnitude;
		damageInfo.isSplashDamage = true;
		damageInfo.isPiercing = false;
		damageInfo.point = point;
		float num = Mathf.Max(info.configuration.damageFalloff.Evaluate(Mathf.Clamp01(magnitude / info.configuration.damageRange)), 0f);
		float num2 = Mathf.Max(info.configuration.balanceFalloff.Evaluate(Mathf.Clamp01(magnitude / info.configuration.balanceRange)), 0f);
		damageInfo.direction = vector.normalized;
		damageInfo.impactForce = damageInfo.direction * info.configuration.force * num2;
		if (!ignoreLevelGeometry && Physics.Linecast(info.point, point, 1))
		{
			damageInfo.healthDamage = 0f;
			damageInfo.balanceDamage = Mathf.Clamp(info.configuration.balanceDamage * num2, 0f, 20f);
		}
		else
		{
			damageInfo.healthDamage = info.configuration.damage * num;
			damageInfo.balanceDamage = info.configuration.balanceDamage * num2;
		}
		return damageInfo;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00003FF4 File Offset: 0x000021F4
	public static DamageInfo EvaluateLastExplosionDamage(Vector3 point, bool ignoreLevelGeometry)
	{
		return ActorManager.EvaluateExplosionDamage(ActorManager.instance.lastExplosion, point, ignoreLevelGeometry);
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0004C94C File Offset: 0x0004AB4C
	public static void CreateBloodExplosion(RaycastHit hitInfo)
	{
		UnityEngine.Object.Instantiate<GameObject>(ActorManager.instance.genericExplosionPrefab, hitInfo.point, SMath.LookRotationRespectUp(Vector3.forward, hitInfo.normal));
		ActorManager.Explode(null, null, hitInfo.point + hitInfo.normal * 0.3f, ActorManager.instance.bloodExplosion, Vehicle.ArmorRating.AntiTank, false);
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
	public static void MakeActorsFleeFrom(Vector3 position, float range, float diveTime, float diveRange, bool requirePointInFov)
	{
		foreach (Actor actor in ActorManager.instance.actors)
		{
			Vector3 vector = position - actor.Position();
			if (!actor.dead && !actor.fallenOver && actor.aiControlled && (!requirePointInFov || Vector3.Dot(vector.normalized, actor.controller.FacingDirection()) > 0.65f) && vector.magnitude < range)
			{
				((AiActorController)actor.controller).FleeAndDiveFrom(position, diveTime, diveRange);
			}
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00004007 File Offset: 0x00002207
	public static void SpawnAmmoReserveOnActor(Actor actor)
	{
		actor.hasSpawnedAmmoReserve = true;
		ActorManager.instance.StartCoroutine(ActorManager.instance.SpawnAmmoReserve(actor.CenterPosition(), 0.4f));
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00004030 File Offset: 0x00002230
	private IEnumerator SpawnAmmoReserve(Vector3 position, float delay)
	{
		yield return new WaitForSeconds(delay);
		UnityEngine.Object.Instantiate<GameObject>(this.ammoReservePrefab, position, Quaternion.identity);
		yield break;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000404D File Offset: 0x0000224D
	private IEnumerator SpawnTape(Vector3 position, float delay)
	{
		yield return new WaitForSeconds(delay);
		UnityEngine.Object.Instantiate<GameObject>(this.tapePrefab, position, Quaternion.identity);
		yield break;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000406A File Offset: 0x0000226A
	public static void RegisterSquad(Squad squad)
	{
		ActorManager.instance.squadsOnTeam[squad.team].Add(squad);
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00004083 File Offset: 0x00002283
	public static void RemoveSquad(Squad squad)
	{
		ActorManager.instance.squadsOnTeam[squad.team].Remove(squad);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000409D File Offset: 0x0000229D
	public static List<Squad> GetSquadsOnTeam(int team)
	{
		return ActorManager.instance.squadsOnTeam[team];
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0004CA68 File Offset: 0x0004AC68
	private void OnLevelLoaded(Scene arg0, LoadSceneMode arg1)
	{
		this.nextActorIndex = 0;
		this.nextActorIndexTeam = new int[2];
		this.actors = null;
		this.actorsOnTeam = new List<Actor>[2];
		this.actorsOnTeam[0] = new List<Actor>();
		this.actorsOnTeam[1] = new List<Actor>();
		this.actorsCanSeeEachOther = null;
		this.squadsOnTeam = new List<Squad>[2];
		this.squadsOnTeam[0] = new List<Squad>();
		this.squadsOnTeam[1] = new List<Squad>();
		this.vehicles = null;
		this.OnExplosion.RemoveAllListeners();
		base.CancelInvoke();
		base.StopAllCoroutines();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x000040AB File Offset: 0x000022AB
	public static void OnExitGameScene()
	{
		ActorManager.instance.ClearData();
		ActorManager.instance.CancelInvoke();
		ActorManager.instance.StopAllCoroutines();
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000040CB File Offset: 0x000022CB
	private void ClearData()
	{
		this.isReady = false;
	}

	// Token: 0x04000241 RID: 577
	public const int AI_VISION_OCCLUDER_LAYER = 23;

	// Token: 0x04000242 RID: 578
	public const int CAN_SEE_RAY_NO_VEHICLE_MASK = 8388609;

	// Token: 0x04000243 RID: 579
	public const int CAN_SEE_RAY_INCLUDE_VEHICLE_MASK = 8392705;

	// Token: 0x04000244 RID: 580
	private const int CAN_SEE_RAYCAST_SAMPLES = 2;

	// Token: 0x04000245 RID: 581
	private const float CAN_SEE_RAY_HORIZONTAL_JITTER = 0.05f;

	// Token: 0x04000246 RID: 582
	private const float CAN_SEE_RAY_VERTICAL_JITTER_PRONE = 0f;

	// Token: 0x04000247 RID: 583
	private const float CAN_SEE_RAY_VERTICAL_JITTER = 0.1f;

	// Token: 0x04000248 RID: 584
	private const float ACTOR_INTERACTION_MAX_REACTION_TIME = 0.7f;

	// Token: 0x04000249 RID: 585
	private const int MAX_INTERACTION_UPDATES_PER_FRAME = 200;

	// Token: 0x0400024A RID: 586
	private const int MAX_INTERACTION_VEHICLE_UPDATES_PER_FRAME = 10;

	// Token: 0x0400024B RID: 587
	public const float PRONE_CAN_SEE_RAY_VERTICAL_OFFSET = 0.2f;

	// Token: 0x0400024C RID: 588
	private const float ACTOR_ALERT_HEARING_DISTANCE = 50f;

	// Token: 0x0400024D RID: 589
	private const int MIN_LOOKUP_ARRAY_DIMENSION = 64;

	// Token: 0x0400024E RID: 590
	private const float MIN_SMOKE_TARGET_SPACING = 50f;

	// Token: 0x0400024F RID: 591
	private const float CAN_HEAR_MOVEMENT_PROXIMITY_SOUNDS_RANGE = 15f;

	// Token: 0x04000250 RID: 592
	public const int EXPLOSION_BLOCKING_MASK = 1;

	// Token: 0x04000251 RID: 593
	public const float MAX_BALANCE_DAMAGE_THROUGH_WALL = 20f;

	// Token: 0x04000252 RID: 594
	private const float KICK_ACTOR_AMMO_RESERVE_DELAY = 0.4f;

	// Token: 0x04000253 RID: 595
	private const string BOT_NAME_FILE = "botnames.txt";

	// Token: 0x04000254 RID: 596
	[NonSerialized]
	public ActorManager.ExplosionEvent OnExplosion = new ActorManager.ExplosionEvent();

	// Token: 0x04000255 RID: 597
	private int currentCapturePointUpdateIndex;

	// Token: 0x04000256 RID: 598
	private int[] randomTeamSeed;

	// Token: 0x04000257 RID: 599
	private bool isReady;

	// Token: 0x04000258 RID: 600
	public static ActorManager instance;

	// Token: 0x04000259 RID: 601
	public GameObject actorPrefab;

	// Token: 0x0400025A RID: 602
	public Material[] teamActorMaterial;

	// Token: 0x0400025B RID: 603
	public Mesh boxManMesh;

	// Token: 0x0400025C RID: 604
	public GameObject[] defaultVehiclePrefabs;

	// Token: 0x0400025D RID: 605
	public GameObject[] defaultTurretPrefabs;

	// Token: 0x0400025E RID: 606
	public GameObject ammoReservePrefab;

	// Token: 0x0400025F RID: 607
	public GameObject tapePrefab;

	// Token: 0x04000260 RID: 608
	public ActorSkin defaultActorSkin;

	// Token: 0x04000261 RID: 609
	public ActorNameSet defaultActorNameSet;

	// Token: 0x04000262 RID: 610
	private ActorNameSet actorNameSet;

	// Token: 0x04000263 RID: 611
	public ActorAccessory walkmanAccessory;

	// Token: 0x04000264 RID: 612
	public GameObject genericExplosionPrefab;

	// Token: 0x04000265 RID: 613
	public ExplodingProjectile.ExplosionConfiguration bloodExplosion;

	// Token: 0x04000266 RID: 614
	public AnimationCurve linearFalloffCurve;

	// Token: 0x04000267 RID: 615
	public AnimationCurve sharpFalloffCurve;

	// Token: 0x04000268 RID: 616
	public AnimationCurve smoothStepFalloffCurve;

	// Token: 0x04000269 RID: 617
	private int team0Bots;

	// Token: 0x0400026A RID: 618
	private int team1Bots;

	// Token: 0x0400026B RID: 619
	private int nextActorIndex;

	// Token: 0x0400026C RID: 620
	private int[] nextActorIndexTeam = new int[2];

	// Token: 0x0400026D RID: 621
	[NonSerialized]
	public ActorSkin[] actorSkin;

	// Token: 0x0400026E RID: 622
	[NonSerialized]
	public ActorMaterials[] actorMaterials;

	// Token: 0x0400026F RID: 623
	[NonSerialized]
	public bool[] hasDefaultSkin;

	// Token: 0x04000270 RID: 624
	[NonSerialized]
	public SpawnPoint[] spawnPoints;

	// Token: 0x04000271 RID: 625
	[NonSerialized]
	public List<Actor> actors;

	// Token: 0x04000272 RID: 626
	[NonSerialized]
	public List<AiActorController> aiActorControllers;

	// Token: 0x04000273 RID: 627
	[NonSerialized]
	public Actor player;

	// Token: 0x04000274 RID: 628
	[NonSerialized]
	public List<Vehicle> vehicles;

	// Token: 0x04000275 RID: 629
	[NonSerialized]
	public List<Ladder> ladders;

	// Token: 0x04000276 RID: 630
	[NonSerialized]
	public List<DamageZone> damageZones;

	// Token: 0x04000277 RID: 631
	[NonSerialized]
	public List<SpeedLimitZone> speedLimitZones;

	// Token: 0x04000278 RID: 632
	[NonSerialized]
	public bool debug;

	// Token: 0x04000279 RID: 633
	private List<Actor>[] actorsOnTeam;

	// Token: 0x0400027A RID: 634
	private Dictionary<int, List<Actor>> aliveActors;

	// Token: 0x0400027B RID: 635
	private List<Squad>[] squadsOnTeam;

	// Token: 0x0400027C RID: 636
	private bool[,] actorsCanSeeEachOther;

	// Token: 0x0400027D RID: 637
	[NonSerialized]
	public bool[] actorCanHearEnemy;

	// Token: 0x0400027E RID: 638
	private bool[] teammateActorCanSeePlayer;

	// Token: 0x0400027F RID: 639
	private float[] teammateActorDistanceToPlayer;

	// Token: 0x04000280 RID: 640
	private float[,] actorsDistance;

	// Token: 0x04000281 RID: 641
	private Actor[] nextTarget;

	// Token: 0x04000282 RID: 642
	private Actor[] pendingNextTarget;

	// Token: 0x04000283 RID: 643
	private float[] nextTargetDistance;

	// Token: 0x04000284 RID: 644
	private int interactionUpdatesPerFrame = 1;

	// Token: 0x04000285 RID: 645
	private int lastAiUpdateEnd;

	// Token: 0x04000286 RID: 646
	private int playerActorIndex;

	// Token: 0x04000287 RID: 647
	[NonSerialized]
	public List<ActorData> actorData;

	// Token: 0x04000288 RID: 648
	private bool lookupTablesInitialized;

	// Token: 0x04000289 RID: 649
	private Dictionary<SpawnPoint, List<Vehicle>> availableVehiclesBySpawnPoint;

	// Token: 0x0400028A RID: 650
	private List<ActorManager.SmokeTarget>[] smokeTargets;

	// Token: 0x0400028B RID: 651
	private int[] smokeTargetUpdateIndex;

	// Token: 0x0400028C RID: 652
	[NonSerialized]
	public List<TriggerVolume.RuntimeData> activeTriggers;

	// Token: 0x0400028D RID: 653
	[NonSerialized]
	public List<TriggerVolumeActorState> triggerStates;

	// Token: 0x0400028E RID: 654
	private const float TARGET_AI_TICK_RATE_HZ = 5f;

	// Token: 0x0400028F RID: 655
	private const float TARGET_AI_TICK_RATE_PERIOD_TIME = 0.2f;

	// Token: 0x04000290 RID: 656
	private const int MAX_AI_TICKS_PER_FRAME = 50;

	// Token: 0x04000291 RID: 657
	private int ai_ticked;

	// Token: 0x04000292 RID: 658
	private int ai_maxTickCount;

	// Token: 0x04000293 RID: 659
	private float ai_tickFillRatio;

	// Token: 0x04000294 RID: 660
	private static List<int> attackingActors = new List<int>(64);

	// Token: 0x04000295 RID: 661
	private int interactionIteratorTeam0;

	// Token: 0x04000296 RID: 662
	private int interactionIteratorTeam1;

	// Token: 0x04000297 RID: 663
	private int interactionIteratorVehicle;

	// Token: 0x04000298 RID: 664
	private int[] interactionIteratorSquads = new int[2];

	// Token: 0x04000299 RID: 665
	private const int MAX_AI_ACTORS_PLAYING_FOOTSTEPS = 8;

	// Token: 0x0400029A RID: 666
	public static readonly Vector3 ACTOR_INSTANTIATE_POSITION = new Vector3(0f, 10000f, 0f);

	// Token: 0x0400029B RID: 667
	private ExplosionInfo lastExplosion;

	// Token: 0x0200006A RID: 106
	public class ExplosionEvent : UnityEvent<ExplosionInfo>
	{
	}

	// Token: 0x0200006B RID: 107
	public enum TargetSight
	{
		// Token: 0x0400029D RID: 669
		CannotSee,
		// Token: 0x0400029E RID: 670
		CanReveal,
		// Token: 0x0400029F RID: 671
		CanTarget
	}

	// Token: 0x0200006C RID: 108
	public class SmokeTarget
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00004135 File Offset: 0x00002335
		public SmokeTarget(Vector3 position, int team, float expireTime)
		{
			this.position = position;
			this.team = team;
			this.expireTime = expireTime;
			this.deployed = false;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00004159 File Offset: 0x00002359
		public bool IsExpired()
		{
			return Time.time > this.expireTime;
		}

		// Token: 0x040002A0 RID: 672
		public Vector3 position;

		// Token: 0x040002A1 RID: 673
		public float expireTime;

		// Token: 0x040002A2 RID: 674
		public int team;

		// Token: 0x040002A3 RID: 675
		public bool deployed;
	}
}

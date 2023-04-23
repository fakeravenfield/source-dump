using System;
using System.Collections;
using UnityEngine;

namespace Ravenfield.SpecOps
{
	// Token: 0x020003A9 RID: 937
	public class SpecOpsDialog
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x000123B1 File Offset: 0x000105B1
		private static string PickOne(string[] lines)
		{
			return lines[UnityEngine.Random.Range(0, lines.Length)];
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000A241C File Offset: 0x000A061C
		public SpecOpsDialog(SpecOpsMode specOps)
		{
			this.specOps = specOps;
			this.playerIsBlue = (this.specOps.attackingTeam == 0);
			bool nightMode = GameManager.GameParameters().nightMode;
			if (!this.playerIsBlue)
			{
				this.actorNeutral = "p gruntred";
				this.actorFrown = "p gruntred frown";
				this.actorGrin = "p gruntred grin";
				this.hqFrown = "p unknown";
				this.hqNeutral = "p unknown";
				this.exfilActor = "p gruntred";
			}
			else if (nightMode)
			{
				this.actorNeutral = "p talon nvg";
				this.actorFrown = "p talon nvg";
				this.actorGrin = "p talon nvg";
			}
			if (nightMode)
			{
				this.eyes = "p eyes nvg";
				for (int i = 0; i < SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO.Length; i++)
				{
					if (SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO[i].actor == "p eyes")
					{
						SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO[i].actor = "p eyes nvg";
					}
				}
				return;
			}
			this.eyes = "p eyes";
			for (int j = 0; j < SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO.Length; j++)
			{
				if (SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO[j].actor == "p eyes nvg")
				{
					SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO[j].actor = "p eyes";
				}
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000A25B4 File Offset: 0x000A07B4
		public void UseCustomSkinPortraits()
		{
			this.actorNeutral = RuntimePortraitGenerator.POSE_NAMES[this.specOps.attackingTeam];
			this.actorFrown = RuntimePortraitGenerator.POSE_NAMES[this.specOps.attackingTeam];
			this.actorGrin = RuntimePortraitGenerator.POSE_NAMES[this.specOps.attackingTeam];
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000123BE File Offset: 0x000105BE
		public void OnPlayerAssumesControl()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.StartPlaying));
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000123D2 File Offset: 0x000105D2
		public void OnC4Planted()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.C4Planted));
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000123E6 File Offset: 0x000105E6
		public void OnC4OrderCannotComply()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.C4CannotComply));
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000123FA File Offset: 0x000105FA
		public void PlayAreaClearDialog(SpawnPoint spawn)
		{
			this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.AreaClear), spawn);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000A2608 File Offset: 0x000A0808
		public void OnScenarioCompleted(SpecOpsScenario scenario)
		{
			if (scenario is ClearScenario)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.ClearCompleted), scenario.spawn);
				return;
			}
			if (scenario is DestroyScenario)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.DestroyCompleted), scenario.spawn);
				return;
			}
			if (scenario is AssassinateScenario)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.AssassinationCompleted), scenario.spawn);
				return;
			}
			if (scenario is SabotageScenario)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.SabotageCompleted), scenario.spawn);
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0001240F File Offset: 0x0001060F
		public void OnPatrolNeutralized(SpecOpsPatrol patrol)
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.PatrolNeutralized));
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00012423 File Offset: 0x00010623
		public void OnPatrolInvestigating(SpecOpsPatrol patrol)
		{
			this.PlayDialog(new SpecOpsDialog.DialogTargetPatrolDel(this.PatrolInvestigating), patrol);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00012438 File Offset: 0x00010638
		public void OnMultiplePatrolsInvestigating()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.MultiplePatrolsInvestigating));
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000A2698 File Offset: 0x000A0898
		public void Update()
		{
			foreach (AiActorController aiActorController in this.specOps.attackerSquad.aiMembers)
			{
				if (aiActorController.HasSpottedTarget())
				{
					Order order = (aiActorController.target.controller as AiActorController).squad.order;
					if (order == null)
					{
						break;
					}
					if (order.type == Order.OrderType.PatrolBase && this.specOps.scenarioAtSpawn.ContainsKey(order.source))
					{
						SpecOpsScenario specOpsScenario = this.specOps.scenarioAtSpawn[order.source];
						if (!specOpsScenario.defenderSpottedByAttackers)
						{
							this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.SpottedDefenders), order.source);
							specOpsScenario.defenderSpottedByAttackers = true;
							break;
						}
					}
				}
				else if (!this.reportedTakingFire && aiActorController.IsTakingFire())
				{
					this.reportedTakingFire = true;
					this.PlayDialog(new SpecOpsDialog.DialogDel(this.TakingFire));
				}
			}
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0001244C File Offset: 0x0001064C
		private void PrintHQPositiveReply()
		{
			IngameDialog.PrintActorText(this.hqNeutral, SpecOpsDialog.PickOne(this.playerIsBlue ? SpecOpsDialog.HQ_POSITIVE_RESPONSES : SpecOpsDialog.HQ_POSITIVE_RESPONSES_RED), "");
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00012477 File Offset: 0x00010677
		public void OnAssassinationTargetIdentified()
		{
			if (this.playerIsBlue)
			{
				this.PlayDialog(new SpecOpsDialog.DialogDel(this.AssassinationTargetIdentified));
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00012493 File Offset: 0x00010693
		public void OnIncomingPatrol(SpecOpsPatrol patrol)
		{
			if (this.playerIsBlue)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetPatrolDel(this.IncomingPatrol), patrol);
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000124B0 File Offset: 0x000106B0
		public void OnIncomingHelicopter(Vehicle vehicle)
		{
			if (this.playerIsBlue)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetVehicleDel(this.IncomingHelicopter), vehicle);
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000124CD File Offset: 0x000106CD
		public void OnEnemiesStartRoaming(SpawnPoint spawn)
		{
			if (this.playerIsBlue)
			{
				this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.EnemiesStartRoaming), spawn);
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000124EA File Offset: 0x000106EA
		public void OnExfiltrationStart(SpawnPoint spawn)
		{
			this.PlayDialog(new SpecOpsDialog.DialogTargetSpawnDel(this.StartExfil), spawn);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000124FF File Offset: 0x000106FF
		public void OnExfiltrationTouchdown()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.ExfilTouchdown));
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00012513 File Offset: 0x00010713
		public void OnExfiltrationVictory()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.ExfilVictory));
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00012527 File Offset: 0x00010727
		private IEnumerator AssassinationTargetIdentified()
		{
			IngameDialog.PrintActorText("p eyes intel", string.Format("That's the target officer.", Array.Empty<object>()), "EYES");
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0001252F File Offset: 0x0001072F
		private IEnumerator EnemiesStartRoaming(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.eyes, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.ENEMY_SQUAD_STARTS_ROAMING_LINES), spawn.shortName), "EYES");
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00012545 File Offset: 0x00010745
		private IEnumerator IncomingPatrol(SpecOpsPatrol patrol)
		{
			Vehicle squadVehicle = patrol.squad.squadVehicle;
			if (squadVehicle == null)
			{
				IngameDialog.PrintActorText(this.eyes, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.INCOMING_INFANTRY_PATROL_LINES), Array.Empty<object>()), "EYES");
				yield return new WaitForSeconds(6f);
			}
			else
			{
				IngameDialog.PrintActorText(this.eyes, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.INCOMING_VEHICLE_PATROL_LINES), squadVehicle.name), "EYES");
				yield return new WaitForSeconds(6f);
			}
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0001255B File Offset: 0x0001075B
		private IEnumerator IncomingHelicopter(Vehicle vehicle)
		{
			IngameDialog.PrintActorText(this.eyes, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.INCOMING_HELICOPTER_PATROL_LINES), vehicle.name), "EYES");
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00012571 File Offset: 0x00010771
		private IEnumerator C4Planted()
		{
			yield return new WaitForSeconds(1f);
			IngameDialog.PrintActorText(this.actorNeutral, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.C4_PLANTED_LINES), Array.Empty<object>()), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00012580 File Offset: 0x00010780
		private IEnumerator C4CannotComply()
		{
			yield return new WaitForSeconds(1f);
			IngameDialog.PrintActorText(this.actorNeutral, string.Format("Negative, explosives are already planted!", Array.Empty<object>()), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0001258F File Offset: 0x0001078F
		private IEnumerator AreaClear(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorGrin, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.AREA_CLEAR_LINES), spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000125A5 File Offset: 0x000107A5
		private IEnumerator PatrolNeutralized()
		{
			IngameDialog.PrintActorText(this.actorGrin, SpecOpsDialog.PickOne(SpecOpsDialog.PATROL_NEUTRALIZED_LINES), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			this.PrintHQPositiveReply();
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000125B4 File Offset: 0x000107B4
		private IEnumerator PatrolInvestigating(SpecOpsPatrol patrol)
		{
			if (this.playerIsBlue)
			{
				Vehicle squadVehicle = patrol.squad.squadVehicle;
				if (squadVehicle == null)
				{
					IngameDialog.PrintActorText(this.eyes, "That flare attracted a nearby patrol.", "EYES");
				}
				else
				{
					IngameDialog.PrintActorText(this.eyes, string.Format("That flare attracted a nearby {0}.", squadVehicle.name), "EYES");
				}
				yield return new WaitForSeconds(6f);
				IngameDialog.Hide();
			}
			yield break;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000125CA File Offset: 0x000107CA
		private IEnumerator MultiplePatrolsInvestigating()
		{
			if (this.playerIsBlue)
			{
				IngameDialog.PrintActorText(this.eyes, "Heads up, that flare attracted multiple patrols!", "EYES");
				yield return new WaitForSeconds(6f);
				IngameDialog.Hide();
			}
			yield break;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000125D9 File Offset: 0x000107D9
		private IEnumerator ClearCompleted(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorGrin, string.Format("We have secured {0}!", spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			this.PrintHQPositiveReply();
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000125EF File Offset: 0x000107EF
		private IEnumerator AssassinationCompleted(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorGrin, string.Format("We neutralized the target officer!", spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			this.PrintHQPositiveReply();
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00012605 File Offset: 0x00010805
		private IEnumerator SabotageCompleted(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorGrin, string.Format("We destroyed the targets at {0}!", spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			this.PrintHQPositiveReply();
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0001261B File Offset: 0x0001081B
		private IEnumerator DestroyCompleted(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorGrin, string.Format("We took out the target vehicle!", spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			this.PrintHQPositiveReply();
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00012631 File Offset: 0x00010831
		private IEnumerator StartPlaying()
		{
			yield return new WaitForSeconds(1f);
			SpecOpsDialog.DialogLine[] array;
			if (this.playerIsBlue)
			{
				if (SpecOpsDialog.hasPlayedEyesIntroduction)
				{
					array = SpecOpsDialog.HQ_START_LINES_BLUE;
				}
				else
				{
					array = SpecOpsDialog.HQ_START_LINES_BLUE_EYES_INTRO;
					SpecOpsDialog.hasPlayedEyesIntroduction = true;
				}
			}
			else
			{
				array = SpecOpsDialog.HQ_START_LINES_RED;
			}
			foreach (SpecOpsDialog.DialogLine dialogLine in array)
			{
				IngameDialog.PrintActorText(dialogLine.actor, dialogLine.text, dialogLine.overrideName);
				yield return new WaitForSeconds(dialogLine.duration);
			}
			SpecOpsDialog.DialogLine[] array2 = null;
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00012640 File Offset: 0x00010840
		private IEnumerator TakingFire()
		{
			IngameDialog.PrintActorText(this.actorFrown, "We are taking fire!", this.GetTalonName());
			yield return new WaitForSeconds(4f);
			if (this.playerIsBlue)
			{
				IngameDialog.PrintActorText(this.hqFrown, SpecOpsDialog.PickOne(SpecOpsDialog.HQ_SUPPORTIVE_RESPONSES), "");
				yield return new WaitForSeconds(4f);
			}
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0001264F File Offset: 0x0001084F
		private IEnumerator SpottedDefenders(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorNeutral, string.Format(SpecOpsDialog.PickOne(SpecOpsDialog.DEFENDERS_SPOTTED_LINES), spawn.shortName), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00012665 File Offset: 0x00010865
		private IEnumerator StartExfil(SpawnPoint spawn)
		{
			IngameDialog.PrintActorText(this.actorNeutral, "All objectives completed, requesting Exfil!", this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.PrintActorText(this.hqFrown, string.Format("Get to the landing zone at {0}.\nWe have a helicopter inbound.", spawn.shortName), "");
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0001267B File Offset: 0x0001087B
		private IEnumerator ExfilTouchdown()
		{
			IngameDialog.PrintActorText(this.exfilActor, "Airlift is touching down now!", "PILOT");
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0001268A File Offset: 0x0001088A
		private IEnumerator ExfilVictory()
		{
			SpecOpsDialog.DialogLine[] array = this.playerIsBlue ? SpecOpsDialog.EXFIL_VICTORY_BLUE : SpecOpsDialog.EXFIL_VICTORY_RED;
			foreach (SpecOpsDialog.DialogLine dialogLine in array)
			{
				IngameDialog.PrintActorText(dialogLine.actor, dialogLine.text, dialogLine.overrideName);
				yield return new WaitForSeconds(dialogLine.duration);
			}
			SpecOpsDialog.DialogLine[] array2 = null;
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00012699 File Offset: 0x00010899
		private IEnumerator StealthVictory()
		{
			if (!this.playerIsBlue)
			{
				string[] victory_LINES_RED = SpecOpsDialog.VICTORY_LINES_RED;
			}
			else
			{
				string[] victory_LINES = SpecOpsDialog.VICTORY_LINES;
			}
			IngameDialog.PrintActorText(this.actorGrin, "All objectives completed! We are exfiltrating on foot.", "");
			yield return new WaitForSeconds(6f);
			if (this.playerIsBlue)
			{
				IngameDialog.PrintActorText(this.hqNeutral, "HQ copies all. Flawless work TALON!", "");
			}
			else
			{
				IngameDialog.PrintActorText(this.hqNeutral, "Splendid. Maybe I should give you a promotion...", "");
			}
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000126A8 File Offset: 0x000108A8
		private IEnumerator PlayerDied()
		{
			IngameDialog.PrintActorText(this.actorFrown, string.Format("Man down, man down! We lost {0}!", ActorManager.instance.player.name), this.GetTalonName());
			yield return new WaitForSeconds(4f);
			IngameDialog.PrintActorText(this.hqFrown, SpecOpsDialog.PickOne(this.playerIsBlue ? SpecOpsDialog.PLAYER_DOWN_LINES : SpecOpsDialog.PLAYER_DOWN_LINES_RED), "");
			yield return new WaitForSeconds(6f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000126B7 File Offset: 0x000108B7
		private IEnumerator TeamWipe()
		{
			yield return new WaitForSeconds(3f);
			IngameDialog.PrintActorText(this.hqFrown, SpecOpsDialog.PickOne(this.playerIsBlue ? SpecOpsDialog.TEAM_WIPE_LINES : SpecOpsDialog.TEAM_WIPE_LINES_RED), "");
			yield return new WaitForSeconds(7f);
			IngameDialog.Hide();
			yield break;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000126C6 File Offset: 0x000108C6
		public void OnVictory()
		{
			this.PlayDialog(new SpecOpsDialog.DialogDel(this.StealthVictory));
			this.muteAllDialog = true;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000126E1 File Offset: 0x000108E1
		public void OnPlayerDied()
		{
			if (this.specOps.AllAttackersDead())
			{
				this.PlayDialog(new SpecOpsDialog.DialogDel(this.TeamWipe));
			}
			else
			{
				this.PlayDialog(new SpecOpsDialog.DialogDel(this.PlayerDied));
			}
			this.muteAllDialog = true;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000A27B0 File Offset: 0x000A09B0
		private string GetTalonName()
		{
			for (int i = 1; i < this.specOps.attackerActors.Length; i++)
			{
				if (!this.specOps.attackerActors[i].dead)
				{
					return this.specOps.attackerActors[i].name;
				}
			}
			return "TALON";
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0001271D File Offset: 0x0001091D
		private void PlayDialog(SpecOpsDialog.DialogDel dialogCoroutine)
		{
			if (this.muteAllDialog)
			{
				return;
			}
			if (this.currentDialog != null)
			{
				this.specOps.StopCoroutine(this.currentDialog);
			}
			this.currentDialog = this.specOps.StartCoroutine(dialogCoroutine());
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000A2804 File Offset: 0x000A0A04
		private void PlayDialog(SpecOpsDialog.DialogTargetSpawnDel dialogCoroutine, SpawnPoint spawn)
		{
			if (this.muteAllDialog || !GameManager.instance.hudEnabled)
			{
				return;
			}
			if (this.currentDialog != null)
			{
				this.specOps.StopCoroutine(this.currentDialog);
			}
			this.currentDialog = this.specOps.StartCoroutine(dialogCoroutine(spawn));
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000A2858 File Offset: 0x000A0A58
		private void PlayDialog(SpecOpsDialog.DialogTargetPatrolDel dialogCoroutine, SpecOpsPatrol patrol)
		{
			if (this.muteAllDialog || !GameManager.instance.hudEnabled)
			{
				return;
			}
			if (this.currentDialog != null)
			{
				this.specOps.StopCoroutine(this.currentDialog);
			}
			this.currentDialog = this.specOps.StartCoroutine(dialogCoroutine(patrol));
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000A28AC File Offset: 0x000A0AAC
		private void PlayDialog(SpecOpsDialog.DialogTargetVehicleDel dialogCoroutine, Vehicle vehicle)
		{
			if (this.muteAllDialog || !GameManager.instance.hudEnabled)
			{
				return;
			}
			if (this.currentDialog != null)
			{
				this.specOps.StopCoroutine(this.currentDialog);
			}
			this.currentDialog = this.specOps.StartCoroutine(dialogCoroutine(vehicle));
		}

		// Token: 0x0400195E RID: 6494
		private const string GRUNT_ACTOR_TALON = "p talon";

		// Token: 0x0400195F RID: 6495
		private const string GRUNT_ACTOR_TALON_NIGHT = "p talon nvg";

		// Token: 0x04001960 RID: 6496
		private const string GRUNT_ACTOR_RED_NEUTRAL = "p gruntred";

		// Token: 0x04001961 RID: 6497
		private const string GRUNT_ACTOR_RED_FROWN = "p gruntred frown";

		// Token: 0x04001962 RID: 6498
		private const string GRUNT_ACTOR_RED_GRIN = "p gruntred grin";

		// Token: 0x04001963 RID: 6499
		private const string HQ_ACTOR_NEUTRAL = "p advisor";

		// Token: 0x04001964 RID: 6500
		private const string HQ_ACTOR_FROWN = "p advisor tense";

		// Token: 0x04001965 RID: 6501
		private const string HQ_ACTOR_RED = "p unknown";

		// Token: 0x04001966 RID: 6502
		private const string EXFIL_ACTOR_BLUE = "p grunt";

		// Token: 0x04001967 RID: 6503
		private const string EXFIL_ACTOR_RED = "p gruntred";

		// Token: 0x04001968 RID: 6504
		private const string EYES_ACTOR = "p eyes";

		// Token: 0x04001969 RID: 6505
		private const string EYES_ACTOR_NVG = "p eyes nvg";

		// Token: 0x0400196A RID: 6506
		private const string EYES_ACTOR_ALT = "p eyes intel";

		// Token: 0x0400196B RID: 6507
		private const string EYES_OVERRIDE_NAME = "EYES";

		// Token: 0x0400196C RID: 6508
		private const string HQ_START_MESSAGE_BLUE = "Good luck team!";

		// Token: 0x0400196D RID: 6509
		private static SpecOpsDialog.DialogLine[] HQ_START_LINES_BLUE = new SpecOpsDialog.DialogLine[]
		{
			new SpecOpsDialog.DialogLine("p advisor", "TALON, the mission is go.", 6f)
		};

		// Token: 0x0400196E RID: 6510
		private static SpecOpsDialog.DialogLine[] HQ_START_LINES_BLUE_EYES_INTRO = new SpecOpsDialog.DialogLine[]
		{
			new SpecOpsDialog.DialogLine("p advisor", "TALON, the mission is go. Today's operation is supported by a recon operator.", 6f),
			new SpecOpsDialog.DialogLine("p advisor tense", "EYES, do you read?", 3f),
			new SpecOpsDialog.DialogLine("p eyes", "I'm in position with eyes on TALON.", 6f)
			{
				overrideName = "EYES"
			},
			new SpecOpsDialog.DialogLine("p advisor", "Great!\nEYES will be relaying enemy movement.", 6f),
			new SpecOpsDialog.DialogLine("p advisor", "That should give you an edge today.", 4f),
			new SpecOpsDialog.DialogLine("p advisor", "Good luck team!", 4f)
		};

		// Token: 0x0400196F RID: 6511
		private static readonly SpecOpsDialog.DialogLine[] HQ_START_LINES_RED = new SpecOpsDialog.DialogLine[]
		{
			new SpecOpsDialog.DialogLine("p unknown", "Move swiftly and quietly.", 6f)
		};

		// Token: 0x04001970 RID: 6512
		private static readonly string[] HQ_POSITIVE_RESPONSES = new string[]
		{
			"Great job, keep it up!",
			"Nice one team!",
			"Keep it up, TALON!"
		};

		// Token: 0x04001971 RID: 6513
		private static readonly string[] HQ_SUPPORTIVE_RESPONSES = new string[]
		{
			"You can do it, TALON!",
			"Keep your heads down!"
		};

		// Token: 0x04001972 RID: 6514
		private static readonly string[] HQ_POSITIVE_RESPONSES_RED = new string[]
		{
			"HQ copies all.",
			"Affirmative.",
			"Roger.",
			"Splendid."
		};

		// Token: 0x04001973 RID: 6515
		private static readonly string[] AREA_CLEAR_LINES = new string[]
		{
			"{0} is clear!",
			"The area is clear!",
			"Area secured.",
			"Clear!",
			"All targets neutralized at {0}."
		};

		// Token: 0x04001974 RID: 6516
		private static readonly string[] PATROL_NEUTRALIZED_LINES = new string[]
		{
			"Enemy patrol has been neutralized!",
			"Enemy patrol is no more.",
			"We neutralized an enemy patrol."
		};

		// Token: 0x04001975 RID: 6517
		private static readonly string[] DEFENDERS_SPOTTED_LINES = new string[]
		{
			"I see targets at {0}.",
			"I've got movement over at {0}.",
			"Targets spotted at {0}.",
			"Looking at {0}, got multiple targets.",
			"Eyes on targets, {0}.",
			"Enemy Man, 200 meters, front!"
		};

		// Token: 0x04001976 RID: 6518
		private static readonly string[] VICTORY_LINES = new string[]
		{
			"All objectives completed!",
			"Excellent work TALON, stand by for exfil!"
		};

		// Token: 0x04001977 RID: 6519
		private static readonly string[] VICTORY_LINES_RED = new string[]
		{
			"Splendid, that seems to be everything taken care of.",
			"Stand by for exfil..."
		};

		// Token: 0x04001978 RID: 6520
		private static readonly string[] PLAYER_DOWN_LINES = new string[]
		{
			"No... Damnit!\nFall back TALON, we need to regroup!"
		};

		// Token: 0x04001979 RID: 6521
		private static readonly string[] PLAYER_DOWN_LINES_RED = new string[]
		{
			"Stand your ground soldiers, we need to finish this fight."
		};

		// Token: 0x0400197A RID: 6522
		private static readonly string[] TEAM_WIPE_LINES = new string[]
		{
			"TALON? Come in TALON...\nTAAAALOOOOOOOOOON!"
		};

		// Token: 0x0400197B RID: 6523
		private static readonly string[] TEAM_WIPE_LINES_RED = new string[]
		{
			"...\nI should never have sent them in."
		};

		// Token: 0x0400197C RID: 6524
		private static readonly string[] C4_PLANTED_LINES = new string[]
		{
			"Explosive planted, get clear!",
			"Get ready for some fireworks!",
			30f.ToString() + " seconds to detonation...",
			"Get out of there it's gonna blow!"
		};

		// Token: 0x0400197D RID: 6525
		private static readonly string[] INCOMING_INFANTRY_PATROL_LINES = new string[]
		{
			"Eyes on enemy patrol heading your way.",
			"Lay low, you've got an incoming patrol.",
			"Get to cover, you've got an incoming enemy patrol."
		};

		// Token: 0x0400197E RID: 6526
		private static readonly string[] INCOMING_VEHICLE_PATROL_LINES = new string[]
		{
			"You have an inbound {0}.",
			"Eyes open, a nearby {0} is patrolling the area.",
			"Stay low, eyes on incoming {0}.",
			"Heads up, you have an incoming {0}."
		};

		// Token: 0x0400197F RID: 6527
		private static readonly string[] INCOMING_HELICOPTER_PATROL_LINES = new string[]
		{
			"I'm seeing an inbound {0}. Better stay out of it's search area!",
			"Incoming {0}, looks like it's heading for that flare!"
		};

		// Token: 0x04001980 RID: 6528
		private static readonly string[] ENEMY_SQUAD_STARTS_ROAMING_LINES = new string[]
		{
			"The enemy squad at {0} are leaving their posts.",
			"I see enemy soldier leaving {0}, they are looking for you."
		};

		// Token: 0x04001981 RID: 6529
		private static SpecOpsDialog.DialogLine[] EXFIL_VICTORY_BLUE = new SpecOpsDialog.DialogLine[]
		{
			new SpecOpsDialog.DialogLine("p grunt", "HQ, TALON is on board. Leaving the danger zone now!", 6f),
			new SpecOpsDialog.DialogLine("p advisor", "Excellent work TALON!", 4f)
		};

		// Token: 0x04001982 RID: 6530
		private static SpecOpsDialog.DialogLine[] EXFIL_VICTORY_RED = new SpecOpsDialog.DialogLine[]
		{
			new SpecOpsDialog.DialogLine("p gruntred", "Team is on board, returning to base!", 6f),
			new SpecOpsDialog.DialogLine("p unknown", "Splendid.", 4f)
		};

		// Token: 0x04001983 RID: 6531
		private static bool hasPlayedEyesIntroduction = false;

		// Token: 0x04001984 RID: 6532
		private SpecOpsMode specOps;

		// Token: 0x04001985 RID: 6533
		private Coroutine currentDialog;

		// Token: 0x04001986 RID: 6534
		private bool muteAllDialog;

		// Token: 0x04001987 RID: 6535
		private bool reportedTakingFire;

		// Token: 0x04001988 RID: 6536
		private bool playerIsBlue;

		// Token: 0x04001989 RID: 6537
		private string actorNeutral = "p talon";

		// Token: 0x0400198A RID: 6538
		private string actorFrown = "p talon";

		// Token: 0x0400198B RID: 6539
		private string actorGrin = "p talon";

		// Token: 0x0400198C RID: 6540
		private string hqNeutral = "p advisor";

		// Token: 0x0400198D RID: 6541
		private string hqFrown = "p advisor tense";

		// Token: 0x0400198E RID: 6542
		private string eyes = "p eyes";

		// Token: 0x0400198F RID: 6543
		private string exfilActor = "p grunt";

		// Token: 0x020003AA RID: 938
		// (Invoke) Token: 0x06001768 RID: 5992
		private delegate IEnumerator DialogDel();

		// Token: 0x020003AB RID: 939
		// (Invoke) Token: 0x0600176C RID: 5996
		private delegate IEnumerator DialogTargetSpawnDel(SpawnPoint spawn);

		// Token: 0x020003AC RID: 940
		// (Invoke) Token: 0x06001770 RID: 6000
		private delegate IEnumerator DialogTargetPatrolDel(SpecOpsPatrol patrol);

		// Token: 0x020003AD RID: 941
		// (Invoke) Token: 0x06001774 RID: 6004
		private delegate IEnumerator DialogTargetVehicleDel(Vehicle vehicle);

		// Token: 0x020003AE RID: 942
		private struct DialogLine
		{
			// Token: 0x06001777 RID: 6007 RVA: 0x00012758 File Offset: 0x00010958
			public DialogLine(string actor, string text, float duration)
			{
				this.actor = actor;
				this.text = text;
				this.duration = duration;
				this.overrideName = "";
			}

			// Token: 0x04001990 RID: 6544
			public string actor;

			// Token: 0x04001991 RID: 6545
			public string text;

			// Token: 0x04001992 RID: 6546
			public string overrideName;

			// Token: 0x04001993 RID: 6547
			public float duration;
		}
	}
}

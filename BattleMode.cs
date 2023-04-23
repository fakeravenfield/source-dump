using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000171 RID: 369
public class BattleMode : GameModeBase
{
	// Token: 0x0600098F RID: 2447 RVA: 0x0006BB18 File Offset: 0x00069D18
	public override void StartGame()
	{
		base.StartGame();
		ActorManager.instance.CreateDefaultAiActors(false);
		this.ticketsPerBattalions = BattleMode.TICKET_COUNT[GameManager.instance.gameModeParameters.gameLength];
		this.tickets = new int[]
		{
			this.ticketsPerBattalions,
			this.ticketsPerBattalions
		};
		this.InitializeMode();
		base.StartCoroutine(this.Drain());
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0006BB84 File Offset: 0x00069D84
	private void InitializeMode()
	{
		GameModeParameters gameModeParameters = GameManager.GameParameters();
		if (gameModeParameters.useConquestRules)
		{
			this.remainingBattalions = gameModeParameters.conquestBattalions;
		}
		this.NewTicketWave(0);
		this.NewTicketWave(1);
		if (GameManager.IsSpectating())
		{
			this.allowTicketBleedTime = Time.time + 60f;
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0006BBD4 File Offset: 0x00069DD4
	private void Update()
	{
		this.ownedFlags[0] = 0;
		this.ownedFlags[1] = 0;
		foreach (SpawnPoint spawnPoint in ActorManager.instance.spawnPoints)
		{
			if (spawnPoint.owner >= 0)
			{
				this.ownedFlags[spawnPoint.owner]++;
			}
		}
		this.blueFlagLabel.text = this.ownedFlags[0].ToString();
		this.redFlagLabel.text = this.ownedFlags[1].ToString();
		int num = this.bleedTeam;
		if (this.ownedFlags[0] >= this.ownedFlags[1] * 2)
		{
			this.bleedTeam = 1;
			this.noFlagsBleed = (this.ownedFlags[1] == 0);
		}
		else if (this.ownedFlags[1] >= this.ownedFlags[0] * 2)
		{
			this.bleedTeam = 0;
			this.noFlagsBleed = (this.ownedFlags[0] == 0);
		}
		else
		{
			this.bleedTeam = -1;
		}
		if (this.bleedTeam != num)
		{
			if (num >= 0)
			{
				this.StopBleeding(num);
			}
			if (this.bleedTeam >= 0)
			{
				this.StartBleeding(this.bleedTeam);
			}
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00008669 File Offset: 0x00006869
	private IEnumerator Drain()
	{
		for (;;)
		{
			float seconds = this.noFlagsBleed ? 0.2f : 4f;
			yield return new WaitForSeconds(seconds);
			if (Time.time > this.allowTicketBleedTime && this.bleedTeam >= 0)
			{
				this.DrainTicket(this.bleedTeam);
			}
		}
		yield break;
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00008678 File Offset: 0x00006878
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		base.PlayerAcceptedLoadoutFirstTime();
		this.allowTicketBleedTime = Time.time + 60f;
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0006BCFC File Offset: 0x00069EFC
	public void StartBleeding(int team)
	{
		this.centerLabel.text = ((team == 0) ? "<" : ">");
		BattleInfantryPanel activePanel = this.GetActivePanel(team);
		if (activePanel != null)
		{
			activePanel.StartFlashing();
		}
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0006BD3C File Offset: 0x00069F3C
	public void StopBleeding(int team)
	{
		this.centerLabel.text = "-";
		BattleInfantryPanel activePanel = this.GetActivePanel(team);
		if (activePanel != null)
		{
			activePanel.StopFlashing();
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00008691 File Offset: 0x00006891
	public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
	{
		base.ActorDied(actor, position, wasSilentKill);
		if (!wasSilentKill)
		{
			this.DrainTicket(actor.team);
		}
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0006BD70 File Offset: 0x00069F70
	private void DrainTicket(int team)
	{
		if (GameManager.gameOver)
		{
			return;
		}
		this.tickets[team]--;
		if (this.remainingBattalions[team] == 1)
		{
			this.maxActiveSoldiersAllowed[team] = Mathf.Min(this.maxActiveSoldiersAllowed[team], this.tickets[team]);
		}
		if (this.tickets[team] <= 0)
		{
			this.OnNoTicketsRemaining(team);
			return;
		}
		this.UpdateTicketLabel(team);
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0006BDD8 File Offset: 0x00069FD8
	private void OnNoTicketsRemaining(int team)
	{
		this.remainingBattalions[team] = Mathf.Max(0, this.remainingBattalions[team] - 1);
		if (this.remainingBattalions[team] > 0)
		{
			OverlayUi.ShowOverlayText(string.Format("{0} LOST A BATTALION", GameManager.instance.GetRichTextColorTeamName(team)), 3.5f);
			this.NewTicketWave(team);
			return;
		}
		this.UpdateInfantryPanels(team);
		this.Win(1 - team);
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0006BE40 File Offset: 0x0006A040
	private void NewTicketWave(int team)
	{
		int num = this.remainingBattalions[team] - 1;
		this.maxActiveSoldiersAllowed[team] = Mathf.RoundToInt(BattleMode.MAX_SOLDIERS_MULTIPLIER[num] * (float)ActorManager.ActorsOnTeam(team).Count);
		this.tickets[team] = this.ticketsPerBattalions;
		this.UpdateInfantryPanels(team);
		this.UpdateTicketLabel(team);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x000086AB File Offset: 0x000068AB
	private void UpdateTicketLabel(int team)
	{
		this.GetActivePanel(team).SetTicketCount(this.tickets[team]);
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0006BE98 File Offset: 0x0006A098
	private void UpdateInfantryPanels(int team)
	{
		BattleInfantryPanel[] infantryPanels = this.GetInfantryPanels(team);
		for (int i = 0; i < 3; i++)
		{
			if (i == this.remainingBattalions[team] - 1)
			{
				infantryPanels[i].SetActive(true);
			}
			else if (i < this.remainingBattalions[team])
			{
				infantryPanels[i].SetActive(false);
				infantryPanels[i].SetTicketCount(this.ticketsPerBattalions);
			}
			else
			{
				infantryPanels[i].Die();
			}
		}
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x000086C1 File Offset: 0x000068C1
	private BattleInfantryPanel[] GetInfantryPanels(int team)
	{
		if (team != 0)
		{
			return this.redInfantryPanels;
		}
		return this.blueInfantryPanels;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x000086D3 File Offset: 0x000068D3
	private BattleInfantryPanel GetActivePanel(int team)
	{
		if (this.remainingBattalions[team] == 0)
		{
			return null;
		}
		return this.GetInfantryPanels(team)[this.remainingBattalions[team] - 1];
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x000086F3 File Offset: 0x000068F3
	public override void Win(int winningTeam)
	{
		VictoryUi.EndGame(winningTeam, true);
		BattleResult.SetWinner(winningTeam);
		BattleResult.AppendBattalionResult(this.remainingBattalions[0], this.remainingBattalions[1]);
		BattleResult.latest.remainingBattalions[1 - winningTeam] = 0;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0006BF00 File Offset: 0x0006A100
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
		BattleResult.AppendBattalionResult(this.remainingBattalions[0], this.remainingBattalions[1]);
		BattleResult.latest.remainingBattalions[num] = 0;
	}

	// Token: 0x04000A77 RID: 2679
	private const int MAX_BATTALIONS = 3;

	// Token: 0x04000A78 RID: 2680
	private const float DRAIN_PERIOD_TIME = 4f;

	// Token: 0x04000A79 RID: 2681
	private const float DRAIN_NO_FLAGS_PERIOD_TIME = 0.2f;

	// Token: 0x04000A7A RID: 2682
	private const int BLEED_TICKETS_NO_FLAGS = 5;

	// Token: 0x04000A7B RID: 2683
	private const float ALLOW_BLEED_START_TIME = 60f;

	// Token: 0x04000A7C RID: 2684
	private static readonly float[] MAX_SOLDIERS_MULTIPLIER = new float[]
	{
		0.6f,
		0.8f,
		1f
	};

	// Token: 0x04000A7D RID: 2685
	private static readonly int[] TICKET_COUNT = new int[]
	{
		60,
		100,
		200,
		400
	};

	// Token: 0x04000A7E RID: 2686
	public BattleInfantryPanel[] blueInfantryPanels;

	// Token: 0x04000A7F RID: 2687
	public BattleInfantryPanel[] redInfantryPanels;

	// Token: 0x04000A80 RID: 2688
	public Text blueFlagLabel;

	// Token: 0x04000A81 RID: 2689
	public Text redFlagLabel;

	// Token: 0x04000A82 RID: 2690
	public Text centerLabel;

	// Token: 0x04000A83 RID: 2691
	private int[] remainingBattalions = new int[]
	{
		3,
		3
	};

	// Token: 0x04000A84 RID: 2692
	private int[] tickets;

	// Token: 0x04000A85 RID: 2693
	private int ticketsPerBattalions;

	// Token: 0x04000A86 RID: 2694
	private int[] ownedFlags = new int[2];

	// Token: 0x04000A87 RID: 2695
	private int bleedTeam = -1;

	// Token: 0x04000A88 RID: 2696
	private bool noFlagsBleed;

	// Token: 0x04000A89 RID: 2697
	private float allowTicketBleedTime = float.MaxValue;
}

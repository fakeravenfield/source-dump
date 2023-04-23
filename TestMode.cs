using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class TestMode : GameModeBase
{
	// Token: 0x06000B2F RID: 2863 RVA: 0x00074220 File Offset: 0x00072420
	private void Start()
	{
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			spawnPoints[i].SetOwner(GameManager.PlayerTeam(), false);
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00074254 File Offset: 0x00072454
	public override void StartGame()
	{
		base.StartGame();
		foreach (Actor actor in ActorManager.instance.InitializeAiActors(3, 0)[0])
		{
			BotPathVisualizer.InstantiateForBot(actor.controller as AiActorController);
		}
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x000094F2 File Offset: 0x000076F2
	private void Update()
	{
		this.OnSpawnWave();
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x000094FA File Offset: 0x000076FA
	public override void PlayerAcceptedLoadoutFirstTime()
	{
		base.PlayerAcceptedLoadoutFirstTime();
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x000742C4 File Offset: 0x000724C4
	public override void OnSpawnWave()
	{
		IEnumerable<Actor> enumerable = from a in ActorManager.ActorsOnTeam(GameManager.PlayerTeam())
		where a.IsReadyToSpawn()
		select a;
		Actor actor = (from a in enumerable
		where !a.aiControlled
		select a).FirstOrDefault<Actor>();
		SpawnPoint spawnPoint = (actor != null) ? actor.controller.SelectedSpawnPoint() : null;
		if (spawnPoint == null)
		{
			return;
		}
		actor.SpawnAt(spawnPoint.GetSpawnPosition(), Quaternion.identity, null);
		foreach (Actor actor2 in enumerable)
		{
			if (actor2.aiControlled)
			{
				actor2.SpawnAt(spawnPoint.GetSpawnPosition(), Quaternion.identity, null);
				actor2.controller.ChangeToSquad(FpsActorController.instance.playerSquad);
			}
		}
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0006E164 File Offset: 0x0006C364
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
	}
}

using System;
using Ravenfield.Modes.Scripted;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class ScriptedMode : GameModeBase
{
	// Token: 0x06000A3F RID: 2623 RVA: 0x0006E4B8 File Offset: 0x0006C6B8
	public static int Trigger(string trigger)
	{
		ScriptedMode scriptedMode = GameModeBase.instance as ScriptedMode;
		if (scriptedMode == null)
		{
			return 0;
		}
		int hashCode = trigger.ToLowerInvariant().GetHashCode();
		int num = 0;
		TriggeredComponent[] array = scriptedMode.triggeredComponents;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].OnGlobalTrigger(hashCode))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00008DC9 File Offset: 0x00006FC9
	private static bool CanBeReusedForSpawn(Actor actor)
	{
		return actor.dead;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0006E514 File Offset: 0x0006C714
	public static Actor SpawnBot(Vector3 position, ActorSpawner spawner)
	{
		Actor actor = null;
		int num = (spawner.team == ActorSpawner.Team.Team0) ? 0 : 1;
		foreach (Actor actor2 in ActorManager.instance.actors)
		{
			if (actor2.aiControlled && actor2.team == num && ScriptedMode.CanBeReusedForSpawn(actor2))
			{
				actor = actor2;
				break;
			}
		}
		if (actor == null)
		{
			actor = ActorManager.instance.CreateAIActor(num);
		}
		ScriptedMode.lastLoadout = spawner.loadout;
		GameModeBase.instance.SpawnBotActor(actor, position, new AiActorController.LoadoutPickStrategy());
		return actor;
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
	public static Actor SpawnPlayer(Vector3 position, ActorSpawner spawner)
	{
		Actor actor = FpsActorController.instance.actor;
		if (!actor.dead)
		{
			Debug.LogError("Trying to spawn player while being alive");
			return actor;
		}
		ScriptedMode.lastLoadout = spawner.loadout;
		actor.SpawnAt(position, Quaternion.identity, null);
		return actor;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00008DD1 File Offset: 0x00006FD1
	protected override void Awake()
	{
		base.Awake();
		this.triggeredComponents = UnityEngine.Object.FindObjectsOfType<TriggeredComponent>();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00008DE4 File Offset: 0x00006FE4
	public override void StartGame()
	{
		base.SetupOwnersDefault(false);
		this.StartTrigger();
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00008DF3 File Offset: 0x00006FF3
	private void StartTrigger()
	{
		ScriptedMode.Trigger("start");
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00008E00 File Offset: 0x00007000
	public override WeaponManager.LoadoutSet GetLoadout(Actor actor)
	{
		return ScriptedMode.lastLoadout;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0006E164 File Offset: 0x0006C364
	public override void OnSurrender()
	{
		int num = GameManager.PlayerTeam();
		BattleResult.SetWinner(1 - num);
	}

	// Token: 0x04000B36 RID: 2870
	private TriggeredComponent[] triggeredComponents;

	// Token: 0x04000B37 RID: 2871
	private static WeaponManager.LoadoutSet lastLoadout;
}

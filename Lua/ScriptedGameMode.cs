using System;
using Lua.Wrapper;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000944 RID: 2372
	public class ScriptedGameMode : GameModeBase
	{
		// Token: 0x06003C2E RID: 15406 RVA: 0x0012E110 File Offset: 0x0012C310
		protected override void Awake()
		{
			base.Awake();
			this.script = ScriptedBehaviour.GetScript(base.gameObject);
			this.script.Set("SpawnDeadActorsOfTeam", new Action<WTeam>(delegate(WTeam t)
			{
				base.SpawnDeadActorsOfTeam((int)t, true);
			}));
			this.rules = this.ModifyRules();
			this.rules.CopyTo(GameManager.instance.gameModeParameters);
			base.enabled = this.rules.spawnDeadActors;
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000094F2 File Offset: 0x000076F2
		private void Update()
		{
			this.OnSpawnWave();
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x0012E184 File Offset: 0x0012C384
		private SgmModifyRulesArgs ModifyRules()
		{
			SgmModifyRulesArgs sgmModifyRulesArgs = new SgmModifyRulesArgs();
			sgmModifyRulesArgs.CopyFrom(GameManager.instance.gameModeParameters);
			this.script.Call("on_modify_rules", new object[]
			{
				sgmModifyRulesArgs
			});
			return sgmModifyRulesArgs;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00028B08 File Offset: 0x00026D08
		public override void StartGame()
		{
			base.StartGame();
			if (!this.script.Call("on_start_game", Array.Empty<object>()).CastToBool())
			{
				ActorManager.instance.CreateDefaultAiActors(true);
			}
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00028B37 File Offset: 0x00026D37
		public override void ActorDied(Actor actor, Vector3 position, bool wasSilentKill)
		{
			if (!this.script.Call("on_actor_died", new object[]
			{
				new SgmActorDiedArgs(actor, position, wasSilentKill)
			}).CastToBool())
			{
				base.ActorDied(actor, position, wasSilentKill);
			}
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00028B6A File Offset: 0x00026D6A
		public override void CapturePointChangedOwner(CapturePoint capturePoint, int oldOwner, bool initialOwner)
		{
			if (!this.script.Call("on_point_captured", new object[]
			{
				new SgmPointCapturedArgs(capturePoint, oldOwner, initialOwner)
			}).CastToBool())
			{
				base.CapturePointChangedOwner(capturePoint, oldOwner, initialOwner);
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x00028B9D File Offset: 0x00026D9D
		public override void PlayerAcceptedLoadoutFirstTime()
		{
			if (!this.script.Call("on_loadout_accepted", new object[]
			{
				new SgmLoadoutAcceptedArgs(true)
			}).CastToBool())
			{
				base.PlayerAcceptedLoadoutFirstTime();
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x0006E164 File Offset: 0x0006C364
		public override void OnSurrender()
		{
			int num = GameManager.PlayerTeam();
			BattleResult.SetWinner(1 - num);
		}

		// Token: 0x040030E2 RID: 12514
		private ScriptedBehaviour script;

		// Token: 0x040030E3 RID: 12515
		private SgmModifyRulesArgs rules;
	}
}

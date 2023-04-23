using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A16 RID: 2582
	[Proxy(typeof(WActorManager))]
	public class WActorManagerProxy : IProxy
	{
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600520E RID: 21006 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		public static IList<Actor> actors
		{
			get
			{
				return WActorManager.GetActors();
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x0003C8F7 File Offset: 0x0003AAF7
		public static CapturePoint[] capturePoints
		{
			get
			{
				return WActorManager.GetCapturePoints();
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x0003C8FE File Offset: 0x0003AAFE
		public static IList<Ladder> ladders
		{
			get
			{
				return WActorManager.GetLadders();
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x0003C905 File Offset: 0x0003AB05
		public static ActorProxy player
		{
			get
			{
				return ActorProxy.New(WActorManager.GetPlayer());
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0003C911 File Offset: 0x0003AB11
		public static SpawnPoint[] spawnPoints
		{
			get
			{
				return WActorManager.GetSpawnPoints();
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06005213 RID: 21011 RVA: 0x0003C918 File Offset: 0x0003AB18
		public static IList<Vehicle> vehicles
		{
			get
			{
				return WActorManager.GetVehicles();
			}
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00138EDC File Offset: 0x001370DC
		public static bool ActorCanSeePlayer(ActorProxy a)
		{
			Actor a2 = null;
			if (a != null)
			{
				a2 = a._value;
			}
			return WActorManager.ActorCanSeePlayer(a2);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00138EFC File Offset: 0x001370FC
		public static float ActorDistanceToPlayer(ActorProxy a)
		{
			Actor a2 = null;
			if (a != null)
			{
				a2 = a._value;
			}
			return WActorManager.ActorDistanceToPlayer(a2);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00138F1C File Offset: 0x0013711C
		public static bool ActorsCanSeeEachOther(ActorProxy a, ActorProxy b)
		{
			Actor a2 = null;
			if (a != null)
			{
				a2 = a._value;
			}
			Actor b2 = null;
			if (b != null)
			{
				b2 = b._value;
			}
			return WActorManager.ActorsCanSeeEachOther(a2, b2);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00138F48 File Offset: 0x00137148
		public static float ActorsDistance(ActorProxy a, ActorProxy b)
		{
			Actor a2 = null;
			if (a != null)
			{
				a2 = a._value;
			}
			Actor b2 = null;
			if (b != null)
			{
				b2 = b._value;
			}
			return WActorManager.ActorsDistance(a2, b2);
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0003C91F File Offset: 0x0003AB1F
		public static IList<Actor> ActorsInRange(Vector3Proxy point, float range)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return WActorManager.ActorsInRange(point._value, range);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0003C93B File Offset: 0x0003AB3B
		public static IList<Actor> AliveActorsInRange(Vector3Proxy point, float range)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return WActorManager.AliveActorsInRange(point._value, range);
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0003C957 File Offset: 0x0003AB57
		public static ActorProxy CreateAIActor(WTeam team)
		{
			return ActorProxy.New(WActorManager.CreateAIActor(team));
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0003C964 File Offset: 0x0003AB64
		public static bool Explode(ExplosionInfoProxy info)
		{
			if (info == null)
			{
				throw new ScriptRuntimeException("argument 'info' is nil");
			}
			return WActorManager.Explode(info._value);
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0003C97F File Offset: 0x0003AB7F
		public static IList<Actor> GetActorsOnTeam(WTeam team)
		{
			return WActorManager.GetActorsOnTeam(team);
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0003C987 File Offset: 0x0003AB87
		public static IList<Actor> GetAliveActorsOnTeam(WTeam team)
		{
			return WActorManager.GetAliveActorsOnTeam(team);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0003C98F File Offset: 0x0003AB8F
		public static CapturePoint[] GetCapturePointsOwnedByTeam(WTeam team)
		{
			return WActorManager.GetCapturePointsOwnedByTeam(team);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0003C997 File Offset: 0x0003AB97
		public static int GetNumberOfBotsInTeam(WTeam team)
		{
			return WActorManager.GetNumberOfBotsInTeam(team);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0003C99F File Offset: 0x0003AB9F
		public static SpawnPoint[] GetSpawnPointsOwnedByTeam(WTeam team)
		{
			return WActorManager.GetSpawnPointsOwnedByTeam(team);
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0003C9A7 File Offset: 0x0003ABA7
		public static IList<Squad> GetSquadsOnTeam(WTeam team)
		{
			return WActorManager.GetSquadsOnTeam(team);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0003C9AF File Offset: 0x0003ABAF
		public static ActorSkinProxy GetTeamSkin(WTeam team)
		{
			return ActorSkinProxy.New(WActorManager.GetTeamSkin(team));
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		public static bool IsTeamDead(WTeam team)
		{
			return WActorManager.IsTeamDead(team);
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00138F74 File Offset: 0x00137174
		public static void PlayerTakeOverBot(ActorProxy actor)
		{
			Actor actor2 = null;
			if (actor != null)
			{
				actor2 = actor._value;
			}
			WActorManager.PlayerTakeOverBot(actor2);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
		public static SpawnPointProxy RandomEnemySpawnPoint(WTeam team)
		{
			return SpawnPointProxy.New(WActorManager.RandomEnemySpawnPoint(team));
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0003C9D1 File Offset: 0x0003ABD1
		public static SpawnPointProxy RandomFrontlineSpawnPointForTeam(WTeam team)
		{
			return SpawnPointProxy.New(WActorManager.RandomFrontlineSpawnPointForTeam(team));
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0003C9DE File Offset: 0x0003ABDE
		public static SpawnPointProxy RandomSpawnPoint()
		{
			return SpawnPointProxy.New(WActorManager.RandomSpawnPoint());
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0003C9EA File Offset: 0x0003ABEA
		public static SpawnPointProxy RandomSpawnPointForTeam(WTeam team)
		{
			return SpawnPointProxy.New(WActorManager.RandomSpawnPointForTeam(team));
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00138F94 File Offset: 0x00137194
		public static void SetTeamSkin(WTeam team, ActorSkinProxy skin)
		{
			ActorSkin skin2 = null;
			if (skin != null)
			{
				skin2 = skin._value;
			}
			WActorManager.SetTeamSkin(team, skin2);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0003C9F7 File Offset: 0x0003ABF7
		public static bool TeamHasAnySpawnPoint(WTeam team)
		{
			return WActorManager.TeamHasAnySpawnPoint(team);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0003C9FF File Offset: 0x0003ABFF
		public static IList<Vehicle> VehiclesInRange(Vector3Proxy point, float range)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return WActorManager.VehiclesInRange(point._value, range);
		}
	}
}

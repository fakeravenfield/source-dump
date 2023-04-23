using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200095C RID: 2396
	[Name("ActorManager")]
	public static class WActorManager
	{
		// Token: 0x06003D14 RID: 15636 RVA: 0x0002959F File Offset: 0x0002779F
		[Getter]
		public static IList<Actor> GetActors()
		{
			return ActorManager.instance.actors;
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000295AB File Offset: 0x000277AB
		[Getter]
		public static SpawnPoint[] GetSpawnPoints()
		{
			return ActorManager.instance.spawnPoints;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x0012EAF4 File Offset: 0x0012CCF4
		public static SpawnPoint[] GetSpawnPointsOwnedByTeam(WTeam team)
		{
			return (from s in ActorManager.instance.spawnPoints
			where s.owner == (int)team
			select s).ToArray<SpawnPoint>();
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0012EB30 File Offset: 0x0012CD30
		[Getter]
		public static CapturePoint[] GetCapturePoints()
		{
			return (from s in ActorManager.instance.spawnPoints
			where s is CapturePoint
			select (CapturePoint)s).ToArray<CapturePoint>();
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0012EB94 File Offset: 0x0012CD94
		public static CapturePoint[] GetCapturePointsOwnedByTeam(WTeam team)
		{
			return (from s in ActorManager.instance.spawnPoints
			where s is CapturePoint && s.owner == (int)team
			select (CapturePoint)s).ToArray<CapturePoint>();
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000295B7 File Offset: 0x000277B7
		[Getter]
		[Doc("Get the player actor.[..] This is the same as using ``Player.actor``")]
		public static Actor GetPlayer()
		{
			return ActorManager.instance.player;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000295C3 File Offset: 0x000277C3
		[Getter]
		[Doc("All currently spawned vehicles")]
		public static IList<Vehicle> GetVehicles()
		{
			return ActorManager.instance.vehicles;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000295CF File Offset: 0x000277CF
		[Getter]
		public static IList<Ladder> GetLadders()
		{
			return ActorManager.instance.ladders;
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000295DB File Offset: 0x000277DB
		[Getter]
		public static IList<DamageZone> GetDamageZones()
		{
			return ActorManager.instance.damageZones;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0012EBF4 File Offset: 0x0012CDF4
		[Doc("Returns true if the two actors have direct line of sight to each other.[..] Please note that only checks between actors of different teams are precalculated and fast. Using this method on actors of the same team is slower.")]
		public static bool ActorsCanSeeEachOther(Actor a, Actor b)
		{
			if (!a.aiControlled)
			{
				return WActorManager.ActorCanSeePlayer(b);
			}
			if (!b.aiControlled)
			{
				return WActorManager.ActorCanSeePlayer(a);
			}
			if (a.team == b.team)
			{
				return !a.dead && !b.dead && !Physics.Linecast(a.CenterPosition(), b.CenterPosition(), 8388609);
			}
			bool result;
			try
			{
				result = ActorManager.ActorsCanSeeEachOther(a, b);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x0012EC7C File Offset: 0x0012CE7C
		public static bool ActorCanSeePlayer(Actor a)
		{
			bool result;
			try
			{
				result = ActorManager.ActorCanSeePlayer(a);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0012ECA8 File Offset: 0x0012CEA8
		[Doc("Returns the distance between two actors.[..] Please note that only checks between actors of different teams are precalculated and fast. Using this method on actors of the same team is slower. If the value is not available, returns -1.")]
		public static float ActorsDistance(Actor a, Actor b)
		{
			if (a.team == b.team)
			{
				return Vector3.Distance(a.Position(), b.Position());
			}
			float result;
			try
			{
				result = ActorManager.ActorsDistance(a, b);
			}
			catch (Exception)
			{
				result = -1f;
			}
			return result;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000295E7 File Offset: 0x000277E7
		public static float ActorDistanceToPlayer(Actor a)
		{
			return ActorManager.ActorDistanceToPlayer(a);
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000295EF File Offset: 0x000277EF
		public static IList<Actor> ActorsInRange(Vector3 point, float range)
		{
			return ActorManager.ActorsInRange(point, range);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000295F8 File Offset: 0x000277F8
		public static IList<Actor> AliveActorsInRange(Vector3 point, float range)
		{
			return ActorManager.AliveActorsInRange(point, range);
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x00029601 File Offset: 0x00027801
		public static IList<Vehicle> VehiclesInRange(Vector3 point, float range)
		{
			return ActorManager.VehiclesInRange(point, range);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0002960A File Offset: 0x0002780A
		public static SpawnPoint RandomSpawnPoint()
		{
			return ActorManager.RandomSpawnPoint();
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x00029611 File Offset: 0x00027811
		public static int GetNumberOfBotsInTeam(WTeam team)
		{
			return ActorManager.instance.GetNumberOfBotsInTeam((int)team);
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x0002961E File Offset: 0x0002781E
		[Doc("Returns true if all actors on the team are dead.[..]")]
		public static bool IsTeamDead(WTeam team)
		{
			return ActorManager.AllActorsOnTeamAreDead((int)team);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x00029626 File Offset: 0x00027826
		public static IList<Actor> GetActorsOnTeam(WTeam team)
		{
			return ActorManager.ActorsOnTeam((int)team);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0002962E File Offset: 0x0002782E
		public static IList<Actor> GetAliveActorsOnTeam(WTeam team)
		{
			return ActorManager.AliveActorsOnTeam((int)team);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x00029636 File Offset: 0x00027836
		[Doc("Create a new AI Actor on the specified team.[..] Use ``Actor.SpawnAt(...)`` to spawn the actor in the world.")]
		public static Actor CreateAIActor(WTeam team)
		{
			return ActorManager.instance.CreateAIActor((int)team);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x00029643 File Offset: 0x00027843
		public static bool TeamHasAnySpawnPoint(WTeam team)
		{
			return ActorManager.TeamHasAnySpawnPoint((int)team);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x0002964B File Offset: 0x0002784B
		public static SpawnPoint RandomSpawnPointForTeam(WTeam team)
		{
			return ActorManager.RandomSpawnPointForTeam((int)team);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x00029653 File Offset: 0x00027853
		public static SpawnPoint RandomFrontlineSpawnPointForTeam(WTeam team)
		{
			return ActorManager.RandomFrontlineSpawnPointForTeam((int)team);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x0002965B File Offset: 0x0002785B
		public static SpawnPoint RandomEnemySpawnPoint(WTeam team)
		{
			return ActorManager.RandomEnemySpawnPoint((int)team);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x00029663 File Offset: 0x00027863
		public static IList<Squad> GetSquadsOnTeam(WTeam team)
		{
			return ActorManager.GetSquadsOnTeam((int)team);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0002966B File Offset: 0x0002786B
		[Doc("Assume control over the target Actor.[..] If the Player already controls a player actor, the player actor is first silently killed. The bot actor is also silently killed.")]
		public static void PlayerTakeOverBot(Actor actor)
		{
			ActorManager.PlayerTakeOverBot(actor);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x00029673 File Offset: 0x00027873
		[Doc("Sets the team skin of all actors on the specified team.[..] Team skins are game-managed, meaning that glow is automatically applied when the player enters night vision mode.")]
		public static void SetTeamSkin(WTeam team, ActorSkin skin)
		{
			ActorManager.SetGlobalTeamSkin((int)team, skin);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x0002967C File Offset: 0x0002787C
		[Doc("Returns the team skin of the specified team.")]
		public static ActorSkin GetTeamSkin(WTeam team)
		{
			return ActorManager.instance.actorSkin[(int)team];
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x0002968A File Offset: 0x0002788A
		[Doc("Triggers an explosion.[..] Returns true if anything (Actor/Vehicle) was damaged.")]
		public static bool Explode(ExplosionInfo info)
		{
			return ActorManager.Explode(info, false);
		}
	}
}

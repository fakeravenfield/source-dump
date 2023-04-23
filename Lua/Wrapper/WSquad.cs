using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000995 RID: 2453
	[Wrapper(typeof(Squad))]
	[Name("Squad")]
	public static class WSquad
	{
		// Token: 0x06003E5C RID: 15964 RVA: 0x0002A252 File Offset: 0x00028452
		[Getter]
		[Doc("The current order assigned to this squad.")]
		public static Order GetOrder(Squad self)
		{
			return self.order;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0002A25A File Offset: 0x0002845A
		[Getter]
		[Doc("Squadmates will always prioritize firing at their squad's attackTarget if they are able to.[..] Attack targets always override the current rules-of-engagement. If the attack target dies, this value will automatically be set to nil.")]
		public static Actor GetAttackTarget(Squad self)
		{
			return self.attackTarget;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0002A262 File Offset: 0x00028462
		[Setter]
		[Doc("Squadmates will always prioritize firing at their squad's attackTarget if they are able to.[..] Attack targets always override the current rules-of-engagement. If the attack target dies, this value will automatically be set to nil.")]
		public static void SetAttackTarget(Squad self, Actor attackTarget)
		{
			self.attackTarget = attackTarget;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x0002A26B File Offset: 0x0002846B
		[Doc("Assigns a new order to this squad.")]
		public static void AssignOrder(Squad self, Order order)
		{
			self.AssignOrder(order);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0002A274 File Offset: 0x00028474
		public static void SetFormationSize(Squad self, float formationWidth, float formationDepth)
		{
			self.SetFormationSize(formationWidth, formationDepth);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0002A27E File Offset: 0x0002847E
		public static void SetFormation(Squad self, Squad.FormationType formation)
		{
			self.SetFormation(formation);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0002A287 File Offset: 0x00028487
		[Doc("Set a random formation, excluding custom formation.")]
		public static void SetRandomFormation(Squad self)
		{
			self.SetRandomFormation();
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0002A28F File Offset: 0x0002848F
		[Doc("Set the formation to a specified custom formation.[..] The formation data contains the location of each squad member, which is rotated according to the squad movement direction and scaled by the ``formationWidth`` and ``formationHeight`` values. You can safely provide a greater number of locations than the current member count, which will be automatically used if more soldiers join the squad.")]
		public static void SetCustomFormation(Squad self, Vector2[] formation)
		{
			self.SetCustomFormation(formation);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0002A298 File Offset: 0x00028498
		[Getter]
		[Doc("The current members of this squad.")]
		public static Actor[] GetMembers(Squad self)
		{
			return self.members.ConvertAll<Actor>((ActorController c) => c.actor).ToArray();
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0002A2C9 File Offset: 0x000284C9
		[Getter]
		[Doc("The current leader of this squad.")]
		public static Actor GetLeader(Squad self)
		{
			return self.Leader().actor;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0002A2D6 File Offset: 0x000284D6
		[Getter]
		public static bool GetHasPlayerLeader(Squad self)
		{
			return self.HasPlayerLeader();
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0002A2DE File Offset: 0x000284DE
		[Getter]
		[Doc("Get the vehicle claimed by this squad.")]
		public static Vehicle GetSquadVehicle(Squad self)
		{
			return self.squadVehicle;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x0002A2E6 File Offset: 0x000284E6
		[Doc("Assign a new member to this squad.")]
		public static void AddMember(Squad self, Actor newMember)
		{
			if (newMember.controller.GetSquad() != self)
			{
				newMember.controller.ChangeToSquad(self);
			}
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x0002A302 File Offset: 0x00028502
		[Doc("Removes a number of actors from this squad.[..] The removed actors will form their own squad. Returns the new squad.")]
		public static Squad SplitSquad(Squad self, int count)
		{
			return self.KickMembersFromSquad(count);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0002A30B File Offset: 0x0002850B
		[Doc("Removes the specified actors from this squad.[..] The removed actors will form their own squad. Returns the new squad.")]
		public static Squad SplitSquad(Squad self, Actor[] membersToDrop)
		{
			return self.SplitSquad((from m in membersToDrop
			select m.controller).ToList<ActorController>());
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0002A33D File Offset: 0x0002853D
		[Doc("Removes a member from this squad.")]
		public static void RemoveMember(Squad self, Actor member)
		{
			if (member.controller.GetSquad() != self)
			{
				self.DropMember(member.controller);
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0012F30C File Offset: 0x0012D50C
		[Doc("Creates a new squad with the specified actors.[..] NOTE: The Player actor will be ignored, you can populate the player squad by adding members to ``Player.squad``")]
		public static Squad Create(List<Actor> actors)
		{
			try
			{
				actors.Remove(FpsActorController.instance.actor);
			}
			catch (Exception)
			{
			}
			foreach (Actor actor in actors)
			{
				AiActorController aiActorController = actor.controller as AiActorController;
				if (aiActorController.squad != null)
				{
					aiActorController.squad.DropMember(aiActorController);
				}
			}
			return new Squad(actors, null, null, null, 0f);
		}
	}
}

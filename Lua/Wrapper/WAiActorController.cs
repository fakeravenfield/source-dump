using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000960 RID: 2400
	[Wrapper(typeof(AiActorController))]
	public static class WAiActorController
	{
		// Token: 0x06003D3C RID: 15676 RVA: 0x000296DC File Offset: 0x000278DC
		[Getter]
		public static float GetMeleeChargeRange(AiActorController self)
		{
			return self.modifier.meleeChargeRange;
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000296E9 File Offset: 0x000278E9
		[Setter]
		[Doc("The maximum range in meters at which the AI will attempt to charge an enemy.[..] Default is 30.")]
		public static void SetMeleeChargeRange(AiActorController self, float meleeChargeRange)
		{
			self.modifier.meleeChargeRange = meleeChargeRange;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000296F7 File Offset: 0x000278F7
		[Getter]
		public static bool GetCanSprint(AiActorController self)
		{
			return self.modifier.canSprint;
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x00029704 File Offset: 0x00027904
		[Setter]
		public static void SetCanSprint(AiActorController self, bool canSprint)
		{
			self.modifier.canSprint = canSprint;
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x00029712 File Offset: 0x00027912
		[Getter]
		public static bool GetCanJoinPlayerSquad(AiActorController self)
		{
			return self.modifier.canJoinPlayerSquad;
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x0002971F File Offset: 0x0002791F
		[Setter]
		[Doc("While false, the player cannot issue a join squad order on this AI bot.")]
		public static void SetCanJoinPlayerSquad(AiActorController self, bool canJoinPlayerSquad)
		{
			self.modifier.canJoinPlayerSquad = canJoinPlayerSquad;
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x0002972D File Offset: 0x0002792D
		[Getter]
		public static bool GetIgnoreFovCheck(AiActorController self)
		{
			return self.modifier.ignoreFovCheck;
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x0002973A File Offset: 0x0002793A
		[Setter]
		[Doc("Ignore FOV Check when querying sight.[..] If set to true, this AI is able to see enemies all around, even behind.")]
		public static void SetIgnoreFovCheck(AiActorController self, bool ignoreFovCheck)
		{
			self.modifier.ignoreFovCheck = ignoreFovCheck;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x00029748 File Offset: 0x00027948
		[Getter]
		public static bool getAlwaysChargeTarget(AiActorController self)
		{
			return self.modifier.alwaysChargeTarget;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x00029755 File Offset: 0x00027955
		[Setter]
		[Doc("Charge towards target at all times.[..] If set to true, this AI will always run towards an enemy target, even when it is out of melee range.")]
		public static void SetAlwaysChargeTarget(AiActorController self, bool alwaysChargeTarget)
		{
			self.modifier.alwaysChargeTarget = alwaysChargeTarget;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x00029763 File Offset: 0x00027963
		[Doc("Disables the built in AI movement, allowing full movement control from scripts.")]
		public static void OverrideDefaultMovement(AiActorController self)
		{
			self.OverrideDefaultMovement();
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0002976B File Offset: 0x0002796B
		[Doc("Re-enables the built in AI movement.")]
		public static void ReleaseDefaultMovementOverride(AiActorController self)
		{
			self.ReleaseDefaultMovementOverride();
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x00029773 File Offset: 0x00027973
		[Getter]
		[Doc("True if the built in AI movement is disabled.")]
		public static bool getIsDefaultMovementOverridden(AiActorController self)
		{
			return self.isDefaultMovementOverridden;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0002977B File Offset: 0x0002797B
		[Getter]
		[Doc("Returns true if the AI is traversing a path.")]
		public static bool GetHasPath(AiActorController self)
		{
			return self.HasPath();
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x00029783 File Offset: 0x00027983
		[Doc("Pathfinds to the closest destination point on the current navmesh.")]
		public static void Goto(AiActorController self, Vector3 destination)
		{
			self.Goto(destination, true);
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0002978D File Offset: 0x0002798D
		[Doc("Go straight to the destination point, ignoring pathfinding.")]
		public static void GotoDirect(AiActorController self, Vector3 destination)
		{
			self.GotoDirect(destination, true);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x00029797 File Offset: 0x00027997
		[Doc("Stops the current AI path movement.")]
		public static void CancelPath(AiActorController self)
		{
			self.CancelPath(true);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000297A0 File Offset: 0x000279A0
		[Doc("Orders the AI to enter the specified vehicle. Also sets the AI's target vehicle to the specified vehicle.")]
		public static void GotoAndEnterVehicle(AiActorController self, Vehicle vehicle)
		{
			self.GotoAndEnterVehicle(vehicle, true);
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000297AA File Offset: 0x000279AA
		[Doc("Leaves the target vehicle.")]
		public static void LeaveVehicle(AiActorController self)
		{
			self.LeaveVehicle(false);
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000297B3 File Offset: 0x000279B3
		[Getter]
		[Doc("Returns true if the AI has a target vehicle, and is currently attempting to enter it.")]
		public static bool GetIsEnteringVehicle(AiActorController self)
		{
			return self.IsEnteringVehicle();
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000297BB File Offset: 0x000279BB
		[Getter]
		[Doc("Returns true if the AI has a target vehicle to enter, or that they are already seated in.")]
		public static bool GetHasTargetVehicle(AiActorController self)
		{
			return self.HasTargetVehicle();
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000297C3 File Offset: 0x000279C3
		[Getter]
		[Doc("Returns the target vehicle the AI is entering or is already seated in.")]
		public static Vehicle GetTargetVehicle(AiActorController self)
		{
			return self.targetVehicle;
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x00004B52 File Offset: 0x00002D52
		[Getter]
		[Doc("The destination point of the last Goto() order.")]
		public static Vector3 getLastGotoPoint(AiActorController self)
		{
			return self.lastGotoPoint;
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000297CB File Offset: 0x000279CB
		[Getter]
		[Doc("The last reached path waypoint.")]
		public static Vector3 getLastWaypoint(AiActorController self)
		{
			return self.lastWaypoint;
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000297D3 File Offset: 0x000279D3
		[Getter]
		[Doc("The current waypoint.")]
		public static Vector3 getCurrentWaypoint(AiActorController self)
		{
			return self.currentWaypoint;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000297DB File Offset: 0x000279DB
		[Doc("Returns true if the targetActor is inside this actor's field of view.")]
		public static bool IsInFOV(AiActorController self, Actor targetActor)
		{
			return self.CanSeeActorFOV(targetActor);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000297E4 File Offset: 0x000279E4
		[Doc("Returns true if the position is inside this actor's field of view.")]
		public static bool IsInFOV(AiActorController self, Vector3 position)
		{
			return self.CanSeePointFOV(position);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000297ED File Offset: 0x000279ED
		[Getter]
		public static float getTargetFlightAltitude(AiActorController self)
		{
			return self.targetFlightHeight;
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000297F5 File Offset: 0x000279F5
		[Setter]
		[Doc("The default altitude the AI will try to maintain in an Aircraft.[..] This value is randomized every time a bot enters or exits an aircraft.")]
		public static void setTargetFlightAltitude(AiActorController self, float altitude)
		{
			self.targetFlightHeight = altitude;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000048EA File Offset: 0x00002AEA
		[Getter]
		[Doc("The target this AI is currently attacking")]
		public static Actor GetCurrentAttackTarget(AiActorController self)
		{
			return self.target;
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000297FE File Offset: 0x000279FE
		[Getter]
		public static AiActorController.SkillLevel GetSkillLevel(AiActorController self)
		{
			return self.skill;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00029806 File Offset: 0x00027A06
		[Setter]
		public static void SetSkillLevel(AiActorController self, AiActorController.SkillLevel skillLevel)
		{
			self.skill = skillLevel;
		}
	}
}

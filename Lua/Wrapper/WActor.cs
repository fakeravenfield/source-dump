using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200095B RID: 2395
	[Wrapper(typeof(Actor))]
	public static class WActor
	{
		// Token: 0x06003CA7 RID: 15527 RVA: 0x00029165 File Offset: 0x00027365
		[Getter]
		public static float GetSpeedMultiplier(Actor self)
		{
			return self.speedMultiplier;
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x0002916D File Offset: 0x0002736D
		[Setter]
		[Doc("Controls the maximum movement speed of the actor.[..] Default is 1.")]
		public static void SetSpeedMultiplier(Actor self, float speedMultiplier)
		{
			self.speedMultiplier = speedMultiplier;
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00029176 File Offset: 0x00027376
		[Getter]
		public static bool IsPlayer(Actor self)
		{
			return self == ActorManager.instance.player;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x00029188 File Offset: 0x00027388
		[Getter]
		public static bool IsBot(Actor self)
		{
			return self.aiControlled;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00029190 File Offset: 0x00027390
		[Getter]
		[Doc("True while actor is aiming with weapon.[..]")]
		public static bool IsAiming(Actor self)
		{
			return self.IsAiming();
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x00029198 File Offset: 0x00027398
		[Getter]
		[Doc("True while actor is sprinting.[..]")]
		public static bool IsSprinting(Actor self)
		{
			return self.controller.IsSprinting();
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000291A5 File Offset: 0x000273A5
		[Getter]
		public static bool IsSwimming(Actor self)
		{
			return self.IsSwimming();
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000291AD File Offset: 0x000273AD
		[Getter]
		public static bool IsInWater(Actor self)
		{
			return self.immersedInWater;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000291B5 File Offset: 0x000273B5
		[Getter]
		public static bool WasRecentlyInWater(Actor self)
		{
			return self.WasRecentlyInWater();
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00008DC9 File Offset: 0x00006FC9
		[Getter]
		public static bool IsDead(Actor self)
		{
			return self.dead;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000291BD File Offset: 0x000273BD
		[Getter]
		public static bool IsFallenOver(Actor self)
		{
			return self.fallenOver;
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000291C5 File Offset: 0x000273C5
		[Getter]
		public static bool IsStanding(Actor self)
		{
			return self.stance == Actor.Stance.Stand;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000291D0 File Offset: 0x000273D0
		[Getter]
		public static bool IsCrouching(Actor self)
		{
			return self.stance == Actor.Stance.Crouch;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000291DB File Offset: 0x000273DB
		[Getter]
		public static bool IsProne(Actor self)
		{
			return self.stance == Actor.Stance.Prone;
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000291E6 File Offset: 0x000273E6
		[Getter]
		[Doc("Returns true if the actor can capture points.[..] This value may be false if the actor is in a vehicle that cannot capture points such as helicopters, etc")]
		public static bool CanCapturePoint(Actor self)
		{
			return self.CanCapturePoint();
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000291EE File Offset: 0x000273EE
		[Getter]
		public static float GetMaxHealth(Actor self)
		{
			return self.maxHealth;
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000291F6 File Offset: 0x000273F6
		[Setter]
		[Doc("The maximum allowed balance value.[..] Healing this actor will make its health cap out at this value. This is the health the actor will have when spawning.")]
		public static void SetMaxHealth(Actor self, float maxHealth)
		{
			self.maxHealth = maxHealth;
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000291FF File Offset: 0x000273FF
		[Getter]
		public static float GetMaxBalance(Actor self)
		{
			return self.maxBalance;
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x00029207 File Offset: 0x00027407
		[Setter]
		[Doc("The maximum allowed balance value.[..] The current balance value will gradually increase until it reaches this value. This is the balance value the actor will have when spawning.")]
		public static void SetMaxBalance(Actor self, float maxBalance)
		{
			self.maxBalance = maxBalance;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x00029210 File Offset: 0x00027410
		[Getter]
		public static float GetHealth(Actor self)
		{
			return self.health;
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00029218 File Offset: 0x00027418
		[Setter]
		public static void SetHealth(Actor self, float health)
		{
			self.health = health;
			if (!self.aiControlled)
			{
				self.UpdateHealthUi();
			}
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x0002922F File Offset: 0x0002742F
		[Getter]
		public static float GetBalance(Actor self)
		{
			return self.balance;
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x00029237 File Offset: 0x00027437
		[Setter]
		public static void SetBalance(Actor self, float balance)
		{
			self.balance = balance;
			self.OnBalanceSetManually();
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x00029246 File Offset: 0x00027446
		[Getter]
		public static WTeam GetTeam(Actor self)
		{
			return (WTeam)self.team;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x0002924E File Offset: 0x0002744E
		[Getter]
		public static bool NeedsResupply(Actor self)
		{
			return self.needsResupply;
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x00029256 File Offset: 0x00027456
		public static bool ResupplyAmmo(Actor self)
		{
			return self.ResupplyAmmo();
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x0002925E File Offset: 0x0002745E
		public static bool ResupplyHealth(Actor self)
		{
			return self.ResupplyHealth();
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x00029266 File Offset: 0x00027466
		[Getter]
		[Doc("Get the squad this actor is a member of.")]
		public static Squad GetSquad(Actor self)
		{
			return self.controller.GetSquad();
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x00029273 File Offset: 0x00027473
		[Getter]
		[Doc("The index of this actor in the ActorManager.actors array")]
		public static int GetActorIndex(Actor self)
		{
			return self.actorIndex + 1;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x0002927D File Offset: 0x0002747D
		[Getter]
		[Doc("The index of this actor in the ActorManager.GetActorsOnTeam() array")]
		public static int GetActorTeamIndex(Actor self)
		{
			return self.teamActorIndex + 1;
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x0012E7F0 File Offset: 0x0012C9F0
		public static bool Damage(Actor self, float health)
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Scripted, null, null)
			{
				healthDamage = health,
				point = self.transform.position
			};
			return self.Damage(info);
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x0012E82C File Offset: 0x0012CA2C
		public static bool Damage(Actor self, Actor source, float health, float balance, bool isSplash, bool isPiercing)
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Scripted, source, null)
			{
				healthDamage = health,
				balanceDamage = balance,
				isSplashDamage = isSplash,
				isPiercing = isPiercing,
				point = self.transform.position
			};
			return self.Damage(info);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x0012E884 File Offset: 0x0012CA84
		public static bool Damage(Actor self, Actor source, float health, float balance, bool isSplash, bool isPiercing, Vector3 point, Vector3 direction, Vector3 force)
		{
			DamageInfo info = new DamageInfo(DamageInfo.DamageSourceType.Scripted, source, null)
			{
				healthDamage = health,
				balanceDamage = balance,
				isSplashDamage = isSplash,
				isPiercing = isPiercing,
				point = point,
				direction = direction,
				impactForce = force
			};
			return self.Damage(info);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00029287 File Offset: 0x00027487
		public static bool Damage(Actor self, Actor source, DamageInfo info)
		{
			info.sourceActor = source;
			return self.Damage(info);
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00029298 File Offset: 0x00027498
		[Doc("Kills the actor without reporting the kill.[..] An actor killed this way will not contribute to the scoreboard or killfeed, and will not spawn a ragdoll.")]
		public static void KillSilently(Actor self)
		{
			self.KillSilently();
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000292A0 File Offset: 0x000274A0
		[Doc("Kills the actor.")]
		public static void Kill(Actor self, Actor killer)
		{
			self.Kill(new DamageInfo(DamageInfo.DamageSourceType.Scripted, killer, null));
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000292B1 File Offset: 0x000274B1
		[Getter]
		[Doc("The root position of the actor.[..] The root position is usually at the ground by the feet.")]
		public static Vector3 GetPosition(Actor self)
		{
			return self.Position();
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000292B9 File Offset: 0x000274B9
		[Getter]
		public static Quaternion GetRotation(Actor self)
		{
			return self.controller.transform.rotation;
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x0012E8E4 File Offset: 0x0012CAE4
		[Doc("Set the position and rotation of this actor.[..] Only rotation on the Y axis is applied to the actor, any rotation on the X and Z axis is ignored.")]
		public static void TeleportTo(Actor self, Vector3 position, Quaternion rotation)
		{
			Vector3 eulerAngles = rotation.eulerAngles;
			eulerAngles.x = 0f;
			eulerAngles.z = 0f;
			self.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles));
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000292CB File Offset: 0x000274CB
		[Getter]
		[Doc("The center-of-mass position of the actor.[..]")]
		public static Vector3 GetCenterPosition(Actor self)
		{
			return self.CenterPosition();
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000292D3 File Offset: 0x000274D3
		[Getter]
		[Doc("The velocity of the actor.[..] If seated, returns the vehicle velocity. If ragdolled, returns the ragdoll velocity. Otherwise returns movement velocity. Setting the velocity is only supported on the player actor. Setting the velocity of an AI actor will has no effect.")]
		public static Vector3 GetVelocity(Actor self)
		{
			return self.Velocity();
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x0012E920 File Offset: 0x0012CB20
		[Doc("Set the bone local scale of the specified HumanoidBodyBone.[..]")]
		public static void SetHumanoidBoneScale(Actor self, HumanBodyBones bone, Vector3 scale)
		{
			try
			{
				Transform transform;
				Transform transform2;
				self.ragdoll.HumanBoneTransform(bone, out transform, out transform2);
				transform.localScale = scale;
				transform2.localScale = scale;
			}
			catch
			{
				ModManager.HandleModException(new Exception(string.Format("Actor does not have bone {0}", bone)));
			}
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000292DB File Offset: 0x000274DB
		[Doc("Set the bone local scale of the specified HumanoidBodyBone.[..]")]
		public static void SetHumanoidBoneScale(Actor self, HumanBodyBones bone, float scale)
		{
			WActor.SetHumanoidBoneScale(self, bone, new Vector3(scale, scale, scale));
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000292EC File Offset: 0x000274EC
		[Doc("Set the local scale of the WeaponParent transform, which controls scale of all held weapons.[..]")]
		public static void SetWeaponParentScale(Actor self, float scale)
		{
			self.controller.TpWeaponParent().localScale = new Vector3(scale, scale, scale);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x0012E97C File Offset: 0x0012CB7C
		[Doc("Sets the world scale of the WeaponParent transform, which controls scale of all held weapons.[..] If all parent bones and game objects are uniformly scaled on all axis, this will yield the correct world scale. If not, the world scale might be off.")]
		public static void SetWeaponParentApproximateWorldScale(Actor self, float scale)
		{
			Transform transform = self.controller.TpWeaponParent();
			float num = 1f;
			while (transform != null)
			{
				num *= transform.localScale.x;
				transform = transform.parent;
			}
			float scale2 = scale / num;
			WActor.SetWeaponParentScale(self, scale2);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x00029306 File Offset: 0x00027506
		[Doc("Get the animated bone transform of the specified HumanoidBodyBone.[..]")]
		public static Transform GetHumanoidTransformAnimated(Actor self, HumanBodyBones bone)
		{
			return self.ragdoll.HumanBoneTransformAnimated(bone);
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x00029314 File Offset: 0x00027514
		[Doc("Get the ragdoll bone transform of the specified HumanoidBodyBone.[..]")]
		public static Transform GetHumanoidTransformRagdoll(Actor self, HumanBodyBones bone)
		{
			return self.ragdoll.HumanBoneTransformRagdoll(bone);
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00029322 File Offset: 0x00027522
		[Doc("Ragdolls the actor.[..]")]
		public static void FallOver(Actor self)
		{
			self.FallOver();
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x0002932A File Offset: 0x0002752A
		[Doc("Ragdolls the actor and applies a force.[..]")]
		public static void KnockOver(Actor self, Vector3 force)
		{
			self.KnockOver(force);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x00029333 File Offset: 0x00027533
		[Doc("Set the ragdoll joint drive values.[..]")]
		public static void SetRagdollJointDrive(Actor self, [Doc("The spring force value (default 1000). A higher value yields a stronger force.")] float spring, [Doc("The drag force value (default 3). A higher value yields a slower and more stable movement.")] float stiffness)
		{
			self.ragdoll.SetDrive(spring, stiffness);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00029342 File Offset: 0x00027542
		[Doc("Reset the ragdoll joint drive to their default values.[..]")]
		public static void SetRagdollJointDriveDefault(Actor self)
		{
			self.ragdoll.SetDriveDefault();
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x0002934F File Offset: 0x0002754F
		[Doc("Sets the skin of this actor to an unmanaged skin.[..] Unmanaged skins are not managed by the game, meaning that glow will not be automatically applied when the player enters night vision mode.")]
		public static void SetSkin(Actor self, ActorSkin actorSkin)
		{
			self.SetModelSkin(actorSkin);
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x0012E9C8 File Offset: 0x0012CBC8
		[Doc("Sets the skin of this actor to an unmanaged skin.[..] Unmanaged skins are not managed by the game, meaning that glow will not be automatically applied when the player enters night vision mode.")]
		public static void SetSkin(Actor self, [Doc("The mesh that should be applied to the actor. If nil, the mesh will not be replaced.")] Mesh mesh, [Doc("The material array that should be applied to the renderer.")] Material[] materials, [Doc("The material index that should be replaced by the team material. Set this value to -1 if no team material should be applied. Please note that this uses C# array indexing, meaning the first material entry is at index 0, the second and index 1, etc")] int teamMaterialIndex)
		{
			ActorSkin modelSkin = new ActorSkin
			{
				characterSkin = new ActorSkin.MeshSkin(mesh, materials, teamMaterialIndex)
			};
			self.SetModelSkin(modelSkin);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00029358 File Offset: 0x00027558
		[Doc("Resets the skin of the actor to the game-managed team skin.")]
		public static void ApplyTeamSkin(Actor self)
		{
			ActorManager.ApplyGlobalTeamSkin(self);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x00029360 File Offset: 0x00027560
		[Doc("Adds an additional accessory renderer on top of the actors regular skin.[..]")]
		public static void AddAccessory(Actor self, Mesh mesh, Material[] materials)
		{
			self.AddAccessory(new ActorAccessory(mesh, materials));
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x0002936F File Offset: 0x0002756F
		[Doc("Removes all accessory renderers.[..]")]
		public static void RemoveAccessories(Actor self)
		{
			self.RemoveAccessories();
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x00029377 File Offset: 0x00027577
		[Getter]
		public static string GetName(Actor self)
		{
			return self.name;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0002937F File Offset: 0x0002757F
		[Setter]
		public static void SetName(Actor self, string name)
		{
			self.name = name;
			self.scoreboardEntry.UpdateNameLabel();
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x00029393 File Offset: 0x00027593
		[Doc("Equip a new weapon in the specified slot (0-4).")]
		public static Weapon EquipNewWeaponEntry(Actor self, WeaponManager.WeaponEntry entry, int slotNumber, bool forceSwitchTo)
		{
			return self.EquipNewWeaponEntry(entry, slotNumber, forceSwitchTo);
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x0002939E File Offset: 0x0002759E
		[Doc("Remove the weapon in the specified slot (0-4).")]
		public static void RemoveWeapon(Actor self, int slotNumber)
		{
			self.DropWeapon(slotNumber);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000293A7 File Offset: 0x000275A7
		[Getter]
		[Doc("The direction this actor is aiming.[..]")]
		public static Vector3 GetFacingDirection(Actor self)
		{
			return self.controller.FacingDirection();
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000293B4 File Offset: 0x000275B4
		[Getter]
		[Doc("The weapon this actor has equipped.[..]")]
		public static Weapon GetActiveWeapon(Actor self)
		{
			return self.activeWeapon;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000293BC File Offset: 0x000275BC
		[Getter]
		public static IList<Weapon> GetWeaponSlots(Actor self)
		{
			return self.weapons;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000293C4 File Offset: 0x000275C4
		public static bool IsWeaponUnholstered(Actor self)
		{
			return self.HasUnholsteredWeapon();
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000293CC File Offset: 0x000275CC
		[Doc("Returns true if Actor takes damage when hit by the given weapon.[..]")]
		public static bool CanBeDamagedBy(Actor self, [Doc("Can this weapon damage the Actor?")] Weapon weapon)
		{
			return self.CanBeDamagedBy(weapon);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000293D5 File Offset: 0x000275D5
		[Doc("Reloads all carried weapons instantly.[..]")]
		public static void InstantlyReloadCarriedWeapons(Actor self)
		{
			self.InstantlyReloadCarriedWeapons();
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000293DD File Offset: 0x000275DD
		[Getter]
		public static bool IsDriver(Actor self)
		{
			return self.IsDriver();
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000293E5 File Offset: 0x000275E5
		[Getter]
		public static bool IsPassenger(Actor self)
		{
			return self.IsPassenger();
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000293ED File Offset: 0x000275ED
		[Getter]
		public static bool IsParachuteDeployed(Actor self)
		{
			return self.parachuteDeployed;
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000293F5 File Offset: 0x000275F5
		[Getter]
		public static bool CanDeployParachute(Actor self)
		{
			return self.canDeployParachute;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000293FD File Offset: 0x000275FD
		[Setter]
		[Doc("When true, the actor can manually deploy their parachute.")]
		public static void CanDeployParachute(Actor self, bool canDeploy)
		{
			self.canDeployParachute = canDeploy;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00029406 File Offset: 0x00027606
		[Doc("Deploys the parachute.[..] This function ignores the canDeployParachute value.")]
		public static void DeployParachute(Actor self)
		{
			self.DeployParachute();
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x0002940E File Offset: 0x0002760E
		[Doc("Cuts the parachute.[..] This function ignores the canDeployParachute value.")]
		public static void CutParachute(Actor self)
		{
			self.CutParachutes();
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x00029416 File Offset: 0x00027616
		[Getter]
		public static bool IsSeated(Actor self)
		{
			return self.IsSeated();
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x0002941E File Offset: 0x0002761E
		[Getter]
		public static Seat GetActiveSeat(Actor self)
		{
			return self.seat;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x00029426 File Offset: 0x00027626
		[Getter]
		public static Vehicle GetActiveVehicle(Actor self)
		{
			if (self.seat)
			{
				return self.seat.vehicle;
			}
			return null;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x00029442 File Offset: 0x00027642
		[Getter]
		public static bool CanBeSeated(Actor self)
		{
			return self.CanEnterSeat();
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x0002944A File Offset: 0x0002764A
		public static bool CanEnterSeat(Actor self, Seat seat)
		{
			return seat && seat.gameObject.activeInHierarchy && self.CanEnterSeat() && !seat.vehicle.dead && !seat.IsOccupied();
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x0012E9F0 File Offset: 0x0012CBF0
		[Doc("Switches to the target seat.[..] If the target seat is already occupied or is not in the same vehicle, the switch is canceled and this function returns false.")]
		public static bool SwitchToSeat(Actor self, Seat seat)
		{
			for (int i = 0; i < seat.vehicle.seats.Count; i++)
			{
				if (seat.vehicle.seats[i] == seat)
				{
					return self.SwitchSeat(i, false);
				}
			}
			return false;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x0012EA3C File Offset: 0x0012CC3C
		[Doc("Swaps with the target seat.[..] If the target seat is already occupied, the two occupants will be swapped. If the target seat is not in the same vehicle, the swap is canceled and this function returns false.")]
		public static bool SwapWithSeat(Actor self, Seat seat)
		{
			for (int i = 0; i < seat.vehicle.seats.Count; i++)
			{
				if (seat.vehicle.seats[i] == seat)
				{
					return self.SwitchSeat(i, true);
				}
			}
			return false;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x00029485 File Offset: 0x00027685
		[Doc("Enters the target seat if it's not already occupied.[..] If the target seat is already occupied, the actor will not enter the seat, and this function returns false. If the actor could successfully enter the seat, the function returns true.")]
		public static bool EnterSeat(Actor self, Seat seat)
		{
			return self.EnterSeat(seat, false);
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x0002948F File Offset: 0x0002768F
		[Doc("Force enters the target seat, kicking out any previous occupant.[..] If another seat is available in the vehicle, the previous occupant will swap to that seat. If not, they will exit the vehicle.")]
		public static void EnterSeatForced(Actor self, Seat seat)
		{
			self.EnterSeat(seat, true);
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0012EA88 File Offset: 0x0012CC88
		[Doc("Makes the actor enter the vehicle.[..] If the actor is a squad leader, this automatically makes the squad claim the vehicle.")]
		public static bool EnterVehicle(Actor self, Vehicle vehicle)
		{
			if (self.aiControlled)
			{
				AiActorController aiActorController = self.controller as AiActorController;
				if (aiActorController.isSquadLeader)
				{
					aiActorController.squad.ClaimVehicle(vehicle);
				}
			}
			return self.EnterVehicle(vehicle);
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x0012EAC4 File Offset: 0x0012CCC4
		public static void ExitVehicle(Actor self)
		{
			self.LeaveSeat(false);
			AiActorController aiActorController = self.controller as AiActorController;
			if (aiActorController != null)
			{
				aiActorController.LeaveVehicle(false);
			}
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x0002949A File Offset: 0x0002769A
		[Getter]
		[Doc("True while Actor is climbing a Ladder.[..]")]
		public static bool IsOnLadder(Actor self)
		{
			return self.IsOnLadder();
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000294A2 File Offset: 0x000276A2
		[Getter]
		[Doc("The ladder this actor is currently climbing")]
		public static Ladder GetLadder(Actor self)
		{
			return self.ladder;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000294AA File Offset: 0x000276AA
		[Doc("Makes the actor start climbing the ladder.[..] The actor is positioned at the closest point on the ladder.")]
		public static void GetOnLadder(Actor self, Ladder ladder)
		{
			self.GetOnLadder(ladder);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000294B3 File Offset: 0x000276B3
		[Doc("Makes the actor get off the ladder.[..] If the actor was close to the top or bottom, the actor will be positioned according on the ladder's top or bottom exit points.")]
		public static void ExitLadder(Actor self)
		{
			self.ExitLadder();
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000294BB File Offset: 0x000276BB
		[Getter]
		public static bool GetIsRendered(Actor self)
		{
			return self.isRendered;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000294C3 File Offset: 0x000276C3
		[Setter]
		[Doc("Controls the rendering of an actor.[..] Please note that this is purely cosmetic, bots can still see and target an actor with disabled renders.")]
		public static void SetIsRendered(Actor self, bool isRendered)
		{
			if (isRendered)
			{
				self.Show();
				return;
			}
			self.Hide();
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000294D5 File Offset: 0x000276D5
		[Getter]
		public static bool GetIsFrozen(Actor self)
		{
			return self.isFrozen;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000294DD File Offset: 0x000276DD
		[Setter]
		[Doc("Controls the frozen state of an Actor.[..] A frozen Actor cannot move or complete action such as firing, etc.")]
		public static void SetIsFrozen(Actor self, bool isFrozen)
		{
			if (isFrozen)
			{
				self.Freeze();
				return;
			}
			self.Unfreeze();
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000294EF File Offset: 0x000276EF
		[Getter]
		public static bool GetHitboxCollidersAreEnabled(Actor self)
		{
			return self.HitboxCollidersAreEnabled();
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000294F7 File Offset: 0x000276F7
		[Setter]
		[Doc("Controls the enabled flag on this Actor's hitbox colliders.[..] Disabling them means this actor can not be hit by projectiles or collide with vehicles")]
		public static void SetHitboxCollidersAreEnabled(Actor self, bool enabled)
		{
			if (enabled)
			{
				self.EnableHitboxColliders();
				return;
			}
			self.DisableHitboxColliders();
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x0002950A File Offset: 0x0002770A
		[Doc("Used to temporarily disable an actor from the game.[..] Hides, Freezes and Disables Hitbox Colliders of this Actor. Also stops the Actor from respawning.")]
		public static void Deactivate(Actor self)
		{
			self.Deactivate();
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x00029512 File Offset: 0x00027712
		[Doc("Undo ``Deactivate()``")]
		public static void Activate(Actor self)
		{
			self.Activate();
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0002951A File Offset: 0x0002771A
		[Getter]
		[Doc("A deactivated actor is temporarily removed from the game and can not interact with anything or respawn.[..]Control this state with ``Activate()`` and ``Deactivate()```")]
		public static bool GetIsDeactivated(Actor self)
		{
			return self.isDeactivated;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x00029522 File Offset: 0x00027722
		[Getter]
		public static bool GetDropsAmmoOnKick(Actor self)
		{
			return self.dropsAmmoOnKick;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x0002952A File Offset: 0x0002772A
		[Setter]
		public static void SetDropsAmmoOnKick(Actor self, bool dropsAmmoOnKick)
		{
			self.dropsAmmoOnKick = dropsAmmoOnKick;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x00029533 File Offset: 0x00027733
		[Getter]
		[Doc("Gets the ``AiActorController`` of this actor.[..] Returns nil if called on the player actor.")]
		public static AiActorController GetAiController(Actor self)
		{
			return self.controller as AiActorController;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00029540 File Offset: 0x00027740
		public static void SpawnAt(Actor self, Vector3 position, Quaternion rotation)
		{
			self.SpawnAt(position, rotation, null);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x0002954B File Offset: 0x0002774B
		public static void SpawnAt(Actor self, Vector3 position)
		{
			self.SpawnAt(position, Quaternion.identity, null);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x0002955A File Offset: 0x0002775A
		[Getter]
		public static bool GetIsReadyToSpawn(Actor self)
		{
			return self.IsReadyToSpawn();
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x00029562 File Offset: 0x00027762
		[Getter]
		[CallbackSignature(new string[]
		{
			"actor",
			"source",
			"info"
		})]
		[Consumable("Consuming this event stops the actor from taking damage.")]
		[Doc("Invoked when this actor takes damage.")]
		public static ScriptEvent GetOnTakeDamage(Actor self)
		{
			return self.onTakeDamage;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x0002956A File Offset: 0x0002776A
		[Getter]
		[Doc("Returns true when the actor can resupply ammo.[..]")]
		public static bool GetIsResupplyingAmmo(Actor self)
		{
			return self.GetIsResupplyingAmmo();
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x00029572 File Offset: 0x00027772
		[Getter]
		[Doc("Returns true when the actor can resupply health.[..]")]
		public static bool GetIsResupplyingHealth(Actor self)
		{
			return self.GetIsResupplyingHealth();
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x0002957A File Offset: 0x0002777A
		[Getter]
		[Doc("Returns true when the actor is at a resupply crate.[..]")]
		public static bool GetIsAtResupplyCrate(Actor self)
		{
			return self.GetIsAtResupplyCrate();
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x00029582 File Offset: 0x00027782
		[Getter]
		[Doc("Returns the capture point this actor is currently inside the capture range of.[..] Returns nil if no capture point is in range.")]
		public static CapturePoint GetCurrentCapturePoint(Actor self)
		{
			return self.GetCurrentCapturePoint();
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x0002958A File Offset: 0x0002778A
		public static Weapon.Difficulty EvaluateShotDifficulty(Actor self, Actor target, Weapon weapon)
		{
			return weapon.EvaluateDifficulty(ActorManager.ActorsDistance(self, target), target.GetTargetType());
		}
	}
}

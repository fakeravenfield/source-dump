using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x020009A2 RID: 2466
	[Wrapper(typeof(Weapon))]
	public static class WWeapon
	{
		// Token: 0x06003EB4 RID: 16052 RVA: 0x0002A5A2 File Offset: 0x000287A2
		[Getter]
		public static Actor GetUser(Weapon self)
		{
			return self.user;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0002A5AA File Offset: 0x000287AA
		[Getter]
		public static Actor GetKillCredit(Weapon self)
		{
			return self.killCredit;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0002A5B2 File Offset: 0x000287B2
		[Setter]
		[Doc("The actor that gets damage/kill credits from this weapon.[..] Automatically set to whoever equips this weapon, but can be overridden if required")]
		public static void SetKillCredit(Weapon self, Actor killCredit)
		{
			self.killCredit = killCredit;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0002A5BB File Offset: 0x000287BB
		[Getter]
		public static WeaponManager.WeaponEntry GetWeaponEntry(Weapon self)
		{
			return self.weaponEntry;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0002A5C3 File Offset: 0x000287C3
		[Getter]
		public static int GetMaxAmmo(Weapon self)
		{
			return self.configuration.ammo;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0002A5D0 File Offset: 0x000287D0
		[Setter]
		public static void SetMaxAmmo(Weapon self, int maxAmmo)
		{
			self.configuration.ammo = maxAmmo;
			self.OnAmmoChanged();
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0002A5E4 File Offset: 0x000287E4
		[Getter]
		public static int GetAmmo(Weapon self)
		{
			return self.ammo;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0002A5EC File Offset: 0x000287EC
		[Setter]
		public static void SetAmmo(Weapon self, int ammo)
		{
			self.ammo = ammo;
			self.OnAmmoChanged();
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0002A5FB File Offset: 0x000287FB
		[Getter]
		public static int GetSpareAmmo(Weapon self)
		{
			return self.spareAmmo;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x0002A603 File Offset: 0x00028803
		[Setter]
		public static void SetSpareAmmo(Weapon self, int ammo)
		{
			self.spareAmmo = ammo;
			self.OnAmmoChanged();
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x0002A612 File Offset: 0x00028812
		[Getter]
		public static int GetMaxSpareAmmo(Weapon self)
		{
			return self.configuration.spareAmmo;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x0002A61F File Offset: 0x0002881F
		[Setter]
		public static void SetMaxSpareAmmo(Weapon self, int ammo)
		{
			self.configuration.spareAmmo = ammo;
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0002A62D File Offset: 0x0002882D
		[Getter]
		public static bool IsReloading(Weapon self)
		{
			return self.reloading;
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0002A635 File Offset: 0x00028835
		public static void InstantlyReload(Weapon self)
		{
			self.InstantlyReload();
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00007345 File Offset: 0x00005545
		public static void Reload(Weapon self)
		{
			self.Reload(false);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0002A63D File Offset: 0x0002883D
		[Getter]
		public static bool GetHasAdvancedReload(Weapon self)
		{
			return self.configuration.advancedReload;
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x0002A64A File Offset: 0x0002884A
		[Getter]
		public static float GetReloadTime(Weapon self)
		{
			return self.configuration.reloadTime;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0002A657 File Offset: 0x00028857
		[Setter]
		public static void SetReloadTime(Weapon self, float reloadTime)
		{
			self.configuration.reloadTime = reloadTime;
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0002A665 File Offset: 0x00028865
		[Getter]
		public static float GetAimFov(Weapon self)
		{
			return self.configuration.aimFov;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0012F4E4 File Offset: 0x0012D6E4
		[Setter]
		[Doc("The Field Of View (zoom level) applied to the player when aiming this weapon")]
		public static void SetAimFov(Weapon self, float fov)
		{
			self.configuration.aimFov = fov;
			if (self.UserIsPlayer() && self.user.activeWeapon != null && self.user.activeWeapon.GetActiveSubWeapon() == self)
			{
				FpsActorController.instance.SetupWeaponFov(self);
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x0002A672 File Offset: 0x00028872
		[Getter]
		[Doc("True while fire button is held down.[..]")]
		public static bool IsHoldingFire(Weapon self)
		{
			return self.holdingFire;
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x0002A67A File Offset: 0x0002887A
		[Getter]
		public static bool IsUnholstered(Weapon self)
		{
			return self.unholstered;
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x0002A682 File Offset: 0x00028882
		[Getter]
		public static int GetSlot(Weapon self)
		{
			return self.slot;
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x0002A68A File Offset: 0x0002888A
		[Getter]
		public static bool IsAiming(Weapon self)
		{
			return self.aiming;
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x0002A692 File Offset: 0x00028892
		[Getter]
		[Doc("Returns true if the weapon can be fired.[..] Checks ammo, cooldown, overheat, lock, target tracker and misc flags")]
		public static bool CanFire(Weapon self)
		{
			return self.CanFire();
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0002A69A File Offset: 0x0002889A
		[Getter]
		public static bool IsCoolingDown(Weapon self)
		{
			return self.CoolingDown();
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0002A6A2 File Offset: 0x000288A2
		[Getter]
		public static float GetCooldown(Weapon self)
		{
			return self.configuration.cooldown;
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0002A6AF File Offset: 0x000288AF
		[Setter]
		public static void SetCooldown(Weapon self, float cooldown)
		{
			self.configuration.cooldown = cooldown;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0002A6BD File Offset: 0x000288BD
		[Getter]
		public static bool IsFull(Weapon self)
		{
			return self.AmmoFull();
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0002A6C5 File Offset: 0x000288C5
		[Getter]
		public static bool IsEmpty(Weapon self)
		{
			return self.IsEmpty();
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0002A6CD File Offset: 0x000288CD
		[Getter]
		public static bool HasSpareAmmo(Weapon self)
		{
			return self.HasSpareAmmo();
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0002A6D5 File Offset: 0x000288D5
		[Getter]
		public static bool HasLoadedAmmo(Weapon self)
		{
			return self.HasLoadedAmmo();
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0002A6DD File Offset: 0x000288DD
		[Getter]
		[Doc("The muzzle transform that the next projectile will fire from.[..]")]
		public static Transform GetCurrentMuzzleTransform(Weapon self)
		{
			return self.CurrentMuzzle();
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0002A6E5 File Offset: 0x000288E5
		[Getter]
		public static Transform[] GetMuzzleTransforms(Weapon self)
		{
			return self.configuration.muzzles;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0002A6F2 File Offset: 0x000288F2
		[Getter]
		[Doc("Get the weapon animator.[..] Please note that weapons carried by the AI never have animators. Only player carried weapons and MountedWeapons can have animators.")]
		public static Animator GetAnimator(Weapon self)
		{
			return self.animator;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0002A6FA File Offset: 0x000288FA
		[Getter]
		[Doc("The current spread magnitude of a weapon.[..] The spread magnitude is the radius of a sphere 1 meter in front of the muzzle. The projectile may fire towards a random point inside that sphere.")]
		public static float GetCurrentSpreadMagnitude(Weapon self)
		{
			return self.GetCurrentSpreadMagnitude();
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0002A702 File Offset: 0x00028902
		[Getter]
		[Doc("The current maximum spread of a weapon in radians.[..]")]
		public static float GetCurrentSpreadMaxAngleRadians(Weapon self)
		{
			return self.GetCurrentSpreadMaxAngleRadians();
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0002A70A File Offset: 0x0002890A
		[Undocumented]
		public static void Shoot(Weapon self)
		{
			WWeapon.Shoot(self, false);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0002A713 File Offset: 0x00028913
		[Doc("Shoots this weapon.[..]If force is true, ignores the CanFire() check.")]
		public static void Shoot(Weapon self, bool force)
		{
			self.Shoot(force);
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0002A71C File Offset: 0x0002891C
		[Getter]
		[Consumable("Consuming this event stops the weapon from firing.")]
		[Doc("Invoked when the weapon is fired.")]
		public static ScriptEvent GetOnFire(Weapon self)
		{
			return self.onFireScriptable;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0002A724 File Offset: 0x00028924
		[Getter]
		[Doc("Invoked when the weapon is fired.[..] Provides an array of all spawned projectiles.")]
		public static ScriptEvent GetOnSpawnProjectiles(Weapon self)
		{
			return RavenscriptManager.events.WrapUnityEvent<Projectile[]>(self, self.onSpawnProjectiles);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0002A737 File Offset: 0x00028937
		public static void SetProjectilePrefab(Weapon self, GameObject prefab)
		{
			self.configuration.projectilePrefab = prefab;
			self.OnProjectilePrefabChanged();
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x0002A74B File Offset: 0x0002894B
		[Getter]
		public static bool GetIsLoud(Weapon self)
		{
			return self.configuration.loud;
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x0002A758 File Offset: 0x00028958
		[Setter]
		[Doc("A loud weapon will alert nearby enemies when being fired.[..]")]
		public static void SetIsLoud(Weapon self, bool isLoud)
		{
			self.configuration.loud = isLoud;
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0002A766 File Offset: 0x00028966
		[Getter]
		public static int GetProjectilesPerShot(Weapon self)
		{
			return self.configuration.projectilesPerShot;
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0002A773 File Offset: 0x00028973
		[Setter]
		[Doc("The number of projectiles spawned when the weapons is fired.[..]")]
		public static void SetProjectilesPerShot(Weapon self, int projectilesPerShot)
		{
			self.configuration.projectilesPerShot = projectilesPerShot;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x0002A781 File Offset: 0x00028981
		[Getter]
		public static bool GetIsAuto(Weapon self)
		{
			return self.configuration.auto;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0002A78E File Offset: 0x0002898E
		[Setter]
		public static void SetIsAuto(Weapon self, bool isAuto)
		{
			if (isAuto)
			{
				self.SetAutoFire();
				return;
			}
			self.SetSingleFire();
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x0002A7A0 File Offset: 0x000289A0
		[Getter]
		public static float GetUnholsterTime(Weapon self)
		{
			return self.configuration.unholsterTime;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0002A7AD File Offset: 0x000289AD
		[Setter]
		public static void SetUnholsterTime(Weapon self, float time)
		{
			self.configuration.unholsterTime = time;
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0002A7BB File Offset: 0x000289BB
		[Getter]
		public static float GetEffectiveRange(Weapon self)
		{
			return self.configuration.effectiveRange;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0002A7C8 File Offset: 0x000289C8
		[Setter]
		[Doc("The range at which the weapon is considered effective.")]
		public static void SetEffectiveRange(Weapon self, float effectiveRange)
		{
			self.configuration.effectiveRange = effectiveRange;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0002A7D6 File Offset: 0x000289D6
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessAir(Weapon self)
		{
			return self.configuration.effAir;
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0002A7E3 File Offset: 0x000289E3
		[Setter]
		[Doc("Effectiveness of this weapon against Air targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessAir(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effAir = effectiveness;
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x0002A7F1 File Offset: 0x000289F1
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessAirFastMover(Weapon self)
		{
			return self.configuration.effAirFastMover;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x0002A7FE File Offset: 0x000289FE
		[Setter]
		[Doc("Effectiveness of this weapon against Airplane targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessAirFastMover(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effAirFastMover = effectiveness;
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x0002A80C File Offset: 0x00028A0C
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessInfantry(Weapon self)
		{
			return self.configuration.effInfantry;
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0002A819 File Offset: 0x00028A19
		[Setter]
		[Doc("Effectiveness of this weapon against Infantry targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessInfantry(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effInfantry = effectiveness;
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0002A827 File Offset: 0x00028A27
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessInfantryGroup(Weapon self)
		{
			return self.configuration.effInfantryGroup;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0002A834 File Offset: 0x00028A34
		[Setter]
		[Doc("Effectiveness of this weapon against InfantryGroup targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessInfantryGroup(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effInfantryGroup = effectiveness;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0002A842 File Offset: 0x00028A42
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessArmored(Weapon self)
		{
			return self.configuration.effArmored;
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0002A84F File Offset: 0x00028A4F
		[Setter]
		[Doc("Effectiveness of this weapon against Armored targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessArmored(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effArmored = effectiveness;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0002A85D File Offset: 0x00028A5D
		[Getter]
		public static Weapon.Effectiveness GetEffectivenessUnarmored(Weapon self)
		{
			return self.configuration.effUnarmored;
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0002A86A File Offset: 0x00028A6A
		[Setter]
		[Doc("Effectiveness of this weapon against Unarmored targets.[..] This value is used by AI and TargetTrackers to prioritize targets.")]
		public static void SetEffectivenessUnarmored(Weapon self, Weapon.Effectiveness effectiveness)
		{
			self.configuration.effUnarmored = effectiveness;
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0002A878 File Offset: 0x00028A78
		[Getter]
		public static Weapon.Difficulty GetDifficultyInfantry(Weapon self)
		{
			return self.configuration.diffInfantry;
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0002A885 File Offset: 0x00028A85
		[Setter]
		[Doc("Difficulty of hitting Infantry target with this weapon.")]
		public static void SetDifficultyInfantry(Weapon self, Weapon.Difficulty difficulty)
		{
			self.configuration.diffInfantry = difficulty;
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0002A893 File Offset: 0x00028A93
		[Getter]
		public static Weapon.Difficulty GetDifficultyInfantryGroup(Weapon self)
		{
			return self.configuration.diffInfantryGroup;
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		[Setter]
		[Doc("Difficulty of hitting InfantryGroup target with this weapon.")]
		public static void SetDifficultyInfantryGroup(Weapon self, Weapon.Difficulty difficulty)
		{
			self.configuration.diffInfantryGroup = difficulty;
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x0002A8AE File Offset: 0x00028AAE
		[Getter]
		public static Weapon.Difficulty GetDifficultyAir(Weapon self)
		{
			return self.configuration.diffAir;
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x0002A8BB File Offset: 0x00028ABB
		[Setter]
		[Doc("Difficulty of hitting Air target with this weapon.")]
		public static void SetDifficultyAir(Weapon self, Weapon.Difficulty difficulty)
		{
			self.configuration.diffAir = difficulty;
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x0002A8C9 File Offset: 0x00028AC9
		[Getter]
		public static Weapon.Difficulty GetDifficultyAirFastMover(Weapon self)
		{
			return self.configuration.diffAirFastMover;
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x0002A8D6 File Offset: 0x00028AD6
		[Setter]
		[Doc("Difficulty of hitting AirFastMover target with this weapon.")]
		public static void SetDifficultyAirFastMover(Weapon self, Weapon.Difficulty difficulty)
		{
			self.configuration.diffAirFastMover = difficulty;
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0002A8E4 File Offset: 0x00028AE4
		[Getter]
		public static Weapon.Difficulty GetDifficultyGroundVehicles(Weapon self)
		{
			return self.configuration.diffGroundVehicles;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0002A8F1 File Offset: 0x00028AF1
		[Setter]
		[Doc("Difficulty of hitting GroundVehicles target with this weapon.")]
		public static void SetDifficultyGroundVehicles(Weapon self, Weapon.Difficulty difficulty)
		{
			self.configuration.diffGroundVehicles = difficulty;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0002A8FF File Offset: 0x00028AFF
		[Getter]
		public static float GetRecoilBaseKickback(Weapon self)
		{
			return self.configuration.kickback;
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0002A90C File Offset: 0x00028B0C
		[Setter]
		public static void SetRecoilBaseKickback(Weapon self, float kickback)
		{
			self.configuration.kickback = kickback;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0002A91A File Offset: 0x00028B1A
		[Getter]
		public static float GetRecoilRandomKickback(Weapon self)
		{
			return self.configuration.randomKick;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0002A927 File Offset: 0x00028B27
		[Setter]
		public static void SetRecoilRandomKickback(Weapon self, float randomKickback)
		{
			self.configuration.randomKick = randomKickback;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0002A935 File Offset: 0x00028B35
		[Getter]
		public static float GetRecoilKickbackProneMultiplier(Weapon self)
		{
			return self.configuration.kickbackProneMultiplier;
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0002A942 File Offset: 0x00028B42
		[Setter]
		public static void SetRecoilKickbackProneMultiplier(Weapon self, float kickbackProneMultiplier)
		{
			self.configuration.kickbackProneMultiplier = kickbackProneMultiplier;
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0002A950 File Offset: 0x00028B50
		[Getter]
		public static float GetRecoilSnapMagnitude(Weapon self)
		{
			return self.configuration.snapMagnitude;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0002A95D File Offset: 0x00028B5D
		[Setter]
		public static void SetRecoilSnapMagnitude(Weapon self, float snapMagnitude)
		{
			self.configuration.snapMagnitude = snapMagnitude;
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0002A950 File Offset: 0x00028B50
		[Getter]
		public static float GetRecoilSnapFrequency(Weapon self)
		{
			return self.configuration.snapMagnitude;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0002A96B File Offset: 0x00028B6B
		[Setter]
		public static void SetRecoilSnapFrequency(Weapon self, float snapFrequency)
		{
			self.configuration.snapFrequency = snapFrequency;
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0002A979 File Offset: 0x00028B79
		[Getter]
		public static float GetRecoilSnapDuration(Weapon self)
		{
			return self.configuration.snapDuration;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0002A986 File Offset: 0x00028B86
		[Setter]
		public static void SetRecoilSnapDuration(Weapon self, float snapDuration)
		{
			self.configuration.snapDuration = snapDuration;
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0002A994 File Offset: 0x00028B94
		[Getter]
		public static float GetRecoilSnapProneMultiplier(Weapon self)
		{
			return self.configuration.snapProneMultiplier;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0002A9A1 File Offset: 0x00028BA1
		[Setter]
		public static void SetRecoilSnapProneMultiplier(Weapon self, float snapProneMultiplier)
		{
			self.configuration.snapProneMultiplier = snapProneMultiplier;
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0002A9AF File Offset: 0x00028BAF
		[Getter]
		public static float GetBaseSpread(Weapon self)
		{
			return self.configuration.spread;
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0002A9BC File Offset: 0x00028BBC
		[Setter]
		public static void SetBaseSpread(Weapon self, float baseSpread)
		{
			self.configuration.spread = baseSpread;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0012F53C File Offset: 0x0012D73C
		[Getter]
		public static WFollowupSpread GetFollowupSpread(Weapon self)
		{
			return new WFollowupSpread
			{
				maxSpreadAim = self.configuration.followupMaxSpreadAim,
				maxSpreadHip = self.configuration.followupMaxSpreadHip,
				spreadDissipateTime = self.configuration.followupSpreadDissipateTime,
				spreadGain = self.configuration.followupSpreadGain,
				proneMultiplier = self.configuration.followupSpreadProneMultiplier,
				spreadStayTime = self.configuration.followupSpreadStayTime
			};
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0012F5C0 File Offset: 0x0012D7C0
		[Setter]
		public static void SetFollowupSpread(Weapon self, WFollowupSpread followupSpread)
		{
			self.configuration.followupMaxSpreadAim = followupSpread.maxSpreadAim;
			self.configuration.followupMaxSpreadHip = followupSpread.maxSpreadHip;
			self.configuration.followupSpreadDissipateTime = followupSpread.spreadDissipateTime;
			self.configuration.followupSpreadGain = followupSpread.spreadGain;
			self.configuration.followupSpreadProneMultiplier = followupSpread.proneMultiplier;
			self.configuration.followupSpreadStayTime = followupSpread.spreadStayTime;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x0002A9CA File Offset: 0x00028BCA
		[Doc("Lock the weapon so it cannot be fired.[..]")]
		public static void LockWeapon(Weapon self)
		{
			self.LockWeapon();
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0002A9D2 File Offset: 0x00028BD2
		[Doc("Unlock the weapon so it can be fired.[..]")]
		public static void UnlockWeapon(Weapon self)
		{
			self.UnlockWeapon();
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0002A9DA File Offset: 0x00028BDA
		[Getter]
		public static bool GetIsLocked(Weapon self)
		{
			return self.isLocked;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0002A9E2 File Offset: 0x00028BE2
		[Setter]
		[Doc("Locked weapons cannot be fired by the user.[..]")]
		public static void SetIsLocked(Weapon self, bool isLocked)
		{
			self.isLocked = isLocked;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x0002A9EB File Offset: 0x00028BEB
		[Getter]
		public static bool IsOverheating(Weapon self)
		{
			return self.isOverheating;
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0002A9F3 File Offset: 0x00028BF3
		[Getter]
		public static float GetHeat(Weapon self)
		{
			return self.heat;
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x0002A9FB File Offset: 0x00028BFB
		[Setter]
		[Doc("The current heat value of the weapon if applyHeat is enabled.[..] When heat reaches 1, the weapon cannot be fired until the heat reaches 0 again.")]
		public static void SetHeat(Weapon self, float heat)
		{
			self.heat = heat;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0002AA04 File Offset: 0x00028C04
		[Getter]
		public static bool GetApplyHeat(Weapon self)
		{
			return self.configuration.applyHeat;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0002AA11 File Offset: 0x00028C11
		[Setter]
		[Doc("If true, each shot will heat up the weapon.[..] If the heat value reaches 1 the gun overheats, and cannot be fired again until it cools down.")]
		public static void SetApplyHeat(Weapon self, bool applyHeat)
		{
			self.configuration.applyHeat = applyHeat;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0002AA1F File Offset: 0x00028C1F
		[Getter]
		public static float GetHeatDrainRate(Weapon self)
		{
			return self.configuration.heatDrainRate;
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0002AA2C File Offset: 0x00028C2C
		[Setter]
		[Doc("The rate at which heat drains in units per second.[..]")]
		public static void SetHeatDrainRate(Weapon self, float drainRate)
		{
			self.configuration.heatDrainRate = drainRate;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0002AA3A File Offset: 0x00028C3A
		[Getter]
		public static float GetHeatGainPerShot(Weapon self)
		{
			return self.configuration.heatGainPerShot;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0002AA47 File Offset: 0x00028C47
		[Setter]
		public static void SetHeatGainPerShot(Weapon self, float heatGain)
		{
			self.configuration.heatGainPerShot = heatGain;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0002AA55 File Offset: 0x00028C55
		[Getter]
		[Doc("True if fire input has been held long enough to fire the weapon.")]
		public static bool IsCharged(Weapon self)
		{
			return self.IsCharged();
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0002AA5D File Offset: 0x00028C5D
		[Getter]
		public static bool GetUseChargeTime(Weapon self)
		{
			return self.configuration.useChargeTime;
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0002AA6A File Offset: 0x00028C6A
		[Setter]
		[Doc("When true, the weapon must charge before firing.[..] Requires the fire button to be held for chargeTime seconds before firing.")]
		public static void SetUseChargeTime(Weapon self, bool useChargeTime)
		{
			self.configuration.useChargeTime = useChargeTime;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x0002AA78 File Offset: 0x00028C78
		[Getter]
		public static float GetChargeTime(Weapon self)
		{
			return self.configuration.chargeTime;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0002AA85 File Offset: 0x00028C85
		[Setter]
		public static void SetChargeTime(Weapon self, float chargeTime)
		{
			self.configuration.chargeTime = chargeTime;
			self.OnChargeTimeChanged();
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x0002AA99 File Offset: 0x00028C99
		[Getter]
		[Doc("Get the currently active Sub Weapon from the ``alternativeWeapons`` list.[..]")]
		public static Weapon GetActiveSubWeapon(Weapon self)
		{
			return self.GetActiveSubWeapon();
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0002AAA1 File Offset: 0x00028CA1
		[Getter]
		[Doc("Get the currently active Sub Weapon index.[..]")]
		public static int GetActiveSubWeaponIndex(Weapon self)
		{
			return self.activeSubWeaponIndex;
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x0002AAA9 File Offset: 0x00028CA9
		[Getter]
		public static Weapon[] GetAlternativeWeapons(Weapon self)
		{
			return self.alternativeWeapons.ToArray();
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0002AAB6 File Offset: 0x00028CB6
		[Doc("Switch to the next Sub Weapon in the Weapon's ``alternativeWeapons`` list.[..]")]
		public static void NextSubWeapon(Weapon self)
		{
			self.NextSubWeapon();
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x0002AABE File Offset: 0x00028CBE
		[Doc("Switch to the next Sub Weapon in the Weapon's ``alternativeWeapons`` list.[..]")]
		public static void EquipSubWeapon(Weapon self, int subWeaponIndex)
		{
			self.EquipSubWeapon(subWeaponIndex);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x0002AAC7 File Offset: 0x00028CC7
		[Doc("Add a subweapon to this parent weapon, returning the subweapon index.")]
		public static int AddSubWeapon(Weapon self, Weapon subWeapon)
		{
			return self.AddSubWeapon(subWeapon);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0002AAD0 File Offset: 0x00028CD0
		[Doc("Removes a subweapon from this parent weapon.")]
		public static void RemoveSubWeapon(Weapon self, int subWeaponIndex)
		{
			self.RemoveSubWeapon(subWeaponIndex);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x0002AAD9 File Offset: 0x00028CD9
		[Doc("Removes a subweapon from this parent weapon.")]
		public static void RemoveSubWeapon(Weapon self, Weapon subWeapon)
		{
			self.RemoveSubWeapon(subWeapon);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x0002AAE2 File Offset: 0x00028CE2
		[Getter]
		public static Sprite GetUiSprite(Weapon self)
		{
			return self.uiSprite;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0002AAEA File Offset: 0x00028CEA
		public static void NextSightMode(Weapon self)
		{
			self.NextSightMode();
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x0002AAF3 File Offset: 0x00028CF3
		public static void PreviousSightMode(Weapon self)
		{
			self.PreviousSightMode();
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x0002AAFC File Offset: 0x00028CFC
		[Getter]
		public static int GetActiveSightModeIndex(Weapon self)
		{
			return self.activeSightModeIndex;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x0002AB04 File Offset: 0x00028D04
		[Getter]
		public static Vector3 GetThirdPersonOffset(Weapon self)
		{
			return self.thirdPersonOffset;
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x0002AB0C File Offset: 0x00028D0C
		[Getter]
		public static Quaternion GetThirdPersonRotation(Weapon self)
		{
			return Quaternion.Euler(self.thirdPersonRotation);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x0002AB19 File Offset: 0x00028D19
		[Getter]
		public static float GetThirdPersonScale(Weapon self)
		{
			return self.thirdPersonScale;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x0002AB21 File Offset: 0x00028D21
		[Doc("Matches a weapon role based on the weapon's current stats.")]
		public static Weapon.WeaponRole GenerateWeaponRoleFromStats(Weapon self)
		{
			return self.GenerateWeaponRoleFromStats();
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x0002AB29 File Offset: 0x00028D29
		[Getter]
		public static GameObject GetScopeAimObject(Weapon self)
		{
			return self.configuration.scopeAimObject;
		}
	}
}

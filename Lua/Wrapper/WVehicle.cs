using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200099D RID: 2461
	[Wrapper(typeof(Vehicle))]
	public static class WVehicle
	{
		// Token: 0x06003E7F RID: 15999 RVA: 0x0002A3D9 File Offset: 0x000285D9
		[Getter]
		public static IEnumerable<Seat> GetSeats(Vehicle self)
		{
			return self.seats;
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0002A3E1 File Offset: 0x000285E1
		[Getter]
		public static string GetName(Vehicle self)
		{
			return self.name;
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0002A3E9 File Offset: 0x000285E9
		[Getter]
		public static Texture GetMinimapBlip(Vehicle self)
		{
			return self.blip;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0002A3F1 File Offset: 0x000285F1
		[Getter]
		public static int GetTeam(Vehicle self)
		{
			return self.ownerTeam;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0002A3F9 File Offset: 0x000285F9
		[Getter]
		public static bool IsDead(Vehicle self)
		{
			return self.dead;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0002A401 File Offset: 0x00028601
		[Getter]
		public static bool GetHasCountermeasures(Vehicle self)
		{
			return self.hasCountermeasures;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0002A409 File Offset: 0x00028609
		[Getter]
		[Doc("Returns true if there is a line of sight between the vehicle's lock on point and the player camera")]
		public static bool GetCanSeePlayer(Vehicle self)
		{
			return self.canSeePlayer;
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0002A411 File Offset: 0x00028611
		[Getter]
		public static float GetPlayerDistance(Vehicle self)
		{
			return self.playerDistance;
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x0002A419 File Offset: 0x00028619
		[Getter]
		public static bool GetPlayerIsInside(Vehicle self)
		{
			return self.playerIsInside;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x0002A421 File Offset: 0x00028621
		[Getter]
		public static bool HasDriver(Vehicle self)
		{
			return self.HasDriver();
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x0002A429 File Offset: 0x00028629
		[Getter]
		public static Actor GetDriver(Vehicle self)
		{
			return self.Driver();
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0002A431 File Offset: 0x00028631
		[Getter]
		[Doc("True while one or more missiles are tracking this vehicle")]
		public static bool GetIsTrackedByMissile(Vehicle self)
		{
			return self.IsBeingTrackedByMissile();
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x0002A439 File Offset: 0x00028639
		[Getter]
		[Doc("True while one or more target tracking weapons are locking onto this vehicle")]
		public static bool GetIsBeingLocked(Vehicle self)
		{
			return self.IsBeingLocked();
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x0002A441 File Offset: 0x00028641
		[Doc("Returns all missile projectiles that are currently tracking this vehicle.")]
		public static TargetSeekingMissile[] GetTrackingMissiles(Vehicle self)
		{
			return self.trackingMissiles.ToArray<TargetSeekingMissile>();
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0012F41C File Offset: 0x0012D61C
		public static void Damage(Vehicle self, Actor source, float amount)
		{
			self.Damage(new DamageInfo(DamageInfo.DamageSourceType.Scripted, source, null)
			{
				healthDamage = amount
			});
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0002A44E File Offset: 0x0002864E
		[Doc("Repairs the vehicle by the specified health amount.[..] Returns true if the vehicle was healed (that is, if it was not already at max health.)")]
		public static bool Repair(Vehicle self, float amount)
		{
			return self.Repair(amount);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0002A457 File Offset: 0x00028657
		[Getter]
		public static float GetHealth(Vehicle self)
		{
			return self.health;
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0002A45F File Offset: 0x0002865F
		[Setter]
		public static void SetHealth(Vehicle self, float health)
		{
			self.health = health;
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0002A468 File Offset: 0x00028668
		[Getter]
		public static float GetMaxHealth(Vehicle self)
		{
			return self.maxHealth;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x0002A470 File Offset: 0x00028670
		[Setter]
		public static void SetMaxHealth(Vehicle self, float maxHealth)
		{
			self.maxHealth = maxHealth;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0002A479 File Offset: 0x00028679
		[Getter]
		public static bool GetIsBurning(Vehicle self)
		{
			return self.burning;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0002A481 File Offset: 0x00028681
		[Doc("Get the first available empty seat in the vehicle.")]
		public static Seat GetEmptySeat(Vehicle self, bool allowDriverSeat)
		{
			return self.GetEmptySeat(allowDriverSeat);
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0002A48A File Offset: 0x0002868A
		[Getter]
		public static bool IsFull(Vehicle self)
		{
			return self.IsFull();
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0002A48A File Offset: 0x0002868A
		[Getter]
		public static bool IsEmpty(Vehicle self)
		{
			return self.IsFull();
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0002A492 File Offset: 0x00028692
		[Getter]
		public static bool IsInWater(Vehicle self)
		{
			return self.IsInWater();
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0002A49A File Offset: 0x0002869A
		[Getter]
		public static Rigidbody GetRigidbody(Vehicle self)
		{
			return self.rigidbody;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0002A4A2 File Offset: 0x000286A2
		[Getter]
		[Doc("Returns true if vehicle is marked as a Turret.")]
		public static bool GetIsTurret(Vehicle self)
		{
			return self.isTurret;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0002A4AA File Offset: 0x000286AA
		[Getter]
		[Doc("Returns true if this vehicle is a Car.[..] If true, you can safely access fields via the Boat class API.")]
		public static bool GetIsCar(Vehicle self)
		{
			return self is ArcadeCar;
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x0002A4B5 File Offset: 0x000286B5
		[Getter]
		[Doc("Returns true if this vehicle is a Boat.[..] If true, you can safely access fields via the Boat class API.")]
		public static bool GetIsBoat(Vehicle self)
		{
			return self is Boat;
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x0002A4C0 File Offset: 0x000286C0
		[Getter]
		[Doc("Returns true if this vehicle is a Helicopter.[..] If true, you can safely access fields via the Helicopter class API.")]
		public static bool GetIsHelicopter(Vehicle self)
		{
			return self is Helicopter;
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x0002A4CB File Offset: 0x000286CB
		[Getter]
		[Doc("Returns true if this vehicle is an Airplane.[..] If true, you can safely access fields via the Airplane class API.")]
		public static bool GetIsAirplane(Vehicle self)
		{
			return self is Airplane;
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x0002A4D6 File Offset: 0x000286D6
		[Getter]
		[Doc("The radius of the vehicle's specified avoidance size")]
		public static float GetAvoidanceRadius(Vehicle self)
		{
			return self.GetAvoidanceCoarseRadius();
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x0002A4DE File Offset: 0x000286DE
		[Getter]
		public static float GetSpotChanceMultiplier(Vehicle self)
		{
			return self.spotChanceMultiplier;
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x0002A4E6 File Offset: 0x000286E6
		[Setter]
		public static void SetSpotChanceMultiplier(Vehicle self, float multiplier)
		{
			self.spotChanceMultiplier = multiplier;
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0002A4EF File Offset: 0x000286EF
		[Getter]
		public static Vehicle.Engine GetEngine(Vehicle self)
		{
			return self.engine;
		}
	}
}

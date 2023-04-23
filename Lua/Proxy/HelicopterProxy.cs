using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009CA RID: 2506
	[Proxy(typeof(Helicopter))]
	public class HelicopterProxy : IProxy
	{
		// Token: 0x06004511 RID: 17681 RVA: 0x00030034 File Offset: 0x0002E234
		[MoonSharpHidden]
		public HelicopterProxy(Helicopter value)
		{
			this._value = value;
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00030043 File Offset: 0x0002E243
		public HelicopterProxy()
		{
			this._value = new Helicopter();
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x00030056 File Offset: 0x0002E256
		// (set) Token: 0x06004514 RID: 17684 RVA: 0x00030063 File Offset: 0x0002E263
		public float aerodynamicLift
		{
			get
			{
				return this._value.aerodynamicLift;
			}
			set
			{
				this._value.aerodynamicLift = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x00030071 File Offset: 0x0002E271
		// (set) Token: 0x06004516 RID: 17686 RVA: 0x0003007E File Offset: 0x0002E27E
		public float altitude
		{
			get
			{
				return this._value.altitude;
			}
			set
			{
				this._value.altitude = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x0003008C File Offset: 0x0002E28C
		// (set) Token: 0x06004518 RID: 17688 RVA: 0x00030099 File Offset: 0x0002E299
		public float controlWhenBurning
		{
			get
			{
				return this._value.controlWhenBurning;
			}
			set
			{
				this._value.controlWhenBurning = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000300A7 File Offset: 0x0002E2A7
		// (set) Token: 0x0600451A RID: 17690 RVA: 0x000300B4 File Offset: 0x0002E2B4
		public float extraForceWhenStopping
		{
			get
			{
				return this._value.extraForceWhenStopping;
			}
			set
			{
				this._value.extraForceWhenStopping = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000300C2 File Offset: 0x0002E2C2
		// (set) Token: 0x0600451C RID: 17692 RVA: 0x000300CF File Offset: 0x0002E2CF
		public float flightAltitudeMultiplier
		{
			get
			{
				return this._value.flightAltitudeMultiplier;
			}
			set
			{
				this._value.flightAltitudeMultiplier = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x000300DD File Offset: 0x0002E2DD
		// (set) Token: 0x0600451E RID: 17694 RVA: 0x000300EA File Offset: 0x0002E2EA
		public float groundEffectAcceleration
		{
			get
			{
				return this._value.groundEffectAcceleration;
			}
			set
			{
				this._value.groundEffectAcceleration = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x000300F8 File Offset: 0x0002E2F8
		// (set) Token: 0x06004520 RID: 17696 RVA: 0x00030105 File Offset: 0x0002E305
		public float manouverability
		{
			get
			{
				return this._value.manouverability;
			}
			set
			{
				this._value.manouverability = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x00030113 File Offset: 0x0002E313
		// (set) Token: 0x06004522 RID: 17698 RVA: 0x00030120 File Offset: 0x0002E320
		public float rotorForce
		{
			get
			{
				return this._value.rotorForce;
			}
			set
			{
				this._value.rotorForce = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x0003012E File Offset: 0x0002E32E
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x00030140 File Offset: 0x0002E340
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x00030152 File Offset: 0x0002E352
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0003015F File Offset: 0x0002E35F
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x0003016C File Offset: 0x0002E36C
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x0003017E File Offset: 0x0002E37E
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x00030190 File Offset: 0x0002E390
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x0003019D File Offset: 0x0002E39D
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x000301AA File Offset: 0x0002E3AA
		public float health
		{
			get
			{
				return WVehicle.GetHealth(this._value);
			}
			set
			{
				WVehicle.SetHealth(this._value, value);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x000301B8 File Offset: 0x0002E3B8
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x000301C5 File Offset: 0x0002E3C5
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x000301D2 File Offset: 0x0002E3D2
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x000301DF File Offset: 0x0002E3DF
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000301EC File Offset: 0x0002E3EC
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06004531 RID: 17713 RVA: 0x000301F9 File Offset: 0x0002E3F9
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x00030206 File Offset: 0x0002E406
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x00030213 File Offset: 0x0002E413
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x00030220 File Offset: 0x0002E420
		// (set) Token: 0x06004535 RID: 17717 RVA: 0x0003022D File Offset: 0x0002E42D
		public float maxHealth
		{
			get
			{
				return WVehicle.GetMaxHealth(this._value);
			}
			set
			{
				WVehicle.SetMaxHealth(this._value, value);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x0003023B File Offset: 0x0002E43B
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x0003024D File Offset: 0x0002E44D
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x0003025A File Offset: 0x0002E45A
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x00030267 File Offset: 0x0002E467
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x00030274 File Offset: 0x0002E474
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00030286 File Offset: 0x0002E486
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x00030293 File Offset: 0x0002E493
		// (set) Token: 0x0600453D RID: 17725 RVA: 0x000302A0 File Offset: 0x0002E4A0
		public float spotChanceMultiplier
		{
			get
			{
				return WVehicle.GetSpotChanceMultiplier(this._value);
			}
			set
			{
				WVehicle.SetSpotChanceMultiplier(this._value, value);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x000302AE File Offset: 0x0002E4AE
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x000302BB File Offset: 0x0002E4BB
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x000302C8 File Offset: 0x0002E4C8
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x000302D5 File Offset: 0x0002E4D5
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x000302E2 File Offset: 0x0002E4E2
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x000302EF File Offset: 0x0002E4EF
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000302FC File Offset: 0x0002E4FC
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x00130A34 File Offset: 0x0012EC34
		[MoonSharpHidden]
		public static HelicopterProxy New(Helicopter value)
		{
			if (value == null)
			{
				return null;
			}
			HelicopterProxy helicopterProxy = (HelicopterProxy)ObjectCache.Get(typeof(HelicopterProxy), value);
			if (helicopterProxy == null)
			{
				helicopterProxy = new HelicopterProxy(value);
				ObjectCache.Add(typeof(HelicopterProxy), value, helicopterProxy);
			}
			return helicopterProxy;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x00030304 File Offset: 0x0002E504
		[MoonSharpUserDataMetamethod("__call")]
		public static HelicopterProxy Call(DynValue _)
		{
			return new HelicopterProxy();
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x00130A80 File Offset: 0x0012EC80
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x0003030B File Offset: 0x0002E50B
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x0003031E File Offset: 0x0002E51E
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0003032B File Offset: 0x0002E52B
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00030339 File Offset: 0x0002E539
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003162 RID: 12642
		[MoonSharpHidden]
		public Helicopter _value;
	}
}

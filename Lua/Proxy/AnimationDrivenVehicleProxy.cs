using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009AF RID: 2479
	[Proxy(typeof(AnimationDrivenVehicle))]
	public class AnimationDrivenVehicleProxy : IProxy
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x0002B999 File Offset: 0x00029B99
		[MoonSharpHidden]
		public AnimationDrivenVehicleProxy(AnimationDrivenVehicle value)
		{
			this._value = value;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public AnimationDrivenVehicleProxy()
		{
			this._value = new AnimationDrivenVehicle();
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x0002B9BB File Offset: 0x00029BBB
		// (set) Token: 0x06004057 RID: 16471 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public int inputSmoothness
		{
			get
			{
				return this._value.inputSmoothness;
			}
			set
			{
				this._value.inputSmoothness = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x0002B9D6 File Offset: 0x00029BD6
		// (set) Token: 0x06004059 RID: 16473 RVA: 0x0002B9E3 File Offset: 0x00029BE3
		public bool planeInput
		{
			get
			{
				return this._value.planeInput;
			}
			set
			{
				this._value.planeInput = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x0002B9F1 File Offset: 0x00029BF1
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x0002BA03 File Offset: 0x00029C03
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x0002BA15 File Offset: 0x00029C15
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x0002BA22 File Offset: 0x00029C22
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x0002BA2F File Offset: 0x00029C2F
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x0002BA41 File Offset: 0x00029C41
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x0002BA53 File Offset: 0x00029C53
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x0002BA60 File Offset: 0x00029C60
		// (set) Token: 0x06004062 RID: 16482 RVA: 0x0002BA6D File Offset: 0x00029C6D
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x0002BA7B File Offset: 0x00029C7B
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x0002BA88 File Offset: 0x00029C88
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x0002BA95 File Offset: 0x00029C95
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x0002BAA2 File Offset: 0x00029CA2
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x0002BAAF File Offset: 0x00029CAF
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x0002BABC File Offset: 0x00029CBC
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x0002BAC9 File Offset: 0x00029CC9
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x0002BAD6 File Offset: 0x00029CD6
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x0002BAE3 File Offset: 0x00029CE3
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x0002BAF0 File Offset: 0x00029CF0
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

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x0002BAFE File Offset: 0x00029CFE
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x0002BB10 File Offset: 0x00029D10
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x0002BB1D File Offset: 0x00029D1D
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x0002BB2A File Offset: 0x00029D2A
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x0002BB37 File Offset: 0x00029D37
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x0002BB49 File Offset: 0x00029D49
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x0002BB56 File Offset: 0x00029D56
		// (set) Token: 0x06004074 RID: 16500 RVA: 0x0002BB63 File Offset: 0x00029D63
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x0002BB71 File Offset: 0x00029D71
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x0002BB7E File Offset: 0x00029D7E
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x0002BB8B File Offset: 0x00029D8B
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x0002BB98 File Offset: 0x00029D98
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x0002BBA5 File Offset: 0x00029DA5
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x0002BBB2 File Offset: 0x00029DB2
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x0002BBBF File Offset: 0x00029DBF
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x0012FC94 File Offset: 0x0012DE94
		[MoonSharpHidden]
		public static AnimationDrivenVehicleProxy New(AnimationDrivenVehicle value)
		{
			if (value == null)
			{
				return null;
			}
			AnimationDrivenVehicleProxy animationDrivenVehicleProxy = (AnimationDrivenVehicleProxy)ObjectCache.Get(typeof(AnimationDrivenVehicleProxy), value);
			if (animationDrivenVehicleProxy == null)
			{
				animationDrivenVehicleProxy = new AnimationDrivenVehicleProxy(value);
				ObjectCache.Add(typeof(AnimationDrivenVehicleProxy), value, animationDrivenVehicleProxy);
			}
			return animationDrivenVehicleProxy;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x0002BBC7 File Offset: 0x00029DC7
		[MoonSharpUserDataMetamethod("__call")]
		public static AnimationDrivenVehicleProxy Call(DynValue _)
		{
			return new AnimationDrivenVehicleProxy();
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x0002BBCE File Offset: 0x00029DCE
		public Vector3Proxy AngularVelocity()
		{
			return Vector3Proxy.New(this._value.AngularVelocity());
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x0002BBE0 File Offset: 0x00029DE0
		public Vector3Proxy Velocity()
		{
			return Vector3Proxy.New(this._value.Velocity());
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0012FCE0 File Offset: 0x0012DEE0
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x0002BBF2 File Offset: 0x00029DF2
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0002BC05 File Offset: 0x00029E05
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0002BC12 File Offset: 0x00029E12
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0002BC20 File Offset: 0x00029E20
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003148 RID: 12616
		[MoonSharpHidden]
		public AnimationDrivenVehicle _value;
	}
}

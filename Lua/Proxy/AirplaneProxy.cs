using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009AD RID: 2477
	[Proxy(typeof(Airplane))]
	public class AirplaneProxy : IProxy
	{
		// Token: 0x06003FF0 RID: 16368 RVA: 0x0002B482 File Offset: 0x00029682
		[MoonSharpHidden]
		public AirplaneProxy(Airplane value)
		{
			this._value = value;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x0002B491 File Offset: 0x00029691
		public AirplaneProxy()
		{
			this._value = new Airplane();
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x0002B4A4 File Offset: 0x000296A4
		// (set) Token: 0x06003FF3 RID: 16371 RVA: 0x0002B4B1 File Offset: 0x000296B1
		public float acceleration
		{
			get
			{
				return this._value.acceleration;
			}
			set
			{
				this._value.acceleration = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x0002B4BF File Offset: 0x000296BF
		// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x0002B4CC File Offset: 0x000296CC
		public float accelerationThrottleDown
		{
			get
			{
				return this._value.accelerationThrottleDown;
			}
			set
			{
				this._value.accelerationThrottleDown = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0002B4DA File Offset: 0x000296DA
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x0002B4E7 File Offset: 0x000296E7
		public float accelerationThrottleUp
		{
			get
			{
				return this._value.accelerationThrottleUp;
			}
			set
			{
				this._value.accelerationThrottleUp = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0002B4F5 File Offset: 0x000296F5
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x0002B502 File Offset: 0x00029702
		public bool airbrake
		{
			get
			{
				return this._value.airbrake;
			}
			set
			{
				this._value.airbrake = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x0002B510 File Offset: 0x00029710
		// (set) Token: 0x06003FFB RID: 16379 RVA: 0x0002B51D File Offset: 0x0002971D
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x0002B52B File Offset: 0x0002972B
		// (set) Token: 0x06003FFD RID: 16381 RVA: 0x0002B538 File Offset: 0x00029738
		public float autoPitchTorqueGain
		{
			get
			{
				return this._value.autoPitchTorqueGain;
			}
			set
			{
				this._value.autoPitchTorqueGain = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0002B546 File Offset: 0x00029746
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x0002B553 File Offset: 0x00029753
		public float baseLift
		{
			get
			{
				return this._value.baseLift;
			}
			set
			{
				this._value.baseLift = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x0002B561 File Offset: 0x00029761
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x0002B56E File Offset: 0x0002976E
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

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x0002B57C File Offset: 0x0002977C
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x0012FB64 File Offset: 0x0012DD64
		public AnimationCurveProxy controlVsAngleOfAttack
		{
			get
			{
				return AnimationCurveProxy.New(this._value.controlVsAngleOfAttack);
			}
			set
			{
				AnimationCurve controlVsAngleOfAttack = null;
				if (value != null)
				{
					controlVsAngleOfAttack = value._value;
				}
				this._value.controlVsAngleOfAttack = controlVsAngleOfAttack;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0002B58E File Offset: 0x0002978E
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x0002B59B File Offset: 0x0002979B
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

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x0002B5A9 File Offset: 0x000297A9
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x0002B5B6 File Offset: 0x000297B6
		public bool gearsRetracted
		{
			get
			{
				return this._value.gearsRetracted;
			}
			set
			{
				this._value.gearsRetracted = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x0002B5C4 File Offset: 0x000297C4
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x0002B5D1 File Offset: 0x000297D1
		public bool isAirborne
		{
			get
			{
				return this._value.isAirborne;
			}
			set
			{
				this._value.isAirborne = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0002B5DF File Offset: 0x000297DF
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x0002B5EC File Offset: 0x000297EC
		public GameObject[] landingGearActivationObjects
		{
			get
			{
				return this._value.landingGearActivationObjects;
			}
			set
			{
				this._value.landingGearActivationObjects = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0002B5FA File Offset: 0x000297FA
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x0002B607 File Offset: 0x00029807
		public float liftGainTime
		{
			get
			{
				return this._value.liftGainTime;
			}
			set
			{
				this._value.liftGainTime = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0002B615 File Offset: 0x00029815
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0012FB8C File Offset: 0x0012DD8C
		public AnimationCurveProxy liftVsAngleOfAttack
		{
			get
			{
				return AnimationCurveProxy.New(this._value.liftVsAngleOfAttack);
			}
			set
			{
				AnimationCurve liftVsAngleOfAttack = null;
				if (value != null)
				{
					liftVsAngleOfAttack = value._value;
				}
				this._value.liftVsAngleOfAttack = liftVsAngleOfAttack;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0002B627 File Offset: 0x00029827
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x0002B634 File Offset: 0x00029834
		public float perpendicularDrag
		{
			get
			{
				return this._value.perpendicularDrag;
			}
			set
			{
				this._value.perpendicularDrag = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x0002B642 File Offset: 0x00029842
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x0002B64F File Offset: 0x0002984F
		public float pitchSensitivity
		{
			get
			{
				return this._value.pitchSensitivity;
			}
			set
			{
				this._value.pitchSensitivity = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x0002B65D File Offset: 0x0002985D
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x0002B66A File Offset: 0x0002986A
		public float rollSensitivity
		{
			get
			{
				return this._value.rollSensitivity;
			}
			set
			{
				this._value.rollSensitivity = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x0002B678 File Offset: 0x00029878
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x0002B685 File Offset: 0x00029885
		public float throttleEngineAudioPitchControl
		{
			get
			{
				return this._value.throttleEngineAudioPitchControl;
			}
			set
			{
				this._value.throttleEngineAudioPitchControl = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x0002B693 File Offset: 0x00029893
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x0012FBB4 File Offset: 0x0012DDB4
		public TransformProxy thruster
		{
			get
			{
				return TransformProxy.New(this._value.thruster);
			}
			set
			{
				Transform thruster = null;
				if (value != null)
				{
					thruster = value._value;
				}
				this._value.thruster = thruster;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x0002B6A5 File Offset: 0x000298A5
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x0002B6B2 File Offset: 0x000298B2
		public float yawSensitivity
		{
			get
			{
				return this._value.yawSensitivity;
			}
			set
			{
				this._value.yawSensitivity = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x0002B6C0 File Offset: 0x000298C0
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0002B6D2 File Offset: 0x000298D2
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0002B6E4 File Offset: 0x000298E4
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0002B6F1 File Offset: 0x000298F1
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0002B6FE File Offset: 0x000298FE
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0002B710 File Offset: 0x00029910
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0002B722 File Offset: 0x00029922
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x0002B72F File Offset: 0x0002992F
		// (set) Token: 0x06004024 RID: 16420 RVA: 0x0002B73C File Offset: 0x0002993C
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

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x0002B74A File Offset: 0x0002994A
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0002B757 File Offset: 0x00029957
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x0002B764 File Offset: 0x00029964
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0002B771 File Offset: 0x00029971
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x0002B77E File Offset: 0x0002997E
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x0002B78B File Offset: 0x0002998B
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x0002B798 File Offset: 0x00029998
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x0002B7A5 File Offset: 0x000299A5
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0002B7B2 File Offset: 0x000299B2
		// (set) Token: 0x0600402E RID: 16430 RVA: 0x0002B7BF File Offset: 0x000299BF
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x0002B7CD File Offset: 0x000299CD
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x0002B7DF File Offset: 0x000299DF
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x0002B7EC File Offset: 0x000299EC
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x0002B7F9 File Offset: 0x000299F9
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0002B806 File Offset: 0x00029A06
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0002B818 File Offset: 0x00029A18
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x0002B825 File Offset: 0x00029A25
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x0002B832 File Offset: 0x00029A32
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x0002B840 File Offset: 0x00029A40
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x0002B84D File Offset: 0x00029A4D
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x0002B85A File Offset: 0x00029A5A
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x0002B867 File Offset: 0x00029A67
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0002B874 File Offset: 0x00029A74
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x0002B881 File Offset: 0x00029A81
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0002B88E File Offset: 0x00029A8E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		[MoonSharpHidden]
		public static AirplaneProxy New(Airplane value)
		{
			if (value == null)
			{
				return null;
			}
			AirplaneProxy airplaneProxy = (AirplaneProxy)ObjectCache.Get(typeof(AirplaneProxy), value);
			if (airplaneProxy == null)
			{
				airplaneProxy = new AirplaneProxy(value);
				ObjectCache.Add(typeof(AirplaneProxy), value, airplaneProxy);
			}
			return airplaneProxy;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x0002B896 File Offset: 0x00029A96
		[MoonSharpUserDataMetamethod("__call")]
		public static AirplaneProxy Call(DynValue _)
		{
			return new AirplaneProxy();
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0012FC28 File Offset: 0x0012DE28
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x0002B89D File Offset: 0x00029A9D
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x0002B8BD File Offset: 0x00029ABD
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x0002B8CB File Offset: 0x00029ACB
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003146 RID: 12614
		[MoonSharpHidden]
		public Airplane _value;
	}
}

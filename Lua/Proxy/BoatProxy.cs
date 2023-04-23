using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B4 RID: 2484
	[Proxy(typeof(Boat))]
	public class BoatProxy : IProxy
	{
		// Token: 0x060041E3 RID: 16867 RVA: 0x0002CF9A File Offset: 0x0002B19A
		[MoonSharpHidden]
		public BoatProxy(Boat value)
		{
			this._value = value;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x0002CFA9 File Offset: 0x0002B1A9
		public BoatProxy()
		{
			this._value = new Boat();
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x0002CFBC File Offset: 0x0002B1BC
		// (set) Token: 0x060041E6 RID: 16870 RVA: 0x0002CFC9 File Offset: 0x0002B1C9
		public float floatAcceleration
		{
			get
			{
				return this._value.floatAcceleration;
			}
			set
			{
				this._value.floatAcceleration = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x0002CFD7 File Offset: 0x0002B1D7
		// (set) Token: 0x060041E8 RID: 16872 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
		public float floatDepth
		{
			get
			{
				return this._value.floatDepth;
			}
			set
			{
				this._value.floatDepth = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x0002CFF2 File Offset: 0x0002B1F2
		// (set) Token: 0x060041EA RID: 16874 RVA: 0x0002CFFF File Offset: 0x0002B1FF
		public Transform[] floatingSamplers
		{
			get
			{
				return this._value.floatingSamplers;
			}
			set
			{
				this._value.floatingSamplers = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x0002D00D File Offset: 0x0002B20D
		// (set) Token: 0x060041EC RID: 16876 RVA: 0x0002D01A File Offset: 0x0002B21A
		public bool inWater
		{
			get
			{
				return this._value.inWater;
			}
			set
			{
				this._value.inWater = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x0002D028 File Offset: 0x0002B228
		// (set) Token: 0x060041EE RID: 16878 RVA: 0x0002D035 File Offset: 0x0002B235
		public bool requireDeepWater
		{
			get
			{
				return this._value.requireDeepWater;
			}
			set
			{
				this._value.requireDeepWater = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x0002D043 File Offset: 0x0002B243
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x0002D050 File Offset: 0x0002B250
		public float reverseMultiplier
		{
			get
			{
				return this._value.reverseMultiplier;
			}
			set
			{
				this._value.reverseMultiplier = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x0002D05E File Offset: 0x0002B25E
		// (set) Token: 0x060041F2 RID: 16882 RVA: 0x0002D06B File Offset: 0x0002B26B
		public float sinkingFloatAcceleration
		{
			get
			{
				return this._value.sinkingFloatAcceleration;
			}
			set
			{
				this._value.sinkingFloatAcceleration = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0002D079 File Offset: 0x0002B279
		// (set) Token: 0x060041F4 RID: 16884 RVA: 0x0002D086 File Offset: 0x0002B286
		public float sinkingMaxTorque
		{
			get
			{
				return this._value.sinkingMaxTorque;
			}
			set
			{
				this._value.sinkingMaxTorque = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x0002D094 File Offset: 0x0002B294
		// (set) Token: 0x060041F6 RID: 16886 RVA: 0x0002D0A1 File Offset: 0x0002B2A1
		public float speed
		{
			get
			{
				return this._value.speed;
			}
			set
			{
				this._value.speed = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x0002D0AF File Offset: 0x0002B2AF
		// (set) Token: 0x060041F8 RID: 16888 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		public float stability
		{
			get
			{
				return this._value.stability;
			}
			set
			{
				this._value.stability = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x0002D0CA File Offset: 0x0002B2CA
		// (set) Token: 0x060041FA RID: 16890 RVA: 0x0002D0D7 File Offset: 0x0002B2D7
		public float turnSpeed
		{
			get
			{
				return this._value.turnSpeed;
			}
			set
			{
				this._value.turnSpeed = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x0002D0E5 File Offset: 0x0002B2E5
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x0002D0F7 File Offset: 0x0002B2F7
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x0002D109 File Offset: 0x0002B309
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x0002D116 File Offset: 0x0002B316
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x0002D123 File Offset: 0x0002B323
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x0002D135 File Offset: 0x0002B335
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x0002D147 File Offset: 0x0002B347
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x0002D154 File Offset: 0x0002B354
		// (set) Token: 0x06004203 RID: 16899 RVA: 0x0002D161 File Offset: 0x0002B361
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

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x0002D16F File Offset: 0x0002B36F
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x0002D17C File Offset: 0x0002B37C
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x0002D189 File Offset: 0x0002B389
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x0002D196 File Offset: 0x0002B396
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x0002D1A3 File Offset: 0x0002B3A3
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x0002D1BD File Offset: 0x0002B3BD
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x0002D1CA File Offset: 0x0002B3CA
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x0002D1D7 File Offset: 0x0002B3D7
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
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

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x0002D1F2 File Offset: 0x0002B3F2
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x0002D204 File Offset: 0x0002B404
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x0002D211 File Offset: 0x0002B411
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x0002D21E File Offset: 0x0002B41E
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x0002D22B File Offset: 0x0002B42B
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x0002D23D File Offset: 0x0002B43D
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x0002D24A File Offset: 0x0002B44A
		// (set) Token: 0x06004215 RID: 16917 RVA: 0x0002D257 File Offset: 0x0002B457
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

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x0002D265 File Offset: 0x0002B465
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x0002D272 File Offset: 0x0002B472
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x0002D27F File Offset: 0x0002B47F
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x0002D28C File Offset: 0x0002B48C
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x0002D299 File Offset: 0x0002B499
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x0002D2A6 File Offset: 0x0002B4A6
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0002D2B3 File Offset: 0x0002B4B3
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x0012FFB8 File Offset: 0x0012E1B8
		[MoonSharpHidden]
		public static BoatProxy New(Boat value)
		{
			if (value == null)
			{
				return null;
			}
			BoatProxy boatProxy = (BoatProxy)ObjectCache.Get(typeof(BoatProxy), value);
			if (boatProxy == null)
			{
				boatProxy = new BoatProxy(value);
				ObjectCache.Add(typeof(BoatProxy), value, boatProxy);
			}
			return boatProxy;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0002D2BB File Offset: 0x0002B4BB
		[MoonSharpUserDataMetamethod("__call")]
		public static BoatProxy Call(DynValue _)
		{
			return new BoatProxy();
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x00130004 File Offset: 0x0012E204
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0002D2C2 File Offset: 0x0002B4C2
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x0002D2D5 File Offset: 0x0002B4D5
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x0002D2E2 File Offset: 0x0002B4E2
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400314D RID: 12621
		[MoonSharpHidden]
		public Boat _value;
	}
}

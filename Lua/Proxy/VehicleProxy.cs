using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A12 RID: 2578
	[Proxy(typeof(Vehicle))]
	public class VehicleProxy : IProxy
	{
		// Token: 0x06005169 RID: 20841 RVA: 0x0003C063 File Offset: 0x0003A263
		[MoonSharpHidden]
		public VehicleProxy(Vehicle value)
		{
			this._value = value;
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x0003C072 File Offset: 0x0003A272
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x0003C084 File Offset: 0x0003A284
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0003C096 File Offset: 0x0003A296
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x0003C0A3 File Offset: 0x0003A2A3
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600516E RID: 20846 RVA: 0x0003C0B0 File Offset: 0x0003A2B0
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600516F RID: 20847 RVA: 0x0003C0C2 File Offset: 0x0003A2C2
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06005170 RID: 20848 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x0003C0E1 File Offset: 0x0003A2E1
		// (set) Token: 0x06005172 RID: 20850 RVA: 0x0003C0EE File Offset: 0x0003A2EE
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

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06005173 RID: 20851 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x0003C109 File Offset: 0x0003A309
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x0003C116 File Offset: 0x0003A316
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x0003C123 File Offset: 0x0003A323
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x0003C130 File Offset: 0x0003A330
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x0003C13D File Offset: 0x0003A33D
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x0003C14A File Offset: 0x0003A34A
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x0003C157 File Offset: 0x0003A357
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0003C164 File Offset: 0x0003A364
		// (set) Token: 0x0600517C RID: 20860 RVA: 0x0003C171 File Offset: 0x0003A371
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

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x0003C17F File Offset: 0x0003A37F
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x0003C191 File Offset: 0x0003A391
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x0003C19E File Offset: 0x0003A39E
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0003C1AB File Offset: 0x0003A3AB
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x0003C1B8 File Offset: 0x0003A3B8
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x0003C1CA File Offset: 0x0003A3CA
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x0003C1D7 File Offset: 0x0003A3D7
		// (set) Token: 0x06005184 RID: 20868 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
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

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x0003C1F2 File Offset: 0x0003A3F2
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x0003C1FF File Offset: 0x0003A3FF
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x0003C20C File Offset: 0x0003A40C
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x0003C219 File Offset: 0x0003A419
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x0003C226 File Offset: 0x0003A426
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0003C233 File Offset: 0x0003A433
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0003C240 File Offset: 0x0003A440
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00138CBC File Offset: 0x00136EBC
		[MoonSharpHidden]
		public static VehicleProxy New(Vehicle value)
		{
			if (value == null)
			{
				return null;
			}
			VehicleProxy vehicleProxy = (VehicleProxy)ObjectCache.Get(typeof(VehicleProxy), value);
			if (vehicleProxy == null)
			{
				vehicleProxy = new VehicleProxy(value);
				ObjectCache.Add(typeof(VehicleProxy), value, vehicleProxy);
			}
			return vehicleProxy;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00138D08 File Offset: 0x00136F08
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0003C248 File Offset: 0x0003A448
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0003C25B File Offset: 0x0003A45B
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x0003C268 File Offset: 0x0003A468
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x0003C276 File Offset: 0x0003A476
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A1 RID: 12961
		[MoonSharpHidden]
		public Vehicle _value;
	}
}

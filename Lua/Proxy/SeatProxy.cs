using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FA RID: 2554
	[Proxy(typeof(Seat))]
	public class SeatProxy : IProxy
	{
		// Token: 0x06004E73 RID: 20083 RVA: 0x00038DA4 File Offset: 0x00036FA4
		[MoonSharpHidden]
		public SeatProxy(Seat value)
		{
			this._value = value;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x00038DB3 File Offset: 0x00036FB3
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00038DC5 File Offset: 0x00036FC5
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004E76 RID: 20086 RVA: 0x00038DD7 File Offset: 0x00036FD7
		public MountedWeaponProxy activeWeapon
		{
			get
			{
				return MountedWeaponProxy.New(WSeat.GetActiveWeapon(this._value));
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00038DE9 File Offset: 0x00036FE9
		// (set) Token: 0x06004E78 RID: 20088 RVA: 0x00038DF6 File Offset: 0x00036FF6
		public Seat.Type cameraType
		{
			get
			{
				return WSeat.GetCameraType(this._value);
			}
			set
			{
				WSeat.SetCameraType(this._value, value);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x00038E04 File Offset: 0x00037004
		public CameraProxy firstPersonCamera
		{
			get
			{
				return CameraProxy.New(WSeat.GetFirstPersonCamera(this._value));
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x00038E16 File Offset: 0x00037016
		public ActorProxy occupant
		{
			get
			{
				return ActorProxy.New(WSeat.GetOccupant(this._value));
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00038E28 File Offset: 0x00037028
		public CameraProxy thirdPersonCamera
		{
			get
			{
				return CameraProxy.New(WSeat.GetThirdPersonCamera(this._value));
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x00038E3A File Offset: 0x0003703A
		public MountedWeapon[] weapons
		{
			get
			{
				return WSeat.GetWeapons(this._value);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x00038E47 File Offset: 0x00037047
		public VehicleProxy vehicle
		{
			get
			{
				return VehicleProxy.New(WSeat.GetVehicle(this._value));
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x00038E59 File Offset: 0x00037059
		public bool hasActiveWeapon
		{
			get
			{
				return WSeat.HasActiveWeapon(this._value);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004E7F RID: 20095 RVA: 0x00038E66 File Offset: 0x00037066
		public bool hasWeapons
		{
			get
			{
				return WSeat.HasWeapons(this._value);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004E80 RID: 20096 RVA: 0x00038E73 File Offset: 0x00037073
		public bool isDriverSeat
		{
			get
			{
				return WSeat.IsDriverSeat(this._value);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x00038E80 File Offset: 0x00037080
		public bool isEnclosed
		{
			get
			{
				return WSeat.IsEnclosed(this._value);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x00038E8D File Offset: 0x0003708D
		public bool isOccupied
		{
			get
			{
				return WSeat.IsOccupied(this._value);
			}
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00038E9A File Offset: 0x0003709A
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00137E88 File Offset: 0x00136088
		[MoonSharpHidden]
		public static SeatProxy New(Seat value)
		{
			if (value == null)
			{
				return null;
			}
			SeatProxy seatProxy = (SeatProxy)ObjectCache.Get(typeof(SeatProxy), value);
			if (seatProxy == null)
			{
				seatProxy = new SeatProxy(value);
				ObjectCache.Add(typeof(SeatProxy), value, seatProxy);
			}
			return seatProxy;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00038EA2 File Offset: 0x000370A2
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003289 RID: 12937
		[MoonSharpHidden]
		public Seat _value;
	}
}

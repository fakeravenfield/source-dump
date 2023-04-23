using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009BF RID: 2495
	[Proxy(typeof(DamageInfo))]
	public class DamageInfoProxy : IProxy
	{
		// Token: 0x060043C9 RID: 17353 RVA: 0x0002EF1C File Offset: 0x0002D11C
		[MoonSharpHidden]
		public DamageInfoProxy(DamageInfo value)
		{
			this._value = value;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x00130360 File Offset: 0x0012E560
		public DamageInfoProxy(DamageInfo.DamageSourceType type, ActorProxy sourceActor, WeaponProxy sourceWeapon)
		{
			Actor sourceActor2 = null;
			if (sourceActor != null)
			{
				sourceActor2 = sourceActor._value;
			}
			Weapon sourceWeapon2 = null;
			if (sourceWeapon != null)
			{
				sourceWeapon2 = sourceWeapon._value;
			}
			this._value = new DamageInfo(type, sourceActor2, sourceWeapon2);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x0002EF2B File Offset: 0x0002D12B
		public DamageInfoProxy(DamageInfoProxy source)
		{
			if (source == null)
			{
				throw new ScriptRuntimeException("argument 'source' is nil");
			}
			this._value = new DamageInfo(source._value);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x0002EF52 File Offset: 0x0002D152
		public DamageInfoProxy()
		{
			this._value = default(DamageInfo);
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x0002EF66 File Offset: 0x0002D166
		// (set) Token: 0x060043CE RID: 17358 RVA: 0x0002EF73 File Offset: 0x0002D173
		public float balanceDamage
		{
			get
			{
				return this._value.balanceDamage;
			}
			set
			{
				this._value.balanceDamage = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x0002EF81 File Offset: 0x0002D181
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x0002EF93 File Offset: 0x0002D193
		public Vector3Proxy direction
		{
			get
			{
				return Vector3Proxy.New(this._value.direction);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.direction = value._value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x0002EFC1 File Offset: 0x0002D1C1
		public float healthDamage
		{
			get
			{
				return this._value.healthDamage;
			}
			set
			{
				this._value.healthDamage = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x0002EFCF File Offset: 0x0002D1CF
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x0002EFE1 File Offset: 0x0002D1E1
		public Vector3Proxy impactForce
		{
			get
			{
				return Vector3Proxy.New(this._value.impactForce);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.impactForce = value._value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x0002F002 File Offset: 0x0002D202
		// (set) Token: 0x060043D6 RID: 17366 RVA: 0x0002F00F File Offset: 0x0002D20F
		public bool isCriticalHit
		{
			get
			{
				return this._value.isCriticalHit;
			}
			set
			{
				this._value.isCriticalHit = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x0002F01D File Offset: 0x0002D21D
		// (set) Token: 0x060043D8 RID: 17368 RVA: 0x0002F02A File Offset: 0x0002D22A
		public bool isPiercing
		{
			get
			{
				return this._value.isPiercing;
			}
			set
			{
				this._value.isPiercing = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x0002F038 File Offset: 0x0002D238
		// (set) Token: 0x060043DA RID: 17370 RVA: 0x0002F045 File Offset: 0x0002D245
		public bool isSplashDamage
		{
			get
			{
				return this._value.isSplashDamage;
			}
			set
			{
				this._value.isSplashDamage = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x0002F053 File Offset: 0x0002D253
		// (set) Token: 0x060043DC RID: 17372 RVA: 0x0002F065 File Offset: 0x0002D265
		public Vector3Proxy point
		{
			get
			{
				return Vector3Proxy.New(this._value.point);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.point = value._value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x0002F086 File Offset: 0x0002D286
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x0013039C File Offset: 0x0012E59C
		public ActorProxy sourceActor
		{
			get
			{
				return ActorProxy.New(this._value.sourceActor);
			}
			set
			{
				Actor sourceActor = null;
				if (value != null)
				{
					sourceActor = value._value;
				}
				this._value.sourceActor = sourceActor;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x0002F098 File Offset: 0x0002D298
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x0002F0A5 File Offset: 0x0002D2A5
		public DamageInfo.DamageSourceType type
		{
			get
			{
				return this._value.type;
			}
			set
			{
				this._value.type = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x0002F0B3 File Offset: 0x0002D2B3
		public bool isPlayerSource
		{
			get
			{
				return this._value.isPlayerSource;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		public bool isScripted
		{
			get
			{
				return this._value.isScripted;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x0002F0CD File Offset: 0x0002D2CD
		// (set) Token: 0x060043E4 RID: 17380 RVA: 0x001303C4 File Offset: 0x0012E5C4
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(this._value.sourceWeapon);
			}
			set
			{
				Weapon sourceWeapon = null;
				if (value != null)
				{
					sourceWeapon = value._value;
				}
				this._value.sourceWeapon = sourceWeapon;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x0002F0DF File Offset: 0x0002D2DF
		public WeaponEntryProxy sourceWeaponEntry
		{
			get
			{
				return WeaponEntryProxy.New(this._value.sourceWeaponEntry);
			}
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x0002F0F1 File Offset: 0x0002D2F1
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x0002F0FE File Offset: 0x0002D2FE
		[MoonSharpHidden]
		public static DamageInfoProxy New(DamageInfo value)
		{
			return new DamageInfoProxy(value);
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0002F106 File Offset: 0x0002D306
		[MoonSharpUserDataMetamethod("__call")]
		public static DamageInfoProxy Call(DynValue _, DamageInfo.DamageSourceType type, ActorProxy sourceActor, WeaponProxy sourceWeapon)
		{
			return new DamageInfoProxy(type, sourceActor, sourceWeapon);
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x0002F110 File Offset: 0x0002D310
		[MoonSharpUserDataMetamethod("__call")]
		public static DamageInfoProxy Call(DynValue _, DamageInfoProxy source)
		{
			return new DamageInfoProxy(source);
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x0002F118 File Offset: 0x0002D318
		[MoonSharpUserDataMetamethod("__call")]
		public static DamageInfoProxy Call(DynValue _)
		{
			return new DamageInfoProxy();
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x0002F11F File Offset: 0x0002D31F
		public static DamageInfoProxy EvaluateLastExplosionDamage(Vector3Proxy point, bool ignoreLevelGeometry)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			return DamageInfoProxy.New(DamageInfo.EvaluateLastExplosionDamage(point._value, ignoreLevelGeometry));
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x0002F140 File Offset: 0x0002D340
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003158 RID: 12632
		[MoonSharpHidden]
		public DamageInfo _value;
	}
}

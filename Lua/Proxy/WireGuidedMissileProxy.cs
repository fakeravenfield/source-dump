using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A2E RID: 2606
	[Proxy(typeof(WireGuidedMissile))]
	public class WireGuidedMissileProxy : IProxy
	{
		// Token: 0x060053A0 RID: 21408 RVA: 0x0003DAF2 File Offset: 0x0003BCF2
		[MoonSharpHidden]
		public WireGuidedMissileProxy(WireGuidedMissile value)
		{
			this._value = value;
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x0003DB01 File Offset: 0x0003BD01
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060053A2 RID: 21410 RVA: 0x0003DB13 File Offset: 0x0003BD13
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x0003DB25 File Offset: 0x0003BD25
		// (set) Token: 0x060053A4 RID: 21412 RVA: 0x0003DB32 File Offset: 0x0003BD32
		public Vehicle.ArmorRating armorDamage
		{
			get
			{
				return WProjectile.GetArmorDamage(this._value);
			}
			set
			{
				WProjectile.SetArmorDamage(this._value, value);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060053A5 RID: 21413 RVA: 0x0003DB40 File Offset: 0x0003BD40
		// (set) Token: 0x060053A6 RID: 21414 RVA: 0x0003DB4D File Offset: 0x0003BD4D
		public float balanceDamage
		{
			get
			{
				return WProjectile.GetBalanceDamage(this._value);
			}
			set
			{
				WProjectile.SetBalanceDamage(this._value, value);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x0003DB5B File Offset: 0x0003BD5B
		// (set) Token: 0x060053A8 RID: 21416 RVA: 0x0003DB68 File Offset: 0x0003BD68
		public float damage
		{
			get
			{
				return WProjectile.GetDamage(this._value);
			}
			set
			{
				WProjectile.SetDamage(this._value, value);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0003DB76 File Offset: 0x0003BD76
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x0003DB83 File Offset: 0x0003BD83
		// (set) Token: 0x060053AB RID: 21419 RVA: 0x0003DB90 File Offset: 0x0003BD90
		public float gravityMultiplier
		{
			get
			{
				return WProjectile.GetGravityMultiplier(this._value);
			}
			set
			{
				WProjectile.SetGravityMultiplier(this._value, value);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x0003DB9E File Offset: 0x0003BD9E
		// (set) Token: 0x060053AD RID: 21421 RVA: 0x0003DBAB File Offset: 0x0003BDAB
		public float impactForce
		{
			get
			{
				return WProjectile.GetImpactForce(this._value);
			}
			set
			{
				WProjectile.SetImpactForce(this._value, value);
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x0003DBB9 File Offset: 0x0003BDB9
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x0003DBC6 File Offset: 0x0003BDC6
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x0003DBD3 File Offset: 0x0003BDD3
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x0003DBED File Offset: 0x0003BDED
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x0003DBFA File Offset: 0x0003BDFA
		// (set) Token: 0x060053B4 RID: 21428 RVA: 0x001398C0 File Offset: 0x00137AC0
		public ActorProxy killCredit
		{
			get
			{
				return ActorProxy.New(WProjectile.GetKillCredit(this._value));
			}
			set
			{
				Actor source = null;
				if (value != null)
				{
					source = value._value;
				}
				WProjectile.SetKillCredit(this._value, source);
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x0003DC0C File Offset: 0x0003BE0C
		// (set) Token: 0x060053B6 RID: 21430 RVA: 0x001398E8 File Offset: 0x00137AE8
		public ActorProxy source
		{
			get
			{
				return ActorProxy.New(WProjectile.GetSource(this._value));
			}
			set
			{
				Actor source = null;
				if (value != null)
				{
					source = value._value;
				}
				WProjectile.SetSource(this._value, source);
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060053B7 RID: 21431 RVA: 0x0003DC1E File Offset: 0x0003BE1E
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x0003DC30 File Offset: 0x0003BE30
		// (set) Token: 0x060053B9 RID: 21433 RVA: 0x0003DC42 File Offset: 0x0003BE42
		public Vector3Proxy velocity
		{
			get
			{
				return Vector3Proxy.New(WProjectile.GetVelocity(this._value));
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				WProjectile.SetVelocity(this._value, value._value);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060053BA RID: 21434 RVA: 0x0003DC63 File Offset: 0x0003BE63
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0003DC70 File Offset: 0x0003BE70
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00139910 File Offset: 0x00137B10
		[MoonSharpHidden]
		public static WireGuidedMissileProxy New(WireGuidedMissile value)
		{
			if (value == null)
			{
				return null;
			}
			WireGuidedMissileProxy wireGuidedMissileProxy = (WireGuidedMissileProxy)ObjectCache.Get(typeof(WireGuidedMissileProxy), value);
			if (wireGuidedMissileProxy == null)
			{
				wireGuidedMissileProxy = new WireGuidedMissileProxy(value);
				ObjectCache.Add(typeof(WireGuidedMissileProxy), value, wireGuidedMissileProxy);
			}
			return wireGuidedMissileProxy;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0003DC78 File Offset: 0x0003BE78
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0003DC86 File Offset: 0x0003BE86
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032AA RID: 12970
		[MoonSharpHidden]
		public WireGuidedMissile _value;
	}
}

using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009E4 RID: 2532
	[Proxy(typeof(Projectile))]
	public class ProjectileProxy : IProxy
	{
		// Token: 0x06004AA5 RID: 19109 RVA: 0x00034E19 File Offset: 0x00033019
		[MoonSharpHidden]
		public ProjectileProxy(Projectile value)
		{
			this._value = value;
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x00034E28 File Offset: 0x00033028
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x00034E3A File Offset: 0x0003303A
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00034E4C File Offset: 0x0003304C
		// (set) Token: 0x06004AA9 RID: 19113 RVA: 0x00034E59 File Offset: 0x00033059
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

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x00034E67 File Offset: 0x00033067
		// (set) Token: 0x06004AAB RID: 19115 RVA: 0x00034E74 File Offset: 0x00033074
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

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x00034E82 File Offset: 0x00033082
		// (set) Token: 0x06004AAD RID: 19117 RVA: 0x00034E8F File Offset: 0x0003308F
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

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x00034E9D File Offset: 0x0003309D
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x00034EAA File Offset: 0x000330AA
		// (set) Token: 0x06004AB0 RID: 19120 RVA: 0x00034EB7 File Offset: 0x000330B7
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x00034EC5 File Offset: 0x000330C5
		// (set) Token: 0x06004AB2 RID: 19122 RVA: 0x00034ED2 File Offset: 0x000330D2
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

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x00034EE0 File Offset: 0x000330E0
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x00034EED File Offset: 0x000330ED
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06004AB5 RID: 19125 RVA: 0x00034EFA File Offset: 0x000330FA
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x00034F07 File Offset: 0x00033107
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x00034F14 File Offset: 0x00033114
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x00034F21 File Offset: 0x00033121
		// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x00131C7C File Offset: 0x0012FE7C
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

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x00034F33 File Offset: 0x00033133
		// (set) Token: 0x06004ABB RID: 19131 RVA: 0x00131CA4 File Offset: 0x0012FEA4
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

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x00034F45 File Offset: 0x00033145
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06004ABD RID: 19133 RVA: 0x00034F57 File Offset: 0x00033157
		// (set) Token: 0x06004ABE RID: 19134 RVA: 0x00034F69 File Offset: 0x00033169
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

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x00034F8A File Offset: 0x0003318A
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00034F97 File Offset: 0x00033197
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00131CCC File Offset: 0x0012FECC
		[MoonSharpHidden]
		public static ProjectileProxy New(Projectile value)
		{
			if (value == null)
			{
				return null;
			}
			ProjectileProxy projectileProxy = (ProjectileProxy)ObjectCache.Get(typeof(ProjectileProxy), value);
			if (projectileProxy == null)
			{
				projectileProxy = new ProjectileProxy(value);
				ObjectCache.Add(typeof(ProjectileProxy), value, projectileProxy);
			}
			return projectileProxy;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00034F9F File Offset: 0x0003319F
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00034FAD File Offset: 0x000331AD
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400317C RID: 12668
		[MoonSharpHidden]
		public Projectile _value;
	}
}

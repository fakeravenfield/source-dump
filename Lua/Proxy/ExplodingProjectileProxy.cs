using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009C4 RID: 2500
	[Proxy(typeof(ExplodingProjectile))]
	public class ExplodingProjectileProxy : IProxy
	{
		// Token: 0x0600446F RID: 17519 RVA: 0x0002F82A File Offset: 0x0002DA2A
		[MoonSharpHidden]
		public ExplodingProjectileProxy(ExplodingProjectile value)
		{
			this._value = value;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x0002F839 File Offset: 0x0002DA39
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x0002F84B File Offset: 0x0002DA4B
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x0002F85D File Offset: 0x0002DA5D
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x0002F86A File Offset: 0x0002DA6A
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

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x0002F878 File Offset: 0x0002DA78
		// (set) Token: 0x06004475 RID: 17525 RVA: 0x0002F885 File Offset: 0x0002DA85
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

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x0002F893 File Offset: 0x0002DA93
		// (set) Token: 0x06004477 RID: 17527 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
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

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x0002F8AE File Offset: 0x0002DAAE
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x0002F8BB File Offset: 0x0002DABB
		// (set) Token: 0x0600447A RID: 17530 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x0002F8D6 File Offset: 0x0002DAD6
		// (set) Token: 0x0600447C RID: 17532 RVA: 0x0002F8E3 File Offset: 0x0002DAE3
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x0002F8F1 File Offset: 0x0002DAF1
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x0002F8FE File Offset: 0x0002DAFE
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x0002F90B File Offset: 0x0002DB0B
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x0002F918 File Offset: 0x0002DB18
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x0002F925 File Offset: 0x0002DB25
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x0002F932 File Offset: 0x0002DB32
		// (set) Token: 0x06004483 RID: 17539 RVA: 0x00130608 File Offset: 0x0012E808
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

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x0002F944 File Offset: 0x0002DB44
		// (set) Token: 0x06004485 RID: 17541 RVA: 0x00130630 File Offset: 0x0012E830
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

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x0002F956 File Offset: 0x0002DB56
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x0002F968 File Offset: 0x0002DB68
		// (set) Token: 0x06004488 RID: 17544 RVA: 0x0002F97A File Offset: 0x0002DB7A
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

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x0002F99B File Offset: 0x0002DB9B
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00130658 File Offset: 0x0012E858
		[MoonSharpHidden]
		public static ExplodingProjectileProxy New(ExplodingProjectile value)
		{
			if (value == null)
			{
				return null;
			}
			ExplodingProjectileProxy explodingProjectileProxy = (ExplodingProjectileProxy)ObjectCache.Get(typeof(ExplodingProjectileProxy), value);
			if (explodingProjectileProxy == null)
			{
				explodingProjectileProxy = new ExplodingProjectileProxy(value);
				ObjectCache.Add(typeof(ExplodingProjectileProxy), value, explodingProjectileProxy);
			}
			return explodingProjectileProxy;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x0002F9B0 File Offset: 0x0002DBB0
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x0002F9BE File Offset: 0x0002DBBE
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315C RID: 12636
		[MoonSharpHidden]
		public ExplodingProjectile _value;
	}
}

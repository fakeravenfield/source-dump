using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009F2 RID: 2546
	[Proxy(typeof(RigidbodyProjectile))]
	public class RigidbodyProjectileProxy : IProxy
	{
		// Token: 0x06004D84 RID: 19844 RVA: 0x00037E65 File Offset: 0x00036065
		[MoonSharpHidden]
		public RigidbodyProjectileProxy(RigidbodyProjectile value)
		{
			this._value = value;
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x00037E74 File Offset: 0x00036074
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x00037E86 File Offset: 0x00036086
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x00037E98 File Offset: 0x00036098
		// (set) Token: 0x06004D88 RID: 19848 RVA: 0x00037EA5 File Offset: 0x000360A5
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

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x00037EB3 File Offset: 0x000360B3
		// (set) Token: 0x06004D8A RID: 19850 RVA: 0x00037EC0 File Offset: 0x000360C0
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

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00037ECE File Offset: 0x000360CE
		// (set) Token: 0x06004D8C RID: 19852 RVA: 0x00037EDB File Offset: 0x000360DB
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x00037EE9 File Offset: 0x000360E9
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x00037EF6 File Offset: 0x000360F6
		// (set) Token: 0x06004D8F RID: 19855 RVA: 0x00037F03 File Offset: 0x00036103
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

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x00037F11 File Offset: 0x00036111
		// (set) Token: 0x06004D91 RID: 19857 RVA: 0x00037F1E File Offset: 0x0003611E
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

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x00037F2C File Offset: 0x0003612C
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06004D93 RID: 19859 RVA: 0x00037F39 File Offset: 0x00036139
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x00037F46 File Offset: 0x00036146
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x00037F53 File Offset: 0x00036153
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x00037F60 File Offset: 0x00036160
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06004D97 RID: 19863 RVA: 0x00037F6D File Offset: 0x0003616D
		// (set) Token: 0x06004D98 RID: 19864 RVA: 0x00137B6C File Offset: 0x00135D6C
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x00037F7F File Offset: 0x0003617F
		// (set) Token: 0x06004D9A RID: 19866 RVA: 0x00137B94 File Offset: 0x00135D94
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

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x00037F91 File Offset: 0x00036191
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x00037FA3 File Offset: 0x000361A3
		// (set) Token: 0x06004D9D RID: 19869 RVA: 0x00037FB5 File Offset: 0x000361B5
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

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06004D9E RID: 19870 RVA: 0x00037FD6 File Offset: 0x000361D6
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00037FE3 File Offset: 0x000361E3
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00137BBC File Offset: 0x00135DBC
		[MoonSharpHidden]
		public static RigidbodyProjectileProxy New(RigidbodyProjectile value)
		{
			if (value == null)
			{
				return null;
			}
			RigidbodyProjectileProxy rigidbodyProjectileProxy = (RigidbodyProjectileProxy)ObjectCache.Get(typeof(RigidbodyProjectileProxy), value);
			if (rigidbodyProjectileProxy == null)
			{
				rigidbodyProjectileProxy = new RigidbodyProjectileProxy(value);
				ObjectCache.Add(typeof(RigidbodyProjectileProxy), value, rigidbodyProjectileProxy);
			}
			return rigidbodyProjectileProxy;
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00037FEB File Offset: 0x000361EB
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00037FF9 File Offset: 0x000361F9
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003282 RID: 12930
		[MoonSharpHidden]
		public RigidbodyProjectile _value;
	}
}

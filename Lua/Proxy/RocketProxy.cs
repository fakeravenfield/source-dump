using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009F4 RID: 2548
	[Proxy(typeof(Rocket))]
	public class RocketProxy : IProxy
	{
		// Token: 0x06004DFE RID: 19966 RVA: 0x000387AE File Offset: 0x000369AE
		[MoonSharpHidden]
		public RocketProxy(Rocket value)
		{
			this._value = value;
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004DFF RID: 19967 RVA: 0x000387BD File Offset: 0x000369BD
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x000387CF File Offset: 0x000369CF
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004E01 RID: 19969 RVA: 0x000387E1 File Offset: 0x000369E1
		// (set) Token: 0x06004E02 RID: 19970 RVA: 0x000387EE File Offset: 0x000369EE
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

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x000387FC File Offset: 0x000369FC
		// (set) Token: 0x06004E04 RID: 19972 RVA: 0x00038809 File Offset: 0x00036A09
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

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00038817 File Offset: 0x00036A17
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x00038824 File Offset: 0x00036A24
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

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00038832 File Offset: 0x00036A32
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x0003883F File Offset: 0x00036A3F
		// (set) Token: 0x06004E09 RID: 19977 RVA: 0x0003884C File Offset: 0x00036A4C
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

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x0003885A File Offset: 0x00036A5A
		// (set) Token: 0x06004E0B RID: 19979 RVA: 0x00038867 File Offset: 0x00036A67
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

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x00038875 File Offset: 0x00036A75
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x00038882 File Offset: 0x00036A82
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x0003888F File Offset: 0x00036A8F
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x0003889C File Offset: 0x00036A9C
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x000388A9 File Offset: 0x00036AA9
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x000388B6 File Offset: 0x00036AB6
		// (set) Token: 0x06004E12 RID: 19986 RVA: 0x00137C54 File Offset: 0x00135E54
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

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x000388C8 File Offset: 0x00036AC8
		// (set) Token: 0x06004E14 RID: 19988 RVA: 0x00137C7C File Offset: 0x00135E7C
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

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06004E15 RID: 19989 RVA: 0x000388DA File Offset: 0x00036ADA
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x000388EC File Offset: 0x00036AEC
		// (set) Token: 0x06004E17 RID: 19991 RVA: 0x000388FE File Offset: 0x00036AFE
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

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0003891F File Offset: 0x00036B1F
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0003892C File Offset: 0x00036B2C
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00137CA4 File Offset: 0x00135EA4
		[MoonSharpHidden]
		public static RocketProxy New(Rocket value)
		{
			if (value == null)
			{
				return null;
			}
			RocketProxy rocketProxy = (RocketProxy)ObjectCache.Get(typeof(RocketProxy), value);
			if (rocketProxy == null)
			{
				rocketProxy = new RocketProxy(value);
				ObjectCache.Add(typeof(RocketProxy), value, rocketProxy);
			}
			return rocketProxy;
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00038934 File Offset: 0x00036B34
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00038942 File Offset: 0x00036B42
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003284 RID: 12932
		[MoonSharpHidden]
		public Rocket _value;
	}
}

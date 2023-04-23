using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009C9 RID: 2505
	[Proxy(typeof(GrenadeProjectile))]
	public class GrenadeProjectileProxy : IProxy
	{
		// Token: 0x060044F2 RID: 17650 RVA: 0x0002FE93 File Offset: 0x0002E093
		[MoonSharpHidden]
		public GrenadeProjectileProxy(GrenadeProjectile value)
		{
			this._value = value;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x0002FEA2 File Offset: 0x0002E0A2
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x0002FEC6 File Offset: 0x0002E0C6
		// (set) Token: 0x060044F6 RID: 17654 RVA: 0x0002FED3 File Offset: 0x0002E0D3
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x0002FEE1 File Offset: 0x0002E0E1
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x0002FEEE File Offset: 0x0002E0EE
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x0002FEFC File Offset: 0x0002E0FC
		// (set) Token: 0x060044FA RID: 17658 RVA: 0x0002FF09 File Offset: 0x0002E109
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x0002FF17 File Offset: 0x0002E117
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x0002FF24 File Offset: 0x0002E124
		// (set) Token: 0x060044FD RID: 17661 RVA: 0x0002FF31 File Offset: 0x0002E131
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x0002FF3F File Offset: 0x0002E13F
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x0002FF4C File Offset: 0x0002E14C
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x0002FF5A File Offset: 0x0002E15A
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06004501 RID: 17665 RVA: 0x0002FF67 File Offset: 0x0002E167
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x0002FF74 File Offset: 0x0002E174
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x0002FF81 File Offset: 0x0002E181
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0002FF8E File Offset: 0x0002E18E
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x0002FF9B File Offset: 0x0002E19B
		// (set) Token: 0x06004506 RID: 17670 RVA: 0x00130998 File Offset: 0x0012EB98
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

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x0002FFAD File Offset: 0x0002E1AD
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x001309C0 File Offset: 0x0012EBC0
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

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x0002FFBF File Offset: 0x0002E1BF
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x0002FFD1 File Offset: 0x0002E1D1
		// (set) Token: 0x0600450B RID: 17675 RVA: 0x0002FFE3 File Offset: 0x0002E1E3
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

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x00030004 File Offset: 0x0002E204
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00030011 File Offset: 0x0002E211
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x001309E8 File Offset: 0x0012EBE8
		[MoonSharpHidden]
		public static GrenadeProjectileProxy New(GrenadeProjectile value)
		{
			if (value == null)
			{
				return null;
			}
			GrenadeProjectileProxy grenadeProjectileProxy = (GrenadeProjectileProxy)ObjectCache.Get(typeof(GrenadeProjectileProxy), value);
			if (grenadeProjectileProxy == null)
			{
				grenadeProjectileProxy = new GrenadeProjectileProxy(value);
				ObjectCache.Add(typeof(GrenadeProjectileProxy), value, grenadeProjectileProxy);
			}
			return grenadeProjectileProxy;
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00030019 File Offset: 0x0002E219
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00030027 File Offset: 0x0002E227
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003161 RID: 12641
		[MoonSharpHidden]
		public GrenadeProjectile _value;
	}
}

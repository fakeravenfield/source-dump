using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A07 RID: 2567
	[Proxy(typeof(TargetSeekingMissile))]
	public class TargetSeekingMissileProxy : IProxy
	{
		// Token: 0x06004F99 RID: 20377 RVA: 0x00039CEC File Offset: 0x00037EEC
		[MoonSharpHidden]
		public TargetSeekingMissileProxy(TargetSeekingMissile value)
		{
			this._value = value;
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00039CFB File Offset: 0x00037EFB
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x00039D0D File Offset: 0x00037F0D
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00039D1F File Offset: 0x00037F1F
		// (set) Token: 0x06004F9D RID: 20381 RVA: 0x00039D2C File Offset: 0x00037F2C
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

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x00039D3A File Offset: 0x00037F3A
		// (set) Token: 0x06004F9F RID: 20383 RVA: 0x00039D47 File Offset: 0x00037F47
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

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x00039D55 File Offset: 0x00037F55
		// (set) Token: 0x06004FA1 RID: 20385 RVA: 0x00039D62 File Offset: 0x00037F62
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

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x00039D70 File Offset: 0x00037F70
		public float distanceTravelled
		{
			get
			{
				return WProjectile.GetDistanceTravelled(this._value);
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x00039D7D File Offset: 0x00037F7D
		// (set) Token: 0x06004FA4 RID: 20388 RVA: 0x00039D8A File Offset: 0x00037F8A
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

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x00039D98 File Offset: 0x00037F98
		// (set) Token: 0x06004FA6 RID: 20390 RVA: 0x00039DA5 File Offset: 0x00037FA5
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

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x00039DB3 File Offset: 0x00037FB3
		public bool isExplodingProjectile
		{
			get
			{
				return WProjectile.GetIsExplodingProjectile(this._value);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x00039DC0 File Offset: 0x00037FC0
		public bool isGrenadeProjectile
		{
			get
			{
				return WProjectile.GetIsGrenadeProjectile(this._value);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06004FA9 RID: 20393 RVA: 0x00039DCD File Offset: 0x00037FCD
		public bool isRigidbodyProjectile
		{
			get
			{
				return WProjectile.GetIsRigidbodyProjectile(this._value);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x00039DDA File Offset: 0x00037FDA
		public bool isRocketProjectile
		{
			get
			{
				return WProjectile.GetIsRocketProjectile(this._value);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x00039DE7 File Offset: 0x00037FE7
		public bool isTargetSeekingMissileProjectile
		{
			get
			{
				return WProjectile.GetIsTargetSeekingMissileProjectile(this._value);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x00039DF4 File Offset: 0x00037FF4
		// (set) Token: 0x06004FAD RID: 20397 RVA: 0x00138504 File Offset: 0x00136704
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

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x00039E06 File Offset: 0x00038006
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x0013852C File Offset: 0x0013672C
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

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x00039E18 File Offset: 0x00038018
		public WeaponProxy sourceWeapon
		{
			get
			{
				return WeaponProxy.New(WProjectile.GetSourceWeapon(this._value));
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x00039E2A File Offset: 0x0003802A
		// (set) Token: 0x06004FB2 RID: 20402 RVA: 0x00039E3C File Offset: 0x0003803C
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

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x00039E5D File Offset: 0x0003805D
		public bool isTravellingTowardsPlayer
		{
			get
			{
				return WProjectile.IsTravellingTowardsPlayer(this._value);
			}
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00039E6A File Offset: 0x0003806A
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00138554 File Offset: 0x00136754
		[MoonSharpHidden]
		public static TargetSeekingMissileProxy New(TargetSeekingMissile value)
		{
			if (value == null)
			{
				return null;
			}
			TargetSeekingMissileProxy targetSeekingMissileProxy = (TargetSeekingMissileProxy)ObjectCache.Get(typeof(TargetSeekingMissileProxy), value);
			if (targetSeekingMissileProxy == null)
			{
				targetSeekingMissileProxy = new TargetSeekingMissileProxy(value);
				ObjectCache.Add(typeof(TargetSeekingMissileProxy), value, targetSeekingMissileProxy);
			}
			return targetSeekingMissileProxy;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00039E72 File Offset: 0x00038072
		public void Stop(bool silent)
		{
			WProjectile.Stop(this._value, silent);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00039E80 File Offset: 0x00038080
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003296 RID: 12950
		[MoonSharpHidden]
		public TargetSeekingMissile _value;
	}
}

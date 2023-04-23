using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009C5 RID: 2501
	[Proxy(typeof(ExplodingProjectile.ExplosionConfiguration))]
	public class ExplosionConfigurationProxy : IProxy
	{
		// Token: 0x0600448E RID: 17550 RVA: 0x0002F9CB File Offset: 0x0002DBCB
		[MoonSharpHidden]
		public ExplosionConfigurationProxy(ExplodingProjectile.ExplosionConfiguration value)
		{
			this._value = value;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x0002F9DA File Offset: 0x0002DBDA
		public ExplosionConfigurationProxy()
		{
			this._value = new ExplodingProjectile.ExplosionConfiguration();
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x0002F9ED File Offset: 0x0002DBED
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x0002F9FA File Offset: 0x0002DBFA
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

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x0002FA08 File Offset: 0x0002DC08
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x001306A4 File Offset: 0x0012E8A4
		public AnimationCurveProxy balanceFalloff
		{
			get
			{
				return AnimationCurveProxy.New(this._value.balanceFalloff);
			}
			set
			{
				AnimationCurve balanceFalloff = null;
				if (value != null)
				{
					balanceFalloff = value._value;
				}
				this._value.balanceFalloff = balanceFalloff;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x0002FA1A File Offset: 0x0002DC1A
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x0002FA27 File Offset: 0x0002DC27
		public float balanceRange
		{
			get
			{
				return this._value.balanceRange;
			}
			set
			{
				this._value.balanceRange = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0002FA35 File Offset: 0x0002DC35
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x0002FA42 File Offset: 0x0002DC42
		public float damage
		{
			get
			{
				return this._value.damage;
			}
			set
			{
				this._value.damage = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x0002FA50 File Offset: 0x0002DC50
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x001306CC File Offset: 0x0012E8CC
		public AnimationCurveProxy damageFalloff
		{
			get
			{
				return AnimationCurveProxy.New(this._value.damageFalloff);
			}
			set
			{
				AnimationCurve damageFalloff = null;
				if (value != null)
				{
					damageFalloff = value._value;
				}
				this._value.damageFalloff = damageFalloff;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0002FA62 File Offset: 0x0002DC62
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x0002FA6F File Offset: 0x0002DC6F
		public float damageRange
		{
			get
			{
				return this._value.damageRange;
			}
			set
			{
				this._value.damageRange = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x0002FA7D File Offset: 0x0002DC7D
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x0002FA8A File Offset: 0x0002DC8A
		public float force
		{
			get
			{
				return this._value.force;
			}
			set
			{
				this._value.force = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x0002FA98 File Offset: 0x0002DC98
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x0002FAA5 File Offset: 0x0002DCA5
		public float infantryDamageMultiplier
		{
			get
			{
				return this._value.infantryDamageMultiplier;
			}
			set
			{
				this._value.infantryDamageMultiplier = value;
			}
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x0002FAB3 File Offset: 0x0002DCB3
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x001306F4 File Offset: 0x0012E8F4
		[MoonSharpHidden]
		public static ExplosionConfigurationProxy New(ExplodingProjectile.ExplosionConfiguration value)
		{
			if (value == null)
			{
				return null;
			}
			ExplosionConfigurationProxy explosionConfigurationProxy = (ExplosionConfigurationProxy)ObjectCache.Get(typeof(ExplosionConfigurationProxy), value);
			if (explosionConfigurationProxy == null)
			{
				explosionConfigurationProxy = new ExplosionConfigurationProxy(value);
				ObjectCache.Add(typeof(ExplosionConfigurationProxy), value, explosionConfigurationProxy);
			}
			return explosionConfigurationProxy;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x0002FABB File Offset: 0x0002DCBB
		[MoonSharpUserDataMetamethod("__call")]
		public static ExplosionConfigurationProxy Call(DynValue _)
		{
			return new ExplosionConfigurationProxy();
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x0002FAC2 File Offset: 0x0002DCC2
		public ExplosionConfigurationProxy CreateLinearFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			return ExplosionConfigurationProxy.New(this._value.CreateLinearFalloff(damage, damageRange, balanceDamage, balanceDamageRange, force));
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x0002FADB File Offset: 0x0002DCDB
		public ExplosionConfigurationProxy CreateSharpFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			return ExplosionConfigurationProxy.New(this._value.CreateSharpFalloff(damage, damageRange, balanceDamage, balanceDamageRange, force));
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public ExplosionConfigurationProxy CreateSmoothStepFalloff(float damage, float damageRange, float balanceDamage, float balanceDamageRange, float force)
		{
			return ExplosionConfigurationProxy.New(this._value.CreateSmoothStepFalloff(damage, damageRange, balanceDamage, balanceDamageRange, force));
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x0002FB0D File Offset: 0x0002DD0D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315D RID: 12637
		[MoonSharpHidden]
		public ExplodingProjectile.ExplosionConfiguration _value;
	}
}

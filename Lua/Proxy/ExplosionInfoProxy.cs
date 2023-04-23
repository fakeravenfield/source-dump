using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009C6 RID: 2502
	[Proxy(typeof(ExplosionInfo))]
	public class ExplosionInfoProxy : IProxy
	{
		// Token: 0x060044A7 RID: 17575 RVA: 0x0002FB1A File Offset: 0x0002DD1A
		[MoonSharpHidden]
		public ExplosionInfoProxy(ExplosionInfo value)
		{
			this._value = value;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0002FB29 File Offset: 0x0002DD29
		public ExplosionInfoProxy()
		{
			this._value = default(ExplosionInfo);
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x0002FB3D File Offset: 0x0002DD3D
		// (set) Token: 0x060044AA RID: 17578 RVA: 0x00130738 File Offset: 0x0012E938
		public ExplosionConfigurationProxy configuration
		{
			get
			{
				return ExplosionConfigurationProxy.New(this._value.configuration);
			}
			set
			{
				ExplodingProjectile.ExplosionConfiguration configuration = null;
				if (value != null)
				{
					configuration = value._value;
				}
				this._value.configuration = configuration;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x0002FB4F File Offset: 0x0002DD4F
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x0002FB5C File Offset: 0x0002DD5C
		public Vehicle.ArmorRating damageRating
		{
			get
			{
				return this._value.damageRating;
			}
			set
			{
				this._value.damageRating = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x0002FB6A File Offset: 0x0002DD6A
		// (set) Token: 0x060044AE RID: 17582 RVA: 0x0002FB7C File Offset: 0x0002DD7C
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

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x0002FB9D File Offset: 0x0002DD9D
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x00130760 File Offset: 0x0012E960
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

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x0002FBAF File Offset: 0x0002DDAF
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x00130788 File Offset: 0x0012E988
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x0002FBC1 File Offset: 0x0002DDC1
		public WeaponEntryProxy sourceWeaponEntry
		{
			get
			{
				return WeaponEntryProxy.New(this._value.sourceWeaponEntry);
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0002FBD3 File Offset: 0x0002DDD3
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
		[MoonSharpHidden]
		public static ExplosionInfoProxy New(ExplosionInfo value)
		{
			return new ExplosionInfoProxy(value);
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0002FBE8 File Offset: 0x0002DDE8
		[MoonSharpUserDataMetamethod("__call")]
		public static ExplosionInfoProxy Call(DynValue _)
		{
			return new ExplosionInfoProxy();
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x001307B0 File Offset: 0x0012E9B0
		public static ExplosionInfoProxy Create(Vector3Proxy point, ActorProxy sourceActor, WeaponProxy sourceWeapon, Vehicle.ArmorRating damageRating, ExplosionConfigurationProxy configuration)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
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
			ExplodingProjectile.ExplosionConfiguration configuration2 = null;
			if (configuration != null)
			{
				configuration2 = configuration._value;
			}
			return ExplosionInfoProxy.New(ExplosionInfo.Create(point._value, sourceActor2, sourceWeapon2, damageRating, configuration2));
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x0002FBEF File Offset: 0x0002DDEF
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315E RID: 12638
		[MoonSharpHidden]
		public ExplosionInfo _value;
	}
}

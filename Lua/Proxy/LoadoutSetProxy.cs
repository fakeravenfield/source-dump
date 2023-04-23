using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009D3 RID: 2515
	[Proxy(typeof(WeaponManager.LoadoutSet))]
	public class LoadoutSetProxy : IProxy
	{
		// Token: 0x060046E0 RID: 18144 RVA: 0x00031837 File Offset: 0x0002FA37
		[MoonSharpHidden]
		public LoadoutSetProxy(WeaponManager.LoadoutSet value)
		{
			this._value = value;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00031846 File Offset: 0x0002FA46
		public LoadoutSetProxy()
		{
			this._value = new WeaponManager.LoadoutSet();
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x00031859 File Offset: 0x0002FA59
		// (set) Token: 0x060046E3 RID: 18147 RVA: 0x00131034 File Offset: 0x0012F234
		public WeaponEntryProxy gear1
		{
			get
			{
				return WeaponEntryProxy.New(this._value.gear1);
			}
			set
			{
				WeaponManager.WeaponEntry gear = null;
				if (value != null)
				{
					gear = value._value;
				}
				this._value.gear1 = gear;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x0003186B File Offset: 0x0002FA6B
		// (set) Token: 0x060046E5 RID: 18149 RVA: 0x0013105C File Offset: 0x0012F25C
		public WeaponEntryProxy gear2
		{
			get
			{
				return WeaponEntryProxy.New(this._value.gear2);
			}
			set
			{
				WeaponManager.WeaponEntry gear = null;
				if (value != null)
				{
					gear = value._value;
				}
				this._value.gear2 = gear;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x0003187D File Offset: 0x0002FA7D
		// (set) Token: 0x060046E7 RID: 18151 RVA: 0x00131084 File Offset: 0x0012F284
		public WeaponEntryProxy gear3
		{
			get
			{
				return WeaponEntryProxy.New(this._value.gear3);
			}
			set
			{
				WeaponManager.WeaponEntry gear = null;
				if (value != null)
				{
					gear = value._value;
				}
				this._value.gear3 = gear;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x0003188F File Offset: 0x0002FA8F
		// (set) Token: 0x060046E9 RID: 18153 RVA: 0x001310AC File Offset: 0x0012F2AC
		public WeaponEntryProxy primary
		{
			get
			{
				return WeaponEntryProxy.New(this._value.primary);
			}
			set
			{
				WeaponManager.WeaponEntry primary = null;
				if (value != null)
				{
					primary = value._value;
				}
				this._value.primary = primary;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x000318A1 File Offset: 0x0002FAA1
		// (set) Token: 0x060046EB RID: 18155 RVA: 0x001310D4 File Offset: 0x0012F2D4
		public WeaponEntryProxy secondary
		{
			get
			{
				return WeaponEntryProxy.New(this._value.secondary);
			}
			set
			{
				WeaponManager.WeaponEntry secondary = null;
				if (value != null)
				{
					secondary = value._value;
				}
				this._value.secondary = secondary;
			}
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x000318B3 File Offset: 0x0002FAB3
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x001310FC File Offset: 0x0012F2FC
		[MoonSharpHidden]
		public static LoadoutSetProxy New(WeaponManager.LoadoutSet value)
		{
			if (value == null)
			{
				return null;
			}
			LoadoutSetProxy loadoutSetProxy = (LoadoutSetProxy)ObjectCache.Get(typeof(LoadoutSetProxy), value);
			if (loadoutSetProxy == null)
			{
				loadoutSetProxy = new LoadoutSetProxy(value);
				ObjectCache.Add(typeof(LoadoutSetProxy), value, loadoutSetProxy);
			}
			return loadoutSetProxy;
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x000318BB File Offset: 0x0002FABB
		[MoonSharpUserDataMetamethod("__call")]
		public static LoadoutSetProxy Call(DynValue _)
		{
			return new LoadoutSetProxy();
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x000318C2 File Offset: 0x0002FAC2
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400316B RID: 12651
		[MoonSharpHidden]
		public WeaponManager.LoadoutSet _value;
	}
}

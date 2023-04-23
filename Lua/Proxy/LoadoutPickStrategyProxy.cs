using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009D2 RID: 2514
	[Proxy(typeof(AiActorController.LoadoutPickStrategy))]
	public class LoadoutPickStrategyProxy : IProxy
	{
		// Token: 0x060046D4 RID: 18132 RVA: 0x000317A5 File Offset: 0x0002F9A5
		[MoonSharpHidden]
		public LoadoutPickStrategyProxy(AiActorController.LoadoutPickStrategy value)
		{
			this._value = value;
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x000317B4 File Offset: 0x0002F9B4
		public LoadoutPickStrategyProxy()
		{
			this._value = new AiActorController.LoadoutPickStrategy();
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000317C7 File Offset: 0x0002F9C7
		public LoadoutPickStrategyProxy(WeaponManager.WeaponEntry.LoadoutType type, WeaponManager.WeaponEntry.Distance distance)
		{
			this._value = new AiActorController.LoadoutPickStrategy(type, distance);
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x000317DC File Offset: 0x0002F9DC
		// (set) Token: 0x060046D8 RID: 18136 RVA: 0x000317E9 File Offset: 0x0002F9E9
		public WeaponManager.WeaponEntry.Distance distance
		{
			get
			{
				return this._value.distance;
			}
			set
			{
				this._value.distance = value;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x000317F7 File Offset: 0x0002F9F7
		// (set) Token: 0x060046DA RID: 18138 RVA: 0x00031804 File Offset: 0x0002FA04
		public WeaponManager.WeaponEntry.LoadoutType type
		{
			get
			{
				return this._value.type;
			}
			set
			{
				this._value.type = value;
			}
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00031812 File Offset: 0x0002FA12
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00130FF0 File Offset: 0x0012F1F0
		[MoonSharpHidden]
		public static LoadoutPickStrategyProxy New(AiActorController.LoadoutPickStrategy value)
		{
			if (value == null)
			{
				return null;
			}
			LoadoutPickStrategyProxy loadoutPickStrategyProxy = (LoadoutPickStrategyProxy)ObjectCache.Get(typeof(LoadoutPickStrategyProxy), value);
			if (loadoutPickStrategyProxy == null)
			{
				loadoutPickStrategyProxy = new LoadoutPickStrategyProxy(value);
				ObjectCache.Add(typeof(LoadoutPickStrategyProxy), value, loadoutPickStrategyProxy);
			}
			return loadoutPickStrategyProxy;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x0003181A File Offset: 0x0002FA1A
		[MoonSharpUserDataMetamethod("__call")]
		public static LoadoutPickStrategyProxy Call(DynValue _)
		{
			return new LoadoutPickStrategyProxy();
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00031821 File Offset: 0x0002FA21
		[MoonSharpUserDataMetamethod("__call")]
		public static LoadoutPickStrategyProxy Call(DynValue _, WeaponManager.WeaponEntry.LoadoutType type, WeaponManager.WeaponEntry.Distance distance)
		{
			return new LoadoutPickStrategyProxy(type, distance);
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x0003182A File Offset: 0x0002FA2A
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400316A RID: 12650
		[MoonSharpHidden]
		public AiActorController.LoadoutPickStrategy _value;
	}
}

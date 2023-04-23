using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A2B RID: 2603
	[Proxy(typeof(WWeaponManager))]
	public class WWeaponManagerProxy : IProxy
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0003D304 File Offset: 0x0003B504
		public static List<WeaponManager.WeaponEntry> allWeapons
		{
			get
			{
				return WWeaponManager.allWeapons;
			}
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x001396A8 File Offset: 0x001378A8
		public static WeaponEntryProxy GetAiWeaponAllGear(LoadoutPickStrategyProxy strategy, WTeam team)
		{
			AiActorController.LoadoutPickStrategy strategy2 = null;
			if (strategy != null)
			{
				strategy2 = strategy._value;
			}
			return WeaponEntryProxy.New(WWeaponManager.GetAiWeaponAllGear(strategy2, team));
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x001396D0 File Offset: 0x001378D0
		public static WeaponEntryProxy GetAiWeaponLargeGear(LoadoutPickStrategyProxy strategy, WTeam team)
		{
			AiActorController.LoadoutPickStrategy strategy2 = null;
			if (strategy != null)
			{
				strategy2 = strategy._value;
			}
			return WeaponEntryProxy.New(WWeaponManager.GetAiWeaponLargeGear(strategy2, team));
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x001396F8 File Offset: 0x001378F8
		public static WeaponEntryProxy GetAiWeaponPrimary(LoadoutPickStrategyProxy strategy, WTeam team)
		{
			AiActorController.LoadoutPickStrategy strategy2 = null;
			if (strategy != null)
			{
				strategy2 = strategy._value;
			}
			return WeaponEntryProxy.New(WWeaponManager.GetAiWeaponPrimary(strategy2, team));
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x00139720 File Offset: 0x00137920
		public static WeaponEntryProxy GetAiWeaponSecondary(LoadoutPickStrategyProxy strategy, WTeam team)
		{
			AiActorController.LoadoutPickStrategy strategy2 = null;
			if (strategy != null)
			{
				strategy2 = strategy._value;
			}
			return WeaponEntryProxy.New(WWeaponManager.GetAiWeaponSecondary(strategy2, team));
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00139748 File Offset: 0x00137948
		public static WeaponEntryProxy GetAiWeaponSmallGear(LoadoutPickStrategyProxy strategy, WTeam team)
		{
			AiActorController.LoadoutPickStrategy strategy2 = null;
			if (strategy != null)
			{
				strategy2 = strategy._value;
			}
			return WeaponEntryProxy.New(WWeaponManager.GetAiWeaponSmallGear(strategy2, team));
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x00139770 File Offset: 0x00137970
		public static bool IsWeaponAvailableToTeam(WeaponEntryProxy entry, WTeam team)
		{
			WeaponManager.WeaponEntry entry2 = null;
			if (entry != null)
			{
				entry2 = entry._value;
			}
			return WWeaponManager.IsWeaponAvailableToTeam(entry2, team);
		}
	}
}

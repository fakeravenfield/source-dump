using System;
using System.Collections.Generic;

namespace Lua.Wrapper
{
	// Token: 0x020009A4 RID: 2468
	[Name("WeaponManager")]
	public static class WWeaponManager
	{
		// Token: 0x06003F3D RID: 16189 RVA: 0x0002AB76 File Offset: 0x00028D76
		[Doc("Picks a primary weapon based on the loadout pick strategy.[..] Only picks weapons that are available to the AI and are available for the specified team.")]
		public static WeaponManager.WeaponEntry GetAiWeaponPrimary(AiActorController.LoadoutPickStrategy strategy, WTeam team)
		{
			return WeaponManager.GetAiWeaponPrimary(strategy, (int)team);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0002AB7F File Offset: 0x00028D7F
		[Doc("Picks a secondary weapon based on the loadout pick strategy.[..] Only picks weapons that are available to the AI and are available for the specified team.")]
		public static WeaponManager.WeaponEntry GetAiWeaponSecondary(AiActorController.LoadoutPickStrategy strategy, WTeam team)
		{
			return WeaponManager.GetAiWeaponSecondary(strategy, (int)team);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0002AB88 File Offset: 0x00028D88
		[Doc("Picks a gear item based on the loadout pick strategy.[..] Only picks weapons that are available to the AI and are available for the specified team.")]
		public static WeaponManager.WeaponEntry GetAiWeaponAllGear(AiActorController.LoadoutPickStrategy strategy, WTeam team)
		{
			return WeaponManager.GetAiWeaponAllGear(strategy, (int)team);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x0002AB91 File Offset: 0x00028D91
		[Doc("Picks a large gear item based on the loadout pick strategy.[..] Only picks weapons that are available to the AI and are available for the specified team.")]
		public static WeaponManager.WeaponEntry GetAiWeaponLargeGear(AiActorController.LoadoutPickStrategy strategy, WTeam team)
		{
			return WeaponManager.GetAiWeaponLargeGear(strategy, (int)team);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0002AB9A File Offset: 0x00028D9A
		[Doc("Picks a small gear item based on the loadout pick strategy.[..] Only picks weapons that are available to the AI and are available for the specified team.")]
		public static WeaponManager.WeaponEntry GetAiWeaponSmallGear(AiActorController.LoadoutPickStrategy strategy, WTeam team)
		{
			return WeaponManager.GetAiWeaponSmallGear(strategy, (int)team);
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x0002ABA3 File Offset: 0x00028DA3
		[Doc("All weapons loaded into the game.")]
		public static List<WeaponManager.WeaponEntry> allWeapons
		{
			get
			{
				return WeaponManager.instance.allWeapons;
			}
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0002ABAF File Offset: 0x00028DAF
		public static bool IsWeaponAvailableToTeam(WeaponManager.WeaponEntry entry, WTeam team)
		{
			return GameManager.instance.gameInfo.team[(int)team].IsWeaponEntryAvailable(entry);
		}
	}
}

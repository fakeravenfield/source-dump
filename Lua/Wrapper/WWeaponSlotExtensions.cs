using System;

namespace Lua.Wrapper
{
	// Token: 0x020009A6 RID: 2470
	public static class WWeaponSlotExtensions
	{
		// Token: 0x06003F44 RID: 16196 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static WeaponManager.WeaponSlot ToWeaponSlot(this WWeaponSlot self)
		{
			return (WeaponManager.WeaponSlot)self;
		}
	}
}

using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x0200074E RID: 1870
	public class AllVehiclesWithinProtectRange : WithinProtectRange<MeoVehicleSpawner>
	{
		// Token: 0x06002E94 RID: 11924 RVA: 0x00020048 File Offset: 0x0001E248
		public AllVehiclesWithinProtectRange() : base("Some vehicle spawners are not inside the protection ring of any capture point")
		{
		}
	}
}

using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x0200074F RID: 1871
	public class AllTurretsWithinProtectRange : WithinProtectRange<MeoTurretSpawner>
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x00020055 File Offset: 0x0001E255
		public AllTurretsWithinProtectRange() : base("Some turrent spawners are not inside the protection ring of any capture point")
		{
		}
	}
}

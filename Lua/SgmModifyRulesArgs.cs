using System;
using Lua.Wrapper;

namespace Lua
{
	// Token: 0x02000949 RID: 2377
	public class SgmModifyRulesArgs
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00028C40 File Offset: 0x00026E40
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x00028C48 File Offset: 0x00026E48
		public int difficulty { get; private set; }

		// Token: 0x06003C3E RID: 15422 RVA: 0x00028C51 File Offset: 0x00026E51
		[Ignore]
		public SgmModifyRulesArgs()
		{
			this.difficulty = Options.GetDropdown(OptionDropdown.Id.Difficulty);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00028C65 File Offset: 0x00026E65
		[Ignore]
		public void CopyFrom(GameModeParameters other)
		{
			this.noVehicles = other.noVehicles;
			this.noTurrets = other.noTurrets;
			this.playerTeam = (WTeam)other.playerTeam;
			this.nightMode = other.nightMode;
			this.balance = other.balance;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00028CA3 File Offset: 0x00026EA3
		[Ignore]
		public void CopyTo(GameModeParameters other)
		{
			other.noVehicles = this.noVehicles;
			other.noTurrets = this.noTurrets;
			other.playerTeam = (int)this.playerTeam;
			other.nightMode = this.nightMode;
			other.balance = this.balance;
		}

		// Token: 0x040030EE RID: 12526
		public bool noVehicles;

		// Token: 0x040030EF RID: 12527
		public bool noTurrets;

		// Token: 0x040030F0 RID: 12528
		public WTeam playerTeam;

		// Token: 0x040030F1 RID: 12529
		public bool nightMode;

		// Token: 0x040030F2 RID: 12530
		public float balance;

		// Token: 0x040030F4 RID: 12532
		public bool spawnDeadActors;
	}
}

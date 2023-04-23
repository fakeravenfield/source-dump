using System;
using System.Collections.Generic;
using Lua.Wrapper;

namespace Lua
{
	// Token: 0x02000948 RID: 2376
	public class SgmSpawnActorsArgs
	{
		// Token: 0x06003C3B RID: 15419 RVA: 0x00028C2A File Offset: 0x00026E2A
		[Ignore]
		public SgmSpawnActorsArgs(IList<Actor> actorsToSpawn, int team)
		{
			this.actorsToSpawn = actorsToSpawn;
			this.team = (WTeam)team;
		}

		// Token: 0x040030EC RID: 12524
		public IList<Actor> actorsToSpawn;

		// Token: 0x040030ED RID: 12525
		public WTeam team;
	}
}

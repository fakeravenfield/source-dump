using System;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000946 RID: 2374
	public class SgmActorDiedArgs
	{
		// Token: 0x06003C39 RID: 15417 RVA: 0x00028BFE File Offset: 0x00026DFE
		[Ignore]
		public SgmActorDiedArgs(Actor actor, Vector3 position, bool silentKill)
		{
			this.actor = actor;
			this.position = position;
			this.silentKill = silentKill;
		}

		// Token: 0x040030E8 RID: 12520
		public Actor actor;

		// Token: 0x040030E9 RID: 12521
		public Vector3 position;

		// Token: 0x040030EA RID: 12522
		public bool silentKill;
	}
}

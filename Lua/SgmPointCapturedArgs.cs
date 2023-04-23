using System;
using Lua.Wrapper;

namespace Lua
{
	// Token: 0x02000945 RID: 2373
	public class SgmPointCapturedArgs
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x00028BD5 File Offset: 0x00026DD5
		[Ignore]
		public SgmPointCapturedArgs(CapturePoint capturePoint, int oldOwner, bool initialOwner)
		{
			this.capturePoint = capturePoint;
			this.newOwner = (WTeam)capturePoint.owner;
			this.oldOwner = (WTeam)oldOwner;
			this.initialOwner = initialOwner;
		}

		// Token: 0x040030E4 RID: 12516
		public CapturePoint capturePoint;

		// Token: 0x040030E5 RID: 12517
		public WTeam newOwner;

		// Token: 0x040030E6 RID: 12518
		public WTeam oldOwner;

		// Token: 0x040030E7 RID: 12519
		public bool initialOwner;
	}
}

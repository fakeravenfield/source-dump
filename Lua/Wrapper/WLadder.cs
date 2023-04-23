using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000970 RID: 2416
	[Wrapper(typeof(Ladder))]
	public static class WLadder
	{
		// Token: 0x06003D92 RID: 15762 RVA: 0x00029A9F File Offset: 0x00027C9F
		[Getter]
		public static float GetHeight(Ladder self)
		{
			return self.height;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x00029AA7 File Offset: 0x00027CA7
		[Getter]
		[Doc("Returns the top position of the ladder in world space")]
		public static Vector3 GetTopPosition(Ladder self)
		{
			return self.GetTopPosition();
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x00029AAF File Offset: 0x00027CAF
		[Getter]
		[Doc("Returns the bottom position of the ladder in world space")]
		public static Vector3 GetBottomPosition(Ladder self)
		{
			return self.GetBottomPosition();
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x00029AB7 File Offset: 0x00027CB7
		[Getter]
		[Doc("Returns the top exit position of the ladder in world space")]
		public static Vector3 GetTopExitPosition(Ladder self)
		{
			return self.GetTopExitPosition();
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00029ABF File Offset: 0x00027CBF
		[Getter]
		[Doc("Returns the bottom exit position of the ladder in world space")]
		public static Vector3 GetBottomExitPosition(Ladder self)
		{
			return self.GetBottomExitPosition();
		}
	}
}

using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200094C RID: 2380
	[Wrapper(typeof(AudioClip))]
	public static class WAudioClip
	{
		// Token: 0x06003C4F RID: 15439 RVA: 0x00028E02 File Offset: 0x00027002
		[Getter]
		public static float GetLength(AudioClip self)
		{
			return self.length;
		}
	}
}

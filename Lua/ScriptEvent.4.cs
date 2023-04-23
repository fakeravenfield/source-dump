using System;

namespace Lua
{
	// Token: 0x0200093B RID: 2363
	public class ScriptEvent<T0, T1, T2> : ScriptEvent
	{
		// Token: 0x06003C06 RID: 15366 RVA: 0x000288F8 File Offset: 0x00026AF8
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			base.UnsafeInvoke(new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}
	}
}

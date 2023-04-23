using System;

namespace Lua
{
	// Token: 0x0200093A RID: 2362
	public class ScriptEvent<T0, T1> : ScriptEvent
	{
		// Token: 0x06003C04 RID: 15364 RVA: 0x000288D8 File Offset: 0x00026AD8
		public void Invoke(T0 arg0, T1 arg1)
		{
			base.UnsafeInvoke(new object[]
			{
				arg0,
				arg1
			});
		}
	}
}

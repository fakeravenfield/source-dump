using System;

namespace Lua
{
	// Token: 0x02000939 RID: 2361
	public class ScriptEvent<T0> : ScriptEvent
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x000288B9 File Offset: 0x00026AB9
		public void Invoke(T0 arg0)
		{
			base.UnsafeInvoke(new object[]
			{
				arg0
			});
		}
	}
}

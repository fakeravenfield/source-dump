using System;

namespace Lua.Wrapper
{
	// Token: 0x020009A0 RID: 2464
	[Name("WaitForSeconds")]
	public class WWaitForSeconds
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x0002A57E File Offset: 0x0002877E
		public WWaitForSeconds(float seconds)
		{
			this.seconds = seconds;
		}

		// Token: 0x0400313C RID: 12604
		public float seconds;
	}
}

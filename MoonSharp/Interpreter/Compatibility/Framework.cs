using System;
using MoonSharp.Interpreter.Compatibility.Frameworks;

namespace MoonSharp.Interpreter.Compatibility
{
	// Token: 0x020008FA RID: 2298
	public static class Framework
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x00027728 File Offset: 0x00025928
		public static FrameworkBase Do
		{
			get
			{
				return Framework.s_FrameworkCurrent;
			}
		}

		// Token: 0x04003037 RID: 12343
		private static FrameworkCurrent s_FrameworkCurrent = new FrameworkCurrent();
	}
}

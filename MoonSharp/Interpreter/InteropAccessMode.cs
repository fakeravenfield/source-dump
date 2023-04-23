using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CF RID: 1999
	public enum InteropAccessMode
	{
		// Token: 0x04002C40 RID: 11328
		Reflection,
		// Token: 0x04002C41 RID: 11329
		LazyOptimized,
		// Token: 0x04002C42 RID: 11330
		Preoptimized,
		// Token: 0x04002C43 RID: 11331
		BackgroundOptimized,
		// Token: 0x04002C44 RID: 11332
		Hardwired,
		// Token: 0x04002C45 RID: 11333
		HideMembers,
		// Token: 0x04002C46 RID: 11334
		NoReflectionAllowed,
		// Token: 0x04002C47 RID: 11335
		Default
	}
}

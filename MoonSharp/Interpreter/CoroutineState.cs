using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007AE RID: 1966
	public enum CoroutineState
	{
		// Token: 0x04002BD7 RID: 11223
		Main,
		// Token: 0x04002BD8 RID: 11224
		NotStarted,
		// Token: 0x04002BD9 RID: 11225
		Suspended,
		// Token: 0x04002BDA RID: 11226
		ForceSuspended,
		// Token: 0x04002BDB RID: 11227
		Running,
		// Token: 0x04002BDC RID: 11228
		Dead
	}
}

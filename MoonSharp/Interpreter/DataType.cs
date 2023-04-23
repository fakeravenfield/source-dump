using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007AF RID: 1967
	public enum DataType
	{
		// Token: 0x04002BDE RID: 11230
		Nil,
		// Token: 0x04002BDF RID: 11231
		Void,
		// Token: 0x04002BE0 RID: 11232
		Boolean,
		// Token: 0x04002BE1 RID: 11233
		Number,
		// Token: 0x04002BE2 RID: 11234
		String,
		// Token: 0x04002BE3 RID: 11235
		Function,
		// Token: 0x04002BE4 RID: 11236
		Table,
		// Token: 0x04002BE5 RID: 11237
		Tuple,
		// Token: 0x04002BE6 RID: 11238
		UserData,
		// Token: 0x04002BE7 RID: 11239
		Thread,
		// Token: 0x04002BE8 RID: 11240
		ClrFunction,
		// Token: 0x04002BE9 RID: 11241
		TailCallRequest,
		// Token: 0x04002BEA RID: 11242
		YieldRequest
	}
}

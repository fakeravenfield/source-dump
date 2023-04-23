using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007BE RID: 1982
	[Flags]
	public enum TypeValidationFlags
	{
		// Token: 0x04002C20 RID: 11296
		None = 0,
		// Token: 0x04002C21 RID: 11297
		AllowNil = 1,
		// Token: 0x04002C22 RID: 11298
		AutoConvert = 2,
		// Token: 0x04002C23 RID: 11299
		Default = 2
	}
}

using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007D7 RID: 2007
	[Flags]
	public enum CoreModules
	{
		// Token: 0x04002C53 RID: 11347
		None = 0,
		// Token: 0x04002C54 RID: 11348
		Basic = 64,
		// Token: 0x04002C55 RID: 11349
		GlobalConsts = 1,
		// Token: 0x04002C56 RID: 11350
		TableIterators = 2,
		// Token: 0x04002C57 RID: 11351
		Metatables = 4,
		// Token: 0x04002C58 RID: 11352
		String = 8,
		// Token: 0x04002C59 RID: 11353
		LoadMethods = 16,
		// Token: 0x04002C5A RID: 11354
		Table = 32,
		// Token: 0x04002C5B RID: 11355
		ErrorHandling = 128,
		// Token: 0x04002C5C RID: 11356
		Math = 256,
		// Token: 0x04002C5D RID: 11357
		Coroutine = 512,
		// Token: 0x04002C5E RID: 11358
		Bit32 = 1024,
		// Token: 0x04002C5F RID: 11359
		OS_Time = 2048,
		// Token: 0x04002C60 RID: 11360
		OS_System = 4096,
		// Token: 0x04002C61 RID: 11361
		IO = 8192,
		// Token: 0x04002C62 RID: 11362
		Debug = 16384,
		// Token: 0x04002C63 RID: 11363
		Dynamic = 32768,
		// Token: 0x04002C64 RID: 11364
		Json = 65536,
		// Token: 0x04002C65 RID: 11365
		Preset_HardSandbox = 1387,
		// Token: 0x04002C66 RID: 11366
		Preset_SoftSandbox = 102383,
		// Token: 0x04002C67 RID: 11367
		Preset_Default = 114687,
		// Token: 0x04002C68 RID: 11368
		Preset_Complete = 131071,
		// Token: 0x04002C69 RID: 11369
		Ravenfield_Sandbox = 2031
	}
}

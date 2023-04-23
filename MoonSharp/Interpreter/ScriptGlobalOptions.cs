using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007E2 RID: 2018
	public class ScriptGlobalOptions
	{
		// Token: 0x0600324E RID: 12878 RVA: 0x00022C45 File Offset: 0x00020E45
		internal ScriptGlobalOptions()
		{
			this.Platform = PlatformAutoDetector.GetDefaultPlatform();
			this.CustomConverters = new CustomConvertersCollection();
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x00022C63 File Offset: 0x00020E63
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x00022C6B File Offset: 0x00020E6B
		public CustomConvertersCollection CustomConverters { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x00022C74 File Offset: 0x00020E74
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x00022C7C File Offset: 0x00020E7C
		public IPlatformAccessor Platform { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x00022C85 File Offset: 0x00020E85
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x00022C8D File Offset: 0x00020E8D
		public bool RethrowExceptionNested { get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x00022C96 File Offset: 0x00020E96
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x00022C9E File Offset: 0x00020E9E
		public bool DecorateAllExceptions { get; set; }
	}
}

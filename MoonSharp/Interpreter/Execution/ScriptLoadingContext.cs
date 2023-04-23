using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A9 RID: 2217
	internal class ScriptLoadingContext
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x00025836 File Offset: 0x00023A36
		// (set) Token: 0x06003789 RID: 14217 RVA: 0x0002583E File Offset: 0x00023A3E
		public Script Script { get; private set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x00025847 File Offset: 0x00023A47
		// (set) Token: 0x0600378B RID: 14219 RVA: 0x0002584F File Offset: 0x00023A4F
		public BuildTimeScope Scope { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x00025858 File Offset: 0x00023A58
		// (set) Token: 0x0600378D RID: 14221 RVA: 0x00025860 File Offset: 0x00023A60
		public SourceCode Source { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x00025869 File Offset: 0x00023A69
		// (set) Token: 0x0600378F RID: 14223 RVA: 0x00025871 File Offset: 0x00023A71
		public bool Anonymous { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x0002587A File Offset: 0x00023A7A
		// (set) Token: 0x06003791 RID: 14225 RVA: 0x00025882 File Offset: 0x00023A82
		public bool IsDynamicExpression { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x0002588B File Offset: 0x00023A8B
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x00025893 File Offset: 0x00023A93
		public Lexer Lexer { get; set; }

		// Token: 0x06003794 RID: 14228 RVA: 0x0002589C File Offset: 0x00023A9C
		public ScriptLoadingContext(Script s)
		{
			this.Script = s;
		}
	}
}

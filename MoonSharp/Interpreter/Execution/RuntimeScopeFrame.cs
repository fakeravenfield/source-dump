using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A8 RID: 2216
	internal class RuntimeScopeFrame
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000257DD File Offset: 0x000239DD
		// (set) Token: 0x06003782 RID: 14210 RVA: 0x000257E5 File Offset: 0x000239E5
		public List<SymbolRef> DebugSymbols { get; private set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000257EE File Offset: 0x000239EE
		public int Count
		{
			get
			{
				return this.DebugSymbols.Count;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000257FB File Offset: 0x000239FB
		// (set) Token: 0x06003785 RID: 14213 RVA: 0x00025803 File Offset: 0x00023A03
		public int ToFirstBlock { get; internal set; }

		// Token: 0x06003786 RID: 14214 RVA: 0x0002580C File Offset: 0x00023A0C
		public RuntimeScopeFrame()
		{
			this.DebugSymbols = new List<SymbolRef>();
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x0002581F File Offset: 0x00023A1F
		public override string ToString()
		{
			return string.Format("ScopeFrame : #{0}", this.Count);
		}
	}
}

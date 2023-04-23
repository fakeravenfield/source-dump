using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x020008A2 RID: 2210
	internal class ClosureContext : List<DynValue>
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000256ED File Offset: 0x000238ED
		// (set) Token: 0x0600376F RID: 14191 RVA: 0x000256F5 File Offset: 0x000238F5
		public string[] Symbols { get; private set; }

		// Token: 0x06003770 RID: 14192 RVA: 0x000256FE File Offset: 0x000238FE
		internal ClosureContext(SymbolRef[] symbols, IEnumerable<DynValue> values)
		{
			this.Symbols = (from s in symbols
			select s.i_Name).ToArray<string>();
			base.AddRange(values);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0002573D File Offset: 0x0002393D
		internal ClosureContext()
		{
			this.Symbols = new string[0];
		}
	}
}

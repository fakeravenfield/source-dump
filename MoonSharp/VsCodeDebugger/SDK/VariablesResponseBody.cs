using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078F RID: 1935
	public class VariablesResponseBody : ResponseBody
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x00020B0F File Offset: 0x0001ED0F
		// (set) Token: 0x06002F7E RID: 12158 RVA: 0x00020B17 File Offset: 0x0001ED17
		public Variable[] variables { get; private set; }

		// Token: 0x06002F7F RID: 12159 RVA: 0x00020B20 File Offset: 0x0001ED20
		public VariablesResponseBody(List<Variable> vars = null)
		{
			if (vars == null)
			{
				this.variables = new Variable[0];
				return;
			}
			this.variables = vars.ToArray<Variable>();
		}
	}
}

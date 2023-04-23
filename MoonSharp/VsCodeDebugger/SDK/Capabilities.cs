using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200078B RID: 1931
	public class Capabilities : ResponseBody
	{
		// Token: 0x04002B67 RID: 11111
		public bool supportsConfigurationDoneRequest;

		// Token: 0x04002B68 RID: 11112
		public bool supportsFunctionBreakpoints;

		// Token: 0x04002B69 RID: 11113
		public bool supportsConditionalBreakpoints;

		// Token: 0x04002B6A RID: 11114
		public bool supportsEvaluateForHovers;

		// Token: 0x04002B6B RID: 11115
		public object[] exceptionBreakpointFilters;
	}
}

using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x020008C1 RID: 2241
	internal interface IPerformanceStopwatch
	{
		// Token: 0x0600387B RID: 14459
		IDisposable Start();

		// Token: 0x0600387C RID: 14460
		PerformanceResult GetResult();
	}
}

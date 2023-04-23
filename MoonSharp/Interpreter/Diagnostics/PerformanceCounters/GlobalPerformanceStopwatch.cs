using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x020008BF RID: 2239
	internal class GlobalPerformanceStopwatch : IPerformanceStopwatch
	{
		// Token: 0x06003875 RID: 14453 RVA: 0x00026391 File Offset: 0x00024591
		public GlobalPerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000263A0 File Offset: 0x000245A0
		private void SignalStopwatchTerminated(Stopwatch sw)
		{
			this.m_Elapsed += sw.ElapsedMilliseconds;
			this.m_Count++;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000263C3 File Offset: 0x000245C3
		public IDisposable Start()
		{
			return new GlobalPerformanceStopwatch.GlobalPerformanceStopwatch_StopwatchObject(this);
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x001259D0 File Offset: 0x00123BD0
		public PerformanceResult GetResult()
		{
			return new PerformanceResult
			{
				Type = PerformanceCounterType.TimeMilliseconds,
				Global = false,
				Name = this.m_Counter.ToString(),
				Instances = this.m_Count,
				Counter = this.m_Elapsed
			};
		}

		// Token: 0x04002F98 RID: 12184
		private int m_Count;

		// Token: 0x04002F99 RID: 12185
		private long m_Elapsed;

		// Token: 0x04002F9A RID: 12186
		private PerformanceCounter m_Counter;

		// Token: 0x020008C0 RID: 2240
		private class GlobalPerformanceStopwatch_StopwatchObject : IDisposable
		{
			// Token: 0x06003879 RID: 14457 RVA: 0x000263CB File Offset: 0x000245CB
			public GlobalPerformanceStopwatch_StopwatchObject(GlobalPerformanceStopwatch parent)
			{
				this.m_Parent = parent;
				this.m_Stopwatch = Stopwatch.StartNew();
			}

			// Token: 0x0600387A RID: 14458 RVA: 0x000263E5 File Offset: 0x000245E5
			public void Dispose()
			{
				this.m_Stopwatch.Stop();
				this.m_Parent.SignalStopwatchTerminated(this.m_Stopwatch);
			}

			// Token: 0x04002F9B RID: 12187
			private Stopwatch m_Stopwatch;

			// Token: 0x04002F9C RID: 12188
			private GlobalPerformanceStopwatch m_Parent;
		}
	}
}

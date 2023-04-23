using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x020008C2 RID: 2242
	internal class PerformanceStopwatch : IDisposable, IPerformanceStopwatch
	{
		// Token: 0x0600387D RID: 14461 RVA: 0x00026403 File Offset: 0x00024603
		public PerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0002641D File Offset: 0x0002461D
		public IDisposable Start()
		{
			if (this.m_Reentrant == 0)
			{
				this.m_Count++;
				this.m_Stopwatch.Start();
			}
			this.m_Reentrant++;
			return this;
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x0002644F File Offset: 0x0002464F
		public void Dispose()
		{
			this.m_Reentrant--;
			if (this.m_Reentrant == 0)
			{
				this.m_Stopwatch.Stop();
			}
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x00125A20 File Offset: 0x00123C20
		public PerformanceResult GetResult()
		{
			return new PerformanceResult
			{
				Type = PerformanceCounterType.TimeMilliseconds,
				Global = false,
				Name = this.m_Counter.ToString(),
				Instances = this.m_Count,
				Counter = this.m_Stopwatch.ElapsedMilliseconds
			};
		}

		// Token: 0x04002F9D RID: 12189
		private Stopwatch m_Stopwatch = new Stopwatch();

		// Token: 0x04002F9E RID: 12190
		private int m_Count;

		// Token: 0x04002F9F RID: 12191
		private int m_Reentrant;

		// Token: 0x04002FA0 RID: 12192
		private PerformanceCounter m_Counter;
	}
}

using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x020008BE RID: 2238
	internal class DummyPerformanceStopwatch : IPerformanceStopwatch, IDisposable
	{
		// Token: 0x06003870 RID: 14448 RVA: 0x00026342 File Offset: 0x00024542
		private DummyPerformanceStopwatch()
		{
			this.m_Result = new PerformanceResult
			{
				Counter = 0L,
				Global = true,
				Instances = 0,
				Name = "::dummy::",
				Type = PerformanceCounterType.TimeMilliseconds
			};
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x00008D0C File Offset: 0x00006F0C
		public IDisposable Start()
		{
			return this;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x0002637D File Offset: 0x0002457D
		public PerformanceResult GetResult()
		{
			return this.m_Result;
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x0000296E File Offset: 0x00000B6E
		public void Dispose()
		{
		}

		// Token: 0x04002F96 RID: 12182
		public static DummyPerformanceStopwatch Instance = new DummyPerformanceStopwatch();

		// Token: 0x04002F97 RID: 12183
		private PerformanceResult m_Result;
	}
}

using System;
using System.Text;
using MoonSharp.Interpreter.Diagnostics.PerformanceCounters;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x020008BC RID: 2236
	public class PerformanceStatistics
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x00026319 File Offset: 0x00024519
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x001258A8 File Offset: 0x00123AA8
		public bool Enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				if (value && !this.m_Enabled)
				{
					if (PerformanceStatistics.m_GlobalStopwatches[3] == null)
					{
						PerformanceStatistics.m_GlobalStopwatches[3] = new GlobalPerformanceStopwatch(PerformanceCounter.AdaptersCompilation);
					}
					for (int i = 0; i < 4; i++)
					{
						this.m_Stopwatches[i] = (PerformanceStatistics.m_GlobalStopwatches[i] ?? new PerformanceStopwatch((PerformanceCounter)i));
					}
				}
				else if (!value && this.m_Enabled)
				{
					this.m_Stopwatches = new IPerformanceStopwatch[4];
					PerformanceStatistics.m_GlobalStopwatches = new IPerformanceStopwatch[4];
				}
				this.m_Enabled = value;
			}
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x00125928 File Offset: 0x00123B28
		public PerformanceResult GetPerformanceCounterResult(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.GetResult();
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0012594C File Offset: 0x00123B4C
		internal IDisposable StartStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x00125970 File Offset: 0x00123B70
		internal static IDisposable StartGlobalStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = PerformanceStatistics.m_GlobalStopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x00125990 File Offset: 0x00123B90
		public string GetPerformanceLog()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				PerformanceResult performanceCounterResult = this.GetPerformanceCounterResult((PerformanceCounter)i);
				if (performanceCounterResult != null)
				{
					stringBuilder.AppendLine(performanceCounterResult.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002F93 RID: 12179
		private IPerformanceStopwatch[] m_Stopwatches = new IPerformanceStopwatch[4];

		// Token: 0x04002F94 RID: 12180
		private static IPerformanceStopwatch[] m_GlobalStopwatches = new IPerformanceStopwatch[4];

		// Token: 0x04002F95 RID: 12181
		private bool m_Enabled;
	}
}

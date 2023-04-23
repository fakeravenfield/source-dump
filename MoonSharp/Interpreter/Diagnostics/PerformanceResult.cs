using System;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x020008BB RID: 2235
	public class PerformanceResult
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x00026291 File Offset: 0x00024491
		// (set) Token: 0x0600385C RID: 14428 RVA: 0x00026299 File Offset: 0x00024499
		public string Name { get; internal set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000262A2 File Offset: 0x000244A2
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x000262AA File Offset: 0x000244AA
		public long Counter { get; internal set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000262B3 File Offset: 0x000244B3
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x000262BB File Offset: 0x000244BB
		public int Instances { get; internal set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000262C4 File Offset: 0x000244C4
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x000262CC File Offset: 0x000244CC
		public bool Global { get; internal set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000262D5 File Offset: 0x000244D5
		// (set) Token: 0x06003864 RID: 14436 RVA: 0x000262DD File Offset: 0x000244DD
		public PerformanceCounterType Type { get; internal set; }

		// Token: 0x06003865 RID: 14437 RVA: 0x00125840 File Offset: 0x00123A40
		public override string ToString()
		{
			return string.Format("{0}{1} : {2} times / {3} {4}", new object[]
			{
				this.Name,
				this.Global ? "(g)" : "",
				this.Instances,
				this.Counter,
				PerformanceResult.PerformanceCounterTypeToString(this.Type)
			});
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000262E6 File Offset: 0x000244E6
		public static string PerformanceCounterTypeToString(PerformanceCounterType Type)
		{
			if (Type == PerformanceCounterType.MemoryBytes)
			{
				return "bytes";
			}
			if (Type != PerformanceCounterType.TimeMilliseconds)
			{
				throw new InvalidOperationException("PerformanceCounterType has invalid value " + Type.ToString());
			}
			return "ms";
		}
	}
}

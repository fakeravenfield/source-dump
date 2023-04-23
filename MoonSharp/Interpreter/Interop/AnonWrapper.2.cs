using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000839 RID: 2105
	public class AnonWrapper<T> : AnonWrapper
	{
		// Token: 0x0600343A RID: 13370 RVA: 0x00023B03 File Offset: 0x00021D03
		public AnonWrapper()
		{
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x00023B0B File Offset: 0x00021D0B
		public AnonWrapper(T o)
		{
			this.Value = o;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x00023B1A File Offset: 0x00021D1A
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x00023B22 File Offset: 0x00021D22
		public T Value { get; set; }
	}
}

using System;

namespace Lua
{
	// Token: 0x02000908 RID: 2312
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class IgnoreAttribute : Attribute
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x00027A83 File Offset: 0x00025C83
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x00027A8B File Offset: 0x00025C8B
		public bool ignoreBase { get; set; }
	}
}

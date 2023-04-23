using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007DC RID: 2012
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleConstantAttribute : Attribute
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x00022A90 File Offset: 0x00020C90
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x00022A98 File Offset: 0x00020C98
		public string Name { get; set; }
	}
}

using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007DB RID: 2011
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleAttribute : Attribute
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x00022A7F File Offset: 0x00020C7F
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x00022A87 File Offset: 0x00020C87
		public string Namespace { get; set; }
	}
}

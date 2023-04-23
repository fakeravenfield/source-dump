using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007DD RID: 2013
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpModuleMethodAttribute : Attribute
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x00022AA1 File Offset: 0x00020CA1
		// (set) Token: 0x06003215 RID: 12821 RVA: 0x00022AA9 File Offset: 0x00020CA9
		public string Name { get; set; }
	}
}

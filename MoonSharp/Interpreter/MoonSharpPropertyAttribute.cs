using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CC RID: 1996
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpPropertyAttribute : Attribute
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000228C2 File Offset: 0x00020AC2
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x000228CA File Offset: 0x00020ACA
		public string Name { get; private set; }

		// Token: 0x060031E0 RID: 12768 RVA: 0x000026F3 File Offset: 0x000008F3
		public MoonSharpPropertyAttribute()
		{
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000228D3 File Offset: 0x00020AD3
		public MoonSharpPropertyAttribute(string name)
		{
			this.Name = name;
		}
	}
}

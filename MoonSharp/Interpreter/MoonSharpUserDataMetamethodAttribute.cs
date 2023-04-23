using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CE RID: 1998
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpUserDataMetamethodAttribute : Attribute
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x00022902 File Offset: 0x00020B02
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x0002290A File Offset: 0x00020B0A
		public string Name { get; private set; }

		// Token: 0x060031E7 RID: 12775 RVA: 0x00022913 File Offset: 0x00020B13
		public MoonSharpUserDataMetamethodAttribute(string name)
		{
			this.Name = name;
		}
	}
}

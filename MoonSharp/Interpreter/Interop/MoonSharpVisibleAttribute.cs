using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200082A RID: 2090
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
	public sealed class MoonSharpVisibleAttribute : Attribute
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x00023953 File Offset: 0x00021B53
		// (set) Token: 0x060033F3 RID: 13299 RVA: 0x0002395B File Offset: 0x00021B5B
		public bool Visible { get; private set; }

		// Token: 0x060033F4 RID: 13300 RVA: 0x00023964 File Offset: 0x00021B64
		public MoonSharpVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}
	}
}

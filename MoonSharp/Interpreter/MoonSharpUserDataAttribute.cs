using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CD RID: 1997
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public sealed class MoonSharpUserDataAttribute : Attribute
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000228E2 File Offset: 0x00020AE2
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000228EA File Offset: 0x00020AEA
		public InteropAccessMode AccessMode { get; set; }

		// Token: 0x060031E4 RID: 12772 RVA: 0x000228F3 File Offset: 0x00020AF3
		public MoonSharpUserDataAttribute()
		{
			this.AccessMode = InteropAccessMode.Default;
		}
	}
}

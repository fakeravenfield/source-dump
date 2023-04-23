using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CB RID: 1995
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public sealed class MoonSharpHideMemberAttribute : Attribute
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000228A2 File Offset: 0x00020AA2
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000228AA File Offset: 0x00020AAA
		public string MemberName { get; private set; }

		// Token: 0x060031DD RID: 12765 RVA: 0x000228B3 File Offset: 0x00020AB3
		public MoonSharpHideMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}
	}
}

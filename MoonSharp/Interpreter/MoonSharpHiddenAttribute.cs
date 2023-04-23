using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007CA RID: 1994
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
	public sealed class MoonSharpHiddenAttribute : Attribute
	{
	}
}

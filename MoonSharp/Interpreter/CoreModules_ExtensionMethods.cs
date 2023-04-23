using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007D8 RID: 2008
	internal static class CoreModules_ExtensionMethods
	{
		// Token: 0x06003201 RID: 12801 RVA: 0x00022A05 File Offset: 0x00020C05
		public static bool Has(this CoreModules val, CoreModules flag)
		{
			return (val & flag) == flag;
		}
	}
}

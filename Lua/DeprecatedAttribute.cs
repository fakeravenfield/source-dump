using System;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000903 RID: 2307
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class DeprecatedAttribute : Attribute
	{
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000279E4 File Offset: 0x00025BE4
		public DeprecatedAttribute(string message)
		{
			this.message = message;
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000279F3 File Offset: 0x00025BF3
		public static bool IsDefined(ICustomAttributeProvider info)
		{
			return info.IsDefined(typeof(DeprecatedAttribute), false);
		}

		// Token: 0x0400303C RID: 12348
		public string message;
	}
}

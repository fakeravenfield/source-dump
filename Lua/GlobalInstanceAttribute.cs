using System;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000907 RID: 2311
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class GlobalInstanceAttribute : Attribute
	{
		// Token: 0x06003ADB RID: 15067 RVA: 0x00027A70 File Offset: 0x00025C70
		public static bool IsDefined(ICustomAttributeProvider info)
		{
			return info.IsDefined(typeof(GlobalInstanceAttribute), false);
		}
	}
}

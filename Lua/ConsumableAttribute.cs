using System;
using System.Linq;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000902 RID: 2306
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class ConsumableAttribute : Attribute
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x00027992 File Offset: 0x00025B92
		public ConsumableAttribute(string comment = null)
		{
			this.comment = comment;
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000279A1 File Offset: 0x00025BA1
		public static string GetComment(ICustomAttributeProvider info)
		{
			if (ConsumableAttribute.IsDefined(info))
			{
				return ((ConsumableAttribute)info.GetCustomAttributes(typeof(ConsumableAttribute), false).First<object>()).comment;
			}
			return "";
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000279D1 File Offset: 0x00025BD1
		public static bool IsDefined(ICustomAttributeProvider info)
		{
			return info.IsDefined(typeof(ConsumableAttribute), false);
		}

		// Token: 0x0400303B RID: 12347
		public readonly string comment;
	}
}

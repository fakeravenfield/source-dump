using System;
using System.Linq;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000904 RID: 2308
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class DocAttribute : Attribute
	{
		// Token: 0x06003AD4 RID: 15060 RVA: 0x00027A06 File Offset: 0x00025C06
		public DocAttribute(string comment = null)
		{
			this.comment = comment;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x00027A15 File Offset: 0x00025C15
		public static string GetComment(ICustomAttributeProvider info)
		{
			if (info.IsDefined(typeof(DocAttribute), false))
			{
				return ((DocAttribute)info.GetCustomAttributes(typeof(DocAttribute), false).First<object>()).comment;
			}
			return "";
		}

		// Token: 0x0400303D RID: 12349
		public readonly string comment;
	}
}

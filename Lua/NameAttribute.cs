using System;
using System.Linq;
using System.Reflection;

namespace Lua
{
	// Token: 0x0200090A RID: 2314
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class NameAttribute : Attribute
	{
		// Token: 0x06003AE0 RID: 15072 RVA: 0x00027A94 File Offset: 0x00025C94
		public NameAttribute(string name = null)
		{
			this.name = name;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00027AA3 File Offset: 0x00025CA3
		public static string GetName(Type type)
		{
			return NameAttribute.GetName(type, type.Name);
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x0012BCF4 File Offset: 0x00129EF4
		public static string GetName(ICustomAttributeProvider type, string defaultName)
		{
			NameAttribute nameAttribute = (NameAttribute)type.GetCustomAttributes(typeof(NameAttribute), false).FirstOrDefault<object>();
			if (nameAttribute == null)
			{
				Type type2 = null;
				WrapperAttribute.FindWrapper(type, out type2);
				if (type2 != null)
				{
					nameAttribute = (NameAttribute)type2.GetCustomAttributes(typeof(NameAttribute), false).FirstOrDefault<object>();
				}
			}
			if (nameAttribute != null && !string.IsNullOrEmpty(nameAttribute.name))
			{
				return nameAttribute.name;
			}
			return defaultName;
		}

		// Token: 0x04003040 RID: 12352
		public readonly string name;
	}
}

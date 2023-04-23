using System;
using System.Linq;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000900 RID: 2304
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class CallbackSignatureAttribute : Attribute
	{
		// Token: 0x06003ACB RID: 15051 RVA: 0x0002793F File Offset: 0x00025B3F
		public CallbackSignatureAttribute(params string[] argNames)
		{
			this.argNames = argNames;
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0002794E File Offset: 0x00025B4E
		public static string[] GetArgNames(ICustomAttributeProvider info)
		{
			if (CallbackSignatureAttribute.IsDefined(info))
			{
				return ((CallbackSignatureAttribute)info.GetCustomAttributes(typeof(CallbackSignatureAttribute), false).First<object>()).argNames;
			}
			return new string[0];
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0002797F File Offset: 0x00025B7F
		public static bool IsDefined(ICustomAttributeProvider info)
		{
			return info.IsDefined(typeof(CallbackSignatureAttribute), false);
		}

		// Token: 0x0400303A RID: 12346
		public readonly string[] argNames;
	}
}

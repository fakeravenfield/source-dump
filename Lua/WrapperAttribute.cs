using System;
using System.Linq;
using System.Reflection;

namespace Lua
{
	// Token: 0x02000910 RID: 2320
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class WrapperAttribute : Attribute
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x00027AE0 File Offset: 0x00025CE0
		// (set) Token: 0x06003AEB RID: 15083 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public Type forType { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x00027AF1 File Offset: 0x00025CF1
		// (set) Token: 0x06003AED RID: 15085 RVA: 0x00027AF9 File Offset: 0x00025CF9
		public bool includeTarget { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x00027B02 File Offset: 0x00025D02
		// (set) Token: 0x06003AEF RID: 15087 RVA: 0x00027B0A File Offset: 0x00025D0A
		public bool includeBase { get; set; }

		// Token: 0x06003AF0 RID: 15088 RVA: 0x00027B13 File Offset: 0x00025D13
		public WrapperAttribute(Type forType)
		{
			this.forType = forType;
			this.includeTarget = false;
			this.includeBase = true;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x0012BD68 File Offset: 0x00129F68
		public static WrapperAttribute FindWrapper(ICustomAttributeProvider type, out Type wrapper)
		{
			var <>f__AnonymousType = (from t in Assembly.GetAssembly(typeof(WrapperAttribute)).GetTypes()
			where t.IsDefined(typeof(WrapperAttribute), false)
			select new
			{
				wrapper = t,
				attribute = WrapperAttribute.GetWrapper(t)
			} into x
			where x.attribute.forType == type
			select x).FirstOrDefault();
			if (<>f__AnonymousType != null)
			{
				wrapper = <>f__AnonymousType.wrapper;
				return <>f__AnonymousType.attribute;
			}
			wrapper = null;
			return null;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x00027B30 File Offset: 0x00025D30
		private static WrapperAttribute GetWrapper(ICustomAttributeProvider type)
		{
			return (WrapperAttribute)type.GetCustomAttributes(typeof(WrapperAttribute), false).FirstOrDefault<object>();
		}
	}
}

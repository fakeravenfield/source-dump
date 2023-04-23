using System;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020008FD RID: 2301
	internal abstract class FrameworkReflectionBase : FrameworkBase
	{
		// Token: 0x06003AB8 RID: 15032
		public abstract Type GetTypeInfoFromType(Type t);

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0002785F File Offset: 0x00025A5F
		public override Assembly GetAssembly(Type t)
		{
			return this.GetTypeInfoFromType(t).Assembly;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x0002786D File Offset: 0x00025A6D
		public override Type GetBaseType(Type t)
		{
			return this.GetTypeInfoFromType(t).BaseType;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0002787B File Offset: 0x00025A7B
		public override bool IsValueType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsValueType;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00027889 File Offset: 0x00025A89
		public override bool IsInterface(Type t)
		{
			return this.GetTypeInfoFromType(t).IsInterface;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x00027897 File Offset: 0x00025A97
		public override bool IsNestedPublic(Type t)
		{
			return this.GetTypeInfoFromType(t).IsNestedPublic;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000278A5 File Offset: 0x00025AA5
		public override bool IsAbstract(Type t)
		{
			return this.GetTypeInfoFromType(t).IsAbstract;
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000278B3 File Offset: 0x00025AB3
		public override bool IsEnum(Type t)
		{
			return this.GetTypeInfoFromType(t).IsEnum;
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000278C1 File Offset: 0x00025AC1
		public override bool IsGenericTypeDefinition(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericTypeDefinition;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000278CF File Offset: 0x00025ACF
		public override bool IsGenericType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericType;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000278DD File Offset: 0x00025ADD
		public override Attribute[] GetCustomAttributes(Type t, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(inherit).OfType<Attribute>().ToArray<Attribute>();
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000278F6 File Offset: 0x00025AF6
		public override Attribute[] GetCustomAttributes(Type t, Type at, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(at, inherit).OfType<Attribute>().ToArray<Attribute>();
		}
	}
}

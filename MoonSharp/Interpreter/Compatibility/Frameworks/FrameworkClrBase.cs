using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020008FC RID: 2300
	internal abstract class FrameworkClrBase : FrameworkReflectionBase
	{
		// Token: 0x06003AA5 RID: 15013 RVA: 0x0002773B File Offset: 0x0002593B
		public override MethodInfo GetAddMethod(EventInfo ei)
		{
			return ei.GetAddMethod(true);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x00027744 File Offset: 0x00025944
		public override ConstructorInfo[] GetConstructors(Type type)
		{
			return this.GetTypeInfoFromType(type).GetConstructors(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00027758 File Offset: 0x00025958
		public override EventInfo[] GetEvents(Type type)
		{
			return this.GetTypeInfoFromType(type).GetEvents(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x0002776C File Offset: 0x0002596C
		public override FieldInfo[] GetFields(Type type)
		{
			return this.GetTypeInfoFromType(type).GetFields(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x00027780 File Offset: 0x00025980
		public override Type[] GetGenericArguments(Type type)
		{
			return this.GetTypeInfoFromType(type).GetGenericArguments();
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0002778E File Offset: 0x0002598E
		public override MethodInfo GetGetMethod(PropertyInfo pi)
		{
			return pi.GetGetMethod(true);
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00027797 File Offset: 0x00025997
		public override Type[] GetInterfaces(Type t)
		{
			return this.GetTypeInfoFromType(t).GetInterfaces();
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000277A5 File Offset: 0x000259A5
		public override MethodInfo GetMethod(Type type, string name)
		{
			return this.GetTypeInfoFromType(type).GetMethod(name);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000277B4 File Offset: 0x000259B4
		public override MethodInfo[] GetMethods(Type type)
		{
			return this.GetTypeInfoFromType(type).GetMethods(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000277C8 File Offset: 0x000259C8
		public override Type[] GetNestedTypes(Type type)
		{
			return this.GetTypeInfoFromType(type).GetNestedTypes(this.BINDINGFLAGS_INNERCLASS);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000277DC File Offset: 0x000259DC
		public override PropertyInfo[] GetProperties(Type type)
		{
			return this.GetTypeInfoFromType(type).GetProperties(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000277F0 File Offset: 0x000259F0
		public override PropertyInfo GetProperty(Type type, string name)
		{
			return this.GetTypeInfoFromType(type).GetProperty(name);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000277FF File Offset: 0x000259FF
		public override MethodInfo GetRemoveMethod(EventInfo ei)
		{
			return ei.GetRemoveMethod(true);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x00027808 File Offset: 0x00025A08
		public override MethodInfo GetSetMethod(PropertyInfo pi)
		{
			return pi.GetSetMethod(true);
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x00027811 File Offset: 0x00025A11
		public override bool IsAssignableFrom(Type current, Type toCompare)
		{
			return this.GetTypeInfoFromType(current).IsAssignableFrom(toCompare);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x00027820 File Offset: 0x00025A20
		public override bool IsInstanceOfType(Type t, object o)
		{
			return this.GetTypeInfoFromType(t).IsInstanceOfType(o);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x0002782F File Offset: 0x00025A2F
		public override MethodInfo GetMethod(Type resourcesType, string name, Type[] types)
		{
			return this.GetTypeInfoFromType(resourcesType).GetMethod(name, types);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x0002783F File Offset: 0x00025A3F
		public override Type[] GetAssemblyTypes(Assembly asm)
		{
			return asm.GetTypes();
		}

		// Token: 0x04003038 RID: 12344
		private BindingFlags BINDINGFLAGS_MEMBER = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04003039 RID: 12345
		private BindingFlags BINDINGFLAGS_INNERCLASS = BindingFlags.Public | BindingFlags.NonPublic;
	}
}

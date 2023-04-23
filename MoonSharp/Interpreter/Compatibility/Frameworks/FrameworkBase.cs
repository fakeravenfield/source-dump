using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020008FB RID: 2299
	public abstract class FrameworkBase
	{
		// Token: 0x06003A84 RID: 14980
		public abstract bool StringContainsChar(string str, char chr);

		// Token: 0x06003A85 RID: 14981
		public abstract bool IsValueType(Type t);

		// Token: 0x06003A86 RID: 14982
		public abstract Assembly GetAssembly(Type t);

		// Token: 0x06003A87 RID: 14983
		public abstract Type GetBaseType(Type t);

		// Token: 0x06003A88 RID: 14984
		public abstract bool IsGenericType(Type t);

		// Token: 0x06003A89 RID: 14985
		public abstract bool IsGenericTypeDefinition(Type t);

		// Token: 0x06003A8A RID: 14986
		public abstract bool IsEnum(Type t);

		// Token: 0x06003A8B RID: 14987
		public abstract bool IsNestedPublic(Type t);

		// Token: 0x06003A8C RID: 14988
		public abstract bool IsAbstract(Type t);

		// Token: 0x06003A8D RID: 14989
		public abstract bool IsInterface(Type t);

		// Token: 0x06003A8E RID: 14990
		public abstract Attribute[] GetCustomAttributes(Type t, bool inherit);

		// Token: 0x06003A8F RID: 14991
		public abstract Attribute[] GetCustomAttributes(Type t, Type at, bool inherit);

		// Token: 0x06003A90 RID: 14992
		public abstract Type[] GetInterfaces(Type t);

		// Token: 0x06003A91 RID: 14993
		public abstract bool IsInstanceOfType(Type t, object o);

		// Token: 0x06003A92 RID: 14994
		public abstract MethodInfo GetAddMethod(EventInfo ei);

		// Token: 0x06003A93 RID: 14995
		public abstract MethodInfo GetRemoveMethod(EventInfo ei);

		// Token: 0x06003A94 RID: 14996
		public abstract MethodInfo GetGetMethod(PropertyInfo pi);

		// Token: 0x06003A95 RID: 14997
		public abstract MethodInfo GetSetMethod(PropertyInfo pi);

		// Token: 0x06003A96 RID: 14998
		public abstract Type GetInterface(Type type, string name);

		// Token: 0x06003A97 RID: 14999
		public abstract PropertyInfo[] GetProperties(Type type);

		// Token: 0x06003A98 RID: 15000
		public abstract PropertyInfo GetProperty(Type type, string name);

		// Token: 0x06003A99 RID: 15001
		public abstract Type[] GetNestedTypes(Type type);

		// Token: 0x06003A9A RID: 15002
		public abstract EventInfo[] GetEvents(Type type);

		// Token: 0x06003A9B RID: 15003
		public abstract ConstructorInfo[] GetConstructors(Type type);

		// Token: 0x06003A9C RID: 15004
		public abstract Type[] GetAssemblyTypes(Assembly asm);

		// Token: 0x06003A9D RID: 15005
		public abstract MethodInfo[] GetMethods(Type type);

		// Token: 0x06003A9E RID: 15006
		public abstract FieldInfo[] GetFields(Type t);

		// Token: 0x06003A9F RID: 15007
		public abstract MethodInfo GetMethod(Type type, string name);

		// Token: 0x06003AA0 RID: 15008
		public abstract Type[] GetGenericArguments(Type t);

		// Token: 0x06003AA1 RID: 15009
		public abstract bool IsAssignableFrom(Type current, Type toCompare);

		// Token: 0x06003AA2 RID: 15010
		public abstract bool IsDbNull(object o);

		// Token: 0x06003AA3 RID: 15011
		public abstract MethodInfo GetMethod(Type resourcesType, string v, Type[] type);
	}
}

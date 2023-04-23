using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.UserDataRegistries
{
	// Token: 0x02000873 RID: 2163
	internal class ExtensionMethodsRegistry
	{
		// Token: 0x060035DD RID: 13789 RVA: 0x0011C2E0 File Offset: 0x0011A4E0
		public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
		{
			object obj = ExtensionMethodsRegistry.s_Lock;
			lock (obj)
			{
				bool flag2 = false;
				foreach (MethodInfo methodInfo in from _mi in Framework.Do.GetMethods(type)
				where _mi.IsStatic
				select _mi)
				{
					if (methodInfo.GetCustomAttributes(typeof(ExtensionAttribute), false).Count<object>() != 0)
					{
						if (methodInfo.ContainsGenericParameters)
						{
							ExtensionMethodsRegistry.s_UnresolvedGenericsRegistry.Add(methodInfo.Name, new ExtensionMethodsRegistry.UnresolvedGenericMethod(methodInfo, mode));
							flag2 = true;
						}
						else if (MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, false))
						{
							MethodMemberDescriptor value = new MethodMemberDescriptor(methodInfo, mode);
							ExtensionMethodsRegistry.s_Registry.Add(methodInfo.Name, value);
							flag2 = true;
						}
					}
				}
				if (flag2)
				{
					ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion++;
				}
			}
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x00024714 File Offset: 0x00022914
		private static object FrameworkGetMethods()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0011C3F4 File Offset: 0x0011A5F4
		public static IEnumerable<IOverloadableMemberDescriptor> GetExtensionMethodsByName(string name)
		{
			object obj = ExtensionMethodsRegistry.s_Lock;
			IEnumerable<IOverloadableMemberDescriptor> result;
			lock (obj)
			{
				result = new List<IOverloadableMemberDescriptor>(ExtensionMethodsRegistry.s_Registry.Find(name));
			}
			return result;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0002471B File Offset: 0x0002291B
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0011C440 File Offset: 0x0011A640
		public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
		{
			List<ExtensionMethodsRegistry.UnresolvedGenericMethod> list = null;
			object obj = ExtensionMethodsRegistry.s_Lock;
			lock (obj)
			{
				list = ExtensionMethodsRegistry.s_UnresolvedGenericsRegistry.Find(name).ToList<ExtensionMethodsRegistry.UnresolvedGenericMethod>();
			}
			foreach (ExtensionMethodsRegistry.UnresolvedGenericMethod unresolvedGenericMethod in list)
			{
				ParameterInfo[] parameters = unresolvedGenericMethod.Method.GetParameters();
				if (parameters.Length != 0)
				{
					Type parameterType = parameters[0].ParameterType;
					Type genericMatch = ExtensionMethodsRegistry.GetGenericMatch(parameterType, extendedType);
					if (unresolvedGenericMethod.AlreadyAddedTypes.Add(genericMatch) && genericMatch != null)
					{
						MethodInfo methodInfo = ExtensionMethodsRegistry.InstantiateMethodInfo(unresolvedGenericMethod.Method, parameterType, genericMatch, extendedType);
						if (methodInfo != null && MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, false))
						{
							MethodMemberDescriptor value = new MethodMemberDescriptor(methodInfo, unresolvedGenericMethod.AccessMode);
							ExtensionMethodsRegistry.s_Registry.Add(unresolvedGenericMethod.Method.Name, value);
							ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion++;
						}
					}
				}
			}
			return (from d in ExtensionMethodsRegistry.s_Registry.Find(name)
			where d.ExtensionMethodType != null && Framework.Do.IsAssignableFrom(d.ExtensionMethodType, extendedType)
			select d).ToList<IOverloadableMemberDescriptor>();
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0011C5A4 File Offset: 0x0011A7A4
		private static MethodInfo InstantiateMethodInfo(MethodInfo mi, Type extensionType, Type genericType, Type extendedType)
		{
			Type[] genericArguments = mi.GetGenericArguments();
			Type[] genericArguments2 = Framework.Do.GetGenericArguments(genericType);
			if (genericArguments2.Length == genericArguments.Length)
			{
				return mi.MakeGenericMethod(genericArguments2);
			}
			return null;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0011C5D8 File Offset: 0x0011A7D8
		private static Type GetGenericMatch(Type extensionType, Type extendedType)
		{
			if (!extensionType.IsGenericParameter)
			{
				extensionType = extensionType.GetGenericTypeDefinition();
				foreach (Type type in extendedType.GetAllImplementedTypes())
				{
					if (Framework.Do.IsGenericType(type) && type.GetGenericTypeDefinition() == extensionType)
					{
						return type;
					}
				}
			}
			return null;
		}

		// Token: 0x04002E60 RID: 11872
		private static object s_Lock = new object();

		// Token: 0x04002E61 RID: 11873
		private static MultiDictionary<string, IOverloadableMemberDescriptor> s_Registry = new MultiDictionary<string, IOverloadableMemberDescriptor>();

		// Token: 0x04002E62 RID: 11874
		private static MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod> s_UnresolvedGenericsRegistry = new MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod>();

		// Token: 0x04002E63 RID: 11875
		private static int s_ExtensionMethodChangeVersion = 0;

		// Token: 0x02000874 RID: 2164
		private class UnresolvedGenericMethod
		{
			// Token: 0x060035E6 RID: 13798 RVA: 0x00024748 File Offset: 0x00022948
			public UnresolvedGenericMethod(MethodInfo mi, InteropAccessMode mode)
			{
				this.AccessMode = mode;
				this.Method = mi;
			}

			// Token: 0x04002E64 RID: 11876
			public readonly MethodInfo Method;

			// Token: 0x04002E65 RID: 11877
			public readonly InteropAccessMode AccessMode;

			// Token: 0x04002E66 RID: 11878
			public readonly HashSet<Type> AlreadyAddedTypes = new HashSet<Type>();
		}
	}
}

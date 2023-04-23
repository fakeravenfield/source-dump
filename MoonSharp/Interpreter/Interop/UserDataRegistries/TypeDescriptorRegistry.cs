using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop.UserDataRegistries
{
	// Token: 0x02000877 RID: 2167
	internal static class TypeDescriptorRegistry
	{
		// Token: 0x060035EC RID: 13804 RVA: 0x0011C650 File Offset: 0x0011A850
		internal static void RegisterAssembly(Assembly asm = null, bool includeExtensionTypes = false)
		{
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}
			if (includeExtensionTypes)
			{
				foreach (var <>f__AnonymousType in from t in asm.SafeGetTypes()
				let attributes = Framework.Do.GetCustomAttributes(t, typeof(ExtensionAttribute), true)
				where attributes != null && attributes.Length != 0
				select new
				{
					Attributes = attributes,
					DataType = t
				})
				{
					UserData.RegisterExtensionType(<>f__AnonymousType.DataType, InteropAccessMode.Default);
				}
			}
			foreach (var <>f__AnonymousType2 in from t in asm.SafeGetTypes()
			let attributes = Framework.Do.GetCustomAttributes(t, typeof(MoonSharpUserDataAttribute), true)
			where attributes != null && attributes.Length != 0
			select new
			{
				Attributes = attributes,
				DataType = t
			})
			{
				UserData.RegisterType(<>f__AnonymousType2.DataType, <>f__AnonymousType2.Attributes.OfType<MoonSharpUserDataAttribute>().First<MoonSharpUserDataAttribute>().AccessMode, null);
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0011C7E8 File Offset: 0x0011A9E8
		internal static bool IsTypeRegistered(Type type)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			bool result;
			lock (obj)
			{
				result = TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(type);
			}
			return result;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0011C830 File Offset: 0x0011AA30
		internal static void UnregisterType(Type t)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			lock (obj)
			{
				if (TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(t))
				{
					TypeDescriptorRegistry.PerformRegistration(t, null, TypeDescriptorRegistry.s_TypeRegistry[t]);
				}
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x0002479D File Offset: 0x0002299D
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x000247A4 File Offset: 0x000229A4
		internal static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return TypeDescriptorRegistry.s_DefaultAccessMode;
			}
			set
			{
				if (value == InteropAccessMode.Default)
				{
					throw new ArgumentException("InteropAccessMode is InteropAccessMode.Default");
				}
				TypeDescriptorRegistry.s_DefaultAccessMode = value;
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0011C88C File Offset: 0x0011AA8C
		internal static IUserDataDescriptor RegisterProxyType_Impl(IProxyFactory proxyFactory, InteropAccessMode accessMode, string friendlyName)
		{
			IUserDataDescriptor proxyDescriptor = TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.ProxyType, accessMode, friendlyName, null);
			return TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.TargetType, accessMode, friendlyName, new ProxyUserDataDescriptor(proxyFactory, proxyDescriptor, friendlyName));
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0011C8C0 File Offset: 0x0011AAC0
		internal static IUserDataDescriptor RegisterType_Impl(Type type, InteropAccessMode accessMode, string friendlyName, IUserDataDescriptor descriptor)
		{
			accessMode = TypeDescriptorRegistry.ResolveDefaultAccessModeForType(accessMode, type);
			object obj = TypeDescriptorRegistry.s_Lock;
			IUserDataDescriptor result;
			lock (obj)
			{
				IUserDataDescriptor oldDescriptor = null;
				TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type, out oldDescriptor);
				if (descriptor == null)
				{
					if (TypeDescriptorRegistry.IsTypeBlacklisted(type))
					{
						result = null;
					}
					else if (Framework.Do.GetInterfaces(type).Any((Type ii) => ii == typeof(IUserDataType)))
					{
						AutoDescribingUserDataDescriptor newDescriptor = new AutoDescribingUserDataDescriptor(type, friendlyName);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor, oldDescriptor);
					}
					else if (Framework.Do.IsGenericTypeDefinition(type))
					{
						StandardGenericsUserDataDescriptor newDescriptor2 = new StandardGenericsUserDataDescriptor(type, accessMode);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor2, oldDescriptor);
					}
					else if (Framework.Do.IsEnum(type))
					{
						StandardEnumUserDataDescriptor newDescriptor3 = new StandardEnumUserDataDescriptor(type, friendlyName, null, null, null);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor3, oldDescriptor);
					}
					else
					{
						StandardUserDataDescriptor udd = new StandardUserDataDescriptor(type, accessMode, friendlyName);
						if (accessMode == InteropAccessMode.BackgroundOptimized)
						{
							ThreadPool.QueueUserWorkItem(delegate(object o)
							{
								((IOptimizableDescriptor)udd).Optimize();
							});
						}
						result = TypeDescriptorRegistry.PerformRegistration(type, udd, oldDescriptor);
					}
				}
				else
				{
					TypeDescriptorRegistry.PerformRegistration(type, descriptor, oldDescriptor);
					result = descriptor;
				}
			}
			return result;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0011CA10 File Offset: 0x0011AC10
		private static IUserDataDescriptor PerformRegistration(Type type, IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			IUserDataDescriptor userDataDescriptor = TypeDescriptorRegistry.RegistrationPolicy.HandleRegistration(newDescriptor, oldDescriptor);
			if (userDataDescriptor != oldDescriptor)
			{
				if (userDataDescriptor == null)
				{
					TypeDescriptorRegistry.s_TypeRegistry.Remove(type);
				}
				else
				{
					TypeDescriptorRegistry.s_TypeRegistry[type] = userDataDescriptor;
					TypeDescriptorRegistry.s_TypeRegistryHistory[type] = userDataDescriptor;
				}
			}
			return userDataDescriptor;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0011CA58 File Offset: 0x0011AC58
		internal static InteropAccessMode ResolveDefaultAccessModeForType(InteropAccessMode accessMode, Type type)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				MoonSharpUserDataAttribute moonSharpUserDataAttribute = Framework.Do.GetCustomAttributes(type, true).OfType<MoonSharpUserDataAttribute>().SingleOrDefault<MoonSharpUserDataAttribute>();
				if (moonSharpUserDataAttribute != null)
				{
					accessMode = moonSharpUserDataAttribute.AccessMode;
				}
			}
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = TypeDescriptorRegistry.s_DefaultAccessMode;
			}
			return accessMode;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0011CA98 File Offset: 0x0011AC98
		internal static IUserDataDescriptor GetDescriptorForType(Type type, bool searchInterfaces)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			IUserDataDescriptor result;
			lock (obj)
			{
				IUserDataDescriptor userDataDescriptor = null;
				if (TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(type))
				{
					result = TypeDescriptorRegistry.s_TypeRegistry[type];
				}
				else if (TypeDescriptorRegistry.RegistrationPolicy.AllowTypeAutoRegistration(type) && !Framework.Do.IsAssignableFrom(typeof(Delegate), type))
				{
					result = TypeDescriptorRegistry.RegisterType_Impl(type, TypeDescriptorRegistry.DefaultAccessMode, type.FullName, null);
				}
				else
				{
					Type type2 = type;
					while (type2 != null)
					{
						IUserDataDescriptor userDataDescriptor2;
						if (TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type2, out userDataDescriptor2))
						{
							userDataDescriptor = userDataDescriptor2;
							break;
						}
						if (Framework.Do.IsGenericType(type2) && TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type2.GetGenericTypeDefinition(), out userDataDescriptor2))
						{
							userDataDescriptor = userDataDescriptor2;
							break;
						}
						type2 = Framework.Do.GetBaseType(type2);
					}
					if (userDataDescriptor is IGeneratorUserDataDescriptor)
					{
						userDataDescriptor = ((IGeneratorUserDataDescriptor)userDataDescriptor).Generate(type);
					}
					if (!searchInterfaces)
					{
						result = userDataDescriptor;
					}
					else
					{
						List<IUserDataDescriptor> list = new List<IUserDataDescriptor>();
						if (userDataDescriptor != null)
						{
							list.Add(userDataDescriptor);
						}
						if (searchInterfaces)
						{
							foreach (Type type3 in Framework.Do.GetInterfaces(type))
							{
								IUserDataDescriptor userDataDescriptor3;
								if (TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type3, out userDataDescriptor3))
								{
									if (userDataDescriptor3 is IGeneratorUserDataDescriptor)
									{
										userDataDescriptor3 = ((IGeneratorUserDataDescriptor)userDataDescriptor3).Generate(type);
									}
									if (userDataDescriptor3 != null)
									{
										list.Add(userDataDescriptor3);
									}
								}
								else if (Framework.Do.IsGenericType(type3) && TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type3.GetGenericTypeDefinition(), out userDataDescriptor3))
								{
									if (userDataDescriptor3 is IGeneratorUserDataDescriptor)
									{
										userDataDescriptor3 = ((IGeneratorUserDataDescriptor)userDataDescriptor3).Generate(type);
									}
									if (userDataDescriptor3 != null)
									{
										list.Add(userDataDescriptor3);
									}
								}
							}
						}
						if (list.Count == 1)
						{
							result = list[0];
						}
						else if (list.Count == 0)
						{
							result = null;
						}
						else
						{
							result = new CompositeUserDataDescriptor(list, type);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x00024714 File Offset: 0x00022914
		private static bool FrameworkIsAssignableFrom(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000247BB File Offset: 0x000229BB
		public static bool IsTypeBlacklisted(Type t)
		{
			return Framework.Do.IsValueType(t) && Framework.Do.GetInterfaces(t).Contains(typeof(IEnumerator));
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x0011CCA4 File Offset: 0x0011AEA4
		public static IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> RegisteredTypes
		{
			get
			{
				object obj = TypeDescriptorRegistry.s_Lock;
				IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> result;
				lock (obj)
				{
					result = TypeDescriptorRegistry.s_TypeRegistry.ToArray<KeyValuePair<Type, IUserDataDescriptor>>();
				}
				return result;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x0011CCEC File Offset: 0x0011AEEC
		public static IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> RegisteredTypesHistory
		{
			get
			{
				object obj = TypeDescriptorRegistry.s_Lock;
				IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> result;
				lock (obj)
				{
					result = TypeDescriptorRegistry.s_TypeRegistryHistory.ToArray<KeyValuePair<Type, IUserDataDescriptor>>();
				}
				return result;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000247E9 File Offset: 0x000229E9
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000247F0 File Offset: 0x000229F0
		internal static IRegistrationPolicy RegistrationPolicy { get; set; }

		// Token: 0x04002E6A RID: 11882
		private static object s_Lock = new object();

		// Token: 0x04002E6B RID: 11883
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistry = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x04002E6C RID: 11884
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistryHistory = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x04002E6D RID: 11885
		private static InteropAccessMode s_DefaultAccessMode;
	}
}

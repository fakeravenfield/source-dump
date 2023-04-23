using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lua.Proxy;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;
using MoonSharp.Interpreter.Interop.StandardDescriptors;
using MoonSharp.Interpreter.Interop.UserDataRegistries;
using MoonSharp.Interpreter.Serialization.Json;
using UnityEngine;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007BF RID: 1983
	public class UserData : RefIdObject
	{
		// Token: 0x06003150 RID: 12624 RVA: 0x0002200F File Offset: 0x0002020F
		private UserData()
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x00022017 File Offset: 0x00020217
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x0002201F File Offset: 0x0002021F
		public DynValue UserValue { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x00022028 File Offset: 0x00020228
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x00022030 File Offset: 0x00020230
		public object Object { get; private set; }

		// Token: 0x06003155 RID: 12629 RVA: 0x0010F3D8 File Offset: 0x0010D5D8
		public bool ProxyValueReferenceEvaluatesToNil()
		{
			IProxy proxy = this.Object as IProxy;
			if (proxy != null)
			{
				object value = proxy.GetValue();
				if (value != null && value is UnityEngine.Object)
				{
					return !(value as UnityEngine.Object);
				}
			}
			return false;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x00022039 File Offset: 0x00020239
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x00022041 File Offset: 0x00020241
		public IUserDataDescriptor Descriptor { get; private set; }

		// Token: 0x06003158 RID: 12632 RVA: 0x0002204A File Offset: 0x0002024A
		static UserData()
		{
			UserData.RegisterType<EventFacade>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<AnonWrapper>(InteropAccessMode.HideMembers, null);
			UserData.RegisterType<EnumerableWrapper>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<JsonNull>(InteropAccessMode.Reflection, null);
			UserData.DefaultAccessMode = InteropAccessMode.LazyOptimized;
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0002207C File Offset: 0x0002027C
		public static IUserDataDescriptor RegisterType<T>(InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), accessMode, friendlyName, null);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x00022090 File Offset: 0x00020290
		public static IUserDataDescriptor RegisterType(Type type, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, accessMode, friendlyName, null);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x0002209B File Offset: 0x0002029B
		public static IUserDataDescriptor RegisterProxyType(IProxyFactory proxyFactory, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterProxyType_Impl(proxyFactory, accessMode, friendlyName);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000220A5 File Offset: 0x000202A5
		public static IUserDataDescriptor RegisterProxyType<TProxy, TTarget>(Func<TTarget, TProxy> wrapDelegate, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null) where TProxy : class where TTarget : class
		{
			return UserData.RegisterProxyType(new DelegateProxyFactory<TProxy, TTarget>(wrapDelegate), accessMode, friendlyName);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000220B4 File Offset: 0x000202B4
		public static IUserDataDescriptor RegisterType<T>(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000220C8 File Offset: 0x000202C8
		public static IUserDataDescriptor RegisterType(Type type, IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000220D3 File Offset: 0x000202D3
		public static IUserDataDescriptor RegisterType(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(customDescriptor.Type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000220E3 File Offset: 0x000202E3
		public static void RegisterAssembly(Assembly asm = null, bool includeExtensionTypes = false)
		{
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}
			TypeDescriptorRegistry.RegisterAssembly(asm, includeExtensionTypes);
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000220FC File Offset: 0x000202FC
		public static bool IsTypeRegistered(Type t)
		{
			return TypeDescriptorRegistry.IsTypeRegistered(t);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x00022104 File Offset: 0x00020304
		public static bool IsTypeRegistered<T>()
		{
			return TypeDescriptorRegistry.IsTypeRegistered(typeof(T));
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00022115 File Offset: 0x00020315
		public static void UnregisterType<T>()
		{
			TypeDescriptorRegistry.UnregisterType(typeof(T));
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x00022126 File Offset: 0x00020326
		public static void UnregisterType(Type t)
		{
			TypeDescriptorRegistry.UnregisterType(t);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0002212E File Offset: 0x0002032E
		public static DynValue Create(object o, IUserDataDescriptor descr)
		{
			return DynValue.NewUserData(new UserData
			{
				Descriptor = descr,
				Object = o
			});
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x0010F418 File Offset: 0x0010D618
		public static DynValue Create(object o)
		{
			IUserDataDescriptor descriptorForObject = UserData.GetDescriptorForObject(o);
			if (descriptorForObject != null)
			{
				return UserData.Create(o, descriptorForObject);
			}
			if (o is Type)
			{
				return UserData.CreateStatic((Type)o);
			}
			return null;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00022148 File Offset: 0x00020348
		public static DynValue CreateStatic(IUserDataDescriptor descr)
		{
			if (descr == null)
			{
				return null;
			}
			return DynValue.NewUserData(new UserData
			{
				Descriptor = descr,
				Object = null
			});
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00022167 File Offset: 0x00020367
		public static DynValue CreateStatic(Type t)
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(t, false));
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x00022175 File Offset: 0x00020375
		public static DynValue CreateStatic<T>()
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(typeof(T), false));
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x0002218C File Offset: 0x0002038C
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x00022193 File Offset: 0x00020393
		public static IRegistrationPolicy RegistrationPolicy
		{
			get
			{
				return TypeDescriptorRegistry.RegistrationPolicy;
			}
			set
			{
				TypeDescriptorRegistry.RegistrationPolicy = value;
			}
		} = InteropRegistrationPolicy.Default;

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x0002219B File Offset: 0x0002039B
		// (set) Token: 0x0600316D RID: 12653 RVA: 0x000221A2 File Offset: 0x000203A2
		public static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return TypeDescriptorRegistry.DefaultAccessMode;
			}
			set
			{
				TypeDescriptorRegistry.DefaultAccessMode = value;
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000221AA File Offset: 0x000203AA
		public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
		{
			ExtensionMethodsRegistry.RegisterExtensionType(type, mode);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000221B3 File Offset: 0x000203B3
		public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsByNameAndType(name, extendedType);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000221BC File Offset: 0x000203BC
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsChangeVersion();
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000221C3 File Offset: 0x000203C3
		public static IUserDataDescriptor GetDescriptorForType<T>(bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(typeof(T), searchInterfaces);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000221D5 File Offset: 0x000203D5
		public static IUserDataDescriptor GetDescriptorForType(Type type, bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(type, searchInterfaces);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000221DE File Offset: 0x000203DE
		public static IUserDataDescriptor GetDescriptorForObject(object o)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(o.GetType(), true);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0010F44C File Offset: 0x0010D64C
		public static Table GetDescriptionOfRegisteredTypes(bool useHistoricalData = false)
		{
			DynValue dynValue = DynValue.NewPrimeTable();
			foreach (KeyValuePair<Type, IUserDataDescriptor> keyValuePair in (useHistoricalData ? TypeDescriptorRegistry.RegisteredTypesHistory : TypeDescriptorRegistry.RegisteredTypes))
			{
				IWireableDescriptor wireableDescriptor = keyValuePair.Value as IWireableDescriptor;
				if (wireableDescriptor != null)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(keyValuePair.Key.FullName, dynValue2);
					wireableDescriptor.PrepareForWiring(dynValue2.Table);
				}
			}
			return dynValue.Table;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000221EC File Offset: 0x000203EC
		public static IEnumerable<Type> GetRegisteredTypes(bool useHistoricalData = false)
		{
			return from p in useHistoricalData ? TypeDescriptorRegistry.RegisteredTypesHistory : TypeDescriptorRegistry.RegisteredTypes
			select p.Value.Type;
		}
	}
}

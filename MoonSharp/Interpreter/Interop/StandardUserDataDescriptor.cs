﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200086F RID: 2159
	public class StandardUserDataDescriptor : DispatchingUserDataDescriptor, IWireableDescriptor
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000246EF File Offset: 0x000228EF
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x000246F7 File Offset: 0x000228F7
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x060035D6 RID: 13782 RVA: 0x0011BC98 File Offset: 0x00119E98
		public StandardUserDataDescriptor(Type type, InteropAccessMode accessMode, string friendlyName = null) : base(type, friendlyName)
		{
			if (accessMode == InteropAccessMode.NoReflectionAllowed)
			{
				throw new ArgumentException("Can't create a StandardUserDataDescriptor under a NoReflectionAllowed access mode");
			}
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = UserData.DefaultAccessMode;
			}
			this.AccessMode = accessMode;
			this.FillMemberList();
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0011BCE8 File Offset: 0x00119EE8
		private void FillMemberList()
		{
			HashSet<string> hashSet = new HashSet<string>(from a in Framework.Do.GetCustomAttributes(base.Type, typeof(MoonSharpHideMemberAttribute), true).OfType<MoonSharpHideMemberAttribute>()
			select a.MemberName);
			Type type = base.Type;
			if (this.AccessMode == InteropAccessMode.HideMembers)
			{
				return;
			}
			if (!type.IsDelegateType())
			{
				foreach (ConstructorInfo methodBase in Framework.Do.GetConstructors(type))
				{
					if (!hashSet.Contains("new"))
					{
						base.AddMember("new", MethodMemberDescriptor.TryCreateIfVisible(methodBase, this.AccessMode, false));
					}
				}
				if (Framework.Do.IsValueType(type) && !hashSet.Contains("new"))
				{
					base.AddMember("new", new ValueTypeDefaultCtorMemberDescriptor(type));
				}
			}
			foreach (MethodInfo methodInfo in Framework.Do.GetMethods(type))
			{
				if (!hashSet.Contains(methodInfo.Name))
				{
					MethodMemberDescriptor methodMemberDescriptor = MethodMemberDescriptor.TryCreateIfVisible(methodInfo, this.AccessMode, false);
					if (methodMemberDescriptor != null && MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, false))
					{
						string name = methodInfo.Name;
						if (methodInfo.IsSpecialName && (methodInfo.Name == "op_Explicit" || methodInfo.Name == "op_Implicit"))
						{
							name = methodInfo.ReturnType.GetConversionMethodName();
						}
						base.AddMember(name, methodMemberDescriptor);
						foreach (string name2 in methodInfo.GetMetaNamesFromAttributes())
						{
							base.AddMetaMember(name2, methodMemberDescriptor);
						}
					}
				}
			}
			foreach (PropertyInfo propertyInfo in Framework.Do.GetProperties(type))
			{
				if (!propertyInfo.IsSpecialName && !propertyInfo.GetIndexParameters().Any<ParameterInfo>() && !hashSet.Contains(propertyInfo.Name))
				{
					base.AddMember(propertyInfo.Name, PropertyMemberDescriptor.TryCreateIfVisible(propertyInfo, this.AccessMode));
				}
			}
			foreach (FieldInfo fieldInfo in Framework.Do.GetFields(type))
			{
				if (!fieldInfo.IsSpecialName && !hashSet.Contains(fieldInfo.Name))
				{
					base.AddMember(fieldInfo.Name, FieldMemberDescriptor.TryCreateIfVisible(fieldInfo, this.AccessMode));
				}
			}
			foreach (EventInfo eventInfo in Framework.Do.GetEvents(type))
			{
				if (!eventInfo.IsSpecialName && !hashSet.Contains(eventInfo.Name))
				{
					base.AddMember(eventInfo.Name, EventMemberDescriptor.TryCreateIfVisible(eventInfo, this.AccessMode));
				}
			}
			foreach (Type type2 in Framework.Do.GetNestedTypes(type))
			{
				if (!hashSet.Contains(type2.Name) && !Framework.Do.IsGenericTypeDefinition(type2) && (Framework.Do.IsNestedPublic(type2) || Framework.Do.GetCustomAttributes(type2, typeof(MoonSharpUserDataAttribute), true).Length != 0) && UserData.RegisterType(type2, this.AccessMode, null) != null)
				{
					base.AddDynValue(type2.Name, UserData.CreateStatic(type2));
				}
			}
			if (!hashSet.Contains("[this]"))
			{
				if (base.Type.IsArray)
				{
					int arrayRank = base.Type.GetArrayRank();
					ParameterDescriptor[] array = new ParameterDescriptor[arrayRank];
					ParameterDescriptor[] array2 = new ParameterDescriptor[arrayRank + 1];
					for (int j = 0; j < arrayRank; j++)
					{
						array[j] = (array2[j] = new ParameterDescriptor("idx" + j.ToString(), typeof(int), false, null, false, false, false));
					}
					array2[arrayRank] = new ParameterDescriptor("value", base.Type.GetElementType(), false, null, false, false, false);
					base.AddMember("set_Item", new ArrayMemberDescriptor("set_Item", true, array2));
					base.AddMember("get_Item", new ArrayMemberDescriptor("get_Item", false, array));
					return;
				}
				if (base.Type == typeof(Array))
				{
					base.AddMember("set_Item", new ArrayMemberDescriptor("set_Item", true));
					base.AddMember("get_Item", new ArrayMemberDescriptor("get_Item", false));
				}
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0011C170 File Offset: 0x0011A370
		public void PrepareForWiring(Table t)
		{
			if (this.AccessMode == InteropAccessMode.HideMembers || Framework.Do.GetAssembly(base.Type) == Framework.Do.GetAssembly(base.GetType()))
			{
				t.Set("skip", DynValue.NewBoolean(true));
				return;
			}
			t.Set("visibility", DynValue.NewString(base.Type.GetClrVisibility()));
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			DynValue dynValue = DynValue.NewPrimeTable();
			t.Set("members", dynValue);
			DynValue dynValue2 = DynValue.NewPrimeTable();
			t.Set("metamembers", dynValue2);
			this.Serialize(dynValue.Table, base.Members);
			this.Serialize(dynValue2.Table, base.MetaMembers);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0011C240 File Offset: 0x0011A440
		private void Serialize(Table t, IEnumerable<KeyValuePair<string, IMemberDescriptor>> members)
		{
			foreach (KeyValuePair<string, IMemberDescriptor> keyValuePair in members)
			{
				IWireableDescriptor wireableDescriptor = keyValuePair.Value as IWireableDescriptor;
				if (wireableDescriptor != null)
				{
					DynValue dynValue = DynValue.NewPrimeTable();
					t.Set(keyValuePair.Key, dynValue);
					wireableDescriptor.PrepareForWiring(dynValue.Table);
				}
				else
				{
					t.Set(keyValuePair.Key, DynValue.NewString("unsupported member type : " + keyValuePair.Value.GetType().FullName));
				}
			}
		}
	}
}

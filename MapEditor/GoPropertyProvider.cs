using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000645 RID: 1605
	public class GoPropertyProvider : PropertyProvider
	{
		// Token: 0x060028FB RID: 10491 RVA: 0x0001C3A5 File Offset: 0x0001A5A5
		public void SetTarget(object target)
		{
			if (this.target != null)
			{
				Debug.LogError("Target already set");
				return;
			}
			if (this.bindings != null)
			{
				Debug.LogError("Bindings already created");
				return;
			}
			this.target = target;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000FBC68 File Offset: 0x000F9E68
		protected override PropertyBinding[] CreateBindings()
		{
			if (this.target == null)
			{
				Debug.LogError("Target not set");
				return null;
			}
			List<PropertyBinding> list = new List<PropertyBinding>();
			list.AddRange(this.CreateBindings(this.target));
			return (from b in list
			orderby b.attribute.order, b.name
			select b).ToArray<PropertyBinding>();
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000FBCF0 File Offset: 0x000F9EF0
		private PropertyBinding[] CreateBindings(object target)
		{
			List<PropertyBinding> list = new List<PropertyBinding>();
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			Type type = target.GetType();
			foreach (FieldInfo fieldInfo in type.GetFields(bindingAttr))
			{
				if (this.IsSerializable(type, fieldInfo.FieldType, fieldInfo))
				{
					PropertyBinding item = this.CreateBinding(target, fieldInfo);
					list.Add(item);
				}
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties(bindingAttr))
			{
				if (propertyInfo.CanRead && propertyInfo.CanWrite && this.IsSerializable(type, propertyInfo.PropertyType, propertyInfo))
				{
					PropertyBinding item2 = this.CreateBinding(target, propertyInfo);
					list.Add(item2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000FBDB4 File Offset: 0x000F9FB4
		protected virtual PropertyBinding CreateBinding(object target, FieldInfo member)
		{
			ShowInMapEditorAttribute attribute = this.ConvertAttributes(member);
			return new PropertyBinding(target, member, attribute);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000FBDD4 File Offset: 0x000F9FD4
		protected virtual PropertyBinding CreateBinding(object target, PropertyInfo member)
		{
			ShowInMapEditorAttribute attribute = this.ConvertAttributes(member);
			return new PropertyBinding(target, member, attribute);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000FBDF4 File Offset: 0x000F9FF4
		protected ShowInMapEditorAttribute ConvertAttributes(MemberInfo info)
		{
			ShowInMapEditorAttribute showInMapEditorAttribute = PropertyBinding.GetAttribute<ShowInMapEditorAttribute>(info);
			if (showInMapEditorAttribute == null)
			{
				showInMapEditorAttribute = new ShowInMapEditorAttribute();
			}
			return showInMapEditorAttribute;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000FBE14 File Offset: 0x000FA014
		protected bool IsSerializable(Type targetType, Type valueType, MemberInfo info)
		{
			return !info.IsDefined(typeof(NonSerializedAttribute), true) && (!(info.DeclaringType != targetType) || !(info.Name != "name")) && (GoPropertyProvider.GetTypeSerializer(valueType) != null || valueType.IsEnum);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000FBE70 File Offset: 0x000FA070
		public static string Serialize(PropertyBinding property)
		{
			object value = property.GetValue();
			if (value == null)
			{
				return "";
			}
			ITypeSerializer typeSerializer = GoPropertyProvider.GetTypeSerializer(property.type);
			if (typeSerializer != null)
			{
				return typeSerializer.Serialize(value);
			}
			return value.ToString();
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000FBEAC File Offset: 0x000FA0AC
		public static bool Deserialize(PropertyBinding property, string serializedValue, GoPropertyProvider.DeserializeContext ctx)
		{
			object obj = null;
			Type type = property.type;
			ITypeSerializer typeSerializer = GoPropertyProvider.GetTypeSerializer(property.type);
			if (typeSerializer != null)
			{
				obj = typeSerializer.Deserialize(serializedValue, ctx);
			}
			else
			{
				if (type.IsEnum)
				{
					try
					{
						obj = Enum.Parse(type, serializedValue);
						goto IL_3A;
					}
					catch
					{
						goto IL_3A;
					}
				}
				obj = null;
			}
			IL_3A:
			if (obj != null)
			{
				property.SetValue(obj);
			}
			return obj != null;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
		private static ITypeSerializer GetTypeSerializer(Type typeToSerialize)
		{
			if (GoPropertyProvider.typeSerializers == null)
			{
				GoPropertyProvider.LoadTypeSerializers();
			}
			if (GoPropertyProvider.typeSerializers.ContainsKey(typeToSerialize))
			{
				return GoPropertyProvider.typeSerializers[typeToSerialize];
			}
			return null;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000FBF14 File Offset: 0x000FA114
		private static void LoadTypeSerializers()
		{
			GoPropertyProvider.typeSerializers = new Dictionary<Type, ITypeSerializer>();
			foreach (Type type in Assembly.GetAssembly(typeof(TypeSerializerAttribute)).GetTypes())
			{
				object obj = type.GetCustomAttributes(typeof(TypeSerializerAttribute), true).FirstOrDefault<object>();
				if (obj != null)
				{
					Type serializedType = ((TypeSerializerAttribute)obj).serializedType;
					ITypeSerializer value = (ITypeSerializer)Activator.CreateInstance(type);
					GoPropertyProvider.typeSerializers.Add(serializedType, value);
				}
			}
		}

		// Token: 0x040026D1 RID: 9937
		private static Dictionary<Type, ITypeSerializer> typeSerializers;

		// Token: 0x040026D2 RID: 9938
		protected object target;

		// Token: 0x02000646 RID: 1606
		public class DeserializeContext
		{
			// Token: 0x040026D3 RID: 9939
			public MaterialList materialList;
		}
	}
}

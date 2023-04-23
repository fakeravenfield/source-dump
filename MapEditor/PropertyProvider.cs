using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000653 RID: 1619
	public class PropertyProvider : MonoBehaviour
	{
		// Token: 0x0600293B RID: 10555 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		public PropertyBinding[] GetBindings()
		{
			if (this.bindings == null)
			{
				this.bindings = this.CreateBindings();
			}
			return this.bindings;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000FC700 File Offset: 0x000FA900
		public void SetValue(string memberName, object value)
		{
			foreach (PropertyBinding propertyBinding in this.GetBindings())
			{
				if (propertyBinding.memberName == memberName)
				{
					propertyBinding.SetValue(value);
					return;
				}
			}
			Debug.LogError("Unable to find property: " + memberName);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000FC754 File Offset: 0x000FA954
		protected virtual PropertyBinding[] CreateBindings()
		{
			List<PropertyBinding> list = new List<PropertyBinding>();
			foreach (IPropertyContainer target in base.GetComponentsInChildren<IPropertyContainer>())
			{
				list.AddRange(this.CreateBindings(target));
			}
			return (from b in list
			orderby b.attribute.order, b.name
			select b).ToArray<PropertyBinding>();
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000FC7DC File Offset: 0x000FA9DC
		private PropertyBinding[] CreateBindings(object target)
		{
			List<PropertyBinding> list = new List<PropertyBinding>();
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			Type type = target.GetType();
			Type typeFromHandle = typeof(ShowInMapEditorAttribute);
			foreach (FieldInfo fieldInfo in type.GetFields(bindingAttr))
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeFromHandle, true);
				if (customAttributes.Any<object>())
				{
					ShowInMapEditorAttribute attribute = customAttributes.First<object>() as ShowInMapEditorAttribute;
					PropertyBinding item = new PropertyBinding(target, fieldInfo, attribute);
					list.Add(item);
				}
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties(bindingAttr))
			{
				if (propertyInfo.CanRead && propertyInfo.CanWrite)
				{
					object[] customAttributes2 = propertyInfo.GetCustomAttributes(typeFromHandle, true);
					if (customAttributes2.Any<object>())
					{
						ShowInMapEditorAttribute attribute2 = customAttributes2.First<object>() as ShowInMapEditorAttribute;
						PropertyBinding item2 = new PropertyBinding(target, propertyInfo, attribute2);
						list.Add(item2);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000FC8D0 File Offset: 0x000FAAD0
		public static void CopyProperties(MonoBehaviour g1, MonoBehaviour g2, bool notify)
		{
			PropertyProvider orCreateComponent = g1.GetOrCreateComponent<PropertyProvider>();
			PropertyProvider orCreateComponent2 = g2.GetOrCreateComponent<PropertyProvider>();
			if (orCreateComponent && orCreateComponent2)
			{
				PropertyProvider.CopyProperties(orCreateComponent, orCreateComponent2, notify);
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000FC904 File Offset: 0x000FAB04
		public static void CopyProperties(PropertyProvider p1, PropertyProvider p2, bool notify)
		{
			IEnumerable<PropertyBinding> outer = p1.GetBindings();
			PropertyBinding[] inner = p2.GetBindings();
			foreach (var <>f__AnonymousType in from b1 in outer
			join b2 in inner on b1.name equals b2.name
			select new
			{
				b1,
				b2
			})
			{
				if (<>f__AnonymousType.b1.GetType() == <>f__AnonymousType.b2.GetType())
				{
					object value = <>f__AnonymousType.b1.GetValue();
					if (notify)
					{
						<>f__AnonymousType.b2.SetValue(value);
					}
					else
					{
						<>f__AnonymousType.b2.SetValueQuiet(value);
					}
				}
			}
		}

		// Token: 0x040026F3 RID: 9971
		protected PropertyBinding[] bindings;
	}
}

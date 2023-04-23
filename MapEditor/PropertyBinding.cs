using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200064D RID: 1613
	public struct PropertyBinding
	{
		// Token: 0x06002916 RID: 10518 RVA: 0x000FC05C File Offset: 0x000FA25C
		public PropertyBinding(object obj, FieldInfo field, ShowInMapEditorAttribute attribute)
		{
			this.memberName = field.Name;
			this.name = PropertyBinding.PickName(field.Name, attribute.name);
			this.type = field.FieldType;
			this.attribute = attribute;
			this.range = PropertyBinding.GetAttribute<RangeAttribute>(field);
			this.hide = (PropertyBinding.GetAttribute<HideInInspector>(field) != null);
			this.setValue = delegate(object v)
			{
				field.SetValue(obj, v);
			};
			this.getValue = (() => field.GetValue(obj));
			this.onChange = PropertyBinding.SetupOnChange(obj);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000FC11C File Offset: 0x000FA31C
		public PropertyBinding(object obj, PropertyInfo property, ShowInMapEditorAttribute attribute)
		{
			this.memberName = property.Name;
			this.name = PropertyBinding.PickName(property.Name, attribute.name);
			this.type = property.PropertyType;
			this.attribute = attribute;
			this.range = PropertyBinding.GetAttribute<RangeAttribute>(property);
			this.hide = (PropertyBinding.GetAttribute<HideInInspector>(property) != null);
			this.setValue = delegate(object v)
			{
				property.SetValue(obj, v, null);
			};
			this.getValue = (() => property.GetValue(obj, null));
			this.onChange = PropertyBinding.SetupOnChange(obj);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000FC1DC File Offset: 0x000FA3DC
		public PropertyBinding(Type valueType, MemberInfo member, Action<object> setValue, Func<object> getValue, ShowInMapEditorAttribute attribute)
		{
			this.memberName = member.Name;
			this.name = PropertyBinding.PickName(member.Name, attribute.name);
			this.type = valueType;
			this.attribute = attribute;
			this.range = PropertyBinding.GetAttribute<RangeAttribute>(member);
			this.hide = (PropertyBinding.GetAttribute<HideInInspector>(member) != null);
			this.setValue = setValue;
			this.getValue = getValue;
			this.onChange = PropertyBinding.SetupOnChange(null);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000FC254 File Offset: 0x000FA454
		public PropertyBinding(Type valueType, string name, Action<object> setValue, Func<object> getValue, ShowInMapEditorAttribute attribute)
		{
			this.memberName = name;
			this.name = PropertyBinding.PickName(name, attribute.name);
			this.type = valueType;
			this.attribute = attribute;
			this.range = null;
			this.hide = false;
			this.setValue = setValue;
			this.getValue = getValue;
			this.onChange = PropertyBinding.SetupOnChange(null);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000FC2B4 File Offset: 0x000FA4B4
		public PropertyBinding(PropertyBinding[] bindings)
		{
			if (bindings == null || bindings.Length < 1)
			{
				throw new ArgumentException("At-least one binding is required", "bindings");
			}
			PropertyBinding template = bindings.First<PropertyBinding>();
			PropertyBinding[] bindings2 = bindings;
			for (int i = 0; i < bindings2.Length; i++)
			{
				if (bindings2[i].type != template.type)
				{
					throw new ArgumentException("All bindings must be same type", "bindings");
				}
			}
			this.memberName = template.memberName;
			this.name = string.Format("{0} [{1}]", template.name, bindings.Length);
			this.type = template.type;
			this.attribute = template.attribute;
			this.range = template.range;
			this.hide = template.hide;
			this.setValue = delegate(object v)
			{
				PropertyBinding[] bindings3 = bindings;
				for (int j = 0; j < bindings3.Length; j++)
				{
					bindings3[j].setValue(v);
				}
			};
			this.getValue = (() => template.getValue());
			this.onChange = delegate()
			{
				PropertyBinding[] bindings3 = bindings;
				for (int j = 0; j < bindings3.Length; j++)
				{
					bindings3[j].onChange();
				}
			};
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0001C473 File Offset: 0x0001A673
		public object GetValue()
		{
			return this.getValue();
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0001C480 File Offset: 0x0001A680
		public void SetValue(object value)
		{
			this.setValue(value);
			this.onChange();
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0001C499 File Offset: 0x0001A699
		public void SetValueQuiet(object value)
		{
			this.setValue(value);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000FC3F8 File Offset: 0x000FA5F8
		private static Action SetupOnChange(object obj)
		{
			IPropertyChangeNotify propertyChangeNotify = obj as IPropertyChangeNotify;
			if (propertyChangeNotify != null)
			{
				return new Action(propertyChangeNotify.OnPropertyChanged);
			}
			return delegate()
			{
			};
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0001C4A7 File Offset: 0x0001A6A7
		private static string PickName(string fromType, string fromAttribute)
		{
			if (!string.IsNullOrEmpty(fromAttribute))
			{
				return PropertyBinding.TitleCase(fromAttribute);
			}
			return PropertyBinding.TitleCase(fromType);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0001C4BE File Offset: 0x0001A6BE
		private static string TitleCase(string s)
		{
			return new string(PropertyBinding.TitleCaseEnumerable(s).ToArray<char>());
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		private static IEnumerable<char> TitleCaseEnumerable(string s)
		{
			char c2 = ' ';
			foreach (char c in s)
			{
				if (c2 == ' ')
				{
					yield return char.ToUpper(c);
				}
				else if (char.IsUpper(c) && char.IsLower(c2))
				{
					yield return ' ';
					yield return c;
				}
				else
				{
					yield return c;
				}
				c2 = c;
			}
			string text = null;
			yield break;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		public static T GetAttribute<T>(MemberInfo member) where T : Attribute
		{
			return member.GetCustomAttributes(typeof(T), true).FirstOrDefault<object>() as T;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000FC43C File Offset: 0x000FA63C
		public override bool Equals(object obj)
		{
			if (!(obj is PropertyBinding))
			{
				return false;
			}
			PropertyBinding propertyBinding = (PropertyBinding)obj;
			return this.memberName == propertyBinding.memberName && this.name == propertyBinding.name && EqualityComparer<Type>.Default.Equals(this.type, propertyBinding.type) && EqualityComparer<ShowInMapEditorAttribute>.Default.Equals(this.attribute, propertyBinding.attribute) && EqualityComparer<RangeAttribute>.Default.Equals(this.range, propertyBinding.range) && this.hide == propertyBinding.hide && EqualityComparer<Action<object>>.Default.Equals(this.setValue, propertyBinding.setValue) && EqualityComparer<Func<object>>.Default.Equals(this.getValue, propertyBinding.getValue) && EqualityComparer<Action>.Default.Equals(this.onChange, propertyBinding.onChange);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x0001C502 File Offset: 0x0001A702
		public static bool operator ==(PropertyBinding a, PropertyBinding b)
		{
			return a.Equals(b);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0001C517 File Offset: 0x0001A717
		public static bool operator !=(PropertyBinding a, PropertyBinding b)
		{
			return !a.Equals(b);
		}

		// Token: 0x040026DA RID: 9946
		public readonly string memberName;

		// Token: 0x040026DB RID: 9947
		public readonly string name;

		// Token: 0x040026DC RID: 9948
		public readonly Type type;

		// Token: 0x040026DD RID: 9949
		public readonly ShowInMapEditorAttribute attribute;

		// Token: 0x040026DE RID: 9950
		public readonly RangeAttribute range;

		// Token: 0x040026DF RID: 9951
		public readonly bool hide;

		// Token: 0x040026E0 RID: 9952
		private readonly Action<object> setValue;

		// Token: 0x040026E1 RID: 9953
		private readonly Func<object> getValue;

		// Token: 0x040026E2 RID: 9954
		private readonly Action onChange;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapEditor
{
	// Token: 0x0200064B RID: 1611
	public class PrefabPropertyProvider : GoPropertyProvider
	{
		// Token: 0x0600290F RID: 10511 RVA: 0x000FBF94 File Offset: 0x000FA194
		protected override PropertyBinding CreateBinding(object target, FieldInfo member)
		{
			base.ConvertAttributes(member);
			object value = member.GetValue(target);
			return this.CreateBinding(member.FieldType, member, value);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000FBFC0 File Offset: 0x000FA1C0
		protected override PropertyBinding CreateBinding(object target, PropertyInfo member)
		{
			base.ConvertAttributes(member);
			object value = member.GetValue(target, null);
			return this.CreateBinding(member.PropertyType, member, value);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000FBFEC File Offset: 0x000FA1EC
		private PropertyBinding CreateBinding(Type valueType, MemberInfo member, object defaultValue)
		{
			Action<object> setValue = delegate(object v)
			{
				this.values[member.Name] = v;
			};
			Func<object> getValue = () => this.values[member.Name];
			this.values[member.Name] = defaultValue;
			ShowInMapEditorAttribute attribute = base.ConvertAttributes(member);
			return new PropertyBinding(valueType, member, setValue, getValue, attribute);
		}

		// Token: 0x040026D7 RID: 9943
		private Dictionary<string, object> values = new Dictionary<string, object>();
	}
}

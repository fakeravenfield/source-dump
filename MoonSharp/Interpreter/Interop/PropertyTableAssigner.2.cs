using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200083C RID: 2108
	public class PropertyTableAssigner : IPropertyTableAssigner
	{
		// Token: 0x06003450 RID: 13392 RVA: 0x00117B60 File Offset: 0x00115D60
		public PropertyTableAssigner(Type type, params string[] expectedMissingProperties)
		{
			this.m_Type = type;
			if (Framework.Do.IsValueType(this.m_Type))
			{
				throw new ArgumentException("Type cannot be a value type.");
			}
			foreach (string key in expectedMissingProperties)
			{
				this.m_PropertyMap.Add(key, null);
			}
			foreach (PropertyInfo propertyInfo in Framework.Do.GetProperties(this.m_Type))
			{
				foreach (MoonSharpPropertyAttribute moonSharpPropertyAttribute in propertyInfo.GetCustomAttributes(true).OfType<MoonSharpPropertyAttribute>())
				{
					string text = moonSharpPropertyAttribute.Name ?? propertyInfo.Name;
					if (this.m_PropertyMap.ContainsKey(text))
					{
						throw new ArgumentException(string.Format("Type {0} has two definitions for MoonSharp property {1}", this.m_Type.FullName, text));
					}
					this.m_PropertyMap.Add(text, propertyInfo);
				}
			}
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00023C62 File Offset: 0x00021E62
		public void AddExpectedMissingProperty(string name)
		{
			this.m_PropertyMap.Add(name, null);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x00117C8C File Offset: 0x00115E8C
		private bool TryAssignProperty(object obj, string name, DynValue value)
		{
			if (this.m_PropertyMap.ContainsKey(name))
			{
				PropertyInfo propertyInfo = this.m_PropertyMap[name];
				if (propertyInfo != null)
				{
					object obj2;
					if (value.Type == DataType.Table && this.m_SubAssigners.ContainsKey(propertyInfo.PropertyType))
					{
						IPropertyTableAssigner propertyTableAssigner = this.m_SubAssigners[propertyInfo.PropertyType];
						obj2 = Activator.CreateInstance(propertyInfo.PropertyType);
						propertyTableAssigner.AssignObjectUnchecked(obj2, value.Table);
					}
					else
					{
						obj2 = ScriptToClrConversions.DynValueToObjectOfType(value, propertyInfo.PropertyType, null, false);
					}
					Framework.Do.GetSetMethod(propertyInfo).Invoke(obj, new object[]
					{
						obj2
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x00117D38 File Offset: 0x00115F38
		private void AssignProperty(object obj, string name, DynValue value)
		{
			if (this.TryAssignProperty(obj, name, value))
			{
				return;
			}
			if (this.TryAssignProperty(obj, DescriptorHelpers.UpperFirstLetter(name), value))
			{
				return;
			}
			if (this.TryAssignProperty(obj, DescriptorHelpers.Camelify(name), value))
			{
				return;
			}
			if (this.TryAssignProperty(obj, DescriptorHelpers.UpperFirstLetter(DescriptorHelpers.Camelify(name)), value))
			{
				return;
			}
			throw new ScriptRuntimeException("Invalid property {0}", new object[]
			{
				name
			});
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x00117DA0 File Offset: 0x00115FA0
		public void AssignObject(object obj, Table data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("Object is null");
			}
			if (!Framework.Do.IsInstanceOfType(this.m_Type, obj))
			{
				throw new ArgumentException(string.Format("Invalid type of object : got '{0}', expected {1}", obj.GetType().FullName, this.m_Type.FullName));
			}
			foreach (TablePair tablePair in data.Pairs)
			{
				if (tablePair.Key.Type != DataType.String)
				{
					throw new ScriptRuntimeException("Invalid property of type {0}", new object[]
					{
						tablePair.Key.Type.ToErrorTypeString()
					});
				}
				this.AssignProperty(obj, tablePair.Key.String, tablePair.Value);
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00117E7C File Offset: 0x0011607C
		public void SetSubassignerForType(Type propertyType, IPropertyTableAssigner assigner)
		{
			if (Framework.Do.IsAbstract(propertyType) || Framework.Do.IsGenericType(propertyType) || Framework.Do.IsInterface(propertyType) || Framework.Do.IsValueType(propertyType))
			{
				throw new ArgumentException("propertyType must be a concrete, reference type");
			}
			this.m_SubAssigners[propertyType] = assigner;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00023C71 File Offset: 0x00021E71
		void IPropertyTableAssigner.AssignObjectUnchecked(object obj, Table data)
		{
			this.AssignObject(obj, data);
		}

		// Token: 0x04002DB0 RID: 11696
		private Type m_Type;

		// Token: 0x04002DB1 RID: 11697
		private Dictionary<string, PropertyInfo> m_PropertyMap = new Dictionary<string, PropertyInfo>();

		// Token: 0x04002DB2 RID: 11698
		private Dictionary<Type, IPropertyTableAssigner> m_SubAssigners = new Dictionary<Type, IPropertyTableAssigner>();
	}
}

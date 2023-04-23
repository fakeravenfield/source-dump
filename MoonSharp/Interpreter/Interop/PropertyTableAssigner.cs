using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200083B RID: 2107
	public class PropertyTableAssigner<T> : IPropertyTableAssigner
	{
		// Token: 0x06003449 RID: 13385 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public PropertyTableAssigner(params string[] expectedMissingProperties)
		{
			this.m_InternalAssigner = new PropertyTableAssigner(typeof(T), expectedMissingProperties);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00023C02 File Offset: 0x00021E02
		public void AddExpectedMissingProperty(string name)
		{
			this.m_InternalAssigner.AddExpectedMissingProperty(name);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x00023C10 File Offset: 0x00021E10
		public void AssignObject(T obj, Table data)
		{
			this.m_InternalAssigner.AssignObject(obj, data);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00023C24 File Offset: 0x00021E24
		public PropertyTableAssigner GetTypeUnsafeAssigner()
		{
			return this.m_InternalAssigner;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x00023C2C File Offset: 0x00021E2C
		public void SetSubassignerForType(Type propertyType, IPropertyTableAssigner assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(propertyType, assigner);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x00023C3B File Offset: 0x00021E3B
		public void SetSubassigner<SubassignerType>(PropertyTableAssigner<SubassignerType> assigner)
		{
			this.m_InternalAssigner.SetSubassignerForType(typeof(SubassignerType), assigner);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x00023C53 File Offset: 0x00021E53
		void IPropertyTableAssigner.AssignObjectUnchecked(object o, Table data)
		{
			this.AssignObject((T)((object)o), data);
		}

		// Token: 0x04002DAF RID: 11695
		private PropertyTableAssigner m_InternalAssigner;
	}
}

using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000843 RID: 2115
	public class CompositeUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06003467 RID: 13415 RVA: 0x00023CFC File Offset: 0x00021EFC
		public CompositeUserDataDescriptor(List<IUserDataDescriptor> descriptors, Type type)
		{
			this.m_Descriptors = descriptors;
			this.m_Type = type;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x00023D12 File Offset: 0x00021F12
		public IList<IUserDataDescriptor> Descriptors
		{
			get
			{
				return this.m_Descriptors;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x00023D1A File Offset: 0x00021F1A
		public string Name
		{
			get
			{
				return "^" + this.m_Type.FullName;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x00023D31 File Offset: 0x00021F31
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x00118524 File Offset: 0x00116724
		public DynValue Index(Script script, object obj, DynValue index, bool isNameIndex)
		{
			foreach (IUserDataDescriptor userDataDescriptor in this.m_Descriptors)
			{
				DynValue dynValue = userDataDescriptor.Index(script, obj, index, isNameIndex);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x00118584 File Offset: 0x00116784
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isNameIndex)
		{
			using (List<IUserDataDescriptor>.Enumerator enumerator = this.m_Descriptors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.SetIndex(script, obj, index, value, isNameIndex))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00023D39 File Offset: 0x00021F39
		public string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x001185E4 File Offset: 0x001167E4
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			foreach (IUserDataDescriptor userDataDescriptor in this.m_Descriptors)
			{
				DynValue dynValue = userDataDescriptor.MetaIndex(script, obj, metaname);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00022955 File Offset: 0x00020B55
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04002DD5 RID: 11733
		private List<IUserDataDescriptor> m_Descriptors;

		// Token: 0x04002DD6 RID: 11734
		private Type m_Type;
	}
}

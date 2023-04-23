using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007D0 RID: 2000
	internal class AutoDescribingUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x060031E8 RID: 12776 RVA: 0x00022922 File Offset: 0x00020B22
		public AutoDescribingUserDataDescriptor(Type type, string friendlyName)
		{
			this.m_FriendlyName = friendlyName;
			this.m_Type = type;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x00022938 File Offset: 0x00020B38
		public string Name
		{
			get
			{
				return this.m_FriendlyName;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x00022940 File Offset: 0x00020B40
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x0010F970 File Offset: 0x0010DB70
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.Index(script, index, isDirectIndexing);
			}
			return null;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x0010F994 File Offset: 0x0010DB94
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			return userDataType != null && userDataType.SetIndex(script, index, value, isDirectIndexing);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00022948 File Offset: 0x00020B48
		public string AsString(object obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0010F9BC File Offset: 0x0010DBBC
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.MetaIndex(script, metaname);
			}
			return null;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00022955 File Offset: 0x00020B55
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04002C48 RID: 11336
		private string m_FriendlyName;

		// Token: 0x04002C49 RID: 11337
		private Type m_Type;
	}
}

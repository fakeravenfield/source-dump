using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200086E RID: 2158
	public class StandardGenericsUserDataDescriptor : IUserDataDescriptor, IGeneratorUserDataDescriptor
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x00024659 File Offset: 0x00022859
		// (set) Token: 0x060035C8 RID: 13768 RVA: 0x00024661 File Offset: 0x00022861
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x060035C9 RID: 13769 RVA: 0x0002466A File Offset: 0x0002286A
		public StandardGenericsUserDataDescriptor(Type type, InteropAccessMode accessMode)
		{
			if (accessMode == InteropAccessMode.NoReflectionAllowed)
			{
				throw new ArgumentException("Can't create a StandardGenericsUserDataDescriptor under a NoReflectionAllowed access mode");
			}
			this.AccessMode = accessMode;
			this.Type = type;
			this.Name = "@@" + type.FullName;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000246A5 File Offset: 0x000228A5
		// (set) Token: 0x060035CB RID: 13771 RVA: 0x000246AD File Offset: 0x000228AD
		public string Name { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000246B6 File Offset: 0x000228B6
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x000246BE File Offset: 0x000228BE
		public Type Type { get; private set; }

		// Token: 0x060035CE RID: 13774 RVA: 0x00002FD8 File Offset: 0x000011D8
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return null;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x0000257D File Offset: 0x0000077D
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x0001C629 File Offset: 0x0001A829
		public string AsString(object obj)
		{
			return obj.ToString();
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00002FD8 File Offset: 0x000011D8
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return null;
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00022955 File Offset: 0x00020B55
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000246C7 File Offset: 0x000228C7
		public IUserDataDescriptor Generate(Type type)
		{
			if (UserData.IsTypeRegistered(type))
			{
				return null;
			}
			if (Framework.Do.IsGenericTypeDefinition(type))
			{
				return null;
			}
			return UserData.RegisterType(type, this.AccessMode, null);
		}
	}
}

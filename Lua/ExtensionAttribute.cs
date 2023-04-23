using System;

namespace Lua
{
	// Token: 0x02000905 RID: 2309
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ExtensionAttribute : Attribute
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00027A50 File Offset: 0x00025C50
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x00027A58 File Offset: 0x00025C58
		public Type extensionType { get; private set; }

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00027A61 File Offset: 0x00025C61
		public ExtensionAttribute(Type extensionType)
		{
			this.extensionType = extensionType;
		}
	}
}

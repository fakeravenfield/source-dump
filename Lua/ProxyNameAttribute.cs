using System;

namespace Lua
{
	// Token: 0x0200090C RID: 2316
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ProxyNameAttribute : Attribute
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x00027AB1 File Offset: 0x00025CB1
		// (set) Token: 0x06003AE5 RID: 15077 RVA: 0x00027AB9 File Offset: 0x00025CB9
		public string name { get; private set; }

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00027AC2 File Offset: 0x00025CC2
		public ProxyNameAttribute(string name)
		{
			this.name = name;
		}
	}
}

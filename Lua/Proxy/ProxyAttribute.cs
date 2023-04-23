using System;

namespace Lua.Proxy
{
	// Token: 0x020009A8 RID: 2472
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ProxyAttribute : Attribute
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		// (set) Token: 0x06003F46 RID: 16198 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		public Type proxiedType { get; private set; }

		// Token: 0x06003F47 RID: 16199 RVA: 0x0002ABD9 File Offset: 0x00028DD9
		public ProxyAttribute(Type proxiedType)
		{
			this.proxiedType = proxiedType;
		}
	}
}

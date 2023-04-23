using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200083F RID: 2111
	public interface IProxyFactory
	{
		// Token: 0x0600345D RID: 13405
		object CreateProxyObject(object o);

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600345E RID: 13406
		Type TargetType { get; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600345F RID: 13407
		Type ProxyType { get; }
	}
}

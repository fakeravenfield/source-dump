using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000840 RID: 2112
	public interface IProxyFactory<TProxy, TTarget> : IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x06003460 RID: 13408
		TProxy CreateProxyObject(TTarget target);
	}
}

using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200083E RID: 2110
	public class DelegateProxyFactory<TProxy, TTarget> : IProxyFactory<TProxy, TTarget>, IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x06003458 RID: 13400 RVA: 0x00023C7B File Offset: 0x00021E7B
		public DelegateProxyFactory(Func<TTarget, TProxy> wrapDelegate)
		{
			this.wrapDelegate = wrapDelegate;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00023C8A File Offset: 0x00021E8A
		public TProxy CreateProxyObject(TTarget target)
		{
			return this.wrapDelegate(target);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00023C98 File Offset: 0x00021E98
		public object CreateProxyObject(object o)
		{
			return this.CreateProxyObject((TTarget)((object)o));
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x00023CAB File Offset: 0x00021EAB
		public Type TargetType
		{
			get
			{
				return typeof(TTarget);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x00023CB7 File Offset: 0x00021EB7
		public Type ProxyType
		{
			get
			{
				return typeof(TProxy);
			}
		}

		// Token: 0x04002DB3 RID: 11699
		private Func<TTarget, TProxy> wrapDelegate;
	}
}

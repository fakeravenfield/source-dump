using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200084B RID: 2123
	public sealed class ProxyUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x060034A8 RID: 13480 RVA: 0x00023FC6 File Offset: 0x000221C6
		internal ProxyUserDataDescriptor(IProxyFactory proxyFactory, IUserDataDescriptor proxyDescriptor, string friendlyName = null)
		{
			this.m_ProxyFactory = proxyFactory;
			this.Name = (friendlyName ?? (proxyFactory.TargetType.Name + "::proxy"));
			this.m_ProxyDescriptor = proxyDescriptor;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x00023FFC File Offset: 0x000221FC
		public IUserDataDescriptor InnerDescriptor
		{
			get
			{
				return this.m_ProxyDescriptor;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x00024004 File Offset: 0x00022204
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x0002400C File Offset: 0x0002220C
		public string Name { get; private set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x00024015 File Offset: 0x00022215
		public Type Type
		{
			get
			{
				return this.m_ProxyFactory.TargetType;
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00024022 File Offset: 0x00022222
		private object Proxy(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return this.m_ProxyFactory.CreateProxyObject(obj);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00024035 File Offset: 0x00022235
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.Index(script, this.Proxy(obj), index, isDirectIndexing);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x0002404D File Offset: 0x0002224D
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.SetIndex(script, this.Proxy(obj), index, value, isDirectIndexing);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x00024067 File Offset: 0x00022267
		public string AsString(object obj)
		{
			return this.m_ProxyDescriptor.AsString(this.Proxy(obj));
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x0002407B File Offset: 0x0002227B
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return this.m_ProxyDescriptor.MetaIndex(script, this.Proxy(obj), metaname);
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x00022955 File Offset: 0x00020B55
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04002DEA RID: 11754
		private IUserDataDescriptor m_ProxyDescriptor;

		// Token: 0x04002DEB RID: 11755
		private IProxyFactory m_ProxyFactory;
	}
}

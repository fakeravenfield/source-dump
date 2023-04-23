using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FC RID: 2556
	[Proxy(typeof(SgmLoadoutAcceptedArgs))]
	public class SgmLoadoutAcceptedArgsProxy : IProxy
	{
		// Token: 0x06004E90 RID: 20112 RVA: 0x00038F33 File Offset: 0x00037133
		[MoonSharpHidden]
		public SgmLoadoutAcceptedArgsProxy(SgmLoadoutAcceptedArgs value)
		{
			this._value = value;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x00038F42 File Offset: 0x00037142
		// (set) Token: 0x06004E92 RID: 20114 RVA: 0x00038F4F File Offset: 0x0003714F
		public bool firstTime
		{
			get
			{
				return this._value.firstTime;
			}
			set
			{
				this._value.firstTime = value;
			}
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00038F5D File Offset: 0x0003715D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00137F40 File Offset: 0x00136140
		[MoonSharpHidden]
		public static SgmLoadoutAcceptedArgsProxy New(SgmLoadoutAcceptedArgs value)
		{
			if (value == null)
			{
				return null;
			}
			SgmLoadoutAcceptedArgsProxy sgmLoadoutAcceptedArgsProxy = (SgmLoadoutAcceptedArgsProxy)ObjectCache.Get(typeof(SgmLoadoutAcceptedArgsProxy), value);
			if (sgmLoadoutAcceptedArgsProxy == null)
			{
				sgmLoadoutAcceptedArgsProxy = new SgmLoadoutAcceptedArgsProxy(value);
				ObjectCache.Add(typeof(SgmLoadoutAcceptedArgsProxy), value, sgmLoadoutAcceptedArgsProxy);
			}
			return sgmLoadoutAcceptedArgsProxy;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x00038F65 File Offset: 0x00037165
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328B RID: 12939
		[MoonSharpHidden]
		public SgmLoadoutAcceptedArgs _value;
	}
}

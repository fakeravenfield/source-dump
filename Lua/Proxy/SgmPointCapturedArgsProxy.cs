using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FE RID: 2558
	[Proxy(typeof(SgmPointCapturedArgs))]
	public class SgmPointCapturedArgsProxy : IProxy
	{
		// Token: 0x06004EA7 RID: 20135 RVA: 0x00039045 File Offset: 0x00037245
		[MoonSharpHidden]
		public SgmPointCapturedArgsProxy(SgmPointCapturedArgs value)
		{
			this._value = value;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x00039054 File Offset: 0x00037254
		// (set) Token: 0x06004EA9 RID: 20137 RVA: 0x00137FC8 File Offset: 0x001361C8
		public CapturePointProxy capturePoint
		{
			get
			{
				return CapturePointProxy.New(this._value.capturePoint);
			}
			set
			{
				CapturePoint capturePoint = null;
				if (value != null)
				{
					capturePoint = value._value;
				}
				this._value.capturePoint = capturePoint;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00039066 File Offset: 0x00037266
		// (set) Token: 0x06004EAB RID: 20139 RVA: 0x00039073 File Offset: 0x00037273
		public bool initialOwner
		{
			get
			{
				return this._value.initialOwner;
			}
			set
			{
				this._value.initialOwner = value;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x00039081 File Offset: 0x00037281
		// (set) Token: 0x06004EAD RID: 20141 RVA: 0x0003908E File Offset: 0x0003728E
		public WTeam newOwner
		{
			get
			{
				return this._value.newOwner;
			}
			set
			{
				this._value.newOwner = value;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0003909C File Offset: 0x0003729C
		// (set) Token: 0x06004EAF RID: 20143 RVA: 0x000390A9 File Offset: 0x000372A9
		public WTeam oldOwner
		{
			get
			{
				return this._value.oldOwner;
			}
			set
			{
				this._value.oldOwner = value;
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x000390B7 File Offset: 0x000372B7
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00137FF0 File Offset: 0x001361F0
		[MoonSharpHidden]
		public static SgmPointCapturedArgsProxy New(SgmPointCapturedArgs value)
		{
			if (value == null)
			{
				return null;
			}
			SgmPointCapturedArgsProxy sgmPointCapturedArgsProxy = (SgmPointCapturedArgsProxy)ObjectCache.Get(typeof(SgmPointCapturedArgsProxy), value);
			if (sgmPointCapturedArgsProxy == null)
			{
				sgmPointCapturedArgsProxy = new SgmPointCapturedArgsProxy(value);
				ObjectCache.Add(typeof(SgmPointCapturedArgsProxy), value, sgmPointCapturedArgsProxy);
			}
			return sgmPointCapturedArgsProxy;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x000390BF File Offset: 0x000372BF
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328D RID: 12941
		[MoonSharpHidden]
		public SgmPointCapturedArgs _value;
	}
}

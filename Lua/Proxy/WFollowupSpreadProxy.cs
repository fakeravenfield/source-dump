using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1A RID: 2586
	[Proxy(typeof(WFollowupSpread))]
	public class WFollowupSpreadProxy : IProxy
	{
		// Token: 0x06005244 RID: 21060 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
		[MoonSharpHidden]
		public WFollowupSpreadProxy(WFollowupSpread value)
		{
			this._value = value;
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0003CAE5 File Offset: 0x0003ACE5
		public WFollowupSpreadProxy()
		{
			this._value = default(WFollowupSpread);
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06005246 RID: 21062 RVA: 0x0003CAF9 File Offset: 0x0003ACF9
		// (set) Token: 0x06005247 RID: 21063 RVA: 0x0003CB06 File Offset: 0x0003AD06
		public float maxSpreadAim
		{
			get
			{
				return this._value.maxSpreadAim;
			}
			set
			{
				this._value.maxSpreadAim = value;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x0003CB14 File Offset: 0x0003AD14
		// (set) Token: 0x06005249 RID: 21065 RVA: 0x0003CB21 File Offset: 0x0003AD21
		public float maxSpreadHip
		{
			get
			{
				return this._value.maxSpreadHip;
			}
			set
			{
				this._value.maxSpreadHip = value;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600524A RID: 21066 RVA: 0x0003CB2F File Offset: 0x0003AD2F
		// (set) Token: 0x0600524B RID: 21067 RVA: 0x0003CB3C File Offset: 0x0003AD3C
		public float proneMultiplier
		{
			get
			{
				return this._value.proneMultiplier;
			}
			set
			{
				this._value.proneMultiplier = value;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600524C RID: 21068 RVA: 0x0003CB4A File Offset: 0x0003AD4A
		// (set) Token: 0x0600524D RID: 21069 RVA: 0x0003CB57 File Offset: 0x0003AD57
		public float spreadDissipateTime
		{
			get
			{
				return this._value.spreadDissipateTime;
			}
			set
			{
				this._value.spreadDissipateTime = value;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600524E RID: 21070 RVA: 0x0003CB65 File Offset: 0x0003AD65
		// (set) Token: 0x0600524F RID: 21071 RVA: 0x0003CB72 File Offset: 0x0003AD72
		public float spreadGain
		{
			get
			{
				return this._value.spreadGain;
			}
			set
			{
				this._value.spreadGain = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x0003CB80 File Offset: 0x0003AD80
		// (set) Token: 0x06005251 RID: 21073 RVA: 0x0003CB8D File Offset: 0x0003AD8D
		public float spreadStayTime
		{
			get
			{
				return this._value.spreadStayTime;
			}
			set
			{
				this._value.spreadStayTime = value;
			}
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0003CB9B File Offset: 0x0003AD9B
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0003CBA8 File Offset: 0x0003ADA8
		[MoonSharpHidden]
		public static WFollowupSpreadProxy New(WFollowupSpread value)
		{
			return new WFollowupSpreadProxy(value);
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		[MoonSharpUserDataMetamethod("__call")]
		public static WFollowupSpreadProxy Call(DynValue _)
		{
			return new WFollowupSpreadProxy();
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0003CBB7 File Offset: 0x0003ADB7
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A5 RID: 12965
		[MoonSharpHidden]
		public WFollowupSpread _value;
	}
}

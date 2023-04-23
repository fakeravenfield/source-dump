using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A29 RID: 2601
	[Proxy(typeof(WWaitForSeconds))]
	public class WWaitForSecondsProxy : IProxy
	{
		// Token: 0x060052F5 RID: 21237 RVA: 0x0003D248 File Offset: 0x0003B448
		[MoonSharpHidden]
		public WWaitForSecondsProxy(WWaitForSeconds value)
		{
			this._value = value;
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0003D257 File Offset: 0x0003B457
		public WWaitForSecondsProxy(float seconds)
		{
			this._value = new WWaitForSeconds(seconds);
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x0003D26B File Offset: 0x0003B46B
		// (set) Token: 0x060052F8 RID: 21240 RVA: 0x0003D278 File Offset: 0x0003B478
		public float seconds
		{
			get
			{
				return this._value.seconds;
			}
			set
			{
				this._value.seconds = value;
			}
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0003D286 File Offset: 0x0003B486
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00139664 File Offset: 0x00137864
		[MoonSharpHidden]
		public static WWaitForSecondsProxy New(WWaitForSeconds value)
		{
			if (value == null)
			{
				return null;
			}
			WWaitForSecondsProxy wwaitForSecondsProxy = (WWaitForSecondsProxy)ObjectCache.Get(typeof(WWaitForSecondsProxy), value);
			if (wwaitForSecondsProxy == null)
			{
				wwaitForSecondsProxy = new WWaitForSecondsProxy(value);
				ObjectCache.Add(typeof(WWaitForSecondsProxy), value, wwaitForSecondsProxy);
			}
			return wwaitForSecondsProxy;
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0003D28E File Offset: 0x0003B48E
		[MoonSharpUserDataMetamethod("__call")]
		public static WWaitForSecondsProxy Call(DynValue _, float seconds)
		{
			return new WWaitForSecondsProxy(seconds);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0003D296 File Offset: 0x0003B496
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A7 RID: 12967
		[MoonSharpHidden]
		public WWaitForSeconds _value;
	}
}

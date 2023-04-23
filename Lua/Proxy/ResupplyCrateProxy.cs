using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009F1 RID: 2545
	[Proxy(typeof(ResupplyCrate))]
	public class ResupplyCrateProxy : IProxy
	{
		// Token: 0x06004D7E RID: 19838 RVA: 0x00037E1D File Offset: 0x0003601D
		[MoonSharpHidden]
		public ResupplyCrateProxy(ResupplyCrate value)
		{
			this._value = value;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06004D7F RID: 19839 RVA: 0x00037E2C File Offset: 0x0003602C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x00037E3E File Offset: 0x0003603E
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x00037E50 File Offset: 0x00036050
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x00137B20 File Offset: 0x00135D20
		[MoonSharpHidden]
		public static ResupplyCrateProxy New(ResupplyCrate value)
		{
			if (value == null)
			{
				return null;
			}
			ResupplyCrateProxy resupplyCrateProxy = (ResupplyCrateProxy)ObjectCache.Get(typeof(ResupplyCrateProxy), value);
			if (resupplyCrateProxy == null)
			{
				resupplyCrateProxy = new ResupplyCrateProxy(value);
				ObjectCache.Add(typeof(ResupplyCrateProxy), value, resupplyCrateProxy);
			}
			return resupplyCrateProxy;
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x00037E58 File Offset: 0x00036058
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003281 RID: 12929
		[MoonSharpHidden]
		public ResupplyCrate _value;
	}
}

using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009F7 RID: 2551
	[Proxy(typeof(ScriptEvent))]
	public class ScriptEventProxy : IProxy
	{
		// Token: 0x06004E2A RID: 20010 RVA: 0x000389A9 File Offset: 0x00036BA9
		[MoonSharpHidden]
		public ScriptEventProxy(ScriptEvent value)
		{
			this._value = value;
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x000389B8 File Offset: 0x00036BB8
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x00137D3C File Offset: 0x00135F3C
		[MoonSharpHidden]
		public static ScriptEventProxy New(ScriptEvent value)
		{
			if (value == null)
			{
				return null;
			}
			ScriptEventProxy scriptEventProxy = (ScriptEventProxy)ObjectCache.Get(typeof(ScriptEventProxy), value);
			if (scriptEventProxy == null)
			{
				scriptEventProxy = new ScriptEventProxy(value);
				ObjectCache.Add(typeof(ScriptEventProxy), value, scriptEventProxy);
			}
			return scriptEventProxy;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x000389C0 File Offset: 0x00036BC0
		public void AddListener(DynValue script, string methodName)
		{
			this._value.AddListener(script, methodName);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x000389CF File Offset: 0x00036BCF
		public void AddListener(DynValue script, string methodName, DynValue listenerData)
		{
			this._value.AddListener(script, methodName, listenerData);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x000389DF File Offset: 0x00036BDF
		public void RemoveListener(DynValue script, string methodName)
		{
			this._value.RemoveListener(script, methodName);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x000389EE File Offset: 0x00036BEE
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003286 RID: 12934
		[MoonSharpHidden]
		public ScriptEvent _value;
	}
}

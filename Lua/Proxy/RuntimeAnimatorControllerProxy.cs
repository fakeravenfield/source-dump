using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009F5 RID: 2549
	[Proxy(typeof(RuntimeAnimatorController))]
	public class RuntimeAnimatorControllerProxy : IProxy
	{
		// Token: 0x06004E1D RID: 19997 RVA: 0x0003894F File Offset: 0x00036B4F
		[MoonSharpHidden]
		public RuntimeAnimatorControllerProxy(RuntimeAnimatorController value)
		{
			this._value = value;
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x0003895E File Offset: 0x00036B5E
		// (set) Token: 0x06004E1F RID: 19999 RVA: 0x0003896B File Offset: 0x00036B6B
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00038979 File Offset: 0x00036B79
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00137CF0 File Offset: 0x00135EF0
		[MoonSharpHidden]
		public static RuntimeAnimatorControllerProxy New(RuntimeAnimatorController value)
		{
			if (value == null)
			{
				return null;
			}
			RuntimeAnimatorControllerProxy runtimeAnimatorControllerProxy = (RuntimeAnimatorControllerProxy)ObjectCache.Get(typeof(RuntimeAnimatorControllerProxy), value);
			if (runtimeAnimatorControllerProxy == null)
			{
				runtimeAnimatorControllerProxy = new RuntimeAnimatorControllerProxy(value);
				ObjectCache.Add(typeof(RuntimeAnimatorControllerProxy), value, runtimeAnimatorControllerProxy);
			}
			return runtimeAnimatorControllerProxy;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x00038981 File Offset: 0x00036B81
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0003898E File Offset: 0x00036B8E
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003285 RID: 12933
		[MoonSharpHidden]
		public RuntimeAnimatorController _value;
	}
}

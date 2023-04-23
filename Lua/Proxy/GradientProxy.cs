using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009C8 RID: 2504
	[Proxy(typeof(Gradient))]
	public class GradientProxy : IProxy
	{
		// Token: 0x060044EA RID: 17642 RVA: 0x0002FE35 File Offset: 0x0002E035
		[MoonSharpHidden]
		public GradientProxy(Gradient value)
		{
			this._value = value;
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x0002FE44 File Offset: 0x0002E044
		public GradientProxy()
		{
			this._value = new Gradient();
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x0002FE57 File Offset: 0x0002E057
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00130954 File Offset: 0x0012EB54
		[MoonSharpHidden]
		public static GradientProxy New(Gradient value)
		{
			if (value == null)
			{
				return null;
			}
			GradientProxy gradientProxy = (GradientProxy)ObjectCache.Get(typeof(GradientProxy), value);
			if (gradientProxy == null)
			{
				gradientProxy = new GradientProxy(value);
				ObjectCache.Add(typeof(GradientProxy), value, gradientProxy);
			}
			return gradientProxy;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0002FE5F File Offset: 0x0002E05F
		[MoonSharpUserDataMetamethod("__call")]
		public static GradientProxy Call(DynValue _)
		{
			return new GradientProxy();
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0002FE66 File Offset: 0x0002E066
		public ColorProxy Evaluate(float time)
		{
			return ColorProxy.New(this._value.Evaluate(time));
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x0002FE79 File Offset: 0x0002E079
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x0002FE86 File Offset: 0x0002E086
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003160 RID: 12640
		[MoonSharpHidden]
		public Gradient _value;
	}
}

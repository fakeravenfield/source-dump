using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009D8 RID: 2520
	[Proxy(typeof(MeanFilterVector3))]
	public class MeanFilterVector3Proxy : IProxy
	{
		// Token: 0x060047FA RID: 18426 RVA: 0x00032865 File Offset: 0x00030A65
		[MoonSharpHidden]
		public MeanFilterVector3Proxy(MeanFilterVector3 value)
		{
			this._value = value;
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x00032874 File Offset: 0x00030A74
		public MeanFilterVector3Proxy(int taps)
		{
			this._value = new MeanFilterVector3(taps);
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x00032888 File Offset: 0x00030A88
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0013143C File Offset: 0x0012F63C
		[MoonSharpHidden]
		public static MeanFilterVector3Proxy New(MeanFilterVector3 value)
		{
			if (value == null)
			{
				return null;
			}
			MeanFilterVector3Proxy meanFilterVector3Proxy = (MeanFilterVector3Proxy)ObjectCache.Get(typeof(MeanFilterVector3Proxy), value);
			if (meanFilterVector3Proxy == null)
			{
				meanFilterVector3Proxy = new MeanFilterVector3Proxy(value);
				ObjectCache.Add(typeof(MeanFilterVector3Proxy), value, meanFilterVector3Proxy);
			}
			return meanFilterVector3Proxy;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00032890 File Offset: 0x00030A90
		[MoonSharpUserDataMetamethod("__call")]
		public static MeanFilterVector3Proxy Call(DynValue _, int taps)
		{
			return new MeanFilterVector3Proxy(taps);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00032898 File Offset: 0x00030A98
		public Vector3Proxy Tick(Vector3Proxy input)
		{
			if (input == null)
			{
				throw new ScriptRuntimeException("argument 'input' is nil");
			}
			return Vector3Proxy.New(this._value.Tick(input._value));
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x000328BE File Offset: 0x00030ABE
		public Vector3Proxy Value()
		{
			return Vector3Proxy.New(this._value.Value());
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x000328D0 File Offset: 0x00030AD0
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003170 RID: 12656
		[MoonSharpHidden]
		public MeanFilterVector3 _value;
	}
}

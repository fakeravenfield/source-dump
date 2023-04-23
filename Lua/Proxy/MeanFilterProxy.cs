using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009D7 RID: 2519
	[Proxy(typeof(MeanFilter))]
	public class MeanFilterProxy : IProxy
	{
		// Token: 0x060047F2 RID: 18418 RVA: 0x0003280A File Offset: 0x00030A0A
		[MoonSharpHidden]
		public MeanFilterProxy(MeanFilter value)
		{
			this._value = value;
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x00032819 File Offset: 0x00030A19
		public MeanFilterProxy(int taps)
		{
			this._value = new MeanFilter(taps);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0003282D File Offset: 0x00030A2D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x001313F8 File Offset: 0x0012F5F8
		[MoonSharpHidden]
		public static MeanFilterProxy New(MeanFilter value)
		{
			if (value == null)
			{
				return null;
			}
			MeanFilterProxy meanFilterProxy = (MeanFilterProxy)ObjectCache.Get(typeof(MeanFilterProxy), value);
			if (meanFilterProxy == null)
			{
				meanFilterProxy = new MeanFilterProxy(value);
				ObjectCache.Add(typeof(MeanFilterProxy), value, meanFilterProxy);
			}
			return meanFilterProxy;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x00032835 File Offset: 0x00030A35
		[MoonSharpUserDataMetamethod("__call")]
		public static MeanFilterProxy Call(DynValue _, int taps)
		{
			return new MeanFilterProxy(taps);
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x0003283D File Offset: 0x00030A3D
		public float Tick(float input)
		{
			return this._value.Tick(input);
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0003284B File Offset: 0x00030A4B
		public float Value()
		{
			return this._value.Value();
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00032858 File Offset: 0x00030A58
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400316F RID: 12655
		[MoonSharpHidden]
		public MeanFilter _value;
	}
}

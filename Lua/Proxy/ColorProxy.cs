using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009BE RID: 2494
	[Proxy(typeof(Color))]
	public class ColorProxy : IProxy
	{
		// Token: 0x06004399 RID: 17305 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		[MoonSharpHidden]
		public ColorProxy(Color value)
		{
			this._value = value;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x0002EB2D File Offset: 0x0002CD2D
		public ColorProxy(float r, float g, float b, float a)
		{
			this._value = new Color(r, g, b, a);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x0002EB45 File Offset: 0x0002CD45
		public ColorProxy(float r, float g, float b)
		{
			this._value = new Color(r, g, b);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x0002EB5B File Offset: 0x0002CD5B
		public ColorProxy()
		{
			this._value = default(Color);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600439D RID: 17309 RVA: 0x0002EB6F File Offset: 0x0002CD6F
		// (set) Token: 0x0600439E RID: 17310 RVA: 0x0002EB7C File Offset: 0x0002CD7C
		public float a
		{
			get
			{
				return this._value.a;
			}
			set
			{
				this._value.a = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x0002EB8A File Offset: 0x0002CD8A
		// (set) Token: 0x060043A0 RID: 17312 RVA: 0x0002EB97 File Offset: 0x0002CD97
		public float b
		{
			get
			{
				return this._value.b;
			}
			set
			{
				this._value.b = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x0002EBA5 File Offset: 0x0002CDA5
		// (set) Token: 0x060043A2 RID: 17314 RVA: 0x0002EBB2 File Offset: 0x0002CDB2
		public float g
		{
			get
			{
				return this._value.g;
			}
			set
			{
				this._value.g = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
		// (set) Token: 0x060043A4 RID: 17316 RVA: 0x0002EBCD File Offset: 0x0002CDCD
		public float r
		{
			get
			{
				return this._value.r;
			}
			set
			{
				this._value.r = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x0002EBDB File Offset: 0x0002CDDB
		public static ColorProxy black
		{
			get
			{
				return ColorProxy.New(Color.black);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x0002EBE7 File Offset: 0x0002CDE7
		public static ColorProxy blue
		{
			get
			{
				return ColorProxy.New(Color.blue);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x0002EBF3 File Offset: 0x0002CDF3
		public static ColorProxy clear
		{
			get
			{
				return ColorProxy.New(Color.clear);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x0002EBFF File Offset: 0x0002CDFF
		public static ColorProxy cyan
		{
			get
			{
				return ColorProxy.New(Color.cyan);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x0002EC0B File Offset: 0x0002CE0B
		public ColorProxy gamma
		{
			get
			{
				return ColorProxy.New(this._value.gamma);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x0002EC1D File Offset: 0x0002CE1D
		public static ColorProxy gray
		{
			get
			{
				return ColorProxy.New(Color.gray);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0002EC29 File Offset: 0x0002CE29
		public float grayscale
		{
			get
			{
				return this._value.grayscale;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x0002EC36 File Offset: 0x0002CE36
		public static ColorProxy green
		{
			get
			{
				return ColorProxy.New(Color.green);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x0002EC42 File Offset: 0x0002CE42
		public static ColorProxy grey
		{
			get
			{
				return ColorProxy.New(Color.grey);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x0002EC4E File Offset: 0x0002CE4E
		public ColorProxy linear
		{
			get
			{
				return ColorProxy.New(this._value.linear);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x0002EC60 File Offset: 0x0002CE60
		public static ColorProxy magenta
		{
			get
			{
				return ColorProxy.New(Color.magenta);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x0002EC6C File Offset: 0x0002CE6C
		public float maxColorComponent
		{
			get
			{
				return this._value.maxColorComponent;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x0002EC79 File Offset: 0x0002CE79
		public static ColorProxy red
		{
			get
			{
				return ColorProxy.New(Color.red);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x0002EC85 File Offset: 0x0002CE85
		public static ColorProxy white
		{
			get
			{
				return ColorProxy.New(Color.white);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x0002EC91 File Offset: 0x0002CE91
		public static ColorProxy yellow
		{
			get
			{
				return ColorProxy.New(Color.yellow);
			}
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x0002EC9D File Offset: 0x0002CE9D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0002ECAA File Offset: 0x0002CEAA
		[MoonSharpHidden]
		public static ColorProxy New(Color value)
		{
			return new ColorProxy(value);
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x0002ECB2 File Offset: 0x0002CEB2
		[MoonSharpUserDataMetamethod("__call")]
		public static ColorProxy Call(DynValue _, float r, float g, float b, float a)
		{
			return new ColorProxy(r, g, b, a);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0002ECBE File Offset: 0x0002CEBE
		[MoonSharpUserDataMetamethod("__call")]
		public static ColorProxy Call(DynValue _, float r, float g, float b)
		{
			return new ColorProxy(r, g, b);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		[MoonSharpUserDataMetamethod("__call")]
		public static ColorProxy Call(DynValue _)
		{
			return new ColorProxy();
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0002ECCF File Offset: 0x0002CECF
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x0002ECE2 File Offset: 0x0002CEE2
		public static ColorProxy HSVToRGB(float H, float S, float V)
		{
			return ColorProxy.New(Color.HSVToRGB(H, S, V));
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x0002ECF1 File Offset: 0x0002CEF1
		public static ColorProxy HSVToRGB(float H, float S, float V, bool hdr)
		{
			return ColorProxy.New(Color.HSVToRGB(H, S, V, hdr));
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x0002ED01 File Offset: 0x0002CF01
		public static ColorProxy Lerp(ColorProxy a, ColorProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return ColorProxy.New(Color.Lerp(a._value, b._value, t));
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x0002ED36 File Offset: 0x0002CF36
		public static ColorProxy LerpUnclamped(ColorProxy a, ColorProxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return ColorProxy.New(Color.LerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0002ED6B File Offset: 0x0002CF6B
		[MoonSharpUserDataMetamethod("__add")]
		public static ColorProxy operator +(ColorProxy a, ColorProxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return ColorProxy.New(a._value + b._value);
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x0002ED9F File Offset: 0x0002CF9F
		[MoonSharpUserDataMetamethod("__div")]
		public static ColorProxy operator /(ColorProxy a, float b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return ColorProxy.New(a._value / b);
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(ColorProxy lhs, ColorProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value == rhs._value;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x0002EDEF File Offset: 0x0002CFEF
		[MoonSharpHidden]
		public static bool operator !=(ColorProxy lhs, ColorProxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return lhs._value != rhs._value;
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x0002EE1E File Offset: 0x0002D01E
		[MoonSharpUserDataMetamethod("__mul")]
		public static ColorProxy operator *(ColorProxy a, ColorProxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return ColorProxy.New(a._value * b._value);
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x0002EE52 File Offset: 0x0002D052
		[MoonSharpUserDataMetamethod("__mul")]
		public static ColorProxy operator *(ColorProxy a, float b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return ColorProxy.New(a._value * b);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x0002EE73 File Offset: 0x0002D073
		[MoonSharpUserDataMetamethod("__mul")]
		public static ColorProxy operator *(float b, ColorProxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return ColorProxy.New(b * a._value);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x0002EE94 File Offset: 0x0002D094
		[MoonSharpUserDataMetamethod("__sub")]
		public static ColorProxy operator -(ColorProxy a, ColorProxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return ColorProxy.New(a._value - b._value);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		public static void RGBToHSV(ColorProxy rgbColor, out float H, out float S, out float V)
		{
			if (rgbColor == null)
			{
				throw new ScriptRuntimeException("argument 'rgbColor' is nil");
			}
			H = 0f;
			S = 0f;
			V = 0f;
			Color.RGBToHSV(rgbColor._value, out H, out S, out V);
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0002EEFB File Offset: 0x0002D0FB
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x0002EF0E File Offset: 0x0002D10E
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x04003157 RID: 12631
		[MoonSharpHidden]
		public Color _value;
	}
}

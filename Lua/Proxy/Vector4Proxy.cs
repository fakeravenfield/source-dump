using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A11 RID: 2577
	[Proxy(typeof(Vector4))]
	public class Vector4Proxy : IProxy
	{
		// Token: 0x06005133 RID: 20787 RVA: 0x0003BB0D File Offset: 0x00039D0D
		[MoonSharpHidden]
		public Vector4Proxy(Vector4 value)
		{
			this._value = value;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public Vector4Proxy(float x, float y, float z, float w)
		{
			this._value = new Vector4(x, y, z, w);
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0003BB34 File Offset: 0x00039D34
		public Vector4Proxy(float x, float y, float z)
		{
			this._value = new Vector4(x, y, z);
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0003BB4A File Offset: 0x00039D4A
		public Vector4Proxy(float x, float y)
		{
			this._value = new Vector4(x, y);
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x0003BB5F File Offset: 0x00039D5F
		public Vector4Proxy()
		{
			this._value = default(Vector4);
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x0003AE53 File Offset: 0x00039053
		public static float kEpsilon
		{
			get
			{
				return 1E-05f;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06005139 RID: 20793 RVA: 0x0003BB73 File Offset: 0x00039D73
		// (set) Token: 0x0600513A RID: 20794 RVA: 0x0003BB80 File Offset: 0x00039D80
		public float w
		{
			get
			{
				return this._value.w;
			}
			set
			{
				this._value.w = value;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x0003BB8E File Offset: 0x00039D8E
		// (set) Token: 0x0600513C RID: 20796 RVA: 0x0003BB9B File Offset: 0x00039D9B
		public float x
		{
			get
			{
				return this._value.x;
			}
			set
			{
				this._value.x = value;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600513D RID: 20797 RVA: 0x0003BBA9 File Offset: 0x00039DA9
		// (set) Token: 0x0600513E RID: 20798 RVA: 0x0003BBB6 File Offset: 0x00039DB6
		public float y
		{
			get
			{
				return this._value.y;
			}
			set
			{
				this._value.y = value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x0003BBC4 File Offset: 0x00039DC4
		// (set) Token: 0x06005140 RID: 20800 RVA: 0x0003BBD1 File Offset: 0x00039DD1
		public float z
		{
			get
			{
				return this._value.z;
			}
			set
			{
				this._value.z = value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06005141 RID: 20801 RVA: 0x0003BBDF File Offset: 0x00039DDF
		public float magnitude
		{
			get
			{
				return this._value.magnitude;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x0003BBEC File Offset: 0x00039DEC
		public static Vector4Proxy negativeInfinity
		{
			get
			{
				return Vector4Proxy.New(Vector4.negativeInfinity);
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x0003BBF8 File Offset: 0x00039DF8
		public Vector4Proxy normalized
		{
			get
			{
				return Vector4Proxy.New(this._value.normalized);
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x0003BC0A File Offset: 0x00039E0A
		public static Vector4Proxy one
		{
			get
			{
				return Vector4Proxy.New(Vector4.one);
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x0003BC16 File Offset: 0x00039E16
		public static Vector4Proxy positiveInfinity
		{
			get
			{
				return Vector4Proxy.New(Vector4.positiveInfinity);
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x0003BC22 File Offset: 0x00039E22
		public float sqrMagnitude
		{
			get
			{
				return this._value.sqrMagnitude;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x0003BC2F File Offset: 0x00039E2F
		public static Vector4Proxy zero
		{
			get
			{
				return Vector4Proxy.New(Vector4.zero);
			}
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x0003BC3B File Offset: 0x00039E3B
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0003BC48 File Offset: 0x00039E48
		[MoonSharpHidden]
		public static Vector4Proxy New(Vector4 value)
		{
			return new Vector4Proxy(value);
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x0003BC50 File Offset: 0x00039E50
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector4Proxy Call(DynValue _, float x, float y, float z, float w)
		{
			return new Vector4Proxy(x, y, z, w);
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0003BC5C File Offset: 0x00039E5C
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector4Proxy Call(DynValue _, float x, float y, float z)
		{
			return new Vector4Proxy(x, y, z);
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0003BC66 File Offset: 0x00039E66
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector4Proxy Call(DynValue _, float x, float y)
		{
			return new Vector4Proxy(x, y);
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0003BC6F File Offset: 0x00039E6F
		[MoonSharpUserDataMetamethod("__call")]
		public static Vector4Proxy Call(DynValue _)
		{
			return new Vector4Proxy();
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0003BC76 File Offset: 0x00039E76
		public static float Distance(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4.Distance(a._value, b._value);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0003BCA5 File Offset: 0x00039EA5
		public static float Dot(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4.Dot(a._value, b._value);
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0003BCE7 File Offset: 0x00039EE7
		public static Vector4Proxy Lerp(Vector4Proxy a, Vector4Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(Vector4.Lerp(a._value, b._value, t));
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0003BD1C File Offset: 0x00039F1C
		public static Vector4Proxy LerpUnclamped(Vector4Proxy a, Vector4Proxy b, float t)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(Vector4.LerpUnclamped(a._value, b._value, t));
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0003BD51 File Offset: 0x00039F51
		public static float Magnitude(Vector4Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4.Magnitude(a._value);
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0003BD6C File Offset: 0x00039F6C
		public static Vector4Proxy Max(Vector4Proxy lhs, Vector4Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector4Proxy.New(Vector4.Max(lhs._value, rhs._value));
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		public static Vector4Proxy Min(Vector4Proxy lhs, Vector4Proxy rhs)
		{
			if (lhs == null)
			{
				throw new ScriptRuntimeException("argument 'lhs' is nil");
			}
			if (rhs == null)
			{
				throw new ScriptRuntimeException("argument 'rhs' is nil");
			}
			return Vector4Proxy.New(Vector4.Min(lhs._value, rhs._value));
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0003BDD4 File Offset: 0x00039FD4
		public static Vector4Proxy MoveTowards(Vector4Proxy current, Vector4Proxy target, float maxDistanceDelta)
		{
			if (current == null)
			{
				throw new ScriptRuntimeException("argument 'current' is nil");
			}
			if (target == null)
			{
				throw new ScriptRuntimeException("argument 'target' is nil");
			}
			return Vector4Proxy.New(Vector4.MoveTowards(current._value, target._value, maxDistanceDelta));
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0003BE09 File Offset: 0x0003A009
		public void Normalize()
		{
			this._value.Normalize();
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0003BE16 File Offset: 0x0003A016
		public static Vector4Proxy Normalize(Vector4Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4Proxy.New(Vector4.Normalize(a._value));
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0003BE36 File Offset: 0x0003A036
		[MoonSharpUserDataMetamethod("__add")]
		public static Vector4Proxy operator +(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(a._value + b._value);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0003BE6A File Offset: 0x0003A06A
		[MoonSharpUserDataMetamethod("__div")]
		public static Vector4Proxy operator /(Vector4Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4Proxy.New(a._value / d);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x0003BE8B File Offset: 0x0003A08B
		[MoonSharpUserDataMetamethod("__eq")]
		public static bool operator ==(Vector4Proxy lhs, Vector4Proxy rhs)
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

		// Token: 0x0600515C RID: 20828 RVA: 0x0003BEBA File Offset: 0x0003A0BA
		[MoonSharpHidden]
		public static bool operator !=(Vector4Proxy lhs, Vector4Proxy rhs)
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

		// Token: 0x0600515D RID: 20829 RVA: 0x0003BEE9 File Offset: 0x0003A0E9
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector4Proxy operator *(Vector4Proxy a, float d)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4Proxy.New(a._value * d);
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0003BF0A File Offset: 0x0003A10A
		[MoonSharpUserDataMetamethod("__mul")]
		public static Vector4Proxy operator *(float d, Vector4Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4Proxy.New(d * a._value);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0003BF2B File Offset: 0x0003A12B
		[MoonSharpUserDataMetamethod("__sub")]
		public static Vector4Proxy operator -(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(a._value - b._value);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0003BF5F File Offset: 0x0003A15F
		[MoonSharpUserDataMetamethod("__unm")]
		public static Vector4Proxy operator -(Vector4Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4Proxy.New(-a._value);
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x0003BF7F File Offset: 0x0003A17F
		public static Vector4Proxy Project(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(Vector4.Project(a._value, b._value));
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0003BFB3 File Offset: 0x0003A1B3
		public void Scale(Vector4Proxy scale)
		{
			if (scale == null)
			{
				throw new ScriptRuntimeException("argument 'scale' is nil");
			}
			this._value.Scale(scale._value);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		public static Vector4Proxy Scale(Vector4Proxy a, Vector4Proxy b)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			if (b == null)
			{
				throw new ScriptRuntimeException("argument 'b' is nil");
			}
			return Vector4Proxy.New(Vector4.Scale(a._value, b._value));
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0003C008 File Offset: 0x0003A208
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this._value.Set(newX, newY, newZ, newW);
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0003C01A File Offset: 0x0003A21A
		public float SqrMagnitude()
		{
			return this._value.SqrMagnitude();
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0003C027 File Offset: 0x0003A227
		public static float SqrMagnitude(Vector4Proxy a)
		{
			if (a == null)
			{
				throw new ScriptRuntimeException("argument 'a' is nil");
			}
			return Vector4.SqrMagnitude(a._value);
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0003C042 File Offset: 0x0003A242
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0003C055 File Offset: 0x0003A255
		public string ToString(string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x040032A0 RID: 12960
		[MoonSharpHidden]
		public Vector4 _value;
	}
}
